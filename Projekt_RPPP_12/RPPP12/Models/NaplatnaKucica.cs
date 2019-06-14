using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace RPPP12.Models
{
    public partial class NaplatnaKucica : IDbAsyncEnumerable<RPPP12.Models.NaplatnaKucica>
    {
        public NaplatnaKucica()
        {
            Cjenik = new HashSet<Cjenik>();
            Racun = new HashSet<Racun>();
        }

        public int SifraKucica { get; set; }
        public int? SifraPostaja { get; set; }
        public int? SifraBlagajnika { get; set; }
        public string VrstaNaplatneKucice { get; set; }

        [Display(Name = "Blagajnik")]
        public Zaposlenik SifraBlagajnikaNavigation { get; set; }
        [Display(Name = "Naplatna postaja")]
        public NaplatnaPostaja SifraPostajaNavigation { get; set; }
        [Display(Name = "Vrsta naplatne kućice")]
        public VrstaNaplatneKucice VrstaNaplatneKuciceNavigation { get; set; }
        public ICollection<Cjenik> Cjenik { get; set; }
        public ICollection<Racun> Racun { get; set; }

     

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }

        IDbAsyncEnumerator<NaplatnaKucica> IDbAsyncEnumerable<NaplatnaKucica>.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
