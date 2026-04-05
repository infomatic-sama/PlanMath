using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace ProcessCalc.Ventanas.Digitaciones
{
    /// <summary>
    /// Lógica de interacción para DivisionesPorCadenaTexto_Ordenaciones.xaml
    /// </summary>
    public partial class DivisionesPorCadenaTexto_Ordenaciones : Window
    {
        public OrdenacionNumeros Ordenacion { get; set; }
        public DivisionCadenaTexto_Ordenacion DivisionSeleccionada { get; set; }
        public DivisionesPorCadenaTexto_Ordenaciones()
        {
            InitializeComponent();
        }


        public void ListarDivisiones_CadenaTexto()
        {
            if (Ordenacion != null)
            {
                DivisionSeleccionada = null;
                divisionesTexto.Children.Clear();

                foreach (var itemDivision in Ordenacion.DivisionesCadenaTexto_Ordenaciones)
                {
                    EtiquetaDivision_CadenaTexto_Ordenacion etiquetaDivision = new EtiquetaDivision_CadenaTexto_Ordenacion();
                    etiquetaDivision.Division = itemDivision;
                    etiquetaDivision.DefinicionOrdenacion = this;
                    etiquetaDivision.CargarDivision();
                    divisionesTexto.Children.Add(etiquetaDivision);
                }

                PintarDivisiones_ConSeleccionada();
            }
        }

        public void EstablecerDivisionSeleccionada(DivisionCadenaTexto_Ordenacion division)
        {
            DivisionSeleccionada = division;
        }

        public void PintarDivisiones_ConSeleccionada()
        {
            foreach (EtiquetaDivision_CadenaTexto_Ordenacion itemEtiquetaDivision in divisionesTexto.Children)
            {
                if (DivisionSeleccionada == itemEtiquetaDivision.Division)
                {
                    itemEtiquetaDivision.botonBorde.Background = System.Windows.SystemColors.HighlightBrush;
                }
                else
                {
                    itemEtiquetaDivision.botonBorde.Background = agregarDivisionTexto.Background;
                }
            }
        }

        private void agregarDivisionTexto_Click(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null)
            {
                IngresarDivisionCadenaTexto_Ordenacion ingresarDivision = new IngresarDivisionCadenaTexto_Ordenacion();
                DivisionCadenaTexto_Ordenacion Division = new DivisionCadenaTexto_Ordenacion();
                if (ingresarDivision.ShowDialog() == true)
                {
                    Division.IndiceLetraInicial = ingresarDivision.IndiceLetraInicial;
                    Division.CantidadLetras = ingresarDivision.CantidadLetras;
                    Ordenacion.DivisionesCadenaTexto_Ordenaciones.Add(Division);
                    ListarDivisiones_CadenaTexto();
                }
            }
        }

        private void quitarDivisionTexto_Click(object sender, RoutedEventArgs e)
        {
            if (Ordenacion != null &&
                DivisionSeleccionada != null)
            {
                Ordenacion.DivisionesCadenaTexto_Ordenaciones.Remove(DivisionSeleccionada);
                ListarDivisiones_CadenaTexto();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarDivisiones_CadenaTexto();
        }
    }
}
