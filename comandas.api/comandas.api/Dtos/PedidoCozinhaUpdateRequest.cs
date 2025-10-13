using comandas.api.Domain;

namespace comandas.api.Dtos
{
    public class PedidoCozinhaUpdateRequest
    {
        
        public int SituacaoId { get; set; } // 1 - Pendente, 2 - Preparando, 3 - Pronto, 4 - Entregue   
        
    }
}
