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
        [Display(Name = "Datum i vrijeme")]
        public byte[] DatumVrijeme { get; set; }
        public string Link { get; set; }
        [Display(Name = "Šifra razine opasnosti")]
        public int SifraRazinaOpasnosti { get; set; }
        [Display(Name = "Šifra dionice")]
        public int SifraDionica { get; set; }
        public string Opis { get; set; }

        [Display(Name = "Dionica")]
        public Dionica SifraDionicaNavigation { get; set; }
        [Display(Name = "Razina opasnosti")]
        public RazinaOpasnosti SifraRazinaOpasnostiNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
        public ICollection<Stanje> Stanje { get; set; }
    }
}
