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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ServerName = Properties.Settings.Default.server;
                string DatabaseName = Properties.Settings.Default.dbname;
                string ServerUsername = Properties.Settings.Default.sever_username;
                string ServerPassword = Properties.Settings.Default.server_password;

                using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {

                    string UsernameFromServer = "";
                    string PasswordFromServer = "";
                    List<string> macAddressesFromServer = new List<string>();

                    string query = "SELECT * FROM users where username = '" + textBox1.Text + "'";
                    myConnect.Open();
                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    MySqlDataReader data = myCommand.ExecuteReader();

                    while (data.Read())
                    {

                        UsernameFromServer = data.GetString("username");
                        PasswordFromServer = data.GetString("password");

                    }


                    if (UsernameFromServer == textBox1.Text && PasswordFromServer == textBox2.Text)
                    {


                        Dashbaord ds = new Dashbaord();
                        ds.Show();
                        this.Hide();

                    }
                    else {


                        MessageBox.Show("Password or Username Incorrect", "Incorrect Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Clear();
                        textBox2.Clear();
                    }

                }

                }
            catch (Exception)
            {

                throw;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
