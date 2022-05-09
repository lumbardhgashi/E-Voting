using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Voting.Models
{
    public class Komentet
    {
        public int Id { get; set; }
        public int  NumriPersonal { get; set; }
        public string  Email { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public string Komenti { get; set; }

    }
}
