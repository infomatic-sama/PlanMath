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
    /// Lógica de interacción para SeleccionarTipoEntrada.xaml
    /// </summary>
    public partial class SeleccionarTipoEntrada : Window
    {
        public TipoEntrada TipoSeleccionado { get; set; }
        public bool MostrarOpcionTextosInformacion { get; set; }
        public SeleccionarTipoEntrada()
        {
            TipoSeleccionado = TipoEntrada.Ninguno;
            InitializeComponent();
        }

        private void btnNumero_Click(object sender, RoutedEventArgs e)
        {
            TipoSeleccionado = TipoEntrada.Numero;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnConjuntoNumeros_Click(object sender, RoutedEventArgs e)
        {
            TipoSeleccionado = TipoEntrada.ConjuntoNumeros;
            Close();
        }

        private void btnTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            TipoSeleccionado = TipoEntrada.TextosInformacion;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MostrarOpcionTextosInformacion)
                btnTextosInformacion.Visibility = Visibility.Visible;
            else
                btnTextosInformacion.Visibility = Visibility.Collapsed;
        }
    }
}
