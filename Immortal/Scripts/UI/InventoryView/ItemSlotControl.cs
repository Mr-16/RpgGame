using Godot;
using RpgGame.Scripts.InventorySystem;
using System;

public partial class ItemSlotControl : Control
{
	private ItemSlot itemSlot;//持有这个引用目的是知道自己是哪格

    public PackedScene ItemCtrlScene;
    public ItemControl ItemCtrl;

	public override void _Ready()
	{
    }

	public override void _Process(double delta)
	{
	}

    public void Refresh()
    {
        if (itemSlot.Item == null)
        {
            if (ItemCtrl != null)
            {
                ItemCtrl.QueueFree();
                ItemCtrl = null;
            }
        }
        else
        {
            if (ItemCtrl == null)
            {
                ItemCtrl = ItemCtrlScene.Instantiate<ItemControl>();
                AddChild(ItemCtrl);
            }
            ItemCtrl.Bind(itemSlot.Item);
            
        }
    }


    public void SetSlot(ItemSlot slot)
	{
		itemSlot = slot;
	}
}
