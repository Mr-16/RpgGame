using Godot;
using RpgGame.Scripts.Systems.InventorySystem;
using System;

public partial class ItemSlotPanel : PanelContainer
{

    [Export] public TextureRect IconTr;
    [Export] public Label CountLb;
    public ItemInstance Item;
    public int Index;
	public event Action<int, int> ItemSwapped;
    public event Action<EquipType, int> UnequipToInv;

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
        if (data.VariantType != Variant.Type.Object) return false;
        GodotObject gdObj = data.As<GodotObject>();
        if (gdObj is not ItemSlotPanel && gdObj is not EquipSlot) return false;
        return true;
    }
    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (data.VariantType != Variant.Type.Object) return;
        GodotObject gdObj = data.As<GodotObject>();
        if(gdObj is ItemSlotPanel itemSlot)//背包内部换
        {
            ItemSwapped?.Invoke(Index, itemSlot.Index);
            return;
        }
        if(gdObj is EquipSlot slot)//从装备槽卸到背包上
        {
            UnequipToInv?.Invoke(slot.SlotType, Index);
        }

    }
}
