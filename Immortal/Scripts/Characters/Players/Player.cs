using Godot;
using RpgGame.Scripts.Characters.Players.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players
{
    public partial class Player : CharacterBase
    {
        public MoveStateMachine moveStateMachine;

        [Export]
        private float moveSpeed = 300f;
        private float rollSpeed = 1000f;

        public float maxStamina = 100;
        public float curStamina = 100;
        public float staminaRecoverPerSec = 10;//每秒恢复量
        public float rollStamina = 20f;

        [Export]
        public AnimatedSprite2D anim;
        public Vector2 curDir = Vector2.Right;

        public override void _Ready()
        {
            moveStateMachine = new MoveStateMachine(this);
        }

        public override void _Process(double delta)
        {
            moveStateMachine.curState.Update((float)delta);
            //attackStateMachine.curState.Update(delta);
            //GD.Print("moveStateMachine.curState" + moveStateMachine.curState);
        }
        public override void _PhysicsProcess(double delta)
        {
            moveStateMachine.curState.FixedUpdate((float)delta);
            //attackStateMachine.curState.FixedUpdate(delta);
            GD.Print("Stamina" + curStamina);
        }

        public void Walk(Vector2 moveDir)
        {
            curDir = moveDir;
            if (curDir.X < 0) anim.FlipH = true;
            else anim.FlipH = false;
            Velocity = moveSpeed * moveDir;
            MoveAndSlide();
        }
        public void Run(Vector2 moveDir)
        {
            curDir = moveDir;
            if (curDir.X < 0) anim.FlipH = true;
            else anim.FlipH = false;
            Velocity = 2 * moveSpeed * moveDir;
            MoveAndSlide();
        }
        public void Roll()
        {
            Velocity = rollSpeed * curDir;
            MoveAndSlide();
        }

        public void RecoverStamina(float delta)
        {
            if (curStamina < maxStamina)
            {
                curStamina += staminaRecoverPerSec * delta;
            }
            else
            {
                curStamina = maxStamina;
            }
        }
    }
}
