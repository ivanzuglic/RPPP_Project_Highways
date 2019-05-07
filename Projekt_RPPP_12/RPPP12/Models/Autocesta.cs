using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Autocesta
    {
        public Autocesta()
        {
            Dionica = new HashSet<Dionica>();
        }

        public int SifraAutoceste { get; set; }
        public string ImeAutoceste { get; set; }
        public int SifraPocetka { get; set; }
        public int SifraZavrsetka { get; set; }
        public string Nadimak { get; set; }
        public int SifraUpravitelja { get; set; }
        public int Kilometraza { get; set; }
        public int? SifraNacinaPlacanja { get; set; }

        public SustavNaplate SifraNacinaPlacanjaNavigation { get; set; }
        public LokacijaAutoceste SifraPocetkaNavigation { get; set; }
        public Upravitelj SifraUpraviteljaNavigation { get; set; }
        public LokacijaAutoceste SifraZavrsetkaNavigation { get; set; }
        public ICollection<Dionica> Dionica { get; set; }
    }
}
