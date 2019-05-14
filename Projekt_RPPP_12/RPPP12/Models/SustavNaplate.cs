using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RPPP12.Models
{
    public partial class SustavNaplate
    {
        public SustavNaplate()
        {
            Autocesta = new HashSet<Autocesta>();
        }

        public int SifraNacinaPlacanja { get; set; }
        [Display(Name = "Način plačanja")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string NacinPlacanja { get; set; }

        public ICollection<Autocesta> Autocesta { get; set; }
    }
}
