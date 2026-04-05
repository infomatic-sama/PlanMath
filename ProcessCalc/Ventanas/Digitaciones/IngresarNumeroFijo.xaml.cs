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
    /// Lógica de interacción para IngresarNumeroFijo.xaml
    /// </summary>
    public partial class IngresarNumeroFijo : Window
    {
        public bool numeroCambiado { get; set; }
        public double Numero { get; set; }
        public IngresarNumeroFijo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnUtilizar_Click(object sender, RoutedEventArgs e)
        {
            double num;
            bool numeroConvertido = double.TryParse(txtNumero.Text, out num);
            if (numeroConvertido)
            {
                Numero = num;
                numeroCambiado = true;
                Close();
            }
            else
                MessageBox.Show("Número incorrecto.", "Número incorrecto", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
