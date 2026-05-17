using Microsoft.EntityFrameworkCore;
using RotaDoViajante.Models;

namespace RotaDoViajante.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Guia> Guias { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Passeio> Passeios { get; set; }
        public DbSet<PasseioPonto> PasseioPontos { get; set; }
        public DbSet<PontoDeInteresse> PontosDeInteresse { get; set; }
        public DbSet<TipoUsuario> TiposUsuario { get; set; }
        public DbSet<UsuarioPasseio> UsuarioPasseios { get; set; }
        public DbSet<Dia> Dias { get; set; }
        public DbSet<PDH> PDHs { get; set; }
    }
}
