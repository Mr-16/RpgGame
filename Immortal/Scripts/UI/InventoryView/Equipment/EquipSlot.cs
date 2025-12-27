using Godot;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.InventorySystem.Old;
using System;


public enum EquipSlotType
{
	WeaponSlot,
	HelmetSlot,
	ArmorSlot,
	RingSlot,
	BootSlot,
}

public partial class EquipSlot : Control
{
	[Export]
	public EquipTypeOld SlotType;

	[Export]
	public TextureRect IconTr;

	public EquipInstance Equip;

	public event Action<EquipInstance> Equipped;

    public override void _Ready()
	{
	}



    public override void _Process(double delta)
	{
	}

	public void SetEquip(EquipInstance equip)
	{
        Equip = equip;
        IconTr.Texture = equip.Data.Icon;
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if(Equip == null) return new Variant();
		TextureRect preview = new TextureRect();
        preview.Texture = IconTr.Texture;
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
        if (gdObj is not ItemSlotPanel itemSlot) return false;
        if (itemSlot.Item.Data.ItemType != ItemTypeOld.Equipment) return false;
        EquipDataOld equipData = (EquipDataOld)itemSlot.Item.Data;
        if (equipData.EquipmentType != SlotType) return false;
        return true;
    }
    public override void _DropData(Vector2 atPosition, Variant data)
    {
        ItemSlotPanel itemSlot = data.As<ItemSlotPanel>();
		if(itemSlot == null) return;
        if (itemSlot.Item.Data.ItemType != ItemTypeOld.Equipment) return;
		EquipInstance equip = (EquipInstance)itemSlot.Item;
        Equipped?.Invoke(equip);
    }
}
