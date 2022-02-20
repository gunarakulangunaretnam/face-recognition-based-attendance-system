using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Management_Auto_Attendance_System
{
    public partial class Create_Dataset : Form
    {
        public Create_Dataset()
        {
            InitializeComponent();
        }

        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;

        CURDFunction curd = new CURDFunction();
        string employee_id = "";
        string employee_full_name = "";

        bool CaputringToolStarted = false;


        string Selected_Employee_ID = "";

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Create_Dataset_Load(object sender, EventArgs e)
        {
            SelectAllEmployeeID();
            DatagridCaller();
            searchby.SelectedIndex = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void SelectAllEmployeeID()
        {

            emp_id.Items.Clear();

            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employee_id FROM employees where is_dataset_available = 'False'";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                while (data.Read())
                {
                    emp_id.Items.Add(data.GetString(0));

                }

            }

        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CaputringToolStarted == false)
            {
                if (emp_id.Text != "")
                {
                    using (StreamWriter writetext = new StreamWriter("Dataset_Creator_Run.bat"))
                    {
                        writetext.WriteLine("call activate ai_attendance_system_env");
                        writetext.WriteLine("python dataset-creator.py -e " + emp_id.Text + " -n " + numericUpDown1.Value);
                        writetext.WriteLine("call activate ai_attendance_system_env");

                    }

                    using (StreamWriter writetext = new StreamWriter("Data_Creator_Status.txt"))
                    {
                        writetext.WriteLine("Starting");
                    }

                    timer1.Start();

                    employee_id = emp_id.Text;
                    CaputringToolStarted = true;

                    System.Diagnostics.Process.Start(@"Dataset_Creator_Run.vbs");

                }
                else
                {

                    MessageBox.Show("Please select an employee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void emp_id_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT first_name, last_name FROM employees where employee_id = '" + emp_id.Text + "'";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                while (data.Read())
                {
                    employee_full_name = data.GetString(0) + " " + data.GetString(1);
                    label1.Text = employee_full_name;

                }

            }

        }

        public void DatagridCaller() {

            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("Full Name");
            datatable1.Columns.Add("Number of Images");
            datatable1.Columns.Add("Created Date");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT * FROM datasets";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("full_name"), data.GetString("number_of_images"), data.GetString("created_date"));
                    num++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }

        public void UpdateDataToDatabase() {

            string sqlCode = "Update employees SET is_dataset_available = 'True' WHERE employee_id = '" + employee_id + "'";
            curd.CUD_Function(sqlCode);

            var fileCount = (from file in Directory.EnumerateFiles(@"Datasets\" + employee_id, "*", SearchOption.AllDirectories) select file).Count();

            DateTime now = DateTime.Now;

            string sqlCode_2 = "INSERT INTO datasets VALUES('','" + employee_id + "','" + employee_full_name + "','" + fileCount + "','" + now.ToString("dd-MM-yyyy") + "')";
            curd.CUD_Function(sqlCode_2);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string val = "";

                using (StreamReader writetext = new StreamReader("Data_Creator_Status.txt"))
                {

                    val = writetext.ReadLine();

                }

                if (val == "Finished")
                {

                    timer1.Stop();
                    CaputringToolStarted = false;
                    UpdateDataToDatabase();
                    DatagridCaller();
                    SelectAllEmployeeID();
                    label1.Text = "";
                    emp_id.Text = "";


                }
                else if (val == "Stopped")
                {

                    timer1.Stop();

                    using (StreamWriter writetext = new StreamWriter("Data_Creator_Status.txt"))
                    {
                        writetext.WriteLine("Starting");
                        CaputringToolStarted = false;
                    }
                }

            }
            catch (Exception)
            {

               
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Selected_Employee_ID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");

            }
        }


        public void DataCaller(string q)
        {

            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("Full Name");
            datatable1.Columns.Add("Number of Images");
            datatable1.Columns.Add("Created Date");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = q;
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("full_name"), data.GetString("number_of_images"), data.GetString("created_date"));
                    num++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }


        private void search_TextChanged(object sender, EventArgs e)
        {
            if (searchby.Text != "")
            {
                string SearchByVal = searchby.Text;

                if (SearchByVal == "Employee ID")
                {

                    string sqlcode = "SELECT * FROM datasets WHERE employee_id LIKE '" + search.Text + "%'";
                    DataCaller(sqlcode);

                }
                else if (SearchByVal == "Full Name")
                {

                    string sqlcode = "SELECT * FROM datasets WHERE full_name LIKE '" + search.Text + "%'";
                    DataCaller(sqlcode);

                }

            }
            else
            {

                DatagridCaller();

            }
        }

        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                //Delete all files from the Directory
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                //Delete all child Directories
                foreach (string directory in Directory.GetDirectories(path))
                {
                    DeleteDirectory(directory);
                }
                //Delete a Directory
                Directory.Delete(path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Selected_Employee_ID != "")
            {

                DialogResult dialogResult = MessageBox.Show("Are you sure to delete Yes/No", "Delete", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {

                    string UpdateCode = "UPDATE employees SET is_dataset_available = 'False' WHERE employee_id = '"+Selected_Employee_ID+"'";
                    curd.CUD_Function(UpdateCode);

                    string DeleteCode = "DELETE FROM datasets WHERE employee_id = '" + Selected_Employee_ID + "'";
                    curd.CUD_Function(DeleteCode);

                    string directoryPath = "Datasets\\" + Selected_Employee_ID;
                    DeleteDirectory(directoryPath);

                    
                    DatagridCaller();
                    SelectAllEmployeeID();

                    MessageBox.Show("Deletion Successful.");

                }

            }
            else {

                MessageBox.Show("Please select an employee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchby_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
