using LatihanLKS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
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
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Microsoft.Win32;
using System.IO;
using System.Reflection.Metadata;
using iTextSharp.text.pdf;
using Syncfusion.UI.Xaml.Grid.Converter;

namespace LatihanLKS
{
    /// <summary>
    /// Interaction logic for KasirPage.xaml
    /// </summary>
    public partial class KasirPage : Window
    {
        public string[] credential = Settings.Default.credential.Split(".");
        public int index = 1;
        public KasirPage()
        {
            InitializeComponent();
            input_menu.SelectedIndex = 0;
            identitas_kasir.Content = credential[2];
            input_quantitas.Text = "1";
            input_harga_satuan.Text = "0";
            input_total_harga.Text = "0";
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            conn.Open();
            SqlCommand command = new SqlCommand($"SELECT kode_barang,nama_barang FROM tbl_barang", conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                input_menu.Items.Add($"{reader.GetString(0)} - {reader.GetString(1)}");
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

        private void AddToKeranjang(object sender, RoutedEventArgs e)
        {
            if (input_menu.SelectedIndex == 0)
            {
                MessageBox.Show("Pilih menu terlebih dahulu");
            }else
            {
                string[] menu = input_menu.Text.Split(" - ");
                TransaksiKasir dataKeranjang = new TransaksiKasir();
                dataKeranjang.kode_barang = menu[0];
                dataKeranjang.nama_barang = menu[1];
                dataKeranjang.harga_satuan = $"Rp. {input_harga_satuan.Text}";
                dataKeranjang.quantitas = Convert.ToInt32(input_quantitas.Text);
                dataKeranjang.subtotal = $"Rp. {input_total_harga.Text}";
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT id_transaksi FROM tbl_transaksi", conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    index++;
                }
                dataKeranjang.id_transaksi = $"TR{index}";
                conn.Close();
                reader.Close();
                command.Dispose();
                data_keranjang.Items.Add(dataKeranjang);
                index++;
                string harga = total_harga.Text.Split(" ")[1];
                string harga_masuk = dataKeranjang.subtotal.Split(" ")[1];
                total_harga.Text = $"Rp. {(Convert.ToInt32(harga) + Convert.ToInt32(harga_masuk)).ToString()}";
            }
        }

        private void InputJumlahBayar(object sender, TextChangedEventArgs e)
        {
            kembalian.Text = $"Rp. {Convert.ToInt32(input_jumlah_bayar.Text) - Convert.ToInt32(total_harga.Text.ToString().Split(" ")[1])}";
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            input_menu.SelectedIndex = 0;
            input_harga_satuan.Text = "0";
            input_quantitas.Text = "1";
            data_keranjang.Items.Clear();
            total_harga.Text = "Rp. 0";
            index = 1;
            input_menu.Focus();
        }

        private void MengetikHarga(object sender, TextChangedEventArgs e)
        {
            string[] normalisasi_harga = input_harga_satuan.Text.Split(".");
            string harga = "";
            for (int i = 0; i < normalisasi_harga.Length; i++)
            {
                harga += normalisasi_harga[i];
            }
            if (input_harga_satuan.Text.Length > 0)
            {
                int harga_satuan;
                if (!int.TryParse(harga, out harga_satuan))
                {
                    string isi = "";
                    for (int i = 0; i < (input_harga_satuan.Text.Length - 1); i++)
                    {
                        isi += input_harga_satuan.Text[i];
                    }
                    input_harga_satuan.Clear();
                    input_harga_satuan.Text = isi;
                    input_harga_satuan.Select(input_harga_satuan.Text.Length, 0);
                }
                int quantitas;
                if (int.TryParse(input_quantitas.Text, out quantitas))
                {
                    input_total_harga.Text = $"{(Convert.ToInt32(harga_satuan)) * (quantitas)}";
                }
                else
                {
                    input_total_harga.Text = $"{(Convert.ToInt32(harga_satuan)) * 0}";
                }
            }
            else
            {
                input_harga_satuan.Text = "0";
            }
        }


        private void MengetikQuantitas(object sender, TextChangedEventArgs e)
        {
            string[] normalisasi_harga = input_quantitas.Text.Split(".");
            string quantitas = "";
            for (int i = 0; i < normalisasi_harga.Length; i++)
            {
                quantitas += normalisasi_harga[i];
            }
            if (input_quantitas.Text.Length > 0)
            {
                int jumlah;
                if (!int.TryParse(quantitas, out jumlah))
                {
                    string isi = "";
                    for (int i = 0; i < (input_quantitas.Text.Length - 1); i++)
                    {
                        isi += input_quantitas.Text[i];
                    }
                    input_quantitas.Clear();
                    input_quantitas.Text = isi;
                    input_quantitas.Select(input_quantitas.Text.Length, 0);
                }
                int harga_satuan;
                if (int.TryParse(input_harga_satuan.Text, out harga_satuan))
                {
                    input_total_harga.Text = $"{jumlah * (harga_satuan)}";
                }else
                {
                    input_total_harga.Text = $"{0 * 0}";
                }
            }
            else
            {
                input_quantitas.Text = "0";
            }
        }

        private void MemilihMenu(object sender, SelectionChangedEventArgs e)
        {
            if (input_menu.SelectedIndex > 0)
            {
                string kode_barang = input_menu.SelectedValue.ToString().Split(" - ")[0];
                SqlConnection conn = new SqlConnection(Settings.Default.conString);
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT harga_satuan FROM tbl_barang WHERE kode_barang='{kode_barang}';", conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    input_harga_satuan.Text = reader.GetValue(0).ToString();
                }
                conn.Close();
                reader.Close();
                command.Dispose();
            }
        }

