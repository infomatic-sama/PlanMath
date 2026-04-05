using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using static PlanMath_para_Word.PlanMath_Word;

namespace PlanMath_para_Word
{
    public class Entradas
    {
        public class Entrada_Desde_Word
        {
            public string RutaDocumento { get; set; }
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
            public bool EsRutaLocal_Word { get; set; }
            public string URLOffice_Original { get; set; }
            public List<TextoBusqueda_DocumentoWord> TextosBusqueda { get; set; }
            public bool EntradaManual { get; set; }
            public bool ReemplazarLecturasArchivos { get; set; }
            public Entrada_Desde_Word()
            {
                Tipo = TipoEntrada.Numero;
                TextosBusqueda = new List<TextoBusqueda_DocumentoWord>();
                URLOffice_Original = string.Empty;
                nom = string.Empty;
            }
        }
        public static void MostrarPanel_DefinirEntradas(bool modoManual = false)
        {
            try
            {
                if (Globals.PlanMath_Word.Application.ActiveDocument != null)
                    Globals.PlanMath_Word.AgregarInstancia(Globals.PlanMath_Word.Application.ActiveDocument);

                if (InstanciaActual == null && Globals.PlanMath_Word.Application.ActiveDocument != null)
                {
                    Globals.PlanMath_Word.Application_WindowActivate(Globals.PlanMath_Word.Application.ActiveDocument,
                        Globals.PlanMath_Word.Application.ActiveWindow);
                }

                if (InstanciaActual != null)
                {
                    if (InstanciaActual.contenidoPanelDefinirEntradas == null)
                    {
                        InstanciaActual.contenidoPanelDefinirEntradas = new PanelDefinirEntradas();
                    }

                    InstanciaActual.contenidoPanelDefinirEntradas.ModoManual = modoManual;

                    if (!Globals.PlanMath_Word.CustomTaskPanes.Contains(InstanciaActual.panelDefinirEntradas))
                    {
                        InstanciaActual.panelDefinirEntradas = Globals.PlanMath_Word.CustomTaskPanes.Add(InstanciaActual.contenidoPanelDefinirEntradas,
                            "Definir variables o vectores de entradas para PlanMath");
                        InstanciaActual.panelDefinirEntradas.Width = 870;
                        //InstanciaActual.contenidoPanelDefinirEntradas.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Word.ListarArchivosPlanMath();
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
                if (Globals.PlanMath_Word.Application.ActiveDocument != null)
                    Globals.PlanMath_Word.AgregarInstancia(Globals.PlanMath_Word.Application.ActiveDocument);

                if (InstanciaActual == null && Globals.PlanMath_Word.Application.ActiveDocument != null)
                {
                    Globals.PlanMath_Word.Application_WindowActivate(Globals.PlanMath_Word.Application.ActiveDocument,
                        Globals.PlanMath_Word.Application.ActiveWindow);
                }

                if (InstanciaActual != null)
                {
                    if (InstanciaActual.contenidoPanelEnviarEntradas == null)
                    {
                        InstanciaActual.contenidoPanelEnviarEntradas = new PanelEnviarEntradas();
                        InstanciaActual.SeleccionesInstancias.Add(new SeleccionInstancia());
                        InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia = InstanciaActual.SeleccionesInstancias.Last();
                    }

                    InstanciaActual.contenidoPanelEnviarEntradas.ModoManual = modoManual;

                    if (!Globals.PlanMath_Word.CustomTaskPanes.Contains(InstanciaActual.panelEnviarEntradas))
                    {
                        InstanciaActual.panelEnviarEntradas = Globals.PlanMath_Word.CustomTaskPanes.Add(InstanciaActual.contenidoPanelEnviarEntradas,
                            "Enviar variables o vectores de entradas a una ejecución de PlanMath");
                        InstanciaActual.panelEnviarEntradas.Width = 600;
                        //Globals.PlanMath_Excel.CargarOpciones_PorDefecto();
                        //InstanciaActual.contenidoPanelDefinirEntradas.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Excel.ListarArchivosPlanMath();
                        InstanciaActual.contenidoPanelEnviarEntradas.lblEnvio.Text = string.Empty;
                        InstanciaActual.contenidoPanelEnviarEntradas.iconoEnvioOk.Visible = false;
                    }

                    if (Ejecuciones_EnvioEntradas.Any())
                    {
                        //InstanciaActual.contenidoPanelEnviarEntradas.IndiceEjecucionSeleccionada = 0;
                        //InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = Ejecuciones_EnvioEntradas;
                        //InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.SelectedIndex = 0;

                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = null;
                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";

                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.DataSource = PlanMath_Word.Ejecuciones_EnvioEntradas;

                        InstanciaActual.contenidoPanelEnviarEntradas.IndiceEjecucionSeleccionada = PlanMath_Word.Ejecuciones_EnvioEntradas.Count - 1;
                        InstanciaActual.contenidoPanelEnviarEntradas.ejecucionSeleccionada.SelectedIndex = InstanciaActual.contenidoPanelEnviarEntradas.IndiceEjecucionSeleccionada;
                    }

                    MostrarSeleccion_EnviarEntradas();
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
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtPagina.Text = string.Empty;
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtLinea.Text = string.Empty;
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.textoActual.Text = string.Empty;

                foreach (var itemSeleccion in InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.SeleccionesTextos)
                {
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtPagina.Text += itemSeleccion.NumeroPagina.ToString() + " , ";
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtLinea.Text += itemSeleccion.NumeroFila.ToString() + " , ";
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.textoActual.Text += itemSeleccion.TextoActual + "\t";
                }

                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtPagina.Text = InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtPagina.Text.Remove(
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtPagina.Text.Length - 2);
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtLinea.Text = InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtLinea.Text.Remove(
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.txtLinea.Text.Length - 2);
                InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.textoActual.Text = InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.textoActual.Text.Remove(
                    InstanciaActual.contenidoPanelDefinirEntradas.PanelSeleccionActual.textoActual.Text.Length - 1);

            }
            catch (Exception) { }
        }

        public static void MostrarSeleccion_EnviarEntradas()
        {
            try
            {
                InstanciaActual.contenidoPanelEnviarEntradas.txtPagina.Text = string.Empty;
                InstanciaActual.contenidoPanelEnviarEntradas.txtLinea.Text = string.Empty;
                InstanciaActual.contenidoPanelEnviarEntradas.textoActual.Text = string.Empty;
                                
                foreach (var itemSeleccion in InstanciaActual.contenidoPanelEnviarEntradas.SeleccionInstancia.SeleccionesTextos)
                {
                    InstanciaActual.contenidoPanelEnviarEntradas.txtPagina.Text += itemSeleccion.NumeroPagina.ToString() + " , ";
                    InstanciaActual.contenidoPanelEnviarEntradas.txtLinea.Text += itemSeleccion.NumeroFila.ToString() + " , ";
                    InstanciaActual.contenidoPanelEnviarEntradas.textoActual.Text += itemSeleccion.TextoActual + "\t";
                }
                                
                InstanciaActual.contenidoPanelEnviarEntradas.txtPagina.Text = InstanciaActual.contenidoPanelEnviarEntradas.txtPagina.Text.Remove(
                    InstanciaActual.contenidoPanelEnviarEntradas.txtPagina.Text.Length - 2);
                InstanciaActual.contenidoPanelEnviarEntradas.txtLinea.Text = InstanciaActual.contenidoPanelEnviarEntradas.txtLinea.Text.Remove(
                    InstanciaActual.contenidoPanelEnviarEntradas.txtLinea.Text.Length - 2);
                InstanciaActual.contenidoPanelEnviarEntradas.textoActual.Text = InstanciaActual.contenidoPanelEnviarEntradas.textoActual.Text.Remove(
                    InstanciaActual.contenidoPanelEnviarEntradas.textoActual.Text.Length - 1);

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
                                string documento = string.Empty;
                                if (!string.IsNullOrEmpty(Globals.PlanMath_Word.NombreDocumentoActivo))
                                {
                                    documento = Globals.PlanMath_Word.NombreDocumentoActivo;
                                }

                                if (envioEntrada.DefinicionEntrada.URLOffice_Original.Trim().ToLower() ==documento.Trim().ToLower())
                                {
                                    if (!Ejecuciones_EnvioEntradas.Any((e) => e.ID_Ejecucion == envioEntrada.ID_Ejecucion))
                                    {
                                        Ejecuciones_EnvioEntradas.Add(envioEntrada);

                                        if (!ejecucionesAgregadas)
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
                                            Globals.PlanMath_Word.CustomTaskPanes.Contains(InstanciaActual.panelEnviarEntradas) &&
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
            DataContractSerializer objeto = new DataContractSerializer(typeof(Entrada_Desde_Word), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            envioEntrada.DefinicionEntrada = (Entrada_Desde_Word)objeto.ReadObject(leerArchivo);
            leerArchivo.ReadEndElement();
            leerArchivo.ReadEndElement();
            leerArchivo.Close();
            envioEntrada.RutaArchivo = rutaArchivo;
            return envioEntrada;
        }

        public class EnvioEntradaDesdeEjecucion
        {
            public string ID_Ejecucion { get; set; }
            public string Texto_Ejecucion { get; set; }
            public Entrada_Desde_Word DefinicionEntrada { get; set; }
            public string RutaArchivo { get; set; }
        }

        public enum TipoEntrada
        {
            Numero = 1,
            ConjuntoNumeros = 2,
            TextosInformacion = 3
        }

        public class TextoBusqueda_DocumentoWord
        {
            public long NumeroPagina { get; set; }
            public long NumeroFila { get; set; }
            public int CaracterInicial { get; set; }
            public int CaracterFinal { get; set; }
        }
    }
}
