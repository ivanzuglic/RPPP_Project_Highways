using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class LokacijaAutoceste
    {
        public LokacijaAutoceste()
        {
            AutocestaSifraPocetkaNavigation = new HashSet<Autocesta>();
            AutocestaSifraZavrsetkaNavigation = new HashSet<Autocesta>();
        }

        [Display(Name = "Šifra lokacije")]
        public int SifraLokacije { get; set; }
        [Display(Name = "Ime lokacije")]
        public string ImeLokacije { get; set; }
        
        public ICollection<Autocesta> AutocestaSifraPocetkaNavigation { get; set; }
        public ICollection<Autocesta> AutocestaSifraZavrsetkaNavigation { get; set; }
    }
}
