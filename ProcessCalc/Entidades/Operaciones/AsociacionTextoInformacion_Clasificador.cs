using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Operaciones
{
    public class AsociacionTextoInformacion_Clasificador
    {
        public CondicionTextosInformacion CondicionesAsociadas { get; set; }
        public Clasificador ElementoClasificador { get; set; }
        public int CantidadConjuntosTextosInformacion { get; set; }
        public bool SiCondicionesCumplen { get; set; }
        public bool SiCondicionesNoCumplen { get; set; }
        public AsociacionTextoInformacion_Clasificador()
        {
            CantidadConjuntosTextosInformacion = 1;
            SiCondicionesCumplen = true;
        }

        public AsociacionTextoInformacion_Clasificador ReplicarObjeto()
        {
            AsociacionTextoInformacion_Clasificador clasificador = new AsociacionTextoInformacion_Clasificador();
            clasificador.CondicionesAsociadas = CondicionesAsociadas.ReplicarObjeto();
            clasificador.ElementoClasificador = ElementoClasificador;
            clasificador.CantidadConjuntosTextosInformacion = CantidadConjuntosTextosInformacion;
            clasificador.SiCondicionesCumplen = SiCondicionesCumplen;
            clasificador.SiCondicionesNoCumplen = SiCondicionesNoCumplen;

            return clasificador;
        }
    }
}
