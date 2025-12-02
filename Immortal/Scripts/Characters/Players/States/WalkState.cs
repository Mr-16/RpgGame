using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace RpgGame.Scripts.Characters.Players.States
{
    public class WalkState : StateBase
    {

        public WalkState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            if (player.stateMachine.curState != player.stateMachine.atkState)
            {
                player.anim.Play("Walk");
            }

        }
        public override void Update(float delta)
        {
            if (player.isMoveAtkEnable == false && player.stateMachine.curState == player.stateMachine.atkState)
            {
                player.stateMachine.ChangeState(player.stateMachine.idleState);
            }

            player.RecoverStamina(delta);


            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir == Vector2.Zero)
            {
                player.stateMachine.ChangeState(player.stateMachine.idleState);
                return;
            }
            if (Input.IsActionJustPressed("Roll") && player.curStamina >= player.rollStamina)
            {
                player.stateMachine.ChangeState(player.stateMachine.rollState);
                return;
            }
            if (Input.IsActionJustPressed("Atk"))
            {
                player.stateMachine.ChangeState(player.stateMachine.atkState);
                return;
            }
            player.Walk(moveDir);
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }
    }
}
