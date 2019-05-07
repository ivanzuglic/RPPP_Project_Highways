using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class VrstaZaposlenika
    {
        public VrstaZaposlenika()
        {
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int SifraVrsteZaposlenika { get; set; }
        public string Naziv { get; set; }

        public ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
