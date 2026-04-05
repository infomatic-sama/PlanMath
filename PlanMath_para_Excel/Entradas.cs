using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using static PlanMath_para_Excel.PlanMath_Excel;

namespace PlanMath_para_Excel
{
    public class Entradas
    {        
        public class Entrada_Desde_Excel
        {
            public string Libro { get; set; }
            public string Hoja { get; set; }
            public string Celdas { get; set; }
            public TipoEntrada Tipo { get; set; }   
            public string ArchivoPlanMath { get; set; }
            public string NombreCalculo { get; set; }
            string nom;
            public string Nombre
            {
                get
                {
                    if (string.IsNullOrEmpty(nom))
                        return "Variable o vector de entrada";
                    else
                        return nom;
                }
                set
                {
                    nom = value;
                }
            }
            public List<AsignacionTextoInformacion> Asignaciones_TextosInformacion { get; set; }
            public bool EsRutaLocal_Excel { get; set; }
            public string URLOffice_Original { get; set; }
            public bool ReemplazarCeldas { get; set; }
            public bool EntradaManual { get; set; }
            public Entrada_Desde_Excel()
            {
                Tipo = TipoEntrada.Numero;
                Asignaciones_TextosInformacion = new List<AsignacionTextoInformacion>();
                URLOffice_Original = string.Empty;
                nom = string.Empty;
            }
        }

        public class EnvioEntradaDesdeEjecucion
        {
            public string ID_Ejecucion { get; set; }
            public string Texto_Ejecucion { get; set; }
            public Entrada_Desde_Excel DefinicionEntrada { get; set; }
            public string RutaArchivo { get; set; }
        }

        public enum TipoEntrada
        {
            Numero = 1,
            ConjuntoNumeros = 2,
            TextosInformacion = 3
        }

        public class AsignacionTextoInformacion
        {
            public string CeldaNumero { get; set; }
            public string CeldaTextoInformacion { get; set; }
        }

        public static void MostrarPanel_DefinirEntradas(bool modoManual = false)
        {
            try
            {
                if(Globals.PlanMath_Excel.Application.ActiveWorkbook != null)
                    Globals.PlanMath_Excel.AgregarInstancia(Globals.PlanMath_Excel.Application.ActiveWorkbook);

                if(InstanciaActual == null && Globals.PlanMath_Excel.Application.ActiveWorkbook != null)
                {
                    Globals.PlanMath_Excel.Application_WorkbookActivate(Globals.PlanMath_Excel.Application.ActiveWorkbook);
                }

                if (InstanciaActual != null)
                {
                    if (InstanciaActual.contenidoPanelDefinirEntradas == null)
                    {
                        InstanciaActual.contenidoPanelDefinirEntradas = new PanelDefinirEntradas();
                    }

                    InstanciaActual.contenidoPanelDefinirEntradas.ModoManual = modoManual;

                    if (!Globals.PlanMath_Excel.CustomTaskPanes.Contains(InstanciaActual.panelDefinirEntradas))
                    {
                        InstanciaActual.panelDefinirEntradas = Globals.PlanMath_Excel.CustomTaskPanes.Add(InstanciaActual.contenidoPanelDefinirEntradas,
                            "Definir variables o vectores de entradas para PlanMath");
                        InstanciaActual.panelDefinirEntradas.Width = 870;
                        InstanciaActual.contenidoPanelDefinirEntradas.lblEnvio.Text = string.Empty;
                        InstanciaActual.contenidoPanelDefinirEntradas.iconoEnvioOk.Visible = false;
                    }

                    MostrarSeleccion_DefinirEntradas();
                    InstanciaActual.panelDefinirEntradas.Visible = true;                    
                    //ListarTextosInformacion_Numeros();
                }
            }
            catch (Exception) { }
        }

