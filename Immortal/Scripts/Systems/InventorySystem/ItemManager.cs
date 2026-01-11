using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.InventorySystem
{
    public class ItemManager
    {
        public EquipmentManager EquipmentManager;
        public InventoryManager InventoryManager;
        public ItemManager(EquipmentManager equipment, InventoryManager inventory)
        {
            EquipmentManager = equipment;
            InventoryManager = inventory;
        }

        public void EquipFromInv(int invIndex, int equipIndex)
        {
            ItemInstance equip = InventoryManager.ItemList[invIndex];
            if (equip == null) return;
            if (!equip.Data.CompSet.Contains(ItemCompType.Equipment)) return;
            ItemInstance oldEquip = EquipmentManager.Equip(equipIndex, equip);
            InventoryManager.SetItem(oldEquip, invIndex);
        }

        //卸下后自动在背包找空格子(右键卸下)
        public void UnequipToInv(int index)
        {
            for(int i = 0; i < InventoryManager.Capacity; i++)
            {
                if (InventoryManager.ItemList[i] != null) continue;
                InventoryManager.SetItem(EquipmentManager.Unequip(index), i);
                break;
            }
        }

        //卸下后去指定格子(拖动卸下)
        public bool UnequipToInv(int equipIndex, int invIndex)
        {
            ItemInstance oldEquip = EquipmentManager.Unequip(equipIndex);
            ItemInstance newEquip = InventoryManager.GetItem(invIndex);
            InventoryManager.SetItem(oldEquip, invIndex);
            EquipmentManager.Equip(equipIndex, newEquip);
            return true;
        }

    }
}
