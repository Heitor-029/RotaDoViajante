namespace RotaDoViajante.Models
{
    public class UsuarioPasseio
    {
        public int FkPasseio { get; set; }
        public Passeio? Passeio { get; set; }

        public int FkUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
