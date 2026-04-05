using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
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

namespace ProcessCalc.Ventanas.Configuraciones
{
    /// <summary>
    /// Lógica de interacción para ConfigurarOperaciones_Elemento.xaml
    /// </summary>
    public partial class ConfigurarOperaciones_Elemento : Window
    {
        public List<OperacionCadenaTexto> OperacionesCadenasTexto { get; set; }
        Brush FondoNormal;
        Brush FondoSeleccionado = System.Windows.Media.Brushes.LightBlue;
        DefinicionOperacionCadenaTexto OperacionSeleccionada;
        public bool ModoSeleccion {  get; set; }
        public ConfigurarOperaciones_Elemento()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            OperacionesCadenasTexto.Add(new OperacionCadenaTexto());
            DefinicionOperacionCadenaTexto definicion = new DefinicionOperacionCadenaTexto();
            definicion.ModoSeleccion = ModoSeleccion;
            definicion.Indice = OperacionesCadenasTexto.Count;
            FondoNormal = Background.Clone();
            definicion.Margin = new Thickness(10);
            definicion.Padding = new Thickness(5);
            definicion.Operacion = OperacionesCadenasTexto.Last();
            definicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
            operaciones.Children.Add(definicion);
        }

        private void TextoDefinicion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (DefinicionOperacionCadenaTexto item in operaciones.Children)
            {
                item.Background = FondoNormal.Clone();
            }

            ((DefinicionOperacionCadenaTexto)sender).Background = FondoSeleccionado.Clone();
            OperacionSeleccionada = (DefinicionOperacionCadenaTexto)sender;
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (OperacionSeleccionada != null)
            {
                OperacionesCadenasTexto.Remove(OperacionSeleccionada.Operacion);
                operaciones.Children.Remove(OperacionSeleccionada);
            }
        }

        private void ListarOperaciones()
        {
            int indice = 0;
            foreach (var item in OperacionesCadenasTexto)
            {
                indice++;
                DefinicionOperacionCadenaTexto definicion = new DefinicionOperacionCadenaTexto();
                definicion.ModoSeleccion = ModoSeleccion;
                definicion.Indice = indice;
                FondoNormal = Background.Clone();
                definicion.Margin = new Thickness(10);
                definicion.Padding = new Thickness(5);
                definicion.Operacion = item;
                definicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                operaciones.Children.Add(definicion);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(ModoSeleccion)
            {
                textoModoOperacion.Visibility = Visibility.Collapsed;
                textoModoSeleccion.Visibility = Visibility.Visible;

                Agregar.Visibility = Visibility.Collapsed;
                AgregarModoSeleccion.Visibility = Visibility.Visible;
                Quitar.Visibility = Visibility.Collapsed;
                QuitarModoSeleccion.Visibility = Visibility.Visible;

            }
            ListarOperaciones();
        }

        private void moverAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (OperacionSeleccionada != null &&
                OperacionesCadenasTexto.Any())
            {
                OperacionCadenaTexto definicion = OperacionSeleccionada.Operacion;

                int indice = OperacionesCadenasTexto.IndexOf(definicion);

                if (indice - 1 > -1)
                {
                    OperacionesCadenasTexto.RemoveAt(indice);
                    OperacionesCadenasTexto.Insert(indice - 1, definicion);

                    operaciones.Children.RemoveAt(indice);
                    operaciones.Children.Insert(indice - 1, OperacionSeleccionada);
                }
            }
        }

        private void moverADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (OperacionSeleccionada != null &&
                OperacionesCadenasTexto.Any())
            {
                OperacionCadenaTexto definicion = OperacionSeleccionada.Operacion;

                int indice = OperacionesCadenasTexto.IndexOf(definicion);

                if (indice < OperacionesCadenasTexto.Count - 1)
                {
                    OperacionesCadenasTexto.RemoveAt(indice);
                    OperacionesCadenasTexto.Insert(indice + 1, definicion);

                    operaciones.Children.RemoveAt(indice);
                    operaciones.Children.Insert(indice + 1, OperacionSeleccionada);
                }
            }
        }
    }
}
