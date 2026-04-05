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

namespace ProcessCalc.Controles.Busquedas
{
    /// <summary>
    /// Lógica de interacción para SeleccionarBusquedasNumeros.xaml
    /// </summary>
    public partial class SeleccionarBusquedasNumeros : UserControl
    {
        public List<BusquedaTextoArchivo> BusquedasEntrada { get; set; }
        public List<BusquedaTextoArchivo> BusquedasSeleccionadas { get; set; }
        public OpcionTextosInformacionBusqueda OpcionTextosInformacion { get; set; }
        public BusquedaTextoArchivo BusquedaSeleccionada { get; set; }
        public SeleccionarBusquedasNumeros()
        {
            InitializeComponent();
        }

        public void ListarBusquedas()
        {
            if (BusquedasEntrada != null)
            {
                busquedasSeleccionadas.Children.Clear();

                int indice = 0;
                

                foreach (var item in BusquedasEntrada)
                {
                    if (item != BusquedaSeleccionada)
                    {

                        busquedasSeleccionadas.RowDefinitions.Add(new RowDefinition());
                        busquedasSeleccionadas.RowDefinitions.Last().Height = GridLength.Auto;

                        TextBlock numero = new TextBlock();
                        numero.Text = (indice + 1).ToString();
                        numero.Margin = new Thickness(10);

                        busquedasSeleccionadas.Children.Add(numero);

                        Grid.SetRow(numero, indice);
                        Grid.SetColumn(numero, 0);

                        CheckBox texto = new CheckBox();
                        texto.Content = item.Nombre;
                        texto.Margin = new Thickness(10);
                        texto.Tag = item;

                        if (BusquedasSeleccionadas.Contains(item))
                            texto.IsChecked = true;
                        else
                            texto.IsChecked = false;

                        texto.Checked += Texto_Checked;
                        texto.Unchecked += Texto_UnChecked;

                        busquedasSeleccionadas.Children.Add(texto);

                        Grid.SetRow(texto, indice);
                        Grid.SetColumn(texto, 1);

                        indice++;
                    }
                }
            }
        }

        private void Texto_Checked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                BusquedasSeleccionadas.Add((BusquedaTextoArchivo)((CheckBox)sender).Tag);
            }
        }

        private void Texto_UnChecked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == false)
            {
                BusquedasSeleccionadas.Remove((BusquedaTextoArchivo)((CheckBox)sender).Tag);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListarBusquedas();
        }
    }
}
