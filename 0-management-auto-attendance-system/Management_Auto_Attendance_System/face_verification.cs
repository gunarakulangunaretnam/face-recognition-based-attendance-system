using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Management_Auto_Attendance_System
{
    public partial class face_verification : Form
    {
        public face_verification()
        {
            InitializeComponent();
        }


        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;

        string Selected_Employee_ID = "";


        public void GetData()
        {

            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("First Name");
            datatable1.Columns.Add("Last Name");
            datatable1.Columns.Add("In Time");
            datatable1.Columns.Add("Out Time");
            datatable1.Columns.Add("Status");
            datatable1.Columns.Add("IN Face Verified");
            datatable1.Columns.Add("IN OUT Verified");
            datatable1.Columns.Add("Date ");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date,attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND (attendance.face_recognition_entering = 'False' OR attendance.face_recognition_exiting = 'False')";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();

                int num = 1;

                while (data.Read())
                {
                    string Face_Verification_Entering = "";
                    string Face_Verification_Exiting = "";

                    if (data.GetString("face_recognition_entering") == "True")
                    {

                        Face_Verification_Entering = "Verified";

                    }
                    else
                    {

                        Face_Verification_Entering = "Not Verified (Pending)";

                    }


                    if (data.GetString("face_recognition_exiting") == "True")
                    {

                        Face_Verification_Exiting = "Verified";

                    }
                    else
                    {

                        Face_Verification_Exiting = "Not Verified (Pending)";

                    }

                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("first_name"), data.GetString("last_name"), data.GetString("in_time"), data.GetString("out_time"), "Present", Face_Verification_Entering, Face_Verification_Exiting, data.GetString("_date"));
                    num++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }

        private void face_verification_Load(object sender, EventArgs e)
        {
            GetData();
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

        private void check_details_Click(object sender, EventArgs e)
        {
            if (Selected_Employee_ID != "")
            {

                string face_entering = "";
                string face_exiting = "";


                using (MySqlConnection myConnect_2 = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {
                    myConnect_2.Open();


                    string query_2 = "SELECT face_recognition_entering, face_recognition_exiting FROM attendance WHERE employee_id = '" + Selected_Employee_ID + "'";

                    MySqlCommand myCommand_2 = new MySqlCommand(query_2, myConnect_2);
                    MySqlDataReader data_2 = myCommand_2.ExecuteReader();

                    while (data_2.Read())
                    {
                        face_entering = data_2.GetString("face_recognition_entering");
                        face_exiting = data_2.GetString("face_recognition_exiting");

                    }

                }

                Verify_Face vf = new Verify_Face(Selected_Employee_ID, face_entering, face_exiting);
                vf.ShowDialog();
                GetData();

            }
            else
            {

                MessageBox.Show("Please select an employee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
