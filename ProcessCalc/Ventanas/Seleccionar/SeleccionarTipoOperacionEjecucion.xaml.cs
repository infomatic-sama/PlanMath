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

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para SeleccionarTipoOperacionEjecucion.xaml
    /// </summary>
    public partial class SeleccionarTipoOperacionEjecucion : Window
    {
        public TipoOperacionEjecucion TipoOperacion { get; set; }
        public bool MostrarOpcionPorSeparado { get; set; }
        public bool MostrarOpcionPorFilas { get; set; }
        public SeleccionarTipoOperacionEjecucion()
        {
            MostrarOpcionPorSeparado = true;
            MostrarOpcionPorFilas = true;
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!MostrarOpcionPorSeparado)
            {
                optOperarPorSeparado.Visibility = Visibility.Collapsed;
                optOperarPorSeparadoPorFilas.Visibility = Visibility.Collapsed;
            }

            if(!MostrarOpcionPorFilas)
            {
                optOperarPorFilas.Visibility = Visibility.Collapsed;
                optOperarPorSeparadoPorFilas.Visibility = Visibility.Collapsed;
            }

            if(TipoOperacion == TipoOperacionEjecucion.OperarTodosJuntos)
            {
                optOperacionTodosJuntos.IsChecked = true;
            }
            else if(TipoOperacion == TipoOperacionEjecucion.OperarPorSeparado)
            {
                optOperarPorSeparado.IsChecked = true;
            }
            else if (TipoOperacion == TipoOperacionEjecucion.OperarPorFilas)
            {
                optOperarPorFilas.IsChecked = true;
            }
            else if (TipoOperacion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
            {
                optOperarPorSeparadoPorFilas.IsChecked = true;
            }
        }

        private void optOperacionTodosJuntos_Checked(object sender, RoutedEventArgs e)
        {
            TipoOperacion = TipoOperacionEjecucion.OperarTodosJuntos;
        }

        private void optOperarPorSeparado_Checked(object sender, RoutedEventArgs e)
        {
            TipoOperacion = TipoOperacionEjecucion.OperarPorSeparado;
        }

        private void optOperarPorFilas_Checked(object sender, RoutedEventArgs e)
        {
            TipoOperacion = TipoOperacionEjecucion.OperarPorFilas;
        }

        private void optOperarPorSeparadoPorFilas_Checked(object sender, RoutedEventArgs e)
        {
            TipoOperacion = TipoOperacionEjecucion.OperarPorSeparadoPorFilas;
        }
    }
}
