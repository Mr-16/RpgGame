using Godot;
using System;

public partial class ItemSlot : Control
{
	[Export]
	public TextureRect IconTr;

	[Export]
	public Label CountLb;

	[Export]
	public Button Btn;

	public override void _Ready()
	{



		//Btn.Pressed += () => { GD.Print(IconTr.SceneFilePath); };

    }

	public override void _Process(double delta)
	{
	}

  //  public override Variant _GetDragData(Vector2 atPosition)
  //  {
		//GD.Print("_GetDragData");
  //      return base._GetDragData(atPosition);
  //  }
    //public override void _DropData(Vector2 atPosition, Variant data)
    //{
    //    base._DropData(atPosition, data);
    //    GD.Print("_DropData");
    //}
    //public override Variant _GetDragData(Vector2 atPosition)
    //{
    //    GD.Print("_GetDragData");

    //    // 设置拖拽预览（必须）
    //    var preview = new TextureRect();
    //    preview.Texture = IconTr.Texture;
    //    preview.CustomMinimumSize = new Vector2(48, 48);
    //    SetDragPreview(preview);

    //    return this; // 或 return itemInstance;
    //}
    //public override bool _CanDropData(Vector2 atPosition, Variant data)
    //{
    //    return true;
    //}
}
