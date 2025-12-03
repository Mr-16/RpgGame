using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class IdleState : StateBase
    {
        public IdleState(MeleeEnemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Enter()
        {
            enemy.anim.Play("Idle");
        }

        public override void Exit()
        {
        }

        public override void FixedUpdate(float delta)
        {
        }

        public override void Update(float delta)
        {
        }
    }
}
