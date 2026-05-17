using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dia>>> GetDias()
        {
            return await _context.Dias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dia>> GetDia(int id)
        {
            var dia = await _context.Dias.FindAsync(id);

            if (dia == null)
                return NotFound();

            return dia;
        }

        [HttpPost]
        public async Task<ActionResult<Dia>> PostDia(Dia dia)
        {
            _context.Dias.Add(dia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDia),
                new { id = dia.IdDia },
                dia
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDia(int id, Dia dia)
        {
            if (id != dia.IdDia)
                return BadRequest();

            _context.Entry(dia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dias.Any(d => d.IdDia == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDia(int id)
        {
            var dia = await _context.Dias.FindAsync(id);

            if (dia == null)
                return NotFound();

            _context.Dias.Remove(dia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
