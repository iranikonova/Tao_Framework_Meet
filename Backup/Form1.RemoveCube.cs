using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tao_Framework_Meet
{

    public partial class Form0 : Form
    {
        public void RemoveCube()
        {
           
                Model.Remove(RemovingCube);
                IsRemovingCube = false;
            
        }
    }
}
