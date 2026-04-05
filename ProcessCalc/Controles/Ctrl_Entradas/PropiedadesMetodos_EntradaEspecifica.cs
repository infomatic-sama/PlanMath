using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using ProcessCalc.Entidades;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Propiedades y lógica interna para EntradaEspecifica.xaml
    /// </summary>
    public partial class EntradaEspecifica : UserControl
    {
        private Entrada entr;
        public Entrada Entrada
        {
            set
            {
                nombreEntrada.Text = value.Nombre;
                opcionCantidadesComoTextosInformacion.IsChecked = value.AgregarCantidad_ComoTextoInformacion;
                opcionConfirmarComprobarCantidades.IsChecked = value.ComprobarConfirmarCantidades_Ejecucion;

                entr = value;

                if(VentanaEntrada == null && Calculo.EsEntradasArchivo &&
                    Calculo.ListaEntradas.Contains(entr))
                {
                    subir.Visibility = Visibility.Visible;
                    bajar.Visibility = Visibility.Visible;

                    nombre.Content = (Calculo.ListaEntradas.IndexOf(entr) + 1).ToString() + " " + nombre.Content.ToString();
                }

                MostrarOcultarOpciones_EjecutarFormaGeneral();
                opcionEjecutarEntradaDesdeCalculos.IsChecked = (bool)!entr.EjecutarDeFormaGeneral;
            }
            get
            {
                return entr;
            }
        }

        public Entradas VistaEntradas { get; set; }
        public MainWindow Ventana { get; set; }
        public bool EsPrimeraEntrada { get; set; }

        public void ActualizarTipo(TipoEntrada tipo)
        {
            opcionesNumero.Visibility = Visibility.Hidden;
            opcionesConjuntoNumeros.Visibility = Visibility.Hidden;

            bool actualizarEntradas_Textos = false;
            Entrada entradaAQuitar = null;

            if (entr.Tipo != tipo)
            {
                actualizarEntradas_Textos = true;

                if (entr.Tipo == TipoEntrada.TextosInformacion)
                    entradaAQuitar = entr;

                entr.TipoOpcionNumero = TipoOpcionNumeroEntrada.Ninguno;
                MostrarOcultarSeDigita(false);
                MostrarOcultarSeObtiene(false);
                MostrarOcultarNumeroFijo(false);

                entr.TipoOpcionConjuntoNumeros = TipoOpcionConjuntoNumerosEntrada.Ninguno;

                MostrarOcultarSeDigitaConjuntoNumeros(false);
                MostrarOcultarSeObtieneConjuntoNumeros(false);
                MostrarOcultarConjuntoNumerosFijo(false);

                entr.TipoOpcionTextosInformacion = TipoOpcionTextosInformacionEntrada.Ninguno;

                MostrarOcultarSeDigitaTextosInformacion(false);
                MostrarOcultarSeObtieneTextosInformacion(false);
                MostrarOcultarTextosInformacionFijos(false);

                Ventana.CerrarPestañasEntradaModificada(entr);
            }

            entr.Tipo = tipo;
            switch (entr.Tipo)
            {
                case TipoEntrada.Numero:
                    seleccionarTipo.Content = "Variables de números";
                    opcionesNumero.Visibility = Visibility.Visible;
                    opcionesConjuntoNumeros.Visibility = Visibility.Collapsed;
                    opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                    iconoNumero.Visibility = Visibility.Visible;
                    iconoNumeros.Visibility = Visibility.Collapsed;
                    iconoTextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesSeleccionCantidades.Visibility = Visibility.Collapsed;
                    break;
                case TipoEntrada.ConjuntoNumeros:
                    seleccionarTipo.Content = "Vectores de números";
                    opcionesNumero.Visibility = Visibility.Collapsed;
                    opcionesConjuntoNumeros.Visibility = Visibility.Visible;
                    opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                    iconoNumero.Visibility = Visibility.Collapsed;
                    iconoNumeros.Visibility = Visibility.Visible;
                    iconoTextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesSeleccionCantidades.Visibility = Visibility.Visible;
                    break;
                case TipoEntrada.TextosInformacion:
                    seleccionarTipo.Content = "Vectores de cadenas de texto";
                    opcionesTextosInformacion.Visibility = Visibility.Visible;
                    opcionesNumero.Visibility = Visibility.Collapsed;
                    opcionesConjuntoNumeros.Visibility = Visibility.Collapsed;
                    iconoNumero.Visibility = Visibility.Collapsed;
                    iconoNumeros.Visibility = Visibility.Collapsed;
                    iconoTextosInformacion.Visibility = Visibility.Visible;
                    opcionesSeleccionCantidades.Visibility= Visibility.Visible;
                    break;
            }

            if (actualizarEntradas_Textos)
                Ventana.ActualizarEntradas_TextosInformacion(entradaAQuitar);

            switch (entr.Tipo)
            {
                case TipoEntrada.Numero:
                    switch (entr.TipoOpcionNumero)
                    {
                        case TipoOpcionNumeroEntrada.NumeroFijo:
                            MostrarOcultarSeDigita(false);
                            MostrarOcultarSeObtiene(false);
                            MostrarOcultarNumeroFijo(true);
                            break;
                        case TipoOpcionNumeroEntrada.SeDigita:
                            MostrarOcultarNumeroFijo(false);
                            MostrarOcultarSeObtiene(false);
                            MostrarOcultarSeDigita(true);
                            break;
                        case TipoOpcionNumeroEntrada.SeObtiene:
                            MostrarOcultarNumeroFijo(false);
                            MostrarOcultarSeDigita(false);
                            MostrarOcultarSeObtiene(true);
                            break;
                    }

                    break;
                case TipoEntrada.ConjuntoNumeros:
                    switch (entr.TipoOpcionConjuntoNumeros)
                    {
                        case TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo:
                            MostrarOcultarSeDigitaConjuntoNumeros(false);
                            MostrarOcultarSeObtieneConjuntoNumeros(false);
                            MostrarOcultarConjuntoNumerosFijo(true);
                            break;
                        case TipoOpcionConjuntoNumerosEntrada.SeDigita:
                            MostrarOcultarConjuntoNumerosFijo(false);
                            MostrarOcultarSeObtieneConjuntoNumeros(false);
                            MostrarOcultarSeDigitaConjuntoNumeros(true);
                            break;
                        case TipoOpcionConjuntoNumerosEntrada.SeObtiene:
                            MostrarOcultarConjuntoNumerosFijo(false);
                            MostrarOcultarSeDigitaConjuntoNumeros(false);
                            MostrarOcultarSeObtieneConjuntoNumeros(true);
                            break;
                    }

                    break;
                case TipoEntrada.TextosInformacion:
                    switch (entr.TipoOpcionTextosInformacion)
                    {
                        case TipoOpcionTextosInformacionEntrada.TextosInformacionFijos:
                            MostrarOcultarSeDigitaTextosInformacion(false);
                            MostrarOcultarSeObtieneTextosInformacion(false);
                            MostrarOcultarTextosInformacionFijos(true);
                            break;
                        case TipoOpcionTextosInformacionEntrada.SeDigita:
                            MostrarOcultarTextosInformacionFijos(false);
                            MostrarOcultarSeObtieneTextosInformacion(false);
                            MostrarOcultarSeDigitaTextosInformacion(true);
                            break;
                        case TipoOpcionTextosInformacionEntrada.SeObtiene:
                            MostrarOcultarTextosInformacionFijos(false);
                            MostrarOcultarSeDigitaConjuntoNumeros(false);
                            MostrarOcultarSeObtieneTextosInformacion(true);
                            break;
                    }
                    break;
            }

            MostrarOcultarOpciones_EjecutarFormaGeneral();
        }

        public void MostrarOcultarOpciones_EjecutarFormaGeneral()
        {
            if (Calculo.EsEntradasArchivo &&
                    entr.Tipo != TipoEntrada.TextosInformacion)
                opcionEjecutarEntradaDesdeCalculos.Visibility = Visibility.Visible;
            else
            {
                opcionEjecutarEntradaDesdeCalculos.Visibility = Visibility.Collapsed;
            }
        }

        public void MostrarOcultarNumeroFijo(bool mostrar)
        {
            if (mostrar)
            {
                btnNumeroFijo.Content = "Variable de número: " + entr.NumeroFijo.ToString();
                btnNumeroFijo.FontWeight = FontWeights.Bold;
            }
            else
            {
                btnNumeroFijo.Content = "Es variable de número fija";
                btnNumeroFijo.FontWeight = FontWeights.Normal;
            }
        }

        public void MostrarOcultarSeDigita(bool mostrar)
        {
            if (mostrar)
            {
                btnSeDigita.FontWeight = FontWeights.Bold;
                opcionesTextosInformacionPredefinidosDigitacion.Visibility = Visibility.Visible;
                opcionListaNumerosPredefinidosDigitacion.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeDigita.FontWeight = FontWeights.Normal;
                opcionesTextosInformacionPredefinidosDigitacion.Visibility = Visibility.Collapsed;
                opcionListaNumerosPredefinidosDigitacion.Visibility = Visibility.Collapsed;
            }
        }

        public void MostrarOcultarSeObtiene(bool mostrar)
        {
            if (mostrar)
            {
                if (entr.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                    btnSeObtiene.Content = MostrarTextoArchivoCarpetaSeleccionada();
                else if (entr.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet)
                    btnSeObtiene.Content = "Se obtiene variable de número desde una URL de Internet";

                btnSeObtiene.FontWeight = FontWeights.Bold;
                columnaSeObtieneOtroOrigen.Visibility = Visibility.Visible;
            }
            else
            {
                btnSeObtiene.Content = "Se obtiene";
                btnSeObtiene.FontWeight = FontWeights.Normal;
                columnaSeObtieneOtroOrigen.Visibility = Visibility.Collapsed;
            }
        }
    }
}