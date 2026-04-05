using Microsoft.VisualBasic.Logging;
using PlanMath_para_Excel;
using PlanMath_para_Word;
using ProcessCalc.Controles;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.OrigenesDatos;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig;
using static PlanMath_para_Excel.Entradas;
using static PlanMath_para_Word.Entradas;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.Ejecuciones.ToolTips;

namespace ProcessCalc.Entidades
{
    public class Entrada
    {
        public string ID { get; set; }
        public string Nombre { get; set; }

        public string NombreCombo
        {
            get
            {
                return Nombre;
            }
        }
        public List<string> Textos { get; set; }
        public TipoEntrada Tipo { get; set; }
        public double NumeroFijo { get; set; }
        public TipoOpcionNumeroEntrada TipoOpcionNumero { get; set; }
        public TipoOrigenDatos TipoOrigenDatos { get; set; }
        public OpcionBusquedaNumero OpcionBusquedaNumero { get; set; }                        
        public TipoOpcionConjuntoNumerosEntrada TipoOpcionConjuntoNumeros { get; set; }
        public TipoOpcionTextosInformacionEntrada TipoOpcionTextosInformacion { get; set; }
        public List<EntidadNumero> ConjuntoNumerosFijo { get; set; }
        public List<FilaTextosInformacion_Entrada> ConjuntoTextosInformacionFijos { get; set; }             
        public BusquedaTextoArchivo BusquedaNumero { get; set; }
        public List<BusquedaTextoArchivo> BusquedasConjuntoNumeros { get; set; }
        public List<BusquedaTextoArchivo> BusquedasTextosInformacion { get; set; }
        public bool MismaLecturaBusquedasArchivo { get; set; }
        public DiseñoOperacion ElementoSalidaCalculoAnterior { get; set; }
        public List<ConfiguracionArchivoEntrada> ListaArchivos { get; set; }
        public List<ConfiguracionURLEntrada> ListaURLs { get; set; }
        public List<Entrada_Desde_Excel> ParametrosExcel { get; set; }        
        public CredencialesFTP CredencialesFTP { get; set; }
        public TipoDefinicionSeparadores ConfiguracionSeparadores { get; set; }        
        public bool ConfigSeleccionarArchivoURL { get; set; }
        public bool UsarURL_Office { get; set; }
        public bool UtilizarCantidadNumeros { get; set; }
        public OpcionCantidadNumerosEntrada OpcionCantidadNumeros { get; set; }
        public List<string> TextosInformacionFijos { get; set; }
        public TipoElementoOperacion OperacionInterna { get; set; }
        public List<string> TextosInformacion_OperacionInterna { get; set; }
        public DefinicionTextoNombresCantidades DefinicionOpcionesNombresCantidades { get; set; }
        public bool AgregarCantidad_ComoTextoInformacion { get; set; }
        public bool ComprobarConfirmarCantidades_Ejecucion { get; set; }
        public bool UtilizarTextosInformacionUnicos { get; set; }        
        public List<ConjuntoTextosInformacion_Digitacion> ConjuntoTextosInformacion_Digitacion { get; set; }
        public bool UtilizarPrimerConjunto_Automaticamente { get; set; }
        public List<OpcionListaNumeros_Digitacion> OpcionesListaNumeros { get; set; }
        public bool SeleccionarNumeroDeOpciones { get; set; }
        public bool FijarCantidadNumerosDigitacion { get; set; }
        public int CantidadNumerosDigitacion { get; set; }
        public bool DigitarEjecucionCantidadNumerosDigitacion { get; set; }
        public bool FijarCantidadTextosDigitacion { get; set; }
        public bool FijarCantidadFilasTextosDigitacion { get; set; }
        public int CantidadTextosDigitacion { get; set; }
        public int CantidadFilasTextosDigitacion { get; set; }
        public bool DigitarEjecucionCantidadTextosDigitacion { get; set; }
        public bool DigitarEjecucionCantidadFilasTextosDigitacion { get; set; }
        public TipoOpcionElementosFijosOperacionInverso OpcionElementosFijosInverso { get; set; }
        public ConfiguracionSeleccionNumeros_Entrada SeleccionNumeros { get; set; }
        public ConfiguracionListaOpcionesNumeros_Predefinidos ConfigListaNumeros { get; set; }
        public bool UtilizarSoloTextosPredefinidos { get; set; }

