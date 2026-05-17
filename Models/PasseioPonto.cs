namespace RotaDoViajante.Models
{
    public class PasseioPonto
    {
        public int FkPasseio { get; set; }
        public Passeio? Passeio { get; set; }

        public int FkPonto { get; set; }
        public PontoDeInteresse? PontoDeInteresse { get; set; }
    }
}
