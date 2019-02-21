using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMPORTADOR.Models
{
    [Table("ibsanbr_ind_fin")]
    class IBSANBR_IND_FIN
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
        public decimal IN002 { get; set; }
        public decimal IN003 { get; set; }
        public decimal IN004 { get; set; }
        public decimal IN005 { get; set; }
        public decimal IN006 { get; set; }
        public decimal IN007 { get; set; }
        public decimal IN008 { get; set; }
        public decimal IN012 { get; set; }
        public decimal IN018 { get; set; }
        public decimal IN019 { get; set; }
        public decimal IN026 { get; set; }
        public decimal IN027 { get; set; }
        public decimal IN029 { get; set; }
        public decimal IN030 { get; set; }
        public decimal IN031 { get; set; }
        public decimal IN032 { get; set; }
        public decimal IN033 { get; set; }
        public decimal IN034 { get; set; }
        public decimal IN035 { get; set; }
        public decimal IN036 { get; set; }
        public decimal IN037 { get; set; }
        public decimal IN038 { get; set; }
        public decimal IN039 { get; set; }
        public decimal IN040 { get; set; }
        public decimal IN041 { get; set; }
        public decimal IN042 { get; set; }
        public decimal IN045 { get; set; }
        public decimal IN048 { get; set; }
        public decimal IN054 { get; set; }
        public decimal IN060 { get; set; }
        public decimal IN101 { get; set; }



    }
}
