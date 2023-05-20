using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class QGon : NGon
    {
        public QGon(Point2D[] p) : base(p) { }
        public double square() {
            Point2D[] a = new Point2D[] { p[0], p[1], p[2] };
            Point2D[] b = new Point2D[] { p[2], p[3], p[0] };
            return new TGon(a).square() + new TGon(b).square();
        } 
    }
}
