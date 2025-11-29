using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;
using RpgGame.Scripts.Characters.Players.StateMachines;

namespace RpgGame.Scripts.Characters.Players.StateMachines.MovementStates
{
    public class IdleState : StateBase
    {
        public IdleState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            if (player.combatStateMachine.curState != player.combatStateMachine.atkState)
            {
                player.anim.Play("Idle");

            }
        }
        public override void Update(float delta)
        {
            player.RecoverStamina(delta);
            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if(moveDir != Vector2.Zero)
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.walkState);
                return;
            }
            if (Input.IsActionJustPressed("Roll") && player.curStamina >= player.rollStamina)
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.rollState);
                return;
            }
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }
    }
}
