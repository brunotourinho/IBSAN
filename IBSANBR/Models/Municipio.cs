using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Competencia { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public decimal GE06A { get; set; }
        public decimal GE12A { get; set; }
    }
}

