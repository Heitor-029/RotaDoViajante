//jesus amado pq q essa porra nn conecta?
using RotaDoViajante.Config;
using RotaDoViajante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RotaDoViajante.Controllers
{
    public class UsuarioPasseioController : Controller
    {
        private readonly DbConfig _dbConfig;

        public UsuarioPasseioController(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public IActionResult Index() => View();

        public IActionResult Cadastro()
        {
            ViewBag.Passeio = _dbConfig.passeio.ToList();
            ViewBag.Usuario = _dbConfig.usuario.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarUsuarioPasseio(UsuarioPasseio usuarioPasseio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbConfig.Add(usuarioPasseio);
                    await _dbConfig.SaveChangesAsync();
                    return RedirectToAction("Listar");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Erro ao salvar: " + ex.Message);
                }
            }
            ViewBag.Passeio = _dbConfig.passeio.ToList();
            ViewBag.Usuario = _dbConfig.usuario.ToList();
            return View("Cadastro", usuarioPasseio);
        }

        public async Task<IActionResult> Listar()
        {
            var registros = await _dbConfig.usuariopasseio
                .Include(up => up.Passeio)
                .Include(up => up.Usuario)
                .ToListAsync();
            return View(registros);
        }

        public async Task<IActionResult> Excluir(int fkPasseio, int fkUsuario)
        {
            var registro = await _dbConfig.usuariopasseio
                .FindAsync(fkPasseio, fkUsuario);
            if (registro == null) return NotFound();

            try
            {
                _dbConfig.usuariopasseio.Remove(registro);
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