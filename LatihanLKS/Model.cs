using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
