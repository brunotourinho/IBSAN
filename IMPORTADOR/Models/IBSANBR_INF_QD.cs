using Dapper.Contrib.Extensions;

namespace IMPORTADOR.Models
{
    [Table("IBSANBR_INF_QD")]
    class IBSANBR_INF_QD
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
        public string QD001 { get; set; }
        public decimal QD002 { get; set; }
        public decimal QD003 { get; set; }
        public decimal QD004 { get; set; }
        public decimal QD006 { get; set; }
        public decimal QD007 { get; set; }
        public decimal QD008 { get; set; }
        public decimal QD009 { get; set; }
        public decimal QD011 { get; set; }
        public decimal QD012 { get; set; }
        public decimal QD015 { get; set; }
        public decimal QD019 { get; set; }
        public decimal QD020 { get; set; }
        public decimal QD021 { get; set; }
        public decimal QD022 { get; set; }
        public decimal QD023 { get; set; }
        public decimal QD024 { get; set; }
        public decimal QD025 { get; set; }
        public decimal QD026 { get; set; }
        public decimal QD027 { get; set; }
        public decimal QD028 { get; set; }

    }
}
