using Godot;
using RpgGame.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RpgGame.Scripts.Items
{
    public enum ItemType
    {
        Consumable,
        Material,
        Equipment,
    }

    public partial class ItemData
    {
        public int Id;
        public string Name;
        public string Description;
        public ItemType Type;
        public int MaxStack;
        public string IconPath;
        public ItemData(int id, string name, string description, ItemType type, int maxStack, string iconPath)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            MaxStack = maxStack;
            IconPath = iconPath;
        }
    }

    public class ItemInstance
    {
        public ItemData ItemData;
        public int Count;
        public ItemInstance(ItemData data, int count)
        {
            ItemData = data;
            Count = count;
        }

        /// <summary>
        /// 尝试合并另一个物品实例
        /// </summary>
        /// <returns>剩余无法合并的数量</returns>
        public int AddQuantity(int amount)
        {
            int total = Count + amount;
            if (total <= ItemData.MaxStack)
            {
                Count = total;
                return 0;
            }
            else
            {
                Count = ItemData.MaxStack;
                return total - ItemData.MaxStack;
            }
        }

        public ItemInstance Clone()
        {
            return new ItemInstance(ItemData, Count);
        }
    }
}


//using Godot;
//using System.Collections.Generic;

//public class Inventory : Node
//{
//    public int Capacity { get; private set; } = 30;
//    private List<ItemInstance> _items = new List<ItemInstance>();

//    public IReadOnlyList<ItemInstance> Items => _items.AsReadOnly();

//    public Inventory(int capacity = 30)
//    {
//        Capacity = capacity;
//    }

//    // 添加物品
//    public bool AddItem(ItemData data, int quantity = 1)
//    {
//        if (data.MaxStack > 1)
//        {
//            // 尝试堆叠
//            var existing = _items.Find(i => i.Data.Id == data.Id && i.Quantity < data.MaxStack);
//            if (existing != null)
//            {
//                int space = data.MaxStack - existing.Quantity;
//                int toAdd = Mathf.Min(space, quantity);
//                existing.Quantity += toAdd;
//                quantity -= toAdd;
//            }
//        }

//        while (quantity > 0)
//        {
//            if (_items.Count >= Capacity)
//                return false; // 背包已满

//            int toAdd = Mathf.Min(data.MaxStack, quantity);
//            _items.Add(new ItemInstance(data, toAdd));
//            quantity -= toAdd;
//        }

//        return true;
//    }

//    // 移除物品
//    public bool RemoveItem(int itemId, int quantity = 1)
//    {
//        var instance = _items.Find(i => i.Data.Id == itemId);
//        if (instance == null)
//            return false;

//        if (instance.Quantity > quantity)
//        {
//            instance.Quantity -= quantity;
//        }
//        else
//        {
//            _items.Remove(instance);
//        }

//        return true;
//    }
//}

