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
        public new double square() => Point2D.sub(p[1], p[0]).abs() * Point2D.sub(p[2], p[1]).abs();
        public new String toString()
        {
            StringBuilder sb = new StringBuilder(); sb.Append("Rectangle: Rectangle(super=NGon([");
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
