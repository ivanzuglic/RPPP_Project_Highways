using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Stanje
    {
        public int SifraStanje { get; set; }
        [Display(Name = "Vrijeme početka")]
        public byte[] VrijemePocetka { get; set; }
        [Display(Name = "Vrijeme završetka")]
        public TimeSpan? VrijemeZavrsetka { get; set; }
        public int? SifraDogadaj { get; set; }
        public int? SifraZabrana { get; set; }
        public string Opis { get; set; }

        [Display(Name = "Događaj")]
        public Dogadaj SifraDogadajNavigation { get; set; }
        [Display(Name = "Zabrana")]
        public Zabrana SifraZabranaNavigation { get; set; }
    }
}
