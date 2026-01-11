using Godot;
using System;

public partial class HUD : CanvasLayer
{
	[Export] public DialogueControl DialogueCtrl;

	private static HUD instance; 

	public static HUD Instance() => instance;

	public override void _Ready()
	{
		instance = this;
    }

	public override void _Process(double delta)
	{
	}
}
