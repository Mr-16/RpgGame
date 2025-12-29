using Godot;
using RpgGame.Scripts.Characters;
using System;
using System.Formats.Tar;

public partial class CharacterBase : CharacterBody2D
{
    public float CurHealth;
    public float CurMana;
    public float CurStam;
    public Vector2 CurDir = Vector2.Right;
    public int Level;

    [Export]
    public PackedScene FloatTextLabel;

    //属性分为基础属性和最终属性, 最终属性只在变更时改动(穿卸装备, 加减buff等), 目的是优化性能, 不用每次用都动态计算
    public CharAttribute BaseAttr = new CharAttribute();
    public CharAttribute FinalAttr = new CharAttribute();
    //public EquipmentSystem EquipmentSystem = new EquipmentSystem();

    //public Equipment Weapon = new Equipment("新手剑", EquipmentType.Sword, EquipmentGrade.White, 1);
    //public Equipment Helmet = new Equipment("新手头盔", EquipmentType.Helmet, EquipmentGrade.White, 1);
    //public Equipment Armor = new Equipment("新手盔甲", EquipmentType.Armor, EquipmentGrade.White, 1);
    //public Equipment Boot = new Equipment("新手靴", EquipmentType.Boot, EquipmentGrade.White, 1);

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
        label.Init(dmg.ToString(), new Color(1, 0, 0, 1));
        GetTree().CurrentScene.AddChild(label);
    }

    protected virtual void Die()
    {
        //QueueFree();
    }

    //伤害计算公式
    public float CalcPhyDamage(CharAttribute targetAttr)
    {
        // 1. 计算有效防御
        float effDef = (targetAttr.PhyDef * (1 - FinalAttr.PhyPen)) - FinalAttr.PhyPenFlat;
        if (effDef < 0) effDef = 0; // 防御不可能为负数（可选）

        // 2. 计算未暴击时的基础伤害
        float dmg = FinalAttr.PhyAtk - effDef;
        if (dmg < 1) dmg = 1;

        // 3. 计算暴击
        Random rd = new Random();
        bool isCrit = rd.NextDouble() < FinalAttr.CritRate;
        float finalDmg = isCrit ? dmg * FinalAttr.CritMult : dmg;

        // 4. 吸血
        float heal = finalDmg * FinalAttr.LifeSteal;
        CurHealth = MathF.Min(FinalAttr.MaxHealth, CurHealth + heal);
        return finalDmg;
    }

    public float CalcMagDamage(CharAttribute targetAttr)
    {
        // 1. 计算有效防御
        float effDef = (targetAttr.MagDef * (1 - FinalAttr.MagPen)) - FinalAttr.MagPenFlat;
        if (effDef < 0) effDef = 0; // 防御不可能为负数（可选）

        // 2. 计算未暴击时的基础伤害
        float dmg = FinalAttr.MagAtk - effDef;
        if (dmg < 1) dmg = 1;

        // 3. 计算暴击
        Random rd = new Random();
        bool isCrit = rd.NextDouble() < FinalAttr.CritRate;
        float finalDmg = isCrit ? dmg * FinalAttr.CritMult : dmg;

        // 4. 吸血
        float heal = finalDmg * FinalAttr.LifeSteal;
        CurHealth = MathF.Min(FinalAttr.MaxHealth, CurHealth + heal);
        return finalDmg;
    }

}
