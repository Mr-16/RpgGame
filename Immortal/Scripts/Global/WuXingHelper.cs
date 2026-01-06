using Godot;
using RpgGame.Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Global
{
    public static class WuXingHelper
    {
        private static readonly Color metalColor = new(1f, 1f, 0f, 1f);
        private static readonly Color woodColor = new(0f, 1f, 0f, 1f);
        private static readonly Color waterColor = new(0f, 0f, 1f, 1f);
        private static readonly Color fireColor = new(1f, 0.75f, 0.2f, 1f);
        private static readonly Color earthColor = new(0.6f, 0.4f, 0.2f, 1f);

        private static string metalStr = "金";
        private static string woodStr = "木";
        private static string waterStr = "水";
        private static string fireStr = "火";
        private static string earthStr = "土";

        public static Color GetColor(WuXingType type)
        {
            return type switch
            {
                WuXingType.Metal => metalColor,
                WuXingType.Wood => woodColor,
                WuXingType.Water => waterColor,
                WuXingType.Fire => fireColor,
                WuXingType.Earth => earthColor,
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
