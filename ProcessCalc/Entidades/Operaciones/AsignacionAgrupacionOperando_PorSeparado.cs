using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class AsignacionAgrupacionOperando_PorSeparado
    {
        public AgrupacionOperando_PorSeparado Agrupacion {  get; set; }
        public DiseñoOperacion ElementoAsociado { get; set; }
        public DiseñoElementoOperacion SubElementoAsociado { get; set; }
        public DiseñoOperacion OperandoAsociado { get; set; }
        public DiseñoElementoOperacion SubOperandoAsociado { get; set; }
    }
}
