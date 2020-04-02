using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
	public class PointComparer : IComparer<Point>
	{
		/// <summary>
		/// A point which is used to compare the polar angle of another points 
		/// relatively to this point
		/// </summary>
		Point p0;

		public int Compare(Point x, Point y)
		{
			int o = Helpers.Orientation(p0, x, y);
			if (o == 0)
				return (Helpers.DistanceSquare(p0, y) >= Helpers.DistanceSquare(p0, x)) ? -1 : 1;

			return o;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p0"></param>
		public PointComparer(Point p0)
		{
			this.p0 = p0;
		}
	}
}
