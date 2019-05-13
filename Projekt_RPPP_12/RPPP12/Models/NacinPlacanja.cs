using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class NacinPlacanja
    {
        public NacinPlacanja()
        {
            Racun = new HashSet<Racun>();
        }

        public int SifraNacinPlacanja { get; set; }
        [Display(Name = "Način plaćanja")]
        public string NacinPlacanja1 { get; set; }

        public ICollection<Racun> Racun { get; set; }
    }
}
