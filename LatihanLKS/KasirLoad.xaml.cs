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

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for KasirLoad.xaml
    /// </summary>
    public partial class KasirLoad : Window
    {
        public KasirLoad()
        {
            InitializeComponent();
            LaunchTask();
        }

        async System.Threading.Tasks.Task MyTask()
        {
            await Task.Run(async () =>
            {
                await System.Threading.Tasks.Task.Delay(5000);
                this.Dispatcher.Invoke(() =>
                {
                    KasirPage kasirPage = new KasirPage();
                    kasirPage.Show();
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
