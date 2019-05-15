using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Upravitelj
    {
        public Upravitelj()
        {
            Autocesta = new HashSet<Autocesta>();
        }

        public int SifraUpravitelja { get; set; }
        [Required(ErrorMessage = "Obavezno polje.")]
        public int Oib { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Ime { get; set; }
        public int SifraSjedista { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Email { get; set; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Upišite broj mobitela.")]
        [StringLength(10, ErrorMessage = "Broj mobitela sadrži 10 znamenki.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Telefon { get; set; }

        [Display(Name = "Sjedište")]
        public Sjediste SifraSjedistaNavigation { get; set; }
        public ICollection<Autocesta> Autocesta { get; set; }
    }
}
