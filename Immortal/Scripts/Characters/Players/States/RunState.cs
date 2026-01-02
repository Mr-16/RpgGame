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
            player.Anim.Play("Run");
        }
        public override void Update(float delta)
        {
            player.CurEnergy -= 0.1f;
            if (player.CurEnergy <= 0)//没力了, 回去走路
            {
                player.Sm.ChangeState(player.Sm.WalkState);
                return;
            }
            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir == Vector2.Zero)//不再输入, 回idle
            {
                player.Sm.ChangeState(player.Sm.IdleState);
                return;
            }
            if (!Input.IsActionPressed("Roll"))//不再按着k, 回去走路
            {
                player.Sm.ChangeState(player.Sm.WalkState);
                return;
            }
            if (Input.IsActionJustPressed("Atk"))
            {
                player.Sm.ChangeState(player.Sm.RtkState);
                return;
            }
            if (Input.IsActionJustPressed("Skill_1"))
            {
                player.curSkillTypeIndex = 0;
                player.Sm.ChangeState(player.Sm.SkillState);
                return;
            }
            if (Input.IsActionJustPressed("Skill_2"))
            {
                player.curSkillTypeIndex = 1;
                player.Sm.ChangeState(player.Sm.SkillState);
                return;
            }
            if (Input.IsActionJustPressed("Skill_3"))
            {
                player.curSkillTypeIndex = 2;
                player.Sm.ChangeState(player.Sm.SkillState);
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
