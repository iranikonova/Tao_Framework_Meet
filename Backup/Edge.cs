using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao_Framework_Meet
{
    public class Edge
    {
        public _Point begin = new _Point();
        public _Point end = new _Point();

        public Edge() { }
        public Edge(_Point p0, _Point p1)
        { begin = p0; end = p1; }
    }
}
