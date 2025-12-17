using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States
{
    public class KnockBackState : StateBase
    {
        private float knockBackSpeed = 500;
        private Vector2 knockBackDir;
        private float timer = 0;
        private float duration = 0.1f;

        public KnockBackState(MeleeEnemy enemy) => this.enemy = enemy;

        public override void Enter()
        {
            timer = 0;
            knockBackDir = -(enemy.Player.GlobalPosition - enemy.GlobalPosition).Normalized();
            enemy.Anim.Modulate = new Color(2f, 2f, 2f, 1f);
            enemy.Anim.Scale = new Vector2(1.5f, 1.5f);
        }


        public override void Update(float delta)
        {
            enemy.Velocity = knockBackDir * knockBackSpeed;
            enemy.MoveAndSlide();
            timer += delta;
            if (timer < duration) return;
            enemy.Sm.ChangeState(enemy.Sm.IdleState);
        }

        public override void FixedUpdate(float delta)
        {
            
        }

        public override void Exit()
        {
            enemy.Anim.Modulate = new Color(1f, 1f, 1f, 1f);
            enemy.Anim.Scale = new Vector2(1f, 1f); 
        }
    }
}
