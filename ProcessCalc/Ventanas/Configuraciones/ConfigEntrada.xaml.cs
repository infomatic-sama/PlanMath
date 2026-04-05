using ProcessCalc.Entidades;
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
    /// Lógica de interacción para ConfigEntrada.xaml
    /// </summary>
    public partial class ConfigEntrada : Window
    {
        public MainWindow Ventana { get; set; }
        public DiseñoCalculo Calculo { get; set; }
        Entrada entr;
        public Entrada Entrada
        {
            set
            {
                entr = value;
            }

            get
            {
                return entr;
            }
        }
        public ConfigEntrada()
        {
            InitializeComponent();
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctrlEntrada.opcionesOtraEntrada.Visibility = Visibility.Collapsed;
            ctrlEntrada.Background = null;
            ctrlEntrada.VentanaEntrada = this;
            ctrlEntrada.Ventana = Ventana;
            ctrlEntrada.Calculo = Calculo;
            ctrlEntrada.CalculoEntradas = Ventana.CalculoActual.ObtenerCalculoEntradas();
            ctrlEntrada.Entrada = entr;
            ctrlEntrada.ActualizarTipo(entr.Tipo);
        }
    }
}
