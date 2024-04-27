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
using System.Data.SqlClient;
using LatihanLKS.Properties;
using System.Data;
using static LatihanLKS.Controller;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_log", conn);
            //DataSet ds = new DataSet();
            ItemsControl ds = new ItemsControl();
            da.Equals(ds);
            Console.WriteLine(da);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void toKelolaUser(object sender, RoutedEventArgs e)
        {
            redirect("kelola_user");
        }

        private void toKelolaLaporan(object sender, RoutedEventArgs e)
        {

        }
    }
}
