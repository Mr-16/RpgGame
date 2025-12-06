using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.States
{
    public class AtkState : StateBase
    {
        private float speedScale;
        public int AtkFrame = 2;

        public AtkState(Player player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            player.DamageArea.Monitoring = true;
            speedScale = player.Anim.SpeedScale;
            player.Anim.SpeedScale = 1 + player.FinalAttr.AtkSpeed;
            player.Anim.Play("Atk");
            player.Anim.AnimationFinished += Anim_AnimationFinished;
            player.Anim.FrameChanged += Anim_FrameChanged;
        }

        private void Anim_FrameChanged()
        {
            if(player.Anim.Frame == AtkFrame)
            {
                GD.Print(player.DmgEnemyList.Count);
            }
            //GD.Print(player.Anim.Frame);
            //throw new NotImplementedException();
        }

        private void Anim_AnimationFinished()
        {
            player.Sm.ChangeState(player.Sm.idleState);
        }

        public override void Update(float delta)
        {
            if (player.isMoveAtkEnable)//可以跑打
            {
                Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
                player.Walk(moveDir);
            }

        }

        public override void FixedUpdate(float delta)
        {
        }

        public override void Exit()
        {
            player.DamageArea.Monitoring = false;
            player.Anim.AnimationFinished -= Anim_AnimationFinished;
            player.Anim.FrameChanged -= Anim_FrameChanged;
            player.Anim.SpeedScale = speedScale;

            if (player.Sm.curState == player.Sm.idleState)
            {
                player.Anim.Play("Idle");
                return;
            }
            if (player.Sm.curState == player.Sm.walkState)
            {
                player.Anim.Play("Walk");
                return;
            }
            if (player.Sm.curState == player.Sm.runState)
            {
                player.Anim.Play("Run");
                return;
            }
        }
    }
}
