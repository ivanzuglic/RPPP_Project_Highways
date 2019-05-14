using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPPP12.Models
{
    public partial class KategorijaVozila
    {
        public KategorijaVozila()
        {
            Cjenik = new HashSet<Cjenik>();
            Racun = new HashSet<Racun>();
        }

        [Display(Name = "Šifra kategorije vozila")]
        public int SifraKategorijaVozila { get; set; }
        public int Oznaka { get; set; }
        public string Opis { get; set; }

        public ICollection<Cjenik> Cjenik { get; set; }
        public ICollection<Racun> Racun { get; set; }
    }
}
