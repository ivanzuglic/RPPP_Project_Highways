using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class RazinaOpasnosti
    {
        public RazinaOpasnosti()
        {
            Dogadaj = new HashSet<Dogadaj>();
        }

        public int SifraRazinaOpasnosti { get; set; }
        public string NazivRazinaOpasnosti { get; set; }

        public ICollection<Dogadaj> Dogadaj { get; set; }
    }
}
