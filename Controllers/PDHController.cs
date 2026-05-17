using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Data;
using RotaDoViajante.Models;

namespace RotaDoViajante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDHController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PDHController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PDH>>> GetPDHs()
        {
            return await _context.PDHs
                .Include(p => p.PontoDeInteresse)
                .Include(p => p.Dia)
                .Include(p => p.Horario)
                .ToListAsync();
        }

        [HttpGet("{fkPonto}/{fkDia}/{fkHorario}")]
        public async Task<ActionResult<PDH>> GetPDH(int fkPonto, int fkDia, int fkHorario)
        {
            var pdh = await _context.PDHs
                .Include(p => p.PontoDeInteresse)
                .Include(p => p.Dia)
                .Include(p => p.Horario)
                .FirstOrDefaultAsync(p =>
                    p.FkPonto == fkPonto &&
                    p.FkDia == fkDia &&
                    p.FkHorario == fkHorario);

            if (pdh == null)
                return NotFound();

            return pdh;
        }

        [HttpPost]
        public async Task<ActionResult<PDH>> PostPDH(PDH pdh)
        {
            _context.PDHs.Add(pdh);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPDH),
                new { fkPonto = pdh.FkPonto, fkDia = pdh.FkDia, fkHorario = pdh.FkHorario },
                pdh
            );
        }

        [HttpDelete("{fkPonto}/{fkDia}/{fkHorario}")]
        public async Task<IActionResult> DeletePDH(int fkPonto, int fkDia, int fkHorario)
        {
            var pdh = await _context.PDHs
                .FirstOrDefaultAsync(p =>
                    p.FkPonto == fkPonto &&
                    p.FkDia == fkDia &&
                    p.FkHorario == fkHorario);

            if (pdh == null)
                return NotFound();

            _context.PDHs.Remove(pdh);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
