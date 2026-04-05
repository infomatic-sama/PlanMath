using ProcessCalc.Entidades;
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
    /// Lógica de interacción para OpcionCondiciones_TextosInformacion.xaml
    /// </summary>
    public partial class OpcionCondiciones_TextosInformacion : UserControl
    {
        public CondicionTextosInformacion Condiciones { get; set; }
        public CondicionesAsignacionSalidas_TextosInformacion asignacion { get; set; }
        public SeleccionOrdenamientoCondiciones_TextosInformacion Asignaciones_Contenedor { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public AsociacionOperandosCondiciones_TextosAsignacion_Implicacion AsociacionTextosInformacionOperando_Implicacion { get; set; }
        public bool MostrarOpcionesTextosImplicacionAsignacion { get; set; }
        public DiseñoOperacion OperandoSeleccionado { get; set; }
        public DiseñoElementoOperacion SubOperandoSeleccionado { get; set; }
        public bool ModoSeleccionEntradas {  get; set; }
        public List<Entrada> Entradas { get; set; }
        public Entrada ElementoEntradaAsociado_Asignacion { get; set; }
        public DiseñoOperacion ElementoOperandoAsociado_Asignacion { get; set; }
        public DiseñoElementoOperacion ElementoSubOperandoAsociado_Asignacion { get; set; }
        public DiseñoOperacion ElementoAsociado { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public bool ModoAsignacionLogicaImplicaciones { get; set; }
        public OpcionCondiciones_TextosInformacion()
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
                EtiquetaCondicionTextosInformacion etiquetaCondicion = new EtiquetaCondicionTextosInformacion();
                etiquetaCondicion.Condiciones_ElementoContenedor = Condiciones;
                etiquetaCondicion.Condicion = Condiciones;
                etiquetaCondicion.ElementoContenedor = this;
                //listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
                listaCondiciones.Text = etiquetaCondicion.Texto;
            }
            //}
            if (!string.IsNullOrEmpty(listaCondiciones.Text))
                botonEstablecer.Visibility = Visibility.Collapsed;
            else
                botonEstablecer.Visibility = Visibility.Visible;

            if (Condiciones != null)
            {
                if (Condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores)
                {
                    botonEstablecer.Visibility = Visibility.Collapsed;
                    listaCondiciones.Visibility = Visibility.Collapsed;
                    textoCondicion_CuandoCantidadesNoCumplen_Anteriores.Visibility = Visibility.Visible;
                }
                else
                {
                    textoCondicion_CuandoCantidadesNoCumplen_Anteriores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EstablecerCondiciones_TextosInformacion establecer = new EstablecerCondiciones_TextosInformacion();
            establecer.ElementoAsociado = ElementoAsociado;
            establecer.Entradas = Entradas;
            establecer.Operandos = Operandos;
            establecer.ListaElementos = ListaElementos;
            establecer.SubOperandos = SubOperandos;
            establecer.Condiciones = Condiciones;
            establecer.MostrarOpcionesTextosImplicacionAsignacion = MostrarOpcionesTextosImplicacionAsignacion;
            establecer.ModoSeleccionEntradas = ModoSeleccionEntradas;
            establecer.ElementoEntradaAsociado_Asignacion = ElementoEntradaAsociado_Asignacion;
            establecer.ElementoOperandoAsociado_Asignacion = ElementoOperandoAsociado_Asignacion;
            establecer.ElementoSubOperandoAsociado_Asignacion = ElementoSubOperandoAsociado_Asignacion;
            establecer.DefinicionesListas = DefinicionesListas;
            establecer.ModoAsignacionLogicaImplicaciones = ModoAsignacionLogicaImplicaciones;
            establecer.ShowDialog();

            if (establecer.Condiciones != null)
            {
                Condiciones = establecer.Condiciones;

                if(AsociacionTextosInformacionOperando_Implicacion != null)
                    AsociacionTextosInformacionOperando_Implicacion.Condiciones = Condiciones;

                if (asignacion != null)
                    asignacion.Condiciones = Condiciones;

                if (Asignaciones_Contenedor != null)
                    Asignaciones_Contenedor.Condiciones_AgregarAsignacion = Condiciones;
            }
            else
            {
                Condiciones = null;
                if (AsociacionTextosInformacionOperando_Implicacion != null)
                    AsociacionTextosInformacionOperando_Implicacion.Condiciones = null;

                if (asignacion != null)
                    asignacion.Condiciones = null;

                if (Asignaciones_Contenedor != null)
                    Asignaciones_Contenedor.Condiciones_AgregarAsignacion = null;
            }

            if(OperandoSeleccionado != null)
            {
                OperandoSeleccionado.CondicionesTextosInformacionOperandosResultados = Condiciones;
            }

            if(SubOperandoSeleccionado != null)
            {
                SubOperandoSeleccionado.CondicionesTextosInformacionOperandosResultados = Condiciones;
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
