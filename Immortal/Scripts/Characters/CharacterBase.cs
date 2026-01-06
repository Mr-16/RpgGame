using Godot;
using RpgGame.Scripts.AttributeSystem;
using RpgGame.Scripts.Characters;
using System;
using System.Formats.Tar;

public partial class CharacterBase : CharacterBody2D
{
    public float CurHealth;
    public float CurEnergy;
    //public float CurStam;
    public Vector2 CurDir = Vector2.Right;

    [Export]
    public PackedScene FloatTextLabel;


    public AttributeContainer AttrContainer = new AttributeContainer();

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
        
	}

    public override void _PhysicsProcess(double delta)
    {
        
    }

    
    public virtual void TakeDmg(float dmg)
    {
        CurHealth -= dmg;
        if(CurHealth <= 0)
        {
            CurHealth = 0;
            Die();
        }
        FloatText label = FloatTextLabel.Instantiate<FloatText>();
        label.GlobalPosition = GlobalPosition;
        if(dmg != 0)
        {
            label.Init(dmg.ToString("F1"), new Color(1, 0, 0, 1));
        }
        else
        {
            label.Init("Miss", new Color(0, 1, 0, 0.8f));
        }
        GetTree().CurrentScene.AddChild(label);
    }

    protected virtual void Die()
    {
        //QueueFree();
    }


    protected virtual void InitAttr()
    {

    }

    ////伤害计算公式
    //public float CalcPhyDamage(CharAttribute targetAttr)
    //{
    //    // 1. 计算有效防御
    //    float effDef = (targetAttr.PhyDef * (1 - FinalAttr.PhyPen)) - FinalAttr.PhyPenFlat;
    //    if (effDef < 0) effDef = 0; // 防御不可能为负数（可选）

    //    // 2. 计算未暴击时的基础伤害
    //    float dmg = FinalAttr.PhyAtk - effDef;
    //    if (dmg < 1) dmg = 1;

    //    // 3. 计算暴击
    //    Random rd = new Random();
    //    bool isCrit = rd.NextDouble() < FinalAttr.CritRate;
    //    float finalDmg = isCrit ? dmg * FinalAttr.CritMult : dmg;

    //    // 4. 吸血
    //    float heal = finalDmg * FinalAttr.LifeSteal;
    //    CurHealth = MathF.Min(FinalAttr.MaxHealth, CurHealth + heal);
    //    return finalDmg;
    //}

    //public float CalcMagDamage(CharAttribute targetAttr)
    //{
    //    // 1. 计算有效防御
    //    float effDef = (targetAttr.MagDef * (1 - FinalAttr.MagPen)) - FinalAttr.MagPenFlat;
    //    if (effDef < 0) effDef = 0; // 防御不可能为负数（可选）

    //    // 2. 计算未暴击时的基础伤害
    //    float dmg = FinalAttr.MagAtk - effDef;
    //    if (dmg < 1) dmg = 1;

    //    // 3. 计算暴击
    //    Random rd = new Random();
    //    bool isCrit = rd.NextDouble() < FinalAttr.CritRate;
    //    float finalDmg = isCrit ? dmg * FinalAttr.CritMult : dmg;

    //    // 4. 吸血
    //    float heal = finalDmg * FinalAttr.LifeSteal;
    //    CurHealth = MathF.Min(FinalAttr.MaxHealth, CurHealth + heal);
    //    return finalDmg;
    //}

}
