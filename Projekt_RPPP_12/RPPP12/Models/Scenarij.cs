using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Scenarij
    {
        public Scenarij()
        {
            Alarm = new HashSet<Alarm>();
        }

        public int SifraScenarija { get; set; }
        public string NazivScenarija { get; set; }
        public string Procedura { get; set; }
        public int SifraVrsteObjekta { get; set; }
        public int SifraVrsteScenarija { get; set; }

        public KategorijaScenarija SifraVrsteScenarijaNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
    }
}
