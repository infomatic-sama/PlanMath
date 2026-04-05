using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class AsociacionTextoInformacion_ElementoSalida
    {
        public CondicionTextosInformacion CondicionesAsociadas { get; set; }
        public DiseñoElementoOperacion ElementoSalida { get; set; }
        public List<DiseñoElementoOperacion> ElementosSalidas { get; set; }
        public DiseñoOperacion ElementoSalida_Operacion { get; set; }
        public int CantidadConjuntosTextosInformacion { get; set; }
        public bool SiCondicionesCumplen { get; set; }
        public bool SiCondicionesNoCumplen { get; set; }
        public AsociacionTextoInformacion_ElementoSalida()
        {
            CantidadConjuntosTextosInformacion = 1;
            ElementosSalidas = new List<DiseñoElementoOperacion>();
            SiCondicionesCumplen = true;
        }

        public AsociacionTextoInformacion_ElementoSalida ReplicarObjeto()
        {
            AsociacionTextoInformacion_ElementoSalida asociacion = new AsociacionTextoInformacion_ElementoSalida();
            asociacion.CondicionesAsociadas = CondicionesAsociadas;
            asociacion.ElementoSalida = ElementoSalida;
            asociacion.ElementosSalidas = ElementosSalidas;
            asociacion.ElementoSalida_Operacion = ElementoSalida_Operacion;
            asociacion.CantidadConjuntosTextosInformacion = CantidadConjuntosTextosInformacion;
            asociacion.SiCondicionesCumplen = SiCondicionesCumplen;
            asociacion.SiCondicionesNoCumplen = SiCondicionesNoCumplen;

            return asociacion;
        }
    }
}
