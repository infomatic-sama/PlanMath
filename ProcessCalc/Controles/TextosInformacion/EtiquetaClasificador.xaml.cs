using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Ventanas.Definiciones;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para EtiquetaClasificador.xaml
    /// </summary>
    public partial class EtiquetaClasificador : UserControl
    {
        public Clasificador Clasificador { get; set; }
        public DefinirClasificadores DefinirClasificadores { get; set; }
        public EtiquetaClasificador()
        {
            InitializeComponent();
        }

        public void CargarClasificador()
        {
            if (Clasificador != null)
            {
                cadenaTexto.Text = Clasificador.CadenaTexto +
                    (Clasificador.UtilizarCadenasTexto_DeCantidad ? "(se utilizarán las cadenas de texto que clasifican estas cantidades)" : string.Empty);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DefinirClasificadores.EstablecerClasificadorSeleccionado(Clasificador);
            DefinirClasificadores.PintarClasificadores_ConSeleccionado();
        }
    }
}
