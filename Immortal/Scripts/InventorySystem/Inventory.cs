using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class Inventory
    {
        private readonly Dictionary<string, List<ItemInstance>> IdInstListMap = new Dictionary<string, List<ItemInstance>>();

        public event Action<ItemInstance> ItemAdded;
        public event Action<ItemInstance> ItemRemoved;
        public event Action<ItemInstance> ItemChanged;


        public bool AddItem(ItemData data, int count)
        {
            if (count <= 0)
                return false;

            if (!IdInstListMap.TryGetValue(data.Id, out var list))
            {
                list = new List<ItemInstance>();
                IdInstListMap.Add(data.Id, list);
            }

            int remain = count;

            // 填已有栈
            foreach (var inst in list)
            {
                if (remain <= 0)
                    break;

                int before = inst.Count;
                int added = inst.Add(remain);
                remain -= added;

                if (added > 0)
                    ItemChanged?.Invoke(inst);
            }

            // 新栈
            while (remain > 0)
            {
                int stackCount = data.MaxStack > 0
                    ? Math.Min(remain, data.MaxStack)
                    : remain;

                var inst = new ItemInstance(data, stackCount);
                list.Add(inst);
                remain -= stackCount;

                ItemAdded?.Invoke(inst);
            }

            return true;
        }

        public bool RemoveItem(string itemId, int count)
        {
            if (count <= 0)
                return false;

            if (!IdInstListMap.TryGetValue(itemId, out var list))
                return false;

            int remain = count;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (remain <= 0)
                    break;

                var inst = list[i];
                int before = inst.Count;
                int removed = inst.Remove(remain);
                remain -= removed;

                if (removed > 0)
                    ItemChanged?.Invoke(inst);

                if (inst.IsEmpty)
                {
                    list.RemoveAt(i);
                    ItemRemoved?.Invoke(inst);
                }
            }

            if (list.Count == 0)
                IdInstListMap.Remove(itemId);

            return remain == 0;
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
                if (slot.Item.ItemData.Id != itemId) continue;

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
                if (slot.Item.ItemData.Id != itemId) continue;

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
