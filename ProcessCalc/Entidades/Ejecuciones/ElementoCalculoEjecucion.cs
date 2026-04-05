using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public class ElementoCalculoEjecucion : ElementoEjecucionCalculo
    {
        public List<ElementoEjecucionCalculo> ElementosCalculo { get; set; }
        public List<ElementoEjecucionCalculo> ElementosDesdeCalculo { get; set; }
        public ElementoCalculoEjecucion()
        {
            ElementosCalculo = new List<ElementoEjecucionCalculo>();
            ElementosDesdeCalculo = new List<ElementoEjecucionCalculo>();
        }
    }
}
