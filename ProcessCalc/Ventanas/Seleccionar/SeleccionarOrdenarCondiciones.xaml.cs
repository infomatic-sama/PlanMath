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
    /// Lógica de interacción para SeleccionarOrdenarCondiciones.xaml
    /// </summary>
    public partial class SeleccionarOrdenarCondiciones : Window
    {
        public SeleccionarOrdenarCondiciones()
        {            
            InitializeComponent();
            listaCondiciones.Operandos = new List<Entidades.DiseñoOperacion>();
            listaCondiciones.ListaElementos = new List<Entidades.DiseñoOperacion>();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void opcionModoManual_Checked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Collapsed;
        }

        private void opcionModoManual_Unchecked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Visible;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
