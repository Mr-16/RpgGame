using Godot;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.LevelSystems
{
    public class LevelSystem
    {
        public Dictionary<WuXingType, int> LevelMap = new Dictionary<WuXingType, int>();
        public Dictionary<WuXingType, int> CurExpMap = new Dictionary<WuXingType, int>();
        public Dictionary<WuXingType, int> NeedExpMap = new Dictionary<WuXingType, int>();
        public event Action<WuXingType, int> TakeExped;
        public event Action<WuXingType, int> LevelUped;

        public LevelSystem()
        {
            LevelMap.Add(WuXingType.Metal, 1);
            LevelMap.Add(WuXingType.Wood, 1);
            LevelMap.Add(WuXingType.Water, 1);
            LevelMap.Add(WuXingType.Fire, 1);
            LevelMap.Add(WuXingType.Earth, 1);

            CurExpMap.Add(WuXingType.Metal, 0);
            CurExpMap.Add(WuXingType.Wood, 0);
            CurExpMap.Add(WuXingType.Water, 0);
            CurExpMap.Add(WuXingType.Fire, 0);
            CurExpMap.Add(WuXingType.Earth, 0);

            NeedExpMap.Add(WuXingType.Metal, NeedExp(0));
            NeedExpMap.Add(WuXingType.Wood, NeedExp(0));
            NeedExpMap.Add(WuXingType.Water, NeedExp(0));
            NeedExpMap.Add(WuXingType.Fire, NeedExp(0));
            NeedExpMap.Add(WuXingType.Earth, NeedExp(0));
        }
        public static int NeedExp(int level)
        {
            return (int)(100 * Math.Pow(level, 1.5));
        }
        public void TakeExp(WuXingType type, int exp)
        {
            CurExpMap[type] += exp;
            TakeExped?.Invoke(type, exp);//通知player弹出经验label
            while(CurExpMap[type] > NeedExpMap[type])
            {
                CurExpMap[type] -= NeedExpMap[type];
                LevelMap[type]++;
                LevelUped?.Invoke(type, LevelMap[type]);//通知player弹出经验label
            }
        }
    }
}
