using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class ConfiguracionOperacionesCadenasTexto
    {
        public List<OperacionCadenaTexto> Operaciones {  get; set; }
        public ConfiguracionOperacionesCadenasTexto()
        {
            Operaciones = new List<OperacionCadenaTexto>();
        }
    }
}
