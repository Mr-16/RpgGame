using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class ChaseState : StateBase
    {
        public ChaseState(MeleeEnemy enemy) => this.enemy = enemy;

        public override void Enter()
        {
            enemy.Anim.Play("Chase");
        }
        public override void Update(float delta)
        {
            if (enemy.atkTarget != null)
            {
                enemy.Sm.ChangeState(enemy.Sm.AtkState);
                return;
            }
            if (enemy.chaseTarget == null)
            {
                enemy.Sm.ChangeState(enemy.Sm.IdleState);
                return;
            }
            enemy.Chase();
        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }

        

        
    }
}
