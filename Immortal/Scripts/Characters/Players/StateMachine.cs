using RpgGame.Scripts.Characters.Players.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players
{
    public class StateMachine
    {
        public StateBase curState;
        public IdleState idleState;
        public WalkState walkState;
        public RunState runState;
        public RollState rollState;
        public AtkState atkState;
        public SkillState skillState;
        public DeathState deathState;

        public StateMachine(Player player)
        {
            idleState = new IdleState(player);
            walkState = new WalkState(player);
            runState = new RunState(player);
            rollState = new RollState(player);
            atkState = new AtkState(player);
            ChangeState(idleState);
        }

        public void ChangeState(StateBase newState)
        {
            if (curState != null) curState.Exit();
            curState = newState;
            curState.Enter();
        }
    }
}
