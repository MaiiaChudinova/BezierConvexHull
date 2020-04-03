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
    }
}
