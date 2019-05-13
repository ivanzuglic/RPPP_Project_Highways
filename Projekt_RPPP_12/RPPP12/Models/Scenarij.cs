using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Vrsta objekta")]
        public int SifraVrsteObjekta { get; set; }
        [Display(Name = "Vrsta scenarija")]
        public int SifraVrsteScenarija { get; set; }
        [Display(Name = "Kategorija scenarija")]
        public KategorijaScenarija SifraVrsteScenarijaNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
    }
}
