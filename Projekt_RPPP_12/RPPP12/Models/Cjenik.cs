using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Cjenik
    {
        public int SifraKucica { get; set; }
        [Display(Name = "Šifra kategorije vozila")]
        public int SifraKategorijaVozila { get; set; }
        public double Cijena { get; set; }

        [Display(Name = "Kategorija vozila")]
        public KategorijaVozila SifraKategorijaVozilaNavigation { get; set; }
        [Display(Name = "Šifra kućice")]
        public NaplatnaKucica SifraKucicaNavigation { get; set; }
    }
}
