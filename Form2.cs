using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using System.IO;
using System.Diagnostics;


namespace Tao_Framework_Meet
{
    public partial class Form2 : Form
    {
        //Lock stuffs

        bool isLockingEdge;
        bool isLockingFace;
        bool isUnlockingFace;
        bool isUnlockingEdge;
        
        //primitives set
        List<Cube> primitives = new List<Cube>();

        private double F_i(Vertex trans_vertex, int i)
        {
            Cube StandartCube = new Cube();
            double fi;
            double vsx = trans_vertex.x * StandartCube.vertices[i].x;
            double vsy = trans_vertex.y * StandartCube.vertices[i].y;
            double vsz = trans_vertex.z * StandartCube.vertices[i].z;
            double vsxyz = trans_vertex.x * StandartCube.vertices[i].y * StandartCube.vertices[i].z;
            double vsyxz = trans_vertex.y * StandartCube.vertices[i].x * StandartCube.vertices[i].z;
            double vszxy = trans_vertex.z * StandartCube.vertices[i].x * StandartCube.vertices[i].y;
            if (i <= 7)
            {
                double a, b, c, d;
                a = (1 + vsx);
                b = (1 + vsy);
                c = (1 + vsz);
                d = vsx +vsy +vsz- 2;

                fi = (a * b * c * d) / 8;

            }
            else
            {
                double a, b, c, d;
                a = (1 + vsx);
                b = (1 + vsy);
                c = (1 + vsz);
                d = (1 -vsxyz*vsxyz-vsyxz*vsyxz-vszxy*vszxy);

                fi = a * b * c * d / 4.0;
            }
            return fi;
           // return 1;
        }

        

        private void CurveThisCube(Cube cube, Cube myCube)
        {
           // Stopwatch sWatch = new Stopwatch();
           // sWatch.Start();
            double[,] arr = new double[20, 3];

            for (int i = 0; i < 20; i++)
            {
                double sumX = 0, sumY = 0, sumZ = 0;
                for (int j = 0; j < 20; j++)
                {
                    double fi = F_i(cube.vertices[i], j);
                    sumX += myCube.vertices[j].x * fi;
                    sumY += myCube.vertices[j].y * fi;
                    sumZ += myCube.vertices[j].z * fi;
                }
                arr[i, 0] = sumX;
                arr[i, 1] = sumY;
                arr[i, 2] = sumZ;

            }
            cube.Initialize(arr);
            //sWatch.Stop();
            //textBox1.Text = sWatch.ElapsedMilliseconds.ToString();
        }

        public void CurveAxis()
        {
            //double[,] curve_axis = new double[4, 3];
           /* Vertex[] curve_axis = myCube.StandartAxis();
            for (int i = 0; i < 4; i++)
            {
                double sumX = 0, sumY = 0, sumZ = 0;
                for (int j = 0; j < 20; j++)
                {
                    sumX += myCube.vertices[j].x * F_i(curve_axis[i], j);
                    sumY += myCube.vertices[j].y * F_i(curve_axis[i], j);
                    sumZ += myCube.vertices[j].z * F_i(curve_axis[i], j);
                }
                myCube.axes[i].x = sumX;
                myCube.axes[i].y = sumY;
                myCube.axes[i].z = sumZ;
            }*/
        }

