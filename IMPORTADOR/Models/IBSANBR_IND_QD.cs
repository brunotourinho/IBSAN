using Dapper.Contrib.Extensions;

namespace IMPORTADOR.Models
{
    [Table("ibsanbr_ind_qd")]
    class IBSANBR_IND_QD
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
        public decimal IN071 { get; set; }
        public decimal IN072 { get; set; }
        public decimal IN073 { get; set; }
        public decimal IN074 { get; set; }
        public decimal IN075 { get; set; }
        public decimal IN076 { get; set; }
        public decimal IN077 { get; set; }
        public decimal IN079 { get; set; }
        public decimal IN080 { get; set; }
        public decimal IN082 { get; set; }
        public decimal IN083 { get; set; }
        public decimal IN084 { get; set; }
        public decimal IN085 { get; set; }

    }
}
