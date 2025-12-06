using Godot;
using RpgGame.Scripts.Characters.Players;
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
        public Area2D chaseArea;
        [Export]
        public Area2D atkArea;
        public Player chaseTarget = null;
        public Player atkTarget = null;

        [Export]
        public float patrolSpeed = 100;
        [Export]
        public float chaseSpeed = 200;

        public override void _Ready()
        {
            base._Ready();
            stateMachine = new StateMachine(this);
            startPos = GlobalPosition;
            chaseArea.BodyEntered += ChaseArea_BodyEntered;
            chaseArea.BodyExited += ChaseArea_BodyExited;
            atkArea.BodyEntered += AtkArea_BodyEntered;
            atkArea.BodyExited += AtkArea_BodyExited;
        }
        private void ChaseArea_BodyEntered(Node2D body)
        {
            if (body is not Player player) return;
            chaseTarget = player;
        }
        private void ChaseArea_BodyExited(Node2D body)
        {
            if (body is not Player player) return;
            chaseTarget = null;
        }

        private void AtkArea_BodyEntered(Node2D body)
        {
            if (body is not Player player) return;
            atkTarget = player;
        }
        private void AtkArea_BodyExited(Node2D body)
        {
            if (body is not Player player) return;
            atkTarget = null;
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
            CurDir = (tarPos - GlobalPosition).Normalized();
            if (CurDir.X < 0) anim.FlipH = true;
            else anim.FlipH = false;
            Velocity = CurDir * patrolSpeed;
            MoveAndSlide();
        }
        public void Chase()
        {
            CurDir = (chaseTarget.GlobalPosition - GlobalPosition).Normalized();
            if (CurDir.X < 0) anim.FlipH = true;
            else anim.FlipH = false;
            Velocity = CurDir * chaseSpeed;
            MoveAndSlide();
        }
    }
}
