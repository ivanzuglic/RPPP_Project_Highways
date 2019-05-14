using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class VrstaUredaja
    {
        public VrstaUredaja()
        {
            Uredaj = new HashSet<Uredaj>();
        }

        public int SifraVrsteUredaja { get; set; }
        [Display(Name = "Naziv vrste uređaja")]
        public string NazivVrsteUredaja { get; set; }

        public ICollection<Uredaj> Uredaj { get; set; }
    }
}
