using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Voting.Models
{
    public class Presidentet
    {
        public int PresidentiId { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public int NumriPersonal { get; set; }
        public int NumriRendorPresidencial { get; set; }
    }
}
