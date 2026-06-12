
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DbConfig _dbConfig;

        public UsuarioController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro()
        {
            ViewBag.TipoUsuario = _dbConfig.tipousuario.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(usuario);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            ViewBag.TipoUsuario = _dbConfig.tipousuario.ToList();
            return View("Cadastro", usuario);
        }

        public async Task<IActionResult> Listar()
        {
            var usuarios = await _dbConfig.usuario
                .Include(u => u.TipoUsuario)
                .ToListAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (id == null) return NotFound();

            var usuario = await _dbConfig.usuario.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewBag.TipoUsuario = await _dbConfig.tipousuario.ToListAsync();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Update(usuario);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao editar: " + ex.Message);
                }
            }
            ViewBag.TipoUsuario = await _dbConfig.tipousuario.ToListAsync();
            return View("Editar", usuario);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var usuario = await _dbConfig.usuario.FindAsync(id);
            if (usuario == null) return NotFound();

            try
            {
                _dbConfig.usuario.Remove(usuario);
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