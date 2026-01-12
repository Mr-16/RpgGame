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
        public StateBase CurState;
        public IdleState IdleState;
        public WalkState WalkState;
        public RunState RunState;
        public RollState RollState;
        public AtkState RtkState;
        //public SkillState SkillState;
        public DeathState DeathState;

        public StateMachine(Player player)
        {
            IdleState = new IdleState(player);
            WalkState = new WalkState(player);
            RunState = new RunState(player);
            RollState = new RollState(player);
            RtkState = new AtkState(player);
            //SkillState = new SkillState(player);
            DeathState = new DeathState(player);
            ChangeState(IdleState);
        }

        public void ChangeState(StateBase newState)
        {
            if (newState == CurState) return;
            if (CurState != null) CurState.Exit();
            CurState = newState;
            CurState.Enter();
        }
    }
}
