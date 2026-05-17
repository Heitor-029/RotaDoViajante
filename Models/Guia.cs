namespace RotaDoViajante.Models
{
    public class Guia
    {
        public int IdGuia { get; set; }
        public string NomeGuia { get; set; } = string.Empty;
        public int CPFGuia { get; set; }
        public string ExperienciaGuia { get; set; } = string.Empty;
        public string EmailGuia { get; set; } = string.Empty;
        public int TelefoneGuia { get; set; }

    }
}
