using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Tao_Framework_Meet
{
    public class CubeEdge
    {
        public Vertex begin;
        public Vertex end;
        public Vertex middle;

        public bool IsPointed = false;

        public Lock_ lockState = new Lock_(false, true);

        public CubeEdge()
        {

            begin = new Vertex();
            end = new Vertex();
            middle = new Vertex();


        }

        public CubeEdge(CubeEdge copy)
        {
            this.begin = Vertex.getCopy(copy.begin);
            this.end = Vertex.getCopy(copy.end);
            this.middle = Vertex.getCopy(copy.middle);
            this.IsPointed = copy.IsPointed;
            this.lockState = new Lock_(copy.lockState);

        }

        public static CubeEdge getCopy(CubeEdge copy)
        {
            CubeEdge copied = new CubeEdge();
            copied.begin = Vertex.getCopy(copy.begin);
            copied.end = Vertex.getCopy(copy.end);
            copied.middle = Vertex.getCopy(copy.middle);
            copied.IsPointed = copy.IsPointed;
            copied.lockState = new Lock_(copy.lockState);

            return copied;
        }


        public bool Equals(CubeEdge e)
        {
            return ((begin.Equals(e.begin)) && (end.Equals(e.end)));
        }

        public void Set(ref CubeEdge e)
        {
            begin = e.begin;
            end = e.end;
            middle = e.middle;
        }

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

        public Edge[] eCubeEdge(double controlHeight)
        {
            Edge[] epolyg = new Edge[3];

            for (int i = 0; i < epolyg.Length; i++)
                epolyg[i] = new Edge();

            double xCoord = 0, yCoord = 0, zCoord = 0;

            Glu.gluProject(middle.x, middle.y, middle.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            _Point p1 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
            Glu.gluProject(begin.x, begin.y, begin.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            _Point p0 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
            epolyg[0] = new Edge(p0, p1);

            Glu.gluProject(end.x, end.y, end.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            p1 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
            Glu.gluProject(middle.x, middle.y, middle.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            p0 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
            epolyg[1] = new Edge(p0, p1);

            Glu.gluProject(end.x, end.y, end.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            p1 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
            Glu.gluProject(begin.x, begin.y, begin.z, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            p0 = new _Point(Math.Round(xCoord, 0), controlHeight - Math.Round(yCoord, 0));
            epolyg[2] = new Edge(p0, p1);


            return epolyg;
        }

        public void Draw()
        {
            if (IsPointed)
            {
                Gl.glLineWidth(2);
                Gl.glColor3f(1.0f, 0.0f, 1.0f);
            }
            else
            {
                Gl.glLineWidth(1);
                Gl.glColor3f(0.0f, 0.0f, 1.0f);
            }

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(begin.x, begin.y, begin.z);
            Gl.glVertex3d(middle.x, middle.y, middle.z);
            Gl.glVertex3d(middle.x, middle.y, middle.z);
            Gl.glVertex3d(end.x, end.y, end.z);
            Gl.glEnd();
        }

    }
}
