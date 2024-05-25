using ClassLibrary1MTKDotNetCore.Shared;
using MTKDotNetCore.WindowFormsApp.Models;
using MTKDotNetCore.WindowFormsApp.Queries;

namespace MTKDotNetCore.WindowFormsApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        private void FormBlog_load(object sender, EventArgs e)
        {

        }

        private void txtCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtBxtitle.Text.Trim();
                blog.BlogAuthor = txtBxauthor.Text.Trim();
                blog.BlogContent = txtBxcontent.Text.Trim();

                int result = _dapperService.Excute(BlogQuery.BlogCreate,blog);
                string message = result > 0 ? "Create Blog Success" : "Create Blog Fail";
                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message,"Blog", MessageBoxButtons.OK,messageBoxIcon);
                if (result > 0) ClearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearControl()
        {
            txtBxtitle.Clear();
            txtBxauthor.Clear();
            txtBxcontent.Clear();
            txtBxtitle.Focus();
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
