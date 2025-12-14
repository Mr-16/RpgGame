using Godot;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;

public partial class FireBall : Node2D
{
    private float duration = 3;
    private float timer = 0;

    [Export]
    public float Range = 50;//火球的碰撞范围
    private float rangeSq;
    [Export]
    public float ExplosionRange = 200;
    private float explosionRangSq;

    [Export]
    public float Dmg = 1;


    private float Speed = 300;

    public Vector2 Dir;

    //private bool isHit = false;

    public override void _Ready()
	{
        rangeSq = Range * Range;
        explosionRangSq = ExplosionRange * ExplosionRange;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        GlobalPosition += Dir * Speed * (float)delta;



        timer += (float)delta;
        if (timer > duration)
        {
            QueueFree();
            return;
        }


        List<Enemy> enemyList = EnemyManager.Instance().EnemyList;
        foreach (Enemy enemy in enemyList)
        {
            if (GlobalPosition.DistanceSquaredTo(enemy.Position) > rangeSq) continue;

            foreach (Enemy tarEnemy in enemyList)
            {
                if (GlobalPosition.DistanceSquaredTo(tarEnemy.Position) > explosionRangSq) continue;
                tarEnemy.TakeDmg(Dmg);
            }
            QueueFree();
            return;
        }

    }
}
