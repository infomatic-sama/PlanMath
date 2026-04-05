using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class CredencialesFTP
    {
        public bool UsuarioAnonimo { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
    }
}
