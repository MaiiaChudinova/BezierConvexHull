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
        public ObservableCollection<PointViewModel> CurrentPointSet { get; } = new ObservableCollection<PointViewModel>();

        public RelayCommand GenerateRandomSampleCommand { get; set; }

        public RelayCommand DrawConvexHullCommand { get; set; }

        public RelayCommand DrawNBezierCurveCommand { get; set; }

        public RelayCommand DrawBezierApproximationCommand { get; set; }

        public RelayCommand ReadFromFileCommand { get; set; }

        public RelayCommand SaveToFileCommand { get; set; }

        public RelayCommand GetHelpCommand { get; set; }

        public int PointsToGenerate { get; set; } = 20;

        public int MaxPointsNumber { get; } = 50;

        public double SmoothCoefficient { get; set; }

        public int SceneSize { get; } = 420;

        private List<Point> CurrentHull { get; set; } = new List<Point>();


        private const int SCALE_INCREASE = 25;

        private const int SCALE_SHIFT = 50;

        private const int RADIUS = 10;

        private const int BEZIER_RADIUS = 2;

        private List<Point> curPoints = new List<Point>();

        public MainViewModel()
        {
            GenerateRandomSampleCommand = new RelayCommand(obj =>
            {
                CurrentPointSet.Clear();
                List<Point> set = new List<Point>();
                Random generator = new Random();

                int num = PointsToGenerate;
                int low = 0;
                int high = 10;

                for (int i = 0; i < num; ++i)
                    set.Add(new Point(generator.Next(low, high), generator.Next(low, high)));

                foreach (Point p in set)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Y = p.y * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Width = RADIUS, 
                                                               Height = RADIUS });
                }

                curPoints = set;

            }, obj => true);

            GenerateRandomSampleCommand.Execute(null);

            DrawConvexHullCommand = new RelayCommand(obj =>
            {
                CurrentHull = QuickHull.BuildConvexHull(curPoints);
                Point p0 = CurrentHull[0];
                CurrentHull.Sort(new PointComparer(p0));

                foreach (Point p in CurrentHull)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Y = p.y * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Width = RADIUS, 
                                                               Height = RADIUS, 
                                                               IsHullPoint = true });
                }

            }, obj => true);

            DrawNBezierCurveCommand = new RelayCommand(obj =>
            {
                List<Point> approximation = Model.BezierRouteApproximation.ProvideNDegreeBezierCurveApproximation(CurrentHull);

                foreach (Point p in approximation)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Y = p.y * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Width = BEZIER_RADIUS, 
                                                               Height = BEZIER_RADIUS, 
                                                               IsHullPoint = true });
                }

            }, obj => true);

            DrawBezierApproximationCommand = new RelayCommand(obj =>
            {
                List<Point> approximation = Model.BezierRouteApproximation.ProvideRouteApproximation(CurrentHull);

                foreach (Point p in approximation)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Y = p.y * SCALE_INCREASE + SCALE_SHIFT, 
                                                               Width = BEZIER_RADIUS, 
                                                               Height = BEZIER_RADIUS, 
                                                               IsHullPoint = true });
                }

            }, obj => true);

            ReadFromFileCommand = new RelayCommand(obj =>
            {
                string path = @"InputFiles\default.txt";

                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    CurrentPointSet.Clear();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] coords = line.Split(',');
                        CurrentPointSet.Add(new PointViewModel() { X = Convert.ToSingle(coords[0]) * SCALE_INCREASE + SCALE_SHIFT, 
                            Y = Convert.ToSingle(coords[1]) * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS });
                    }
                }
            }, obj => true);

            SaveToFileCommand = new RelayCommand(obj =>
            {
                string path = @"InputFiles\defaultSaved.txt";

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    foreach (Point p in curPoints)
                    {
                        sw.WriteLine(p);
                    }
                }

            }, obj => true);
        }
    }
}
