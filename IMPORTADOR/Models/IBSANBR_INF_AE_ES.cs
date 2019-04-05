using Dapper.Contrib.Extensions;

namespace IMPORTADOR.Models
{
    [Table("ibsanbr_inf_ae_es")]
    class IBSANBR_INF_AE_ES
    {
        [Key]
        public int Id { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Referencia { get; set; }
        public decimal ES001 { get; set; }
        public decimal ES002 { get; set; }
        public decimal ES003 { get; set; }
        public decimal ES004 { get; set; }
        public decimal ES005 { get; set; }
        public decimal ES006 { get; set; }
        public decimal ES007 { get; set; }
        public decimal ES008 { get; set; }
        public decimal ES009 { get; set; }
        public decimal ES012 { get; set; }
        public decimal ES013 { get; set; }
        public decimal ES014 { get; set; }
        public decimal ES015 { get; set; }
        public decimal ES026 { get; set; }
        public decimal ES028 { get; set; }
    }
}
