using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RPPP12.Models
{
    public partial class Zabrana
    {
        public Zabrana()
        {
            Stanje = new HashSet<Stanje>();
        }

        public int SifraZabrana { get; set; }
        [Display(Name = "Vrsta zabrane")]
        [Range(0, 99999, ErrorMessage = "Upišite broj u rasponu od 0 do 99999.")]
        [Required(ErrorMessage = "Obavezno polje.")]
        public int VrstaZabrane { get; set; }

        public ICollection<Stanje> Stanje { get; set; }
    }
}
