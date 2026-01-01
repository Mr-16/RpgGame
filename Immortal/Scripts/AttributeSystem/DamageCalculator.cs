using System;

namespace RpgGame.Scripts.AttributeSystem
{
    public enum DamageType
    {
        Physical,
        Magical,
        True        // 真实伤害
    }

    public static class DamageCalculator
    {
        public static Random random = new Random();

        public static float CalculateDamage(
            AttributeContainer attacker,
            AttributeContainer defender,
            float baseMin,
            float baseMax,
            DamageType damageType = DamageType.Physical,
            float skillMultiplier = 1f // 技能倍率
        )
        {
            // 1. 基础伤害随机
            float baseDamage = (float)random.NextDouble() * (baseMax - baseMin) + baseMin;

            // 2. 属性加成
            float attributeDamage = 0f;
            switch (damageType)
            {
                case DamageType.Physical:
                    float physMin = attacker.GetAttributeFinalValue(AttributeType.PhysicalAttackMin);
                    float physMax = attacker.GetAttributeFinalValue(AttributeType.PhysicalAttackMax);
                    attributeDamage = (float)random.NextDouble() * (physMax - physMin) + physMin;
                    break;

                case DamageType.Magical:
                    attributeDamage = attacker.GetAttributeFinalValue(AttributeType.MagicAttack);
                    break;

                case DamageType.True:
                    attributeDamage = 0f; // 真实伤害不受属性加成
                    break;
            }

            float damage = (baseDamage + attributeDamage) * skillMultiplier;

            // 3. 暴击
            if (damageType == DamageType.Physical)
            {
                float critChance = attacker.GetAttributeFinalValue(AttributeType.CritChance);
                float critDamage = attacker.GetAttributeFinalValue(AttributeType.CritDamage);
                if (random.NextDouble() < critChance)
                    damage *= (1 + critDamage);
            }

            // 4. 攻击者增伤百分比
            float damagePercent = attacker.GetAttributeFinalValue(AttributeType.DamagePercent);
            damage *= (1 + damagePercent);

            // 5. 防御减伤
            float reductionMultiplier = 1f;
            switch (damageType)
            {
                case DamageType.Physical:
                    float armor = defender.GetAttributeFinalValue(AttributeType.Armor);
                    reductionMultiplier = 100f / (100f + armor);
                    break;
                case DamageType.Magical:
                    float magicResist = defender.GetAttributeFinalValue(AttributeType.MagicResistance);
                    reductionMultiplier = 100f / (100f + magicResist);
                    break;
                case DamageType.True:
                    reductionMultiplier = 1f; // 真实伤害不受减伤
                    break;
            }

            float damageReductionPercent = defender.GetAttributeFinalValue(AttributeType.DamageReductionPercent);
            damage *= reductionMultiplier * (1 - damageReductionPercent);

            // 6. 最低伤害保护
            return MathF.Max(damage, 0f);
        }
    }
}
