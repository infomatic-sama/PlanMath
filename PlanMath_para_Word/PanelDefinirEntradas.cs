using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PlanMath_para_Word.PlanMath_Word;

namespace PlanMath_para_Word
{
    public partial class PanelDefinirEntradas : UserControl
    {
        public bool ModoManual { get; set; }
        public DefinicionEntradaSeleccion PanelSeleccionActual
        {
            get
            {
                return (DefinicionEntradaSeleccion)selecciones.TabPages[selecciones.SelectedIndex].Controls[0];
            }
        }
        public DefinicionEntradaManualSeleccion PanelSeleccionActual_Manual
        {
            get
            {
                return (DefinicionEntradaManualSeleccion)selecciones.TabPages[selecciones.SelectedIndex].Controls[0];
            }
        }

        public SeleccionInstancia SeleccionActual
        {
            get
            {
                return (SeleccionInstancia)((DefinicionEntradaSeleccion)selecciones.TabPages[selecciones.SelectedIndex].Controls[0]).SeleccionInstancia;
            }
        }
        public List<DefinicionEntradaSeleccion> FichasDefinicionesEntradas { get; set; }
        public List<DefinicionEntradaManualSeleccion> FichasDefinicionesEntradasManuales { get; set; }
        public PanelDefinirEntradas()
        {
            InitializeComponent();
            FichasDefinicionesEntradas = new List<DefinicionEntradaSeleccion>();
            FichasDefinicionesEntradasManuales = new List<DefinicionEntradaManualSeleccion>();
        }

        private void btnDefinirEntrada_Click(object sender, EventArgs e)
        {
            lblEnvio.Visible = true;

            if (!Globals.PlanMath_Word.VerificarURL_Documento_Complemento(InstanciaActual.Documento.FullName))
            {
                lblEnvio.Text = "Guarda primero el documento.";
                iconoEnvioOk.Visible = false;
            }
            else
            {
                foreach (var seleccion in selecciones.TabPages)
                {
                    if (ModoManual)
                    {
                        DefinicionEntradaManualSeleccion definicionSeleccion = (DefinicionEntradaManualSeleccion)((TabPage)seleccion).Controls[0];
                        EnviarDefinicionEntradaManual(definicionSeleccion);
                    }
                    else
                    {
                        DefinicionEntradaSeleccion definicionSeleccion = (DefinicionEntradaSeleccion)((TabPage)seleccion).Controls[0];
                        EnviarDefinicionEntrada(definicionSeleccion);
                    }
                }
            }
        }

        private void PanelDefinirEntradas_Load(object sender, EventArgs e)
        {
            
        }

        public void EnviarDefinicionEntrada(DefinicionEntradaSeleccion definicionSeleccion)
        {
            if (definicionSeleccion.cmbArchivoPlanMath.SelectedValue == null)
            {
                lblEnvio.Text = "Selecciona un archivo de PlanMath.";
            }
            else
            {
                if (string.IsNullOrEmpty(definicionSeleccion.cmbCalculos.Text))
                {
                    lblEnvio.Text = "Selecciona un cálculo del archivo de PlanMath.";
                }
                else
                {
                    if (string.IsNullOrEmpty(definicionSeleccion.txtNombreEntrada.Text))
                    {
                        lblEnvio.Text = "Indica un nombre para la variable o vector de entrada de PlanMath.";
                    }
                    else
                    {
                        if (definicionSeleccion.chkTipoTextosInformacion.Checked && !definicionSeleccion.cmbCalculos.Text.Trim().Replace(" ", string.Empty).ToLower().Equals("entradasgenerales"))
                        {
                            lblEnvio.Text = "Las cadenas de texto sólo se envían a variables o vectores de entradas generales.";
                        }
                        else
                        {
                            Entradas.Entrada_Desde_Word nuevaEntrada = new Entradas.Entrada_Desde_Word();
                            nuevaEntrada.ArchivoPlanMath = (string)definicionSeleccion.cmbArchivoPlanMath.SelectedValue;
                            nuevaEntrada.RutaDocumento = InstanciaActual.Documento.FullName;
                            nuevaEntrada.EsRutaLocal_Word = !(nuevaEntrada.RutaDocumento.Contains("http") | nuevaEntrada.RutaDocumento.Contains("https"));
                            nuevaEntrada.URLOffice_Original = InstanciaActual.Documento.FullName;
                            nuevaEntrada.NombreCalculo = (string)definicionSeleccion.cmbCalculos.Text;
                            nuevaEntrada.Tipo = definicionSeleccion.SeleccionInstancia.EntradaActual.Tipo;
                            nuevaEntrada.Nombre = definicionSeleccion.txtNombreEntrada.Text;
                            nuevaEntrada.ReemplazarLecturasArchivos = definicionSeleccion.chkReemplazarLecturasArchivos.Checked;

                            foreach (var itemTexto in definicionSeleccion.SeleccionInstancia.TextosBusqueda)
                            {
                                Entradas.TextoBusqueda_DocumentoWord texto = new Entradas.TextoBusqueda_DocumentoWord();
                                texto.NumeroFila = itemTexto.NumeroFila;
                                texto.NumeroPagina = itemTexto.NumeroPagina;
                                texto.CaracterInicial = itemTexto.CaracterInicial;
                                texto.CaracterFinal = itemTexto.CaracterFinal;
                                nuevaEntrada.TextosBusqueda.Add(texto);
                            }

                            Globals.PlanMath_Word.EnviarEntrada(nuevaEntrada);
                            lblEnvio.Text = "Se ha enviado la variable o vector de entrada a PlanMath.";
                            iconoEnvioOk.Visible = true;
                        }
                    }
                }
            }
        }

