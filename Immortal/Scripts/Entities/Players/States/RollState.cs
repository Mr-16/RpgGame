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
            if(player.CurEnergy < rollCost)
            {
                player.Sm.ChangeState(player.Sm.IdleState);
                return;
            }
            timer = 0;
            player.CurEnergy -= rollCost;
            player.Anim.Play("Roll");
            if (player.Sm.CurState == player.Sm.RtkState)
            {
                player.Sm.ChangeState(player.Sm.IdleState);
            }
        }

        public override void Update(float delta)
        {
            timer += delta;
            if (timer > duration)
            {
                if (Input.IsActionPressed("Roll"))
                {
                    player.Sm.ChangeState(player.Sm.RunState);
                    return;
                }
                else
                {
                    player.Sm.ChangeState(player.Sm.IdleState);
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
