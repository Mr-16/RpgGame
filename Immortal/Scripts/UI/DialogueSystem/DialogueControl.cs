using Godot;
using RpgGame.Scripts.Systems.DialogueSystem;
using RpgGame.Scripts.Utilities;
using System;
using System.ComponentModel;

public partial class DialogueControl : Control
{
	[Export] public TextureRect HeadTr;
	[Export] public Label NameLb;
	[Export] public Label LineTextLb;
	[Export] public VBoxContainer OptVbc;

    private DialogueNode curNode;

    public override void _Ready()
	{
		Hide();
        //DialogueManager.Instance().OnEnterNode += DialogueControl_OnEnterNode;
        //DialogueManager.Instance().OnLineChanged += DialogueControl_OnLineChanged; ;
    }

    private void DialogueControl_OnEnterNode(string nodeId)
    {
		curNode = DialogueManager.Instance().IdNodeMap[nodeId];
		HeadTr.Texture = TextureCache.Instance().GetTexture(curNode.SpeakerHeadPath);
		NameLb.Text = curNode.SpeakerName;
        //清空optVbc, 重新添加当前选项
        CreateBtn();
        Show();
    }
    private void DialogueControl_OnLineChanged(int lineIndex)
    {
        LineTextLb.Text = curNode.LineList[lineIndex];
    }

    public override void _Process(double delta)
	{
	}

	//进入新节点
	private void CreateBtn()
	{
        foreach (Node child in OptVbc.GetChildren()) child.QueueFree();
        foreach (DialogueOption opt in curNode.OptionList)
        {
            StyleBoxFlat normal = new StyleBoxFlat();
            normal.BgColor = new Color(0.15f, 0.15f, 0.15f);
            normal.SetCornerRadiusAll(6);
            normal.ContentMarginLeft = 12;
            normal.ContentMarginRight = 12;
            normal.ContentMarginTop = 8;
            normal.ContentMarginBottom = 8;

            StyleBoxFlat hover = normal.Duplicate() as StyleBoxFlat;
            hover.BgColor = new Color(0.25f, 0.25f, 0.25f);
            Button btn = new Button() { Text = opt.OptionText};
            btn.AddThemeStyleboxOverride("normal", normal);
            btn.AddThemeStyleboxOverride("hover", hover);

            OptVbc.AddChild(btn);
        }
    }
}