        public void EnviarDefinicionEntradaManual(DefinicionEntradaManualSeleccion definicionSeleccion)
        {
            if (definicionSeleccion.cmbArchivoPlanMath.SelectedValue == null)
            {
                lblEnvio.Text = "Selecciona un archivo de PlanMath.";
            }
            else
            {
                if (string.IsNullOrEmpty(definicionSeleccion.cmbCalculos.Text))
                {
                    lblEnvio.Text = "Selecciona un cálculo del archivo de PlanMath.";
                }
                else
                {
                    if (string.IsNullOrEmpty(definicionSeleccion.txtNombreEntrada.Text))
                    {
                        lblEnvio.Text = "Indica un nombre para la variable o vector de entrada de PlanMath.";
                    }
                    else
                    {                        
                        Entradas.Entrada_Desde_Word nuevaEntrada = new Entradas.Entrada_Desde_Word();
                        nuevaEntrada.EntradaManual = true;
                        nuevaEntrada.ArchivoPlanMath = (string)definicionSeleccion.cmbArchivoPlanMath.SelectedValue;
                        nuevaEntrada.RutaDocumento = InstanciaActual.Documento.FullName;
                        nuevaEntrada.EsRutaLocal_Word = !(nuevaEntrada.RutaDocumento.Contains("http") | nuevaEntrada.RutaDocumento.Contains("https"));
                        nuevaEntrada.URLOffice_Original = InstanciaActual.Documento.FullName;
                        nuevaEntrada.NombreCalculo = (string)definicionSeleccion.cmbCalculos.Text;
                        nuevaEntrada.Nombre = definicionSeleccion.txtNombreEntrada.Text;
                        nuevaEntrada.ReemplazarLecturasArchivos = definicionSeleccion.chkReemplazarLecturasArchivos.Checked;

                        if (definicionSeleccion.cmbTipo.SelectedIndex == 0)
                        {
                            nuevaEntrada.Tipo = Entradas.TipoEntrada.Numero;
                        }
                        else if (definicionSeleccion.cmbTipo.SelectedIndex == 1)
                        {
                            nuevaEntrada.Tipo = Entradas.TipoEntrada.ConjuntoNumeros;
                        }
                        else if (definicionSeleccion.cmbTipo.SelectedIndex == 2)
                        {
                            nuevaEntrada.Tipo = Entradas.TipoEntrada.TextosInformacion;
                        }

                        Globals.PlanMath_Word.EnviarEntrada(nuevaEntrada);
                        lblEnvio.Text = "Se ha enviado la variable o vector de entrada a PlanMath.";
                        iconoEnvioOk.Visible = true;                        
                    }
                }
            }
        }

        private void btnQuitarTodasDefiniciones_Click(object sender, EventArgs e)
        {
            if (InstanciaActual != null)
            {
                if (ModoManual)
                {
                    FichasDefinicionesEntradasManuales.Clear();
                }
                else
                {
                    FichasDefinicionesEntradas.Clear();
                }
            }

            foreach (TabPage seleccion in selecciones.TabPages)
            {
                selecciones.TabPages.Remove(seleccion);
            }

            if (selecciones.TabCount == 0)
                btnNuevaEntrada_Click(sender, e);
        }

