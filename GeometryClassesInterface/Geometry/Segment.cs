using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Segment: OpenFigure
    {
        private Point2D start;
        private Point2D finish;

        public Segment(Point2D s, Point2D f)
        {
            start = s;
            finish = f;
        }
        public Point2D getStart() => start;
        public Point2D setStart(Point2D a) => start = a;
        public Point2D getFinish() => finish;
        public void setFinish(Point2D a) => finish = a;
        public override double length() => Point2D.sub(finish, start).abs();
        public override Segment shift(Point2D a) { start.add(a); finish.add(a); return this; }
        public override Segment rot(double phi) { start.rot(phi); finish.rot(phi); return this; }
        public override Segment symAxis(int i) { start.symAxis(i); finish.symAxis(i); return this; }
        
        public override bool cross(IShape i) { 
            if (i.GetType() == typeof(Segment)) {
                return counterclockwise(this.start, (i as Segment).start, (i as Segment).finish)
                    != counterclockwise(this.finish, (i as Segment).start, (i as Segment).finish) &&
                    counterclockwise(this.start, this.finish, (i as Segment).start) !=
                    counterclockwise(this.start, this.finish, (i as Segment).finish);
            }
            else if (i.GetType() == typeof(Circle)) {
                return ((Point2D.sub(start, (i as Circle).getP())).abs() < (i as Circle).getR() 
                            || (Point2D.sub(finish, (i as Circle).getP())).abs() < (i as Circle).getR()) 
                        && !((Point2D.sub(start, (i as Circle).getP())).abs() < (i as Circle).getR()
                            && (Point2D.sub(finish, (i as Circle).getP())).abs() < (i as Circle).getR());
            }
            else
            {
                Point2D[] pts = null;
                if (i.GetType() == typeof(Polyline)) pts = (i as Polyline).getP();
                else if (i.GetType() == typeof(NGon)) pts = (i as NGon).getP();
                else if (i.GetType() == typeof(TGon)) pts = (i as TGon).getP();
                else if (i.GetType() == typeof(QGon)) pts = (i as QGon).getP();
                else if (i.GetType() == typeof(Rectangle)) pts = (i as Rectangle).getP();
                else if (i.GetType() == typeof(Trapeze)) pts = (i as Trapeze).getP();
                if (pts == null) throw new Exception("No points have been passed down");
                Point2D prev = pts[0];
                foreach (Point2D pt in pts)
                {
                    if (new Segment(prev, pt).cross(this)) return true;
                    prev = pt;
                }                
                if ((i.GetType().IsSubclassOf(typeof(NGon)) || i.GetType() == typeof(NGon)) && new Segment(pts[0], pts[pts.Length - 1]).cross(this)) return true;
                return false;
            }
            return false;
        }

        public bool counterclockwise(Point2D a, Point2D b, Point2D c) => (c.getX()[1] - a.getX()[1]) * (b.getX()[0] - a.getX()[0])
                                                                         > (b.getX()[1] - a.getX()[1]) * (c.getX()[0] - a.getX()[0]);
        public override String toString() => "start: " + start.toString() + ", finish:" + finish.toString();
    }
}
