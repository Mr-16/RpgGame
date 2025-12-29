using RpgGame.Scripts.Characters.Players;
using RpgGame.Scripts.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.EffectSystem
{
    public class EffectSystem
    {
        public void ApplyEffect(EffectData data, Player targetPlayer)
        {
            switch (data.EffectType)
            {
                case EffectType.Heal:
                    break;
                case EffectType.Damage:
                    break;
                case EffectType.AddSpeed:
                    break;
                case EffectType.AddAtk:
                    break;
                case EffectType.AddBuff:
                    break;
                default:
                    break;
            }
        }
    }
}
