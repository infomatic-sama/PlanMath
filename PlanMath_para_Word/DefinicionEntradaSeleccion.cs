using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PlanMath_para_Word.PlanMath_Word;

namespace PlanMath_para_Word
{
    public partial class DefinicionEntradaSeleccion : UserControl
    {
        public PanelDefinirEntradas panelDefinicionEntradas { get; set; }
        public SeleccionInstancia SeleccionInstancia { get; set; }
        public DefinicionEntradaSeleccion()
        {
            InitializeComponent();
        }

        private void btnAgregarArchivo_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;

            DialogResult resp = buscarArchivo.ShowDialog();
            if (resp == DialogResult.OK)
            {
                Globals.PlanMath_Word.ArchivosPlanMath.Add(buscarArchivo.FileName);
                cmbArchivoPlanMath.DataSource = Globals.PlanMath_Word.ListarArchivosPlanMath();
                cmbArchivoPlanMath.SelectedIndex = cmbArchivoPlanMath.Items.Count - 1;
            }
        }

        private void btnQuitarArchivo_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;

            if (cmbArchivoPlanMath.SelectedIndex > -1)
            {
                Globals.PlanMath_Word.ArchivosPlanMath.RemoveAt(cmbArchivoPlanMath.SelectedIndex);
                cmbArchivoPlanMath.DataSource = Globals.PlanMath_Word.ListarArchivosPlanMath();
                cmbArchivoPlanMath.SelectedIndex = -1;
                cmbArchivoPlanMath.Text = string.Empty;
            }
        }

        private void cmbArchivoPlanMath_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;
        }

        private void cmbArchivoPlanMath_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            System.Drawing.Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (RutaArchivoPlanMath s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s.Nombre, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width + 5;
        }

        private void cmbArchivoPlanMath_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCalculos.DataSource = null;
            cmbCalculos.DisplayMember = "Nombre";
            cmbCalculos.ValueMember = "Nombre";
            cmbCalculos.DataSource = Globals.PlanMath_Word.ListarCalculos_ArchivoPlanMath((string)cmbArchivoPlanMath.SelectedValue);
        }

        private void cmbCalculos_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;
        }

        private void txtNombreEntrada_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;
        }

        private void btnIrA_Click(object sender, EventArgs e)
        {
            if (InstanciaActual != null &&
                listaTextos.SelectedItems.Count == 1)
            {
                var texto = (TextoBusqueda_Instancia)listaTextos.SelectedItems[0].Tag;

                object what = WdGoToItem.wdGoToPage;
                object which = WdGoToDirection.wdGoToAbsolute;
                object count = texto.NumeroPagina;
                var range = InstanciaActual.Documento.Content.GoTo(ref what, ref which, ref count);

                object what1 = WdGoToItem.wdGoToLine;
                object which1 = WdGoToDirection.wdGoToAbsolute;
                object count1 = texto.NumeroFila;
                var range1 = range.GoTo(ref what1, ref which1, ref count1);

                range1.SetRange(texto.CaracterInicial, texto.CaracterFinal);
                range1.Select();
            }
        }

        private void agregarTexto_Click(object sender, EventArgs e)
        {
            if (SeleccionInstancia != null)
            {
                foreach (var itemSeleccion in SeleccionInstancia.SeleccionesTextos)
                {
                    var texto = SeleccionInstancia.TextosBusqueda.Where(i => i.CaracterInicial == itemSeleccion.CaracterInicial &&
                    i.CaracterFinal == itemSeleccion.CaracterFinal &&
                    i.NumeroPagina == itemSeleccion.NumeroPagina &&
                    i.NumeroFila == itemSeleccion.NumeroFila).FirstOrDefault();

                    if (texto == null)
                    {
                        SeleccionInstancia.TextosBusqueda.Add(new TextoBusqueda_Instancia()
                        {
                            CaracterFinal = itemSeleccion.CaracterFinal,
                            CaracterInicial = itemSeleccion.CaracterInicial,
                            NumeroFila = itemSeleccion.NumeroFila,
                            NumeroPagina = itemSeleccion.NumeroPagina
                        });

                        string[] subTextos = new string[3];
                        subTextos[0] = itemSeleccion.TextoActual;
                        subTextos[1] = SeleccionInstancia.TextosBusqueda.Last().NumeroPagina.ToString();
                        subTextos[2] = SeleccionInstancia.TextosBusqueda.Last().NumeroFila.ToString();

                        ListViewItem item = new ListViewItem(subTextos);
                        item.Tag = SeleccionInstancia.TextosBusqueda.Last();

                        listaTextos.Items.Add(item);

                        panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                        panelDefinicionEntradas.lblEnvio.Visible = false;
                    }
                }
            }
        }

        private void btnQuitarAsignaciones_Click(object sender, EventArgs e)
        {
            if (SeleccionInstancia != null &&
                listaTextos.SelectedItems.Count == 1)
            {
                var texto = (TextoBusqueda_Instancia)listaTextos.SelectedItems[0].Tag;
                var indice = listaTextos.SelectedItems[0].Index;

                SeleccionInstancia.TextosBusqueda.Remove(texto);
                listaTextos.Items.RemoveAt(indice);

                panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                panelDefinicionEntradas.lblEnvio.Visible = false;
            }
        }

        private void btnLimpiarAsignaciones_Click(object sender, EventArgs e)
        {
            if (SeleccionInstancia != null &&
                listaTextos.SelectedItems.Count > 0)
            {
                SeleccionInstancia.TextosBusqueda.Clear();
                listaTextos.Items.Clear();

                panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                panelDefinicionEntradas.lblEnvio.Visible = false;
            }
        }

        private void chkTipoTextosInformacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTipoTextosInformacion.Checked)
            {
                SeleccionInstancia.EntradaActual.Tipo = Entradas.TipoEntrada.TextosInformacion;
                cmbCalculos.Text = "Variables o vectores de entradas generales";
                cmbCalculos.Enabled = false;
                cmbTipo.Enabled = false;
            }
            else
            {
                cmbTipo_SelectedIndexChanged(this, e);

                cmbCalculos.Text = string.Empty;
                cmbCalculos.Enabled = true;
                cmbTipo.Enabled = true;
            }

            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;

            Entradas.MostrarSeleccion_DefinirEntradas();
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SeleccionInstancia != null)
            {
                if (cmbTipo.SelectedIndex == 0)
                {
                    SeleccionInstancia.EntradaActual.Tipo = Entradas.TipoEntrada.Numero;
                }
                else if (cmbTipo.SelectedIndex == 1)
                    SeleccionInstancia.EntradaActual.Tipo = Entradas.TipoEntrada.ConjuntoNumeros;

                panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                panelDefinicionEntradas.lblEnvio.Visible = false;
            }
        }

        private void DefinicionEntradaSeleccion_Load(object sender, EventArgs e)
        {
            cmbTipo.SelectedIndex = 1;

            if(Globals.PlanMath_Word.EsModoOscuro())
            {
                Globals.PlanMath_Word.ColorearControles(this);
            }
        }

        private void btnDefinirEntrada_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.EnviarDefinicionEntrada(this);
        }

        private void btnActivarDesactivarSeleccionandoTextos_Click(object sender, EventArgs e)
        {
            if (!SeleccionInstancia.SeleccionandoTextos)
            {
                SeleccionInstancia.SeleccionandoTextos = true;
                btnActivarDesactivarSeleccionandoTextos.Text = "Seleccionando textos...\nDetener seleccionar textos";
                btnActivarDesactivarSeleccionandoTextos.Image = global::PlanMath_para_Word.Properties.Resources._02;
                Globals.PlanMath_Word.PlanMath_Word_WindowSelectionChange(
                     Globals.PlanMath_Word.Application.Selection);
            }
            else
            {
                SeleccionInstancia.SeleccionandoTextos = false;
                btnActivarDesactivarSeleccionandoTextos.Text = "Comenzar a seleccionar textos";
                btnActivarDesactivarSeleccionandoTextos.Image = global::PlanMath_para_Word.Properties.Resources._01;
            }
        }
    }
}
