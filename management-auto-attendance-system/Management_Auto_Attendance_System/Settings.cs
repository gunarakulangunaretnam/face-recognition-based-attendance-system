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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;

        string defult_username = "";
        string defult_password = "";

        CURDFunction curd = new CURDFunction();

        string face_rec = "";
        string face_rec_temp = "";


        private void Settings_Load(object sender, EventArgs e)
        {

            text_1.Visible = false;
            text_2.Visible = false;

            using (MySqlConnection myConnect_2 = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
            {
                myConnect_2.Open();


                string query_2 = "SELECT * FROM users";

                MySqlCommand myCommand_2 = new MySqlCommand(query_2, myConnect_2);
                MySqlDataReader data_2 = myCommand_2.ExecuteReader();

                while (data_2.Read())
                {
                    defult_username = data_2.GetString("username");
                    defult_password = data_2.GetString("password");

                }

            }

            username.Text = defult_username;



            using (StreamReader writetext = new StreamReader(@"System_Settings\Face_rec.txt"))
            {

                face_rec = writetext.ReadLine();
                face_rec_temp = face_rec;

            }

            if (face_rec == "True")
            {
                checkBox1.Checked = true;
                text_1.Visible = true;
                text_2.Visible = true;
                

            }
            else if (face_rec == "False") {

                checkBox1.Checked = true;
                text_1.Visible = false;
                text_2.Visible = false;

            }

        }

        public void setToDefult() {


            username.Text = defult_username;
            old_password.Text = "";
            new_password.Text = "";
            con_password.Text = "";


        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (con_password.Text == new_password.Text)
            {

                if (old_password.Text == defult_password)
                {

                    string sqlCode = "UPDATE users SET username = '" + username.Text + "', password='" + new_password.Text + "'";
                    curd.CUD_Function(sqlCode);
                    MessageBox.Show("The system credentials has been changed successfully");
                    old_password.Text = "";
                    new_password.Text = "";
                    con_password.Text = "";
                    this.Hide();

                }
                else {


                    MessageBox.Show("You have entered the wrong old password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }

            }
            else {



                MessageBox.Show("The new password does not match with confirm password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

           

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (face_rec_temp == "True")
            {
                using (StreamWriter writetext = new StreamWriter(@"System_Settings\Face_rec.txt"))
                {

                    writetext.WriteLine("False");

                }

                checkBox1.Checked = false;
                text_1.Visible = false;
                text_2.Visible = false;
                face_rec_temp = "False";

            }
            else if (face_rec_temp == "False")
            {
                using (StreamWriter writetext = new StreamWriter(@"System_Settings\Face_rec.txt"))
                {

                    writetext.WriteLine("True");

                }

                checkBox1.Checked = true;
                text_1.Visible = true;
                text_2.Visible = true;
                face_rec_temp = "True";
            }

            
        }
    }
}
