using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
    public class PointSetViewModel : BaseViewModel
    {
        public ObservableCollection<PointViewModel> PointSet { get; set; }

        public int PointsNumber { get; set; }

        public int MinX { get; set; }

        public int MaxX { get; set; }

        public int MinY { get; set; }

        public int MaxY { get; set; }   

        public PointSetViewModel()
        {
            PointSet = new ObservableCollection<PointViewModel>();
            
        }
    }
}
