using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class VrstaZaposlenika
    {
        public VrstaZaposlenika()
        {
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int SifraVrsteZaposlenika { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Naziv { get; set; }

        public ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
