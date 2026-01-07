using Godot;
using RpgGame.Scripts.Utilities;
using RpgGame.Scripts.Utilities.Enums;
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
                Id = GameConstants.Sword,
                Name = "金法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"),
                Description = "这是一把金心法, 意味着质量和防御",
                WuXingType = WuXingType.Metal,
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.Sword,
                Name = "金法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"),
                Description = "这是一把金心法, 意味着质量和防御",
                WuXingType = WuXingType.Metal,
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.Sword,
                Name = "金法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"),
                Description = "这是一把金心法, 意味着质量和防御",
                WuXingType = WuXingType.Metal,
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.Sword,
                Name = "金法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"),
                Description = "这是一把金心法, 意味着质量和防御",
                WuXingType = WuXingType.Metal,
                CompSet = new HashSet<ItemComponentType>() { ItemComponentType.EquippableComponent },
            });
            Register(new ItemData()
            {
                Id = GameConstants.Sword,
                Name = "金法",
                MaxStack = 1,
                Icon = GD.Load<Texture2D>("res://Assets/MyAssets/Sword.png"),
                Description = "这是一把金心法, 意味着质量和防御",
                WuXingType = WuXingType.Metal,
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
