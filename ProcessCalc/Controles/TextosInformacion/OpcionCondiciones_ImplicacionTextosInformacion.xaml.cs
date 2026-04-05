using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para OpcionCondiciones_ImplicacionTextosInformacion.xaml
    /// </summary>
    public partial class OpcionCondiciones_ImplicacionTextosInformacion : UserControl
    {
        public CondicionImplicacionTextosInformacion Condiciones { get; set; }
        public List<DiseñoTextosInformacion> Entradas { get; set; }
        public List<DiseñoOperacion> Elementos { get; set; }
        public AsignacionImplicacion_TextosInformacion asignacionImplicacion { get; set; }
        public Implicacion_TextosInformacion Implicaciones_Contenedor { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoTextosInformacion> Definiciones { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public bool ModoProcesamientoCantidades {  get; set; }
        public OpcionCondiciones_ImplicacionTextosInformacion()
        {
            InitializeComponent();
        }

        public void ListarCondiciones()
        {
            //listaCondiciones.Children.Clear();
            listaCondiciones.Text = String.Empty;
            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (Condiciones != null)
            {
                EtiquetaCondicionImplicacionTextosInformacion etiquetaCondicion = new EtiquetaCondicionImplicacionTextosInformacion();
                etiquetaCondicion.Condiciones_ElementoContenedor = Condiciones;
                etiquetaCondicion.Condicion = Condiciones;
                etiquetaCondicion.ElementoContenedor = this;
                //listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
                listaCondiciones.Text = etiquetaCondicion.Texto;
            }
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EstablecerCondiciones_ImplicacionTextosInformacion establecer = new EstablecerCondiciones_ImplicacionTextosInformacion();
            establecer.Entradas = Entradas;
            establecer.Elementos = Elementos;
            establecer.Operandos = Operandos;
            establecer.SubOperandos = SubOperandos;
            establecer.Condiciones = Condiciones;
            establecer.Definiciones = Definiciones;
            establecer.DefinicionesListas = DefinicionesListas;
            establecer.ModoProcesamientoCantidades = ModoProcesamientoCantidades;
            establecer.ShowDialog();
            
            if (establecer.Condiciones != null)
            {
                Condiciones = establecer.Condiciones;
                if (asignacionImplicacion != null)
                    asignacionImplicacion.Condiciones_TextoCondicion = Condiciones;
            }
            else
            {
                Condiciones = null;
                if (asignacionImplicacion != null)
                    asignacionImplicacion.Condiciones_TextoCondicion = null;

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
            if (!string.IsNullOrEmpty(listaCondiciones.Text))
                botonEstablecer.Visibility = Visibility.Collapsed;
        }

        private void listaCondiciones_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button_Click(this, e);
        }
    }
}
