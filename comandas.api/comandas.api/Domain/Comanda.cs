using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandas.api.Domain
{
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
       [MaxLength(200)]
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } // 0 - Aberta, 1 - Fechada, 2 - Cancelada
        public virtual List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();

    }
}
