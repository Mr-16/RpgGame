using Godot;
using System;

public partial class InteractTipControl : Control
{
	[Export] public Label TipLabel;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void SetTip(string tip)
	{
        TipLabel.Text = tip;
	}
}
