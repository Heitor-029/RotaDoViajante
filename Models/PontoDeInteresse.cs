namespace RotaDoViajante.Models
{
    public class PontoDeInteresse
    {
        public int IdPontoDeInteresse { get; set; }
        public string Localizacao { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int Latitude { get; set; }
        public int Altitude { get; set; }
    }
}
