using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class NacinPlacanja
    {
        public NacinPlacanja()
        {
            Racun = new HashSet<Racun>();
        }

        public int SifraNacinPlacanja { get; set; }
        public string NacinPlacanja1 { get; set; }

        public ICollection<Racun> Racun { get; set; }
    }
}
