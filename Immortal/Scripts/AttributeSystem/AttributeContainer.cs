using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.AttributeSystem
{
    public class AttributeContainer
    {
        public Dictionary<AttributeType, AttributeValue> AttributeValueMap = new Dictionary<AttributeType, AttributeValue>();
        public List<Modifier> ModifierList = new List<Modifier>();
        public List<AttributeConversion> ConversionList = new List<AttributeConversion>();

        public AttributeContainer()
        {
            foreach(AttributeType attrType in Enum.GetValues(typeof(AttributeType)))
            {
                AttributeValueMap[attrType] = new AttributeValue();
            }

            ConversionList.Add(new AttributeConversion() { From = AttributeType.Strength, To = AttributeType.PhysicalAttackMin, Ratio = 1f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Strength, To = AttributeType.PhysicalAttackMax, Ratio = 1.5f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Strength, To = AttributeType.MaxHp, Ratio = 10f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Strength, To = AttributeType.CritDamage, Ratio = 0.01f });

            ConversionList.Add(new AttributeConversion() { From = AttributeType.Dexterity, To = AttributeType.DodgeChance, Ratio = 0.001f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Dexterity, To = AttributeType.MoveSpeed, Ratio = 0.01f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Dexterity, To = AttributeType.CritChance, Ratio = 0.001f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Dexterity, To = AttributeType.AttackSpeed, Ratio = 0.005f });

            ConversionList.Add(new AttributeConversion() { From = AttributeType.Intelligence, To = AttributeType.MaxMp, Ratio = 5f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Intelligence, To = AttributeType.MpRegen, Ratio = 0.2f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Intelligence, To = AttributeType.MagicAttack, Ratio = 0.5f });

            ConversionList.Add(new AttributeConversion() { From = AttributeType.Vitality, To = AttributeType.Armor, Ratio = 0.5f });
            ConversionList.Add(new AttributeConversion() { From = AttributeType.Vitality, To = AttributeType.HpRegen, Ratio = 1f });

        }

        //这个功能是计算mod的生命周期的
        public void Update(float delta)
        {
            bool needsRecalc = false;
            for (int i = ModifierList.Count - 1; i >= 0; i--)
            {
                Modifier mod = ModifierList[i];
                if (mod.Duration >= 0)
                {
                    mod.ElapsedTime += delta;
                    if (mod.IsExpired)
                    {
                        ModifierList.RemoveAt(i);
                        needsRecalc = true;
                    }
                }
            }
            if (needsRecalc) RecalculateAllAttributes();
        }

        public void RecalculateAllAttributes()
        {
            // 1. 重置累加值
            foreach (AttributeValue attr in AttributeValueMap.Values)
            {
                attr.FlatBonus = 0;
                attr.AdditivePercent = 0;
                attr.Multiplicative = 0;
            }

            // 2. 应用所有 Modifier
            foreach (var mod in ModifierList)
            {
                if (mod.TargetValue != null)
                    ModifierApplier.ApplyModifier(mod.TargetValue, mod);
            }

            // 3. 先计算主属性最终值
            AttributeValueMap[AttributeType.Strength].Recalculate();
            AttributeValueMap[AttributeType.Dexterity].Recalculate();
            AttributeValueMap[AttributeType.Intelligence].Recalculate();
            AttributeValueMap[AttributeType.Vitality].Recalculate();

            // 4. 主属性 -> 二级属性转换
            foreach (var conv in ConversionList)
            {
                if (AttributeValueMap.TryGetValue(conv.From, out var fromAttr) && AttributeValueMap.TryGetValue(conv.To, out var toAttr))
                {
                    toAttr.FlatBonus += fromAttr.FinalValue * conv.Ratio;
                }
            }

            // 5. 最后计算所有属性最终值
            foreach (var attr in AttributeValueMap.Values)
                attr.Recalculate();
        }


        public void AddModifier(Modifier modifier) => ModifierList.Add(modifier);
        public void RemoveModifiersBySource(ModifierSource source) => ModifierList.RemoveAll(m => m.ModifierSource == source);
        public void RemoveModifierBySourceRef(object sourceRef) => ModifierList.RemoveAll(m => m.SourceRef == sourceRef);
        public float GetAttributeFinalValue(AttributeType type) => AttributeValueMap.TryGetValue(type, out var attr) ? attr.FinalValue : 0f;
    }


}
