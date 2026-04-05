using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para ConfigURLEntrada.xaml
    /// </summary>
    public partial class ConfigURLEntrada : Window
    {
        public ConfiguracionURLEntrada Config { get; set; }
        public ConfigURLEntrada()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                switch (Config.ConfiguracionEscribirURL)
                {
                    case OpcionEscribirURLEntrada.UtilizarURLIndicada:
                        opcionURLFija.IsChecked = true;
                        break;

                    case OpcionEscribirURLEntrada.EscribirURLEjecucion:
                        opcionEscribirURL.IsChecked = true;
                        break;

                    case OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada:
                        opcionEscribirURLPorEntrada.IsChecked = true;
                        break;
                }

                establecerParametrosEnEjecucion.IsChecked = Config.EstablecerParametrosEjecucion;
            }
        }

        private void opcionURLFija_Checked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                if (opcionURLFija.IsChecked == true)
                    Config.ConfiguracionEscribirURL = OpcionEscribirURLEntrada.UtilizarURLIndicada;
            }
        }

        private void opcionEscribirURL_Checked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                if (opcionEscribirURL.IsChecked == true)
                    Config.ConfiguracionEscribirURL = OpcionEscribirURLEntrada.EscribirURLEjecucion;
            }
        }

        private void opcionEscribirURLPorEntrada_Checked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
            {
                if (opcionEscribirURLPorEntrada.IsChecked == true)
                    Config.ConfiguracionEscribirURL = OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada;
            }
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void establecerParametrosEnEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
                Config.EstablecerParametrosEjecucion = (bool)establecerParametrosEnEjecucion.IsChecked;
        }

        private void establecerParametrosEnEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Config != null)
                Config.EstablecerParametrosEjecucion = (bool)establecerParametrosEnEjecucion.IsChecked;
        }
    }
}
