using RpgGame.Scripts.Characters.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.GameSystem
{
    public class EnemyManager
    {
        public List<Enemy> EnemyList = new List<Enemy>();
        public KDTree<Enemy> EnemyKdTree = new KDTree<Enemy>(2);//优化敌人遍历
        private static EnemyManager instance;
        private EnemyManager() { }
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
