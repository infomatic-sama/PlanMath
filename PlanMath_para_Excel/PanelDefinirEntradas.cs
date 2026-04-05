using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PlanMath_para_Excel.PlanMath_Excel;

namespace PlanMath_para_Excel
{
    public partial class PanelDefinirEntradas : UserControl
    {
        public bool ModoManual {  get; set; }
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
            if (!Globals.PlanMath_Excel.VerificarURL_Libro_Complemento(InstanciaActual.Libro.FullName))
            {
                lblEnvio.Text = "Guarda primero el libro.";
                lblEnvio.Visible = true;
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

        public void btnNuevaEntrada_Click(object sender, EventArgs e)
        {
            if(InstanciaActual != null)
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
                    nuevaDefinicion.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Excel.ListarArchivosPlanMath();

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
                    nuevaDefinicion.cmbArchivoPlanMath.DataSource = Globals.PlanMath_Excel.ListarArchivosPlanMath();

                    FichasDefinicionesEntradas.Add(nuevaDefinicion);
                }

                //var Sh = Globals.PlanMath_Excel.Application.ActiveSheet;
                //Globals.PlanMath_Excel.ActivarSeleccion(Globals.PlanMath_Excel.Application.ActiveWindow.RangeSelection, ref Sh, Globals.PlanMath_Excel.Application.ActiveWindow.RangeSelection);
            }
        }

