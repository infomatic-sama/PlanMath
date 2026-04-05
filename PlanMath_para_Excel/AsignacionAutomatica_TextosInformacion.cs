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
using Label = System.Windows.Forms.Label;

namespace PlanMath_para_Excel
{
    public partial class AsignacionAutomatica_TextosInformacion : Form
    {
        public List<AsignacionTextoInformacion_Numero_Instancia> Asignaciones { get; set; }
        public Range Celdas { get; set; }
        public Worksheet Hoja { get; set; }
        private bool clicCelda;
        Range PrimeraCelda_Arrastre;
        Range SegundaCelda_Arrastre;
        public List<TextosInformacionCelda> NumerosSeleccionados { get; set; }
        public AsignacionAutomatica_TextosInformacion()
        {
            Asignaciones = new List<AsignacionTextoInformacion_Numero_Instancia>();
            NumerosSeleccionados = new List<TextosInformacionCelda>();
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Asignaciones.Clear();
            Close();
        }

        private void btnAsignarTextosInformacion_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tablaCeldas_Paint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);

            if (Asignaciones.Any())
            {
                foreach (var itemAsignacion in Asignaciones)
                {
                    foreach (Label textoCelda_TextoInformacion in ((TableLayoutPanel)sender).Controls)
                    {
                        //var celdaTextoInformacion = (from C in tablaCeldas.Controls where C.Tag == itemAsignacion.Celda_TextoInformacion select C).First();

                        if (((Range)textoCelda_TextoInformacion.Tag).Address == itemAsignacion.Celda_TextoInformacion.Celda.Address)
                        {
                            //var celdaNumero = (from C in Asignaciones where C.Celda_TextoInformacion == textoCelda.Tag select C.Celda_TextoInformacion).First();

                            foreach (Label textoCelda_Numero in ((TableLayoutPanel)sender).Controls)
                            {
                                if (((Range)textoCelda_Numero.Tag).Address == itemAsignacion.Celda_Numero.Celda.Address)
                                {
                                    Pen pen = new Pen(Brushes.Black);
                                    PointF puntoInicial = new PointF(textoCelda_TextoInformacion.Left + textoCelda_TextoInformacion.Size.Width / 2,
                                        textoCelda_TextoInformacion.Top + textoCelda_TextoInformacion.Size.Height / 2);

                                    PointF puntoFinal = new PointF(textoCelda_Numero.Left + textoCelda_Numero.Size.Width / 2,
                                        textoCelda_Numero.Top + textoCelda_Numero.Size.Height / 2);

                                    e.Graphics.DrawLine(pen, puntoInicial, puntoFinal);

                                    break;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        private void AsignacionAutomatica_TextosInformacion_Load(object sender, EventArgs e)
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
                tablaCeldas.Paint += tablaCeldas_Paint;

                var celdasValidas = celdaActual;
                                
                int filas = 0;
                int columnas = 0;
                int maxColumns = 0;

                for (filas = 1; filas <= celdasValidas.Rows.Count; filas++)
                {
                    //bool terminar = false;

                    for (columnas = 1; columnas <= celdasValidas.Columns.Count; columnas++)
                    {
                        string strTexto = string.Empty;
                        Label textoCelda = new Label();

                        double numero = 0;
                        string valor = string.Empty;

                        if (celdasValidas[filas, columnas].Value != null)
                        {
                            valor = celdasValidas[filas, columnas].Value.ToString();
                        }

                        bool esNumero = false;

                        if (!string.IsNullOrEmpty(valor) &&
                            double.TryParse(valor, out numero))
                            esNumero = true;

                        if (esNumero &&
                            NumerosSeleccionados.Any(i => i.Celda.Address == celdasValidas[filas, columnas].Address))
                        {
                            textoCelda.BackColor = SystemColors.ControlLight;
                        }

                        if(Asignaciones.Any(i => i.Celda_Numero.Celda.Address == celdasValidas[filas, columnas].Address |
                            i.Celda_TextoInformacion.Celda.Address == celdasValidas[filas, columnas].Address))
                        {
                            textoCelda.BackColor = SystemColors.GrayText;
                            textoCelda.ForeColor = SystemColors.Window;
                            textoCelda.BorderStyle = BorderStyle.FixedSingle;
                        }

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

                        tablaCeldas.SetRow(textoCelda, filas - 1);
                        tablaCeldas.SetColumn(textoCelda, columnas - 1);

                    }

                }

                tablaCeldas.RowCount = filas - 1;
                tablaCeldas.ColumnCount = columnas - 1;
                

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
        }

        private void TextoCelda_Click(object sender, EventArgs e)
        {
            if (!clicCelda && 
                !(NumerosSeleccionados.Any(i => i.Celda.Address == ((Range)((Label)sender).Tag).Address)))
            {
                clicCelda = true;
                PrimeraCelda_Arrastre = (Range)((Label)sender).Tag;
                ColorearCeldas(PrimeraCelda_Arrastre.Address, null, false);
            }
            else if(clicCelda)
            {
                if (VerificarNumero_Celda(((Label)sender).Text) &&
                    NumerosSeleccionados.Any(i => i.Celda.Address == ((Range)((Label)sender).Tag).Address))
                {
                    SegundaCelda_Arrastre = (Range)((Label)sender).Tag;

                    if (PrimeraCelda_Arrastre != null & SegundaCelda_Arrastre != null)
                    {
                        TableLayoutPanel tabla = (TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0];
                        int columna = tabla.GetColumn((Label)sender) + 1;

                        AgregarAsignacion(PrimeraCelda_Arrastre, SegundaCelda_Arrastre, columna, false);
                        ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).Refresh();
                        clicCelda = false;
                    }
                }
                else
                {
                    clicCelda = false;
                    ColorearCeldas(PrimeraCelda_Arrastre.Address, null, false);
                }
            }
        }

        private bool VerificarNumero_Celda(string strNumero)
        {
            double numero;
            return double.TryParse(strNumero, out numero);
        }

        private void AgregarAsignacion(Range textoInformacion, Range numero,
            int columna, bool relleno)
        {
            if (relleno)
            {
                if (PrimeraCelda_Arrastre.Address.Equals(textoInformacion.Address) &&
                    SegundaCelda_Arrastre.Address.Equals(numero.Address))
                { return; }
            }

            double numeroValor = 0;
            bool esTextoInformacion = false;
            bool esNumero = false;

            if(textoInformacion.Value != null)
                esTextoInformacion = !double.TryParse(textoInformacion.Value.ToString(), out numeroValor) || 
                    (double.TryParse(textoInformacion.Value.ToString(), out numeroValor) && 
                    !(NumerosSeleccionados.Any(i => i.Celda.Address == textoInformacion.Address)));

            if (numero.Value != null)
            {
                esNumero = double.TryParse(numero.Value.ToString(), out numeroValor) && 
                    (NumerosSeleccionados.Any(i => i.Celda.Address == numero.Address));
            }

            if (esTextoInformacion && esNumero)
            {
                var asignacion = (from A in Asignaciones
                                  where A.Celda_TextoInformacion.Celda.Address == textoInformacion.Address &
                                  A.Celda_Numero.Celda.Address == numero.Address
                                  select A).FirstOrDefault();

                if (asignacion == null)
                {
                    asignacion = new AsignacionTextoInformacion_Numero_Instancia();
                    asignacion.Celda_Numero = new TextosInformacionCelda();
                    asignacion.Celda_Numero.Celda = numero;
                    asignacion.Celda_TextoInformacion = new TextosInformacionCelda();
                    asignacion.Celda_TextoInformacion.Celda = textoInformacion;
                    Asignaciones.Add(asignacion);
                }
                else
                {
                    Asignaciones.Remove(asignacion);
                }

                ColorearCeldas(textoInformacion.Address, numero.Address, relleno);
            }
        }

        private void AsignacionAutomatica_TextosInformacion_Shown(object sender, EventArgs e)
        {
            foreach (TabPage itemTab in contenedorTablas.TabPages)
            {
                ((TableLayoutPanel)itemTab.Controls[0]).Refresh();
            }
        }

        private void RellenarAsignaciones(TipoRellenoAsignaciones opcionRelleno, 
            Range primeraCeldaSeleccionada, Range segundaCeldaSeleccionada, Range CeldasSeleccionadas)
        {
            if (primeraCeldaSeleccionada != null && segundaCeldaSeleccionada != null)
            {
                int filas = 0;
                int columnas = 0;
                int filaPrimeraCelda = 0;
                int filaSegundaCelda = 0;
                int columnaPrimeraCelda = 0;
                int columnaSegundaCelda = 0;

                int UltimaFila = 0;
                int UltimaColumna = 0;
                                
                for (filas = 1; filas <= CeldasSeleccionadas.Rows.Count; filas++)
                {
                    //quitarCiclo = true;
                    for (columnas = 1; columnas <= CeldasSeleccionadas.Columns.Count; columnas++)
                    {
                        if (((Range)CeldasSeleccionadas[filas, columnas]).Value != null)
                        {
                            if (((Range)CeldasSeleccionadas[filas, columnas]).Address == primeraCeldaSeleccionada.Address)
                            {
                                filaPrimeraCelda = filas;
                                columnaPrimeraCelda = columnas;
                            }
                            else if (((Range)CeldasSeleccionadas[filas, columnas]).Address == segundaCeldaSeleccionada.Address)
                            {
                                filaSegundaCelda = filas;
                                columnaSegundaCelda = columnas;
                            }

                            //quitarCiclo = false;
                        }
                            
                    }

                }
                    
                UltimaFila = CeldasSeleccionadas.Rows.Count;
                UltimaColumna = CeldasSeleccionadas.Columns.Count;
                

                switch (opcionRelleno)
                {
                    case TipoRellenoAsignaciones.HaciaAbajo:

                        int filaPrimeraCelda_Fija = filaPrimeraCelda;

                        while (true)
                        {
                            AgregarAsignacion(CeldasSeleccionadas[(!chkUsarPrimerosTextosTodoRelleno.Checked) ? filaPrimeraCelda : filaPrimeraCelda_Fija, columnaPrimeraCelda],
                                CeldasSeleccionadas[filaSegundaCelda, columnaSegundaCelda], columnaSegundaCelda, true);

                            filaPrimeraCelda++;
                            filaSegundaCelda++;
                            if (filaPrimeraCelda == UltimaFila + 1 |
                                filaSegundaCelda == UltimaFila + 1)
                                break;
                        }
                        break;

                    case TipoRellenoAsignaciones.HaciaArriba:

                        filaPrimeraCelda_Fija = filaPrimeraCelda;

                        while (true)
                        {
                            AgregarAsignacion(CeldasSeleccionadas[(!chkUsarPrimerosTextosTodoRelleno.Checked) ? filaPrimeraCelda : filaPrimeraCelda_Fija, columnaPrimeraCelda],
                                CeldasSeleccionadas[filaSegundaCelda, columnaSegundaCelda], columnaSegundaCelda, true);

                            filaPrimeraCelda--;
                            filaSegundaCelda--;
                            if (filaPrimeraCelda == 0 |
                                filaSegundaCelda == 0)
                                break;
                        }
                        break;

                    case TipoRellenoAsignaciones.HaciaDerecha:

                        int columnaPrimeraCelda_Fija = columnaPrimeraCelda;

                        while (true)
                        {
                            int diferencia = 0;

                            if (columnaSegundaCelda > columnaPrimeraCelda)
                                diferencia = (columnaSegundaCelda - columnaPrimeraCelda);
                            else if (columnaSegundaCelda < columnaPrimeraCelda)
                                diferencia = (columnaPrimeraCelda - columnaSegundaCelda);

                            if (columnaPrimeraCelda >= UltimaColumna + diferencia + 1 |
                                columnaSegundaCelda >= UltimaColumna + diferencia + 1)
                                break;

                            AgregarAsignacion(CeldasSeleccionadas[filaPrimeraCelda, (!chkUsarPrimerosTextosTodoRelleno.Checked) ? columnaPrimeraCelda : columnaPrimeraCelda_Fija],
                                CeldasSeleccionadas[filaSegundaCelda, columnaSegundaCelda], columnaSegundaCelda, true);

                            columnaPrimeraCelda += diferencia + 1;
                            columnaSegundaCelda += diferencia + 1;
                        }

                        break;

                    case TipoRellenoAsignaciones.HaciaIzquierda:

                        columnaPrimeraCelda_Fija = columnaPrimeraCelda;

                        while (true)
                        {
                            int diferencia = 0;

                            if (columnaSegundaCelda > columnaPrimeraCelda)
                                diferencia = (columnaSegundaCelda - columnaPrimeraCelda);
                            else if (columnaSegundaCelda < columnaPrimeraCelda)
                                diferencia = (columnaPrimeraCelda - columnaSegundaCelda);

                            if (columnaPrimeraCelda + diferencia <= 0 |
                                columnaSegundaCelda + diferencia <= 0)
                                break;

                            AgregarAsignacion(CeldasSeleccionadas[filaPrimeraCelda, (!chkUsarPrimerosTextosTodoRelleno.Checked) ? columnaPrimeraCelda : columnaPrimeraCelda_Fija],
                                CeldasSeleccionadas[filaSegundaCelda, columnaSegundaCelda], columnaSegundaCelda, true);

                            columnaPrimeraCelda -= diferencia + 1;
                            columnaSegundaCelda -= diferencia + 1;
                        }
                        break;

                    case TipoRellenoAsignaciones.HaciaTodosLados:

                        int filaPrimeraCelda_ = filaPrimeraCelda;
                        int filaSegundaCelda_ = filaSegundaCelda;

                        filaPrimeraCelda_Fija = filaPrimeraCelda_;

                        while (true)
                        {
                            AgregarAsignacion(CeldasSeleccionadas[(!chkUsarPrimerosTextosTodoRelleno.Checked) ? filaPrimeraCelda_ : filaPrimeraCelda_Fija, columnaPrimeraCelda],
                                CeldasSeleccionadas[filaSegundaCelda_, columnaSegundaCelda], columnaSegundaCelda, true);

                            filaPrimeraCelda_++;
                            filaSegundaCelda_++;
                            if (filaPrimeraCelda_ == UltimaFila + 1 |
                                filaSegundaCelda_ == UltimaFila + 1)
                                break;
                        }

                        filaPrimeraCelda_ = filaPrimeraCelda;
                        filaSegundaCelda_ = filaSegundaCelda;

                        filaPrimeraCelda_Fija = filaPrimeraCelda_;

                        while (true)
                        {
                            AgregarAsignacion(CeldasSeleccionadas[(!chkUsarPrimerosTextosTodoRelleno.Checked) ? filaPrimeraCelda_ : filaPrimeraCelda_Fija, columnaPrimeraCelda],
                                CeldasSeleccionadas[filaSegundaCelda_, columnaSegundaCelda], columnaSegundaCelda, true);

                            filaPrimeraCelda_--;
                            filaSegundaCelda_--;

                            if (filaPrimeraCelda_ == 0 |
                                filaSegundaCelda_ == 0)
                                break;
                        }

                        int columnaPrimeraCelda_ = columnaPrimeraCelda;
                        int columnaSegundaCelda_ = columnaSegundaCelda;

                        columnaPrimeraCelda_Fija = columnaPrimeraCelda_;

                        while (true)
                        {
                            int diferencia = 0;

                            if (columnaSegundaCelda_ > columnaPrimeraCelda_)
                                diferencia = (columnaSegundaCelda_ - columnaPrimeraCelda_);
                            else if (columnaSegundaCelda_ < columnaPrimeraCelda_)
                                diferencia = (columnaPrimeraCelda_ - columnaSegundaCelda_);

                            if (columnaPrimeraCelda_ >= UltimaColumna + diferencia + 1 |
                                columnaSegundaCelda_ >= UltimaColumna + diferencia + 1)
                                break;

                            AgregarAsignacion(CeldasSeleccionadas[filaPrimeraCelda, (!chkUsarPrimerosTextosTodoRelleno.Checked) ? columnaPrimeraCelda_ : columnaPrimeraCelda_Fija],
                                CeldasSeleccionadas[filaSegundaCelda, columnaSegundaCelda_], columnaSegundaCelda_, true);

                            columnaPrimeraCelda_ += diferencia + 1;
                            columnaSegundaCelda_ += diferencia + 1;
                        }

                        columnaPrimeraCelda_ = columnaPrimeraCelda;
                        columnaSegundaCelda_ = columnaSegundaCelda;

                        columnaPrimeraCelda_Fija = columnaPrimeraCelda_;

                        while (true)
                        {
                            int diferencia = 0;

                            if (columnaSegundaCelda_ > columnaPrimeraCelda_)
                                diferencia = (columnaSegundaCelda_ - columnaPrimeraCelda_);
                            else if (columnaSegundaCelda_ < columnaPrimeraCelda_)
                                diferencia = (columnaPrimeraCelda_ - columnaSegundaCelda_);

                            if (columnaPrimeraCelda_ + diferencia <= 0 |
                                columnaSegundaCelda_ + diferencia <= 0)
                                break;

                            AgregarAsignacion(CeldasSeleccionadas[filaPrimeraCelda, (!chkUsarPrimerosTextosTodoRelleno.Checked) ? columnaPrimeraCelda_ : columnaPrimeraCelda_Fija],
                                CeldasSeleccionadas[filaSegundaCelda, columnaSegundaCelda_], columnaSegundaCelda_, true);

                            columnaPrimeraCelda_ -= diferencia + 1;
                            columnaSegundaCelda_ -= diferencia + 1;
                        }
                        break;
                }

                contenedorTablas.SelectedTab.Controls[0].Refresh();
            }
        }

        private void btnRellenarHaciaAbajo_Click(object sender, EventArgs e)
        {
            RellenarAsignaciones(TipoRellenoAsignaciones.HaciaAbajo, PrimeraCelda_Arrastre, SegundaCelda_Arrastre, (Range)contenedorTablas.SelectedTab.Controls[0].Tag);
        }

        private void btnRellenarHaciaDerecha_Click(object sender, EventArgs e)
        {
            RellenarAsignaciones(TipoRellenoAsignaciones.HaciaDerecha, PrimeraCelda_Arrastre, SegundaCelda_Arrastre, (Range)contenedorTablas.SelectedTab.Controls[0].Tag);
        }

        private void btnRellenarHaciaArriba_Click(object sender, EventArgs e)
        {
            RellenarAsignaciones(TipoRellenoAsignaciones.HaciaArriba, PrimeraCelda_Arrastre, SegundaCelda_Arrastre, (Range)contenedorTablas.SelectedTab.Controls[0].Tag);
        }

        private void btnRellenarHaciaIzquierda_Click(object sender, EventArgs e)
        {
            RellenarAsignaciones(TipoRellenoAsignaciones.HaciaIzquierda, PrimeraCelda_Arrastre, SegundaCelda_Arrastre, (Range)contenedorTablas.SelectedTab.Controls[0].Tag);
        }

        private void btnRellenarHaciaTodosLados_Click(object sender, EventArgs e)
        {
            RellenarAsignaciones(TipoRellenoAsignaciones.HaciaTodosLados, PrimeraCelda_Arrastre, SegundaCelda_Arrastre, (Range)contenedorTablas.SelectedTab.Controls[0].Tag);
        }

        private void ColorearCeldas(string celdaTextoInformacion, string celdaNumero, bool relleno)
        {
            if (!relleno && (!string.IsNullOrEmpty(celdaTextoInformacion) && 
                string.IsNullOrEmpty(celdaNumero)))
            {
                foreach (Label textoCelda in ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).Controls)
                {
                    if (((Range)textoCelda.Tag).Address.Equals(celdaTextoInformacion))
                    {
                        if (textoCelda.BackColor != SystemColors.GrayText)
                        {
                            textoCelda.BackColor = SystemColors.GrayText;
                            textoCelda.ForeColor = SystemColors.Window;
                            textoCelda.BorderStyle = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            textoCelda.BackColor = ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).BackColor;
                            textoCelda.ForeColor = ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).ForeColor;
                            textoCelda.BorderStyle = BorderStyle.None;
                        }
                    }
                }
            }
            else if(!string.IsNullOrEmpty(celdaTextoInformacion) &&
                !string.IsNullOrEmpty(celdaNumero))
            {
                foreach (Label textoCelda in ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).Controls)
                {
                    bool esCelda = false;
                    
                    if (!string.IsNullOrEmpty(celdaNumero))
                    {
                        if (((Range)textoCelda.Tag).Address.Equals(celdaNumero))
                            esCelda = true;
                    }

                    if (relleno && !string.IsNullOrEmpty(celdaTextoInformacion) &&
                        ((Range)textoCelda.Tag).Address.Equals(celdaTextoInformacion))
                        esCelda = true;
                    else
                    {
                        if (esCelda)
                        {
                            if (!string.IsNullOrEmpty(celdaTextoInformacion))
                            {
                                if (((Range)textoCelda.Tag).Address.Equals(celdaTextoInformacion))
                                    esCelda = false;
                            }
                        }
                    }

                    if (esCelda)
                    {
                        if (textoCelda.BackColor != SystemColors.GrayText)
                        {
                            textoCelda.BackColor = SystemColors.GrayText;
                            textoCelda.ForeColor = SystemColors.Window;
                            textoCelda.BorderStyle = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            bool esNumero = false;

                            double numero = 0;
                            string valor = string.Empty;

                            if (((Range)textoCelda.Tag).Value != null)
                            {
                                valor = ((Range)textoCelda.Tag).Value.ToString();
                            }

                            if (!string.IsNullOrEmpty(valor) &&
                            double.TryParse(valor, out numero))
                                esNumero = true;

                            if (esNumero &&
                            NumerosSeleccionados.Any(i => i.Celda.Address == ((Range)textoCelda.Tag).Address))
                            {
                                textoCelda.BackColor = SystemColors.ControlLight;
                            }
                            else
                            {
                                textoCelda.BackColor = ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).BackColor;
                            }
                            textoCelda.ForeColor = ((TableLayoutPanel)contenedorTablas.SelectedTab.Controls[0]).ForeColor;
                            textoCelda.BorderStyle = BorderStyle.None;
                        }
                    }
                }
            }
        }
    }

    public enum TipoRellenoAsignaciones
    {
        Ninguno = 0,
        HaciaAbajo = 1,
        HaciaDerecha = 2,
        HaciaArriba = 3,
        HaciaIzquierda = 4,
        HaciaTodosLados = 5
    }
}
