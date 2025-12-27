using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Datas
{
    public enum ItemType
    {
        Equipment,
        Consumable,
        Material,
    }

    public enum Rarity
    {
        Common,     //普通(白)
        Rare,       //稀有(蓝)
        Epic,       //史诗(紫)
        Legendary,  //传说(金)
    }

    public enum EquipType
    {
        Weapon,
        Helmet,
        Armor,
        Ring,
        Boot,
    }

    public enum WeaponType
    {
        Sword,
        Bow,
        Dao,
        Spear,
    }

    public class ItemData
    {
        public string Id;
        public string Name;
        public Texture2D Icon;
        public string Description;
        public ItemType ItemType;

        //消耗品 ItemType == ItemType.Consumable



        //装备 ItemType == ItemType.Equipment
        public Rarity Rarity;
        public EquipType EquipType;

        //武器 ItemType == ItemType.Equipment && EquipType == EquipType.Weapon
        public WeaponType WeaponType;

    }
}
