using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.TextosInformacion
{
    public class OperacionCadenaTexto
    {
        public TipoOperacionCadenaTexto Tipo {  get; set; }
        public int PosicionInicial { get; set; }
        public int PosicionFinal { get; set; }
        public OperacionCadenaTexto()
        {
            Tipo = TipoOperacionCadenaTexto.Ninguna;
        }

        public OperacionCadenaTexto ReplicarObjeto()
        {
            OperacionCadenaTexto operacion = new OperacionCadenaTexto();
            operacion.Tipo = Tipo;
            operacion.PosicionInicial = PosicionInicial;
            operacion.PosicionFinal = PosicionFinal;

            return operacion;
        }
    }
}
