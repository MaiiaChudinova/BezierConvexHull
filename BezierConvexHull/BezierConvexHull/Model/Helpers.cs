using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
	public static class Helpers
	{
		/// <summary>
		/// < 0 if p is to the right of p1p2, 0 if p is on p1p2, > 0 if p is to the left of p1p2
		/// </summary>
		/// <param name="p1">Start point of the line</param>
		/// <param name="p2">End point of the line</param>
		/// <param name="p">Point to localize</param>
		/// <returns>Cross product of vectors p1p2 and p1p</returns>
		public static double CrossProduct(Point p1, Point p2, Point p)
		{
			return (p2.x - p1.x) * (p.y - p1.y) - (p.x - p1.x) * (p2.y - p1.y);
		}

		public static double CrossProductSign(Point p1, Point p2, Point p)
		{
			return Math.Sign((p2.x - p1.x) * (p.y - p1.y) - (p.x - p1.x) * (p2.y - p1.y));
		}

		public static double CrossProductLength(Point p1, Point p2, Point p)
		{
			return Math.Abs((p2.x - p1.x) * (p.y - p1.y) - (p.x - p1.x) * (p2.y - p1.y));
		}

		/// <summary>
		/// Orientation of ordered triplet (p, q, r).
		/// </summary>
		/// <param name="p"></param>
		/// <param name="q"></param>
		/// <param name="r"></param>
		/// <returns>0 - p, q and r are colinear, 1 - Clockwise, -1 - Counterclockwise</returns>
		public static double Orientation(Point p, Point q, Point r)
		{
			double value = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

			if (value == 0) return 0; // colinear
			return (value > 0) ? 1 : -1;
		}

		public static double DistanceSquare(Point p1, Point p2)
		{
			return (p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y);
		}
	}
}
