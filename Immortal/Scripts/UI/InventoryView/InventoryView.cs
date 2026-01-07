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

        EquipControl.WeaponSlot.EquipFromInv += EquipFromInv;
        EquipControl.HelmetSlot.EquipFromInv += EquipFromInv;
        EquipControl.RingSlot.EquipFromInv += EquipFromInv;
        EquipControl.ArmorSlot.EquipFromInv += EquipFromInv;
        EquipControl.BootSlot.EquipFromInv += EquipFromInv;

		for(int i = 0; i < InventoryGrid.SlotList.Count; i++)
		{
            InventoryGrid.SlotList[i].UnequipToInv += UnequipToInv;

        }
    }

    private void UnequipToInv(EquipType equipType, int invIndex)
    {
        itemManager.UnequipToInv(equipType, invIndex);
    }

    private void EquipFromInv(int invIndex)
    {
        itemManager.EquipFromInv(invIndex);
    }

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
