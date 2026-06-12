
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class PontoDeInteresseController : Controller
    {
        private readonly DbConfig _dbConfig;

        public PontoDeInteresseController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarPonto(PontoDeInteresse ponto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(ponto);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            return View("Cadastro", ponto);
        }

        public async Task<IActionResult> Listar()
        {
            var pontos = await _dbConfig.pontodeinteresse.ToListAsync();
            return View(pontos);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var ponto = await _dbConfig.pontodeinteresse.FindAsync(id);
            if (ponto == null) return NotFound();

            return View(ponto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPonto(int id, PontoDeInteresse ponto)
        {
            if (id != ponto.IdPontoDeInteresse) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(ponto);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            return View("Editar", ponto);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var ponto = await _dbConfig.pontodeinteresse.FindAsync(id);
            if (ponto == null) return NotFound();

            try
            {
                _dbConfig.pontodeinteresse.Remove(ponto);
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