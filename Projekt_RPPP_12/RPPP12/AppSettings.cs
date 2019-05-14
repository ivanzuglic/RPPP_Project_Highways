using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPPP12
{
    public class AppSettings
    {
        public int PageSize { get; set; } = 5;
        public int PageOffset { get; set; } = 5;
        public string ConnectionString { get; set; }
        public int AutoCompleteCount { get; set; } = 50;
    }
}
