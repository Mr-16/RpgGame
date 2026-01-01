using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.AttributeSystem
{
    public enum AttributeType
    {
        //Primary
        Strength,
        Dexterity,
        Intelligence,
        Vitality,
        
        //Secondary
        MaxHp,
        HpRegen,
        MaxMp,
        MpRegen,
        DodgeChance,
        PhysicalAttackMin,
        PhysicalAttackMax,
        MagicAttack,             //吃智力, 技能伤害加成
        MagicResistance,         //吃智力, 技能伤害抗性
        Armor,
        CritChance,
        CritDamage,
        AttackSpeed,
        MoveSpeed,

        //Combat
        DamagePercent,              //最后百分比增伤
        DamageReductionPercent,     //最后百分比减伤
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

    public class AttributeConversion
    {
        public AttributeType From;   // Primary 属性
        public AttributeType To;     // Secondary 属性
        public float Ratio;          // 转换比例，例如 1 STR -> 2 PhysicalAttackMin
    }

}
