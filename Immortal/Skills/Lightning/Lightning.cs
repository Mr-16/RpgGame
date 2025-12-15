using Godot;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using static Lightning;

public partial class Lightning : Node2D
{
    public Enemy curTarget;
	public float Dmg = 1;

    //连锁电
    private float chaseRange = 300;//锁定范围
    private float chaseRangeSq;
    private float range = 50;//捕获范围
    private float rangeSq;
    private int remainChainCount = 10;
    private float lightDuration = 0.2f;
    private float timer = 0;
    private float speed = 500;

    public LightningState curState = LightningState.Chase;
    public enum LightningState
    {
        Chase,
        Lighting,
    }

    //chase -> lighting -> chase -> lighting -> free

    public override void _Ready()
	{
        chaseRangeSq = chaseRange * chaseRange;
        rangeSq = range * range;
        ChangeState(LightningState.Chase);
    }

	public override void _Process(double delta)
	{
	}
    public override void _PhysicsProcess(double delta)
    {
        GD.Print(curState);
        Update((float)delta);
    }
    private void ChangeState(LightningState newState)
    {
        ExitState(curState);
        curState = newState;
        EnterState(curState);
    } 
    private void Update(float delta)
    {
        switch (curState)
        {
            case LightningState.Chase:
                if (!IsInstanceValid(curTarget))
                {
                    QueueFree();
                    return;
                }
                GlobalPosition += (float)delta * speed * (curTarget.GlobalPosition - GlobalPosition).Normalized();
                if(GlobalPosition.DistanceSquaredTo(curTarget.GlobalPosition) < rangeSq)
                {
                    ChangeState(LightningState.Lighting);
                    return;
                }
                return;
            case LightningState.Lighting:
                timer += delta;
                if (!IsInstanceValid(curTarget))
                {
                    QueueFree();
                    return;
                }
                GlobalPosition = curTarget.GlobalPosition;
                curTarget.TakeDmg(Dmg);
                if (timer < lightDuration) return;
                ChangeState(LightningState.Chase);
                return;
            default:
                return;
        }
    }
    private void EnterState(LightningState state)
    {
        switch (state)
        {
            case LightningState.Chase:
                if(remainChainCount == 0)
                {
                    QueueFree();
                }
                remainChainCount--;
                List<Enemy> enemyList = EnemyManager.Instance().EnemyList;
                Enemy target = null;
                float minDisSq = float.MaxValue;
                foreach(Enemy curEnemy in enemyList)
                {
                    if (curEnemy == curTarget) continue;
                    float curDisSq = GlobalPosition.DistanceSquaredTo(curEnemy.GlobalPosition);
                    if (curDisSq < minDisSq)
                    {
                        target = curEnemy;
                        minDisSq = curDisSq;
                    }
                }
                if(minDisSq > chaseRangeSq)
                {
                    QueueFree();
                }
                curTarget = target;
                return;
            case LightningState.Lighting:
                return;
            default:
                return;
        }
    }
    private void ExitState(LightningState state)
    {
        switch (curState)
        {
            case LightningState.Chase:
                return;
            case LightningState.Lighting:
                timer = 0;
                return;
            default:
                return;
        }
    }
}
