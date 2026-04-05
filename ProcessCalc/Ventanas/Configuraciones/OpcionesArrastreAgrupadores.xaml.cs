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
    /// Lógica de interacción para OpcionesArrastreAgrupadores.xaml
    /// </summary>
    public partial class OpcionesArrastreAgrupadores : Window
    {
        public DiseñoOperacion AgrupadorContenedor { get; set; }
        public DiseñoOperacion AgrupadorContenido { get; set; }
        public DiseñoOperacion AgrupadorArrastre { get; set; }
        public DiseñoOperacion AgrupadorSeleccionado { get; set; }
        public bool Aceptar { get; set; }
        public OpcionesArrastreAgrupadores()
        {
            InitializeComponent();
        }

        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            Aceptar = true;
            Close();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetearTextoOpciones()
        {
            opcionAgrupadorArrastreContenidoAgrupadorSeleccionadoContenedor.Content = "Agrupar '" + AgrupadorContenido.NombreCombo +
                "' en '" + AgrupadorContenedor.NombreCombo + "'";

            opcionAgrupadorSeleccionadoContenidoAgrupadorArrastreContenedor.Content = "Agrupar '" + AgrupadorContenedor.NombreCombo +
                "' en '" + AgrupadorContenido.NombreCombo + "'";
        }

        private void SetearOpcion(bool AgrupadorArrastreContenidoAgrupadorSeleccionadoContenedor)
        {
            if (AgrupadorArrastre != null && AgrupadorSeleccionado != null)
            {
                if (AgrupadorArrastreContenidoAgrupadorSeleccionadoContenedor)
                {
                    AgrupadorContenido = AgrupadorArrastre;
                    AgrupadorContenedor = AgrupadorSeleccionado;
                }
                else
                {
                    AgrupadorContenedor = AgrupadorArrastre;
                    AgrupadorContenido = AgrupadorSeleccionado;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetearOpcion(true);
            SetearTextoOpciones();
        }

        private void opcionAgrupadorArrastreContenidoAgrupadorSeleccionadoContenedor_Checked(object sender, RoutedEventArgs e)
        {
            SetearOpcion(true);
        }

        private void opcionAgrupadorSeleccionadoContenidoAgrupadorArrastreContenedor_Checked(object sender, RoutedEventArgs e)
        {
            SetearOpcion(false);
        }
    }
}
