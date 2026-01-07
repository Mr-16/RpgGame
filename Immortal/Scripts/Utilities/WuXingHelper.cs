using Godot;
using RpgGame.Scripts.GameSystem;
using RpgGame.Scripts.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Utilities
{
    public static class WuXingHelper
    {
        private static string metalStr = "金";
        private static string woodStr = "木";
        private static string waterStr = "水";
        private static string fireStr = "火";
        private static string earthStr = "土";

        public static Color GetColor(WuXingType type, float alpha = 1f)
        {
            return type switch
            {
                WuXingType.Metal => new(1f, 1f, 0f, alpha),
                WuXingType.Wood => new(0f, 1f, 0f, alpha),
                WuXingType.Water => new(0f, 0f, 1f, alpha),
                WuXingType.Fire => new(1f, 0.75f, 0.2f, alpha),
                WuXingType.Earth => new(0.6f, 0.4f, 0.2f, alpha),
            };
        }

        public static string GetChineseStr(WuXingType type)
        {
            return type switch
            {
                WuXingType.Metal => metalStr,
                WuXingType.Wood => woodStr,
                WuXingType.Water => waterStr,
                WuXingType.Fire => fireStr,
                WuXingType.Earth => earthStr,
            };
        }
    }

}
