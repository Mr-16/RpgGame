using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.AttributeSystem
{


    public enum AttributeType
    {
        Def,        //金
        HpRegen,    //木
        EnergyRegen, 
        MoveSpeed,
        MaxEnergy,      //水
        Atk,        //火
        MaxHp,      //土
    }

    public class AttributeValue
    {
        public float BaseValue;     //加成前的基础值
        public float FinalValue;    //加成后的最终值缓存

        public float FlatBonus;
        public float AdditivePercent;
        public float Multiplicative;

        public float MinValue = float.MinValue;
        public float MaxValue = float.MaxValue;


        //公式 : FinalValue = (BaseValue + FlatBonus) * (1 + AdditivePercent) * (1 + Multiplicative)
        public void Recalculate()
        {
            FinalValue = (BaseValue + FlatBonus) * (1 + AdditivePercent) * (1 + Multiplicative);
            FinalValue = MathF.Max(FinalValue, MinValue);
            FinalValue = MathF.Min(FinalValue, MaxValue);
        }
    }

    public enum ModifierType
    {
        Flat,           // +50
        AddPercent,     // +20%
        MulPercent,     // ×1.5
    }

    public enum ModifierSource
    {
        Equipment,   // 装备
        Skill,       // 技能
        Buff,        // 临时状态
        Passive,     // 被动技能
        Aura,        // 光环效果
        Environment  // 场景/环境效果
    }

    public class Modifier
    {
        public AttributeValue TargetValue;     // 要修改的属性值
        public ModifierType ModifierType;      // 修改器类型
        public ModifierSource ModifierSource;  // 修改器来源(大类)
        public object SourceRef;               // 修改器来源(具体实例，用于精确移除)

        public float Value;                    // Modifier 的值
    }

    public static class ModifierApplier
    {
        public static void ApplyModifier(AttributeValue attribute, Modifier modifier)
        {
            switch (modifier.ModifierType)
            {
                case ModifierType.Flat:
                    attribute.FlatBonus += modifier.Value;
                    break;
                case ModifierType.AddPercent:
                    attribute.AdditivePercent += modifier.Value;
                    break;
                case ModifierType.MulPercent:
                    attribute.Multiplicative += modifier.Value;
                    break;
            }
        }
    }
}
