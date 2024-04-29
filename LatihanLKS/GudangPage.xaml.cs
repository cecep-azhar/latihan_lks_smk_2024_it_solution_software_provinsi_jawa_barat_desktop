using LatihanLKS.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
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
using static LatihanLKS.Model;
using static LatihanLKS.Controller;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for GudangPage.xaml
    /// </summary>
    public partial class GudangPage : Window
    {
        public string[] credential = Settings.Default.credential.Split(".");
        public GudangPage()
        {
            InitializeComponent();
            input_expired_date.SelectedDate = DateTime.Now.AddMonths(3);
            input_satuan.SelectedIndex = 0;
            RefreshDataGrid();
        }

        public void RefreshDataGrid(string query = "SELECT id_barang,kode_barang,nama_barang,expired_date,jumlah_barang,satuan,harga_satuan FROM tbl_barang")
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            data_barang.Items.Clear();
            while (reader.Read())
            {
                Barang barang = new Barang();
                barang.id_barang = $"BRG{reader.GetValue(0)}";
                barang.kode_barang = reader.GetValue(1).ToString();
                barang.nama_barang = reader.GetValue(2).ToString();
                barang.expired_date = reader.GetValue(3).ToString();
                barang.jumlah_barang = reader.GetValue(4).ToString();
                barang.satuan = reader.GetValue(5).ToString();
                barang.harga_satuan = $"Rp. {reader.GetValue(6)}";
                data_barang.Items.Add(barang);
            }
            conn.Close();
            reader.Close();
            command.Dispose();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            log($"Logout", Convert.ToInt32(credential[0]));
            mainWindow.Show();
            Close();
        }

        private void Mencari(object sender, TextChangedEventArgs e)
        {
            if (input_keyword.Text.Length > 0)
            {
                placeholder_keyword.Visibility = Visibility.Hidden;
                RefreshDataGrid($"SELECT id_barang,kode_barang,nama_barang,expired_date,jumlah_barang,satuan,harga_satuan FROM tbl_barang WHERE nama_barang LIKE '{input_keyword.Text}%';");
            }
            else
            {
                placeholder_keyword.Visibility = Visibility.Visible;
                RefreshDataGrid();
            }
        }

        private void AddBarang(object sender, RoutedEventArgs e)
        {
            int ceck;
            if (input_kode_barang.Text == "" || input_kode_barang.Text == null)
            {
                MessageBox.Show("Mohon isikan kode barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_nama_barang.Text == "" || input_nama_barang.Text == null)
            {
                MessageBox.Show("Mohon masukan nama barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_expired_date.Text == "" || input_expired_date.Text == null)
            {
                MessageBox.Show("Mohon tentukan tanggal kadaluarsa", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_jumlah_barang.Text == "" || input_jumlah_barang.Text == null)
            {
                MessageBox.Show("Mohon isikan jumlah barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (!int.TryParse(input_jumlah_barang.Text, out ceck))
            {
                MessageBox.Show("Mohon isikan jumlah barang dengan angka", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_satuan.Text == "" || input_satuan.Text == null)
            {
                MessageBox.Show("Mohon tentukan satuan barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_harga_satuan.Text == "" || input_harga_satuan.Text == null)
            {
                MessageBox.Show("Mohon beri harga per satuan barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (!int.TryParse(input_harga_satuan.Text, out ceck))
            {
                MessageBox.Show("Mohon isikan harga barang dengan angka", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                SqlCommand command = new SqlCommand($"SELECT * FROM tbl_barang WHERE kode_barang='{input_kode_barang.Text}'", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                int result = 0;
                while (reader.Read())
                {
                    result++;
                }
                conn.Close();
                reader.Close();
                command.Dispose();
                if (result == 0)
                {
                    MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin data sudah sesuai?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (answer == MessageBoxResult.Yes)
                    {
                        string[] waktu = input_expired_date.Text.Split('/');
                        string date = $"{waktu[2]}-{waktu[1]}-{waktu[0]}";
                        conn.Open();
                        command = new SqlCommand($"INSERT INTO tbl_barang(kode_barang,nama_barang,expired_date,jumlah_barang,satuan,harga_satuan) VALUES('{input_kode_barang.Text.ToLower()}','{input_nama_barang.Text}','{date}',{input_jumlah_barang.Text},'{input_satuan.Text}',{input_harga_satuan.Text});", conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        command.Dispose();
                        log($"Add new data barang", Convert.ToInt32(credential[0]));
                    }
                }
                else
                {
                    MessageBox.Show("Maaf, barang sudah terdaftar di gudang. Mohon lakukan pengeditan atau masukan kode barang lain!");
                }
            }
            RefreshDataGrid();
        }

        private void Compare(object sender, MouseButtonEventArgs e)
        {
            Barang barang = this.data_barang.SelectedItem as Barang;
            if (barang.satuan.ToString() == "Botol")
            {
                input_satuan.SelectedIndex = 0;
            }
            else if (barang.satuan.ToString() == "Box")
            {
                input_satuan.SelectedIndex = 1;
            }
            input_kode_barang.Text = barang.kode_barang.ToString();
            input_nama_barang.Text = barang.nama_barang.ToString() ;
            input_expired_date.SelectedDate = DateTime.Parse(barang.expired_date);
            input_jumlah_barang.Text = barang.jumlah_barang.ToString();
            string harga = barang.harga_satuan.ToString().Split(' ')[1];
            input_harga_satuan.Text = harga;

        }

        private void EditBarang(object sender, RoutedEventArgs e)
        {
            if (input_kode_barang.Text == "" || input_kode_barang.Text == null)
            {
                MessageBox.Show("Mohon isikan kode barang terlebih dahulu", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_nama_barang.Text == "" || input_nama_barang.Text == null)
            {
                MessageBox.Show("Mohon beri nama barang untuk disimpan", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_expired_date.Text == "" || input_expired_date.Text == null)
            {
                MessageBox.Show("Mohon pilih tanggal kadaluarsa", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_jumlah_barang.Text == "" || input_jumlah_barang.Text == null)
            {
                MessageBox.Show("Mohon jumlah barang untuk dimasukan", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_satuan.Text == "" || input_satuan.Text == null)
            {
                MessageBox.Show("Mohon pilih satuan untuk barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_harga_satuan.Text == "" || input_harga_satuan.Text == null)
            {
                MessageBox.Show("Mohon beri harga per satuan barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                SqlCommand command = new SqlCommand($"SELECT * FROM tbl_barang WHERE kode_barang='{input_kode_barang.Text}'", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                int result=0;
                while (reader.Read())
                {
                    result++;
                }
                conn.Close();
                reader.Close();
                command.Dispose();
                if (result > 0)
                {
                    MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin ingin mengubah data barang {input_nama_barang.Text}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (answer == MessageBoxResult.Yes)
                    {
                        conn.Open();
                        string[] waktu = input_expired_date.Text.Split('/');
                        string date = $"{waktu[2]}-{waktu[1]}-{waktu[0]}";
                        command = new SqlCommand($"UPDATE tbl_barang SET kode_barang='{input_kode_barang.Text.ToLower()}',nama_barang='{input_nama_barang.Text}',expired_date='{date}',jumlah_barang='{input_jumlah_barang.Text}',satuan='{input_satuan.Text}',harga_satuan={input_harga_satuan.Text} WHERE kode_barang='{input_kode_barang.Text}';", conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        command.Dispose();
                        log($"Edit data barang", Convert.ToInt32(credential[0]));
                    }
                }
                else
                {
                    MessageBox.Show("Maaf, barang belum terdaftar di gudang");
                }

            }
            RefreshDataGrid();
        }

        private void RemoveBarang(object sender, RoutedEventArgs e)
        {
            if (input_kode_barang.Text == "" || input_kode_barang.Text == null)
            {
                MessageBox.Show("Mohon isikan kode barang terlebih dahulu", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_nama_barang.Text == "" || input_nama_barang.Text == null)
            {
                MessageBox.Show("Mohon beri nama barang untuk disimpan", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_expired_date.Text == "" || input_expired_date.Text == null)
            {
                MessageBox.Show("Mohon pilih tanggal kadaluarsa", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_jumlah_barang.Text == "" || input_jumlah_barang.Text == null)
            {
                MessageBox.Show("Mohon jumlah barang untuk dimasukan", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_satuan.Text == "" || input_satuan.Text == null)
            {
                MessageBox.Show("Mohon pilih satuan untuk barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else if (input_harga_satuan.Text == "" || input_harga_satuan.Text == null)
            {
                MessageBox.Show("Mohon beri harga per satuan barang", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            }
            else
            {
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                SqlCommand command = new SqlCommand($"SELECT * FROM tbl_barang WHERE kode_barang='{input_kode_barang.Text}'", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                int result = 0;
                while (reader.Read())
                {
                    result++;
                }
                conn.Close();
                reader.Close();
                command.Dispose();
                if (result > 0)
                {
                    MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin ingin menghapus {input_nama_barang.Text} dari gudang?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (answer == MessageBoxResult.Yes)
                    {
                        conn.Open();
                        command = new SqlCommand($"DELETE FROM tbl_barang WHERE kode_barang='{input_kode_barang.Text}';", conn);
                        command.ExecuteNonQuery();
                        conn.Close();
                        command.Dispose();
                        log($"Remove data barang", Convert.ToInt32(credential[0]));
                    }
                }
                else
                {
                    MessageBox.Show("Maaf, barang tidak terdaftar di gudang");
                }
            }
            RefreshDataGrid();
        }
    }
}
