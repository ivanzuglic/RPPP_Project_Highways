using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPPP12.Models
{
    public partial class Cjenik
    {
        public int SifraKucica { get; set; }
        public int SifraKategorijaVozila { get; set; }
        [Required(ErrorMessage = "Obavezno polje.")]
        [Range(1, 999, ErrorMessage = "Upišite broj u rasponu od 1 do 999.")]
        [DataType(DataType.Currency)]
        public double Cijena { get; set; }

        [Display(Name = "Kategorija vozila")]
        public KategorijaVozila SifraKategorijaVozilaNavigation { get; set; }
        [Display(Name = "Šifra kućice")]
        public NaplatnaKucica SifraKucicaNavigation { get; set; }
    }
}
