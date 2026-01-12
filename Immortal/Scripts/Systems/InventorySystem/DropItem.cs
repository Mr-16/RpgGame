using Godot;
using RpgGame.Scripts.Systems.InventorySystem;
using RpgGame.Scripts.Utilities;
using System;
using System.Collections.Generic;

public partial class DropItem : StaticBody2D
{
	private ItemInstance item;
    [Export] public InteractTipControl InteractTipCtrl;
	[Export] public Sprite2D EquipSprite;

    public override void _Ready()
	{
        InteractTipCtrl.SetTip("按E拾取");
		ShowTip(false);
		//初始化时随机生成装备的属性, 效果
    }

    public override void _Process(double delta)
	{
	}

	public void Init(Vector2 gPos)
	{
		GlobalPosition = gPos;
		GenerateEquip();
		EquipSprite.Texture = item.Data.Icon;
    }

	private void GenerateEquip()
	{
		item = ItemFactory.Instance().Create(GameConstants.FireEquip);
    }


    public void ShowTip(bool isShow)
	{
		if(isShow)
		{
			InteractTipCtrl.Show();
		}
		else
		{
            InteractTipCtrl.Hide();
        }
    }

	public ItemInstance TakeItem()
	{
		return item;
	}
}
