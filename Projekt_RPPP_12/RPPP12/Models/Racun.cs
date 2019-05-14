using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Racun
    {
        public int SifraRacun { get; set; }
        public int SifraKucica { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string RegistarskaOznaka { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string DatumVrijeme { get; set; }
        public int SifraKategorijaVozila { get; set; }
        public int SifraNacinPlacanja { get; set; }

        [Display(Name = "Kategorija vozila")]
        public KategorijaVozila SifraKategorijaVozilaNavigation { get; set; }
        [Display(Name = "Naplatna kućica")]
        public NaplatnaKucica SifraKucicaNavigation { get; set; }
        [Display(Name = "Način plaćanja")]
        public NacinPlacanja SifraNacinPlacanjaNavigation { get; set; }
    }
}
