using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.ItemSystem
{
    public class ItemInstance
    {
        public ItemData ItemData;
        public int Count;
        public bool IsEmpty => Count <= 0;
        public bool IsFull => ItemData.MaxStack > 0 && Count >= ItemData.MaxStack;

        public ItemInstance(ItemData data, int count)
        {
            ItemData = data;
            Count = count;
        }

        public int CanAdd()
        {
            if (ItemData.MaxStack <= 1)
                return 0;

            return ItemData.MaxStack - Count;
        }

        public int Add(int amount)
        {
            int add = Math.Min(amount, CanAdd());
            Count += add;
            return add;
        }

        public int Remove(int amount)
        {
            int remove = Math.Min(amount, Count);
            Count -= remove;
            return remove;
        }


    }
}
