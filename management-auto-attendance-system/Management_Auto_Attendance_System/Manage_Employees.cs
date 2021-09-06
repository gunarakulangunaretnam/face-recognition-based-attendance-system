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
    public partial class Manage_Employees : Form
    {
        public Manage_Employees()
        {
            InitializeComponent();
        }

        CURDFunction CUD = new CURDFunction();

        string profile_img_path = "";

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Manage_Employees_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            DatagridView();
        }

        public void clearText() {

            emp_id.Clear();
            first_name.Clear();
            last_name.Clear();
            gender.SelectedItem = null;
            job_title.Clear();
            dob.Clear();
            nic_card_no.Clear();
            phone_no.Clear();
            address.Clear();
            email.Clear();
            m_status.SelectedItem = null;
            pictureBox1.Image = Image.FromFile("Employees_Profile_Pictures\\No_image.jpg");

        }

        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;


        public void DatagridView()
        {


            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("First Name");
            datatable1.Columns.Add("Last Name");
            datatable1.Columns.Add("Date of Birth");
            datatable1.Columns.Add("Gender");
            datatable1.Columns.Add("Job Title");
            datatable1.Columns.Add("NIC Card No");
            datatable1.Columns.Add("Phone No");
            datatable1.Columns.Add("Address");
            datatable1.Columns.Add("Email Address");
            datatable1.Columns.Add("Marital Status");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT * FROM employees";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("first_name"), data.GetString("last_name"), data.GetString("dob"), data.GetString("gender"), data.GetString("job_title"), data.GetString("nic"), data.GetString("phone_no"), data.GetString("address"), data.GetString("email_address"), data.GetString("marital_status"));
                    num++;
                }
                
            }

            dataGridView1.DataSource = datatable1;

        }


        private void button2_Click(object sender, EventArgs e)
        {

            if (emp_id.Text != String.Empty && first_name.Text != String.Empty && last_name.Text != String.Empty && gender.Text != String.Empty && job_title.Text != String.Empty && dob.Text != String.Empty)
            {
                string emp_id_val = "";


                using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {

                    List<string> macAddressesFromServer = new List<string>();

                    string query = "SELECT employee_id FROM employees where employee_id = '" + emp_id.Text + "'";
                    myConnect.Open();
                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    MySqlDataReader data = myCommand.ExecuteReader();

                    while (data.Read())
                    {
                        emp_id_val = data.GetString("employee_id");

                    }
                }

                if (emp_id_val == "")
                {


                    string profile_photo_name = "";

                    if (profile_img_path == "")
                    {

                        profile_photo_name = "No_Image";

                    }
                    else
                    {

                        string ext = Path.GetExtension(profile_img_path);
                        profile_photo_name = emp_id.Text + ext;
                        File.Copy(@profile_img_path, @"Employees_Profile_Pictures\" + profile_photo_name);

                    }

                    string sqlCode = "INSERT INTO employees VALUES('','" + emp_id.Text + "','" + first_name.Text + "','" + last_name.Text + "','" + dob.Text + "','" + gender.Text + "','" + job_title.Text + "','" + nic_card_no.Text + "','" + phone_no.Text + "','" + address.Text + "','" + email.Text + "','" + m_status.Text + "','" + profile_photo_name + "','False','False')";

                    CUD.CUD_Function(sqlCode);

                    DatagridView();
                    MessageBox.Show("Data Inserted Successfully!");
                    profile_img_path = "";
                    clearText();


                }
                else {

                    MessageBox.Show("The employee ID has been already exited in the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    emp_id.Text = "";
                }
                

            }
            else {

                MessageBox.Show("Please fill out all columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose Image(*.jpg;*.png;*.gif,*.jpeg)|*.jpg;*.png;*.gif;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                profile_img_path = ofd.FileName;             
            }

        }
    }
}
