using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace RPPP12.Models
{
    public partial class Dogadaj : IDbAsyncEnumerable<RPPP12.Models.Dogadaj>
    {
        public Dogadaj()
        {
            Alarm = new HashSet<Alarm>();
            Stanje = new HashSet<Stanje>();
        }

        public int SifraDogadaj { get; set; }
        [Display(Name = "Datum i vrijeme")]
        public byte[] DatumVrijeme { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string Link { get; set; }
        [Display(Name = "Šifra razine opasnosti")]
        public int SifraRazinaOpasnosti { get; set; }
        [Display(Name = "Šifra dionice")]
        public int SifraDionica { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string Opis { get; set; }

        [Display(Name = "Dionica")]
        public Dionica SifraDionicaNavigation { get; set; }
        [Display(Name = "Razina opasnosti")]
        public RazinaOpasnosti SifraRazinaOpasnostiNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }
        public ICollection<Stanje> Stanje { get; set; }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }

        IDbAsyncEnumerator<Dogadaj> IDbAsyncEnumerable<Dogadaj>.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
