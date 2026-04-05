using ProcessCalc.Controles.Archivos;
using ProcessCalc.Entidades;
using ProcessCalc.Ventanas.Entradas;
using ProcessCalc.Vistas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para InfoCalculo.xaml
    /// </summary>
    public partial class InfoCalculo : UserControl
    {           
        private Calculo calc;
        public Calculo Calculo
        {
            set
            {
                descripcion.Text = value.Descripcion;
                entradas.Text = value.Entradas;
                resultados.Text = value.Resultados;

                opcionMostrarInformacionElementos_InformeResultados.IsChecked = value.MostrarInformacionElementos_InformeResultados;

                nombreArchivo.Content = value.NombreArchivo;
                rutaArchivo.Content = value.RutaArchivo;
                cantidadDecimales.Text = value.CantidadDecimalesCantidades.ToString();

                opcionNoMostrarCeros_InformeEjecucionResultados.IsChecked = value.NoMostrarCeros_InformeEjecucionResultados;
                opcionNoMostrarSinTextos_InformeEjecucionResultados.IsChecked = value.NoMostrarCantidadesSinTextos_InformeEjecucionResultados;

                calc = value;
                //ListarConfiguracionesEntradas_Ejecucion();
            }
            get
            {
                return calc;
            }
        }
        public InfoCalculo()
        {
            calc = new Calculo();
            InitializeComponent();
        }

        public void ListarConfiguracionesEntradas_Ejecucion()
        {
            configEntradas.Children.Clear();

            foreach (var itemConfig in calc.EntradasOperandos_ArchivoExterno)
            {
                ConfigEntradas_ArchivoExterno config = new ConfigEntradas_ArchivoExterno();
                config.NombreEntrada = itemConfig.Entrada.NombreCombo;
                config.IDEntrada = itemConfig.Entrada.ID;
                config.Config = itemConfig.Configuracion;

                configEntradas.Children.Add(config);
            }
        }

        private void descripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc.Descripcion = descripcion.Text;
        }

        private void entradas_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc.Entradas = entradas.Text;
        }

        private void resultados_TextChanged(object sender, TextChangedEventArgs e)
        {
            calc.Resultados = resultados.Text;
        }

        private void contenido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calculo.TabSeleccionada_TextosInformacion = contenido.SelectedIndex;
        }

        private void opcionMostrarInformacionElementos_InformeResultados_Checked(object sender, RoutedEventArgs e)
        {
            Calculo.MostrarInformacionElementos_InformeResultados = true;
        }

        private void opcionMostrarInformacionElementos_InformeResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            Calculo.MostrarInformacionElementos_InformeResultados = false;
        }

        private void cantidadDecimales_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Calculo != null)
            {
                int cantidad = 0;
                bool cantidadValida = int.TryParse(cantidadDecimales.Text, out cantidad);

                if (cantidad > 10)
                {
                    cantidadDecimales.Text = "10";
                }
                else if (cantidad < 0)
                {
                    cantidadDecimales.Text = "0";
                }
                else if (!cantidadValida)
                {
                    cantidadDecimales.Text = "0";
                }
                else
                    Calculo.CantidadDecimalesCantidades = cantidad;
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void configCantidadesDigitadas_Click(object sender, RoutedEventArgs e)
        {
            AdmCantidadesDigitadas admCantidadesDigitadas = new AdmCantidadesDigitadas();
            admCantidadesDigitadas.Calculo = Calculo;
            admCantidadesDigitadas.ShowDialog();
        }

        private void opcionNoMostrarCeros_InformeEjecucionResultados_Checked(object sender, RoutedEventArgs e)
        {
            Calculo.NoMostrarCeros_InformeEjecucionResultados = true;
        }

        private void opcionNoMostrarCeros_InformeEjecucionResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            Calculo.NoMostrarCeros_InformeEjecucionResultados = false;
        }

        private void opcionNoMostrarSinTextos_InformeEjecucionResultados_Checked(object sender, RoutedEventArgs e)
        {
            Calculo.NoMostrarCantidadesSinTextos_InformeEjecucionResultados = true;
        }

        private void opcionNoMostrarSinTextos_InformeEjecucionResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            Calculo.NoMostrarCantidadesSinTextos_InformeEjecucionResultados = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                opcionMostrarInformacionElementos_InformeResultados.Visibility = Visibility.Collapsed;
                opcionesMostrarCantidadesInforme_EjecucionResultados.Visibility = Visibility.Collapsed;
            }

            ListarConfiguracionesEntradas_Ejecucion();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirConfiguracionArchivoCalculo");
#endif
        }
    }
}