        public static void MostrarPanel_EnviarEntradas(bool modoManual = false)
        {
            try
            {
                if (Globals.PlanMath_Excel.Application.ActiveWorkbook != null)
                    Globals.PlanMath_Excel.AgregarInstancia(Globals.PlanMath_Excel.Application.ActiveWorkbook);

                if (InstanciaActual == null && Globals.PlanMath_Excel.Application.ActiveWorkbook != null)
                {
                    Globals.PlanMath_Excel.Application_WorkbookActivate(Globals.PlanMath_Excel.Application.ActiveWorkbook);
                }

                if (InstanciaActual != null)
                {
                    if (InstanciaActual.contenidoPanelEnviarEntradas == null)
                    {
                        InstanciaActual.contenidoPanelEnviarEntradas = new PanelEnviarEntradas();
                        InstanciaActual.contenidoPanelEnviarEntradas.ModoManual = modoManual;
                        InstanciaActual.SeleccionesInstancias.Add(new SeleccionInstancia());
                        InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia = InstanciaActual.SeleccionesInstancias.Last();
                    }
                    else
                    {
                        InstanciaActual.contenidoPanelEnviarEntradas.ModoManual = modoManual;
                    }

                    if (!Globals.PlanMath_Excel.CustomTaskPanes.Contains(InstanciaActual.panelEnviarEntradas))
                    {
                        InstanciaActual.panelEnviarEntradas = Globals.PlanMath_Excel.CustomTaskPanes.Add(InstanciaActual.contenidoPanelEnviarEntradas,
                            "Enviar variables o vectores de entradas a una ejecución de PlanMath");
                        InstanciaActual.panelEnviarEntradas.Width = 600;
                        //Globals.PlanMath_Excel.CargarOpciones_PorDefecto();
                        //InstanciaActual.contenidoPanelDefinirEntradas.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Excel.ListarArchivosPlanMath();
                        InstanciaActual.contenidoPanelEnviarEntradas.lblEnvio.Text = string.Empty;
                        InstanciaActual.contenidoPanelEnviarEntradas.iconoEnvioOk.Visible = false;
                        InstanciaActual.contenidoPanelEnviarEntradas.lblNombresDescripciones.Text = string.Empty;
                    }

                    if (Ejecuciones_EnvioEntradas.Any())
                    {
                        //InstanciaActual.contenidoPanelEnviarEntradas.IndiceEjecucionSeleccionada = 0;
                        //InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = Ejecuciones_EnvioEntradas;
                        //InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.SelectedIndex = 0;

                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = null;
                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";

                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = PlanMath_Excel.Ejecuciones_EnvioEntradas
                            .Where(i => i.DefinicionEntrada.EntradaManual == modoManual).ToList();

                        InstanciaActual.contenidoPanelEnviarEntradas.IndiceEjecucionSeleccionada = PlanMath_Excel.Ejecuciones_EnvioEntradas.Count(i => i.DefinicionEntrada.EntradaManual == modoManual) - 1;
                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.SelectedIndex = InstanciaActual.contenidoPanelEnviarEntradas.IndiceEjecucionSeleccionada;
                    }

                    InstanciaActual.panelEnviarEntradas.Visible = true;
                    InstanciaActual.contenidoPanelEnviarEntradas.procesoActualizarEjecucionSeleccionada.Start();
                }
            }
            catch (Exception) { }
        }

        public static void MostrarSeleccion_DefinirEntradas()
        {
            try
            {
                InstanciaActual.contenidoPanelDefinirEntradas.txtLibro.Text = InstanciaActual.Libro.FullName;

                if (InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.Hoja != null &
                    InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.Celdas != null)
                {
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtHoja.Text = ((Worksheet)InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.Hoja).Name;
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtCeldas.Text = ((Range)InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.Celdas).AddressLocal;
                }

                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasTextosInformacion.DisplayMember = "NombreCelda";
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasTextosInformacion.ValueMember = "Valor";

                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasNumeros.DisplayMember = "NombreCelda";
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasNumeros.ValueMember = "Valor";

                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasTextosInformacion.DataSource = InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.TextosInformacion;
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasNumeros.DataSource = InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.NumerosSeleccionados;

                if (InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.EntradaActual.Tipo == TipoEntrada.Numero)
                {
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtTipoEntrada.Text = "Variable de número";
                }
                else if (InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.EntradaActual.Tipo == TipoEntrada.ConjuntoNumeros)
                {
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtTipoEntrada.Text = "Vector de números";
                }
                else if (InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.EntradaActual.Tipo == TipoEntrada.TextosInformacion)
                {
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtTipoEntrada.Text = "Vector de cadenas de texto";
                }
            }
            catch (Exception) { }
        }

