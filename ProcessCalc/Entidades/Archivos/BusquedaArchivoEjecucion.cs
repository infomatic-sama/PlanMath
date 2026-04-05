using Microsoft.VisualBasic.Logging;
using PlanMath_para_Excel;
using ProcessCalc.Controles;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Entidades
{
    public class BusquedaArchivoEjecucion
    {
        private const string cadenaFormatoNumero = "/*n*/";
        private const string cadenaFormatoDatos = "/*d*/";
        private const string cadenaFormatoTextos = "/*t*/";
        public string TextoBusquedaNumero { get; set; }
        public string TextoBusquedaNumero_Proceso { get; set; }
        public int NumeroVecesBusquedaNumero { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TextoBusquedaNumero_EnEjecucion { get; set; }
        public string TextoBusquedaNumero_EnEjecucion_ConEspacios { get; set; }
        public int CantidadCamposDatosNumeros { get; set; }
        public List<List<int>> tamañoDatosTexto = new List<List<int>>();
        public double ValorNumeroEncontrado = 0;
        public bool NumeroEncontrado = false;
        public OpcionFinBusquedaTexto_Archivos FinalizacionBusqueda { get; set; }
        public string Codificacion { get; set; }
        public bool BusquedaIniciada { get; set; }
        public List<string> TextosInformacion;
        public List<string> TextosInformacion_AAsignar_NumerosSiguientes;
        public OpcionTextosInformacionBusqueda OpcionTextosInformacion { get; set; }
        public int IndiceProcesamientoTexto { get; set; }
        public bool UsarCantidad_SiNohayNumeros { get; set; }
        public double NumeroUtilizar_NoEncontrados { get; set; }
        public OpcionAsignarTextosInformacion_NumerosBusqueda OpcionAsignarTextosInformacion_Numeros { get; set; }
        public OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones OpcionAsignarTextosInformacion_Numeros_Iteraciones { get; set; }
        public List<string> TextosInformacion_AsignarNumeros { get; set; }
        public int CantidadNumeros_TextosInformacion_AsignarNumeros { get; set; }
        public int CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones { get; set; }
        public List<BusquedaTextoArchivo> Busquedas_AsignarNumeros { get; set; }
        public BusquedaTextoArchivo BusquedaRelacionada_Diseño { get; set; }
        public int cantidadActualNumeros_AsignarTextosInformacion { get; set; }
        public int IndiceActualNumeros_AsignarTextosInformacion_Iteraciones { get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public bool EsConjuntoBusquedas { get; set; }
        public OpcionFinBusquedaTexto_Archivos FinalizacionConjuntoBusquedas { get; set; }
        public List<BusquedaArchivoEjecucion> ConjuntoBusquedas { get; set; }
        public int NumeroVecesConjuntoBusquedas { get; set; }
        public CondicionConjuntoBusquedas Condiciones { get; set; }
        public CondicionConjuntoBusquedas CondicionesTextoBusqueda { get; set; }
        public CondicionConjuntoBusquedas CondicionesTextoBusqueda_Filtros { get; set; }
        public CondicionConjuntoBusquedas Condiciones_RealizacionBusqueda { get; set; }
        public int BusquedasConjuntoEjecutadas { get; set; }
        public List<double> NumerosEncontrados { get; set; }
        public int CantidadNumerosEncontrados { get; set; }
        public List<string> TextosInformacionEncontrados { get; set; }
        public int CantidadTextosInformacionEncontrados { get; set; }
        public List<string> TextosInformacionEncontrados_NombresCantidades { get; set; }
        public bool EjecutarBusquedaCabeceraIteraciones { get; set; }
        //public bool FinalArchivo { get; set; }
        public List<IteracionBusquedaEjecucion> Iteraciones { get; set; }
        public BusquedaArchivoEjecucion()
        {
            TextoBusquedaNumero_EnEjecucion = string.Empty;
            TextosInformacion = new List<string>();
            TextosInformacion_AAsignar_NumerosSiguientes = new List<string>();
            OpcionTextosInformacion = OpcionTextosInformacionBusqueda.NumeroActual;
            OpcionAsignarTextosInformacion_Numeros = OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros;
            TextosInformacion_AsignarNumeros = new List<string>();
            CantidadNumeros_TextosInformacion_AsignarNumeros = 1;
            Busquedas_AsignarNumeros = new List<BusquedaTextoArchivo>();
            cantidadActualNumeros_AsignarTextosInformacion = 0;
            TextosInformacionFijos = new List<string>();
            FinalizacionConjuntoBusquedas = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo;
            ConjuntoBusquedas = new List<BusquedaArchivoEjecucion>();
            CondicionesTextoBusqueda = new CondicionConjuntoBusquedas();
            NumerosEncontrados = new List<double>();
            TextosInformacionEncontrados = new List<string>();
            TextosInformacionEncontrados_NombresCantidades = new List<string>();
            NumeroUtilizar_NoEncontrados = 0;
            OpcionAsignarTextosInformacion_Numeros_Iteraciones = OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones;
            CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones = 1;
            Iteraciones = new List<IteracionBusquedaEjecucion>();
        }

        private bool busqValida;
        public bool BusquedaValida
        {
            get
            {
                return busqValida;
            }
            set
            {
                busqValida = value;
            }
        }

        public bool LecturaTerminada { get; set; }

        //public void ProcesarValidacion()
        //{
        //    bool valida = true;
        //    int cantidadCamposDatosNumerosProceso = 0;
        //    int indice = 0;

        //    do
        //    {
        //        if (indice + cadenaFormatoDatos.Length <= TextoBusquedaNumero.Length & 
        //            indice + cadenaFormatoNumero.Length <= TextoBusquedaNumero.Length)
        //        {
        //            if (TextoBusquedaNumero.Substring(indice, cadenaFormatoDatos.Length).Equals(cadenaFormatoDatos))
        //            {
        //                indice += cadenaFormatoDatos.Length;
        //                cantidadCamposDatosNumerosProceso++;
        //            }
        //            else if (TextoBusquedaNumero.Substring(indice, cadenaFormatoNumero.Length).Equals(cadenaFormatoNumero))
        //            {
        //                indice += cadenaFormatoNumero.Length;
        //                cantidadCamposDatosNumerosProceso++;
        //            }
        //        }

        //        indice++;
        //    } while (indice <= TextoBusquedaNumero.Length - 1);

        //    if (cantidadCamposDatosNumerosProceso == CantidadCamposDatosNumeros)
        //    {
        //        int cantidadLineasIguales = 0;
        //        int indiceSubString = 0;
        //        int indiceCaracter = 0;
        //        int cantidadCaracteres = 0;
        //        List<string> lineasTextoEjecucion = new List<string>();
        //        List<string> lineasTextoOriginal = new List<string>();

        //        while (true)
        //        {
        //            if (TextoBusquedaNumero_EnEjecucion.Length > 0 && (TextoBusquedaNumero_EnEjecucion[indiceCaracter] == '\n' |
        //                indiceCaracter == TextoBusquedaNumero_EnEjecucion.Length - 1))
        //            {
        //                if (indiceCaracter == TextoBusquedaNumero_EnEjecucion.Length - 1) cantidadCaracteres++;

        //                lineasTextoEjecucion.Add(TextoBusquedaNumero_EnEjecucion.Substring(indiceSubString, cantidadCaracteres).Trim());
        //                if (lineasTextoEjecucion.Last().Equals(string.Empty))
        //                    lineasTextoEjecucion.Remove(lineasTextoEjecucion.Last());
        //                indiceSubString = indiceCaracter + 1;
        //                cantidadCaracteres = 0;
        //            }
        //            else
        //                cantidadCaracteres++;

        //            indiceCaracter++;
        //            if (indiceCaracter >= TextoBusquedaNumero_EnEjecucion.Length)
        //                break;
        //        }

        //        indiceSubString = 0;
        //        indiceCaracter = 0;
        //        cantidadCaracteres = 0;
        //        while (true)
        //        {
        //            if (TextoBusquedaNumero.Length > 0 && TextoBusquedaNumero[indiceCaracter] == '\n' | 
        //                indiceCaracter == TextoBusquedaNumero.Length - 1)
        //            {
        //                if (indiceCaracter == TextoBusquedaNumero.Length - 1) cantidadCaracteres++;

        //                lineasTextoOriginal.Add(TextoBusquedaNumero.Substring(indiceSubString, cantidadCaracteres).Trim());
        //                if (lineasTextoOriginal.Last().Equals(string.Empty))
        //                    lineasTextoOriginal.Remove(lineasTextoOriginal.Last());
        //                indiceSubString = indiceCaracter + 1;
        //                cantidadCaracteres = 0;
        //            }
        //            else
        //                cantidadCaracteres++;

        //            indiceCaracter++;
        //            if (indiceCaracter >= TextoBusquedaNumero.Length)
        //                break;
        //        }

        //        if (lineasTextoEjecucion.Count == lineasTextoOriginal.Count)
        //        {
        //            for (int indiceLista = 0; indiceLista <= lineasTextoOriginal.Count - 1; indiceLista++)
        //            {
        //                string cadena = lineasTextoEjecucion[indiceLista];
        //                CompararProcesarStrings(ref cadena, lineasTextoOriginal[indiceLista], indiceLista);
        //                lineasTextoEjecucion[indiceLista] = cadena;
        //            }

        //            for(int indiceLista = 0; indiceLista <= lineasTextoEjecucion.Count - 1; indiceLista++)
        //            {
        //                if (string.Compare(lineasTextoEjecucion[indiceLista], lineasTextoOriginal[indiceLista]) == 0)
        //                    cantidadLineasIguales++;
        //            }

        //            if (cantidadLineasIguales != cantidadCamposDatosNumerosProceso &
        //            cantidadLineasIguales != CantidadCamposDatosNumeros)
        //                valida = false;
        //        }
        //        else
        //            valida = false;

        //        busqValida = valida;
        //    }
        //}

        public static BusquedaArchivoEjecucion PrepararBusquedas(BusquedaTextoArchivo itemBusqueda, EjecucionCalculo ejecucion)
        {
            BusquedaArchivoEjecucion busqueda = new BusquedaArchivoEjecucion();
            busqueda.TextoBusquedaNumero = itemBusqueda.TextoBusquedaNumero;
            busqueda.TextoBusquedaNumero = busqueda.TextoBusquedaNumero.Replace("\r", string.Empty);
            busqueda.NumeroVecesBusquedaNumero = itemBusqueda.NumeroVecesBusquedaNumero;
            busqueda.Nombre = itemBusqueda.Nombre;
            busqueda.Descripcion = itemBusqueda.Descripcion;
            busqueda.FinalizacionBusqueda = itemBusqueda.FinalizacionBusqueda;
            busqueda.Codificacion = itemBusqueda.Codificacion;
            busqueda.OpcionTextosInformacion = itemBusqueda.OpcionTextosInformacion;
            busqueda.UsarCantidad_SiNohayNumeros = itemBusqueda.UsarCantidad_SiNohayNumeros;
            busqueda.NumeroUtilizar_NoEncontrados = itemBusqueda.NumeroUtilizar_NoEncontrados;
            busqueda.OpcionAsignarTextosInformacion_Numeros = itemBusqueda.OpcionAsignarTextosInformacion_Numeros;
            busqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones = itemBusqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones;
            busqueda.TextosInformacion_AsignarNumeros.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda.TextosInformacion_AsignarNumeros));
            busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros = itemBusqueda.CantidadNumeros_TextosInformacion_AsignarNumeros;
            busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones = itemBusqueda.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones;
            busqueda.Busquedas_AsignarNumeros.AddRange(itemBusqueda.Busquedas_AsignarNumeros);
            busqueda.TextosInformacionFijos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda.TextosInformacionFijos));
            busqueda.BusquedaRelacionada_Diseño = itemBusqueda;
            busqueda.EsConjuntoBusquedas = itemBusqueda.EsConjuntoBusquedas;
            busqueda.FinalizacionConjuntoBusquedas = itemBusqueda.FinalizacionConjuntoBusquedas;
            busqueda.NumeroVecesConjuntoBusquedas = itemBusqueda.NumeroVecesConjuntoBusquedas;
            busqueda.Condiciones = itemBusqueda.Condiciones;
            busqueda.CondicionesTextoBusqueda = itemBusqueda.CondicionesTextoBusqueda;
            busqueda.CondicionesTextoBusqueda_Filtros = itemBusqueda.CondicionesTextoBusqueda_Filtros;
            busqueda.Condiciones_RealizacionBusqueda = itemBusqueda.Condiciones_RealizacionBusqueda;
            busqueda.EjecutarBusquedaCabeceraIteraciones = itemBusqueda.EjecutarBusquedaCabeceraIteraciones;

            if (busqueda.EsConjuntoBusquedas)
            {
                foreach (var itemBusquedaConjunto in itemBusqueda.ConjuntoBusquedas)
                {
                    BusquedaArchivoEjecucion busquedaConjunto = BusquedaArchivoEjecucion.PrepararBusquedas(itemBusquedaConjunto, ejecucion);
                    busqueda.ConjuntoBusquedas.Add(busquedaConjunto);
                }
            }

            return busqueda;
        }

        public BusquedaArchivoEjecucion ReplicarBusqueda()
        {
            BusquedaArchivoEjecucion busqueda = new BusquedaArchivoEjecucion();
            busqueda.CopiarObjeto(this);
            return busqueda;
        }

        private void CopiarObjeto(BusquedaArchivoEjecucion busquedaACopiar)
        {
            this.BusquedaIniciada = busquedaACopiar.BusquedaIniciada;
            this.BusquedaRelacionada_Diseño = busquedaACopiar.BusquedaRelacionada_Diseño;
            this.BusquedasConjuntoEjecutadas = busquedaACopiar.BusquedasConjuntoEjecutadas;
            this.Busquedas_AsignarNumeros.AddRange(busquedaACopiar.Busquedas_AsignarNumeros);
            this.BusquedaValida = busquedaACopiar.BusquedaValida;
            this.cantidadActualNumeros_AsignarTextosInformacion = busquedaACopiar.cantidadActualNumeros_AsignarTextosInformacion;
            this.CantidadCamposDatosNumeros = busquedaACopiar.CantidadCamposDatosNumeros;
            this.CantidadNumerosEncontrados = busquedaACopiar.CantidadNumerosEncontrados;
            this.CantidadNumeros_TextosInformacion_AsignarNumeros = busquedaACopiar.CantidadNumeros_TextosInformacion_AsignarNumeros;
            this.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones = busquedaACopiar.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones;
            this.CantidadTextosInformacionEncontrados = busquedaACopiar.CantidadTextosInformacionEncontrados;
            this.Condiciones = busquedaACopiar.Condiciones;
            this.CondicionesTextoBusqueda = busquedaACopiar.CondicionesTextoBusqueda;
            this.CondicionesTextoBusqueda_Filtros = busquedaACopiar.CondicionesTextoBusqueda_Filtros;
            this.Condiciones_RealizacionBusqueda = busquedaACopiar.Condiciones_RealizacionBusqueda;
            this.ConjuntoBusquedas = busquedaACopiar.ConjuntoBusquedas.Select(i => i.ReplicarBusqueda()).ToList();
            this.Descripcion = busquedaACopiar.Descripcion;
            this.EsConjuntoBusquedas = busquedaACopiar.EsConjuntoBusquedas;
            this.FinalizacionBusqueda = busquedaACopiar.FinalizacionBusqueda;
            this.FinalizacionConjuntoBusquedas = busquedaACopiar.FinalizacionConjuntoBusquedas;
            this.IndiceProcesamientoTexto = busquedaACopiar.IndiceProcesamientoTexto;
            this.LecturaTerminada = busquedaACopiar.LecturaTerminada;
            this.Nombre = busquedaACopiar.Nombre;
            this.Codificacion = busquedaACopiar.Codificacion;
            this.NumeroEncontrado = busquedaACopiar.NumeroEncontrado;
            this.NumeroUtilizar_NoEncontrados = busquedaACopiar.NumeroUtilizar_NoEncontrados;
            this.NumeroVecesBusquedaNumero = busquedaACopiar.NumeroVecesBusquedaNumero;
            this.NumeroVecesConjuntoBusquedas = busquedaACopiar.NumeroVecesConjuntoBusquedas;
            this.OpcionAsignarTextosInformacion_Numeros = busquedaACopiar.OpcionAsignarTextosInformacion_Numeros;
            this.OpcionAsignarTextosInformacion_Numeros_Iteraciones = busquedaACopiar.OpcionAsignarTextosInformacion_Numeros_Iteraciones;
            this.OpcionTextosInformacion = busquedaACopiar.OpcionTextosInformacion;
            this.TextosInformacionEncontrados = busquedaACopiar.TextosInformacionEncontrados.ToList();
            this.TextosInformacionEncontrados_NombresCantidades = busquedaACopiar.TextosInformacionEncontrados_NombresCantidades.ToList();
            this.TextoBusquedaNumero = busquedaACopiar.TextoBusquedaNumero;
            this.TextoBusquedaNumero_Proceso = busquedaACopiar.TextoBusquedaNumero_Proceso;
            this.TextosInformacionFijos.AddRange(busquedaACopiar.TextosInformacionFijos);
            this.TextosInformacion_AsignarNumeros.AddRange(busquedaACopiar.TextosInformacion_AsignarNumeros);
            this.UsarCantidad_SiNohayNumeros = busquedaACopiar.UsarCantidad_SiNohayNumeros; 
            this.EjecutarBusquedaCabeceraIteraciones = busquedaACopiar.EjecutarBusquedaCabeceraIteraciones;
        }

        public static void EjecutarBusquedas_ArchivoUrl(BusquedaArchivoEjecucion busqueda, bool mostrarLog, ElementoArchivoOrigenDatosEjecucion archivo,
            ElementoInternetOrigenDatosEjecucion url, ref string rutaTemporalArchivoFTP, ref string RutaArchivo, ref HttpResponseMessage respuestaArchivoFTP,
            List<FileStream> archivosAbiertos, List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos, List<string> log, List<string> logResultados, ElementoEntradaEjecucion entrada, ref bool ConError,
            EjecucionCalculo ejecucion, List<BusquedaArchivoEjecucion> busquedasAsignarNumeros, ElementoEntradaEjecucion numeros,
            ref int indiceBusqueda, BusquedaArchivoEjecucion busquedaContenedora, LecturaNavegacion lecturaNavegacion, Encoding codificacion, bool MismaLecturaArchivo,
            bool soloCabecera = false, bool archivoTemporal_EntradaManual = false)
        {
            bool valorCondicionesBusqueda = true;

            if (busqueda.Condiciones_RealizacionBusqueda != null)
            {
                if (archivo != null)
                    valorCondicionesBusqueda = busqueda.Condiciones_RealizacionBusqueda.EvaluarCondiciones(archivo);
                else if (url != null)
                    valorCondicionesBusqueda = busqueda.Condiciones_RealizacionBusqueda.EvaluarCondiciones(url);
            }

            if(valorCondicionesBusqueda)
            {
                if (lecturaNavegacion != null)
                {
                    valorCondicionesBusqueda = lecturaNavegacion.BusquedasARealizar.Contains(busqueda.BusquedaRelacionada_Diseño);
                }
            }

            if (valorCondicionesBusqueda)
            {
                string descripcionBusquedaActual = string.Empty;

                if (archivo != null)
                {
                    if (!MismaLecturaArchivo)
                    {
                        bool archivoTemporal = false;

                        if (archivoTemporal_EntradaManual)
                        {
                            rutaTemporalArchivoFTP = RutaArchivo;
                            archivo.TipoFormatoArchivo_Entrada = TipoFormatoArchivoEntrada.ArchivoTextoPlano;
                            archivoTemporal = true;
                        }
                        else
                        {

                            switch (archivo.TipoArchivo)
                            {
                                case TipoArchivo.EquipoLocal:
                                case TipoArchivo.RedLocal:
                                    rutaTemporalArchivoFTP = RutaArchivo;
                                    break;

                                case TipoArchivo.ServidorFTP:
                                case TipoArchivo.Internet:

                                    rutaTemporalArchivoFTP = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                                    archivoTemporal = true;
                                    ElementoArchivoOrigenDatosEjecucion.GuardarArchivoTemporalEntrada_FTP(rutaTemporalArchivoFTP, respuestaArchivoFTP);
                                    break;
                            }
                        }

                        rutaTemporalArchivoFTP = archivo.ObtenerArchivoTextoPlano_Temporal(rutaTemporalArchivoFTP, archivoTemporal, codificacion,
                            ref mostrarLog, log, ref ConError, ejecucion, entrada.ElementoDiseñoRelacionado.EntradaRelacionada, 
                            entrada);

                        string ruta = rutaTemporalArchivoFTP;
                        while (archivosAbiertos.Any(i => i.Name == ruta && i.CanRead))
                            Thread.Sleep(10);

                        while (true)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            try
                            {
                                archivo.lectorArchivo = new StreamReader(new FileStream(rutaTemporalArchivoFTP, FileMode.OpenOrCreate), false);
                                break;
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(10);
                                continue;
                            }
                        }

                        archivo.lector = (FileStream)archivo.lectorArchivo.BaseStream;
                        archivosAbiertos.Add(archivo.lector);
                        ArchivosOrigenesDatos.Add(archivo);
                    }

                    descripcionBusquedaActual = archivo.Busquedas.First().Descripcion;
                }
                else if (url != null)
                {
                    if (!url.MismaLecturaArchivo)
                    {
                        //archivo.lector = new FileStream(archivo.RutaArchivo, FileMode.OpenOrCreate);
                        url.ContenidoTexto = url.ObjetoURL.ObtenerTexto().Replace(" ", string.Empty);
                        url.IndiceProcesamientoTexto = 0;
                    }

                    descripcionBusquedaActual = url.Busquedas.First().Descripcion;
                }

                if (busqueda != null)
                {
                    if (mostrarLog && !string.IsNullOrEmpty(busqueda.Nombre))
                    {
                        log.Add("Realizando la búsqueda '" + busqueda.Nombre + "' en el archivo...");
                        logResultados.Add("Realizando la búsqueda '" + busqueda.Nombre + "' en el archivo...");
                    }

                    if (mostrarLog && !string.IsNullOrEmpty(busqueda.Descripcion))
                    {
                        log.Add("Información y comentarios de la búsqueda: " + descripcionBusquedaActual + ".");
                        logResultados.Add("Información y comentarios de la búsqueda: " + descripcionBusquedaActual + ".");
                    }

                    if (busqueda.EsConjuntoBusquedas &&
                        !soloCabecera)
                    {
                        busqueda.Iteraciones.Add(new IteracionBusquedaEjecucion());
                        busqueda.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones = busqueda.Iteraciones.Count;
                    }

                    bool variasVeces = true;
                    int totalVeces = 1;
                    if (entrada.TipoEntrada == TipoEntrada.ConjuntoNumeros)
                    {
                        variasVeces = false;
                        totalVeces = busqueda.NumeroVecesBusquedaNumero;
                    }

                    if (busqueda.FinalizacionBusqueda == OpcionFinBusquedaTexto_Archivos.EncontrarNveces)
                    {
                        for (int veces = 1; veces <= totalVeces; veces++)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            //busqueda.tamañoDatosTexto.Clear();
                            try
                            {
                                if (archivo != null)
                                    busqueda.ProcesarTextoBusqueda(archivo, variasVeces);
                                else if (url != null)
                                    busqueda.ProcesarTextoBusqueda(url, variasVeces);
                            }
                            catch (Exception e)
                            {
                                try { Thread.Sleep(3000); } catch (Exception) { };
                                ConError = true;

                                if (mostrarLog)
                                {
                                    log.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                    logResultados.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                }

                                ejecucion.Detener();
                                return;
                            }
                            busqueda.ProcesarValidacion();
                            busqueda.TextosInformacion.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacionFijos));
                            busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacionFijos));

                            EntidadNumero numeroAgregar = new EntidadNumero(string.Empty, 0);

                            bool procesarNumerosSiguientes_AsignacionTextos = false;
                            bool busquedaTextosSinNumeros_Valida = false;

                            if (busqueda.BusquedaValida || (!busqueda.BusquedaValida && busqueda.UsarCantidad_SiNohayNumeros))
                            {
                                if (busqueda.NumeroEncontrado || (!busqueda.NumeroEncontrado && busqueda.UsarCantidad_SiNohayNumeros))
                                {
                                    numeroAgregar.Numero = busqueda.BusquedaValida & busqueda.NumeroEncontrado ? 
                                        busqueda.ValorNumeroEncontrado : busqueda.NumeroUtilizar_NoEncontrados;
                                    numeroAgregar.BusquedaRelacionada = busqueda;

                                    numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(entrada.TextosInformacionFijos));

                                    if (archivo != null)
                                    {
                                        numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionFijos));

                                        var ruta = RutaArchivo;
                                        numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionIncluidos.Where(i => ruta.Contains(i)).ToList()));
                                    }

                                    if (busquedaContenedora != null)
                                    {
                                        if (busqueda.BusquedaRelacionada_Diseño.IncluirTextosInformacion_BusquedaContenedora)
                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(busquedaContenedora.TextosInformacionEncontrados));
                                    }

                                    numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));

                                    if (busquedasAsignarNumeros.Count > 0)
                                    {
                                        foreach (var itemBusqueda_AsignarTextosInformacion in busquedasAsignarNumeros)
                                        {
                                            bool asignar = false;

                                            switch (itemBusqueda_AsignarTextosInformacion.OpcionAsignarTextosInformacion_Numeros_Iteraciones)
                                            {
                                                case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.IteracionActual:
                                                    if (itemBusqueda_AsignarTextosInformacion == busquedaContenedora)
                                                        asignar = true;
                                                    break;

                                                case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones:
                                                    asignar = true;
                                                    break;

                                                case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.CantidadIteraciones:
                                                    if (busqueda.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones <= itemBusqueda_AsignarTextosInformacion.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones)
                                                        asignar = true;

                                                    break;
                                            }

                                            if (asignar)
                                            {
                                                switch (itemBusqueda_AsignarTextosInformacion.OpcionAsignarTextosInformacion_Numeros)
                                                {
                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros:

                                                        numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));

                                                        break;

                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.CantidadNumerosEspecifica:

                                                        if (itemBusqueda_AsignarTextosInformacion.cantidadActualNumeros_AsignarTextosInformacion < itemBusqueda_AsignarTextosInformacion.CantidadNumeros_TextosInformacion_AsignarNumeros)
                                                        {
                                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));

                                                            itemBusqueda_AsignarTextosInformacion.cantidadActualNumeros_AsignarTextosInformacion++;
                                                        }

                                                        break;

                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.TextosInformacionPrevios:

                                                        bool asignarTextos = false;

                                                        foreach (var itemTexto in itemBusqueda_AsignarTextosInformacion.TextosInformacion_AsignarNumeros)
                                                        {
                                                            if (ejecucion.VerificarTextoInformacion(itemTexto, numeroAgregar.Textos))
                                                            {
                                                                asignarTextos = true;
                                                                break;
                                                            }
                                                        }

                                                        if (asignarTextos)
                                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));

                                                        break;

                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.BusquedasNumeros:

                                                        if (itemBusqueda_AsignarTextosInformacion.BusquedaRelacionada_Diseño != busqueda.BusquedaRelacionada_Diseño)
                                                        {
                                                            if (itemBusqueda_AsignarTextosInformacion.Busquedas_AsignarNumeros.Contains(busqueda.BusquedaRelacionada_Diseño))
                                                            {
                                                                numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));
                                                            }
                                                        }

                                                        break;
                                                }
                                            }
                                        }
                                    }

                                    //string cadenaParteNumericaNombre = string.Empty;

                                    int cantidadTextos;

                                    if (busqueda.TextosInformacion.Count >= 1)
                                    {
                                        cantidadTextos = (from Texto in numeros.Numeros
                                                          where Texto.Textos.Contains(busqueda.TextosInformacion.First())
                                                          select Texto).Count() + 1;
                                    }
                                    else
                                    {
                                        cantidadTextos = (from Texto in numeros.Numeros
                                                          where (numeros.Numeros.Where((i) => i.Textos.Count == 0)).Any()
                                                          select Texto).Count() + 1;
                                    }



                                    //if (cantidadTextos > 1)
                                    //    cadenaParteNumericaNombre = ", posición " + cantidadTextos;
                                    if (busquedaContenedora != null)
                                    {
                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas)
                                        {
                                            if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                                ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, cantidadTextos, null, entrada);
                                        }

                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas)
                                        {
                                            if (busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.DefinirNombresDuranteEjecucion_Elemento)
                                                ejecucion.EstablecerNombreCantidad(numeroAgregar, busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas, cantidadTextos, null);
                                        }
                                    }
                                    else
                                    {
                                        if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                            ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, cantidadTextos, null, entrada);
                                    }

                                    //if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                    //    ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, cantidadTextos, null, entrada, null, false);

                                    numeroAgregar.PosicionElemento_DefinicionNombres = cantidadTextos;
                                    //numeroAgregar.Nombre = (busqueda.TextosInformacion.Count >= 1) ?
                                    //    numeroAgregar.Nombre + cadenaParteNumericaNombre + " en " + numeros.Nombre : 
                                    //    "Número" + cadenaParteNumericaNombre + " encontrado en " + numeros.Nombre;

                                    //if (busqueda.BusquedaRelacionada_Diseño.OpcionesNombresCantidades.IncluirTextosImplica)
                                    //{
                                    //    if (busqueda.TextosInformacionFijos.Any())
                                    //    {
                                    //        string cadenaTextosFijos = ". Este número implica: ";

                                    //        foreach (var itemTexto in busqueda.TextosInformacionFijos)
                                    //        {
                                    //            if (itemTexto != busqueda.TextosInformacionFijos.Last())
                                    //                cadenaTextosFijos += itemTexto + ", ";
                                    //            else
                                    //                cadenaTextosFijos += itemTexto + ".";

                                    //        }
                                    //        numeroAgregar.Nombre += cadenaTextosFijos;
                                    //    }

                                    //    if (entrada.TextosInformacionFijos.Any())
                                    //    {
                                    //        string cadenaTextosFijos = ". También implica: ";

                                    //        foreach (var itemTexto in entrada.TextosInformacionFijos)
                                    //        {
                                    //            if (itemTexto != entrada.TextosInformacionFijos.Last())
                                    //                cadenaTextosFijos += itemTexto + ", ";
                                    //            else
                                    //                cadenaTextosFijos += itemTexto + ".";

                                    //        }
                                    //        numeroAgregar.Nombre += cadenaTextosFijos;
                                    //    }
                                    //}

                                    numeros.Numeros.Add(numeroAgregar);

                                    if(busqueda.Iteraciones.Any())
                                        busqueda.Iteraciones.LastOrDefault().Numeros.Add(numeroAgregar);

                                    //busqueda.NumerosEncontrados.Add(numeroAgregar.Numero);
                                    //busqueda.CantidadNumerosEncontrados++;
                                    //busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(numeroAgregar.Textos));
                                    //busqueda.CantidadTextosInformacionEncontrados += numeroAgregar.Textos.Count;

                                    busqueda.ValorNumeroEncontrado = 0;
                                    busqueda.TextosInformacion.Clear();
                                    busqueda.BusquedaValida = false;
                                    busqueda.NumeroEncontrado = false;
                                }
                                else
                                {
                                    procesarNumerosSiguientes_AsignacionTextos = true;
                                }
                            }
                            else if(busqueda.TextosInformacionEncontrados.Any())
                            {
                                procesarNumerosSiguientes_AsignacionTextos = true;
                                busquedaTextosSinNumeros_Valida = true;
                            }

                            if (procesarNumerosSiguientes_AsignacionTextos)
                            {
                                switch (busqueda.OpcionTextosInformacion)
                                {
                                    case OpcionTextosInformacionBusqueda.UltimoNumeroEncontrado:
                                        List<EntidadNumero> NumerosSeleccionados = new List<EntidadNumero>();

                                        switch (busqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones)
                                        {
                                            case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones:
                                                NumerosSeleccionados = numeros.Numeros;
                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.CantidadIteraciones:
                                                NumerosSeleccionados = busquedaContenedora.Iteraciones.Where(i => busquedaContenedora.Iteraciones.IndexOf(i) + 1 >= busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones)
                                                    .SelectMany(i => i.Numeros).ToList();

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.IteracionActual:
                                                NumerosSeleccionados = busquedaContenedora.Iteraciones.LastOrDefault().Numeros;

                                                break;
                                        }

                                        switch (busqueda.OpcionAsignarTextosInformacion_Numeros)
                                        {
                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros:

                                                foreach (var itemNumero in NumerosSeleccionados)
                                                {
                                                    itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                    busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                }

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.CantidadNumerosEspecifica:

                                                numeros.Numeros.Reverse();
                                                int cantidadNumeros = 0;

                                                foreach (var itemNumero in NumerosSeleccionados)
                                                {
                                                    itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                    busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));

                                                    cantidadNumeros++;
                                                    if (cantidadNumeros == busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros)
                                                        break;
                                                }

                                                numeros.Numeros.Reverse();

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.TextosInformacionPrevios:

                                                foreach (var itemNumero in NumerosSeleccionados)
                                                {
                                                    bool asignar = false;

                                                    foreach (var itemTexto in busqueda.TextosInformacion_AsignarNumeros)
                                                    {
                                                        if (ejecucion.VerificarTextoInformacion(itemTexto, itemNumero.Textos))
                                                        {
                                                            asignar = true;
                                                            break;
                                                        }
                                                    }

                                                    if (asignar)
                                                    {
                                                        itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                        busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                    }
                                                }

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.BusquedasNumeros:
                                                List<BusquedaArchivoEjecucion> Busquedas = null;

                                                if (archivo != null)
                                                {
                                                    Busquedas = archivo.Busquedas;
                                                }
                                                else if (url != null)
                                                {
                                                    Busquedas = url.Busquedas;
                                                }

                                                foreach (var itemBusqueda in Busquedas)
                                                {
                                                    if (itemBusqueda.BusquedaRelacionada_Diseño != busqueda.BusquedaRelacionada_Diseño)
                                                    {
                                                        if (busqueda.Busquedas_AsignarNumeros.Contains(itemBusqueda.BusquedaRelacionada_Diseño))
                                                        {
                                                            foreach (var itemNumero in NumerosSeleccionados.Where((i) => i.BusquedaRelacionada.BusquedaRelacionada_Diseño == itemBusqueda.BusquedaRelacionada_Diseño))
                                                            {
                                                                itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                                busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                            }
                                                        }
                                                    }
                                                }

                                                break;
                                        }

                                        break;

                                    case OpcionTextosInformacionBusqueda.SiguienteNumeroAEncontrar:
                                        busquedasAsignarNumeros.Add(busqueda);
                                        break;
                                }

                                if (busquedaTextosSinNumeros_Valida)
                                {
                                    busqueda.TextosInformacion_AAsignar_NumerosSiguientes.Clear();
                                    busqueda.TextosInformacion_AAsignar_NumerosSiguientes.AddRange(busqueda.TextosInformacion);
                                    busqueda.TextosInformacion.Clear();
                                }
                            }
                            //        else
                            //        {
                            //            if (mostrarLog)
                            //                log.Add("No se encontró el " + numeroAgregar.Nombre + " de la entrada " + entrada.Nombre + ".");
                            //            archivo.lector.Close();

                            //            if (archivo.TipoArchivo == TipoArchivo.ServidorFTP &
                            //!string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                            //File.Exists(rutaTemporalArchivoFTP))
                            //                File.Delete(rutaTemporalArchivoFTP);

                            //            ConError = true;
                            //            Thread.CurrentThread.Abort();
                            //        }

                            if (entrada.TipoEntrada == TipoEntrada.ConjuntoNumeros)
                                busqueda.LimpiarVariables();

                            if (busquedaContenedora != null)
                            {
                                if (busqueda.LecturaTerminada)
                                {
                                    busquedaContenedora.LecturaTerminada = busqueda.LecturaTerminada;
                                }
                            }
                        }
                    }
                    else
                    {
                        bool continuar = true;
                        while (continuar)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            try
                            {
                                if (archivo != null)
                                    continuar = busqueda.ProcesarTextoBusqueda(archivo, variasVeces);
                                else if (url != null)
                                    continuar = busqueda.ProcesarTextoBusqueda(url, variasVeces);
                            }
                            catch (Exception e)
                            {
                                try { Thread.Sleep(3000); } catch (Exception) { };
                                ConError = true;

                                if (mostrarLog)
                                {
                                    log.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                    logResultados.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                }

                                ejecucion.Detener();
                                return;
                            }

                            busqueda.ProcesarValidacion();
                            busqueda.TextosInformacion.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacionFijos));
                            busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacionFijos));

                            EntidadNumero numeroAgregar = new EntidadNumero(string.Empty, 0);

                            bool procesarNumerosSiguientes_AsignacionTextos = false;
                            bool busquedaTextosSinNumeros_Valida = false;

                            if (busqueda.BusquedaValida || (continuar && !busqueda.BusquedaValida && busqueda.UsarCantidad_SiNohayNumeros))
                            {
                                if (busqueda.NumeroEncontrado || (!busqueda.NumeroEncontrado && busqueda.UsarCantidad_SiNohayNumeros))
                                {
                                    numeroAgregar.Numero = busqueda.BusquedaValida & busqueda.NumeroEncontrado ?
                                        busqueda.ValorNumeroEncontrado : busqueda.NumeroUtilizar_NoEncontrados;

                                    numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(entrada.TextosInformacionFijos));

                                    if (busquedaContenedora != null)
                                    {
                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.IncluirTextosInformacion_BusquedaContenedora)
                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(busquedaContenedora.TextosInformacionEncontrados));
                                    }

                                    numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));

                                    if (archivo != null)
                                    {
                                        numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionFijos));

                                        var ruta = RutaArchivo;
                                        numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionIncluidos.Where(i => ruta.Contains(i)).ToList()));
                                    }

                                    numeroAgregar.BusquedaRelacionada = busqueda;

                                    if (busquedasAsignarNumeros.Count > 0)
                                    {
                                        foreach (var itemBusqueda_AsignarTextosInformacion in busquedasAsignarNumeros)
                                        {
                                            bool asignar = false;

                                            switch (itemBusqueda_AsignarTextosInformacion.OpcionAsignarTextosInformacion_Numeros_Iteraciones)
                                            {
                                                case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.IteracionActual:
                                                    if (itemBusqueda_AsignarTextosInformacion == busquedaContenedora)
                                                        asignar = true;

                                                    break;

                                                case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones:
                                                    asignar = true;
                                                    break;

                                                case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.CantidadIteraciones:
                                                    if (busqueda.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones <= itemBusqueda_AsignarTextosInformacion.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones)
                                                        asignar = true;

                                                    break;
                                            }

                                            if (asignar)
                                            {
                                                switch (itemBusqueda_AsignarTextosInformacion.OpcionAsignarTextosInformacion_Numeros)
                                                {
                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros:

                                                        numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));

                                                        break;

                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.CantidadNumerosEspecifica:

                                                        if (itemBusqueda_AsignarTextosInformacion.cantidadActualNumeros_AsignarTextosInformacion < itemBusqueda_AsignarTextosInformacion.CantidadNumeros_TextosInformacion_AsignarNumeros)
                                                        {
                                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));

                                                            itemBusqueda_AsignarTextosInformacion.cantidadActualNumeros_AsignarTextosInformacion++;
                                                        }
                                                        break;

                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.TextosInformacionPrevios:

                                                        bool asignarTextos = false;

                                                        foreach (var itemTexto in itemBusqueda_AsignarTextosInformacion.TextosInformacion_AsignarNumeros)
                                                        {
                                                            if (ejecucion.VerificarTextoInformacion(itemTexto, numeroAgregar.Textos))
                                                            {
                                                                asignarTextos = true;
                                                                break;
                                                            }
                                                        }

                                                        if (asignarTextos)
                                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));

                                                        break;

                                                    case OpcionAsignarTextosInformacion_NumerosBusqueda.BusquedasNumeros:

                                                        if (itemBusqueda_AsignarTextosInformacion.BusquedaRelacionada_Diseño != busqueda.BusquedaRelacionada_Diseño)
                                                        {
                                                            if (itemBusqueda_AsignarTextosInformacion.Busquedas_AsignarNumeros.Contains(busqueda.BusquedaRelacionada_Diseño))
                                                            {
                                                                numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(itemBusqueda_AsignarTextosInformacion.TextosInformacion_AAsignar_NumerosSiguientes));
                                                            }
                                                        }

                                                        break;
                                                }
                                            }
                                        }
                                    }

                                    //string cadenaParteNumericaNombre = string.Empty;

                                    int cantidadTextos;

                                    if (busqueda.TextosInformacion.Count >= 1)
                                    {
                                        cantidadTextos = (from Texto in numeros.Numeros
                                                          where Texto.Textos.Contains(busqueda.TextosInformacion.First())
                                                          select Texto).Count() + 1;
                                    }
                                    else
                                    {
                                        cantidadTextos = (from Texto in numeros.Numeros
                                                          where (numeros.Numeros.Where((i) => i.Textos.Count == 0)).Any()
                                                          select Texto).Count() + 1;
                                    }



                                    //if (cantidadTextos > 1)
                                    //    cadenaParteNumericaNombre = ", posición " + cantidadTextos;

                                    if (busquedaContenedora != null)
                                    {
                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas)
                                        {
                                            if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                                ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, cantidadTextos, null, entrada);
                                        }

                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas)
                                        {
                                            if (busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.DefinirNombresDuranteEjecucion_Elemento)
                                                ejecucion.EstablecerNombreCantidad(numeroAgregar, busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas, cantidadTextos, null);
                                        }
                                    }
                                    else
                                    {
                                        if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                            ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, cantidadTextos, null, entrada);
                                    }

                                    //if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                    //    ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, cantidadTextos, null, entrada, null, false);

                                    numeroAgregar.PosicionElemento_DefinicionNombres = cantidadTextos;
                                    //numeroAgregar.Nombre = (busqueda.TextosInformacion.Count >= 1) ?
                                    //    numeroAgregar.Nombre + cadenaParteNumericaNombre + " en " + numeros.Nombre :
                                    //    "Número" + cadenaParteNumericaNombre + " encontrado en " + numeros.Nombre;

                                    //if (busqueda.BusquedaRelacionada_Diseño.OpcionesNombresCantidades.IncluirTextosImplica)
                                    //{
                                    //    if (busqueda.TextosInformacionFijos.Any())
                                    //    {
                                    //        string cadenaTextosFijos = ". Este número implica: ";

                                    //        foreach (var itemTexto in busqueda.TextosInformacionFijos)
                                    //        {
                                    //            if (itemTexto != busqueda.TextosInformacionFijos.Last())
                                    //                cadenaTextosFijos += itemTexto + ", ";
                                    //            else
                                    //                cadenaTextosFijos += itemTexto + ".";

                                    //        }
                                    //        numeroAgregar.Nombre += cadenaTextosFijos;
                                    //    }

                                    //    if (entrada.TextosInformacionFijos.Any())
                                    //    {
                                    //        string cadenaTextosFijos = ". También implica: ";

                                    //        foreach (var itemTexto in entrada.TextosInformacionFijos)
                                    //        {
                                    //            if (itemTexto != entrada.TextosInformacionFijos.Last())
                                    //                cadenaTextosFijos += itemTexto + ", ";
                                    //            else
                                    //                cadenaTextosFijos += itemTexto + ".";

                                    //        }
                                    //        numeroAgregar.Nombre += cadenaTextosFijos;
                                    //    }
                                    //}

                                    numeros.Numeros.Add(numeroAgregar);

                                    if (busqueda.Iteraciones.Any())
                                        busqueda.Iteraciones.LastOrDefault().Numeros.Add(numeroAgregar);

                                    //busqueda.NumerosEncontrados.Add(numeroAgregar.Numero);
                                    //busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(numeroAgregar.Textos));

                                    //if (busquedaContenedora != null)
                                    //{
                                    //busqueda.CantidadNumerosEncontrados++;
                                    //busqueda.CantidadTextosInformacionEncontrados += numeroAgregar.Textos.Count;
                                    //}

                                    if (!busqueda.BusquedaIniciada) busqueda.BusquedaIniciada = true;

                                    busqueda.ValorNumeroEncontrado = 0;
                                    busqueda.TextosInformacion.Clear();
                                    busqueda.BusquedaValida = false;
                                    busqueda.NumeroEncontrado = false;
                                }
                                else
                                {
                                    procesarNumerosSiguientes_AsignacionTextos = true;
                                }
                            }
                            else if(busqueda.TextosInformacionEncontrados.Any())
                            {
                                procesarNumerosSiguientes_AsignacionTextos = true;
                                busquedaTextosSinNumeros_Valida = true;
                            }

                            if (procesarNumerosSiguientes_AsignacionTextos)
                            {
                                switch (busqueda.OpcionTextosInformacion)
                                {
                                    case OpcionTextosInformacionBusqueda.UltimoNumeroEncontrado:

                                        List<EntidadNumero> NumerosSeleccionados = new List<EntidadNumero>();

                                        switch (busqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones)
                                        {
                                            case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.TodasIteraciones:
                                                NumerosSeleccionados = numeros.Numeros;
                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.CantidadIteraciones:
                                                NumerosSeleccionados = busquedaContenedora.Iteraciones.Where(i => busquedaContenedora.Iteraciones.IndexOf(i) + 1 >= busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros_Iteraciones)
                                                    .SelectMany(i => i.Numeros).ToList();

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda_Iteraciones.IteracionActual:
                                                NumerosSeleccionados = busquedaContenedora.Iteraciones.LastOrDefault().Numeros;

                                                break;
                                        }

                                        switch (busqueda.OpcionAsignarTextosInformacion_Numeros)
                                        {
                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.TodosNumeros:

                                                foreach (var itemNumero in NumerosSeleccionados)
                                                {
                                                    itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                    busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                }

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.CantidadNumerosEspecifica:

                                                numeros.Numeros.Reverse();
                                                int cantidadNumeros = 0;

                                                foreach (var itemNumero in NumerosSeleccionados)
                                                {
                                                    itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                    busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));

                                                    cantidadNumeros++;
                                                    if (cantidadNumeros == busqueda.CantidadNumeros_TextosInformacion_AsignarNumeros)
                                                        break;
                                                }

                                                numeros.Numeros.Reverse();

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.TextosInformacionPrevios:

                                                numeros.Numeros.Reverse();

                                                foreach (var itemNumero in NumerosSeleccionados)
                                                {
                                                    bool asignar = false;

                                                    foreach (var itemTexto in busqueda.TextosInformacion_AsignarNumeros)
                                                    {
                                                        if (ejecucion.VerificarTextoInformacion(itemTexto, itemNumero.Textos))
                                                        {
                                                            asignar = true;
                                                            break;
                                                        }
                                                    }

                                                    if (asignar)
                                                    {
                                                        itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                        busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                    }
                                                }

                                                break;

                                            case OpcionAsignarTextosInformacion_NumerosBusqueda.BusquedasNumeros:
                                                List<BusquedaArchivoEjecucion> Busquedas = null;

                                                if (archivo != null)
                                                {
                                                    Busquedas = archivo.Busquedas;
                                                }
                                                else if (url != null)
                                                {
                                                    Busquedas = url.Busquedas;
                                                }

                                                foreach (var itemBusqueda in Busquedas)
                                                {
                                                    if (itemBusqueda.BusquedaRelacionada_Diseño != busqueda.BusquedaRelacionada_Diseño)
                                                    {
                                                        if (busqueda.Busquedas_AsignarNumeros.Contains(itemBusqueda.BusquedaRelacionada_Diseño))
                                                        {
                                                            foreach (var itemNumero in NumerosSeleccionados.Where((i) => i.BusquedaRelacionada.BusquedaRelacionada_Diseño == itemBusqueda.BusquedaRelacionada_Diseño))
                                                            {
                                                                itemNumero.Textos.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                                busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                                            }
                                                        }
                                                    }
                                                }

                                                break;
                                        }

                                        break;

                                    case OpcionTextosInformacionBusqueda.SiguienteNumeroAEncontrar:
                                        busquedasAsignarNumeros.Add(busqueda);
                                        break;
                                }

                                if(busquedaTextosSinNumeros_Valida)
                                {
                                    busqueda.TextosInformacion_AAsignar_NumerosSiguientes.Clear();
                                    busqueda.TextosInformacion_AAsignar_NumerosSiguientes.AddRange(busqueda.TextosInformacion);
                                    busqueda.TextosInformacion.Clear();
                                }
                            }
                            //        else
                            //        {
                            //            if (mostrarLog)
                            //                log.Add("No se encontró el " + numeroAgregar.Nombre + " de la entrada " + entrada.Nombre + ".");
                            //            archivo.lector.Close();

                            //            if (archivo.TipoArchivo == TipoArchivo.ServidorFTP &
                            //!string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                            //File.Exists(rutaTemporalArchivoFTP))
                            //                File.Delete(rutaTemporalArchivoFTP);

                            //            ConError = true;
                            //            Thread.CurrentThread.Abort();
                            //        }


                            if (entrada.TipoEntrada == TipoEntrada.ConjuntoNumeros)
                                busqueda.LimpiarVariables();

                            if (busqueda.LecturaTerminada)
                            {
                                if (busquedaContenedora != null)
                                    busquedaContenedora.LecturaTerminada = busqueda.LecturaTerminada;

                                continuar = false;
                            }

                            if (ejecucion.Detenida)
                            {
                                return;
                            }
                        }
                    }

                    if (busquedaContenedora != null)
                    {
                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas)
                        {
                            if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDespuesEjecucion_Elemento)
                            {
                                foreach (var itemNumero in numeros.Numeros)
                                {
                                    ejecucion.EstablecerNombreCantidad(itemNumero, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, itemNumero.PosicionElemento_DefinicionNombres, null, entrada);
                                }
                            }
                        }

                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas)
                        {
                            if (busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.DefinirNombresDespuesEjecucion_Elemento)
                            {
                                foreach (var itemNumero in numeros.Numeros)
                                {
                                    ejecucion.EstablecerNombreCantidad(itemNumero, busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas, itemNumero.PosicionElemento_DefinicionNombres, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDespuesEjecucion_Elemento)
                        {
                            foreach (var itemNumero in numeros.Numeros)
                            {
                                ejecucion.EstablecerNombreCantidad(itemNumero, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, itemNumero.PosicionElemento_DefinicionNombres, null, entrada);
                            }
                        }
                    }

                }


                if (busqueda.EsConjuntoBusquedas &&
                        !soloCabecera)
                {
                    bool ejecucionPrimera = true;
                    bool continuarConjunto = true;
                    int nVecesConjunto = 1;

                    int indiceBusquedaConjunto = 1;

                    while (continuarConjunto &&
                        busqueda.ConjuntoBusquedas.Any())
                    {
                        if (ejecucion.Pausada) ejecucion.Pausar();
                        if (ejecucion.Detenida) return;

                        if (!ejecucionPrimera &&
                            busqueda.EjecutarBusquedaCabeceraIteraciones)
                        {
                            busqueda.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones = busqueda.Iteraciones.Count;
                            busqueda.TextosInformacionEncontrados.Clear();
                            busqueda.NumerosEncontrados.Clear();
                            busqueda.TextosInformacion_AAsignar_NumerosSiguientes.Clear();

                            EjecutarBusquedas_ArchivoUrl(busqueda, mostrarLog, archivo, url, ref rutaTemporalArchivoFTP,
                                                            ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                                                            ejecucion, busquedasAsignarNumeros, numeros, ref indiceBusquedaConjunto, busqueda,
                                                            lecturaNavegacion, codificacion, MismaLecturaArchivo, true);

                            //numeros.CantidadSubElementosProcesados++;
                            busqueda.BusquedasConjuntoEjecutadas++;
                        }

                        foreach (var itemBusquedaConjunto in busqueda.ConjuntoBusquedas)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            itemBusquedaConjunto.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones = busqueda.Iteraciones.Count;
                            itemBusquedaConjunto.TextosInformacionEncontrados.Clear();
                            itemBusquedaConjunto.NumerosEncontrados.Clear();
                            itemBusquedaConjunto.TextosInformacion_AAsignar_NumerosSiguientes.Clear();
                            itemBusquedaConjunto.TextosInformacion.Clear();

                            EjecutarBusquedas_ArchivoUrl(itemBusquedaConjunto, mostrarLog, archivo, url, ref rutaTemporalArchivoFTP,
                                                            ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                                                            ejecucion, busquedasAsignarNumeros, numeros, ref indiceBusquedaConjunto, busqueda, 
                                                            lecturaNavegacion, codificacion, MismaLecturaArchivo);                            

                            numeros.CantidadSubElementosProcesados++;
                            busqueda.BusquedasConjuntoEjecutadas++;

                            switch (busqueda.FinalizacionConjuntoBusquedas)
                            {
                                case OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo:
                                    if (busqueda.LecturaTerminada)
                                        continuarConjunto = false;

                                    break;

                                case OpcionFinBusquedaTexto_Archivos.EncontrarNveces:
                                    if (nVecesConjunto == busqueda.NumeroVecesConjuntoBusquedas)
                                        continuarConjunto = false;
                                    else
                                    {
                                        nVecesConjunto++;
                                    }

                                    break;

                                case OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida:
                                case OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida:

                                    bool valorCondiciones = false;

                                    if (busqueda.Condiciones != null)
                                    {
                                        if (archivo != null)
                                            valorCondiciones = busqueda.Condiciones.EvaluarCondiciones(archivo);
                                        else if (url != null)
                                            valorCondiciones = busqueda.Condiciones.EvaluarCondiciones(url);
                                    }

                                    if (busqueda.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida)
                                    {
                                        if (valorCondiciones) continuarConjunto = false;
                                    }
                                    else if (busqueda.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida)
                                    {
                                        if (!valorCondiciones) continuarConjunto = false;
                                    }

                                    break;
                            }

                            ejecucionPrimera = false;

                            foreach (var itemBusquedaAsignar in busquedasAsignarNumeros)
                                itemBusquedaAsignar.TextosInformacion_AAsignar_NumerosSiguientes.Clear();

                            try { Thread.Sleep(10); } catch (Exception) { };

                        }

                        busqueda.Iteraciones.Add(new IteracionBusquedaEjecucion());

                        foreach (var itemBusquedaConjunto in busqueda.ConjuntoBusquedas)
                            itemBusquedaConjunto.TextosInformacion.Clear();

                        foreach (var itemBusquedaAsignar in busquedasAsignarNumeros)
                            itemBusquedaAsignar.TextosInformacion_AAsignar_NumerosSiguientes.Clear();
                    }

                    indiceBusqueda++;

                }

                if (archivo != null)
                {
                    if (!MismaLecturaArchivo)
                    {
                        archivo.lector.Close();
                        archivo.lectorArchivo.Close();
                        archivosAbiertos.Remove(archivo.lector);
                        archivo.CaracteresCorridos = 0;
                        archivo.CaracteresDescartados = 0;

                        if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                            archivo.TipoArchivo == TipoArchivo.Internet) ||
                            ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                            (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                            archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                    !string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                    File.Exists(rutaTemporalArchivoFTP))
                            File.Delete(rutaTemporalArchivoFTP);

                        ArchivosOrigenesDatos.Remove(archivo);
                    }
                }
            }
        }

        public static void EjecutarBusquedas_ArchivoUrl_TextosInformacion(BusquedaArchivoEjecucion busqueda, bool mostrarLog, ElementoArchivoOrigenDatosEjecucion archivo,
            ElementoInternetOrigenDatosEjecucion url, ref string rutaTemporalArchivoFTP, ref string RutaArchivo, ref HttpResponseMessage respuestaArchivoFTP,
            List<FileStream> archivosAbiertos, List<ElementoArchivoOrigenDatosEjecucion> ArchivosOrigenesDatos, List<string> log, List<string> logResultados, ElementoEntradaEjecucion entrada, ref bool ConError,
            EjecucionCalculo ejecucion, ElementoConjuntoTextosEntradaEjecucion textos, ref int indiceBusqueda, BusquedaArchivoEjecucion busquedaContenedora,
            List<string> TextosEncontradosBusquedaConjunto, List<string> TextosEncontradosBusquedaConjunto_Total, LecturaNavegacion lecturaNavegacion,
            Encoding codificacion, bool soloCabecera = false)
        {
            bool valorCondicionesBusqueda = true;

            if (busqueda.Condiciones_RealizacionBusqueda != null)
            {
                if (archivo != null)
                    valorCondicionesBusqueda = busqueda.Condiciones_RealizacionBusqueda.EvaluarCondiciones(archivo);
                else if (url != null)
                    valorCondicionesBusqueda = busqueda.Condiciones_RealizacionBusqueda.EvaluarCondiciones(url);
            }

            if (valorCondicionesBusqueda)
            {
                if (lecturaNavegacion != null)
                {
                    valorCondicionesBusqueda = lecturaNavegacion.BusquedasARealizar.Contains(busqueda.BusquedaRelacionada_Diseño);
                }
            }

            if (valorCondicionesBusqueda)
            {
                string descripcionBusquedaActual = string.Empty;

                if (archivo != null)
                {
                    if (!archivo.MismaLecturaArchivo)
                    {
                        bool archivoTemporal = false;

                        switch (archivo.TipoArchivo)
                        {
                            case TipoArchivo.EquipoLocal:
                            case TipoArchivo.RedLocal:
                                rutaTemporalArchivoFTP = RutaArchivo;
                                break;

                            case TipoArchivo.ServidorFTP:
                            case TipoArchivo.Internet:

                                rutaTemporalArchivoFTP = App.RutaArchivos_Temporales + "\\" + Guid.NewGuid().ToString();
                                archivoTemporal = true;
                                ElementoArchivoOrigenDatosEjecucion.GuardarArchivoTemporalEntrada_FTP(rutaTemporalArchivoFTP, respuestaArchivoFTP);
                                break;
                        }

                        rutaTemporalArchivoFTP = archivo.ObtenerArchivoTextoPlano_Temporal(rutaTemporalArchivoFTP, archivoTemporal, codificacion,
                            ref mostrarLog, log, ref ConError, ejecucion, entrada.ElementoDiseñoRelacionado.EntradaRelacionada, entrada);

                        string ruta = rutaTemporalArchivoFTP;
                        while (archivosAbiertos.Any(i => i.Name == ruta && i.CanRead))
                            Thread.Sleep(10);

                        while (true)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            try
                            {
                                archivo.lectorArchivo = new StreamReader(new FileStream(rutaTemporalArchivoFTP, FileMode.OpenOrCreate), false);
                                break;
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(10);
                                continue;
                            }
                        }

                        archivo.lector = (FileStream)archivo.lectorArchivo.BaseStream;
                        archivosAbiertos.Add(archivo.lector);
                        ArchivosOrigenesDatos.Add(archivo);
                    }

                    descripcionBusquedaActual = archivo.Busquedas.First().Descripcion;
                }
                else if (url != null)
                {
                    if (!url.MismaLecturaArchivo)
                    {
                        //archivo.lector = new FileStream(archivo.RutaArchivo, FileMode.OpenOrCreate);
                        url.ContenidoTexto = url.ObjetoURL.ObtenerTexto().Replace(" ", string.Empty);
                        url.IndiceProcesamientoTexto = 0;
                    }

                    descripcionBusquedaActual = url.Busquedas.First().Descripcion;
                }

                if (busqueda != null)
                {
                    if (mostrarLog && !string.IsNullOrEmpty(busqueda.Nombre))
                    {
                        log.Add("Realizando la búsqueda '" + busqueda.Nombre + "' en el archivo...");
                        logResultados.Add("Realizando la búsqueda '" + busqueda.Nombre + "' en el archivo...");
                    }

                    if (mostrarLog && !string.IsNullOrEmpty(busqueda.Descripcion))
                    {
                        log.Add("Información y comentarios de la búsqueda: " + descripcionBusquedaActual + ".");
                        logResultados.Add("Información y comentarios de la búsqueda: " + descripcionBusquedaActual + ".");
                    }

                    if (busqueda.EsConjuntoBusquedas &&
                        !soloCabecera)
                    {
                        busqueda.Iteraciones.Add(new IteracionBusquedaEjecucion());
                        busqueda.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones = busqueda.Iteraciones.Count;
                    }

                    busqueda.TextosInformacionEncontrados_NombresCantidades.Clear();

                    bool variasVeces = true;
                    int totalVeces = 1;
                    if (entrada.TipoEntrada == TipoEntrada.TextosInformacion)
                    {
                        variasVeces = false;
                        totalVeces = busqueda.NumeroVecesBusquedaNumero;
                    }

                    List<string> TextosEncontradosBusqueda = new List<string>();

                    if (busqueda.FinalizacionBusqueda == OpcionFinBusquedaTexto_Archivos.EncontrarNveces)
                    {
                        for (int veces = 1; veces <= totalVeces; veces++)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            //busqueda.tamañoDatosTexto.Clear();
                            try
                            {
                                if (archivo != null)
                                    busqueda.ProcesarTextoBusqueda(archivo, variasVeces);
                                else if (url != null)
                                    busqueda.ProcesarTextoBusqueda(url, variasVeces);
                            }
                            catch (Exception e)
                            {
                                try { Thread.Sleep(3000); } catch (Exception) { };
                                ConError = true;

                                if (mostrarLog)
                                {
                                    log.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                    logResultados.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                }

                                ejecucion.Detener();
                                return;
                            }

                            busqueda.ProcesarValidacion();

                            string textoAgregar = string.Empty;

                            if (busqueda.BusquedaValida)
                            {
                                if (busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaElemento)
                                {
                                    if(busqueda.TextosInformacion.Any())
                                        textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion) });
                                }

                                TextosEncontradosBusqueda.AddRange(busqueda.TextosInformacion);

                                if (busquedaContenedora != null)
                                {
                                    if (busquedaContenedora.BusquedaRelacionada_Diseño.IncluirTextosInformacion_BusquedaContenedora)
                                    {
                                        TextosEncontradosBusqueda.AddRange(busquedaContenedora.TextosInformacionEncontrados);
                                        busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busquedaContenedora.TextosInformacionEncontrados));
                                    }

                                    if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas)
                                    {
                                        if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                        {
                                            EntidadNumero numeroAgregar = new EntidadNumero();

                                            if(textos.FilasTextosInformacion.Any() &&
                                                textos.FilasTextosInformacion.Last().TextosInformacion.Any())
                                                numeroAgregar.Textos.AddRange(textos.FilasTextosInformacion.Last().TextosInformacion);

                                            var ruta = RutaArchivo;
                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionIncluidos.Where(i => ruta.Contains(i)).ToList()));

                                            ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, 1, null, entrada);
                                            
                                            if(!string.IsNullOrEmpty(numeroAgregar.Nombre))
                                                busqueda.TextosInformacionEncontrados_NombresCantidades.Add(numeroAgregar.Nombre);
                                        }
                                    }

                                    if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas)
                                    {
                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.DefinirNombresDuranteEjecucion_Elemento)
                                        {
                                            EntidadNumero numeroAgregar = new EntidadNumero();

                                            if (textos.FilasTextosInformacion.Any() &&
                                                textos.FilasTextosInformacion.Last().TextosInformacion.Any())
                                                numeroAgregar.Textos.AddRange(textos.FilasTextosInformacion.Last().TextosInformacion);
                                            
                                            ejecucion.EstablecerNombreCantidad(numeroAgregar, busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas, 1, null);

                                            if (!string.IsNullOrEmpty(numeroAgregar.Nombre))
                                                busqueda.TextosInformacionEncontrados_NombresCantidades.Add(numeroAgregar.Nombre);
                                        }
                                    }
                                }
                                else
                                {
                                    if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                    {
                                        EntidadNumero numeroAgregar = new EntidadNumero();
                                        
                                        if (textos.FilasTextosInformacion.Any() &&
                                            textos.FilasTextosInformacion.Last().TextosInformacion.Any())
                                            numeroAgregar.Textos.AddRange(textos.FilasTextosInformacion.Last().TextosInformacion);
                                        
                                        ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, 1, null, entrada);

                                        if (!string.IsNullOrEmpty(numeroAgregar.Nombre))
                                            busqueda.TextosInformacionEncontrados_NombresCantidades.Add(numeroAgregar.Nombre);
                                    }
                                }

                                if (textos.FilasTextosInformacion.Any())
                                    textos.FilasTextosInformacion.Last().TextosInformacion.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionFijos));

                                if (archivo.TextosInformacionFijos.Any())
                                    TextosEncontradosBusqueda.AddRange(archivo.TextosInformacionFijos);

                                busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                busqueda.TextosInformacion.Clear();
                            }

                            if (entrada.TipoEntrada == TipoEntrada.TextosInformacion)
                                busqueda.LimpiarVariables();

                            if (busqueda.LecturaTerminada)
                            {
                                busquedaContenedora.LecturaTerminada = busqueda.LecturaTerminada;
                            }
                        }
                    }
                    else
                    {
                        bool continuar = true;
                        while (continuar)
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            try
                            {
                                if (archivo != null)
                                    continuar = busqueda.ProcesarTextoBusqueda(archivo, variasVeces);
                                else if (url != null)
                                    continuar = busqueda.ProcesarTextoBusqueda(url, variasVeces);
                            }
                            catch (Exception e)
                            {
                                try { Thread.Sleep(3000); } catch (Exception) { };
                                ConError = true;

                                if (mostrarLog)
                                {
                                    log.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                    logResultados.Add("Error al leer datos de la variable o vector de entrada '" + entrada.Nombre + "': " + e.Message);
                                }

                                ejecucion.Detener();
                                return;
                            }

                            busqueda.ProcesarValidacion();

                            if (busqueda.BusquedaValida || continuar)
                            {
                                if (busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaElemento)
                                {
                                    if(busqueda.TextosInformacion.Any())
                                        textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion) });
                                }

                                if(busqueda.TextosInformacion.Any())
                                    TextosEncontradosBusqueda.AddRange(busqueda.TextosInformacion);

                                if (busquedaContenedora != null)
                                {
                                    if (busquedaContenedora.BusquedaRelacionada_Diseño.IncluirTextosInformacion_BusquedaContenedora)
                                    {
                                        if(busquedaContenedora.TextosInformacionEncontrados.Any())
                                            TextosEncontradosBusqueda.AddRange(busquedaContenedora.TextosInformacionEncontrados);
                                        
                                        if(busquedaContenedora.TextosInformacionEncontrados.Any())
                                            busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busquedaContenedora.TextosInformacionEncontrados));
                                    }

                                    if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas)
                                    {
                                        if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                        {
                                            EntidadNumero numeroAgregar = new EntidadNumero();

                                            if (textos.FilasTextosInformacion.Any() &&
                                            textos.FilasTextosInformacion.Last().TextosInformacion.Any())
                                                numeroAgregar.Textos.AddRange(textos.FilasTextosInformacion.Last().TextosInformacion);

                                            var ruta = RutaArchivo;
                                            numeroAgregar.Textos.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionIncluidos.Where(i => ruta.Contains(i)).ToList()));

                                            ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, 1, null, entrada);

                                            if (!string.IsNullOrEmpty(numeroAgregar.Nombre))
                                                busqueda.TextosInformacionEncontrados_NombresCantidades.Add(numeroAgregar.Nombre);
                                        }
                                    }

                                    if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas)
                                    {
                                        if (busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.DefinirNombresDuranteEjecucion_Elemento)
                                        {
                                            EntidadNumero numeroAgregar = new EntidadNumero();

                                            if (textos.FilasTextosInformacion.Any() &&
                                            textos.FilasTextosInformacion.Last().TextosInformacion.Any())
                                                numeroAgregar.Textos.AddRange(textos.FilasTextosInformacion.Last().TextosInformacion);

                                            ejecucion.EstablecerNombreCantidad(numeroAgregar, busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas, 1, null);

                                            if (!string.IsNullOrEmpty(numeroAgregar.Nombre))
                                                busqueda.TextosInformacionEncontrados_NombresCantidades.Add(numeroAgregar.Nombre);
                                        }
                                    }
                                }
                                else
                                {
                                    if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDuranteEjecucion_Elemento)
                                    {
                                        EntidadNumero numeroAgregar = new EntidadNumero();

                                        if (textos.FilasTextosInformacion.Any() &&
                                            textos.FilasTextosInformacion.Last().TextosInformacion.Any())
                                            numeroAgregar.Textos.AddRange(textos.FilasTextosInformacion.Last().TextosInformacion);

                                        ejecucion.EstablecerNombreCantidad(numeroAgregar, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, 1, null, entrada);

                                        if (!string.IsNullOrEmpty(numeroAgregar.Nombre))
                                            busqueda.TextosInformacionEncontrados_NombresCantidades.Add(numeroAgregar.Nombre);
                                    }
                                }

                                if (textos.FilasTextosInformacion.Any())
                                    textos.FilasTextosInformacion.Last().TextosInformacion.AddRange(ejecucion.GenerarTextosInformacion(archivo.TextosInformacionFijos));

                                if (archivo.TextosInformacionFijos.Any())
                                    TextosEncontradosBusqueda.AddRange(archivo.TextosInformacionFijos);

                                busqueda.TextosInformacionEncontrados.AddRange(ejecucion.GenerarTextosInformacion(busqueda.TextosInformacion));
                                busqueda.TextosInformacion.Clear();
                            }

                            if (entrada.TipoEntrada == TipoEntrada.TextosInformacion)
                                busqueda.LimpiarVariables();

                            if (busqueda.LecturaTerminada)
                            {
                                if (busquedaContenedora != null)
                                    busquedaContenedora.LecturaTerminada = busqueda.LecturaTerminada;

                                continuar = false;
                            }

                            if (ejecucion.Detenida)
                            {
                                return;
                            }
                        }

                    }

                    if (busquedaContenedora != null)
                    {
                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesBusquedasIteracion_ConjuntoBusquedas)
                        {
                            if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDespuesEjecucion_Elemento)
                            {
                                foreach (var fila in textos.FilasTextosInformacion)
                                {
                                    if (ejecucion.Pausada) ejecucion.Pausar();
                                    if (ejecucion.Detenida) return;

                                    EntidadNumero itemNumero = new EntidadNumero();

                                    if (fila.TextosInformacion.Any())
                                        itemNumero.Textos.AddRange(fila.TextosInformacion);

                                    ejecucion.EstablecerNombreCantidad(itemNumero, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, itemNumero.PosicionElemento_DefinicionNombres, null, entrada);

                                    if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                        busqueda.TextosInformacionEncontrados_NombresCantidades.Add(itemNumero.Nombre);
                                }
                            }
                        }

                        if (busquedaContenedora.BusquedaRelacionada_Diseño.EstablecerNombresCantidadesIteracion_ConjuntoBusquedas)
                        {
                            if (busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas.DefinirNombresDespuesEjecucion_Elemento)
                            {
                                foreach (var fila in textos.FilasTextosInformacion)
                                {
                                    if (ejecucion.Pausada) ejecucion.Pausar();
                                    if (ejecucion.Detenida) return;

                                    EntidadNumero itemNumero = new EntidadNumero();

                                    if (fila.TextosInformacion.Any())
                                        itemNumero.Textos.AddRange(fila.TextosInformacion);

                                    ejecucion.EstablecerNombreCantidad(itemNumero, busquedaContenedora.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades_ConjuntoBusquedas, itemNumero.PosicionElemento_DefinicionNombres, null);

                                    if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                        busqueda.TextosInformacionEncontrados_NombresCantidades.Add(itemNumero.Nombre);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades.DefinirNombresDespuesEjecucion_Elemento)
                        {
                            EntidadNumero itemNumero = new EntidadNumero();
                            ejecucion.EstablecerNombreCantidad(itemNumero, busqueda.BusquedaRelacionada_Diseño.DefinicionOpcionesNombresCantidades, itemNumero.PosicionElemento_DefinicionNombres, null, entrada);

                            if (!string.IsNullOrEmpty(itemNumero.Nombre))
                                busqueda.TextosInformacionEncontrados_NombresCantidades.Add(itemNumero.Nombre);
                        }
                    }

                    if (busqueda.TextosInformacionEncontrados_NombresCantidades.Any())
                    {
                        if (busqueda.BusquedaRelacionada_Diseño.ReemplazarTextosInformacion_NombresCantidades)
                        {
                            busqueda.TextosInformacionEncontrados.Clear();
                            TextosEncontradosBusqueda.Clear();
                        }
                                                
                        busqueda.TextosInformacionEncontrados.AddRange(busqueda.TextosInformacionEncontrados_NombresCantidades);
                        TextosEncontradosBusqueda.AddRange(busqueda.TextosInformacionEncontrados_NombresCantidades);
                    }

                    if (busquedaContenedora != null)
                    {
                        if(TextosEncontradosBusqueda.Any())
                            TextosEncontradosBusquedaConjunto.AddRange(ejecucion.GenerarTextosInformacion(TextosEncontradosBusqueda));
                        
                        if(TextosEncontradosBusquedaConjunto.Any())
                            TextosEncontradosBusquedaConjunto_Total.AddRange(TextosEncontradosBusquedaConjunto);
                    }

                    if (!busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaElemento &&
                            !busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto &&
                            !busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto &&
                            !busqueda.BusquedaRelacionada_Diseño.NoGenerarFilasTextosInformacion)
                    {
                        if(TextosEncontradosBusqueda.Any())
                            textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = ejecucion.GenerarTextosInformacion(TextosEncontradosBusqueda) });
                    }

                    if (busquedaContenedora != null)
                    {
                        if (busquedaContenedora.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto)
                        {
                            if(TextosEncontradosBusquedaConjunto.Any())
                                textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = ejecucion.GenerarTextosInformacion(TextosEncontradosBusquedaConjunto) });
                        }
                    }


                    if (busqueda.EsConjuntoBusquedas &&
                        !soloCabecera)
                    {
                        bool ejecucionPrimera = true;
                        bool continuarConjunto = true;
                        int nVecesConjunto = 1;

                        int indiceBusquedaConjunto = 1;

                        List<string> TextosEncontradosBusquedaConjunto_Total_Iteracion = new List<string>();

                        while (continuarConjunto &&
                            busqueda.ConjuntoBusquedas.Any())
                        {
                            if (ejecucion.Pausada) ejecucion.Pausar();
                            if (ejecucion.Detenida) return;

                            List<string> TextosEncontradosBusquedaConjunto_Iteraciones = new List<string>();

                            if (!ejecucionPrimera &&
                            busqueda.EjecutarBusquedaCabeceraIteraciones)
                            {
                                busqueda.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones = busqueda.Iteraciones.Count;

                                List<string> TextosEncontradosBusquedaConjunto_Iteracion = new List<string>();
                                busqueda.TextosInformacionEncontrados.Clear();

                                BusquedaArchivoEjecucion.EjecutarBusquedas_ArchivoUrl_TextosInformacion(busqueda, mostrarLog, archivo, url, ref rutaTemporalArchivoFTP,
                                                                ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                                                                ejecucion, textos, ref indiceBusquedaConjunto, busqueda, TextosEncontradosBusquedaConjunto_Iteracion,
                                                                TextosEncontradosBusquedaConjunto_Total_Iteracion, lecturaNavegacion, codificacion, true);

                                busqueda.BusquedasConjuntoEjecutadas++;

                                if (TextosEncontradosBusquedaConjunto_Iteracion.Any())
                                    TextosEncontradosBusquedaConjunto_Iteraciones.AddRange(TextosEncontradosBusquedaConjunto_Iteracion);

                                if (archivo != null)
                                {
                                    if (!archivo.MismaLecturaArchivo)
                                    {
                                        archivo.lector.Close();
                                        archivo.lectorArchivo.Close();
                                        archivosAbiertos.Remove(archivo.lector);
                                        archivo.CaracteresCorridos = 0;
                                        archivo.CaracteresDescartados = 0;

                                        if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                            archivo.TipoArchivo == TipoArchivo.Internet) ||
                            ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                            (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                            archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                                    !string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                                    File.Exists(rutaTemporalArchivoFTP))
                                            File.Delete(rutaTemporalArchivoFTP);

                                        ArchivosOrigenesDatos.Remove(archivo);
                                    }
                                }
                            }

                            foreach (var itemBusquedaConjunto in busqueda.ConjuntoBusquedas)
                            {
                                if (ejecucion.Pausada) ejecucion.Pausar();
                                if (ejecucion.Detenida) return;

                                itemBusquedaConjunto.IndiceActualNumeros_AsignarTextosInformacion_Iteraciones = busqueda.Iteraciones.Count;

                                List<string> TextosEncontradosBusquedaConjunto_Iteracion = new List<string>();
                                itemBusquedaConjunto.TextosInformacionEncontrados.Clear();

                                BusquedaArchivoEjecucion.EjecutarBusquedas_ArchivoUrl_TextosInformacion(itemBusquedaConjunto, mostrarLog, archivo, url, ref rutaTemporalArchivoFTP,
                                                                ref RutaArchivo, ref respuestaArchivoFTP, archivosAbiertos, ArchivosOrigenesDatos, log, logResultados, entrada, ref ConError,
                                                                ejecucion, textos, ref indiceBusquedaConjunto, busqueda, TextosEncontradosBusquedaConjunto_Iteracion,
                                                                TextosEncontradosBusquedaConjunto_Total_Iteracion, lecturaNavegacion, codificacion);

                                busqueda.BusquedasConjuntoEjecutadas++;

                                if(TextosEncontradosBusquedaConjunto_Iteracion.Any())
                                    TextosEncontradosBusquedaConjunto_Iteraciones.AddRange(TextosEncontradosBusquedaConjunto_Iteracion);

                                if (archivo != null)
                                {
                                    if (!archivo.MismaLecturaArchivo)
                                    {
                                        archivo.lector.Close();
                                        archivo.lectorArchivo.Close();
                                        archivosAbiertos.Remove(archivo.lector);
                                        archivo.CaracteresCorridos = 0;
                                        archivo.CaracteresDescartados = 0;

                                        if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                            archivo.TipoArchivo == TipoArchivo.Internet) ||
                            ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                            (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                            archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                                    !string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                                    File.Exists(rutaTemporalArchivoFTP))
                                            File.Delete(rutaTemporalArchivoFTP);

                                        ArchivosOrigenesDatos.Remove(archivo);
                                    }
                                }

                            }

                            if (busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaIteracion_BusquedasConjunto)
                            {
                                if(TextosEncontradosBusquedaConjunto_Iteraciones.Any())
                                    textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = ejecucion.GenerarTextosInformacion(TextosEncontradosBusquedaConjunto_Iteraciones) });
                            }

                            switch (busqueda.FinalizacionConjuntoBusquedas)
                            {
                                case OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo:
                                    if (busqueda.LecturaTerminada)
                                        continuarConjunto = false;

                                    break;

                                case OpcionFinBusquedaTexto_Archivos.EncontrarNveces:
                                    if (nVecesConjunto == busqueda.NumeroVecesConjuntoBusquedas)
                                        continuarConjunto = false;
                                    else
                                    {
                                        nVecesConjunto++;
                                    }

                                    break;

                                case OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida:
                                case OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida:

                                    bool valorCondiciones = false;

                                    if (archivo != null)
                                        valorCondiciones = busqueda.Condiciones.EvaluarCondiciones(archivo);
                                    else if (url != null)
                                        valorCondiciones = busqueda.Condiciones.EvaluarCondiciones(url);

                                    if (busqueda.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida)
                                    {
                                        if (valorCondiciones) continuarConjunto = false;
                                    }
                                    else if (busqueda.FinalizacionConjuntoBusquedas == OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida)
                                    {
                                        if (!valorCondiciones) continuarConjunto = false;
                                    }

                                    break;
                            }

                            ejecucionPrimera = false;

                            busqueda.Iteraciones.Add(new IteracionBusquedaEjecucion());

                            try { Thread.Sleep(10); } catch (Exception) { };

                            if (ejecucion.Detenida)
                            {
                                return;
                            }
                        }

                        //if (busqueda.BusquedaRelacionada_Diseño.GenerarFilasTextosInformacion_PorCadaElemento_BusquedasConjunto)
                        //{
                        //    if(TextosEncontradosBusquedaConjunto_Total_Iteracion.Any())
                        //        textos.FilasTextosInformacion.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = ejecucion.GenerarTextosInformacion(TextosEncontradosBusquedaConjunto_Total_Iteracion) });
                        //}
                    }

                    indiceBusqueda++;
                    textos.CantidadSubElementosProcesados++;
                }

                if (archivo != null)
                {
                    if (!archivo.MismaLecturaArchivo)
                    {
                        archivo.lector.Close();
                        archivo.lectorArchivo.Close();
                        archivosAbiertos.Remove(archivo.lector);
                        archivo.CaracteresCorridos = 0;
                        archivo.CaracteresDescartados = 0;

                        if (((archivo.TipoArchivo == TipoArchivo.ServidorFTP |
                            archivo.TipoArchivo == TipoArchivo.Internet) ||
                            ((archivo.TipoArchivo == TipoArchivo.EquipoLocal |
                            archivo.TipoArchivo == TipoArchivo.RedLocal) &&
                            (archivo.TipoFormatoArchivo_Entrada != TipoFormatoArchivoEntrada.ArchivoTextoPlano ||
                            archivo.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla))) &
                    !string.IsNullOrEmpty(rutaTemporalArchivoFTP) &
                    File.Exists(rutaTemporalArchivoFTP))
                            File.Delete(rutaTemporalArchivoFTP);

                        ArchivosOrigenesDatos.Remove(archivo);
                    }
                }
            }
        }

        public void ProcesarValidacion()
        {
            bool valida = true;
            int cantidadCamposDatosNumerosProceso = 0;
            int indice = 0;

            do
            {
                if (indice + cadenaFormatoDatos.Length <= TextoBusquedaNumero_Proceso.Length &
                    indice + cadenaFormatoNumero.Length <= TextoBusquedaNumero_Proceso.Length)
                {
                    if (TextoBusquedaNumero_Proceso.Substring(indice, cadenaFormatoDatos.Length).ToLower().Equals(cadenaFormatoDatos))
                    {
                        //indice += cadenaFormatoDatos.Length;
                        cantidadCamposDatosNumerosProceso++;
                    }
                    else if (TextoBusquedaNumero_Proceso.Substring(indice, cadenaFormatoNumero.Length).ToLower().Equals(cadenaFormatoNumero))
                    {
                        //indice += cadenaFormatoNumero.Length;
                        cantidadCamposDatosNumerosProceso++;
                    }
                }

                indice++;
            } while (indice <= TextoBusquedaNumero_Proceso.Length - 1);

            if (CantidadCamposDatosNumeros > 0 && 
                cantidadCamposDatosNumerosProceso == CantidadCamposDatosNumeros)
            {
                int cantidadLineasIguales = 0;
                int indiceSubString = 0;
                int indiceCaracter = 0;
                int cantidadCaracteres = 0;
                List<string> lineasTextoEjecucion = new List<string>();
                List<string> lineasTextoOriginal = new List<string>();

                while (true)
                {
                    if (TextoBusquedaNumero_EnEjecucion.Length > 0 && (TextoBusquedaNumero_EnEjecucion[indiceCaracter] == '\n' |
                        indiceCaracter == TextoBusquedaNumero_EnEjecucion.Length - 1))
                    {
                        if (indiceCaracter == TextoBusquedaNumero_EnEjecucion.Length - 1) cantidadCaracteres++;

                        lineasTextoEjecucion.Add(TextoBusquedaNumero_EnEjecucion.Substring(indiceSubString, cantidadCaracteres).Trim());
                        if (lineasTextoEjecucion.Last().Equals(string.Empty))
                            lineasTextoEjecucion.Remove(lineasTextoEjecucion.Last());
                        indiceSubString = indiceCaracter + 1;
                        cantidadCaracteres = 0;
                    }
                    else
                        cantidadCaracteres++;

                    indiceCaracter++;
                    if (indiceCaracter >= TextoBusquedaNumero_EnEjecucion.Length)
                        break;
                }

                indiceSubString = 0;
                indiceCaracter = 0;
                cantidadCaracteres = 0;

                TextoBusquedaNumero_Proceso = TextoBusquedaNumero_Proceso.Replace(cadenaFormatoDatos.ToUpper(), cadenaFormatoDatos);

                while (true)
                {
                    if (TextoBusquedaNumero_Proceso.Length > 0 && TextoBusquedaNumero_Proceso[indiceCaracter] == '\n' |
                        indiceCaracter == TextoBusquedaNumero_Proceso.Length - 1)
                    {
                        if (indiceCaracter == TextoBusquedaNumero_Proceso.Length - 1) cantidadCaracteres++;

                        lineasTextoOriginal.Add(TextoBusquedaNumero_Proceso.Substring(indiceSubString, cantidadCaracteres).Trim());
                        if (lineasTextoOriginal.Last().Equals(string.Empty))
                            lineasTextoOriginal.Remove(lineasTextoOriginal.Last());
                        indiceSubString = indiceCaracter + 1;
                        cantidadCaracteres = 0;
                    }
                    else
                        cantidadCaracteres++;

                    indiceCaracter++;
                    if (indiceCaracter >= TextoBusquedaNumero_Proceso.Length)
                        break;
                }

                if (lineasTextoEjecucion.Count == lineasTextoOriginal.Count)
                {
                    for (int indiceLista = 0; indiceLista <= lineasTextoOriginal.Count - 1; indiceLista++)
                    {
                        string cadena = lineasTextoEjecucion[indiceLista];
                        CompararProcesarStrings(ref cadena, lineasTextoOriginal[indiceLista], tamañoDatosTexto[indiceLista], false);
                        lineasTextoEjecucion[indiceLista] = cadena;
                    }

                    for (int indiceLista = 0; indiceLista <= lineasTextoEjecucion.Count - 1; indiceLista++)
                    {
                        if (string.Compare(lineasTextoEjecucion[indiceLista], lineasTextoOriginal[indiceLista]) == 0)
                            cantidadLineasIguales++;
                    }

                    if (cantidadLineasIguales != lineasTextoEjecucion.Count)
                        valida = false;
                }
                else
                    valida = false;

                busqValida = valida;
            }
            else if (!NumeroEncontrado)
                    busqValida = true;
        }

        //public void CompararProcesarStrings(ref string cadenaDatos, string cadenaOriginal, int indiceTamaños)
        //{
        //    int indiceCaracter = 0;
        //    while (true)
        //    {
        //        if(cadenaDatos[indiceCaracter] != cadenaOriginal[indiceCaracter])
        //        {
        //            if (cadenaOriginal.Substring(indiceCaracter, cadenaFormatoDatos.Length).Equals(cadenaFormatoDatos))
        //            {
        //                cadenaDatos = cadenaDatos.Remove(indiceCaracter, tamañoDatosTexto[indiceTamaños]);
        //                cadenaDatos = cadenaDatos.Insert(indiceCaracter, cadenaFormatoDatos);
        //            }
        //            else if (cadenaOriginal.Substring(indiceCaracter, cadenaFormatoNumero.Length).Equals(cadenaFormatoNumero))
        //            {
        //                while (char.IsDigit(cadenaDatos[indiceCaracter]) |
        //                        char.IsPunctuation(cadenaDatos[indiceCaracter]) |
        //                        char.IsWhiteSpace(cadenaDatos[indiceCaracter]))
        //                {
        //                    cadenaDatos = cadenaDatos.Remove(indiceCaracter, 1);
        //                    if (indiceCaracter >= cadenaDatos.Length)
        //                        break;
        //                }
        //                cadenaDatos = cadenaDatos.Insert(indiceCaracter, cadenaFormatoNumero);
        //            }
        //        }

        //        indiceCaracter++;
        //        if (indiceCaracter >= cadenaDatos.Length - 1 |
        //            indiceCaracter >= cadenaOriginal.Length - 1)
        //        {
        //            break;
        //        }
        //    }
        //}

        //public void ProcesarTextoBusqueda(ElementoArchivoOrigenDatosEjecucion archivo, bool NVeces)
        //{
        //    TextoBusquedaNumero = TextoBusquedaNumero.Replace(" ", string.Empty);

        //    int cantidadVeces;
        //    if (NVeces)
        //        cantidadVeces = NumeroVecesBusquedaNumero;
        //    else
        //        cantidadVeces = 1;

        //    int cantidadCaracteresCampoDatos = 0;
        //    int indiceCaracteresCampoDatos = -1;
        //    int cantidadTotalCaracteresEjecucion = 0;
        //    int cantidadCaracteresNoCadenaOriginal = 0;
        //    int cantidadDigitosCampo = 0;
        //    int cantidadCaracteresCadenaOriginal = 0;
        //    string CadenaVerificaVacia = string.Empty;

        //    bool numeroLeido = false;
        //    bool esCampoNumero = false;
        //    bool esCampoDatos = false;
        //    bool TextoDiferente = false;
        //    //bool LineaVaciaEncontrada = false;

        //    string CadenaValorNumero = string.Empty;
        //    byte[] textoLeido = new byte[1];

        //    int cantidad = 0;
        //    do
        //    {
        //        archivo.lector.Read(textoLeido, 0, 1);
        //        while (archivo.lector.Position < archivo.lector.Length)
        //        {
        //            if (Encoding.Default.GetChars(textoLeido)[0] == ' ')
        //                archivo.lector.Read(textoLeido, 0, 1);
        //            else
        //                break;
        //        }

        //        bool LineaVaciaEncontrada = false;

        //        //if (Encoding.Default.GetChars(textoLeido)[0] == '\r')
        //        //{
        //        //    LineaVaciaEncontrada = true;
        //        //    continue;
        //        //}

        //        if (Encoding.Default.GetChars(textoLeido)[0] == '\r')
        //            continue;

        //        if (Encoding.Default.GetChars(textoLeido)[0] == '\n')
        //        {
        //            if (CadenaVerificaVacia.Equals(string.Empty))
        //            {
        //                LineaVaciaEncontrada = true;
        //            }

        //            CadenaVerificaVacia = string.Empty;
        //        }
        //        else
        //        {
        //            CadenaVerificaVacia += Encoding.Default.GetString(textoLeido);
        //        }

        //        if (!LineaVaciaEncontrada)
        //        {
        //            TextoBusquedaNumero_EnEjecucion += Encoding.Default.GetString(textoLeido);
        //            cantidadTotalCaracteresEjecucion++;

        //            indiceCaracteresCampoDatos = cantidadTotalCaracteresEjecucion - cantidadCaracteresCampoDatos - cantidadCaracteresNoCadenaOriginal +
        //                (((CantidadCamposDatosNumeros - (numeroLeido ? 1 : 0)) >= 0 ? (CantidadCamposDatosNumeros - (numeroLeido ? 1 : 0)) : 0) * cadenaFormatoDatos.Length + (numeroLeido ? cadenaFormatoNumero.Length : 0)) - 1;

        //            if (esCampoDatos | esCampoNumero)
        //            {
        //                if (esCampoNumero)
        //                {
        //                    if (textoLeido.Length > 0 && char.IsDigit(Encoding.Default.GetChars(textoLeido)[0]) |
        //                        Encoding.Default.GetChars(textoLeido)[0] == '.' |
        //                        Encoding.Default.GetChars(textoLeido)[0] == ',')
        //                    {
        //                        if (esCampoNumero)
        //                        {
        //                            CadenaValorNumero += Encoding.Default.GetString(textoLeido);
        //                        }

        //                        cantidadDigitosCampo++;
        //                    }
        //                    else
        //                    {
        //                        //if (esCampoNumero)
        //                        //{
        //                        double.TryParse(CadenaValorNumero, out ValorNumeroEncontrado);
        //                        //}
        //                        tamañoDatosTexto.Add(cantidadDigitosCampo);
        //                        cantidadDigitosCampo = 0;

        //                        if (numeroLeido == false)
        //                            numeroLeido = true;

        //                        esCampoNumero = false;

        //                        CantidadCamposDatosNumeros++;
        //                        cantidadCaracteresCampoDatos--;
        //                    }
        //                }
        //                else if (esCampoDatos)
        //                {
        //                    if (indiceCaracteresCampoDatos <= TextoBusquedaNumero.Length - 1)
        //                    {
        //                        if (TextoBusquedaNumero_EnEjecucion[cantidadTotalCaracteresEjecucion - 1] == TextoBusquedaNumero[indiceCaracteresCampoDatos])
        //                        {
        //                            //cantidadCaracteresNoCadenaOriginal++;
        //                            //TextoDiferente = true;

        //                            esCampoDatos = false;

        //                            CantidadCamposDatosNumeros++;
        //                            cantidadCaracteresCampoDatos--;
        //                        }
        //                    }
        //                }

        //                cantidadCaracteresCampoDatos++;
        //            }
        //            else
        //            {
        //                if (indiceCaracteresCampoDatos + cadenaFormatoDatos.Length <= TextoBusquedaNumero.Length &
        //                indiceCaracteresCampoDatos + cadenaFormatoNumero.Length <= TextoBusquedaNumero.Length)
        //                {
        //                    if (string.Equals(TextoBusquedaNumero.Substring(indiceCaracteresCampoDatos, cadenaFormatoDatos.Length), cadenaFormatoDatos))
        //                    {
        //                        esCampoDatos = true;
        //                    }
        //                    else if (string.Equals(TextoBusquedaNumero.Substring(indiceCaracteresCampoDatos, cadenaFormatoNumero.Length), cadenaFormatoNumero))
        //                    {
        //                        esCampoNumero = true;
        //                    }
        //                }

        //                if (esCampoDatos | esCampoNumero)
        //                {
        //                    if (textoLeido.Length > 0 && char.IsDigit(Encoding.Default.GetChars(textoLeido)[0]) |
        //                        Encoding.Default.GetChars(textoLeido)[0] == '.' |
        //                        Encoding.Default.GetChars(textoLeido)[0] == ',')
        //                    {
        //                        if (esCampoNumero)
        //                        {
        //                            CadenaValorNumero += Encoding.Default.GetString(textoLeido);
        //                        }

        //                        cantidadDigitosCampo++;
        //                    }

        //                    cantidadCaracteresCampoDatos++;
        //                }
        //                else
        //                {
        //                    if (indiceCaracteresCampoDatos <= TextoBusquedaNumero.Length - 1)
        //                    {
        //                        if (TextoBusquedaNumero[indiceCaracteresCampoDatos] != Encoding.Default.GetChars(textoLeido)[0])
        //                        {
        //                            //cantidadCaracteresNoCadenaOriginal++;
        //                            TextoDiferente = true;
        //                        }
        //                    }
        //                }
        //            }


        //            cantidadCaracteresCadenaOriginal = cantidadTotalCaracteresEjecucion - cantidadCaracteresCampoDatos - cantidadCaracteresNoCadenaOriginal +
        //                (((CantidadCamposDatosNumeros - (numeroLeido ? 1 : 0)) >= 0 ? (CantidadCamposDatosNumeros - (numeroLeido ? 1 : 0)) : 0) * cadenaFormatoDatos.Length + (numeroLeido ? cadenaFormatoNumero.Length : 0));

        //            if (!TextoDiferente && cantidadCaracteresCadenaOriginal - 1 == TextoBusquedaNumero.Length)
        //            {
        //                cantidadCaracteresCampoDatos = 0;
        //                cantidadTotalCaracteresEjecucion = 0;
        //                cantidadCaracteresNoCadenaOriginal = 0;
        //                cantidadDigitosCampo = 0;
        //                cantidadCaracteresCadenaOriginal = 0;

        //                numeroLeido = false;
        //                esCampoNumero = false;
        //                esCampoDatos = false;

        //                cantidad++;
        //                if (cantidad < cantidadVeces)
        //                {
        //                    TextoBusquedaNumero_EnEjecucion = string.Empty;
        //                    CadenaValorNumero = string.Empty;
        //                    ValorNumeroEncontrado = 0;
        //                    tamañoDatosTexto.Clear();
        //                    CantidadCamposDatosNumeros = 0;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //            if (TextoDiferente)
        //            {
        //                cantidadCaracteresCampoDatos = 0;
        //                cantidadTotalCaracteresEjecucion = 0;
        //                cantidadCaracteresNoCadenaOriginal = 0;
        //                cantidadDigitosCampo = 0;
        //                cantidadCaracteresCadenaOriginal = 0;

        //                numeroLeido = false;
        //                esCampoNumero = false;
        //                esCampoDatos = false;

        //                TextoBusquedaNumero_EnEjecucion = string.Empty;
        //                TextoDiferente = false;
        //            }
        //        }

        //        if (LineaVaciaEncontrada)
        //        {
        //            if (Encoding.Default.GetChars(textoLeido)[0] == '\n')
        //                LineaVaciaEncontrada = false;
        //        }


        //        if (archivo.lector.Position == archivo.lector.Length)
        //        {
        //            break;
        //        }

        //    } while (true);

        //    TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Replace("\0", string.Empty);

        //    if (TextoBusquedaNumero_EnEjecucion.Length > 0 && TextoBusquedaNumero_EnEjecucion[TextoBusquedaNumero_EnEjecucion.Length - 1] == '\n')
        //        TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Remove(TextoBusquedaNumero_EnEjecucion.Length - 1);

        //    if (TextoBusquedaNumero.Length > 0 && TextoBusquedaNumero[TextoBusquedaNumero.Length - 1] == '\n')
        //        TextoBusquedaNumero = TextoBusquedaNumero.Remove(TextoBusquedaNumero.Length - 1);
        //}

        public void CompararProcesarStrings(ref string cadenaDatos, string cadenaOriginal, List<int> cantidadesCaracteres,
            bool procesoQuitadoCaracteres)
        {
            if (cantidadesCaracteres.Count == 0) return;

            int indiceCaracter = 0;
            int indiceCantidad = 0;
            int cantidadCaracteres = cantidadesCaracteres[indiceCantidad];
            //bool cadenaEncontrada = false;
            //bool caracteresQuitados = false;

            while (true)
            {
                if (cantidadCaracteres > 0)
                {
                    if (cadenaDatos[indiceCaracter] != cadenaOriginal[indiceCaracter]) //|| cadenaEncontrada)
                    {
                        //if (!cadenaEncontrada)
                        //{
                        if (indiceCaracter + cadenaFormatoDatos.Length <= cadenaOriginal.Length &&
                            indiceCaracter + cadenaFormatoNumero.Length <= cadenaOriginal.Length)
                        {
                            if (cadenaOriginal.Substring(indiceCaracter, cadenaFormatoDatos.Length).ToLower().Equals(cadenaFormatoDatos))
                            {
                                cadenaDatos = cadenaDatos.Remove(indiceCaracter, cantidadCaracteres);
                                cadenaDatos = cadenaDatos.Insert(indiceCaracter, cadenaFormatoDatos);

                                //cadenaEncontrada = true;
                                indiceCaracter += cadenaFormatoDatos.Length - 1;

                                indiceCantidad++;
                                if (indiceCantidad < cantidadesCaracteres.Count)
                                    cantidadCaracteres = cantidadesCaracteres[indiceCantidad];
                            }
                            else if (cadenaOriginal.Substring(indiceCaracter, cadenaFormatoNumero.Length).ToLower().Equals(cadenaFormatoNumero))
                            {
                                //while (char.IsLetterOrDigit(cadenaDatos[indiceCaracter]) |
                                //        char.IsPunctuation(cadenaDatos[indiceCaracter]) |
                                //        char.IsWhiteSpace(cadenaDatos[indiceCaracter]))
                                //{
                                //    cadenaDatos = cadenaDatos.Remove(indiceCaracter, 1);
                                //    if (indiceCaracter >= cadenaDatos.Length)
                                //        break;
                                //}
                                cadenaDatos = cadenaDatos.Remove(indiceCaracter, cantidadCaracteres);
                                cadenaDatos = cadenaDatos.Insert(indiceCaracter, cadenaFormatoNumero);

                                //cadenaEncontrada = true;
                                indiceCaracter += cadenaFormatoNumero.Length - 1;

                                indiceCantidad++;
                                if (indiceCantidad < cantidadesCaracteres.Count)
                                    cantidadCaracteres = cantidadesCaracteres[indiceCantidad];
                            }
                        }
                        //}
                        //else
                        //{
                        //    if (indiceCaracter + cadenaFormatoDatos.Length <= cadenaOriginal.Length &&
                        //        indiceCaracter + cadenaFormatoNumero.Length <= cadenaOriginal.Length)
                        //    {
                        //        if (cadenaOriginal.Substring(indiceCaracter, cadenaFormatoDatos.Length).ToLower().Equals(cadenaFormatoDatos))
                        //        {
                        //            //if (!caracteresQuitados)
                        //            //{
                        //            //    cadenaDatos = cadenaDatos.Remove(indiceCaracter, cantidadCaracteres);
                        //            //    caracteresQuitados = true;
                        //            //}
                        //            cadenaDatos = cadenaDatos.Insert(indiceCaracter, cadenaFormatoDatos);
                        //            indiceCaracter += cadenaFormatoDatos.Length - 1;
                        //        }
                        //        else if (cadenaOriginal.Substring(indiceCaracter, cadenaFormatoNumero.Length).ToLower().Equals(cadenaFormatoNumero))
                        //        {
                        //            //if (!caracteresQuitados)
                        //            //{
                        //            //    cadenaDatos = cadenaDatos.Remove(indiceCaracter, cantidadCaracteres);
                        //            //    caracteresQuitados = true;
                        //            //}
                        //            cadenaDatos = cadenaDatos.Insert(indiceCaracter, cadenaFormatoNumero);
                        //            indiceCaracter += cadenaFormatoNumero.Length - 1;
                        //        }
                        //        else
                        //        {
                        //            cadenaEncontrada = false;
                        //            //caracteresQuitados = false;

                        //            indiceCantidad++;
                        //            if (indiceCantidad < cantidadesCaracteres.Count)
                        //                cantidadCaracteres = cantidadesCaracteres[indiceCantidad];
                        //        }
                        //    }
                        //    else
                        //    {
                        //        cadenaEncontrada = false;
                        //    }
                        //}
                    }
                    //else
                    //{
                    //    cadenaEncontrada = false;
                    //    //caracteresQuitados = false;
                    //}

                    indiceCaracter++;
                    if (indiceCaracter >= cadenaDatos.Length - 1 |
                        indiceCaracter >= cadenaOriginal.Length - 1)
                    {
                        if (!procesoQuitadoCaracteres)
                        {
                            if (cadenaOriginal.Substring(indiceCaracter).Length >= cadenaFormatoDatos.Length && 
                                cadenaOriginal.Substring(indiceCaracter, cadenaFormatoDatos.Length).ToLower().Equals(cadenaFormatoDatos))
                            {
                                cadenaDatos = cadenaDatos.Remove(indiceCaracter);
                                cadenaDatos += cadenaFormatoDatos;
                            }
                            else if (cadenaOriginal.Substring(indiceCaracter).Length >= cadenaFormatoNumero.Length &&
                                cadenaOriginal.Substring(indiceCaracter, cadenaFormatoNumero.Length).ToLower().Equals(cadenaFormatoNumero))
                            {
                                cadenaDatos = cadenaDatos.Remove(indiceCaracter);
                                cadenaDatos += cadenaFormatoNumero;
                            }
                        }

                        break;
                    }


                }
                else
                {
                    indiceCantidad++;
                    if (indiceCantidad < cantidadesCaracteres.Count)
                        cantidadCaracteres = cantidadesCaracteres[indiceCantidad];

                    if (indiceCantidad == cantidadesCaracteres.Count)
                        break;

                    continue;
                }

                //if (indiceCantidad == cantidadesCaracteres.Count)
                //    break;
            }
        }

        public bool ProcesarTextoBusqueda(ElementoOrigenDatosEjecucion archivo, bool NVeces)
        {
            TextoBusquedaNumero_Proceso = QuitarLineasVacias(TextoBusquedaNumero.ToString().ToLower());
            TextoBusquedaNumero_Proceso = TextoBusquedaNumero_Proceso.Replace("\\t", "\t");
            TextoBusquedaNumero_Proceso = TextoBusquedaNumero_Proceso.Replace("\\n", "\n");

            TextoBusquedaNumero_Proceso = TextoBusquedaNumero_Proceso.Replace(" ", string.Empty);
            if (TextoBusquedaNumero_Proceso.Trim().Equals(string.Empty)) return false;

            Encoding codificacion;
            if (!string.IsNullOrEmpty(Codificacion))
                codificacion = Encoding.GetEncoding(Codificacion);
            else
                codificacion = Encoding.Default;

            OpcionFinBusquedaTexto_Archivos finalizacionNveces_Conjunto = OpcionFinBusquedaTexto_Archivos.Ninguna;

            int cantidadVeces;
            if (NVeces)
            {
                cantidadVeces = NumeroVecesBusquedaNumero;
                finalizacionNveces_Conjunto = FinalizacionBusqueda;
            }
            else
            {
                cantidadVeces = 1;
                finalizacionNveces_Conjunto = OpcionFinBusquedaTexto_Archivos.EncontrarNveces;
            }

            var TextoBusquedaNumero_Proceso_Condicion = TextoBusquedaNumero_Proceso;
            TextoBusquedaNumero_Proceso_Condicion = TextoBusquedaNumero_Proceso_Condicion.Replace("\0", string.Empty).Replace("\r", string.Empty)
                .Replace("\t", string.Empty).Replace("\n", string.Empty);

            bool sinTextoFijoFinal = false;

            if (TextoBusquedaNumero_Proceso_Condicion.Substring(TextoBusquedaNumero_Proceso_Condicion.Length - cadenaFormatoNumero.Length).ToLower().Equals(cadenaFormatoNumero) |
                TextoBusquedaNumero_Proceso_Condicion.Substring(TextoBusquedaNumero_Proceso_Condicion.Length - cadenaFormatoDatos.Length).ToLower().Equals(cadenaFormatoDatos) |
                TextoBusquedaNumero_Proceso_Condicion.Substring(TextoBusquedaNumero_Proceso_Condicion.Length - cadenaFormatoTextos.Length).ToLower().Equals(cadenaFormatoTextos))
                sinTextoFijoFinal = true;

            TextoBusquedaNumero_Proceso = TextoBusquedaNumero_Proceso.Replace(cadenaFormatoTextos, cadenaFormatoDatos.ToUpper());

            if(TextoBusquedaNumero_Proceso.ToLower().Contains(cadenaFormatoDatos + cadenaFormatoDatos) |
                TextoBusquedaNumero_Proceso.ToLower().Contains(cadenaFormatoDatos + cadenaFormatoNumero) |
                TextoBusquedaNumero_Proceso.ToLower().Contains(cadenaFormatoNumero + cadenaFormatoDatos))
                throw new Exception("La búsqueda no debe contener campos de datos, cadenas de texto o la variable número juntos sin al menos 1 caracter entre ellos.");

            //List<int> indicesIconosJuntos = new List<int>();
            //if (!TextoBusquedaNumero_Proceso.Contains(cadenaFormatoNumero))
            //    throw new Exception("La búsqueda debe especificar la ubicación del registro.");
            //TextoBusquedaNumero_Proceso = AgregarCaracteresEspeciales_IconosJuntos(TextoBusquedaNumero_Proceso, ref indicesIconosJuntos);

            int cantidadCaracteresCampoDatos = 0;
            int indiceCaracteresCampoDatos = -1;
            int cantidadTotalCaracteresEjecucion = 0;
            int cantidadTotalCaracteresEjecucion_ConEspacios = 0;
            int cantidadDigitosCampo = 0;
            int indiceInicialTextoInformacion = -1;
            string CadenaVerificaVacia = string.Empty;
            //bool sumarCaracter = false;

            bool numeroLeido = false;
            bool esCampoNumero = false;
            bool esCampoDatos = false;
            bool esCampoTextos = false;
            bool TextoDiferente = false;
            bool esFinalTextoRegistro = false;
            bool esFinalTextoRegistro_Numero = false;
            bool agregarFila = false;
            bool caracterActual_NoDigito = false;
            int cantidadCaracteres_Iguales = 0;

            bool LineaVacia_Archivo = false;
            bool InicioLineaVacia_Archivo = false;

            bool ContinuarNveces = true;

            string CadenaValorNumero = string.Empty;
           
            string textoLeido_URL = string.Empty;

            int cantidad = 0;

            tamañoDatosTexto.Add(new List<int>());

            //if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
            //    lectorArchivo = new StreamReader(((ElementoArchivoOrigenDatosEjecucion)archivo).lector, codificacion);

            if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
            {
                if (((ElementoInternetOrigenDatosEjecucion)archivo).MismaLecturaArchivo)
                    IndiceProcesamientoTexto = ((ElementoInternetOrigenDatosEjecucion)archivo).IndiceProcesamientoTexto;
            }

            string ultimoCaracter = string.Empty;
            char caracterLeido = '\0';

            //if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
            //{
            //    caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
            //    TextoBusquedaNumero_EnEjecucion_ConEspacios += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
            //    cantidadTotalCaracteresEjecucion_ConEspacios++;

            //    bytesCorridos = codificacion.GetByteCount(new char[] { caracterLeido });
            //}

            do
            {

                if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                {                    

                    if (ultimoCaracter == string.Empty)
                    {
                        //((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Read(textoLeido, 0, 1);
                        caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
                        ((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresCorridos++;
                        //((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresADescartar++;
                        TextoBusquedaNumero_EnEjecucion_ConEspacios += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
                        cantidadTotalCaracteresEjecucion_ConEspacios++;

                        //while (((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Position < ((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Length)
                        while (!((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.EndOfStream)
                        {
                            if (caracterLeido == ' ')
                            {
                                caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
                                ((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresCorridos++;
                                //((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresADescartar++;
                                //((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Read(textoLeido, 0, 1);
                                TextoBusquedaNumero_EnEjecucion_ConEspacios += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
                                cantidadTotalCaracteresEjecucion_ConEspacios++;
                            }
                            else
                                break;
                        }

                        //if (((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Position == ((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Length)
                        if(((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.EndOfStream)
                        {
                            if (caracterLeido != '\n')
                                ultimoCaracter = "\n";
                        }
                    }
                    else
                    {
                        //textoLeido = codificacion.GetBytes(ultimoCaracter);
                        //caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
                        //bytesCorridos = codificacion.GetByteCount(new char[] { caracterLeido });
                        
                        TextoBusquedaNumero_EnEjecucion_ConEspacios += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
                        cantidadTotalCaracteresEjecucion_ConEspacios++;

                        ultimoCaracter = string.Empty;
                    }
                }
                else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                {
                    if (ultimoCaracter == string.Empty)
                    {                        
                        textoLeido_URL = ((ElementoInternetOrigenDatosEjecucion)archivo).ContenidoTexto.Substring(IndiceProcesamientoTexto, 1).ToLower();
                        IndiceProcesamientoTexto++;

                        TextoBusquedaNumero_EnEjecucion_ConEspacios += textoLeido_URL;
                        cantidadTotalCaracteresEjecucion_ConEspacios++;

                        while (IndiceProcesamientoTexto < ((ElementoInternetOrigenDatosEjecucion)archivo).ContenidoTexto.Length)
                        {
                            if (textoLeido_URL == " ")
                            {                                
                                textoLeido_URL = ((ElementoInternetOrigenDatosEjecucion)archivo).ContenidoTexto.Substring(IndiceProcesamientoTexto, 1).ToLower();
                                IndiceProcesamientoTexto++;

                                TextoBusquedaNumero_EnEjecucion_ConEspacios += textoLeido_URL;
                                cantidadTotalCaracteresEjecucion_ConEspacios++;
                            }
                            else
                                break;
                        }

                        if (IndiceProcesamientoTexto == ((ElementoInternetOrigenDatosEjecucion)archivo).ContenidoTexto.Length)
                        {
                            if (textoLeido_URL != "\n")
                                ultimoCaracter = "\n";
                        }
                    }
                    else
                    {
                        IndiceProcesamientoTexto++;
                        textoLeido_URL = ultimoCaracter;

                        TextoBusquedaNumero_EnEjecucion_ConEspacios += textoLeido_URL;
                        cantidadTotalCaracteresEjecucion_ConEspacios++;

                        ultimoCaracter = string.Empty;
                    }
                }


                //bool LineaVaciaEncontrada = false;

                if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                {
                    if (caracterLeido == '\r' |
                        caracterLeido == '\t')
                    {
                        //caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
                        //bytesCorridos = codificacion.GetByteCount(new char[] { caracterLeido });
                        continue;
                    }
                }
                else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                {
                    if (textoLeido_URL == "\r")
                        continue;

                    if (textoLeido_URL == "\t")
                        continue;
                }

                //if (TextoBusquedaNumero_EnEjecucion_ConEspacios.Length == 1)
                //{
                //    if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                //    {
                //        if (caracterLeido == '\n')
                //        {
                //            //caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
                //            //bytesCorridos = codificacion.GetByteCount(new char[] { caracterLeido });
                //            continue;
                //        }
                //    }
                //    else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                //    {
                //        if (textoLeido_URL == "\n")
                //            continue;
                //    }
                //}
                //if (Encoding.Default.GetChars(textoLeido)[0] == '\n')
                //{
                //    if (CadenaVerificaVacia.Equals(string.Empty))
                //    {
                //        LineaVaciaEncontrada = true;

                //        if (esCampoDatos | esCampoNumero)
                //        {
                //            sumarCaracter = true;
                //        }
                //    }

                //    CadenaVerificaVacia = string.Empty;
                //}
                //else
                //{
                //    CadenaVerificaVacia += Encoding.Default.GetString(textoLeido);
                //}

                //if (!LineaVaciaEncontrada)
                //{
                if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                    TextoBusquedaNumero_EnEjecucion += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
                else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                    TextoBusquedaNumero_EnEjecucion += textoLeido_URL.ToLower();

                if (TextoBusquedaNumero_EnEjecucion.Last() == '\n')
                {
                    if (InicioLineaVacia_Archivo)
                    {
                        InicioLineaVacia_Archivo = false;
                        LineaVacia_Archivo = true;
                    }
                    else
                        InicioLineaVacia_Archivo = true;
                }                    
                else
                {
                    InicioLineaVacia_Archivo = false;
                }
                    

                if (!LineaVacia_Archivo &&
                    TextoBusquedaNumero_EnEjecucion != "\n")
                {
                    cantidadTotalCaracteresEjecucion++;

                    indiceCaracteresCampoDatos = cantidadTotalCaracteresEjecucion - cantidadCaracteresCampoDatos +
                        (((CantidadCamposDatosNumeros - (numeroLeido ? 1 : 0)) >= 0 ? (CantidadCamposDatosNumeros - (numeroLeido ? 1 : 0)) : 0) * cadenaFormatoDatos.Length +
                        (numeroLeido ? cadenaFormatoNumero.Length : 0)) - 1;

                    int cantidadCaracteresSaltados = 0;

                    int cantidadCaracteresNoCampoDatos = CalcularCantidadCaracteresNoCampoDatos(indiceCaracteresCampoDatos, esCampoDatos, esCampoNumero, ref cantidadCaracteresSaltados);

                    if (sinTextoFijoFinal)
                    {
                        if (esCampoNumero && (!(TextoBusquedaNumero_EnEjecucion.Last() == ',' | TextoBusquedaNumero_EnEjecucion.Last() == '.' |
                    char.IsDigit(TextoBusquedaNumero_EnEjecucion.Last())) || indiceCaracteresCampoDatos == TextoBusquedaNumero_Proceso.Length))
                            caracterActual_NoDigito = true;
                    }

                    if (!esCampoNumero & !esCampoDatos)
                    {
                        if (indiceCaracteresCampoDatos + cadenaFormatoDatos.Length <= TextoBusquedaNumero_Proceso.Length &
                            indiceCaracteresCampoDatos + cadenaFormatoNumero.Length <= TextoBusquedaNumero_Proceso.Length)
                        {
                            if (string.Equals(TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos, cadenaFormatoDatos.Length).ToLower(), cadenaFormatoDatos))
                            {
                                esCampoDatos = true;
                                if (TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos, cadenaFormatoDatos.Length).Equals(cadenaFormatoDatos.ToUpper()))
                                {
                                    esCampoTextos = true;
                                    indiceInicialTextoInformacion = cantidadTotalCaracteresEjecucion_ConEspacios - 1;
                                }
                            }
                            else if (string.Equals(TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos, cadenaFormatoNumero.Length).ToLower(), cadenaFormatoNumero))
                            {
                                esCampoNumero = true;
                            }
                        }
                    }
                    else
                    {
                        string subCadenaInicialTextoBusquedaNumero = string.Empty;

                        if (esCampoNumero | esCampoDatos)
                        {

                            if (indiceCaracteresCampoDatos + cantidadCaracteresSaltados + cantidadCaracteresNoCampoDatos + 1 >= TextoBusquedaNumero_Proceso.Length)
                            {
                                subCadenaInicialTextoBusquedaNumero = TextoBusquedaNumero_Proceso.Replace(cadenaFormatoDatos.ToUpper(), cadenaFormatoDatos.ToLower());
                            }
                            else
                                subCadenaInicialTextoBusquedaNumero = TextoBusquedaNumero_Proceso.Replace(cadenaFormatoDatos.ToUpper(), cadenaFormatoDatos.ToLower())
                                    .Remove(indiceCaracteresCampoDatos + cantidadCaracteresSaltados + cantidadCaracteresNoCampoDatos);
                        }
                        //if (esCampoDatos)
                        //{

                        //    if (indiceCaracteresCampoDatos + cantidadCaracteresSaltados + cantidadCaracteresNoCampoDatos + 1 >= TextoBusquedaNumero_Proceso.Length)
                        //    {
                        //        subCadenaInicialTextoBusquedaNumero = TextoBusquedaNumero_Proceso;
                        //    }
                        //    else
                        //        subCadenaInicialTextoBusquedaNumero = TextoBusquedaNumero_Proceso.Remove(indiceCaracteresCampoDatos + cantidadCaracteresSaltados + cantidadCaracteresNoCampoDatos);

                        //}


                        List<int> tamañoDatosTextoMetodo = new List<int>();
                        foreach (var linea in tamañoDatosTexto)
                            tamañoDatosTextoMetodo.AddRange(linea.ToList());

                        tamañoDatosTextoMetodo.Add(cantidadDigitosCampo + 1 - cantidadCaracteresNoCampoDatos);

                        string subCadenaInicialTextoBusquedaNumero_Ejecucion = QuitarCaracteres_Ejecucion(TextoBusquedaNumero_EnEjecucion, subCadenaInicialTextoBusquedaNumero, tamañoDatosTextoMetodo);

                        if ((!sinTextoFijoFinal || ((sinTextoFijoFinal && esCampoNumero && caracterActual_NoDigito) ||
                            (sinTextoFijoFinal && esCampoDatos))) && 
                            string.Equals(subCadenaInicialTextoBusquedaNumero, (TextoBusquedaNumero_EnEjecucion.Last() == '\n' && esCampoNumero && sinTextoFijoFinal &&
                            subCadenaInicialTextoBusquedaNumero.Last() != '\n') ? 
                            subCadenaInicialTextoBusquedaNumero_Ejecucion.Remove(subCadenaInicialTextoBusquedaNumero_Ejecucion.Length - 1) : 
                            subCadenaInicialTextoBusquedaNumero_Ejecucion))
                        {
                            if (caracterActual_NoDigito) caracterActual_NoDigito = false;

                            if (sinTextoFijoFinal) esFinalTextoRegistro_Numero = true;
                            esFinalTextoRegistro = true;
                        }

                    }

                    if (esCampoDatos | esCampoNumero)
                    {
                        if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                            CadenaValorNumero += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
                        else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                            CadenaValorNumero += textoLeido_URL;

                        cantidadDigitosCampo++;
                        cantidadCaracteresCampoDatos++;
                    }
                    
                    if(!(esCampoDatos | esCampoNumero))
                    {
                        if (indiceCaracteresCampoDatos <= TextoBusquedaNumero_Proceso.Length - 1)
                        {
                            if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                            {
                                if (TextoBusquedaNumero_Proceso[indiceCaracteresCampoDatos].ToString() != caracterLeido.ToString().ToLower())//codificacion.GetChars(textoLeido)[0].ToString().ToLower())
                                {
                                    TextoDiferente = true;
                                }
                            }
                            else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                            {
                                if (TextoBusquedaNumero_Proceso[indiceCaracteresCampoDatos].ToString() != textoLeido_URL)
                                {
                                    TextoDiferente = true;
                                }
                            }
                        }
                    }
                    else if ((archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion)) &&
                        esCampoNumero && (!char.IsDigit(caracterLeido) || (char.IsDigit(caracterLeido) &&
                        CadenaValorNumero.Any(i => !char.IsDigit(i) && i != '.' && i != ',')))
                        && caracterLeido != '.' && caracterLeido != ',')
                    {
                        if (indiceCaracteresCampoDatos + cadenaFormatoNumero.Length + cantidadCaracteres_Iguales <= TextoBusquedaNumero_Proceso.Length - 1)
                        {
                            if (TextoBusquedaNumero_Proceso[indiceCaracteresCampoDatos + cadenaFormatoNumero.Length + cantidadCaracteres_Iguales].ToString() != caracterLeido.ToString().ToLower())//codificacion.GetChars(textoLeido)[0].ToString().ToLower())
                            {
                                TextoDiferente = true;
                                cantidadCaracteres_Iguales = 0;
                            }   
                            else
                            {
                                cantidadCaracteres_Iguales++;
                            }
                        }
                    }
                    else if(archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion) &&
                        esCampoNumero && (!char.IsDigit(textoLeido_URL[0]) || (char.IsDigit(textoLeido_URL[0]) &&
                        CadenaValorNumero.Any(i => !char.IsDigit(i) && i != '.' && i != ','))) 
                        && textoLeido_URL[0] != '.' && textoLeido_URL[0] != ',')
                    {
                        if (indiceCaracteresCampoDatos + cadenaFormatoNumero.Length + cantidadCaracteres_Iguales <= TextoBusquedaNumero_Proceso.Length - 1)
                        {
                            if (TextoBusquedaNumero_Proceso[indiceCaracteresCampoDatos + cadenaFormatoNumero.Length + cantidadCaracteres_Iguales].ToString() != textoLeido_URL)
                            {
                                TextoDiferente = true;
                                cantidadCaracteres_Iguales = 0;
                            }
                            else
                            {
                                cantidadCaracteres_Iguales++;
                            }
                        }
                    }

                    if ((esFinalTextoRegistro) & (esCampoDatos | esCampoNumero))
                    {
                        bool filtro = EvaluarFiltros_Condiciones(archivo, TextoDiferente, TextoBusquedaNumero_EnEjecucion_ConEspacios);
                        int cantidadCaracteresNumero = 0;

                        int cantidadCaracteresCampo = cantidadDigitosCampo - cantidadCaracteresNoCampoDatos;
                        
                        if (esCampoNumero)
                        {
                            string CadenaNumero = ObtenerNumeroCadena(CadenaValorNumero);//, ref cantidadCaracteresNoNumero);
                            NumberFormatInfo formato = ProcesarSeparadores(archivo.ConfiguracionSeparadores);

                            bool numeroConvertido = double.TryParse(CadenaNumero, System.Globalization.NumberStyles.Number, formato, out ValorNumeroEncontrado);
                            cantidadCaracteresNumero = CadenaNumero.Length;

                            if (sinTextoFijoFinal && filtro && numeroConvertido)
                            {
                                if (cantidadCaracteresCampo - cantidadCaracteresNumero > 0)
                                {
                                    TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Remove(
                                        TextoBusquedaNumero_EnEjecucion.Length - (cantidadCaracteresCampo - cantidadCaracteresNumero - 1));

                                    int cantidadCaracteresQuitados = 0;
                                    for (int indice = TextoBusquedaNumero_EnEjecucion_ConEspacios.Length - 1; 
                                        indice >= 0; indice--)
                                    {
                                        if (cantidadCaracteresQuitados == cantidadCaracteresCampo - cantidadCaracteresNumero - 1)
                                        {
                                            break;
                                        }

                                        if (TextoBusquedaNumero_EnEjecucion_ConEspacios[indice] != ' ')
                                        {
                                            TextoBusquedaNumero_EnEjecucion_ConEspacios = TextoBusquedaNumero_EnEjecucion_ConEspacios.Remove(indice, 1);
                                            cantidadCaracteresQuitados++;
                                        }
                                    }
                                }
                                
                                cantidadCaracteresCampo = cantidadCaracteresNumero;
                            }
                        }

                        if (esCampoTextos)
                        {
                            if (filtro)
                            {
                                TextosInformacion.Add(AgregarEspaciosTextosInformacion(indiceInicialTextoInformacion, CadenaValorNumero.Substring(0, cantidadCaracteresCampo)));
                                CantidadTextosInformacionEncontrados++;
                                TextosInformacionEncontrados.Add(TextosInformacion.Last());
                                //CantidadCaracteresADescartar = cantidadDigitosCampo;
                                //((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresADescartar += cantidadCaracteresCampoDatos;
                            }

                            esCampoTextos = false;
                        }

                        tamañoDatosTexto.Last().Add(cantidadCaracteresCampo);

                        cantidadDigitosCampo = 0;

                        if (esCampoNumero & filtro & numeroLeido == false)
                        {
                            numeroLeido = true;
                            NumeroEncontrado = true;
                            CantidadNumerosEncontrados++;
                            NumerosEncontrados.Add(ValorNumeroEncontrado);
                        }

                        CadenaValorNumero = string.Empty;

                        esCampoDatos = false;
                        esCampoNumero = false;

                        CantidadCamposDatosNumeros++;
                        cantidadCaracteresCampoDatos -= cantidadCaracteresNoCampoDatos;

                        cantidadCaracteresNoCampoDatos = 0;

                        esFinalTextoRegistro = false;

                        if (agregarFila)
                        {
                            tamañoDatosTexto.Add(new List<int>());
                            agregarFila = false;
                        }

                    }

                    bool finalizar = false;
                    bool otroTexto = false;

                    if (TextoDiferente)
                    {
                        if (EvaluarFinalizacion_Condiciones(archivo, TextoDiferente, 
                            TextoBusquedaNumero_EnEjecucion_ConEspacios.Remove(TextoBusquedaNumero_EnEjecucion_ConEspacios.Length - 1)))
                        {
                            finalizar = true;
                            if (!numeroLeido) ContinuarNveces = false;
                            
                        }

                        if (CantidadCamposDatosNumeros == 0)
                        {
                            //if (esCampoNumero && !char.IsDigit(caracterLeido))
                            //{
                            //    otroTexto = true;
                            //}
                            //else
                            //{
                            //    cantidadTotalCaracteresEjecucion = 0;
                            //    cantidadTotalCaracteresEjecucion_ConEspacios = 0;
                            //    numeroLeido = false;
                            //    TextoBusquedaNumero_EnEjecucion = string.Empty;
                            //    TextoBusquedaNumero_EnEjecucion_ConEspacios = string.Empty;
                            //}

                            otroTexto = true;

                            TextoDiferente = false;
                        }
                    }
                    else
                    {
                        if (indiceCaracteresCampoDatos == TextoBusquedaNumero_Proceso.Length)
                        {
                            if (EvaluarFinalizacion_Condiciones(archivo, TextoDiferente, 
                                TextoBusquedaNumero_EnEjecucion_ConEspacios.Remove(TextoBusquedaNumero_EnEjecucion_ConEspacios.Length - 1)))
                            {
                                finalizar = true;
                                if (!numeroLeido) ContinuarNveces = false;
                                
                            }
                        }
                        else
                        {
                            if (sinTextoFijoFinal &&
                                NumeroEncontrado &&
                                esFinalTextoRegistro_Numero &&
                                !TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos).Contains(cadenaFormatoDatos) &&
                                !TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos).Contains(cadenaFormatoDatos.ToUpper()))
                            {
                                esFinalTextoRegistro_Numero = false;
                                finalizar = true;

                                if (EvaluarFinalizacion_Condiciones(archivo, TextoDiferente,
                                    TextoBusquedaNumero_EnEjecucion_ConEspacios.Remove(TextoBusquedaNumero_EnEjecucion_ConEspacios.Length - 1)))
                                {
                                    LecturaTerminada = true;
                                }
                            }
                        }
                    }

                    if (indiceCaracteresCampoDatos == TextoBusquedaNumero_Proceso.Length)
                    {
                        cantidad++;
                        if (cantidad < cantidadVeces)
                        {
                            if (!finalizar)
                            {
                                otroTexto = true;
                            }
                        }
                        else
                        {
                            if (finalizacionNveces_Conjunto == OpcionFinBusquedaTexto_Archivos.EncontrarNveces)
                            {
                                finalizar = true;
                            }
                            else
                            {
                                otroTexto = true;
                            }
                        }
                    }

                    if (otroTexto)
                    {
                        cantidadDigitosCampo = 0;
                        numeroLeido = false;
                        esCampoDatos = false;
                        esCampoNumero = false;
                        cantidadCaracteresCampoDatos = 0;
                        CadenaValorNumero = string.Empty;
                        esFinalTextoRegistro = false;
                        cantidadCaracteresNoCampoDatos = 0;
                        cantidadTotalCaracteresEjecucion = 0;
                        cantidadTotalCaracteresEjecucion_ConEspacios = 0;

                        LimpiarVariables();
                        tamañoDatosTexto.Add(new List<int>());
                    }

                    if (finalizar)
                    {
                        if (TextoBusquedaNumero_EnEjecucion.Length > 0)
                        {

                            TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Remove(TextoBusquedaNumero_EnEjecucion.Length - 1);
                            TextoBusquedaNumero_EnEjecucion_ConEspacios = TextoBusquedaNumero_EnEjecucion_ConEspacios.Remove(TextoBusquedaNumero_EnEjecucion_ConEspacios.Length - 1);
                            
                        }

                        if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                        {
                            //if (((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Position == ((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Length)
                            if(((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.EndOfStream)
                            {
                                LecturaTerminada = true;
                                if (!numeroLeido) ContinuarNveces = false;

                                break;
                            }
                            else
                            {
                                if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                                {
                                    ((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Close();
                                    ((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Close();

                                    ((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo = 
                                        new StreamReader(new FileStream(((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Name, FileMode.OpenOrCreate), false);
                                    ((ElementoArchivoOrigenDatosEjecucion)archivo).lector = 
                                        (FileStream)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.BaseStream;

                                    ((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresDescartados++;

                                    for (int lecturas = 1; lecturas <= ((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresCorridos - 
                                        ((ElementoArchivoOrigenDatosEjecucion)archivo).CaracteresDescartados; lecturas++)
                                        ((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();

                                    //caracteresCorridos = 0;
                                    //((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Seek(-bytesCorridos, SeekOrigin.Current);

                                        //((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.DiscardBufferedData();

                                        //long posicion = ((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Position;

                                        //((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo = new StreamReader(((ElementoArchivoOrigenDatosEjecucion)archivo).lector, codificacion, detectEncodingFromByteOrderMarks: false);

                                        //((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.BaseStream.Position = posicion;
                                }
                            }
                        }

                        if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                        {
                            if (IndiceProcesamientoTexto == ((ElementoInternetOrigenDatosEjecucion)archivo).ContenidoTexto.Length - 1)
                            {
                                LecturaTerminada = true;
                                if (!numeroLeido) ContinuarNveces = false;

                                break;
                            }
                            else
                                ((ElementoInternetOrigenDatosEjecucion)archivo).IndiceProcesamientoTexto = IndiceProcesamientoTexto;
                        }

                        break;
                    }

                    if (agregarFila)
                    {
                        //if (!(esCampoDatos | esCampoNumero))
                        //{
                            tamañoDatosTexto.Add(new List<int>());
                            agregarFila = false;
                        //}
                    }

                    string caracter = string.Empty;

                    if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                    {
                        caracter = caracterLeido.ToString().ToLower();//codificacion.GetChars(textoLeido)[0].ToString().ToLower();
                    }
                    else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                    {
                        caracter = textoLeido_URL;
                    }

                    if (caracter == "\n")
                    {
                        //if (sumarCaracter)
                        //{
                        //    cantidadTotalCaracteresEjecucion++;
                        //    cantidadDigitosCampo++;
                        //    cantidadCaracteresCampoDatos++;
                        //    sumarCaracter = false;
                        //}

                        if (TextoBusquedaNumero_EnEjecucion.Length > 0)
                        {
                            int cantidadLineas = TextoBusquedaNumero_EnEjecucion.Count((i) => i == '\n');
                            if (cantidadLineas == tamañoDatosTexto.Count)
                                agregarFila = true;
                        }

                        //if (verificarSiAgregarLinea)
                        //{
                        //if (!TextoBusquedaNumero_EnEjecucion.Equals(string.Empty))
                        //{
                        //if (!(esCampoDatos | esCampoNumero))
                        //{
                        //    //tamañoDatosTexto.Add(new List<int>());
                        //    //agregarLinea = false;
                        //    if (agregarLinea)
                        //    {
                        //        if(tamañoDatosTexto.Count == 0 | (tamañoDatosTexto.Count > 0 && TextoBusquedaNumero_EnEjecucion.Length == 0))
                        //            tamañoDatosTexto.Add(new List<int>());
                        //        //agregarLinea = false;
                        //    }
                        //    cantidadCamposDatosLinea = 0;
                        //}
                        //}
                        //}

                    }
                    //}

                    //if (LineaVaciaEncontrada)
                    //{
                    //    if (Encoding.Default.GetChars(textoLeido)[0] == '\n')
                    //        LineaVaciaEncontrada = false;
                    //}
                }
                else
                {
                    LineaVacia_Archivo = false;
                    //cantidadTotalCaracteresEjecucion = 0;
                    //cantidadTotalCaracteresEjecucion_ConEspacios = 0;
                    //numeroLeido = false;
                    TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Remove(TextoBusquedaNumero_EnEjecucion.Length - 1);
                    //TextoBusquedaNumero_EnEjecucion_ConEspacios = string.Empty;
                    //TextoDiferente = false;
                }

                //if (ultimoCaracter == string.Empty)
                //{
                    if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion))
                    {
                        //if (((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Position == ((ElementoArchivoOrigenDatosEjecucion)archivo).lector.Length)
                        if(((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.EndOfStream)
                        {
                            LecturaTerminada = true;
                            if (!numeroLeido) ContinuarNveces = false;

                            break;
                        }
                    }
                    else if (archivo.GetType() == typeof(ElementoInternetOrigenDatosEjecucion))
                    {
                        if (IndiceProcesamientoTexto >= ((ElementoInternetOrigenDatosEjecucion)archivo).ContenidoTexto.Length - 1)
                        {
                            LecturaTerminada = true;
                            if (!numeroLeido) ContinuarNveces = false;

                            break;
                        }
                    }
                //}

                //if (archivo.GetType() == typeof(ElementoArchivoOrigenDatosEjecucion) &&
                //    !((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.EndOfStream)
                //{
                //    caracterLeido = (char)((ElementoArchivoOrigenDatosEjecucion)archivo).lectorArchivo.Read();
                //    bytesCorridos = codificacion.GetByteCount(new char[] { caracterLeido });

                //    if (caracterLeido != '\r')
                //    {
                //        TextoBusquedaNumero_EnEjecucion_ConEspacios += caracterLeido.ToString().ToLower(); //codificacion.GetString(textoLeido).ToLower();
                //        cantidadTotalCaracteresEjecucion_ConEspacios++;
                //    }
                //}

            } while (true);
                      

            TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Replace("\0", string.Empty);

                //TextoBusquedaNumero_EnEjecucion_ConEspacios = TextoBusquedaNumero_EnEjecucion_ConEspacios.Replace("\0", string.Empty);
                //TextoBusquedaNumero_EnEjecucion = TextoBusquedaNumero_EnEjecucion.Replace("\n", string.Empty);

                //if (TextoBusquedaNumero.Length > 0 && TextoBusquedaNumero[TextoBusquedaNumero.Length - 1] == '\n')
                //    TextoBusquedaNumero = TextoBusquedaNumero.Remove(TextoBusquedaNumero.Length - 1);
            return ContinuarNveces;
        }

        private string QuitarLineasVacias(string cadena)
        {
            while (cadena.Contains("\n\n"))
            {
                for (int indice = 0; indice < cadena.Length; indice++)
                {
                    if(indice < cadena.Length - 1 &&
                        cadena[indice] == '\n' &&
                        cadena[indice + 1] == '\n')
                    {
                        cadena = cadena.Remove(indice + 1, 1);
                    }
                }
            }

            return cadena;
        }

        private bool EvaluarFinalizacion_Condiciones(ElementoOrigenDatosEjecucion archivo, bool TextoBusquedaDistinto,
            string TextoBusquedaEncontrado)
        {
            bool valorCondicion = false;

            switch (FinalizacionBusqueda)
            {
                //case OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo:
                //    if (!LecturaTerminada)
                //        return false;

                //    break;

                //case OpcionFinBusquedaTexto_Archivos.EncontrarNveces:
                //    if (nVecesConjunto == busqueda.NumeroVecesConjuntoBusquedas)
                //        continuarConjunto = false;
                //    else
                //    {
                //        nVecesConjunto++;
                //    }

                //    break;

                case OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida:
                case OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida:

                    bool valorCondiciones = false;

                    if (CondicionesTextoBusqueda != null)
                    {
                        CondicionesTextoBusqueda.LimpiarBusqueda();
                        valorCondiciones = CondicionesTextoBusqueda.EvaluarCondiciones(archivo, TextoBusquedaDistinto, TextoBusquedaEncontrado);
                    }

                    if (FinalizacionBusqueda == OpcionFinBusquedaTexto_Archivos.EncontrarHastaCoincida)
                    {
                        if (valorCondiciones) valorCondicion = true;
                    }
                    else if (FinalizacionBusqueda == OpcionFinBusquedaTexto_Archivos.EncontrarMientrasCoincida)
                    {
                        if (!valorCondiciones) valorCondicion = true;
                    }

                    break;
            }

            return valorCondicion;
            
        }

        private bool EvaluarFiltros_Condiciones(ElementoOrigenDatosEjecucion archivo, bool TextoBusquedaDistinto,
            string TextoBusquedaEncontrado)
        {
            bool valorCondiciones = true;

            if(CondicionesTextoBusqueda_Filtros != null)
                valorCondiciones = CondicionesTextoBusqueda_Filtros.EvaluarCondiciones(archivo, TextoBusquedaDistinto, TextoBusquedaEncontrado);

            return valorCondiciones;

        }

        private string ObtenerNumeroCadena(string cadena) //ref int cantidadNoCaracteresNumero = 0)
        {
            string numero = string.Empty;
            bool inicioObtencionNumero = false;

            for (int indice = 0; indice <= cadena.Length-1; indice++)
            {
                if (!inicioObtencionNumero)
                {
                    if (cadena[indice] == ',' | cadena[indice] == '.' |
                        char.IsDigit(cadena[indice]))
                    {
                        inicioObtencionNumero = true;
                        numero += cadena[indice].ToString();
                    }
                }
                else
                {

                    if (cadena[indice] == ',' | cadena[indice] == '.' |
                        char.IsDigit(cadena[indice]))
                    {
                        numero += cadena[indice].ToString();
                    }
                    else
                    {
                        //cantidadNoCaracteresNumero = cadena.Length - indice;
                        break;
                    }
                }
            }

            if (numero.Last() == '.' |
                numero.Last() == ',')
                numero = numero.Remove(numero.Length - 1);

            return numero;
        }

        private int CalcularCantidadCaracteresNoCampoDatos(int indiceCaracteresCampoDatos,
            bool esCampoDatos, bool esCampoNumero, ref int cantidadCaracteresSaltados)
        {
            if (esCampoDatos | esCampoNumero)
            {
                int CantidadCaracteresCadena = 0;

                if (esCampoNumero)
                {
                    CantidadCaracteresCadena = cadenaFormatoNumero.Length;
                }

                if (esCampoDatos)
                {
                    CantidadCaracteresCadena = cadenaFormatoDatos.Length;
                }

                cantidadCaracteresSaltados = CantidadCaracteresCadena;

                if (TextoBusquedaNumero_Proceso.ToLower().Contains(cadenaFormatoDatos))
                {
                    int cantidadSubCadenaNoCampoDatos = 0;
                    for (int indice = indiceCaracteresCampoDatos + CantidadCaracteresCadena; ; indice++)
                    {
                        if (esCampoNumero)
                        {
                            if (indice + cadenaFormatoNumero.Length <= TextoBusquedaNumero_Proceso.Length)
                            {
                                if (TextoBusquedaNumero_Proceso.Substring(indice, CantidadCaracteresCadena).ToLower().Equals(cadenaFormatoDatos))
                                {
                                    if (cantidadSubCadenaNoCampoDatos == 0)
                                    {
                                        indice += CantidadCaracteresCadena - 1;
                                        cantidadCaracteresSaltados += CantidadCaracteresCadena;
                                        CantidadCamposDatosNumeros++;
                                        continue;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (indice >= TextoBusquedaNumero_Proceso.Length - 1)
                            {
                                cantidadSubCadenaNoCampoDatos++;
                                break;
                            }
                        }
                        else if (esCampoDatos)
                        {
                            if (indice + cadenaFormatoDatos.Length <= TextoBusquedaNumero_Proceso.Length)
                            {
                                if (TextoBusquedaNumero_Proceso.Substring(indice, CantidadCaracteresCadena).ToLower().Equals(cadenaFormatoDatos) |
                                    TextoBusquedaNumero_Proceso.Substring(indice, CantidadCaracteresCadena).ToLower().Equals(cadenaFormatoNumero))
                                {
                                    if (cantidadSubCadenaNoCampoDatos == 0)
                                    {
                                        indice += CantidadCaracteresCadena - 1;
                                        cantidadCaracteresSaltados += CantidadCaracteresCadena;
                                        CantidadCamposDatosNumeros++;
                                        continue;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (indice >= TextoBusquedaNumero_Proceso.Length - 1)
                            {
                                cantidadSubCadenaNoCampoDatos++;
                                break;
                            }
                        }

                        cantidadSubCadenaNoCampoDatos++;

                    }

                    return cantidadSubCadenaNoCampoDatos;
                    //return TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos + CantidadCaracteresCadena, cantidadSubCadenaNoCampoDatos).Length;
                }
                else
                    return TextoBusquedaNumero_Proceso.Substring(indiceCaracteresCampoDatos + CantidadCaracteresCadena).Length;
            }
            else
                return 0;
        }

        private string QuitarCaracteres_Ejecucion(string TextoEjecucion, string TextoOriginal, List<int> CantidadesCaracteres)
        {
            char[] CaracteresTextoProcesamiento = new char[TextoEjecucion.Length];
            TextoEjecucion.CopyTo(0, CaracteresTextoProcesamiento, 0, TextoEjecucion.Length);

            List<int> CantidadesCaracteresMetodo = new List<int>();
            foreach (var cantidad in CantidadesCaracteres)
                CantidadesCaracteresMetodo.Add(cantidad);

            string TextoProcesamiento = new string(CaracteresTextoProcesamiento);
            CompararProcesarStrings(ref TextoProcesamiento, TextoOriginal, CantidadesCaracteresMetodo, true);
            return TextoProcesamiento;
        }

        public void LimpiarVariables()
        {
            CantidadCamposDatosNumeros = 0;
            TextoBusquedaNumero_EnEjecucion = string.Empty;
            TextoBusquedaNumero_EnEjecucion_ConEspacios = string.Empty;

            tamañoDatosTexto.Clear();  
        }

        public void ReiniciarBusqueda()
        {
            LimpiarVariables();

            BusquedaIniciada = false;
            TextosInformacion.Clear();
            IndiceProcesamientoTexto = 0;
            BusquedasConjuntoEjecutadas = 0;
            NumerosEncontrados.Clear();
            CantidadNumerosEncontrados = 0;
            TextosInformacionEncontrados.Clear();
            CantidadTextosInformacionEncontrados = 0;
            TextosInformacionEncontrados_NombresCantidades.Clear();
            LecturaTerminada = false;
            BusquedaValida = false;
        }

        private NumberFormatInfo ProcesarSeparadores(TipoDefinicionSeparadores separadores)
        {
            NumberFormatInfo formato = new CultureInfo("es-CL").NumberFormat;

            switch (separadores)
            {
                case TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesComa:
                    formato.NumberDecimalSeparator = ",";
                    formato.NumberGroupSeparator = ".";
                    break;

                case TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesComa:
                    formato.NumberDecimalSeparator = ",";
                    //formato.NumberGroupSeparator = "";
                    break;

                case TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesPunto:
                    formato.NumberDecimalSeparator = ".";
                    //formato.NumberGroupSeparator = "";
                    break;

                case TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesNinguno:
                    //formato.NumberDecimalSeparator = "";
                    formato.NumberGroupSeparator = ".";
                    break;

                case TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesNinguno:
                    //formato.NumberDecimalSeparator = "";
                    formato.NumberGroupSeparator = ",";
                    break;

                case TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesPunto:
                    formato.NumberDecimalSeparator = ".";
                    formato.NumberGroupSeparator = ",";
                    break;
            }

            return formato;
        }

        private string AgregarEspaciosTextosInformacion(int indiceInicial, string textoInformacion)
        {
            string cadenaResultado = string.Empty;
            int indiceTextoInformacion = 0;

            for(int indice = indiceInicial; indice <= TextoBusquedaNumero_EnEjecucion_ConEspacios.Length-1; indice++)
            {
                if (TextoBusquedaNumero_EnEjecucion_ConEspacios[indice] == '\t' |
                    TextoBusquedaNumero_EnEjecucion_ConEspacios[indice] == ' ')
                {
                    cadenaResultado += TextoBusquedaNumero_EnEjecucion_ConEspacios[indice].ToString();
                }
                else if (indiceTextoInformacion <= textoInformacion.Length - 1 && 
                    TextoBusquedaNumero_EnEjecucion_ConEspacios[indice] == textoInformacion[indiceTextoInformacion])
                {
                    cadenaResultado += TextoBusquedaNumero_EnEjecucion_ConEspacios[indice].ToString();
                    indiceTextoInformacion++;
                }
                else if(indiceTextoInformacion <= textoInformacion.Length - 1 && 
                    TextoBusquedaNumero_EnEjecucion_ConEspacios[indice] != textoInformacion[indiceTextoInformacion])
                {
                    break;
                }
            }

            return cadenaResultado;
        }
    }
}
