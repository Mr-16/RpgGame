using Godot;
using Godot.Collections;
using RpgGame.Scripts.Datas;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.InventorySystem;
using System;
using System.Net.NetworkInformation;

public partial class EquipControl : Control
{
	private Equipment equipment;
    [Export] public EquipSlot WeaponSlot;
    [Export] public EquipSlot HelmetSlot;
    [Export] public EquipSlot RingSlot;
    [Export] public EquipSlot ArmorSlot;
    [Export] public EquipSlot BootSlot;
    //private Dictionary<EquipType, EquipSlot> slotMap = new Dictionary<EquipType, EquipSlot>();

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public void Init(Equipment equipment)
    {
        this.equipment = equipment;
        equipment.EquipChange += Equipment_EquipChange;
        //slotMap.Add(EquipType.Weapon, null);
        //slotMap.Add(EquipType.Helmet, null);
        //slotMap.Add(EquipType.Ring, null);
        //slotMap.Add(EquipType.Armor, null);
        //slotMap.Add(EquipType.Boot, null);
        //FixEventBus.Instance().EquipmentEquipped += EquipContorl_EquipmentEquipped;
        //WeaponSlot.Equipped += (equip) => OnEquipped(equip);
        //HelmetSlot.Equipped += (equip) => OnEquipped(equip);
        //RingSlot.Equipped += (equip) => OnEquipped(equip);
        //ArmorSlot.Equipped += (equip) => OnEquipped(equip);
        //BootSlot.Equipped += (equip) => OnEquipped(equip);

    }

    private void Equipment_EquipChange(EquipType equipType)
    {
        switch (equipType)
        {
            case EquipType.Weapon:
                WeaponSlot.SetEquip(equipment.TypeEquipMap[equipType]);
                break;
            case EquipType.Helmet:
                HelmetSlot.SetEquip(equipment.TypeEquipMap[equipType]);
                break;
            case EquipType.Ring:
                RingSlot.SetEquip(equipment.TypeEquipMap[equipType]);
                break;
            case EquipType.Armor:
                ArmorSlot.SetEquip(equipment.TypeEquipMap[equipType]);
                break;
            case EquipType.Boot:
                BootSlot.SetEquip(equipment.TypeEquipMap[equipType]);
                break;
        }
        //throw new NotImplementedException();
    }

    //   private void EquipContorl_EquipmentEquipped(EquipInstance equip)
    //   {
    //       EquipDataOld equipData = equip.Data as EquipDataOld;
    //       switch (equipData.EquipmentType)
    //       {
    //           case EquipTypeOld.Weapon:
    //               WeaponSlot.SetEquip(equip);
    //               break;
    //           case EquipTypeOld.Helmet:
    //               HelmetSlot.SetEquip(equip);
    //               break;
    //           case EquipTypeOld.Ring:
    //               RingSlot.SetEquip(equip);
    //               break;
    //           case EquipTypeOld.Armor:
    //               ArmorSlot.SetEquip(equip);
    //               break;
    //           case EquipTypeOld.Boot:
    //               BootSlot.SetEquip(equip);
    //               break;
    //       }
    //   }

    //   private void OnEquipped(EquipInstance equip)
    //   {
    //       equipment.Equip(equip);
    //   }
}
