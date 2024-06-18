namespace MTKDotNetCore.WindowFormsApp
{
    partial class FrmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            bLOGToolStripMenuItem = new ToolStripMenuItem();
            createBLOGToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { bLOGToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // bLOGToolStripMenuItem
            // 
            bLOGToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createBLOGToolStripMenuItem });
            bLOGToolStripMenuItem.Name = "bLOGToolStripMenuItem";
            bLOGToolStripMenuItem.Size = new Size(71, 29);
            bLOGToolStripMenuItem.Text = "BLOG";
            // 
            // createBLOGToolStripMenuItem
            // 
            createBLOGToolStripMenuItem.Name = "createBLOGToolStripMenuItem";
            createBLOGToolStripMenuItem.Size = new Size(270, 34);
            createBLOGToolStripMenuItem.Text = "Create BLOG";
            createBLOGToolStripMenuItem.Click += createBLOGToolStripMenuItem_Click;
            // 
            // FrmMainMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmMainMenu";
            Text = "FrmMainMenu";
            Load += FrmMainMenu_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem bLOGToolStripMenuItem;
        private ToolStripMenuItem createBLOGToolStripMenuItem;
    }
}