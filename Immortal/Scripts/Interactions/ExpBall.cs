using Godot;
using RpgGame.Scripts.Characters.Players;
using RpgGame.Scripts.GameSystem;
using System;

public partial class ExpBall : Node2D
{
	public int Exp = 1;

	private Player player;
	private float chaseRange = 300;
	private float chaseRangeSq;
	private float catchRange = 50;
	private float catchRangeSq;
	private bool isChasing = false;
	private float moveSpeed = 300;

	private float timer = 0;
	private float duration = 0.1f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GameManager.Instance().Player;
        chaseRangeSq = chaseRange * chaseRange;
		catchRangeSq = catchRange * catchRange;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
            player.AddExp(Exp);
            QueueFree();
            return;
        }
        if (disSq < chaseRangeSq)
        {
            isChasing = true;
        }
    }
    //public override void _PhysicsProcess(double delta)
    //{
    //    base._PhysicsProcess(delta);

    //}
}
