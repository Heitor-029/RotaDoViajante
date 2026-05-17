using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoDeInteresseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PontoDeInteresseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PontoDeInteresse>>> GetPontos()
        {
            return await _context.PontosDeInteresse.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PontoDeInteresse>> GetPonto(int id)
        {
            var ponto = await _context.PontosDeInteresse.FindAsync(id);

            if (ponto == null)
                return NotFound();

            return ponto;
        }

        [HttpPost]
        public async Task<ActionResult<PontoDeInteresse>> PostPonto(PontoDeInteresse ponto)
        {
            _context.PontosDeInteresse.Add(ponto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPonto),
                new { id = ponto.IdPontoDeInteresse },
                ponto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPonto(int id, PontoDeInteresse ponto)
        {
            if (id != ponto.IdPontoDeInteresse)
                return BadRequest();

            _context.Entry(ponto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PontosDeInteresse.Any(p => p.IdPontoDeInteresse == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePonto(int id)
        {
            var ponto = await _context.PontosDeInteresse.FindAsync(id);

            if (ponto == null)
                return NotFound();

            _context.PontosDeInteresse.Remove(ponto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
