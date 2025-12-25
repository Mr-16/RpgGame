using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
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
                ItemList[i] = new ItemInstance(item.Data, putCount);
                item.Count -= putCount;
                ItemChanged?.Invoke(i, ItemList[i]);
                if (item.Count <= 0) return true;
            }

            // 背包满了，还有剩余
            return false;
        }
        
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


    public class VeryGoodInventory
    {
        public int Capacity = 30;
        public List<ItemSlot> SlotList;
        public event Action<ItemSlot> OnSlotChanged;

        public VeryGoodInventory()
        {
            SlotList = new List<ItemSlot>();
            for (int i = 0; i < Capacity; i++)
                SlotList.Add(new ItemSlot());
        }

        public bool AddItem(string itemId, int count)
        {
            ItemData data = ItemDataBase.Instance().GetData(itemId);
            if (data == null) return false;

            int countToAdd = count;

            // 1. 堆叠已有槽
            foreach (var slot in SlotList)
            {
                if (slot.Item == null) continue;
                if (slot.Item.Data.Id != itemId) continue;

                int added = slot.AddItem(countToAdd);
                if (added > 0) OnSlotChanged?.Invoke(slot);


                countToAdd -= added;
                if (countToAdd == 0) return true;
            }

            // 2. 使用空槽
            foreach (var slot in SlotList)
            {
                if (slot.Item != null) continue;

                int curAdd = Math.Min(countToAdd, data.MaxStack);
                slot.Item = new ItemInstance(data, curAdd);
                OnSlotChanged?.Invoke(slot);

                countToAdd -= curAdd;
                if (countToAdd == 0) return true;
            }

            return false;
        }

        public bool RemoveItem(string itemId, int count)
        {
            int countToRemove = count;
            foreach (var slot in SlotList)
            {
                if (slot.Item == null) continue;
                if (slot.Item.Data.Id != itemId) continue;

                int removed = slot.DelItemCount(countToRemove);
                if (removed > 0) OnSlotChanged?.Invoke(slot);

                countToRemove -= removed;

                if (slot.Item.Count == 0)
                {
                    slot.Item = null;
                    OnSlotChanged?.Invoke(slot);
                }

                if (countToRemove == 0)
                    return true;
            }

            return false;
        }
    }

}
