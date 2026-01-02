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

        public AttributeContainer()
        {
            foreach(AttributeType attrType in Enum.GetValues(typeof(AttributeType)))
            {
                AttributeValueMap[attrType] = new AttributeValue();
            }
        }

        public void RecalculateAllAttributes()
        {
            // 重置累加值
            foreach (AttributeValue attr in AttributeValueMap.Values)
            {
                attr.FlatBonus = 0;
                attr.AdditivePercent = 0;
                attr.Multiplicative = 0;
            }

            // 应用所有 Modifier
            foreach (Modifier mod in ModifierList)
            {
                if (mod.TargetValue != null)
                    ModifierApplier.ApplyModifier(mod.TargetValue, mod);
            }

            // 最后计算主属性之外的所有属性最终值
            foreach (KeyValuePair<AttributeType, AttributeValue> pair in AttributeValueMap)
            {
                pair.Value.Recalculate();
            }
        }


        public void AddModifier(Modifier modifier) => ModifierList.Add(modifier);
        public void RemoveModifiersBySource(ModifierSource source) => ModifierList.RemoveAll(m => m.ModifierSource == source);
        public void RemoveModifierBySourceRef(object sourceRef) => ModifierList.RemoveAll(m => m.SourceRef == sourceRef);
        //public void SetAttrBaseValue(AttributeType attrType, float baseValue)
        //{
        //    AttributeValueMap[attrType].BaseValue = baseValue;
        //}
        //public float GetAttrFinalValue(AttributeType type) => AttributeValueMap.TryGetValue(type, out AttributeValue attr) ? attr.FinalValue : 0f;
        public AttributeValue GetAttrValue(AttributeType type) => AttributeValueMap[type];
    }


}