        private void SaveToDatabase(object sender, RoutedEventArgs e)
        {
            string[] waktu = DateTime.Now.ToString().Split(" ");
            string[] date_time = waktu[0].Split("/");
            string id_barang = "";
            SqlConnection conn = new SqlConnection(Settings.Default.conString);
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            conn.Open();

            TransaksiKasir rowdata = new TransaksiKasir();
            int rowcount = data_keranjang.Items.Count;
            List<TransaksiKasir> datalist = new List<TransaksiKasir>();
            var rows = (data_keranjang).SelectedItems;
            foreach (TransaksiKasir p in data_keranjang.Items)
            {
                MessageBoxResult answer = MessageBox.Show($"Apakah anda yakin data di keranjang sudah sesuai?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (answer == MessageBoxResult.Yes)
                {
                    command = new SqlCommand($"SELECT id_barang FROM tbl_barang WHERE kode_barang='{p.kode_barang}'", conn);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id_barang = reader.GetValue(0).ToString();
                    }
                    reader.Close();
                    command = new SqlCommand($"INSERT INTO tbl_transaksi(no_transaksi,total_bayar,tgl_transaksi,id_user,id_barang) VALUES('{p.id_transaksi}',{p.subtotal.Split(" ")[1]},'{date_time[2]}-{date_time[1]}-{date_time[0]} {waktu[1]}',{credential[0]},{id_barang});", conn);
                    command.ExecuteNonQuery();
                    log($"Create new transaksi", Convert.ToInt32(credential[0]));
                }
            }
            input_menu.SelectedIndex = 0;
            input_harga_satuan.Text = "0";
            input_quantitas.Text = "1";
            data_keranjang.Items.Clear();
            total_harga.Text = "Rp. 0";
            index = 1;
            input_menu.Focus();
            conn.Close();
            command.Dispose();
        }

        private void PrintInvoice(object sender, RoutedEventArgs e)
        {
            Pairing();
        }

        private async void Pairing()
        {
            /*var document = GridPdfExportExtension.ExportToPdf(data_keranjang);
            document.Save("Sample.pdf");*/
            if (data_keranjang.Items.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                sfd.ShowDialog();
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (IOException ex)
                    {
                        fileError = true;
                        MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                    }
                }
                /*if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }*/
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
            /*DeviceInformationCollection PairedBluetoothDevices = await DeviceInformation.FindAllAsync(BluetoothDevice.GetDeviceSelectorFromPairingState(true));
            foreach (DeviceInformation item in PairedBluetoothDevices)
            {
                MessageBox.Show(item.Name);
                if (item.Name == "")
                {
                    //connect to device use DeviceID  
                }
            }*/
        }
    }
}
