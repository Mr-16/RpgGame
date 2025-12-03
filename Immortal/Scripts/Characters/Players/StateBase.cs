using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Characters.Players
{
    public abstract class StateBase
    {
        public Player player;

        public abstract void Enter();
        public abstract void Update(float delta);
        public abstract void FixedUpdate(float delta);
        public abstract void Exit();
    }
}
