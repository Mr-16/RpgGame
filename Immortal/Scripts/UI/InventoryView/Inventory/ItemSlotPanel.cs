using Godot;
using RpgGame.Scripts.InventorySystem;
using System;

public partial class ItemSlotPanel : PanelContainer
{

    [Export] public TextureRect IconTr;
    [Export] public Label CountLb;
    public ItemInstance Item;
    public int Index;
	public event Action<int, int> ItemSwapped;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public void SetItem(ItemInstance item)
    {
        Item = item;
        if (item == null)
        {
            IconTr.Texture = null;
            CountLb.Text = "";
            return;
        }
        IconTr.Texture = item.Data.Icon;
        CountLb.Text = item.Count > 1 ? item.Count.ToString() : "";
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (Item == null) return new Variant();

        TextureRect preview = new TextureRect();
        preview.Texture = IconTr.Texture;
        preview.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        preview.Size = IconTr.Size;
        preview.Position = -atPosition;
        Control container = new Control();
        container.AddChild(preview);
        SetDragPreview(container);

        return this;
    }
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        ItemSlotPanel slot = data.As<ItemSlotPanel>();
        if (slot == null) return false;
        return true;
    }
    public override void _DropData(Vector2 atPosition, Variant data)
    {
        ItemSlotPanel dragSlot = data.As<ItemSlotPanel>();
        if (dragSlot == null) return;

        ItemSwapped?.Invoke(Index, dragSlot.Index);
        //// 2. 获取被拖拽物品原来的槽位 (也就是它的父节点)
        //ItemSlotControl sourceSlot = dragSlot.GetParent<ItemSlotControl>();

        //if (this.ItemCtrl == null)
        //{
        //    // --- 情况 A: 当前槽位是空的，直接放入 ---
        //    if (sourceSlot != null) sourceSlot.ItemCtrl = null;
        //    this.ItemCtrl = dragSlot;
        //    dragSlot.Reparent(this);
        //}
        //else
        //{
        //    // --- 情况 B: 当前槽位已有物品，进行交换 ---
        //    ItemControl existingItem = this.ItemCtrl;
        //    if (sourceSlot != null)
        //    {
        //        existingItem.Reparent(sourceSlot);
        //        sourceSlot.ItemCtrl = existingItem;
        //    }
        //    dragSlot.Reparent(this);
        //    this.ItemCtrl = dragSlot;
        //}
        //GD.Print($"物品已交换/移动到 {this.Name}");
    }
}
