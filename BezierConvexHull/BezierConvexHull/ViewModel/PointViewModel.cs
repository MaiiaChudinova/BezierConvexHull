﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierConvexHull
{
    public class PointViewModel : BaseViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsHullPoint { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
