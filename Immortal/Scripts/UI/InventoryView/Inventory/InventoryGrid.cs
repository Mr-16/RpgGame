using Godot;
using RpgGame.Scripts.Systems.InventorySystem;
using System;
using System.Collections.Generic;

public partial class InventoryGrid : GridContainer
{
	public InventoryManager Inventory;
	[Export] public PackedScene ItemSlotScene;
	public List<ItemSlotPanel> SlotList = new List<ItemSlotPanel>();
	public void Init(InventoryManager inventory)
	{
		Inventory = inventory;
		for (int i = 0; i < Inventory.Capacity; i++)
		{
			ItemSlotPanel slot = ItemSlotScene.Instantiate<ItemSlotPanel>();
			slot.ItemSwapped += (index1, index2) =>
			{
				Inventory.SwapItem(index1, index2);
			};
            slot.Item = inventory.ItemList[i];
			slot.Index = i;
            AddChild(slot);
			SlotList.Add(slot);
		}
		Inventory.ItemChanged += (index, item) =>
		{
			SlotList[index].SetItem(item);
		};
	}

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
