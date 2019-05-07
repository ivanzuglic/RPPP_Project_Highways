using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class NaplatnaPostaja
    {
        public NaplatnaPostaja()
        {
            NaplatnaKucica = new HashSet<NaplatnaKucica>();
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int SifraPostaje { get; set; }
        public int SifraDionice { get; set; }
        public int SifraLokacijePostaje { get; set; }
        public string ImePostaje { get; set; }

        public LokacijaPostaje SifraLokacijePostajeNavigation { get; set; }
        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
        public ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
