using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;
using RpgGame.Scripts.Characters.Players.StateMachines;

namespace RpgGame.Scripts.Characters.Players.States.MovementStates
{
    public class RunState : StateBase
    {
        public RunState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            player.anim.Play("Run");
        }
        public override void Update(float delta)
        {
            player.curStamina -= 0.1f;
            if(player.curStamina <= 0)
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.walkState);
                return;
            }
            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir == Vector2.Zero)
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.idleState);
                return;
            }
            if (!Input.IsActionPressed("Roll"))
            {
                player.moveStateMachine.ChangeState(player.moveStateMachine.walkState);
                return;
            }
            player.Run(moveDir);
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }
    }
}
