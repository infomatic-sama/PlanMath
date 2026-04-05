using System.Windows;
using System.Windows.Controls;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Propiedades y lógica interna para EntradaEspecifica.xaml
    /// </summary>
    public partial class EntradaEspecifica : UserControl
    {
        public void MostrarOcultarSeDigitaTextosInformacion(bool mostrar)
        {
            if (mostrar)
            {
                btnSeDigitaTextosInformacion.FontWeight = FontWeights.Bold;
                opcionesTextosInformacionPredefinidosDigitacion.Visibility = Visibility.Visible;
                opcionCantidadFijaTextosDefinidosDigitacion.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeDigitaTextosInformacion.FontWeight = FontWeights.Normal;
                opcionesTextosInformacionPredefinidosDigitacion.Visibility = Visibility.Collapsed;
                opcionCantidadFijaTextosDefinidosDigitacion.Visibility = Visibility.Collapsed;
            }
        }

        public void MostrarOcultarTextosInformacionFijos(bool mostrar)
        {
            if (mostrar)
            {
                btnTextosInformacionFijos.Content = "Es conjunto fijo de " + entr.ConjuntoTextosInformacionFijos.Count.ToString() + " listas de textos de información";
                btnTextosInformacionFijos.FontWeight = FontWeights.Bold;
            }
            else
            {
                btnTextosInformacionFijos.Content = "Es conjunto de listas de textos de información fijos";
                btnTextosInformacionFijos.FontWeight = FontWeights.Normal;
            }
        }

        public void MostrarOcultarSeObtieneTextosInformacion(bool mostrar)
        {
            if (mostrar)
            {
                if (entr.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                    btnSeObtieneTextosInformacion.Content = MostrarTextoArchivoCarpetaSeleccionada();
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {
                    string cantidad = entr.ListaURLs.Count.ToString();
                    btnSeObtieneTextosInformacion.Content = "Se obtiene conjunto de textos de información en " + cantidad + " URLs de Internet.";
                }

                btnSeObtieneTextosInformacion.FontWeight = FontWeights.Bold;
                columnaSeObtieneOtroOrigenTextosInformacion.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeObtieneTextosInformacion.Content = "Se obtiene";
                btnSeObtieneTextosInformacion.FontWeight = FontWeights.Normal;
                columnaSeObtieneOtroOrigenTextosInformacion.Visibility = Visibility.Collapsed;
            }
        }
    }
}