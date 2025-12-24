using Godot;
using RpgGame.Scripts.InventorySystem;
using System;

public partial class ItemControl : Control
{
	[Export]
	public TextureRect IconTr;

	[Export]
	public Label CountLb;

	private ItemInstance item;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void Bind(ItemInstance item)
    {
        this.item = item;
        Refresh();
    }

    public void Refresh()
    {
        if (item == null) return;

        IconTr.Texture = item.ItemData.Icon;
        CountLb.Text = item.Count > 1 ? item.Count.ToString() : "";
    }
}
