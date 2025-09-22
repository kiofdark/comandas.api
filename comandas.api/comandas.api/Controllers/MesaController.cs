using comandas.api.Data;
using comandas.api.Domain;
using comandas.api.Dtos;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace comandas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        private readonly ComandasDbContext _context;
        public MesaController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/<MesaController>
        [HttpGet]
        public IEnumerable<MesaResponse> Get()
        {
            return _context.Mesas.Select(m => new MesaResponse { Id = m.Id, NumeroMesa = m.NumeroMesa, SituacaoMesa = m.SituacaoMesa}).ToList();
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public ActionResult<UsuarioResponse> Get(int id)
        {
          var mesa = _context.Mesas
                .Where(mesa => mesa.Id == id)
                .Select(mesa => new MesaResponse 
                { Id = mesa.Id, 
                  NumeroMesa = mesa.NumeroMesa, 
                  SituacaoMesa = mesa.SituacaoMesa 
                }).FirstOrDefault();

            if (mesa == null)
        {
                return NotFound("Mesa não encontrada");
            }

            return Ok(mesa);

        }

        // POST api/<MesaController>
        [HttpPost]
        public ActionResult<MesaResponse> Post([FromBody] MesaCreateRequest createRequest)
        {
            var newMesa = new Domain.Mesa
        {
                NumeroMesa = createRequest.NumeroMesa,
                SituacaoMesa = createRequest.SituacaoMesa
            };
            _context.Mesas.Add(newMesa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newMesa.Id, newMesa } );
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MesaUpdateRequest updateRequest)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if(mesa == null)
        {
                return NotFound("Mesa não encontrada");
            }
            mesa.NumeroMesa = updateRequest.NumeroMesa;
            mesa.SituacaoMesa = updateRequest.SituacaoMesa;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa == null)
        {
                return NotFound("Mesa não encontrada");
            }
            _context.Mesas.Remove(mesa);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
