namespace comandas.api.Dtos
{
    public class PedidoCozinhaResponse
    {
        public int Id { get; set; }
        public int MesaNumero { get; set; }
        public string NomeCliente { get; set; }

        public int SituacaoId { get; set; }
        public string item  { get; set; } 

    }

    public class PedidoCozinhaFilterRequest
    {
        public int SituacaoId { get; set; }
        public string? NomeCliente { get; set; }
        public int? MesaNumero { get; set; }
    }
}
