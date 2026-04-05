using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para CondicionConjuntoBusquedas.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionConjuntoBusquedas : Window
    {
        public bool Aceptar { get; set; }
        public bool ModoEdicion { get; set; }
        public bool ModoTextoBusqueda { get; set; }
        public CondicionConjuntoBusquedas Condicion { get; set; }
        public List<BusquedaTextoArchivo> ListaBusquedas { get; set; }
        public AgregarQuitar_CondicionConjuntoBusquedas()
        {
            InitializeComponent();
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (busquedaRelacionadaCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una lógica de búsqueda.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (elementoBusquedaRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un elemento de lógica de búsqueda.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (conectorCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de conector con la condición anterior.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility == Visibility.Visible && opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int numero = 0;

                if(int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero))
                {
                    if (!ModoEdicion)
                    {
                        Condicion = new CondicionConjuntoBusquedas();
                        Condicion.CantidadNumerosCumplenCondicion = numero;
                    }

                    Condicion.BusquedaCondicion = (BusquedaTextoArchivo)busquedaRelacionadaCondicion.SelectedItem;
                    Condicion.TipoElementoCondicion = (TipoElementoCondicion_ConjuntoBusquedas)int.Parse(((ComboBoxItem)elementoBusquedaRelacionadoCondicion.SelectedItem).Uid);
                    Condicion.TipoOpcionCondicion = (TipoOpcionCondicion_ConjuntoBusquedas)int.Parse(((ComboBoxItem)opcionCondicion.SelectedItem).Uid);
                    Condicion.Valores_Condicion = valoresRelacinadosCondicion.Text;
                    Condicion.TipoConector = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)conectorCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadNumerosCumplenCondicion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.ConectorO_Excluyente = (bool)conectorO_Excluyente.IsChecked;

                    Aceptar = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ingresa una cantidad válida que se deba cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarBusquedas();

            if (ModoEdicion)
            {
                busquedaRelacionadaCondicion.SelectedItem = Condicion.BusquedaCondicion;
                elementoBusquedaRelacionadoCondicion.SelectedItem = (from ComboBoxItem I in elementoBusquedaRelacionadoCondicion.Items where I.Uid == ((int)Condicion.TipoElementoCondicion).ToString() select I).FirstOrDefault();
                opcionCondicion.SelectedItem = (from ComboBoxItem I in opcionCondicion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion).ToString() select I).FirstOrDefault();
                valoresRelacinadosCondicion.Text = Condicion.Valores_Condicion;
                conectorCondicion.SelectedItem = (from ComboBoxItem I in conectorCondicion.Items where I.Uid == ((int)Condicion.TipoConector).ToString() select I).FirstOrDefault();

                conectorCondicion_SelectionChanged(this, null);
                conectorO_Excluyente.IsChecked = (bool)Condicion.ConectorO_Excluyente;

                agregarCondicion.Content = "Editar condición";
                opcionCantidadNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                cantidadNumerosCumplenCondicion.Text = Condicion.CantidadNumerosCumplenCondicion.ToString();
            }

            if(ModoTextoBusqueda)
            {
                busquedaRelacionadaCondicion.SelectedIndex = 0;
                busquedaRelacionadaCondicion.IsEnabled = false;
                opcionCantidadBusquedasConjunto.Visibility = Visibility.Collapsed;
                opcionTextosBusqueda.Visibility = Visibility.Visible;

                elementoBusquedaRelacionadoCondicion_SelectionChanged(this, null);
            }
        }

        private void ListarBusquedas()
        {            
            List<BusquedaTextoArchivo> busquedas = new List<BusquedaTextoArchivo>();

            foreach (var itemBusqueda in ListaBusquedas)
            {
                itemBusqueda.NombreCombo = itemBusqueda.Nombre;
                busquedas.Add(itemBusqueda);

                foreach (var itemBusquedaConjunto in itemBusqueda.ConjuntoBusquedas)
                {
                    itemBusquedaConjunto.NombreCombo = itemBusqueda.Nombre + " - " + itemBusquedaConjunto.Nombre;
                    busquedas.Add(itemBusquedaConjunto);
                }
            }

            busquedaRelacionadaCondicion.DisplayMemberPath = "NombreCombo";
            busquedaRelacionadaCondicion.ItemsSource = busquedas;            
        }

        private void opcionCantidadNumerosCumplenCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadNumerosCumplenCondicion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility = Visibility.Visible;
                    cantidadNumerosCumplenCondicion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility = Visibility.Collapsed;
                    cantidadNumerosCumplenCondicion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadNumerosCumplenCondicion_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numero = 0;
            int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero);
            if (Condicion != null) Condicion.CantidadNumerosCumplenCondicion = numero;
        }

        private void opcionCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBoxItem)opcionCondicion.SelectedItem).Uid == "11" |
                ((ComboBoxItem)opcionCondicion.SelectedItem).Uid == "12")
            {
                textoValoresCondicion.Visibility = Visibility.Collapsed;
                valoresRelacinadosCondicion.Visibility = Visibility.Collapsed;
            }
            else
            {
                textoValoresCondicion.Visibility = Visibility.Visible;
                valoresRelacinadosCondicion.Visibility = Visibility.Visible;
            }
        }

        private void elementoBusquedaRelacionadoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elementoBusquedaRelacionadoCondicion.SelectedItem != null &&
                ((ComboBoxItem)elementoBusquedaRelacionadoCondicion.SelectedItem).Uid == "6")
            {
                opcionTextoBusquedaCoincida.Visibility = Visibility.Visible;
                opcionTextoBusquedaNoCoincida.Visibility = Visibility.Visible;

                //MostrarOcultasOpcionesCombo(Visibility.Collapsed);
            }
            else
            {
                opcionTextoBusquedaCoincida.Visibility = Visibility.Collapsed;
                opcionTextoBusquedaNoCoincida.Visibility = Visibility.Collapsed;

                //MostrarOcultasOpcionesCombo(Visibility.Visible);
            }
        }

        private void conectorCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(conectorCondicion.SelectedItem == (from ComboBoxItem I in conectorCondicion.Items where I.Uid == "3" select I).FirstOrDefault())
            {
                conectorO_Excluyente.Visibility = Visibility.Visible;
            }
            else
            {
                conectorO_Excluyente.Visibility = Visibility.Collapsed;
            }
        }

        //private void MostrarOcultasOpcionesCombo(Visibility visible)
        //{
        //    var opciones = from ComboBoxItem C in opcionCondicion.Items where C.GetType() == typeof(ComboBoxItem) &&
        //                   int.Parse(C.Uid) >= 1 && int.Parse(C.Uid) <= 10 select C;

        //    foreach(var item in opciones)
        //        item.Visibility = visible;
        //}
    }
}
