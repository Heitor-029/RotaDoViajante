using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasseioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PasseioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passeio>>> GetPasseios()
        {
            return await _context.Passeios
                .Include(p => p.GuiaNavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Passeio>> GetPasseio(int id)
        {
            var passeio = await _context.Passeios
                .Include(p => p.GuiaNavigation)
                .FirstOrDefaultAsync(p => p.IdPasseio == id);

            if (passeio == null)
                return NotFound();

            return passeio;
        }

        [HttpPost]
        public async Task<ActionResult<Passeio>> PostPasseio(Passeio passeio)
        {
            _context.Passeios.Add(passeio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPasseio),
                new { id = passeio.IdPasseio },
                passeio
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasseio(int id, Passeio passeio)
        {
            if (id != passeio.IdPasseio)
                return BadRequest();

            _context.Entry(passeio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Passeios.Any(p => p.IdPasseio == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasseio(int id)
        {
            var passeio = await _context.Passeios.FindAsync(id);

            if (passeio == null)
                return NotFound();

            _context.Passeios.Remove(passeio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
