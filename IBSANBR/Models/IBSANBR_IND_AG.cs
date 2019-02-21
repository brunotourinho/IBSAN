using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBSANBR.Models
{
    [Table("ibsanbr_ind_ag")]
    class IBSANBR_IND_AG
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
        public decimal IN001 { get; set; }
        public decimal IN009 { get; set; }
        public decimal IN010 { get; set; }
        public decimal IN011 { get; set; }
        public decimal IN013 { get; set; }
        public decimal IN014 { get; set; }
        public decimal IN017 { get; set; }
        public decimal IN020 { get; set; }
        public decimal IN022 { get; set; }
        public decimal IN023 { get; set; }
        public decimal IN025 { get; set; }
        public decimal IN028 { get; set; }
        public decimal IN043 { get; set; }
        public decimal IN044 { get; set; }
        public decimal IN049 { get; set; }
        public decimal IN050 { get; set; }
        public decimal IN051 { get; set; }
        public decimal IN052 { get; set; }
        public decimal IN053 { get; set; }
        public decimal IN055 { get; set; }
        public decimal IN057 { get; set; }
        public decimal IN058 { get; set; }


    }
}
