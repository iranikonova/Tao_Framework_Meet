using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Tao.FreeGlut;
using Tao_Framework_Meet;

namespace Tao_Framework_Meet
{
    public partial class Form2 : Form
    {
       

        private void Form1_Load(object sender, EventArgs e)
        {
            AnT.InitializeContexts();
            AnT.SwapBuffers();

            AnT.Load += new EventHandler(AnT_Load);
            AnT.MouseMove += new System.Windows.Forms.MouseEventHandler(glOnMouseMove);
            AnT.SizeChanged += new EventHandler(AnT_SizeChanged);

            AnT_SizeChanged(sender, e);

            LastTransformation.SetIdentity(); // Reset Rotation
            ThisTransformation.SetIdentity(); // Reset Rotation
            ThisTransformation.get_Renamed(matrix);

            MouseControl mouseControl = new MouseControl(AnT);
            mouseControl.AddControl(AnT);
            mouseControl.LeftMouseDown += new MouseEventHandler(glOnLeftMouseDown);
            mouseControl.LeftMouseUp += new MouseEventHandler(glOnLeftMouseUp);
            mouseControl.RightMouseDown += new MouseEventHandler(glOnRightMouseDown);
            mouseControl.RightMouseUp += new MouseEventHandler(glOnRightMouseUp);
            mouseControl.MiddleMouseDown += new MouseEventHandler(glOnMiddleMouseDown);
            mouseControl.MiddleMouseUp += new MouseEventHandler(glOnMiddleMouseUp);

            foreach (Cube myCube in Model)
            {
                myCube.Origin = new Cube();
                myCube.TransCube = new Cube();
                myCube.Origin.Initialize(myCube.vertices);
            }
        }

        /*private void AnT_Load(object sender, EventArgs e)
        {
            this.PlotGL();
        }
       
        private void AnT_SizeChanged(object sender, EventArgs e)
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
            
            Gl.glTranslated(-3,0,-30);
            //Gl.glRotated(60, 0, 1, 0);
           // Gl.glScaled(2, 2, 2);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Gl.glPopMatrix();

            arcBall.setBounds((float)width, (float)height); // Update mouse bounds for arcball
            PlotGL();
        }*/

       /* public void reset()
        {
            lock (matrixLock)
            {
                LastTransformation.SetIdentity();                                // Reset Rotation
                ThisTransformation.SetIdentity();                                // Reset Rotation
            }

            this.PlotGL();
        }*/

        /*private void Draw()
        {
            myCube.Draw(true);
            foreach (Cube c in CubeParts)
            {
                c.Draw(false);
            }

        }
        void DrawText(string text, double posX, double posY, double posZ)
        {
            IntPtr hdc = Wgl.wglGetCurrentDC();
            Wgl.wglUseFontBitmapsA(hdc, 0, 255, 0);
            
            Gl.glListBase(0);
            Gl.glRasterPos3d(posX, posY, posZ);
            Gl.glCallLists(text.Length, Gl.GL_UNSIGNED_SHORT, text);
        }*/

       /* public void PlotGL()
        {
            try
            {
                lock (matrixLock)
                {
                    ThisTransformation.get_Renamed(matrix);
                }
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); // Clear screen and DepthBuffer
                Gl.glLoadIdentity();

               // Gl.glPushMatrix(); 
                // NEW: Prepare Dynamic Transform
                //if(fignya==true)
                Gl.glMultMatrixf(matrix);           // NEW: Apply Dynamic Transform

                Draw();

                DrawText("x", myCube.axes[1].x+0.3,myCube.axes[1].y,myCube.axes[1].z);
                DrawText("y", myCube.axes[2].x, myCube.axes[2].y + 0.3, myCube.axes[2].z);
                DrawText("z", myCube.axes[3].x, myCube.axes[3].y, myCube.axes[3].z + 0.3);
                
                //Print(0, 1, chars);
               /* Gl.glColor3d(1, 0, 0);
                Gl.glEnable(Gl.GL_POINT_SMOOTH);
                Gl.glPointSize(20);

                Gl.glBegin(Gl.GL_POINTS);

                Gl.glVertex3d(3, 1, 0);
                Gl.glEnd();
               // Gl.glPopMatrix(); // NEW: Unapply Dynamic Transform
                Gl.glFlush();     // Flush the GL Rendering Pipeline

                this.AnT.Invalidate();

            }
            catch
            {
                return;
            }

        }*/

