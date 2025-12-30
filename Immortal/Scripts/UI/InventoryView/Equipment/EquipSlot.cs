using Godot;
using RpgGame.Scripts.Datas;
using RpgGame.Scripts.GameSystem;
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
    public EquipType SlotType;

    [Export]
    public TextureRect IconTr;

    public ItemInstance Equip;

    //public event Action<EquipInstance> Equipped;
    public event Action<int> EquipFromInv;

    public override void _Ready()
    {
    }
    public override void _Process(double delta)
    {
    }

    public void SetEquip(ItemInstance equip)
    {
        Equip = equip;
        IconTr.Texture = equip.Data.Icon;
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (Equip == null) return new Variant();
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
        if (!itemSlot.Item.Data.CompSet.Contains(ItemComponentType.EquippableComponent)) return false;
        if (itemSlot.Item.Data.EquipType != SlotType) return false;
        return true;
    }
    public override void _DropData(Vector2 atPosition, Variant data)
    {
        ItemSlotPanel itemSlot = data.As<ItemSlotPanel>();
        if (itemSlot == null) return;
        if (!itemSlot.Item.Data.CompSet.Contains(ItemComponentType.EquippableComponent)) return;
        EquipFromInv?.Invoke(itemSlot.Index);
    }
}
