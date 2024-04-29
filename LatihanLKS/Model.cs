using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LatihanLKS
{
    internal class Model
    {
        public class User
        {
            public int id_user { get; set; }
            public string tipe_user { get; set; }
            public string nama { get; set; }
            public string alamat { get; set; }
            public string telepon { get; set; }
            public string username { get; set; }
            public string password { get; set; }
        }

        public class Log
        {
            public int id_log { get; set; }
            public string username { get; set; }
            public string waktu { get; set; }
            public string aktivitas { get; set; }
        }

        public class Transaksi
        {
            public string id_transaksi { get; set; }
            public string tgl_transaksi { get; set; }
            public string total_bayar { get; set; }
            public string nama { get; set; }
        }

        public class Barang
        {
            public string id_barang { get; set; }
            public string kode_barang { get; set; }
            public string nama_barang { get; set; }
            public string expired_date { get; set; }
            public string jumlah_barang { get; set; }
            public string satuan { get; set; }
            public string harga_satuan { get; set; }
        }

        public class TransaksiKasir
        {
            public string id_transaksi { get; set; }
            public string kode_barang { get; set; }
            public string nama_barang { get; set; }
            public string harga_satuan { get; set; }
            public int quantitas { get; set; }
            public string subtotal { get; set; }
        }
    }
}
