using Godot;
using RpgGame.Scripts.ItemSystem;

public partial class ItemInstCtrl : Control
{
    [Export]
    public TextureRect IconTr;
    [Export]
    public Label CountLb;

    public ItemInstance ItemInst { get; private set; }

    public void SetItem(ItemInstance inst)
    {
        ItemInst = inst;

        // 设置图标
        if (!string.IsNullOrEmpty(inst.ItemData.IconPath))
            IconTr.Texture = GD.Load<Texture2D>(inst.ItemData.IconPath);
        else
            IconTr.Texture = null; // 或者用占位图

        Refresh();
    }

    // 刷新数量显示
    public void Refresh()
    {
        if (ItemInst == null)
        {
            CountLb.Text = "";
            IconTr.Texture = null;
            return;
        }

        // 只有数量大于1才显示
        CountLb.Text = ItemInst.Count > 1 ? ItemInst.Count.ToString() : "";
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
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
}
