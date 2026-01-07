using Godot;
using RpgGame.Scripts.Systems.SkillSystem;
using RpgGame.Scripts.Utilities;
using RpgGame.Scripts.Utilities.Enums;
using System;
using System.Collections.Generic;

namespace RpgGame.Scripts.Systems.InventorySystem
{
    public class ItemDatabase
    {
        private static ItemDatabase instance = new ItemDatabase();
        private Dictionary<string, ItemData> idItemMap = new Dictionary<string, ItemData>();
        public static ItemDatabase Instance() => instance;
        private ItemDatabase() 
        {
            Register(new ItemData()
            {
                Id = GameConstants.HealthPotion,
                Name = "生命药水",
                MaxStack = 16,
                Icon = GD.Load<Texture2D>("res://Assets/NinjaAssets/Items/Potion/LifePot.png"),
                Description = "回血",
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.ConsumableComponent },
            });

            Register(new ItemData()
            {
                Id = GameConstants.ManaPotion,
                Name = "法力药水",
                MaxStack = 16,
                Icon = GD.Load<Texture2D>("res://Assets/NinjaAssets/Items/Potion/WaterPot.png"),
                Description = "回血",
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.ConsumableComponent },
            });

            Register(new ItemData()
            {
                Id = GameConstants.Wd40,
                Name = "WD40",
                MaxStack = 16,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Wd40.png"),
                Description = "回所有",
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.ConsumableComponent },
            });

            Register(new ItemData()
            {
                Id = GameConstants.MetalEquip,
                Name = "金法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Metal.png"),
                Description = "这是一把金心法, 意味着质量和防御",
                WuXingType = WuXingType.Metal,
                SkillData = new SkillData() { AtkRadiusSq = 1000 * 1000, MaxFlyDisSq = 1000 * 1000, AngleRange = MathF.PI / 6, FlySpeed = 800, ProjectileCount = 1, EnergyCost = 1, CastSpeed = 6 },
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.WoodEquip,
                Name = "木法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Wood.png"),
                Description = "这是一把木心法, 意味着自然和恢复",
                WuXingType = WuXingType.Wood,
                SkillData = new SkillData() { AtkRadiusSq = 900 * 900, MaxFlyDisSq = 900 * 900, AngleRange = MathF.PI / 3, FlySpeed = 700, ProjectileCount = 3, EnergyCost = 2, CastSpeed = 5},
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.WaterEquip,
                Name = "水法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Water.png"),
                Description = "这是一把水心法, 意味着智慧和能量",
                WuXingType = WuXingType.Water,
                SkillData = new SkillData() { AtkRadiusSq = 800 * 80, MaxFlyDisSq = 800 * 800, AngleRange = MathF.PI * 2 / 3, FlySpeed = 600, ProjectileCount = 5, EnergyCost = 3, CastSpeed = 4},
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.FireEquip,
                Name = "火法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Fire.png"),
                Description = "这是一把火心法, 意味着进攻和燃烧",
                WuXingType = WuXingType.Fire,
                SkillData = new SkillData() { AtkRadiusSq = 200 * 200, MaxFlyDisSq = 200 * 200, AngleRange = MathF.PI, FlySpeed = 500, ProjectileCount = 9, EnergyCost = 4, CastSpeed = 3 },
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.EarthEquip,
                Name = "土法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Earth.png"),
                Description = "这是一把土心法, 意味着生命和根源",
                WuXingType = WuXingType.Earth,
                SkillData = new SkillData() { AtkRadiusSq = 120 * 120, MaxFlyDisSq = 100 * 100, AngleRange = MathF.PI * 2, FlySpeed = 200, ProjectileCount = 15, EnergyCost = 5, CastSpeed = 2f },
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            //Register(new ItemData()
            //{
            //    Id = ItemId.Sword,
            //    Name = "新手之剑",
            //    MaxStack = 1,
            //    Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"),
            //    Description = "这是一把新手之剑, 意味着一切的开始.",
            //    EquipType = EquipType.Weapon,
            //    CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            //});

            //Register(new ItemData()
            //{
            //    Id = ItemId.Bow,
            //    Name = "新手之弓",
            //    MaxStack = 1,
            //    Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Bow.png"),
            //    Description = "这是一把新手之弓, 意味着一切的开始.",
            //    EquipType = EquipType.Weapon,
            //    CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            //});

            //Register(new ItemData()
            //{
            //    Id = ItemId.Armor,
            //    Name = "新手盔甲",
            //    MaxStack = 1,
            //    Icon = GD.Load<Texture2D>("res://Assets/NinjaAssets/Items/Weapons/Axe/Sprite.png"),
            //    Description = "这是一把新手盔甲, 意味着一切的开始.",
            //    EquipType = EquipType.Armor,
            //    CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            //});
        }

        public void Register(ItemData data)
        {
            idItemMap[data.Id] = data;
        }

        public ItemData GetItemData(string id)
        {
            return idItemMap[id];
        }
    }
}
