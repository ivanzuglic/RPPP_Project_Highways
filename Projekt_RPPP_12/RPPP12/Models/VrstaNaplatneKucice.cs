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

        [Display(Name = "Vrsta naplatne kućice")]
        public string VrstaNaplatneKucice1 { get; set; }
        [Display(Name = "Naplatna kućica")]
        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
    }
}
