using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Systems.InventorySystem
{
    public class EquipmentManager
    {
        public Dictionary<EquipType, ItemInstance> TypeEquipMap = new Dictionary<EquipType, ItemInstance>();
        public event Action<EquipType> EquipChange;
        public ItemInstance Equip(ItemInstance equip)
        {
            TypeEquipMap.TryGetValue(equip.Data.EquipType, out ItemInstance oldEquip);
            TypeEquipMap[equip.Data.EquipType] = equip;
            EquipChange?.Invoke(equip.Data.EquipType);
            return oldEquip;
        }

        public ItemInstance Unequip(EquipType type)
        {
            if (TypeEquipMap.TryGetValue(type, out ItemInstance equip))
            {
                TypeEquipMap[type] = null;
                EquipChange?.Invoke(type);
                return equip;
            }
            return null;
        }

        public ItemInstance GetEquip(EquipType type)
        {
            TypeEquipMap.TryGetValue(type, out ItemInstance equip);
            return equip;
        }
    }
}
