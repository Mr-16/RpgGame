using RpgGame.Scripts.Characters.Enemies.MeleeEnemies.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies
{
    public class StateMachine
    {
        public StateBase CurState;
        public IdleState IdleState;
        public PatrolState PatrolState;
        public ChaseState ChaseState;
        public AtkState AtkState;
        public DeathState DeathState;

        public StateMachine(MeleeEnemy enemy)
        {
            IdleState = new IdleState(enemy);
            PatrolState = new PatrolState(enemy);
            ChaseState = new ChaseState(enemy);
            AtkState = new AtkState(enemy);
            DeathState = new DeathState(enemy);
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
