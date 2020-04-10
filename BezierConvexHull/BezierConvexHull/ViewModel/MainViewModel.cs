using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int MaxPointsNumber { get; }

        public int SceneSize { get; } = 420;

        private const int SCALE_INCREASE = 25;

        private const int SCALE_SHIFT = 50;

        private const int RADIUS = 10;
       
        public MainViewModel()
        {

            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 8 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 8 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 8 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 8 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 8 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 8 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS });
            CurrentPointSet.Add(new PointViewModel() { X = 8 * SCALE_INCREASE, Y = 8 * SCALE_INCREASE, Width = RADIUS, Height = RADIUS, IsHullPoint = true });

            GenerateRandomSampleCommand = new RelayCommand( obj =>
            {
                CurrentPointSet.Clear();
                List<Point> points = new List<Point>();
                points = SamplePointSets.GenerateRandomPointSet(10, 0, 10);
                foreach (Point p in points)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, Y = p.y * SCALE_INCREASE + SCALE_SHIFT, Width = RADIUS, Height = RADIUS });
                }
            }, obj => true);
            SamplePointSetsCollection = new ObservableCollection<ObservableCollection<PointViewModel>>();
            SamplePointSetsCollection.Add(CurrentPointSet);
            SamplePointSetsCollection.Add(CurrentPointSet);
            /*
            List<PointViewModel> test = new List<PointViewModel>();
            test.Add(new PointViewModel() { X = 100, Y = 100, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 70, Y = 65, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 40, Y = 100, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 100, Y = 130, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 190, Y = 10, Width = radius, Height = radius });

            SamplePointSetsCollection = new ObservableCollection<PointSetViewModel>(
                SamplePointSets.GetStandartSamples().Select(pointset => new PointSetViewModel(
                    new List<PointViewModel>(
                        pointset.Select(point => new PointViewModel()
                        { X = point.x, Y = point.y, Height = radius, Width = radius, IsHullPoint = true })))));

            SamplesListBoxSelectionChangedCommand = new RelayCommand(obj =>
            {
                int selectedIndex = 4;
                CurrentPointSet = SamplePointSetsCollection[selectedIndex];

            }, obj => true);
            */
        }
    }
}
