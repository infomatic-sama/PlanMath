using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PlanMath_para_Excel.PlanMath_Excel;

namespace PlanMath_para_Excel
{
    public partial class DefinicionEntradaSeleccion : UserControl
    {
        public PanelDefinirEntradas panelDefinicionEntradas {  get; set; }
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
                Globals.PlanMath_Excel.ArchivosPlanMath.Add(buscarArchivo.FileName);
                cmbArchivoPlanMath.DataSource = Globals.PlanMath_Excel.ListarArchivosPlanMath();
                cmbArchivoPlanMath.SelectedIndex = cmbArchivoPlanMath.Items.Count - 1;
            }
        }

        private void btnQuitarArchivo_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;

            if (cmbArchivoPlanMath.SelectedIndex > -1)
            {
                Globals.PlanMath_Excel.ArchivosPlanMath.RemoveAt(cmbArchivoPlanMath.SelectedIndex);
                cmbArchivoPlanMath.DataSource = Globals.PlanMath_Excel.ListarArchivosPlanMath();
                cmbArchivoPlanMath.SelectedIndex = -1;
                cmbArchivoPlanMath.Text = string.Empty;
            }
        }

        private void AdjustWidthComboBox_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
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
            cmbCalculos.DataSource = Globals.PlanMath_Excel.ListarCalculos_ArchivoPlanMath((string)cmbArchivoPlanMath.SelectedValue);
        }



        private void cmbArchivoPlanMath_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;
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

        private void btnAsignarTextoInformacionNumero_Click(object sender, EventArgs e)
        {
            PlanMath_Excel.AgregarAsignacionTextoInformacion((TextosInformacionCelda)listaCeldasTextosInformacion.SelectedItem,
                (TextosInformacionCelda)listaCeldasNumeros.SelectedItem);

            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;
        }

        private void btnAsignacionAutomatica_Click(object sender, EventArgs e)
        {
            AsignacionAutomatica_TextosInformacion asignar = new AsignacionAutomatica_TextosInformacion();
            asignar.Celdas = SeleccionInstancia.Celdas;
            asignar.Hoja = SeleccionInstancia.Hoja;
            asignar.Asignaciones.AddRange(SeleccionInstancia.AsignacionesTextosInformacion);

            foreach (var numero in SeleccionInstancia.NumerosSeleccionados)
            {
                asignar.NumerosSeleccionados.Add(numero);
            }

            asignar.ShowDialog();

            if (asignar.Asignaciones.Any())
            {
                SeleccionInstancia.AsignacionesTextosInformacion.Clear();
                listaAsignacionesTextosInformacion.Items.Clear();
                AsignacionAutomatica(asignar.Asignaciones);
            }
        }

        private void AsignacionAutomatica(List<AsignacionTextoInformacion_Numero_Instancia> asignaciones)
        {
            foreach (var itemAsignacion in asignaciones)
            {
                PlanMath_Excel.AgregarAsignacionTextoInformacion(itemAsignacion.Celda_TextoInformacion,
                itemAsignacion.Celda_Numero);
            }
        }

        private void chkTipoTextosInformacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTipoTextosInformacion.Checked)
            {
                SeleccionInstancia.EntradaActual.Tipo = Entradas.TipoEntrada.TextosInformacion;
                cmbCalculos.Text = "Variables o vectores de entradas generales";
                cmbCalculos.Enabled = false;
            }
            else
            {
                if (SeleccionInstancia.Celdas != null && 
                        SeleccionInstancia.Celdas.Count == 1)
                {
                    SeleccionInstancia.EntradaActual.Tipo = Entradas.TipoEntrada.Numero;
                }
                else
                    SeleccionInstancia.EntradaActual.Tipo = Entradas.TipoEntrada.ConjuntoNumeros;

                cmbCalculos.Text = string.Empty;
                cmbCalculos.Enabled = true;
            }

            Entradas.MostrarSeleccion_DefinirEntradas();
        }

        private void btnQuitarAsignaciones_Click(object sender, EventArgs e)
        {
            if (listaAsignacionesTextosInformacion.SelectedIndices.Count >= 1)
            {
                var celdaTextoInformacion = InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.ElementAt(
                    listaAsignacionesTextosInformacion.SelectedIndices[0]).Celda_TextoInformacion;

                var celdaNumero = InstanciaActual.contenidoPanelDefinirEntradas.SeleccionActual.AsignacionesTextosInformacion.ElementAt(
                    listaAsignacionesTextosInformacion.SelectedIndices[0]).Celda_Numero;

                PlanMath_Excel.QuitarAsignacionTextoInformacion(celdaTextoInformacion, celdaNumero);

                panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                panelDefinicionEntradas.lblEnvio.Visible = false;
            }
        }

        private void btnEsTexto_Click(object sender, EventArgs e)
        {
            if (listaCeldasNumeros.SelectedItem != null)
            {
                TextosInformacionCelda texto = new TextosInformacionCelda();
                texto.Celda = SeleccionInstancia.NumerosSeleccionados[listaCeldasNumeros.SelectedIndex].Celda;
                SeleccionInstancia.TextosInformacion.Add(texto);

                SeleccionInstancia.NumerosSeleccionados.RemoveAt(listaCeldasNumeros.SelectedIndex);

                panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                panelDefinicionEntradas.lblEnvio.Visible = false;
            }
        }

        private void btnLimpiarAsignaciones_Click(object sender, EventArgs e)
        {
            PlanMath_Excel.LimpiarAsignaciones();

            panelDefinicionEntradas.iconoEnvioOk.Visible = false;
            panelDefinicionEntradas.lblEnvio.Visible = false;
        }

        private void btnSeleccionMultipleNumeros_Click(object sender, EventArgs e)
        {
            EstablecerNumeroTextos establecer = new EstablecerNumeroTextos();
            establecer.Celdas = SeleccionInstancia.Celdas;
            establecer.Hoja = SeleccionInstancia.Hoja;

            foreach (var numero in SeleccionInstancia.NumerosSeleccionados)
            {
                establecer.NumerosSeleccionados.Add(numero);
            }
            //establecer.ColumnaNumero = InstanciaActual.ColumnaNumero;
            //establecer.ColumnasNumeros = InstanciaActual.ColumnasNumeros;
            establecer.ShowDialog();

            if (establecer.DialogResult == DialogResult.OK)
            {
                btnLimpiarAsignaciones_Click(this, e);

                //InstanciaActual.NumerosSeleccionados.Clear();
                //foreach (var numero in establecer.NumerosSeleccionados)
                //{
                //    InstanciaActual.NumerosSeleccionados.Add(numero);
                //}
                for (int indice = 0; indice < listaCeldasNumeros.Items.Count; indice++)
                {
                    if (!establecer.NumerosSeleccionados.Any(i => i.Celda.Address == SeleccionInstancia.NumerosSeleccionados[indice].Celda.Address))
                    {
                        TextosInformacionCelda texto = new TextosInformacionCelda();
                        texto.Celda = SeleccionInstancia.NumerosSeleccionados[indice].Celda;
                        SeleccionInstancia.TextosInformacion.Add(texto);

                        SeleccionInstancia.NumerosSeleccionados.RemoveAt(indice);
                        indice--;
                    }
                }

                for (int indice = 0; indice < listaCeldasTextosInformacion.Items.Count; indice++)
                {
                    if (establecer.NumerosSeleccionados.Any(i => i.Celda.Address == SeleccionInstancia.TextosInformacion[indice].Celda.Address))
                    {
                        TextosInformacionCelda texto = new TextosInformacionCelda();
                        texto.Celda = SeleccionInstancia.TextosInformacion[indice].Celda;
                        SeleccionInstancia.NumerosSeleccionados.Add(texto);

                        SeleccionInstancia.TextosInformacion.RemoveAt(indice);
                        indice--;
                    }
                }

                panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                panelDefinicionEntradas.lblEnvio.Visible = false;
            }
            //InstanciaActual.ColumnaNumero = establecer.ColumnaNumero;
            //InstanciaActual.ColumnasNumeros.Clear();
        }

        private void btnDefinirEntrada_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.EnviarDefinicionEntrada(this);
        }

        private void txtNombreEntrada_TextChanged(object sender, EventArgs e)
        {
            panelDefinicionEntradas.selecciones.SelectedTab.Text = txtNombreEntrada.Text;
        }

        private void btnActivarDesactivarSeleccionandoCeldas_Click(object sender, EventArgs e)
        {
            if(!SeleccionInstancia.SeleccionandoCeldas)
            {
                SeleccionInstancia.SeleccionandoCeldas = true;
                btnActivarDesactivarSeleccionandoCeldas.Text = "Seleccionando celdas...\nDetener seleccionar celdas";
                btnActivarDesactivarSeleccionandoCeldas.Image = global::PlanMath_para_Excel.Properties.Resources._04;
                Globals.PlanMath_Excel.PlanMath_Excel_SheetSelectionChange(
                     Globals.PlanMath_Excel.Application.ActiveSheet,
                      Globals.PlanMath_Excel.Application.Selection);
            }
            else
            {
                SeleccionInstancia.SeleccionandoCeldas = false;
                btnActivarDesactivarSeleccionandoCeldas.Text = "Comenzar a seleccionar celdas";
                btnActivarDesactivarSeleccionandoCeldas.Image = global::PlanMath_para_Excel.Properties.Resources._03;
            }
        }

        private void DefinicionEntradaSeleccion_Load(object sender, EventArgs e)
        {
            if (Globals.PlanMath_Excel.EsModoOscuro())
            {
                Globals.PlanMath_Excel.ColorearControles(this);
            }
        }
    }
}
