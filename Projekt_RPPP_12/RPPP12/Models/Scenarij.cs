using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Scenarij
    {
        public Scenarij()
        {
            Alarm = new HashSet<Alarm>();
        }

        public int SifraScenarija { get; set; }
        [Display(Name = "Naziv scenarija")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string NazivScenarija { get; set; }
        [StringLength(100, ErrorMessage = "Znakovno polje mora biti manje od 100 znakova.")]
        public string Procedura { get; set; }
        [Display(Name = "Vrsta objekta")]
        [Range(0, 99, ErrorMessage = "Upišite broj u rasponu od 0 do 99.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public int SifraVrsteObjekta { get; set; }
        [Display(Name = "Vrsta scenarija")]
        public int SifraVrsteScenarija { get; set; }
        [Display(Name = "Kategorija scenarija")]
        public KategorijaScenarija SifraVrsteScenarijaNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
    }
}
