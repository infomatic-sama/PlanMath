using ProcessCalc.Controles.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Ejecuciones.ToolTips
{
    public class AsociacionToolTipElemento
    {
        public string IDCalculo {  get; set; }
        public string IDElemento { get; set; }
        public string IDSubElemento { get; set; }
        public string IDCalculoElemento { get; set; }
    }
}
