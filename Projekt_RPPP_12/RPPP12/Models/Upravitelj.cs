﻿using System;
using System.Collections.Generic;

namespace RPPP12.Models
{
    public partial class Upravitelj
    {
        public Upravitelj()
        {
            Autocesta = new HashSet<Autocesta>();
        }

        public int SifraUpravitelja { get; set; }
        public int Oib { get; set; }
        public string Ime { get; set; }
        public int SifraSjedista { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public Sjediste SifraSjedistaNavigation { get; set; }
        public ICollection<Autocesta> Autocesta { get; set; }
    }
}