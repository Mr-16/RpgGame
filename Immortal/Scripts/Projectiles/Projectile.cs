using Godot;
using RpgGame.Scripts.AttributeSystem;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.Characters.Players;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.Global;
using System;

public partial class Projectile : Node2D
{

	[Export]
	public Area2D Area;
    [Export]
    public ColorRect ColorRect;

    //伤害/属性/额外效果
    private Player attacker;		//用来算伤害
    private WuXingType wuXingType;

    private Vector2 dir;            //方向
	private float maxFlyDisSq;			//飞行距离
	private float speed;            //飞行速度

    private Vector2 startPos;


    public override void _Ready()
	{
        
        Area.BodyEntered += Area_BodyEntered;
        
    }
    private void Area_BodyEntered(Node2D body)
    {
        if (body is not Enemy enemy) return;
        enemy.TakeDmg(DamageCalculator.CalculateDamage(attacker.AttrContainer, enemy.AttrContainer));
        QueueFree();
    }

    public override void _Process(double delta)
	{
        GlobalPosition += dir * speed * (float)delta;
	    if( GlobalPosition.DistanceSquaredTo(startPos) >= maxFlyDisSq)
        {
            QueueFree();
        }
    }

	public void Init(Player attacker, WuXingType wuXingType, Vector2 gPos, Vector2 dir, float maxFlyDisSq, float speed)
	{
		this.attacker = attacker;
		this.wuXingType = wuXingType;
        GlobalPosition = gPos;
        startPos = GlobalPosition;
        this.dir = dir;
        Rotation = this.dir.Angle();
        this.maxFlyDisSq = maxFlyDisSq;
		this.speed = speed;

        ShaderMaterial mat = ColorRect.Material as ShaderMaterial;
        mat = (ShaderMaterial)mat.Duplicate();
        ColorRect.Material = mat;
        mat.SetShaderParameter("fire_color", WuXingHelper.GetColor(wuXingType));

    }
}
