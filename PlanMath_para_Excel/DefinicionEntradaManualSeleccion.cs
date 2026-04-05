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
    public partial class DefinicionEntradaManualSeleccion : UserControl
    {
        public PanelDefinirEntradas panelDefinicionEntradas { get; set; }
        public DefinicionEntradaManualSeleccion()
        {
            InitializeComponent();
            cmbTipo.SelectedIndex = 0;
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

        private void btnDefinirEntrada_Click(object sender, EventArgs e)
        {
            panelDefinicionEntradas.EnviarDefinicionEntradaManual(this);
        }

        private void txtNombreEntrada_TextChanged(object sender, EventArgs e)
        {
            panelDefinicionEntradas.selecciones.SelectedTab.Text = txtNombreEntrada.Text;
        }

        private void DefinicionEntradaManualSeleccion_Load(object sender, EventArgs e)
        {
            if (Globals.PlanMath_Excel.EsModoOscuro())
            {
                Globals.PlanMath_Excel.ColorearControles(this);
            }
        }
    }
}
