using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    public class BenchmarkItem
    {
        public List<Elemento> Elementos { get; set; } = new List<Elemento>();
        public string[] Categorias { get; set; } = new string[] { };
        public List<BenchmarkSerie> Series { get; set; } = new List<BenchmarkSerie>();
    }
}
