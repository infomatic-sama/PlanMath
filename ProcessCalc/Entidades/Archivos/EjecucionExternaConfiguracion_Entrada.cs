using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Archivos
{
    public class EjecucionExternaConfiguracion_Entrada
    {
        public bool EjecucionNormal { get; set; }
        public bool EjecucionTraspaso { get; set; }
        public ElementoEjecucionCalculo DatosEntrada { get; set; }
        public ElementoDiseñoOperacionAritmeticaEjecucion DatosSubEntrada { get; set; }
        public AsociacionEntradaOperando_ArchivoExterno InfoEntradaConfig {  get; set; }
        public string IDElementoAsociado { get; set; }
        public string IDSubElementoAsociado { get; set; }
    }
}
