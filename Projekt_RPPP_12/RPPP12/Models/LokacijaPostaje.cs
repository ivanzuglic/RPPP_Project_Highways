using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class LokacijaPostaje
    {
        public LokacijaPostaje()
        {
            DionicaSifraKrajaNavigation = new HashSet<Dionica>();
            DionicaSifraPocetkaNavigation = new HashSet<Dionica>();
            NaplatnaPostaja = new HashSet<NaplatnaPostaja>();
        }

        [Display(Name = "Šifra lokacije")]
        public int SifraLokacije { get; set; }
        [Display(Name = "Naziv lokacije")]
        public string NazivLokacije { get; set; }

        public ICollection<Dionica> DionicaSifraKrajaNavigation { get; set; }
        public ICollection<Dionica> DionicaSifraPocetkaNavigation { get; set; }
        public ICollection<NaplatnaPostaja> NaplatnaPostaja { get; set; }
    }
}
