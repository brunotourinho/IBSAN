using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }
        public int CodigoRegiao { get; set; }
        public string CodigoPrestadorServico { get; set; }
        public string NomePrestador { get; set; }
        public string Sigla { get; set; }
        public string Abrangencia { get; set; }
        public string Natureza { get; set; }
        public string TipoServico { get; set; }
    }
}
