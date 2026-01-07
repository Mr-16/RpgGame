using Godot;
using Godot.Collections;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.Systems.InventorySystem;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public partial class EquipControl : Control
{
	private EquipmentManager equipment;
    [Export] public EquipSlot Slot0;
    [Export] public EquipSlot Slot1;
    [Export] public EquipSlot Slot2;
    [Export] public EquipSlot Slot3;
    [Export] public EquipSlot Slot4;

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

    private void Equipment_EquipChange(int equipIndex, ItemInstance equip)
    {
        switch (equipIndex)
        {
            case 0:
                Slot0.SetEquip(equip);
                break;
            case 1:
                Slot1.SetEquip(equip);
                break;
            case 2:
                Slot2.SetEquip(equip);
                break;
            case 3:
                Slot3.SetEquip(equip);
                break;
            case 4:
                Slot4.SetEquip(equip);
                break;
        }
    }

}
