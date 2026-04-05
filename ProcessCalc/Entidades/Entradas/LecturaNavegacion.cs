using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class LecturaNavegacion
    {
        public bool MismaLecturaBusquedasArchivo { get; set; }
        public List<BusquedaTextoArchivo> BusquedasARealizar { get; set; }

        public LecturaNavegacion()
        {
            BusquedasARealizar = new List<BusquedaTextoArchivo>();
        }
    }
}
