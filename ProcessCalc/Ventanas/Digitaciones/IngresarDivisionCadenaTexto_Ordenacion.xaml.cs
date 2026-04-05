using ProcessCalc.Entidades.Operaciones;
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

namespace ProcessCalc.Ventanas.Digitaciones
{
    /// <summary>
    /// Lógica de interacción para IngresarDivisionCadenaTexto_Ordenacion.xaml
    /// </summary>
    public partial class IngresarDivisionCadenaTexto_Ordenacion : Window
    {
        public int IndiceLetraInicial {  get; set; }
        public int CantidadLetras { get; set; }
        public IngresarDivisionCadenaTexto_Ordenacion()
        {
            InitializeComponent();
        }

        private void indiceLetraInicial_TextChanged(object sender, TextChangedEventArgs e)
        {
            int letraInicial = 0;
            int.TryParse(indiceLetraInicial.Text, out letraInicial);
            IndiceLetraInicial = letraInicial;
        }

        private void cantidadLetras_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cantidad = 0;
            int.TryParse(cantidadLetras.Text, out cantidad);
            CantidadLetras = cantidad;
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
    }
}
