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
        public new double square() {
            Point2D[] a = new Point2D[] { p[0], p[1], p[2] };
            Point2D[] b = new Point2D[] { p[2], p[3], p[0] };
            return new TGon(a).square() + new TGon(b).square();
        }

        public new String toString()
        {
            StringBuilder sb = new StringBuilder(); sb.Append("Quadrilateral: QGon(super=NGon([");
            foreach (Point2D e in p)
                if (e != p[p.Count() - 1])
                    sb.Append(e.toString() + ", ");
                else
                    sb.Append(e.toString());
            sb.Append("]))");
            return sb.ToString();
        }
    }
}
