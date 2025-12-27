using Godot;
using RpgGame.Scripts.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem.Old
{
    public class ItemInstance
    {
        public ItemDataOld Data;
        public int Count;
        public bool IsEmpty => Count <= 0;
        public bool IsFull => Data.MaxStack > 0 && Count >= Data.MaxStack;

        public ItemInstance(ItemDataOld data, int count)
        {
            Data = data;
            Count = count;
        }
    }
    public class EquipInstance : ItemInstance
    {
        public int Level = 1;
        public int Stage = 0;
        public CharAttribute AttrBonus;
        public EquipInstance(EquipDataOld data, int count) : base(data, count)
        {
            //Data = data;
        }
    }
    public class WeaponInstance : EquipInstance
    {
        public WeaponInstance(WeaponDataOld data, int count) : base(data, count)
        {
            //Data = data;
        }
    }
}
