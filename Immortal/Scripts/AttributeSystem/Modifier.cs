using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.AttributeSystem
{
    public enum ModifierType
    {
        Flat,           // +50
        AddPercent,     // +20%
        MulPercent,     // ×1.5
        Override        // 设置为固定值（极少）
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
        public int Priority;                   // 冲突时优先级（Override等特殊Modifier使用）

        public float Duration = -1f;             // 持续时间，-1 表示永久
        public float ElapsedTime = 0f;           // 已经过的时间
        public bool IsExpired => Duration >= 0 && ElapsedTime >= Duration; // 是否过期
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
                case ModifierType.Override:
                    if (modifier.Priority >= 0)
                        attribute.BaseValue = modifier.Value;
                    break;
            }
        }
    }
}
