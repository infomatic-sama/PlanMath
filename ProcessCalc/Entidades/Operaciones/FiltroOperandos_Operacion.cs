using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class FiltroOperandos_Operacion
    {
        public List<CondicionesAsignacionSalidas_TextosInformacion> CondicionesTextosInformacion_SeleccionOrdenamiento { get; set; }

        public FiltroOperandos_Operacion()
        {
            CondicionesTextosInformacion_SeleccionOrdenamiento = new List<CondicionesAsignacionSalidas_TextosInformacion>();
        }
    }
}
