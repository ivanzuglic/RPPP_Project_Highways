using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RPPP12.Models
{
    public partial class VrstaNaplatneKucice
    {
        public VrstaNaplatneKucice()
        {
            NaplatnaKucica = new HashSet<NaplatnaKucica>();
        }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string VrstaNaplatneKucice1 { get; set; }

        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
    }
}
