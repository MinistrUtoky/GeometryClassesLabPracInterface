using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Point3D : Point
    {
        public Point3D() : base(3) { }

        public Point3D(double[] x) : base(3, x) { }

        public static Point3D cross_prod(Point3D p1, Point3D p2) => new Point3D(new double[3]
            {p1.x[1] * p2.x[2] - p1.x[2] * p2.x[1],
            p1.x[2] * p2.x[0] - p1.x[0] * p2.x[2],
            p1.x[0] * p2.x[1] - p1.x[1] * p2.x[0]}
        );

        public Point3D cross_prod(Point3D p)
        {
            Point3D newPoint = cross_prod(this, p);
            x = newPoint.x;
            dim = newPoint.dim;
            return newPoint;
        } 

        // (a*[b x c])
        public static double mix_prod(Point3D p1, Point3D p2, Point3D p3) => mult(p1, cross_prod(p2, p3));

        public double mix_prod(Point3D p1, Point3D p2) => mix_prod(this, p1, p2);

    }
}
