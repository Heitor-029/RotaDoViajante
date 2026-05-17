using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TipoUsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUsuario>>> GetTiposUsuario()
        {
            return await _context.TiposUsuario.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> GetTipoUsuario(int id)
        {
            var tipoUsuario = await _context.TiposUsuario.FindAsync(id);

            if (tipoUsuario == null)
                return NotFound();

            return tipoUsuario;
        }

        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> PostTipoUsuario(TipoUsuario tipoUsuario)
        {
            _context.TiposUsuario.Add(tipoUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTipoUsuario),
                new { id = tipoUsuario.IdTiUsu },
                tipoUsuario
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsuario(int id, TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.IdTiUsu)
                return BadRequest();

            _context.Entry(tipoUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TiposUsuario.Any(t => t.IdTiUsu == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUsuario(int id)
        {
            var tipoUsuario = await _context.TiposUsuario.FindAsync(id);

            if (tipoUsuario == null)
                return NotFound();

            _context.TiposUsuario.Remove(tipoUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}