using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class EtapaOperacionEjecucion
    {
        public List<ElementoDiseñoOperacionAritmeticaEjecucion> Elementos { get; set; }
        public EtapaOperacionEjecucion()
        {
            Elementos = new List<ElementoDiseñoOperacionAritmeticaEjecucion>();
        }
    }
}
