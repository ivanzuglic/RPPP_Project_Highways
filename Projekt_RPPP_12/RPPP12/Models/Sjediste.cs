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
        [Display(Name = "Ime sjedišta")]
        public string ImeSjedista { get; set; }
        public string Adresa { get; set; }

        public ICollection<Upravitelj> Upravitelj { get; set; }
    }
}
