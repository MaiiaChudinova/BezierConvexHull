using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
    public static class SamplePointSets
    {
        public static List<Point> GenerateRandomPointSet(int num, int low = 0, int high = 50)
        {
            List<Point> set = new List<Point>();
            Random generator = new Random();

            for (int i = 0; i < num; ++i)
                set.Add(new Point(generator.Next(low, high), generator.Next(low, high)));

            return set;
        }

        // TODO: implement this:
        public static List<List<Point>> GetSamplesFromFile(string path)
        {
            List<List<Point>> samples = new List<List<Point>>();

            return samples;
        }

        public static List<Point> sample1 = new List<Point>()
        {
            new Point(1, 1),
            new Point(4, 1),
            new Point(4, 4),
            new Point(1, 4)
        };

        public static List<Point> sample2 = new List<Point>()
        {
            new Point(1, 1),
            new Point(4, 1),
            new Point(4, 4),
            new Point(1, 4),
            new Point(2, 1),
            new Point(3, 1),
            new Point(4, 2),
            new Point(4, 3),
            new Point(2, 4),
            new Point(3, 4),
            new Point(1, 2),
            new Point(1, 3)
        };

        public static List<Point> sample3 = new List<Point>()
        {
            new Point(1, 2),
            new Point(2, 2),
            new Point(4, 3),
            new Point(7, 2),
            new Point(6, 3),
            new Point(8, 4),
            new Point(1, 4),
            new Point(4, 5),
            new Point(3, 5),
            new Point(6, 5)
        };

        public static List<Point> sample4 = new List<Point>()
        {
            new Point(1, 3),
            new Point(3, 2),
            new Point(5, 1),
            new Point(2, 4),
            new Point(3, 5),
            new Point(4, 6)
        };

        public static List<Point> sample5 = new List<Point>()
        {
            new Point(6, 1),
            new Point(2, 1),
            new Point(4, 1),
            new Point(1, 5),
            new Point(1, 3),
            new Point(1, 4),
            new Point(1, 6),
            new Point(4, 3),
            new Point(3, 3),
            new Point(3, 4),
            new Point(2, 2)
        };

        public static List<Point> sample6 = new List<Point>()
        {
            new Point(3, 5),
            new Point(4, 2),
            new Point(4, 6),
            new Point(5, 5),
            new Point(5, 6),
            new Point(6, 3),
            new Point(6, 7),
            new Point(7, 3),
            new Point(8, 5),
            new Point(8, 6),
            new Point(9, 4),
            new Point(9, 6),
            new Point(10, 5),
            new Point(11, 6),
            new Point(12, 2),
            new Point(12, 5),
            new Point(12, 7),
            new Point(12, 8),
            new Point(14, 7),
            new Point(15, 3),
            new Point(3, 3),
            new Point(3, 6),
            new Point(3, 4),
            new Point(15, 4),
            new Point(15, 2),
            new Point(15, 5),
            new Point(15, 6)
        };

        public static List<Point> sample7 = new List<Point>()
        {
            new Point(1, 1),
            new Point(3, 2),
            new Point(5, 1),
            new Point(2, 4),
            new Point(4, 4),
            new Point(0, 5),
            new Point(6, 5),
            new Point(2, 6),
            new Point(4, 6),
            new Point(3, 8)
        };

        public static List<Point> sample8 = new List<Point>()
        {
            new Point(0, 3),
            new Point(0, 5),
            new Point(0, 8),
            new Point(1, 4),
            new Point(4, 3),
            new Point(6, 3),
            new Point(10, 5),
            new Point(4, 5),
            new Point(1, 5),
            new Point(3, 8)
        };

        public static List<Point> sample9 = new List<Point>()
        {
            new Point(3, 7),
            new Point(2, 5),
            new Point(4, 6),
            new Point(3, 4),
            new Point(6, 4),
            new Point(3, 1),
            new Point(1, 6)
        };
    }
}
