using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static LatihanLKS.Controller;
using System.Data.SqlClient;
using LatihanLKS.Properties;
using static LatihanLKS.Model;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for KelolaUser.xaml
    /// </summary>
    public partial class KelolaUser : Window
    {
        public KelolaUser()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlCommand command;
            SqlDataReader data;
            User user = new User();
            conn.Open();

            command = new SqlCommand($"SELECT id_user,tipe_user,nama,alamat,telepon FROM tbl_user;", conn);
            data = command.ExecuteReader();
            while (data.Read())
            {
                user.id_user = (int)data.GetValue(0);
                user.tipe_user = data.GetValue(1).ToString();
                user.nama = data.GetValue(2).ToString();
                user.alamat = data.GetValue(3).ToString();
                user.telepon = data.GetValue(4).ToString();
                data_user.Items.Add(user);
            }
            conn.Close();
            data.Close();
            command.Dispose();
        }

        private void toKelolaLaporan(object sender, RoutedEventArgs e)
        {
            redirect("kelola_laporan");
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            logout();
        }
    }
}
