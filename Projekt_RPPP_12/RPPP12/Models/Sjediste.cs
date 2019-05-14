using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RPPP12.Models
{
    public partial class Sjediste
    {
        public Sjediste()
        {
            Upravitelj = new HashSet<Upravitelj>();
        }

        public int SifraSjedista { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string ImeSjedista { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Adresa { get; set; }

        public ICollection<Upravitelj> Upravitelj { get; set; }
    }
}
