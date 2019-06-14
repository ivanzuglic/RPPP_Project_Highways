using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace RPPP12.Models
{
    public partial class Dionica : IDbAsyncEnumerable<RPPP12.Models.Dionica>
    {
        public Dionica()
        {
            Dogadaj = new HashSet<Dogadaj>();
            NaplatnaPostaja = new HashSet<NaplatnaPostaja>();
            Objekt = new HashSet<Objekt>();
        }

        public int SifraDionice { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Naziv { get; set; }
        public int SifraPocetka { get; set; }
        public int SifraKraja { get; set; }
        public int SifraAutoceste { get; set; }
        [Range(0, 99999, ErrorMessage = "Upišite broj u rasponu od 0 do 99999.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public int Duljina { get; set; }

        [Display(Name = "Autocesta")]
        public Autocesta SifraAutocesteNavigation { get; set; }
        [Display(Name = "Kraj")]
        public LokacijaPostaje SifraKrajaNavigation { get; set; }
        [Display(Name = "Početak")]
        public LokacijaPostaje SifraPocetkaNavigation { get; set; }
        public ICollection<Dogadaj> Dogadaj { get; set; }
        [Display(Name = "Naplatna postaja")]
        public ICollection<NaplatnaPostaja> NaplatnaPostaja { get; set; }
        public ICollection<Objekt> Objekt { get; set; }

        public IDbAsyncEnumerator<Dionica> GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
