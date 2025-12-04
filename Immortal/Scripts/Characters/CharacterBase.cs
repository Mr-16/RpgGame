using Godot;
using RpgGame.Scripts.Equipments;
using System;

public partial class CharacterBase : CharacterBody2D
{
    public float curHealth;
    public float curMana;
    public Vector2 curDir = Vector2.Right;
    public int level;

    //基础
    public float baseMaxHealth;
    public float baseMaxMana;
    public float baseMoveSpeed;

    //攻击
    public float baseAtkSpeed;
    public float basePhyAtk;
    public float basePhyPen;
    public float baseMagAtk;
    public float baseMagPen;
    public float baseCritRate;
    public float baseCritDamage;

    //防御
    public float basePhyDef;
    public float baseMagDef;

    //特殊
    public float baseLifeSteal;

    //基础
    public float maxHealth => baseMaxHealth + weapon.maxHealthBonus + ;
    public float maxMana;
    public float moveSpeed;

    //攻击
    public float atkSpeed;
    public float phyAtk;
    public float phyPen;
    public float magAtk;
    public float magPen;
    public float critRate;
    public float critDamage;

    //防御
    public float phyDef;
    public float magDef;

    //特殊
    public float lifeSteal;

    Equipment weapon = null;
    Equipment helmet = null;
    Equipment armor = null;
    Equipment boot = null;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
        
	}

    public override void _PhysicsProcess(double delta)
    {
        
    }
}
