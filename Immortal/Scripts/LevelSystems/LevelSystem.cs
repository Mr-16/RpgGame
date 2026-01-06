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
        public event Action<WuXingType, int> TakeExped;//经验弹窗
        public event Action<WuXingType, int> LevelUped;//升级弹窗
        public event Action<WuXingType, int> CurExpChanged;//经验条
        public event Action<WuXingType, int> NeedExpChanged;//经验条
        public event Action<WuXingType, int> LevelChanged;//等级lb

        public LevelSystem()
        {

        }
        public void Init()
        {
            LevelMap.Add(WuXingType.Metal, 1);
            LevelChanged?.Invoke(WuXingType.Metal, 1);
            LevelMap.Add(WuXingType.Wood, 1);
            LevelChanged?.Invoke(WuXingType.Wood, 1);
            LevelMap.Add(WuXingType.Water, 1);
            LevelChanged?.Invoke(WuXingType.Water, 1);
            LevelMap.Add(WuXingType.Fire, 1);
            LevelChanged?.Invoke(WuXingType.Fire, 1);
            LevelMap.Add(WuXingType.Earth, 1);
            LevelChanged?.Invoke(WuXingType.Earth, 1);

            CurExpMap.Add(WuXingType.Metal, 0);
            CurExpChanged?.Invoke(WuXingType.Metal, 0);
            CurExpMap.Add(WuXingType.Wood, 0);
            CurExpChanged?.Invoke(WuXingType.Wood, 0);
            CurExpMap.Add(WuXingType.Water, 0);
            CurExpChanged?.Invoke(WuXingType.Water, 0);
            CurExpMap.Add(WuXingType.Fire, 0);
            CurExpChanged?.Invoke(WuXingType.Fire, 0);
            CurExpMap.Add(WuXingType.Earth, 0);
            CurExpChanged?.Invoke(WuXingType.Earth, 0);

            NeedExpMap.Add(WuXingType.Metal, NeedExp(1));
            NeedExpChanged?.Invoke(WuXingType.Metal, NeedExp(1));
            NeedExpMap.Add(WuXingType.Wood, NeedExp(1));
            NeedExpChanged?.Invoke(WuXingType.Wood, NeedExp(1));
            NeedExpMap.Add(WuXingType.Water, NeedExp(1));
            NeedExpChanged?.Invoke(WuXingType.Water, NeedExp(1));
            NeedExpMap.Add(WuXingType.Fire, NeedExp(1));
            NeedExpChanged?.Invoke(WuXingType.Fire, NeedExp(1));
            NeedExpMap.Add(WuXingType.Earth, NeedExp(1));
            NeedExpChanged?.Invoke(WuXingType.Earth, NeedExp(1));
        }
        public static int NeedExp(int level)
        {
            return (int)(100 * Math.Pow(level, 1.5));
        }
        public void TakeExp(WuXingType type, int exp)
        {
            CurExpMap[type] += exp;
            TakeExped?.Invoke(type, exp);//通知player弹出经验label
            CurExpChanged?.Invoke(type, CurExpMap[type]);
            while (CurExpMap[type] >= NeedExpMap[type])
            {
                CurExpMap[type] -= NeedExpMap[type];
                CurExpChanged?.Invoke(type, CurExpMap[type]);
                LevelMap[type]++;
                NeedExpMap[type] = NeedExp(LevelMap[type]);
                LevelUped?.Invoke(type, LevelMap[type]);//通知player弹出经验label
                NeedExpChanged?.Invoke(type, NeedExpMap[type]);
                LevelChanged?.Invoke(type, LevelMap[type]);
            }
        }
    }
}
