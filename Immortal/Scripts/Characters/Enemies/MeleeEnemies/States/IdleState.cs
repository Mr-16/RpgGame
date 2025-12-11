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
        float duration = 1f;

        public IdleState(MeleeEnemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            enemy.Anim.Play("Idle");
        }

        public override void Update(float delta)
        {
            

            if (enemy.GlobalPosition.DistanceSquaredTo(enemy.Player.GlobalPosition) < enemy.ChaseRangeSq)
            {
                enemy.Sm.ChangeState(enemy.Sm.ChaseState);
                return;
            }

            timer += delta;
            if(timer >= duration)
            {
                timer = 0;
                enemy.Sm.ChangeState(enemy.Sm.PatrolState);
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
