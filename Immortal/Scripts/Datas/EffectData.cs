using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Datas
{
    public enum EffectType
    {
        Heal,
        AddMana,
        Damage,
        AddSpeed,
        AddAtk,
        AddBuff,
    }

    public class EffectData
    {
        public EffectType EffectType;
        public float Health;
        public float AddMana;
        public float Damage;
        public float AddSpeed;
    }
}
