using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class PasseioPontoController : Controller
    {
        private readonly DbConfig _dbConfig;

        public PasseioPontoController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro()
        {
            ViewBag.Passeio = _dbConfig.passeio.ToList();
            ViewBag.PontoDeInteresse = _dbConfig.pontodeinteresse.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarPasseioPonto(PasseioPonto passeioPonto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(passeioPonto);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            ViewBag.Passeio = _dbConfig.passeio.ToList();
            ViewBag.PontoDeInteresse = _dbConfig.pontodeinteresse.ToList();
            return View("Cadastro", passeioPonto);
        }

        public async Task<IActionResult> Listar()
        {
            var registros = await _dbConfig.passeioponto
                .Include(pp => pp.Passeio)
                .Include(pp => pp.PontoDeInteresse)
                .ToListAsync();
            return View(registros);
        }

        public async Task<IActionResult> Excluir(int fkPasseio, int fkPonto)
        {
            var registro = await _dbConfig.passeioponto
                .FindAsync(fkPasseio, fkPonto);
            if (registro == null) return NotFound();

            try
            {
                _dbConfig.passeioponto.Remove(registro);
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