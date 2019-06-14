using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoceste.Utils
{
    /// <summary>
    /// Razred za pomoć formatiranja excel datoteka
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelFormatAttribute : Attribute
    {
        public string ExcelFormat { get; set; } = string.Empty;

        public ExcelFormatAttribute(string format)
        {
            ExcelFormat = format;
        }
    }
}