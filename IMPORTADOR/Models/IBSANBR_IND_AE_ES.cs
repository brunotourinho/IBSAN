using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMPORTADOR.Models
{
    [Table("IBSANBR_IND_AE_ES")]
    class IBSANBR_IND_AE_ES
    {
        [Key]
        public int Id { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Referencia { get; set; }
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
