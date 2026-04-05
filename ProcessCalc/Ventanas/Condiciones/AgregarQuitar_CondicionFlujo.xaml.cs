using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
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
    /// Lógica de interacción para AgregarQuitar_CondicionFlujo.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionFlujo : Window
    {
        public bool Aceptar { get; set; }
        public bool ModoEdicion { get; set; }
        public CondicionFlujo Condicion { get; set; }
        public List<DiseñoOperacion> ListaOperandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public AgregarQuitar_CondicionFlujo()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ListaElementosCombo = new ArrayList();

            ListaElementosCombo.Add(new ComboBoxItem()
            {
                Content = "Operandos de esta operación",
                IsEnabled = false,
            });

            ListaElementosCombo.Add(new DiseñoOperacion()
            {
                Nombre = "Operando actual de la ejecución (todos o cualquier operando)",
                ID = "0",
            });

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

            elementoRelacionadoCondicion.DisplayMemberPath = "NombreCombo";
            elementoRelacionadoCondicion.SelectedValuePath = "NombreCombo";
            elementoRelacionadoCondicion.ItemsSource = ListaElementosCombo;

            opcionElementoOperacion.DisplayMemberPath = "NombreCombo";
            opcionElementoOperacion.SelectedValuePath = "NombreCombo";
            opcionElementoOperacion.ItemsSource = ListaElementosCombo;


            if (ModoOperacion)
            {
                elementoRelacionadoCondicion.SelectedIndex = 0;
                elementoRelacionadoCondicion.IsEnabled = false;
            }

            if (ModoEdicion)
            {
                if (Condicion.EsOperandoActual)
                {
                    elementoRelacionadoCondicion.SelectedItem = ListaElementosCombo.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionOperandoSubElementoCondicion.SelectedItem = null;
                }
                else
                {
                    if (Condicion.OperandoCondicion != null)
                        elementoRelacionadoCondicion.SelectedValue = Condicion.OperandoCondicion.NombreCombo;

                    if (Condicion.OperandoSubElemento_Condicion != null)
                        opcionOperandoSubElementoCondicion.SelectedValue = Condicion.OperandoSubElemento_Condicion.NombreCombo;
                }

                opcionSeleccionNumerosElementoCondicion.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_Condicion).ToString() select I).FirstOrDefault();
                opcionSeleccionNumerosElementoCondicion_SelectionChanged(this, null);

                if (Condicion.Tipo_Valores == TipoOpcion_ValoresCondicion_Flujo.ValoresFijos)
                    opcionValoresFijos.IsChecked = true;
                else if (Condicion.Tipo_Valores == TipoOpcion_ValoresCondicion_Flujo.Valores_DesdeElementoOperacion)
                    opcionValores_ElementoOperacion.IsChecked = true;

                opcionCondicion.SelectedItem = (from ComboBoxItem I in opcionCondicion.Items where I.Uid == ((int)Condicion.TipoOpcionCondicion_TextosInformacion).ToString() select I).FirstOrDefault();
                
                subelementoElementoRelacionadoCondicion.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoCondicion.Items where I.Uid == ((int)Condicion.TipoSubElemento_Condicion).ToString() select I).FirstOrDefault();
                subelementoElementoRelacionadoCondicion_SelectionChanged(this, null);

                subelementoElementoRelacionadoValores.SelectedItem = (from ComboBoxItem I in subelementoElementoRelacionadoValores.Items where I.Uid == ((int)Condicion.TipoSubElemento_Valores).ToString() select I).FirstOrDefault();
                subelementoElementoRelacionadoValores_SelectionChanged(this, null);

                opcionVaciarListaTextosInformacion_CumplenCondicion.IsChecked = Condicion.VaciarListaTextosInformacion_CumplenCondicion;

                if(opcionVaciarListaTextosInformacion_CumplenCondicion.IsChecked == true)
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

                opcionVaciarListaTextosInformacion_CumplenCondicion_Valores.IsChecked = Condicion.VaciarListaTextosInformacion_CumplenCondicion_Valores;

                if (opcionVaciarListaTextosInformacion_CumplenCondicion_Valores.IsChecked == true)
                {
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.Visibility = Visibility.Visible;
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.Visibility = Visibility.Collapsed;
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple_Valores.Visibility = Visibility.Collapsed;
                }

                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.IsChecked = Condicion.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple;
                if (opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.IsChecked == false)
                    opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple_Valores.IsChecked = true;

                if (Condicion.EsOperandoValoresActual)
                {
                    opcionElementoOperacion.SelectedItem = ListaElementosCombo.ToArray().FirstOrDefault(i => i is DiseñoOperacion && ((DiseñoOperacion)i).ID == "0");
                    opcionSubElementoOperacion.SelectedItem = null;
                }
                else
                {
                    opcionElementoOperacion.SelectedItem = Condicion.ElementoOperacion_Valores;
                    opcionSubElementoOperacion.SelectedItem = Condicion.ElementoSubOperacion_Valores;
                }

                opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionSeleccionNumerosElementoCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionSeleccionNumerosElemento_CondicionValores).ToString() select I).FirstOrDefault();
                opcionSeleccionNumerosElementoCondicion_Valores_SelectionChanged(this, null);

                valoresRelacionadosCondicion.Text = Condicion.Valores_Condicion;
                conectorCondicion.SelectedItem = (from ComboBoxItem I in conectorCondicion.Items where I.Uid == ((int)Condicion.TipoConector).ToString() select I).FirstOrDefault();

                conectorCondicion_SelectionChanged(this, null);
                conectorO_Excluyente.IsChecked = (bool)Condicion.ConectorO_Excluyente;

                opcionCantidadSubNumerosCumplenCondicion_SubElemento.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_SubElemento.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_SubElemento.Text = Condicion.CantidadSubNumerosCumplenCondicion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion).ToString() select I).FirstOrDefault();

                opcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion).ToString() select I).FirstOrDefault();

                opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_Valores.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores).ToString() select I).FirstOrDefault();

                opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();
                cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Text = Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.ToString();
                opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem = (from ComboBoxItem I in opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Items where I.Uid == ((int)Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion).ToString() select I).FirstOrDefault();

                opcionCantidadNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem = (from ComboBoxItem I in opcionCantidadDeterminadaNumerosCumplenCondicion.Items where I.Uid == ((int)Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion).ToString() select I).FirstOrDefault();
                cantidadNumerosCumplenCondicion.Text = Condicion.CantidadNumerosCumplenCondicion.ToString();

                opcionNegarCondicion.IsChecked = (bool)Condicion.NegarCondicion;
                opcionCumpleCondicion_ElementoSinCantidades.IsChecked = (bool)Condicion.CumpleCondicion_ElementoSinNumeros;
                opcionCumpleCondicion_ElementoValores_SinCantidades.IsChecked = (bool)Condicion.CumpleCondicion_ElementoValores_SinNumeros;

                opcionConsiderarOperandoCondicion_SiCumple.IsChecked = (bool)Condicion.ConsiderarOperandoCondicion_SiCumple;
                opcionCantidadTextosInformacion_PorElemento.IsChecked = (bool)Condicion.CantidadTextosInformacion_PorElemento;
                opcionCantidadTextosInformacion_PorElemento_Valores.IsChecked = (bool)Condicion.CantidadTextosInformacion_PorElemento_Valores;

                if (opcionConsiderarOperandoCondicion_SiCumple.IsChecked == true)
                    opcionConsiderarCondicionesHijas.Visibility = Visibility.Visible;
                else
                    opcionConsiderarCondicionesHijas.Visibility = Visibility.Collapsed;

                opcionConsiderarCondicionesHijas.IsChecked = (bool)Condicion.ConsiderarIncluirCondicionesHijas;
            }
            else
            {
                opcionConsiderarOperandoCondicion_SiCumple.IsChecked = true;
                opcionConsiderarCondicionesHijas.IsChecked = true;
                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.IsChecked = true;

                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.Visibility = Visibility.Collapsed;
                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple.Visibility = Visibility.Collapsed;

                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.IsChecked = true;

                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.Visibility = Visibility.Collapsed;
                opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple_Valores.Visibility = Visibility.Collapsed;
            }

            if (opcionValoresFijos.IsChecked == true)
                opcionValoresFijos_Checked(this, e);
            else if (opcionValores_ElementoOperacion.IsChecked == true)
                opcionValores_ElementoOperacion_Checked(this, e);
        }

        private void opcionValoresFijos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionValoresFijos.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Visible;
                    opcionesOperandos_Valores.Visibility = Visibility.Collapsed;
                    revertirOperandosCondicionValores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionValores_ElementoOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionValores_ElementoOperacion.IsChecked == true)
                {
                    valoresRelacionadosCondicion.Visibility = Visibility.Collapsed;
                    opcionesOperandos_Valores.Visibility = Visibility.Visible;
                    revertirOperandosCondicionValores.Visibility = Visibility.Visible;
                }
            }
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (opcionesOperandos.Visibility == Visibility.Visible && elementoRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (subelementoElementoRelacionadoCondicion.Visibility == Visibility.Visible && subelementoElementoRelacionadoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de la variable o vector para la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (subelementoElementoRelacionadoValores.Visibility == Visibility.Visible && subelementoElementoRelacionadoValores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de la variable o vector para los valores.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesOperandos_Valores.Visibility == Visibility.Visible && (bool)opcionValores_ElementoOperacion.IsChecked && opcionElementoOperacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una variable o vector de operación.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCondicion.Visibility == Visibility.Visible && opcionCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción de condición de cadenas de texto.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (conectorCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de conector con la condición anterior.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_SubElemento.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_SubElemento.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_SubElemento.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility == Visibility.Visible && opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.Visibility == Visibility.Visible && opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números de la variable o vector, que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una opción para la cantidad de números que deben cunmplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionCantidadDeterminadaNumerosCumplenCondicion.Visibility == Visibility.Visible && opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de cantidad de números que deben cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int numero = 0;

                if (int.TryParse(cantidadNumerosCumplenCondicion.Text, out numero))
                {
                    if (!ModoEdicion)
                    {
                        Condicion = new CondicionFlujo();
                        Condicion.CantidadNumerosCumplenCondicion = numero;
                    }

                    if (elementoRelacionadoCondicion.SelectedItem != null &&
                        ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ID != "0")
                    {
                        Condicion.OperandoCondicion = (DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem;
                        Condicion.OperandoSubElemento_Condicion = (DiseñoElementoOperacion)opcionOperandoSubElementoCondicion.SelectedItem;
                        Condicion.EsOperandoActual = false;
                    }
                    else
                    {
                        Condicion.OperandoCondicion = null;
                        Condicion.OperandoSubElemento_Condicion = null;
                        Condicion.EsOperandoActual = true;
                    }

                    if (opcionSeleccionNumerosElementoCondicion.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_Condicion = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid);

                    if (opcionCondicion.SelectedItem != null)
                        Condicion.TipoOpcionCondicion_TextosInformacion = (TipoOpcion_CondicionTextosInformacion_Flujo)int.Parse(((ComboBoxItem)opcionCondicion.SelectedItem).Uid);

                    if (opcionValoresFijos.IsChecked == true)
                        Condicion.Tipo_Valores = TipoOpcion_ValoresCondicion_Flujo.ValoresFijos;
                    else if (opcionValores_ElementoOperacion.IsChecked == true)
                        Condicion.Tipo_Valores = TipoOpcion_ValoresCondicion_Flujo.Valores_DesdeElementoOperacion;

                    if (subelementoElementoRelacionadoCondicion.SelectedItem != null)
                        Condicion.TipoSubElemento_Condicion = (TipoSubElemento_EvaluacionCondicion_Flujo)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoCondicion.SelectedItem).Uid);

                    if (subelementoElementoRelacionadoValores.SelectedItem != null)
                        Condicion.TipoSubElemento_Valores = (TipoSubElemento_EvaluacionCondicion_Flujo)int.Parse(((ComboBoxItem)subelementoElementoRelacionadoValores.SelectedItem).Uid);

                    Condicion.VaciarListaTextosInformacion_CumplenCondicion = (bool)opcionVaciarListaTextosInformacion_CumplenCondicion.IsChecked;
                    Condicion.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple = (bool)opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple.IsChecked;

                    Condicion.VaciarListaTextosInformacion_CumplenCondicion_Valores = (bool)opcionVaciarListaTextosInformacion_CumplenCondicion_Valores.IsChecked;
                    Condicion.VaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores = (bool)opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.IsChecked;

                    if (opcionElementoOperacion.SelectedItem != null &&
                        ((DiseñoOperacion)opcionElementoOperacion.SelectedItem).ID != "0")
                    {
                        if (opcionElementoOperacion.SelectedItem != null)
                            Condicion.ElementoOperacion_Valores = (DiseñoOperacion)opcionElementoOperacion.SelectedItem;

                        if (opcionSubElementoOperacion.SelectedItem != null)
                            Condicion.ElementoSubOperacion_Valores = (DiseñoElementoOperacion)opcionSubElementoOperacion.SelectedItem;

                        Condicion.EsOperandoValoresActual = false;
                    }
                    else
                    {
                        Condicion.ElementoOperacion_Valores = null;
                        Condicion.ElementoSubOperacion_Valores = null;
                        Condicion.EsOperandoValoresActual = true;
                    }

                    if (opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem != null)
                        Condicion.OpcionSeleccionNumerosElemento_CondicionValores = (TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid);

                    int numero2 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Text, out numero2);
                    Condicion.CantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = numero2;

                    int numero3 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_SubElemento.Text, out numero3);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion = numero3;

                    int numero4 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_Valores.Text, out numero4);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores = numero4;

                    int numero5 = 0;
                    int.TryParse(cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Text, out numero5);
                    if (Condicion != null) Condicion.CantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = numero5;

                    Condicion.Valores_Condicion = valoresRelacionadosCondicion.Text;
                    Condicion.TipoConector = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)conectorCondicion.SelectedItem).Uid);

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_SubElemento.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.SelectedItem).Uid);

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_ElementoTextoInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedItem).Uid);

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores.SelectedItem).Uid);

                    Condicion.OpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem).Uid);
                    Condicion.OpcionTipoCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion = (TipoOpcionCategoriaCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedItem).Uid);

                    Condicion.OpcionCantidadNumerosCumplenCondicion = (TipoOpcionCantidadNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadNumerosCumplenCondicion.SelectedItem).Uid);
                    Condicion.OpcionCantidadDeterminadaNumerosCumplenCondicion = (TipoOpcionCantidadDeterminadaNumerosCumplenCondicion)int.Parse(((ComboBoxItem)opcionCantidadDeterminadaNumerosCumplenCondicion.SelectedItem).Uid);

                    Condicion.NegarCondicion = (bool)opcionNegarCondicion.IsChecked;
                    Condicion.CumpleCondicion_ElementoSinNumeros = (bool)opcionCumpleCondicion_ElementoSinCantidades.IsChecked;
                    Condicion.CumpleCondicion_ElementoValores_SinNumeros = (bool)opcionCumpleCondicion_ElementoValores_SinCantidades.IsChecked;
                    Condicion.ConectorO_Excluyente = (bool)conectorO_Excluyente.IsChecked;

                    Condicion.ConsiderarOperandoCondicion_SiCumple = (bool)opcionConsiderarOperandoCondicion_SiCumple.IsChecked;
                    Condicion.ConsiderarIncluirCondicionesHijas = (bool)opcionConsiderarCondicionesHijas.IsChecked;
                    Condicion.CantidadTextosInformacion_PorElemento = (bool)opcionCantidadTextosInformacion_PorElemento.IsChecked;
                    Condicion.CantidadTextosInformacion_PorElemento_Valores = (bool)opcionCantidadTextosInformacion_PorElemento_Valores.IsChecked;

                    Aceptar = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ingresa una cantidad válida que se deba cumplir la condición.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
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

                if (ModoOperacion)
                {
                    opcionOperandoSubElementoCondicion.ItemsSource = ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ElementosDiseñoOperacion.Where(item => SubOperandos.Contains(item) && 
                    (item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota));
                }
                else
                {
                    opcionOperandoSubElementoCondicion.ItemsSource = ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ElementosDiseñoOperacion.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota);
                }
            }
        }
        private void quitarSeleccion_opcionOperandoSubElementoCondicion_Click(object sender, RoutedEventArgs e)
        {
            opcionOperandoSubElementoCondicion.SelectedItem = null;
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

        private void opcionElementoOperacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (opcionElementoOperacion.SelectedItem != null &&
                ((DiseñoOperacion)opcionElementoOperacion.SelectedItem).ID != "0")
            {
                opcionSubElementoOperacion.DisplayMemberPath = "NombreCombo";
                opcionSubElementoOperacion.SelectedValuePath = "NombreCombo";

                if (ModoOperacion)
                {
                    opcionSubElementoOperacion.ItemsSource = ((DiseñoOperacion)opcionElementoOperacion.SelectedItem).ElementosDiseñoOperacion.Where(item => SubOperandos.Contains(item) &&
                    (item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota));
                }
                else
                {
                    opcionSubElementoOperacion.ItemsSource = ((DiseñoOperacion)opcionElementoOperacion.SelectedItem).ElementosDiseñoOperacion.Where(item => item.Tipo != TipoElementoDiseñoOperacion.Salida &
                    item.Tipo != TipoElementoDiseñoOperacion.Nota);
                }
            }
        }

        private void quitarSeleccion_opcionSubElementoOperacion_Click(object sender, RoutedEventArgs e)
        {
            opcionSubElementoOperacion.SelectedItem = null;
        }

        private void opcionSeleccionNumerosElementoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionSeleccionNumerosElementoCondicion_Valores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionSeleccionNumerosElemento_Condicion)int.Parse(((ComboBoxItem)opcionSeleccionNumerosElementoCondicion_Valores.SelectedItem).Uid) == TipoOpcionSeleccionNumerosElemento_Condicion.ConjuntoNumerosOperando)
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Visible;
                    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                    opcionCantidadTextosInformacion_PorElemento_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadSubNumerosCumplenCondicion_SubElemento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_SubElemento.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadSubNumerosCumplenCondicion_SubElemento_TextChanged(object sender, TextChangedEventArgs e)
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
        private void opcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.SelectedIndex == 2)
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_SelectionChanged(this, e);
                }
                else
                {
                    opcionCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void cantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void subelementoElementoRelacionadoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (subelementoElementoRelacionadoCondicion.SelectedItem == (from ComboBoxItem I in subelementoElementoRelacionadoCondicion.Items where I.Uid == "3" select I).FirstOrDefault())
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Visible;                    
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }

                if(subelementoElementoRelacionadoCondicion.SelectedItem == (from ComboBoxItem I in subelementoElementoRelacionadoCondicion.Items where I.Uid == "5" select I).FirstOrDefault() |
                    subelementoElementoRelacionadoCondicion.SelectedItem == (from ComboBoxItem I in subelementoElementoRelacionadoCondicion.Items where I.Uid == "6" select I).FirstOrDefault())
                {
                    opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionesVaciarListaTextosInformacionCumplenCondicion.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void subelementoElementoRelacionadoValores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (subelementoElementoRelacionadoValores.SelectedItem == (from ComboBoxItem I in subelementoElementoRelacionadoValores.Items where I.Uid == "3" select I).FirstOrDefault())
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionesCantidadSubNumerosCumplenCondicion_Valores_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
                }

                if (subelementoElementoRelacionadoValores.SelectedItem == (from ComboBoxItem I in subelementoElementoRelacionadoValores.Items where I.Uid == "5" select I).FirstOrDefault() |
                    subelementoElementoRelacionadoValores.SelectedItem == (from ComboBoxItem I in subelementoElementoRelacionadoValores.Items where I.Uid == "6" select I).FirstOrDefault())
                {
                    opcionesVaciarListaTextosInformacionCumplenCondicion_Valores.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionesVaciarListaTextosInformacionCumplenCondicion_Valores.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_SubElemento.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
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

        private void opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCantidadDeterminadaSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.SelectedIndex == 0)
                {
                    cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    cantidadSubNumerosCumplenCondicion_SubElemento_ElementoTextoInformacion.Visibility = Visibility.Collapsed;
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

        private void opcionVaciarListaTextosInformacion_CumplenCondicion_Valores_Checked(object sender, RoutedEventArgs e)
        {
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.Visibility = Visibility.Visible;
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple_Valores.Visibility = Visibility.Visible;
        }

        private void opcionVaciarListaTextosInformacion_CumplenCondicion_Valores_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoCumple_Valores.Visibility = Visibility.Collapsed;
            opcionVaciarListaTextosInformacion_CumplenCondicion_CuandoNoCumple_Valores.Visibility = Visibility.Collapsed;
        }

        private void revertirOperandosCondicionValores_Click(object sender, RoutedEventArgs e)
        {
            if (elementoRelacionadoCondicion.SelectedItem != null &&
                opcionElementoOperacion.SelectedItem != null &&
                ((DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem).ID != "0" &&
                ((DiseñoOperacion)opcionElementoOperacion.SelectedItem).ID != "0")
            {
                DiseñoOperacion operandoCondicion = (DiseñoOperacion)elementoRelacionadoCondicion.SelectedItem;
                elementoRelacionadoCondicion.SelectedItem = opcionElementoOperacion.SelectedItem;

                opcionElementoOperacion.SelectedItem = operandoCondicion;
            }
        }
    }
}
