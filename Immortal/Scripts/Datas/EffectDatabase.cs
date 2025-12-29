using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Datas
{
    public class EffectId
    {
        public const string HealEffect = "HealEffect";
        public const string AddManaEffect = "AddManaEffect";
    }

    public class EffectDatabase
    {
        private static EffectDatabase instance = new EffectDatabase();
        private Dictionary<string, EffectData> idEffectMap = new Dictionary<string, EffectData>();
        public static EffectDatabase Instance() => instance;

        private EffectDatabase()
        {
            EffectData healEffect = new EffectData();
            healEffect.EffectType = EffectType.Heal;
            healEffect.Health = 20;
            idEffectMap.Add(EffectId.HealEffect, healEffect);
            EffectData addManaEffect = new EffectData();
            healEffect.EffectType = EffectType.AddMana;
            healEffect.AddMana = 10;
            idEffectMap.Add(EffectId.AddManaEffect, addManaEffect);
        }

        public EffectData GetEffectData(string id)
        {
            return idEffectMap[id];
        }
         
    }
}
