using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Voting.Models
{
    public class Shteti
    {

        public int ShtetiId { get; set; }

        public string ShtetiEmri { get; set; }

        public string Kryeministri { get; set; }

        public int NrPopullsis { get; set; }

        public int NrQyteteve { get; set; }

        public string OrganizataPerZgjedhje { get; set; }
        
    }
}
