using Godot;
using RpgGame.Scripts.GameSystem;
using System;
using System.Net.NetworkInformation;

public partial class EquipControl : Control
{
	//private Equipment equipment;

	[Export]
	public EquipSlot WeaponSlot;

    [Export]
    public EquipSlot HelmetSlot;

    [Export]
    public EquipSlot ArmorSlot;

    [Export]
    public EquipSlot RingSlot;

    [Export]
    public EquipSlot BootSlot;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	//public void Init(Equipment equipment)
	//{
	//	this.equipment = equipment;
 //       FixEventBus.Instance().EquipmentEquipped += EquipContorl_EquipmentEquipped;
 //       WeaponSlot.Equipped += (equip) => OnEquipped(equip);
 //       HelmetSlot.Equipped += (equip) => OnEquipped(equip);
 //       RingSlot.Equipped += (equip) => OnEquipped(equip);
 //       ArmorSlot.Equipped += (equip) => OnEquipped(equip);
 //       BootSlot.Equipped += (equip) => OnEquipped(equip);

 //   }

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
