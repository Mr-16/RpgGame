using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.AttributeSystem
{
    public static class DamageCalculator
    {
        public static Random random = new Random();

        public static float CalculatePhysicalDamage(AttributeContainer attacker, AttributeContainer defender, float weaponBaseMin, float weaponBaseMax)
        {
            // 基础伤害
            float baseDamage = (float)random.NextDouble() * (weaponBaseMax - weaponBaseMin) + weaponBaseMin;

            // 攻击力加成
            float physicalMin = attacker.GetAttributeFinalValue(AttributeType.PhysicalAttackMin);
            float physicalMax = attacker.GetAttributeFinalValue(AttributeType.PhysicalAttackMax);
            float attackBonus = (float)random.NextDouble() * (physicalMax - physicalMin) + physicalMin;

            float damage = baseDamage + attackBonus;

            // 总增伤
            float damagePercent = attacker.GetAttributeFinalValue(AttributeType.DamagePercent);
            damage *= (1 + damagePercent);

            // 防御减伤
            float armor = defender.GetAttributeFinalValue(AttributeType.Armor);
            float damageReductionPercent = defender.GetAttributeFinalValue(AttributeType.DamageReductionPercent);

            float armorReduction = armor / (armor + 100f); // 可调整公式
            damage *= (1 - armorReduction);
            damage *= (1 - damageReductionPercent);

            // 最低伤害
            return MathF.Max(damage, 0f);
        }
    }

}
