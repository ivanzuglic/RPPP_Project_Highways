using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Dogadaj
    {
        public Dogadaj()
        {
            Alarm = new HashSet<Alarm>();
            Stanje = new HashSet<Stanje>();
        }

        public int SifraDogadaj { get; set; }
        public byte[] DatumVrijeme { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string Link { get; set; }
        public int SifraRazinaOpasnosti { get; set; }
        public int SifraDionica { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string Opis { get; set; }

        [Display(Name = "Dionica")]
        public Dionica SifraDionicaNavigation { get; set; }
        [Display(Name = "Razina opasnosti")]
        public RazinaOpasnosti SifraRazinaOpasnostiNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
        public ICollection<Stanje> Stanje { get; set; }
    }
}
