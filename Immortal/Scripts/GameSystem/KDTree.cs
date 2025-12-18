using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericKDTree
{

}


namespace RpgGame.Scripts.GameSystem
{

    // 泛型 KD 树节点
    public class KDNode<T>
    {
        public T Data;
        public double[] Point; // k 维坐标
        public KDNode<T> Left;
        public KDNode<T> Right;
        public int Depth;

        public KDNode(T data, double[] point, int depth)
        {
            Data = data;
            Point = point;
            Depth = depth;
        }
    }

    public class KDTree<T>
    {
        private int k; // 维度
        private KDNode<T> Root;

        public KDTree(int dimension)
        {
            k = dimension;
        }

        // 建树
        public void Build(List<(T Data, double[] Point)> items)
        {
            Root = BuildRecursive(items, 0);
        }

        private KDNode<T> BuildRecursive(List<(T Data, double[] Point)> items, int depth)
        {
            if (items == null || items.Count == 0) return null;

            int axis = depth % k;
            items.Sort((a, b) => a.Point[axis].CompareTo(b.Point[axis]));
            int median = items.Count / 2;

            var node = new KDNode<T>(items[median].Data, items[median].Point, depth);
            node.Left = BuildRecursive(items.GetRange(0, median), depth + 1);
            node.Right = BuildRecursive(items.GetRange(median + 1, items.Count - median - 1), depth + 1);
            return node;
        }

        // 最近邻查询
        public T Nearest(double[] target)
        {
            var result = NearestRecursive(Root, target, Root, double.MaxValue);
            return result.Data;
        }

        private KDNode<T> NearestRecursive(KDNode<T> node, double[] target, KDNode<T> best, double bestDist)
        {
            if (node == null) return best;

            double dist = DistanceSquared(node.Point, target);
            if (dist < bestDist)
            {
                bestDist = dist;
                best = node;
            }

            int axis = node.Depth % k;
            double diff = target[axis] - node.Point[axis];

            KDNode<T> first = diff < 0 ? node.Left : node.Right;
            KDNode<T> second = diff < 0 ? node.Right : node.Left;

            best = NearestRecursive(first, target, best, bestDist);
            if (diff * diff < DistanceSquared(best.Point, target))
                best = NearestRecursive(second, target, best, DistanceSquared(best.Point, target));

            return best;
        }

        // 范围查询
        public List<T> RangeSearch(double[] min, double[] max)
        {
            List<T> result = new List<T>();
            RangeSearchRecursive(Root, min, max, result);
            return result;
        }

        private void RangeSearchRecursive(KDNode<T> node, double[] min, double[] max, List<T> result)
        {
            if (node == null) return;

            bool inside = true;
            for (int i = 0; i < k; i++)
            {
                if (node.Point[i] < min[i] || node.Point[i] > max[i])
                {
                    inside = false;
                    break;
                }
            }
            if (inside) result.Add(node.Data);

            int axis = node.Depth % k;
            if (min[axis] <= node.Point[axis]) RangeSearchRecursive(node.Left, min, max, result);
            if (max[axis] >= node.Point[axis]) RangeSearchRecursive(node.Right, min, max, result);
        }

        // 工具方法
        private double DistanceSquared(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < k; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return sum;
        }
    }

    // 测试
    class Program
    {
        static void Main()
        {
            var points = new List<(string, double[])>
            {
                ("A", new double[]{2,3}),
                ("B", new double[]{5,4}),
                ("C", new double[]{9,6}),
                ("D", new double[]{4,7}),
                ("E", new double[]{8,1}),
                ("F", new double[]{7,2})
            };

            var tree = new KDTree<string>(2);
            tree.Build(points);

            double[] target = { 9, 2 };
            var nearest = tree.Nearest(target);
            Console.WriteLine($"Nearest to [9,2]: {nearest}");

            double[] minRange = { 3, 1 };
            double[] maxRange = { 8, 5 };
            var range = tree.RangeSearch(minRange, maxRange);
            Console.WriteLine("Points in range:");
            foreach (var p in range) Console.WriteLine(p);
        }
    }
}
