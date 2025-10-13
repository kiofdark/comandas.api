using comandas.api.Domain;
using System.ComponentModel.DataAnnotations;

namespace comandas.api.Dtos
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; }
        [MaxLength(200)]
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
        public virtual List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();
    }
}
