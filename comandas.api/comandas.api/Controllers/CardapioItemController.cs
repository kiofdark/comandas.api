using comandas.api.Data;
using comandas.api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comandas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioItemController : ControllerBase
    {
        private readonly ComandasDbContext _context;
        public CardapioItemController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/<CardapioItemController>
        [HttpGet]
        public ActionResult<IEnumerable<CardapioItemResponse>> Get()
        {
            var items = _context.CardapioItems
                .Select(ci => new CardapioItemResponse {
                    Id = ci.Id,
                    Titulo = ci.Titulo, 
                    Descricao = ci.Descricao, 
                    PossuiPreparo =ci.PossuiPreparo, 
                    Preco = ci.Preco 
                } )
                .ToList();

            return Ok(items);
        }

        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public ActionResult<CardapioItemResponse> Get(int id)
        {
            var item = _context.CardapioItems
                .Where(ci => ci.Id == id)
                .Select(ci => new CardapioItemResponse {
                    Id = ci.Id,
                    Titulo = ci.Titulo, 
                    Descricao = ci.Descricao, 
                    PossuiPreparo =ci.PossuiPreparo, 
                    Preco = ci.Preco 
                } )
                .FirstOrDefault();
            
            if (item == null)
            {
                return NotFound("Item do cardápio não encontrado");
            }

            return Ok(item);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public ActionResult<CardapioItemResponse> Post([FromBody] CardapioItemCreateRequest createRequest)
        {
            var newItem = new Domain.CardapioItem
            {
                Titulo = createRequest.Titulo,
                Descricao = createRequest.Descricao,
                Preco = createRequest.Preco,
                PossuiPreparo = createRequest.PossuiPreparo
            };
            _context.CardapioItems.Add(newItem);
            _context.SaveChanges();
            var response = new CardapioItemResponse
            {
                Id = newItem.Id,
                Titulo = newItem.Titulo,
                Descricao = newItem.Descricao,
                Preco = newItem.Preco,
                PossuiPreparo = newItem.PossuiPreparo
            };
            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, response);
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]   
        public ActionResult Put(int id, [FromBody] CardapioItemUpdateRequest updateRequest)
        {
            var cardapioItem = _context.CardapioItems.FirstOrDefault(ci => ci.Id == id);
            if (cardapioItem == null)
            {
                return NotFound("Item do cardápio não encontrado");
            }
            cardapioItem.Titulo = updateRequest.Titulo;
            cardapioItem.Descricao = updateRequest.Descricao;
            cardapioItem.Preco = updateRequest.Preco;
            cardapioItem.PossuiPreparo = updateRequest.PossuiPreparo;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var cardapioItem = _context.CardapioItems.FirstOrDefault(ci => ci.Id == id);
            if (cardapioItem == null)
            {
                return NotFound("Item do cardápio não encontrado");
            }
            _context.CardapioItems.Remove(cardapioItem);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
