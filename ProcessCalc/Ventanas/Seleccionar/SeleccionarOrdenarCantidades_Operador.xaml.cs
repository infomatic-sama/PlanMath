using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para SeleccionarOrdenarCantidades_Operador.xaml
    /// </summary>
    public partial class SeleccionarOrdenarCantidades_Operador : Window
    {
        public bool MostrarOpcionesAsignacion { get; set; }
        public MainWindow Ventana { get; set; }
        public DiseñoCalculo CalculoDiseñoSeleccionado { get; set; }
        public SeleccionarOrdenarCantidades_Operador()
        {
            InitializeComponent();
            listaTextos.Operandos = new List<Entidades.DiseñoOperacion>();
            listaTextos.SubOperandos = new List<Entidades.DiseñoElementoOperacion>();
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

        private void opcionModoUnir_Checked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Collapsed;
            if (MostrarOpcionesAsignacion)
                opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Collapsed;
        }

        private void opcionModoUnir_Unchecked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Visible;
            if (MostrarOpcionesAsignacion)
                opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
        }

        private void btnDefinirAsignacionImplicacionTextos_Click(object sender, RoutedEventArgs e)
        {            
            Ventana.Elemento_AgregarImplicacionTextos = listaTextos.OperacionRelacionada;
            Ventana.Calculo_AgregarImplicacionTextos = CalculoDiseñoSeleccionado;
            Ventana.ElementoDiseño_AgregarImplicacionTextos = listaTextos.ElementoDiseñoRelacionado;
                
            Ventana.btnTextosInformacion_Click(this, null);

            Thread.Sleep(300);

            Ventana.Elemento_AgregarImplicacionTextos = null;
            Ventana.Calculo_AgregarImplicacionTextos = null;
            Ventana.ElementoDiseño_AgregarImplicacionTextos = null;

            MostrarOpcionesAsignacion = true;

            if(opcionModoUnir.IsChecked == true)
                opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Collapsed;
            else
                opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
        }

        private void opcionModoSeleccionManual_Checked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Collapsed;
            if (MostrarOpcionesAsignacion)
                opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Collapsed;
        }

        private void opcionModoSeleccionManual_Unchecked(object sender, RoutedEventArgs e)
        {
            contenedorlistaTextos.Visibility = Visibility.Visible;
            if (MostrarOpcionesAsignacion)
                opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
        }
    }
}