        [IgnoreDataMember]
        public ElementoEntradaEjecucion EntradaProcesada { get; 
            set; }
        [IgnoreDataMember]
        public List<OpcionListaNumeros_Digitacion> OpcionesListaNumeros_Ejecucion { get; set; }
        [IgnoreDataMember]
        public bool NombreEditado { get; set; }
        [AtributoNoComparar]
        public DiseñoCalculo CalculoDiseñoAsociado { get; set; }
        public bool EjecutarDeFormaGeneral {  get; set; }
        public TipoFormatoArchivoEntrada TipoFormatoArchivo_Entrada {  get; set; }
        public Entrada_Desde_Word EntradaWord { get; set; }
        public ListaCantidadesDigitadas CantidadesDigitadas { get; set; }
        public ConfiguracionLimpiarDatos ConfigLimpiarDatos_OperacionInterna { get; set; }
        public ConfiguracionRedondearCantidades ConfigRedondeo_OperacionInterna { get; set; }
        public List<ListaTextosDigitados> TextosDigitados { get; set; }
        public bool EntradaArchivoManual { get; set; }
        [IgnoreDataMember]
        public List<EjecucionExternaConfiguracion_Entrada> EjecucionesExternas_SubElementos_Config { get; set; }
        public bool ConCambios_ToolTips { get; set; }                
        public Entrada(string nombre)
        {
            this.Nombre = nombre;
            this.Textos = new List<string>();
            this.Tipo = TipoEntrada.Ninguno;
            ConjuntoNumerosFijo = new List<EntidadNumero>();
            ConjuntoTextosInformacionFijos = new List<FilaTextosInformacion_Entrada>();
            BusquedasConjuntoNumeros = new List<BusquedaTextoArchivo>();
            BusquedaNumero = new BusquedaTextoArchivo();
            MismaLecturaBusquedasArchivo = true;
            ParametrosExcel = new List<Entrada_Desde_Excel>();
            ListaArchivos = new List<ConfiguracionArchivoEntrada>();
            ListaURLs = new List<ConfiguracionURLEntrada>();
            ListaURLs.Add(new ConfiguracionURLEntrada());
            ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesComa;
            OpcionCantidadNumeros = OpcionCantidadNumerosEntrada.AgregarCantidadNumeros;
            TextosInformacionFijos = new List<string>();
            OperacionInterna = TipoElementoOperacion.Ninguna;
            TextosInformacion_OperacionInterna = new List<string>();
            BusquedasTextosInformacion = new List<BusquedaTextoArchivo>();
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();
            ConjuntoTextosInformacion_Digitacion = new List<ConjuntoTextosInformacion_Digitacion>();
            UtilizarPrimerConjunto_Automaticamente = true;
            OpcionesListaNumeros = new List<OpcionListaNumeros_Digitacion>();
            CantidadNumerosDigitacion = 1;
            OpcionElementosFijosInverso = TipoOpcionElementosFijosOperacionInverso.InversoSumaResta;
            CantidadTextosDigitacion = 1;
            CantidadFilasTextosDigitacion = 1;
            SeleccionNumeros = new ConfiguracionSeleccionNumeros_Entrada();
            ConfigListaNumeros = new ConfiguracionListaOpcionesNumeros_Predefinidos();
            TipoFormatoArchivo_Entrada = TipoFormatoArchivoEntrada.Ninguno;
            CantidadesDigitadas = new ListaCantidadesDigitadas();
            EjecucionesExternas_SubElementos_Config = new List<EjecucionExternaConfiguracion_Entrada>();
            TextosDigitados = new List<ListaTextosDigitados>();
        }

