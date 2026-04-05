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
    public partial class AsignacionManual_ListaTextos : Form
    {
        public SeleccionInstancia SeleccionInstancia { get; set; }
        public AsignacionManual_ListaTextos()
        {
            InitializeComponent();
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
                            NumeroPagina = itemSeleccion.NumeroPagina,
                            Texto = itemSeleccion.TextoActual
                        });

                        string[] subTextos = new string[3];
                        subTextos[0] = SeleccionInstancia.TextosBusqueda.Last().Texto;
                        subTextos[1] = SeleccionInstancia.TextosBusqueda.Last().NumeroPagina.ToString();
                        subTextos[2] = SeleccionInstancia.TextosBusqueda.Last().NumeroFila.ToString();

                        ListViewItem item = new ListViewItem(subTextos);
                        item.Tag = SeleccionInstancia.TextosBusqueda.Last();

                        listaTextos.Items.Add(item);
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
            }
        }

        private void btnLimpiarAsignaciones_Click(object sender, EventArgs e)
        {
            if (SeleccionInstancia != null &&
                listaTextos.SelectedItems.Count > 0)
            {
                SeleccionInstancia.TextosBusqueda.Clear();
                listaTextos.Items.Clear();
            }
        }

        public void MostrarSeleccion_DefinirEntradas()
        {
            try
            {
                txtPagina.Text = string.Empty;
                txtLinea.Text = string.Empty;
                textoActual.Text = string.Empty;

                foreach (var itemSeleccion in SeleccionInstancia.SeleccionesTextos)
                {
                    txtPagina.Text += itemSeleccion.NumeroPagina.ToString() + " , ";
                    txtLinea.Text += itemSeleccion.NumeroFila.ToString() + " , ";
                    textoActual.Text += itemSeleccion.TextoActual + "\t";
                }

                txtPagina.Text = txtPagina.Text.Remove(
                    txtPagina.Text.Length - 2);
                txtLinea.Text = txtLinea.Text.Remove(
                    txtLinea.Text.Length - 2);
                textoActual.Text = textoActual.Text.Remove(
                    textoActual.Text.Length - 1);

            }
            catch (Exception) { }
        }

        public void ListarTextosSeleccionados()
        {
            foreach (var itemTextoBusqueda in SeleccionInstancia.TextosBusqueda)
            {                
                string[] subTextos = new string[3];
                subTextos[0] = itemTextoBusqueda.Texto;
                subTextos[1] = itemTextoBusqueda.NumeroPagina.ToString();
                subTextos[2] = itemTextoBusqueda.NumeroFila.ToString();

                ListViewItem item = new ListViewItem(subTextos);
                item.Tag = itemTextoBusqueda;

                listaTextos.Items.Add(item);                
            }            
        }

        private void AsignacionManual_ListaTextos_Load(object sender, EventArgs e)
        {
            ListarTextosSeleccionados();
            MostrarSeleccion_DefinirEntradas();
        }
    }
}
