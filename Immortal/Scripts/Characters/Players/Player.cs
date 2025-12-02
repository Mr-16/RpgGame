using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players
{
    public partial class Player : CharacterBase
    {
        public StateMachine stateMachine;

        [Export]
        private float moveSpeed = 200f;
        private float rollSpeed = 800f;

        public float maxStamina = 200;
        public float curStamina;
        public float staminaRecoverPerSec = 100;//每秒恢复量
        public float rollStamina = 20f;
        [Export] public ProgressBar staminaProgressBar;

        [Export]
        public AnimatedSprite2D anim;
        public Vector2 curDir = Vector2.Right;

        public int curSkillIndex = -1;//当前技能索引, 取值 : 012, 每次进入技能状态都要根据索引在技能list里找到具体技能

        [Export]
        public bool isMoveAtkEnable = true;

        public override void _Ready()
        {
            stateMachine = new StateMachine(this);
            curStamina = maxStamina;
        }

        public override void _Process(double delta)
        {
            stateMachine.curState.Update((float)delta);

            staminaProgressBar.MaxValue = maxStamina;
            staminaProgressBar.Value = curStamina;
            GD.Print("curState" + stateMachine.curState);
        }
        public override void _PhysicsProcess(double delta)
        {
            stateMachine.curState.FixedUpdate((float)delta);
            //GD.Print("Stamina" + curStamina);
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
