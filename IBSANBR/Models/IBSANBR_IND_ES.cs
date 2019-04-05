using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    [Table("ibsanbr_ind_es")]
    class IBSANBR_IND_ES
    {
        [Key]
        public int Id { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
        public string CodigoPrestador { get; set; }
        public string Prestador { get; set; }
        public string SiglaPrestador { get; set; }
        public string Abrangencia { get; set; }
        public string TipoServico { get; set; }
        public string NaturezaJuridica { get; set; }
        public decimal IN015 { get; set; }
        public decimal IN016 { get; set; }
        public decimal IN021 { get; set; }
        public decimal IN024 { get; set; }
        public decimal IN046 { get; set; }
        public decimal IN047 { get; set; }
        public decimal IN056 { get; set; }
        public decimal IN059 { get; set; }
    }
}
