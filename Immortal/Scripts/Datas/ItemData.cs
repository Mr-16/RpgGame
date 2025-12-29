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

    public enum MaterialType
    {
        Gold,
        Wood,
        Water,
        Fire,
        Earth,
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
        public int MaxStack;
        public ItemType ItemType;

        //消耗品 ItemType == ItemType.Consumable
        public List<EffectData> EffectDataList;

        //材料 ItemType == ItemType.Material
        public MaterialType MaterialType;

        //装备 ItemType == ItemType.Equipment
        public Rarity Rarity;
        public EquipType EquipType;

        //武器 ItemType == ItemType.Equipment && EquipType == EquipType.Weapon
        public WeaponType WeaponType;

        public ItemData(string id, string name, int maxStack, ItemType itemType, Texture2D icon, string description)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
            ItemType = itemType;
            Icon = icon;
            Description = description;
        }
        public ItemData(string id, string name, int maxStack, ItemType itemType, Texture2D icon, string description, MaterialType materialType)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
            ItemType = itemType;
            Icon = icon;
            Description = description;
            MaterialType = materialType;
        }
        public ItemData(string id, string name, int maxStack, ItemType itemType, Texture2D icon, string description, List<EffectData> effectDataList)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
            ItemType = itemType;
            Icon = icon;
            Description = description;
            EffectDataList = effectDataList;
        }
        public ItemData(string id, string name, int maxStack, ItemType itemType, Texture2D icon, string description, Rarity rarity, EquipType equipType)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
            ItemType = itemType;
            Icon = icon;
            Description = description;
            Rarity = rarity;
            EquipType = equipType;
        }
        public ItemData(string id, string name, int maxStack, ItemType itemType, Texture2D icon, string description, Rarity rarity, EquipType equipType, WeaponType weaponType)
        {
            Id = id;
            Name = name;
            MaxStack = maxStack;
            ItemType = itemType;
            Icon = icon;
            Description = description;
            Rarity = rarity;
            EquipType = equipType;
            WeaponType = weaponType;
        }
    }
}
