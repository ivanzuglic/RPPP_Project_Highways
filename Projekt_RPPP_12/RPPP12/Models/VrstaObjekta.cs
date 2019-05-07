using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class VrstaObjekta
    {
        public VrstaObjekta()
        {
            Objekt = new HashSet<Objekt>();
        }

        public int SifraVrsteObjekta { get; set; }
        public string NazivObjekta { get; set; }

        public ICollection<Objekt> Objekt { get; set; }
    }
}
