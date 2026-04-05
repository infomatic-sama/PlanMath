using ProcessCalc.Controles.TextosInformacion;
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
using System.Xml.Linq;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para AgregarQuitar_CondicionTextosInformacion.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionTextosInformacion : Window
    {
        public bool Aceptar { get; set; }
        public bool ModoEdicion { get; set; }
        public CondicionTextosInformacion Condicion { get; set; }
        public List<DiseñoOperacion> ListaOperandos { get; set; }
        public List<DiseñoElementoOperacion> ListaSubOperandos { get; set; }
        public bool MostrarOpcionesTextosImplicacionAsignacion { get; set; }
        public bool ModoSeleccionEntradas { get; set; }
        public List<Entrada> ListaEntradas { get; set; }
        public Entrada ElementoEntradaAsociado_Asignacion { get; set; }
        public DiseñoOperacion ElementoOperandoAsociado_Asignacion { get; set; }
        public DiseñoElementoOperacion ElementoSubOperandoAsociado_Asignacion { get; set; }
        BusquedaTextoArchivo Busqueda_TextoBusqueda = new BusquedaTextoArchivo() { Nombre = "Búsqueda entre los textos de información" , FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo };
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<DiseñoListaCadenasTexto> ListaDefinicionesListas { get; set; }
        public bool ModoAsignacionLogicaImplicaciones {  get; set; }
        public AgregarQuitar_CondicionTextosInformacion()
        {
            InitializeComponent();
        }

        private void opcionTipoElemento_TextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                {
                    opcionesOperandos_TextosInformacion.Visibility = Visibility.Visible;
                    opcionCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    textoTiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    tiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    textoOpcionCondicion.Visibility = Visibility.Visible;
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;

                    opcionValoresFijos_UnChecked(this, e);
                    opcionValores_ElementoOperacion_UnChecked(this, e);

                    if (opcionTextosInformacionFijos.IsChecked == true)
                        opcionTextosInformacionFijos_Checked(this, e);
                    else if (opcionTextosInformacion_ElementoEntrada.IsChecked == true)
                        opcionTextosInformacion_ElementoEntrada_Checked(this, e);
                    else if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                        opcionTextosInformacion_ElementoOperacion_Checked(this, e);
                    //else if (opcionTextosInformacion_Definicion.IsChecked == true)
                    //    opcionTextosInformacion_Definicion_Checked(this, e);

                    if (opcionConsiderarTextosInformacionComoCantidades.IsChecked == true)
                        opcionCondicion_OperacionEntrada_TextosInformacion.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionTipoElemento_TextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoElemento_TextosInformacion.IsChecked == false)
                {
                    opcionesOperandos_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    textoTiposValoresCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionCondicion.Visibility = Visibility.Collapsed;
                    tiposValoresCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoElemento_OperacionEntrada_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                {
                    opcionesElementos.Visibility = Visibility.Visible;
                    textoSubelementoElementoRelacionadoCondicion.Visibility = Visibility.Visible;                    
                    subelementoElementoRelacionadoCondicion.Visibility = Visibility.Visible;
                    opcionCondicion_OperacionEntrada.Visibility = Visibility.Visible;
                    tiposValoresCondicion.Visibility = Visibility.Visible;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;

                    opcionTextosInformacionFijos_UnChecked(this, e);
                    opcionTextosInformacion_ElementoEntrada_UnChecked(this, e);
                    opcionTextosInformacion_ElementoOperacion_UnChecked(this, e);

                    if (opcionValoresFijos.IsChecked == true)
                        opcionValoresFijos_Checked(this, e);
                    else if (opcionValores_ElementoOperacion.IsChecked == true)
                        opcionValores_ElementoOperacion_Checked(this, e);
                }
            }
        }

        private void opcionValores_ElementoOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionValores_ElementoOperacion.IsChecked == true)
                {
                    subelementoElementoRelacionadoCondicion_SelectionChanged(this, null);

                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Visible;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Visible;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Visible;
                    revertirOperandosCondicionValores.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionValores_ElementoOperacion_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                subelementoElementoRelacionadoCondicion_SelectionChanged(this, null);

                valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                textoValores.Visibility = Visibility.Collapsed;
                opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                //opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                revertirOperandosCondicionValores.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionValoresFijos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionValoresFijos.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    textoValores.Visibility = Visibility.Visible;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion_Valores.Items where I.Uid == ((int)TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento).ToString() select I).FirstOrDefault();
                }
            }
        }

        private void opcionValoresFijos_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                
                valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                textoValores.Visibility = Visibility.Collapsed;
                //opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                //opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;

            }
        }

        private void opcionTipoElemento_OperacionEntrada_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoElemento_OperacionEntrada.IsChecked == false)
                {
                    opcionesElementos.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion.Visibility = Visibility.Collapsed;
                    opcionCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    tiposValoresCondicion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacionFijos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacionFijos.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    textoValores.Visibility = Visibility.Visible;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Visible;
                    opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacionFijos_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {                
                valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                //opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                textoValores.Visibility = Visibility.Collapsed;
                //opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Collapsed;
               
            }
        }

        private void opcionTextosInformacion_ElementoOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Visible;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Visible;

                    opcionCondicion_TextosInformacion_SelectionChanged(this, null);
                }
            }
        }

        private void opcionTextosInformacion_ElementoOperacion_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {                
                //valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                //textoValores.Visibility = Visibility.Collapsed;
                //opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Collapsed;

                //opcionCondicion_TextosInformacion_SelectionChanged(this, null);
                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ListaElementosComboOperandos = new ArrayList();

            if(ModoAsignacionLogicaImplicaciones)
            {
                opcionConsiderarOperandoValores_CantidadesAsignacion_LogicaImplicaciones.Visibility = Visibility.Visible;
                opcionConsiderarOperandoCondicion_CantidadesAsignacion_LogicaImplicaciones.Visibility = Visibility.Visible;
            }
            else
            {
                opcionConsiderarOperandoValores_CantidadesAsignacion_LogicaImplicaciones.Visibility = Visibility.Collapsed;
                opcionConsiderarOperandoCondicion_CantidadesAsignacion_LogicaImplicaciones.Visibility = Visibility.Collapsed;
            }

            if (ModoSeleccionEntradas)
            {
                opcionTextosInformacion_ElementoOperacion.Visibility = Visibility.Collapsed;
                opcionTextosInformacion_ElementoEntrada.Visibility = Visibility.Visible;
                opcionTipoElemento_OperacionEntrada.Visibility = Visibility.Collapsed;

                textoOpcionOperandoCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionOperandoCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                textoOpcionOperandoSubElementoCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionOperandoSubElementoCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                quitarSeleccion_opcionOperandoSubElementoCondicion_TextosInformacion.Visibility = Visibility.Collapsed;

                opcionElementoOperacion_TextosInformacion.Visibility = Visibility.Collapsed;
                textoOpcionOperandoSubElemento_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionOperandoSubElemento_TextosInformacion.Visibility = Visibility.Collapsed;
                quitarSeleccion_opcionOperandoSubElemento_TextosInformacion.Visibility = Visibility.Collapsed;

                //textoTipoElemento_OperacionEntrada.Visibility = Visibility.Collapsed;
                //elementoRelacionadoCondicion.Visibility = Visibility.Collapsed;
                //textoOpcionOperandoSubElementoCondicion.Visibility = Visibility.Collapsed;
                //opcionOperandoSubElementoCondicion.Visibility = Visibility.Collapsed;
                //quitarSeleccion_opcionOperandoSubElementoCondicion.Visibility = Visibility.Collapsed;

                //textoOpcionOperandoCondicion_Elemento.Visibility = Visibility.Collapsed;
                //opcionOperandoCondicion_Elemento.Visibility = Visibility.Collapsed;
                //textoOpcionOperandoSubElementoCondicion_Elemento.Visibility = Visibility.Collapsed;
                //opcionOperandoSubElementoCondicion_Elemento.Visibility = Visibility.Collapsed;
                //quitarSeleccion_opcionOperandoSubElementoCondicion_Elemento.Visibility = Visibility.Collapsed;


                //opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Visible;
                textoopcionEntrada_TextosInformacion.Visibility = Visibility.Visible;
                opcionEntrada_TextosInformacion.Visibility = Visibility.Visible;

                opcionEntrada_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionEntrada_TextosInformacion.SelectedValuePath = "Nombre";
                opcionEntrada_TextosInformacion.ItemsSource = ListaEntradas;

                opcionEntrada_TextosInformacion_Valores.DisplayMemberPath = "NombreCombo";
                opcionEntrada_TextosInformacion_Valores.SelectedValuePath = "Nombre";
                opcionEntrada_TextosInformacion_Valores.ItemsSource = ListaEntradas;
            }
            else
            {
                ListaElementosComboOperandos.Add(new ComboBoxItem()
                {
                    Content = "Operandos de esta operación",
                    IsEnabled = false,
                });

                ListaElementosComboOperandos.Add(new DiseñoOperacion()
                {
                    Nombre = "Operando actual de la ejecución (todos o cualquier operando)",
                    ID = "0",
                });

                if (ListaOperandos != null &&
                    ListaOperandos.Any())
                {
                    ListaElementosComboOperandos.AddRange(ListaOperandos.Where(item => !string.IsNullOrEmpty(item.NombreCombo)
                &&
                item.Tipo != TipoElementoOperacion.Salida).ToList());
                }

                ListaElementosComboOperandos.Add(new ComboBoxItem()
                {
                    Content = "Elementos operandos del cálculo",
                    IsEnabled = false,
                });

                if (ListaElementos != null &&
                    ListaElementos.Any())
                {
                    ListaElementosComboOperandos.AddRange(ListaElementos.Where(item => !string.IsNullOrEmpty(item.NombreCombo)
                &&
                item.Tipo != TipoElementoOperacion.Salida).ToList());
                }

                elementoRelacionadoCondicion.DisplayMemberPath = "NombreCombo";
                elementoRelacionadoCondicion.SelectedValuePath = "NombreCombo";

                elementoRelacionadoCondicion.ItemsSource = ListaElementosComboOperandos;

                opcionElementoOperacion_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionElementoOperacion_TextosInformacion.SelectedValuePath = "NombreCombo";
                opcionElementoOperacion_TextosInformacion.ItemsSource = ListaElementosComboOperandos;

                opcionOperandoSubElemento_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionOperandoSubElemento_TextosInformacion.SelectedValuePath = "NombreCombo";

                if (ListaSubOperandos != null)
                    opcionOperandoSubElemento_TextosInformacion.ItemsSource = ListaSubOperandos.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota);

                opcionOperandoCondicion_Elemento.DisplayMemberPath = "NombreCombo";
                opcionOperandoCondicion_Elemento.SelectedValuePath = "NombreCombo";

                opcionOperandoCondicion_Elemento.ItemsSource = ListaElementosComboOperandos;

                opcionOperandoSubElementoCondicion_Elemento.DisplayMemberPath = "NombreCombo";
                opcionOperandoSubElementoCondicion_Elemento.SelectedValuePath = "NombreCombo";

                if (ListaSubOperandos != null)
                    opcionOperandoSubElementoCondicion_Elemento.ItemsSource = ListaSubOperandos.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota);

                opcionOperandoCondicion_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionOperandoCondicion_TextosInformacion.SelectedValuePath = "NombreCombo";
                opcionOperandoCondicion_TextosInformacion.ItemsSource = ListaElementosComboOperandos;

                opcionOperandoSubElementoCondicion_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionOperandoSubElementoCondicion_TextosInformacion.SelectedValuePath = "NombreCombo";

                if (ListaSubOperandos != null)
                    opcionOperandoSubElementoCondicion_TextosInformacion.ItemsSource = ListaSubOperandos.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota);

                opcionDefinicion_ListasTextosInformacion.DisplayMemberPath = "Nombre";
                opcionDefinicion_ListasTextosInformacion.ItemsSource = ListaDefinicionesListas;
            }

            if(MostrarOpcionesTextosImplicacionAsignacion)
            {
                opcionesImplicacionAsignacionTextosInformacion.Visibility = Visibility.Visible;
                opcionOperandoCondicion_TextosInformacion.ItemsSource = new List<DiseñoOperacion>() { ElementoOperandoAsociado_Asignacion };
                opcionOperandoCondicion_TextosInformacion.SelectedIndex = 0;
                opcionOperandoCondicion_TextosInformacion.IsEnabled = false;

                opcionOperandoSubElementoCondicion_TextosInformacion.ItemsSource = new List<DiseñoElementoOperacion>() { ElementoSubOperandoAsociado_Asignacion };
                opcionOperandoSubElementoCondicion_TextosInformacion.SelectedIndex = 0;
                opcionOperandoSubElementoCondicion_TextosInformacion.IsEnabled = false;

                opcionEntrada_TextosInformacion.ItemsSource = new List<Entrada>() { ElementoEntradaAsociado_Asignacion };
                opcionEntrada_TextosInformacion.SelectedIndex = 0;
                opcionEntrada_TextosInformacion.IsEnabled = false;
            }

            if (ModoEdicion)
            {
                if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    opcionTipoElemento_TextosInformacion.IsChecked = true;

                    opcionTipoElemento_TextosInformacion_Checked(this, e);
                    opcionTipoElemento_OperacionEntrada_Unchecked(this, e);
                    
                }
                else if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    opcionTipoElemento_OperacionEntrada.IsChecked = true;

                    opcionTipoElemento_OperacionEntrada_Checked(this, e);
                    opcionTipoElemento_TextosInformacion_Unchecked(this, e);
                }

                if (Condicion.EsOperandoActual)
                {
                    elementoRelacionadoCondicion.SelectedItem = ListaElementosComboOperandos.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElementoCondicion.SelectedItem = null;
                }
                else
                {
                    if (Condicion.ElementoCondicion != null)
                        elementoRelacionadoCondicion.SelectedValue = Condicion.ElementoCondicion.NombreCombo;

                    if (Condicion.OperandoSubElemento_Condicion != null)
                        opcionOperandoSubElementoCondicion.SelectedValue = Condicion.OperandoSubElemento_Condicion.NombreCombo;
                }

                opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion_Elemento).ToString() select I).FirstOrDefault();

                if (Condicion.EsOperandoTextosActual)
                {
                    opcionOperandoCondicion_TextosInformacion.SelectedItem = ListaElementosComboOperandos.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElementoCondicion_TextosInformacion.SelectedItem = null;
                }
                else
                {
                    if (Condicion.OperandoCondicion != null)
                        opcionOperandoCondicion_TextosInformacion.SelectedValue = Condicion.OperandoCondicion.NombreCombo;

                    if (Condicion.OperandoSubElemento_Condicion_TextosInformacion != null)
                        opcionOperandoSubElementoCondicion_TextosInformacion.SelectedValue = Condicion.OperandoSubElemento_Condicion_TextosInformacion.NombreCombo;
                }

                OpcionOperandoCondicion_Clasificadores.IsChecked = Condicion.CadenasTextoSon_Clasificadores;
                OpcionOperandoValores_Clasificadores.IsChecked = Condicion.CadenasTextoSon_Clasificadores_Valores;

                opcionSeleccionNumerosElementoCondicion.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion).ToString() select I).FirstOrDefault();
                opcionSeleccionNumerosElementoCondicion_SelectionChanged(this, null);

                opcionCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_TextosInformacion).ToString() select I).FirstOrDefault();

                opcionDefinicion_ListasTextosInformacion.SelectedItem = Condicion.ElementoDefinicionListas_Valores;

                subelementoElementoRelacionadoCondicion.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion.Items where I.Uid == ((int)Condicion.TipoSubElemento_Condicion).ToString() select I).FirstOrDefault();
                subelementoElementoRelacionadoCondicion_Valores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion_Valores.Items where I.Uid == ((int)Condicion.TipoSubElemento_Condicion_Valores).ToString() select I).FirstOrDefault();

                if (Condicion.OpcionValorPosicion == TipoOpcionPosicion.Ninguna)
                {
                    if (Condicion.TipoElemento_Valores == TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos)
                    {
                        opcionValoresFijos.IsChecked = true;
                        opcionValoresFijos_Checked(this, e);
                    }
                    else if (Condicion.TipoElemento_Valores == TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.Valores_DesdeElementoOperacion)
                    {
                        opcionValores_ElementoOperacion.IsChecked = true;
                        opcionValores_ElementoOperacion_Checked(this, e);
                    }
                }

                if (Condicion.TipoSubElemento_Condicion == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento &&
                    Condicion.OpcionValorPosicion != TipoOpcionPosicion.Ninguna)
                {
                    if (Condicion.OpcionValorPosicion == TipoOpcionPosicion.PosicionPrimera)
                        opcionValorPosicionPrimera.IsChecked = true;
                    else if (Condicion.OpcionValorPosicion == TipoOpcionPosicion.PosicionSegunda)
                        opcionValorPosicionSegunda.IsChecked = true;
                    else if (Condicion.OpcionValorPosicion == TipoOpcionPosicion.PosicionMitad)
                        opcionValorPosicionMitad.IsChecked = true;
                    else if (Condicion.OpcionValorPosicion == TipoOpcionPosicion.PosicionPenultima)
                        opcionValorPosicionPeultima.IsChecked = true;
                    else if (Condicion.OpcionValorPosicion == TipoOpcionPosicion.PosicionUltima)
                        opcionValorPosicionUltima.IsChecked = true;

                    opcionValoresFijos_UnChecked(this, e);
                    opcionValores_ElementoOperacion_UnChecked(this, e);
                }

                opcionConsiderarTextosInformacionComoCantidades.IsChecked = Condicion.ConsiderarTextosInformacionComoCantidades;

                if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos)
                        opcionTextosInformacionFijos.IsChecked = true;
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion)
                    {
                        opcionTextosInformacion_ElementoOperacion.IsChecked = true;
                        opcionCondicion_TextosInformacion_SelectionChanged(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada)
                    {
                        opcionTextosInformacion_ElementoEntrada.IsChecked = true;
                        opcionTextosInformacion_ElementoEntrada_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeCumplenCondicionImplicacion)
                    {
                        opcionTextosInformacionCumplenCondicionImplicacion.IsChecked = true;
                        opcionTextosInformacionCumplenCondicionImplicacion_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionInstancia)
                    {
                        opcionTextosInformacionImplicacionInstancia.IsChecked = true;
                        opcionTextosInformacionImplicacionInstancia_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionCondicion)
                    {
                        opcionTextosInformacionImplicacionCondicion.IsChecked = true;
                        opcionTextosInformacionImplicacionCondicion_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacion)
                    {
                        opcionTextosInformacionImplicacion.IsChecked = true;
                        opcionTextosInformacionImplicacion_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion)
                    {
                        opcionTextosInformacion_TextosInformacion_CumplenCondicion.IsChecked = true;
                        opcionTextosInformacion_TextosInformacion_CumplenCondicion_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TodosTextosInformacion_CumplenCondicion)
                    {
                        opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion.IsChecked = true;
                        opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion_Checked(this, null);
                    }
                    else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista)
                    {
                        opcionTextosInformacion_DefinicionListas.IsChecked = true;
                        opcionTextosInformacion_DefinicionListas_Checked(this, null);
                    }

                    if (opcionConsiderarTextosInformacionComoCantidades.IsChecked == true)
                        opcionCondicion_OperacionEntrada_TextosInformacion.Visibility = Visibility.Visible;
                }
                else if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    if (opcionValoresFijos.IsChecked == true)
                        opcionValoresFijos_Checked(this, e);
                    else if (opcionValores_ElementoOperacion.IsChecked == true)
                        opcionValores_ElementoOperacion_Checked(this, e);
                }

                if (Condicion.EsOperandoValoresTextosActual)
                {
                    opcionOperandoCondicion_Elemento.SelectedItem = ListaElementosComboOperandos.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElementoCondicion_Elemento.SelectedItem = null;
                }
                else
                {
                    if (Condicion.ElementoOperacion_Valores_ElementoAsociado != null)
                        opcionOperandoCondicion_Elemento.SelectedValue = Condicion.ElementoOperacion_Valores_ElementoAsociado.NombreCombo;

                    if (Condicion.OperandoSubElemento_Condicion_Elemento != null)
                        opcionOperandoSubElementoCondicion_Elemento.SelectedValue = Condicion.OperandoSubElemento_Condicion_Elemento.NombreCombo;
                }

                opcionVaciarListaTextosInformacion_CumplenCondicion.IsChecked = Condicion.VaciarListaTextosInformacion_CumplenCondicion;

                if (opcionVaciarListaTextosInformacion_CumplenCondicion.IsChecked == true)
                {
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.Visibility = Visibility.Visible;
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.Visibility = Visibility.Collapsed;
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.Visibility = Visibility.Collapsed;
                }

                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.IsChecked = Condicion.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple;
                if (opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.IsChecked == false)
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.IsChecked = true;

                opcionTipoElementoCompararTextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoElementoCompararTextosInformacion.Items where I.Uid == ((int)Condicion.TipoElementoComparar_TextosInformacion).ToString() select I).FirstOrDefault();

                //if (opcionTextosInformacionFijos.IsChecked == true)
                //    opcionTextosInformacionFijos_Checked(this, e);
                //else if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                //    opcionTextosInformacion_ElementoOperacion_Checked(this, e);
                if (Condicion.EsOperandoValoresActual)
                {
                    opcionElementoOperacion_TextosInformacion.SelectedItem = ListaElementosComboOperandos.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElemento_TextosInformacion.SelectedItem = null;
                }
                else
                {
                    opcionElementoOperacion_TextosInformacion.SelectedItem = Condicion.ElementoOperacion_Valores;

                    if (Condicion.OperandoSubElemento_Valores_TextosInformacion != null)
                        opcionOperandoSubElemento_TextosInformacion.SelectedValue = Condicion.OperandoSubElemento_Valores_TextosInformacion.NombreCombo;
                }

                opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.ToString();
                opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.IsChecked = (bool)Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada;

                opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_CondicionValores).ToString() select I).FirstOrDefault();
                opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores_Listas.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_CondicionValores).ToString() select I).FirstOrDefault();

                conectorCondicion.SelectedItem = (from ComboBoxItem I in conectorCondicion.Items where I.Uid == ((int)Condicion.TipoConector).ToString() select I).FirstOrDefault();
                conectorCondicion_SelectionChanged(this, null);
                conectorO_Excluyente.IsChecked = (bool)Condicion.ConectorO_Excluyente;

                opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_OperacionEntrada.Text = Condicion.CantidadSubNumerosCumplenCondicion_OperacionEntrada.ToString();
                opcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada;
                                
                opcionCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_ElementoOperacionEntrada).ToString() select I).FirstOrDefault();
                opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCondicion_OperacionEntrada_TextosInformacion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_ElementoOperacionEntrada).ToString() select I).FirstOrDefault();

                opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;

                opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(this, null);
                

                opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_TextosInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion;

                opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_SelectionChanged(this, null);
                //opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_SelectionChanged(this, null);

                opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.IsChecked = (bool)Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;

                opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_SelectionChanged(this, null);
                //opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_SelectionChanged(this, null);

                opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_Valores.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_Valores.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores;

                //opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                //opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                //cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.ToString();
                //opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                //opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;

                //opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(this, null);
                //opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(this, null);

                opcionCantidadNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                cantidadNumerosCumplenCondicion.Text = Condicion.CantidadNumerosCumplenCondicion.ToString();
                opcionSaldoCantidadNumerosCumplenCondicion.IsChecked = (bool)Condicion.OpcionSaldoCantidadNumerosCumplenCondicion;

                if (Condicion.EntradaTextosInformacion_Condicion != null)
                    opcionEntrada_TextosInformacion.SelectedValue = Condicion.EntradaTextosInformacion_Condicion.Nombre;

                if (Condicion.EntradaTextosInformacion_Condicion_Valores != null)
                    opcionEntrada_TextosInformacion_Valores.SelectedValue = Condicion.EntradaTextosInformacion_Condicion_Valores.Nombre;

                opcionIncluirNombreElemento.IsChecked = (bool)Condicion.IncluirNombreElementoConTextos;
                opcionIncluirSoloNombreElemento.IsChecked = (bool)Condicion.IncluirSoloNombreElemento;

                if (!Condicion.IncluirNombreElementoConTextos &
                    !Condicion.IncluirSoloNombreElemento)
                    opcionIncluirSoloTextosElemento.IsChecked = true;

                opcionNegarCondicion.IsChecked = (bool)Condicion.NegarCondicion;
                opcionQuitarEspaciosTemporalmente_CadenaCondicion.IsChecked = (bool)Condicion.QuitarEspaciosTemporalmente_CadenaCondicion;
                opcionAplicarCondicionFinalNumerosOperando.IsChecked = (bool)Condicion.SeguirAplicandoCondicion_AlFinalCantidadesOperando;
                opcionAplicarCondicionFinalNumerosOperandoValores.IsChecked = (bool)Condicion.SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores;
                opcionAplicarReiniciarFinalNumerosOperando.IsChecked = (bool)Condicion.ReiniciarPosicion_AlFinalCantidadesOperando;
                opcionAplicarReiniciarFinalNumerosOperando_Valores.IsChecked = (bool)Condicion.ReiniciarPosicion_AlFinalCantidadesOperando_Valores;

                opcionCumpleCondicion_ElementoSinCantidades.IsChecked = (bool)Condicion.CumpleCondicion_ElementoSinNumeros;
                opcionCumpleCondicion_ElementoValores_SinCantidades.IsChecked = (bool)Condicion.CumpleCondicion_ElementoValores_SinNumeros;

                opcionConsiderarOperandoCondicion_SiCumple.IsChecked = (bool)Condicion.ConsiderarOperandoCondicion_SiCumple;
                opcionCantidadTextosInformacion_PorElemento.IsChecked = (bool)Condicion.CantidadTextosInformacion_PorElemento;
                opcionCantidadTextosInformacion_PorElemento_Valores.IsChecked = (bool)Condicion.CantidadTextosInformacion_PorElemento_Valores;

                opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked = (bool)Condicion.CantidadTextosInformacion_SoloCadenasCumplen;
                opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = (bool)Condicion.CantidadTextosInformacion_SoloCadenasCumplen_Valores;

                opcionCantidadNumeros_PorElemento.IsChecked = (bool)Condicion.CantidadNumeros_PorElemento;
                opcionCantidadNumeros_PorElemento_Valores.IsChecked = (bool)Condicion.CantidadNumeros_PorElemento_Valores;

                if (opcionConsiderarOperandoCondicion_SiCumple.IsChecked == true)
                    opcionConsiderarCondicionesHijas.Visibility = Visibility.Visible;
                else
                    opcionConsiderarCondicionesHijas.Visibility = Visibility.Collapsed;

                opcionConsiderarCondicionesHijas.IsChecked = (bool)Condicion.ConsiderarIncluirCondicionesHijas;
                opcionCualquierTextoInformacion.IsChecked = (bool)Condicion.BuscarCualquierTextoInformacion_TextoBusqueda;

                valoresRelacionadosCondicion.Text = EstablecerPosicion_SiCorresponde(Condicion.Valores_Condicion, false);
                opcionConsiderarOperandoValores_CantidadesAsignacion_LogicaImplicaciones.IsChecked = Condicion.ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones;
                opcionConsiderarOperandoCondicion_CantidadesAsignacion_LogicaImplicaciones.IsChecked = Condicion.ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones;

                Busqueda_TextoBusqueda = Condicion.Busqueda_TextoBusqueda;

                if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                {
                    opcionTipoElemento_OperacionEntrada_Unchecked(this, e);
                    opcionTipoElemento_TextosInformacion_Checked(this, e);

                    MostrarOcultarOpciones_CadenasTexto();
                }
                else if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                {
                    opcionTipoElemento_TextosInformacion_Unchecked(this, e);
                    opcionTipoElemento_OperacionEntrada_Checked(this, e);
                }

            }
            else
            {
                opcionTextosInformacionFijos.IsChecked = true;                
                opcionConsiderarOperandoCondicion_SiCumple.IsChecked = true;
                opcionConsiderarCondicionesHijas.IsChecked = true;
                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.IsChecked = true;

                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.Visibility = Visibility.Collapsed;
                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.Visibility = Visibility.Collapsed;
            }

            GenerarBusqueda_TextoBusqueda();
            textoBusqueda.UserControl_Loaded(this, e);
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (!ModoSeleccionEntradas && opcionesOperandos_TextosInformacion.Visibility == Visibility.Visible && opcionOperandoCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesElementos.Visibility == Visibility.Visible && elementoRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesOperandosValoresCondicion.Visibility == Visibility.Visible && opcionElementoOperacion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de cadenas de texto.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_TextosInformacion.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de la variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_TextosInformacion.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion_Valores.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de la variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCondicion_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de variables o vectores.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion_OperacionEntrada_TextosInformacion.Visibility == Visibility.Visible && opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de variables o vectores.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionDefinicion_ListasTextosInformacion.Visibility == Visibility.Visible && opcionDefinicion_ListasTextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una definición de listas de cadenas de texto.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_Elemento_Valores.Visibility == Visibility.Visible && opcionOperandoCondicion_Elemento.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (conectorCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de conector con la condición anterior.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de cadenas de texto de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionCantidadNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números del vector que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (!ModoSeleccionEntradas && opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility == Visibility.Visible && opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números del vector que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if(ModoSeleccionEntradas && opcionTextosInformacion_ElementoEntrada.IsChecked == true && opcionEntrada_TextosInformacion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector de entrada que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int numero = 0;

                if (int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero))
                {
                    if (!ModoEdicion)
                    {
                        Condicion = new CondicionTextosInformacion();
                        Condicion.CantidadNumerosCumplenCondicion = numero;
                    }

                    if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                        Condicion.TipoElementoCondicion = TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion;
                    else if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                        Condicion.TipoElementoCondicion = TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada;

                    if (elementoRelacionadoCondicion.SelectedItem != null && 
                        ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ID != "0")
                    {
                        Condicion.ElementoCondicion = (DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem;
                        Condicion.OperandoSubElemento_Condicion = (DiseñoElementoOperacion)opcionOperandoSubElementoCondicion.SelectedItem;
                        Condicion.EsOperandoActual = false;
                    }
                    else
                    {
                        Condicion.ElementoCondicion = null;
                        Condicion.OperandoSubElemento_Condicion = null;
                        Condicion.EsOperandoActual = true;
                    }

                    if (opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion_Elemento = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem).Uid);

                    if (opcionOperandoCondicion_TextosInformacion.SelectedItem != null && 
                        ((DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem).ID != "0")
                    {
                        Condicion.OperandoCondicion = (DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem;
                        Condicion.OperandoSubElemento_Condicion_TextosInformacion = (DiseñoElementoOperacion)opcionOperandoSubElementoCondicion_TextosInformacion.SelectedItem;
                        Condicion.EsOperandoTextosActual = false;
                    }
                    else
                    {
                        Condicion.OperandoCondicion = null;
                        Condicion.OperandoSubElemento_Condicion_TextosInformacion = null;
                        Condicion.EsOperandoTextosActual = true;
                    }

                    Condicion.CadenasTextoSon_Clasificadores = (bool)OpcionOperandoCondicion_Clasificadores.IsChecked;
                    Condicion.CadenasTextoSon_Clasificadores_Valores = (bool)OpcionOperandoValores_Clasificadores.IsChecked;

                    if (opcionSeleccionNumerosElementoCondicion.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid);

                    if (opcionCondicion_TextosInformacion.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_TextosInformacion = (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid);

                    if (opcionTextosInformacionFijos.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos;
                    else if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion;
                    else if (opcionTextosInformacionCumplenCondicionImplicacion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeCumplenCondicionImplicacion;
                    else if (opcionTextosInformacionImplicacionInstancia.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionInstancia;
                    else if (opcionTextosInformacionImplicacionCondicion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacionCondicion;
                    else if (opcionTextosInformacionImplicacion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeImplicacion;
                    else if (opcionTextosInformacion_ElementoEntrada.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada;
                    else if (opcionTextosInformacion_TextosInformacion_CumplenCondicion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TextosInformacion_CumplenCondicion;
                    else if (opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_TodosTextosInformacion_CumplenCondicion;
                    else if (opcionTextosInformacion_DefinicionListas.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista;

                    if (opcionDefinicion_ListasTextosInformacion.SelectedItem != null)
                        Condicion.ElementoDefinicionListas_Valores = (DiseñoListaCadenasTexto)opcionDefinicion_ListasTextosInformacion.SelectedItem;

                    if (opcionOperandoCondicion_Elemento.SelectedItem != null && 
                        ((DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem).ID != "0")
                    {
                        if (opcionOperandoCondicion_Elemento.SelectedItem != null)
                            Condicion.ElementoOperacion_Valores_ElementoAsociado = (DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem;

                        Condicion.OperandoSubElemento_Condicion_Elemento = (DiseñoElementoOperacion)opcionOperandoSubElementoCondicion_Elemento.SelectedItem;

                        Condicion.EsOperandoValoresTextosActual = false;
                    }
                    else
                    {
                        Condicion.ElementoOperacion_Valores_ElementoAsociado = null;
                        Condicion.OperandoSubElemento_Condicion_Elemento = null;
                        Condicion.EsOperandoValoresTextosActual = true;
                    }

                    Condicion.VaciarListaTextosInformacion_CumplenCondicion = (bool)opcionVaciarListaTextosInformacion_CumplenCondicion.IsChecked;
                    Condicion.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple = (bool)opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.IsChecked;

                    if (opcionTipoElementoCompararTextosInformacion.SelectedItem != null)
                        Condicion.TipoElementoComparar_TextosInformacion = (TipoOpcionElementoComparar_TextosInformacion)int.Parse(((ComboBoxItem)opcionTipoElementoCompararTextosInformacion.SelectedItem).Uid);

                    if (opcionElementoOperacion_TextosInformacion.SelectedItem != null && 
                        ((DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem).ID != "0")
                    {
                        if (opcionElementoOperacion_TextosInformacion.SelectedItem != null)
                            Condicion.ElementoOperacion_Valores = (DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem;

                        Condicion.OperandoSubElemento_Valores_TextosInformacion = (DiseñoElementoOperacion)opcionOperandoSubElemento_TextosInformacion.SelectedItem;

                        Condicion.EsOperandoValoresActual = false;
                    }
                    else
                    {
                        Condicion.ElementoOperacion_Valores = null;
                        Condicion.OperandoSubElemento_Valores_TextosInformacion = null;
                        Condicion.EsOperandoValoresActual = true;
                    }

                    if (opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_CondicionValores = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid);

                    if (opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_CondicionValores = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem).Uid);

                    if (opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores_OperacionEntrada = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem).Uid);

                    numero = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_TextosInformacion.Text, out numero);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion = numero;

                    int numero2 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_Valores.Text, out numero2);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores = numero2;

                    int numero3 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Text, out numero3);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = numero3;

                    int numero4 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Text, out numero4);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = numero4;

                    int numero5 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_OperacionEntrada.Text, out numero5);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_OperacionEntrada = numero5;

                    int numero6 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Text, out numero6);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = numero6;

                    Condicion.TipoConector = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)conectorCondicion.SelectedItem).Uid);

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada.IsChecked;

                    if (subelementoElementoRelacionadoCondicion.SelectedItem != null)
                        Condicion.TipoSubElemento_Condicion = (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion.SelectedItem).Uid);
                    if (subelementoElementoRelacionadoCondicion_Valores.SelectedItem != null)
                        Condicion.TipoSubElemento_Condicion_Valores = (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion_Valores.SelectedItem).Uid);
                    
                    Condicion.ConsiderarTextosInformacionComoCantidades = (bool)opcionConsiderarTextosInformacionComoCantidades.IsChecked;

                    if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion &&
                        Condicion.ConsiderarTextosInformacionComoCantidades &&
                        opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_ElementoOperacionEntrada = (TipoOpcion_CondicionTextosInformacion_Implicacion)int.Parse(((ComboBoxItem)opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem).Uid);

                    if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada &&
                        opcionCondicion_OperacionEntrada.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_ElementoOperacionEntrada = (TipoOpcion_CondicionTextosInformacion_Implicacion)int.Parse(((ComboBoxItem)opcionCondicion_OperacionEntrada.SelectedItem).Uid);


                    if (opcionValoresFijos.IsChecked == true |
                        opcionValores_ElementoOperacion.IsChecked == true)
                    {
                        if (opcionValoresFijos.IsChecked == true)
                            Condicion.TipoElemento_Valores = TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.ValoresFijos;
                        else if (opcionValores_ElementoOperacion.IsChecked == true)
                            Condicion.TipoElemento_Valores = TipoOpcionElemetn_ValoresCondicion_ImplicacionTextosInformacion.Valores_DesdeElementoOperacion;

                        Condicion.OpcionValorPosicion = TipoOpcionPosicion.Ninguna;
                    }
                    else
                    {
                        if (opcionValorPosicionPrimera.IsChecked == true)
                            Condicion.OpcionValorPosicion = TipoOpcionPosicion.PosicionPrimera;
                        else if (opcionValorPosicionSegunda.IsChecked == true)
                            Condicion.OpcionValorPosicion = TipoOpcionPosicion.PosicionSegunda;
                        else if (opcionValorPosicionMitad.IsChecked == true)
                            Condicion.OpcionValorPosicion = TipoOpcionPosicion.PosicionMitad;
                        else if (opcionValorPosicionPeultima.IsChecked == true)
                            Condicion.OpcionValorPosicion = TipoOpcionPosicion.PosicionPenultima;
                        else if (opcionValorPosicionUltima.IsChecked == true)
                            Condicion.OpcionValorPosicion = TipoOpcionPosicion.PosicionUltima;
                    }

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.IsChecked;

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.IsChecked;

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion.IsChecked;

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.IsChecked;

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_Valores.IsChecked;

                    
                    Condicion.OpcionCantidadNumerosCumplenCondicion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadNumerosCumplenCondicion = (bool)opcionSaldoCantidadNumerosCumplenCondicion.IsChecked;

                    Condicion.IncluirNombreElementoConTextos = (bool)opcionIncluirNombreElemento.IsChecked;
                    Condicion.IncluirSoloNombreElemento = (bool)opcionIncluirSoloNombreElemento.IsChecked;

                    Condicion.EntradaTextosInformacion_Condicion = (Entrada)opcionEntrada_TextosInformacion.SelectedItem;
                    Condicion.EntradaTextosInformacion_Condicion_Valores = (Entrada)opcionEntrada_TextosInformacion_Valores.SelectedItem;

                    Condicion.NegarCondicion = (bool)opcionNegarCondicion.IsChecked;
                    Condicion.QuitarEspaciosTemporalmente_CadenaCondicion = (bool)opcionQuitarEspaciosTemporalmente_CadenaCondicion.IsChecked;
                    Condicion.SeguirAplicandoCondicion_AlFinalCantidadesOperando = (bool)opcionAplicarCondicionFinalNumerosOperando.IsChecked;
                    Condicion.SeguirAplicandoCondicion_AlFinalCantidadesOperando_Valores = (bool)opcionAplicarCondicionFinalNumerosOperandoValores.IsChecked;
                    Condicion.ReiniciarPosicion_AlFinalCantidadesOperando = (bool)opcionAplicarReiniciarFinalNumerosOperando.IsChecked;
                    Condicion.ReiniciarPosicion_AlFinalCantidadesOperando_Valores = (bool)opcionAplicarReiniciarFinalNumerosOperando_Valores.IsChecked;

                    Condicion.CumpleCondicion_ElementoSinNumeros = (bool)opcionCumpleCondicion_ElementoSinCantidades.IsChecked;
                    Condicion.CumpleCondicion_ElementoValores_SinNumeros = (bool)opcionCumpleCondicion_ElementoValores_SinCantidades.IsChecked;

                    Condicion.ConectorO_Excluyente = (bool)conectorO_Excluyente.IsChecked;

                    Condicion.ConsiderarOperandoCondicion_SiCumple = (bool)opcionConsiderarOperandoCondicion_SiCumple.IsChecked;
                    Condicion.ConsiderarIncluirCondicionesHijas = (bool)opcionConsiderarCondicionesHijas.IsChecked;

                    Condicion.CantidadTextosInformacion_PorElemento = (bool)opcionCantidadTextosInformacion_PorElemento.IsChecked;
                    Condicion.CantidadTextosInformacion_PorElemento_Valores = (bool)opcionCantidadTextosInformacion_PorElemento_Valores.IsChecked;

                    Condicion.CantidadTextosInformacion_SoloCadenasCumplen = (bool)opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
                    Condicion.CantidadTextosInformacion_SoloCadenasCumplen_Valores = (bool)opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked;

                    Condicion.CantidadNumeros_PorElemento = (bool)opcionCantidadNumeros_PorElemento.IsChecked;
                    Condicion.CantidadNumeros_PorElemento_Valores = (bool)opcionCantidadNumeros_PorElemento_Valores.IsChecked;

                    Condicion.Busqueda_TextoBusqueda = Busqueda_TextoBusqueda;
                    Condicion.BuscarCualquierTextoInformacion_TextoBusqueda = (bool)opcionCualquierTextoInformacion.IsChecked;

                    Condicion.Valores_Condicion = EstablecerPosicion_SiCorresponde(valoresRelacionadosCondicion.Text, true);
                    Condicion.ConsiderarCantidades_OperandoValores_AsignacionLogicaImplicaciones = (bool)opcionConsiderarOperandoValores_CantidadesAsignacion_LogicaImplicaciones.IsChecked;
                    Condicion.ConsiderarCantidades_OperandoCondicion_AsignacionLogicaImplicaciones = (bool)opcionConsiderarOperandoCondicion_CantidadesAsignacion_LogicaImplicaciones.IsChecked;

                    Condicion.ModoSeleccionEntradas = ModoSeleccionEntradas;

                    Aceptar = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ingresa una cantidad válida que se deba cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private string EstablecerPosicion_SiCorresponde(string valor, bool guardar)
        {
            
            if ((subelementoElementoRelacionadoCondicion.Visibility == Visibility.Visible && (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion.SelectedItem).Uid) == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento) ||

                (opcionCondicion_TextosInformacion.Visibility == Visibility.Visible &&
                opcionCondicion_TextosInformacion.SelectedItem != null &&
                ((TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoDistinto |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoIgual |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorIgualQue |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorQue |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorIgualQue |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorQue)) ||

                (subelementoElementoRelacionadoCondicion_Valores.Visibility == Visibility.Visible &&
                subelementoElementoRelacionadoCondicion_Valores.SelectedItem != null &&
                (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion_Valores.SelectedItem).Uid) == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento)
                )
            {
                int posicion = 0;
                if (int.TryParse(valor, out posicion))
                {
                    if (guardar)
                        posicion++;
                    else
                        posicion--;

                    return posicion.ToString();
                }
            }

            return valor;
        }
        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void quitarSeleccion_opcionOperandoSubElementoCondicion_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElementoCondicion_TextosInformacion.SelectedItem = null;
        }

        private void quitarSeleccion_opcionOperandoSubElemento_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElemento_TextosInformacion.SelectedItem = null;
        }

        private void opcionCantidadNumerosCumplenCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadNumerosCumplenCondicion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility = Visibility.Visible;
                    cantidadNumerosCumplenCondicion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility = Visibility.Collapsed;
                    cantidadNumerosCumplenCondicion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadNumerosCumplenCondicion_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numero = 0;
            int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero);
            if (Condicion != null) Condicion.CantidadNumerosCumplenCondicion = numero;
        }

        private void opcionSeleccionNumerosElementoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento.Visibility = Visibility.Visible;
                    opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_SelectionChanged(this, e);
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionSeleccionNumerosElementoCondicion_Valores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Visible;
                    opcionCantidadSubNumerosCumplenCondicion_Valores_SelectionChanged(this, e);
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    cantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_Valores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    //opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    //opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if(opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacionImplicacionInstancia_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacionImplicacionInstancia.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacionImplicacionCondicion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacionImplicacionCondicion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacionImplicacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacionImplicacion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCondicion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCondicion_TextosInformacion.SelectedIndex >= 5)
                {
                    textoOpcionTipoElementoCompararTextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoElementoCompararTextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionTipoElementoCompararTextosInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoElementoCompararTextosInformacion.Visibility = Visibility.Collapsed;
                }

                if (opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "13" select I).FirstOrDefault())
                {
                    textoContenedorTextoBusqueda.Visibility = Visibility.Visible;
                    contenedorTextoBusqueda.Visibility = Visibility.Visible;
                    opcionCualquierTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    textoContenedorTextoBusqueda.Visibility = Visibility.Collapsed;
                    contenedorTextoBusqueda.Visibility = Visibility.Collapsed;
                    opcionCualquierTextoInformacion.Visibility = Visibility.Collapsed;
                }

                MostrarOcultarOpciones_CadenasTexto();
            }
        }

        private void MostrarOcultarOpciones_CadenasTexto()
        {
            if (opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "16" select I).FirstOrDefault() |
                    opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "17" select I).FirstOrDefault() |
                    opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "18" select I).FirstOrDefault() |
                    opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "19" select I).FirstOrDefault() |
                    opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "20" select I).FirstOrDefault() |
                    opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "21" select I).FirstOrDefault())
            {
                opcionTextosInformacionFijos.Visibility = Visibility.Collapsed;
                opcionCualquierTextoInformacion.Visibility = Visibility.Collapsed;
                opcionCualquierTextoInformacion.Visibility = Visibility.Collapsed;
                opcionTextosInformacion_ElementoOperacion.Visibility = Visibility.Collapsed;
                opcionTextosInformacion_ElementoEntrada.Visibility = Visibility.Collapsed;
                opcionTextosInformacion_TextosInformacion_CumplenCondicion.Visibility = Visibility.Collapsed;
                opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion.Visibility = Visibility.Collapsed;
                opcionTextosInformacion_DefinicionListas.Visibility = Visibility.Collapsed;
                revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Collapsed;

                tiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;
                textoValores.Visibility = Visibility.Collapsed;
                valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                textoTiposValoresCondicion_TextosInformacion.Visibility = Visibility.Collapsed;

                opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
            }
            else
            {
                opcionTextosInformacionFijos.Visibility = Visibility.Visible;
                opcionCualquierTextoInformacion.Visibility = Visibility.Visible;
                opcionCualquierTextoInformacion.Visibility = Visibility.Visible;
                opcionTextosInformacion_ElementoOperacion.Visibility = Visibility.Visible;
                opcionTextosInformacion_ElementoEntrada.Visibility = Visibility.Visible;
                opcionTextosInformacion_TextosInformacion_CumplenCondicion.Visibility = Visibility.Visible;
                opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion.Visibility = Visibility.Visible;
                opcionTextosInformacion_DefinicionListas.Visibility = Visibility.Visible;
                revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Visible;

                tiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;

                if (opcionTextosInformacionFijos.IsChecked == true)
                {
                    textoValores.Visibility = Visibility.Visible;
                    valoresRelacionadosCondicion.Visibility = Visibility.Visible;
                }
                else
                {
                    textoValores.Visibility = Visibility.Collapsed;
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                }
                
                textoTiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;

                if (opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Collapsed)
                {
                    opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
                }
            }
        }

        private void opcionTextosInformacion_ElementoEntrada_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                if(opcionTextosInformacion_ElementoEntrada.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Visible;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;

                    opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionTextosInformacion_ElementoEntrada_UnChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {               
                //valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                //textoValores.Visibility = Visibility.Collapsed;

                opcionEntrada_TextosInformacion_Valores.Visibility = Visibility.Collapsed;
                
            }
        }

        private void conectorCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (conectorCondicion.SelectedItem == (from ComboBoxItem I in conectorCondicion.Items where I.Uid == "3" select I).FirstOrDefault())
            {
                conectorO_Excluyente.Visibility = Visibility.Visible;
            }
            else
            {
                conectorO_Excluyente.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionConsiderarOperandoCondicion_SiCumple_Checked(object sender, RoutedEventArgs e)
        {
            opcionConsiderarCondicionesHijas.Visibility = Visibility.Visible;
        }

        private void opcionConsiderarOperandoCondicion_SiCumple_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionConsiderarCondicionesHijas.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacion_TextosInformacion_CumplenCondicion_Checked(object sender, RoutedEventArgs e)
        {
            opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Visible;

            if (IsLoaded)
            {
                if (opcionTextosInformacion_TextosInformacion_CumplenCondicion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion_Checked(object sender, RoutedEventArgs e)
        {
            opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Visible;

            if (IsLoaded)
            {
                if (opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacion_TextosInformacion_CumplenCondicion_Unchecked(object sender, RoutedEventArgs e)
        {
            if(opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion.IsChecked == false)
                opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacion_TodosTextosInformacion_CumplenCondicion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (opcionTextosInformacion_TextosInformacion_CumplenCondicion.IsChecked == false)
                opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Collapsed;
        }

        private void opcionVaciarListaTextosInformacion_CumplenCondicion_Checked(object sender, RoutedEventArgs e)
        {
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.Visibility = Visibility.Visible;
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.Visibility = Visibility.Visible;
        }

        private void opcionVaciarListaTextosInformacion_CumplenCondicion_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.Visibility = Visibility.Collapsed;
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.Visibility = Visibility.Collapsed;
        }

        private void opcionTextosInformacionCumplenCondicionImplicacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacionCumplenCondicionImplicacion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void elementoRelacionadoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elementoRelacionadoCondicion.SelectedItem != null &&
                ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ID != "0")
            {
                opcionOperandoSubElementoCondicion.DisplayMemberPath = "NombreCombo";
                opcionOperandoSubElementoCondicion.SelectedValuePath = "NombreCombo";
                opcionOperandoSubElementoCondicion.ItemsSource = ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ElementosDiseñoOperacion.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);
            }
        }

        private void quitarSeleccion_opcionOperandoSubElementoCondicion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElementoCondicion.SelectedItem = null;
        }

        private void opcionSeleccionNumerosElementoCondicion_OperacionEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionCantidadNumeros_PorElemento.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                    opcionCantidadNumeros_PorElemento.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionOperandoCondicion_Elemento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (opcionOperandoCondicion_Elemento.SelectedItem != null &&
                ((DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem).ID != "0")
            {
                opcionOperandoSubElementoCondicion_Elemento.DisplayMemberPath = "NombreCombo";
                opcionOperandoSubElementoCondicion_Elemento.SelectedValuePath = "NombreCombo";
                opcionOperandoSubElementoCondicion_Elemento.ItemsSource = ((DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem).ElementosDiseñoOperacion.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);
            }
        }

        private void quitarSeleccion_opcionOperandoSubElementoCondicion_Elemento_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElementoCondicion_Elemento.SelectedItem = null;
        }

        private void opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_OperacionEntrada.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionCantidadNumeros_PorElemento_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Collapsed;
                    opcionCantidadNumeros_PorElemento_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_OperacionEntrada.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_Valores_OperacionEntrada.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void GenerarBusqueda_TextoBusqueda()
        {
            if(Busqueda_TextoBusqueda == null)
            {
                Busqueda_TextoBusqueda = new BusquedaTextoArchivo() { Nombre = "Búsqueda entre las cadenas de texto", FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo };
            }

            textoBusqueda.Busqueda = Busqueda_TextoBusqueda;

            if (Busqueda_TextoBusqueda.TextoBusquedaNumero != null)
            {
                textoBusqueda.textoArchivo.Focus();
                textoBusqueda.textoArchivo.Text = Busqueda_TextoBusqueda.TextoBusquedaNumero.Replace(textoBusqueda.ObtenerCadenaFormatoNumeroGuardar(), textoBusqueda.ObtenerCadenaFormatoNumero());
                textoBusqueda.textoArchivo.Text = textoBusqueda.textoArchivo.Text.Replace(textoBusqueda.ObtenerCadenaFormatoDatosGuardar(), textoBusqueda.ObtenerCadenaFormatoNumero());
                textoBusqueda.textoArchivo.Text = textoBusqueda.textoArchivo.Text.Replace(textoBusqueda.ObtenerCadenaFormatoTextosGuardar(), textoBusqueda.ObtenerCadenaFormatoNumero());
            }
            textoBusqueda.txtVeces.Text = Busqueda_TextoBusqueda.NumeroVecesBusquedaNumero.ToString();
            textoBusqueda.nombreBusqueda.Text = Busqueda_TextoBusqueda.Nombre;

            textoBusqueda.opcionAsignarTextosInformacionNumeros.SelectedItem = (from ComboBoxItem I in textoBusqueda.opcionAsignarTextosInformacionNumeros.Items where I.Uid == ((int)Busqueda_TextoBusqueda.OpcionAsignarTextosInformacion_Numeros).ToString() select I).FirstOrDefault();
            textoBusqueda.opcionAsignarTextosInformacionNumeros_Iteraciones.SelectedItem = (from ComboBoxItem I in textoBusqueda.opcionAsignarTextosInformacionNumeros_Iteraciones.Items where I.Uid == ((int)Busqueda_TextoBusqueda.OpcionAsignarTextosInformacion_Numeros_Iteraciones).ToString() select I).FirstOrDefault();

            textoBusqueda.ModoTextosInformacion = true;
            textoBusqueda.ModoBusqueda = true;
        }

        private void opcionCualquierTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCualquierTextoInformacion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void subelementoElementoRelacionadoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsLoaded)
            {
                if (subelementoElementoRelacionadoCondicion.SelectedItem != null && 
                    (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion.SelectedItem).Uid) == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento &&
                    (opcionValoresFijos.IsChecked == true ||
                    (opcionValoresFijos.IsChecked == false &&
                    opcionValores_ElementoOperacion.IsChecked == false)))
                {
                    opcionesValoresPosiciones.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionesValoresPosiciones.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionConsiderarTextosInformacionComoCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionCondicion_OperacionEntrada_TextosInformacion.Visibility = Visibility.Visible;
            }
        }

        private void opcionConsiderarTextosInformacionComoCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionCondicion_OperacionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionValorPosicion_Checked(object sender, RoutedEventArgs e)
        {
            valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
            textoValores.Visibility = Visibility.Collapsed;
            subelementoElementoRelacionadoCondicion_Valores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion_Valores.Items where I.Uid == ((int)TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento).ToString() select I).FirstOrDefault();
            opcionCantidadNumeros_PorElemento_Valores.IsChecked = true;
        }

        private void opcionTextosInformacion_ElementoOperacion_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Collapsed;
            }
        }

        private void revertirOperandosCondicionValores_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (opcionOperandoCondicion_TextosInformacion.SelectedItem != null &&
                opcionElementoOperacion_TextosInformacion.SelectedItem != null &&
                ((DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem).ID != "0" &&
                ((DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem).ID != "0")
            {
                DiseñoOperacion operandoCondicion = (DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem;
                opcionOperandoCondicion_TextosInformacion.SelectedItem = opcionElementoOperacion_TextosInformacion.SelectedItem;

                opcionElementoOperacion_TextosInformacion.SelectedItem = operandoCondicion;
            }
        }

        private void revertirOperandosCondicionValores_Click(object sender, RoutedEventArgs e)
        {
            if (elementoRelacionadoCondicion.SelectedItem != null &&
                opcionOperandoCondicion_Elemento.SelectedItem != null &&
                ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ID != "0" &&
                ((DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem).ID != "0")
            {
                DiseñoOperacion operandoCondicion = (DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem;
                elementoRelacionadoCondicion.SelectedItem = opcionOperandoCondicion_Elemento.SelectedItem;

                opcionOperandoCondicion_Elemento.SelectedItem = operandoCondicion;
            }
        }

        private void opcionTextosInformacion_DefinicionListas_Checked(object sender, RoutedEventArgs e)
        {
            opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Collapsed;

            if (IsLoaded)
            {
                if (opcionTextosInformacion_DefinicionListas.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandosValoresCondicion.Visibility = Visibility.Collapsed;
                    textoValores.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Visible;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionTextosInformacion_DefinicionListas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (opcionTextosInformacion_DefinicionListas.IsChecked == false)
                opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Collapsed;
        }

        private void opcionSeleccionNumerosElementoCondicion_Valores_Listas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                //if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando)
                //{
                //    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                //    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                //    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Visible;
                //    opcionCantidadSubNumerosCumplenCondicion_Valores_SelectionChanged(this, e);
                //}
                //else
                //{
                //    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                //    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                //    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Collapsed;
                //}
            }
        }

        private void opcionCantidadTextosInformacion_SoloCadenasCumplen_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Collapsed)
                {
                    opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
                }
            }
        }
    }
}
