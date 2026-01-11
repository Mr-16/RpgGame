using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.InventorySystem
{
    public class ItemInstance
    {
        public ItemData Data;
        public int Count;
        public ItemInstance(ItemData data, int count)
        {
            Data = data;
            Count = count;
        }
        public Dictionary<ItemCompType, IItemComponent> TypeCompMap = new Dictionary<ItemCompType, IItemComponent>();

    }

    public class ItemFactory
    {
        private static ItemFactory instance = new ItemFactory();
        public static ItemFactory Instance() { return instance; }
        private ItemFactory()
        {

        }

        public ItemInstance Create(string itemId, int count = 1)
        {
            ItemData data = ItemDatabase.Instance().GetItemData(itemId);

            ItemInstance instance = new ItemInstance(data, count);

            foreach (ItemCompType type in data.CompSet)
            {
                switch(type)
                {
                    case ItemCompType.Equipment:
                        IItemComponent equipmentComp = new EquipComp(data.WuXingType, data.SkillData);
                        instance.TypeCompMap.Add(type, equipmentComp);
                        break;
                    case ItemCompType.Consumable:
                        IItemComponent ConsumableComp = new ConsumableComp();
                        instance.TypeCompMap.Add(type,ConsumableComp);
                        break;
                }
            }
            return instance;
        }

    }
}
