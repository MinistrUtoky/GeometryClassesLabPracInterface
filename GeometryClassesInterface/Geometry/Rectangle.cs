using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Rectangle: QGon
    {
        public Rectangle(Point2D[] p) : base(p) { }
        public double square() => Point2D.sub(p[1], p[0]).abs() * Point2D.sub(p[2], p[1]).abs();
    }
}
