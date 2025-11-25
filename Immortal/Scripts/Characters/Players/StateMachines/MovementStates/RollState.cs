using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;
using RpgGame.Scripts.Characters.Players.StateMachines;

namespace RpgGame.Scripts.Characters.Players.States.MovementStates
{
    public class RollState : StateBase
    {
        public RollState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            player.anim.Play("Roll");
            player.GetTree().CreateTimer(0.5).Timeout += () =>
            {
                if (Input.IsActionPressed("Roll"))
                {
                    player.moveStateMachine.ChangeState(player.moveStateMachine.runState);
                    return;
                }
                else
                {
                    player.moveStateMachine.ChangeState(player.moveStateMachine.idleState);
                    return;
                }
            };
        }
        public override void Update(double delta)
        {
            player.Roll();
        }
        public override void FixedUpdate(double delta)
        {
            
        }
        public override void Exit()
        {
        }
    }
}
