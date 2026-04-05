using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Ventanas.Digitaciones;
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
    /// Lógica de interacción para EtiquetaDivision_CadenaTexto_Ordenacion.xaml
    /// </summary>
    public partial class EtiquetaDivision_CadenaTexto_Ordenacion : UserControl
    {
        public DivisionCadenaTexto_Ordenacion Division {  get; set; }
        public DivisionesPorCadenaTexto_Ordenaciones DefinicionOrdenacion { get; set; }
        public EtiquetaDivision_CadenaTexto_Ordenacion()
        {
            InitializeComponent();
        }

        public void CargarDivision()
        {
            if(Division != null)
            {
                letraInicial.Text = Division.IndiceLetraInicial.ToString();
                cantidadLetras.Text = Division.CantidadLetras.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DefinicionOrdenacion.EstablecerDivisionSeleccionada(Division);
            DefinicionOrdenacion.PintarDivisiones_ConSeleccionada();
        }
    }
}
