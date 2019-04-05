using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    public class BenchmarkSerie
    {
        public string name { get; set; }
        public decimal[] data { get; set; } = new decimal[]{};
    }
}