        public void btnNuevaEntrada_Click(object sender, EventArgs e)
        {
            if (InstanciaActual != null)
            {
                if (ModoManual)
                {
                    DefinicionEntradaManualSeleccion nuevaDefinicion = new DefinicionEntradaManualSeleccion();
                    nuevaDefinicion.panelDefinicionEntradas = this;
                    nuevaDefinicion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                    TabPage nuevaPestaña = new TabPage();
                    nuevaPestaña.Controls.Add(nuevaDefinicion);
                    selecciones.TabPages.Add(nuevaPestaña);
                    nuevaPestaña.Text = "Variable o vector de entrada manual " + selecciones.TabPages.Count;
                    nuevaDefinicion.Size = nuevaPestaña.Size;
                    selecciones.SelectedIndex = selecciones.TabPages.Count - 1;
                    nuevaDefinicion.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Word.ListarArchivosPlanMath();

                    FichasDefinicionesEntradasManuales.Add(nuevaDefinicion);
                }
                else
                {
                    InstanciaActual.SeleccionesInstancias.Add(new SeleccionInstancia());

                    DefinicionEntradaSeleccion nuevaDefinicion = new DefinicionEntradaSeleccion();
                    nuevaDefinicion.SeleccionInstancia = InstanciaActual.SeleccionesInstancias.Last();
                    nuevaDefinicion.panelDefinicionEntradas = this;
                    nuevaDefinicion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                    TabPage nuevaPestaña = new TabPage();
                    nuevaPestaña.Text = nuevaDefinicion.SeleccionInstancia.EntradaActual.Nombre;
                    nuevaPestaña.Controls.Add(nuevaDefinicion);
                    selecciones.TabPages.Add(nuevaPestaña);
                    nuevaDefinicion.Size = nuevaPestaña.Size;
                    selecciones.SelectedIndex = selecciones.TabPages.Count - 1;
                    nuevaDefinicion.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Word.ListarArchivosPlanMath();

                    FichasDefinicionesEntradas.Add(nuevaDefinicion);
                }

                //var Sh = Globals.PlanMath_Excel.Application.ActiveSheet;
                //Globals.PlanMath_Excel.ActivarSeleccion(Globals.PlanMath_Excel.Application.ActiveWindow.RangeSelection, ref Sh, Globals.PlanMath_Excel.Application.ActiveWindow.RangeSelection);
            }
        }

        private void btnQuitarDefinicionEntrada_Click(object sender, EventArgs e)
        {
            if (InstanciaActual != null)
            {
                if (ModoManual)
                {
                    FichasDefinicionesEntradasManuales.Remove((DefinicionEntradaManualSeleccion)(selecciones.SelectedTab.Controls[0]));
                }
                else
                {
                    FichasDefinicionesEntradas.Remove((DefinicionEntradaSeleccion)(selecciones.SelectedTab.Controls[0]));
                }
            }

            selecciones.TabPages.Remove(selecciones.SelectedTab);
            if (selecciones.TabCount == 0)
                btnNuevaEntrada_Click(sender, e);
        }

        public void ListarFichas()
        {
            selecciones.TabPages.Clear();

            if (InstanciaActual != null)
            {
                if (ModoManual)
                {
                    foreach (var item in FichasDefinicionesEntradasManuales)
                    {
                        TabPage nuevaPestaña = new TabPage();
                        nuevaPestaña.Controls.Add(item);
                        selecciones.TabPages.Add(nuevaPestaña);
                        nuevaPestaña.Text = (string.IsNullOrEmpty(item.txtNombreEntrada.Text)) ?
                            "Variable o vector de entrada manual " + selecciones.TabPages.Count : item.txtNombreEntrada.Text;
                        item.Size = nuevaPestaña.Size;
                    }
                }
                else
                {
                    foreach (var item in FichasDefinicionesEntradas)
                    {
                        TabPage nuevaPestaña = new TabPage();
                        nuevaPestaña.Controls.Add(item);
                        selecciones.TabPages.Add(nuevaPestaña);
                        nuevaPestaña.Text = nuevaPestaña.Text = (string.IsNullOrEmpty(item.txtNombreEntrada.Text)) ?
                            item.panelDefinicionEntradas.SeleccionActual.EntradaActual.Nombre : item.txtNombreEntrada.Text;
                        item.Size = nuevaPestaña.Size;
                    }
                }
            }

            selecciones.SelectedIndex = 0;
        }

        public void AplicarModoOscuro_SiCorresponde()
        {
            if (Globals.PlanMath_Word.EsModoOscuro())
            {
                Globals.PlanMath_Word.ColorearControles(this);
            }
        }

        public void AgregarPrimeraFicha()
        {
            if(Visible)
            {
                if ((ModoManual &&
                        !FichasDefinicionesEntradasManuales.Any()) ||
                        (!ModoManual &&
                        !FichasDefinicionesEntradas.Any()))
                {
                    btnNuevaEntrada_Click(null, null);
                }
            }
        }
    }
}
