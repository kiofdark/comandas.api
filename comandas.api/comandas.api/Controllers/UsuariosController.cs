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
        public ActionResult<UsuarioResponse> Get(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return new UsuarioResponse { Id = usuario.Id, Nome = usuario.Nome, Email = usuario.Email };
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public ActionResult<Usuario> Post([FromBody] UsuarioCreateRequest usuarioCreateRequest)
        {
            var usuario = new Usuario
            {
                Nome = usuarioCreateRequest.Nome,
                Email = usuarioCreateRequest.Email,
                Senha = usuarioCreateRequest.Senha
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, usuario);
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdateRequest)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            usuario.Nome = usuarioUpdateRequest.Nome;
            usuario.Email = usuarioUpdateRequest.Email;
            usuario.Senha = usuarioUpdateRequest.Senha;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if(usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
