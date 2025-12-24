using Godot;
using RpgGame.Scripts.InventorySystem;
using System;
using System.Collections.Generic;

public partial class InventoryGrid : GridContainer
{
	public VeryGoodInventory Inventory;
    private Dictionary<ItemSlot, ItemSlotControl> slotCtrlMap = new Dictionary<ItemSlot, ItemSlotControl>();

    [Export]
    public PackedScene ItemSlotCtrlScene;

    public void BuildInventoryGrid()
    {
        Inventory.OnSlotChanged += Inventory_OnSlotChanged;


        foreach (var slot in Inventory.SlotList)
        {
            ItemSlotControl ctrl = ItemSlotCtrlScene.Instantiate<ItemSlotControl>();
            ctrl.SetSlot(slot);          // 绑定数据
            AddChild(ctrl);

            slotCtrlMap.Add(slot, ctrl);
        }
    }


    private void Inventory_OnSlotChanged(ItemSlot slot)
    {
        if (slotCtrlMap.TryGetValue(slot, out var ctrl))
            ctrl.Refresh();
    }


    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
