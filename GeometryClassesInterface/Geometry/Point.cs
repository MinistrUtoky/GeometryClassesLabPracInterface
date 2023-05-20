using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryClasses
{
    internal class Point
    {
        public int dim; public double[] x;
        public Point(int dim)
        {
            this.dim = dim; x = new double[dim];
        }
        public Point(int dim, double[] x)
        {
            this.dim = dim; this.x = x;
        }
        public int getDim() => dim;

        public double[] getX() => this.x;

        public void setX(double[] x) => this.x = x;

        public void setX(double x, int i) => this.x[i] = x;

        public double abs() { 
            double sum = 0; 
            foreach (double i in x) sum += i * i; 
            return Math.Sqrt(sum); 
        }

        public static Point add(Point a, Point b) {
            int newDim = Convert.ToInt32(Math.Max(a.getDim(), b.getDim()));
            double[] newX = new double[newDim];
            for (int i = 0; i < newDim; i++) { 
                if (i < a.dim) newX[i] += a.x[i];
                if (i < b.dim) newX[i] += b.x[i];
            }
            return new Point(newDim, newX);
        }

        public Point add(Point b) { 
            Point newPoint = add(this, b);
            x = newPoint.x;
            dim = newPoint.dim;
            return newPoint;
        }

        // a-b
        public static Point sub(Point a, Point b)
        {
            int newDim = Convert.ToInt32(Math.Max(a.getDim(), b.getDim()));
            double[] newX = new double[newDim];
            for (int i = 0; i < newDim; i++)
            {
                if (i < a.dim) newX[i] += a.x[i];
                if (i < b.dim) newX[i] -= b.x[i];
            };
            return new Point(newDim, newX);
        }

        public Point sub(Point b)
        {            
            Point newPoint = sub(this, b);
            x = newPoint.x;
            dim = newPoint.dim;
            return newPoint;
        }

        public static Point mult(Point a, double r)
        {
            double[] newX = new double[a.dim];
            for (int i = 0; i < a.dim; i++) newX[i] = r * a.x[i];
            return new Point(a.dim, newX);
        }

        public Point mult(double r)
        {
            Point newPoint = mult(this, r);
            x = newPoint.x;
            dim = newPoint.dim;
            return newPoint;
        }

        public static double mult(Point a, Point b) {
            double result = 0;
            for (int i = 0; i < Math.Min(a.x.Length, b.x.Length); i++) result += a.x[i] * b.x[i];
            return result;
        }

        public double mult(Point b) => mult(this, b);

        public static Point symAxis(Point a, int i)
        {
            double[] newX = new double[a.dim];
            for (int j = 0; j < a.dim; j++)
            {
                newX[j] = -a.x[j];
            }
            newX[i] = a.x[i];
            return new Point(a.dim, newX);
        }

        public Point symAxis(int i)
        {
            Point newPoint = symAxis(this, i);
            x = newPoint.x;
            dim = newPoint.dim;
            return newPoint;
        }

        public String toString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("( ");
            foreach (double coord in x)
                sb.Append(coord.ToString() + " ");
            sb.Append(")");
            return sb.ToString();
        }
    }
}
