using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class VrstaObjekta
    {
        public VrstaObjekta()
        {
            Objekt = new HashSet<Objekt>();
        }

        public int SifraVrsteObjekta { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string NazivObjekta { get; set; }

        public ICollection<Objekt> Objekt { get; set; }
    }
}
