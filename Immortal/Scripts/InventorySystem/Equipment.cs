using RpgGame.Scripts.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.InventorySystem
{
    public class Equipment
    {
        public Dictionary<EquipType, EquipInstance> TypeEquipMap = new Dictionary<EquipType, EquipInstance>();
        public EquipInstance Equip(EquipInstance equip)
        {
            TypeEquipMap.TryGetValue(equip.Data.EquipType, out EquipInstance oldEquip);
            TypeEquipMap[equip.Data.EquipType] = equip;
            return oldEquip;
        }

        public EquipInstance Unequip(EquipType type)
        {
            if (TypeEquipMap.TryGetValue(type, out EquipInstance equip))
            {
                TypeEquipMap.Remove(type);
                return equip;
            }
            return null;
        }

        public EquipInstance GetEquip(EquipType type)
        {
            TypeEquipMap.TryGetValue(type, out EquipInstance equip);
            return equip;
        }
    }
}
