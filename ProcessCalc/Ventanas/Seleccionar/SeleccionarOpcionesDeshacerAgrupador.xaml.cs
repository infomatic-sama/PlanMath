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
    /// Lógica de interacción para SeleccionarOpcionesDeshacerAgrupador.xaml
    /// </summary>
    public partial class SeleccionarOpcionesDeshacerAgrupador : Window
    {
        public bool Aceptar { get; set; }
        public bool QuitarElementosCalculo { get; set; }
        public SeleccionarOpcionesDeshacerAgrupador()
        {
            InitializeComponent();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deshacerAgrupador_Click(object sender, RoutedEventArgs e)
        {
            Aceptar = true;
            QuitarElementosCalculo = opcionQuitarElementosCalculo.IsChecked == true ? true: false;
            Close();
        }
    }
}
