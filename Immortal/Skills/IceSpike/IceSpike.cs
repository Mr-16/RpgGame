using Godot;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class IceSpike : Node2D
{
	public Vector2 Dir;
	public float Dmg = 50;
    private float speed = 300;
	private float range = 30;//伤害范围
	private float rangeSq;
	


	public override async void _Ready()
	{
		rangeSq = range * range;
		await Task.Delay(3000);
        if (!IsInstanceValid(this)) return;
        if (!IsInsideTree()) return;
        QueueFree();
    }

    public override void _Process(double delta)
    {
		base._Process(delta);

		List<Enemy> enemyList =  EnemyManager.Instance().EnemyList;
		foreach(Enemy curEnemy in enemyList)
		{
			if (curEnemy.GlobalPosition.DistanceSquaredTo(GlobalPosition) > rangeSq) continue;
			curEnemy.TakeDmg(Dmg);
            QueueFree();
			return;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		base._Process(delta);
		GlobalPosition += Dir * (float)delta * speed;
	}
}
