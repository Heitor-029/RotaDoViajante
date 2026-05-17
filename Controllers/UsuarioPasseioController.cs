using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPasseioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioPasseioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioPasseio>>> GetUsuarioPasseios()
        {
            return await _context.UsuarioPasseios
                .Include(up => up.Usuario)
                .Include(up => up.Passeio)
                .ToListAsync();
        }

        [HttpGet("{fkPasseio}/{fkUsuario}")]
        public async Task<ActionResult<UsuarioPasseio>> GetUsuarioPasseio(int fkPasseio, int fkUsuario)
        {
            var usuarioPasseio = await _context.UsuarioPasseios
                .Include(up => up.Usuario)
                .Include(up => up.Passeio)
                .FirstOrDefaultAsync(up => up.FkPasseio == fkPasseio && up.FkUsuario == fkUsuario);

            if (usuarioPasseio == null)
                return NotFound();

            return usuarioPasseio;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioPasseio>> PostUsuarioPasseio(UsuarioPasseio usuarioPasseio)
        {
            _context.UsuarioPasseios.Add(usuarioPasseio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUsuarioPasseio),
                new { fkPasseio = usuarioPasseio.FkPasseio, fkUsuario = usuarioPasseio.FkUsuario },
                usuarioPasseio
            );
        }

        [HttpDelete("{fkPasseio}/{fkUsuario}")]
        public async Task<IActionResult> DeleteUsuarioPasseio(int fkPasseio, int fkUsuario)
        {
            var usuarioPasseio = await _context.UsuarioPasseios
                .FirstOrDefaultAsync(up => up.FkPasseio == fkPasseio && up.FkUsuario == fkUsuario);

            if (usuarioPasseio == null)
                return NotFound();

            _context.UsuarioPasseios.Remove(usuarioPasseio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
