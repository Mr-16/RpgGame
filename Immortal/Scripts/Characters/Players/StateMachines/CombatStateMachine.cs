using RpgGame.Scripts.Characters.Players.StateMachines.CombatStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.StateMachines
{
    public class CombatStateMachine
    {
        public StateBase curState;
        public IdleAtkState idleAtkState;
        public AtkState atkState;
        public SkillState skillState;

        public CombatStateMachine(Player player)
        {
            idleAtkState = new IdleAtkState(player);
            atkState = new AtkState(player);
            skillState = new SkillState(player);
        }

        public void ChangeState(StateBase newState)
        {
            if (curState != null) curState.Exit();
            curState = newState;
            curState.Enter();
        }
    }
}
