using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.ComponentModel;
using Microsoft.Office.Tools;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Office.Interop.Word;
using static PlanMath_para_Word.Entradas;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PlanMath_para_Word
{
    public partial class PlanMath_Word
    {
        public static InstanciaPlanMath InstanciaActual;
        public static List<InstanciaPlanMath> Instancias;
        public List<string> ArchivosPlanMath = new List<string>();
        public const string Nombre_Aplicacion = "PlanMath_para_Word";
        public const string Nombre_Aplicacion_PlanMath = "PlanMath";
        public static string RutaCarpeta_PlanMath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion_PlanMath + "\\" + Nombre_Aplicacion;
        public static string RutaArchivo_ArchivoRecientes = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\ArchivosPlanMath.dat";
        public static string RutaCarpeta_PlanMath_EnvioEntradas = RutaCarpeta_PlanMath + "\\Entradas_Enviadas";
        public static string RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion = RutaCarpeta_PlanMath + "\\Entradas_Enviadas_Ejecucion";

        public static Thread ProcesoLecturaEnviosEntradas = new Thread(Entradas.LeerEnviosEntradas);
        public static bool ProcesoTerminado_EnvioEntradas;
        public static bool ProcesoPausado_EnvioEntradas;
        public static List<EnvioEntradaDesdeEjecucion> Ejecuciones_EnvioEntradas = new List<EnvioEntradaDesdeEjecucion>();
        public static List<string> ArchivosTemporalesAEliminar = new List<string>();
        public static bool Confirmacion_ProcesoTerminado_EnvioEntradas;
        public string NombreDocumentoActivo;
        public static List<DuplaAplicacionWord_Ejecucion> AplicacionesWord = new List<DuplaAplicacionWord_Ejecucion>();
        private void PlanMath_Word_Startup(object sender, System.EventArgs e)
        {
            try
            {
                CrearCarpetas();

                Instancias = new List<InstanciaPlanMath>();
                Globals.PlanMath_Word.Application.DocumentOpen += Application_DocumentOpen;
                Globals.PlanMath_Word.Application.WindowActivate += Application_WindowActivate;
                ((Word.ApplicationEvents4_Event)Globals.PlanMath_Word.Application).NewDocument += PlanMath_Word_NewDocument;
                ((Word.ApplicationEvents4_Event)Globals.PlanMath_Word.Application).WindowSelectionChange += PlanMath_Word_WindowSelectionChange;
                //((Excel.AppEvents_Event)Globals.PlanMath_Excel.Application).SheetActivate += PlanMath_Excel_SheetActivate;
                ((Word.ApplicationEvents4_Event)Globals.PlanMath_Word.Application).DocumentBeforeSave += PlanMath_Word_DocumentBeforeSave;

                CargarListaArchivosPlanMath();
                ProcesoLecturaEnviosEntradas.Start();
            }
            catch (Exception) { }
        }

        private void PlanMath_Word_DocumentBeforeSave(Word.Document Doc, ref bool SaveAsUI, ref bool Cancel)
        {
            try
            {
                if (InstanciaActual != null)
                {
                    if (Globals.PlanMath_Word.CustomTaskPanes.Contains(InstanciaActual.panelDefinirEntradas) &&
                    InstanciaActual.panelDefinirEntradas.Visible)
                        MostrarPanel_DefinirEntradas();
                }
            }
            catch (Exception) { }
        }

        public void PlanMath_Word_WindowSelectionChange(Word.Selection Sel)
        {
            if (InstanciaActual != null &&
                    ((InstanciaActual.contenidoPanelDefinirEntradas != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.Visible) ||
                    (InstanciaActual.contenidoPanelEnviarEntradas != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.ModoManual &&
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.Visible)))
                ActivarSeleccion(Sel);
        }

        private void PlanMath_Word_NewDocument(Word.Document Doc)
        {
            AgregarInstancia(Doc);
        }

        public void Application_WindowActivate(Word.Document Doc, Word.Window Wn)
        {
            PlanMath_Word.ProcesoPausado_EnvioEntradas = true;
            Thread.Sleep(500);

            PlanMath_Word.Ejecuciones_EnvioEntradas.Clear();

            Thread.Sleep(500);
            PlanMath_Word.ProcesoPausado_EnvioEntradas = false;

            if(Globals.PlanMath_Word.Application.ActiveDocument != null)
                NombreDocumentoActivo = Globals.PlanMath_Word.Application.ActiveDocument.FullName;

            if (InstanciaActual != null &&
                InstanciaActual.contenidoPanelEnviarEntradas != null)
            {
                InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = null;
                InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";
            }

            AgregarInstancia(Doc);
            SeleccionarInstancia(Doc);
        }

        private void Application_DocumentOpen(Word.Document Doc)
        {
            Doc.Activate();
        }

        private void PlanMath_Word_Shutdown(object sender, System.EventArgs e)
        {
            GuardarListaArchivosPlanMath();

            ProcesoTerminado_EnvioEntradas = true;
            while (Confirmacion_ProcesoTerminado_EnvioEntradas == false)
            {
                Thread.Sleep(500);
            }
        }

        public void AgregarInstancia(Word.Document Doc)
        {
            try
            {
                var instancia = (from I in Instancias where I.Documento == Doc select I).FirstOrDefault();
                if (instancia == null)
                {
                    InstanciaPlanMath nuevaInstancia = new InstanciaPlanMath();
                    nuevaInstancia.Documento = Doc;
                    //nuevaInstancia.Hoja = Wb.ActiveSheet;
                    //nuevaInstancia.Celdas = Globals.PlanMath_Excel.Application.ActiveCell;
                    //nuevaInstancia.EntradaActual.Tipo = TipoEntrada.Numero;
                    Instancias.Add(nuevaInstancia);
                }
            }
            catch (Exception) { }
        }

        private void SeleccionarInstancia(Word.Document Doc)
        {
            try
            {
                var instancia = (from I in Instancias where I.Documento == Doc select I).FirstOrDefault();
                if (instancia != null)
                    InstanciaActual = instancia;
            }
            catch (Exception) { }
        }

        public static bool VerificarURL_Documento(string url)
        {
            try
            {
                Word.Application aplicacion = new Word.Application();
                aplicacion.Documents.CanCheckOut(url);

                aplicacion.Quit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Abrir_Documento(string url)
        {
            try
            {
                //string ejecutable = ObtenerRutaEjecutableWord().Result;

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "winword.exe",
                    Arguments = "\"" + url + "\"",
                    UseShellExecute = true
                };

                Process.Start(psi);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<string> ObtenerTextosBusqueda_DocumentoWord(List<TextoBusqueda_DocumentoWord> entradas, string RutaDocumento, string IDEjecucion)
        {
            Thread.Sleep(500);
            //string[] textos = new string[1];
            Word.Application aplicacion = null;
            bool aplicacionTemporal = false;

            Word.Document documento = null;

            if (Globals.PlanMath_Word == null || Globals.PlanMath_Word.Application == null)
            {
                Word.Application AplicacionWord_Ejecucion = AplicacionesWord.FirstOrDefault(i => i.IDEjecucionAsociada == IDEjecucion)?.AplicacionWord;

                if (AplicacionWord_Ejecucion == null)
                {
                    AplicacionWord_Ejecucion = new Word.Application();
                    AplicacionWord_Ejecucion.DisplayAlerts = WdAlertLevel.wdAlertsNone;

                    AplicacionesWord.Add(new DuplaAplicacionWord_Ejecucion() { AplicacionWord = AplicacionWord_Ejecucion, IDEjecucionAsociada = IDEjecucion });
                }

                aplicacion = AplicacionWord_Ejecucion;
                aplicacionTemporal = true;
            }
            else
            {
                aplicacion = Globals.PlanMath_Word.Application;
                //documento = (from Word.Document L in aplicacion.Documents where L.Name == RutaDocumento select L).FirstOrDefault();
            }

            List<string> textosObtenidos = new List<string>();

            try
            {
                documento = aplicacion.Documents.Open(RutaDocumento);
                
                foreach(var texto in entradas)
                {
                    object what = WdGoToItem.wdGoToPage;
                    object which = WdGoToDirection.wdGoToAbsolute;
                    object count = texto.NumeroPagina;
                    var range = documento.Content.GoTo(ref what, ref which, ref count);

                    object what1 = WdGoToItem.wdGoToLine;
                    object which1 = WdGoToDirection.wdGoToAbsolute;
                    object count1 = texto.NumeroFila;
                    var range1 = range.GoTo(ref what1, ref which1, ref count1);

                    range1.SetRange(texto.CaracterInicial, texto.CaracterFinal);
                    range1.Select();

                    textosObtenidos.Add(range1.Text);

                    Marshal.FinalReleaseComObject(range1);
                    range1 = null;
                }

                if (aplicacionTemporal)
                {
                    documento.Close();

                    Marshal.FinalReleaseComObject(documento);
                    documento = null;
                }
                //if (aplicacionTemporal) aplicacion.Quit();
            }
            catch (Exception e)
            {
                Thread.Sleep(3000);

                try
                {
                    if (e.HResult != -2146827284)
                    {
                        if (aplicacionTemporal)
                        {
                            if (documento != null)
                            {
                                documento.Close();
                                Marshal.FinalReleaseComObject(documento);
                                documento = null;
                            }
                        }
                    }
                    //if (aplicacionTemporal) aplicacion.Quit();
                }
                catch (Exception)
                {
                    //if (aplicacionTemporal) aplicacion.Quit();
                    //throw new Exception("Error al leer documento word '" + RutaDocumento + "'.");
                }

                //throw e;
            }

            return textosObtenidos;
        }

        private void ActivarSeleccion(Word.Selection Target)
        {
            try
            {
                
                    if (Target != null &&
                    Target.Range != null &&
                    InstanciaActual != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.Visible &&
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual != null &&
                    Target.Type != WdSelectionType.wdNoSelection &&
                    Target.Type != WdSelectionType.wdSelectionIP &&
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionandoTextos)
                    {
                        if (!EsCtrlPresionada())
                        {
                            InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.RangosSeleccionados.Clear();
                        }

                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.RangosSeleccionados.Add(Target.Range);
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionesTextos.Clear();

                        foreach (Word.Range rangoActual in InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.RangosSeleccionados)
                        {
                            SeleccionTexto seleccion = new SeleccionTexto();
                            seleccion.TextoActual = rangoActual.Text;

                            seleccion.NumeroPagina = rangoActual.Information[WdInformation.wdActiveEndPageNumber];
                            seleccion.NumeroFila = rangoActual.Information[WdInformation.wdFirstCharacterLineNumber];
                            //InstanciaActual.NumeroColumna = Target.Range.Information[WdInformation.wdFirstCharacterColumnNumber];

                            seleccion.CaracterInicial = rangoActual.Start;
                            seleccion.CaracterFinal = rangoActual.End;

                            InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionesTextos.Add(seleccion);
                        }
                    }
                    else if (InstanciaActual != null &&
                        InstanciaActual.contenidoPanelDefinirEntradas != null &&
                        InstanciaActual.contenidoPanelDefinirEntradas.Visible &&
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual != null &&
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionandoTextos)
                    {
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.RangosSeleccionados.Clear();
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionesTextos.Clear();
                    }

                    if (InstanciaActual != null &&
                        InstanciaActual.contenidoPanelDefinirEntradas != null &&
                        InstanciaActual.contenidoPanelDefinirEntradas.Visible &&
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual != null &&
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionandoTextos)
                    {
                        if (Globals.PlanMath_Word.CustomTaskPanes.Contains(InstanciaActual.panelDefinirEntradas) &&
                            InstanciaActual.panelDefinirEntradas.Visible)
                            Entradas.MostrarPanel_DefinirEntradas(InstanciaActual.contenidoPanelDefinirEntradas.ModoManual);
                    }

                if (Target != null &&
                Target.Range != null &&
                InstanciaActual != null &&
                InstanciaActual.contenidoPanelEnviarEntradas != null &&
                InstanciaActual.contenidoPanelEnviarEntradas.ModoManual &&
                InstanciaActual.contenidoPanelEnviarEntradas.Visible &&
                InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia != null &&
                Target.Type != WdSelectionType.wdNoSelection &&
                Target.Type != WdSelectionType.wdSelectionIP &&
                InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionandoTextos)
                {
                    if (!EsCtrlPresionada())
                    {
                        InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.RangosSeleccionados.Clear();
                    }

                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.RangosSeleccionados.Add(Target.Range);
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionesTextos.Clear();

                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.RutaDocumento =
                            Globals.PlanMath_Word.Application.ActiveDocument.FullName;

                    foreach (Word.Range rangoActual in InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.RangosSeleccionados)
                    {
                        SeleccionTexto seleccion = new SeleccionTexto();
                        seleccion.TextoActual = rangoActual.Text;

                        seleccion.NumeroPagina = rangoActual.Information[WdInformation.wdActiveEndPageNumber];
                        seleccion.NumeroFila = rangoActual.Information[WdInformation.wdFirstCharacterLineNumber];
                        //InstanciaActual.NumeroColumna = Target.Range.Information[WdInformation.wdFirstCharacterColumnNumber];

                        seleccion.CaracterInicial = rangoActual.Start;
                        seleccion.CaracterFinal = rangoActual.End;

                        InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionesTextos.Add(seleccion);
                    }

                    MostrarSeleccion_EnviarEntradas();
                }
                else if (InstanciaActual != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.Visible &&
                    InstanciaActual.contenidoPanelEnviarEntradas.ModoManual &&
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionandoTextos)
                {
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.RangosSeleccionados.Clear();
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionesTextos.Clear();
                }

            }
            catch (Exception) { }
        }

        private void CrearCarpetas()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion);
            }

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion_PlanMath))
            {
                if (!Directory.Exists(RutaCarpeta_PlanMath))
                {
                    Directory.CreateDirectory(RutaCarpeta_PlanMath);
                }

                if (!Directory.Exists(RutaCarpeta_PlanMath_EnvioEntradas))
                {
                    Directory.CreateDirectory(RutaCarpeta_PlanMath_EnvioEntradas);
                }

                if (!Directory.Exists(RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion))
                {
                    Directory.CreateDirectory(RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion);
                }
            }
        }

        public void CargarListaArchivosPlanMath()
        {
            if (File.Exists(RutaArchivo_ArchivoRecientes))
            {
                StreamReader leerArchivo = new StreamReader(RutaArchivo_ArchivoRecientes);

                ArchivosPlanMath.Add(leerArchivo.ReadLine());
                while (!leerArchivo.EndOfStream)
                {
                    ArchivosPlanMath.Add(leerArchivo.ReadLine());
                }

                leerArchivo.Close();
            }
        }

        public void GuardarListaArchivosPlanMath()
        {
            try
            {
                StreamWriter guardarArchivo = new StreamWriter(RutaArchivo_ArchivoRecientes);

                foreach (var linea in ArchivosPlanMath)
                {
                    guardarArchivo.WriteLine(linea);
                }

                guardarArchivo.Close();
            }
            catch (Exception) { }
        }

        public bool VerificarURL_Documento_Complemento(string url)
        {
            try
            {
                Globals.PlanMath_Word.Application.Documents.CanCheckOut(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CalculoArchivoPlanMath> ListarCalculos_ArchivoPlanMath(string rutaArchivoPlanMath)
        {
            if (string.IsNullOrEmpty(rutaArchivoPlanMath)) return null;

            CrearCarpetas();

            string ID = Guid.NewGuid().ToString();

            string argumentos = "ListarCalculos ";
            argumentos += @"""" + @rutaArchivoPlanMath + @""" ";
            argumentos += " " + ID + " ";

            //Process.Start("com.microsoft.print3d://");
            //Process aplicacionPlanMath = Process.Start("ms-windows-store://pdp/?productid=9NP6HTT20VDW", argumentos);
            //if(aplicacionPlanMath != null) aplicacionPlanMath.WaitForExit();

            List<CalculoArchivoPlanMath> lista = null;

            if (File.Exists(RutaCarpeta_PlanMath + "\\" + ID + ".dat"))
            {
                lista = LeerListaCalculos(RutaCarpeta_PlanMath + "\\" + ID + ".dat");
                File.Delete(RutaCarpeta_PlanMath + "\\" + ID + ".dat");
            }

            return lista;
        }

        public List<RutaArchivoPlanMath> ListarArchivosPlanMath()
        {
            List<RutaArchivoPlanMath> lista = new List<RutaArchivoPlanMath>();

            foreach (var ruta in ArchivosPlanMath)
            {
                RutaArchivoPlanMath archivo = new RutaArchivoPlanMath();
                archivo.Ruta = ruta;
                lista.Add(archivo);
            }

            return lista;
        }

        public List<CalculoArchivoPlanMath> LeerListaCalculos(string rutaArchivo)
        {
            XmlReader guarda = XmlReader.Create(rutaArchivo);
            DataContractSerializer objeto = new DataContractSerializer(typeof(ListaCalculos_ArchivoPlanMath), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            ListaCalculos_ArchivoPlanMath lista = (ListaCalculos_ArchivoPlanMath)objeto.ReadObject(guarda);
            guarda.Close();

            return lista.Calculos;
        }

        public void EnviarEntrada(Entrada_Desde_Word entrada)
        {
            string ID = Guid.NewGuid().ToString();

            XmlWriter guardaEntrada = XmlWriter.Create(RutaCarpeta_PlanMath_EnvioEntradas + "\\" + ID + ".dat");
            DataContractSerializer objetoEntrada = new DataContractSerializer(typeof(Entrada_Desde_Word), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objetoEntrada.WriteObject(guardaEntrada, entrada);
            guardaEntrada.Close();
        }

        public static void SeleccionarTextos_DefinicionEjecucion(Entrada_Desde_Word definicion, int indiceTexto)
        {
            Word.Document libro = null;

            foreach (Word.Document itemLibro in Globals.PlanMath_Word.Application.Documents)
            {
                if (itemLibro.FullName == definicion.RutaDocumento)
                {
                    libro = itemLibro;
                    break;
                }
            }

            if (libro != null)
            {
                object what = WdGoToItem.wdGoToPage;
                object which = WdGoToDirection.wdGoToAbsolute;
                object count = definicion.TextosBusqueda[indiceTexto].NumeroPagina;
                var range = InstanciaActual.Documento.Content.GoTo(ref what, ref which, ref count);

                object what1 = WdGoToItem.wdGoToLine;
                object which1 = WdGoToDirection.wdGoToAbsolute;
                object count1 = definicion.TextosBusqueda[indiceTexto].NumeroFila;
                var range1 = range.GoTo(ref what1, ref which1, ref count1);

                range1.SetRange(definicion.TextosBusqueda[indiceTexto].CaracterInicial, definicion.TextosBusqueda[indiceTexto].CaracterFinal);
                range1.Select();
            }
        }

        public static void CerrarAplicacionesWord(string ejecucionAsociada)
        {
            Word.Application AplicacionWord_Ejecucion = AplicacionesWord.FirstOrDefault(i => i.IDEjecucionAsociada == ejecucionAsociada)?.AplicacionWord;

            try
            {
                if (AplicacionWord_Ejecucion != null)
                {
                    AplicacionWord_Ejecucion.Quit();
                    Marshal.FinalReleaseComObject(AplicacionWord_Ejecucion);
                    AplicacionesWord.Remove(AplicacionesWord.FirstOrDefault(i => i.IDEjecucionAsociada == ejecucionAsociada));

                }
            }
            catch (Exception) { }
        }

        public class InstanciaPlanMath
        {
            public Word.Document Documento;
            public List<SeleccionInstancia> SeleccionesInstancias;            
            public PanelDefinirEntradas contenidoPanelDefinirEntradas;
            public PanelEnviarEntradas contenidoPanelEnviarEntradas;
            public CustomTaskPane panelDefinirEntradas;
            public CustomTaskPane panelEnviarEntradas;
            
            public InstanciaPlanMath()
            {
                SeleccionesInstancias = new List<SeleccionInstancia>();
            }
        }

        public class SeleccionInstancia
        {
            public List<SeleccionTexto> SeleccionesTextos { get; set; }
            public List<TextoBusqueda_Instancia> TextosBusqueda;
            public Entrada_Desde_Word EntradaActual { get; set; }
            public List<Range> RangosSeleccionados { get; set; }
            public bool SeleccionandoTextos { get; set; }

            public SeleccionInstancia()
            {
                EntradaActual = new Entrada_Desde_Word();
                TextosBusqueda = new List<TextoBusqueda_Instancia>();
                SeleccionesTextos = new List<SeleccionTexto>();
                RangosSeleccionados = new List<Range>();
            }
        }

        public class SeleccionTexto
        {
            public string TextoActual;
            public long NumeroPagina;
            public long NumeroFila;
            //public long NumeroColumna;
            public int CaracterInicial;
            public int CaracterFinal;
        }

        public class RutaArchivoPlanMath
        {
            public string Ruta { get; set; }
            public string Nombre
            {
                get
                {
                    return Ruta.Substring(Ruta.LastIndexOf("\\") + 1);
                }
            }
        }

        public class CalculoArchivoPlanMath
        {
            public string Nombre { get; set; }
        }

        public class ListaCalculos_ArchivoPlanMath
        {
            public List<CalculoArchivoPlanMath> Calculos { get; set; }
            public ListaCalculos_ArchivoPlanMath()
            {
                Calculos = new List<CalculoArchivoPlanMath>();
            }
        }

        public class TextoBusqueda_Instancia
        {
            public long NumeroPagina { get; set; }
            public long NumeroFila { get; set; }
            public int CaracterInicial { get; set; }
            public int CaracterFinal { get; set; }
            public string Texto { get; set; }
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        // Código de la tecla Ctrl (izquierda o derecha)
        private const int VK_CONTROL = 0x11;

        public static bool EsCtrlPresionada()
        {
            // Verificar si la tecla Ctrl está presionada
            return (GetAsyncKeyState(VK_CONTROL) & 0x8000) != 0;
        }

        public class DuplaAplicacionWord_Ejecucion
        {
            public Word.Application AplicacionWord { get; set; }
            public string IDEjecucionAsociada { get; set; }
        }


        public bool EsModoOscuro()
        {
            const string registryKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string valueName = "AppsUseLightTheme";

            using (var key = Registry.CurrentUser.OpenSubKey(registryKey))
            {
                if (key != null && key.GetValue(valueName) is int value)
                {
                    return value == 0; // 0 significa modo oscuro
                }
            }

            return false; // Por defecto, considera modo claro
        }

        public void ColorearControles(Control ctrl)
        {
            ctrl.BackColor = Color.Black;
            ctrl.ForeColor = Color.White;

            foreach (Control ctrlInterno in ctrl.Controls)
            {
                ColorearControles(ctrlInterno);
            }
        }

    #region Código generado por VSTO

    /// <summary>
    /// Método necesario para admitir el Diseñador. No se puede modificar
    /// el contenido de este método con el editor de código.
    /// </summary>
    private void InternalStartup()
        {
            this.Startup += new System.EventHandler(PlanMath_Word_Startup);
            this.Shutdown += new System.EventHandler(PlanMath_Word_Shutdown);
        }
        
        #endregion
    }
}
