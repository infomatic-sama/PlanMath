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
    /// Lógica de interacción para EstablecerCantidadFijaNumerosDefinidos_Digitacion.xaml
    /// </summary>
    public partial class EstablecerCantidadFijaNumerosDefinidos_Digitacion : Window
    {
        public Entrada Entrada { get; set; }
        public bool EsTextos { get; set; }
        public EstablecerCantidadFijaNumerosDefinidos_Digitacion()
        {
            InitializeComponent();
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void opcionFijarCantidad_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.FijarCantidadTextosDigitacion = (bool)opcionFijarCantidad.IsChecked;
                    }
                    else
                    {
                        Entrada.FijarCantidadNumerosDigitacion = (bool)opcionFijarCantidad.IsChecked;
                    }

                    cantidadNumeros.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionFijarCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.FijarCantidadTextosDigitacion = (bool)opcionFijarCantidad.IsChecked;
                    }
                    else
                    {
                        Entrada.FijarCantidadNumerosDigitacion = (bool)opcionFijarCantidad.IsChecked;
                    }

                    cantidadNumeros.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                int numeroVeces = 0;
                int.TryParse(cantidadNumeros.Text, out numeroVeces);

                if (numeroVeces <= 0)
                    cantidadNumeros.Text = "1";

                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.CantidadTextosDigitacion = numeroVeces;
                    }
                    else
                    {
                        Entrada.CantidadNumerosDigitacion = numeroVeces;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(EsTextos)
            {
                etiqueta1.Text = "cadenas de texto";
                etiqueta2.Text = "cadenas de texto";
                etiqueta3.Text = "cadenas de texto";

                etiqueta1Filas.Text = "filas de cadenas de texto";
                etiqueta2Filas.Text = "filas de cadenas de texto";
                etiqueta3Filas.Text = "filas de cadenas de texto";

                etiquetaFilas.Visibility = Visibility.Visible;
                etiquetaTexto.Visibility = Visibility.Visible;
            }
            else
            {
                etiqueta1.Text = "números";
                etiqueta2.Text = "números";
                etiqueta3.Text = "números";
            }

            if(Entrada != null)
            {
                if (EsTextos)
                {
                    opcionFijarCantidad.IsChecked = Entrada.FijarCantidadTextosDigitacion;
                    opcionDigitarCantidad.IsChecked = Entrada.DigitarEjecucionCantidadTextosDigitacion;
                    cantidadNumeros.Text = Entrada.CantidadTextosDigitacion.ToString();

                    opcionFijarCantidadFilas.IsChecked = Entrada.FijarCantidadFilasTextosDigitacion;
                    opcionDigitarCantidadFilas.IsChecked = Entrada.DigitarEjecucionCantidadFilasTextosDigitacion;
                    cantidadNumerosFilas.Text = Entrada.CantidadFilasTextosDigitacion.ToString();

                    opcionCantidadVariableFilas.Visibility = Visibility.Visible;
                    opcionFijarCantidadFilas.Visibility = Visibility.Visible;
                    opcionDigitarCantidadFilas.Visibility = Visibility.Visible;

                    if(Entrada.FijarCantidadFilasTextosDigitacion)
                        cantidadNumerosFilas.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionFijarCantidad.IsChecked = Entrada.FijarCantidadNumerosDigitacion;
                    opcionDigitarCantidad.IsChecked = Entrada.DigitarEjecucionCantidadNumerosDigitacion;
                    cantidadNumeros.Text = Entrada.CantidadNumerosDigitacion.ToString();
                }
            }
        }

        private void opcionDigitarCantidad_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.DigitarEjecucionCantidadTextosDigitacion = (bool)opcionDigitarCantidad.IsChecked;
                    }
                    else
                    {
                        Entrada.DigitarEjecucionCantidadNumerosDigitacion = (bool)opcionDigitarCantidad.IsChecked;
                    }
                }
            }
        }

        private void opcionDigitarCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.DigitarEjecucionCantidadTextosDigitacion = (bool)opcionDigitarCantidad.IsChecked;
                    }
                    else
                    {
                        Entrada.DigitarEjecucionCantidadNumerosDigitacion = (bool)opcionDigitarCantidad.IsChecked;
                    }
                }
            }
        }

        private void opcionFijarCantidadFilas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.FijarCantidadFilasTextosDigitacion = (bool)opcionFijarCantidadFilas.IsChecked;
                    }

                    cantidadNumerosFilas.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionFijarCantidadFilas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.FijarCantidadFilasTextosDigitacion = (bool)opcionFijarCantidadFilas.IsChecked;
                    }

                    cantidadNumerosFilas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionDigitarCantidadFilas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.DigitarEjecucionCantidadFilasTextosDigitacion = (bool)opcionDigitarCantidadFilas.IsChecked;
                    }
                }
            }
        }

        private void opcionDigitarCantidadFilas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.DigitarEjecucionCantidadFilasTextosDigitacion = (bool)opcionDigitarCantidadFilas.IsChecked;
                    }
                }
            }
        }

        private void cantidadNumerosFilas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                int numeroVeces = 0;
                int.TryParse(cantidadNumerosFilas.Text, out numeroVeces);

                if (numeroVeces <= 0)
                    cantidadNumerosFilas.Text = "1";

                if (Entrada != null)
                {
                    if (EsTextos)
                    {
                        Entrada.CantidadFilasTextosDigitacion = numeroVeces;
                    }
                }
            }
        }
    }
}
