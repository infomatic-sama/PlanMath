using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class OrdenarNumerosElemento
    {
        public DiseñoOperacion Operando { get; set; }
        public DiseñoElementoOperacion SubOperando { get; set; }
        public OrdenacionNumeros Ordenacion { get; set; }
        public List<OrdenacionNumeros> Ordenaciones { get; set; }
        public bool RevertirListaTextos { get; set; }
        public OrdenarNumerosElemento()
        {
            Ordenacion = new OrdenacionNumeros();
            Ordenaciones = new List<OrdenacionNumeros>();
        }
    }
}
