using Godot;
using RpgGame.Scripts.Systems.InventorySystem;
using System;

public partial class InventoryView : Control
{
	[Export]
	public InventoryGrid InventoryGrid;

	[Export]
	public EquipControl EquipControl;

	private ItemManager itemManager;

	public void Init(ItemManager itemManager)
	{
		this.itemManager = itemManager;
		InventoryGrid.Init(itemManager.InventoryManager);
		EquipControl.Init(itemManager.EquipmentManager);

        EquipControl.Slot0.EquipFromInv += EquipFromInv;
        EquipControl.Slot1.EquipFromInv += EquipFromInv;
        EquipControl.Slot2.EquipFromInv += EquipFromInv;
        EquipControl.Slot3.EquipFromInv += EquipFromInv;
        EquipControl.Slot4.EquipFromInv += EquipFromInv;

		for(int i = 0; i < InventoryGrid.SlotList.Count; i++)
		{
            InventoryGrid.SlotList[i].UnequipToInv += UnequipToInv;

        }
    }

    private void UnequipToInv(int equipIndex, int invIndex)
    {
        itemManager.UnequipToInv(equipIndex, invIndex);
    }

    private void EquipFromInv(int invIndex, int equipIndex)
    {
        itemManager.EquipFromInv(invIndex, equipIndex);
    }

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
