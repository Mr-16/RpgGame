using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Datas
{
    public class ItemId
    {
        public static string HealthPotion = "HealthPotion";
        public static string ManaPotion = "ManaPotion";
        public static string Wd40 = "Wd40";
        public static string Sword = "Sword";
        public static string Bow = "Bow";
        public static string Armor = "Armor";
    }

    public class ItemDatabase
    {
        private static ItemDatabase instance = new ItemDatabase();
        private Dictionary<string, ItemData> idItemMap = new Dictionary<string, ItemData>();
        public static ItemDatabase Instance() => instance;
        private ItemDatabase() 
        {
            Register(new ItemData(ItemId.HealthPotion, "生命药水", 16, ItemType.Consumable, GD.Load<Texture2D>(""), "回血", new List<EffectData> { EffectDatabase.Instance().GetEffectData(EffectId.HealEffect) }));

            Register(new ItemData(ItemId.ManaPotion, "法力药水", 16, ItemType.Consumable, GD.Load<Texture2D>(""), "回蓝", new List<EffectData> { EffectDatabase.Instance().GetEffectData(EffectId.AddManaEffect) }));

            Register(new ItemData(ItemId.Sword, "新手之剑", 1, ItemType.Equipment, GD.Load<Texture2D>(""), "这是一把新手之剑, 意味着一切的开始.", Rarity.Common, EquipType.Weapon));

            Register(new ItemData(ItemId.Bow, "新手之弓", 1, ItemType.Equipment, GD.Load<Texture2D>(""), "这是一把新手之弓, 意味着一切的开始.", Rarity.Common, EquipType.Weapon));

            Register(new ItemData(ItemId.Armor, "新手盔甲", 1, ItemType.Equipment, GD.Load<Texture2D>(""), "这是一把新手盔甲, 意味着一切的开始.", Rarity.Common, EquipType.Armor));
        }


        public void Register(ItemData data)
        {
            idItemMap[data.Id] = data;
        }

        public ItemData GetItemData(string id)
        {
            //idItemMap.TryGetValue(id, out var item);
            return idItemMap[id];
        }
    }
}
