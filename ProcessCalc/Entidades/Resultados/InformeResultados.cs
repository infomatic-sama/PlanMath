using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessCalc.Entidades.Resultados;

namespace ProcessCalc.Entidades
{
    public class InformeResultados
    {
        public List<string> TextoLog { get; set; }
        public List<ResultadoEjecucion> Resultados { get; set; }
        public InformeResultados()
        {
            TextoLog = new List<string>();
            Resultados = new List<ResultadoEjecucion>();
        }
    }
}
