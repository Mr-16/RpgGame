using Godot;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.InventorySystem;
using System;

public partial class PlayerView : Control
{
	[Export] public ProgressBar HpPb;
	[Export] public ProgressBar EnergyPb;

    [Export] public ProgressBar MetalLevelPb;
    [Export] public Label MetalLevelLb;

    [Export] public ProgressBar WoodLevelPb;
    [Export] public Label WoodLevelLb;

    [Export] public ProgressBar WaterLevelPb;
    [Export] public Label WaterLevelLb;

    [Export] public ProgressBar FireLevelPb;
    [Export] public Label FireLevelLb;

    [Export] public ProgressBar EarthLevelPb;
    [Export] public Label EarthLevelLb;


  

    public override void _Ready()
	{
        
    }

    public override void _Process(double delta)
	{
	}
}
