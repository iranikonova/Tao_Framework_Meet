using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao_Framework_Meet
{
    public class _Point
    {
        public double x=0, y=0;

        public _Point() { x = 0; y = 0; }
        public _Point(double x1, double y1) { x = x1; y = y1; }

        public static _Point operator +(_Point p1, _Point p2)
        {
            return new _Point(p1.x+p2.x,p1.y+p2.y);
        }
        public static _Point operator -(_Point p1, _Point p2)
        {
            return new _Point(p1.x - p2.x, p1.y - p2.y);
        }

        
 

        public double Length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public string Classify(_Point p0, _Point p1)
        {
            _Point p2 = new _Point(this.x, this.y);
            _Point a = p1 - p0;
            _Point b = p2 - p0;
            double sa = a.x * b.y - b.x * a.y;
            if (sa > 0)
                return "LEFT";
            if (sa < 0)
                return "RIGHT";
            if ((a.x * b.x < 0) || (a.y * b.y < 0))
                return "BEHIND";
            if (a.Length() < b.Length())
                return "BEYOND";
            if ((p2.x == p0.x) && (p2.y == p0.y))
                return "BEGIN";
            if ((p2.x == p1.x) && (p2.y == p1.y))
                return "END";
            return "BETWEEN";
        }

        public string Classify(Edge e)
        {
            return Classify(e.begin, e.end);
        }
    }
}
