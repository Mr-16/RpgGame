using Godot;
using RpgGame.Scripts.Characters;
using RpgGame.Scripts.Equipments;
using System;

public partial class CharacterBase : CharacterBody2D
{
    public float CurHealth;
    public float CurMana;
    public float CurStam;
    public Vector2 CurDir = Vector2.Right;
    public int Level;

    //属性分为基础属性和最终属性, 最终属性只在变更时改动(穿卸装备, 加减buff等), 目的是优化性能, 不用每次用都动态计算
    public CharAttribute BaseAttr = new CharAttribute();
    public CharAttribute FinalAttr = new CharAttribute();
    //public EquipmentSystem EquipmentSystem = new EquipmentSystem();

    public Equipment Weapon = new Equipment("新手剑", EquipmentType.Sword, EquipmentGrade.White, 1);
    public Equipment Helmet = new Equipment("新手头盔", EquipmentType.Helmet, EquipmentGrade.White, 1);
    public Equipment Armor = new Equipment("新手盔甲", EquipmentType.Armor, EquipmentGrade.White, 1);
    public Equipment Boot = new Equipment("新手靴", EquipmentType.Boot, EquipmentGrade.White, 1);

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
        
	}

    public override void _PhysicsProcess(double delta)
    {
        
    }

    
    public void TakeDmg(float dmg)
    {
        CurHealth -= dmg;
        if(CurHealth <= 0)
        {
            CurHealth = 0;
            Die();
        }
    }
    protected void Die()
    {
        QueueFree();
    }
}
