using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Tao_Framework_Meet
{


    public class Vertex
    {
        public double x, y, z;
        public bool IsSelected = false;
        public bool IsPicked = false;
        public bool EnableInit = false;
        //public List<Cube> Parents = new List<Cube>();
        public List<Cube> Parents;
        public static Vertex getCopy(Vertex copy)
        {
            Vertex copied = new Vertex();
            copied.x = copy.x;
            copied.y = copy.y;
            copied.z = copy.z;

            copied.IsSelected = copy.IsSelected;
            copied.IsPicked = copy.IsPicked;
            copied.EnableInit = copy.EnableInit;

            /*copied.Parents.Clear();
            foreach (Cube c in copy.Parents)
            {
                Cube copyCube = new Cube(); copyCube = copyCube.getCopy(c);

                copyCube.Origin = copyCube.getCopy(c.Origin);
                copyCube.TransCube = copyCube.getCopy(c.TransCube);

                copyCube.Parts.Clear();

                foreach (Cube c1 in c.Parts)
                    copyCube.Parts.Add(copyCube.getCopy(c1));

                copied.Parents.Add(copyCube);
            }*/

            Pair<Vertex, List<Cube>> newItem = new Pair<Vertex, List<Cube>>();
            newItem.First = copied;
            newItem.Second = copy.Parents;
            GlobalVar.verParents.Add(newItem);

            return copied;

        }



        public Vertex()
        {

            Parents = new List<Cube>();

        }

        public void Set(double x1, double y1, double z1)
        {
            x = x1; y = y1; z = z1;
        }
        public void Draw()
        {

            Gl.glVertex3d(x, y, z);

        }
        public void Draw(double red, double green, double blue, float size)
        {
            Gl.glColor3d(red, green, blue);
            Gl.glEnable(Gl.GL_POINT_SMOOTH);
            Gl.glPointSize(size);

            Gl.glBegin(Gl.GL_POINTS);

            Gl.glVertex3d(x, y, z);

            Gl.glEnd();
        }
        public bool ApproximatelyEquals(double x1, double y1, double z1)
        {
            if ((Math.Abs(x1 - x) <= 0.2) && (Math.Abs(y - y1) <= 0.2) && (Math.Abs(z - z1) <= 0.2))
                return true;
            else
                return false;
        }
        public bool Equals(Vertex v)
        {
            if ((x == v.x) && (y == v.y) && (z == v.z))
                return true;
            else
                return false;
        }
    }
}
