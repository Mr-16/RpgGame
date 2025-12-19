using Godot;
using System;

public partial class ItemSlot : PanelContainer
{
	[Export]
	public TextureRect IconTr;

	[Export]
	public Label NameLb;

	[Export]
	public Label CountLb;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
