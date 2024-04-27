using LatihanLKS.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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

        public static void redirect(string destination)
        {
            AdminPage adminPage = new AdminPage();
            KelolaUser kelolaUser = new KelolaUser();
            KelolaLaporan kelolaLaporan = new KelolaLaporan();
            KasirPage kasirPage = new KasirPage();
            GudangPage gudangPage = new GudangPage();
            switch (destination)
            {
                case "admin_page":
                    adminPage.ShowDialog();
                    break;
                case "kelola_user":
                    kelolaUser.ShowDialog();
                    break;
                case "kelola_laporan":
                    kelolaLaporan.ShowDialog();
                    break;
                case "gudang_page":
                    gudangPage.ShowDialog();
                    break;
                case "kasir_page":
                    kasirPage.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        public static void authentication(string username, string password)
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlCommand command;
            SqlDataReader data;
            string tipe = "";
            conn.Open();

            command = new SqlCommand($"SELECT * FROM tbl_user WHERE username='{username}' AND password='{password}';", conn);
            data = command.ExecuteReader();
            while (data.Read())
            {
                tipe = data.GetValue(1).ToString();
            }
            conn.Close();
            data.Close();
            command.Dispose();
            if (tipe == "admin")
            {
                redirect("admin_page");
            }
            else if (tipe == "gudang")
            {
                redirect("gudang_page");
            }
            else if (tipe == "kasir")
            {
                redirect("kasir_page");
            }
        }

        public static void logout()
        {
            confirm("Are you sure?");
            message("logout...");
        }
    }
}
