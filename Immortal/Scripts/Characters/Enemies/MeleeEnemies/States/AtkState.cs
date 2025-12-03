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
        public AtkState(MeleeEnemy enemy) => this.enemy = enemy;

        public override void Enter()
        {
            enemy.anim.Play("Atk");
            enemy.anim.AnimationFinished += Anim_AnimationFinished;
        }

        private void Anim_AnimationFinished()
        {
            GD.Print("atk finish!!!");
            enemy.stateMachine.ChangeState(enemy.stateMachine.idleState);
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
            enemy.anim.AnimationFinished -= Anim_AnimationFinished;
        }

    }
}
