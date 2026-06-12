
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class PasseioController : Controller
    {
        private readonly DbConfig _dbConfig;

        public PasseioController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro()
        {
            ViewBag.Guia = _dbConfig.guia.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarPasseio(Passeio passeio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(passeio);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            ViewBag.Guia = _dbConfig.guia.ToList();
            return View("Cadastro", passeio);
        }

        public async Task<IActionResult> Listar()
        {
            var passeios = await _dbConfig.passeio
                .Include(p => p.Guia)
                .ToListAsync();
            return View(passeios);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var passeio = await _dbConfig.passeio.FindAsync(id);
            if (passeio == null) return NotFound();

            ViewBag.Guia = await _dbConfig.guia.ToListAsync();
            return View(passeio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPasseio(int id, Passeio passeio)
        {
            if (id != passeio.IdPasseio) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(passeio);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            ViewBag.Guia = await _dbConfig.guia.ToListAsync();
            return View("Editar", passeio);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var passeio = await _dbConfig.passeio.FindAsync(id);
            if (passeio == null) return NotFound();

            try
            {
                _dbConfig.passeio.Remove(passeio);
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