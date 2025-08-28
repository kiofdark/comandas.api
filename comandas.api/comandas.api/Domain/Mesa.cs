using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandas.api.Domain
{
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public int SituacaoMesa { get; set; }   // 0 - Livre, 1 - Ocupada, 2 - Reservada
    }
}
