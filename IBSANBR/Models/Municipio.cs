using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    [Table("municipios")]
    public class Municipio
    {
        [Key]
        public int Id { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
    }
}

