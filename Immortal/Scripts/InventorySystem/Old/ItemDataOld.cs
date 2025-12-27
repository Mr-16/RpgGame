using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem.Old
{
    public enum ItemTypeOld
    {
        Equipment,
        Consumable,
        Material,
    }

    public class ItemDataOld
    {
        public string Id;
        public ItemTypeOld ItemType;
        public Texture2D Icon;
        public int MaxStack;
        public string Name;
        public string Description;

        public ItemDataOld(string id, ItemTypeOld type, Texture2D icon, int maxStack, string name, string dscription)
        {
            Id = id;
            ItemType = type;
            Icon = icon;
            MaxStack = maxStack;
            Name = name;
            Description = dscription;
        }
    }

    public enum EquipTypeOld
    {
        Helmet,     //头 上
        Weapon,     //手 左
        Ring,       //环 右
        Armor,      //甲 中
        Boot,       //靴 下
    }
    public enum RarityTypeOld
    {
        Common,     //普通(白)
        Rare,       //稀有(蓝)
        Epic,       //史诗(紫)
        Legendary,  //传说(金)
    }
    public class EquipDataOld : ItemDataOld
    {
        public EquipTypeOld EquipmentType;
        public RarityTypeOld Rarity;
        public int MaxLevel = 100;        //满级100, 一开始0阶, 最多5阶
        public int LevelToUpgrade = 20;  //20级升一次阶
        public EquipDataOld(string id, ItemTypeOld type, Texture2D icon, int maxStack, string name, string dscription, EquipTypeOld equipmentType, RarityTypeOld rarity) : base(id, type, icon, maxStack, name, dscription)
        {
            EquipmentType = equipmentType;
            Rarity = rarity;
        }
    }

    public enum WeaponTypeOld
    {
        Sword,      //剑
        Bow,        //弓
        Dao,        //刀
        Spear,      //枪
    }
    public class WeaponDataOld : EquipDataOld
    {
        public WeaponTypeOld WeaponType;
        public float Range;

        public WeaponDataOld(string id, ItemTypeOld type, Texture2D icon, int maxStack, string name, string dscription, EquipTypeOld equipmentType, RarityTypeOld rarity, WeaponTypeOld weaponType, float range) : base(id, type, icon, maxStack, name, dscription, equipmentType, rarity)
        {
            WeaponType = weaponType;
            Range = range;
        }
    }
}
