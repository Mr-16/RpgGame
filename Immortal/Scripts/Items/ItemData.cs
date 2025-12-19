using Godot;
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