        public static void MostrarSeleccion_EnviarEntradas()
        {
            try
            {
                InstanciaActual.contenidoPanelEnviarEntradas.txtLibro.Text = InstanciaActual.Libro.FullName;

                if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.Hoja != null &
                    InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.Celdas != null)
                {
                    InstanciaActual.contenidoPanelEnviarEntradas.hojaSeleccionada.Text = ((Worksheet)InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.Hoja).Name;
                    InstanciaActual.contenidoPanelEnviarEntradas.celdasSeleccionadas.Text = ((Range)InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.Celdas).AddressLocal;
                }

                //InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasTextosInformacion.DisplayMember = "NombreCelda";
                //InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasTextosInformacion.ValueMember = "Valor";

                //InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasNumeros.DisplayMember = "NombreCelda";
                //InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasNumeros.ValueMember = "Valor";

                //InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasTextosInformacion.DataSource = InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.TextosInformacion;
                //InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.listaCeldasNumeros.DataSource = InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.NumerosSeleccionados;

                //if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Tipo == TipoEntrada.Numero)
                //{
                //    InstanciaActual.contenidoPanelEnviarEntradas.txtTipoEntrada.Text = "Número";
                //}
                //else if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Tipo == TipoEntrada.ConjuntoNumeros)
                //{
                //    InstanciaActual.contenidoPanelEnviarEntradas.txtTipoEntrada.Text = "Conjunto de números";
                //}
                //else if (InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.EntradaActual.Tipo == TipoEntrada.TextosInformacion)
                //{
                //    InstanciaActual.contenidoPanelEnviarEntradas.txtTipoEntrada.Text = "Conjunto de textos de información";
                //}
            }
            catch (Exception) { }
        }

        public static void LeerEnviosEntradas()
        {            
            while (!ProcesoTerminado_EnvioEntradas)
            {
                bool ejecucionesAgregadas = false;

                string[] archivos = Directory.GetFiles(RutaCarpeta_PlanMath, "Ejec-*");
                foreach (var ruta in archivos)
                {
                    if (!ProcesoPausado_EnvioEntradas)
                    {
                        if (File.Exists(ruta))
                        {
                            EnvioEntradaDesdeEjecucion envioEntrada = LeerArchivoEnvioEntrada(ruta);
                            if (envioEntrada != null)
                            {
                                string libro = string.Empty;
                                if (!string.IsNullOrEmpty(Globals.PlanMath_Excel.NombreLibroActivo))
                                {
                                    libro = Globals.PlanMath_Excel.NombreLibroActivo;
                                }

                                if(envioEntrada.DefinicionEntrada.URLOffice_Original.ToString().Trim().ToLower() == libro.ToString().Trim().ToLower())
                                {
                                    if (!Ejecuciones_EnvioEntradas.Any((e) => e.ID_Ejecucion == envioEntrada.ID_Ejecucion))
                                    {
                                        Ejecuciones_EnvioEntradas.Add(envioEntrada);
                                        
                                        if(!ejecucionesAgregadas)
                                            ejecucionesAgregadas = true;
                                    }
                                }
                            }
                        }
                    }
                }
                
                if(ejecucionesAgregadas)
                {
                    if (InstanciaActual != null && InstanciaActual.panelEnviarEntradas != null &&
                                            Globals.PlanMath_Excel.CustomTaskPanes.Contains(InstanciaActual.panelEnviarEntradas) &&
                            InstanciaActual.panelEnviarEntradas.Visible)
                    {
                        InstanciaActual.contenidoPanelEnviarEntradas.ActualizarEjecucionSeleccionada = true;
                    }
                }

                Thread.Sleep(1000);
            }

            Confirmacion_ProcesoTerminado_EnvioEntradas = true;

            try
            {
                Thread.CurrentThread.Abort();
            }
            catch (Exception) { }
        }

        public static EnvioEntradaDesdeEjecucion LeerArchivoEnvioEntrada(string rutaArchivo)
        {
            EnvioEntradaDesdeEjecucion envioEntrada = new EnvioEntradaDesdeEjecucion();
            XmlReader leerArchivo = XmlReader.Create(rutaArchivo);
            leerArchivo.ReadStartElement();
            envioEntrada.ID_Ejecucion = leerArchivo.ReadElementContentAsString();
            //leerArchivo.ReadEndElement();
            envioEntrada.Texto_Ejecucion = leerArchivo.ReadElementContentAsString();
            //leerArchivo.ReadEndElement();
            leerArchivo.ReadStartElement();
            DataContractSerializer objeto = new DataContractSerializer(typeof(Entrada_Desde_Excel), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            envioEntrada.DefinicionEntrada = (Entrada_Desde_Excel)objeto.ReadObject(leerArchivo);
            leerArchivo.ReadEndElement();
            leerArchivo.ReadEndElement();
            leerArchivo.Close();
            envioEntrada.RutaArchivo = rutaArchivo;
            return envioEntrada;
        }
    }
}
