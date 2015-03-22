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
    public partial class Form0 : Form
    {
         CubeEdge selectedEdge_;

        public bool EdgeHasPoint(_Point a, _Point begin,_Point end )
        {
            double p = (a.x - end.x) / (begin.x - end.x);
            if ((((p * begin.y + (1 - p) * end.y)) == a.y)&&(p>=0)&&(p<=1))
                return true;
            else
                return false;
        }

        public void EdgeSelection(_Point p)
        {
            foreach (Cube myCube in Model)
            {
                foreach (CubeEdge p1 in myCube.edges)
                {
                   // if (p1 != selected)
                    p1.IsPointed = false;
                }
            }

            foreach (Cube myCube in Model)
            {
                foreach (CubeEdge p1 in myCube.edges)
                {
                    Edge[] polygon = p1.eCubeEdge(AnT.Height * 1.0);
                    /*txtMouseX.Text = polygon[0].end.x.ToString();
                    txtMouseY.Text = p.x.ToString();

                    txtMouseZ.Text = polygon[0].end.y.ToString();
                    textBox1.Text = p.y.ToString();*/

                    if ((Math.Abs(polygon[0].end.x - p.x) <= 2) && (Math.Abs(polygon[0].end.y - p.y) <= 2))
                    {
                        p1.IsPointed =true;
                        selectedEdge_ = p1;
                        //MessageBox.Show("yahoo!!");
                        return;
                    }
                }
            }
        }

        public string EdgeType(_Point a, Edge e)
        {
            _Point v = e.begin;
            _Point w = e.end;
            switch (a.Classify(e))
            {
                case "LEFT":
                    if ((v.y < a.y) && (a.y <= w.y))
                        return "CROSSING";
                    else
                        return "INESSENTIAL";

                case "RIGHT":
                    if ((w.y < a.y) && (a.y <= v.y))
                        return "CROSSING";
                    else
                        return "INESSENTIAL";
                case "BETWEEN":
                case "BEGIN":
                case "END":
                    return "TOUCHING";
                default:
                    return "INESSENTIAL";

            }
        }

        public int PointInPolygon(Edge[] polygon, _Point p)
        {
            int parity = 0;
            for (int i = 0; i < polygon.Length; i++)
            {
                switch (EdgeType(p, polygon[i]))
                {
                    case "TOUCHING":
                    case "CROSSING":
                        parity++;
                        break;
                }
            }
            if ((parity % 2) == 0)
                return 0;
            else
                return 1;
        }

        public int k = 0;
        public void SideSelection(_Point p)
        {
            string[] sideSelection = new string[6];
            int i = 0;
            foreach (Cube myCube in Model)
            {
                foreach (Polygon p1 in myCube.faces)
                {
                    //if (p1 != selected)
                    p1.IsPointed = false;
                }
            }

            List<Polygon> SelectedSides = new List<Polygon>();
            i = 0;
            foreach (Cube myCube in Model)
            {
                foreach (Polygon p1 in myCube.faces)
                {
                    Edge[] polygon = p1.ePolygon(AnT.Height * 1.0);
                    if (PointInPolygon(polygon, p) == 1)
                    {
                        SelectedSides.Add(p1);
                        p1.IsPointed = true;
                        i++;
                    }
                    
                }
            }
                k=i;
            //textBox1.Text = i.ToString();

            double z = 0;
            double min = 1;
            Polygon selected = new Polygon();
            foreach (Polygon p1 in SelectedSides)
            {
                Polygon temp = p1.Depth(out z);
                if (z < min)
                {
                    selected = temp;
                    min = z;
                }
            }

            foreach (Cube myCube in Model)
            {
                foreach (Polygon p1 in myCube.faces)
                {
                    if (p1 != selected)
                        p1.IsPointed = false;
                }
            }
            selected.IsPointed = true;
            selectedSide = selected;

            
        }

        public void SelectSide(MouseEventArgs e)
        {
            SideSelection(new _Point(e.X * 1.0, e.Y * 1.0));
        }

        public void selectEdge(MouseEventArgs e)
        {
            EdgeSelection(new _Point(e.X * 1.0, e.Y * 1.0));
        }
    }
}
