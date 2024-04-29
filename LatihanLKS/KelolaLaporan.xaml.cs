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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for KelolaLaporan.xaml
    /// </summary>
    public partial class KelolaLaporan : Page
    {
        public KelolaLaporan()
        {
            InitializeComponent();
            input_first_date.SelectedDate = DateTime.Now;
            input_last_date.SelectedDate = DateTime.Now.AddDays(1);
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            conn.Open();
            string[] waktu_awal = input_first_date.SelectedDate.Value.ToString().Split(" ");
            string[] first_date_time = waktu_awal[0].Split("/");
            string[] waktu_akhir = input_last_date.SelectedDate.Value.ToString().Split(" ");
            string[] last_date_time = waktu_akhir[0].Split("/");
            SqlCommand command = new SqlCommand($"SELECT tbl_transaksi.id_transaksi,tbl_transaksi.tgl_transaksi,tbl_transaksi.total_bayar,tbl_user.nama FROM tbl_transaksi INNER JOIN tbl_user ON (tbl_transaksi.id_user = tbl_user.id_user) WHERE (tbl_transaksi.tgl_transaksi BETWEEN '{first_date_time[2]}-{first_date_time[1]}-{first_date_time[0]} {waktu_awal[1]}' AND '{last_date_time[2]}-{last_date_time[1]}-{last_date_time[0]} {waktu_akhir[1]}')", conn);
            SqlDataReader reader = command.ExecuteReader();
            data_transaksi.Items.Clear();
            while (reader.Read())
            {
                Transaksi transaksi = new Transaksi();
                transaksi.id_transaksi = $"TR{reader.GetValue(0)}";
                transaksi.tgl_transaksi = reader.GetValue(1).ToString();
                transaksi.total_bayar = $"Rp. {reader.GetValue(2)}";
                transaksi.nama = reader.GetValue(3).ToString();
                data_transaksi.Items.Add(transaksi);
            }
            conn.Close();
            reader.Close();
            command.Dispose();
        }

        private void FillterTransaksi(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            conn.Open();
            string[] waktu_awal = input_first_date.SelectedDate.Value.ToString().Split(" ");
            string[] first_date_time = waktu_awal[0].Split("/");
            string[] waktu_akhir = input_last_date.SelectedDate.Value.ToString().Split(" ");
            string[] last_date_time = waktu_akhir[0].Split("/");
            SqlCommand command = new SqlCommand($"SELECT tbl_transaksi.id_transaksi,tbl_transaksi.tgl_transaksi,tbl_transaksi.total_bayar,tbl_user.nama FROM tbl_transaksi INNER JOIN tbl_user ON (tbl_transaksi.id_user = tbl_user.id_user) WHERE (tgl_transaksi BETWEEN '{first_date_time[2]}-{first_date_time[1]}-{first_date_time[0]} {waktu_awal[1]}' AND '{last_date_time[2]}-{last_date_time[1]}-{last_date_time[0]} {waktu_akhir[1]}')", conn);
            SqlDataReader reader = command.ExecuteReader();
            data_transaksi.Items.Clear();
            while (reader.Read())
            {

                Transaksi transaksi = new Transaksi();
                transaksi.id_transaksi = $"TR{reader.GetValue(0)}";
                transaksi.tgl_transaksi = reader.GetValue(1).ToString();
                transaksi.total_bayar = $"Rp. {reader.GetValue(2)}";
                transaksi.nama = reader.GetValue(3).ToString();
                data_transaksi.Items.Add(transaksi);
            }
            conn.Close();
            reader.Close();
            command.Dispose();
        }

        private void GenerateChart(object sender, RoutedEventArgs e)
        {
            /*Transaksi rowdata = new Transaksi();
            int rowcount = data_transaksi.Items.Count;
            List<Transaksi> datalist = new List<Transaksi>();
            var rows = (data_transaksi).SelectedItems;
            foreach (Transaksi row in data_transaksi.Items )
            {
                datalist.Add(row);
            }
            data_chart.Resources.Add(datalist.ToArray());*/
        }
    }
}
