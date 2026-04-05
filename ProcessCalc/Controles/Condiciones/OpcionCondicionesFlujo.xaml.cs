using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
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

namespace ProcessCalc.Controles.Condiciones
{
    /// <summary>
    /// Lógica de interacción para OpcionCondicionesFlujo.xaml
    /// </summary>
    public partial class OpcionCondicionesFlujo : UserControl
    {
        public CondicionFlujo Condiciones { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public SeleccionOrdenamiento_Condiciones DefinicionCondiciones_Contenedor { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public OpcionCondicionesFlujo()
        {
            InitializeComponent();
        }

        public void ListarCondiciones()
        {
            listaCondiciones.Text = String.Empty;

            if (Condiciones != null)
            {
                EtiquetaCondicionFlujo etiquetaCondicion = new EtiquetaCondicionFlujo();
                etiquetaCondicion.Condiciones_ElementoContenedor = Condiciones;
                etiquetaCondicion.Condicion = Condiciones;
                etiquetaCondicion.ElementoContenedor = this;
                etiquetaCondicion.MostrarEtiquetaCondiciones();
                listaCondiciones.Text = etiquetaCondicion.Texto;
            }

            if (!string.IsNullOrEmpty(listaCondiciones.Text))
                botonEstablecer.Visibility = Visibility.Collapsed;
            else
                botonEstablecer.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EstablecerCondiciones_Flujo establecer = new EstablecerCondiciones_Flujo();
            establecer.Operandos = Operandos;
            establecer.Condiciones = Condiciones;
            establecer.SubOperandos = SubOperandos;
            establecer.ModoOperacion = ModoOperacion;
            establecer.ListaElementos = ListaElementos;
            establecer.ShowDialog();

            if (establecer.Condiciones != null)
            {
                Condiciones = establecer.Condiciones;
            }
            else
            {
                Condiciones = null;
            }


            ListarCondiciones();

            if (Condiciones == null)
            {
                botonEstablecer.Visibility = Visibility.Visible;
            }
            else
            {
                botonEstablecer.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCondiciones();            
        }

        private void listaCondiciones_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button_Click(this, e);
        }
    }
}
