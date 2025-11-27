using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;
using RpgGame.Scripts.Characters.Players.StateMachines;

namespace RpgGame.Scripts.Characters.Players.States.MovementStates
{
    public class WalkState : StateBase
    {

        public WalkState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            player.anim.Play("Walk");
        }
        public override void Update(float delta)
        {
            player.RecoverStamina(delta);


            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if(moveDir == Vector2.Zero)
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.idleState);
                return;
            }
            if (Input.IsActionJustPressed("Roll") && player.curStamina >= player.rollStamina)
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.rollState);
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
