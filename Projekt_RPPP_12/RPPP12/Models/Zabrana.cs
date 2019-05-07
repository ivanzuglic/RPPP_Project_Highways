using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Zabrana
    {
        public Zabrana()
        {
            Stanje = new HashSet<Stanje>();
        }

        public int SifraZabrana { get; set; }
        public int VrstaZabrane { get; set; }

        public ICollection<Stanje> Stanje { get; set; }
    }
}
