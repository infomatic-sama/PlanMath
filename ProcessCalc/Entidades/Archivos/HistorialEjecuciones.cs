using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class HistorialEjecuciones
    {
        public List<EjecucionArchivo> Historial { get; set; }

        public HistorialEjecuciones()
        {
            Historial = new List<EjecucionArchivo>();
        }
    }
}
