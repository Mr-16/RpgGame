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
        StateMachine stateMachine;

        [Export]
        public AnimatedSprite2D anim;

        public override void _Ready()
        {
            base._Ready();
            stateMachine = new StateMachine(this);
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
    }
}
