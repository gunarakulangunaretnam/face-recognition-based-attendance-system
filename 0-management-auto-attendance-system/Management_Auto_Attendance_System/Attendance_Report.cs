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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Management_Auto_Attendance_System
{
    public partial class Attendance_Report : Form
    {
        public Attendance_Report()
        {
            InitializeComponent();
        }


        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;

        string Selected_Employee_ID = "";

        

        public void SelectPendingData(string date)
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

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date,attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "' AND (attendance.face_recognition_entering = 'False' OR attendance.face_recognition_exiting = 'False')";
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


        public void SelectAbsentData(string date)
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
            datatable1.Columns.Add("OUT Face Verified");
            datatable1.Columns.Add("Date ");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name FROM employees WHERE employee_id NOT IN (SELECT employee_id FROM attendance WHERE _date ='" + date + "')";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();



                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("first_name"), data.GetString("last_name"), "----------", "----------", "Absent", "----------","----------", date);
                    num++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }


        public void SelectPresentData(string date)
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
            datatable1.Columns.Add("OUT Face Verified");
            datatable1.Columns.Add("Date ");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name,attendance._date, attendance.in_time, attendance.out_time, attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "'";
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

        public void SelectALLData(string date)
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
            datatable1.Columns.Add("OUT Face Verified");
            datatable1.Columns.Add("Date ");



            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name,attendance._date, attendance.in_time, attendance.out_time, attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "'";
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



            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name FROM employees WHERE employee_id NOT IN (SELECT employee_id FROM attendance WHERE _date ='" + date + "')";
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();



                int num = 1;

                while (data.Read())
                {
                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("first_name"), data.GetString("last_name"), "----------", "----------", "Absent", "----------", "----------", date);
                    num++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }



        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {


        }

        private void Attendance_Report_Load(object sender, EventArgs e)
        {
            listby.SelectedIndex = 0;
            check_details.Visible = false;
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;
            SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            comboBox1.SelectedIndex = 0;
            
        }

        private void listby_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                listby.SelectedIndex = 0;
                SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

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

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listby.Text == "Present")
            {
                check_details.Visible = false;
                SelectPresentData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));


            }
            else if (listby.Text == "Absent")
            {
                check_details.Visible = false;
                SelectAbsentData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            }
            else if (listby.Text == "Pending (Face Not Verified)")
            {

                check_details.Visible = true;
                SelectPendingData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            }
            else if (listby.Text == "All")
            {

                check_details.Visible = false;
                SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            }
        }

        public void ExportExcel_All_Data(string date)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            string sfdname = sfd.FileName;
            sfd.Filter = "Excel Files | *.xls";
            sfd.FileName = dateTimePicker1.Value.ToString("dd-MM-yyyy")+"_(Full Report)";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string save_path = Path.GetFullPath(sfd.FileName);

                Loading l = new Loading();
                l.Show();
                
                Excel.Application excelApp = new Excel.Application();
                if (excelApp != null)
                {
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                    Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets.Add();

                    excelWorksheet.Cells[1, 1] = "Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + " (All Attendance Report)";
                    excelWorksheet.Cells[1, 1].Font.Size = 20;

                    Excel.Range range = excelWorksheet.get_Range("A1", "G1");
                    range.Merge();
                    excelWorksheet.Cells[1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    excelWorksheet.Cells[3, 1] = "Employee ID";
                    excelWorksheet.Cells[3, 2] = "First Name";
                    excelWorksheet.Cells[3, 3] = "Last Name";
                    excelWorksheet.Cells[3, 4] = "In Time";
                    excelWorksheet.Cells[3, 5] = "Out Time";
                    excelWorksheet.Cells[3, 6] = "Status";
                    excelWorksheet.Cells[3, 7] = "IN Face Verified";
                    excelWorksheet.Cells[3, 8] = "IN Face Verified";
                    excelWorksheet.Cells[3, 9] = "Date";


                    //Editing            
                    excelWorksheet.Cells[3, 1].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 2].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 3].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 4].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 5].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 6].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 7].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 8].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 9].EntireRow.Font.Bold = true;


                    excelWorksheet.Cells[3, 1].Font.Size = 13;
                    excelWorksheet.Cells[3, 2].Font.Size = 13;
                    excelWorksheet.Cells[3, 3].Font.Size = 13;
                    excelWorksheet.Cells[3, 4].Font.Size = 13;
                    excelWorksheet.Cells[3, 5].Font.Size = 13;
                    excelWorksheet.Cells[3, 6].Font.Size = 13;
                    excelWorksheet.Cells[3, 7].Font.Size = 13;
                    excelWorksheet.Cells[3, 8].Font.Size = 13;
                    excelWorksheet.Cells[3, 9].Font.Size = 13;


                    excelWorksheet.Cells[3, 1].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 2].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 3].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 4].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 5].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 6].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 7].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 8].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 9].ColumnWidth = 30;


                    int num = 4;

                    using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                    {
                        myConnect.Open();

                        string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date, attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "'";
                        MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                        MySqlDataReader data = myCommand.ExecuteReader();

                        

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

                            excelWorksheet.Cells[num, 1] = data.GetString("employee_id");
                            excelWorksheet.Cells[num, 2] = data.GetString("first_name");
                            excelWorksheet.Cells[num, 3] = data.GetString("last_name");
                            excelWorksheet.Cells[num, 4] = data.GetString("in_time");
                            excelWorksheet.Cells[num, 5] = data.GetString("out_time");
                            excelWorksheet.Cells[num, 6] = "Present";
                            excelWorksheet.Cells[num, 7] = Face_Verification_Entering;
                            excelWorksheet.Cells[num, 8] = Face_Verification_Exiting;
                            excelWorksheet.Cells[num, 9] = data.GetString("_date");
                            num++;

                        }
                    }




                    using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                    {
                        myConnect.Open();

                        string query = "SELECT employees.employee_id, employees.first_name, employees.last_name FROM employees WHERE employee_id NOT IN (SELECT employee_id FROM attendance WHERE _date ='" + date + "')";
                        MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                        MySqlDataReader data = myCommand.ExecuteReader();

                      
                        while (data.Read())
                        {


                            excelWorksheet.Cells[num, 1] = data.GetString("employee_id");
                            excelWorksheet.Cells[num, 2] = data.GetString("first_name");
                            excelWorksheet.Cells[num, 3] = data.GetString("last_name");
                            excelWorksheet.Cells[num, 4] = "----------";
                            excelWorksheet.Cells[num, 5] = "----------";
                            excelWorksheet.Cells[num, 6] = "Absent";
                            excelWorksheet.Cells[num, 7] = "----------";
                            excelWorksheet.Cells[num, 8] = "----------";
                            excelWorksheet.Cells[num, 9] = date;
                            num++;

                        }
                    }
                   


                    excelApp.DisplayAlerts = false;

                    string ExcelReportSavePath = save_path;

                    excelApp.ActiveWorkbook.SaveAs(ExcelReportSavePath, Excel.XlFileFormat.xlWorkbookNormal);
                    excelWorkbook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    l.Hide();

                    MessageBox.Show("Exporting has been done");
                }
            }
        }

        public void ExportExcel_Pending_Data(string date)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            string sfdname = sfd.FileName;
            sfd.Filter = "Excel Files | *.xls";
            sfd.FileName = dateTimePicker1.Value.ToString("dd-MM-yyyy")+"_(Pending)";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string save_path = Path.GetFullPath(sfd.FileName);

                Loading l = new Loading();
                l.Show();

                Excel.Application excelApp = new Excel.Application();
                if (excelApp != null)
                {
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                    Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets.Add();

                    excelWorksheet.Cells[1, 1] = "Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + " (Pending Attendance Report)";
                    excelWorksheet.Cells[1, 1].Font.Size = 20;

                    Excel.Range range = excelWorksheet.get_Range("A1", "G1");
                    range.Merge();
                    excelWorksheet.Cells[1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    excelWorksheet.Cells[3, 1] = "Employee ID";
                    excelWorksheet.Cells[3, 2] = "First Name";
                    excelWorksheet.Cells[3, 3] = "Last Name";
                    excelWorksheet.Cells[3, 4] = "In Time";
                    excelWorksheet.Cells[3, 5] = "Out Time";
                    excelWorksheet.Cells[3, 6] = "Status";
                    excelWorksheet.Cells[3, 7] = "IN Face Verified";
                    excelWorksheet.Cells[3, 8] = "OUT Face Verified";
                    excelWorksheet.Cells[3, 9] = "Date";


                    //Editing            
                    excelWorksheet.Cells[3, 1].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 2].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 3].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 4].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 5].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 6].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 7].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 8].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 9].EntireRow.Font.Bold = true;


                    excelWorksheet.Cells[3, 1].Font.Size = 13;
                    excelWorksheet.Cells[3, 2].Font.Size = 13;
                    excelWorksheet.Cells[3, 3].Font.Size = 13;
                    excelWorksheet.Cells[3, 4].Font.Size = 13;
                    excelWorksheet.Cells[3, 5].Font.Size = 13;
                    excelWorksheet.Cells[3, 6].Font.Size = 13;
                    excelWorksheet.Cells[3, 7].Font.Size = 13;
                    excelWorksheet.Cells[3, 8].Font.Size = 13;
                    excelWorksheet.Cells[3, 9].Font.Size = 13;


                    excelWorksheet.Cells[3, 1].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 2].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 3].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 4].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 5].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 6].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 7].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 8].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 9].ColumnWidth = 30;



                    using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                    {
                        myConnect.Open();

                        string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date,attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "' AND (attendance.face_recognition_entering = 'False' OR attendance.face_recognition_exiting = 'False')";
                        MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                        MySqlDataReader data = myCommand.ExecuteReader();

                        int num = 4;

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

                            excelWorksheet.Cells[num, 1] = data.GetString("employee_id");
                            excelWorksheet.Cells[num, 2] = data.GetString("first_name");
                            excelWorksheet.Cells[num, 3] = data.GetString("last_name");
                            excelWorksheet.Cells[num, 4] = data.GetString("in_time");
                            excelWorksheet.Cells[num, 5] = data.GetString("out_time");
                            excelWorksheet.Cells[num, 6] = "Present";
                            excelWorksheet.Cells[num, 7] = Face_Verification_Entering;
                            excelWorksheet.Cells[num, 8] = Face_Verification_Exiting;
                            excelWorksheet.Cells[num, 9] = data.GetString("_date");
                            num++;

                        }
                    }



                    excelApp.DisplayAlerts = false;

                    string ExcelReportSavePath = save_path;

                    excelApp.ActiveWorkbook.SaveAs(ExcelReportSavePath, Excel.XlFileFormat.xlWorkbookNormal);
                    excelWorkbook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    l.Hide();

                    MessageBox.Show("Exporting has been done");
                }
            }
        }


        public void ExportExcel_Absent_Data(string date)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            string sfdname = sfd.FileName;
            sfd.Filter = "Excel Files | *.xls";
            sfd.FileName = dateTimePicker1.Value.ToString("dd-MM-yyyy")+"_(Absent)";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string save_path = Path.GetFullPath(sfd.FileName);

                Loading l = new Loading();
                l.Show();

                Excel.Application excelApp = new Excel.Application();
                if (excelApp != null)
                {
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                    Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets.Add();

                    excelWorksheet.Cells[1, 1] = "Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + " (Absent Attendance Report)";
                    excelWorksheet.Cells[1, 1].Font.Size = 20;

                    Excel.Range range = excelWorksheet.get_Range("A1", "G1");
                    range.Merge();
                    excelWorksheet.Cells[1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    excelWorksheet.Cells[3, 1] = "Employee ID";
                    excelWorksheet.Cells[3, 2] = "First Name";
                    excelWorksheet.Cells[3, 3] = "Last Name";
                    excelWorksheet.Cells[3, 4] = "In Time";
                    excelWorksheet.Cells[3, 5] = "Out Time";
                    excelWorksheet.Cells[3, 6] = "Status";
                    excelWorksheet.Cells[3, 7] = "IN Face Verified";
                    excelWorksheet.Cells[3, 8] = "OUT Face Verified";
                    excelWorksheet.Cells[3, 9] = "Date";


                    //Editing            
                    excelWorksheet.Cells[3, 1].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 2].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 3].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 4].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 5].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 6].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 7].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 8].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 9].EntireRow.Font.Bold = true;


                    excelWorksheet.Cells[3, 1].Font.Size = 13;
                    excelWorksheet.Cells[3, 2].Font.Size = 13;
                    excelWorksheet.Cells[3, 3].Font.Size = 13;
                    excelWorksheet.Cells[3, 4].Font.Size = 13;
                    excelWorksheet.Cells[3, 5].Font.Size = 13;
                    excelWorksheet.Cells[3, 6].Font.Size = 13;
                    excelWorksheet.Cells[3, 7].Font.Size = 13;
                    excelWorksheet.Cells[3, 8].Font.Size = 13;


                    excelWorksheet.Cells[3, 1].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 2].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 3].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 4].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 5].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 6].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 7].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 8].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 9].ColumnWidth = 30;



                    using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                    {
                        myConnect.Open();

                        string query = "SELECT employees.employee_id, employees.first_name, employees.last_name FROM employees WHERE employee_id NOT IN (SELECT employee_id FROM attendance WHERE _date ='" + date + "')";
                        MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                        MySqlDataReader data = myCommand.ExecuteReader();

                        int num = 4;

                        while (data.Read())
                        {


                            excelWorksheet.Cells[num, 1] = data.GetString("employee_id");
                            excelWorksheet.Cells[num, 2] = data.GetString("first_name");
                            excelWorksheet.Cells[num, 3] = data.GetString("last_name");
                            excelWorksheet.Cells[num, 4] = "----------";
                            excelWorksheet.Cells[num, 5] = "----------";
                            excelWorksheet.Cells[num, 6] = "Absent";
                            excelWorksheet.Cells[num, 7] = "----------";
                            excelWorksheet.Cells[num, 8] = "----------";
                            excelWorksheet.Cells[num, 9] = date;
                            num++;

                        }
                    }



                    excelApp.DisplayAlerts = false;

                    string ExcelReportSavePath = save_path;

                    excelApp.ActiveWorkbook.SaveAs(ExcelReportSavePath, Excel.XlFileFormat.xlWorkbookNormal);
                    excelWorkbook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    l.Hide();

                    MessageBox.Show("Exporting has been done");
                }
            }
        }

        public void ExportExcel_Present_Data(string date)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            string sfdname = sfd.FileName;
            sfd.Filter = "Excel Files | *.xls";
            sfd.FileName = dateTimePicker1.Value.ToString("dd-MM-yyyy")+"_(Present)";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string save_path =Path.GetFullPath(sfd.FileName);

                Loading l = new Loading();
                l.Show();

                Excel.Application excelApp = new Excel.Application();
                if (excelApp != null)
                {
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                    Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets.Add();

                    excelWorksheet.Cells[1, 1] = "Date: " + dateTimePicker1.Value.ToString("dd-MM-yyyy") + " (Present Attendance Report)";
                    excelWorksheet.Cells[1, 1].Font.Size = 20;

                    Excel.Range range = excelWorksheet.get_Range("A1", "G1");
                    range.Merge();
                    excelWorksheet.Cells[1].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    excelWorksheet.Cells[3, 1] = "Employee ID";
                    excelWorksheet.Cells[3, 2] = "First Name";
                    excelWorksheet.Cells[3, 3] = "Last Name";
                    excelWorksheet.Cells[3, 4] = "In Time";
                    excelWorksheet.Cells[3, 5] = "Out Time";
                    excelWorksheet.Cells[3, 6] = "Status";
                    excelWorksheet.Cells[3, 7] = "IN Face Verified";
                    excelWorksheet.Cells[3, 8] = "OUT Face Verified";
                    excelWorksheet.Cells[3, 9] = "Date";

                    //Editing            
                    excelWorksheet.Cells[3, 1].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 2].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 3].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 4].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 5].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 6].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 7].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 8].EntireRow.Font.Bold = true;
                    excelWorksheet.Cells[3, 9].EntireRow.Font.Bold = true;


                    excelWorksheet.Cells[3, 1].Font.Size = 13;
                    excelWorksheet.Cells[3, 2].Font.Size = 13;
                    excelWorksheet.Cells[3, 3].Font.Size = 13;
                    excelWorksheet.Cells[3, 4].Font.Size = 13;
                    excelWorksheet.Cells[3, 5].Font.Size = 13;
                    excelWorksheet.Cells[3, 6].Font.Size = 13;
                    excelWorksheet.Cells[3, 7].Font.Size = 13;
                    excelWorksheet.Cells[3, 8].Font.Size = 13;
                    excelWorksheet.Cells[3, 9].Font.Size = 13;


                    excelWorksheet.Cells[3, 1].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 2].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 3].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 4].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 5].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 6].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 7].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 8].ColumnWidth = 30;
                    excelWorksheet.Cells[3, 9].ColumnWidth = 30;



                    using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                    {
                        myConnect.Open();

                        string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date, attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "'";
                        MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                        MySqlDataReader data = myCommand.ExecuteReader();

                        int num = 4;

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

                            excelWorksheet.Cells[num, 1] = data.GetString("employee_id");
                            excelWorksheet.Cells[num, 2] = data.GetString("first_name");
                            excelWorksheet.Cells[num, 3] = data.GetString("last_name");
                            excelWorksheet.Cells[num, 4] = data.GetString("in_time");
                            excelWorksheet.Cells[num, 5] = data.GetString("out_time");
                            excelWorksheet.Cells[num, 6] = "Present";
                            excelWorksheet.Cells[num, 7] = Face_Verification_Entering;
                            excelWorksheet.Cells[num, 8] = Face_Verification_Exiting;
                            excelWorksheet.Cells[num, 9] = data.GetString("_date");
                            num++;

                        }
                    }



                    excelApp.DisplayAlerts = false;

                    string ExcelReportSavePath = save_path;

                    excelApp.ActiveWorkbook.SaveAs(ExcelReportSavePath, Excel.XlFileFormat.xlWorkbookNormal);
                    excelWorkbook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    l.Hide();

                    MessageBox.Show("Exporting has been done");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listby.Text == "Present")
            {

                check_details.Visible = false;
                SelectPresentData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                ExportExcel_Present_Data(dateTimePicker1.Value.ToString("dd-MM-yyyy"));


            }
            else if (listby.Text == "Absent")
            {

                check_details.Visible = false;
                SelectAbsentData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                ExportExcel_Absent_Data(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            }
            else if (listby.Text == "Pending (Face Not Verified)")
            {
                check_details.Visible = true;
                SelectPendingData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                ExportExcel_Pending_Data(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            }
            else if (listby.Text == "All")
            {
                check_details.Visible = false;
                SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                ExportExcel_All_Data(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        public void SearchData_FirstName(string date) {


            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("First Name");
            datatable1.Columns.Add("Last Name");
            datatable1.Columns.Add("In Time");
            datatable1.Columns.Add("Out Time");
            datatable1.Columns.Add("Status");
            datatable1.Columns.Add("IN Face Verified");
            datatable1.Columns.Add("OUT Face Verified");
            datatable1.Columns.Add("Date ");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date, attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "' AND employees.first_name LIKE '" + search_value.Text + "%'";
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

                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("first_name"), data.GetString("last_name"), data.GetString("in_time"), data.GetString("out_time"), "Present", Face_Verification_Entering,Face_Verification_Exiting, data.GetString("_date"));
                    num++;
                }
            }

            using (MySqlConnection myConnect_2 = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect_2.Open();

                string query_2 = "SELECT employees.employee_id, employees.first_name, employees.last_name FROM employees WHERE employee_id NOT IN (SELECT employee_id FROM attendance WHERE _date ='" + date + "')  AND employees.first_name LIKE '" + search_value.Text + "%'";
                MySqlCommand myCommand_2 = new MySqlCommand(query_2, myConnect_2);
                MySqlDataReader data_2 = myCommand_2.ExecuteReader();

                int num_2 = 1;

                while (data_2.Read())
                {
                    datatable1.Rows.Add(num_2.ToString(), data_2.GetString("employee_id"), data_2.GetString("first_name"), data_2.GetString("last_name"), "----------", "----------", "Absent", "----------", date);
                    num_2++;
                }

            }

            dataGridView1.DataSource = datatable1;


        }

        public void SearchData_Employee_ID(string date) {


            System.Data.DataTable datatable1 = new System.Data.DataTable();
            datatable1.Columns.Add("NO");
            datatable1.Columns.Add("Employee ID");
            datatable1.Columns.Add("First Name");
            datatable1.Columns.Add("Last Name");
            datatable1.Columns.Add("In Time");
            datatable1.Columns.Add("Out Time");
            datatable1.Columns.Add("Status");
            datatable1.Columns.Add("IN Face Verified");
            datatable1.Columns.Add("OUT Face Verified");
            datatable1.Columns.Add("Date ");


            using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect.Open();

                string query = "SELECT employees.employee_id, employees.first_name, employees.last_name, attendance.in_time, attendance.out_time, attendance._date, attendance.face_recognition_entering, attendance.face_recognition_exiting FROM employees,attendance WHERE employees.employee_id = attendance.employee_id AND attendance._date = '" + date + "' AND employees.employee_id LIKE '" + search_value.Text+"%'";
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


                    datatable1.Rows.Add(num.ToString(), data.GetString("employee_id"), data.GetString("first_name"), data.GetString("last_name"), data.GetString("in_time"), data.GetString("out_time"), "Present", Face_Verification_Entering,Face_Verification_Exiting, data.GetString("_date"));
                    num++;
                }
            }

            using (MySqlConnection myConnect_2 = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect_2.Open();

                string query_2 = "SELECT employees.employee_id, employees.first_name, employees.last_name FROM employees WHERE employee_id NOT IN (SELECT employee_id FROM attendance WHERE _date ='" + date + "')  AND employees.employee_id LIKE '" + search_value.Text + "%'";
                MySqlCommand myCommand_2 = new MySqlCommand(query_2, myConnect_2);
                MySqlDataReader data_2 = myCommand_2.ExecuteReader();

                int num_2 = 1;

                while (data_2.Read())
                {
                    datatable1.Rows.Add(num_2.ToString(), data_2.GetString("employee_id"), data_2.GetString("first_name"), data_2.GetString("last_name"), "----------", "----------", "Absent", "----------", date);
                    num_2++;
                }

            }

            dataGridView1.DataSource = datatable1;

        }

        private void search_value_TextChanged(object sender, EventArgs e)
        {
            listby.SelectedIndex = 0;
            SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            if (search_value.Text != "")
            {

                if (comboBox1.Text == "Employee ID")
                {

                    SearchData_Employee_ID(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

                }
                else if (comboBox1.Text == "First Name") {


                    SearchData_FirstName(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

                }

            }
            else {

                SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listby.SelectedIndex = 0;
            search_value.Text = "";
            SelectALLData(dateTimePicker1.Value.ToString("dd-MM-yyyy"));
        }
    }
}
