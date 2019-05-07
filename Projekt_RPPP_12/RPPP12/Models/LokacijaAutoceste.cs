using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class LokacijaAutoceste
    {
        public LokacijaAutoceste()
        {
            AutocestaSifraPocetkaNavigation = new HashSet<Autocesta>();
            AutocestaSifraZavrsetkaNavigation = new HashSet<Autocesta>();
        }

        public int SifraLokacije { get; set; }
        public string ImeLokacije { get; set; }

        public ICollection<Autocesta> AutocestaSifraPocetkaNavigation { get; set; }
        public ICollection<Autocesta> AutocestaSifraZavrsetkaNavigation { get; set; }
    }
}
