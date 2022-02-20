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
using System.IO;

namespace Management_Auto_Attendance_System
{
    public partial class Verify_Face : Form
    {

        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;


        string Employee_ID = "";
        string face_Entering= "";
        string face_Exiting = "";
        string Face_Entering_Img_Path = "";
        string Face_Exiting_Img_Path = "";

        CURDFunction curd = new CURDFunction();
        

        public Verify_Face(string emp_id, string face_entering, string face_exiting)
        {
            InitializeComponent();

            Employee_ID = emp_id;
            face_Entering = face_entering;
            face_Exiting = face_exiting;

        }

        private void Verify_Face_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            if (face_Entering == "False") {

                using (MySqlConnection myConnect_2 = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {
                    myConnect_2.Open();


                    string query_2 = "SELECT face_recognition_entering_img_path FROM attendance WHERE employee_id = '" + Employee_ID + "'";

                    MySqlCommand myCommand_2 = new MySqlCommand(query_2, myConnect_2);
                    MySqlDataReader data_2 = myCommand_2.ExecuteReader();

                    while (data_2.Read())
                    {

                        Face_Entering_Img_Path = data_2.GetString("face_recognition_entering_img_path");

                    }

                }

                try
                {
                    var fs = File.OpenRead(@"Attendance_Pending_Images\" + Face_Entering_Img_Path);
                    pictureBox1.Image = Image.FromStream(fs);
                    fs.Close();
                }
                catch (Exception)
                {
                    
                }

            }


            if (face_Exiting == "False") {


                using (MySqlConnection myConnect_2 = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {
                    myConnect_2.Open();


                    string query_2 = "SELECT face_recognition_exiting_img_path FROM attendance WHERE employee_id = '" + Employee_ID + "'";

                    MySqlCommand myCommand_2 = new MySqlCommand(query_2, myConnect_2);
                    MySqlDataReader data_2 = myCommand_2.ExecuteReader();

                    while (data_2.Read())
                    {

                        Face_Exiting_Img_Path = data_2.GetString("face_recognition_exiting_img_path");

                    }

                }

                try
                {

                    var fs = File.OpenRead(@"Attendance_Pending_Images\" + Face_Exiting_Img_Path);
                    pictureBox2.Image = Image.FromStream(fs);
                    fs.Close();

                }
                catch (Exception)
                {

                    
                }

                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (face_Entering == "False") {


                string sqlCode = "UPDATE attendance SET face_recognition_entering = 'True', face_recognition_entering_img_path = ''";
                curd.CUD_Function(sqlCode);

                try
                {
                    System.IO.File.Delete(@"Attendance_Pending_Images\" + Face_Entering_Img_Path);
                }
                catch (Exception)
                {

                    
                }
               

            }

            if (face_Exiting == "False")
            {

                string sqlCode = "UPDATE attendance SET face_recognition_exiting = 'True', face_recognition_exiting_img_path = ''";
                curd.CUD_Function(sqlCode);

                try
                {
                    System.IO.File.Delete(@"Attendance_Pending_Images\" + Face_Exiting_Img_Path);
                }
                catch (Exception)
                {

                    
                }
                

            }

            MessageBox.Show("Face Verification is Successful");
            this.Hide();

        }
    }
}
