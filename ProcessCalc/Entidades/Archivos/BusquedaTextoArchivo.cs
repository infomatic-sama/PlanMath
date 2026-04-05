using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProcessCalc.Entidades
{
    public class BusquedaTextoArchivo
    {
        public List<InfoArrastreDatos> DatosFormatoBusquedaTexto { get; set; }
        public int IndiceCaracterNumero { get; set; }
        public int NumeroVecesBusquedaNumero { get; set; }
        [NonSerialized] public Rect Position;
        public string TextoBusquedaNumero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public OpcionFinBusquedaTexto_Archivos FinalizacionBusqueda { get; set; }
        public string Codificacion { get; set; }
        public OpcionTextosInformacionBusqueda OpcionTextosInformacion { get; set; }
        public bool UsarCantidad_SiNohayNumeros { get; set; }
        public double NumeroUtilizar_NoEncontrados { get; set; }
        public OpcionAsignarTextosInformacion_NumerosBusqueda OpcionAsignarTextosInformacion_Numeros { get; set; }
        public OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones OpcionAsignarTextosInformacion_Numeros_Iteraciones { get; set; }
        public List<string> TextosInformacion_AsignarNumeros { get; set; }
        public int CantidadNumeros_TextosInformacion_AsignarNumeros { get; set; }
        public int CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones { get; set; }
        public List<BusquedaTextoArchivo> Busquedas_AsignarNumeros { get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public bool EsConjuntoBusquedas { get; set; }
        public bool GenerarFilasTextosInformacion_PorCadaElemento { get; set; }
        public bool GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto { get; set; }
        public bool GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto { get; set; }
        public bool NoGenerarFilasTextosInformacion { get; set; }
        public List<BusquedaTextoArchivo> ConjuntoBusquedas { get; set; }
        public OpcionFinBusquedaTexto_Archivos FinalizacionConjuntoBusquedas { get; set; }
        public int NumeroVecesConjuntoBusquedas { get; set; }
        public CondicionConjuntoBusquedas Condiciones { get; set; }
        public CondicionConjuntoBusquedas Condiciones_RealizacionBusqueda { get; set; }
        public CondicionConjuntoBusquedas CondicionesTextoBusqueda { get; set; }
        public CondicionConjuntoBusquedas CondicionesTextoBusqueda_Filtros { get; set; }
        public string NombreCombo { get; set; }
        public bool OpcionTextoInformacion_Condicion_AsignacionImplicacion { get; set; }
        public bool OpcionTextoInformacion_Asignacion_AsignacionImplicacion { get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresCantidades { get; set; }
        public bool EstablecerNombresCantidadesIteracion_ConjuntoBusquedas { get; set; }
        public bool EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas { get; set; }
        public List<string> TextosInformacionFijos_ConjuntoBusquedas { get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresCantidades_ConjuntoBusquedas { get; set; }
        public bool IncluirTextosInformacion_BusquedaContenedora { get; set; }
        public bool ReemplazarTextosInformacion_NombresCantidades { get; set; }
        public bool EjecutarBusquedaCabeceraIteraciones {  get; set; }
        public BusquedaTextoArchivo()
        {
            DatosFormatoBusquedaTexto = new List<InfoArrastreDatos>();
            IndiceCaracterNumero = -1;
            TextoBusquedaNumero = string.Empty;
            NumeroVecesBusquedaNumero = 1;
            FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            Codificacion = "utf-8";
            OpcionTextosInformacion = OpcionTextosInformacionBusqueda.NumeroActual;
            OpcionAsignarTextosInformacion_Numeros = OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros;
            TextosInformacion_AsignarNumeros = new List<string>();
            CantidadNumeros_TextosInformacion_AsignarNumeros = 1;
            CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones = 1;
            Busquedas_AsignarNumeros = new List<BusquedaTextoArchivo>();
            TextosInformacionFijos = new List<string>();
            ConjuntoBusquedas = new List<BusquedaTextoArchivo>();
            FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            NumeroVecesConjuntoBusquedas = 1;
            OpcionTextoInformacion_Condicion_AsignacionImplicacion = true;
            OpcionTextoInformacion_Asignacion_AsignacionImplicacion = true;
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();
            EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas = true;
            TextosInformacionFijos_ConjuntoBusquedas = new List<string>();
            DefinicionOpcionesNombresCantidades_ConjuntoBusquedas = new DefinicionTextoNombresCantidades();
            NumeroUtilizar_NoEncontrados = 0;
            EjecutarBusquedaCabeceraIteraciones = true;
            OpcionAsignarTextosInformacion_Numeros_Iteraciones = OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones;
        }
        public BusquedaTextoArchivo(string nom, string descrip)
        {
            Nombre = nom;
            Descripcion = descrip;
            DatosFormatoBusquedaTexto = new List<InfoArrastreDatos>();
            IndiceCaracterNumero = -1;
            TextoBusquedaNumero = string.Empty;
            NumeroVecesBusquedaNumero = 1;
            FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            Codificacion = "utf-8";
            OpcionTextosInformacion = OpcionTextosInformacionBusqueda.NumeroActual;
            OpcionAsignarTextosInformacion_Numeros = OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros;
            TextosInformacion_AsignarNumeros = new List<string>();
            CantidadNumeros_TextosInformacion_AsignarNumeros = 1;
            CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones = 1;
            Busquedas_AsignarNumeros = new List<BusquedaTextoArchivo>();
            TextosInformacionFijos = new List<string>();
            ConjuntoBusquedas = new List<BusquedaTextoArchivo>();
            FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            NumeroVecesConjuntoBusquedas = 1;
            OpcionTextoInformacion_Condicion_AsignacionImplicacion = true;
            OpcionTextoInformacion_Asignacion_AsignacionImplicacion = true;
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();
            EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas = true;
            TextosInformacionFijos_ConjuntoBusquedas = new List<string>();
            DefinicionOpcionesNombresCantidades_ConjuntoBusquedas = new DefinicionTextoNombresCantidades();
            NumeroUtilizar_NoEncontrados = 0;
            EjecutarBusquedaCabeceraIteraciones = true;
            OpcionAsignarTextosInformacion_Numeros_Iteraciones = OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones;
        }
    }
}
