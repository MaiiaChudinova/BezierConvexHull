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
        // TODO: change to suitable collection
        public ObservableCollection<PointSetViewModel> SamplePointSetsCollection { get; set; }

        public ObservableCollection<PointViewModel> CurrentPointSet { get; set; } = new ObservableCollection<PointViewModel>();

        public RelayCommand NextSampleCommand { get; set; }

        public RelayCommand GenerateRandomSampleCommand { get; set; }

        public RelayCommand ShowHelpCommand { get; set; }

        public int MaxPointsNumber { get; }

        private const int SCALE_INCREASE = 25;

        private const int SCALE_SHIFT = 50;
       
        public MainViewModel()
        {
            int radius = 10;
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = radius, Height = radius });
            CurrentPointSet.Add(new PointViewModel() { X = 8 * SCALE_INCREASE, Y = 4 * SCALE_INCREASE, Width = radius, Height = radius });
            CurrentPointSet.Add(new PointViewModel() { X = 4 * SCALE_INCREASE, Y = 8 * SCALE_INCREASE, Width = radius, Height = radius });
            CurrentPointSet.Add(new PointViewModel() { X = 8 * SCALE_INCREASE, Y = 8 * SCALE_INCREASE, Width = radius, Height = radius, IsHullPoint = true });

            GenerateRandomSampleCommand = new RelayCommand( obj =>
            {
                CurrentPointSet.Clear();
                List<Point> points = new List<Point>();
                points = SamplePointSets.GenerateRandomPointSet(10, 0, 10);
                foreach (Point p in points)
                {
                    CurrentPointSet.Add(new PointViewModel() { X = p.x * SCALE_INCREASE + SCALE_SHIFT, Y = p.y * SCALE_INCREASE + SCALE_SHIFT, Width = radius, Height = radius });
                }
            }, obj => true);

            HashSet<PointViewModel> test = new HashSet<PointViewModel>();
            test.Add(new PointViewModel() { X = 100, Y = 100, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 70, Y = 65, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 40, Y = 100, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 100, Y = 130, Width = radius, Height = radius });
            test.Add(new PointViewModel() { X = 190, Y = 10, Width = radius, Height = radius });
            //SamplePointSets.Add(new PointSetViewModel(test));

            SamplePointSetsCollection = new ObservableCollection<PointSetViewModel>();
            SamplePointSetsCollection.Add(new PointSetViewModel(test));
            SamplePointSetsCollection.Add(new PointSetViewModel(test));
            //SamplePointSets
            
            
        }
    }
}
