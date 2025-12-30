using RpgGame.Scripts.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class Inventory
    {
        public int Capacity;
        public List<ItemInstance> ItemList = new List<ItemInstance>();
        public event Action<int, ItemInstance> ItemChanged;
        public Inventory(int capacity)
        {
            Capacity = capacity;
            for(int i = 0; i< Capacity; i++)
            {
                ItemList.Add(null);
            }
        }
        public bool AddItem(ItemInstance item)
        {
            if (item.Count > item.Data.MaxStack) return false;
            for(int i = 0; i < Capacity; i++)
            { 
                ItemInstance curItem = ItemList[i];
                if (curItem == null) continue;//空格子
                if (curItem.Data.Id != item.Data.Id) continue;//不同类
                if (curItem.Count > curItem.Data.MaxStack) continue;//已满

                int canAddCount = curItem.Data.MaxStack - curItem.Count;
                int addCount = Math.Min(item.Count, canAddCount);
                item.Count -= addCount;
                curItem.Count += addCount;
                ItemChanged?.Invoke(i, curItem);
                if (item.Count <= 0) return true;
            }
            for(int i = 0; i < Capacity; i++)
            {
                if (ItemList[i] != null) continue;
                ItemList[i] = item;
                ItemChanged?.Invoke(i, ItemList[i]);
                return true;
            }
            return false;
        }
        public bool RemoveItem(int itemIndex, int count)
        {
            ItemInstance item = ItemList[itemIndex];
            if (item == null) return false;
            item.Count -= count;
            if (item.Count <= 0) ItemList.RemoveAt(itemIndex);
            ItemChanged?.Invoke(itemIndex, item);
            return true;
        }
    }
}
