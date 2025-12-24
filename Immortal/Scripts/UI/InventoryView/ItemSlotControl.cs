using Godot;
using RpgGame.Scripts.InventorySystem;
using System;

public partial class ItemSlotControl : PanelContainer
{
	private ItemSlot itemSlot;//持有这个引用目的是知道自己是哪格

    [Export]
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

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        ItemControl itemCtrl = data.As<ItemControl>();
        if (itemCtrl == null) return false;
        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        ItemControl draggedItem = data.As<ItemControl>();
        if (draggedItem == null) return;

        // 2. 获取被拖拽物品原来的槽位 (也就是它的父节点)
        ItemSlotControl sourceSlot = draggedItem.GetParent<ItemSlotControl>();

        if (this.ItemCtrl == null)
        {
            // --- 情况 A: 当前槽位是空的，直接放入 ---
            if (sourceSlot != null) sourceSlot.ItemCtrl = null;
            this.ItemCtrl = draggedItem;
            draggedItem.Reparent(this);
        }
        else
        {
            // --- 情况 B: 当前槽位已有物品，进行交换 ---
            ItemControl existingItem = this.ItemCtrl;
            if (sourceSlot != null)
            {
                existingItem.Reparent(sourceSlot);
                sourceSlot.ItemCtrl = existingItem;
            }
            draggedItem.Reparent(this);
            this.ItemCtrl = draggedItem;
        }
        GD.Print($"物品已交换/移动到 {this.Name}");
    }

}

