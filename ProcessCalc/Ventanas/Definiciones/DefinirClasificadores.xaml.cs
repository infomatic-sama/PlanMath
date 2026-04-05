using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas.Digitaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para DefinirClasificadores.xaml
    /// </summary>
    public partial class DefinirClasificadores : Window
    {
        public List<Clasificador> ListaClasificadores {  get; set; }
        public Clasificador ClasificadorSeleccionado { get; set; }
        public DefinirClasificadores()
        {
            InitializeComponent();
        }

        public void ListarClasificadores()
        {
            if (ListaClasificadores != null)
            {
                ClasificadorSeleccionado = null;
                clasificadores.Children.Clear();

                foreach (var itemClasificador in ListaClasificadores)
                {
                    EtiquetaClasificador etiquetaClasificador = new EtiquetaClasificador();
                    etiquetaClasificador.Clasificador = itemClasificador;
                    etiquetaClasificador.DefinirClasificadores = this;
                    etiquetaClasificador.CargarClasificador();
                    clasificadores.Children.Add(etiquetaClasificador);
                }

                PintarClasificadores_ConSeleccionado();
            }
        }

        public void EstablecerClasificadorSeleccionado(Clasificador clasificador)
        {
            ClasificadorSeleccionado = clasificador;
        }

        public void PintarClasificadores_ConSeleccionado()
        {
            foreach (EtiquetaClasificador itemEtiquetaClasificador in clasificadores.Children)
            {
                if (ClasificadorSeleccionado == itemEtiquetaClasificador.Clasificador)
                {
                    itemEtiquetaClasificador.botonBorde.Background = System.Windows.SystemColors.HighlightBrush;
                }
                else
                {
                    itemEtiquetaClasificador.botonBorde.Background = agregarClasificador.Background;
                }
            }
        }

        private void agregarClasificador_Click(object sender, RoutedEventArgs e)
        {
            if (ListaClasificadores != null)
            {
                IngresarClasificador ingresarClasificador = new IngresarClasificador();
                Clasificador Clasificador = new Clasificador();
                ingresarClasificador.SeleccionCadenasTexto = new List<OperacionCadenaTexto>();
                Clasificador.ID = App.GenerarID_Elemento();

                if (ingresarClasificador.ShowDialog() == true)
                {
                    Clasificador.CadenaTexto = ingresarClasificador.CadenaTexto;
                    Clasificador.UtilizarCadenasTexto_DeCantidad = ingresarClasificador.UtilizarCadenasTexto_DeCantidad;
                    Clasificador.SeleccionCadenasTexto = ingresarClasificador.SeleccionCadenasTexto;
                    ListaClasificadores.Add(Clasificador);
                    ListarClasificadores();
                }
            }
        }

        private void quitarClasificador_Click(object sender, RoutedEventArgs e)
        {
            if (ListaClasificadores != null &&
                ClasificadorSeleccionado != null)
            {
                ListaClasificadores.Remove(ClasificadorSeleccionado);
                ListarClasificadores();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarClasificadores();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void subirClasificador_Click(object sender, RoutedEventArgs e)
        {
            if (ListaClasificadores != null &&
                ClasificadorSeleccionado != null)
            {
                if (ClasificadorSeleccionado != ListaClasificadores.FirstOrDefault())
                {
                    var indiceActual = ListaClasificadores.IndexOf(ClasificadorSeleccionado);
                    ListaClasificadores.Remove(ClasificadorSeleccionado);

                    indiceActual--;
                    ListaClasificadores.Insert(indiceActual, ClasificadorSeleccionado);
                    ListarClasificadores();
                }
            }
        }

        private void bajarClasificador_Click(object sender, RoutedEventArgs e)
        {
            if (ListaClasificadores != null &&
                ClasificadorSeleccionado != null)
            {
                if (ClasificadorSeleccionado != ListaClasificadores.LastOrDefault())
                {
                    var indiceActual = ListaClasificadores.IndexOf(ClasificadorSeleccionado);
                    ListaClasificadores.Remove(ClasificadorSeleccionado);

                    indiceActual++;
                    ListaClasificadores.Insert(indiceActual, ClasificadorSeleccionado);
                    ListarClasificadores();
                }
            }
        }

        private void editarClasificador_Click(object sender, RoutedEventArgs e)
        {
            if (ListaClasificadores != null &&
                ClasificadorSeleccionado != null)
            {
                IngresarClasificador ingresarClasificador = new IngresarClasificador();
                ingresarClasificador.CadenaTexto = ClasificadorSeleccionado.CadenaTexto;
                ingresarClasificador.UtilizarCadenasTexto_DeCantidad = ClasificadorSeleccionado.UtilizarCadenasTexto_DeCantidad;
                ingresarClasificador.SeleccionCadenasTexto = ClasificadorSeleccionado.SeleccionCadenasTexto;

                if (ingresarClasificador.ShowDialog() == true)
                {
                    ClasificadorSeleccionado.CadenaTexto = ingresarClasificador.CadenaTexto;
                    ClasificadorSeleccionado.UtilizarCadenasTexto_DeCantidad = ingresarClasificador.UtilizarCadenasTexto_DeCantidad;
                    ClasificadorSeleccionado.SeleccionCadenasTexto = ingresarClasificador.SeleccionCadenasTexto;
                    ListarClasificadores();
                }
            }
        }
    }
}
