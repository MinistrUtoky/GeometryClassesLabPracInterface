using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Polyline: OpenFigure
    {
        int n;
        Point2D[] p;
        public Polyline(Point2D[] p) { n = p.Length; this.p = p; }
        public int getN() => n;
        public Point2D[] getP() => p;
        public Point2D getP(int i) => p[i];
        public void setP(Point2D[] p) { this.p = p; n = p.Length; }
        public void setP(Point2D p, int i) => this.p[i] = p;
        public override double length() {
            double l = 0;
            Point2D prevp = p[0];
            foreach (Point2D point in p) { l += new Segment(prevp, point).length(); prevp = point; }
            return l;
        }
        public override Polyline shift(Point2D a) { foreach (Point2D e in p) e.add(a); return this; }
        public override Polyline rot(double phi) { foreach (Point2D e in p) e.rot(phi); return this; }
        public override Polyline symAxis(int i) { foreach (Point2D e in p) e.symAxis(i); return this; }
        public override bool cross(IShape i) {
            Point2D prev = p[0];
            foreach (Point2D pt in p)
            {
                if (new Segment(prev, pt).cross(i)) return true;
                prev = pt;
            }
            return false;
        }
        public override String toString() { 
            StringBuilder sb = new StringBuilder(); sb.Append("Polyline: Polyline([");
            foreach (Point2D e in p)
                if (e != p[p.Count() - 1])
                    sb.Append(e.toString() + ", ");
                else
                    sb.Append(e.toString());
            sb.Append("])");
            return sb.ToString();
        }
    }
}
