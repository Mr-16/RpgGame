using Godot;
using Godot.Collections;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.Systems.InventorySystem;
using System;
using System.Net.NetworkInformation;

public partial class EquipControl : Control
{
	private EquipmentManager equipment;
    [Export] public EquipSlot WeaponSlot;
    [Export] public EquipSlot HelmetSlot;
    [Export] public EquipSlot RingSlot;
    [Export] public EquipSlot ArmorSlot;
    [Export] public EquipSlot BootSlot;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public void Init(EquipmentManager equipment)
    {
        this.equipment = equipment;
        equipment.EquipChange += Equipment_EquipChange;
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
    }

}
