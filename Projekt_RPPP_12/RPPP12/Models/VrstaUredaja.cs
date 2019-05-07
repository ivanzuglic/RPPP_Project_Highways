using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class VrstaUredaja
    {
        public VrstaUredaja()
        {
            Uredaj = new HashSet<Uredaj>();
        }

        public int SifraVrsteUredaja { get; set; }
        public string NazivVrsteUredaja { get; set; }

        public ICollection<Uredaj> Uredaj { get; set; }
    }
}
