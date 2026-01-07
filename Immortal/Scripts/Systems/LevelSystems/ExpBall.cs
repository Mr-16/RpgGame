using Godot;
using RpgGame.Scripts.Characters.Players;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.Utilities;
using RpgGame.Scripts.Utilities.Enums;
using System;

public partial class ExpBall : RigidBody2D
{
	private int exp;
    private WuXingType type;
    [Export]
    public ColorRect ColorRect;

    private Player player;
	private float chaseRange = 300;
	private float chaseRangeSq;
	private float catchRange = 50;
	private float catchRangeSq;
	private bool isChasing = false;
	private float moveSpeed = 300;

	private float timer = 0;
	private float duration = 0.1f;

    public void Init(WuXingType type, int exp, Vector2 gPos)
    {
        this.exp = exp;
        this.type = type;
        this.GlobalPosition = gPos;
        ColorRect.Color = WuXingHelper.GetColor(type);
    }

    public override void _Ready()
	{
		player = GameManager.Instance().Player;
        chaseRangeSq = chaseRange * chaseRange;
		catchRangeSq = catchRange * catchRange;
    }

	public override void _Process(double delta)
	{
        if (!IsInstanceValid(player))
        {
            QueueFree();
            return;
        }

        if (isChasing)
        {
            GlobalPosition += (player.GlobalPosition - GlobalPosition).Normalized() * moveSpeed * (float)delta;
        }


        timer += (float)delta;
        if (timer < duration) return;
        timer = 0;
        float disSq = GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
        if (disSq < catchRangeSq)
        {
            player.LevelSystem.TakeExp(type, exp);
            QueueFree();
            return;
        }
        if (disSq < chaseRangeSq)
        {
            isChasing = true;
        }
    }
}
