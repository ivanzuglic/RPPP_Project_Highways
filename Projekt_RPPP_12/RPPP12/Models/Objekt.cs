using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = " Dionice")]
        public Dionica SifraDioniceNavigation { get; set; }
        [Display(Name = "Vrsta objekta")]
        public VrstaObjekta SifraVrstaObjektaNavigation { get; set; }
        public ICollection<Uredaj> Uredaj { get; set; }
    }
}
