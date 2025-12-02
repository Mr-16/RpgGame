using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.StateMachines.CombatStates
{
    public class IdleAtkState : StateBase
    {
        public IdleAtkState(Player player)
        {
            this.player = player;
        }


        public override void Enter()
        {
        }

       

        public override void Update(float delta)
        {
            if (Input.IsActionJustPressed("Atk") && player.moveStateMachine.curState != player.moveStateMachine.rollState)
            {
                player.combatStateMachine.ChangeState(player.combatStateMachine.atkState);
                return;
            }
            if (Input.IsActionJustPressed("Skill_1") && player.moveStateMachine.curState != player.moveStateMachine.rollState)
            {
                player.curSkillIndex = 0;
                player.combatStateMachine.ChangeState(player.combatStateMachine.skillState);
                return;
            }
            if (Input.IsActionJustPressed("Skill_2") && player.moveStateMachine.curState != player.moveStateMachine.rollState)
            {
                player.curSkillIndex = 1;
                player.combatStateMachine.ChangeState(player.combatStateMachine.skillState);
                return;
            }
            if (Input.IsActionJustPressed("Skill_3") && player.moveStateMachine.curState != player.moveStateMachine.rollState)
            {
                player.curSkillIndex = 2;
                player.combatStateMachine.ChangeState(player.combatStateMachine.skillState);
                return;
            }

        }
        public override void FixedUpdate(float delta)
        {
        }

        public override void Exit()
        {
        }
    }
}
