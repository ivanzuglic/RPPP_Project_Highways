using System;
using System.Collections.Generic;

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
        public string Link { get; set; }
        public int SifraRazinaOpasnosti { get; set; }
        public int SifraDionica { get; set; }
        public string Opis { get; set; }

        public Dionica SifraDionicaNavigation { get; set; }
        public RazinaOpasnosti SifraRazinaOpasnostiNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
        public ICollection<Stanje> Stanje { get; set; }
    }
}
