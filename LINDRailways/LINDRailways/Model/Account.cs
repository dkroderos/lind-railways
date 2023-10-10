using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class Account
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
        public int Balance { get; set; }
    }
}
