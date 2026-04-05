using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class EtapaCalculoEjecucion
    {
        public List<ElementoEjecucionCalculo> Elementos { get; set; }
        public EtapaCalculoEjecucion()
        {
            Elementos = new List<ElementoEjecucionCalculo>();
        }
    }
}
