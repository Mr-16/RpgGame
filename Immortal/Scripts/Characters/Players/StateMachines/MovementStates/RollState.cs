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
        double timer = 0;
        double duration = 0.3;
        public RollState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            timer = 0;
            player.anim.Play("Roll");
        }
        public override void Update(double delta)
        {
            timer += delta;
            if(timer > duration)
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
            }
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
