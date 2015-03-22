using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Tao_Framework_Meet
{
    public partial class Form0 : Form
    {
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
        Vector WorldPixelCoordinates(double wx, double wy, double pointX, double pointY, double pointZ, int type)
        {
            Vector v0, n;
            if (type == 1)
            {
                v0 = new Vector(0, 0, pointZ); //v0 є z = pointZ

                n = new Vector(0, 0, 1);// нормаль
            }
            else
            {
                v0 = new Vector(0, pointY, 0); //v0 є z = pointY

                n = new Vector(0, 1, 0);// нормаль
            }

            double x = 0, y = 0, z = 0;


            Glu.gluUnProject((double)wx, (double)wy, 0, GetModelview(), GetProjection(), GetViewport(), out x, out y, out z);
            Vector p0 = new Vector(Math.Round(x, 3), Math.Round(y, 3), Math.Round(z, 3));

            Glu.gluUnProject((double)wx, (double)wy, 1, GetModelview(), GetProjection(), GetViewport(), out x, out y, out z);
            Vector p1 = new Vector(Math.Round(x, 3), Math.Round(y, 3), Math.Round(z, 3));

            Vector u = p1.Sum(p0.ScalarMult(-1.0));// (p1 - p0)

            Vector w = p0.Sum(v0.ScalarMult(-1.0));// p0-w0

            double s = (-1.0 * (n.Product(w))) / (n.Product(u));// s = -(n,w)/(n,u)

            Vector v1 = p0.Sum(u.ScalarMult(s));

            return v1;
        }


        void Pick(double x, double y, double z)
        {
            foreach (Cube myCube in Model)
            {
                for (int i = 0; i < 20; i++)
                {
                    myCube.vertices[i].IsSelected = false;
                }

                foreach (Polygon face in myCube.faces)
                {
                    if (face.lockState.lockStatus == true)
                    {
                        for (int i = 0; i < face.vertices.Length; i++)
                        {
                            if (face.vertices[i].ApproximatelyEquals(x, y, z))
                            {
                                //face.vertices[i].IsSelected = true;
                                GlobalVar.polygonToScale = face;
                                GlobalVar.scaleType = 2;
                                PlotGL();
                                //MessageBox.Show("yes");
                                return;
                            }
                        }
                    }

                }
            }


            foreach (Cube myCube in Model)
            {
                for (int i = 0; i < 20; i++)
                {
                    myCube.vertices[i].IsSelected = false;
                }

                foreach (CubeEdge edge in myCube.edges)
                {
                    if (edge.lockState.lockStatus == true)
                    {
                        if ((edge.begin.ApproximatelyEquals(x, y, z) ||
                            (edge.middle.ApproximatelyEquals(x, y, z)) |
                            (edge.end.ApproximatelyEquals(x, y, z))))
                        {
                            //GlobalVar.polygonToScale = face;
                            GlobalVar.scaleType = 2;
                            PlotGL();
                            //MessageBox.Show("yes");
                            return;
                        }


                    }

                }
            }


            foreach (Cube myCube in Model)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (myCube.vertices[i].ApproximatelyEquals(x, y, z))
                    {

                        myCube.vertices[i].IsSelected = true;
                        vertex = myCube.vertices[i];
                        PlotGL();
                    }
                    else
                    {
                        myCube.vertices[i].IsSelected = false;
                    }


                }
            }
            

        }

        void SetVertexState()
        {
            if (vertex.IsPicked == true)
            {
                vertex.EnableInit = true;
                vertex.IsPicked = false;
                vertex.IsSelected = false;
                PlotGL();
            }
        }

        void ShowCoordinates()
        {
           if (vertex.EnableInit == true)
                textBoxX.ReadOnly = textBoxY.ReadOnly = textBoxZ.ReadOnly = false;
            else
            {
                textBoxX.ReadOnly = textBoxY.ReadOnly = textBoxZ.ReadOnly = true;
            }
            
            if ((vertex.EnableInit == true) || (vertex.IsPicked == true) || (vertex.IsSelected == true))
            {
                textBoxX.Text = vertex.x.ToString();
                textBoxY.Text = vertex.y.ToString();
                textBoxZ.Text = vertex.z.ToString();
            }
            else
            {
                textBoxX.Text = "";
                textBoxY.Text = "";
                textBoxZ.Text = "";
            }
        }

        public void GetMouseWorldCoordinates(MouseEventArgs e)
        {
            double mouseX = 0;
            double mouseY = 0;
            double mouseZ = 0;

            double x = 0, y = 0, z = 0;
            int wx = e.X;
            int wy = AnT.Height - e.Y - 1;
            float[] wz = new float[1];
            Gl.glReadPixels(wx, wy, 1, 1, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, wz);

            if (wz[0] == 1)
            {
                //Vector vertex1 = WorldPixelCoordinates(wx, wy);
                    if (e.Button == MouseButtons.Left)
                    {
                        Vector v = WorldPixelCoordinates(wx, wy, 1, 1, 1, 1);
                        mouseX = Math.Round(v.x, 3);
                        mouseY = Math.Round(v.y, 3);
                        mouseZ = Math.Round(v.z, 3);
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        Vector v = WorldPixelCoordinates(wx, wy, 1, 1, 1, 2);
                        mouseX = Math.Round(v.x, 3);
                        mouseY = Math.Round(v.y, 3);
                        mouseZ = Math.Round(v.z, 3);
                    }
                    //txtMouseX.Text = mouseX.ToString();
                   // txtMouseY.Text = mouseY.ToString();
                   // txtMouseZ.Text = mouseZ.ToString();

                    

            }
            else
            {
                
                    Glu.gluUnProject((double)wx, (double)wy, (double)wz[0], GetModelview(), GetProjection(), GetViewport(), out x, out y, out z);

                    //txtMouseX.Text = x.ToString();
                    //txtMouseY.Text = y.ToString();
                    //txtMouseZ.Text = z.ToString();
                
            }
        }

         Vertex TranslateVertex(MouseEventArgs e)
        {
            // PlotGL();
            //SelectSide(e);
            ShowCoordinates();
            double x = 0, y = 0, z = 0;
            int wx = e.X;
            int wy = AnT.Height - e.Y - 1;
            float[] wz = new float[1];
            Gl.glReadPixels(wx, wy, 1, 1, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, wz);

            if (wz[0] == 1)
            {
                if (vertex.IsPicked == false)
                {
                    vertex.IsSelected = false;
                    // PlotGL();

                }
                else
                {
                    
                        //Vector vertex1 = WorldPixelCoordinates(wx, wy);
                        if (e.Button == MouseButtons.Left)
                        {
                            Vector v = WorldPixelCoordinates(wx, wy, vertex.x, vertex.y, vertex.z, 1);
                            vertex.x = Math.Round(v.x, 3);
                            vertex.y = Math.Round(v.y, 3);
                            vertex.z = Math.Round(v.z, 3);
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            Vector v = WorldPixelCoordinates(wx, wy, vertex.x, vertex.y, vertex.z, 2);
                            vertex.x = Math.Round(v.x, 3);
                            vertex.y = Math.Round(v.y, 3);
                            vertex.z = Math.Round(v.z, 3);
                        }

                       // CurveAxis();
                        foreach (Cube c in Model)
                        {
                            if ((c.Parts.Count != 0)&&(vertex.Parents.Contains(c)))
                            {
                                int nx, ny, nz;
                                nx = int.Parse(textBoxNx.Text);
                                ny = int.Parse(textBoxNy.Text);
                                nz = int.Parse(textBoxNz.Text);
                                DevideCurveCube(1,1,1,false);
                                // PlotGL();
                                //StandartCubeToolStripMenuItem.Enabled = false;
                            }
                            //PlotGL();
                        }
                    
                }
                PlotGL();

            }
            else
            {
                if (vertex.IsPicked == false)
                {
                    Glu.gluUnProject((double)wx, (double)wy, (double)wz[0], GetModelview(), GetProjection(), GetViewport(), out x, out y, out z);

                    Pick(x, y, z);
                }
            }

            Vertex mouseCoordinates = new Vertex();
            mouseCoordinates.x = x;
            mouseCoordinates.y = y;
            mouseCoordinates.z = z;
            return mouseCoordinates;

        }

        private void AnT_MouseMove(object sender, MouseEventArgs e)
        { }/*
           // PlotGL();
            //SelectSide(e);
                ShowCoordinates();
                double x = 0, y = 0, z = 0;
                int wx = e.X;
                int wy = AnT.Height - e.Y - 1;
                float[] wz = new float[1];
                Gl.glReadPixels(wx, wy, 1, 1, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, wz);

                if (wz[0] == 1)
                {
                    if (vertex.IsPicked == false)
                    {
                        vertex.IsSelected = false;
                       // PlotGL();

                    }
                    else
                    {
                        if (myCube.EnabledTransform == true)
                        {
                            //Vector vertex1 = WorldPixelCoordinates(wx, wy);
                            if (e.Button == MouseButtons.Left)
                            {
                                Vector v = WorldPixelCoordinates(wx, wy, vertex.x, vertex.y, vertex.z, 1);
                                vertex.x = Math.Round(v.x,3);
                                vertex.y = Math.Round(v.y, 3);
                                vertex.z = Math.Round(v.z, 3);
                            }
                            if (e.Button == MouseButtons.Right)
                            {
                                Vector v = WorldPixelCoordinates(wx, wy, vertex.x, vertex.y, vertex.z, 2);
                                vertex.x = Math.Round(v.x, 3);
                                vertex.y = Math.Round(v.y, 3);
                                vertex.z = Math.Round(v.z, 3);
                            }

                            CurveAxis();

                            if (CubeParts.Count != 0)
                            {
                                int nx, ny, nz;
                                nx = int.Parse(textBoxNx.Text);
                                ny = int.Parse(textBoxNy.Text);
                                nz = int.Parse(textBoxNz.Text);
                                DevideCurveCube(nx, ny, nz);
                               // PlotGL();
                                StandartCubeToolStripMenuItem.Enabled = false;
                            }
                            //PlotGL();
                        }
                    }
                    PlotGL();

                }
                else
                {
                    if (vertex.IsPicked == false)
                    {
                        Glu.gluUnProject((double)wx, (double)wy, (double)wz[0], GetModelview(), GetProjection(), GetViewport(), out x, out y, out z);

                        Pick(x, y, z);
                    }
                }
            
        }*/

        private void AnT_MouseDown(object sender, MouseEventArgs e)
        {
            //контекстне меню
            /*if ((e.Button == MouseButtons.Right) && (vertex.IsPicked == false) && (vertex.IsSelected == false))
                contextMenuStrip1.Show(AnT, new System.Drawing.Point((AnT.Width / 2) + 10, (AnT.Height / 2) + 5));
            */
            
                /*????
                if (vertex.EnableInit == true)
                    vertex.EnableInit = false;
                if (vertex.IsSelected == true)
                {
                    vertex.IsPicked = true;
                  
                }
                 */
            foreach (Cube myCube in Model)
            {
                foreach (Vertex v in myCube.vertices)
                {
                    if (v.EnableInit == true)
                        v.EnableInit = false;
                    if (v.IsSelected == true)
                    {
                        v.IsPicked = true;
                        vertex.IsSelected = true;
                    }

                }
            }
            
        }

        
        private void textBoxCoord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
        }
        private void textBoxDevide_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                        DevideCurveCube(nx, ny, nz,true);
                        PlotGL();
                        StandartCubeToolStripMenuItem.Enabled = false;
                        //myCube.EnabledTransform = false;
                    }
                    else { MessageBox.Show("Неправильні дані"); }
                }
                catch { MessageBox.Show("Неправильні дані"); }

            }
        }
    }
}
