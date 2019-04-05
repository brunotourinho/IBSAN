using Dapper.Contrib.Extensions;

namespace IBSANBR.Models
{
    [Table("ibsanbr_inf_ag")]
    class IBSANBR_INF_AG
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
        public decimal AG001 { get; set; }
        public decimal AG002 { get; set; }
        public decimal AG003 { get; set; }
        public decimal AG004 { get; set; }
        public decimal AG005 { get; set; }
        public decimal AG006 { get; set; }
        public decimal AG007 { get; set; }
        public decimal AG008 { get; set; }
        public decimal AG010 { get; set; }
        public decimal AG011 { get; set; }
        public decimal AG012 { get; set; }
        public decimal AG013 { get; set; }
        public decimal AG014 { get; set; }
        public decimal AG015 { get; set; }
        public decimal AG017 { get; set; }
        public decimal AG018 { get; set; }
        public decimal AG019 { get; set; }
        public decimal AG020 { get; set; }
        public decimal AG021 { get; set; }
        public decimal AG022 { get; set; }
        public decimal AG024 { get; set; }
        public decimal AG026 { get; set; }
        public decimal AG027 { get; set; }
        public decimal AG028 { get; set; }



    }
}
