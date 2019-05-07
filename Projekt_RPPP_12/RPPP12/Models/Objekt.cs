using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Objekt
    {
        public Objekt()
        {
            Uredaj = new HashSet<Uredaj>();
        }

        public int SifraObjekta { get; set; }
        public int SifraDionice { get; set; }
        public int SifraVrstaObjekta { get; set; }

        public Dionica SifraDioniceNavigation { get; set; }
        public VrstaObjekta SifraVrstaObjektaNavigation { get; set; }
        public ICollection<Uredaj> Uredaj { get; set; }
    }
}
