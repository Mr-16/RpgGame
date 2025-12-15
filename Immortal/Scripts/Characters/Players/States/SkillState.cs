using Godot;
using RpgGame.Scripts.Characters.Enemies;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.States
{
    public class SkillState : StateBase
    {
        public SkillState(Player player)
        {
            this.player = player;
        }

        SkillData curSkillData;
        private float speedScale;

        public override void Enter()
        {
            //GD.Print("curSkillIndex : " + player.curSkillTypeIndex);
            SkillType skillType = player.SkillTypeList[player.curSkillTypeIndex];
            curSkillData = GameManager.Instance().SkillDataMap[skillType];
            if (player.CurMana < curSkillData.ManaCost)
            {
                player.Sm.ChangeState(player.Sm.IdleState);
                return;
            }
            player.CurMana -= curSkillData.ManaCost;
            speedScale = player.Anim.SpeedScale;
            player.Anim.SpeedScale = 1 + curSkillData.AnimSpeedScale;

            player.Anim.Play(curSkillData.AnimStr);
           
            player.Anim.AnimationFinished += Anim_AnimationFinished;
            player.Anim.FrameChanged += Anim_FrameChanged;
        }

        private void Anim_FrameChanged()
        {
            if (player.Anim.Frame != curSkillData.CastFrame) return;
            player.CastSkill(player.SkillTypeList[player.curSkillTypeIndex]);
        }

        private void Anim_AnimationFinished()
        {
            player.Anim.AnimationFinished -= Anim_AnimationFinished;
            player.Anim.FrameChanged -= Anim_FrameChanged;
            player.Sm.ChangeState(player.Sm.IdleState);
            return;
        }

        public override void Update(float delta)
        {
            if (curSkillData.IsFaceTar)
            {
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
        }

        public override void FixedUpdate(float delta)
        {

        }

        public override void Exit()
        {
            player.curSkillTypeIndex = -1;
            player.Anim.SpeedScale = speedScale;
        }
    }
}
