using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class AtkState : StateBase
    {
        private float speedScale;
        public int DmgFrame = 2;

        public AtkState(MeleeEnemy enemy) => this.enemy = enemy;

        public override void Enter()
        {
            enemy.DmgArea.Monitoring = true;
            speedScale = enemy.Anim.SpeedScale;
            enemy.Anim.SpeedScale = 1 + enemy.FinalAttr.AtkSpeed;
            enemy.Anim.Play("Atk");
            enemy.Anim.AnimationFinished += Anim_AnimationFinished;
            enemy.Anim.FrameChanged += Anim_FrameChanged;
        }

        private void Anim_FrameChanged()
        {
            if(enemy.Anim.Frame == DmgFrame)
            {
                enemy.Atk();
            }
        }

        private void Anim_AnimationFinished()
        {
            //GD.Print("atk finish!!!");
            enemy.Sm.ChangeState(enemy.Sm.IdleState);
            return;
        }

        public override void Update(float delta)
        {
            
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
            enemy.DmgArea.Monitoring = false;
            enemy.Anim.FrameChanged -= Anim_FrameChanged;
            enemy.Anim.AnimationFinished -= Anim_AnimationFinished;
            enemy.Anim.SpeedScale = speedScale;
        }

    }
}
