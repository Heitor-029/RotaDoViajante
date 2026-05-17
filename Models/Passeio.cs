namespace RotaDoViajante.Models
{
    public class Passeio
    {
        public int IdPasseio { get; set; }
        public string NomePasseio { get; set; } = string.Empty;
        public int FkGuia { get; set; }
        public Guia? GuiaNavigation { get; set; }

    }
}
