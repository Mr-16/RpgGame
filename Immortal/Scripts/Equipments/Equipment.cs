using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Equipments
{
    public enum EquipmentGrade
    {
        White,  //路边
        Green,  //普通
        Blue,   //稀有
        Purple, //史诗
        Gold,   //传说!!!
    }

    public enum EquipmentType
    {
        Sword,  //剑
        Dao,    //刀
        Bow,    //弓
        Spear,  //枪
        Helmet, //头
        Armor,  //甲
        Boot,   //靴
    }

    public class Equipment
    {
        public string name;
        public EquipmentGrade grade;
        public int level;

        //基础属性加成
        public float maxHealthBonus;
        public float maxManaBonus;
        public float moveSpeedBonus;

        //攻击属性加成
        public float atkSpeedBonus;
        public float phyAtkBonus;
        public float phyPenBouns;
        public float magAtkBonus;
        public float magPenBonus;
        public float critRateBonus;
        public float critDamageBonus;

        //防御属性加成
        public float phyDefBonus;
        public float magDefBonus;

        //特殊属性加成
        public float lifeStealBonus;
    }
}
