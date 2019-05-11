using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            NaplatnaKucica = new HashSet<NaplatnaKucica>();
        }

        public int SifraZaposlenika { get; set; }
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }
        public int SifraPostaje { get; set; }
        public int SifraVrsteZaposlenika { get; set; }

        [Display(Name = "Ime postaje")]
        public NaplatnaPostaja SifraPostajeNavigation { get; set; }
        [Display(Name = "Vrsta zaposlenika")]
        public VrstaZaposlenika SifraVrsteZaposlenikaNavigation { get; set; }
        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
    }
}
