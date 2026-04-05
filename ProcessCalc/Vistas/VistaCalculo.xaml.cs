using ProcessCalc.Controles;
using ProcessCalc.Entidades;
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

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para InfoCalculo.xaml
    /// </summary>
    public partial class VistaCalculo : UserControl
    {
        private Calculo calc;
        public Calculo Calculo
        {
            set
            {
                descripcion.Text = value.Descripcion;
                entradas.Text = value.Entradas;
                resultados.Text = value.Resultados;

                nombreArchivo.Content = value.NombreArchivo;
                rutaArchivo.Content = value.RutaArchivo;

                calc = value;
            }
            get
            {
                return calc;
            }
        }
        public MainWindow Ventana { get; set; }
        public VistaCalculo()
        {
            calc = new Calculo();
            InitializeComponent();
        }

        public void ListarEntradas()
        {
            listaEntradas.Children.Clear();
            foreach (var itemEntrada in Calculo.SubCalculoSeleccionado_VistaCalculo.ListaEntradas)
            {
                EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                nuevaEntrada.Entrada = itemEntrada;
                nuevaEntrada.Bloqueada = true;
                nuevaEntrada.EnDiagrama = true;
                //nuevaEntrada.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                listaEntradas.Children.Add(nuevaEntrada);
            }

            foreach (var itemCalculo in Calculo.SubCalculoSeleccionado_VistaCalculo.ElementosAnteriores)
            {
                foreach (var itemEntrada in (from O in itemCalculo.ElementosOperaciones where O.ContieneSalida select O).ToList())
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                    nuevaEntrada.Bloqueada = true;
                    nuevaEntrada.EnDiagrama = true;

                    Entrada nuevaElementoEntrada = new Entrada();
                    if (itemEntrada.Tipo != TipoElementoOperacion.Entrada)
                        nuevaElementoEntrada.Nombre = itemEntrada.Nombre + " desde " + itemCalculo.Nombre;
                    else
                        nuevaElementoEntrada.Nombre = itemEntrada.EntradaRelacionada.Nombre + " desde " + itemCalculo.Nombre;

                    nuevaElementoEntrada.Tipo = TipoEntrada.Calculo;

                    nuevaElementoEntrada.ElementoSalidaCalculoAnterior = itemEntrada;
                    nuevaEntrada.Entrada = nuevaElementoEntrada;
                    listaEntradas.Children.Add(nuevaEntrada);
                }
            }
        }

        public void ListarResultados()
        {
            listaResultados.Children.Clear();

            foreach (var itemResultado in Calculo.ListaResultados)
            {
                ResultadoInfoCalculo nuevoResultado = new ResultadoInfoCalculo();
                nuevoResultado.Resultado = itemResultado;
                //nuevoResultado.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                listaResultados.Children.Add(nuevoResultado);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            ListarCalculos();
            ListarEntradas();
            ListarResultados();

            MarcarCalculoSeleccionado();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("ConsultarInformacionArchivoCalculo");
#endif
        }

        public void ListarCalculos()
        {
            calculos.Children.Clear();
            calculos.RowDefinitions.Clear();
            calculos.ColumnDefinitions.Clear();

            ColumnDefinition definicionColumna = new ColumnDefinition();
            definicionColumna.Width = new GridLength(1, GridUnitType.Star);

            calculos.ColumnDefinitions.Add(definicionColumna);

            for (int cantidadFilas = 1; cantidadFilas <= calc.Calculos.Count; cantidadFilas++)
            {
                RowDefinition definicionFila = new RowDefinition();
                definicionFila.Height = GridLength.Auto;

                calculos.RowDefinitions.Add(definicionFila);
            }

            int indiceFila = 0;

            foreach (var itemCalculo in calc.Calculos)
            {
                CalculoEspecifico nuevoCalculo = new CalculoEspecifico();
                nuevoCalculo.Margin = new Thickness(10);
                nuevoCalculo.CalculoDiseño = itemCalculo;
                nuevoCalculo.botonFondo.Click += SeleccionarCalculo;
                nuevoCalculo.botonFondo.Background = SystemColors.InactiveBorderBrush;
                calculos.Children.Add(nuevoCalculo);

                Grid.SetRow(nuevoCalculo, indiceFila);
                indiceFila++;
            }
        }

        private void SeleccionarCalculo(object sender, RoutedEventArgs e)
        {
            Calculo.SubCalculoSeleccionado_VistaCalculo = ((CalculoEspecifico)((Control)sender).Parent).CalculoDiseño;

            MarcarCalculoSeleccionado();
            ListarEntradas();
        }

        public void MarcarCalculoSeleccionado()
        {
            foreach (var itemCalculo in calculos.Children)
            {
                ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.InactiveBorderBrush;
            }

            if (Calculo.SubCalculoSeleccionado_VistaCalculo != null)
            {
                foreach (var itemCalculo in calculos.Children)
                {
                    if (((CalculoEspecifico)itemCalculo).CalculoDiseño == Calculo.SubCalculoSeleccionado_VistaCalculo)
                    {
                        ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                    }
                }
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
