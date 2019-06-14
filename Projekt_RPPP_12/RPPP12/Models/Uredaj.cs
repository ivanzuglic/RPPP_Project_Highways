using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace RPPP12.Models
{
    public partial class Uredaj : IDbAsyncEnumerable<RPPP12.Models.Uredaj>
    {
        public Uredaj()
        {
            Alarm = new HashSet<Alarm>();
        }

        public int SifraUredaja { get; set; }
        [StringLength(50, ErrorMessage = "Znakovno polje mora biti manje od 50 znakova.")]
        public string Status { get; set; }
        public int? SifraObjekta { get; set; }
        public int? SifraVrsteUredaja { get; set; }

        [Display(Name = "Objekt")]
        public Objekt SifraObjektaNavigation { get; set; }
        [Display(Name = "Vrsta uređaja")]
        public VrstaUredaja SifraVrsteUredajaNavigation { get; set; }
        public ICollection<Alarm> Alarm { get; set; }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }

        IDbAsyncEnumerator<Uredaj> IDbAsyncEnumerable<Uredaj>.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
