using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Condiciones
{
    public class AsociacionCondicionTextosInformacion_Entradas_ElementoSalida
    {
        public CondicionTextosInformacion Condiciones { get; set; }
        public DiseñoOperacion ElementoSalida_Operacion { get; set; }
        public bool ModoManual { get; set; }
    }
}
