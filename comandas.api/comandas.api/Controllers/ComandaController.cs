using comandas.api.Data;
using comandas.api.Domain;
using comandas.api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace comandas.api.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    
    
    
    public class ComandaController : ControllerBase
    {
        private readonly ComandasDbContext _context;

        public ComandaController(ComandasDbContext context)
        {
            _context = context;
        }
        //criar filtro opcional da comanda nomedocliente, numerodamesa, situacaocomanda
        // GET: api/<ComandaController>
        [HttpGet]
        public IEnumerable<ComandaResponse> Get([FromQuery] ComandaFilterRequest filter)
        {
           var sql =  _context.Comandas
                .Where(c => c.SituacaoComanda == filter.SituacaoComanda );
            if (!string.IsNullOrEmpty(filter.NomeCliente))
            {
                sql = sql.Where(c => c.NomeCliente.Contains(filter.NomeCliente));
            }
            if (filter.NumeroMesa.HasValue)
            {
                sql = sql.Where(c => c.NumeroMesa == filter.NumeroMesa.Value);
            }
            var resposta = sql.Select(c => new ComandaResponse
            {
                Id = c.Id,
                NumeroMesa = c.NumeroMesa,
                NomeCliente = c.NomeCliente,
                SituacaoComanda = c.SituacaoComanda,
                Itens = c.Itens.Select(c => new ComandaItemResponse
                {
                    Id = c.Id,
                    NomeProduto = c.CardapioItem.Titulo + " - " + c.CardapioItem.Descricao
                }).ToList()
            }).ToList();
            return resposta;


        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public ActionResult<ComandaResponse> Get(int id)
        {
            var comanda = _context.Comandas
                .Include( c => c.Itens)
                .ThenInclude(ci => ci.CardapioItem)
                .FirstOrDefault(c => c.Id == id);
            if (comanda == null)
            {
                return NotFound("Comanda não encontrada");
            }
            return new ComandaResponse { 
                Id = comanda.Id,
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente, 
                SituacaoComanda = comanda.SituacaoComanda, 
                Itens = comanda.Itens.Select(c => new ComandaItemResponse
                {
                    Id = c.Id,
                    NomeProduto = c.CardapioItem.Titulo + " - " + c.CardapioItem.Descricao
                }).ToList()
            };

        }

        // POST api/<ComandaController>
        [HttpPost]
        public ActionResult<Comanda> Post([FromBody] ComandaCreateRequest comandaCreateRequest)
        {
            var comanda = new Comanda
            {
                NumeroMesa = comandaCreateRequest.NumeroMesa,
                NomeCliente = comandaCreateRequest.NomeCliente,
                SituacaoComanda = comandaCreateRequest.SituacaoComanda
                //verificar questão dos itens

            };

            _context.Comandas.Add(comanda);
            foreach (var itemId in comandaCreateRequest.CardapioItemIds)
            {
                var cardapioItem = _context.CardapioItems.FirstOrDefault(ci => ci.Id == itemId);
                if (cardapioItem != null)
                {
                    var comandaItem = new ComandaItem
                    {
                        CardapioItemId = cardapioItem.Id,

                    };
                    comanda.Itens.Add(comandaItem);
                    if (cardapioItem.PossuiPreparo)
                    {
                        var pedidoCozinha = new PedidoCozinha
                        {
                            Comanda = comanda,

                        };
                        _context.PedidoCozinhas.Add(pedidoCozinha);
                        var pedidoCozinhaItem = new PedidoCozinhaItem
                        {
                            ComandaItem = comandaItem,
                            PedidoCozinha = pedidoCozinha,
                        };
                        _context.PedidoCozinhaItems.Add(pedidoCozinhaItem);
                    }
                }
            }
            _context.SaveChanges();
            var response = new ComandaCreateResponse
            {
                Id = comanda.Id,
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente,
                SituacaoComanda = comanda.SituacaoComanda
            };

            return CreatedAtAction(nameof(Get), new { id = comanda.Id }, response);
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdateRequest)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda == null)
            {
                return NotFound("Comanda não encontrada");
            }
            comanda.NumeroMesa = comandaUpdateRequest.NumeroMesa;
            comanda.NomeCliente = comandaUpdateRequest.NomeCliente; 
            comanda.SituacaoComanda = comandaUpdateRequest.SituacaoComanda;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
            if (comanda == null)
            {
                return NotFound("Comanda não encontrada");
            }

            return NoContent();
        }
    }
}
// criar controller pedido cozinha