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
        [Range(0, 99999, ErrorMessage = "Upišite broj u rasponu od 0 do 99999.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public int SifraLokacije { get; set; }
        [Display(Name = "Ime lokacije")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string ImeLokacije { get; set; }
        
        public ICollection<Autocesta> AutocestaSifraPocetkaNavigation { get; set; }
        public ICollection<Autocesta> AutocestaSifraZavrsetkaNavigation { get; set; }
    }
}