        public Entrada()
        {
            Textos = new List<string>();
            this.Tipo = TipoEntrada.Ninguno;
            ConjuntoNumerosFijo = new List<EntidadNumero>();
            ConjuntoTextosInformacionFijos = new List<FilaTextosInformacion_Entrada>();
            BusquedasConjuntoNumeros = new List<BusquedaTextoArchivo>();
            BusquedaNumero = new BusquedaTextoArchivo();
            MismaLecturaBusquedasArchivo = true;
            ParametrosExcel = new List<Entrada_Desde_Excel>();
            ListaArchivos = new List<ConfiguracionArchivoEntrada>();
            ListaURLs = new List<ConfiguracionURLEntrada>();
            ListaURLs.Add(new ConfiguracionURLEntrada());
            ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesComa;
            OpcionCantidadNumeros = OpcionCantidadNumerosEntrada.AgregarCantidadNumeros;
            TextosInformacionFijos = new List<string>();
            OperacionInterna = TipoElementoOperacion.Ninguna;
            TextosInformacion_OperacionInterna = new List<string>();
            BusquedasTextosInformacion = new List<BusquedaTextoArchivo>();
            DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();
            ConjuntoTextosInformacion_Digitacion = new List<ConjuntoTextosInformacion_Digitacion>();
            UtilizarPrimerConjunto_Automaticamente = true;
            OpcionesListaNumeros = new List<OpcionListaNumeros_Digitacion>();
            CantidadNumerosDigitacion = 1;
            OpcionElementosFijosInverso = TipoOpcionElementosFijosOperacionInverso.InversoSumaResta;
            CantidadTextosDigitacion = 1;
            CantidadFilasTextosDigitacion = 1;
            SeleccionNumeros = new ConfiguracionSeleccionNumeros_Entrada();
            ConfigListaNumeros = new ConfiguracionListaOpcionesNumeros_Predefinidos();
            TipoFormatoArchivo_Entrada = TipoFormatoArchivoEntrada.Ninguno;
            CantidadesDigitadas = new ListaCantidadesDigitadas();
            EjecucionesExternas_SubElementos_Config = new List<EjecucionExternaConfiguracion_Entrada>();
            TextosDigitados = new List<ListaTextosDigitados>();
        }

        public bool VerificarCambios(Entrada UltimoEstadoEntrada, ComparadorObjetos ComparadorObjetos)
        {
            bool hayCambios = false;

            ComparadorObjetos._paresVisitados.Clear();

            if (!ComparadorObjetos.CompararObjetos(this, UltimoEstadoEntrada))
                hayCambios = true;

            //DataContractSerializer objetoGuardado = new DataContractSerializer(typeof(Entrada), new DataContractSerializerSettings() { PreserveObjectReferences = true });

            //MemoryStream flujoEntradaGuardada = new MemoryStream();
            //objetoGuardado.WriteObject(flujoEntradaGuardada, UltimoEstadoEntrada);

            //MemoryStream flujoEntrada = new MemoryStream();
            //objetoGuardado.WriteObject(flujoEntrada, this);

            //if (!Calculo.VerificarIgualdad(flujoEntrada.ToArray(), flujoEntradaGuardada.ToArray()))
            //    hayCambios = true;

            //flujoEntrada.Close();
            //flujoEntradaGuardada.Close();

            return hayCambios;
        }

        public static Entrada_Desde_Excel ObtenerEntrada_Excel_Archivo(string ruta)
        {
            XmlReader guarda = null;
            try
            {
                guarda = XmlReader.Create(ruta);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Entrada_Desde_Excel), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                Entrada_Desde_Excel nuevaEntrada = (Entrada_Desde_Excel)objeto.ReadObject(guarda);
                guarda.Close();

                return nuevaEntrada;
            }
            catch(Exception)
            {
                guarda.Close();
                return null;
            }
        }

