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
        private float rollCost = 20;
        public RollState(Player player)
        {
            this.player = player;
        }


        public override void Enter()
        {
            if(player.CurStam < rollCost)
            {
                player.Sm.ChangeState(player.Sm.idleState);
            }
            timer = 0;
            player.CurStam -= rollCost;
            player.anim.Play("Roll");
            if (player.Sm.curState == player.Sm.atkState)
            {
                player.Sm.ChangeState(player.Sm.idleState);
            }
        }

        public override void Update(float delta)
        {
            timer += delta;
            if (timer > duration)
            {
                if (Input.IsActionPressed("Roll"))
                {
                    player.Sm.ChangeState(player.Sm.runState);
                    return;
                }
                else
                {
                    player.Sm.ChangeState(player.Sm.idleState);
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
