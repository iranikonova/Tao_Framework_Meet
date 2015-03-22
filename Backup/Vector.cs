using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao_Framework_Meet
{
    class Vector
    {
        public double x, y, z;
        public Vector() { x = 0; y = 0; z = 0; }
        public Vector(double x1, double y1, double z1) { x = x1; y = y1; z = z1; }
        public void Set(double x1, double y1, double z1) { x = x1; y = y1; z = z1; }
        public Vector Sum(Vector v)
        {
            Vector res = new Vector(this.x, this.y, this.z);
            res.x += v.x; res.y += v.y; res.z += v.z;
            return res;
        }
        public Vector ScalarMult(double a)
        {
            Vector res = new Vector(this.x, this.y, this.z);
            res.x *= a; res.y *= a; res.z *= a;
            return res;
        }
        public double Product(Vector v)
        {
            return v.x * x + v.y * y + v.z * z;
        }
    }
}
