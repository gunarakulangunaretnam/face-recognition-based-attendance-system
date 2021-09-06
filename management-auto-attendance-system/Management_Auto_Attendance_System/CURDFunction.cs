using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Management_Auto_Attendance_System
{
    class CURDFunction
    {
        string ServerName = Properties.Settings.Default.server;
        string DatabaseName = Properties.Settings.Default.dbname;
        string ServerUsername = Properties.Settings.Default.sever_username;
        string ServerPassword = Properties.Settings.Default.server_password;


        public void CUD_Function(string query)
        {
            try
            {
                using (MySqlConnection myConnect = new MySqlConnection("SERVER=" + ServerName + ";DATABASE=" + DatabaseName + ";UID=" + ServerUsername + ";PASSWORD=" + ServerPassword + ""))
                {
                    myConnect.Open();
                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    myCommand.ExecuteNonQuery();
                    myConnect.Close();

                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

            }

        }
    }
}