        /*private void startDrag(Point MousePt)
        {
            lock (matrixLock)
            {
                LastTransformation.set_Renamed(ThisTransformation); // Set Last Static Rotation To Last Dynamic One
            }
            arcBall.click(MousePt); // Update Start Vector And Prepare For Dragging

            mouseStartDrag = MousePt;

        }

        private void drag(Point MousePt)
        {
            Quat4f ThisQuat = new Quat4f();

            arcBall.drag(MousePt, ThisQuat); // Update End Vector And Get Rotation As Quaternion

            lock (matrixLock)
            {
                if (isMiddleDrag) //zoom
                {
                    double len = Math.Sqrt(mouseStartDrag.X * mouseStartDrag.X + mouseStartDrag.Y * mouseStartDrag.Y)
                        / Math.Sqrt(MousePt.X * MousePt.X + MousePt.Y * MousePt.Y);

                    ThisTransformation.Scale = (float)len;
                    ThisTransformation.Pan = new Vector3f(0, 0, 0);
                    ThisTransformation.Rotation = new Quat4f();
                    ThisTransformation.MatrixMultiply(ThisTransformation, LastTransformation);// Accumulate Last Rotation Into This One
                }
                else if (isRightDrag) //pan
                {
                    float x = (float)(MousePt.X - mouseStartDrag.X) / (float)this.AnT.Width;
                    float y = (float)(MousePt.Y - mouseStartDrag.Y) / (float)this.AnT.Height;
                    float z = 0.0f;

                    ThisTransformation.Pan = new Vector3f(x, y, z);
                    ThisTransformation.Scale = 1.0f;
                    ThisTransformation.Rotation = new Quat4f();
                    ThisTransformation.MatrixMultiply(ThisTransformation, LastTransformation);
                }
                else if (isLeftDrag) //rotate
                {
                    ThisTransformation.Pan = new Vector3f(0, 0, 0);
                    ThisTransformation.Scale = 1.0f;
                    ThisTransformation.Rotation = ThisQuat;
                    ThisTransformation.MatrixMultiply(ThisTransformation, LastTransformation);
                }
            }
        }

        public void glOnMouseMove(object sender, MouseEventArgs e)
        {
            
            if ((fignya == true)&&(vertex.IsSelected==false))
            {
                Point tempAux = new Point(e.X, e.Y);

                this.drag(tempAux);
                this.PlotGL();
            }
        }

        public void glOnLeftMouseDown(object sender, MouseEventArgs e)
        {
          //  MessageBox.Show(vertex.IsSelected.ToString());
            if ((vertex.IsSelected == false))
            {
                isLeftDrag = true;
                mouseStartDrag = new Point(e.X, e.Y);
                this.startDrag(mouseStartDrag);
                this.PlotGL();
                fignya = true;
            }
            
        }

        public void glOnLeftMouseUp(object sender, MouseEventArgs e)
        {
            isLeftDrag = false;
            fignya = false;
        }

        private void glOnRightMouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeAll;
            isRightDrag = true;
            mouseStartDrag = new Point(e.X, e.Y);
            this.startDrag(mouseStartDrag);
            this.PlotGL();
            fignya = true;
        }

        private void glOnRightMouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            isRightDrag = false;
            fignya = false;
        }

        private void glOnMiddleMouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.NoMove2D;
            isMiddleDrag = true;
            mouseStartDrag = new Point(e.X, e.Y);
            this.startDrag(mouseStartDrag);
            this.PlotGL();
            fignya = true;
        }

        private void glOnMiddleMouseUp(object sender, MouseEventArgs e)
        {
           Cursor.Current = Cursors.Default;

            isMiddleDrag = false;
            fignya = false;
        }
        */

    }


   /* public class MouseControl
    {
        protected Control newCtrl;
        protected MouseButtons FinalClick;

        public event EventHandler LeftClick;
        public event EventHandler RightClick;
        public event EventHandler MiddleClick;

        public event MouseEventHandler LeftMouseDown;
        public event MouseEventHandler LeftMouseUp;
        public event MouseEventHandler RightMouseDown;
        public event MouseEventHandler RightMouseUp;

        public event MouseEventHandler MiddleMouseDown;
        public event MouseEventHandler MiddleMouseUp;



        public Control Control
        {
            get { return newCtrl; }
            set
            {
                newCtrl = value;
                Initialize();
            }
        }

        public MouseControl()
        {
        }

        public MouseControl(Control ctrl)
        {
            Control = ctrl;
        }

        public void AddControl(Control ctrl)
        {
            Control = ctrl;
        }

        protected virtual void Initialize()
        {
            newCtrl.Click += new EventHandler(OnClick);
            newCtrl.MouseDown += new MouseEventHandler(OnMouseDown);
            newCtrl.MouseUp += new MouseEventHandler(OnMouseUp);



        }

        private void OnClick(object sender, EventArgs e)
        {
            switch (FinalClick)
            {
                case MouseButtons.Left:
                    if (LeftClick != null)
                    {
                        LeftClick(sender, e);
                    }
                    break;

                case MouseButtons.Right:
                    if (RightClick != null)
                    {
                        RightClick(sender, e);
                    }
                    break;
            }
        }




        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            FinalClick = e.Button;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (LeftMouseDown != null)
                        {
                            LeftMouseDown(sender, e);
                        }
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        if (MiddleMouseDown != null)
                        {
                            MiddleMouseDown(sender, e);
                        }
                        break;
                    }

                
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (LeftMouseUp != null)
                        {
                            LeftMouseUp(sender, e);
                        }
                        break;
                    }
                case MouseButtons.Middle:
                    {
                        if (MiddleMouseUp != null)
                        {
                            MiddleMouseUp(sender, e);
                        }
                        break;
                    }

                case MouseButtons.Right:
                    {
                        if (RightMouseUp != null)
                        {
                            RightMouseUp(sender, e);
                        }
                        break;
                    }
            }
        }
    }*/

}
