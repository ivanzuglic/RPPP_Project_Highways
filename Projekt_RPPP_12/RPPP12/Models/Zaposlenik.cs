using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            NaplatnaKucica = new HashSet<NaplatnaKucica>();
        }

        public int SifraZaposlenika { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public int SifraPostaje { get; set; }
        public int SifraVrsteZaposlenika { get; set; }

        public NaplatnaPostaja SifraPostajeNavigation { get; set; }
        public VrstaZaposlenika SifraVrsteZaposlenikaNavigation { get; set; }
        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
    }
}
