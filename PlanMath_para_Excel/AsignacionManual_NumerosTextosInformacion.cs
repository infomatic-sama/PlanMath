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
    public partial class AsignacionManual_NumerosTextosInformacion : Form
    {
        public SeleccionInstancia SeleccionInstancia { get; set; }
        public AsignacionManual_NumerosTextosInformacion()
        {
            InitializeComponent();
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
                AsignacionAutomatica(asignar.Asignaciones, true);
            }
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
            }
        }

        private void btnLimpiarAsignaciones_Click(object sender, EventArgs e)
        {
            LimpiarAsignaciones();
        }

        private void btnAsignarTextoInformacionNumero_Click(object sender, EventArgs e)
        {
            AgregarAsignacionTextoInformacion((TextosInformacionCelda)listaCeldasTextosInformacion.SelectedItem,
                (TextosInformacionCelda)listaCeldasNumeros.SelectedItem, true);
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnEsTexto_Click(object sender, EventArgs e)
        {
            if (listaCeldasNumeros.SelectedItem != null)
            {
                TextosInformacionCelda texto = new TextosInformacionCelda();
                texto.Celda = SeleccionInstancia.NumerosSeleccionados[listaCeldasNumeros.SelectedIndex].Celda;
                SeleccionInstancia.TextosInformacion.Add(texto);

                SeleccionInstancia.NumerosSeleccionados.RemoveAt(listaCeldasNumeros.SelectedIndex);
            }
        }

        public void AgregarAsignacionTextoInformacion(TextosInformacionCelda textoInformacion, TextosInformacionCelda numero,
            bool agregarAInstancia)
        {
            if (textoInformacion != null && numero != null)
            {
                AsignacionTextoInformacion_Numero_Instancia asignacion = new AsignacionTextoInformacion_Numero_Instancia();
                asignacion.Celda_Numero = numero;
                asignacion.Celda_TextoInformacion = textoInformacion;

                if(agregarAInstancia)
                    SeleccionInstancia.AsignacionesTextosInformacion.Add(asignacion);

                System.Windows.Forms.ListViewItem itemLista = new System.Windows.Forms.ListViewItem();
                itemLista.Tag = asignacion;
                itemLista.Text = asignacion.Celda_TextoInformacion.NombreCelda;

                System.Windows.Forms.ListViewItem.ListViewSubItem subItemCelda = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                subItemCelda.Text = asignacion.Celda_Numero.NombreCelda;
                itemLista.SubItems.Add(subItemCelda);

                listaAsignacionesTextosInformacion.Items.Add(itemLista);
            }
        }

        public void QuitarAsignacionTextoInformacion(TextosInformacionCelda textoInformacion, TextosInformacionCelda numero)
        {
            if (textoInformacion != null && numero != null)
            {
                var asignacion = (from A in SeleccionInstancia.AsignacionesTextosInformacion
                                  where A.Celda_TextoInformacion.Celda.Address == textoInformacion.Celda.Address &
                                  A.Celda_Numero.Celda.Address == numero.Celda.Address
                                  select A).FirstOrDefault();

                if (asignacion != null)
                {
                    listaAsignacionesTextosInformacion.Items.RemoveAt(SeleccionInstancia.AsignacionesTextosInformacion.IndexOf(asignacion));
                    SeleccionInstancia.AsignacionesTextosInformacion.Remove(asignacion);
                }
            }
        }

        public void LimpiarAsignaciones()
        {
            SeleccionInstancia.AsignacionesTextosInformacion.Clear();
            listaAsignacionesTextosInformacion.Items.Clear();
        }

        private void AsignacionAutomatica(List<AsignacionTextoInformacion_Numero_Instancia> asignaciones,
            bool asignarAInstancia)
        {
            foreach (var itemAsignacion in asignaciones)
            {
                AgregarAsignacionTextoInformacion(itemAsignacion.Celda_TextoInformacion,
                itemAsignacion.Celda_Numero, asignarAInstancia);
            }
        }

        private void AsignacionManual_NumerosTextosInformacion_Load(object sender, EventArgs e)
        {
            if(SeleccionInstancia != null &&
                SeleccionInstancia.AsignacionesTextosInformacion != null)
                AsignacionAutomatica(SeleccionInstancia.AsignacionesTextosInformacion, false);

            LLenarComboBoxes();
        }

        private void LLenarComboBoxes()
        {
            listaCeldasTextosInformacion.DisplayMember = "NombreCelda";
            listaCeldasTextosInformacion.ValueMember = "Valor";

            listaCeldasNumeros.DisplayMember = "NombreCelda";
            listaCeldasNumeros.ValueMember = "Valor";

            listaCeldasTextosInformacion.DataSource = SeleccionInstancia.TextosInformacion;
            listaCeldasNumeros.DataSource = SeleccionInstancia.NumerosSeleccionados;
        }

        private void btnQuitarAsignaciones_Click(object sender, EventArgs e)
        {
            if (listaAsignacionesTextosInformacion.SelectedIndices.Count >= 1)
            {
                var celdaTextoInformacion = SeleccionInstancia.AsignacionesTextosInformacion.ElementAt(
                    listaAsignacionesTextosInformacion.SelectedIndices[0]).Celda_TextoInformacion;

                var celdaNumero = SeleccionInstancia.AsignacionesTextosInformacion.ElementAt(
                    listaAsignacionesTextosInformacion.SelectedIndices[0]).Celda_Numero;

                QuitarAsignacionTextoInformacion(celdaTextoInformacion, celdaNumero);
            }
        }
    }
}
