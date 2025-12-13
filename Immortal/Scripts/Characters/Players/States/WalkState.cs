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
            if (player.Sm.CurState != player.Sm.RtkState)
            {
                player.Anim.Play("Walk");
            }

        }
        public override void Update(float delta)
        {
            if (player.isMoveAtkEnable == false && player.Sm.CurState == player.Sm.RtkState)
            {
                player.Sm.ChangeState(player.Sm.IdleState);
            }

            player.RegenStam(delta);


            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir == Vector2.Zero)
            {
                player.Sm.ChangeState(player.Sm.IdleState);
                return;
            }
            if (Input.IsActionJustPressed("Roll"))
            {
                player.Sm.ChangeState(player.Sm.RollState);
                return;
            }
            if (Input.IsActionJustPressed("Atk"))
            {
                player.Sm.ChangeState(player.Sm.RtkState);
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
