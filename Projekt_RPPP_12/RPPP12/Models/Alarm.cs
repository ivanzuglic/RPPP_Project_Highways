using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Alarm
    {
        public int SifraUredaja { get; set; }
        public int SifraDogadaja { get; set; }
        public int? SifraScenarija { get; set; }
        public int? SifraOperatera { get; set; }

        public Dogadaj SifraDogadajaNavigation { get; set; }
        public Scenarij SifraScenarijaNavigation { get; set; }
        public Uredaj SifraUredajaNavigation { get; set; }
    }
}
