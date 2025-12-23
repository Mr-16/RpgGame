using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.ItemSystem
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
}
