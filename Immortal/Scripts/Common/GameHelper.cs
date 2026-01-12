using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Scripts.Common
{
    public static class GameHelper
    {
        /// <summary>
        /// 获取某点附近距离最近的 T 类型对象（使用平方距离）
        /// </summary>
        public static T GetClosestNode<T>(Vector2 origin, IEnumerable<T> candidates) where T : Node2D
        {
            T closest = null;
            float minDistSqr = float.MaxValue;

            foreach (var candidate in candidates)
            {
                float distSqr = origin.DistanceSquaredTo(candidate.GlobalPosition);
                if (distSqr < minDistSqr)
                {
                    minDistSqr = distSqr;
                    closest = candidate;
                }
            }

            return closest;
        }
    }
}
