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
        public new double square() => base.square();
        
        public new String toString()
        {
            StringBuilder sb = new StringBuilder(); sb.Append("Trapeze: Trapeze(super=NGon([");
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
