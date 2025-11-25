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
        public StateMachine moveStateMachine;
        public StateMachine attackStateMachine;

        public override void _Ready()
        {
            moveStateMachine = new StateMachine(this);
            attackStateMachine = new StateMachine(this);
        }

        public override void _Process(double delta)
        {
            moveStateMachine.curState.Update();
            attackStateMachine.curState.Update();
        }
        public override void _PhysicsProcess(double delta)
        {
            moveStateMachine.curState.FixedUpdate();
            attackStateMachine.curState.FixedUpdate();
        }

    }
}
