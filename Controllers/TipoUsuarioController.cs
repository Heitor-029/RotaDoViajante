
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private readonly DbConfig _dbConfig;

        public TipoUsuarioController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarTipoUsuario(TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(tipoUsuario);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            return View("Cadastro", tipoUsuario);
        }

        public async Task<IActionResult> Listar()
        {
            var tipos = await _dbConfig.tipousuario.ToListAsync();
            return View(tipos);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var tipo = await _dbConfig.tipousuario.FindAsync(id);
            if (tipo == null) return NotFound();

            return View(tipo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarTipoUsuario(int id, TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.IdTiUsu) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(tipoUsuario);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            return View("Editar", tipoUsuario);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var tipo = await _dbConfig.tipousuario.FindAsync(id);
            if (tipo == null) return NotFound();

            try
            {
                _dbConfig.tipousuario.Remove(tipo);
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