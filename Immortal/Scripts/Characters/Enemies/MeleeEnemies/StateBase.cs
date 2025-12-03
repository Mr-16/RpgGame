using RpgGame.Scripts.Characters.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Enemies.MeleeEnemies
{
    public abstract class StateBase
    {
        public MeleeEnemy enemy;



        public abstract void Enter();
        public abstract void Update(float delta);
        public abstract void FixedUpdate(float delta);
        public abstract void Exit();
    }
}
