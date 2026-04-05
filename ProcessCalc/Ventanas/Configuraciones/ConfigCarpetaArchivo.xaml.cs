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
    /// Lógica de interacción para ConfigCarpetaArchivo.xaml
    /// </summary>
    public partial class ConfigCarpetaArchivo : Window
    {
        public Entrada Entrada { get; set; }
        public ConfiguracionArchivoEntrada ArchivoEntrada { get; set; }
        public bool ModoArchivoExterno { get; set; }
        public ConfigCarpetaArchivo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null &
                ((!ModoArchivoExterno && Entrada != null) ||
                ModoArchivoExterno && Entrada == null))
            {
                switch (ArchivoEntrada.ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                        opcionCarpetaFija.IsChecked = true;
                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                        opcionCarpetaArchivoCalculo.IsChecked = true;
                        break;
                }

                switch(ArchivoEntrada.ConfiguracionSeleccionArchivo)
                {
                    case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                        opcionArchivoFijo.IsChecked = true;
                        break;

                    case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                        opcionSeleccionarArchivo.IsChecked = true;
                        break;

                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                        opcionSeleccionarArchivoPorEntrada.IsChecked = true;
                        break;
                }

                if ( ((Entrada != null && Entrada.TipoOrigenDatos == TipoOrigenDatos.Archivo) ||
                    Entrada == null) && 
                    (ArchivoEntrada.TipoArchivo == TipoArchivo.ServidorFTP |
                    ArchivoEntrada.TipoArchivo == TipoArchivo.Internet))
                {
                    opcionCarpetaFija.Visibility = Visibility.Collapsed;
                    opcionCarpetaArchivoCalculo.Visibility = Visibility.Collapsed;
                    textoCarpeta.Visibility = Visibility.Collapsed;
                }

                if(ModoArchivoExterno)
                {
                    opcionSeleccionarArchivoPorEntrada.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionArchivoFijo_Checked(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null)
            {
                if (opcionArchivoFijo.IsChecked == true)
                    ArchivoEntrada.ConfiguracionSeleccionArchivo = OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado;
            }
        }

        private void opcionSeleccionarArchivo_Checked(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null)
            {
                if (opcionSeleccionarArchivo.IsChecked == true)
                    ArchivoEntrada.ConfiguracionSeleccionArchivo = OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion;
            }
        }

        private void opcionSeleccionarArchivoPorEntrada_Checked(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null &&
                !ModoArchivoExterno)
            {
                if (opcionSeleccionarArchivoPorEntrada.IsChecked == true)
                    ArchivoEntrada.ConfiguracionSeleccionArchivo = OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada;
            }
        }

        private void opcionCarpetaFija_Checked(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null)
            {
                if (opcionCarpetaFija.IsChecked == true)
                    ArchivoEntrada.ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada;
            }
        }

        private void opcionCarpetaArchivoCalculo_Checked(object sender, RoutedEventArgs e)
        {
            if (ArchivoEntrada != null)
            {
                if (opcionCarpetaArchivoCalculo.IsChecked == true)
                    ArchivoEntrada.ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion;
            }
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void QuitarRutaArchivo()
        {
            if (Entrada != null)
            {
                if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    ArchivoEntrada.RutaArchivoConjuntoNumerosEntrada = string.Empty;
                else if (Entrada.Tipo == TipoEntrada.Numero)
                    ArchivoEntrada.RutaArchivoEntrada = string.Empty;
                else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                    ArchivoEntrada.RutaArchivoConjuntoTextosEntrada = string.Empty;
            }
            else
                ArchivoEntrada.RutaArchivoEntrada = string.Empty;
        }

        private void opcionArchivoFijo_Unchecked(object sender, RoutedEventArgs e)
        {
            QuitarRutaArchivo();
        }

        private void opcionSeleccionarArchivo_Unchecked(object sender, RoutedEventArgs e)
        {
            QuitarRutaArchivo();
        }

        private void opcionSeleccionarArchivoPorEntrada_Unchecked(object sender, RoutedEventArgs e)
        {
            if(!ModoArchivoExterno)
                QuitarRutaArchivo();
        }

        private void opcionCarpetaFija_Unchecked(object sender, RoutedEventArgs e)
        {
            QuitarRutaArchivo();
        }

        private void opcionCarpetaArchivoCalculo_Unchecked(object sender, RoutedEventArgs e)
        {
            QuitarRutaArchivo();
        }
    }
}
