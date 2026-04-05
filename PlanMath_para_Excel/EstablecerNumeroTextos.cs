using Microsoft.Office.Interop.Excel;
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
    public partial class EstablecerNumeroTextos : Form
    {
        public Range Celdas { get; set; }
        public Worksheet Hoja { get; set; }
        public List<TextosInformacionCelda> NumerosSeleccionados {  get; set; }
        public EstablecerNumeroTextos()
        {
            NumerosSeleccionados = new List<TextosInformacionCelda>();
            InitializeComponent();
        }

        private void EstablecerNumeroTextos_Load(object sender, EventArgs e)
        {
            if (Globals.PlanMath_Excel.EsModoOscuro())
            {
                Globals.PlanMath_Excel.ColorearControles(this);
            }

            string[] nombresCeldas = Celdas.Address.ToString().Split(',');

            for (int indice = 0; indice < nombresCeldas.Length; indice++)
            {
                var celdaActual = ((Worksheet)Hoja).Range[nombresCeldas[indice]];

                TableLayoutPanel tablaCeldas = new TableLayoutPanel();
                tablaCeldas.AllowDrop = true;
                tablaCeldas.BackColor = SystemColors.Window;
                //tablaCeldas.AutoScroll = true;
                tablaCeldas.AutoSize = true;
                tablaCeldas.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                //tablaCeldas.Paint += tablaCeldas_Paint;

                var celdasValidas = celdaActual;

                int filas = 0;
                int columnas = 0;
                int maxColumns = 0;
                bool primeraFila = true;

                for (filas = 1; filas <= celdasValidas.Rows.Count; filas++)
                {
                    //bool terminar = false;

                    for (columnas = 1; columnas <= celdasValidas.Columns.Count; columnas++)
                    {
                        if (primeraFila)
                        {
                            System.Windows.Forms.Button seleccionarColumna = new System.Windows.Forms.Button();
                            seleccionarColumna.Margin = new Padding(20);
                            seleccionarColumna.Text = "Seleccionar columnna";
                            seleccionarColumna.Click += SeleccionarColumna_Click;

                            seleccionarColumna.TextAlign = ContentAlignment.MiddleCenter;
                            seleccionarColumna.Tag = columnas - 1; //celdaActual[filas, columnas];

                            tablaCeldas.Controls.Add(seleccionarColumna);

                            tablaCeldas.SetRow(seleccionarColumna, filas - 1);
                            tablaCeldas.SetColumn(seleccionarColumna, columnas - 1);
                        }
                        else
                        {
                            string strTexto = string.Empty;
                            System.Windows.Forms.Label textoCelda = new System.Windows.Forms.Label();

                            //if (ColumnasNumeros.Contains(columnas))
                            //{
                            //    textoCelda.BackColor = SystemColors.ControlLightLight;
                            //}

                            if (celdasValidas[filas, columnas].Value != null)
                            {
                                strTexto = celdasValidas[filas, columnas].Value.ToString();

                                textoCelda.AllowDrop = true;
                                textoCelda.Click += TextoCelda_Click;

                                if (columnas > maxColumns)
                                    maxColumns = columnas;
                            }

                            textoCelda.TextAlign = ContentAlignment.MiddleCenter;
                            textoCelda.Tag = celdaActual[filas, columnas];

                            textoCelda.Text = strTexto;
                            textoCelda.Margin = new Padding(40);
                            tablaCeldas.Controls.Add(textoCelda);

                            tablaCeldas.SetRow(textoCelda, filas);
                            tablaCeldas.SetColumn(textoCelda, columnas - 1);
                        }
                    }

                    if (primeraFila)
                    {
                        primeraFila = false;
                        filas--;
                    }
                }

                tablaCeldas.RowCount = filas + 1;
                tablaCeldas.ColumnCount = columnas - 1;
                //}

                tablaCeldas.Tag = celdasValidas;

                foreach (RowStyle estiloFila in tablaCeldas.RowStyles)
                {
                    estiloFila.SizeType = SizeType.AutoSize;
                }

                foreach (ColumnStyle estiloColumna in tablaCeldas.ColumnStyles)
                {
                    estiloColumna.SizeType = SizeType.AutoSize;
                }

                contenedorTablas.TabPages.Add(new TabPage("Selección " + (indice + 1).ToString() + " - " + ((Worksheet)Hoja).Name));
                contenedorTablas.TabPages[contenedorTablas.TabPages.Count - 1].AutoScroll = true;
                contenedorTablas.TabPages[contenedorTablas.TabPages.Count - 1].Controls.Add(tablaCeldas);
            }

            contenedorTablas.SelectedTab = contenedorTablas.TabPages[0];
            PintarColumnas();
        }

        private void SeleccionarColumna_Click(object sender, EventArgs e)
        {
            TableLayoutPanel tabla = (TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0];
            
            for (int fila = 1; fila < tabla.RowCount - 1; fila++)
            {
                int columna = (int)((System.Windows.Forms.Button)sender).Tag;
                System.Windows.Forms.Label celda = (System.Windows.Forms.Label)tabla.GetControlFromPosition(columna, fila);
                TextoCelda_Click(celda, e);
            }
        }

        private void TextoCelda_Click(object sender, EventArgs e)
        {
            if(sender != null)
            {
                if (!NumerosSeleccionados.Any(i => i.Celda.Address == ((Range)((System.Windows.Forms.Label)sender).Tag).Address))
                {
                    if (((Range)((System.Windows.Forms.Label)sender).Tag).Value != null)
                    {
                        string valor = ((Range)((System.Windows.Forms.Label)sender).Tag).Value.ToString();
                        double numero = 0;

                        if (double.TryParse(valor, out numero))
                        {
                            NumerosSeleccionados.Add(new TextosInformacionCelda() { Celda = ((Range)((System.Windows.Forms.Label)sender).Tag) });
                        }
                    }
                }
                else
                {
                    var celda = NumerosSeleccionados.FirstOrDefault(i => i.Celda.Address == ((Range)((System.Windows.Forms.Label)sender).Tag).Address);

                    if(celda != null)
                    {
                        NumerosSeleccionados.Remove(celda);
                    }
                }

                PintarColumnas();
            }
        }

        private void PintarColumnas()
        {
            foreach (TabPage contenedorTabla in contenedorTablas.TabPages)
            {
                TableLayoutPanel tabla = (TableLayoutPanel)contenedorTabla.Controls[0];
                System.Windows.Forms.Label textoCelda = new System.Windows.Forms.Label();

                for (int fila = 0; fila < tabla.RowCount; fila++)
                {
                    for (int columna = 0; columna < tabla.ColumnCount; columna++)
                    {
                        if (tabla.GetControlFromPosition(columna, fila) != null &&
                            tabla.GetControlFromPosition(columna, fila).GetType() == typeof(System.Windows.Forms.Label))
                        {
                            System.Windows.Forms.Label celda = (System.Windows.Forms.Label)tabla.GetControlFromPosition(columna, fila);
                            Range columnaCelda = (Range)celda.Tag;

                            double numero = 0;
                            string valor = string.Empty;

                            if (columnaCelda.Value != null)
                            {
                                valor = columnaCelda.Value.ToString();
                            }

                            bool esNumero = false;

                            if (!string.IsNullOrEmpty(valor) &&
                                double.TryParse(valor, out numero))
                                esNumero = true;

                            if (esNumero &&
                                NumerosSeleccionados.Any(i => i.Celda.Address == columnaCelda.Address))
                            {
                                celda.BackColor = SystemColors.ControlLight;
                                celda.BorderStyle = BorderStyle.FixedSingle;
                            }
                            else
                            {
                                celda.BorderStyle = BorderStyle.None;

                                if (esNumero)
                                    celda.BackColor = textoCelda.BackColor;
                                else
                                    celda.BackColor = SystemColors.ControlLightLight;
                            }
                        }

                        //if (ColumnasNumeros.Contains(columnaCelda))
                        //{
                        //    if(columnaCelda == ColumnaNumero)
                        //        celda.BackColor = SystemColors.ControlLight;
                        //    else
                        //        celda.BackColor = SystemColors.ControlLightLight;
                        //}
                        //else
                        //{
                        //    celda.BackColor = textoCelda.BackColor;
                        //}
                    }
                }
            }
        }

        private void btnAsignarTextosInformacion_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
