using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.Condiciones
{
    /// <summary>
    /// Lógica de interacción para SeleccionOrdenamiento_Condiciones.xaml
    /// </summary>
    public partial class SeleccionOrdenamiento_Condiciones : UserControl
    {
        public List<CondicionFlujo> CondicionesFlujo { get; set; }
        public List<AsociacionCondicionFlujo_ElementoSalida> AsociacionesCondicionesFlujo_ElementosSalida { get; set; }
        public List<AsociacionCondicionFlujo_ElementoSalida> AsociacionesCondicionesFlujo_ElementosSalida2 { get; set; }
        public List<DiseñoElementoOperacion> ElementosSalida { get; set; }
        public List<DiseñoOperacion> ElementosSalida_Operacion { get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public DiseñoOperacion OperacionRelacionada { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public SeleccionOrdenamiento_Condiciones()
        {
            InitializeComponent();
            AsociacionesCondicionesFlujo_ElementosSalida = new List<AsociacionCondicionFlujo_ElementoSalida>();
        }

        private void ListarCondiciones()
        {
            if (CondicionesFlujo != null)
            {
                condicionesSeleccionadas.Children.Clear();

                int indice = 0;

                foreach (var item in CondicionesFlujo)
                {
                    condicionesSeleccionadas.RowDefinitions.Add(new RowDefinition());
                    condicionesSeleccionadas.RowDefinitions.Last().Height = GridLength.Auto;

                    TextBlock numero = new TextBlock();
                    numero.Text = (indice + 1).ToString();
                    numero.Margin = new Thickness(10);

                    condicionesSeleccionadas.Children.Add(numero);

                    Grid.SetRow(numero, indice);
                    Grid.SetColumn(numero, 0);

                    TextBlock textoCondiciones = new TextBlock();
                    textoCondiciones.Text = "Las condiciones :";
                    textoCondiciones.Margin = new Thickness(10);

                    condicionesSeleccionadas.Children.Add(textoCondiciones);

                    Grid.SetRow(textoCondiciones, indice);
                    Grid.SetColumn(textoCondiciones, 1);

                    OpcionCondicionesFlujo condiciones = new OpcionCondicionesFlujo();
                    condiciones.Margin = new Thickness(10);
                    condiciones.Condiciones = item;
                    condiciones.Operandos = Operandos;
                    condiciones.SubOperandos = SubOperandos;
                    condiciones.ModoOperacion = ModoOperacion;
                    condiciones.ListaElementos = ListaElementos;
                    condiciones.MaxWidth = 400;

                    condicionesSeleccionadas.Children.Add(condiciones);

                    Grid.SetRow(condiciones, indice);
                    Grid.SetColumn(condiciones, 2);

                    Button botonOpcionAsignacionOperandos = new Button();
                    botonOpcionAsignacionOperandos.Margin = new Thickness(10);
                    botonOpcionAsignacionOperandos.MaxHeight = 70;
                    botonOpcionAsignacionOperandos.VerticalAlignment = VerticalAlignment.Center;
                    //comboOpcionAsignacion.Tag = new object[] { indice, };
                    ListarOpcionesBotonTextoAsignacionOperandos(botonOpcionAsignacionOperandos, item);

                    condicionesSeleccionadas.Children.Add(botonOpcionAsignacionOperandos);

                    Grid.SetRow(botonOpcionAsignacionOperandos, indice);
                    Grid.SetColumn(botonOpcionAsignacionOperandos, 3);

                    TextBlock etiquetaElementoSalida = new TextBlock();
                    etiquetaElementoSalida.Text = "si se cumplen, continuar en: ";
                    etiquetaElementoSalida.Margin = new Thickness(10);

                    StackPanel asociacionTextoInformacionElementoSalida = new StackPanel();
                    asociacionTextoInformacionElementoSalida.Children.Add(etiquetaElementoSalida);

                    if (ModoOperacion)
                    {
                        Button botonOpcionesElementosSalida = new Button();
                        botonOpcionesElementosSalida.Margin = new Thickness(10);
                        botonOpcionesElementosSalida.MaxHeight = 70;
                        botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                        ListarOpcionesBotonElementosSalida(botonOpcionesElementosSalida, condiciones);
                        asociacionTextoInformacionElementoSalida.Children.Add(botonOpcionesElementosSalida);
                    }
                    else
                    {
                        Button botonOpcionesElementosSalida_Operacion = new Button();
                        botonOpcionesElementosSalida_Operacion.Margin = new Thickness(10);
                        botonOpcionesElementosSalida_Operacion.MaxHeight = 70;
                        botonOpcionesElementosSalida_Operacion.VerticalAlignment = VerticalAlignment.Center;
                        ListarOpcionesBotonElementosSalida_Operacion(botonOpcionesElementosSalida_Operacion, condiciones);
                        asociacionTextoInformacionElementoSalida.Children.Add(botonOpcionesElementosSalida_Operacion);
                    }

                    condicionesSeleccionadas.Children.Add(asociacionTextoInformacionElementoSalida);

                    Grid.SetRow(asociacionTextoInformacionElementoSalida, indice);
                    Grid.SetColumn(asociacionTextoInformacionElementoSalida, 4);

                    TextBlock etiquetaElementoSalida2 = new TextBlock();
                    etiquetaElementoSalida2.Text = "si no se cumplen, continuar en: ";
                    etiquetaElementoSalida2.Margin = new Thickness(10);

                    StackPanel asociacionTextoInformacionElementoSalida2 = new StackPanel();
                    asociacionTextoInformacionElementoSalida2.Children.Add(etiquetaElementoSalida2);

                    if (ModoOperacion)
                    {
                        Button botonOpcionesElementosSalida = new Button();
                        botonOpcionesElementosSalida.Margin = new Thickness(10);
                        botonOpcionesElementosSalida.MaxHeight = 70;
                        botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                        ListarOpcionesBotonElementosSalida2(botonOpcionesElementosSalida, condiciones);
                        asociacionTextoInformacionElementoSalida2.Children.Add(botonOpcionesElementosSalida);
                    }
                    else
                    {
                        Button botonOpcionesElementosSalida_Operacion = new Button();
                        botonOpcionesElementosSalida_Operacion.Margin = new Thickness(10);
                        botonOpcionesElementosSalida_Operacion.MaxHeight = 70;
                        botonOpcionesElementosSalida_Operacion.VerticalAlignment = VerticalAlignment.Center;
                        ListarOpcionesBotonElementosSalida_Operacion2(botonOpcionesElementosSalida_Operacion, condiciones);
                        asociacionTextoInformacionElementoSalida2.Children.Add(botonOpcionesElementosSalida_Operacion);
                    }

                    condicionesSeleccionadas.Children.Add(asociacionTextoInformacionElementoSalida2);

                    Grid.SetRow(asociacionTextoInformacionElementoSalida2, indice);
                    Grid.SetColumn(asociacionTextoInformacionElementoSalida2, 5);

                    Image ImagenBotonQuitar = new Image();
                    ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\06.png", UriKind.Relative));
                    ImagenBotonQuitar.Width = 24;
                    ImagenBotonQuitar.Height = 24;

                    Button botonQuitar = new Button();
                    botonQuitar.Content = ImagenBotonQuitar;
                    botonQuitar.Margin = new Thickness(10);
                    botonQuitar.MaxHeight = 70;
                    botonQuitar.Tag = indice;
                    botonQuitar.Click += QuitarCondicion;

                    condicionesSeleccionadas.Children.Add(botonQuitar);

                    Grid.SetRow(botonQuitar, indice);
                    Grid.SetColumn(botonQuitar, 6);

                    Image ImagenBotonSubir = new Image();
                    ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos6\\icono_03.png", UriKind.Relative));
                    ImagenBotonSubir.Width = 24;
                    ImagenBotonSubir.Height = 24;

                    Button botonSubir = new Button();
                    botonSubir.Content = ImagenBotonSubir;
                    botonSubir.Margin = new Thickness(10);
                    botonSubir.MaxHeight = 70;
                    botonSubir.Tag = indice;
                    botonSubir.Click += SubirCondicion;

                    condicionesSeleccionadas.Children.Add(botonSubir);

                    Grid.SetRow(botonSubir, indice);
                    Grid.SetColumn(botonSubir, 7);

                    Image ImagenBotonBajar = new Image();
                    ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos6\\icono_04.png", UriKind.Relative));
                    ImagenBotonBajar.Width = 24;
                    ImagenBotonBajar.Height = 24;

                    Button botonBajar = new Button();
                    botonBajar.Content = ImagenBotonBajar;
                    botonBajar.Margin = new Thickness(10);
                    botonBajar.MaxHeight = 70;
                    botonBajar.Tag = indice;
                    botonBajar.Click += BajarCondicion;

                    condicionesSeleccionadas.Children.Add(botonBajar);

                    Grid.SetRow(botonBajar, indice);
                    Grid.SetColumn(botonBajar, 8);

                    indice++;
                }
            }
        }

        private void ListarOpcionesBotonTextoAsignacionOperandos(Button boton, CondicionFlujo itemInstancia)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Aplicarlas a ";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            if (Operandos != null &&
                !ModoOperacion)
            {
                foreach (var itemOperando in Operandos.Distinct())
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    CheckBox checkOperandos = new CheckBox();
                    checkOperandos.Content = "Operando " + itemOperando.NombreCombo;
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando };

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.Operandos_AplicarCondiciones.Contains(itemOperando);

                    checkOperandos.Checked += CheckOperandos_Checked1;
                    checkOperandos.Unchecked += CheckOperandos_Unchecked1;
                    opcionOperandos.Content = checkOperandos;
                    menuBoton.Items.Add(opcionOperandos);
                }
            }

            if (SubOperandos != null)
            {
                foreach (var itemOperando in SubOperandos.Distinct())
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    CheckBox checkOperandos = new CheckBox();
                    checkOperandos.Content = "Operando " + itemOperando.NombreCombo;
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando };

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.SubOperandos_AplicarCondiciones.Contains(itemOperando);

                    checkOperandos.Checked += CheckSubOperandos_Checked1;
                    checkOperandos.Unchecked += CheckSubOperandos_Unchecked1;
                    opcionOperandos.Content = checkOperandos;
                    menuBoton.Items.Add(opcionOperandos);
                }
            }
        }

        private void CheckOperandos_Checked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                CondicionFlujo asignacion = (CondicionFlujo)objetos[0];
                DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                if (check.IsChecked == true)
                {
                    if (asignacion != null)
                        asignacion.Operandos_AplicarCondiciones.Add(operando);
                }
            }
        }

        private void CheckOperandos_Unchecked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                CondicionFlujo asignacion = (CondicionFlujo)objetos[0];
                DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    if (asignacion != null)
                        asignacion.Operandos_AplicarCondiciones.Remove(operando);
                }
            }
        }

        private void CheckSubOperandos_Checked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                CondicionFlujo asignacion = (CondicionFlujo)objetos[0];
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == true)
                {
                    if (asignacion != null)
                        asignacion.SubOperandos_AplicarCondiciones.Add(operando);
                }
            }
        }

        private void CheckSubOperandos_Unchecked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                CondicionFlujo asignacion = (CondicionFlujo)objetos[0];
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    if (asignacion != null)
                        asignacion.SubOperandos_AplicarCondiciones.Remove(operando);
                }
            }
        }

        private void ListarOpcionesBotonElementosSalida(Button boton, OpcionCondicionesFlujo condiciones)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar elementos de salida";
            boton.ContextMenu = menuBoton;
            boton.Tag = condiciones;
            boton.Click += Boton_Click;

            if (ElementosSalida != null)
            {
                foreach (var itemSalida in ElementosSalida)
                {
                    ComboBoxItem opcionElementoSalida = new ComboBoxItem();
                    CheckBox checkElementoSalida = new CheckBox();
                    checkElementoSalida.Content = itemSalida.NombreElemento;
                    checkElementoSalida.Tag = new object[] { condiciones, itemSalida };

                    checkElementoSalida.IsChecked = AsociacionesCondicionesFlujo_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                    item.Condiciones == condiciones.Condiciones);

                    checkElementoSalida.Checked += CheckElementoSalida_Checked;
                    checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked;
                    opcionElementoSalida.Content = checkElementoSalida;
                    menuBoton.Items.Add(opcionElementoSalida);
                }
            }
        }

        private void ListarOpcionesBotonElementosSalida2(Button boton, OpcionCondicionesFlujo condiciones)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar elementos de salida";
            boton.ContextMenu = menuBoton;
            boton.Tag = condiciones;
            boton.Click += Boton_Click;

            if (ElementosSalida != null)
            {
                foreach (var itemSalida in ElementosSalida)
                {
                    ComboBoxItem opcionElementoSalida = new ComboBoxItem();
                    CheckBox checkElementoSalida = new CheckBox();
                    checkElementoSalida.Content = itemSalida.NombreElemento;
                    checkElementoSalida.Tag = new object[] { condiciones, itemSalida };

                    checkElementoSalida.IsChecked = AsociacionesCondicionesFlujo_ElementosSalida2.Any(item => item.ElementoSalida == itemSalida &&
                    item.Condiciones == condiciones.Condiciones);

                    checkElementoSalida.Checked += CheckElementoSalida_Checked2;
                    checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked2;
                    opcionElementoSalida.Content = checkElementoSalida;
                    menuBoton.Items.Add(opcionElementoSalida);
                }
            }
        }
        private void CheckElementoSalida_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = (from A in AsociacionesCondicionesFlujo_ElementosSalida
                                                                            where
                   A.Condiciones == condiciones.Condiciones &
                   A.ElementoSalida == elementoSalida
                                                                            select A).FirstOrDefault();

                    if (asociacion != null)
                        AsociacionesCondicionesFlujo_ElementosSalida.Remove(asociacion);
                }
            }
        }

        private void CheckElementoSalida_Unchecked2(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = (from A in AsociacionesCondicionesFlujo_ElementosSalida2
                                                                          where
                 A.Condiciones == condiciones.Condiciones &
                 A.ElementoSalida == elementoSalida
                                                                          select A).FirstOrDefault();

                    if (asociacion != null)
                        AsociacionesCondicionesFlujo_ElementosSalida2.Remove(asociacion);
                }
            }
        }
        private void CheckElementoSalida_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == true)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = new AsociacionCondicionFlujo_ElementoSalida();
                    asociacion.ElementoSalida = elementoSalida;
                    asociacion.Condiciones = condiciones.Condiciones;
                    AsociacionesCondicionesFlujo_ElementosSalida.Add(asociacion);
                }
            }
        }

        private void CheckElementoSalida_Checked2(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == true)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = new AsociacionCondicionFlujo_ElementoSalida();
                    asociacion.ElementoSalida = elementoSalida;
                    asociacion.Condiciones = condiciones.Condiciones;
                    AsociacionesCondicionesFlujo_ElementosSalida2.Add(asociacion);
                }
            }
        }
        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void ListarOpcionesBotonElementosSalida_Operacion(Button boton, OpcionCondicionesFlujo condiciones)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar elementos de salida";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            if (ElementosSalida_Operacion != null)
            {
                foreach (var itemSalida in ElementosSalida_Operacion)
                {
                    ComboBoxItem opcionElementoSalida = new ComboBoxItem();
                    
                    StackPanel opciones = new StackPanel();
                    opciones.Orientation = Orientation.Horizontal;                    

                    CheckBox checkElementoSalida = new CheckBox();

                    ComboBox botonOpcionesElementosSalida = new ComboBox();
                    botonOpcionesElementosSalida.Margin = new Thickness(10);
                    botonOpcionesElementosSalida.MaxHeight = 70;
                    botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                    botonOpcionesElementosSalida.Visibility = Visibility.Collapsed;

                    opciones.Children.Add(checkElementoSalida);
                    opciones.Children.Add(botonOpcionesElementosSalida);

                    checkElementoSalida.Content = itemSalida.NombreCombo;
                    checkElementoSalida.Tag = new object[] { condiciones, itemSalida, botonOpcionesElementosSalida };

                    checkElementoSalida.IsChecked = AsociacionesCondicionesFlujo_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                    item.Condiciones == condiciones.Condiciones);

                    checkElementoSalida.Checked += CheckElementoSalida_Checked1;
                    checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked1;

                    ListarOpcionesBotonElementosSalida_DentroOperacion(botonOpcionesElementosSalida, itemSalida,
                           AsociacionesCondicionesFlujo_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                       item.Condiciones == condiciones.Condiciones));

                    if (checkElementoSalida.IsChecked == true)
                    {
                        if (!itemSalida.DefinicionSimple_Operacion)
                        {
                            botonOpcionesElementosSalida.Visibility = Visibility.Visible;
                        }
                    }

                    opcionElementoSalida.Content = opciones;
                    menuBoton.Items.Add(opcionElementoSalida);
                }
            }
        }

        private void ListarOpcionesBotonElementosSalida_DentroOperacion(ComboBox combo,
            DiseñoOperacion ElementoOperando, AsociacionCondicionFlujo_ElementoSalida asociacion)
        {
            ComboBox menuBoton = new ComboBox();
            combo.Items.Add(new ComboBoxItem() { Content = "Seleccionar elementos de salida", Visibility = Visibility.Collapsed });
            combo.SelectedIndex = 0;
            //boton.ContextMenu = menuBoton;
            combo.Tag = asociacion;
            //boton.Click += Boton_Click;

            ListarOpcionesMenuBotonElementosSalida_DentroOperacion(combo, ElementoOperando, asociacion);
        }

        private void ListarOpcionesMenuBotonElementosSalida_DentroOperacion(ComboBox combo, DiseñoOperacion ElementoOperando,
            AsociacionCondicionFlujo_ElementoSalida asociacion)
        {
            //combo.Items.Clear();

            if (ElementoOperando.ElementosDiseñoOperacion != null &&
                ElementoOperando.ElementosDiseñoOperacion.Any(i => !i.ElementosAnteriores.Any() &&
                i.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion &&
                i.ElementoDiseñoRelacionado == OperacionRelacionada))
            {
                var Elementos = ElementoOperando.ElementosDiseñoOperacion.Where(i => !i.ElementosAnteriores.Any() &&
                i.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion).ToList();

                foreach (var itemSalida in Elementos)
                {
                    MenuItem opcionElementoSalida = new MenuItem();
                    CheckBox checkElementoSalida = new CheckBox();
                    checkElementoSalida.Content = itemSalida.NombreCombo;
                    checkElementoSalida.Tag = new object[] { asociacion, itemSalida, combo };

                    if (asociacion != null)
                    {
                        checkElementoSalida.IsChecked = asociacion.ElementosSalidas.Any(item => item == itemSalida);

                        checkElementoSalida.Checked += CheckElementoSalida_Checked_DentroOperacion;
                        checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked_DentroOperacion;
                    }
                    //checkElementoSalida.Click += CheckElementoSalida_Click;
                    opcionElementoSalida.Header = checkElementoSalida;
                    combo.Items.Add(opcionElementoSalida);

                }
            }
        }

        private void CheckElementoSalida_Checked_DentroOperacion(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                AsociacionCondicionFlujo_ElementoSalida asociacion = (AsociacionCondicionFlujo_ElementoSalida)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == true)
                {
                    if (!asociacion.ElementosSalidas.Contains(elementoSalida))
                        asociacion.ElementosSalidas.Add(elementoSalida);
                }
            }
        }
        private void CheckElementoSalida_Unchecked_DentroOperacion(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                AsociacionCondicionFlujo_ElementoSalida asociacion = (AsociacionCondicionFlujo_ElementoSalida)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    if (asociacion.ElementosSalidas.Contains(elementoSalida))
                        asociacion.ElementosSalidas.Remove(elementoSalida);
                }
            }
        }

        private void ListarOpcionesBotonElementosSalida_Operacion2(Button boton, OpcionCondicionesFlujo condiciones)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar elementos de salida";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            if (ElementosSalida_Operacion != null)
            {
                foreach (var itemSalida in ElementosSalida_Operacion)
                {
                    ComboBoxItem opcionElementoSalida = new ComboBoxItem();

                    StackPanel opciones = new StackPanel();
                    opciones.Orientation = Orientation.Horizontal;

                    CheckBox checkElementoSalida = new CheckBox();

                    ComboBox botonOpcionesElementosSalida = new ComboBox();
                    botonOpcionesElementosSalida.Margin = new Thickness(10);
                    botonOpcionesElementosSalida.MaxHeight = 70;
                    botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                    botonOpcionesElementosSalida.Visibility = Visibility.Collapsed;

                    opciones.Children.Add(checkElementoSalida);
                    opciones.Children.Add(botonOpcionesElementosSalida);

                    checkElementoSalida.Content = itemSalida.NombreCombo;
                    checkElementoSalida.Tag = new object[] { condiciones, itemSalida, botonOpcionesElementosSalida };

                    checkElementoSalida.IsChecked = AsociacionesCondicionesFlujo_ElementosSalida2.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                    item.Condiciones == condiciones.Condiciones);

                    checkElementoSalida.Checked += CheckElementoSalida_Checked1_2;
                    checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked1_2;

                    ListarOpcionesBotonElementosSalida_DentroOperacion(botonOpcionesElementosSalida, itemSalida,
                           AsociacionesCondicionesFlujo_ElementosSalida2.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                       item.Condiciones == condiciones.Condiciones));

                    if (checkElementoSalida.IsChecked == true)
                    {
                        if (!itemSalida.DefinicionSimple_Operacion)
                        {
                            botonOpcionesElementosSalida.Visibility = Visibility.Visible;
                        }
                    }

                    opcionElementoSalida.Content = opciones;
                    menuBoton.Items.Add(opcionElementoSalida);
                }
            }
        }
        private void CheckElementoSalida_Unchecked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];
                ComboBox botonElementosInternos = (ComboBox)objetos[2];

                if (check.IsChecked == false)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = (from A in AsociacionesCondicionesFlujo_ElementosSalida
                                                                            where
                   A.Condiciones == condiciones.Condiciones &
                   A.ElementoSalida_Operacion == elementoSalida
                                                                            select A).FirstOrDefault();

                    if (asociacion != null)
                        AsociacionesCondicionesFlujo_ElementosSalida.Remove(asociacion);

                    if (!elementoSalida.DefinicionSimple_Operacion)
                    {
                        botonElementosInternos.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void CheckElementoSalida_Unchecked1_2(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];
                ComboBox botonElementosInternos = (ComboBox)objetos[2];

                if (check.IsChecked == false)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = (from A in AsociacionesCondicionesFlujo_ElementosSalida2
                                                                          where
                 A.Condiciones == condiciones.Condiciones &
                 A.ElementoSalida_Operacion == elementoSalida
                                                                          select A).FirstOrDefault();

                    if (asociacion != null)
                        AsociacionesCondicionesFlujo_ElementosSalida2.Remove(asociacion);

                    if (!elementoSalida.DefinicionSimple_Operacion)
                    {
                        botonElementosInternos.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void CheckElementoSalida_Checked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];
                ComboBox botonElementosInternos = (ComboBox)objetos[2];

                if (check.IsChecked == true)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = new AsociacionCondicionFlujo_ElementoSalida();
                    asociacion.ElementoSalida_Operacion = elementoSalida;
                    asociacion.Condiciones = condiciones.Condiciones;
                    AsociacionesCondicionesFlujo_ElementosSalida.Add(asociacion);

                    if (!elementoSalida.DefinicionSimple_Operacion)
                    {
                        botonElementosInternos.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CheckElementoSalida_Checked1_2(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondicionesFlujo condiciones = (OpcionCondicionesFlujo)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];
                ComboBox botonElementosInternos = (ComboBox)objetos[2];

                if (check.IsChecked == true)
                {
                    AsociacionCondicionFlujo_ElementoSalida asociacion = new AsociacionCondicionFlujo_ElementoSalida();
                    asociacion.ElementoSalida_Operacion = elementoSalida;
                    asociacion.Condiciones = condiciones.Condiciones;
                    AsociacionesCondicionesFlujo_ElementosSalida2.Add(asociacion);

                    if (!elementoSalida.DefinicionSimple_Operacion)
                    {
                        botonElementosInternos.Visibility = Visibility.Visible;
                    }
                }
            }
        }
        private void QuitarCondicion(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;
                        
            var asociaciones = (from E in AsociacionesCondicionesFlujo_ElementosSalida where E.Condiciones == CondicionesFlujo[indice] select E).ToList();

            while (asociaciones.Any())
            {
                AsociacionesCondicionesFlujo_ElementosSalida.Remove(asociaciones.FirstOrDefault());
                asociaciones.Remove(asociaciones.FirstOrDefault());
            }
            
            CondicionesFlujo.RemoveAt(indice);
            ListarCondiciones();
        }

        private void SubirCondicion(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice > 0)
            {
                CondicionFlujo condicion = CondicionesFlujo[indice];
                CondicionesFlujo.RemoveAt(indice);

                CondicionesFlujo.Insert(indice - 1, condicion);
                ListarCondiciones();
            }
        }

        private void BajarCondicion(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice < CondicionesFlujo.Count - 1)
            {
                CondicionFlujo condicion = CondicionesFlujo[indice];
                CondicionesFlujo.RemoveAt(indice);

                CondicionesFlujo.Insert(indice + 1, condicion);
                ListarCondiciones();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
            ListarCondiciones();
        }

        private void agregarCondiciones_Click(object sender, RoutedEventArgs e)
        {
            
                List<DiseñoOperacion> Operandos_Condiciones = new List<DiseñoOperacion>();
                List<DiseñoElementoOperacion> SubOperandos_Condiciones = new List<DiseñoElementoOperacion>();
                Operandos_Condiciones.AddRange(Operandos);

                if (ModoOperacion)
                {
                    SubOperandos.AddRange(SubOperandos);
                }

                CondicionesFlujo.Add(new CondicionFlujo() {  ContenedorCondiciones = true });
                CondicionesFlujo.Last().Operandos_AplicarCondiciones.AddRange(Operandos_Condiciones);
                CondicionesFlujo.Last().SubOperandos_AplicarCondiciones.AddRange(SubOperandos_Condiciones);

                ListarCondiciones();
            
        }
    }
}