        public static Entrada_Desde_Word ObtenerEntrada_Word_Archivo(string ruta)
        {
            XmlReader guarda = null;
            try
            {
                guarda = XmlReader.Create(ruta);
                DataContractSerializer objeto = new DataContractSerializer(typeof(Entrada_Desde_Word), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                Entrada_Desde_Word nuevaEntrada = (Entrada_Desde_Word)objeto.ReadObject(guarda);
                guarda.Close();

                return nuevaEntrada;
            }
            catch (Exception e)
            {
                guarda.Close();
                return null;
            }
        }

        public static List<Entrada> AgregarEntrada_Desde_Excel_Calculo(Entrada_Desde_Excel nuevaEntrada, DiseñoCalculo calculoEncontrado)
        {
            var entradasEncontradas = (from Entrada E in calculoEncontrado.ListaEntradas where E.Nombre == nuevaEntrada.Nombre & 
                                       E.TipoOrigenDatos == TipoOrigenDatos.Excel select E).ToList();


            //if (entradaEncontrada != null)
            //{
            //    //if (entradaEncontrada.Tipo == TipoEntrada.Numero && entradaEncontrada.RutaArchivoEntrada != nuevaEntrada.Libro)
            //    //{
            //    //    entradaEncontrada = null;
            //    //}
            //    //else if (entradaEncontrada.Tipo == TipoEntrada.ConjuntoNumeros && entradaEncontrada.RutaArchivoConjuntoNumerosEntrada != nuevaEntrada.Libro)
            //    //{
            //    //    entradaEncontrada = null;
            //    //}
            //    //else if (entradaEncontrada.Tipo == TipoEntrada.TextosInformacion && entradaEncontrada.RutaArchivoConjuntoTextosEntrada != nuevaEntrada.Libro)
            //    //{
            //    //    entradaEncontrada = null;
            //    //}

            //    if (nuevaEntrada.Nombre != entradaEncontrada.Nombre)
            //        entradaEncontrada = null;
            //}
            if (entradasEncontradas != null && 
                entradasEncontradas.Any())
            {
                foreach (var entradaEncontrada in entradasEncontradas)
                {
                    entradaEncontrada.TipoOrigenDatos = TipoOrigenDatos.Excel;

                    entradaEncontrada.ListaArchivos.Clear();
                    entradaEncontrada.ListaArchivos.Add(new ConfiguracionArchivoEntrada());

                    if (nuevaEntrada.ReemplazarCeldas)
                    {
                        entradaEncontrada.ParametrosExcel.Clear();
                    }
                    entradaEncontrada.ParametrosExcel.Add(nuevaEntrada);

                    foreach(var item in entradaEncontrada.ParametrosExcel)
                    {
                        item.Libro = nuevaEntrada.Libro;
                        item.ArchivoPlanMath = nuevaEntrada.ArchivoPlanMath;
                        item.EntradaManual = nuevaEntrada.EntradaManual;
                        item.EsRutaLocal_Excel = nuevaEntrada.EsRutaLocal_Excel;
                        item.URLOffice_Original = nuevaEntrada.URLOffice_Original;
                        item.ReemplazarCeldas = nuevaEntrada.ReemplazarCeldas;
                    }
                    
                    if (entradaEncontrada.ParametrosExcel.Count == 1 &&
                        nuevaEntrada.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.Numero)
                        entradaEncontrada.Tipo = TipoEntrada.Numero;
                    else if (nuevaEntrada.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.ConjuntoNumeros)
                        entradaEncontrada.Tipo = TipoEntrada.ConjuntoNumeros;
                    else if (nuevaEntrada.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.TextosInformacion)
                        entradaEncontrada.Tipo = TipoEntrada.TextosInformacion;

                    if (entradaEncontrada.Tipo == TipoEntrada.Numero)
                    {
                        entradaEncontrada.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;
                        entradaEncontrada.ListaArchivos.Last().RutaArchivoEntrada = nuevaEntrada.Libro;
                        entradaEncontrada.ListaArchivos.Last().NombreArchivoEntrada = nuevaEntrada.Libro.Substring(nuevaEntrada.Libro.LastIndexOf(nuevaEntrada.EsRutaLocal_Excel ? "\\" : "/") + 1);
                    }
                    else if (entradaEncontrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    {
                        entradaEncontrada.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;
                        entradaEncontrada.ListaArchivos.Last().RutaArchivoConjuntoNumerosEntrada = nuevaEntrada.Libro;
                        entradaEncontrada.ListaArchivos.Last().NombreArchivoEntrada = nuevaEntrada.Libro.Substring(nuevaEntrada.Libro.LastIndexOf(nuevaEntrada.EsRutaLocal_Excel ? "\\" : "/") + 1);
                    }
                    else if (entradaEncontrada.Tipo == TipoEntrada.TextosInformacion)
                    {
                        entradaEncontrada.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;
                        entradaEncontrada.ListaArchivos.Last().RutaArchivoConjuntoTextosEntrada = nuevaEntrada.Libro;
                        entradaEncontrada.ListaArchivos.Last().NombreArchivoEntrada = nuevaEntrada.Libro.Substring(nuevaEntrada.Libro.LastIndexOf(nuevaEntrada.EsRutaLocal_Excel ? "\\" : "/") + 1);
                    }

                    entradaEncontrada.UsarURL_Office = !nuevaEntrada.EsRutaLocal_Excel;
                    entradaEncontrada.EntradaArchivoManual = nuevaEntrada.EntradaManual;

                    entradaEncontrada.ConCambios_ToolTips = true;
                }

                return entradasEncontradas;
            }
            else
            {
                Entrada nuevaEntradaCalculo = new Entrada();
                nuevaEntradaCalculo.ID = App.GenerarID_Elemento();
                nuevaEntradaCalculo.Nombre = nuevaEntrada.Nombre;

                if (nuevaEntrada.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.Numero)
                    nuevaEntradaCalculo.Tipo = TipoEntrada.Numero;
                else if (nuevaEntrada.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.ConjuntoNumeros)
                    nuevaEntradaCalculo.Tipo = TipoEntrada.ConjuntoNumeros;
                else if (nuevaEntrada.Tipo == PlanMath_para_Excel.Entradas.TipoEntrada.TextosInformacion)
                    nuevaEntradaCalculo.Tipo = TipoEntrada.TextosInformacion;

                nuevaEntradaCalculo.TipoOrigenDatos = TipoOrigenDatos.Excel;
                nuevaEntradaCalculo.ListaArchivos.Add(new ConfiguracionArchivoEntrada());

                if (nuevaEntradaCalculo.Tipo == TipoEntrada.Numero)
                {
                    nuevaEntradaCalculo.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;
                    nuevaEntradaCalculo.ListaArchivos.FirstOrDefault().RutaArchivoEntrada = nuevaEntrada.Libro;
                    nuevaEntradaCalculo.ListaArchivos.FirstOrDefault().NombreArchivoEntrada = nuevaEntrada.Libro.Substring(nuevaEntrada.Libro.LastIndexOf(nuevaEntrada.EsRutaLocal_Excel ? "\\" : "/") + 1);
                }
                else if (nuevaEntradaCalculo.Tipo == TipoEntrada.ConjuntoNumeros)
                {
                    nuevaEntradaCalculo.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;
                    nuevaEntradaCalculo.ListaArchivos.FirstOrDefault().RutaArchivoConjuntoNumerosEntrada = nuevaEntrada.Libro;
                    nuevaEntradaCalculo.ListaArchivos.FirstOrDefault().NombreArchivoEntrada = nuevaEntrada.Libro.Substring(nuevaEntrada.Libro.LastIndexOf(nuevaEntrada.EsRutaLocal_Excel ? "\\" : "/") + 1);
                }
                else if (nuevaEntradaCalculo.Tipo == TipoEntrada.TextosInformacion)
                {
                    nuevaEntradaCalculo.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;
                    nuevaEntradaCalculo.ListaArchivos.FirstOrDefault().RutaArchivoConjuntoTextosEntrada = nuevaEntrada.Libro;
                    nuevaEntradaCalculo.ListaArchivos.FirstOrDefault().NombreArchivoEntrada = nuevaEntrada.Libro.Substring(nuevaEntrada.Libro.LastIndexOf(nuevaEntrada.EsRutaLocal_Excel ? "\\" : "/") + 1);
                }

                nuevaEntradaCalculo.UsarURL_Office = !nuevaEntrada.EsRutaLocal_Excel;
                nuevaEntradaCalculo.EntradaArchivoManual = nuevaEntrada.EntradaManual;

                nuevaEntradaCalculo.ParametrosExcel.Add(nuevaEntrada);
                nuevaEntradaCalculo.CalculoDiseñoAsociado = calculoEncontrado;
                calculoEncontrado.ListaEntradas.Add(nuevaEntradaCalculo);

                if (calculoEncontrado.EsEntradasArchivo)
                {
                    nuevaEntradaCalculo.EjecutarDeFormaGeneral = true;
                    calculoEncontrado.AgregarEntrada_CalculoEntradas(nuevaEntradaCalculo);
                }

                nuevaEntradaCalculo.ConCambios_ToolTips = true;

                return new List<Entrada>() { nuevaEntradaCalculo };
            }
        }

        public static List<Entrada> AgregarEntrada_Desde_Word_Calculo(Entrada_Desde_Word nuevaEntrada, DiseñoCalculo calculoEncontrado)
        {
            var entradasEncontradas = (from Entrada E in calculoEncontrado.ListaEntradas
                                       where E.Nombre == nuevaEntrada.Nombre select E).ToList();

            if (entradasEncontradas != null &&
                entradasEncontradas.Any())
            {
                foreach (var entradaEncontrada in entradasEncontradas)
                {
                    entradaEncontrada.TipoOrigenDatos = TipoOrigenDatos.Archivo;
                    entradaEncontrada.TipoFormatoArchivo_Entrada = TipoFormatoArchivoEntrada.Word;
                    entradaEncontrada.EntradaWord = nuevaEntrada;

                    if (nuevaEntrada.Tipo == PlanMath_para_Word.Entradas.TipoEntrada.Numero)
                    {
                        entradaEncontrada.Tipo = TipoEntrada.Numero;
                        entradaEncontrada.ListaArchivos.Clear();
                    }
                    else if (nuevaEntrada.Tipo == PlanMath_para_Word.Entradas.TipoEntrada.ConjuntoNumeros)
                    {
                        entradaEncontrada.Tipo = TipoEntrada.ConjuntoNumeros;
                        if (nuevaEntrada.ReemplazarLecturasArchivos)
                        {
                            entradaEncontrada.ListaArchivos.Clear();
                        }
                    }
                    else if (nuevaEntrada.Tipo == PlanMath_para_Word.Entradas.TipoEntrada.TextosInformacion)
                    {
                        entradaEncontrada.Tipo = TipoEntrada.TextosInformacion;
                        if (nuevaEntrada.ReemplazarLecturasArchivos)
                        {
                            entradaEncontrada.ListaArchivos.Clear();
                        }
                    }

                    if (entradaEncontrada.Tipo == TipoEntrada.Numero)
                    {
                        entradaEncontrada.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;
                                                
                        var archivoWord = new ConfiguracionArchivoEntrada()
                        {
                            TipoArchivo = TipoArchivo.EquipoLocal,
                            RutaArchivoEntrada = nuevaEntrada.RutaDocumento,
                            URLOffice_Original = nuevaEntrada.URLOffice_Original,
                            UsarURLOffice_original = !nuevaEntrada.EsRutaLocal_Word,
                            NombreArchivoEntrada = nuevaEntrada.RutaDocumento.Substring(nuevaEntrada.RutaDocumento.LastIndexOf(nuevaEntrada.EsRutaLocal_Word ? "\\" : "/") + 1)
                        };

                        archivoWord.EntradaManual = nuevaEntrada.EntradaManual;
                        entradaEncontrada.ListaArchivos.Add(archivoWord);

                        if(nuevaEntrada.EntradaManual)
                        {
                            foreach(var item in entradaEncontrada.ListaArchivos)
                            {
                                item.EntradaManual = nuevaEntrada.EntradaManual;
                            }
                        }
                                                
                        archivoWord.ParametrosWord.AddRange(nuevaEntrada.TextosBusqueda);                        
                    }
                    else if (entradaEncontrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    {
                        entradaEncontrada.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;

                        ConfiguracionArchivoEntrada archivoWord = entradaEncontrada.ListaArchivos.Where(i => i.RutaArchivoConjuntoNumerosEntrada == nuevaEntrada.RutaDocumento).FirstOrDefault();
                        if (archivoWord == null)
                        {
                            archivoWord = new ConfiguracionArchivoEntrada()
                            {
                                TipoArchivo = TipoArchivo.EquipoLocal,
                                RutaArchivoConjuntoNumerosEntrada = nuevaEntrada.RutaDocumento,
                                URLOffice_Original = nuevaEntrada.URLOffice_Original,
                                UsarURLOffice_original = !nuevaEntrada.EsRutaLocal_Word,
                                NombreArchivoEntrada = nuevaEntrada.RutaDocumento.Substring(nuevaEntrada.RutaDocumento.LastIndexOf(nuevaEntrada.EsRutaLocal_Word ? "\\" : "/") + 1)
                            };

                            entradaEncontrada.ListaArchivos.Add(archivoWord);
                        }

                        archivoWord.EntradaManual = nuevaEntrada.EntradaManual;

                        if (nuevaEntrada.EntradaManual)
                        {
                            foreach (var item in entradaEncontrada.ListaArchivos)
                            {
                                item.EntradaManual = nuevaEntrada.EntradaManual;
                            }
                        }

                        archivoWord.ParametrosWord.AddRange(nuevaEntrada.TextosBusqueda);

                    }
                    else if (entradaEncontrada.Tipo == TipoEntrada.TextosInformacion)
                    {
                        entradaEncontrada.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;

                        ConfiguracionArchivoEntrada archivoWord = entradaEncontrada.ListaArchivos.Where(i => i.RutaArchivoConjuntoTextosEntrada == nuevaEntrada.RutaDocumento).FirstOrDefault();
                        if (archivoWord == null)
                        {
                            archivoWord = new ConfiguracionArchivoEntrada()
                            {
                                TipoArchivo = TipoArchivo.EquipoLocal,
                                RutaArchivoConjuntoTextosEntrada = nuevaEntrada.RutaDocumento,
                                URLOffice_Original = nuevaEntrada.URLOffice_Original,
                                UsarURLOffice_original = !nuevaEntrada.EsRutaLocal_Word,
                                NombreArchivoEntrada = nuevaEntrada.RutaDocumento.Substring(nuevaEntrada.RutaDocumento.LastIndexOf(nuevaEntrada.EsRutaLocal_Word ? "\\" : "/") + 1)
                            };

                            entradaEncontrada.ListaArchivos.Add(archivoWord);
                        }

                        archivoWord.EntradaManual = nuevaEntrada.EntradaManual;

                        if (nuevaEntrada.EntradaManual)
                        {
                            foreach (var item in entradaEncontrada.ListaArchivos)
                            {
                                item.EntradaManual = nuevaEntrada.EntradaManual;
                            }
                        }

                        archivoWord.ParametrosWord.AddRange(nuevaEntrada.TextosBusqueda);
                    }

                    entradaEncontrada.UsarURL_Office = !nuevaEntrada.EsRutaLocal_Word;
                    entradaEncontrada.EntradaArchivoManual = nuevaEntrada.EntradaManual;

                    entradaEncontrada.ConCambios_ToolTips = true;
                }

                return entradasEncontradas;
            }
            else
            {
                Entrada nuevaEntradaCalculo = new Entrada();
                nuevaEntradaCalculo.ID = App.GenerarID_Elemento();
                nuevaEntradaCalculo.Nombre = nuevaEntrada.Nombre;
                nuevaEntradaCalculo.EntradaWord = nuevaEntrada;

                if (nuevaEntrada.Tipo == PlanMath_para_Word.Entradas.TipoEntrada.Numero)
                    nuevaEntradaCalculo.Tipo = TipoEntrada.Numero;
                else if (nuevaEntrada.Tipo == PlanMath_para_Word.Entradas.TipoEntrada.ConjuntoNumeros)
                    nuevaEntradaCalculo.Tipo = TipoEntrada.ConjuntoNumeros;
                else if (nuevaEntrada.Tipo == PlanMath_para_Word.Entradas.TipoEntrada.TextosInformacion)
                    nuevaEntradaCalculo.Tipo = TipoEntrada.TextosInformacion;

                nuevaEntradaCalculo.TipoOrigenDatos = TipoOrigenDatos.Archivo;
                nuevaEntradaCalculo.TipoFormatoArchivo_Entrada = TipoFormatoArchivoEntrada.Word;

                if (nuevaEntradaCalculo.Tipo == TipoEntrada.Numero)
                {
                    nuevaEntradaCalculo.TipoOpcionNumero = TipoOpcionNumeroEntrada.SeObtiene;

                    var archivoWord = new ConfiguracionArchivoEntrada()
                    {
                        TipoArchivo = TipoArchivo.EquipoLocal,
                        RutaArchivoEntrada = nuevaEntrada.RutaDocumento,
                        URLOffice_Original = nuevaEntrada.URLOffice_Original,
                        UsarURLOffice_original = !nuevaEntrada.EsRutaLocal_Word,
                        NombreArchivoEntrada = nuevaEntrada.RutaDocumento.Substring(nuevaEntrada.RutaDocumento.LastIndexOf(nuevaEntrada.EsRutaLocal_Word ? "\\" : "/") + 1),
                        ParametrosWord = nuevaEntrada.TextosBusqueda
                    };

                    archivoWord.EntradaManual = nuevaEntrada.EntradaManual;
                    nuevaEntradaCalculo.ListaArchivos.Add(archivoWord);
                }
                else if (nuevaEntradaCalculo.Tipo == TipoEntrada.ConjuntoNumeros)
                {
                    nuevaEntradaCalculo.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.SeObtiene;

                    var archivoWord = new ConfiguracionArchivoEntrada()
                    {
                        TipoArchivo = TipoArchivo.EquipoLocal,
                        RutaArchivoConjuntoNumerosEntrada = nuevaEntrada.RutaDocumento,
                        URLOffice_Original = nuevaEntrada.URLOffice_Original,
                        UsarURLOffice_original = !nuevaEntrada.EsRutaLocal_Word,
                        NombreArchivoEntrada = nuevaEntrada.RutaDocumento.Substring(nuevaEntrada.RutaDocumento.LastIndexOf(nuevaEntrada.EsRutaLocal_Word ? "\\" : "/") + 1),
                        ParametrosWord = nuevaEntrada.TextosBusqueda
                    };

                    archivoWord.EntradaManual = nuevaEntrada.EntradaManual;
                    nuevaEntradaCalculo.ListaArchivos.Add(archivoWord);
                }
                else if (nuevaEntradaCalculo.Tipo == TipoEntrada.TextosInformacion)
                {
                    nuevaEntradaCalculo.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.SeObtiene;

                    var archivoWord = new ConfiguracionArchivoEntrada()
                    {
                        TipoArchivo = TipoArchivo.EquipoLocal,
                        RutaArchivoConjuntoTextosEntrada = nuevaEntrada.RutaDocumento,
                        URLOffice_Original = nuevaEntrada.URLOffice_Original,
                        UsarURLOffice_original = !nuevaEntrada.EsRutaLocal_Word,
                        NombreArchivoEntrada = nuevaEntrada.RutaDocumento.Substring(nuevaEntrada.RutaDocumento.LastIndexOf(nuevaEntrada.EsRutaLocal_Word ? "\\" : "/") + 1),
                        ParametrosWord = nuevaEntrada.TextosBusqueda
                    };

                    archivoWord.EntradaManual = nuevaEntrada.EntradaManual;
                    nuevaEntradaCalculo.ListaArchivos.Add(archivoWord);
                }

                nuevaEntradaCalculo.UsarURL_Office = !nuevaEntrada.EsRutaLocal_Word;
                nuevaEntradaCalculo.EntradaArchivoManual = nuevaEntrada.EntradaManual;

                //nuevaEntradaCalculo.ParametrosExcel.Add(nuevaEntrada);
                nuevaEntradaCalculo.CalculoDiseñoAsociado = calculoEncontrado;
                calculoEncontrado.ListaEntradas.Add(nuevaEntradaCalculo);

                if (calculoEncontrado.EsEntradasArchivo)
                {
                    nuevaEntradaCalculo.EjecutarDeFormaGeneral = true;
                    calculoEncontrado.AgregarEntrada_CalculoEntradas(nuevaEntradaCalculo);
                }

                nuevaEntradaCalculo.ConCambios_ToolTips = true;

                return new List<Entrada>() { nuevaEntradaCalculo };
            }
        }

        public void ObtenerTextosInformacion_EntradasAnteriores_Conjuntos(EjecucionCalculo ejecucion)
        {
            foreach(var itemConjunto in ConjuntoTextosInformacion_Digitacion)
            {
                if (itemConjunto.ConTextosInformacion_EntradasAnteriores)
                {
                    itemConjunto.TextosInformacion = new List<TextosInformacion_Digitacion>();

                    var entradaElemento = ejecucion.EntradasProcesadas.FirstOrDefault(i => i.EntradaRelacionada == itemConjunto.EntradaAnterior_TextosInformacion_Predefinidos);
                    var entradaEjecucion = ejecucion.ObtenerElementoEjecucion(entradaElemento);

                    if(entradaEjecucion == null)
                        entradaEjecucion = ejecucion.ObtenerElementoEjecucion_EnHistorial(entradaElemento);

                    if (entradaEjecucion != null &&
                        entradaEjecucion.GetType() == typeof(ElementoConjuntoTextosEntradaEjecucion))
                    {                        
                        foreach (var itemFila in ((ElementoConjuntoTextosEntradaEjecucion)entradaEjecucion).FilasTextosInformacion)
                        {
                            itemConjunto.TextosInformacion.AddRange((from T in itemFila.TextosInformacion select new TextosInformacion_Digitacion(string.Empty, T)).ToList());
                        }
                    }
                }
            }
        }

        public void ObtenerTextosInformacion_EntradasAnteriores_OpcionesListasNumeros(EjecucionCalculo ejecucion)
        {
            //foreach (var itemConjunto in ConjuntoTextosInformacion_Digitacion)
            //{
            OpcionesListaNumeros_Ejecucion = new List<OpcionListaNumeros_Digitacion>();

            if (ConfigListaNumeros.ConTextosInformacion_EntradasAnteriores)
            {
                var entradaElemento = ejecucion.EntradasProcesadas.FirstOrDefault(i => i.EntradaRelacionada == ConfigListaNumeros.EntradaAnterior_TextosInformacion_Predefinidos);
                var entradaEjecucion = ejecucion.ObtenerElementoEjecucion(entradaElemento);

                if (entradaEjecucion != null &&
                        entradaEjecucion.GetType() == typeof(ElementoConjuntoTextosEntradaEjecucion))
                {

                    foreach (var itemFila in ((ElementoConjuntoTextosEntradaEjecucion)entradaEjecucion).FilasTextosInformacion)
                    {
                        decimal numero = 0;
                        int numero_ = 0;

                        OpcionesListaNumeros_Ejecucion = (from T in itemFila.TextosInformacion
                                                          where decimal.TryParse(T, out numero) || int.TryParse(T, out numero_)
                                                          select new OpcionListaNumeros_Digitacion(string.Empty, T)).ToList();
                    }
                }
            }
        }
    }
}
