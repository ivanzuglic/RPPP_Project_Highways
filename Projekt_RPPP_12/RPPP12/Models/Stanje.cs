using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Stanje
    {
        public int SifraStanje { get; set; }
        public byte[] VrijemePocetka { get; set; }
        public TimeSpan? VrijemeZavrsetka { get; set; }
        public int? SifraDogadaj { get; set; }
        public int? SifraZabrana { get; set; }
        [StringLength(100, MinimumLength = 1)]
        public string Opis { get; set; }

        [Display(Name = "Događaj")]
        public Dogadaj SifraDogadajNavigation { get; set; }
        [Display(Name = "Zabrana")]
        public Zabrana SifraZabranaNavigation { get; set; }
    }
}
