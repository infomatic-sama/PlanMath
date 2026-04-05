using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Archivos
{
    public class AsociacionResultadoOperando_ArchivoExterno
    {
        public DiseñoOperacion Operacion { get; set; }
        public DiseñoElementoOperacion SubOperacion { get; set; }
        public string IDResultado { get; set; }

        public AsociacionResultadoOperando_ArchivoExterno ReplicarObjeto()
        {
            AsociacionResultadoOperando_ArchivoExterno asociacion = new AsociacionResultadoOperando_ArchivoExterno();
            asociacion.Operacion = Operacion;
            asociacion.SubOperacion = SubOperacion;
            asociacion.IDResultado = IDResultado;

            return asociacion;
        }
    }
}
