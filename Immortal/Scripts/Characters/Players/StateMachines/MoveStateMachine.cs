using RpgGame.Scripts.Characters.Players.StateMachines.MovementStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.StateMachines
{
    public class MoveStateMachine
    {
        public StateBase curState;
        public IdleState idleState;
        public WalkState walkState;
        public RunState runState;
        public RollState rollState;

        public MoveStateMachine(Player player)
        {
            idleState = new IdleState(player);
            walkState = new WalkState(player);
            runState = new RunState(player);
            rollState = new RollState(player);
        }
        
        public void ChangeState(StateBase newState)
        {
            if (curState != null) curState.Exit();
            curState = newState;
            curState.Enter();
        }
    }
}
