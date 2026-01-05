using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Skills
{
    public class SpellData
    {
        public float AtkRadiusSq;      //索敌距离
        public float MaxFlyDisSq;      //飞行距离
        public float FlySpeed;
        public float AngleRange;    //角度范围
        public float ProjectileCount;         //发射物数量
        public float EnergyCost;          //消耗
        public float CastSpeed;     //施法速度
        public WuXingType WuXingType;   //属性

    }

    public interface ISpell
    {
        public void Cast();
    }
}
