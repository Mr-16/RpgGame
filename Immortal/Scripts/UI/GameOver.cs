using Godot;
using RpgGame.Scripts.GameSystem;
using System;

public partial class GameOver : Control
{
	[Export]
	public Button BackMenuBtn;
    [Export]
    public Button ExitBtn;

    public override void _Ready()
	{
		BackMenuBtn.Pressed += () => GameManager.Instance().ChangeState(GameState.StartMenu);
        ExitBtn.Pressed += () => GetTree().Quit();
    }

	public override void _Process(double delta)
	{
	}
}
