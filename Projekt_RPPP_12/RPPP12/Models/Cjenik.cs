using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Cjenik
    {
        public int SifraKucica { get; set; }
        public int SifraKategorijaVozila { get; set; }
        public double Cijena { get; set; }

        public KategorijaVozila SifraKategorijaVozilaNavigation { get; set; }
        public NaplatnaKucica SifraKucicaNavigation { get; set; }
    }
}
