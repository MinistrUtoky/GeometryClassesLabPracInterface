using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal abstract class OpenFigure: IShape
    {
        public double square() => 0;
        public abstract double length();
        public abstract IShape shift(Point2D a);
        public abstract IShape rot(double phi);
        public abstract IShape symAxis(int i);
        public abstract bool cross(IShape i);
        public abstract String toString();
    }
}
