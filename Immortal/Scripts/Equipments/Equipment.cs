using RpgGame.Scripts.Characters;
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
        public string Name;
        public EquipmentType Type;
        public EquipmentGrade Grade;
        public int Level;

        public CharAttribute BonusAttr = new CharAttribute();

        public Equipment(string name, EquipmentType type, EquipmentGrade grade, int level)
        {
            Name = name;
            Type = type;
            Grade = grade;
            Level = level;

            BonusAttr.MaxHealth = 5;
        }
    }
}
