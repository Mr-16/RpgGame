using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class IdleState : StateBase
    {
        float timer = 0;
        float duration = 0.5f;

        public IdleState(MeleeEnemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            enemy.anim.Play("Idle");
        }

        public override void Update(float delta)
        {
            if(enemy.atkTarget != null)
            {
                enemy.stateMachine.ChangeState(enemy.stateMachine.atkState);
                return;
            }
            if(enemy.chaseTarget != null)
            {
                enemy.stateMachine.ChangeState(enemy.stateMachine.chaseState);
                return;
            }

            timer += delta;
            if(timer >= duration)
            {
                timer = 0;
                enemy.stateMachine.ChangeState(enemy.stateMachine.patrolState);
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
