using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal interface IShape
    {
        public double square();
        public double length();
        public IShape shift(Point2D a);
        public IShape rot(double phi);
        public IShape symAxis(int i);
        public bool cross(IShape i);
        public String toString();
    }
}
