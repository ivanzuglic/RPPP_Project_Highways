using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class SustavNaplate
    {
        public SustavNaplate()
        {
            Autocesta = new HashSet<Autocesta>();
        }

        public int SifraNacinaPlacanja { get; set; }
        public string NacinPlacanja { get; set; }

        public ICollection<Autocesta> Autocesta { get; set; }
    }
}
