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
    /// Lógica de interacción para SeleccionarDefinicion.xaml
    /// </summary>
    public partial class SeleccionarDefinicion : Window
    {
        public bool DefinicionSimple { get; set; }
        public SeleccionarDefinicion()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void optDefinicionNormal_Checked(object sender, RoutedEventArgs e)
        {
            if (optDefinicionNormal.IsChecked == true)
                DefinicionSimple = false;
        }

        private void optDefinicionSimple_Checked(object sender, RoutedEventArgs e)
        {
            if (optDefinicionSimple.IsChecked == true)
                DefinicionSimple = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DefinicionSimple)
                optDefinicionSimple.IsChecked = DefinicionSimple;
            else
                optDefinicionNormal.IsChecked = true;
        }
    }
}
