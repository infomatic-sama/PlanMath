using ProcessCalc.Entidades;
using System;
using System.CodeDom;
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
    /// Lógica de interacción para ResultadoEspecifico.xaml
    /// </summary>
    public partial class ResultadoEspecifico : UserControl
    {
        private Resultado result;
        public Resultado Resultado
        {
            set
            {
                switch (value.SalidaRelacionada.Tipo)
                {
                    case TipoElementoOperacion.Entrada:
                        nombreSalida.Text = value.SalidaRelacionada.EntradaRelacionada.Nombre;

                        switch (value.SalidaRelacionada.EntradaRelacionada.Tipo)
                        {
                            case TipoEntrada.Numero:
                                tipoSalida.Text = "Variable de número";
                                break;
                            case TipoEntrada.ConjuntoNumeros:
                                tipoSalida.Text = "Vector de números";
                                break;
                            case TipoEntrada.Calculo:
                                tipoSalida.Visibility = Visibility.Collapsed;
                                break;
                        }                        
                        break;
                    default:
                        nombreSalida.Text = value.SalidaRelacionada.Nombre;
                        tipoSalida.Text = "Operación ";

                        switch (value.SalidaRelacionada.Tipo)
                        {
                            case TipoElementoOperacion.Suma:
                                tipoSalida.Text += "suma";
                                break;

                            case TipoElementoOperacion.Resta:
                                tipoSalida.Text += "resta";
                                break;

                            case TipoElementoOperacion.Multiplicacion:
                                tipoSalida.Text += "multiplicación";
                                break;

                            case TipoElementoOperacion.Division:
                                tipoSalida.Text += "división";
                                break;

                            case TipoElementoOperacion.Porcentaje:
                                tipoSalida.Text += "porcentaje";
                                break;

                            case TipoElementoOperacion.Potencia:
                                tipoSalida.Text += "potencia";
                                break;

                            case TipoElementoOperacion.Raiz:
                                tipoSalida.Text += "raíz";
                                break;

                            case TipoElementoOperacion.Logaritmo:
                                tipoSalida.Text += "logaritmo";
                                break;

                            case TipoElementoOperacion.Inverso:
                                tipoSalida.Text += "inverso";
                                break;

                            case TipoElementoOperacion.Factorial:
                                tipoSalida.Text += "factorial";
                                break;

                            case TipoElementoOperacion.ContarCantidades:
                                tipoSalida.Text += "contar números de variables, vectores de entrada y retornados";
                                break;

                            case TipoElementoOperacion.SeleccionarOrdenar:
                                tipoSalida.Text += "seleccionar y ordenar números de variables, vectores de entrada y retornados";
                                break;

                            case TipoElementoOperacion.CondicionesFlujo:
                                tipoSalida.Text += "seleccionar variables, vectores de entrada y retornados con condiciones";
                                break;

                            case TipoElementoOperacion.SeleccionarEntradas:
                                tipoSalida.Text += "seleccionar variables o vectores de entrada con condiciones";
                                break;

                            case TipoElementoOperacion.RedondearCantidades:
                                tipoSalida.Text += "redondear números de variables, vectores de entrada y retornados";
                                break;
                        }

                        break;
                }

                if (!string.IsNullOrEmpty(value.Nombre))
                    nombreResultado.Text = value.Nombre;

                if (!string.IsNullOrEmpty(value.Descripcion))
                    descripcionResultado.Text = value.Descripcion;

                opcionNoMostrarSiNoTieneNumeros.IsChecked = value.NoMostrar_SiEsConjunto_SiNoTieneNumeros;
                opcionNoMostrarSiEsCero.IsChecked = value.NoMostrar_SiEsCero;

                result = value;
            }
            get
            {
                return result;
            }
        }
        public ResultadoEspecifico()
        {
            InitializeComponent();
        }

        private void nombreResultado_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (result != null) result.Nombre = nombreResultado.Text;
        }

        private void descripcionResultado_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (result != null) result.Descripcion = descripcionResultado.Text;
        }

        private void opcionNoMostrarSiNoTieneNumeros_Checked(object sender, RoutedEventArgs e)
        {
            if (result != null) result.NoMostrar_SiEsConjunto_SiNoTieneNumeros = (bool)opcionNoMostrarSiNoTieneNumeros.IsChecked;
        }

        private void opcionNoMostrarSiNoTieneNumeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (result != null) result.NoMostrar_SiEsConjunto_SiNoTieneNumeros = (bool)opcionNoMostrarSiNoTieneNumeros.IsChecked;
        }

        private void opcionNoMostrarSiEsCero_Checked(object sender, RoutedEventArgs e)
        {
            if (result != null) result.NoMostrar_SiEsCero = (bool)opcionNoMostrarSiEsCero.IsChecked;
        }

        private void opcionNoMostrarSiEsCero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (result != null) result.NoMostrar_SiEsCero = (bool)opcionNoMostrarSiEsCero.IsChecked;
        }
    }
}
