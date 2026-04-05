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
    /// Lógica de interacción para SeleccionManualOperaciones_CondicionFlujo.xaml
    /// </summary>
    public partial class SeleccionManualOperaciones_CondicionFlujo : Window
    {
        public List<DiseñoOperacion> Operaciones { get; set; }
        public List<DiseñoElementoOperacion> SubOperaciones { get; set; }
        public bool ModoOperacion { get; set; }
        public SeleccionManualOperaciones_CondicionFlujo()
        {
            InitializeComponent();
            Operaciones = new List<DiseñoOperacion>();
            SubOperaciones = new List<DiseñoElementoOperacion>();
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (ModoOperacion)
            {
                SubOperaciones.Clear();

                foreach (var item in listaOperaciones.Children)
                {
                    CheckBox opcionOperacion = (CheckBox)item;
                    if (opcionOperacion.IsChecked == true)
                        SubOperaciones.Add((DiseñoElementoOperacion)opcionOperacion.Tag);
                }
            }
            else
            {
                Operaciones.Clear();

                foreach (var item in listaOperaciones.Children)
                {
                    CheckBox opcionOperacion = (CheckBox)item;
                    if (opcionOperacion.IsChecked == true)
                        Operaciones.Add((DiseñoOperacion)opcionOperacion.Tag);
                }
            }

            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ModoOperacion)
            {
                foreach (var itemOperacion in SubOperaciones)
                {
                    CheckBox opcionOperacion = new CheckBox();
                    opcionOperacion.Tag = itemOperacion;
                    opcionOperacion.Content = itemOperacion.NombreCombo;
                    opcionOperacion.Margin = new Thickness(10);

                    listaOperaciones.Children.Add(opcionOperacion);
                }
            }
            else
            {
                foreach (var itemOperacion in Operaciones)
                {
                    CheckBox opcionOperacion = new CheckBox();
                    opcionOperacion.Tag = itemOperacion;
                    opcionOperacion.Content = itemOperacion.NombreCombo;
                    opcionOperacion.Margin = new Thickness(10);

                    listaOperaciones.Children.Add(opcionOperacion);
                }
            }
        }
    }
}
