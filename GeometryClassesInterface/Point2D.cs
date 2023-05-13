using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Point2D : Point
    {
        public Point2D() : base(2) { }

        public Point2D(double[] x): base(2,x) {  }

        public static Point2D rot(Point2D p, double phi) => new Point2D(new double[2] { p.x[0] * Math.Cos(phi) - p.x[1] * Math.Sin(phi), p.x[0] * Math.Sin(phi) + p.x[1] * Math.Cos(phi)});

        public Point2D rot(double phi)
        {
            Point2D newPoint = rot(this, phi); 
            x = newPoint.x;
            dim = newPoint.dim;
            return newPoint;
        }
    }
}
