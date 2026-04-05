using ProcessCalc.Entidades.Entradas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProcessCalc.Entidades
{
    public abstract class ElementoOrigenDatosEjecucion
    {
        public TipoOrigenDatos TipoOrigenDatos { get; set; }
        public TipoDefinicionSeparadores ConfiguracionSeparadores { get; set; }
        public abstract BusquedaArchivoEjecucion ObtenerBusquedaRelacionadaCondicion(BusquedaTextoArchivo busqueda);
        public int CantidadTextosInformacion_Obtenidos_UltimaEjecucion { get; set; }
        public int CantidadNumeros_Obtenidos_UltimaEjecucion { get; set; }
        public int PosicionInicialNumeros_Obtenidos_UltimaEjecucion { get; set; }
        public int PosicionFinalNumeros_Obtenidos_UltimaEjecucion { get; set; }
        public bool EstablecerLecturasNavegaciones_Busquedas { get; set; }
        public List<LecturaNavegacion> LecturasNavegaciones { get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public ElementoOrigenDatosEjecucion(int CantidadNumeros_Obtenidos_UltimaEjecucion,
            int CantidadTextosInformacion_Obtenidos_UltimaEjecucion,
            int PosicionInicialNumeros_Obtenidos_UltimaEjecucion,
            int PosicionFinalNumeros_Obtenidos_UltimaEjecucion)
        {
            this.CantidadNumeros_Obtenidos_UltimaEjecucion = CantidadNumeros_Obtenidos_UltimaEjecucion;
            this.CantidadTextosInformacion_Obtenidos_UltimaEjecucion = CantidadTextosInformacion_Obtenidos_UltimaEjecucion;
            this.PosicionInicialNumeros_Obtenidos_UltimaEjecucion = PosicionInicialNumeros_Obtenidos_UltimaEjecucion;
            this.PosicionFinalNumeros_Obtenidos_UltimaEjecucion = PosicionFinalNumeros_Obtenidos_UltimaEjecucion;
        }
    }
}
