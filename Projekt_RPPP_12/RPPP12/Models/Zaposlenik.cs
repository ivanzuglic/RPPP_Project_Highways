using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            NaplatnaKucica = new HashSet<NaplatnaKucica>();
        }

        public int SifraZaposlenika { get; set; }
        [Display(Name = "Ime")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Ime { get; set; }
        [Display(Name = "Prezime")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Prezime { get; set; }
        [Display(Name = "Telefon")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Upišite broj mobitela.")]
        [StringLength(10, ErrorMessage = "Broj telefona sadrži 10 znamenki.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Telefon { get; set; }
        public int? SifraPostaje { get; set; }
        public int? SifraVrsteZaposlenika { get; set; }

        [Display(Name = "Ime postaje")]
        public NaplatnaPostaja SifraPostajeNavigation { get; set; }
        [Display(Name = "Vrsta zaposlenika")]
        public VrstaZaposlenika SifraVrsteZaposlenikaNavigation { get; set; }
        [Display(Name = "Naplatna kućica")]
        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
    }
}
