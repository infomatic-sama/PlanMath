using ProcessCalc.Entidades.Entradas;
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
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para AgregarQuitar_CondicionSeleccionarCantidades_Entrada.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionSeleccionarCantidades_Entrada : Window
    {
        public bool Aceptar { get; set; }
        public bool ModoEdicion { get; set; }
        public CondicionSeleccionCantidadNumeros_Entrada Condicion { get; set; }
        public bool ModoPosicion { get; set; }
        public AgregarQuitar_CondicionSeleccionarCantidades_Entrada()
        {
            InitializeComponent();
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (!ModoPosicion && elementoBusquedaRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un elemento de lógica.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de lógica.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (conectorCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de conector con la lógica anterior.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números que deben cumplir la lógica.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility == Visibility.Visible && opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números que deben cunmplir la lógica.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int numero = 0;

                if (int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero))
                {
                    if (!ModoEdicion)
                    {
                        Condicion = new CondicionSeleccionCantidadNumeros_Entrada();
                        Condicion.CantidadNumerosCumplenCondicion = numero;
                    }

                    if(!ModoPosicion)   
                        Condicion.TipoElementoCondicion = (TipoElementoCondicion_SeleccionarNumeros_Entrada)int.Parse(((ComboBoxItem)elementoBusquedaRelacionadoCondicion.SelectedItem).Uid);
                    
                    Condicion.TipoOpcionCondicion = (TipoOpcionCondicion_SeleccionarNumeros_Entrada)int.Parse(((ComboBoxItem)opcionCondicion.SelectedItem).Uid);
                    Condicion.Valores_Condicion = valoresRelacinadosCondicion.Text;
                    Condicion.TipoElementoValores = (TipoElementoCondicion_SeleccionarNumeros_Entrada)int.Parse(((ComboBoxItem)elementoRelacionadoValores.SelectedItem).Uid);
                    Condicion.TipoConector = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)conectorCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadNumerosCumplenCondicion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem).Uid);

                    Condicion.ConectorO_Excluyente = (bool)conectorO_Excluyente.IsChecked;

                    Aceptar = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ingresa una cantidad válida que se deba cumplir la lógica.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ModoEdicion)
            {
                elementoBusquedaRelacionadoCondicion.SelectedItem = (from ComboBoxItem I in elementoBusquedaRelacionadoCondicion.Items where I.Uid == ((int)Condicion.TipoElementoCondicion).ToString() select I).FirstOrDefault();
                opcionCondicion.SelectedItem = (from ComboBoxItem I in opcionCondicion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion).ToString() select I).FirstOrDefault();
                valoresRelacinadosCondicion.Text = Condicion.Valores_Condicion;
                elementoRelacionadoValores.SelectedItem = (from ComboBoxItem I in elementoRelacionadoValores.Items where I.Uid == ((int)Condicion.TipoElementoValores).ToString() select I).FirstOrDefault();
                
                conectorCondicion.SelectedItem = (from ComboBoxItem I in conectorCondicion.Items where I.Uid == ((int)Condicion.TipoConector).ToString() select I).FirstOrDefault();
                conectorCondicion_SelectionChanged(this, null);
                conectorO_Excluyente.IsChecked = (bool)Condicion.ConectorO_Excluyente;

                agregarCondicion.Content = "Editar lógica";
                opcionCantidadNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                cantidadNumerosCumplenCondicion.Text = Condicion.CantidadNumerosCumplenCondicion.ToString();
            }

            if(ModoPosicion)
            {
                elementoBusquedaRelacionadoCondicion.Visibility = Visibility.Collapsed;
                textoElementoBusqueda.Visibility = Visibility.Collapsed;

                ((ComboBoxItem)elementoRelacionadoValores.Items[2]).Visibility = Visibility.Collapsed;
                ((ComboBoxItem)elementoRelacionadoValores.Items[3]).Visibility = Visibility.Collapsed;
            }
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

        private void elementoBusquedaRelacionadoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)elementoRelacionadoValores.SelectedItem).Uid == "5")
            {
                textoValoresCondicion.Visibility = Visibility.Visible;
                valoresRelacinadosCondicion.Visibility = Visibility.Visible;
            }
            else
            {
                textoValoresCondicion.Visibility = Visibility.Collapsed;
                valoresRelacinadosCondicion.Visibility = Visibility.Collapsed;
            }
        }

        private void conectorCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (conectorCondicion.SelectedItem == (from ComboBoxItem I in conectorCondicion.Items where I.Uid == "3" select I).FirstOrDefault())
            {
                conectorO_Excluyente.Visibility = Visibility.Visible;
            }
            else
            {
                conectorO_Excluyente.Visibility = Visibility.Collapsed;
            }
        }
    }
}
