using RpgGame.Scripts.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Equipments
{
    public class EquipmentSystem
    {
        public Equipment Weapon = null;
        public Equipment Helmet = null;
        public Equipment Armor = null;
        public Equipment Boot = null;

        ////一般属性
        //public float MaxHealth = 0;
        //public float MaxMana = 0;
        //public float MoveSpeed = 0;
        //public float RollSpeed = 0;
        //public float MaxStamina = 0;
        //public float StaminaRecoverPerSec = 0;//每秒恢复量
        //public float RollStamina = 0;
        ////攻击属性
        //public float AtkSpeed = 0;
        //public float PhyAtk = 0;
        //public float PhyPenPercent = 0;     //百分比穿透
        //public float PhyPenFlat = 0;        //固定穿透  公式 EffectDef = (Def * PhyPenPercent) - PhyPenFlat
        //public float MagAtk = 0;
        //public float MagPenPercent = 0;
        //public float MagPenFlat = 0;
        //public float CritRate = 0;
        //public float CritDamage = 0;
        ////防御属性
        //public float PhyDef = 0;
        //public float MagDef = 0;
        ////特殊属性
        //public float LifeSteal = 0;

        public CharAttribute AttrTolBonus;
    }
}
