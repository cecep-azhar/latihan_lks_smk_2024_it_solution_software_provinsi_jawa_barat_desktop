using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using static LatihanLKS.Controller;


namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void authentication(object sender, RoutedEventArgs e)
        {
            string username = input_username.Text;
            string password = input_password.Password;
            Controller.authentication(username, password);
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            input_username.Clear();
            input_password.Clear();
        }
    }
}