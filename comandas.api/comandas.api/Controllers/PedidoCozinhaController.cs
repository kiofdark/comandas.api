using comandas.api.Data;
using comandas.api.Domain;
using comandas.api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace comandas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {
        private readonly ComandasDbContext _context;
        public PedidoCozinhaController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: PedidoCozinhaController
        [HttpGet]
        public IEnumerable<PedidoCozinhaResponse> Get([FromQuery] PedidoCozinhaFilterRequest filter)
        {
            var sql = _context.PedidoCozinhas
                .Where(p => p.SituacaoId == filter.SituacaoId);
            if (!string.IsNullOrEmpty(filter.NomeCliente))
            {
                sql = sql.Where(p => p.Comanda.NomeCliente.Contains(filter.NomeCliente));
            }
            if (filter.MesaNumero.HasValue)
            {
                sql = sql.Where(p => p.Comanda.NumeroMesa == filter.MesaNumero.Value);
            }
                var resposta =  sql.Select(p => new PedidoCozinhaResponse
            {
                Id = p.Id,
                MesaNumero = p.Comanda.NumeroMesa,
                NomeCliente = p.Comanda.NomeCliente,
                item = p.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Titulo+"-"+p.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Descricao,
                SituacaoId = p.SituacaoId

                }).ToList();
            return resposta;
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PedidoCozinhaUpdateRequest request)
        {
            var pedido = _context.PedidoCozinhas
                .Include(p => p.Comanda)
                .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            pedido.SituacaoId = request.SituacaoId;

            _context.PedidoCozinhas.Update(pedido);
            _context.SaveChanges();

            var response = new PedidoCozinhaResponse
            {
                Id = pedido.Id,
                MesaNumero = pedido.Comanda.NumeroMesa,
                NomeCliente = pedido.Comanda.NomeCliente,
                SituacaoId = pedido.SituacaoId
            };

            return Ok(response);
        }


    }
}
//finalizar controller / atualizar status do pedido / get by id 
//PUT DO PEDIDO COM NOVO STATUS
