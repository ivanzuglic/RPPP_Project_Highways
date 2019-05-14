using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class RazinaOpasnosti
    {
        public RazinaOpasnosti()
        {
            Dogadaj = new HashSet<Dogadaj>();
        }

        public int SifraRazinaOpasnosti { get; set; }
        [Display(Name = "Naziv")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string NazivRazinaOpasnosti { get; set; }

        public ICollection<Dogadaj> Dogadaj { get; set; }
    }
}
