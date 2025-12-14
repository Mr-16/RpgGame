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
            player.Anim.Play("Idle");
        }
        public override void Update(float delta)
        {
            player.RegenStam(delta);
            Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
            if (moveDir != Vector2.Zero)
            {
                player.Sm.ChangeState(player.Sm.WalkState);
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
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }
    }
}
