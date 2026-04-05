using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class HistorialArchivos
    {
        public List<AperturaArchivo> Historial { get; set; }

        public HistorialArchivos()
        {
            Historial = new List<AperturaArchivo>();
        }
    }
}
