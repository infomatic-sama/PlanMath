using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class RegistroCantidadesObtenidas_UltimasEjecuciones
    {
        public List<InfoCantidadesElementosObtenidos_UltimaEjecucion> Registros { get; set; }

        public RegistroCantidadesObtenidas_UltimasEjecuciones()
        {
            Registros = new List<InfoCantidadesElementosObtenidos_UltimaEjecucion>();
        }
    }
}
