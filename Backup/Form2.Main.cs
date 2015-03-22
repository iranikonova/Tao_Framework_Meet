using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace Tao_Framework_Meet
{
    public partial class Form2 : Form
    {
        private static int height = 0;
        private static int width = 0;

        // Rotation/Zoom/Pan
        private System.Object matrixLock = new System.Object();
        private arcball arcBall = new arcball(640.0f, 480.0f);
        private float[] matrix = new float[16];
        private Matrix4f LastTransformation = new Matrix4f();
        private Matrix4f ThisTransformation = new Matrix4f();

        // mouse 
        private Point mouseStartDrag;
        private static bool isLeftDrag = false;
        private static bool isRightDrag = false;
        private static bool isMiddleDrag = false;

        //Graphic objects
        //Cube myCube = new Cube();
        List<Cube> Model = new List<Cube>();
        List<Cube> standartParts = new List<Cube>();
        int nx = 0, ny = 0, nz = 0;
        //List<Cube> CubeParts = new List<Cube>();

        //?????

        //Translated Vertex
        Vertex vertex = new Vertex();
        Polygon selectedSide;
        //TranslationPos
        double trans_x = 0, trans_y = 0, trans_z = -10;

        //Tools
        bool IsAddingCube = false;

        Cube RemovingCube = new Cube();
        bool IsRemovingCube;

        //scale
        double mouseStartXScale = -1;
        double mouseStartYScale = -1;
        double mouseEndXScale = 0;
        double mouseEndYScale = 0;

        List<Cube> scaledShapes = new List<Cube>();
        List<List<Vertex>> scaledVerticies = new List<List<Vertex>>();

        List<Cube> scaledFaces = new List<Cube>();
        


        bool isScaled = false;

        public Form2()
        {
            InitializeComponent();

            //Початкове розбиття
            textBoxNx.Text = "1";
            textBoxNy.Text = "1";
            textBoxNz.Text = "1";

            //this.WindowState = FormWindowState.Maximized;

            Cube first = new Cube();
           /* Cube second = new Cube();
            second.Translate(2, 0, 0);
            Cube third = new Cube();
            third.Translate(-2, 0, 0);*/
            Model.Add(first);
           // Model.Add(second);
           // Model.Add(third);
        }

        

        private void AnT_Load(object sender, EventArgs e)
        {
            this.PlotGL();
        }

        public void RedrawScene(double transX, double transY, double transZ)
        {

            Size s = AnT.Size;
            height = s.Height;
            width = s.Width;


            Gl.glShadeModel(Gl.GL_SMOOTH);								// enable smooth shading
            Gl.glClearColor(0.8f, 0.8f, 0.9f, 0.0f);					// black background
            Gl.glClearDepth(1.0f);										// depth buffer setup
            Gl.glEnable(Gl.GL_DEPTH_TEST);								// enables depth testing
            Gl.glDepthFunc(Gl.GL_LEQUAL);								// type of depth testing
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);	// nice perspective calculations

            Gl.glViewport(0, 0, width, height);

            Gl.glPushMatrix();
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(40.0, (double)width / (double)height, 1.0, 200);
            Gl.glTranslated(transX,transY,transZ);
            Gl.glRotated(-20, 0, 1, 0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPopMatrix();


            arcBall.setBounds((float)width, (float)height); // Update mouse bounds for arcball
            PlotGL();
        }

        private void AnT_SizeChanged(object sender, EventArgs e)
        {
            RedrawScene(0, 0 ,- 10);
            
        }

        public void reset()
        {
            lock (matrixLock)
            {
                LastTransformation.SetIdentity();                                // Reset Rotation
                ThisTransformation.SetIdentity();                                // Reset Rotation
            }

            this.PlotGL();
        }

        //Drawing
        private void Draw()
        {
            
            foreach (Cube myCube in Model)
            {
                 if ((myCube.Parts.Count == 0) )
                {
                    
                    myCube.Draw(true, false);
                    //myCube.TransCube.Draw1();
                    //myCube.TransCube.Draw1();
                    
                }
                else
                    myCube.Draw(true, true);
                if (!myCube.IsSelected)
                {
                    foreach (Cube part in myCube.Parts)
                        part.Draw(false, false);
                }
            }

        }
        private void Draw1()
        {/*
            double org = myCube.Origin.faces[3].MinX();
            double now = myCube.faces[3].MinX();
            if (now>org)
            {
                myCube.Origin.Translate(2 + (now - org), 0, 0);
            }
            else
            {
                myCube.Origin.Translate(2 - (org-now), 0, 0);
            }
            if ((CubeParts.Count == 0) || (CubeParts.Count == 1))
            {
                myCube.Draw(true, false);
                myCube.Origin.Draw(true, false);
            }
           /* else
            {
                myCube.Draw(true, true);
                myCube.Origin.Draw(true, false);
            }
            foreach (Cube c in CubeParts)
            {
                c.Draw(false, false);
            }*/

        }
        void DrawText(string text, double posX, double posY, double posZ)
        {
            IntPtr hdc = Wgl.wglGetCurrentDC();
            Wgl.wglUseFontBitmapsA(hdc, 0, 255, 0);

            Gl.glListBase(0);
            Gl.glRasterPos3d(posX, posY, posZ);
            Gl.glCallLists(text.Length, Gl.GL_UNSIGNED_SHORT, text);
        }
        public void PlotGL1()
        {
            try
            {
                lock (matrixLock)
                {
                    ThisTransformation.get_Renamed(matrix);
                }
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); // Clear screen and DepthBuffer
                Gl.glLoadIdentity();

                Gl.glMultMatrixf(matrix);           // NEW: Apply Dynamic Transform

                //Drawing smth/
                Draw1();


                /* DrawText("x", myCube.axes[1].x + 0.3, myCube.axes[1].y, myCube.axes[1].z);
                 DrawText("y", myCube.axes[2].x, myCube.axes[2].y + 0.3, myCube.axes[2].z);
                 DrawText("z", myCube.axes[3].x, myCube.axes[3].y, myCube.axes[3].z + 0.3);*/

                Gl.glFlush();     // Flush the GL Rendering Pipeline

                this.AnT.Invalidate();
            }
            catch
            {
                return;
            }
        }

        public void PlotGL()
        {
            try
            {
                lock (matrixLock)
                {
                    ThisTransformation.get_Renamed(matrix);
                }
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); // Clear screen and DepthBuffer
                Gl.glLoadIdentity();

                Gl.glMultMatrixf(matrix);           // NEW: Apply Dynamic Transform

                //Drawing smth/
                Draw();


               /* DrawText("x", myCube.axes[1].x + 0.3, myCube.axes[1].y, myCube.axes[1].z);
                DrawText("y", myCube.axes[2].x, myCube.axes[2].y + 0.3, myCube.axes[2].z);
                DrawText("z", myCube.axes[3].x, myCube.axes[3].y, myCube.axes[3].z + 0.3);*/

                Gl.glFlush();     // Flush the GL Rendering Pipeline

                this.AnT.Invalidate();
            }
            catch
            {
                return;
            }
        }

        //MouseControl
        private void startDrag(Point MousePt)
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
                    float x = (float)(MousePt.X - mouseStartDrag.X) / (float)((this.AnT.Width)*10);
                    float y = (float)(MousePt.Y - mouseStartDrag.Y) / (float)((this.AnT.Height)*10);
                    float z = 0.0f;
                    trans_x += (double)x;
                    trans_y += (double)y;
                    trans_z += (double)z;
                    RedrawScene(trans_x,-1*trans_y,trans_z);
                   /* ThisTransformation.Pan = new Vector3f(x, -1*y, z);
                    ThisTransformation.Scale = 1.0f;
                    ThisTransformation.Rotation = new Quat4f();
                    ThisTransformation.MatrixMultiply(ThisTransformation, LastTransformation);*/
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

        bool cursorInside(_Point p)
        {
            foreach (Cube myCube in Model)
            {
                foreach (Polygon p1 in myCube.faces)
                {
                    Edge[] polygon = p1.ePolygon(AnT.Height * 1.0);
                    if (PointInPolygon(polygon, p) == 1)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        double ScaleCoeff(_Point p)
        {
            double xScale;

            if (cursorInside(p))
            {
                 xScale = 1 - Math.Abs(mouseEndXScale - mouseStartXScale) / 100;
            }
            else
            {
                 xScale = 1 + Math.Abs(mouseEndXScale - mouseStartXScale) / 100;
            }
            //txtMouseX.Text = xScale.ToString();
            //txtMouseY.Text = mouseStartXScale.ToString();
            //txtMouseZ.Text = mouseEndXScale.ToString();
            if (xScale < 0)
                return 0.3;
            return xScale;
        }

        public void LockFace()
        {
            if ((selectedSide.Parent != null) && (selectedSide.Parent.Origin != null))
            {
                if (selectedSide.lockState.enableChange)
                {
                    selectedSide.lockState.lockStatus = true;
                    selectedSide.IsPointed = false;
                }
            }
        }

        public void LockEdge()
        {
            if (selectedEdge_!= null) 
            {
                if (selectedEdge_.lockState.enableChange)
                {
                    selectedEdge_.lockState.lockStatus = true;
                    selectedEdge_.IsPointed = false;
                }
            }
        }

        public void UnlockEdge()
        {
            if (selectedEdge_ != null)
            {
                if (selectedEdge_.lockState.enableChange)
                {
                    selectedEdge_.lockState.lockStatus = false;
                    selectedEdge_.IsPointed = false;
                }
                else {
                    selectedEdge_.IsPointed = false;
                }
            }
        }

        public void UnlockFace()
        {
            if ((selectedSide.Parent != null) && (selectedSide.Parent.Origin != null))
            {
                if (selectedSide.lockState.enableChange)
                {
                    selectedSide.lockState.lockStatus = false;
                    selectedSide.IsPointed = false;
                }
                else {
                    selectedSide.IsPointed = false;
                }
            }
        }

        public void glOnMouseMove(object sender, MouseEventArgs e)
        {
            try {
                //txtMouseX.Text = selectedSide.lockState.lockStatus.ToString();
                //txtMouseY.Text = selectedSide.lockState.lockStatus.ToString();
            }
            catch { }
            
            if ((vertex.IsSelected == false)&&(!isScaled))
            {
                Point tempAux = new Point(e.X, e.Y);

                this.drag(tempAux);
                
            }
            if ((isLockingFace)||(isUnlockingFace))
            {
                SelectSide(e);
                this.PlotGL();
                return;
            }
            if ((isLockingEdge) )
            {
                //SelectSide(e);
                selectEdge(e);
                this.PlotGL();
                return;
            }

            if ((isUnlockingEdge))
            {
                //SelectSide(e);
                selectEdge(e);
                this.PlotGL();
                return;
            }
            if ((IsAddingCube == false) && (IsRemovingCube == false))
            {
                Vertex mouseCoord = TranslateVertex(e);
                /*txtMouseX.Text = mouseCoord.x.ToString();
                txtMouseY.Text = mouseCoord.y.ToString();
                txtMouseZ.Text = mouseCoord.z.ToString();*/
            }
            else
            {
                if (IsRemovingCube == false)
                {
                    SelectSide(e);
                }
                else
                {
                    SelectSide(e);
                    SelectCube();
                }
            }

            if (AxisName == "x")
            {
                mouseEndXScale = e.X;
            }

            if ((AxisName == "y") || (AxisName == "z"))
            {
                mouseEndXScale = e.Y;
            }

            //ScaleCoeff();
            if ((isScaled)&&(!((mouseStartXScale==-1)&&(mouseStartYScale==-1))))
            {
               // ResizeShape(Model[0], ScaleCoeff(), 1, 1);
                if (AxisName == "x")
                {
                    ResizeShapes(ScaleCoeff(new _Point(e.X * 1.0, e.Y * 1.0)), 1, 1);
                }

                if (AxisName == "y") 
                {
                    ResizeShapes(1, ScaleCoeff(new _Point(e.X * 1.0, e.Y * 1.0)), 1);
                }

                if (AxisName == "z")
                {
                    ResizeShapes(1, 1, ScaleCoeff(new _Point(e.X * 1.0, e.Y * 1.0)));
                }

                //ResizeShapes(ScaleCoeff(new _Point(e.X*1.0,e.Y*1.0)), 1, 1);
                ScaleUpdate();
            }

            this.PlotGL();

            

        }

        /// <summary>
        /// start rotation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        void ScaleUpdate()
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
        void DefineScaledShapes()
        {
            scaledShapes.Clear();

            if (GlobalVar.scaleType == 1)
            {
                scaledVerticies.Clear();

                scaledShapes.Add(Model[0]);
                foreach (Cube c in scaledShapes)
                {
                    List<Vertex> shapeVerticies = new List<Vertex>();
                    for (int i = 0; i < 20; i++)
                    {
                        Vertex v = new Vertex();
                        v.x = c.vertices[i].x;
                        v.y = c.vertices[i].y;
                        v.z = c.vertices[i].z;
                        shapeVerticies.Add(v);
                    }
                    scaledVerticies.Add(shapeVerticies);
                }
            }

            if (GlobalVar.scaleType == 2)
            {
                scaledVerticies.Clear();

                
                foreach (Cube c in scaledShapes)
                {
                    List<Vertex> shapeVerticies = new List<Vertex>();
                    for (int i = 0; i < 6; i++)
                    {
                        if (c.faces[i].Equals(GlobalVar.polygonToScale))
                        {
                            scaledShapes.Add(c);

                            for (int j = 0; j < c.faces[i].vertices.Length; j++)
                            {
                                Vertex v = new Vertex();
                                v.x = c.faces[i].vertices[j].x;
                                v.y = c.faces[i].vertices[j].y;
                                v.z = c.faces[i].vertices[j].z;
                                shapeVerticies.Add(v);
                            }
                        }
                    }
                    scaledVerticies.Add(shapeVerticies);
                }
            }
        }

        public void glOnLeftMouseDown(object sender, MouseEventArgs e)
        {
            isLeftDrag = true;
            mouseStartDrag = new Point(e.X, e.Y);
            this.startDrag(mouseStartDrag);

            if (IsAddingCube)
            {
                if (Model.Count == 0)
                {
                    Cube first = new Cube();
                    first.Origin = new Cube();
                    first.TransCube = new Cube();
                    Model.Add(first);
                }
                else
                    AddCube();
            }
            if (isLockingFace)
            {
                LockFace();
                isLockingFace = false;
                //MessageBox.Show("Lock it");
            }
            if (isUnlockingFace)
            {
                UnlockFace();
                isUnlockingFace = false;
            }
            if (isLockingEdge)
            {
                isLockingEdge = false;
                LockEdge();
                //isLockingEdge = !(isLockingEdge);
                GlobalVar.cubeDrawMode = 1;
                //MessageBox.Show("Lock edge");
                GlobalVar.cubeDrawMode = 0;

            }

            if (isUnlockingEdge)
            {
                isUnlockingEdge = false;
                UnlockEdge();
                //isLockingEdge = !(isLockingEdge);
                //GlobalVar.cubeDrawMode = 1;
                //MessageBox.Show("UnLock edge");
                GlobalVar.cubeDrawMode = 0;

            }


            
            if ((IsRemovingCube) && (k>0))
            {
                RemoveCube();
            }
            this.PlotGL();

            DefineScaledShapes();
            if (AxisName == "x")
            {
                mouseStartXScale = e.X;
            }
            if ((AxisName == "y") || (AxisName == "z"))
            {
                mouseStartXScale = e.Y;
            }
            mouseStartYScale = e.Y;
        }

        /// <summary>
        /// end rotation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void glOnLeftMouseUp(object sender, MouseEventArgs e)
        {
            isLeftDrag = false;
            
            //vertex translation
            SetVertexState();

            mouseStartXScale = -1;
            mouseStartYScale = -1;
            isScaled = false;
            GlobalVar.scaleType = 1;
        }

        /// <summary>
        /// start pan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glOnRightMouseDown(object sender, MouseEventArgs e)
        {
            if(vertex.IsSelected==false)
                Cursor.Current = Cursors.SizeAll;
            isRightDrag = true;
            mouseStartDrag = new Point(e.X, e.Y);
            this.startDrag(mouseStartDrag);
            this.PlotGL();
        }

        /// <summary>
        /// end pan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glOnRightMouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            isRightDrag = false;

            //vertex translation
            SetVertexState();
        }

        /// <summary>
        /// start zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glOnMiddleMouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.NoMove2D;
            isMiddleDrag = true;
            mouseStartDrag = new Point(e.X, e.Y);
            this.startDrag(mouseStartDrag);
            this.PlotGL();
        }

        /// <summary>
        /// end zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glOnMiddleMouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            isMiddleDrag = false;
        }
    }

    /*public class MouseControl
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

                case MouseButtons.Right:
                    {
                        if (RightMouseDown != null)
                        {
                            RightMouseDown(sender, e);
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
