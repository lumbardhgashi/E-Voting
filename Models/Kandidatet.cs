
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_Voting.Models
{
    public class Kandidatet
    {

        public int KandidatiId { get; set; }
        public string EmriKandidatit { get; set; }
        public string MbiemriKandidatit { get; set; }
        public int NumriRendor { get; set; }
        public int NumriPersonal { get; set; }

    }
}
