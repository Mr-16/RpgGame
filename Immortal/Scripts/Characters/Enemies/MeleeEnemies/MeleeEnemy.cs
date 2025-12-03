using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies
{
    public partial class MeleeEnemy : Enemy
    {
        public StateMachine stateMachine;

        public Vector2 startPos;

        [Export]
        public AnimatedSprite2D anim;

        [Export]
        public float patrolSpeed = 100;
        [Export]
        public float chaseSpeed = 200;

        public override void _Ready()
        {
            base._Ready();
            stateMachine = new StateMachine(this);
            startPos = GlobalPosition;
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            stateMachine.curState.Update((float)delta);

        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            stateMachine.curState.FixedUpdate((float)delta);
        }

        public void PatrolTo(Vector2 tarPos)
        {
            Velocity = (tarPos - GlobalPosition).Normalized() * patrolSpeed;
            MoveAndSlide();
        }
        //public void ChaseTo(Vector2 tarPos)
        //{
        //    Velocity = (tarPos - GlobalPosition).Normalized() * chaseSpeed;
        //    MoveAndSlide();
        //}
    }
}
