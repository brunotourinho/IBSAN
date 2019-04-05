using Dapper.Contrib.Extensions;

namespace IMPORTADOR.Models
{
    [Table("ibsanbr_inf_ge")]
    class IBSANBR_INF_GE
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
        public decimal GE001 { get; set; }
        public decimal GE002 { get; set; }
        public decimal GE003 { get; set; }
        public decimal GE008 { get; set; }
        public decimal GE009 { get; set; }
        public decimal GE010 { get; set; }
        public decimal GE011 { get; set; }
        public decimal GE014 { get; set; }
        public decimal GE015 { get; set; }
        public decimal GE016 { get; set; }
        public decimal GE017 { get; set; }
        public decimal GE018 { get; set; }
        public string GE019 { get; set; }
        public string GE020 { get; set; }
        public decimal GE030 { get; set; }
        public decimal POP_TOT { get; set; }
        public decimal POP_URB { get; set; }
    }
}
