using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Condiciones
{
    public class AsociacionCondicionFlujo_ElementoSalida
    {
        public CondicionFlujo Condiciones { get; set; }
        public DiseñoElementoOperacion ElementoSalida { get; set; }
        public DiseñoOperacion ElementoSalida_Operacion { get; set; }
        public List<DiseñoElementoOperacion> ElementosSalidas { get; set; }
        public AsociacionCondicionFlujo_ElementoSalida()
        {
            ElementosSalidas = new List<DiseñoElementoOperacion>();
        }

        public AsociacionCondicionFlujo_ElementoSalida ReplicarObjeto()
        {
            AsociacionCondicionFlujo_ElementoSalida asociacion = new AsociacionCondicionFlujo_ElementoSalida();
            asociacion.Condiciones = Condiciones.ReplicarObjeto();
            asociacion.ElementoSalida = ElementoSalida;
            asociacion.ElementoSalida_Operacion = ElementoSalida_Operacion;
            asociacion.ElementosSalidas = ElementosSalidas.ToList();

            return asociacion;
        }
    }
}
