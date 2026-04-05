using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas.Configuraciones;
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

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para IngresarClasificador.xaml
    /// </summary>
    public partial class IngresarClasificador : Window
    {
        public string CadenaTexto {  get; set; }
        public bool UtilizarCadenasTexto_DeCantidad {  get; set; }
        public List<OperacionCadenaTexto> SeleccionCadenasTexto {  get; set; }
        public IngresarClasificador()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void cadenaTexto_TextChanged(object sender, TextChangedEventArgs e)
        {
            CadenaTexto = cadenaTexto.Text;
        }

        private void utilizarCadenasTexto_DeCantidad_Checked(object sender, RoutedEventArgs e)
        {
            UtilizarCadenasTexto_DeCantidad = (bool)utilizarCadenasTexto_DeCantidad.IsChecked;
        }

        private void utilizarCadenasTexto_DeCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            UtilizarCadenasTexto_DeCantidad = (bool)utilizarCadenasTexto_DeCantidad.IsChecked;
        }

        private void definirCadenasTexto_Click(object sender, RoutedEventArgs e)
        {
            ConfigurarOperaciones_Elemento config = new ConfigurarOperaciones_Elemento();
            config.ModoSeleccion = true;
            config.OperacionesCadenasTexto = SeleccionCadenasTexto?.Select(i => i.ReplicarObjeto()).ToList();
            bool resp = (bool)config.ShowDialog();

            if (resp)
            {
                SeleccionCadenasTexto = config.OperacionesCadenasTexto;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cadenaTexto.Text = CadenaTexto;
            utilizarCadenasTexto_DeCantidad.IsChecked = UtilizarCadenasTexto_DeCantidad;
        }
    }
}
