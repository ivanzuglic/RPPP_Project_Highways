using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Sjediste
    {
        public Sjediste()
        {
            Upravitelj = new HashSet<Upravitelj>();
        }

        public int SifraSjedista { get; set; }
        public string ImeSjedista { get; set; }
        public string Adresa { get; set; }

        public ICollection<Upravitelj> Upravitelj { get; set; }
    }
}
