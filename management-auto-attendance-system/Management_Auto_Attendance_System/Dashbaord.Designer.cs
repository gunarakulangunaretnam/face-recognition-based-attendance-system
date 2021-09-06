namespace Management_Auto_Attendance_System
{
    partial class Dashbaord
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageEmployeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertEmployeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyEmploteesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDatasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attendanceReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.attendanceReportToolStripMenuItem,
            this.faceToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageEmployeesToolStripMenuItem,
            this.createDatasetToolStripMenuItem,
            this.traningToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(84, 25);
            this.projectToolStripMenuItem.Text = "Manage";
            // 
            // manageEmployeesToolStripMenuItem
            // 
            this.manageEmployeesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertEmployeesToolStripMenuItem,
            this.modifyEmploteesToolStripMenuItem});
            this.manageEmployeesToolStripMenuItem.Name = "manageEmployeesToolStripMenuItem";
            this.manageEmployeesToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.manageEmployeesToolStripMenuItem.Text = "Manage Employees";
            this.manageEmployeesToolStripMenuItem.Click += new System.EventHandler(this.manageEmployeesToolStripMenuItem_Click);
            // 
            // insertEmployeesToolStripMenuItem
            // 
            this.insertEmployeesToolStripMenuItem.Name = "insertEmployeesToolStripMenuItem";
            this.insertEmployeesToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.insertEmployeesToolStripMenuItem.Text = "Insert Employees";
            this.insertEmployeesToolStripMenuItem.Click += new System.EventHandler(this.insertEmployeesToolStripMenuItem_Click);
            // 
            // modifyEmploteesToolStripMenuItem
            // 
            this.modifyEmploteesToolStripMenuItem.Name = "modifyEmploteesToolStripMenuItem";
            this.modifyEmploteesToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.modifyEmploteesToolStripMenuItem.Text = "Modify Emplotees";
            this.modifyEmploteesToolStripMenuItem.Click += new System.EventHandler(this.modifyEmploteesToolStripMenuItem_Click);
            // 
            // createDatasetToolStripMenuItem
            // 
            this.createDatasetToolStripMenuItem.Name = "createDatasetToolStripMenuItem";
            this.createDatasetToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.createDatasetToolStripMenuItem.Text = "Create Dataset";
            this.createDatasetToolStripMenuItem.Click += new System.EventHandler(this.createDatasetToolStripMenuItem_Click_1);
            // 
            // traningToolStripMenuItem
            // 
            this.traningToolStripMenuItem.Name = "traningToolStripMenuItem";
            this.traningToolStripMenuItem.Size = new System.Drawing.Size(229, 26);
            this.traningToolStripMenuItem.Text = "Traning";
            this.traningToolStripMenuItem.Click += new System.EventHandler(this.traningToolStripMenuItem_Click);
            // 
            // attendanceReportToolStripMenuItem
            // 
            this.attendanceReportToolStripMenuItem.Name = "attendanceReportToolStripMenuItem";
            this.attendanceReportToolStripMenuItem.Size = new System.Drawing.Size(165, 25);
            this.attendanceReportToolStripMenuItem.Text = "Attendance Report";
            this.attendanceReportToolStripMenuItem.Click += new System.EventHandler(this.attendanceReportToolStripMenuItem_Click);
            // 
            // faceToolStripMenuItem
            // 
            this.faceToolStripMenuItem.Name = "faceToolStripMenuItem";
            this.faceToolStripMenuItem.Size = new System.Drawing.Size(149, 25);
            this.faceToolStripMenuItem.Text = "Face Verification";
            this.faceToolStripMenuItem.Click += new System.EventHandler(this.faceToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(84, 25);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(76, 25);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // Dashbaord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Dashbaord";
            this.Text = "Dashbaord";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashbaord_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageEmployeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDatasetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertEmployeesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyEmploteesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attendanceReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
    }
}