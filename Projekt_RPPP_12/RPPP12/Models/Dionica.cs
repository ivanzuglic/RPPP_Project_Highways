using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class Dionica
    {
        public Dionica()
        {
            Dogadaj = new HashSet<Dogadaj>();
            NaplatnaPostaja = new HashSet<NaplatnaPostaja>();
            Objekt = new HashSet<Objekt>();
        }

        public int SifraDionice { get; set; }
        public string Naziv { get; set; }
        public int SifraPocetka { get; set; }
        public int SifraKraja { get; set; }
        public int SifraAutoceste { get; set; }
        public int Duljina { get; set; }

        [Display(Name = "Autocesta")]
        public Autocesta SifraAutocesteNavigation { get; set; }
        [Display(Name = "Kraj")]
        public LokacijaPostaje SifraKrajaNavigation { get; set; }
        [Display(Name = "Početak")]
        public LokacijaPostaje SifraPocetkaNavigation { get; set; }
        public ICollection<Dogadaj> Dogadaj { get; set; }
        public ICollection<NaplatnaPostaja> NaplatnaPostaja { get; set; }
        public ICollection<Objekt> Objekt { get; set; }
    }
}
