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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para ResultadoInfoCalculo.xaml
    /// </summary>
    public partial class ResultadoInfoCalculo : UserControl
    {
        private Resultado result;
        public Resultado Resultado
        {
            set
            {
                switch (value.SalidaRelacionada.Tipo)
                {
                    case TipoElementoOperacion.Entrada:
                        salida.Text = value.SalidaRelacionada.EntradaRelacionada.Nombre;

                        switch (value.SalidaRelacionada.EntradaRelacionada.Tipo)
                        {
                            case TipoEntrada.Numero:
                                salida.Text += " Variable de número";
                                break;
                            case TipoEntrada.ConjuntoNumeros:
                                salida.Text += " Vector de números";
                                break;
                            case TipoEntrada.Calculo:
                                salida.Visibility = Visibility.Collapsed;
                                break;
                        }
                        break;
                    default:
                        salida.Text = value.SalidaRelacionada.Nombre;
                        salida.Text += " Operación ";

                        switch (value.SalidaRelacionada.Tipo)
                        {
                            case TipoElementoOperacion.Suma:
                                salida.Text += "suma";
                                break;

                            case TipoElementoOperacion.Resta:
                                salida.Text += "resta";
                                break;

                            case TipoElementoOperacion.Multiplicacion:
                                salida.Text += "multiplicación";
                                break;

                            case TipoElementoOperacion.Division:
                                salida.Text += "división";
                                break;

                            case TipoElementoOperacion.Porcentaje:
                                salida.Text += "porcentaje";
                                break;

                            case TipoElementoOperacion.Potencia:
                                salida.Text += "potencia";
                                break;

                            case TipoElementoOperacion.Raiz:
                                salida.Text += "raíz";
                                break;

                            case TipoElementoOperacion.Logaritmo:
                                salida.Text += "logaritmo";
                                break;

                            case TipoElementoOperacion.Inverso:
                                salida.Text += "inverso";
                                break;

                            case TipoElementoOperacion.Factorial:
                                salida.Text += "factorial";
                                break;

                            case TipoElementoOperacion.ContarCantidades:
                                salida.Text += "contar números de variables, vectores de entrada y retornados";
                                break;

                            case TipoElementoOperacion.SeleccionarOrdenar:
                                salida.Text += "seleccionar y ordenar números de variables, vectores de entrada y retornados";
                                break;

                            case TipoElementoOperacion.CondicionesFlujo:
                                salida.Text += "seleccionar variables, vectores de entrada y retornados con condiciones";
                                break;

                            case TipoElementoOperacion.SeleccionarEntradas:
                                salida.Text += "seleccionar variables o vectores de entrada con condiciones";
                                break;

                            case TipoElementoOperacion.RedondearCantidades:
                                salida.Text += "redondear números de variables, vectores de entrada y retornados";
                                break;
                        }

                        break;
                }

                if (!string.IsNullOrEmpty(value.Nombre))
                    nombreResultado.Text = value.Nombre;
                else
                    nombreResultado.Text = string.Empty;

                if (!string.IsNullOrEmpty(value.Descripcion))
                    descripcionResultado.Text = value.Descripcion;
                else
                    descripcionResultado.Text = string.Empty;

                result = value;
            }
            get
            {
                return result;
            }
        }
        public ResultadoInfoCalculo()
        {
            InitializeComponent();
            botonFondo.Background = Brushes.Transparent;
        }
    }
}
