using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.StateMachines.CombatStates
{
    public class SkillState : StateBase
    {
        public SkillState(Player player)
        {
            this.player = player;
        }

        private float timer = 0f;
        private float duration = 0.5f;

        public override void Enter()
        {
            GD.Print("curSkillIndex : " + player.curSkillIndex);
            //todo : 这里根据索引找到具体技能, 从具体技能那获取技能是否禁用移动, 持续时间, 能否被roll打断等信息
            
        }

        public override void Update(float delta)
        {
            timer += delta;
            if (timer < duration) return;
            player.combatStateMachine.ChangeState(player.combatStateMachine.idleAtkState);
            return;
        }

        public override void FixedUpdate(float delta)
        {
            
        }

        public override void Exit()
        {
            timer = 0;
        }
    }
}
