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

        public AtkState(Player player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            player.anim.Play("Atk");
            player.anim.AnimationFinished += Anim_AnimationFinished;
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
            player.anim.AnimationFinished -= Anim_AnimationFinished;

            if (player.Sm.curState == player.Sm.idleState)
            {
                player.anim.Play("Idle");
                return;
            }
            if (player.Sm.curState == player.Sm.walkState)
            {
                player.anim.Play("Walk");
                return;
            }
            if (player.Sm.curState == player.Sm.runState)
            {
                player.anim.Play("Run");
                return;
            }
        }
    }
}
