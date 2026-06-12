using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class PDHController : Controller
    {
        private readonly DbConfig _dbConfig;

        public PDHController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro()
        {
            ViewBag.PontoDeInteresse = _dbConfig.pontodeinteresse.ToList();
            ViewBag.Dia = _dbConfig.dia.ToList();
            ViewBag.Horario = _dbConfig.horario.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarPDH(PDH pdh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(pdh);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            ViewBag.PontoDeInteresse = _dbConfig.pontodeinteresse.ToList();
            ViewBag.Dia = _dbConfig.dia.ToList();
            ViewBag.Horario = _dbConfig.horario.ToList();
            return View("Cadastro", pdh);
        }

        public async Task<IActionResult> Listar()
        {
            var registros = await _dbConfig.pdh
                .Include(p => p.PontoDeInteresse)
                .Include(p => p.Dia)
                .Include(p => p.Horario)
                .ToListAsync();
            return View(registros);
        }

        public async Task<IActionResult> Excluir(int fkPonto, int fkDia, int fkHorario)
        {
            var registro = await _dbConfig.pdh
                .FindAsync(fkPonto, fkDia, fkHorario);
            if (registro == null) return NotFound();

            try
            {
                _dbConfig.pdh.Remove(registro);
                await _dbConfig.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro ao excluir: " + ex.Message;
            }
            return RedirectToAction("Listar");
        }
    }
}