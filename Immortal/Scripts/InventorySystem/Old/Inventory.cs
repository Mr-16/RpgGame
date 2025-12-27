using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem.Old
{
    public class Inventory
    {
        public List<ItemInstance> ItemList = new List<ItemInstance>();
        public int Capacity = 30;
        public event Action<int, ItemInstance> ItemChanged;//索引+格子
        
        public Inventory() 
        {
            for(int i = 0; i < Capacity; i++)
            {
                ItemList.Add(null);
            }
        }

        public bool AddItem(ItemInstance item)
        {
            if (item == null) return false;

            // 先找可堆叠的格子
            for (int i = 0; i < Capacity; i++)
            {
                ItemInstance curItem = ItemList[i];
                if (curItem == null) continue;
                if (curItem.Data.Id !=  item.Data.Id) continue;
                if (curItem.Count >= curItem.Data.MaxStack) continue;
               
                int addCount = Math.Min(curItem.Data.MaxStack - curItem.Count, item.Count);

                curItem.Count += addCount;
                item.Count -= addCount;
                ItemChanged?.Invoke(i, curItem);

                if (item.Count <= 0) return true;
            }

            // 再找空槽
            for (int i = 0; i < Capacity; i++)
            {
                if (ItemList[i] != null) continue;

                int putCount = Math.Min(item.Count, item.Data.MaxStack);
                //ItemList[i] = new ItemInstance(item.Data, putCount);
                ItemList[i] = item;
                item.Count -= putCount;
                ItemChanged?.Invoke(i, ItemList[i]);
                if (item.Count <= 0) return true;
            }

            // 背包满了，还有剩余
            return false;
        }

        //public bool AddEquip(EquipInstance equip)
        //{
        //    if (equip == null) return false;

        //    for (int i = 0; i < Capacity; i++)
        //    {
        //        if (ItemList[i] != null) continue;

        //        ItemList[i] = equip;
        //        ItemChanged?.Invoke(i, ItemList[i]);
        //        return true;
        //    }
        //    // 背包满了，还有剩余
        //    return false;
        //}

        public bool RemoveItem(int index, int count)
        {
            if (index < 0 || index >= ItemList.Count) return false;
            ItemInstance item = ItemList[index];
            if (item == null) return false;
            if (count <= 0) return false;
            if (count > item.Count) return false;
            item.Count -= count;
            if (item.Count == 0)
            {
                ItemList[index] = null;
                ItemChanged?.Invoke(index, null);
            }
            else
            {
                ItemChanged?.Invoke(index, item);
            }
            return true;
        }
        
        public bool SwapItem(int indexA, int indexB)
        {
            if (indexA == indexB) return false;

            if (indexA < 0 || indexA >= ItemList.Count) return false;

            if (indexB < 0 || indexB >= ItemList.Count) return false;
            ItemInstance temp = ItemList[indexA];
            ItemList[indexA] = ItemList[indexB];
            ItemList[indexB] = temp;
            ItemChanged?.Invoke(indexA, ItemList[indexA]);
            ItemChanged?.Invoke(indexB, ItemList[indexB]);
            return true;
        }
    }
}
