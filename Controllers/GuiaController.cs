
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class GuiaController : Controller
    {
        private readonly DbConfig _dbConfig;

        public GuiaController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarGuia(Guia guia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(guia);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            return View("Cadastro", guia);
        }

        public async Task<IActionResult> Listar()
        {
            var guias = await _dbConfig.guia.ToListAsync();
            return View(guias);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var guia = await _dbConfig.guia.FindAsync(id);
            if (guia == null) return NotFound();

            return View(guia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarGuia(int id, Guia guia)
        {
            if (id != guia.IdGuia) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(guia);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            return View("Editar", guia);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var guia = await _dbConfig.guia.FindAsync(id);
            if (guia == null) return NotFound();

            try
            {
                _dbConfig.guia.Remove(guia);
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