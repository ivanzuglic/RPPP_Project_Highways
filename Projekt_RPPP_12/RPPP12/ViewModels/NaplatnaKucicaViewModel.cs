using RPPP12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPPP12.ViewModels
{
    public class NaplatnaKucicaViewModel
    {
        public IEnumerable<NaplatnaKucica> NaplatneKucice { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
