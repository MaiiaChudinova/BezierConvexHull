using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
    public class MainViewModel : BaseViewModel
    {
        public List<PointSetViewModel> SamplePointSets;

        public RelayCommand NextSampleCommand;

        public RelayCommand GenerateRandomSampleCommand;

        public RelayCommand ShowHelpCommand;

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
       
    }
}
