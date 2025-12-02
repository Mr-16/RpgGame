using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace RpgGame.Scripts.Characters.Players.States
{
    public class IdleState : StateBase
    {
        public IdleState(Player player)
        {
            this.player = player;
        }
        public override void Enter()
        {
            player.anim.Play("Idle");
        }
        public override void Update(float delta)
        {
            player.RecoverStamina(delta);
            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir != Vector2.Zero)
            {
                player.stateMachine.ChangeState(player.stateMachine.walkState);
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
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }
    }
}
