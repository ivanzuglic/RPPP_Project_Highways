using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class VrstaNaplatneKucice
    {
        public VrstaNaplatneKucice()
        {
            NaplatnaKucica = new HashSet<NaplatnaKucica>();
        }

        public string VrstaNaplatneKucice1 { get; set; }

        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
    }
}
