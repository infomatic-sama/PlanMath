using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class CondicionesAsignacionSalidas_TextosInformacion
    {
        public CondicionTextosInformacion Condiciones { get; set; }
        public List<DiseñoOperacion> Operandos_AplicarCondiciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos_AplicarCondiciones { get; set; }

        public CondicionesAsignacionSalidas_TextosInformacion()
        {
            Operandos_AplicarCondiciones = new List<DiseñoOperacion>();
            SubOperandos_AplicarCondiciones = new List<DiseñoElementoOperacion>();
        }

        public CondicionesAsignacionSalidas_TextosInformacion ReplicarObjeto()
        {
            CondicionesAsignacionSalidas_TextosInformacion condiciones = new CondicionesAsignacionSalidas_TextosInformacion();
            condiciones.Condiciones = Condiciones.ReplicarObjeto();
            condiciones.Operandos_AplicarCondiciones = Operandos_AplicarCondiciones.ToList();
            condiciones.SubOperandos_AplicarCondiciones = SubOperandos_AplicarCondiciones.ToList();

            return condiciones;
        }
    }
}
