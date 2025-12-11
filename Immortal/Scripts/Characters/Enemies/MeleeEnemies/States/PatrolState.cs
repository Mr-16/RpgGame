using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class PatrolState : StateBase
    {
        float range = 300;//巡逻范围, 在出生点的这个范围内随机生成巡逻点
        Vector2 curTarPos;
        Random rd = null;

        public PatrolState(MeleeEnemy enemy) => this.enemy = enemy;

        public override void Enter()
        {
            if(rd == null) rd = new Random();
            curTarPos.X = rd.Next((int)(enemy.startPos.X - range), (int)(enemy.startPos.X + range));
            curTarPos.Y = rd.Next((int)(enemy.startPos.Y - range), (int)(enemy.startPos.Y + range));

            enemy.Anim.Play("Patrol");
        }

        public override void Update(float delta)
        {
            //if (enemy.atkTarget != null)
            //{
            //    enemy.Sm.ChangeState(enemy.Sm.AtkState);
            //    return;
            //}
            //if (enemy.chaseTarget != null)
            //{
            //    enemy.Sm.ChangeState(enemy.Sm.ChaseState);
            //    return;
            //}
            if(enemy.GlobalPosition.DistanceSquaredTo(enemy.Player.GlobalPosition) < enemy.ChaseRangeSq)
            {
                enemy.Sm.ChangeState(enemy.Sm.ChaseState);
                return;
            }


            if (enemy.GlobalPosition.DistanceSquaredTo(curTarPos) < 1)
            {
                enemy.Sm.ChangeState(enemy.Sm.IdleState);
            }
            enemy.PatrolTo(curTarPos);
        }

        public override void FixedUpdate(float delta)
        {
            
        }

        

        public override void Exit()
        {
        }
    }
}
