using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades.Entradas
{
    public class InfoCantidadesElementosObtenidos_UltimaEjecucion
    {
        public string IDArchivoCalculo { get; set; }
        public string IDEntrada { get; set; }
        public int CantidadTextosInformacion_Obtenidos_UltimaEjecucion { get; set; }
        public int CantidadNumeros_Obtenidos_UltimaEjecucion { get; set; }
        public int PosicionInicialNumeros_Obtenidos_UltimaEjecucion { get; set; }
        public int PosicionFinalNumeros_Obtenidos_UltimaEjecucion { get; set; }
    }
}
