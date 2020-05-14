using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BezierConvexHull
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<ObservableCollection<PointViewModel>> SamplePointSetsCollection { get; set; }

        public ObservableCollection<PointViewModel> CurrentPointSet { get; } = new ObservableCollection<PointViewModel>();

        public RelayCommand NextSampleCommand { get; set; }

        public RelayCommand GenerateRandomSampleCommand { get; set; }

        public RelayCommand ShowHelpCommand { get; set; }
        
        public RelayCommand SaveAsSampleCommand { get; set; }

        public RelayCommand SamplesListBoxSelectionChangedCommand { get; set; }

        public RelayCommand DrawBezierCommand { get; set; }

        public RelayCommand ReadFromFileCommand { get; set; }

        public RelayCommand InterpreteAsControlPointsCommand { get; set; }

        public int MaxPointsNumber { get; }

        public int SceneSize { get; } = 420;

        private const int SCALE_INCREASE = 25;

        private const int SCALE_SHIFT = 50;

        private const int RADIUS = 10;

        private const int BEZIER_RADIUS = 2;

        private List<Point> curPoints = new List<Point>();
       
        public MainViewModel()
        {
            GenerateRandomSampleCommand = new RelayCommand( obj =>
            {
                CurrentPointSet.Clear();
                List<Point> points = new List<Point>();
                points = SamplePointSets.GenerateRandomPointSet(10, 0, 10);
                foreach (Point p in points)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, Y = p.y * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS });
                }
                points = QuickHull.BuildConvexHull(points);
                using (StreamWriter sw = new StreamWriter("test.txt", false, System.Text.Encoding.Default))
                {
                    foreach (Point p in points)
                    {
                        CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, Y = p.y * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS, IsHullPoint = true });

                        sw.WriteLine(p.x + "," + p.y);
                    }
                }
                curPoints = points;
                Point p0 = curPoints[0];
                curPoints.Sort(new PointComparer(p0));

                bezierBuilt = false;
            }, obj => true);

            GenerateRandomSampleCommand.Execute(null);

            DrawBezierCommand = new RelayCommand(obj =>
            {
                SetPathData(curPoints);

            }, obj => true);

            ReadFromFileCommand = new RelayCommand(obj =>
            {
                string path = @"C:\Users\Asus\Desktop\InputFiles\input1.txt";

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    CurrentPointSet.Clear(); 
                    curPoints = new List<Point>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] coords = line.Split(' '); 
                        CurrentPointSet.Add(new PointViewModel() { X = Convert.ToSingle(coords[0]) * SCALE_INCREASE + SCALE_SHIFT, Y = Convert.ToSingle(coords[1]) * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS, IsHullPoint = true });
                        curPoints.Add(new Point(Convert.ToSingle(coords[0]), Convert.ToSingle(coords[1])));
                    }
                }
            }, obj => true);

            InterpreteAsControlPointsCommand = new RelayCommand(obj =>
            {
                List<Point> bezierPoints = new List<Point>();
                for (double t = 0; t < 1; t += 0.005)
                {
                    double x = 0;
                    double y = 0;

                    for (int k = 0; k < curPoints.Count; ++k)
                    {
                        x += curPoints[k].x * b(curPoints.Count - 1, k, t);
                        y += curPoints[k].y * b(curPoints.Count - 1, k, t);
                    }
                    bezierPoints.Add(new Point(x, y));
                }

                foreach (Point p in bezierPoints)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, Y = p.y * SCALE_INCREASE + SCALE_SHIFT, Width = BEZIER_RADIUS, Height = BEZIER_RADIUS, IsHullPoint = true });
                }

                CurrentPointSet.Add(new PointViewModel() { X = curPoints[0].x * SCALE_INCREASE + SCALE_SHIFT, Y = curPoints[0].y * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS, IsHullPoint = false });
                CurrentPointSet.Add(new PointViewModel() { X = curPoints[curPoints.Count-1].x * SCALE_INCREASE + SCALE_SHIFT, Y = curPoints[curPoints.Count - 1].y * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS, IsHullPoint = false });

            }, obj => true);

            SamplePointSetsCollection = new ObservableCollection<ObservableCollection<PointViewModel>>();
            SamplePointSetsCollection.Add(CurrentPointSet);
        }

        private bool bezierBuilt = false;

        void SetPathData(List<Point> Points)
        {
            if (bezierBuilt) return;
            var bezierSegments = InterpolatePointWithBeizerCurves(Points, true);

            foreach (var segment in bezierSegments)
            {
                DrawCubicBezier(segment);
            }
            bezierBuilt = true;
        }

        private void DrawCubicBezier(BezierCurveSegment segment)
        {
            for (double t = 0; t < 1; t+=0.01)
            {
                double x = (1 - t) * (1 - t) * (1 - t) * segment.StartPoint.X + 3 * t * (1 - t) * (1 - t) * segment.FirstControlPoint.X + 3 * t * t * (1 - t) * segment.SecondControlPoint.X + t * t * t * segment.EndPoint.X;

                double y = (1 - t) * (1 - t) * (1 - t) * segment.StartPoint.Y + 3 * t * (1 - t) * (1 - t) * segment.FirstControlPoint.Y + 3 * t * t * (1 - t) * segment.SecondControlPoint.Y + t * t * t * segment.EndPoint.Y;

                CurrentPointSet.Add(new PointViewModel() { X = x * SCALE_INCREASE + SCALE_SHIFT, Y = y * SCALE_INCREASE + SCALE_SHIFT, Width = BEZIER_RADIUS, Height = BEZIER_RADIUS, IsHullPoint = true });
            }
        }

        private double b(int n, int k, double t)
        {
            return C(n, k) * Math.Pow(t, k) * Math.Pow(1 - t, n - k);
        }

        private double C(int n, int k)
        {
            return factorial(n) / (factorial(k) * factorial(n - k));
        }

        private double factorial(int arg)
        {
            if (arg == 0) return 1;

            double res = 1;
            for (int i = 1; i <= arg; i++)
                res *= i;
            return res;
        }

        public class BezierCurveSegment
        {
            public System.Windows.Point StartPoint { get; set; }
            public System.Windows.Point EndPoint { get; set; }
            public System.Windows.Point FirstControlPoint { get; set; }
            public System.Windows.Point SecondControlPoint { get; set; }
        }

        public static List<BezierCurveSegment> InterpolatePointWithBeizerCurves(List<Point> points, bool isClosedCurve)
        {
            if (points.Count < 3)
                return null;
            var toRet = new List<BezierCurveSegment>();

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

                //double k1 = 0.5;
                //double k2 = 0.5;

                double xm1 = xc1 + (xc2 - xc1) * k1;
                double ym1 = yc1 + (yc2 - yc1) * k1;

                double xm2 = xc2 + (xc3 - xc2) * k2;
                double ym2 = yc2 + (yc3 - yc2) * k2;

                const double smoothValue = 0.8;
                // Resulting control points. Here smooth_value is mentioned
                // above coefficient K whose value should be in range [0...1].
                //double ctrl1_x = xm1 + (xc2 - xm1) * smoothValue + x1 - xm1;
                //double ctrl1_y = ym1 + (yc2 - ym1) * smoothValue + y1 - ym1;

                double ctrl1_x = (xc2 - xm1) * smoothValue + x1;
                double ctrl1_y = (yc2 - ym1) * smoothValue + y1;

                //double ctrl2_x = xm2 + (xc2 - xm2) * smoothValue + x2 - xm2;
                //double ctrl2_y = ym2 + (yc2 - ym2) * smoothValue + y2 - ym2;
                double ctrl2_x = (xc2 - xm2) * smoothValue + x2;
                double ctrl2_y = (yc2 - ym2) * smoothValue + y2;
                toRet.Add(new BezierCurveSegment
                {
                    StartPoint = new System.Windows.Point(x1, y1),
                    EndPoint = new System.Windows.Point(x2, y2),
                    FirstControlPoint = i == 0 && !isClosedCurve ? new System.Windows.Point(x1, y1) : new System.Windows.Point(ctrl1_x, ctrl1_y),
                    SecondControlPoint = i == points.Count - 2 && !isClosedCurve ? new System.Windows.Point(x2, y2) : new System.Windows.Point(ctrl2_x, ctrl2_y)
                });
            }

            return toRet;
        }
    }
}
