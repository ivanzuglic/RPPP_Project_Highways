using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class KategorijaScenarija
    {
        public KategorijaScenarija()
        {
            Scenarij = new HashSet<Scenarij>();
        }

        public int SifraKategorijeScenarija { get; set; }
        public string NazivKategorijeScenarija { get; set; }

        public ICollection<Scenarij> Scenarij { get; set; }
    }
}
