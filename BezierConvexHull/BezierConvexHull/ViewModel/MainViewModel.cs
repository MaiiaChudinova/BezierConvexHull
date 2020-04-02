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
        public List<PointSetViewModel> SamplePointSets { get; set; }

        public ObservableCollection<PointViewModel> CurrentPointSet { get; set; } = new ObservableCollection<PointViewModel>();

        public RelayCommand NextSampleCommand { get; set; }

        public RelayCommand GenerateRandomSampleCommand { get; set; }

        public RelayCommand ShowHelpCommand { get; set; }

        public int MaxPointsNumber { get; }

        // TODO: Delete these properties and use element binding in XAML only instead of data context binding
        public int MaxPointsXLowerBound { get; }
        public int MinPointsXLowerBound { get; }
        public int MaxPointsXUpperBound { get; }
        public int MinPointsXUpperBound { get; }
        public int MaxPointsYLowerBound { get; }
        public int MinPointsYLowerBound { get; }
        public int MaxPointsYUpperBound { get; } = 400;
        public int MinPointsYUpperBound { get; } = 10;
       
        public MainViewModel()
        {
            int w = 20, h = 20;
            CurrentPointSet.Add(new PointViewModel() { X = 100, Y = 100, Width = w, Height = h });
            CurrentPointSet.Add(new PointViewModel() { X = 200, Y = 100, Width = w, Height = h });
            CurrentPointSet.Add(new PointViewModel() { X = 100, Y = 200, Width = w, Height = h });
            CurrentPointSet.Add(new PointViewModel() { X = 200, Y = 200, Width = w, Height = h });
        }
    }
}
