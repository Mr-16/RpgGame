using Godot;
using RpgGame.Scripts.InventorySystem;
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
		InventoryGrid.Init(itemManager.Inventory);
		EquipControl.Init(itemManager.Equipment);
    }

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
