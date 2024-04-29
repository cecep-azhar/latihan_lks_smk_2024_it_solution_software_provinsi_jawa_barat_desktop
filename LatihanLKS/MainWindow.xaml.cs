using LatihanLKS.Properties;
using System;
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
            Settings.Default.credential = "";
            LaunchTask();
        }

        async System.Threading.Tasks.Task MyTask()
        {
            await Task.Run(async () =>
            {
                await System.Threading.Tasks.Task.Delay(5000);
                this.Dispatcher.Invoke(() =>
                {
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    Close();
                });
            });
        }

        private async void LaunchTask()
        {
            await MyTask();
        }
    }
}