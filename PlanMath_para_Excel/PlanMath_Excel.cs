using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools;
using static PlanMath_para_Excel.Entradas;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Runtime.Serialization;
using System.Threading;
using System.ComponentModel;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PlanMath_para_Excel
{
    public partial class PlanMath_Excel
    {
        public static InstanciaPlanMath InstanciaActual;
        public static List<InstanciaPlanMath> Instancias;
        public List<string> ArchivosPlanMath = new List<string>();
        public const string Nombre_Aplicacion = "PlanMath_para_Excel";
        public static string RutaArchivo_ArchivoRecientes = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion + "\\ArchivosPlanMath.dat";
        public const string Nombre_Aplicacion_PlanMath = "PlanMath";
        public static string RutaCarpeta_PlanMath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\" + Nombre_Aplicacion_PlanMath + "\\" + Nombre_Aplicacion;
        public static string RutaCarpeta_PlanMath_EnvioEntradas = RutaCarpeta_PlanMath + "\\Entradas_Enviadas";
        public static string RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion = RutaCarpeta_PlanMath + "\\Entradas_Enviadas_Ejecucion";

        public static Thread ProcesoLecturaEnviosEntradas = new Thread(Entradas.LeerEnviosEntradas);
        public static bool ProcesoTerminado_EnvioEntradas;
        public static bool ProcesoPausado_EnvioEntradas;
        public static List<EnvioEntradaDesdeEjecucion> Ejecuciones_EnvioEntradas = new List<EnvioEntradaDesdeEjecucion>();
        public static List<string> ArchivosTemporalesAEliminar = new List<string>();
        public static bool Confirmacion_ProcesoTerminado_EnvioEntradas;
        public string NombreLibroActivo;
        //public static List<DuplaAplicacionExcel_Ejecucion> AplicacionesExcel_Abiertas = new List<DuplaAplicacionExcel_Ejecucion>();
        public static List<DuplaAplicacionExcel_Ejecucion> AplicacionesExcel = new List<DuplaAplicacionExcel_Ejecucion>();

        private void PlanMath_Excel_Startup(object sender, System.EventArgs e)
        {
            try
            {
                CrearCarpetas();

                Instancias = new List<InstanciaPlanMath>();
                Globals.PlanMath_Excel.Application.WorkbookOpen += Application_WorkbookOpen;
                Globals.PlanMath_Excel.Application.WorkbookActivate += Application_WorkbookActivate;
                ((Excel.AppEvents_Event)Globals.PlanMath_Excel.Application).NewWorkbook += PlanMath_Excel_NewWorkbook;
                ((Excel.AppEvents_Event)Globals.PlanMath_Excel.Application).SheetSelectionChange += PlanMath_Excel_SheetSelectionChange;
                ((Excel.AppEvents_Event)Globals.PlanMath_Excel.Application).SheetActivate += PlanMath_Excel_SheetActivate;
                ((Excel.AppEvents_Event)Globals.PlanMath_Excel.Application).WorkbookAfterSave += PlanMath_Excel_WorkbookAfterSave;

                CargarListaArchivosPlanMath();
                ProcesoLecturaEnviosEntradas.Start();
            }
            catch (Exception) { }
        }

        public static bool VerificarURL_Libro(string url)
        {
            try
            {
                Excel.Application aplicacion = new Excel.Application();
                aplicacion.Workbooks.CanCheckOut(url);

                aplicacion.Quit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerificarURL_Libro_Complemento(string url)
        {
            try
            {
                Globals.PlanMath_Excel.Application.Workbooks.CanCheckOut(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Abrir_Libro(string url)
        {
            try
            {
                //string ejecutable = ObtenerRutaEjecutableExcel().Result;

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "excel.exe",
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
        public static double ObtenerEntrada_En_Excel(Entrada_Desde_Excel entrada, ref bool encontrado, string IDEjecucion)
        {
            double numero = 0;
            Excel.Application aplicacion = null;
            bool aplicacionTemporal = false;

            Excel.Workbook libro = null;

            if (Globals.PlanMath_Excel == null || Globals.PlanMath_Excel.Application == null)
            {
                Excel.Application AplicacionExcel_Ejecucion = AplicacionesExcel.FirstOrDefault(i => i.IDEjecucionAsociada == IDEjecucion)?.AplicacionExcel;

                if (AplicacionExcel_Ejecucion == null)
                {
                    AplicacionExcel_Ejecucion = new Excel.Application();
                    AplicacionExcel_Ejecucion.DisplayAlerts = false;
                    AplicacionExcel_Ejecucion.EnableEvents = false;

                    AplicacionesExcel.Add(new DuplaAplicacionExcel_Ejecucion() { AplicacionExcel = AplicacionExcel_Ejecucion, IDEjecucionAsociada = IDEjecucion });
                }

                aplicacion = AplicacionExcel_Ejecucion;
                aplicacionTemporal = true;

                //if (!string.IsNullOrEmpty(IDEjecucion))
                //    AplicacionesExcel_Abiertas.Add(new DuplaAplicacionExcel_Ejecucion() { AplicacionExcel = aplicacion, IDEjecucionAsociada = IDEjecucion });
            }
            else
            {
                aplicacion = Globals.PlanMath_Excel.Application;
                //libro = (from Excel.Workbook L in aplicacion.Workbooks where L.FullName == entrada.Libro select L).FirstOrDefault();
            }

            try
            {
                libro = aplicacion.Workbooks.Open(entrada.Libro);
                Excel.Worksheet hoja = (from Excel.Worksheet H in libro.Worksheets where H.Name == entrada.Hoja select H).FirstOrDefault();

                if (hoja != null)
                {
                    Excel.Range celda = hoja.Range[entrada.Celdas];
                    if (celda != null)
                    {
                        if (!double.TryParse(celda.Value.ToString(), out numero))
                        {
                            throw new Exception("El valor de la celda '" + entrada.Celdas + "' en la hoja '" + entrada.Hoja + "' del libro no es válido para la entrada.");
                        }

                        encontrado = true;
                        if (aplicacionTemporal)
                        {
                            Marshal.FinalReleaseComObject(celda);
                            celda = null;

                            Marshal.FinalReleaseComObject(hoja);
                            hoja = null;

                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }
                        //if(aplicacionTemporal) aplicacion.Quit();
                    }
                    else
                    {
                        if (aplicacionTemporal)
                        {
                            Marshal.FinalReleaseComObject(celda);
                            celda = null;

                            Marshal.FinalReleaseComObject(hoja);
                            hoja = null;

                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }

                        throw new Exception("No se encontró la celda '" + entrada.Celdas + "' en la hoja '" + entrada.Hoja + "' del libro.");
                    }
                }
                else
                {
                    if (aplicacionTemporal)
                    {
                        Marshal.FinalReleaseComObject(hoja);
                        hoja = null;

                        libro.Close();
                        Marshal.FinalReleaseComObject(libro);
                        libro = null;
                    }

                    throw new Exception("No se encontró la hoja '" + entrada.Hoja + "' en el libro.");
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (e.HResult != -2146827284)
                    {
                        if (aplicacionTemporal)
                        {
                            if (libro != null)
                            {
                                libro.Close();
                                Marshal.FinalReleaseComObject(libro);
                                libro = null;
                            }
                        }
                    }
                    //if(aplicacionTemporal) aplicacion.Quit();
                }
                catch (Exception)
                {
                    //if(aplicacionTemporal) aplicacion.Quit();
                }

                throw e;
            }

            return numero;
        }

        public static List<NumeroObtenido> ObtenerEntrada_ConjuntoNumeros_En_Excel(Entrada_Desde_Excel entrada, ref bool encontrado,
            string IDEjecucion)
        {
            //string[] textos = new string[1];
            Excel.Application aplicacion = null;
            bool aplicacionTemporal = false;

            Excel.Workbook libro = null;

            if (Globals.PlanMath_Excel == null || Globals.PlanMath_Excel.Application == null)
            {
                Excel.Application AplicacionExcel_Ejecucion = AplicacionesExcel.FirstOrDefault(i => i.IDEjecucionAsociada == IDEjecucion)?.AplicacionExcel;

                if (AplicacionExcel_Ejecucion == null)
                {
                    AplicacionExcel_Ejecucion = new Excel.Application();
                    AplicacionExcel_Ejecucion.DisplayAlerts = false;
                    AplicacionExcel_Ejecucion.EnableEvents = false;

                    AplicacionesExcel.Add(new DuplaAplicacionExcel_Ejecucion() { AplicacionExcel = AplicacionExcel_Ejecucion, IDEjecucionAsociada = IDEjecucion });
                }

                aplicacion = AplicacionExcel_Ejecucion;
                aplicacionTemporal = true;

                //if (!string.IsNullOrEmpty(IDEjecucion))
                //    AplicacionesExcel_Abiertas.Add(new DuplaAplicacionExcel_Ejecucion() { AplicacionExcel = aplicacion, IDEjecucionAsociada = IDEjecucion });
            }
            else
            {
                aplicacion = Globals.PlanMath_Excel.Application;
                //libro = (from Excel.Workbook L in aplicacion.Workbooks where L.FullName == entrada.Libro select L).FirstOrDefault();
            }

            List<NumeroObtenido> numeros = new List<NumeroObtenido>();
            
            try
            {
                libro = aplicacion.Workbooks.Open(entrada.Libro);
                Excel.Worksheet hoja = (from Excel.Worksheet H in libro.Worksheets where H.Name == entrada.Hoja select H).FirstOrDefault();

                if (hoja != null)
                {
                    Excel.Range celdas = hoja.Range[entrada.Celdas];
                    if (celdas != null)
                    {

                        string[] nombresCeldas = celdas.Address.ToString().Split(',');
                        Excel.Range celdaActual = null;

                        for (int indice = 0; indice < nombresCeldas.Length; indice++)
                        {
                            celdaActual = hoja.Range[nombresCeldas[indice]];

                            if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count == 1)
                            {
                                //for (int filas = 1; filas <= celdas.Rows.Count; filas++)
                                //{
                                //    for (int columnas = 1; columnas <= celdas.Columns.Count; columnas++)
                                //    {
                                //do
                                //{

                                double numero = 0;

                                if (double.TryParse(celdaActual.Value.ToString(), out numero))
                                    numeros.Add(new NumeroObtenido() { TextosInformacion = new string[] { }, Numero = numero });

                                //if (celdaActual.Name == nombresCeldas[indice])
                                //{
                                //if (!double.TryParse(celdaActual.Value.ToString(), out numero))
                                //{
                                //    throw new Exception("Las celdas '" + entrada.Celdas + "' en la hoja '" + entrada.Hoja + "' del libro no son válidas para la entrada.");
                                //}


                                //}


                                //celdaActual = celdas.Next;

                                //} while (celdas != null);
                                //    }
                                //}                            
                            }
                            else
                            {
                                //int posicionTextoNumero = 1;

                                if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count > 1)
                                {
                                    //if (entrada.TipoProcesamiento == TipoProcesamiento.PorColumnas)
                                    //{
                                        for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                                        {
                                            bool continuar = false;

                                            for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                                            {
                                                continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada,
                                                    ref celdaActual, ref hoja, ref filas, ref columnas, numeros);

                                                if (!continuar) break;
                                            }

                                            if (!continuar) break;
                                        }
                                    //}
                                    //else if (entrada.TipoProcesamiento == TipoProcesamiento.PorFilas)
                                    //{

                                    //    for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                                    //    {
                                    //        bool continuar = false;

                                    //        for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                                    //        {
                                    //            continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                                    //                ref celdaActual, ref filas, ref columnas, numeros);

                                    //            if (!continuar) break;
                                    //        }

                                    //        if (!continuar) break;
                                    //    }
                                    //}
                                }
                                else if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count == 1)
                                {
                                    int unaColumna = -1;

                                    //if (entrada.TipoProcesamiento == TipoProcesamiento.PorColumnas)
                                    //{
                                    for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                                    {
                                        //for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                                        //{
                                        bool continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada,
                                            ref celdaActual, ref hoja, ref filas, ref unaColumna, numeros);

                                        if (!continuar) break;
                                        //}
                                    }
                                    //}
                                }
                                else if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count > 1)
                                {
                                    int unaFila = -1;

                                    //if (entrada.TipoProcesamiento == TipoProcesamiento.PorColumnas)
                                    //{
                                    //for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                                    //{
                                    for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                                    {
                                        bool continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada,
                                            ref celdaActual, ref hoja, ref unaFila, ref columnas, numeros);

                                        if (!continuar) break;


                                    }

                                    //}
                                    //}
                                }
                            }
                        }

                        encontrado = true;
                        if (aplicacionTemporal)
                        {
                            Marshal.FinalReleaseComObject(celdaActual);
                            celdaActual = null;

                            Marshal.FinalReleaseComObject(celdas);
                            celdas = null;

                            Marshal.FinalReleaseComObject(hoja);
                            hoja = null;

                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }
                        //if(aplicacionTemporal) aplicacion.Quit();
                    }
                    else
                    {
                        if (aplicacionTemporal)
                        {
                            Marshal.FinalReleaseComObject(celdas);
                            celdas = null;

                            Marshal.FinalReleaseComObject(hoja);
                            hoja = null;

                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }

                        throw new Exception("No se encontró la celda '" + entrada.Celdas + "' en la hoja '" + entrada.Hoja + "' del libro.");
                    }
                }
                else
                {
                    if (aplicacionTemporal)
                    {
                        Marshal.FinalReleaseComObject(hoja);
                        hoja = null;

                        libro.Close();
                        Marshal.FinalReleaseComObject(libro);
                        libro = null;
                    }

                    throw new Exception("No se encontró la hoja '" + entrada.Hoja + "' en el libro.");
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (e.HResult != -2146827284)
                    {
                        if (aplicacionTemporal)
                        {
                            if (libro != null)
                            {
                                libro.Close();
                                Marshal.FinalReleaseComObject(libro);
                                libro = null;
                            }
                        }
                    }
                    //if(aplicacionTemporal) aplicacion.Quit();
                }
                catch (Exception)
                {
                    //if(aplicacionTemporal) aplicacion.Quit();
                }

                throw e;
            }

            return numeros;
        }

        private static bool ObtenerNumero_Celdas_ConjuntoNumeros(Entrada_Desde_Excel entrada, ref Excel.Range celdas, ref Excel.Worksheet hoja,
            ref int filas, ref int columnas, List<NumeroObtenido> numeros)
        {
            try
            {
                double numero = 0;
                List<string> textosInformacion = new List<string>();

                string celdaNumero = string.Empty;

                if (filas != -1 && columnas != -1)
                {
                    celdaNumero = ((Excel.Range)celdas[filas, columnas]).Address;
                }
                else
                {
                    int indice = 0;

                    if (filas == -1)
                        indice = columnas;
                    else if (columnas == -1)
                        indice = filas;

                    celdaNumero = ((Excel.Range)celdas[indice]).Address;
                }

                var asignaciones = (from A in entrada.Asignaciones_TextosInformacion where A.CeldaNumero == celdaNumero select A).ToList();

                var celdasRelacionadas = (from A in entrada.Asignaciones_TextosInformacion where A.CeldaNumero == celdaNumero | 
                                          A.CeldaTextoInformacion == celdaNumero select A).ToList();

                if (asignaciones != null)
                {
                    foreach (var itemAsignacion in asignaciones)
                    {
                        var celdaTextoInformacion = (hoja.Range[itemAsignacion.CeldaTextoInformacion]);

                        if (celdaTextoInformacion != null && celdaTextoInformacion.Value != null)
                        {
                            textosInformacion.Add(celdaTextoInformacion.Value.ToString());
                        }
                    }
                }

                if (filas != -1 && columnas != -1)
                {
                    if (((Excel.Range)celdas[filas, columnas]).Value != null)
                    {
                        if (double.TryParse(((Excel.Range)celdas[filas, columnas]).Value.ToString(), out numero))
                        {
                            if ((entrada.Asignaciones_TextosInformacion.Any() && 
                                !(celdasRelacionadas == null || (celdasRelacionadas != null && !celdasRelacionadas.Any()))) || 
                                !entrada.Asignaciones_TextosInformacion.Any())
                                numeros.Add(new NumeroObtenido() { TextosInformacion = textosInformacion.ToArray(), Numero = numero });
                        }
                    }
                }
                else
                {
                    int indice = 0;

                    if (filas == -1)
                        indice = columnas;
                    else if (columnas == -1)
                        indice = filas;

                    if (((Excel.Range)celdas[indice]).Value == null)
                        return false;

                    if (double.TryParse(((Excel.Range)celdas[indice]).Value.ToString(), out numero))
                    {
                        if ((entrada.Asignaciones_TextosInformacion.Any() && 
                            !(celdasRelacionadas == null || (celdasRelacionadas != null && !celdasRelacionadas.Any()))) ||
                            !entrada.Asignaciones_TextosInformacion.Any())
                            numeros.Add(new NumeroObtenido() { TextosInformacion = textosInformacion.ToArray(), Numero = numero });
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static List<List<string>> ObtenerEntrada_ConjuntoTextosInformacion_En_Excel(Entrada_Desde_Excel entrada, ref bool encontrado, string IDEjecucion)
        {
            //string[] textos = new string[1];
            Excel.Application aplicacion = null;
            bool aplicacionTemporal = false;

            Excel.Workbook libro = null;

            if (Globals.PlanMath_Excel == null || Globals.PlanMath_Excel.Application == null)
            {
                Excel.Application AplicacionExcel_Ejecucion = AplicacionesExcel.FirstOrDefault(i => i.IDEjecucionAsociada == IDEjecucion)?.AplicacionExcel;

                if (AplicacionExcel_Ejecucion == null)
                {
                    AplicacionExcel_Ejecucion = new Excel.Application();
                    AplicacionExcel_Ejecucion.DisplayAlerts = false;
                    AplicacionExcel_Ejecucion.EnableEvents = false;

                    AplicacionesExcel.Add(new DuplaAplicacionExcel_Ejecucion() { AplicacionExcel = AplicacionExcel_Ejecucion, IDEjecucionAsociada = IDEjecucion });
                }

                aplicacion = AplicacionExcel_Ejecucion;
                aplicacionTemporal = true;
            }
            else
            {
                aplicacion = Globals.PlanMath_Excel.Application;
                //libro = (from Excel.Workbook L in aplicacion.Workbooks where L.FullName == entrada.Libro select L).FirstOrDefault();
            }

            List<List<string>> textos = new List<List<string>>();

            try
            {
                libro = aplicacion.Workbooks.Open(entrada.Libro);
                Excel.Worksheet hoja = (from Excel.Worksheet H in libro.Worksheets where H.Name == entrada.Hoja select H).FirstOrDefault();

                if (hoja != null)
                {
                    Excel.Range celdas = hoja.Range[entrada.Celdas];
                    if (celdas != null)
                    {

                        string[] nombresCeldas = celdas.Address.ToString().Split(',');
                        List<string> texto = new List<string>();

                        for (int indice = 0; indice < nombresCeldas.Length; indice++)
                        {
                            Excel.Range celdaActual = hoja.Range[nombresCeldas[indice]];

                            if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count == 1)
                            {
                                if (celdaActual.Value != null)
                                {
                                    texto.Add(celdaActual.Value.ToString());
                                    textos.Add(texto);
                                }                         
                            }
                            else
                            {
                                if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count > 1)
                                {
                                    for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                                    {
                                        //bool continuar = false;
                                        List<string> textoFila = new List<string>();

                                        for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                                        {
                                            if (celdaActual[filas, columnas].Value != null)
                                            {
                                                textoFila.Add(celdaActual[filas, columnas].Value.ToString());
                                            }
                                            //else
                                            //    continuar = false;

                                            //if (!continuar) break;
                                        }

                                        textos.Add(textoFila);
                                        //if (!continuar) break;
                                    }
                                }
                                else if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count == 1)
                                {
                                    //int unaColumna = -1;

                                    for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                                    {
                                        List<string> textoFila = new List<string>();

                                        if (celdaActual[filas].Value != null)
                                        {
                                            textoFila.Add(celdaActual[filas].Value.ToString());
                                        }
                                        //else
                                        //    break;
                                        textos.Add(textoFila);
                                    }
                                }
                                else if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count > 1)
                                {
                                    //int unaFila = -1;

                                    for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                                    {
                                        if (celdaActual[columnas].Value != null)
                                        {
                                            texto.Add(celdaActual[columnas].Value.ToString());
                                        }
                                        //else
                                        //    break;
                                    }

                                    textos.Add(texto);
                                }
                            }
                        }

                        encontrado = true;
                        if (aplicacionTemporal)
                        {
                            Marshal.FinalReleaseComObject(celdas);
                            celdas = null;

                            Marshal.FinalReleaseComObject(hoja);
                            hoja = null;

                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }
                        //if (aplicacionTemporal) aplicacion.Quit();
                    }
                    else
                    {
                        if (aplicacionTemporal)
                        {
                            Marshal.FinalReleaseComObject(celdas);
                            celdas = null;

                            Marshal.FinalReleaseComObject(hoja);
                            hoja = null;

                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }

                        throw new Exception("No se encontró la celda '" + entrada.Celdas + "' en la hoja '" + entrada.Hoja + "' del libro.");
                    }
                }
                else
                {
                    if (aplicacionTemporal)
                    {
                        Marshal.FinalReleaseComObject(hoja);
                        hoja = null;

                        libro.Close();
                        Marshal.FinalReleaseComObject(libro);
                        libro = null;
                    }

                    throw new Exception("No se encontró la hoja '" + entrada.Hoja + "' en el libro.");
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (e.HResult != -2146827284)
                    {
                        if (aplicacionTemporal)
                        {
                            libro.Close();
                            Marshal.FinalReleaseComObject(libro);
                            libro = null;
                        }
                    }
                    //if (aplicacionTemporal) aplicacion.Quit();
                }
                catch (Exception)
                {
                    //if (aplicacionTemporal) aplicacion.Quit();
                }

                throw e;
            }

            return textos;
        }


        private void PlanMath_Excel_WorkbookAfterSave(Excel.Workbook Wb, bool Success)
        {
            try
            {
                if (InstanciaActual != null)
                {
                    if (Globals.PlanMath_Excel.CustomTaskPanes.Contains(InstanciaActual.panelDefinirEntradas) &&
                    InstanciaActual.panelDefinirEntradas.Visible)
                        MostrarPanel_DefinirEntradas();
                }
            }
            catch (Exception) { }
        }

        private void PlanMath_Excel_SheetActivate(object Sh)
        {
            if (InstanciaActual != null)
                ActivarSeleccion(Globals.PlanMath_Excel.Application.ActiveWindow.RangeSelection, ref Sh, Globals.PlanMath_Excel.Application.ActiveWindow.RangeSelection);
        }

        public void PlanMath_Excel_SheetSelectionChange(object Sh, Excel.Range Target)
        {
            try
            {
                if (InstanciaActual.contenidoPanelDefinirEntradas != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.Visible)
                {
                    if (InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionandoCeldas)
                    {
                        string[] nombresCeldas = Target.Address.ToString().Split(',');

                        if (InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.chkTipoTextosInformacion.Checked)
                        {
                            InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.EntradaActual.Tipo = TipoEntrada.TextosInformacion;
                        }
                        else
                        {
                            if (Target.Count == 1)
                            {
                                InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.EntradaActual.Tipo = TipoEntrada.Numero;
                            }
                            else
                                InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.EntradaActual.Tipo = TipoEntrada.ConjuntoNumeros;
                        }

                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.TextosInformacion.Clear();
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.NumerosSeleccionados.Clear();
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.Clear();

                        if (InstanciaActual.contenidoPanelDefinirEntradas != null)
                        {
                            InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaAsignacionesTextosInformacion.Items.Clear();
                        }

                        if (nombresCeldas.Length == 1)
                            ActivarSeleccion(Target, ref Sh, Target);
                        else if (nombresCeldas.Length > 1)
                        {
                            for (int indice = 0; indice < nombresCeldas.Length; indice++)
                            {
                                var celdaActual = ((Excel.Worksheet)Sh).Range[nombresCeldas[indice]];
                                ActivarSeleccion(celdaActual, ref Sh, Target);
                            }
                        }

                        //ListarTextosInformacion_Numeros();
                    }
                }

                if (InstanciaActual.contenidoPanelEnviarEntradas != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.ModoManual &&
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.Visible)
                {
                    if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionandoCeldas)
                    {
                        string[] nombresCeldas = Target.Address.ToString().Split(',');

                        if (InstanciaActual.contenidoPanelEnviarEntradas.EjecucionSeleccionada != null)
                        {                            

                            InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.TextosInformacion.Clear();
                            InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.NumerosSeleccionados.Clear();
                            InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.AsignacionesTextosInformacion.Clear();

                            bool seleccionValida = true;
                            string strError = string.Empty;

                            if (Target.Count > 1)
                            {
                                if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Tipo == TipoEntrada.Numero)
                                {
                                    seleccionValida = false;
                                    strError = "La entrada debe ser sólo un número.";
                                }
                            }
                            else
                            {
                                if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Tipo == TipoEntrada.ConjuntoNumeros)
                                {
                                    seleccionValida = false;
                                    strError = "La entrada debe ser un conjunto de números.";
                                }
                            }

                            if(!InstanciaActual.contenidoPanelEnviarEntradas.EsEntradaTextosInformacion &&
                                InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Tipo == TipoEntrada.TextosInformacion)
                            {
                                seleccionValida = false;
                                strError = "La entrada debe ser un conjunto de textos de información.";
                            }

                            if (seleccionValida)
                            {
                                InstanciaActual.contenidoPanelEnviarEntradas.mensajeErrorSeleccion.Visible = false;
                                InstanciaActual.contenidoPanelEnviarEntradas.btnEnviarEntrada.Enabled = true;

                                InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Libro =
                            Globals.PlanMath_Excel.Application.ActiveWorkbook.FullName;

                                if (nombresCeldas.Length == 1)
                                    ActivarSeleccion(Target, ref Sh, Target);
                                else if (nombresCeldas.Length > 1)
                                {
                                    for (int indice = 0; indice < nombresCeldas.Length; indice++)
                                    {
                                        var celdaActual = ((Excel.Worksheet)Sh).Range[nombresCeldas[indice]];
                                        ActivarSeleccion(celdaActual, ref Sh, Target);
                                    }
                                }
                            }
                            else
                            {
                                InstanciaActual.contenidoPanelEnviarEntradas.mensajeErrorSeleccion.Text = strError;
                                InstanciaActual.contenidoPanelEnviarEntradas.mensajeErrorSeleccion.Visible = true;
                                InstanciaActual.contenidoPanelEnviarEntradas.btnEnviarEntrada.Enabled = false;
                            }
                        }
                        //ListarTextosInformacion_Numeros();
                    }
                }
            }
            catch (Exception) { }
        }

        private void PlanMath_Excel_NewWorkbook(Excel.Workbook Wb)
        {
            AgregarInstancia(Wb);
        }

        public void Application_WorkbookActivate(Excel.Workbook Wb)
        {
            PlanMath_Excel.ProcesoPausado_EnvioEntradas = true;
            Thread.Sleep(500);

            PlanMath_Excel.Ejecuciones_EnvioEntradas.Clear();

            Thread.Sleep(500);
            PlanMath_Excel.ProcesoPausado_EnvioEntradas = false;

            if(Globals.PlanMath_Excel.Application.ActiveWorkbook != null)
                NombreLibroActivo = Globals.PlanMath_Excel.Application.ActiveWorkbook.FullName;

            if (InstanciaActual != null &&
                InstanciaActual.contenidoPanelEnviarEntradas != null)
            {
                InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = null;
                InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";
            }

            SeleccionarInstancia(Wb);
        }

        private void Application_WorkbookOpen(Excel.Workbook Wb)
        {
            AgregarInstancia(Wb);
        }

        private void PlanMath_Excel_Shutdown(object sender, System.EventArgs e)
        {
            GuardarListaArchivosPlanMath();

            ProcesoTerminado_EnvioEntradas = true;
            while (Confirmacion_ProcesoTerminado_EnvioEntradas == false)
            {
                Thread.Sleep(500);
            }

            EliminarArchivosTemporales();
        }

        private void SeleccionarInstancia(Excel.Workbook Wb)
        {
            try
            {
                var instancia = (from I in Instancias where I.Libro == Wb select I).FirstOrDefault();
                if (instancia != null)
                    InstanciaActual = instancia;
            }
            catch (Exception) { }
        }

        public void ActivarSeleccion(Excel.Range Target, ref object Sh, Excel.Range SeleccionCeldas)
        {
            try
            {
                if (InstanciaActual.contenidoPanelDefinirEntradas != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.Visible &&
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual != null &&
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionandoCeldas)
                {
                    var interseccion = Target.Application.Intersect(Target.CurrentRegion, Target);

                    if (interseccion != null)
                    {
                        //if (interseccion == Target)
                        //    InstanciaActual.Celdas = Target;
                        //else
                        //    InstanciaActual.Celdas = Target.CurrentRegion;

                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.Hoja = Target.Worksheet;
                        InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.Celdas = SeleccionCeldas;

                        if (Globals.PlanMath_Excel.CustomTaskPanes.Contains(InstanciaActual.panelDefinirEntradas) &&
                            InstanciaActual.panelDefinirEntradas.Visible)
                            MostrarPanel_DefinirEntradas(InstanciaActual.contenidoPanelDefinirEntradas.ModoManual);

                        EstablecerNumeros_SeleccionCeldas(ref interseccion, ref Sh, InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual);
                        EstablecerTextosInformacion_SeleccionCeldas(ref interseccion, ref Sh, InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual);
                    }
                }

                if (InstanciaActual.contenidoPanelEnviarEntradas != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.ModoManual &&
                    InstanciaActual.contenidoPanelEnviarEntradas.Visible &&
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia != null &&
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionandoCeldas)
                {
                    var interseccion = Target.Application.Intersect(Target.CurrentRegion, Target);

                    if (interseccion != null)
                    {
                        //if (interseccion == Target)
                        //    InstanciaActual.Celdas = Target;
                        //else
                        //    InstanciaActual.Celdas = Target.CurrentRegion;
                                                
                        InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.Hoja = Target.Worksheet;
                        InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.Celdas = SeleccionCeldas;

                        //if (Globals.PlanMath_Excel.CustomTaskPanes.Contains(InstanciaActual.panelEnviarEntradas) &&
                        //    InstanciaActual.panelEnviarEntradas.Visible)
                        //    MostrarPanel_EnviarEntradas();

                        EstablecerNumeros_SeleccionCeldas(ref interseccion, ref Sh, InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia);
                        EstablecerTextosInformacion_SeleccionCeldas(ref interseccion, ref Sh, InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia);

                        MostrarSeleccion_EnviarEntradas();
                    }
                }
            }
            catch (Exception) { }
        }

        public void AgregarInstancia(Excel.Workbook Wb)
        {
            try
            {
                var instancia = (from I in Instancias where I.Libro == Wb select I).FirstOrDefault();
                if (instancia == null)
                {
                    InstanciaPlanMath nuevaInstancia = new InstanciaPlanMath();
                    nuevaInstancia.Libro = Wb;
                    //nuevaInstancia.SeleccionesInstancias.Add(new SeleccionInstancia());
                    //nuevaInstancia.SeleccionesInstancias.Last().Hoja = Wb.ActiveSheet;
                    //nuevaInstancia.SeleccionesInstancias.Last().Celdas = Globals.PlanMath_Excel.Application.ActiveCell;
                    //nuevaInstancia.SeleccionesInstancias.Last().EntradaActual.Tipo = TipoEntrada.Numero;
                    Instancias.Add(nuevaInstancia);
                }
            }
            catch (Exception) { }
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

        public List<CalculoArchivoPlanMath> LeerListaCalculos(string rutaArchivo)
        {
            XmlReader guarda = XmlReader.Create(rutaArchivo);
            DataContractSerializer objeto = new DataContractSerializer(typeof(ListaCalculos_ArchivoPlanMath), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            ListaCalculos_ArchivoPlanMath lista = (ListaCalculos_ArchivoPlanMath)objeto.ReadObject(guarda);
            guarda.Close();

            return lista.Calculos;
        }

        public void EnviarEntrada(Entrada_Desde_Excel entrada)
        {
            string ID = Guid.NewGuid().ToString();

            XmlWriter guardaEntrada = XmlWriter.Create(RutaCarpeta_PlanMath_EnvioEntradas + "\\" + ID + ".dat");
            DataContractSerializer objetoEntrada = new DataContractSerializer(typeof(Entrada_Desde_Excel), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objetoEntrada.WriteObject(guardaEntrada, entrada);
            guardaEntrada.Close();
        }

        public static void SeleccionarCeldas_DefinicionEjecucion(Entrada_Desde_Excel definicion)
        {
            Excel.Workbook libro = null;

            foreach (Excel.Workbook itemLibro in Globals.PlanMath_Excel.Application.Workbooks)
            {
                if (itemLibro.FullName == definicion.Libro)
                {
                    libro = itemLibro;
                    break;
                }
            }

            if (libro != null)
            {
                Excel.Worksheet hoja = null;

                foreach (Excel.Worksheet itemHoja in libro.Sheets)
                {
                    if (itemHoja.Name == definicion.Hoja)
                    {
                        hoja = itemHoja;
                        break;
                    }
                }

                if (hoja != null)
                {
                    Excel.Range celdas = hoja.Range[definicion.Celdas];

                    if (celdas != null)
                    {
                        Globals.PlanMath_Excel.Application.Goto(celdas);
                    }
                }
            }
        }

        public void EliminarArchivosTemporales()
        {
            foreach (var itemRuta in ArchivosTemporalesAEliminar)
                if (File.Exists(itemRuta)) File.Delete(itemRuta);
        }
                        
        public void EstablecerTextosInformacion_SeleccionCeldas(ref Excel.Range celdas, ref object hoja,
            SeleccionInstancia SeleccionInstanciaActual)
        {
            string[] nombresCeldas = celdas.Address.ToString().Split(',');

            for (int indice = 0; indice < nombresCeldas.Length; indice++)
            {
                var celdaActual = ((Excel.Worksheet)hoja).Range[nombresCeldas[indice]];
                if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count == 1)
                {
                    //double numero = 0;

                    if (((Excel.Range)celdaActual).Value == null)
                        continue;

                    //if (!double.TryParse(celdaActual.Value.ToString(), out numero))
                    if (!SeleccionInstanciaActual.NumerosSeleccionados.Any(i => i.Valor == celdaActual.Value.ToString()))
                    {
                        TextosInformacionCelda definicionTextoInformacion = new TextosInformacionCelda();
                        definicionTextoInformacion.Celda = celdaActual;
                        SeleccionInstanciaActual.TextosInformacion.Add(definicionTextoInformacion);
                    }
                    //else
                    //{
                    //    TextosInformacionCelda definicionNumero = new TextosInformacionCelda();
                    //    definicionNumero.Celda = celdaActual;
                    //    InstanciaActual.NumerosSeleccionados.Add(definicionNumero);
                    //}
                }
                else
                {
                    //int posicionTextoNumero = 1;

                    if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count > 1)
                    {
                        for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                        {
                            //bool continuar = false;
                            int columnasRecorridas = 0;

                            for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                            {
                                Range celda = celdaActual[filas, columnas];
                                //continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                                //    ref celdaActual, ref filas, ref columnas, numeros);
                                if (celda.Value == null) continue;
                                //if (!continuar) break;
                                //continuar = ObtenerTextosInformacion_Celdas(ref celdas, ref filas, ref columnas);
                                //ObtenerTextosInformacion_Celdas(ref celdaActual, ref filas, ref columnas);
                                if (!SeleccionInstanciaActual.NumerosSeleccionados.Any(i => i.Valor == celda.Value.ToString()))
                                {
                                    TextosInformacionCelda definicionTextoInformacion = new TextosInformacionCelda();
                                    definicionTextoInformacion.Celda = celda;
                                    SeleccionInstanciaActual.TextosInformacion.Add(definicionTextoInformacion);
                                }
                                //if (!continuar) break;
                                columnasRecorridas++;
                            }

                            if (columnasRecorridas == 0) break;
                        }
                    }
                    else if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count == 1)
                    {
                        //int unaColumna = -1;

                        for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                        {
                            Range celda = celdaActual[filas];
                            //bool continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                            //    ref celdaActual, ref filas, ref unaColumna, numeros);
                            if (celda.Value == null) break;
                            //ObtenerTextosInformacion_Celdas(ref celdaActual, ref filas, ref unaColumna);
                            if (!SeleccionInstanciaActual.NumerosSeleccionados.Any(i => i.Valor == celda.Value.ToString()))
                            {
                                TextosInformacionCelda definicionTextoInformacion = new TextosInformacionCelda();
                                definicionTextoInformacion.Celda = celda;
                                SeleccionInstanciaActual.TextosInformacion.Add(definicionTextoInformacion);
                            }
                            //if (!continuar) break;
                        }
                    }
                    else if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count > 1)
                    {
                        //int unaFila = -1;

                        for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                        {
                            Range celda = celdaActual[columnas];
                            //bool continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                            //    ref celdaActual, ref unaFila, ref columnas, numeros);
                            if (celda.Value == null) break;
                            //ObtenerTextosInformacion_Celdas(ref celdaActual, ref unaFila, ref columnas);
                            if (!SeleccionInstanciaActual.NumerosSeleccionados.Any(i => i.Valor == celda.Value.ToString()))
                            {
                                TextosInformacionCelda definicionTextoInformacion = new TextosInformacionCelda();
                                definicionTextoInformacion.Celda = celda;
                                SeleccionInstanciaActual.TextosInformacion.Add(definicionTextoInformacion);
                            }
                            //if (!continuar) break;
                        }
                    }
                }
            }
        }

        public void EstablecerNumeros_SeleccionCeldas(ref Excel.Range celdas, ref object hoja,
            SeleccionInstancia SeleccionInstanciaActual)
        {
            string[] nombresCeldas = celdas.Address.ToString().Split(',');

            double numero = 0;

            for (int indice = 0; indice < nombresCeldas.Length; indice++)
            {
                var celdaActual = ((Excel.Worksheet)hoja).Range[nombresCeldas[indice]];
                if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count == 1)
                {
                    //double numero = 0;

                    if (((Excel.Range)celdaActual).Value == null)
                        continue;

                    //if (!double.TryParse(celdaActual.Value.ToString(), out numero))
                    if (double.TryParse(((Excel.Range)celdaActual).Value.ToString(), out numero))
                    {
                        TextosInformacionCelda definicionNumero = new TextosInformacionCelda();
                        definicionNumero.Celda = celdaActual;
                        SeleccionInstanciaActual.NumerosSeleccionados.Add(definicionNumero);
                    }
                    //else
                    //{
                    //    TextosInformacionCelda definicionNumero = new TextosInformacionCelda();
                    //    definicionNumero.Celda = celdaActual;
                    //    InstanciaActual.NumerosSeleccionados.Add(definicionNumero);
                    //}
                }
                else
                {
                    //int posicionTextoNumero = 1;

                    if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count > 1)
                    {
                        for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                        {
                            //bool continuar = false;
                            int columnasRecorridas = 0;

                            for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                            {
                                Range celda = celdaActual[filas, columnas];
                                //continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                                //    ref celdaActual, ref filas, ref columnas, numeros);
                                if (celda.Value == null) continue;
                                //if (!continuar) break;
                                //continuar = ObtenerTextosInformacion_Celdas(ref celdas, ref filas, ref columnas);
                                //ObtenerTextosInformacion_Celdas(ref celdaActual, ref filas, ref columnas);
                                if (double.TryParse(((Excel.Range)celda).Value.ToString(), out numero))
                                {
                                    TextosInformacionCelda definicionNumero = new TextosInformacionCelda();
                                    definicionNumero.Celda = celda;
                                    SeleccionInstanciaActual.NumerosSeleccionados.Add(definicionNumero);
                                }
                                //if (!continuar) break;
                                columnasRecorridas++;
                            }

                            if (columnasRecorridas == 0) break;
                        }
                    }
                    else if (celdaActual.Rows.Count > 1 && celdaActual.Columns.Count == 1)
                    {
                        //int unaColumna = -1;

                        for (int filas = 1; filas <= celdaActual.Rows.Count; filas++)
                        {
                            Range celda = celdaActual[filas];
                            //bool continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                            //    ref celdaActual, ref filas, ref unaColumna, numeros);
                            if (celda.Value == null) break;
                            //ObtenerTextosInformacion_Celdas(ref celdaActual, ref filas, ref unaColumna);
                            if (double.TryParse(((Excel.Range)celda).Value.ToString(), out numero))
                            {
                                TextosInformacionCelda definicionNumero = new TextosInformacionCelda();
                                definicionNumero.Celda = celda;
                                SeleccionInstanciaActual.NumerosSeleccionados.Add(definicionNumero);
                            }
                            //if (!continuar) break;
                        }
                    }
                    else if (celdaActual.Rows.Count == 1 && celdaActual.Columns.Count > 1)
                    {
                        //int unaFila = -1;

                        for (int columnas = 1; columnas <= celdaActual.Columns.Count; columnas++)
                        {
                            Range celda = celdaActual[columnas];
                            //bool continuar = ObtenerNumero_Celdas_ConjuntoNumeros(entrada, ref posicionTextoNumero, ref textos,
                            //    ref celdaActual, ref unaFila, ref columnas, numeros);
                            if (celda.Value == null) break;
                            //ObtenerTextosInformacion_Celdas(ref celdaActual, ref unaFila, ref columnas);
                            if (double.TryParse(((Excel.Range)celda).Value.ToString(), out numero))
                            {
                                TextosInformacionCelda definicionNumero = new TextosInformacionCelda();
                                definicionNumero.Celda = celda;
                                SeleccionInstanciaActual.NumerosSeleccionados.Add(definicionNumero);
                            }
                            //if (!continuar) break;
                        }
                    }
                }
            }
        }

        public static void AgregarAsignacionTextoInformacion(TextosInformacionCelda textoInformacion, TextosInformacionCelda numero)
        {
            if (textoInformacion != null && numero != null)
            {                
                AsignacionTextoInformacion_Numero_Instancia asignacion = new AsignacionTextoInformacion_Numero_Instancia();
                asignacion.Celda_Numero = numero;
                asignacion.Celda_TextoInformacion = textoInformacion;
                InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.Add(asignacion);

                System.Windows.Forms.ListViewItem itemLista = new System.Windows.Forms.ListViewItem();
                itemLista.Tag = asignacion;
                itemLista.Text = asignacion.Celda_TextoInformacion.NombreCelda;

                System.Windows.Forms.ListViewItem.ListViewSubItem subItemCelda = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subItemCelda.Text = asignacion.Celda_Numero.NombreCelda;
                itemLista.SubItems.Add(subItemCelda);

                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaAsignacionesTextosInformacion.Items.Add(itemLista);                
            }
        }

        public static void QuitarAsignacionTextoInformacion(TextosInformacionCelda textoInformacion, TextosInformacionCelda numero)
        {
            if (textoInformacion != null && numero != null)
            {
                var asignacion = (from A in InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion
                                  where A.Celda_TextoInformacion.Celda.Address == textoInformacion.Celda.Address &
                                  A.Celda_Numero.Celda.Address == numero.Celda.Address
                                  select A).FirstOrDefault();

                if (asignacion != null)
                {
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaAsignacionesTextosInformacion.Items.RemoveAt(InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.IndexOf(asignacion));
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.Remove(asignacion);
                }
            }
        }

        public static void LimpiarAsignaciones()
        {
            InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.Clear();
            InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaAsignacionesTextosInformacion.Items.Clear();
        }

        public static void CerrarAplicacionesExcel(string ejecucionAsociada)
        {
            Excel.Application AplicacionExcel_Ejecucion = AplicacionesExcel.FirstOrDefault(i => i.IDEjecucionAsociada == ejecucionAsociada)?.AplicacionExcel;

            try
            {
                if (AplicacionExcel_Ejecucion != null)
                {
                    AplicacionExcel_Ejecucion.Quit();
                    Marshal.FinalReleaseComObject(AplicacionExcel_Ejecucion);
                    AplicacionesExcel.Remove(AplicacionesExcel.FirstOrDefault(i => i.IDEjecucionAsociada == ejecucionAsociada));

                }
            }
            catch (Exception) { }

            //List<Excel.Application> AplicacionesEjecucion = new List<Excel.Application>();

            //if(!string.IsNullOrEmpty(ejecucionAsociada))
            //    AplicacionesEjecucion = AplicacionesExcel_Abiertas.Where(i => i.IDEjecucionAsociada == ejecucionAsociada).Select(i => i.AplicacionExcel).ToList();
            //else
            //    AplicacionesEjecucion = AplicacionesExcel_Abiertas.Select(i => i.AplicacionExcel).ToList();

            //while (AplicacionesEjecucion.Any())
            //{
            //    var aplicacion = AplicacionesEjecucion.FirstOrDefault();

            //    if (aplicacion != null)
            //    {
            //        //foreach (var libro in aplicacion.Workbooks)
            //        //{
            //        //    if (libro != null)
            //        //    {
            //        //        try
            //        //        {
            //        //            var libroExcel = (Microsoft.Office.Interop.Excel.Workbook)libro;

            //        //            libroExcel.Close();
            //        //        }
            //        //        catch (Exception) { }
            //        //    }
            //        //}

            //        AplicacionesEjecucion.Remove(aplicacion);
            //        AplicacionesExcel_Abiertas.Remove(AplicacionesExcel_Abiertas.FirstOrDefault(i => i.AplicacionExcel == aplicacion));

            //        try
            //        {
            //            aplicacion.Quit();
            //            Marshal.FinalReleaseComObject(aplicacion);
            //            aplicacion = null;
            //        }
            //        catch (Exception) { }
            //    }
            //}
        }

        public class InstanciaPlanMath
        {
            public PanelDefinirEntradas contenidoPanelDefinirEntradas;
            public PanelEnviarEntradas contenidoPanelEnviarEntradas;
            public CustomTaskPane panelDefinirEntradas;
            public CustomTaskPane panelEnviarEntradas;
            public List<SeleccionInstancia> SeleccionesInstancias;
            public Excel.Workbook Libro;
            public InstanciaPlanMath()
            {
                SeleccionesInstancias = new List<SeleccionInstancia>();
            }
        }

        public class SeleccionInstancia
        {            
            public Excel.Worksheet Hoja;
            public Excel.Range Celdas;
            public BindingList<TextosInformacionCelda> TextosInformacion;
            public BindingList<TextosInformacionCelda> NumerosSeleccionados;
            public List<AsignacionTextoInformacion_Numero_Instancia> AsignacionesTextosInformacion;
            public List<int> ColumnasNumeros = new List<int>();
            public Entrada_Desde_Excel EntradaActual { get; set; }
            public bool SeleccionandoCeldas {  get; set; }

            public SeleccionInstancia()
            {
                EntradaActual = new Entrada_Desde_Excel();
                TextosInformacion = new BindingList<TextosInformacionCelda>();
                NumerosSeleccionados = new BindingList<TextosInformacionCelda>();
                AsignacionesTextosInformacion = new List<AsignacionTextoInformacion_Numero_Instancia>();
            }
        }

        public class RutaArchivoPlanMath
        {
            public string Ruta { get; set; }
            public string Nombre
            {
                get
                {
                    if (!string.IsNullOrEmpty(Ruta))
                        return Ruta.Substring(Ruta.LastIndexOf("\\") + 1);
                    else
                        return string.Empty;
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

        public class TextosInformacionCelda
        {
            public Excel.Range Celda;

            public string Valor
            {
                get
                {
                    try
                    {
                        return Celda.Value.ToString();
                    }
                    catch (Exception)
                    {
                        return string.Empty;
                    }
                }

                set
                {
                    Celda.Value = value;
                }
            }

            public string NombreCelda
            {
                get
                {
                    if (Celda != null)
                    {
                        try
                        {
                            return Celda.Address + " - " + Valor;
                        }
                        catch (Exception)
                        {
                            return string.Empty;
                        }
                    }
                    else
                        return Valor;
                }
            }
        }

        public class AsignacionTextoInformacion_Numero_Instancia
        {
            public TextosInformacionCelda Celda_TextoInformacion { get; set; }
            public TextosInformacionCelda Celda_Numero { get; set; }
        }

        public class NumeroObtenido
        {
            public string[] TextosInformacion { get; set; }
            public double Numero { get; set; }
        }

        public class DuplaAplicacionExcel_Ejecucion
        {
            public Excel.Application AplicacionExcel { get; set; }
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
            this.Startup += new System.EventHandler(PlanMath_Excel_Startup);
            this.Shutdown += new System.EventHandler(PlanMath_Excel_Shutdown);
        }

        #endregion
    }
}
