using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class AgrupacionOperando_PorSeparado
    {
        public DiseñoOperacion OperacionRelacionada { get; set; }
        public DiseñoElementoOperacion OperacionElementoRelacionado { get; set; }
        public string NombreAgrupacion { get; set; }
    }
}
