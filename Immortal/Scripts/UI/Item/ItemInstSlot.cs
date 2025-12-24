//using Godot;
//using System;

//public partial class ItemInstSlot : PanelContainer
//{
//    public ItemInstCtrl ItemInstCtrl;

//    public override void _Ready()
//	{
//	}

//	public override void _Process(double delta)
//	{
//	}

//    public override bool _CanDropData(Vector2 atPosition, Variant data)
//    {
//        ItemInstCtrl itemInstCtrl = data.As<ItemInstCtrl>();
//        if (itemInstCtrl == null) return false;
//        return true;
//    }

//    public override void _DropData(Vector2 atPosition, Variant data)
//    {
//        ItemInstCtrl draggedItem = data.As<ItemInstCtrl>();
//        if (draggedItem == null) return;

//        // 2. 获取被拖拽物品原来的槽位 (也就是它的父节点)
//        ItemInstSlot sourceSlot = draggedItem.GetParent<ItemInstSlot>();

//        if (this.ItemInstCtrl == null)
//        {
//            // --- 情况 A: 当前槽位是空的，直接放入 ---
//            if (sourceSlot != null) sourceSlot.ItemInstCtrl = null;
//            this.ItemInstCtrl = draggedItem;
//            draggedItem.Reparent(this);
//        }
//        else
//        {
//            // --- 情况 B: 当前槽位已有物品，进行交换 ---
//            ItemInstCtrl existingItem = this.ItemInstCtrl;
//            if (sourceSlot != null)
//            {
//                existingItem.Reparent(sourceSlot);
//                sourceSlot.ItemInstCtrl = existingItem;
//            }
//            draggedItem.Reparent(this);
//            this.ItemInstCtrl = draggedItem;
//        }
//        GD.Print($"物品已交换/移动到 {this.Name}");
//    }

//}