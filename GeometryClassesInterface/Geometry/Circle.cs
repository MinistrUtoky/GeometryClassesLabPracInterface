using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Circle: IShape
    {
        Point2D p;
        double r;
        public Circle(Point2D p, double r)
        {
            this.p = p;
            this.r = r;
        }
        public Point2D getP() => p;
        public double getR() => r;
        public void setP(Point2D p) => this.p = p;
        public void setR(double r) => this.r = r;
        public double square() => Math.PI * r * r;
        public double length() => 2 * Math.PI * r;
        public IShape shift(Point2D a)
        {
            Point newP = Point.add(a, p);
            p = new Point2D(newP.getX());
            return this;
        }
        public IShape rot(double phi) => this;

        public IShape symAxis(int i)
        {
            p.symAxis(i); return this;
        }

        public bool cross(IShape i)
        {
            if (i.GetType() == typeof(Circle))
            {
                if (Point2D.sub(getP(), (i as Circle).getP()).abs() <= getR() + (i as Circle).getR()
                    && Point2D.sub(getP(), (i as Circle).getP()).abs() > Math.Abs(getR() - (i as Circle).getR())) return true;
                else return false;
            }
            else return i.cross(this);            
        }
        public String toString() => "Circle: Circle(Center=" + p.toString() + ", Radius=" + r + ")";
    }
}
