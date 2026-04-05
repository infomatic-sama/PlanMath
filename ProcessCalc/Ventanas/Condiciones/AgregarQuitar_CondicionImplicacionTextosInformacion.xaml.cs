using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para AgregarQuitar_CondicionImplicacionTextosInformacion.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionImplicacionTextosInformacion : Window
    {
        public bool Aceptar { get; set; }
        public bool ModoEdicion { get; set; }
        public CondicionImplicacionTextosInformacion Condicion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<Entrada> ListaEntradas { get; set; }
        public List<DiseñoOperacion> ListaOperandos { get; set; }
        public List<DiseñoElementoOperacion> ListaSubOperandos { get; set; }
        public List<DiseñoTextosInformacion> ListaDefiniciones { get; set; }
        public List<DiseñoListaCadenasTexto> ListaDefinicionesListas { get; set; }
        BusquedaTextoArchivo Busqueda_TextoBusqueda = new BusquedaTextoArchivo() { Nombre = "Búsqueda entre los textos de información", FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo };
        public bool ModoProcesamientoCantidades { get; set; }
        public AgregarQuitar_CondicionImplicacionTextosInformacion()
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
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    
                    if (opcionTextosInformacionFijos.IsChecked == true)
                        opcionTextosInformacionFijos_Checked(this, e);
                    else if (opcionTextosInformacion_Entrada.IsChecked == true)
                        opcionTextosInformacion_Entrada_Checked(this, e);
                    else if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                        opcionTextosInformacion_ElementoOperacion_Checked(this, e);
                    else if (opcionTextosInformacion_Definicion.IsChecked == true)
                        opcionTextosInformacion_Definicion_Checked(this, e);

                    if(opcionConsiderarTextosInformacionComoCantidades.IsChecked == true)
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
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;

                    if (opcionValoresFijos.IsChecked == true)
                        opcionValoresFijos_Checked(this, e);
                    else if (opcionValores_ElementoOperacion.IsChecked == true)
                        opcionValores_ElementoOperacion_Checked(this, e);
                }
            }
        }

        private void opcionValoresFijos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionValoresFijos.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion_Valores.Items where I.Uid == ((int)TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.NumerosElemento).ToString() select I).FirstOrDefault();

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

                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Visible;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Visible;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Visible;
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    revertirOperandosCondicionValores.Visibility = Visibility.Visible;
                }
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
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
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
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;                    
                }
            }
        }
        private void opcionTextosInformacion_Entrada_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_Entrada.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Visible;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Visible;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;                    
                }
            }
        }
        private void opcionTextosInformacion_Definicion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_Definicion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void opcionTextosInformacion_ElementoOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Visible;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Visible;

                    opcionCondicion_TextosInformacion_SelectionChanged(this, null);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ListaElementosComboOperandos = new ArrayList();

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

            if(ListaElementos != null)
                elementoRelacionadoCondicion.ItemsSource = ListaElementosComboOperandos;
            
            opcionEntrada_TextosInformacion.DisplayMemberPath = "Nombre";
            opcionEntrada_TextosInformacion.ItemsSource = ListaEntradas;

            opcionElementoOperacion_TextosInformacion.DisplayMemberPath = "NombreCombo";
            opcionElementoOperacion_TextosInformacion.SelectedValuePath = "NombreCombo";

            opcionElementoOperacion_TextosInformacion.ItemsSource = ListaElementosComboOperandos;

            opcionDefinicion_TextosInformacion.DisplayMemberPath = "Nombre";
            opcionDefinicion_TextosInformacion.ItemsSource = ListaDefiniciones;

            opcionDefinicion_ListasTextosInformacion.DisplayMemberPath = "Nombre";
            opcionDefinicion_ListasTextosInformacion.ItemsSource = ListaDefinicionesListas;

            var ListaElementosCombo = new ArrayList();

            if (ListaDefiniciones != null &&
                    ListaDefiniciones.Select(item => item.EntradaRelacionada).ToList().Any())
            {
                ListaElementosCombo.Add(new ComboBoxItem()
                {
                    Content = "Vector de cadenas de texto",
                    IsEnabled = false,
                });

                ListaElementosCombo.AddRange(ListaDefiniciones.Select(item => item.EntradaRelacionada).ToList());
            }

            ListaElementosCombo.Add(new DiseñoOperacion()
            {
                Nombre = "Operando actual de la ejecución (todos o cualquier operando)",
                ID = "0",
            });

            if (ListaOperandos != null &&
                ListaOperandos.Any())
            {
                ListaElementosCombo.Add(new ComboBoxItem()
                {
                    Content = "Variables o vectores operandos de esta operación",
                    IsEnabled = false,
                });

                ListaElementosCombo.AddRange(ListaOperandos.Where(item => !string.IsNullOrEmpty(item.NombreCombo)
                &&
                item.Tipo != TipoElementoOperacion.Salida).ToList());
            }

            if (ListaElementos != null &&
                ListaElementos.Any())
            {
                ListaElementosCombo.Add(new ComboBoxItem()
                {
                    Content = "Variables o vectores del cálculo",
                    IsEnabled = false,
                });

                ListaElementosCombo.AddRange(ListaElementos.Where(item => !string.IsNullOrEmpty(item.NombreCombo)
            &&
            item.Tipo != TipoElementoOperacion.Salida).ToList());
            }

            opcionOperandoCondicion_TextosInformacion.DisplayMemberPath = "NombreCombo";
            opcionOperandoCondicion_TextosInformacion.SelectedValuePath = "NombreCombo";

            opcionOperandoCondicion_TextosInformacion.ItemsSource = ListaElementosCombo;

            opcionOperandoSubElementoCondicion_TextosInformacion.DisplayMemberPath = "NombreCombo";
            opcionOperandoSubElementoCondicion_TextosInformacion.SelectedValuePath = "NombreCombo";

            if(ListaSubOperandos != null)
                opcionOperandoSubElementoCondicion_TextosInformacion.ItemsSource = ListaSubOperandos.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);
                        
            opcionOperandoCondicion_Elemento.DisplayMemberPath = "NombreCombo";
            opcionOperandoCondicion_Elemento.SelectedValuePath = "NombreCombo";

            opcionOperandoCondicion_Elemento.ItemsSource = ListaElementosComboOperandos;
            
            opcionOperandoSubElementoCondicion_Elemento.DisplayMemberPath = "NombreCombo";
            opcionOperandoSubElementoCondicion_Elemento.SelectedValuePath = "NombreCombo";

            if (ListaSubOperandos != null)
                opcionOperandoSubElementoCondicion_Elemento.ItemsSource = ListaSubOperandos.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);

            if (ModoEdicion)
            {
                if (Condicion.EsOperandoActual)
                {
                    elementoRelacionadoCondicion.SelectedItem = ListaElementosComboOperandos.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElementoCondicion.SelectedItem = null;
                }
                else
                {
                    if (Condicion.ElementoCondicion != null)
                    {
                        elementoRelacionadoCondicion.SelectedValue = Condicion.ElementoCondicion.NombreCombo;
                        elementoRelacionadoCondicion_SelectionChanged(this, null);
                    }

                    if (Condicion.OperandoSubElemento_Condicion != null)
                        opcionOperandoSubElementoCondicion.SelectedValue = Condicion.OperandoSubElemento_Condicion.NombreCombo;
                }

                if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                    opcionTipoElemento_TextosInformacion.IsChecked = true;
                else if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                    opcionTipoElemento_OperacionEntrada.IsChecked = true;
                                
                opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion).ToString() select I).FirstOrDefault();
                opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion_TextosInformacion).ToString() select I).FirstOrDefault();

                if (Condicion.EsOperandoTextosActual)
                {
                    opcionOperandoCondicion_TextosInformacion.SelectedItem = ListaElementosCombo.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElementoCondicion_TextosInformacion.SelectedItem = null;
                }
                else
                {
                    if (Condicion.OperandoCondicion != null)
                    {
                        opcionOperandoCondicion_TextosInformacion.SelectedItem = Condicion.OperandoCondicion;
                        opcionOperandoCondicion_TextosInformacion_SelectionChanged(this, null);
                    }
                    else if (Condicion.EntradaCondicion != null)
                        opcionOperandoCondicion_TextosInformacion.SelectedItem = Condicion.EntradaCondicion;

                    if (Condicion.OperandoSubElemento_Condicion_TextosInformacion != null)
                        opcionOperandoSubElementoCondicion_TextosInformacion.SelectedValue = Condicion.OperandoSubElemento_Condicion_TextosInformacion.NombreCombo;
                }

                OpcionOperandoCondicion_Clasificadores.IsChecked = Condicion.CadenasTextoSon_Clasificadores;
                OpcionOperandoValores_Clasificadores.IsChecked = Condicion.CadenasTextoSon_Clasificadores_Valores;

                opcionEntrada_TextosInformacion.SelectedItem = Condicion.ElementoEntrada_Valores;
                subelementoElementoRelacionadoCondicion.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion.Items where I.Uid == ((int)Condicion.TipoSubElemento_Condicion).ToString() select I).FirstOrDefault();

                if(Condicion.TipoSubElemento_Condicion_Valores != TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.Ninguno)
                    subelementoElementoRelacionadoCondicion_Valores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion_Valores.Items where I.Uid == ((int)Condicion.TipoSubElemento_Condicion_Valores).ToString() select I).FirstOrDefault();

                if (Condicion.EsOperandoValoresTextosActual)
                {
                    opcionElementoOperacion_TextosInformacion.SelectedItem = ListaElementosComboOperandos.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionSubElementoOperacion_TextosInformacion.SelectedItem = null;
                }
                else
                {
                    if (Condicion.ElementoOperacion_Valores != null)
                        opcionElementoOperacion_TextosInformacion.SelectedItem = Condicion.ElementoOperacion_Valores;

                    if (Condicion.SubElementoOperacion_Valores != null)
                        opcionSubElementoOperacion_TextosInformacion.SelectedValue = Condicion.SubElementoOperacion_Valores.NombreCombo;
                }

                if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos)
                    opcionTextosInformacionFijos.IsChecked = true;
                else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada)
                    opcionTextosInformacion_Entrada.IsChecked = true;
                else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion)
                    opcionTextosInformacion_ElementoOperacion.IsChecked = true;
                else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicion)
                    opcionTextosInformacion_Definicion.IsChecked = true;
                else if (Condicion.TipoTextosInformacion_Valores == TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista)
                    opcionTextosInformacion_DefinicionListas.IsChecked = true;

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

                    opcionValoresFijos_Unchecked(this, e);
                    opcionValores_ElementoOperacion_Unchecked(this, e);
                }

                opcionTipoElementoCompararTextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoElementoCompararTextosInformacion.Items where I.Uid == ((int)Condicion.TipoElementoComparar_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionConsiderarTextosInformacionComoCantidades.IsChecked = Condicion.ConsiderarTextosInformacionComoCantidades;
                                
                if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    if (opcionTextosInformacionFijos.IsChecked == true)
                        opcionTextosInformacionFijos_Checked(this, e);
                    else if (opcionTextosInformacion_Entrada.IsChecked == true)
                        opcionTextosInformacion_Entrada_Checked(this, e);
                    else if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                        opcionTextosInformacion_ElementoOperacion_Checked(this, e);
                    else if (opcionTextosInformacion_Definicion.IsChecked == true)
                        opcionTextosInformacion_Definicion_Checked(this, e);
                    else if (opcionTextosInformacion_DefinicionListas.IsChecked == true)
                        opcionTextosInformacion_DefinicionListas_Checked(this, e);

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

                opcionCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_ElementoOperacionEntrada).ToString() select I).FirstOrDefault();
                opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCondicion_OperacionEntrada_TextosInformacion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_ElementoOperacionEntrada).ToString() select I).FirstOrDefault();
                                
                opcionDefinicion_TextosInformacion.SelectedItem = Condicion.ElementoDefinicion_Valores;
                opcionDefinicion_ListasTextosInformacion.SelectedItem = Condicion.ElementoDefinicionListas_Valores;

                if(tiposValoresCondicion_TextosInformacion.Visibility == Visibility.Visible)
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores_Listas.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores).ToString() select I).FirstOrDefault();
                else
                    opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores).ToString() select I).FirstOrDefault();

                opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion).ToString() select I).FirstOrDefault();

                conectorCondicion.SelectedItem = (from ComboBoxItem I in conectorCondicion.Items where I.Uid == ((int)Condicion.TipoConector).ToString() select I).FirstOrDefault();

                conectorCondicion_SelectionChanged(this, null);
                conectorO_Excluyente.IsChecked = (bool)Condicion.ConectorO_Excluyente;

                opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_OperacionEntrada.Text = Condicion.CantidadSubNumerosCumplenCondicion_OperacionEntrada.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada;

                opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_TextosInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion;

                opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion;

                opcionCantidadNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                cantidadNumerosCumplenCondicion.Text = Condicion.CantidadNumerosCumplenCondicion.ToString();
                opcionSaldoCantidadNumerosCumplenCondicion.IsChecked = (bool)Condicion.OpcionSaldoCantidadNumerosCumplenCondicion;

                if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion)
                {
                    if (opcionTextosInformacion_Entrada.IsChecked == true)
                    {
                        opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                        opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                        cantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores.ToString();
                        opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion).ToString() select I).FirstOrDefault();
                        opcionSaldoCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;

                        opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion).ToString() select I).FirstOrDefault();
                        opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion).ToString() select I).FirstOrDefault();
                        cantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion.ToString();
                        opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion).ToString() select I).FirstOrDefault();
                        opcionSaldoCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion;

                    }
                    else
                    {
                        opcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                        opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                        cantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.ToString();
                        opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion).ToString() select I).FirstOrDefault();
                        opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion;
                    
                        if((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando)
                        {
                            opcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion).ToString() select I).FirstOrDefault();
                            opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion).ToString() select I).FirstOrDefault();
                            cantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion.ToString();
                            opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion).ToString() select I).FirstOrDefault();
                            opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.IsChecked = Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion;
                        }
                    }
                }
                else if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada)
                {
                    opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                    cantidadSubNumerosCumplenCondicion_Valores.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores.ToString();
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                    opcionSaldoCantidadSubNumerosCumplenCondicion_Valores.IsChecked = (bool)Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores;
                }

                if (Condicion.EsOperandoValoresActual)
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
                opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked = (bool)Condicion.CantidadTextosInformacion_SoloCadenasCumplen;

                if (opcionTextosInformacion_Entrada.IsChecked == true)
                {
                    opcionCantidadTextosInformacion_PorElemento_Valores_ElementoEntradaValores.IsChecked = (bool)Condicion.CantidadTextosInformacion_PorElemento_Valores;
                }
                else
                {
                    opcionCantidadTextosInformacion_PorElemento_Valores.IsChecked = (bool)Condicion.CantidadTextosInformacion_PorElemento_Valores;                    
                }

                opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = (bool)Condicion.CantidadTextosInformacion_SoloCadenasCumplen_Valores;

                opcionCantidadNumeros_PorElemento.IsChecked = (bool)Condicion.CantidadNumeros_PorElemento;
                opcionCantidadNumeros_PorElemento_Valores.IsChecked = (bool)Condicion.CantidadNumeros_PorElemento_Valores;

                if (opcionConsiderarOperandoCondicion_SiCumple.IsChecked == true)
                    opcionConsiderarCondicionesHijas.Visibility = Visibility.Visible;
                else
                    opcionConsiderarCondicionesHijas.Visibility = Visibility.Collapsed;

                opcionConsiderarCondicionesHijas.IsChecked = (bool)Condicion.ConsiderarIncluirCondicionesHijas;

                opcionCualquierTextoInformacion.IsChecked = (bool)Condicion.BuscarCualquierTextoInformacion_TextoBusqueda;
                opcionValoresProcesados_ProcesamientoCantidades.IsChecked = (bool)Condicion.ConsiderarValores_ProcesamientoCantidades;
                opcionValoresProcesados_SoloProcesamientoCantidades.IsChecked = (bool)Condicion.ConsiderarSoloValores_ProcesamientoCantidades;
                opcionValoresProcesados_ProcesamientoCantidades_Valores.IsChecked = (bool)Condicion.ConsiderarValores_ProcesamientoCantidades_Valores;
                opcionValoresProcesados_SoloProcesamientoCantidades_Valores.IsChecked = (bool)Condicion.ConsiderarSoloValores_ProcesamientoCantidades_Valores;

                valoresRelacionadosCondicion.Text = EstablecerPosicion_SiCorresponde(Condicion.Valores_Condicion, false);

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
                opcionConsiderarOperandoCondicion_SiCumple.IsChecked = true;
                opcionConsiderarCondicionesHijas.IsChecked = true;
            }

            if (ModoProcesamientoCantidades)
            {
                opcionValoresProcesados_ProcesamientoCantidades.Visibility = Visibility.Visible;
                opcionValoresProcesados_ProcesamientoCantidades_Valores.Visibility = Visibility.Visible;
                opcionValoresProcesados_SoloProcesamientoCantidades.Visibility = Visibility.Visible;
                opcionValoresProcesados_SoloProcesamientoCantidades_Valores.Visibility = Visibility.Visible;
            }

            GenerarBusqueda_TextoBusqueda();
            textoBusqueda.UserControl_Loaded(this, e);
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (opcionesElementos.Visibility == Visibility.Visible && elementoRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_TextosInformacion.Visibility == Visibility.Visible && opcionOperandoCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (subelementoElementoRelacionadoCondicion.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (subelementoElementoRelacionadoCondicion_Valores.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_Elemento_Valores.Visibility == Visibility.Visible && opcionOperandoCondicion_Elemento.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector de operación.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de cadenas de texto.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCondicion_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de variables o vectores de entrada o retornados.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion_OperacionEntrada_TextosInformacion.Visibility == Visibility.Visible && opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de variables o vectores de entrada o retornados.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionEntrada_TextosInformacion.Visibility == Visibility.Visible && opcionEntrada_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector de entrada.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionDefinicion_TextosInformacion.Visibility == Visibility.Visible && opcionDefinicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector de definición de lógica de asignación de cadenas de texto.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionDefinicion_ListasTextosInformacion.Visibility == Visibility.Visible && opcionDefinicion_ListasTextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una definición de listas de cadenas de texto.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_Elemento_TextosInformacion.Visibility == Visibility.Visible && opcionElementoOperacion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }            
            else if (opcionesCantidadSubNumerosCumplenCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de cadenas de texto de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }            
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (conectorCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de conector con la condición anterior.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility == Visibility.Visible && opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int numero = 0;

                if (int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero))
                {
                    if (!ModoEdicion)
                    {
                        Condicion = new CondicionImplicacionTextosInformacion();
                        Condicion.CantidadNumerosCumplenCondicion = numero;
                    }

                    if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                    {
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
                    }
                    else
                    {
                        Condicion.EsOperandoActual = false;
                    }

                    if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                    {
                        if (opcionOperandoCondicion_TextosInformacion.SelectedItem != null &&
                        ((DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem).ID != "0")
                        {
                            if (opcionOperandoCondicion_TextosInformacion.SelectedItem != null)
                            {
                                if (opcionOperandoCondicion_TextosInformacion.SelectedItem.GetType() == typeof(DiseñoOperacion))
                                {
                                    Condicion.OperandoCondicion = (DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem;
                                    Condicion.EntradaCondicion = null;
                                }
                                else if (opcionOperandoCondicion_TextosInformacion.SelectedItem.GetType() == typeof(Entrada))
                                {
                                    Condicion.OperandoCondicion = null;
                                    Condicion.EntradaCondicion = (Entrada)opcionOperandoCondicion_TextosInformacion.SelectedItem;
                                }
                            }

                            Condicion.OperandoSubElemento_Condicion_TextosInformacion = (DiseñoElementoOperacion)opcionOperandoSubElementoCondicion_TextosInformacion.SelectedItem;
                            Condicion.EsOperandoTextosActual = false;
                        }
                        else
                        {
                            Condicion.OperandoCondicion = null;
                            Condicion.EntradaCondicion = null;
                            Condicion.OperandoSubElemento_Condicion_TextosInformacion = null;
                            Condicion.EsOperandoTextosActual = true;
                        }
                    }
                    else
                    {
                        Condicion.EsOperandoTextosActual = false;
                    }

                    if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                        Condicion.TipoElementoCondicion = TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion;
                    else if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                        Condicion.TipoElementoCondicion = TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada;

                    Condicion.CadenasTextoSon_Clasificadores = (bool)OpcionOperandoCondicion_Clasificadores.IsChecked;
                    Condicion.CadenasTextoSon_Clasificadores_Valores = (bool)OpcionOperandoValores_Clasificadores.IsChecked;

                    Condicion.ConsiderarTextosInformacionComoCantidades = (bool)opcionConsiderarTextosInformacionComoCantidades.IsChecked;

                    if (opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_OperacionEntrada.SelectedItem).Uid);
                    if (opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion_TextosInformacion = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem).Uid);

                    if (opcionTextosInformacionFijos.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacionFijos;
                    else if (opcionTextosInformacion_Entrada.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeEntrada;
                    else if (opcionTextosInformacion_ElementoOperacion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeElementoOperacion;
                    else if (opcionTextosInformacion_Definicion.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicion;
                    else if (opcionTextosInformacion_DefinicionListas.IsChecked == true)
                        Condicion.TipoTextosInformacion_Valores = TipoOpcionTextosInformacion_ValoresCondicion_ImplicacionTextosInformacion.TextosInformacion_DesdeDefinicionLista;

                    if (opcionCondicion_TextosInformacion.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_TextosInformacion = (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid);
                    if (Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.TextosInformacion && 
                        Condicion.ConsiderarTextosInformacionComoCantidades && 
                        opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_ElementoOperacionEntrada = (TipoOpcion_CondicionTextosInformacion_Implicacion)int.Parse(((ComboBoxItem)opcionCondicion_OperacionEntrada_TextosInformacion.SelectedItem).Uid);
                    
                    if(Condicion.TipoElementoCondicion == TipoOpcionElemento_Condicion_ImplicacionTextosInformacion.OperacionEntrada &&
                        opcionCondicion_OperacionEntrada.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_ElementoOperacionEntrada = (TipoOpcion_CondicionTextosInformacion_Implicacion)int.Parse(((ComboBoxItem)opcionCondicion_OperacionEntrada.SelectedItem).Uid);

                    if (opcionTipoElementoCompararTextosInformacion.SelectedItem != null)
                        Condicion.TipoElementoComparar_TextosInformacion = (TipoOpcionElementoComparar_TextosInformacion)int.Parse(((ComboBoxItem)opcionTipoElementoCompararTextosInformacion.SelectedItem).Uid);

                    if (opcionEntrada_TextosInformacion.SelectedItem != null)
                        Condicion.ElementoEntrada_Valores = (Entrada)opcionEntrada_TextosInformacion.SelectedItem;

                    if (subelementoElementoRelacionadoCondicion.SelectedItem != null)
                        Condicion.TipoSubElemento_Condicion = (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion.SelectedItem).Uid);
                    if (subelementoElementoRelacionadoCondicion_Valores.SelectedItem != null)
                        Condicion.TipoSubElemento_Condicion_Valores = (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion_Valores.SelectedItem).Uid);

                    if (opcionElementoOperacion_TextosInformacion.SelectedItem != null && 
                        ((DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem).ID != "0")
                    {
                        if (opcionElementoOperacion_TextosInformacion.SelectedItem != null)
                        {
                            Condicion.ElementoOperacion_Valores = (DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem;
                            Condicion.SubElementoOperacion_Valores = (DiseñoElementoOperacion)opcionSubElementoOperacion_TextosInformacion.SelectedItem;
                        }

                        Condicion.EsOperandoValoresTextosActual = false;
                    }
                    else
                    {
                        Condicion.ElementoOperacion_Valores = null;
                        Condicion.SubElementoOperacion_Valores = null;
                        Condicion.EsOperandoValoresTextosActual = true;
                    }

                    if (opcionDefinicion_TextosInformacion.SelectedItem != null)
                        Condicion.ElementoDefinicion_Valores = (DiseñoTextosInformacion)opcionDefinicion_TextosInformacion.SelectedItem;

                    if (opcionDefinicion_ListasTextosInformacion.SelectedItem != null)
                        Condicion.ElementoDefinicionListas_Valores = (DiseñoListaCadenasTexto)opcionDefinicion_ListasTextosInformacion.SelectedItem;

                    if (opcionTipoElemento_TextosInformacion.IsChecked == true ||
                        (opcionValoresFijos.IsChecked == true |
                        opcionValores_ElementoOperacion.IsChecked == true))
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

                    if (opcionOperandoCondicion_Elemento.SelectedItem != null && 
                        ((DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem).ID != "0")
                    {
                        if (opcionOperandoCondicion_Elemento.SelectedItem != null)
                            Condicion.ElementoOperacion_Valores_ElementoAsociado = (DiseñoOperacion)opcionOperandoCondicion_Elemento.SelectedItem;

                        Condicion.OperandoSubElemento_Condicion_Elemento = (DiseñoElementoOperacion)opcionOperandoSubElementoCondicion_Elemento.SelectedItem;
                        Condicion.EsOperandoValoresActual = false;
                    }
                    else
                    {
                        Condicion.ElementoOperacion_Valores_ElementoAsociado = null;
                        Condicion.OperandoSubElemento_Condicion_Elemento = null;
                        Condicion.EsOperandoValoresActual = true;
                    }
                                        
                    if (opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem != null &&
                        tiposValoresCondicion_TextosInformacion.Visibility == Visibility.Visible)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_Listas.SelectedItem).Uid);
                    else if (opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid);

                    if (opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid);
                    if (opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElementoEntrada_Condicion_Valores_TextosInformacion = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem).Uid);
                                        
                    Condicion.TipoConector = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)conectorCondicion.SelectedItem).Uid);

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_OperacionEntrada = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_OperacionEntrada = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_OperacionEntrada.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_OperacionEntrada.IsChecked;

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_TextosInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion.IsChecked;

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.IsChecked;

                    int numero2 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_TextosInformacion.Text, out numero2);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion = numero2;

                    if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                    {
                        if (opcionTextosInformacion_Entrada.IsChecked == true)
                        {
                            int numero8 = 0;
                            int.TryParse(cantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Text, out numero8);
                            if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores = numero8;
                        }
                    }
                    else if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                    {
                        int numero3 = 0;
                        int.TryParse(cantidadSubNumerosCumplenCondicion_Valores.Text, out numero3);
                        if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores = numero3;
                    }

                    int numero4 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_OperacionEntrada.Text, out numero4);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_OperacionEntrada = numero4;

                    int numero5 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Text, out numero5);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = numero5;

                    int numero6 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Text, out numero6);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion = numero6;

                    if (opcionTextosInformacion_Entrada.IsChecked == true)
                    {
                        int numero9 = 0;
                        int.TryParse(cantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Text, out numero9);
                        if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = numero9;
                    }
                    else
                    {
                        int numero7 = 0;
                        int.TryParse(cantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Text, out numero7);
                        if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = numero7;
                    }

                    if (opcionTipoElemento_TextosInformacion.IsChecked == true)
                    {
                        if (opcionTextosInformacion_Entrada.IsChecked == true)
                        {
                            Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.IsChecked;

                            Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.IsChecked;
                        }
                        else
                        {                            
                            Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedItem).Uid);
                            Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.IsChecked;

                            if (Condicion.OpcionSeleccionNumerosElemento_Condicion_Valores_TextosInformacion == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando)
                            {
                                Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedItem).Uid);
                                Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedItem).Uid);
                                Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextosInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedItem).Uid);
                                Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores_CantidadTextoInformacion = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.IsChecked;
                            }
                        }
                    }
                    else if (opcionTipoElemento_OperacionEntrada.IsChecked == true)
                    {
                        Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                        Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                        Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                        Condicion.OpcionSaldoCantidadSubNumerosCumplenCondicion_Valores = (bool)opcionSaldoCantidadSubNumerosCumplenCondicion_Valores.IsChecked;
                    }
                    
                    Condicion.OpcionCantidadNumerosCumplenCondicion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionSaldoCantidadNumerosCumplenCondicion = (bool)opcionSaldoCantidadNumerosCumplenCondicion.IsChecked;

                    Condicion.IncluirNombreElementoConTextos = (bool)opcionIncluirNombreElemento.IsChecked;
                    Condicion.IncluirSoloNombreElemento = (bool)opcionIncluirSoloNombreElemento.IsChecked;

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
                    Condicion.CantidadTextosInformacion_SoloCadenasCumplen = (bool)opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;

                    if (opcionTextosInformacion_Entrada.IsChecked == true)
                        Condicion.CantidadTextosInformacion_PorElemento_Valores = (bool)opcionCantidadTextosInformacion_PorElemento_Valores_ElementoEntradaValores.IsChecked;
                    else
                        Condicion.CantidadTextosInformacion_PorElemento_Valores = (bool)opcionCantidadTextosInformacion_PorElemento_Valores.IsChecked;

                    Condicion.CantidadTextosInformacion_SoloCadenasCumplen_Valores = (bool)opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked;

                    Condicion.CantidadNumeros_PorElemento = (bool)opcionCantidadNumeros_PorElemento.IsChecked;
                    Condicion.CantidadNumeros_PorElemento_Valores = (bool)opcionCantidadNumeros_PorElemento_Valores.IsChecked;

                    Condicion.Busqueda_TextoBusqueda = Busqueda_TextoBusqueda;
                    Condicion.BuscarCualquierTextoInformacion_TextoBusqueda = (bool)opcionCualquierTextoInformacion.IsChecked;
                    Condicion.ConsiderarSoloValores_ProcesamientoCantidades = (bool)opcionValoresProcesados_SoloProcesamientoCantidades.IsChecked;
                    Condicion.ConsiderarValores_ProcesamientoCantidades = (bool)opcionValoresProcesados_ProcesamientoCantidades.IsChecked;
                    Condicion.ConsiderarSoloValores_ProcesamientoCantidades_Valores = (bool)opcionValoresProcesados_SoloProcesamientoCantidades_Valores.IsChecked;
                    Condicion.ConsiderarValores_ProcesamientoCantidades_Valores = (bool)opcionValoresProcesados_ProcesamientoCantidades_Valores.IsChecked;

                    Condicion.Valores_Condicion = EstablecerPosicion_SiCorresponde(valoresRelacionadosCondicion.Text, true);

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

                (opcionCondicion_TextosInformacion.Visibility == Visibility.Visible && ((TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoDistinto |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoIgual |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorIgualQue |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMayorQue |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorIgualQue |
                (TipoOpcionImplicacion_AsignacionTextoInformacion)int.Parse(((ComboBoxItem)opcionCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionImplicacion_AsignacionTextoInformacion.PosicionTextoMenorQue)) ||

                (subelementoElementoRelacionadoCondicion_Valores.Visibility == Visibility.Visible && (TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion_Valores.SelectedItem).Uid) == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento)

                )
            {
                int posicion = 0;
                if(int.TryParse(valor, out posicion))
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

        private void quitarSeleccion_opcionOperandoSubElementoCondicion_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElementoCondicion_TextosInformacion.SelectedItem = null;
        }

        private void quitarSeleccion_opcionOperandoSubElementoCondicion_Elemento_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElementoCondicion_Elemento.SelectedItem = null;
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

        private void opcionOperandoCondicion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (opcionOperandoCondicion_TextosInformacion.SelectedItem != null &&
                opcionOperandoCondicion_TextosInformacion.SelectedItem.GetType() == typeof(DiseñoOperacion) &&
                ((DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem).ID != "0")
            {
                opcionOperandoSubElementoCondicion_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionOperandoSubElementoCondicion_TextosInformacion.SelectedValuePath = "NombreCombo";
                opcionOperandoSubElementoCondicion_TextosInformacion.ItemsSource = ((DiseñoOperacion)opcionOperandoCondicion_TextosInformacion.SelectedItem).ElementosDiseñoOperacion.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);
            }
            else
            {
                opcionOperandoSubElementoCondicion_TextosInformacion.DisplayMemberPath = "";
                opcionOperandoSubElementoCondicion_TextosInformacion.SelectedValuePath = "";
                opcionOperandoSubElementoCondicion_TextosInformacion.ItemsSource = null;
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

        private void opcionElementoOperacion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (opcionElementoOperacion_TextosInformacion.SelectedItem != null &&
                ((DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem).ID != "0")
            {
                opcionSubElementoOperacion_TextosInformacion.DisplayMemberPath = "NombreCombo";
                opcionSubElementoOperacion_TextosInformacion.SelectedValuePath = "NombreCombo";
                opcionSubElementoOperacion_TextosInformacion.ItemsSource = ((DiseñoOperacion)opcionElementoOperacion_TextosInformacion.SelectedItem).ElementosDiseñoOperacion.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                item.Tipo != TipoElementoDiseñoOperacion.Nota);
            }            
        }

        private void quitarSeleccion_opcionSubElementoOperacion_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionSubElementoOperacion_TextosInformacion.SelectedItem = null;
        }

        private void opcionSeleccionNumerosElementoCondicion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento.Visibility = Visibility.Collapsed;
                }
            }
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
                    opcionCantidadNumeros_PorElemento_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionCantidadNumeros_PorElemento_Valores.Visibility = Visibility.Collapsed;
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

        private void cantidadSubNumerosCumplenCondicion_TextosInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
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

        private void cantidadSubNumerosCumplenCondicion_OperacionEntrada_TextChanged(object sender, TextChangedEventArgs e)
        {
            
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
                    cantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadSubNumerosCumplenCondicion_Valores_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void opcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadSubNumerosCumplenCondicion_Valores_TextosInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
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
                    cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
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

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_TextosInformacion_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
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

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_TextosInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCondicion_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsLoaded)
            {
                if(opcionCondicion_TextosInformacion.SelectedIndex >= 5)
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
            if (opcionCondicion_TextosInformacion.Visibility == Visibility.Visible)
            {
                if (opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "16" select I).FirstOrDefault() |
                opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "17" select I).FirstOrDefault() |
                opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "18" select I).FirstOrDefault() |
                opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "19" select I).FirstOrDefault() |
                opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "20" select I).FirstOrDefault() |
                opcionCondicion_TextosInformacion.SelectedItem == (from ComboBoxItem I in opcionCondicion_TextosInformacion.Items where I.Uid == "21" select I).FirstOrDefault())
                {
                    opcionTextosInformacionFijos.Visibility = Visibility.Collapsed;
                    opcionTextosInformacion_Entrada.Visibility = Visibility.Collapsed;
                    opcionCualquierTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionTextosInformacion_ElementoOperacion.Visibility = Visibility.Collapsed;
                    opcionTextosInformacion_Definicion.Visibility = Visibility.Collapsed;
                    opcionTextosInformacion_DefinicionListas.Visibility = Visibility.Collapsed;
                    revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Collapsed;

                    tiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;

                    opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
                }
                else
                {
                    opcionTextosInformacionFijos.Visibility = Visibility.Visible;
                    opcionTextosInformacion_Entrada.Visibility = Visibility.Visible;
                    opcionCualquierTextoInformacion.Visibility = Visibility.Visible;
                    opcionTextosInformacion_ElementoOperacion.Visibility = Visibility.Visible;
                    opcionTextosInformacion_Definicion.Visibility = Visibility.Visible;
                    opcionTextosInformacion_DefinicionListas.Visibility = Visibility.Visible;
                    revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Visible;

                    tiposValoresCondicion_TextosInformacion.Visibility = Visibility.Visible;

                    if (opcionTextosInformacionFijos.IsChecked == true)
                    {
                        textoValoresRelacionadosCondicion.Visibility = Visibility.Visible;
                        valoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                        valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    }
                    
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;

                    if(opcionesCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility == Visibility.Collapsed)
                    {
                        opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
                    }
                }
            }
        }

        private void opcionSeleccionNumerosElementoEntradaValores_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando_PosicionActual |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoEntradaValores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.TodosNumerosOperando_PosicionActual)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento_Valores_ElementoEntradaValores.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_ElementoEntradaValores_TextosInformacion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento_Valores_ElementoEntradaValores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_ElementoEntradaValores__TextosInformacion.Visibility = Visibility.Collapsed;
                }
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

        private void opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando ||
                    ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionActualEjecucion |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionSiguienteDeActualEjecucion |
                    (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.PosicionAnteriorDeActualEjecucion))
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                }

                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores_TextosInformacion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_Valores_Cantidades_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void GenerarBusqueda_TextoBusqueda()
        {
            if (Busqueda_TextoBusqueda == null)
            {
                Busqueda_TextoBusqueda = new BusquedaTextoArchivo() { Nombre = "Búsqueda entre los textos de información", FinalizacionBusqueda = OpcionFinBusquedaTexto_Archivos.EncontrarHastaFinalArchivo };
            }

            textoBusqueda.Busqueda = Busqueda_TextoBusqueda;

            if (Busqueda_TextoBusqueda.TextoBusquedaNumero != null)
            {
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
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCualquierTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCualquierTextoInformacion.IsChecked == false)
                {
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Visible;
                }
            }
        }

        private void subelementoElementoRelacionadoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion.SelectedItem).Uid) == TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento &&
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

        private void opcionValores_ElementoOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                subelementoElementoRelacionadoCondicion_SelectionChanged(this, null);

                opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                revertirOperandosCondicionValores.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionValoresFijos_Unchecked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
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
            textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
            subelementoElementoRelacionadoCondicion_Valores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion_Valores.Items where I.Uid == ((int)TipoSubElemento_EvaluacionCondicion_ImplicacionTextosInformacion.PosicionesNumerosElemento).ToString() select I).FirstOrDefault();
            opcionCantidadNumeros_PorElemento_Valores.IsChecked = true;
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

        private void opcionTextosInformacion_ElementoOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                revertirOperandosCondicionValores_TextosInformacion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionValoresProcesados_ProcesamientoCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                opcionValoresProcesados_SoloProcesamientoCantidades.IsChecked = false;
            }
        }

        private void opcionValoresProcesados_SoloProcesamientoCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionValoresProcesados_ProcesamientoCantidades.IsChecked = false;
            }
        }

        private void opcionValoresProcesados_ProcesamientoCantidades_Valores_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                opcionValoresProcesados_SoloProcesamientoCantidades_Valores.IsChecked = false;
            }
        }

        private void opcionValoresProcesados_SoloProcesamientoCantidades_Valores_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionValoresProcesados_ProcesamientoCantidades_Valores.IsChecked = false;
            }
        }

        private void opcionTextosInformacion_DefinicionListas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_DefinicionListas.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Visible;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacionFijos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacionFijos.IsChecked == false)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacion_Entrada_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_Entrada.IsChecked == false)
                {
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacion_Definicion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_Definicion.IsChecked == false)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTextosInformacion_DefinicionListas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTextosInformacion_DefinicionListas.IsChecked == false)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    textoValoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Elemento_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionEntrada_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionesEntrada_Valores_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_TextosInformacion.Visibility = Visibility.Collapsed;
                    opcionDefinicion_ListasTextosInformacion.Visibility = Visibility.Collapsed;
                    textoOpcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                    opcionSeleccionNumerosElementoCondicion_Valores_Listas.Visibility = Visibility.Visible;
                    opcionesOperandos_Elemento_Valores.Visibility = Visibility.Collapsed;
                    textoSubelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                    subelementoElementoRelacionadoCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadTextosInformacion_SoloCadenasCumplen_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionesCantidadSubNumerosCumplenCondicion_Valores_TextosInformacion.Visibility == Visibility.Collapsed)
                {
                    opcionCantidadTextosInformacion_SoloCadenasCumplen_Valores.IsChecked = opcionCantidadTextosInformacion_SoloCadenasCumplen.IsChecked;
                }
            }
        }
    }
}
