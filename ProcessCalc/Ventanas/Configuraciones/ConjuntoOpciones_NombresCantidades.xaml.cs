using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
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
    /// Lógica de interacción para ConjuntoOpciones_NombresCantidades.xaml
    /// </summary>
    public partial class ConjuntoOpciones_NombresCantidades : Window
    {
        public DefinicionTextoNombresCantidades TextosNombre { get; set; }
        public OpcionesNombreCantidad_TextosInformacion OpcionesTextoSeleccionada { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool OcultarOpcionesDefiniciones { get; set; }
        public bool OcultarOpcionesOperando { get; set; }
        public bool ModoDefinicionesImplicaciones { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public ConjuntoOpciones_NombresCantidades()
        {
            InitializeComponent();
            ListaElementos = new List<DiseñoOperacion>();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListarTextos()
        {
            listaTextos.Children.Clear();

            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (TextosNombre != null)
            {
                foreach (var itemOpcion in TextosNombre.OpcionesTextos)
                {
                    EtiquetaTextosInformacionNombreCantidades etiquetaCondicion = new EtiquetaTextosInformacionNombreCantidades();
                    etiquetaCondicion.DefinicionTextoNombre_ElementoContenedor = TextosNombre;
                    etiquetaCondicion.OpcionTextosSeleccionada_ElementoContenedor = OpcionesTextoSeleccionada;
                    etiquetaCondicion.ElementoContenedor = this;
                    etiquetaCondicion.OpcionTextos = itemOpcion;
                    listaTextos.Children.Add(etiquetaCondicion);
                    etiquetaCondicion.MostrarEtiquetaCondiciones();
                }
            }
            //}
        }

        public string ObtenerDescripcion()
        {
            string descripcion = string.Empty;

            if (TextosNombre != null)
            {
                foreach (var itemOpcion in TextosNombre.OpcionesTextos)
                {
                    EtiquetaTextosInformacionNombreCantidades etiquetaCondicion = new EtiquetaTextosInformacionNombreCantidades();
                    etiquetaCondicion.DefinicionTextoNombre_ElementoContenedor = TextosNombre;
                    etiquetaCondicion.OpcionTextosSeleccionada_ElementoContenedor = OpcionesTextoSeleccionada;
                    etiquetaCondicion.ElementoContenedor = this;
                    etiquetaCondicion.OpcionTextos = itemOpcion;
                    etiquetaCondicion.MostrarEtiquetaCondiciones();
                    descripcion += etiquetaCondicion.Texto + " ";
                }
            }

            return descripcion;
        }

        public void DesmarcarCondicionesBusquedas()
        {
            foreach (EtiquetaTextosInformacionNombreCantidades itemCondicion in listaTextos.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        private void agregarTexto_Click(object sender, RoutedEventArgs e)
        {
            EstablecerOpciones_NombresCantidades establecer = new EstablecerOpciones_NombresCantidades();
            establecer.ListaOperandos = Operandos;
            establecer.ListaElementos = ListaElementos.Except(establecer.ListaOperandos).ToList();
            establecer.ListaSubOperandos = SubOperandos;
            establecer.OpcionesNombresCantidades = new OpcionesNombreCantidad_TextosInformacion();
            establecer.OcultarOpcionesOperando = OcultarOpcionesOperando;
            establecer.ModoDefinicionesImplicaciones = ModoDefinicionesImplicaciones;

            if (establecer.ShowDialog() == true)
            {
                TextosNombre.OpcionesTextos.Add(establecer.OpcionesNombresCantidades);
                ListarTextos();
            }
        }

        private void editarTexto_Click(object sender, RoutedEventArgs e)
        {
            if (OpcionesTextoSeleccionada != null)
            {
                EstablecerOpciones_NombresCantidades establecer = new EstablecerOpciones_NombresCantidades();
                establecer.ListaOperandos = Operandos;
                establecer.ListaElementos = ListaElementos.Except(establecer.ListaOperandos).ToList();
                establecer.ListaSubOperandos = SubOperandos;
                establecer.OpcionesNombresCantidades = new OpcionesNombreCantidad_TextosInformacion();
                establecer.OpcionesNombresCantidades.CopiarObjeto(OpcionesTextoSeleccionada);
                establecer.OcultarOpcionesOperando = OcultarOpcionesOperando;
                establecer.ModoDefinicionesImplicaciones = ModoDefinicionesImplicaciones;

                if (establecer.ShowDialog() == true)
                {
                    OpcionesTextoSeleccionada.CopiarObjeto(establecer.OpcionesNombresCantidades);
                    ListarTextos();
                }
            }
        }

        private void quitarTexto_Click(object sender, RoutedEventArgs e)
        {
            if (OpcionesTextoSeleccionada != null)
            {
                TextosNombre.OpcionesTextos.Remove(OpcionesTextoSeleccionada);
                ListarTextos();
            }
        }

        private void moverTextoAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (OpcionesTextoSeleccionada != null)
            {                
                int indice = TextosNombre.OpcionesTextos.IndexOf(OpcionesTextoSeleccionada);

                if (indice > 0)
                {                    
                    TextosNombre.OpcionesTextos.RemoveAt(indice);
                    TextosNombre.OpcionesTextos.Insert(indice - 1, OpcionesTextoSeleccionada);

                    ListarTextos();
                }
            }
        }

        private void moverTextoADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (OpcionesTextoSeleccionada != null)
            {
                int indice = TextosNombre.OpcionesTextos.IndexOf(OpcionesTextoSeleccionada);

                if (indice < TextosNombre.OpcionesTextos.Count - 1)
                {
                    TextosNombre.OpcionesTextos.RemoveAt(indice);
                    TextosNombre.OpcionesTextos.Insert(indice + 1, OpcionesTextoSeleccionada);

                    ListarTextos();
                }
            }
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            opcionDefinicionNombresAntesEjecucion.IsChecked = TextosNombre.DefinirNombresAntesEjecucion_Elemento;
            opcionDefinicionNombresDuranteEjecucion.IsChecked = TextosNombre.DefinirNombresDuranteEjecucion_Elemento;
            opcionDefinicionNombresDespuesEjecucion.IsChecked = TextosNombre.DefinirNombresDespuesEjecucion_Elemento;

            opcionEstablecerNombreNumerosCantidades.IsChecked = TextosNombre.DefinirNombresNumeros_Elemento;
            opcionEstablecerNombreOperacion.IsChecked = TextosNombre.DefinirNombres_Elemento;

            if (OcultarOpcionesDefiniciones)
            {
                opcionesDefiniciones.Visibility = Visibility.Collapsed;
            }

            ListarTextos();
        }

        private void opcionDefinicionNombresDuranteEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresDuranteEjecucion_Elemento = (bool)opcionDefinicionNombresDuranteEjecucion.IsChecked;
        }

        private void opcionDefinicionNombresDuranteEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresDuranteEjecucion_Elemento = (bool)opcionDefinicionNombresDuranteEjecucion.IsChecked;
        }

        private void opcionDefinicionNombresDespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresDespuesEjecucion_Elemento = (bool)opcionDefinicionNombresDespuesEjecucion.IsChecked;
        }

        private void opcionDefinicionNombresDespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresDespuesEjecucion_Elemento = (bool)opcionDefinicionNombresDespuesEjecucion.IsChecked;
        }
        private void opcionDefinicionNombresAntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresAntesEjecucion_Elemento = (bool)opcionDefinicionNombresAntesEjecucion.IsChecked;
        }

        private void opcionDefinicionNombresAntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresAntesEjecucion_Elemento = (bool)opcionDefinicionNombresAntesEjecucion.IsChecked;
        }

        private void opcionEstablecerNombreNumerosCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresNumeros_Elemento = (bool)opcionEstablecerNombreNumerosCantidades.IsChecked;
        }

        private void opcionEstablecerNombreOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombres_Elemento = (bool)opcionEstablecerNombreOperacion.IsChecked;
        }

        private void opcionEstablecerNombreNumerosCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombresNumeros_Elemento = (bool)opcionEstablecerNombreNumerosCantidades.IsChecked;

            if (opcionEstablecerNombreOperacion.IsChecked == false)
                opcionEstablecerNombreNumerosCantidades.IsChecked = true;
        }

        private void opcionEstablecerNombreOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextosNombre != null)
                TextosNombre.DefinirNombres_Elemento = (bool)opcionEstablecerNombreOperacion.IsChecked;

            if (opcionEstablecerNombreNumerosCantidades.IsChecked == false)
                opcionEstablecerNombreOperacion.IsChecked = true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
