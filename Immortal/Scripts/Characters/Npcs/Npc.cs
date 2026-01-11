using Godot;
using Godot.Collections;
using RpgGame.Scripts.Systems.DialogueSystem;
using RpgGame.Scripts.Utilities;
using System;

public partial class Npc : StaticBody2D
{
	[Export] public Area2D InteractArea;

    [Export] public Control InteractTipCtrl;


	public override void _Ready()
	{
        InteractTipCtrl.Visible = false;

    }

    public override void _Process(double delta)
	{
	}
    public void ShowTipVis(bool isShow)
    {
        if (isShow) InteractTipCtrl.Show();
        else InteractTipCtrl.Hide();
    }
    public void StartTalk()
    {
        DialogueManager.Instance().StartDialogue(GetDialogueNode());
        //HUD.Instance().DialogueCtrl.Show();
    }

    private string GetDialogueNode()
    {
        return GameConstants.TestId1;
    }
}
