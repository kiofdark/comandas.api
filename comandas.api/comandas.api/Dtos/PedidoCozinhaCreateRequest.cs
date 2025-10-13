using comandas.api.Domain;

namespace comandas.api.Dtos
{
    public class PedidoCozinhaCreateRequest
    {
        public int ComandaId { get; set; }
        public virtual Comanda Comanda { get; set; }
        public int SituacaoId { get; set; } // 1 - Pendente, 2 - Preparando, 3 - Pronto, 4 - Entregue   
        public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItems { get; set; }
    }
}
