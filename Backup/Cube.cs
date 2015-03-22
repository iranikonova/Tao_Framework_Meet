using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace Tao_Framework_Meet
{
    public class Cube
    {
        public string primitiveName = "";
        public bool EnabledTransform = true;
        //public Vertex [] vertices = new Vertex [20];

        //public Vertex [] axes = new Vertex [4];
        //public Polygon[] faces = new Polygon[6];
        public Vertex[] vertices;

        public Vertex[] axes;
        public Polygon[] faces;

        public Cube Origin;
        public Cube TransCube;
        //public Translation translation = new Translation();
        public Translation translation;
        //public List<Cube> Parts = new List<Cube>();
        public List<Cube> Parts;
        public bool IsSelected = false;
        public int nx = 0, ny = 0, nz = 0;
        //public CubeEdge[] edges = new CubeEdge[12];
        public CubeEdge[] edges;

        public static Cube getCopy(Cube copy)
        {
            Cube copied = new Cube();
            try
            {
                copied.primitiveName = copy.primitiveName;
            }
            catch { }

            for (int i = 0; i < 20; i++)
                copied.vertices[i] = Vertex.getCopy(copy.vertices[i]);

            copied.EnabledTransform = copy.EnabledTransform;

            for (int i = 0; i < 4; i++)
                copied.axes[i] = Vertex.getCopy(copy.axes[i]);

            for (int i = 0; i < 6; i++)
            {
                copied.faces[i] = Polygon.getCopy(copy.faces[i]);
            }

            Pair<Cube, Cube> newItem1 = new Pair<Cube, Cube>();
            newItem1.First = copied;
            newItem1.Second = copy.Origin;
            GlobalVar.cubeOrigin.Add(newItem1);

            Pair<Cube, Cube> newItem2 = new Pair<Cube, Cube>();
            newItem2.First = copied;
            newItem2.Second = copy.TransCube;
            GlobalVar.cubeTrans.Add(newItem2);


            //this.Origin = new Cube(copy.Origin);
            //this.TransCube = new Cube(copy.TransCube);

            copied.translation = new Translation(copy.translation);

            /*this.Parts = new List<Cube>();

            foreach (Cube c in copy.Parts)
            {
                Cube newItem = new Cube(c);
                this.Parts.Add(newItem);
            }*/

            Pair<Cube, List<Cube>> newItem3 = new Pair<Cube, List<Cube>>();
            newItem3.First = copied;
            newItem3.Second = copy.Parts;
            GlobalVar.cubeParts.Add(newItem3);

            copied.IsSelected = copy.IsSelected;

            copied.nx = copy.nx;
            copied.ny = copy.ny;
            copied.nz = copy.nz;

            //this.edges = new CubeEdge[copy.edges.Length];
            for (int i = 0; i < 12; i++)
            {
                copied.edges[i] = CubeEdge.getCopy(copy.edges[i]);
            }

            return copied;

        }
        public Cube(Cube copy)
        {
            this.primitiveName = copy.primitiveName;

            for (int i = 0; i < 20; i++)
                this.vertices[i] = Vertex.getCopy(copy.vertices[i]);

            this.EnabledTransform = copy.EnabledTransform;

            for (int i = 0; i < 4; i++)
                this.axes[i] = Vertex.getCopy(copy.axes[i]);

            for (int i = 0; i < 6; i++)
            {
                this.faces[i] = new Polygon(copy.faces[i]);
            }

            this.Origin = new Cube(copy.Origin);
            this.TransCube = new Cube(copy.TransCube);
            this.translation = new Translation(copy.translation);

            this.Parts = new List<Cube>();

            foreach (Cube c in copy.Parts)
            {
                Cube newItem = new Cube(c);
                this.Parts.Add(newItem);
            }

            this.IsSelected = copy.IsSelected;

            this.nx = copy.nx;
            this.ny = copy.ny;
            this.nz = copy.nz;

            //this.edges = new CubeEdge[copy.edges.Length];
            for (int i = 0; i < 12; i++)
            {
                this.edges[i] = new CubeEdge(copy.edges[i]);
            }



        }


        public void VertexToEdges()
        {

            vertices[0] = edges[0].begin;
            vertices[4] = edges[0].end;
            vertices[12] = edges[0].middle;

            vertices[3] = edges[1].begin;
            vertices[7] = edges[1].end;
            vertices[15] = edges[1].middle;

            vertices[2] = edges[2].begin;
            vertices[6] = edges[2].end;
            vertices[14] = edges[2].middle;

            vertices[1] = edges[3].begin;
            vertices[5] = edges[3].end;
            vertices[13] = edges[3].middle;

            vertices[4] = edges[4].begin;
            vertices[7] = edges[4].end;
            vertices[19] = edges[4].middle;

            vertices[7] = edges[5].begin;
            vertices[6] = edges[5].end;
            vertices[18] = edges[5].middle;

            vertices[6] = edges[6].begin;
            vertices[5] = edges[6].end;
            vertices[17] = edges[6].middle;

            vertices[5] = edges[7].begin;
            vertices[4] = edges[7].end;
            vertices[16] = edges[7].middle;

            vertices[0] = edges[8].begin;
            vertices[3] = edges[8].end;
            vertices[11] = edges[8].middle;

            vertices[3] = edges[9].begin;
            vertices[2] = edges[9].end;
            vertices[10] = edges[9].middle;

            vertices[2] = edges[10].begin;
            vertices[1] = edges[10].end;
            vertices[9] = edges[10].middle;

            vertices[1] = edges[11].begin;
            vertices[0] = edges[11].end;
            vertices[8] = edges[11].middle;

        }

        public void InitializeEdges()
        {
            edges[0] = new CubeEdge();
            edges[0].begin = vertices[0];
            edges[0].end = vertices[4];
            edges[0].middle = vertices[12];

            edges[1] = new CubeEdge();
            edges[1].begin = vertices[3];
            edges[1].end = vertices[7];
            edges[1].middle = vertices[15];

            edges[2] = new CubeEdge();
            edges[2].begin = vertices[2];
            edges[2].end = vertices[6];
            edges[2].middle = vertices[14];

            edges[3] = new CubeEdge();
            edges[3].begin = vertices[1];
            edges[3].end = vertices[5];
            edges[3].middle = vertices[13];

            edges[4] = new CubeEdge();
            edges[4].begin = vertices[4];
            edges[4].end = vertices[7];
            edges[4].middle = vertices[19];

            edges[5] = new CubeEdge();
            edges[5].begin = vertices[7];
            edges[5].end = vertices[6];
            edges[5].middle = vertices[18];

            edges[6] = new CubeEdge();
            edges[6].begin = vertices[6];
            edges[6].end = vertices[5];
            edges[6].middle = vertices[17];

            edges[7] = new CubeEdge();
            edges[7].begin = vertices[5];
            edges[7].end = vertices[4];
            edges[7].middle = vertices[16];

            edges[8] = new CubeEdge();
            edges[8].begin = vertices[0];
            edges[8].end = vertices[3];
            edges[8].middle = vertices[11];

            edges[9] = new CubeEdge();
            edges[9].begin = vertices[3];
            edges[9].end = vertices[2];
            edges[9].middle = vertices[10];

            edges[10] = new CubeEdge();
            edges[10].begin = vertices[2];
            edges[10].end = vertices[1];
            edges[10].middle = vertices[9];

            edges[11] = new CubeEdge();
            edges[11].begin = vertices[1];
            edges[11].end = vertices[0];
            edges[11].middle = vertices[8];

        }

        public Cube()
        {
            Parts = new List<Cube>();
            translation = new Translation();
            vertices = new Vertex[20];

            axes = new Vertex[4];
            faces = new Polygon[6];
            edges = new CubeEdge[12];


            for (int i = 0; i < 20; i++)
            {
                vertices[i] = new Vertex();
                try
                {
                    vertices[i].Parents.Add(this);
                }
                catch
                {
                    //vertices[i].pa
                }
            }
            //1 2 3 4
            vertices[0].x = -1; vertices[0].y = -1; vertices[0].z = 1;
            vertices[1].x = 1; vertices[1].y = -1; vertices[1].z = 1;
            vertices[2].x = 1; vertices[2].y = -1; vertices[2].z = -1;
            vertices[3].x = -1; vertices[3].y = -1; vertices[3].z = -1;
            //5 6 7 8
            vertices[4].x = -1; vertices[4].y = 1; vertices[4].z = 1;
            vertices[5].x = 1; vertices[5].y = 1; vertices[5].z = 1;
            vertices[6].x = 1; vertices[6].y = 1; vertices[6].z = -1;
            vertices[7].x = -1; vertices[7].y = 1; vertices[7].z = -1;

            MiddleVertices();

            for (int i = 0; i < 4; i++)
                axes[i] = new Vertex();

            axes[0].x = 0; axes[0].y = 0; axes[0].z = 0;//axes[0, j] = center
            axes[1].x = 2; axes[1].y = 0; axes[1].z = 0;//axes[1, j] = xAxes
            axes[2].x = 0; axes[2].y = 2; axes[2].z = 0;//axes[2, j] = yAxes
            axes[3].x = 0; axes[3].y = 0; axes[3].z = 2;//axes[3, j] = zAxes

            for (int i = 0; i < 6; i++)
            {
                faces[i] = new Polygon();
                faces[i].Parent = this;
                faces[i].vertices = new Vertex[9];
            }

            UpdateFace();
            InitializeEdges();
        }
        public void UpdateFace()
        {
            /*Vertex[] temp1 = { vertices[0], vertices[8], vertices[1], vertices[9], vertices[2], vertices[10], vertices[3], vertices[11], vertices[0] };
            faces[0].vertices = temp1;*/
            faces[0].vertices[0] = vertices[0];
            faces[0].vertices[1] = vertices[8];
            faces[0].vertices[2] = vertices[1];
            faces[0].vertices[3] = vertices[9];
            faces[0].vertices[4] = vertices[2];
            faces[0].vertices[5] = vertices[10];
            faces[0].vertices[6] = vertices[3];
            faces[0].vertices[7] = vertices[11];
            faces[0].vertices[8] = vertices[0];
            faces[0].maxState = "minY";

            //Vertex[] temp2 = { vertices[4], vertices[16], vertices[5], vertices[17], vertices[6], vertices[18], vertices[7], vertices[19], vertices[4] };
            //faces[1].vertices = temp2;
            faces[1].vertices[0] = vertices[4];
            faces[1].vertices[1] = vertices[16];
            faces[1].vertices[2] = vertices[5];
            faces[1].vertices[3] = vertices[17];
            faces[1].vertices[4] = vertices[6];
            faces[1].vertices[5] = vertices[18];
            faces[1].vertices[6] = vertices[7];
            faces[1].vertices[7] = vertices[19];
            faces[1].vertices[8] = vertices[4];
            faces[1].maxState = "maxY";

            //Vertex[] temp3 = { vertices[0], vertices[8], vertices[1], vertices[13], vertices[5], vertices[16], vertices[4], vertices[12], vertices[0] };
            // faces[2].vertices = temp3;
            faces[2].vertices[0] = vertices[0];
            faces[2].vertices[1] = vertices[8];
            faces[2].vertices[2] = vertices[1];
            faces[2].vertices[3] = vertices[13];
            faces[2].vertices[4] = vertices[5];
            faces[2].vertices[5] = vertices[16];
            faces[2].vertices[6] = vertices[4];
            faces[2].vertices[7] = vertices[12];
            faces[2].vertices[8] = vertices[0];
            faces[2].maxState = "maxZ";

            //Vertex[] temp4 = { vertices[1], vertices[9], vertices[2], vertices[14], vertices[6], vertices[17], vertices[5], vertices[13], vertices[1] };
            //faces[3].vertices = temp4;
            faces[3].vertices[0] = vertices[1];
            faces[3].vertices[1] = vertices[9];
            faces[3].vertices[2] = vertices[2];
            faces[3].vertices[3] = vertices[14];
            faces[3].vertices[4] = vertices[6];
            faces[3].vertices[5] = vertices[17];
            faces[3].vertices[6] = vertices[5];
            faces[3].vertices[7] = vertices[13];
            faces[3].vertices[8] = vertices[1];
            faces[3].maxState = "maxX";

            // Vertex[] temp5 = { vertices[2], vertices[14], vertices[6], vertices[18], vertices[7], vertices[15], vertices[3], vertices[10], vertices[2] };
            // faces[4].vertices = temp5;
            faces[4].vertices[0] = vertices[3];
            faces[4].vertices[1] = vertices[10];
            faces[4].vertices[2] = vertices[2];
            faces[4].vertices[3] = vertices[14];
            faces[4].vertices[4] = vertices[6];
            faces[4].vertices[5] = vertices[18];
            faces[4].vertices[6] = vertices[7];
            faces[4].vertices[7] = vertices[15];
            faces[4].vertices[8] = vertices[3];
            faces[4].maxState = "minZ";

            // Vertex[] temp6 = { vertices[0], vertices[11], vertices[3], vertices[15], vertices[7], vertices[19], vertices[4], vertices[12], vertices[0] };
            //  faces[5].vertices = temp6;
            faces[5].vertices[0] = vertices[0];
            faces[5].vertices[1] = vertices[11];
            faces[5].vertices[2] = vertices[3];
            faces[5].vertices[3] = vertices[15];
            faces[5].vertices[4] = vertices[7];
            faces[5].vertices[5] = vertices[19];
            faces[5].vertices[6] = vertices[4];
            faces[5].vertices[7] = vertices[12];
            faces[5].vertices[8] = vertices[0];
            faces[5].maxState = "minX";

            foreach (Vertex v in vertices)
            {
                if (!v.Parents.Contains(this))
                    v.Parents.Add(this);
            }
        }

        public Polygon FaceMinY()
        {
            return faces[0];
        }
        public Polygon FaceMaxY()
        {
            return faces[1];
        }
        public Polygon FaceMinX()
        {
            return faces[5];
        }
        public Polygon FaceMaxX()
        {
            return faces[3];
        }
        public Polygon FaceMinZ()
        {
            return faces[4];
        }
        public Polygon FaceMaxZ()
        {
            return faces[2];
        }


        public Vertex[] StandartAxis()
        {
            Vertex[] axis = new Vertex[4];

            for (int i = 0; i < 4; i++)
                axis[i] = new Vertex();

            axis[0].x = 0; axis[0].y = 0; axis[0].z = 0;//axes[0, j] = center
            axis[1].x = 2; axis[1].y = 0; axis[1].z = 0;//axes[1, j] = xAxes
            axis[2].x = 0; axis[2].y = 2; axis[2].z = 0;//axes[2, j] = yAxes
            axis[3].x = 0; axis[3].y = 0; axis[3].z = 2;//axes[3, j] = zAxes

            return axis;

        }

        public void MiddleVertices()
        {
            for (int i = 0; i < 4; i++)
            {
                vertices[i + 8].x = (vertices[i + 1].x + vertices[i].x) / 2;
                vertices[i + 8].y = (vertices[i + 1].y + vertices[i].y) / 2;
                vertices[i + 8].z = (vertices[i + 1].z + vertices[i].z) / 2;
                if (i == 3)
                {
                    vertices[i + 8].x = (vertices[0].x + vertices[i].x) / 2;
                    vertices[i + 8].y = (vertices[0].y + vertices[i].y) / 2;
                    vertices[i + 8].z = (vertices[0].z + vertices[i].z) / 2;
                }

            }

            for (int i = 0; i < 4; i++)
            {
                vertices[i + 12].x = (vertices[i + 4].x + vertices[i].x) / 2;
                vertices[i + 12].y = (vertices[i + 4].y + vertices[i].y) / 2;
                vertices[i + 12].z = (vertices[i + 4].z + vertices[i].z) / 2;

            }
            for (int i = 0; i < 4; i++)
            {
                vertices[i + 16].x = (vertices[i + 4].x + vertices[i + 5].x) / 2;
                vertices[i + 16].y = (vertices[i + 4].y + vertices[i + 5].y) / 2;
                vertices[i + 16].z = (vertices[i + 4].z + vertices[i + 5].z) / 2;
                if (i == 3)
                {
                    vertices[i + 16].x = (vertices[7].x + vertices[4].x) / 2;
                    vertices[i + 16].y = (vertices[7].y + vertices[4].y) / 2;
                    vertices[i + 16].z = (vertices[7].z + vertices[4].z) / 2;
                }

            }

        }

        /*public double F_i(int trans_vertex, int i)
         {
             Cube StandartCube = new Cube();
             double fi;
             if (i <= 7)
             {
                 double a, b, c, d;
                 a = (1 + vertices[trans_vertex].x * StandartCube.vertices[i].x);
                 b = (1 + vertices[trans_vertex].y * StandartCube.vertices[i].y);
                 c = (1 + vertices[trans_vertex].z * StandartCube.vertices[i].z);
                 d = (
                     vertices[trans_vertex].x * StandartCube.vertices[i].x +
                     vertices[trans_vertex].y * StandartCube.vertices[i].y +
                     vertices[trans_vertex].z * StandartCube.vertices[i].z - 2
                     );

                 fi = (a * b * c * d) / 8;

             }
             else
             {
                 double a, b, c, d;
                 a = (1 + vertices[trans_vertex].x * StandartCube.vertices[i].x);
                 b = (1 + vertices[trans_vertex].y * StandartCube.vertices[i].y);
                 c = (1 + vertices[trans_vertex].z * StandartCube.vertices[i].z);
                 d = (1 -
                     Math.Pow(vertices[trans_vertex].x * StandartCube.vertices[i].y * StandartCube.vertices[i].z, 2.0) -
                     Math.Pow(vertices[trans_vertex].y * StandartCube.vertices[i].x * StandartCube.vertices[i].z, 2.0) -
                     Math.Pow(vertices[trans_vertex].z * StandartCube.vertices[i].x * StandartCube.vertices[i].y, 2.0)
                     );

                 fi = a * b * c * d / 4.0;
             }
             return fi;
         }*/



        void Print(double x, double y, char[] text)
        {
            Gl.glRasterPos2d(x, y);
            int length = text.Length;
            for (int i = 0; i < length; i++)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, text[i]);
            }
        }
        public void Draw1()
        {
            foreach (Polygon p in faces)
                p.Draw(true);
        }
        public void DrawFinal(bool mainCube, bool verticesOnly)
        {
            this.Draw(true, verticesOnly);
            if (IsSelected)
            {
                foreach (Cube c in Parts)
                    foreach (Polygon p in faces)
                        p.Draw(true);

            }
            else
            {
                foreach (Cube c in Parts)
                    foreach (Polygon p in faces)
                        p.Draw(false);
            }

        }
        public void Draw(bool mainCube, bool verticesOnly)
        {
            /* foreach (CubeEdge e in edges)
                 e.Draw();*/
            //Origin.Draw1();
            if (GlobalVar.cubeDrawMode == 0)
            {
                if (IsSelected)
                {
                    foreach (Polygon p in faces)
                        p.Draw(true);
                    foreach (Cube c in Parts)
                        foreach (Polygon p in c.faces)
                            p.Draw(true);
                }
                else
                {

                    if (!verticesOnly)
                    {
                        foreach (Polygon p in faces)
                            p.Draw(false);
                    }
                    else
                    {
                        foreach (Polygon p in faces)
                        {
                            if (p.IsPointed == true)
                                p.Draw(false);
                        }
                    }
                }
            }

            if (GlobalVar.cubeDrawMode == 1)
            {


                if (IsSelected)
                {
                    foreach (CubeEdge edg in edges)
                        edg.Draw();
                    foreach (Cube c in Parts)
                        foreach (Polygon p in c.faces)
                            p.Draw(true);
                }
                else
                {

                    if (!verticesOnly)
                    {
                        foreach (CubeEdge edg in edges)
                            edg.Draw();
                    }
                    else
                    {
                        /*foreach (Polygon p in faces)
                        {
                            if (p.IsPointed == true)
                                p.Draw(false);
                        }*/
                    }
                }

            }

            //вершини
            if (mainCube)
            {
                foreach (Vertex v in vertices)
                {
                    if ((v.IsSelected == true) || (v.EnableInit == true) || (v.IsPicked == true))
                        v.Draw(1, 0, 1, 5);
                    else
                        v.Draw(0, 0, 1, 5);
                }
                //Axes
                Gl.glLineWidth(2);
                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(1.0f, 0.0f, 0.0f);

                axes[0].Draw();
                axes[1].Draw();

                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(0.0f, 1.0f, 0.0f);

                axes[0].Draw();
                axes[2].Draw();

                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINES);
                Gl.glColor3f(1.0f, 1.0f, 0.0f);

                axes[0].Draw();
                axes[3].Draw();

                Gl.glEnd();
                Gl.glLineWidth(1);
            }
            else
            {
                Gl.glColor3f(1.0f, 0.0f, 0.0f);
                Gl.glEnable(Gl.GL_POINT_SMOOTH);
                Gl.glPointSize(1);
                Gl.glBegin(Gl.GL_POINTS);

                for (int i = 0; i < 20; i++)
                {
                    vertices[i].Draw();

                }
                Gl.glEnd();
            }



        }


        internal void Initialize(double[,] points)
        {
            for (int i = 0; i < 20; i++)
            {
                vertices[i].x = points[i, 0];
                vertices[i].y = points[i, 1];
                vertices[i].z = points[i, 2];

            }
        }

        internal void Initialize(Vertex[] vMas)
        {
            for (int i = 0; i < 20; i++)
            {
                vertices[i].x = vMas[i].x;
                vertices[i].y = vMas[i].y;
                vertices[i].z = vMas[i].z;

            }
            UpdateFace();
            
        }

        internal void Initialize(List<Vertex> vMas)
        {
            for (int i = 0; i < 20; i++)
            {
                vertices[i].x = vMas[i].x;
                vertices[i].y = vMas[i].y;
                vertices[i].z = vMas[i].z;

            }
            UpdateFace();
        }

        internal void Initialize(Cube c)
        {
            for (int i = 0; i < 20; i++)
            {
                vertices[i].EnableInit = c.vertices[i].EnableInit;
                vertices[i].Parents = new List<Cube>();
                vertices[i].Parents.Add(this);

                vertices[i].x = c.vertices[i].x;
                vertices[i].y = c.vertices[i].y;
                vertices[i].z = c.vertices[i].z;

            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    faces[i].lockState.enableChange = c.faces[i].lockState.enableChange;
                    faces[i].maxState = c.faces[i].maxState;
                    faces[i].Parent = this;
                    faces[i].lockState.lockStatus = c.faces[i].lockState.lockStatus;


                    faces[i].vertices[j].x = c.faces[i].vertices[j].x;
                    faces[i].vertices[j].y = c.faces[i].vertices[j].y;
                    faces[i].vertices[j].z = c.faces[i].vertices[j].z;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                edges[i].begin.x = c.edges[i].begin.x;
                edges[i].begin.y = c.edges[i].begin.y;
                edges[i].begin.z = c.edges[i].begin.z;

                edges[i].middle.x = c.edges[i].middle.x;
                edges[i].middle.y = c.edges[i].middle.y;
                edges[i].middle.z = c.edges[i].middle.z;

                edges[i].end.x = c.edges[i].end.x;
                edges[i].end.y = c.edges[i].end.y;
                edges[i].end.z = c.edges[i].end.z;

                edges[i].lockState.enableChange = c.edges[i].lockState.enableChange;
                edges[i].lockState.lockStatus = c.edges[i].lockState.lockStatus;
            }
        }

        public void Translate(double x, double y, double z)
        {
            for (int i = 0; i < 20; i++)
            {
                vertices[i].x += x;
                vertices[i].y += y;
                vertices[i].z += z;

            }
        }

    }
}
