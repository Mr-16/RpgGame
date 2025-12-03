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
        public StateBase curState;
        public IdleState idleState;
        public PatrolState patrolState;
        public ChaseState chaseState;
        public AtkState atkState;

        public StateMachine(MeleeEnemy enemy)
        {
            idleState = new IdleState(enemy);
            patrolState = new PatrolState(enemy);
            chaseState = new ChaseState(enemy);
            atkState = new AtkState(enemy);
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
