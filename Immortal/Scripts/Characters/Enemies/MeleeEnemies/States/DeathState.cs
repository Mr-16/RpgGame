using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class DeathState : StateBase
    {
        public DeathState(MeleeEnemy enemy) => this.enemy = enemy;

        public override void Enter()
        {
            enemy.HealthPb.Visible = false;
            enemy.Anim.Play("Death");
            enemy.Anim.AnimationFinished += Anim_AnimationFinished;
            enemy.DmgParticles.OneShot = false;
            enemy.DmgParticles.Emitting = true;

        }

        private void Anim_AnimationFinished()
        {
            //enemy.Sm.ChangeState(enemy.Sm.)
            enemy.Anim.AnimationFinished -= Anim_AnimationFinished;
            //生成经验
            enemy.SpawnExpBall();
            enemy.QueueFree();
        }

        public override void Update(float delta)
        {
           
        }
        public override void FixedUpdate(float delta)
        {
            
        }
        public override void Exit()
        {
            
        }

    }
}
