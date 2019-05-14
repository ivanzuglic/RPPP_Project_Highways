using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int? SifraDionice { get; set; }
        public int? SifraLokacijePostaje { get; set; }
        [Display(Name = "Ime postaje")]
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string ImePostaje { get; set; }

        [Display(Name = "Dionica")]
        public Dionica SifraDioniceNavigation { get; set; }
        [Display(Name = "Lokacija postaje")]
        public LokacijaPostaje SifraLokacijePostajeNavigation { get; set; }
        [Display(Name = "Naplatna kućica")]
        public ICollection<NaplatnaKucica> NaplatnaKucica { get; set; }
        public ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
