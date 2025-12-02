using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace RpgGame.Scripts.Characters.Players.States
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
            player.curStamina -= player.rollStamina;
            player.anim.Play("Roll");
            if (player.stateMachine.curState == player.stateMachine.atkState)
            {
                player.stateMachine.ChangeState(player.stateMachine.idleState);
            }
        }

        public override void Update(float delta)
        {
            timer += delta;
            if (timer > duration)
            {
                if (Input.IsActionPressed("Roll"))
                {
                    player.stateMachine.ChangeState(player.stateMachine.runState);
                    return;
                }
                else
                {
                    player.stateMachine.ChangeState(player.stateMachine.idleState);
                    return;
                }
            }
            player.Roll();
        }

        public override void FixedUpdate(float delta)
        {

        }

        public override void Exit()
        {

        }
    }
}
