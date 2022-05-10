using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Voting.Models
{
    public class Partia
    {

        public int PartiaId { get; set; }

        public string EmriPartis { get; set; }

        public string KryetariPartis { get; set; }

        public int NumriAntareve { get; set; }

        public int NumriRendorIpartis { get; set; }
    }
}
