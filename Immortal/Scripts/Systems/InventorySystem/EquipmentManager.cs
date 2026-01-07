using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.InventorySystem
{
    public class EquipmentManager
    {
        public List<ItemInstance> EquipList = new List<ItemInstance>();
        public event Action<int, ItemInstance> EquipChange;

        public EquipmentManager()
        {
            for (int i = 0; i < 5; i++)
            {
                EquipList.Add(null);
            }
        }

        public ItemInstance Equip(int index, ItemInstance equip)
        {
            ItemInstance oldEquip = EquipList[index];
            EquipList[index] = equip;
            EquipChange?.Invoke(index, equip);
            return oldEquip;
        }

        public ItemInstance Unequip(int index)
        {
            if (EquipList[index] == null) return null;
            ItemInstance oldEquip = EquipList[index];
            EquipList[index] = null;
            EquipChange?.Invoke(index, null);
            return oldEquip;
        }

        public ItemInstance GetEquip(int index)
        {
            return EquipList[index];
        }
    }
}
