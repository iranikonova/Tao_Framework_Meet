using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tao_Framework_Meet
{
    public partial class Form2 : Form
    {
        public void SelectCube()
        {
            foreach (Cube c in Model)
                c.IsSelected = false;
            if (selectedSide.Parent != null)
            {
                selectedSide.IsPointed = false;
                selectedSide.Parent.IsSelected = true;
                RemovingCube = selectedSide.Parent;
                //textBox1.Text = vertex.Parents.Contains(RemovingCube).ToString();
            }
            //else
               // IsRemovingCube = false;
            
        }
        
    }
}
