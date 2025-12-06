using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters
{
    public class CharAttribute
    {
        //这个类是个数据结构, 用来定义整个游戏的全部属性
        //包括人物的基础属性, 最终属性, 装备的属性加成, buff的属性改动

        //一般属性
        public float MaxHealth = 0;
        public float MaxMana = 0;
        public float MoveSpeed = 0;
        public float RollSpeed = 0;
        public float MaxStam = 0;
        public float StamRegen = 0;//每秒恢复量
        //攻击属性
        public float AtkSpeed = 0;
        public float PhyAtk = 0;
        public float PhyPen = 0;     //百分比穿透
        public float PhyPenFlat = 0;        //固定穿透  公式 EffectDef = (Def * PhyPenPercent) - PhyPenFlat
        public float MagAtk = 0;
        public float MagPen = 0;
        public float MagPenFlat = 0;
        public float CritRate = 0;
        public float CritMult = 0;
        //防御属性
        public float PhyDef = 0;
        public float MagDef = 0;
        //特殊属性
        public float LifeSteal = 0;
    }
}
