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

        public int PointsNumber { get; set; } = 15;

        public int MinX { get; set; }

        public int MaxX { get; set; }

        public int MinY { get; set; }

        public int MaxY { get; set; }   

        public PointSetViewModel(List<PointViewModel> points)
        {
            
            PointsNumber = points.Count;
            MinX = points.Select(p => p.X).Min();
            MaxX = points.Select(p => p.X).Max();
            MinY = points.Select(p => p.Y).Min();
            MaxY = points.Select(p => p.Y).Max();
            PointSet = new ObservableCollection<PointViewModel>();
            foreach (PointViewModel p in points)
                PointSet.Add(p);
            
        }
    }
}
