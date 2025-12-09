using RpgGame.Scripts.Characters.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.GameManager
{
    public class EnemyManager
    {
        public List<Enemy> EnemyList = new List<Enemy>();
        private static EnemyManager instance;
        public static EnemyManager Instance()
        {
            if (instance == null)
            {
                instance = new EnemyManager();
            }
            return instance;
        }

    }
}
