using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tao_Framework_Meet
{
    public partial class Form2 : Form
    {

        void InitializeEdge(ref Cube edited, ref Cube c,int edge)
        {
            switch (edge)
            { 
                case 1:
                    if (((edited.TransCube.vertices[0].Equals(c.TransCube.vertices[7])) && (edited.TransCube.vertices[1].Equals(c.TransCube.vertices[6]))))
                        {
                            edited.vertices[0] = c.vertices[7];
                            edited.vertices[1] = c.vertices[6];
                            edited.vertices[8] = c.vertices[18];
                            edited.UpdateFace();
                            
                        }
                    
                    break;

                case 2:
                        if (((edited.TransCube.vertices[1].Equals(c.TransCube.vertices[4])) && (edited.TransCube.vertices[2].Equals(c.TransCube.vertices[7]))))
                        {
                            edited.vertices[1] = c.vertices[4];
                            edited.vertices[2] = c.vertices[7];
                            edited.vertices[9] = c.vertices[19];
                            edited.UpdateFace();
                        }
                    break;


                case 3:
                        if (((edited.TransCube.vertices[2].Equals(c.TransCube.vertices[5])) && (edited.TransCube.vertices[3].Equals(c.TransCube.vertices[4]))))
                        {
                            edited.vertices[2] = c.vertices[5];
                            edited.vertices[3] = c.vertices[4];
                            edited.vertices[10] = c.vertices[16];
                            edited.UpdateFace();
                        }
                    break;

                case 4:
                        if (((edited.TransCube.vertices[0].Equals(c.TransCube.vertices[5])) && (edited.TransCube.vertices[3].Equals(c.TransCube.vertices[6]))))
                        {
                            edited.vertices[0] = c.vertices[5];
                            edited.vertices[3] = c.vertices[6];
                            edited.vertices[11] = c.vertices[17];
                            edited.UpdateFace();
                        }
                    break;

                case 5:
                    if (((edited.TransCube.vertices[4].Equals(c.TransCube.vertices[3])) && (edited.TransCube.vertices[5].Equals(c.TransCube.vertices[2]))))
                        {
                            edited.vertices[4] = c.vertices[3];
                            edited.vertices[5] = c.vertices[2];
                            edited.vertices[16] = c.vertices[10];
                            edited.UpdateFace();
                        }
                    break;

                case 6:
                    if (((edited.TransCube.vertices[5].Equals(c.TransCube.vertices[0])) && (edited.TransCube.vertices[6].Equals(c.TransCube.vertices[3]))))
                        {
                            edited.vertices[5] = c.vertices[0];
                            edited.vertices[6] = c.vertices[3];
                            edited.vertices[17] = c.vertices[11];
                            edited.UpdateFace();
                        }
                    break;

                case 7:
                    if (((edited.TransCube.vertices[7].Equals(c.TransCube.vertices[0])) && (edited.TransCube.vertices[6].Equals(c.TransCube.vertices[1]))))
                        {
                            edited.vertices[7] = c.vertices[0];
                            edited.vertices[6] = c.vertices[1];
                            edited.vertices[18] = c.vertices[8];
                            edited.UpdateFace();
                        }
                    break;

                case 8:
                    if (((edited.TransCube.vertices[4].Equals(c.TransCube.vertices[1])) && (edited.TransCube.vertices[7].Equals(c.TransCube.vertices[2]))))
                        {
                            edited.vertices[4] = c.vertices[1];
                            edited.vertices[7] = c.vertices[2];
                            edited.vertices[19] = c.vertices[9];
                            edited.UpdateFace();
                        }
                    break;

                case 9:
                    if (((edited.TransCube.vertices[0].Equals(c.TransCube.vertices[2])) && (edited.TransCube.vertices[4].Equals(c.TransCube.vertices[6]))))
                        {
                            edited.vertices[0] = c.vertices[2];
                            edited.vertices[4] = c.vertices[6];
                            edited.vertices[12] = c.vertices[14];
                            edited.UpdateFace();
                        }
                    break;

                case 10:

                        if (((edited.TransCube.vertices[5].Equals(c.TransCube.vertices[7])) && (edited.TransCube.vertices[1].Equals(c.TransCube.vertices[3]))))
                        {
                            edited.vertices[5] = c.vertices[7];
                            edited.vertices[1] = c.vertices[3];
                            edited.vertices[13] = c.vertices[15];
                            edited.UpdateFace();
                        }
                    break;

                case 11:
                        if (((edited.TransCube.vertices[6].Equals(c.TransCube.vertices[4])) && (edited.TransCube.vertices[2].Equals(c.TransCube.vertices[0]))))
                        {
                            edited.vertices[6] = c.vertices[4];
                            edited.vertices[2] = c.vertices[0];
                            edited.vertices[14] = c.vertices[12];
                            edited.UpdateFace();
                        }
                    break;

                case 12:
                        if (((edited.TransCube.vertices[7].Equals(c.TransCube.vertices[5])) && (edited.TransCube.vertices[3].Equals(c.TransCube.vertices[1]))))
                        {
                            edited.vertices[7] = c.vertices[5];
                            edited.vertices[3] = c.vertices[1];
                            edited.vertices[15] = c.vertices[13];
                            edited.UpdateFace();
                        }
                    
                    break;


            }
        }

        void InitializeFace(ref Cube edited, string face)
        {
            switch (face)
            {
                case "minY":
                    foreach (Cube c in Model)
                    {
                        Cube c1 = c;
                        InitializeEdge(ref edited, ref c1, 1);
                        InitializeEdge(ref edited, ref c1, 2);
                        InitializeEdge(ref edited, ref c1, 3);
                        InitializeEdge(ref edited, ref c1, 4);

                    }
                    break;

                case "maxY":
                    foreach (Cube c in Model)
                    {
                        Cube c1 = c;
                        InitializeEdge(ref edited, ref c1, 5);
                        InitializeEdge(ref edited, ref c1, 6);
                        InitializeEdge(ref edited, ref c1, 7);
                        InitializeEdge(ref edited, ref c1, 8);

                    }
                    break;

                case "minX":
                    foreach (Cube c in Model)
                    {
                        Cube c1 = c;
                        InitializeEdge(ref edited, ref c1, 4);
                        InitializeEdge(ref edited, ref c1, 12);
                        InitializeEdge(ref edited, ref c1, 8);
                        InitializeEdge(ref edited, ref c1, 9);

                    }
                    break;

                case "maxX":
                    foreach (Cube c in Model)
                    {
                        Cube c1 = c;
                        InitializeEdge(ref edited, ref c1, 2);
                        InitializeEdge(ref edited, ref c1, 11);
                        InitializeEdge(ref edited, ref c1, 6);
                        InitializeEdge(ref edited, ref c1, 10);

                    }
                    break;

                case "minZ":
                    foreach (Cube c in Model)
                    {
                        Cube c1 = c;
                        InitializeEdge(ref edited, ref c1, 3);
                        InitializeEdge(ref edited, ref c1, 11);
                        InitializeEdge(ref edited, ref c1, 7);
                        InitializeEdge(ref edited, ref c1, 12);

                    }
                    break;

                case "maxZ":
                    foreach (Cube c in Model)
                    {
                        Cube c1 = c;
                        InitializeEdge(ref edited, ref c1, 1);
                        InitializeEdge(ref edited, ref c1, 10);
                        InitializeEdge(ref edited, ref c1, 5);
                        InitializeEdge(ref edited, ref c1, 9);

                    }
                    break;

                
            }
        }

        void InitializeFace(ref Cube newCube, ref Cube oldCube, string face)
        {
            switch (face)
            {
                case "minY":

                    newCube.vertices[4] = oldCube.vertices[0];
                    newCube.vertices[16] = oldCube.vertices[8];
                    newCube.vertices[5] = oldCube.vertices[1];
                    newCube.vertices[17] = oldCube.vertices[9];
                    newCube.vertices[6] = oldCube.vertices[2];
                    newCube.vertices[18] = oldCube.vertices[10];
                    newCube.vertices[7] = oldCube.vertices[3];
                    newCube.vertices[19] = oldCube.vertices[11];

                    newCube.UpdateFace();

                    break;

                case "maxY":

                    newCube.vertices[0] = oldCube.vertices[4];
                    newCube.vertices[8] = oldCube.vertices[16];
                    newCube.vertices[1] = oldCube.vertices[5];
                    newCube.vertices[9] = oldCube.vertices[17];
                    newCube.vertices[2] = oldCube.vertices[6];
                    newCube.vertices[10] = oldCube.vertices[18];
                    newCube.vertices[3] = oldCube.vertices[7];
                    newCube.vertices[11] = oldCube.vertices[19];
                    newCube.UpdateFace();
                    break;

                case "minX":

                    newCube.vertices[5] = oldCube.vertices[4];
                    newCube.vertices[13] = oldCube.vertices[12];
                    newCube.vertices[1] = oldCube.vertices[0];
                    newCube.vertices[9] = oldCube.vertices[11];
                    newCube.vertices[14] = oldCube.vertices[15];
                    newCube.vertices[6] = oldCube.vertices[7];
                    newCube.vertices[17] = oldCube.vertices[19];
                    newCube.vertices[2] = oldCube.vertices[3];
                    newCube.UpdateFace();
                    break;

                case "maxX":

                    newCube.vertices[4] = oldCube.vertices[5];
                    newCube.vertices[12] = oldCube.vertices[13];
                    newCube.vertices[0] = oldCube.vertices[1];
                    newCube.vertices[11] = oldCube.vertices[9];
                    newCube.vertices[15] = oldCube.vertices[14];
                    newCube.vertices[7] = oldCube.vertices[6];
                    newCube.vertices[19] = oldCube.vertices[17];
                    newCube.vertices[3] = oldCube.vertices[2];
                    newCube.UpdateFace();

                    break;

                case "minZ":
                    newCube.vertices[0] = oldCube.vertices[3];
                    newCube.vertices[8] = oldCube.vertices[10];
                    newCube.vertices[1] = oldCube.vertices[2];
                    newCube.vertices[13] = oldCube.vertices[14];
                    newCube.vertices[5] = oldCube.vertices[6];
                    newCube.vertices[16] = oldCube.vertices[18];
                    newCube.vertices[4] = oldCube.vertices[7];
                    newCube.vertices[12] = oldCube.vertices[15];
                    newCube.UpdateFace();

                    break;

                case "maxZ":

                    newCube.vertices[3] = oldCube.vertices[0];
                    newCube.vertices[10] = oldCube.vertices[8];
                    newCube.vertices[2] = oldCube.vertices[1];
                    newCube.vertices[14] = oldCube.vertices[13];
                    newCube.vertices[6] = oldCube.vertices[5];
                    newCube.vertices[18] = oldCube.vertices[16];
                    newCube.vertices[7] = oldCube.vertices[4];
                    newCube.vertices[15] = oldCube.vertices[12];
                    newCube.UpdateFace();
                    break;
            }
        }

        void UpdateEdges(ref Cube c)
        {
            for(int i =0; i<Model.Count;i++)
            {
                Cube c1 = Model[i];

                if (c.TransCube.edges[0].Equals(c1.TransCube.edges[1]))
                    c.edges[0].Set(ref c1.edges[1]);
                if (c.TransCube.edges[0].Equals(c1.TransCube.edges[2]))
                    c.edges[0].Set(ref c1.edges[2]);
                if (c.TransCube.edges[0].Equals(c1.TransCube.edges[3]))
                    c.edges[0].Set(ref c1.edges[3]);

                if (c.TransCube.edges[1].Equals(c1.TransCube.edges[2]))
                    c.edges[1].Set(ref c1.edges[2]);
                if (c.TransCube.edges[1].Equals(c1.TransCube.edges[3]))
                    c.edges[1].Set(ref c1.edges[3]);
                if (c.TransCube.edges[1].Equals(c1.TransCube.edges[0]))
                    c.edges[1].Set(ref c1.edges[0]);

                if (c.TransCube.edges[2].Equals(c1.TransCube.edges[3]))
                    c.edges[2].Set(ref c1.edges[3]);
                if (c.TransCube.edges[2].Equals(c1.TransCube.edges[0]))
                    c.edges[2].Set(ref c1.edges[0]);
                if (c.TransCube.edges[2].Equals(c1.TransCube.edges[1]))
                    c.edges[2].Set(ref c1.edges[1]);

                if (c.TransCube.edges[3].Equals(c1.TransCube.edges[0]))
                    c.edges[3].Set(ref c1.edges[0]);
                if (c.TransCube.edges[3].Equals(c1.TransCube.edges[1]))
                    c.edges[3].Set(ref c1.edges[1]);
                if (c.TransCube.edges[3].Equals(c1.TransCube.edges[2]))
                    c.edges[3].Set(ref c1.edges[2]);

                if (c.TransCube.edges[4].Equals(c1.TransCube.edges[8]))
                    c.edges[4].Set(ref c1.edges[8]);
                if (c.TransCube.edges[4].Equals(c1.TransCube.edges[10]))
                    c.edges[4].Set(ref c1.edges[10]);
                if (c.TransCube.edges[4].Equals(c1.TransCube.edges[6]))
                    c.edges[4].Set(ref c1.edges[6]);

                if (c.TransCube.edges[5].Equals(c1.TransCube.edges[9]))
                    c.edges[5].Set(ref c1.edges[9]);
                if (c.TransCube.edges[5].Equals(c1.TransCube.edges[7]))
                    c.edges[5].Set(ref c1.edges[7]);
                if (c.TransCube.edges[5].Equals(c1.TransCube.edges[11]))
                    c.edges[5].Set(ref c1.edges[11]);

                if (c.TransCube.edges[6].Equals(c1.TransCube.edges[4]))
                    c.edges[6].Set(ref c1.edges[4]);
                if (c.TransCube.edges[6].Equals(c1.TransCube.edges[10]))
                    c.edges[6].Set(ref c1.edges[10]);
                if (c.TransCube.edges[6].Equals(c1.TransCube.edges[8]))
                    c.edges[6].Set(ref c1.edges[8]);

                if (c.TransCube.edges[7].Equals(c1.TransCube.edges[9]))
                    c.edges[7].Set(ref c1.edges[9]);
                if (c.TransCube.edges[7].Equals(c1.TransCube.edges[5]))
                    c.edges[7].Set(ref c1.edges[5]);
                if (c.TransCube.edges[7].Equals(c1.TransCube.edges[11]))
                    c.edges[7].Set(ref c1.edges[11]);

                if (c.TransCube.edges[8].Equals(c1.TransCube.edges[4]))
                    c.edges[8].Set(ref c1.edges[4]);
                if (c.TransCube.edges[8].Equals(c1.TransCube.edges[6]))
                    c.edges[8].Set(ref c1.edges[6]);
                if (c.TransCube.edges[8].Equals(c1.TransCube.edges[10]))
                    c.edges[8].Set(ref c1.edges[10]);

                if (c.TransCube.edges[9].Equals(c1.TransCube.edges[11]))
                    c.edges[9].Set(ref c1.edges[11]);
                if (c.TransCube.edges[9].Equals(c1.TransCube.edges[7]))
                    c.edges[9].Set(ref c1.edges[7]);
                if (c.TransCube.edges[9].Equals(c1.TransCube.edges[5]))
                    c.edges[9].Set(ref c1.edges[5]);

                if (c.TransCube.edges[10].Equals(c1.TransCube.edges[6]))
                    c.edges[10].Set(ref c1.edges[6]);
                if (c.TransCube.edges[10].Equals(c1.TransCube.edges[8]))
                    c.edges[10].Set(ref c1.edges[8]);
                if (c.TransCube.edges[10].Equals(c1.TransCube.edges[4]))
                    c.edges[10].Set(ref c1.edges[4]);

                if (c.TransCube.edges[11].Equals(c1.TransCube.edges[7]))
                    c.edges[11].Set(ref c1.edges[7]);
                if (c.TransCube.edges[11].Equals(c1.TransCube.edges[5]))
                    c.edges[11].Set(ref c1.edges[5]);
                if (c.TransCube.edges[11].Equals(c1.TransCube.edges[9]))
                    c.edges[11].Set(ref c1.edges[9]);

                
            }
            foreach (Cube t in Model)
                t.VertexToEdges();
            c.VertexToEdges();
            c.UpdateFace();
        }

        void CheckAllFaces(int x1, int x2, int y1, int y2, int z1, int z2, ref Cube newCube)
        {
            int countMinX = 0;
            int countMaxX = 0;
            int countMinY = 0;
            int countMaxY = 0;
            int countMinZ = 0;
            int countMaxZ = 0;
            string str = "";
            Cube c;
            for (int i = 0; i < Model.Count; i++)
            {

                if (newCube.TransCube.FaceMaxX().Equals(Model[i].TransCube.FaceMinX()))
                {
                    str += " , newcube maxX = Minx Of " + i.ToString();
                    c = Model[i];
                    countMinX++;
                    InitializeFace(ref newCube, ref c, "minX");
                }
                

                 if (newCube.TransCube.FaceMinX().Equals(Model[i].TransCube.FaceMaxX()))
                    {
                        str += " , newcube minX = Maxx Of " + i.ToString();
                        c = Model[i];
                        countMaxX++;
                        InitializeFace(ref newCube, ref c, "maxX");
                       
                    }


                 if (newCube.TransCube.FaceMaxZ().Equals(Model[i].TransCube.FaceMinZ()))
                 {
                     str += " , newcube maxZ = MinZ Of " + i.ToString();
                     c = Model[i];
                     countMinZ++;
                     InitializeFace(ref newCube, ref c, "minZ");

                 }
             //    else { str +=( newCube.TransCube.FaceMaxZ().ToString() + "\n" + Model[i].TransCube.FaceMinZ().ToString()); }


                 if (newCube.TransCube.FaceMinZ().Equals(Model[i].TransCube.FaceMaxZ()))
                 {
                     str += " , newcube minZ = MaxZ Of " + i.ToString();
                     c = Model[i];
                     countMaxZ++;
                     InitializeFace(ref newCube, ref c, "maxZ");

                 }
                 else { 
                      str +=( newCube.TransCube.FaceMinZ().ToString() + "\n" + Model[i].TransCube.FaceMaxZ().ToString()); 

                 }
                

                  if (newCube.TransCube.FaceMinY().Equals(Model[i].TransCube.FaceMaxY()))
                    {
                        str += " , newcube minY = MaxY Of " + i.ToString();
                        c = Model[i];
                        countMaxY++;
                        InitializeFace(ref newCube, ref c, "maxY");
                       
                    }
                

               if (newCube.TransCube.FaceMaxY().Equals(Model[i].TransCube.FaceMinY()))
                    {
                        str += " , newcube maxY = MinY Of " + i.ToString()+" \n";
                        c = Model[i];
                        countMinY++;
                        InitializeFace(ref newCube, ref c, "minY");
                        
                    }
                

            }

            if (countMaxX == 0)
                InitializeFace(ref newCube, "minX");
            if (countMaxY == 0)
                InitializeFace(ref newCube, "minY");
            if (countMaxZ == 0)
                InitializeFace(ref newCube, "minZ");
            if (countMinX == 0)
                InitializeFace(ref newCube, "maxX");
            if (countMinY == 0)
                InitializeFace(ref newCube, "maxY");
            if (countMinZ == 0)
                InitializeFace(ref newCube, "maxZ");
           // MessageBox.Show(str);

        }

        public void AddCube()
        {
            if ((selectedSide.Parent != null) && (selectedSide.Parent.Origin != null))
            {
                
                Cube newCube = new Cube();
                
                newCube.Initialize(selectedSide.Parent.Origin);
                newCube.TransCube = new Cube();
                newCube.TransCube.Initialize(selectedSide.Parent.TransCube);
                double org = 0, now = 0;
                switch (selectedSide.maxState)
                {
                    case "minY":
                        org = newCube.faces[0].MaxY();
                        now = selectedSide.Parent.faces[0].MaxY();

                        newCube.TransCube.Translate(0, -2, 0);

                        if (org > now)
                        {
                            newCube.Translate(0, -2 - (org - now), 0);
                            
                        }
                        else
                            newCube.Translate(0, -2 + (now - org), 0);


                        newCube.Origin = new Cube();
                        newCube.Origin.Initialize(newCube);
                        
                       InitializeFace(ref newCube, ref selectedSide.Parent, "minY");


                       CheckAllFaces(0, 0, 0, 0, 0, 0, ref newCube);


                        Model.Add(newCube);
                        break;

                    case "maxY":
                        org = newCube.faces[1].MinY();
                        now = selectedSide.Parent.faces[1].MinY();

                        newCube.TransCube.Translate(0, 2, 0);
                        
                        if (org < now)
                        {
                            newCube.Translate(0, 2 + (org - now), 0);

                        }
                        else
                            newCube.Translate(0, 2 - (org-now), 0);

                        newCube.Origin = new Cube();
                        newCube.Origin.Initialize(newCube);

                        InitializeFace(ref newCube,ref  selectedSide.Parent, "maxY");
                        CheckAllFaces(0, 0, 0,0, 0, 0, ref newCube);
                        
                        Model.Add(newCube);

                        break;

                    case "minX":
                        org = newCube.faces[5].MaxX();
                        now = selectedSide.Parent.faces[5].MaxX();

                        newCube.TransCube.Translate(-2, 0, 0);

                        if (org > now)
                        {
                            newCube.Translate(-2 - (org - now), 0, 0);

                        }
                        else
                            newCube.Translate(-2 + (now - org), 0, 0);

                        newCube.Origin = new Cube();
                        newCube.Origin.Initialize(newCube);

                       InitializeFace(ref newCube, ref selectedSide.Parent, "minX");
                       CheckAllFaces(1, 0, 0, 0, 0, 0, ref newCube);
                        

                        Model.Add(newCube);
                        break;

                    case "maxX":
                        org = newCube.faces[3].MinX();
                        now = selectedSide.Parent.faces[3].MinX();

                        newCube.TransCube.Translate(2, 0, 0);

                        if (org < now)
                        {
                            newCube.Translate(2 + (org - now), 0, 0);

                        }
                        else
                            newCube.Translate(2 - (org-now), 0, 0);

                        newCube.Origin = new Cube();
                        newCube.Origin.Initialize(newCube);

                        InitializeFace(ref newCube, ref selectedSide.Parent, "maxX");
                        CheckAllFaces(0, 0, 0, 0, 0, 0, ref newCube);
                        
                        Model.Add(newCube);
                        break;

                    case "minZ":
                        org = newCube.faces[4].MaxZ();
                        now = selectedSide.Parent.faces[4].MaxZ();

                        newCube.TransCube.Translate(0,0,-2);

                        if (org > now)
                        {
                            newCube.Translate(0, 0, -2 - (org - now));

                        }
                        else
                            newCube.Translate(0, 0, -2 + (now - org));

                        newCube.Origin = new Cube();
                        newCube.Origin.Initialize(newCube);

                        InitializeFace(ref newCube, ref selectedSide.Parent, "minZ");
                        CheckAllFaces(0, 0, 0, 0, 0, 0, ref newCube);
                       
                        Model.Add(newCube);
                        break;

                    case "maxZ":
                        org = newCube.faces[2].MinZ();
                        now = selectedSide.Parent.faces[2].MinZ();

                        newCube.TransCube.Translate(0, 0, 2);

                        if (org < now)
                        {
                            newCube.Translate(0, 0, 2 + (org - now));

                        }
                        else
                            newCube.Translate(0, 0, 2 - (org-now));

                        newCube.Origin = new Cube();
                        newCube.Origin.Initialize(newCube);

                        InitializeFace(ref newCube, ref selectedSide.Parent, "maxZ");
                        CheckAllFaces(0, 0, 0, 0, 0, 0, ref newCube);

                        Model.Add(newCube);
                        break;

                }
                DevideCurveCube(Model.Count - 1, ref newCube);
                
               
            }
            else
            {
                IsAddingCube = false;
            }
            IsAddingCube = false;
            selectedSide.IsPointed = false ;
        }


    }
}
