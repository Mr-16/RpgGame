using System;

namespace RpgGame.Scripts.Systems.AttributeSystem
{
    //public enum DamageType
    //{
    //    Physical,
    //    Magical,
    //    True        // 真实伤害
    //}

    public static class DamageCalculator
    {
        public static Random random = new Random();

        public static float CalculateDamage(AttributeContainer atkAttrContainer, AttributeContainer defAttrContainer)
        {
            //Damage = ATK * (ATK / (ATK + DEF))
            float atk = atkAttrContainer.GetAttrValue(AttributeType.Atk).FinalValue;
            float def = atkAttrContainer.GetAttrValue(AttributeType.Def).FinalValue;
            float damage = atk * (atk / (atk + def));
            return damage;


            //// 1. 基础伤害随机
            //float baseDamage = (float)random.NextDouble() * (baseMax - baseMin) + baseMin;

            //// 2. 属性加成
            //float attributeDamage = 0f;
            //switch (damageType)
            //{
            //    case DamageType.Physical:
            //        float physMin = attacker.GetAttrFinalValue(AttributeType.PhysicalAttackMin);
            //        float physMax = attacker.GetAttrFinalValue(AttributeType.PhysicalAttackMax);
            //        attributeDamage = (float)random.NextDouble() * (physMax - physMin) + physMin;
            //        break;

            //    case DamageType.Magical:
            //        attributeDamage = attacker.GetAttrFinalValue(AttributeType.MagicAttack);
            //        break;

            //    case DamageType.True:
            //        attributeDamage = 0f; // 真实伤害不受属性加成
            //        break;
            //}

            //float damage = (baseDamage + attributeDamage) * skillMultiplier;

            //// 3. 暴击
            //if (damageType == DamageType.Physical)
            //{
            //    float critChance = attacker.GetAttrFinalValue(AttributeType.CritChance);
            //    float critDamage = attacker.GetAttrFinalValue(AttributeType.CritDamage);
            //    if (random.NextDouble() < critChance)
            //        damage *= (1 + critDamage);
            //}

            //// 4. 攻击者增伤百分比
            //float damagePercent = attacker.GetAttrFinalValue(AttributeType.DamageRate);
            //damage *= (1 + damagePercent);

            //// 5. 防御减伤
            //float reductionMultiplier = 1f;
            //switch (damageType)
            //{
            //    case DamageType.Physical:
            //        float armor = defender.GetAttrFinalValue(AttributeType.Armor);
            //        reductionMultiplier = 100f / (100f + armor);
            //        break;
            //    case DamageType.Magical:
            //        float magicResist = defender.GetAttrFinalValue(AttributeType.MagicResistance);
            //        reductionMultiplier = 100f / (100f + magicResist);
            //        break;
            //    case DamageType.True:
            //        reductionMultiplier = 1f; // 真实伤害不受减伤
            //        break;
            //}

            //float damageReductionPercent = defender.GetAttrFinalValue(AttributeType.DamageReductionRate);
            //damage *= reductionMultiplier * (1 - damageReductionPercent);

            //// 闪避
            //if (defender.GetAttrFinalValue(AttributeType.DodgeChance) > random.NextDouble())
            //    damage = 0;

            //// 6. 最低伤害保护
            //return MathF.Max(damage, 0f);
        }
    }
}
