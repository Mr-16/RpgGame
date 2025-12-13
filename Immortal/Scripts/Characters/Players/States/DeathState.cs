using Godot;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players.States
{
    public class DeathState : StateBase
    {
        public DeathState(Player player)
        {
            this.player = player;
            
        }

        public override void Enter()
        {
            player.Anim.Play("Death");
            player.Anim.AnimationFinished += Anim_AnimationFinished;
        }

        private void Anim_AnimationFinished()
        {
            player.Anim.AnimationFinished -= Anim_AnimationFinished;
            player.QueueFree();
            GameManager.Instance().ChangeState(GameState.GameOver);
        }

        public override void Update(float delta)
        {

        }
        public override void FixedUpdate(float delta)
        {
        }
        public override void Exit()
        {
        }
    }
}
