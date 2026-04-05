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
    /// Lógica de interacción para SeleccionarOrdenarCantidades.xaml
    /// </summary>
    public partial class SeleccionarOrdenarCantidades : Window
    {
        public bool ModoImplicacion { get; set; }
        public SeleccionarOrdenarCantidades()
        {
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

        private void opcionModoUnir_Checked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Collapsed;
        }

        private void opcionModoUnir_Unchecked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(ModoImplicacion)
            {
                titulo.Visibility = Visibility.Visible;
                descripcionEntrada.Visibility = Visibility.Visible;
            }
        }
    }
}
