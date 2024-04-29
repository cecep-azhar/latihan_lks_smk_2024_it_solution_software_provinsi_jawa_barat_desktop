using LatihanLKS.Properties;
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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void authentication(object sender, RoutedEventArgs e)
        {
            if (input_username.Text != "")
            {
                if (input_password.Password != "")
                {
                    string username = input_username.Text;
                    string password = input_password.Password;
                    Controller.authentication(username, password);
                    string[] credential = Settings.Default.credential.Split('.');
                    if (credential.Length > 1)
                    {
                        if (credential[1] == "admin")
                        {
                            AdminLoad adminLoad = new AdminLoad();
                            adminLoad.Show();
                            Close();
                        }
                        else if (credential[1] == "gudang")
                        {
                            GudangLoad gudangLoad = new GudangLoad();
                            gudangLoad.Show();
                            Close();
                        }
                        else if (credential[1] == "kasir")
                        {
                            KasirLoad kasirLoad = new KasirLoad();
                            kasirLoad.Show();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Maaf, data tidak terdaftar di database!");
                    }
                }else
                {
                    MessageBox.Show("Mohon isi bagian password terlebih dahulu!");
                }
            }else
            {
                MessageBox.Show("Mohon isi bagian username terlebih dahulu!");
            }
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            input_username.Clear();
            input_password.Clear();
            input_username.Focus();
        }

        private void mengisiUsername(object sender, TextChangedEventArgs e)
        {
            if (input_username.Text.Length > 0)
            {
                placeholder_username.Visibility = Visibility.Hidden;
            }
            else
            {
                placeholder_username.Visibility = Visibility.Visible;
            }
        }

        private void mengisiPassword(object sender, RoutedEventArgs e)
        {
            if (input_password.Password.Length > 0)
            {
                placeholder_password.Visibility = Visibility.Hidden;
            }
            else
            {
                placeholder_password.Visibility = Visibility.Visible;
            }
        }
    }
}
