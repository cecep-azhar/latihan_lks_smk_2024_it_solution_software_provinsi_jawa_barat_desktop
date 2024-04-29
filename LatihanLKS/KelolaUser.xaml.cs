using LatihanLKS.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LatihanLKS.Model;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for KelolaUser.xaml
    /// </summary>
    public partial class KelolaUser : Page
    {
        public KelolaUser()
        {
            InitializeComponent();
            input_tipe_user.SelectedIndex = 0;
            RefreshDataGrid();
        }

        private void Mencari(object sender, TextChangedEventArgs e)
        {
            if(input_keyword.Text.Length > 0)
            {
                placeholder_keyword.Visibility = Visibility.Hidden;
                RefreshDataGrid($"SELECT id_user,tipe_user,nama,alamat,telepon,username,password FROM tbl_user WHERE nama LIKE '{input_keyword.Text}%' EXCEPT SELECT id_user,tipe_user,nama,alamat,telepon,username,password FROM tbl_user WHERE tipe_user='admin';");
            }else
            {
                placeholder_keyword.Visibility = Visibility.Visible;
                RefreshDataGrid();
            }
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (input_tipe_user.Text == "" || input_tipe_user.Text == null)
            {
                MessageBox.Show("Mohon pilih tipe user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_nama.Text == "" || input_nama.Text == null)
            {
                MessageBox.Show("Mohon masukan nama user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_telepon.Text == "" || input_telepon.Text == null)
            {
                MessageBox.Show("Mohon isikan nomor telepon user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_alamat.Text == "" || input_alamat.Text == null)
            {
                MessageBox.Show("Mohon isikan alamat user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_username.Text == "" || input_username.Text == null)
            {
                MessageBox.Show("Mohon isikan username untuk user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_password.Text == "" || input_password.Text == null)
            {
                MessageBox.Show("Mohon isikan password untuk user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                SqlCommand command;
                SqlDataReader reader;
                int result = 0;
                conn.Open();
                command = new SqlCommand($"SELECT * FROM tbl_user WHERE username='{input_username.Text}'", conn);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    result++;
                }
                conn.Close();
                command.Dispose();
                if (result == 0)
                {
                    MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin data sudah sesuai?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (answer == MessageBoxResult.Yes)
                    {
                        conn.Open();
                        command = new SqlCommand($"INSERT INTO tbl_user(tipe_user,nama,telepon,alamat,username,password) VALUES('{input_tipe_user.Text.ToLower()}','{input_nama.Text}','{input_telepon.Text}','{input_alamat.Text}','{input_username.Text}','{input_password.Text}');", conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        command.Dispose();
                    }
                }else
                {
                    MessageBox.Show("User sudah terdaftar, mohon masukan username yang berbeda!");
                }
                
            }
            RefreshDataGrid();
        }

        public void RefreshDataGrid(string query = "SELECT id_user,tipe_user,nama,alamat,telepon,username,password FROM tbl_user EXCEPT SELECT id_user,tipe_user,nama,alamat,telepon,username,password FROM tbl_user WHERE tipe_user='admin';")
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlCommand command;
            SqlDataReader data;
            conn.Open();

            command = new SqlCommand(query, conn);
            data = command.ExecuteReader();
            data_user.Items.Clear();
            while (data.Read())
            {
                User user = new User();
                user.id_user = (int)data.GetValue(0);
                user.tipe_user = data.GetValue(1).ToString();
                user.nama = data.GetValue(2).ToString();
                user.alamat = data.GetValue(3).ToString();
                user.telepon = data.GetValue(4).ToString();
                user.username = data.GetValue(5).ToString();
                user.password = data.GetValue(6).ToString();
                data_user.Items.Add(user);
            }
            conn.Close();
            data.Close();
            command.Dispose();
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            if (input_tipe_user.Text == "" || input_tipe_user.Text == null)
            {
                MessageBox.Show("Mohon pilih tipe user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_nama.Text == "" || input_nama.Text == null)
            {
                MessageBox.Show("Mohon masukan nama user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_telepon.Text == "" || input_telepon.Text == null)
            {
                MessageBox.Show("Mohon isikan nomor telepon user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_alamat.Text == "" || input_alamat.Text == null)
            {
                MessageBox.Show("Mohon isikan alamat user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_username.Text == "" || input_username.Text == null)
            {
                MessageBox.Show("Mohon isikan username untuk user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_password.Text == "" || input_password.Text == null)
            {
                MessageBox.Show("Mohon isikan password untuk user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                SqlCommand command;
                SqlDataReader reader;
                int result = 0;
                conn.Open();
                command = new SqlCommand($"SELECT * FROM tbl_user WHERE username='{input_username.Text}'", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result++;
                }
                conn.Close();
                command.Dispose();
                if (result > 0)
                {
                    MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin ingin mengubah data dari user {input_username.Text}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (answer == MessageBoxResult.Yes)
                    {
                        conn.Open();
                        command = new SqlCommand($"UPDATE tbl_user SET tipe_user='{input_tipe_user.Text.ToLower()}',nama='{input_nama.Text}',telepon='{input_telepon.Text}',alamat='{input_alamat.Text}',password='{input_password.Text}' WHERE username='{input_username.Text}';", conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        command.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Maaf, user belum terdaftar. Mohon buat user terlebih dahulu");
                }

            }
            RefreshDataGrid();
        }

        private void Compare(object sender, MouseButtonEventArgs e)
        {
            User user = this.data_user.SelectedItem as User;
            if (user.tipe_user.ToString() == "gudang")
            {
                input_tipe_user.SelectedIndex = 0;
            }
            else if (user.tipe_user.ToString() == "kasir")
            {
                input_tipe_user.SelectedIndex = 1;
            }
            input_nama.Text = user.nama;
            input_telepon.Text = user.telepon;
            input_alamat.Text = user.alamat;
            input_username.Text = user.username;
            input_password.Text = user.password;
        }

        private void RemoveUser(object sender, RoutedEventArgs e)
        {
            if (input_tipe_user.Text == "" || input_tipe_user.Text == null)
            {
                MessageBox.Show("Mohon pilih tipe user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_nama.Text == "" || input_nama.Text == null)
            {
                MessageBox.Show("Mohon masukan nama user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_telepon.Text == "" || input_telepon.Text == null)
            {
                MessageBox.Show("Mohon isikan nomor telepon user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_alamat.Text == "" || input_alamat.Text == null)
            {
                MessageBox.Show("Mohon isikan alamat user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_username.Text == "" || input_username.Text == null)
            {
                MessageBox.Show("Mohon isikan username untuk user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_password.Text == "" || input_password.Text == null)
            {
                MessageBox.Show("Mohon isikan password untuk user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                SqlCommand command;
                SqlDataReader reader;
                int result = 0;
                conn.Open();
                command = new SqlCommand($"SELECT * FROM tbl_user WHERE username='{input_username.Text}'", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result++;
                }
                conn.Close();
                command.Dispose();
                if (result > 0)
                {
                    MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin ingin menghapus data dari user {input_username.Text}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (answer == MessageBoxResult.Yes)
                    {
                        conn.Open();
                        command = new SqlCommand($"DELETE FROM tbl_user WHERE username='{input_username.Text}';", conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        command.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Maaf, user tidak ada dalam database");
                }

            }
            RefreshDataGrid();
        }
    }
}
