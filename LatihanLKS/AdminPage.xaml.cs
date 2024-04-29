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
using static LatihanLKS.Model;
using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public string[] credential = Settings.Default.credential.Split(".");
        public AdminPage()
        {
            InitializeComponent();
            LogActivity logActivity = new LogActivity();
            main_content.Content = logActivity;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            log("Logout", Convert.ToInt32(credential[0]));
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void toKelolaUser(object sender, RoutedEventArgs e)
        {
            KelolaUser kelolaUser = new KelolaUser();
            main_content.Content = kelolaUser;
        }

        private void toKelolaLaporan(object sender, RoutedEventArgs e)
        {
            KelolaLaporan kelolaLaporan = new KelolaLaporan();
            main_content.Content = kelolaLaporan;
        }
    }
}
