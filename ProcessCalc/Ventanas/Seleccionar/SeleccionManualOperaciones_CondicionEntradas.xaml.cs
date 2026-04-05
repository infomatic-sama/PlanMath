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
    /// Lógica de interacción para SeleccionManualOperaciones_CondicionEntradas.xaml
    /// </summary>
    public partial class SeleccionManualOperaciones_CondicionEntradas : Window
    {
        public List<DiseñoOperacion> Entradas { get; set; }
        public List<DiseñoOperacion> EntradasSeleccionadas { get; set; }
        public bool ModoToolTips { get; set; }
        public SeleccionManualOperaciones_CondicionEntradas()
        {
            InitializeComponent();
            Entradas = new List<DiseñoOperacion>();
            EntradasSeleccionadas = new List<DiseñoOperacion>();
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {            
            Entradas.Clear();

            foreach (var item in listaEntradas.Children)
            {
                CheckBox opcionOperacion = (CheckBox)item;
                if (opcionOperacion.IsChecked == true)
                    Entradas.Add((DiseñoOperacion)opcionOperacion.Tag);
            }            

            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var itemOperacion in Entradas)
            {
                CheckBox opcionOperacion = new CheckBox();
                opcionOperacion.Tag = itemOperacion;
                opcionOperacion.Content = itemOperacion.NombreCombo;
                opcionOperacion.Margin = new Thickness(10);

                opcionOperacion.IsChecked = EntradasSeleccionadas.Contains(itemOperacion);

                listaEntradas.Children.Add(opcionOperacion);
            }
            
            if(ModoToolTips)
            {
                opciones.Visibility = Visibility.Collapsed;
                opcionesPie.Visibility = Visibility.Visible;
            }
        }
    }
}
