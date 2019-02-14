using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    public class PopulacaoCobertura
    {
        public string Competencia { get; set; }
        public string Prestador { get; set; }
        public decimal GE12A { get; set; }
        public decimal GE06A { get; set; }
        public decimal AG001 { get; set; }
        public decimal ES026 { get; set; }
    }
}

