using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_Auto_Attendance_System
{
    public partial class Dashbaord : Form
    {
        public Dashbaord()
        {
            InitializeComponent();
        }

        private void createDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void insertEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Employees MC = new Manage_Employees();
            MC.WindowState = FormWindowState.Maximized;
            MC.MdiParent = this;
            MC.Show();
        }

        private void modifyEmploteesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee_Edit MC = new Employee_Edit();
            MC.WindowState = FormWindowState.Maximized;
            MC.MdiParent = this;
            MC.Show();
        }

        private void createDatasetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            Create_Dataset MC = new Create_Dataset();
            MC.WindowState = FormWindowState.Maximized;
            MC.MdiParent = this;
            MC.Show();

        }

        private void traningToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Trainning T = new Trainning();
            T.WindowState = FormWindowState.Maximized;
            T.MdiParent = this;
            T.Show();

        }

        private void attendanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Attendance_Report AR = new Attendance_Report();
            AR.WindowState = FormWindowState.Maximized;
            AR.MdiParent = this;
            AR.Show();
        }

        private void Dashbaord_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void faceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            face_verification fv = new face_verification();
            fv.WindowState = FormWindowState.Maximized;
            fv.MdiParent = this;
            fv.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 lo = new Form1();
            lo.Show();
            this.Hide();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.WindowState = FormWindowState.Maximized;
            s.MdiParent = this;
            s.Show();
        }
    }
}
