using ProcessCalc.Entidades.TextosInformacion;
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
    /// Lógica de interacción para SeleccionarConjuntoTextosInformacion_Digitacion.xaml
    /// </summary>
    public partial class SeleccionarConjuntoTextosInformacion_Digitacion : Window
    {
        public List<ConjuntoTextosInformacion_Digitacion> ConjuntosTextosInformacionDigitacion { get; set; }
        public ConjuntoTextosInformacion_Digitacion ConjuntoTextosInformacionDigitacion_Seleccionado { get; set; }
        public SeleccionarConjuntoTextosInformacion_Digitacion()
        {
            InitializeComponent();
        }

        private void seleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (conjuntosTextosInformacion.SelectedItem != null)
            {
                ConjuntoTextosInformacionDigitacion_Seleccionado = (ConjuntoTextosInformacion_Digitacion)conjuntosTextosInformacion.SelectedItem;
                Close();
            }
            else
                MessageBox.Show("Seleccionar un vector de cadenas de texto.", "Seleccionar", MessageBoxButton.OK);
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conjuntosTextosInformacion.DisplayMemberPath = "Nombre";
            conjuntosTextosInformacion.ItemsSource = ConjuntosTextosInformacionDigitacion;
        }
    }
}
