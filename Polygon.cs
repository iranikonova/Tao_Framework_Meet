using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Tao_Framework_Meet
{
    static class GlobalVar
    {
        public static int PolygondrawMode = 0;
        public static int cubeDrawMode = 0;

        public static Polygon polygonToScale = new Polygon();

        public static List<Pair<Vertex, List<Cube>>> verParents = new List<Pair<Vertex, List<Cube>>>();

        public static List<Pair<Polygon, Cube>> faceParent = new List<Pair<Polygon, Cube>>();

        public static List<Pair<Cube, Cube>> cubeOrigin = new List<Pair<Cube, Cube>>();
        public static List<Pair<Cube, Cube>> cubeTrans = new List<Pair<Cube, Cube>>();

        public static List<Pair<Cube, List<Cube>>> cubeParts = new List<Pair<Cube, List<Cube>>>();

        public static int scaleType = 1;
    }

    static class GlobalVar1
    {
        public static int PolygondrawMode = 0;
        public static int cubeDrawMode = 0;

        public static Polygon polygonToScale = new Polygon();

        public static List<Pair<Vertex, List<Cube>>> verParents = new List<Pair<Vertex, List<Cube>>>();

        public static List<Pair<Polygon, Cube>> faceParent = new List<Pair<Polygon, Cube>>();

        public static List<Pair<Cube, Cube>> cubeOrigin = new List<Pair<Cube, Cube>>();
        public static List<Pair<Cube, Cube>> cubeTrans = new List<Pair<Cube, Cube>>();

        public static List<Pair<Cube, List<Cube>>> cubeParts = new List<Pair<Cube, List<Cube>>>();

        public static int scaleType = 1;
    }

    public class Polygon
    {
        public Vertex[] vertices;
        //public Polygon() { }
        public Polygon(Vertex[] v) { vertices = v; }
        public void SetPolygon(Vertex[] v) { vertices = v; }
        public bool IsPointed = false;
        public string maxState = "";
        public Cube Parent;

        public Polygon() { }

        public static Polygon getCopy(Polygon copy)
        {
            Polygon copied = new Polygon();
            copied.vertices = new Vertex[copy.vertices.Length];

            for (int i = 0; i < copy.vertices.Length; i++)
                copied.vertices[i] = Vertex.getCopy(copy.vertices[i]);

            copied.IsPointed = copy.IsPointed;

            copied.lockState = new Lock_(copy.lockState);

            copied.maxState = copy.maxState;

            Pair<Polygon, Cube> newItem = new Pair<Polygon, Cube>();
            newItem.First = copied;
            newItem.Second = copy.Parent;

            GlobalVar.faceParent.Add(newItem);

            /*copied.Parent = new Cube(); copied.Parent = copied.Parent.getCopy(copy.Parent);

            copied.Parent.Origin = copied.Parent.getCopy(copy.Parent.Origin);
            copied.Parent.TransCube = copied.Parent.getCopy(copy.Parent.TransCube);

            copied.Parent.Parts.Clear();

            foreach (Cube c1 in copy.Parent.Parts)
                copied.Parent.Parts.Add(copied.Parent.getCopy(c1));*/
            return copied;

        }
        public Polygon(Polygon copy)
        {
            /*  this.vertices = new Vertex[copy.vertices.Length];

              for (int i = 0; i < copy.vertices.Length;i++ )
                  this.vertices[i] = Vertex.getCopy(copy.vertices[i]);

              this.IsPointed=copy.IsPointed ;

              this.lockState = new Lock_(copy.lockState);

              this.maxState=copy.maxState ;

              this.Parent = new Cube(); this.Parent = this.Parent.getCopy(copy.Parent);

              this.Parent.Origin = this.Parent.getCopy(copy.Parent.Origin);
              this.Parent.TransCube = this.Parent.getCopy(copy.Parent.TransCube);

              this.Parent.Parts.Clear();

              foreach (Cube c1 in copy.Parent.Parts)
                  this.Parent.Parts.Add(this.Parent.getCopy(c1));

           
              */
        }

        public Lock_ lockState = new Lock_(false, true);

        int[] GetViewport()
        {
            int[] viewport = new int[4];
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, viewport);
            return viewport;
        }

        double[] GetModelview()
        {
            double[] modelview = new double[16];
            Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, modelview);
            return modelview;
        }

        double[] GetProjection()
        {
            double[] projection = new double[16];
            Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, projection);
            return projection;

        }

        public void Draw(bool IsParentSelected)
        {

            if (IsParentSelected)
            {
                Gl.glLineWidth(3);
                Gl.glColor3f(0.0f, 1.0f, 0.0f);
            }
            else
            {

                if (IsPointed)
                {
                    Gl.glLineWidth(3);
                    Gl.glColor3f(1.0f, 0.0f, 1.0f);
                }
                else
                {
                    Gl.glLineWidth(1);
                    Gl.glColor3f(0.0f, 0.0f, 1.0f);
                }
            }

            Gl.glBegin(Gl.GL_LINE_LOOP);


            foreach (Vertex v in vertices)
            {
                Gl.glVertex3d(v.x, v.y, v.z);
            }


            Gl.glEnd();
            Gl.glLineWidth(1);



        }

        public Edge[] ePolygon(double controlHeight)
        {
            Edge[] epolyg = new Edge[vertices.Length - 1];

            for (int i = 0; i < epolyg.Length; i++)
                epolyg[i] = new Edge();

            double xCoord = 0, yCoord = 0, zCoord = 0;

            for (int i = 0; i < epolyg.Length; i++)
            {
                Glu.gluProject(vertices[i + 1].x, vertices[i + 1].y, vertices[i + 1].z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
                _Point p1 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
                Glu.gluProject(vertices[i].x, vertices[i].y, vertices[i].z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
                _Point p0 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
                epolyg[i] = new Edge(p0, p1);

            }
            return epolyg;
        }

        public Polygon Depth(out double depth)
        {
            double max = 0;

            double xCoord = 0, yCoord = 0, zCoord = 0;
            foreach (Vertex v in vertices)
            {
                Glu.gluProject(v.x, v.y, v.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
                if (zCoord >= max)
                    max = zCoord;

            }
            depth = max;
            return this;
        }

        public double MaxX()
        {
            double max = vertices[0].x;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if ((i % 2 == 0) && (vertices[i].x >= max))
                    max = vertices[i].x;

            }
            return max;
        }

        public double MinX()
        {
            double min = vertices[0].x;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if ((i % 2 == 0) && (vertices[i].x <= min))
                    min = vertices[i].x;

            }
            return min;
        }

        public double AvgX()
        {
            double avg = 0;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if (i % 2 == 0)
                    avg += vertices[i].x;

            }
            return (avg * 1.0) / 4;
        }

        public double MaxY()
        {
            double max = vertices[0].y;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if ((i % 2 == 0) && (vertices[i].y >= max))
                    max = vertices[i].y;

            }
            return max;
        }

        public double MinY()
        {
            double min = vertices[0].y;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if ((i % 2 == 0) && (vertices[i].y <= min))
                    min = vertices[i].y;

            }
            return min;
        }
        public double AvgY()
        {
            double avg = 0;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if (i % 2 == 0)
                    avg += vertices[i].y;

            }
            return (avg * 1.0) / 4;
        }
        public double MaxZ()
        {
            double max = vertices[0].z;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if ((i % 2 == 0) && (vertices[i].z >= max))
                    max = vertices[i].z;

            }
            return max;
        }

        public double MinZ()
        {
            double min = vertices[0].z;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if ((i % 2 == 0) && (vertices[i].z <= min))
                    min = vertices[i].z;

            }
            return min;
        }

        public double AvgZ()
        {
            double avg = 0;
            for (int i = 0; i < vertices.Length - 1; i++)
            {
                if (i % 2 == 0)
                    avg += vertices[i].z;

            }
            return (avg * 1.0) / 4;
        }

        public bool Equals(Polygon p)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if ((vertices[i].x != p.vertices[i].x) || (vertices[i].y != p.vertices[i].y) || (vertices[i].z != p.vertices[i].z))
                    return false;
            }
            return true;
        }
        public string ToString()
        {
            string str = "[ ";
            foreach (Vertex v in vertices)
                str += (" (" + v.x + " ," + v.y + " ," + v.z + " ) ; ");
            str += " ]";
            return str;
        }

    }
}
