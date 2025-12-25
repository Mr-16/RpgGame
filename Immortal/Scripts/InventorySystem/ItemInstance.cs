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
    }
    public class EquipmentInstance : ItemInstance
    {
        public int Level = 1;
        public int Stage = 0;
        public EquipmentInstance(ItemData data, int count) : base(data, count)
        {
        }
    }
    public class WeaponInstance : EquipmentInstance
    {
        public WeaponInstance(ItemData data, int count) : base(data, count)
        {
        }
    }
}
