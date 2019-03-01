using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMPORTADOR.Models
{
    [Table("PRESTADORES")]
    class Prestadores
    {
        [Key]
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string CodigoMunicipio { get; set; }
        public string CodigoPrestador { get; set; }
        public string Prestador { get; set; }
        public string Sigla { get; set; }
        public string Abrangencia { get; set; }
        public string TipoServico { get; set; }
        public string Natureza { get; set; }
    }
}
