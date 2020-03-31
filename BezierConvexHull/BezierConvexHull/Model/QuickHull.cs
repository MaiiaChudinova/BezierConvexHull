using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
	public static class QuickHull
	{
		public static HashSet<Point> BuildConvexHull(List<Point> points)
		{
			HashSet<Point> hull = new HashSet<Point>();

			if (points.Count < 3)
			{
				foreach (Point p in points)
					hull.Add(p);
				return hull;
			}

			int leftBottom = 0, rightTop = 0;
			for (int i = 1; i < points.Count; ++i)
			{
				if (points[i].x < points[leftBottom].x || (points[i].x == points[leftBottom].x && points[i].y < points[leftBottom].y))
					leftBottom = i;
				if (points[i].x > points[rightTop].x || (points[i].x == points[rightTop].x && points[i].y > points[rightTop].y))
					rightTop = i;
			}

			Quickhull(points, points[leftBottom], points[rightTop], 1, hull);
			Quickhull(points, points[leftBottom], points[rightTop], -1, hull);

			return hull;
		}

		private static void Quickhull(List<Point> points, Point p1, Point p2, int side, HashSet<Point> hull)
		{
			List<int> indexes = new List<int>();
			int maxDist = 0;

			for (int i = 0; i < points.Count; ++i) indexes.Add(i);
			indexes = indexes.Select(i => i).Where(i => Helpers.CrossProductSign(p1, p2, points[i]) == side).ToList();

			if (indexes.Count == 0)
			{
				hull.Add(p1);
				hull.Add(p2);
				return;
			}

			maxDist = indexes.Select(i => Helpers.CrossProductLength(p1, p2, points[i])).Max();

			indexes.RemoveAll(i => Helpers.CrossProductLength(p1, p2, points[i]) != maxDist);

			int ind = indexes[0];

			// Choose leftmost bottom point from the extremes
			foreach (int index in indexes)
			{
				if (points[index].x < points[ind].x || (points[index].x == points[ind].x && points[index].y < points[ind].y))
					ind = index;
			}

			Quickhull(points, points[ind], p1, -Helpers.CrossProductSign(points[ind], p1, p2), hull);
			Quickhull(points, points[ind], p2, -Helpers.CrossProductSign(points[ind], p2, p1), hull);
		}
	}
}
