using Godot;
using RpgGame.Scripts.GameSystem;
using System;

public partial class StartMenu : Control
{
	[Export] public Button StartBtn;
	[Export] public Button SettingBtn;
	[Export] public Button StatisticsBtn;
	[Export] public Button ExitBtn;
    
    public override void _Ready()
	{
		StartBtn.Pressed += () => 
			GameManager.Instance().ChangeState(GameState.Play);
		ExitBtn.Pressed += () => 
			GetTree().Quit();
	}

	public override void _Process(double delta)
	{
	}
}
