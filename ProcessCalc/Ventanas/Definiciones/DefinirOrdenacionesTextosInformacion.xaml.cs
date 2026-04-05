using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
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
    /// Lógica de interacción para DefinirOrdenacionesTextosInformacion.xaml
    /// </summary>
    public partial class DefinirOrdenacionesTextosInformacion : Window
    {
        public List<OrdenacionNumeros> Ordenaciones { get; set; }
        Brush FondoNormal;
        Brush FondoSeleccionado = System.Windows.Media.Brushes.LightBlue;
        DefinicionOrdenacionTextosInformacion TextoDefinicionSeleccionado;
        public bool RevertirListaTextos {  get; set; }
        public DefinirOrdenacionesTextosInformacion()
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
            Ordenaciones.Add(new OrdenacionNumeros());
            DefinicionOrdenacionTextosInformacion definicion = new DefinicionOrdenacionTextosInformacion();
            definicion.Indice = Ordenaciones.Count;
            FondoNormal = Background.Clone();
            definicion.Margin = new Thickness(10);
            definicion.Padding = new Thickness(5);
            definicion.Ordenacion = Ordenaciones.Last();
            definicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
            ordenaciones.Children.Add(definicion);
        }

        private void TextoDefinicion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (DefinicionOrdenacionTextosInformacion item in ordenaciones.Children)
            {
                item.Background = FondoNormal.Clone();
            }

            ((DefinicionOrdenacionTextosInformacion)sender).Background = FondoSeleccionado.Clone();
            TextoDefinicionSeleccionado = (DefinicionOrdenacionTextosInformacion)sender;
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null)
            {
                Ordenaciones.Remove(TextoDefinicionSeleccionado.Ordenacion);
                ordenaciones.Children.Remove(TextoDefinicionSeleccionado);
            }
        }
        private void ListarOrdenaciones()
        {
            int indice = 0;
            foreach (var item in Ordenaciones)
            {
                indice++;
                DefinicionOrdenacionTextosInformacion definicion = new DefinicionOrdenacionTextosInformacion();
                definicion.Indice = indice;
                FondoNormal = Background.Clone();
                definicion.Margin = new Thickness(10);
                definicion.Padding = new Thickness(5);
                definicion.Ordenacion = item;
                definicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                ordenaciones.Children.Add(definicion);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            opcionInvertirListaTextos.IsChecked = (bool)RevertirListaTextos;
            ListarOrdenaciones();
        }

        private void moverCondicionAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null &&
                Ordenaciones.Any())
            {
                OrdenacionNumeros definicion = TextoDefinicionSeleccionado.Ordenacion;

                int indice = Ordenaciones.IndexOf(definicion);

                if (indice - 1 > -1)
                {
                    Ordenaciones.RemoveAt(indice);
                    Ordenaciones.Insert(indice - 1, definicion);

                    ordenaciones.Children.RemoveAt(indice);
                    ordenaciones.Children.Insert(indice - 1, TextoDefinicionSeleccionado);
                }
            }
        }

        private void moverCondicionADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null &&
                Ordenaciones.Any())
            {
                OrdenacionNumeros definicion = TextoDefinicionSeleccionado.Ordenacion;

                int indice = Ordenaciones.IndexOf(definicion);

                if (indice < Ordenaciones.Count - 1)
                {
                    Ordenaciones.RemoveAt(indice);
                    Ordenaciones.Insert(indice + 1, definicion);

                    ordenaciones.Children.RemoveAt(indice);
                    ordenaciones.Children.Insert(indice + 1, TextoDefinicionSeleccionado);
                }
            }
        }

        private void opcionInvertirListaTextos_Checked(object sender, RoutedEventArgs e)
        {
            RevertirListaTextos = (bool)opcionInvertirListaTextos.IsChecked;
        }

        private void opcionInvertirListaTextos_Unchecked(object sender, RoutedEventArgs e)
        {
            RevertirListaTextos = (bool)opcionInvertirListaTextos.IsChecked;
        }
    }
}