        List<Cube> DevideCube(int nx, int ny, int nz)
        {
            Cube mycube1 ;
            List<Cube> CubeParts = new List<Cube>();

            double[,] arr = new double[20, 3];

            //(x0,y0,z0) - ліва нижня вершина стандартного кубика
            double x0 = -1;
            double y0 = -1;
            double z0 = 1;

            //довжина ребра стандартного кубика = 2
            // length, height,width - виміри кубика утвореного поділом стандарного на nx, ny, nz частини
            double length = 2.0 / nx;
            double height = 2.0 / ny;
            double width = 2.0 / nz;
            for (int i1 = 0; i1 < ny; i1++)
            {


                arr[0, 1] = arr[1, 1] = arr[2, 1] = arr[3, 1] = y0 + i1 * height;
                arr[4, 1] = arr[5, 1] = arr[6, 1] = arr[7, 1] = y0 + (i1 + 1) * height;

                for (int i2 = 0; i2 < nx; i2++)
                {
                    arr[0, 0] = arr[3, 0] = arr[4, 0] = arr[7, 0] = x0 + i2 * length;
                    arr[1, 0] = arr[2, 0] = arr[5, 0] = arr[6, 0] = x0 + (i2 + 1) * length;

                    for (int i3 = 0; i3 < nz; i3++)
                    {

                        arr[0, 2] = arr[1, 2] = arr[4, 2] = arr[5, 2] = z0 -i3 * width;
                        arr[2, 2] = arr[3, 2] = arr[6, 2] = arr[7, 2] = z0 - (i3 + 1) * width;
                        mycube1 = new Cube();
                        mycube1.Initialize(arr);
                        CubeParts.Add(mycube1);
                    }
                }
            }

            foreach (Cube c in CubeParts)
            {
                c.MiddleVertices();
            }
            standartParts = CubeParts;
            return CubeParts;
          //  return new List<Cube>();
           
        }
        void DevideCurveCube(int index, ref Cube myCube)
        {
            myCube.nx = nx;
            myCube.ny = ny;
            myCube.nz = nz;

            List<Cube> temp = new List<Cube>();

            //temp.AddRange(standartParts);
            temp = DevideCube(nx, ny, nz);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            foreach (Cube c in temp)
            {
                CurveThisCube(c, myCube);
            }
            myCube.Parts = temp;
           // sWatch.Stop();
            //textBox2.Text = sWatch.ElapsedMilliseconds.ToString();
        }

        void DevideCurveCube(int nx,int ny, int nz,bool forAll)
        {
            if (forAll)
            {
                foreach (Cube myCube in Model)
                {

                    myCube.nx = nx;
                    myCube.ny = ny;
                    myCube.nz = nz;

                    List<Cube> temp = new List<Cube>();
                    
                       // temp.AddRange(standartParts);
                         temp = DevideCube(nx, ny, nz);
                        //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                        Stopwatch sWatch = new Stopwatch();
                        sWatch.Start();
                    foreach (Cube c in temp)
                    {
                        CurveThisCube(c, myCube);
                    }
                    myCube.Parts = temp;
                    sWatch.Stop();
            //textBox2.Text = sWatch.ElapsedMilliseconds.ToString();
        

                }
            }
            else
            {
                foreach (Cube myCube in Model)
                {
                    if ((myCube.Parts.Count != 0) &&(vertex.Parents.Contains(myCube)))
                    {

                      //  List<Cube> temp = new List<Cube>();
                        List<Cube> temp = new List<Cube>();

                        //temp.AddRange(standartParts);
                        temp = DevideCube(myCube.nx,myCube.ny,myCube.nz);
                      //  Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
                       // List<Cube> temp = new List<Cube>();

                        //temp.AddRange(standartParts);
                        foreach (Cube c in temp)
                        {
                            CurveThisCube(c, myCube);
                        }
                        myCube.Parts = temp;
                    }

                }
            }
        }

        

