using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace RotaDoViajante.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string EmailUsuario { get; set; } = string.Empty;
        public string SenhaUsuario { get; set; } = string.Empty;
        public int TelefoneUsuario { get; set; }

        public int FkTiUsu { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }


                    

   }
}
