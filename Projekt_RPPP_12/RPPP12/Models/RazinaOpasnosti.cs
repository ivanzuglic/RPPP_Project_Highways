using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class RazinaOpasnosti
    {
        public RazinaOpasnosti()
        {
            Dogadaj = new HashSet<Dogadaj>();
        }

        public int SifraRazinaOpasnosti { get; set; }
        [Display(Name = "Naziv")]
        public string NazivRazinaOpasnosti { get; set; }

        public ICollection<Dogadaj> Dogadaj { get; set; }
    }
}
