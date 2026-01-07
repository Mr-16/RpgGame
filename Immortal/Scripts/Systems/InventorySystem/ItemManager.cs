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

        public void EquipFromInv(int invIndex)
        {
            if (invIndex < 0 || invIndex >= InventoryManager.ItemList.Count) return;

            ItemInstance equip = InventoryManager.ItemList[invIndex];
            if (equip == null) return;
            if (!equip.Data.CompSet.Contains(ItemComponentType.EquippableComponent)) return;
            ItemInstance oldEquip = EquipmentManager.GetEquip(equip.Data.EquipType);

            EquipmentManager.Equip(equip);

            if (oldEquip != null)
            {
                InventoryManager.SetItem(oldEquip, invIndex);
            }
            else
            {
                InventoryManager.SetItem(null, invIndex);
            }
        }

        //卸下后自动在背包找空格子(右键卸下)
        public void UnequipToInv(EquipType equipType)
        {
            ItemInstance equip = EquipmentManager.GetEquip(equipType);
            if (equip == null) return;
            for(int i = 0; i < InventoryManager.Capacity; i++)
            {
                if (InventoryManager.ItemList[i] != null) continue;
                InventoryManager.SetItem(equip, i);
                EquipmentManager.Unequip(equipType);
                break;
            }
        }

        //卸下后去指定格子(拖动卸下)
        public bool UnequipToInv(EquipType equipType, int invIndex)
        {
            ItemInstance equip = EquipmentManager.GetEquip(equipType);
            if (equip == null) return false;

            if (InventoryManager.ItemList[invIndex] != null) return false;
            EquipmentManager.Unequip(equipType);
            InventoryManager.SetItem(equip, invIndex);
            return true;
        }

    }
}
