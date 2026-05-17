namespace RotaDoViajante.Models
{
    public class PDH
    {
        public int FkPonto { get; set; }
        public PontoDeInteresse? PontoDeInteresse { get; set; }

        public int FkDia { get; set; }
        public Dia? Dia { get; set; }

        public int FkHorario { get; set; }
        public Horario? Horario { get; set; }
    }
}