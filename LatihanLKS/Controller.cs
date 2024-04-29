using LatihanLKS.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace LatihanLKS
{
    public class Controller
    {
        public static void message(string message)
        {
            MessageBox.Show(message);
        }

        public static string confirm(string message_text, string name_dialog = "Confirmation")
        {
            MessageBoxResult result = MessageBox.Show(message_text, name_dialog, MessageBoxButton.YesNo);
            return result.ToString();
        }

        public static void authentication(string username, string password)
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlCommand command;
            SqlDataReader data;
            conn.Open();
            command = new SqlCommand($"SELECT * FROM tbl_user WHERE username='{username}' AND password='{password}';", conn);
            data = command.ExecuteReader();
            while (data.Read())
            {
                Settings.Default.credential = $"{(int)data.GetValue(0)}.{data.GetValue(1)}.{data.GetValue(2)}.{data.GetValue(5)}";
                log("Login", (int)data.GetValue(0));
            }
            conn.Close();
            data.Close();
            command.Dispose();
        }

        public static void log(string aktivitas, int id_user)
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlCommand command;
            conn.Open();
            command = new SqlCommand($"INSERT INTO tbl_log(aktivitas,id_user) VALUES('{aktivitas}',{id_user});", conn);
            command.ExecuteNonQuery();
            conn.Close();
            command.Dispose();
        }
    }
}
