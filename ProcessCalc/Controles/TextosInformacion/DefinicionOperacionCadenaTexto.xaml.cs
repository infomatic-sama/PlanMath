using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para DefinicionOperacionCadenaTexto.xaml
    /// </summary>
    public partial class DefinicionOperacionCadenaTexto : UserControl
    {
        public OperacionCadenaTexto Operacion { get; set; }
        public int Indice { get; set; }
        public bool ModoSeleccion { get; set; }
        public DefinicionOperacionCadenaTexto()
        {
            InitializeComponent();
        }

        private void posicionInicial_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numero = 0;
            int.TryParse(posicionInicial.Text, out numero);
            if (Operacion != null) Operacion.PosicionInicial = numero;
        }

        private void posicionFinal_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numero = 0;
            int.TryParse(posicionFinal.Text, out numero);
            if (Operacion != null) Operacion.PosicionFinal = numero;
        }

        private void tipoOperacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Operacion != null)
            {
                Operacion.Tipo = (TipoOperacionCadenaTexto)int.Parse(((ComboBoxItem)tipoOperacion.SelectedItem).Uid);

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(ModoSeleccion)
            {
                tipoOperacion.SelectedIndex = 2;
                tipoOperacion.IsEnabled = false;

                textoPosicionInicialModoOperacion.Visibility = Visibility.Collapsed;
                textoPosicionInicialModoSeleccion.Visibility = Visibility.Visible;

                textoPosicionFinal.Visibility = Visibility.Collapsed;
                posicionFinal.Visibility = Visibility.Collapsed;
            }

            if(Operacion != null)
            {
                tipoOperacion.SelectedItem = (from ComboBoxItem I in tipoOperacion.Items where I.Uid == ((int)Operacion.Tipo).ToString() select I).FirstOrDefault();
                posicionInicial.Text = Operacion.PosicionInicial.ToString();
                posicionFinal.Text = Operacion.PosicionFinal.ToString();
            }
        }
    }
}
