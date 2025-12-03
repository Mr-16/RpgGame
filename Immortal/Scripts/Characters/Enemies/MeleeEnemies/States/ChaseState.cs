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
            enemy.anim.Play("Chase");
        }
        public override void Update(float delta)
        {
            if(enemy.chaseTarget == null)
            {
                enemy.stateMachine.ChangeState(enemy.stateMachine.idleState);
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
