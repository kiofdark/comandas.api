using comandas.api.Data;
using comandas.api.Domain;
using comandas.api.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace comandas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ComandasDbContext _context;
        public UsuariosController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<UsuarioResponse> Get()
        {
            return _context.Usuarios.Select(u => new UsuarioResponse { Id = u.Id, Nome = u.Nome, Email = u.Email}).ToList();
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public UsuarioResponse Get(int id)
        {
           var usuario =  _context.Usuarios.First(u => u.Id == id);
              return new UsuarioResponse { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email};
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
