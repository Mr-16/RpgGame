using RpgGame.Scripts.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class ItemManager
    {
        private Equipment equipment;
        private Inventory inventory;
        public ItemManager(Equipment equipment, Inventory inventory)
        {
            this.equipment = equipment;
            this.inventory = inventory;
        }

        public void EquipFromInv(int invIndex)
        {
            if (invIndex < 0 || invIndex >= inventory.ItemList.Count) return;

            ItemInstance equip = inventory.ItemList[invIndex];
            if (equip == null) return;
            if (!equip.Data.CompSet.Contains(ItemComponentType.EquippableComponent)) return;
            ItemInstance oldEquip = equipment.GetEquip(equip.Data.EquipType);

            equipment.Equip(equip);

            if (oldEquip != null)
            {
                inventory.SetItem(oldEquip, invIndex);
            }
            else
            {
                inventory.SetItem(null, invIndex);
            }
        }

        //卸下后自动在背包找空格子(右键卸下)
        public void UnequipToInv(EquipType equipType)
        {
            ItemInstance equip = equipment.GetEquip(equipType);
            if (equip == null) return;
            for(int i = 0; i < inventory.Capacity; i++)
            {
                if (inventory.ItemList[i] != null) continue;
                inventory.SetItem(equip, i);
                equipment.Unequip(equipType);
                break;
            }
        }

        //卸下后去指定格子(拖动卸下)
        public bool UnequipToInv(EquipType equipType, int invIndex)
        {
            ItemInstance equip = equipment.GetEquip(equipType);
            if (equip == null) return false;

            if (inventory.ItemList[invIndex] != null) return false;
            equipment.Unequip(equipType);
            inventory.SetItem(equip, invIndex);
            return true;
        }

    }
}
