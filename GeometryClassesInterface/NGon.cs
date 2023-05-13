using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class NGon: IShape
    {
        public int n;
        public Point2D[] p;
        public NGon(Point2D[] p) { this.p = p; n = p.Length; }
        public int getN() => n;
        public Point2D[] getP() => p;
        public void setP(Point2D[] p) { this.p = p; n = p.Length; }
        public void setP(Point2D p, int i) => this.p[i] = p;
        public double square()
        {
            if (n > 3)
            {
                Point2D[] a = p.Take(n - 1).ToArray();
                Point2D[] b = new Point2D[] { p[0], p[n - 2], p[n - 1] };
                return new NGon(a).square() + new TGon(b).square();
            }
            else return new TGon(p).square();
        }

        public double length()
        {
            double l = 0;
            Point2D prevp = p[0];
            foreach (Point2D point in p) { l += new Segment(prevp, point).length(); prevp = point; }
            l += new Segment(p[0], p[n - 1]).length();
            return l;
        }
        
        public IShape shift(Point2D a) { for (int i = 0; i < n; i++) p[i].add(a); return this; }
        public IShape rot(double phi) { for (int i = 0; i < n; i++) p[i].rot(phi); return this; }
        public IShape symAxis(int i) { for (int j = 0; j < n; j++) p[j].symAxis(i); return this; }
        public bool cross(IShape i)
        {
            Point2D prev = p[0];
            foreach (Point2D pt in p)
            {
                if (new Segment(prev, pt).cross(i)) return true;
                prev = pt;
            }
            if (new Segment(p[0], p[n - 1]).cross(i)) return true;
            return false;
        }

        public String toString()
        {
            StringBuilder sb = new StringBuilder(); sb.Append("Number of headpoints: " + n + "\nPoints: [ ");
            foreach (Point2D e in p) sb.Append(e.toString() + " ");
            sb.Append(" ]");
            return sb.ToString();
        }
    } 
}
