using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Voting.Models
{
    public class OrganizatatPerZgjedhje
    {
        public int Id { get; set; }
        public string Emri { get; set; }
        public string KryetariOrganizates { get; set; }
        public int NumriPersonalKryetarit { get; set; }
    }
}
