using comandas.api.Domain;
using System.ComponentModel.DataAnnotations;

namespace comandas.api.Dtos
{
    public class ComandaResponse
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        [MaxLength(200)]
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
        public  List<ComandaItemResponse> Itens { get; set; } = new List<ComandaItemResponse>();
    }

    public class  ComandaItemResponse 
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }

    }
    public class ComandaCreateResponse
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        [MaxLength(200)]
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
        
    }

    public class ComandaFilterRequest
    {
        public string? NomeCliente { get; set; }
        public int? NumeroMesa { get; set; }
        public int SituacaoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
    }
}
