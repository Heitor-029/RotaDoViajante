using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasseioPontoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PasseioPontoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasseioPonto>>> GetPasseioPontos()
        {
            return await _context.PasseioPontos
                .Include(pp => pp.Passeio)
                .Include(pp => pp.PontoDeInteresse)
                .ToListAsync();
        }

        [HttpGet("{fkPasseio}/{fkPonto}")]
        public async Task<ActionResult<PasseioPonto>> GetPasseioPonto(int fkPasseio, int fkPonto)
        {
            var passeioPonto = await _context.PasseioPontos
                .Include(pp => pp.Passeio)
                .Include(pp => pp.PontoDeInteresse)
                .FirstOrDefaultAsync(pp => pp.FkPasseio == fkPasseio && pp.FkPonto == fkPonto);

            if (passeioPonto == null)
                return NotFound();

            return passeioPonto;
        }

        [HttpPost]
        public async Task<ActionResult<PasseioPonto>> PostPasseioPonto(PasseioPonto passeioPonto)
        {
            _context.PasseioPontos.Add(passeioPonto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPasseioPonto),
                new { fkPasseio = passeioPonto.FkPasseio, fkPonto = passeioPonto.FkPonto },
                passeioPonto
            );
        }

        [HttpDelete("{fkPasseio}/{fkPonto}")]
        public async Task<IActionResult> DeletePasseioPonto(int fkPasseio, int fkPonto)
        {
            var passeioPonto = await _context.PasseioPontos
                .FirstOrDefaultAsync(pp => pp.FkPasseio == fkPasseio && pp.FkPonto == fkPonto);

            if (passeioPonto == null)
                return NotFound();

            _context.PasseioPontos.Remove(passeioPonto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
