using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Trapeze : QGon
    {
        public Trapeze(Point2D[] p) : base(p) { }
        public double square() => base.square();   
    }
}
