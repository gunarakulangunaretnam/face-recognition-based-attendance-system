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
    public partial class Trainning : Form
    {
        public Trainning()
        {
            InitializeComponent();
        }

        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;

        bool TrainingStarted = false;
        string Employee_FullName = "";
        string Employee_ID = "";

        string Selected_Employee_ID = "";

        CURDFunction curd = new CURDFunction();


        public void DatagridCaller()
        {

            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("Full Name");
            datatable1.Columns.Add("Number of Trained Images");
            datatable1.Columns.Add("Trained Date");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT * FROM training";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("full_name"), data.GetString("number_of_trained_images"), data.GetString("trained_date"));
                    num++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }

        public void SelectAllEmployeeID()
        {

            emp_id.Items.Clear();

            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employee_id FROM employees where is_dataset_available = 'True' AND is_model_available = 'False'";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                while (data.Read())
                {
                    emp_id.Items.Add(data.GetString(0));

                }

            }

        }

        private void Trainning_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            SelectAllEmployeeID();
            DatagridCaller();
            searchby.SelectedIndex = 0;
        }

        public void DataCaller(string q)
        {

            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("Full Name");
            datatable1.Columns.Add("Number of Trained Images");
            datatable1.Columns.Add("Trained Date");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = q;
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("full_name"), data.GetString("number_of_trained_images"), data.GetString("trained_date"));
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

                    string sqlcode = "SELECT * FROM training WHERE employee_id LIKE '" + search.Text + "%'";
                    DataCaller(sqlcode);

                }
                else if (SearchByVal == "Full Name")
                {

                    string sqlcode = "SELECT * FROM training WHERE full_name LIKE '" + search.Text + "%'";
                    DataCaller(sqlcode);

                }

            }
            else
            {

                DatagridCaller();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TrainingStarted == false)
            {

                if (emp_id.Text != "")
                {
                    using (StreamWriter writetext = new StreamWriter("Training_Run.bat"))
                    {
                        writetext.WriteLine("call activate ai_attendance_system_env");
                        writetext.WriteLine("python training.py -e " + emp_id.Text);
                        writetext.WriteLine("call activate ai_attendance_system_env");

                    }

                    using (StreamWriter writetext = new StreamWriter("Training_Status.txt"))
                    {
                        writetext.WriteLine("");

                    }

                    Employee_ID = emp_id.Text;
                    timer1.Start();
                    TrainingStarted = true;
                    System.Diagnostics.Process.Start(@"Training_Run.vbs");


                }
                else
                {

                    MessageBox.Show("Please select an employee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void UpdateTrainingInfo() {

            string sqlCode_1 = "UPDATE employees SET is_model_available = 'True' WHERE employee_id = '" + Employee_ID+"'";
            curd.CUD_Function(sqlCode_1);
            
            
            string number_of_imgs = "";

            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT number_of_images FROM datasets where employee_id = '" + Employee_ID + "'";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                while (data.Read())
                {
                    number_of_imgs = data.GetString(0);
                    
                }

            }

            
            DateTime now = DateTime.Now;

            string sqlCode_2 = "INSERT INTO training VALUES('','" + Employee_ID + "','" + Employee_FullName + "','"+ number_of_imgs + "','" + now.ToString("dd-MM-yyyy") + "')";
            curd.CUD_Function(sqlCode_2);
            
        }

        public void DefaultPercentage() {

            progressBar1.Value = 0;
            emp_id.Text = "";
            label1.Text = "";
            per.Text = "0%";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int current = 0;
            int total = 0;
            try
            {

                using (StreamReader writetext = new StreamReader("Training_Status.txt"))
                {

                    IList<string> vals = writetext.ReadLine().Split('|').ToList<string>();
                    current = Convert.ToInt32(vals[0]);
                    total = Convert.ToInt32(vals[1]);

                }


                int percentComplete = (int)Math.Round((double)(100 * current) / total);
                progressBar1.Value = percentComplete;

                per.Text = percentComplete.ToString()+"%";

                if (percentComplete == 100) {

                    UpdateTrainingInfo();
                    DefaultPercentage();
                    SelectAllEmployeeID();
                    DatagridCaller();
                    TrainingStarted = false;
                    timer1.Stop();
                    MessageBox.Show("Training process has been completed");

                }
           

            }
            catch (Exception)
            {

               
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
                    Employee_FullName = data.GetString(0) + " " + data.GetString(1);
                    label1.Text = Employee_FullName;

                }

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

                    string sqlCode_1 = "UPDATE employees SET is_model_available = 'False' WHERE employee_id = '" + Selected_Employee_ID + "'";
                    curd.CUD_Function(sqlCode_1);

                    string sqlCode_2 = "DELETE FROM training WHERE employee_id = '" + Selected_Employee_ID + "'";
                    curd.CUD_Function(sqlCode_2);


                    string directoryPath = "Trained_Models\\" + Selected_Employee_ID;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Selected_Employee_ID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                pictureBox1.InitialImage = null;
                pictureBox1.Image = Image.FromFile("Trained_Models\\"+Selected_Employee_ID+"\\"+ Selected_Employee_ID+"_(QRCode).jpg");

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");

            }
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
         
            SaveFileDialog sfd = new SaveFileDialog();
            string sfdname = sfd.FileName;
            sfd.Filter = "Image File | *.jpg";
            sfd.FileName = Selected_Employee_ID + "_(QRCode)";
            string copy_from = startupPath + "\\" + "Trained_Models\\" + Selected_Employee_ID + "\\" + Selected_Employee_ID + "_(QRCode).jpg";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string save_path = Path.GetFullPath(sfd.FileName);
                File.Copy(copy_from, save_path);
            }
        }
    }
}
