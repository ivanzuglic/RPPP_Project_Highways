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

        public int SifraKategorijaVozila { get; set; }
        [Required(ErrorMessage = "Obavezno polje.")]
        [Range(0, 999, ErrorMessage = "Upišite broj u rasponu od 0 do 999.")]
        public int Oznaka { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public string Opis { get; set; }

        public ICollection<Cjenik> Cjenik { get; set; }
        public ICollection<Racun> Racun { get; set; }
    }
}
