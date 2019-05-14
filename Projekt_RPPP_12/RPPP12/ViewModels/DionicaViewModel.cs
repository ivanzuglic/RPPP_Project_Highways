using RPPP12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPPP12.ViewModels
{
    public class DionicaViewModel
    {
        public IEnumerable<Dionica> Dionice { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
