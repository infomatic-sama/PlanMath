using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para ElementoDiagramaEjecucion.xaml
    /// </summary>
    public partial class ElementoDiagramaEjecucion : UserControl
    {
        private DiseñoOperacion elemento;
        public DiseñoOperacion ElementoRelacionado
        {
            get 
            {
                return elemento;
            }
            set
            {
                tipoElemento.Visibility = Visibility.Collapsed;

                if (value.Tipo == TipoElementoOperacion.Entrada)
                {
                    nombreElemento.Text = value.EntradaRelacionada.Nombre;
                    tipoElemento.Visibility = Visibility.Visible;

                    if (!EsEntrada)
                    {
                        switch (value.EntradaRelacionada.Tipo)
                        {
                            case TipoEntrada.Numero:
                                tipoElemento.Text = "Variable de número";
                                iconoNumero.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.ConjuntoNumeros:
                                tipoElemento.Text = "Vector de números";
                                iconoNumeros.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.Calculo:
                                tipoElemento.Text = "Entrada desde cálculo anterior";
                                iconoCalculo.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.TextosInformacion:
                                tipoElemento.Text = "Cadenas de texto";
                                iconoTextosInformacion.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.Ninguno:
                                tipoElemento.Text = string.Empty;
                                tipoElemento.Visibility = Visibility.Collapsed;
                                break;
                        }
                    }

                    if (EsEntrada && value.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    value.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                    {
                        iconoCalculo.Visibility = Visibility.Collapsed;

                        switch (value.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.Tipo)
                        {
                            case TipoEntrada.Numero:
                                tipoElemento.Text = "Variable de número";
                                iconoNumero.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.ConjuntoNumeros:
                                tipoElemento.Text = "Vector de números";
                                iconoNumeros.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.Calculo:
                                tipoElemento.Text = "Entrada desde cálculo anterior";
                                iconoCalculo.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.TextosInformacion:
                                tipoElemento.Text = "Cadenas de texto";
                                iconoTextosInformacion.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.Ninguno:
                                tipoElemento.Text = string.Empty;
                                tipoElemento.Visibility = Visibility.Collapsed;
                                break;
                        }
                    }
                    else
                    {
                        iconoCalculo.Visibility = Visibility.Collapsed;

                        switch (value.EntradaRelacionada.Tipo)
                        {
                            case TipoEntrada.Numero:
                                tipoElemento.Text = "Variable de número";
                                iconoNumero.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.ConjuntoNumeros:
                                tipoElemento.Text = "Vector de números";
                                iconoNumeros.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.Calculo:
                                tipoElemento.Text = "Entrada desde cálculo anterior";
                                iconoCalculo.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.TextosInformacion:
                                tipoElemento.Text = "Cadenas de texto";
                                iconoTextosInformacion.Visibility = Visibility.Visible;
                                break;
                            case TipoEntrada.Ninguno:
                                tipoElemento.Text = string.Empty;
                                tipoElemento.Visibility = Visibility.Collapsed;
                                break;
                        }
                    }
                }
                else if (value.Tipo == TipoElementoOperacion.Nota)
                {
                    nombreElemento.Text = value.Info;
                }
                else
                {
                    nombreElemento.Text = value.Nombre;
                    switch (value.Tipo)
                    {
                        case TipoElementoOperacion.Suma:
                            iconoSuma.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Resta:
                            iconoResta.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Multiplicacion:
                            iconoMultiplicacion.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Division:
                            iconoDivision.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Potencia:
                            iconoPotencia.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Porcentaje:
                            iconoPorcentaje.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Raiz:
                            iconoRaiz.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Logaritmo:
                            iconoLogaritmo.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Factorial:
                            iconoFactorial.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Inverso:
                            iconoInverso.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.ContarCantidades:
                            iconoContarCantidades.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.SeleccionarOrdenar:
                            iconoSeleccionarOrdenar.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                            iconoSeleccionarOrdenar_ConjuntoAgrupado.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.CondicionesFlujo:
                            iconoCondicionesFlujo.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.SeleccionarEntradas:
                            iconoSeleccionarEntradas.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.Espera:
                            iconoEspera.Visibility = Visibility.Visible;
                            break;
                        case TipoElementoOperacion.RedondearCantidades:
                            iconoRedondear.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.LimpiarDatos:
                            iconoLimpiarDatos.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.ArchivoExterno:
                            iconoArchivoExterno.Visibility = Visibility.Visible;
                            break;

                        case TipoElementoOperacion.SubCalculo:
                            iconoSubCalculo.Visibility = Visibility.Visible;
                            break;
                    }
                }

                elemento = value;
            }
        }

        private DiseñoCalculo elementoCalc;
        public DiseñoCalculo ElementoCalculoRelacionado
        {
            get
            {
                return elementoCalc;
            }
            set
            {
                tipoElemento.Visibility = Visibility.Collapsed;
                nombreElemento.Text = value.Nombre;
                iconoCalculo.Visibility = Visibility.Visible;

                elementoCalc = value;
            }
        }
        public VistaInformeResultados Ventana { get; set; }
        public bool Bloqueado { get; set; }
        public bool EsEntrada { get; set; }
        public ElementoDiagramaEjecucion()
        {
            InitializeComponent();
        }

        private void Clic(object sender, MouseButtonEventArgs e)
        {
            if (!Bloqueado)
            {
                if (Ventana != null)
                {
                    if (elemento == null)
                    {
                        Ventana.SubCalculoSeleccionado_VistaInformeResultados = elementoCalc;
                        Ventana.CargarDiagramaOperaciones_Calculo();
                    }
                    else
                    {
                        if (elemento.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                        {
                            Ventana.AgrupadorSeleccionado = elemento;
                            Ventana.AgrupadorAnterior = elemento.AgrupadorContenedor;
                            Ventana.CargarDiagramaOperaciones_Agrupador();
                        }
                    }
                }
            }
        }
    }
}
