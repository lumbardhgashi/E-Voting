using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Voting.Models
{
    public class FletVotimiZyrtar
    {
        public int Id { get; set; }
        public string TipiZgjedhjeve { get; set; }

        public DateTime KohaStartuese { get; set; }
        public DateTime KohaPerfunduese { get; set; }
    }
}