        public void EnviarDefinicionEntrada(DefinicionEntradaSeleccion definicionSeleccion)
        {
            if (definicionSeleccion.cmbArchivoPlanMath.SelectedValue == null)
            {
                lblEnvio.Text = "Selecciona un archivo de PlanMath.";
                lblEnvio.Visible = true;
            }
            else
            {
                if (string.IsNullOrEmpty(definicionSeleccion.cmbCalculos.Text))
                {
                    lblEnvio.Text = "Selecciona un cálculo del archivo de PlanMath.";
                    lblEnvio.Visible = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(definicionSeleccion.txtNombreEntrada.Text))
                    {
                        lblEnvio.Text = "Indica un nombre para la variable o vector de entrada de PlanMath.";
                        lblEnvio.Visible = true;
                    }
                    else
                    {
                        if (definicionSeleccion.chkTipoTextosInformacion.Checked && !definicionSeleccion.cmbCalculos.Text.Trim().Replace(" ", string.Empty).ToLower().Equals("entradasgenerales"))
                        {
                            lblEnvio.Text = "Las cadenas de texto sólo se envían a variables o vectores de entradas generales.";
                            lblEnvio.Visible = true;
                        }
                        else
                        {
                            Entradas.Entrada_Desde_Excel nuevaEntrada = new Entradas.Entrada_Desde_Excel();
                            nuevaEntrada.ArchivoPlanMath = (string)definicionSeleccion.cmbArchivoPlanMath.SelectedValue;
                            nuevaEntrada.Celdas = definicionSeleccion.txtCeldas.Text;
                            nuevaEntrada.Hoja = definicionSeleccion.txtHoja.Text;
                            nuevaEntrada.Libro = txtLibro.Text;
                            nuevaEntrada.EsRutaLocal_Excel = !(nuevaEntrada.Libro.Contains("http") | nuevaEntrada.Libro.Contains("https"));
                            nuevaEntrada.URLOffice_Original = InstanciaActual.Libro.FullName;
                            nuevaEntrada.NombreCalculo = (string)definicionSeleccion.cmbCalculos.Text;
                            if (definicionSeleccion.txtTipoEntrada.Text.Equals("Variable de número"))
                                nuevaEntrada.Tipo = Entradas.TipoEntrada.Numero;
                            else if (definicionSeleccion.txtTipoEntrada.Text.Equals("Vector de números"))
                                nuevaEntrada.Tipo = Entradas.TipoEntrada.ConjuntoNumeros;
                            else if (definicionSeleccion.txtTipoEntrada.Text.Equals("Vector de cadenas de texto"))
                                nuevaEntrada.Tipo = Entradas.TipoEntrada.TextosInformacion;
                            nuevaEntrada.Nombre = definicionSeleccion.txtNombreEntrada.Text;
                            nuevaEntrada.ReemplazarCeldas = definicionSeleccion.chkReemplazarCeldasActuales.Checked;

                            foreach (var itemAsignacion in definicionSeleccion.SeleccionInstancia.AsignacionesTextosInformacion)
                            {
                                Entradas.AsignacionTextoInformacion asignacion = new Entradas.AsignacionTextoInformacion();
                                asignacion.CeldaNumero = itemAsignacion.Celda_Numero.Celda.Address;
                                asignacion.CeldaTextoInformacion = itemAsignacion.Celda_TextoInformacion.Celda.Address;
                                nuevaEntrada.Asignaciones_TextosInformacion.Add(asignacion);
                            }

                            Globals.PlanMath_Excel.EnviarEntrada(nuevaEntrada);
                            lblEnvio.Text = "Se ha enviado la variable o vector de entrada a PlanMath.";
                            iconoEnvioOk.Visible = true;
                            lblEnvio.Visible = true;
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
                lblEnvio.Visible = true;
            }
            else
            {
                if (string.IsNullOrEmpty(definicionSeleccion.cmbCalculos.Text))
                {
                    lblEnvio.Text = "Selecciona un cálculo del archivo de PlanMath.";
                    lblEnvio.Visible = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(definicionSeleccion.txtNombreEntrada.Text))
                    {
                        lblEnvio.Text = "Indica un nombre para la variable o vector de entrada de PlanMath.";
                        lblEnvio.Visible = true;
                    }
                    else
                    {                        
                        Entradas.Entrada_Desde_Excel nuevaEntrada = new Entradas.Entrada_Desde_Excel();
                        nuevaEntrada.EntradaManual = true;
                        nuevaEntrada.ArchivoPlanMath = (string)definicionSeleccion.cmbArchivoPlanMath.SelectedValue;
                        nuevaEntrada.Libro = txtLibro.Text;
                        nuevaEntrada.EsRutaLocal_Excel = !(nuevaEntrada.Libro.Contains("http") | nuevaEntrada.Libro.Contains("https"));
                        nuevaEntrada.URLOffice_Original = InstanciaActual.Libro.FullName;
                        nuevaEntrada.NombreCalculo = (string)definicionSeleccion.cmbCalculos.Text;
                        nuevaEntrada.Nombre = definicionSeleccion.txtNombreEntrada.Text;
                        nuevaEntrada.ReemplazarCeldas = definicionSeleccion.chkReemplazarCeldasActuales.Checked;
                        
                        if(definicionSeleccion.cmbTipo.SelectedIndex == 0)
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

                        Globals.PlanMath_Excel.EnviarEntrada(nuevaEntrada);
                        lblEnvio.Text = "Se ha enviado la variable o vector de entrada a PlanMath.";
                        iconoEnvioOk.Visible = true;
                        lblEnvio.Visible = true;                        
                    }
                }
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
            if(selecciones.TabCount == 0)
                btnNuevaEntrada_Click(sender, e);
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

        private void PanelDefinirEntradas_Load(object sender, EventArgs e)
        {
            
        }

        public void ListarFichas()
        {
            selecciones.TabPages.Clear();

            if (InstanciaActual != null)
            {
                if (ModoManual)
                {
                    foreach(var item in FichasDefinicionesEntradasManuales)
                    {
                        TabPage nuevaPestaña = new TabPage();
                        nuevaPestaña.Controls.Add(item);
                        selecciones.TabPages.Add(nuevaPestaña);
                        nuevaPestaña.Text = (string.IsNullOrEmpty(item.txtNombreEntrada.Text)) ?
                            "Varible o vector de entrada manual " + selecciones.TabPages.Count : item.txtNombreEntrada.Text;
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
            if (Globals.PlanMath_Excel.EsModoOscuro())
            {
                Globals.PlanMath_Excel.ColorearControles(this);
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
