using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Stanje
    {
        public int SifraStanje { get; set; }
        public byte[] VrijemePocetka { get; set; }
        public TimeSpan? VrijemeZavrsetka { get; set; }
        public int? SifraDogadaj { get; set; }
        public int? SifraZabrana { get; set; }
        public string Opis { get; set; }

        public Dogadaj SifraDogadajNavigation { get; set; }
        public Zabrana SifraZabranaNavigation { get; set; }
    }
}
