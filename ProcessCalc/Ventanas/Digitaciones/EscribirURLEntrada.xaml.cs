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
    /// Lógica de interacción para EscribirURLEntrada.xaml
    /// </summary>
    public partial class EscribirURLEntrada : Window
    {
        public string TextoSeleccionado { get; set; }
        public bool Pausado { get; set; }
        public EscribirURLEntrada()
        {
            InitializeComponent();
        }

        private void seleccionarURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextoSeleccionado = seleccionarURL.Text;
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Pausado = true;
            Close();
        }
    }
}
