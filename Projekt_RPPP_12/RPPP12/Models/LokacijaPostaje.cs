using System;
using System.Collections.Generic;

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

        public int SifraLokacije { get; set; }
        public string NazivLokacije { get; set; }

        public ICollection<Dionica> DionicaSifraKrajaNavigation { get; set; }
        public ICollection<Dionica> DionicaSifraPocetkaNavigation { get; set; }
        public ICollection<NaplatnaPostaja> NaplatnaPostaja { get; set; }
    }
}
