namespace comandas.api.Dtos
{
    public class MesaCreateRequest
    {
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }   // 0 - Livre, 1 - Ocupada, 2 - Reservada
    }
}
