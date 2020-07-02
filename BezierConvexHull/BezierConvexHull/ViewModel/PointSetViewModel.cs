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

        public PointSetViewModel(List<PointViewModel> points)
        {      
            PointSet = new ObservableCollection<PointViewModel>();
            foreach (PointViewModel p in points)
                PointSet.Add(p);         
        }
    }
}
