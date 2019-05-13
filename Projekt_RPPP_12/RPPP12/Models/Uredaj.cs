using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Uredaj
    {
        public Uredaj()
        {
            Alarm = new HashSet<Alarm>();
        }

        public int SifraUredaja { get; set; }
        public string Status { get; set; }
        public int? SifraObjekta { get; set; }
        public int? SifraVrsteUredaja { get; set; }

        [Display(Name = "Objekt")]
        public Objekt SifraObjektaNavigation { get; set; }
        [Display(Name = "Vrsta uređaja")]
        public VrstaUredaja SifraVrsteUredajaNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
    }
}
