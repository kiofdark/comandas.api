using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandas.api.Domain
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public int SituacaoId { get; set; } // 1 - Pendente, 2 - Preparando, 3 - Pronto, 4 - Entregue   
        public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItems { get; set; }
    }
}
