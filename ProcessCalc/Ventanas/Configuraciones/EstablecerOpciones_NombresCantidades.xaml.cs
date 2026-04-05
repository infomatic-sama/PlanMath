using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections;
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
    /// Lógica de interacción para EstablecerOpciones_NombresCantidades.xaml
    /// </summary>
    public partial class EstablecerOpciones_NombresCantidades : Window
    {
        public OpcionesNombreCantidad_TextosInformacion OpcionesNombresCantidades { get; set; }
        public List<DiseñoOperacion> ListaOperandos { get; set; }
        public List<DiseñoElementoOperacion> ListaSubOperandos { get; set; }
        public bool OcultarOpcionesOperando { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public bool ModoDefinicionesImplicaciones { get; set; }
        public EstablecerOpciones_NombresCantidades()
        {
            InitializeComponent();
        }

        //private void DesactivarRadioButtons(RadioButton actual)
        //{
        //    if (IsLoaded)
        //    {
        //        if (opcionPrimerTextoInformacion != actual)
        //            opcionPrimerTextoInformacion.IsChecked = false;

        //        if (opcionPrimerosNTextosInformacion != actual)
        //            opcionPrimerosNTextosInformacion.IsChecked = false;

        //        if (opcionUltimoTextoInformacion != actual)
        //            opcionUltimoTextoInformacion.IsChecked = false;

        //        if (opcionUltimosNTextosInformacion != actual)
        //            opcionUltimosNTextosInformacion.IsChecked = false;

        //        if (opcionTextosInformacionCumplenCondiciones != actual)
        //            opcionTextosInformacionCumplenCondiciones.IsChecked = false;

        //        if (opcionTodosTextosInformacion != actual)
        //            opcionTodosTextosInformacion.IsChecked = false;

        //        if (opcionTextosInformacionPosiciones != actual)
        //            opcionTextosInformacionPosiciones.IsChecked = false;

        //        if (opcionTextosInformacionFijos != actual)
        //            opcionTextosInformacionFijos.IsChecked = false;

        //        if (opcionTextoInformacionFijoNombreElemento != actual)
        //            opcionTextoInformacionFijoNombreElemento.IsChecked = false;

        //        if (opcionTextoInformacionFijoNombreOperacion != actual)
        //            opcionTextoInformacionFijoNombreOperacion.IsChecked = false;

        //        if (opcionTextoInformacionFijoPosicion != actual)
        //            opcionTextoInformacionFijoPosicion.IsChecked = false;

        //        if (opcionTextosInformacionNombreNumero != actual)
        //            opcionTextosInformacionNombreNumero.IsChecked = false;

        //        if (opcionTextosInformacionNombreNumeroPosicion != actual)
        //            opcionTextosInformacionNombreNumeroPosicion.IsChecked = false;

        //        if (opcionTextosInformacionNombreTodosNumeros != actual)
        //            opcionTextosInformacionNombreTodosNumeros.IsChecked = false;

        //        if (opcionTodosTextosInformacionNumeroElemento != actual)
        //            opcionTodosTextosInformacionNumeroElemento.IsChecked = false;
        //    }
        //}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ListaElementosCombo = new ArrayList();

            ListaElementosCombo.Add(new ComboBoxItem()
            {
                Content = "Operandos de esta operación",
                IsEnabled = false,
            });

            if(ModoDefinicionesImplicaciones)
            {
                ListaElementosCombo.Add(new DiseñoOperacion()
                {
                    Nombre = "Operando actual de la ejecución (todos o cualquier operando)",
                    ID = "0",
                });
            }

            ListaElementosCombo.AddRange(ListaOperandos.Where(item => !string.IsNullOrEmpty(item.NombreCombo)
            &&
            item.Tipo != TipoElementoOperacion.Salida).ToList());

            ListaElementosCombo.Add(new ComboBoxItem()
            {
                Content = "Elementos operandos del cálculo",
                IsEnabled = false,
            });

            ListaElementosCombo.AddRange(ListaElementos.Where(item => !string.IsNullOrEmpty(item.NombreCombo)
            &&
            item.Tipo != TipoElementoOperacion.Salida).ToList());

            opcionOperando_TextosInformacion.DisplayMemberPath = "NombreCombo";
            opcionOperando_TextosInformacion.SelectedValuePath = "NombreCombo";
            opcionOperando_TextosInformacion.ItemsSource = ListaElementosCombo;

            opcionOperandoSubElemento_TextosInformacion.DisplayMemberPath = "NombreCombo";
            opcionOperandoSubElemento_TextosInformacion.SelectedValuePath = "NombreCombo";

            if (ListaSubOperandos != null)
                opcionOperandoSubElemento_TextosInformacion.ItemsSource = ListaSubOperandos.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);
            
            condicionesTexto.Operandos = ListaOperandos;
            condicionesTexto.SubOperandos = ListaSubOperandos;
            condicionesTexto.ListaElementos = ListaElementos;

            condicionesFiltroNumeros.Operandos = ListaOperandos;
            condicionesFiltroNumeros.SubOperandos = ListaSubOperandos;
            condicionesFiltroNumeros.ListaElementos = ListaElementos;

            condicionesTextoNumerosFiltrados.Operandos = ListaOperandos;
            condicionesTextoNumerosFiltrados.SubOperandos = ListaSubOperandos;
            condicionesTextoNumerosFiltrados.ListaElementos = ListaElementos;

            condicionesTextoNumero.Operandos = ListaOperandos;
            condicionesTextoNumero.SubOperandos = ListaSubOperandos;
            condicionesTextoNumero.ListaElementos = ListaElementos;

            if ((ListaOperandos != null && !ListaOperandos.Any()) || ListaOperandos == null)
                opcionesOperandos.Visibility = Visibility.Collapsed;

            if (OpcionesNombresCantidades != null)
            {
                if (OpcionesNombresCantidades.Operando != null)
                    opcionOperando_TextosInformacion.SelectedValue = OpcionesNombresCantidades.Operando.NombreCombo;
                else
                {
                    opcionOperando_TextosInformacion.SelectedItem = ListaElementosCombo.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElemento_TextosInformacion.SelectedItem = null;
                }

                if (OpcionesNombresCantidades.OperandoSubElemento != null)
                    opcionOperandoSubElemento_TextosInformacion.SelectedValue = OpcionesNombresCantidades.OperandoSubElemento.NombreCombo;

                condicionesTexto.Condiciones = OpcionesNombresCantidades.Condiciones;
                condicionesTexto.ListarCondiciones();

                condicionesFiltroNumeros.Condiciones = OpcionesNombresCantidades.CondicionesFiltroNumeros;
                condicionesFiltroNumeros.ListarCondiciones();

                condicionesTextoNumerosFiltrados.Condiciones = OpcionesNombresCantidades.CondicionesTexto;
                condicionesTextoNumerosFiltrados.ListarCondiciones();

                condicionesTextoNumero.Condiciones = OpcionesNombresCantidades.CondicionesTextoNumero;
                condicionesTextoNumero.ListarCondiciones();

                posicionesTextos.Text = OpcionesNombresCantidades.PosicionesTextosInformacion;
                posicionesTextosNumerosFiltrados.Text = OpcionesNombresCantidades.PosicionesTextosInformacionNumerosFiltrados;
                posicionesTextosNumero.Text = OpcionesNombresCantidades.PosicionesTextosInformacionNumero;
                textoInformacionFijo.Text = OpcionesNombresCantidades.TextoInformacionFijo;

                posicionesTextosNumeros.Text = OpcionesNombresCantidades.PosicionesTextosInformacionNumero_Elemento;

                ActivarOpciones();

                opcionIncluirTextosImplica.IsChecked = OpcionesNombresCantidades.IncluirTextosImplica;
                opcionIncluirComillas.IsChecked = OpcionesNombresCantidades.IncluirComillas;
            }

            if(OcultarOpcionesOperando)
            {
                opcionesOperandos.Visibility = Visibility.Collapsed;
            }
        }

        private void ActivarOpciones()
        {
            switch (OpcionesNombresCantidades.TipoOpcion)
            {
                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacion:
                    opcionPrimerTextoInformacion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacion:
                    opcionPrimerosNTextosInformacion.IsChecked = true;
                    NPrimerosTextosInformacion.Text = OpcionesNombresCantidades.NPrimerosTextosInformacion.ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacion:
                    opcionUltimoTextoInformacion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacion:
                    opcionUltimosNTextosInformacion.IsChecked = true;
                    NUltimosTextosInformacion.Text = OpcionesNombresCantidades.NUltimosTextosInformacion.ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondiciones:
                    opcionTextosInformacionCumplenCondiciones.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.Todos:
                    opcionTodosTextosInformacion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosiciones:
                    opcionTextosInformacionPosiciones.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextosInformacionFijos:
                    opcionTextosInformacionFijos.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreElemento:
                    opcionTextoInformacionFijoNombreElemento.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadElemento:
                    opcionTextoInformacionFijoCantidadElemento.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreOperacion:
                    opcionTextoInformacionFijoNombreOperacion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadOperacion:
                    opcionTextoInformacionFijoCantidadOperacion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicion:
                    opcionTextoInformacionFijoPosicion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionDefinicion:
                    opcionTextoInformacionPosicionDefinicion.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionOperando:
                    opcionTextoInformacionPosicionOperando.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumeroElemento:
                    opcionTextosInformacionNombreNumero.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumeroElemento:
                    opcionTextosInformacionCantidadNumero.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumerosFiltrados:
                    opcionTextosInformacionNombreNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumerosFiltrados:
                    opcionTextosInformacionCantidadNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumerosFiltrados:
                    opcionTodosTextosInformacionNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumerosFiltrados:
                    opcionPrimerTextoInformacionNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumerosFiltrados:
                    opcionPrimerosNTextosInformacionNumerosFiltrados.IsChecked = true;
                    NPrimerosTextosInformacionNumerosFiltrados.Text = OpcionesNombresCantidades.NPrimerosTextosInformacion.ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumerosFiltrados:
                    opcionUltimoTextoInformacionNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumerosFiltrados:
                    opcionUltimosNTextosInformacionNumerosFiltrados.IsChecked = true;
                    NUltimosTextosInformacionNumerosFiltrados.Text = OpcionesNombresCantidades.NUltimosTextosInformacionNumerosFiltrados.ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumerosFiltrados:
                    opcionTextosInformacionCumplenCondicionesNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumerosFiltrados:
                    opcionTextosInformacionPosicionesNumerosFiltrados.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumero:
                    opcionTodosTextosInformacionNumero.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumero:
                    opcionPrimerTextoInformacionNumero.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumero:
                    opcionPrimerosNTextosInformacionNumero.IsChecked = true;
                    NPrimerosTextosInformacionNumero.Text = OpcionesNombresCantidades.NPrimerosTextosInformacionNumero.ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumero:
                    opcionUltimoTextoInformacionNumero.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumero:
                    opcionUltimosNTextosInformacionNumero.IsChecked = true;
                    NUltimosTextosInformacionNumero.Text = OpcionesNombresCantidades.NUltimosTextosInformacionNumero.ToString();
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumero:
                    opcionTextosInformacionCumplenCondicionesNumero.IsChecked = true;
                    break;

                case TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumero:
                    opcionTextosInformacionPosicionesNumero.IsChecked = true;
                    break;
            }

            switch(OpcionesNombresCantidades.TipoOpcionFiltroNumeros)
            {
                case TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros:
                    opcionFiltroNumerosTodos.IsChecked = true;
                    break;

                case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones:
                    opcionFiltroNumerosCumplenCondiciones.IsChecked = true;
                    break;

                case TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones:
                    opcionFiltroNumerosPosiciones.IsChecked = true;
                    break;
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            //if (opcionesOperandos.Visibility == Visibility.Visible && opcionOperando_TextosInformacion.SelectedItem == null)
            //{
            //    MessageBox.Show("Selecciona un operando.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}
            ////else if (opcionOperandoSubElemento_TextosInformacion.SelectedItem == null)
            ////{
            ////    MessageBox.Show("Selecciona una variable de elemento.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            ////}
            //else
            //{
            if (opcionOperando_TextosInformacion.SelectedItem != null &&
                        ((DiseñoOperacion)opcionOperando_TextosInformacion.SelectedItem).ID != "0")
            {
                if (opcionOperando_TextosInformacion.SelectedItem != null)
                    OpcionesNombresCantidades.Operando = (DiseñoOperacion)opcionOperando_TextosInformacion.SelectedItem;

                if (opcionOperandoSubElemento_TextosInformacion.SelectedItem != null)
                    OpcionesNombresCantidades.OperandoSubElemento = (DiseñoElementoOperacion)opcionOperandoSubElemento_TextosInformacion.SelectedItem;
            }
            else
            {
                OpcionesNombresCantidades.Operando = null;
                OpcionesNombresCantidades.OperandoSubElemento = null;
            }

                OpcionesNombresCantidades.PosicionesTextosInformacion = posicionesTextos.Text;
            OpcionesNombresCantidades.PosicionesTextosInformacionNumerosFiltrados = posicionesTextosNumerosFiltrados.Text;
            OpcionesNombresCantidades.PosicionesTextosInformacionNumero = posicionesTextosNumero.Text;
            OpcionesNombresCantidades.TextoInformacionFijo = textoInformacionFijo.Text;

                OpcionesNombresCantidades.PosicionesTextosInformacionNumero_Elemento = posicionesTextosNumeros.Text;

            if (opcionPrimerTextoInformacion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacion;
            else if (opcionPrimerosNTextosInformacion.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacion;
                int NPrimerosTextos = 0;
                int.TryParse(NPrimerosTextosInformacion.Text, out NPrimerosTextos);
                OpcionesNombresCantidades.NPrimerosTextosInformacion = NPrimerosTextos;
            }
            else if (opcionUltimoTextoInformacion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacion;
            else if (opcionUltimosNTextosInformacion.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacion;
                int NUltimosTextos = 0;
                int.TryParse(NUltimosTextosInformacion.Text, out NUltimosTextos);
                OpcionesNombresCantidades.NUltimosTextosInformacion = NUltimosTextos;
            }
            else if (opcionTextosInformacionCumplenCondiciones.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondiciones;
            }
            else if (opcionTodosTextosInformacion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.Todos;
            else if (opcionTextosInformacionPosiciones.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.EnPosiciones;
            else if (opcionTextosInformacionFijos.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextosInformacionFijos;
            else if (opcionTextoInformacionFijoNombreElemento.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreElemento;
            else if (opcionTextoInformacionFijoCantidadElemento.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadElemento;
            else if (opcionTextoInformacionFijoNombreOperacion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreOperacion;
            else if (opcionTextoInformacionFijoCantidadOperacion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadOperacion;
            else if (opcionTextoInformacionFijoPosicion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicion;
            else if (opcionTextoInformacionPosicionDefinicion.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionDefinicion;
            else if (opcionTextoInformacionPosicionOperando.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoPosicionOperando;
            else if (opcionTextosInformacionNombreNumero.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumeroElemento;
            else if (opcionTextosInformacionCantidadNumero.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumeroElemento;
            else if (opcionTextosInformacionNombreNumerosFiltrados.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoNombreNumerosFiltrados;
            else if (opcionTextosInformacionCantidadNumerosFiltrados.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TextoInformacionFijoCantidadNumerosFiltrados;
            else if (opcionTodosTextosInformacionNumerosFiltrados.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumerosFiltrados;
            else if (opcionPrimerTextoInformacionNumerosFiltrados.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumerosFiltrados;
            else if (opcionPrimerosNTextosInformacionNumerosFiltrados.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumerosFiltrados;
                int NPrimerosTextos = 0;
                int.TryParse(NPrimerosTextosInformacionNumerosFiltrados.Text, out NPrimerosTextos);
                OpcionesNombresCantidades.NPrimerosTextosInformacionNumerosFiltrados = NPrimerosTextos;
            }
            else if (opcionUltimoTextoInformacionNumerosFiltrados.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumerosFiltrados;
            else if (opcionUltimosNTextosInformacionNumerosFiltrados.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumerosFiltrados;
                int NUltimosTextos = 0;
                int.TryParse(NUltimosTextosInformacionNumerosFiltrados.Text, out NUltimosTextos);
                OpcionesNombresCantidades.NUltimosTextosInformacionNumerosFiltrados = NUltimosTextos;
            }
            else if (opcionTextosInformacionCumplenCondicionesNumerosFiltrados.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumerosFiltrados;
            }
            else if (opcionTextosInformacionPosicionesNumerosFiltrados.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumerosFiltrados;
            else if (opcionTodosTextosInformacionNumero.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.TodosTextosInformacionNumero;
            else if (opcionPrimerTextoInformacionNumero.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.PrimerTextoInformacionNumero;
            else if (opcionPrimerosNTextosInformacionNumero.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.PrimerosNTextosInformacionNumero;
                int NPrimerosTextos = 0;
                int.TryParse(NPrimerosTextosInformacionNumero.Text, out NPrimerosTextos);
                OpcionesNombresCantidades.NPrimerosTextosInformacionNumero = NPrimerosTextos;
            }
            else if (opcionUltimoTextoInformacionNumero.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimoTextoInformacionNumero;
            else if (opcionUltimosNTextosInformacionNumero.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.UltimosNTextosInformacionNumero;
                int NUltimosTextos = 0;
                int.TryParse(NUltimosTextosInformacionNumero.Text, out NUltimosTextos);
                OpcionesNombresCantidades.NUltimosTextosInformacionNumero = NUltimosTextos;
            }
            else if (opcionTextosInformacionCumplenCondicionesNumero.IsChecked == true)
            {
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.CumplenCondicionesNumero;
            }
            else if (opcionTextosInformacionPosicionesNumero.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcion = TipoOpcionesNombreCantidad_TextosInformacion.EnPosicionesNumero;

            if (opcionIncluirTextosImplica.IsChecked == true)
                    OpcionesNombresCantidades.IncluirTextosImplica = true;
                else
                    OpcionesNombresCantidades.IncluirTextosImplica = false;

            if (opcionIncluirComillas.IsChecked == true)
                OpcionesNombresCantidades.IncluirComillas = true;
            else
                OpcionesNombresCantidades.IncluirComillas = false;

            if (opcionFiltroNumerosTodos.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcionFiltroNumeros = TipoOpcionesFiltroNumeros_NombreCantidad.TodosNumeros;
            else if (opcionFiltroNumerosCumplenCondiciones.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcionFiltroNumeros = TipoOpcionesFiltroNumeros_NombreCantidad.NumerosCumplenCondiciones;
            else if (opcionFiltroNumerosPosiciones.IsChecked == true)
                OpcionesNombresCantidades.TipoOpcionFiltroNumeros = TipoOpcionesFiltroNumeros_NombreCantidad.NumerosEnPosiciones;

            DialogResult = true;
                Close();
            //}
        }

        private void opcionPrimerosNTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            NPrimerosTextosInformacion.Visibility = Visibility.Visible;
        }

        private void opcionPrimerosNTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            NPrimerosTextosInformacion.Visibility = Visibility.Collapsed;
        }

        private void opcionUltimosNTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            NUltimosTextosInformacion.Visibility = Visibility.Visible;
        }

        private void opcionUltimosNTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            NUltimosTextosInformacion.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionCumplenCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            opcionesCondiciones.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionCumplenCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionesCondiciones.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionPosiciones_Checked(object sender, RoutedEventArgs e)
        {
            posiciones.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionPosiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            posiciones.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionPosicionesNumerosFiltrados_Checked(object sender, RoutedEventArgs e)
        {
            posicionesNumerosFiltrados.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionPosicionesNumerosFiltrados_Unchecked(object sender, RoutedEventArgs e)
        {
            posicionesNumerosFiltrados.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionFijos_Checked(object sender, RoutedEventArgs e)
        {
            textoInformacionFijo.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionFijos_Unchecked(object sender, RoutedEventArgs e)
        {
            textoInformacionFijo.Visibility = Visibility.Collapsed;
        }

        private void quitarSeleccion_opcionOperandoSubElemento_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElemento_TextosInformacion.SelectedItem = null;
        }

        private void quitarSeleccion_opcionOperandoElemento_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperando_TextosInformacion.SelectedItem = null;
        }

        private void opcionTextosInformacionNombreNumeroPosicion_Checked(object sender, RoutedEventArgs e)
        {
            posicionesNumero.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionNombreNumeroPosicion_Unchecked(object sender, RoutedEventArgs e)
        {
            posicionesNumero.Visibility = Visibility.Collapsed;
        }

        private void opcionFiltroNumerosCumplenCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            opcionesCondicionesFiltroNumeros.Visibility = Visibility.Visible;
        }

        private void opcionFiltroNumerosCumplenCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionesCondicionesFiltroNumeros.Visibility = Visibility.Collapsed;
        }

        private void opcionPrimerosNTextosInformacionNumerosFiltrados_Checked(object sender, RoutedEventArgs e)
        {
            NPrimerosTextosInformacionNumerosFiltrados.Visibility = Visibility.Visible;
        }

        private void opcionPrimerosNTextosInformacionNumerosFiltrados_Unchecked(object sender, RoutedEventArgs e)
        {
            NPrimerosTextosInformacionNumerosFiltrados.Visibility = Visibility.Collapsed;
        }

        private void opcionUltimosNTextosInformacionNumerosFiltrados_Checked(object sender, RoutedEventArgs e)
        {
            NUltimosTextosInformacionNumerosFiltrados.Visibility = Visibility.Visible;
        }

        private void opcionUltimosNTextosInformacionNumerosFiltrados_Unchecked(object sender, RoutedEventArgs e)
        {
            NUltimosTextosInformacionNumerosFiltrados.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionCumplenCondicionesNumerosFiltrados_Checked(object sender, RoutedEventArgs e)
        {
            opcionesCondicionesNumerosFiltrados.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionCumplenCondicionesNumerosFiltrados_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionesCondicionesNumerosFiltrados.Visibility = Visibility.Collapsed;
        }

        private void opcionPrimerosNTextosInformacionNumero_Checked(object sender, RoutedEventArgs e)
        {
            NPrimerosTextosInformacionNumero.Visibility = Visibility.Visible;
        }

        private void opcionPrimerosNTextosInformacionNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            NPrimerosTextosInformacionNumero.Visibility = Visibility.Collapsed;
        }

        private void opcionUltimosNTextosInformacionNumero_Checked(object sender, RoutedEventArgs e)
        {
            NUltimosTextosInformacionNumero.Visibility = Visibility.Visible;
        }

        private void opcionUltimosNTextosInformacionNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            NUltimosTextosInformacionNumero.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionCumplenCondicionesNumero_Checked(object sender, RoutedEventArgs e)
        {
            opcionesCondicionesNumero.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionCumplenCondicionesNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionesCondicionesNumero.Visibility = Visibility.Collapsed;
        }
        private void opcionTextosInformacionPosicionesNumero_Checked(object sender, RoutedEventArgs e)
        {
            posicionesNumeroActual.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionPosicionesNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            posicionesNumeroActual.Visibility = Visibility.Collapsed;
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
