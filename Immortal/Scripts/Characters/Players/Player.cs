using Godot;
using RpgGame.Scripts.Characters.Players.StateMachines;
using RpgGame.Scripts.Characters.Players.StateMachines.CombatStates;
using RpgGame.Scripts.Characters.Players.StateMachines.MovementStates;
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
        public CombatStateMachine combatStateMachine;

        [Export]
        private float moveSpeed = 300f;
        private float rollSpeed = 1000f;

        public float maxStamina = 100;
        public float curStamina = 100;
        public float staminaRecoverPerSec = 50;//每秒恢复量
        public float rollStamina = 20f;
        [Export] public ProgressBar staminaProgressBar;

        [Export]
        public AnimatedSprite2D anim;
        public Vector2 curDir = Vector2.Right;

        public int curSkillIndex = -1;//当前技能索引, 取值 : 012, 每次进入技能状态都要根据索引在技能list里找到具体技能

        public override void _Ready()
        {
            moveStateMachine = new MoveStateMachine(this);
            combatStateMachine = new CombatStateMachine(this);
            moveStateMachine.ChangeState(moveStateMachine.idleState);
            combatStateMachine.ChangeState(combatStateMachine.idleAtkState);

        }

        public override void _Process(double delta)
        {
            moveStateMachine.curState.Update((float)delta);
            combatStateMachine.curState.Update((float)delta);

            staminaProgressBar.MaxValue = maxStamina;
            staminaProgressBar.Value = curStamina;
            //attackStateMachine.curState.Update(delta);
            //GD.Print("moveStateMachine.curState" + moveStateMachine.curState);
            GD.Print("combatStateMachine.curState" + combatStateMachine.curState);
        }
        public override void _PhysicsProcess(double delta)
        {
            moveStateMachine.curState.FixedUpdate((float)delta);
            combatStateMachine.curState.FixedUpdate((float)delta);
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
