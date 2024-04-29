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
    /// Interaction logic for LogActivity.xaml
    /// </summary>
    public partial class LogActivity : Page
    {
        public LogActivity()
        {
            InitializeComponent();
            date.SelectedDate = DateTime.Now;
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            conn.Open();
            string[] waktu = date.SelectedDate.Value.ToString().Split(" ");
            string[] date_time = waktu[0].Split("/");
            SqlCommand command = new SqlCommand($"SELECT tbl_log.id_log,tbl_user.username,tbl_log.waktu,tbl_log.aktivitas FROM tbl_log INNER JOIN tbl_user ON (tbl_log.id_user = tbl_user.id_user) WHERE (waktu BETWEEN '{date_time[2]}-{date_time[1]}-{date_time[0]} {waktu[1]}' AND '{date_time[2]}-{date_time[1]}-{(Convert.ToInt32(date_time[0]) + 1).ToString()} {waktu[1]}')", conn);
            SqlDataReader reader = command.ExecuteReader();
            data_log.Items.Clear();
            while (reader.Read())
            {
                Log log = new Log();
                log.id_log = (int)reader.GetValue(0);
                log.username = reader.GetValue(1).ToString();
                log.waktu = reader.GetValue(2).ToString();
                log.aktivitas = reader.GetValue(3).ToString();
                data_log.Items.Add(log);
            }
            conn.Close();
            reader.Close();
            command.Dispose();
        }

        private void FilterDate(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            conn.Open();
            string[] waktu = date.SelectedDate.Value.ToString().Split(" ");
            string[] date_time = waktu[0].Split("/");
            string[] waktu2 = date.SelectedDate.Value.AddDays(1).ToString().Split(" ");
            string[] date_time2 = waktu2[0].Split("/");
            SqlCommand command = new SqlCommand($"SELECT id_log,id_user,waktu,aktivitas FROM tbl_log WHERE (waktu BETWEEN '{date_time[2]}-{date_time[1]}-{date_time[0]} {waktu[1]}' AND '{date_time2[2]}-{date_time2[1]}-{date_time2[0]} {waktu2[1]}')", conn);
            SqlDataReader reader = command.ExecuteReader();
            Log log = new Log();
            data_log.Items.Clear();
            while (reader.Read())
            {
                log.id_log = (int)reader.GetValue(0);
                log.username = reader.GetValue(1).ToString();
                log.waktu = reader.GetValue(2).ToString();
                log.aktivitas = reader.GetValue(3).ToString();
                data_log.Items.Add(log);
            }
            conn.Close();
            reader.Close();
            command.Dispose();
        }
    }
}
