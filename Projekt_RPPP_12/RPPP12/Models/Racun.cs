using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Racun
    {
        public int SifraRacun { get; set; }
        public int SifraKucica { get; set; }
        public string RegistarskaOznaka { get; set; }
        public string DatumVrijeme { get; set; }
        public int SifraKategorijaVozila { get; set; }
        public int SifraNacinPlacanja { get; set; }

        public KategorijaVozila SifraKategorijaVozilaNavigation { get; set; }
        public NaplatnaKucica SifraKucicaNavigation { get; set; }
        public NacinPlacanja SifraNacinPlacanjaNavigation { get; set; }
    }
}
