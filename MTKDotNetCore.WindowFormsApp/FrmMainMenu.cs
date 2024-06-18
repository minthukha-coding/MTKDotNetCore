using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTKDotNetCore.WindowFormsApp
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void FrmMainMenu_Load(object sender, EventArgs e)
        {

        }

        private void createBLOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlog f = new FrmBlog();
            f.ShowDialog();
        }
    }
}
