
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class HorarioController : Controller
    {
        private readonly DbConfig _dbConfig;

        public HorarioController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarHorario(Horario horario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(horario);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            return View("Cadastro", horario);
        }

        public async Task<IActionResult> Listar()
        {
            var horarios = await _dbConfig.horario.ToListAsync();
            return View(horarios);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var horario = await _dbConfig.horario.FindAsync(id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarHorario(int id, Horario horario)
        {
            if (id != horario.IdHorario) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(horario);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            return View("Editar", horario);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var horario = await _dbConfig.horario.FindAsync(id);
            if (horario == null) return NotFound();

            try
            {
                _dbConfig.horario.Remove(horario);
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