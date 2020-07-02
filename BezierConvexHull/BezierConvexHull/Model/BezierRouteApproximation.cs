using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull.Model
{
    public class BezierRouteApproximation
    {
        private static List<BezierCurveSegment> BuildCubicBezierSegments(List<Point> points, bool isClosedCurve)
        {
            if (points.Count < 3)
                return null;

            var approximationSegments = new List<BezierCurveSegment>();

            //if is close curve then add the first point at the end
            if (isClosedCurve)
                points.Add(points.First());

            for (int i = 0; i < points.Count - 1; i++)   //iterate for points but the last one
            {
                // Assume we need to calculate the control
                // points between (x1,y1) and (x2,y2).
                // Then x0,y0 - the previous vertex,
                //      x3,y3 - the next one.
                double x1 = points[i].x;
                double y1 = points[i].y;

                double x2 = points[i + 1].x;
                double y2 = points[i + 1].y;

                double x0;
                double y0;

                if (i == 0) //if is first point
                {
                    if (isClosedCurve)
                    {
                        var previousPoint = points[points.Count - 2];    //last Point, but one (due inserted the first at the end)
                        x0 = previousPoint.x;
                        y0 = previousPoint.y;
                    }
                    else    //Get some previouse point
                    {
                        var previousPoint = points[i];  //if is the first point the previous one will be it self
                        x0 = previousPoint.x;
                        y0 = previousPoint.y;
                    }
                }
                else
                {
                    x0 = points[i - 1].x;   //Previous Point
                    y0 = points[i - 1].y;
                }

                double x3, y3;

                if (i == points.Count - 2)    //if is the last point
                {
                    if (isClosedCurve)
                    {
                        var nextPoint = points[1];  //second Point(due inserted the first at the end)
                        x3 = nextPoint.x;
                        y3 = nextPoint.y;
                    }
                    else    //Get some next point
                    {
                        var nextPoint = points[i + 1];  //if is the last point the next point will be the last one
                        x3 = nextPoint.x;
                        y3 = nextPoint.y;
                    }
                }
                else
                {
                    x3 = points[i + 2].x;   //Next Point
                    y3 = points[i + 2].y;
                }

                double xc1 = (x0 + x1) / 2.0;
                double yc1 = (y0 + y1) / 2.0;
                double xc2 = (x1 + x2) / 2.0;
                double yc2 = (y1 + y2) / 2.0;
                double xc3 = (x2 + x3) / 2.0;
                double yc3 = (y2 + y3) / 2.0;

                double len1 = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
                double len2 = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                double len3 = Math.Sqrt((x3 - x2) * (x3 - x2) + (y3 - y2) * (y3 - y2));

                double k1 = len1 / (len1 + len2);
                double k2 = len2 / (len2 + len3);

                double xm1 = xc1 + (xc2 - xc1) * k1;
                double ym1 = yc1 + (yc2 - yc1) * k1;

                double xm2 = xc2 + (xc3 - xc2) * k2;
                double ym2 = yc2 + (yc3 - yc2) * k2;

                const double smoothValue = 0.8;
                // Resulting control points. Here smooth_value is mentioned
                // above coefficient K whose value should be in range [0...1].
                double ctrl1_x = (xc2 - xm1) * smoothValue + x1;
                double ctrl1_y = (yc2 - ym1) * smoothValue + y1;

                double ctrl2_x = (xc2 - xm2) * smoothValue + x2;
                double ctrl2_y = (yc2 - ym2) * smoothValue + y2;

                approximationSegments.Add(new BezierCurveSegment
                {
                    StartPoint = new System.Windows.Point(x1, y1),
                    EndPoint = new System.Windows.Point(x2, y2),
                    FirstControlPoint = i == 0 && !isClosedCurve ? new System.Windows.Point(x1, y1) : new System.Windows.Point(ctrl1_x, ctrl1_y),
                    SecondControlPoint = i == points.Count - 2 && !isClosedCurve ? new System.Windows.Point(x2, y2) : new System.Windows.Point(ctrl2_x, ctrl2_y)
                });
            }

            return approximationSegments;
        }

        public static List<Point> ProvideRouteApproximation(List<Point> points, bool isClosedCurve = true, double discrete = 0.01)
        {
            List<BezierCurveSegment> approximationSegments = BuildCubicBezierSegments(points, isClosedCurve);

            List<Point> approximationPointSet = new List<Point>();

            foreach (BezierCurveSegment segment in approximationSegments)
            {
                for (double t = 0; t < 1; t += discrete)
                {
                    double x = (1 - t) * (1 - t) * (1 - t) * segment.StartPoint.X +
                            3 * t * (1 - t) * (1 - t) * segment.FirstControlPoint.X + 
                            3 * t * t * (1 - t) * segment.SecondControlPoint.X + 
                            t * t * t * segment.EndPoint.X;

                    double y = (1 - t) * (1 - t) * (1 - t) * segment.StartPoint.Y + 
                            3 * t * (1 - t) * (1 - t) * segment.FirstControlPoint.Y + 
                            3 * t * t * (1 - t) * segment.SecondControlPoint.Y + 
                            t * t * t * segment.EndPoint.Y;

                    approximationPointSet.Add(new Point(x, y));
                }
            }

            return approximationPointSet;
        }

        public static List<Point> ProvideNDegreeBezierCurveApproximation(List<Point> points, bool isClosedCurve = true, double discrete = 0.01)
        {
            List<Point> approximationPointSet = new List<Point>();

            if (isClosedCurve) points.Add(points[0]);

            for (double t = 0; t < 1; t += discrete)
            {
                double x = 0;
                double y = 0;

                for (int k = 0; k < points.Count; ++k)
                {
                    x += points[k].x * Helpers.GetBersteinPolinomial(points.Count - 1, k, t);
                    y += points[k].y * Helpers.GetBersteinPolinomial(points.Count - 1, k, t);
                }
                approximationPointSet.Add(new Point(x, y));
            }

            return approximationPointSet;
        }
    }
}
