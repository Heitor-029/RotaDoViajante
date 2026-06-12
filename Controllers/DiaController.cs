
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class DiaController : Controller
    {
        private readonly DbConfig _dbConfig;

        public DiaController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarDia(Dia dia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(dia);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            return View("Cadastro", dia);
        }

        public async Task<IActionResult> Listar()
        {
            var dias = await _dbConfig.dia.ToListAsync();
            return View(dias);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var dia = await _dbConfig.dia.FindAsync(id);
            if (dia == null) return NotFound();

            return View(dia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarDia(int id, Dia dia)
        {
            if (id != dia.IdDia) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(dia);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            return View("Editar", dia);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var dia = await _dbConfig.dia.FindAsync(id);
            if (dia == null) return NotFound();

            try
            {
                _dbConfig.dia.Remove(dia);
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