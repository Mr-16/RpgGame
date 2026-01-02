using Godot;
using RpgGame.Scripts.Characters.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.States
{
    public class AtkState : StateBase
    {
        private float speedScale;
        public int AtkFrame = 2;


        public AtkState(Player player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            speedScale = player.Anim.SpeedScale;
            player.Anim.SpeedScale = 1 + player.AttrContainer.GetAttrFinalValue(AttributeSystem.AttributeType.AttackSpeed);
            player.Anim.Play("Atk");
            player.Anim.AnimationFinished += Anim_AnimationFinished;
            player.Anim.FrameChanged += Anim_FrameChanged;


        }

        private void Anim_FrameChanged()
        {
            if(player.Anim.Frame == AtkFrame)
            {
                player.Atk();
            }
            //GD.Print(player.Anim.Frame);
            //throw new NotImplementedException();
        }

        private void Anim_AnimationFinished()
        {
            player.Sm.ChangeState(player.Sm.IdleState);
        }

        public override void Update(float delta)
        {
            if (player.isMoveAtkEnable)//可以跑打
            {
                Vector2 moveDir = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
                player.CurDir = moveDir;
                player.Velocity = player.AttrContainer.GetAttrFinalValue(AttributeSystem.AttributeType.MoveSpeed) * moveDir;
                player.MoveAndSlide();
            }
            //攻击状态期间, 在攻击范围内, 若有目标, 找到最近目标, 朝向他
            Enemy target = player.GetClosestEnemy(player.AtkRangeSq);
            if (target != null)
            {
                //GD.Print(target);
                player.CurDir = (target.GlobalPosition - player.GlobalPosition).Normalized();//target.GlobalPosition.DirectionTo(player.GlobalPosition);
                if (player.CurDir.X < 0) player.Anim.FlipH = true;
                else if (player.CurDir.X > 0) player.Anim.FlipH = false;
            }
            

        }

        public override void FixedUpdate(float delta)
        {
        }

        public override void Exit()
        {
            player.Anim.AnimationFinished -= Anim_AnimationFinished;
            player.Anim.FrameChanged -= Anim_FrameChanged;
            player.Anim.SpeedScale = speedScale;
        }
    }
}
