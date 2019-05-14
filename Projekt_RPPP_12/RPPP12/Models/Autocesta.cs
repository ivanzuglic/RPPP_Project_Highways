using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Autocesta
    {
        public Autocesta()
        {
            Dionica = new HashSet<Dionica>();
        }
        [Range(0, 99999, ErrorMessage = "Upišite broj u rasponu od 0 do 99999.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public int SifraAutoceste { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string ImeAutoceste { get; set; }
        public int SifraPocetka { get; set; }
        public int SifraZavrsetka { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string Nadimak { get; set; }
        public int SifraUpravitelja { get; set; }
        [Range(0, 99999, ErrorMessage = "Upišite broj u rasponu od 0 do 99999.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public int Kilometraza { get; set; }
        public int? SifraNacinaPlacanja { get; set; }

        public SustavNaplate SifraNacinaPlacanjaNavigation { get; set; }
        [Display(Name = "Početak")]
        public LokacijaAutoceste SifraPocetkaNavigation { get; set; }
        [Display(Name = "Upravitelj")]
        public Upravitelj SifraUpraviteljaNavigation { get; set; }
        [Display(Name = "Kraj")]
        public LokacijaAutoceste SifraZavrsetkaNavigation { get; set; }
        public ICollection<Dionica> Dionica { get; set; }
    }
}
