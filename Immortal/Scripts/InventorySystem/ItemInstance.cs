using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class ItemInstance
    {
        public ItemData Data;
        public int Count;
        public bool IsEmpty => Count <= 0;
        public bool IsFull => Data.MaxStack > 0 && Count >= Data.MaxStack;

        public ItemInstance(ItemData data, int count)
        {
            Data = data;
            Count = count;
        }

        public int CanAdd()
        {
            if (Data.MaxStack <= 1)
                return 0;

            return Data.MaxStack - Count;
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
