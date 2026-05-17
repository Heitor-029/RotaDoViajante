using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GuiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guia>>> GetGuias()
        {
            return await _context.Guias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guia>> GetGuia(int id)
        {
            var guia = await _context.Guias.FindAsync(id);

            if (guia == null)
                return NotFound();

            return guia;
        }

        [HttpPost]
        public async Task<ActionResult<Guia>> PostGuia(Guia guia)
        {
            _context.Guias.Add(guia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGuia),
                new { id = guia.IdGuia },
                guia
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuia(int id, Guia guia)
        {
            if (id != guia.IdGuia)
                return BadRequest();

            _context.Entry(guia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Guias.Any(g => g.IdGuia == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuia(int id)
        {
            var guia = await _context.Guias.FindAsync(id);

            if (guia == null)
                return NotFound();

            _context.Guias.Remove(guia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
