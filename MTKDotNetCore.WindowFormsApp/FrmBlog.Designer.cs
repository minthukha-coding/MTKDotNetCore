namespace MTKDotNetCore.WindowFormsApp
{
    partial class FrmBlog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtBxtitle = new TextBox();
            txtBxcontent = new TextBox();
            txtBxauthor = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtTitle = new Label();
            txtAuthor = new Label();
            txtContent = new Label();
            txtCancel = new Button();
            txtSave = new Button();
            SuspendLayout();
            // 
            // txtBxtitle
            // 
            txtBxtitle.Location = new Point(183, 102);
            txtBxtitle.Name = "txtBxtitle";
            txtBxtitle.Size = new Size(652, 37);
            txtBxtitle.TabIndex = 1;
            txtBxtitle.TextChanged += textBox1_TextChanged;
            // 
            // txtBxcontent
            // 
            txtBxcontent.Location = new Point(183, 305);
            txtBxcontent.Multiline = true;
            txtBxcontent.Name = "txtBxcontent";
            txtBxcontent.Size = new Size(652, 155);
            txtBxcontent.TabIndex = 2;
            txtBxcontent.TextChanged += textBox2_TextChanged;
            // 
            // txtBxauthor
            // 
            txtBxauthor.HideSelection = false;
            txtBxauthor.Location = new Point(183, 205);
            txtBxauthor.Name = "txtBxauthor";
            txtBxauthor.Size = new Size(652, 37);
            txtBxauthor.TabIndex = 3;
            txtBxauthor.TextChanged += textBox3_TextChanged;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 9;
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 8;
            // 
            // label3
            // 
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 7;
            // 
            // txtTitle
            // 
            txtTitle.AutoSize = true;
            txtTitle.Location = new Point(183, 59);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(59, 30);
            txtTitle.TabIndex = 4;
            txtTitle.Text = "Title:";
            txtTitle.Click += txtTitle_Click;
            // 
            // txtAuthor
            // 
            txtAuthor.AutoSize = true;
            txtAuthor.Location = new Point(183, 162);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(84, 30);
            txtAuthor.TabIndex = 5;
            txtAuthor.Text = "Author:";
            // 
            // txtContent
            // 
            txtContent.AutoSize = true;
            txtContent.Location = new Point(183, 262);
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(95, 30);
            txtContent.TabIndex = 6;
            txtContent.Text = "Content:";
            // 
            // txtCancel
            // 
            txtCancel.ForeColor = SystemColors.Desktop;
            txtCancel.Location = new Point(183, 477);
            txtCancel.Name = "txtCancel";
            txtCancel.Size = new Size(94, 39);
            txtCancel.TabIndex = 10;
            txtCancel.Text = "&Cancel";
            txtCancel.UseVisualStyleBackColor = true;
            txtCancel.Click += txtCancel_Click;
            // 
            // txtSave
            // 
            txtSave.ForeColor = SystemColors.Desktop;
            txtSave.Location = new Point(298, 477);
            txtSave.Name = "txtSave";
            txtSave.Size = new Size(104, 39);
            txtSave.TabIndex = 11;
            txtSave.Text = "&Save";
            txtSave.UseVisualStyleBackColor = true;
            txtSave.Click += txtSave_Click;
            // 
            // FrmBlog
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1320, 728);
            Controls.Add(txtSave);
            Controls.Add(txtCancel);
            Controls.Add(txtContent);
            Controls.Add(txtAuthor);
            Controls.Add(txtTitle);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtBxauthor);
            Controls.Add(txtBxcontent);
            Controls.Add(txtBxtitle);
            Font = new Font("Segoe UI", 11F);
            ForeColor = SystemColors.HighlightText;
            Margin = new Padding(4);
            Name = "FrmBlog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            Load += FormBlog_load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtBxtitle;
        private TextBox txtBxcontent;
        private TextBox txtBxauthor;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label txtTitle;
        private Label txtAuthor;
        private Label txtContent;
        private Button txtCancel;
        private Button txtSave;
    }
}
