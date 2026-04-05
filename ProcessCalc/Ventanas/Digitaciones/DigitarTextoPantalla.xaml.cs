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
    /// Lógica de interacción para DigitarNumero.xaml
    /// </summary>
    public partial class DigitarTextoPantalla : Window
    {
        public string Texto { get; set; }
        public bool Pausado { get; set; }
        public bool ModoToolTip { get; set; }
        public DigitarTextoPantalla()
        {
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(ModoToolTip)
            {
                btnPausar.Visibility = Visibility.Collapsed;
                btnDetener.Content = "Cancelar";
                btnContinuar.Content = "Aceptar";

                textoDigitado.Text = Texto;
            }

            textoDigitado.Focus();
        }

        private void textoDigitado_TextChanged(object sender, TextChangedEventArgs e)
        {
            Texto = textoDigitado.Text;
        }
    }
}
