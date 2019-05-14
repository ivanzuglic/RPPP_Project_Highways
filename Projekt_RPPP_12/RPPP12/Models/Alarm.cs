using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Alarm
    {
        [Display(Name = "Šifra uređaja")]
        public int SifraUredaja { get; set; }
        [Display(Name = "Šifra događaja")]
        public int SifraDogadaja { get; set; }
        [Display(Name = "Šifra scenarija")]
        public int? SifraScenarija { get; set; }
        [Display(Name = "Šifra operatera")]
        public int? SifraOperatera { get; set; }

        [Display(Name = "Događaj")]
        public Dogadaj SifraDogadajaNavigation { get; set; }
        [Display(Name = "Scenarij")]
        public Scenarij SifraScenarijaNavigation { get; set; }
        [Display(Name = "Uređaj")]
        public Uredaj SifraUredajaNavigation { get; set; }
    }
}
