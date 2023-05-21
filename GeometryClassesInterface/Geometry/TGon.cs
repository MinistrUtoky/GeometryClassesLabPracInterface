using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class TGon : NGon
    {
        public TGon(Point2D[] p) : base(p) { }
        public new double square()
        {
            double ab = Point2D.sub(p[1], p[0]).abs(), 
                bc = Point2D.sub(p[2], p[1]).abs(),
                ca = Point2D.sub(p[0], p[2]).abs(),
                pp = (ab + bc + ca)/2;
            return Math.Sqrt(pp * (pp - ab)*(pp - bc)*(pp - ca));
        }

        public new String toString()
        {
            StringBuilder sb = new StringBuilder(); sb.Append("Triangle: TGon(super=NGon([");
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
