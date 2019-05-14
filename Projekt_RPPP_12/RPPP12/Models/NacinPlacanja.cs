using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class NacinPlacanja
    {
        public NacinPlacanja()
        {
            Racun = new HashSet<Racun>();
        }

        [Display(Name = "Šifra načina plaćanja")]
        public int SifraNacinPlacanja { get; set; }
        [Display(Name = "Način plaćanja")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string NacinPlacanja1 { get; set; }

        public ICollection<Racun> Racun { get; set; }
    }
}
