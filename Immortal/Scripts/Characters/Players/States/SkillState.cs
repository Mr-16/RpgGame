using Godot;
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

        public override void Enter()
        {
            GD.Print("curSkillIndex : " + player.curSkillTypeIndex);
            //todo : 这里根据索引找到具体技能, 从具体技能那获取技能是否禁用移动, 持续时间, 能否被roll打断等信息
            SkillType skillType = player.SkillTypeList[player.curSkillTypeIndex];
            curSkillData = GameManager.Instance().SkillDataMap[skillType];
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
            player.Sm.ChangeState(player.Sm.IdleState);
            return;
        }

        public override void Update(float delta)
        {
            //timer += delta;
            //if (timer < duration) return;
            //player.Sm.ChangeState(player.Sm.IdleState);
            //return;
        }

        public override void FixedUpdate(float delta)
        {

        }

        public override void Exit()
        {
            player.curSkillTypeIndex = -1;
            player.Anim.AnimationFinished -= Anim_AnimationFinished;
            player.Anim.FrameChanged -= Anim_FrameChanged;
        }
    }
}
