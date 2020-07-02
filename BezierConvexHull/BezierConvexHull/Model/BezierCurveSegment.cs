using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull.Model
{
    public class BezierCurveSegment
    {
        public System.Windows.Point StartPoint { get; set; }
        public System.Windows.Point EndPoint { get; set; }
        public System.Windows.Point FirstControlPoint { get; set; }
        public System.Windows.Point SecondControlPoint { get; set; }
    }
}