        private void devide_button_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int.Parse(textBoxNx.Text) > 0)&&(int.Parse(textBoxNy.Text) > 0)&&
                    (int.Parse(textBoxNz.Text) > 0))
                {
                    int nx, ny, nz;
                    nx = int.Parse(textBoxNx.Text);
                    ny = int.Parse(textBoxNy.Text);
                    nz = int.Parse(textBoxNz.Text);
                    //DevideCurveCube(nx,ny,nz);
                    PlotGL();
                    StandartCubeToolStripMenuItem.Enabled = false;
                    //myCube.EnabledTransform = false;
               }
                else {MessageBox.Show("Неправильні дані");}
             }
            catch { MessageBox.Show("Неправильні дані"); }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
        
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (Cube myCube in Model)
                {
                    foreach (Vertex v in myCube.vertices)
                    {
                        if (v.EnableInit == true)
                        {
                            v.x = double.Parse(textBoxX.Text);
                            v.y = double.Parse(textBoxY.Text);
                            v.z = double.Parse(textBoxZ.Text);
                            DevideCurveCube(int.Parse(textBoxNx.Text),int.Parse(textBoxNy.Text) , int.Parse(textBoxNz.Text), true);
                            PlotGL();
                            AnT.Focus();
                        }
                    }
                }
            }
        }

        private void StandartCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* myCube = new Cube();
            StandartCubeToolStripMenuItem.Enabled = true;*/
        }

        private void ResetPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///fignya = false;
         height = 0;
        width = 0;

        // Rotation/Zoom/Pan
         matrixLock = new System.Object();
         arcBall = new arcball(740.0f, 480.0f);
        matrix = new float[16];
         LastTransformation = new Matrix4f();
         ThisTransformation = new Matrix4f();

        // mouse 
        //private Point mouseStartDrag;
        isLeftDrag = false;
        //isRightDrag = false;
        // isMiddleDrag = false;
        PlotGL();

        }

        private void NonDevideToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* textBoxNx.Text = textBoxNy.Text = textBoxNz.Text = "1";
            CubeParts.Clear();
            PlotGL();
            myCube.EnabledTransform = true;
            StandartCubeToolStripMenuItem.Enabled = true;*/
        }
        int click = 1;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            Size s = AnT.Size;
            height = s.Height;
            width = s.Width;

            Gl.glClearColor(0.0f, 1.0f, 0.9f, 0.0f);					// black background

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            Gl.glViewport(0, 0, width, height);

            //Gl.glPushMatrix();
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(40.0, (double)width / (double)height, 1, 200);

            Gl.glTranslated(-2, 0, -30);
            //Gl.glRotated(60, 0, 1, 0);
            Gl.glScaled(Math.Pow(1.5, click), Math.Pow(1.5, click), Math.Pow(1.5, click));
            //if(click<3)
                click++;
            
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Gl.glPopMatrix();

            arcBall.setBounds((float)width, (float)height); // Update mouse bounds for arcball
            PlotGL();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Size s = AnT.Size;
            height = s.Height;
            width = s.Width;

            Gl.glClearColor(0.0f, 1.0f, 0.9f, 0.0f);					// black background

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            Gl.glViewport(0, 0, width, height);

            //Gl.glPushMatrix();
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(40.0, (double)width / (double)height, 1, 200);

            Gl.glTranslated(-2, 0, -30);
            //Gl.glRotated(60, 0, 1, 0);
            Gl.glScaled(Math.Pow(1.5, click-2), Math.Pow(1.5, click-2), Math.Pow(1.5, click-2));
            
            click--;
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Gl.glPopMatrix();

            arcBall.setBounds((float)width, (float)height); // Update mouse bounds for arcball
            PlotGL();

        }
        
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            IsAddingCube = true;
            IsRemovingCube = false;
            isScaled = false;
            isLockingEdge = false;
            isLockingFace = false;
            
        }

        private void GetAbsoluteLocation(int controlX, int controlY, int locationX, int LocationY, out int x, out int y)
        {
            x = controlX+locationX;
            y = 0;
        }

        private void DrawAxes()
        {
           /* double xCoord = 0, yCoord = 0, zCoord=0;
            int locationX = 0, locationY = 0;

            Vertex x = myCube.axes[1];
            Vertex y = myCube.axes[2];
            Vertex z = myCube.axes[3];*/

            /*Glu.gluProject(1, 0, 0, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            xLabel.Location = new Point((int)Math.Round(xCoord, 0),(int) Math.Round(yCoord, 0));*/

            /*Glu.gluProject(1 , 1, 1, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            locationX = (int)(Math.Round(xCoord,0) + AnT.Location.X);
            locationY = (int)((AnT.Height - Math.Round(yCoord,0)) + AnT.Location.Y);
            yLabel.Location = new Point(locationX, locationY);
            yLabel.BackColor = Color.Transparent;
            textBox6.Text = yLabel.Location.X.ToString();
            textBox10.Text = yLabel.Location.Y.ToString();*/

            /*Glu.gluProject(z.x , z.y, z.z+0.3, GetModelview(), GetProjection(), GetViewport(), out xCoord, out yCoord, out zCoord);
            zLabel.Location = new Point((int)Math.Round(xCoord, 0), (int)Math.Round(yCoord, 0));*/
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            AnT.Width = this.Width - 30;
            AnT.Height = this.Height - 135;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((IsAddingCube == true) && (selectedSide != null))
                AddCube();
            //PlotGL1();
           // textBox1.Text = myCube.faces[3].MinX().ToString() + " " + myCube.Origin.faces[3].MinX().ToString();
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            IsRemovingCube = true;
            IsAddingCube = false;
        }

        private void textBoxDevide_Leave(object sender, EventArgs e)
        {
            
                try
                {
                    if ((int.Parse(textBoxNx.Text) > 0) && (int.Parse(textBoxNy.Text) > 0) &&
                        (int.Parse(textBoxNz.Text) > 0))
                    {
                        //int nx, ny, nz;
                        nx = int.Parse(textBoxNx.Text);
                        ny = int.Parse(textBoxNy.Text);
                        nz = int.Parse(textBoxNz.Text);
                        DevideCube(nx, ny, nz);
                        DevideCurveCube(nx, ny, nz, true);
                        PlotGL();
                        StandartCubeToolStripMenuItem.Enabled = false;
                        //myCube.EnabledTransform = false;
                    }
                    else { MessageBox.Show("Неправильні дані"); }
                }
                catch { MessageBox.Show("Неправильні дані"); }

            
        }

        private void textBoxCoord_Leave(object sender, EventArgs e)
        {

            if (vertex.EnableInit == true)
            {
                try
                {

                    vertex.x = double.Parse(textBoxX.Text);
                    vertex.y = double.Parse(textBoxY.Text);
                    vertex.z = double.Parse(textBoxZ.Text);
                    DevideCurveCube(Model[0].nx, Model[0].ny, Model[0].nz, false);
                    AnT.Focus();
                    PlotGL();
                }
                catch
                {
                    MessageBox.Show("Неправильні дані");
                }
            }


        }

        void ResizeShapes(double cofficientX, double cofficientY, double cofficientZ)
        {
            if (GlobalVar.scaleType == 1)
            {
                for (int i = 0; i < scaledShapes.Count; i++)
                {
                    //center

                    Vertex center = new Vertex();
                    center.x = 0; center.y = 0; center.z = 0;

                    //serendyp approxymations
                    double sumX = 0, sumY = 0, sumZ = 0;
                    for (int j = 0; j < 20; j++)
                    {
                        double fi = F_i(center, j);
                        sumX += scaledShapes[i].vertices[j].x * fi;
                        sumY += scaledShapes[i].vertices[j].y * fi;
                        sumZ += scaledShapes[i].vertices[j].z * fi;
                    }
                    double x0 = sumX;
                    double y0 = sumY;
                    double z0 = sumZ;

                    /*foreach (Vertex v in scaledShapes[i].vertices)
                    {
                        v.x = (v.x - x0) * cofficientX + x0;
                        v.y = (v.y - y0) * cofficientY + y0; ;
                        v.z = (v.z - z0) * cofficientZ + z0;

                    }*/
                    //txtMouseX.Text = scaledVerticies[i][0].x.ToString();
                    for (int j = 0; j < 20; j++)
                    {
                        double prevX = scaledVerticies[i][j].x;
                        double prevY = scaledVerticies[i][j].y;
                        double prevZ = scaledVerticies[i][j].z;
                        scaledShapes[i].vertices[j].x = (prevX - x0) * cofficientX + x0;
                        scaledShapes[i].vertices[j].y = (prevY - y0) * cofficientY + y0;
                        scaledShapes[i].vertices[j].z = (prevZ - z0) * cofficientZ + z0;
                    }
                }
            }

            if (GlobalVar.scaleType == 2)
            {
                for (int i = 0; i < scaledShapes.Count; i++)
                {
                    //center

                    Vertex center = new Vertex();
                    center.x = 0; center.y = 0; center.z = 0;

                    //serendyp approxymations
                    double sumX = 0, sumY = 0, sumZ = 0;
                    for (int j = 0; j < 20; j++)
                    {
                        double fi = F_i(center, j);
                        sumX += scaledShapes[i].vertices[j].x * fi;
                        sumY += scaledShapes[i].vertices[j].y * fi;
                        sumZ += scaledShapes[i].vertices[j].z * fi;
                    }
                    double x0 = sumX;
                    double y0 = sumY;
                    double z0 = sumZ;

                    /*foreach (Vertex v in scaledShapes[i].vertices)
                    {
                        v.x = (v.x - x0) * cofficientX + x0;
                        v.y = (v.y - y0) * cofficientY + y0; ;
                        v.z = (v.z - z0) * cofficientZ + z0;

                    }*/
                    //txtMouseX.Text = scaledVerticies[i][0].x.ToString();
                    for (int j = 0; j < 6; j++)
                    {
                        if (scaledShapes[i].faces[j].Equals(GlobalVar.polygonToScale))
                        {
                            for (int k = 0; k < scaledShapes[i].faces[j].vertices.Length; k++)
                            {
                                double prevX = scaledVerticies[i][k].x;
                                double prevY = scaledVerticies[i][k].y;
                                double prevZ = scaledVerticies[i][k].z;
                                scaledShapes[i].faces[j].vertices[k].x = (prevX - x0) * cofficientX + x0;
                                scaledShapes[i].faces[j].vertices[k].y = (prevY - y0) * cofficientY + y0;
                                scaledShapes[i].faces[j].vertices[k].z = (prevZ - z0) * cofficientZ + z0;
                            }
                        }
                    }
                }
            }
        }

        void ResizeShape(Cube shape, double cofficientX, double cofficientY, double cofficientZ)
        {
            //center

            Vertex center = new Vertex();
            center.x = 0; center.y = 0; center.z = 0;

             //serendyp approxymations
            double sumX = 0, sumY = 0, sumZ = 0;
            for (int j = 0; j < 20; j++)
            {
                double fi = F_i(center, j);
                sumX += shape.vertices[j].x * fi;
                sumY += shape.vertices[j].y * fi;
                sumZ += shape.vertices[j].z * fi;
            }
            double x0 = sumX;
            double y0 = sumY;
            double z0 = sumZ;


            //MessageBox.Show(x0 + ", " + y0 + ", " + z0);

            foreach (Vertex v in shape.vertices)
            {
                v.x = (v.x - x0) * cofficientX + x0;
                v.y = (v.y - y0) * cofficientY + y0; ;
                v.z = (v.z - z0) * cofficientZ + z0;

            }
           
        }

        private void btnTryScale_Click(object sender, EventArgs e)
        {
            string str = "(";
            str+=Model[0].edges[0].eCubeEdge(AnT.Height * 1.0)[0].begin.x.ToString();
            str += ", ";
            str += Model[0].edges[0].eCubeEdge(AnT.Height * 1.0)[0].begin.y.ToString();
            str += ") ";
            str += "(";
            str += Model[0].edges[0].eCubeEdge(AnT.Height * 1.0)[0].end.x.ToString();
            str += ", ";
            str += Model[0].edges[0].eCubeEdge(AnT.Height * 1.0)[0].end.y.ToString();
            str += ") ";
            MessageBox.Show(str);
            GlobalVar.cubeDrawMode = 1;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            isScaled = !isScaled;
        }

       /* private void textBoxCoord_KeyDown(object sender, KeyEventArgs e)
        {

        }*/

        private void lockEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLockingEdge = !(isLockingEdge);
            GlobalVar.cubeDrawMode = 1;

        }

        private void lockFaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLockingFace = !(isLockingFace);
            isUnlockingFace = false;
        }

        private void unlockEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isUnlockingEdge = !isUnlockingEdge;
            //MessageBox.Show(isUnlockingEdge.ToString());
            GlobalVar.cubeDrawMode = 1;
            isLockingEdge = false;
            isLockingFace = false;
            isUnlockingFace = false;
        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isUnlockingFace = true;
            isLockingFace = false;
        }

        public string newPrimitiveName = "";

        public void getFullCopies()
        {
            //(for int i = 0; i<GlobalVar.verParents.Count; i++)

            /*for (int i = 0; i< GlobalVar.verParents.Count; i++)
            {
                GlobalVar.verParents[i].First.Parents = new List<Cube>();
                List<Cube> parents = GlobalVar.verParents[i].First.Parents;
                

                MessageBox.Show(GlobalVar.verParents[i].Second.Count.ToString());
                for (int j = 0; j < GlobalVar.verParents[i].Second.Count; j++)
                {
                    MessageBox.Show(j.ToString());
                    parents.Add(Cube.getCopy(GlobalVar.verParents[i].Second[j]));
                }
                MessageBox.Show(GlobalVar.verParents[i].Second.Count.ToString(" "));
            }*/

            /*foreach (Pair<Vertex, List<Cube>> item in GlobalVar.verParents)
            {
                item.First.Parents = new List<Cube>();
                foreach (Cube c in item.Second)
                {
                    item.First.Parents.Add(Cube.getCopy(c));
                }
            }*/

            for(int i = 0; i<GlobalVar.cubeOrigin.Count; i++)
            {
                GlobalVar.cubeOrigin[i].First.Origin = Cube.getCopy(GlobalVar.cubeOrigin[i].Second);
            }
            /*foreach (Pair<Cube, Cube> item in GlobalVar.cubeOrigin)
            {
                item.First.Origin = Cube.getCopy(item.Second);
            }*/

            /*foreach (Pair<Cube, Cube> item in GlobalVar.cubeTrans)
            {
                item.First.TransCube = Cube.getCopy(item.Second);
            }

            foreach (Pair<Cube, List<Cube>> item in GlobalVar.cubeParts)
            {
                item.First.Parts = new List<Cube>();
                foreach (Cube c in item.Second)
                {
                    item.First.Parts.Add(Cube.getCopy(c));
                }
            }
            
            foreach(Pair<Polygon,Cube> item in GlobalVar.faceParent)
            {
                item.First.Parent = Cube.getCopy(item.Second);
            }*/
            
        }

        public void renewPrimitivesCollection()
        {
            toolCmbPrimitives.Items.Clear();
            foreach(Cube primitive in primitives)
            {
                toolCmbPrimitives.Items.Add(primitive.primitiveName);
            }
        }

        public void AddPrimitive(string name)
        {
            Cube newPrimitive = new Cube(); newPrimitive.Initialize(Model[0]);
            newPrimitive.Origin = new Cube(); newPrimitive.Origin.Initialize(Model[0]);
            newPrimitive.TransCube = new Cube(); newPrimitive.TransCube.Initialize(Model[0]);

            for (int i = 0; i < newPrimitive.edges.Length; i++)
            {
                if (newPrimitive.edges[i].lockState.lockStatus)
                    newPrimitive.edges[i].lockState.enableChange = false;
            }

            for (int i = 0; i < newPrimitive.faces.Length; i++)
            {
                if (newPrimitive.faces[i].lockState.lockStatus)
                    newPrimitive.faces[i].lockState.enableChange = false;
            }

            newPrimitive.primitiveName = name;
            primitives.Add(newPrimitive);

            renewPrimitivesCollection();

            frmParent.newPrimitive_.Initialize(newPrimitive);
            frmParent.newPrimitive_.Origin = new Cube();
            frmParent.newPrimitive_.Origin.Initialize(newPrimitive.Origin);
            frmParent.newPrimitive_.TransCube = new Cube();
            frmParent.newPrimitive_.TransCube.Initialize(newPrimitive.TransCube);
            frmParent.AddPrimitive(name);
            frmParent.renewPrimitivesCollection();
            //frmParent.Model.Add(frmParent.newPrimitive_);
            //getFullCopies();
        }

        private void btnSavePrimitive_Click(object sender, EventArgs e)
        {
            frmDialogueSavePrimitive frmSave = new frmDialogueSavePrimitive();
            frmSave.frmParent = this;
            frmSave.ShowDialog();
        }

        private void toolCmbPrimitives_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Model.Clear();

                //MessageBox.Show(toolCmbPrimitives.Text);
                foreach (Cube primitive in primitives)
                {
                    MessageBox.Show(primitive.primitiveName);
                    if (primitive.primitiveName == toolCmbPrimitives.Text)
                    {
                        Cube newCube = new Cube(); newCube.Initialize(primitive);
                        newCube.Origin = new Cube(); newCube.Origin.Initialize(primitive);
                        newCube.TransCube = new Cube(); newCube.TransCube.Initialize(primitive);


                        Model.Add(newCube);
                    }
                   // break;
                }

                scaledShapes.Clear();
                DefineScaledShapes();
            }
        }

        public string AxisName = "x";

        public Form0 frmParent;

        private void xAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isScaled = !isScaled;
            AxisName = "x";
        }

        private void yAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isScaled = !isScaled;
            AxisName = "y";
        }

        private void zAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isScaled = !isScaled;
            AxisName = "z";
        }
        

        

        

     }
}
