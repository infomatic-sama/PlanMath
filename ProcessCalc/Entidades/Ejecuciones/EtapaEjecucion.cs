using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class EtapaEjecucion
    {
        public List<ElementoEjecucionCalculo> Elementos { get; set; }
        public EtapaEjecucion()
        {
            Elementos = new List<ElementoEjecucionCalculo>();
        }
    }
}
