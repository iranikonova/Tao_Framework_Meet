using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tao_Framework_Meet
{
    public partial class frmDialogueSavePrimitive : Form
    {
        public Form2 frmParent;
        public frmDialogueSavePrimitive()
        {
            InitializeComponent();
        }

        private void frmDialogueSavePrimitive_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            frmParent.AddPrimitive(textBox1.Text);
            textBox1.Text = "";
            this.Hide();
        }
    }
}
