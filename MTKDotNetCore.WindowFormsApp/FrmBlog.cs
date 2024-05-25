using ClassLibrary1MTKDotNetCore.Shared;

namespace MTKDotNetCore.WindowFormsApp
{
    public partial class FrmBlog : Form
    {
        public FrmBlog()
        {
            InitializeComponent();
        }

        private void FormBlog_load(object sender, EventArgs e)
        {

        }

        private void txtCancel_Click(object sender, EventArgs e)
        {
            txtBxtitle.Clear();
            txtBxauthor.Clear();
            txtBxcontent.Clear();
            txtTitle.Focus();
        }

        private void txtSave_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTitle_Click(object sender, EventArgs e)
        {
            DapperService dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Hello Myanmar");
        //}

        //private void label4_Click(object sender, EventArgs e)
        //{

        //}

        //private void label6_Click(object sender, EventArgs e)
        //{

        //}

        //private void label5_Click(object sender, EventArgs e)
        //{

        //}

        //private void button1_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void txtSave_Click(object sender, EventArgs e)
        //{

        //}
    }
}
