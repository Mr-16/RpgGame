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
        Damage,
        AddSpeed,
        AddAtk,

    }

    public class EffectData
    {
        public EffectType Type;
    }
}
