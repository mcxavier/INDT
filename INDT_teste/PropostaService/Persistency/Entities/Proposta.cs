using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropostaService.Persistency.Entities
{
    [Table("PROPOSTAS")]
    public class Proposta
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }
        [Column("DESCRICAO")]
        [StringLength(120)]
        public string descricao { get; set; }
        [Column("STATUS")]
        public int status { get; set; }
        [Column("DATA_CONTRATACAO")]
        public DateTime? dataContratacao { get; set; }
    }
}
