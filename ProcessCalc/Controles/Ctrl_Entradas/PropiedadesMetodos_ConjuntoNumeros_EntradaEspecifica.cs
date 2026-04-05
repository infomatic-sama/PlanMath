using System.Windows;
using System.Windows.Controls;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Propiedades y lógica interna para EntradaEspecifica.xaml
    /// </summary>
    public partial class EntradaEspecifica : UserControl
    {
        public void MostrarOcultarSeDigitaConjuntoNumeros(bool mostrar)
        {
            if (mostrar)
            {
                btnSeDigitaConjunto.FontWeight = FontWeights.Bold;
                opcionesTextosInformacionPredefinidosDigitacion.Visibility = Visibility.Visible;
                opcionListaNumerosPredefinidosDigitacion.Visibility = Visibility.Visible;
                opcionCantidadFijaNumerosDefinidosDigitacion.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeDigitaConjunto.FontWeight = FontWeights.Normal;
                opcionesTextosInformacionPredefinidosDigitacion.Visibility = Visibility.Collapsed;
                opcionListaNumerosPredefinidosDigitacion.Visibility = Visibility.Collapsed;
                opcionCantidadFijaNumerosDefinidosDigitacion.Visibility = Visibility.Collapsed;
            }
        }

        public void MostrarOcultarConjuntoNumerosFijo(bool mostrar)
        {
            if (mostrar)
            {
                btnConjuntoNumerosFijo.Content = "Es conjunto fijo de " + entr.ConjuntoNumerosFijo.Count.ToString() + " números";
                btnConjuntoNumerosFijo.FontWeight = FontWeights.Bold;
            }
            else
            {
                btnConjuntoNumerosFijo.Content = "Es conjunto de números fijo";
                btnConjuntoNumerosFijo.FontWeight = FontWeights.Normal;
            }
        }

        public void MostrarOcultarSeObtieneConjuntoNumeros(bool mostrar)
        {
            if (mostrar)
            {
                if (entr.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                    btnSeObtieneConjunto.Content = MostrarTextoArchivoCarpetaSeleccionada();
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                {
                    string cantidad = entr.ListaURLs.Count.ToString();
                    btnSeObtieneConjunto.Content = "Se obtiene conjunto de números en " + cantidad + " URLs de Internet.";
                }

                btnSeObtieneConjunto.FontWeight = FontWeights.Bold;
                columnaSeObtieneOtroOrigenConjuntoNums.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeObtieneConjunto.Content = "Se obtiene";
                btnSeObtieneConjunto.FontWeight = FontWeights.Normal;
                columnaSeObtieneOtroOrigenConjuntoNums.Visibility = Visibility.Collapsed;
            }
        }
    }
}