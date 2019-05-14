using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class KategorijaScenarija
    {
        public KategorijaScenarija()
        {
            Scenarij = new HashSet<Scenarij>();
        }

        [Display(Name = "Šifra kategorije scenarija")]
        public int SifraKategorijeScenarija { get; set; }
        [Display(Name = "Kategorija scenarija")]
        public string NazivKategorijeScenarija { get; set; }

        public ICollection<Scenarij> Scenarij { get; set; }
    }
}
