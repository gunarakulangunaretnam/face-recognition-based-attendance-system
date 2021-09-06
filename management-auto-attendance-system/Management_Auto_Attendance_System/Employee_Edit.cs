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
    public partial class Employee_Edit : Form
    {
        public Employee_Edit()
        {
            InitializeComponent();
        }

        string Selected_Employee_ID = "";
        string profile_photo_name = "";
        string profile_img_path = "";


        CURDFunction CUD = new CURDFunction();

        public void clearText()
        {

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

        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;

        private void Employee_Edit_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            DatagridView();
            searchby.SelectedIndex = 0;
        }

        public void DataGetterFromDatabase(string employee_id) {

            try
            {

                using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {
                    myConnect.Open();

                    string query = "SELECT * FROM employees WHERE employee_id = '"+ employee_id + "'";
                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    MySqlDataReader data = myCommand.ExecuteReader();


                    string img_name = "";

                    while (data.Read())
                    {
                        emp_id.Text = data.GetString("employee_id");
                        first_name.Text = data.GetString("first_name");
                        last_name.Text = data.GetString("last_name");
                        dob.Text = data.GetString("dob");
                        gender.Text = data.GetString("gender");
                        job_title.Text = data.GetString("job_title");
                        nic_card_no.Text = data.GetString("nic");
                        phone_no.Text = data.GetString("phone_no");
                        address.Text = data.GetString("address");
                        email.Text = data.GetString("email_address");
                        m_status.Text = data.GetString("marital_status");

                        if (data.GetString("photo_path") == "No_Image")
                        {
                            var fs = File.OpenRead("Employees_Profile_Pictures\\No_image.jpg");
                            pictureBox1.Image = Image.FromStream(fs);
                            fs.Close();

                        }
                        else {

                            var fs = File.OpenRead("Employees_Profile_Pictures\\" + data.GetString("photo_path"));
                            pictureBox1.Image = Image.FromStream(fs);
                            fs.Close();
                           
                        }

                        
                        profile_photo_name = data.GetString("photo_path");
                    }

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong!");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Selected_Employee_ID = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                DataGetterFromDatabase(Selected_Employee_ID);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (profile_img_path == "")
            {

                string sqlCode = "UPDATE employees SET  first_name = '" + first_name.Text + "', last_name = '" + last_name.Text + "', dob = '" + dob.Text + "', gender = '" + gender.Text + "', job_title = '" + job_title.Text + "', nic = '" + nic_card_no.Text + "', phone_no = '" + phone_no.Text + "', address = '" + address.Text + "', email_address = '" + email.Text + "', marital_status = '" + m_status.Text + "' WHERE employee_id = '" + emp_id.Text + "'";
                CUD.CUD_Function(sqlCode);
                DataGetterFromDatabase(Selected_Employee_ID);
                MessageBox.Show("Updated Successfully!");
                profile_img_path = "";
            }
            else {

                if (profile_photo_name == "No_Image")
                {

                    string ext = Path.GetExtension(profile_img_path);
                    profile_photo_name = emp_id.Text + ext;
                    File.Copy(@profile_img_path, @"Employees_Profile_Pictures\" + profile_photo_name);

                    string sqlCode = "UPDATE employees SET  first_name = '" + first_name.Text + "', last_name = '" + last_name.Text + "', dob = '" + dob.Text + "', gender = '" + gender.Text + "', job_title = '" + job_title.Text + "', nic = '" + nic_card_no.Text + "', phone_no = '" + phone_no.Text + "', address = '" + address.Text + "', email_address = '" + email.Text + "', marital_status = '" + m_status.Text + "', photo_path = '" + profile_photo_name + "' WHERE employee_id = '" + emp_id.Text + "'";
                    CUD.CUD_Function(sqlCode);
                    DataGetterFromDatabase(Selected_Employee_ID);
                    MessageBox.Show("Updated Successfully!");
                    profile_photo_name = "";
                }
                else {

                    
                    File.Delete(@"Employees_Profile_Pictures\" + profile_photo_name);
                    
                    string ext = Path.GetExtension(profile_img_path);
                    profile_photo_name = emp_id.Text + ext;
                    File.Copy(@profile_img_path, @"Employees_Profile_Pictures\" + profile_photo_name);

                    string sqlCode = "UPDATE employees SET  first_name = '" + first_name.Text + "', last_name = '" + last_name.Text + "', dob = '" + dob.Text + "', gender = '" + gender.Text + "', job_title = '" + job_title.Text + "', nic = '" + nic_card_no.Text + "', phone_no = '" + phone_no.Text + "', address = '" + address.Text + "', email_address = '" + email.Text + "', marital_status = '" + m_status.Text + "', photo_path = '" + profile_photo_name + "' WHERE employee_id = '" + emp_id.Text + "'";
                    CUD.CUD_Function(sqlCode);
                    DataGetterFromDatabase(Selected_Employee_ID);
                    MessageBox.Show("Updated Successfully!");
                    profile_photo_name = "";
                    
                }
                
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

       

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete Yes/No", "Delete", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string sqlCode = "DELETE FROM employees WHERE employee_id = '" + Selected_Employee_ID + "'";
                CUD.CUD_Function(sqlCode);
                File.Delete(@"Employees_Profile_Pictures\" + profile_photo_name);
                DatagridView();
                clearText();

                //Delete Dataset
                //Delete Model 


                MessageBox.Show("Data deteled successfully");
                
            }
            

        }

        public void SearchByMethod(string q)
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

                string query = q;
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



        private void search_TextChanged(object sender, EventArgs e)
        {
            if (searchby.Text != "")
            {
                string SearchByVal = searchby.Text;

                if (SearchByVal == "Employee ID")
                {

                    string sqlcode = "SELECT * FROM employees WHERE employee_id LIKE '" + search.Text + "%'";
                    SearchByMethod(sqlcode);

                }
                else if (SearchByVal == "First Name")
                {

                    string sqlcode = "SELECT * FROM employees WHERE first_name LIKE '" + search.Text + "%'";
                    SearchByMethod(sqlcode);

                }
                else if (SearchByVal == "NIC Card No")
                {

                    string sqlcode = "SELECT * FROM employees WHERE nic LIKE '" + search.Text + "%'";
                    SearchByMethod(sqlcode);

                }
                else if (SearchByVal == "Phone Number") {

                    string sqlcode = "SELECT * FROM employees WHERE phone_no LIKE '" + search.Text + "%'";
                    SearchByMethod(sqlcode);
                }

            }
            else{

                DatagridView();

            }
        }
    }
}
