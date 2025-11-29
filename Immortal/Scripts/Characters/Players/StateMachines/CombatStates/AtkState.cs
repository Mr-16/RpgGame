using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.StateMachines.CombatStates
{
    public class AtkState : StateBase
    {



        public AtkState(Player player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            player.anim.Play("Atk");
            player.anim.AnimationFinished += Anim_AnimationFinished;
        }

        private void Anim_AnimationFinished()
        {
            player.anim.AnimationFinished -= Anim_AnimationFinished;

            player.combatStateMachine.ChangeState(player.combatStateMachine.idleAtkState);
        }

        public override void Update(float delta)
        {
        }

        public override void FixedUpdate(float delta)
        {
        }

        public override void Exit()
        {
            if(player.moveStateMachine.curState == player.moveStateMachine.idleState)
            {
                player.anim.Play("Idle");
                return;
            }
            if (player.moveStateMachine.curState == player.moveStateMachine.walkState)
            {
                player.anim.Play("Walk");
                return;
            }
            if (player.moveStateMachine.curState == player.moveStateMachine.runState)
            {
                player.anim.Play("Run");
                return;
            }
        }
    }
}
