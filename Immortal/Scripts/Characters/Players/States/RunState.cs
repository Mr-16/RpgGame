using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace RpgGame.Scripts.Characters.Players.States
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
            if (player.curStamina <= 0)//没力了, 回去走路
            {
                player.stateMachine.ChangeState(player.stateMachine.walkState);
                return;
            }
            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir == Vector2.Zero)//不再输入, 回idle
            {
                player.stateMachine.ChangeState(player.stateMachine.idleState);
                return;
            }
            if (!Input.IsActionPressed("Roll"))//不再按着k, 回去走路
            {
                player.stateMachine.ChangeState(player.stateMachine.walkState);
                return;
            }
            if (Input.IsActionJustPressed("Atk"))
            {
                player.stateMachine.ChangeState(player.stateMachine.atkState);
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
