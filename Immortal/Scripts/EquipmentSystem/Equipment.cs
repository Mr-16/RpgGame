using RpgGame.Scripts.Characters;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.InventorySystem.Old;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Equipments
{
    public class Equipment
    {
        public EquipInstance Weapon;
        public EquipInstance Helmet;
        public EquipInstance Ring;
        public EquipInstance Armor;
        public EquipInstance Boot;

        public void Equip(EquipInstance equip)
        {
            if (equip.Data.ItemType != ItemTypeOld.Equipment) return;
                //if (equip.Data is not EquipmentData equipData) return;
            EquipDataOld equipData = equip.Data as EquipDataOld;
            switch (equipData.EquipmentType)
            {
                case EquipTypeOld.Weapon:
                    Weapon = equip;
                    break;
                case EquipTypeOld.Helmet:
                    Helmet = equip;
                    break;
                case EquipTypeOld.Ring:
                    Ring = equip;
                    break;
                case EquipTypeOld.Armor:
                    Armor = equip;
                    break;
                case EquipTypeOld.Boot:
                    Boot = equip;
                    break;
            }
            FixEventBus.Instance().PublishEquipmentEquipped(equip);
        }

        public void UnEquip(EquipInstance equip)
        {

        }
    }
}
