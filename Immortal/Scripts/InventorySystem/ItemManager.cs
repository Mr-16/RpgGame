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
        public Equipment Equipment;
        public Inventory Inventory;
        public ItemManager(Equipment equipment, Inventory inventory)
        {
            Equipment = equipment;
            Inventory = inventory;
        }

        public void EquipFromInv(int invIndex)
        {
            if (invIndex < 0 || invIndex >= Inventory.ItemList.Count) return;

            ItemInstance equip = Inventory.ItemList[invIndex];
            if (equip == null) return;
            if (!equip.Data.CompSet.Contains(ItemComponentType.EquippableComponent)) return;
            ItemInstance oldEquip = Equipment.GetEquip(equip.Data.EquipType);

            Equipment.Equip(equip);

            if (oldEquip != null)
            {
                Inventory.SetItem(oldEquip, invIndex);
            }
            else
            {
                Inventory.SetItem(null, invIndex);
            }
        }

        //卸下后自动在背包找空格子(右键卸下)
        public void UnequipToInv(EquipType equipType)
        {
            ItemInstance equip = Equipment.GetEquip(equipType);
            if (equip == null) return;
            for(int i = 0; i < Inventory.Capacity; i++)
            {
                if (Inventory.ItemList[i] != null) continue;
                Inventory.SetItem(equip, i);
                Equipment.Unequip(equipType);
                break;
            }
        }

        //卸下后去指定格子(拖动卸下)
        public bool UnequipToInv(EquipType equipType, int invIndex)
        {
            ItemInstance equip = Equipment.GetEquip(equipType);
            if (equip == null) return false;

            if (Inventory.ItemList[invIndex] != null) return false;
            Equipment.Unequip(equipType);
            Inventory.SetItem(equip, invIndex);
            return true;
        }

    }
}
