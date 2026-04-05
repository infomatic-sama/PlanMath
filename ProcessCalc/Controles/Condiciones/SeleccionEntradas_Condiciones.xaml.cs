using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
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
    /// Lógica de interacción para SeleccionEntradas_Condiciones.xaml
    /// </summary>
    public partial class SeleccionEntradas_Condiciones : UserControl
    {
        public List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida> AsociacionesCondicionesEntradas_ElementosSalida { get; set; }
        public List<CondicionTextosInformacion> CondicionesEntradas { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public SeleccionarEntradasCondiciones VistaEntrada { get; set; }
        public List<Entrada> Entradas { get; set; }
        bool modoSeleccionEntradas;
        public bool ModoSeleccionEntradas
        {
            set
            {
                if (value)
                {
                    agregarCondiciones.Visibility = Visibility.Collapsed;
                    agregarCondiciones_Entradas_SeleccionEntradas.Visibility = Visibility.Visible;
                    agregarCondiciones_SeleccionEntradas.Visibility = Visibility.Visible;
                }

                modoSeleccionEntradas = value;
            }

            get
            {
                return modoSeleccionEntradas;
            }
        }
        public DiseñoOperacion ElementoAsociado { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public SeleccionEntradas_Condiciones()
        {
            InitializeComponent();
            AsociacionesCondicionesEntradas_ElementosSalida = new List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida>();
        }

        public void ListarCondiciones()
        {
            if (CondicionesEntradas != null)
            {
                condicionesSeleccionadas.Children.Clear();

                int indice = 0;

                foreach (var item in CondicionesEntradas)
                {
                    condicionesSeleccionadas.RowDefinitions.Add(new RowDefinition());
                    condicionesSeleccionadas.RowDefinitions.Last().Height = GridLength.Auto;

                    TextBlock numero = new TextBlock();
                    numero.Text = (indice + 1).ToString();
                    numero.Margin = new Thickness(10);

                    condicionesSeleccionadas.Children.Add(numero);

                    Grid.SetRow(numero, indice);
                    Grid.SetColumn(numero, 0);

                    ScrollViewer contenedorCondiciones = new ScrollViewer();
                    contenedorCondiciones.MaxWidth = 400;
                    contenedorCondiciones.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    contenedorCondiciones.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    contenedorCondiciones.VerticalAlignment = VerticalAlignment.Center;

                    OpcionCondiciones_TextosInformacion condiciones = new OpcionCondiciones_TextosInformacion();
                    condiciones.ElementoAsociado = ElementoAsociado;
                    condiciones.Entradas = Entradas;
                    condiciones.Condiciones = item;
                    condiciones.Tag = indice;
                    //condiciones.asignacion = item;
                    condiciones.Operandos = Operandos;
                    condiciones.ListaElementos = ListaElementos;
                    condiciones.DefinicionesListas = DefinicionesListas;
                    //condiciones.SubOperandos = SubOperandos;
                    condiciones.Margin = new Thickness(10);
                    condiciones.VerticalAlignment = VerticalAlignment.Center;
                    condiciones.ModoSeleccionEntradas = item.ModoSeleccionEntradas;

                    contenedorCondiciones.Content = condiciones;

                    condicionesSeleccionadas.Children.Add(contenedorCondiciones);

                    Grid.SetRow(contenedorCondiciones, indice);
                    Grid.SetColumn(contenedorCondiciones, 1);

                    //Button botonOpcionAsignacionOperandos = new Button();
                    //botonOpcionAsignacionOperandos.Margin = new Thickness(10);
                    //botonOpcionAsignacionOperandos.MaxHeight = 70;
                    //botonOpcionAsignacionOperandos.VerticalAlignment = VerticalAlignment.Center;
                    ////comboOpcionAsignacion.Tag = new object[] { indice, };
                    //ListarOpcionesBotonTextoAsignacionOperandos(botonOpcionAsignacionOperandos, item);

                    //condicionesSeleccionadas.Children.Add(botonOpcionAsignacionOperandos);

                    //Grid.SetRow(botonOpcionAsignacionOperandos, indice);
                    //Grid.SetColumn(botonOpcionAsignacionOperandos, 2);

                        TextBlock etiquetaElementoSalida = new TextBlock();
                        etiquetaElementoSalida.Text = "Comenzar con: ";
                        etiquetaElementoSalida.Margin = new Thickness(10);

                        StackPanel asociacionTextoInformacionElementoSalida = new StackPanel();
                        asociacionTextoInformacionElementoSalida.Children.Add(etiquetaElementoSalida);

                        Button botonElementosSalida = null;

                        //if (ModoOperacion)
                        //{
                        //    Button botonOpcionesElementosSalida = new Button();
                        //    botonOpcionesElementosSalida.Margin = new Thickness(10);
                        //    botonOpcionesElementosSalida.MaxHeight = 70;
                        //    botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                        //    ListarOpcionesBotonElementosSalida(botonOpcionesElementosSalida, condiciones);
                        //    asociacionTextoInformacionElementoSalida.Children.Add(botonOpcionesElementosSalida);
                        //    botonElementosSalida = botonOpcionesElementosSalida;
                        //}
                        //else
                        //{
                            Button botonOpcionesElementosSalida_Operacion = new Button();
                            botonOpcionesElementosSalida_Operacion.Margin = new Thickness(10);
                            botonOpcionesElementosSalida_Operacion.MaxHeight = 70;
                            botonOpcionesElementosSalida_Operacion.VerticalAlignment = VerticalAlignment.Center;
                            ListarOpcionesBotonElementosSalida_Operacion(botonOpcionesElementosSalida_Operacion, condiciones);
                            asociacionTextoInformacionElementoSalida.Children.Add(botonOpcionesElementosSalida_Operacion);
                            botonElementosSalida = botonOpcionesElementosSalida_Operacion;
                        //}

                        //Grid grillaOrdenamientoAlfabetico = new Grid();

                        //grillaOrdenamientoAlfabetico.RowDefinitions.Add(new RowDefinition());
                        //grillaOrdenamientoAlfabetico.RowDefinitions.Last().Height = GridLength.Auto;
                        //grillaOrdenamientoAlfabetico.RowDefinitions.Add(new RowDefinition());
                        //grillaOrdenamientoAlfabetico.RowDefinitions.Last().Height = GridLength.Auto;
                        //grillaOrdenamientoAlfabetico.RowDefinitions.Add(new RowDefinition());
                        //grillaOrdenamientoAlfabetico.RowDefinitions.Last().Height = GridLength.Auto;

                        //grillaOrdenamientoAlfabetico.ColumnDefinitions.Add(new ColumnDefinition());
                        //grillaOrdenamientoAlfabetico.ColumnDefinitions.Last().Width = GridLength.Auto;
                        //grillaOrdenamientoAlfabetico.ColumnDefinitions.Add(new ColumnDefinition());
                        //grillaOrdenamientoAlfabetico.ColumnDefinitions.Last().Width = GridLength.Auto;

                        //CheckBox opcionOrdenamiento = new CheckBox();
                        //opcionOrdenamiento.Content = "Ordenar automáticamente los números en las salidas";
                        //opcionOrdenamiento.Margin = new Thickness(5);
                        //opcionOrdenamiento.Checked += OpcionOrdenamiento_Checked;
                        //opcionOrdenamiento.Unchecked += OpcionOrdenamiento_Unchecked;

                        //if (ModoOperacion)
                        //    opcionOrdenamiento.Click += OpcionOrdenamiento_Click;
                        //else
                        //    opcionOrdenamiento.Click += OpcionOrdenamientoOperacion_Click;

                        //grillaOrdenamientoAlfabetico.Children.Add(opcionOrdenamiento);
                        //Grid.SetRow(opcionOrdenamiento, 0);
                        //Grid.SetColumn(opcionOrdenamiento, 0);
                        //Grid.SetColumnSpan(opcionOrdenamiento, 2);

                        //TextBlock textoOrdenamiento = new TextBlock();
                        //textoOrdenamiento.Text = "Ordenar por:";
                        //textoOrdenamiento.Margin = new Thickness(5);
                        //grillaOrdenamientoAlfabetico.Children.Add(textoOrdenamiento);
                        //Grid.SetRow(textoOrdenamiento, 1);
                        //Grid.SetColumn(textoOrdenamiento, 0);

                        //ComboBox opcionesOrdenamiento = new ComboBox();
                        //opcionesOrdenamiento.ItemsSource = ListarOpcionesTextosInformacionOrdenamiento();
                        //opcionesOrdenamiento.Margin = new Thickness(5);
                        //opcionesOrdenamiento.Tag = item.Condiciones;
                        //grillaOrdenamientoAlfabetico.Children.Add(opcionesOrdenamiento);
                        //Grid.SetRow(opcionesOrdenamiento, 1);
                        //Grid.SetColumn(opcionesOrdenamiento, 1);
                        //opcionesOrdenamiento.SelectionChanged += OpcionesOrdenamiento_SelectionChanged;

                        //RadioButton opcionAscendente = new RadioButton();
                        //opcionAscendente.Margin = new Thickness(5);
                        //opcionAscendente.Tag = item;
                        //opcionAscendente.Content = "De forma ascendente";
                        //opcionAscendente.Checked += OpcionAscendente_Checked;
                        //opcionAscendente.Unchecked += OpcionAscendente_Unchecked;
                        //grillaOrdenamientoAlfabetico.Children.Add(opcionAscendente);
                        //Grid.SetRow(opcionAscendente, 2);
                        //Grid.SetColumn(opcionAscendente, 0);
                        //opcionAscendente.IsChecked = item.Condiciones.OrdenamientoSalidasAscendente;

                        //RadioButton opcionDescendente = new RadioButton();
                        //opcionDescendente.Margin = new Thickness(5);
                        //opcionDescendente.Tag = item;
                        //opcionDescendente.Content = "De forma descendente";
                        //opcionDescendente.Checked += OpcionDescendente_Checked;
                        //opcionDescendente.Unchecked += OpcionDescendente_Unchecked;
                        //grillaOrdenamientoAlfabetico.Children.Add(opcionDescendente);
                        //Grid.SetRow(opcionDescendente, 2);
                        //Grid.SetColumn(opcionDescendente, 1);
                        //opcionDescendente.IsChecked = item.Condiciones.OrdenamientoSalidasDescendente;

                        //asociacionTextoInformacionElementoSalida.Children.Add(grillaOrdenamientoAlfabetico);
                        //opcionOrdenamiento.Tag = new object[] { item, textoOrdenamiento, opcionesOrdenamiento, botonElementosSalida, condiciones, opcionAscendente, opcionDescendente };
                        //opcionOrdenamiento.IsChecked = item.Condiciones.OrdenarAlfabeticamenteNumerosSalidas;

                        //if (opcionOrdenamiento.IsChecked == true)
                        //    OpcionOrdenamiento_Checked(opcionOrdenamiento, null);
                        //else
                        //    OpcionOrdenamiento_Unchecked(opcionOrdenamiento, null);

                        //opcionesOrdenamiento.SelectedItem = (from ComboBoxItem I in opcionesOrdenamiento.Items where I.Uid == ((int)item.Condiciones.Tipo_OrdenamientoNumerosSalidas).ToString() select I).FirstOrDefault();

                        condicionesSeleccionadas.Children.Add(asociacionTextoInformacionElementoSalida);

                        Grid.SetRow(asociacionTextoInformacionElementoSalida, indice);
                        Grid.SetColumn(asociacionTextoInformacionElementoSalida, 2);
                    

                    Image ImagenBotonQuitar = new Image();
                    ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\06.png", UriKind.Relative));
                    ImagenBotonQuitar.Width = 24;
                    ImagenBotonQuitar.Height = 24;

                    Button botonQuitar = new Button();
                    botonQuitar.Content = ImagenBotonQuitar;
                    botonQuitar.Margin = new Thickness(10);
                    botonQuitar.MaxHeight = 70;
                    botonQuitar.Tag = indice;
                    botonQuitar.Click += QuitarTexto;

                    condicionesSeleccionadas.Children.Add(botonQuitar);

                    Grid.SetRow(botonQuitar, indice);
                    Grid.SetColumn(botonQuitar, 3);

                    Image ImagenBotonSubir = new Image();
                    ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\07.png", UriKind.Relative));
                    ImagenBotonSubir.Width = 24;
                    ImagenBotonSubir.Height = 24;

                    Button botonSubir = new Button();
                    botonSubir.Content = ImagenBotonSubir;
                    botonSubir.Margin = new Thickness(10);
                    botonSubir.MaxHeight = 70;
                    botonSubir.Tag = indice;
                    botonSubir.Click += SubirTexto;

                    condicionesSeleccionadas.Children.Add(botonSubir);

                    Grid.SetRow(botonSubir, indice);
                    Grid.SetColumn(botonSubir, 4);

                    Image ImagenBotonBajar = new Image();
                    ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\08.png", UriKind.Relative));
                    ImagenBotonBajar.Width = 24;
                    ImagenBotonBajar.Height = 24;

                    Button botonBajar = new Button();
                    botonBajar.Content = ImagenBotonBajar;
                    botonBajar.Margin = new Thickness(10);
                    botonBajar.MaxHeight = 70;
                    botonBajar.Tag = indice;
                    botonBajar.Click += BajarTexto;

                    condicionesSeleccionadas.Children.Add(botonBajar);

                    Grid.SetRow(botonBajar, indice);
                    Grid.SetColumn(botonBajar, 5);

                    indice++;
                }
            }
        }

        private void ListarOpcionesBotonElementosSalida_Operacion(Button boton, OpcionCondiciones_TextosInformacion textoInformacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            menuBoton.Tag = boton;
            boton.Content = "Seleccionar entradas como salida";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            ListarOpcionesMenuBotonElementosSalida_Operacion(textoInformacion, menuBoton);
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void ListarOpcionesMenuBotonElementosSalida_Operacion(OpcionCondiciones_TextosInformacion textoInformacion,
            ContextMenu menuBoton)
        {
            menuBoton.Items.Clear();

            //List<DiseñoOperacion> Operandos_ModoManual = new List<DiseñoOperacion>();
            //Operandos_ModoManual.AddRange(VistaEntrada.OperacionRelacionada.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Where(i => i.ModoManual).Select(i => i.ElementoSalida_Operacion));

            if (Operandos != null)
            {
                //int orden = 0;
                List<DiseñoOperacion> ListaOperandos = new List<DiseñoOperacion>();

                if (ElementoAsociado != null)
                    ListaOperandos = Operandos.Intersect(ElementoAsociado.ElementosPosteriores).ToList();
                else
                    ListaOperandos = Operandos;

                foreach (var itemSalida in 
                    ListaOperandos.OrderByDescending(i => AsociacionesCondicionesEntradas_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.Condiciones == textoInformacion.Condiciones))
                    //.ThenBy(i => AsociacionesCondicionesEntradas_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                    //    item.Condiciones == textoInformacion.Condiciones) ? AsociacionesCondicionesEntradas_ElementosSalida.IndexOf(AsociacionesCondicionesEntradas_ElementosSalida.Where(j => j.ElementoSalida_Operacion == i &&
                    //    j.Condiciones == textoInformacion.Condiciones).FirstOrDefault()) :
                    //    Operandos.IndexOf(i))
                    )
                {
                    //if (textoInformacion.Condiciones.OrdenarAlfabeticamenteNumerosSalidas &&
                    //    AsociacionesCondicionesEntradas_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                    //    item.Condiciones == textoInformacion.Condiciones))
                    //{
                    //    orden++;

                    //    Grid grillaOrdenamiento = new Grid();
                    //    grillaOrdenamiento.RowDefinitions.Add(new RowDefinition());
                    //    grillaOrdenamiento.RowDefinitions.Last().Height = GridLength.Auto;
                    //    grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                    //    grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                    //    grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                    //    grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                    //    grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                    //    grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                    //    grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                    //    grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                    //    grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                    //    grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                    //    grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                    //    grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                    //    TextBlock textoOrden = new TextBlock();
                    //    textoOrden.Margin = new Thickness(5);
                    //    textoOrden.Text = "Orden: " + orden.ToString();
                    //    textoOrden.VerticalAlignment = VerticalAlignment.Center;
                    //    grillaOrdenamiento.Children.Add(textoOrden);
                    //    Grid.SetRow(textoOrden, 0);
                    //    Grid.SetColumn(textoOrden, 0);

                    //    ComboBox botonOpcionesElementosSalida = new ComboBox();
                    //    botonOpcionesElementosSalida.Margin = new Thickness(10);
                    //    botonOpcionesElementosSalida.MaxHeight = 70;
                    //    botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                    //    botonOpcionesElementosSalida.Visibility = Visibility.Collapsed;

                    //    botonOpcionesElementosSalida.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                    //        item.CondicionesAsociadas == textoInformacion.Condiciones);

                    //    ListarOpcionesBotonElementosSalida_DentroOperacion(botonOpcionesElementosSalida, itemSalida,
                    //        AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                    //    item.CondicionesAsociadas == textoInformacion.Condiciones));

                    //    grillaOrdenamiento.Children.Add(botonOpcionesElementosSalida);

                    //    Grid.SetRow(botonOpcionesElementosSalida, 0);
                    //    Grid.SetColumn(botonOpcionesElementosSalida, 2);

                    //    CheckBox checkElementoSalida = new CheckBox();
                    //    checkElementoSalida.Content = itemSalida.NombreCombo;
                    //    checkElementoSalida.Tag = new object[] { textoInformacion, itemSalida, menuBoton, botonOpcionesElementosSalida };

                    //    checkElementoSalida.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                    //    item.CondicionesAsociadas == textoInformacion.Condiciones);

                    //    if (checkElementoSalida.IsChecked == true)
                    //    {
                    //        if (!itemSalida.DefinicionSimple_Operacion)
                    //        {
                    //            botonOpcionesElementosSalida.Visibility = Visibility.Visible;
                    //        }
                    //    }

                    //    checkElementoSalida.Checked += CheckElementoSalida_Checked1;
                    //    checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked1;
                    //    checkElementoSalida.Click += CheckElementoSalidaOperacion_Click;

                    //    checkElementoSalida.VerticalAlignment = VerticalAlignment.Center;
                    //    grillaOrdenamiento.Children.Add(checkElementoSalida);
                    //    Grid.SetRow(checkElementoSalida, 0);
                    //    Grid.SetColumn(checkElementoSalida, 1);

                    //    StackPanel panelCantidadTextosInformacion = new StackPanel();
                    //    panelCantidadTextosInformacion.Margin = new Thickness(20, 0, 0, 0);
                    //    panelCantidadTextosInformacion.Orientation = Orientation.Horizontal;

                    //    TextBlock textoCantidadTextosInformacion = new TextBlock();
                    //    textoCantidadTextosInformacion.Inlines.Add("Conjuntos de textos de información");
                    //    textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                    //    textoCantidadTextosInformacion.Inlines.Add("distintos antes de continuar");
                    //    textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                    //    textoCantidadTextosInformacion.Inlines.Add("con el siguiente elemento de salida:");
                    //    textoCantidadTextosInformacion.Margin = new Thickness(2);


                    //    TextBox cantidadTextosInformacion = new TextBox();
                    //    cantidadTextosInformacion.Margin = new Thickness(2);
                    //    cantidadTextosInformacion.Width = 50;
                    //    cantidadTextosInformacion.MaxWidth = 50;
                    //    cantidadTextosInformacion.TextAlignment = TextAlignment.Center;
                    //    cantidadTextosInformacion.HorizontalContentAlignment = HorizontalAlignment.Center;
                    //    cantidadTextosInformacion.VerticalContentAlignment = VerticalAlignment.Center;
                    //    cantidadTextosInformacion.Tag = new object[] { textoInformacion, itemSalida };

                    //    if (AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                    //    item.CondicionesAsociadas == textoInformacion.Condiciones))
                    //    {
                    //        var asociacion = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                    //    item.CondicionesAsociadas == textoInformacion.Condiciones);

                    //        if (asociacion != null)
                    //        {
                    //            cantidadTextosInformacion.Text = asociacion.CantidadConjuntosTextosInformacion.ToString();
                    //        }
                    //    }
                    //    else
                    //        cantidadTextosInformacion.Text = "1";

                    //    cantidadTextosInformacion.TextChanged += CantidadTextosInformacion_TextChanged2;

                    //    panelCantidadTextosInformacion.Children.Add(textoCantidadTextosInformacion);
                    //    panelCantidadTextosInformacion.Children.Add(cantidadTextosInformacion);

                    //    grillaOrdenamiento.Children.Add(panelCantidadTextosInformacion);

                    //    Grid.SetRow(panelCantidadTextosInformacion, 0);
                    //    Grid.SetColumn(panelCantidadTextosInformacion, 3);

                    //    //Image ImagenBotonSubir = new Image();
                    //    ////La imagen es la que tiene que ser
                    //    //ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_01.png", UriKind.Relative));
                    //    //ImagenBotonSubir.Width = 16;
                    //    //ImagenBotonSubir.Height = 16;

                    //    Button botonSubir = new Button();
                    //    //botonSubir.Content = ImagenBotonSubir;
                    //    botonSubir.Content = "Subir";
                    //    botonSubir.Margin = new Thickness(10);
                    //    botonSubir.Tag = new object[] { textoInformacion, menuBoton, itemSalida };
                    //    botonSubir.Click += BotonSubir_Click1;
                    //    botonSubir.VerticalAlignment = VerticalAlignment.Center;

                    //    grillaOrdenamiento.Children.Add(botonSubir);

                    //    Grid.SetRow(botonSubir, 0);
                    //    Grid.SetColumn(botonSubir, 4);

                    //    //Image ImagenBotonBajar = new Image();
                    //    ////La imagen es la que tiene que ser
                    //    //ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_02.png", UriKind.Relative));
                    //    //ImagenBotonBajar.Width = 16;
                    //    //ImagenBotonBajar.Height = 16;

                    //    Button botonBajar = new Button();
                    //    //botonBajar.Content = ImagenBotonBajar;
                    //    botonBajar.Content = "Bajar";
                    //    botonBajar.Margin = new Thickness(10);
                    //    botonBajar.Tag = new object[] { textoInformacion, menuBoton, itemSalida };
                    //    botonBajar.Click += BotonBajar_Click1;
                    //    botonBajar.VerticalAlignment = VerticalAlignment.Center;

                    //    grillaOrdenamiento.Children.Add(botonBajar);

                    //    Grid.SetRow(botonBajar, 0);
                    //    Grid.SetColumn(botonBajar, 5);

                    //    MenuItem opcionElementoSalida = new MenuItem();
                    //    grillaOrdenamiento.VerticalAlignment = VerticalAlignment.Center;
                    //    opcionElementoSalida.Header = grillaOrdenamiento;
                    //    menuBoton.Items.Add(opcionElementoSalida);
                    //}
                    //else
                    //{
                        MenuItem opcionElementoSalida = new MenuItem();

                        StackPanel stackOpciones = new StackPanel();
                        stackOpciones.Orientation = Orientation.Horizontal;

                        menuBoton.Items.Add(stackOpciones);

                        //ComboBox botonOpcionesElementosSalida = new ComboBox();
                        //botonOpcionesElementosSalida.Margin = new Thickness(10);
                        //botonOpcionesElementosSalida.MaxHeight = 70;
                        //botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                        //botonOpcionesElementosSalida.Visibility = Visibility.Collapsed;

                        //botonOpcionesElementosSalida.Tag = AsociacionesCondicionesEntradas_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        //    item.Condiciones == textoInformacion.Condiciones);

                        //ListarOpcionesBotonElementosSalida_DentroOperacion(botonOpcionesElementosSalida, itemSalida,
                        //    AsociacionesCondicionesEntradas_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        //item.Condiciones == textoInformacion.Condiciones));

                        CheckBox checkElementoSalida = new CheckBox();
                        checkElementoSalida.Content = itemSalida.NombreCombo;
                        checkElementoSalida.Tag = new object[] { textoInformacion, itemSalida, menuBoton };

                        checkElementoSalida.IsChecked = AsociacionesCondicionesEntradas_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.Condiciones == textoInformacion.Condiciones);

                        //if (checkElementoSalida.IsChecked == true)
                        //{
                        //    if (!itemSalida.DefinicionSimple_Operacion)
                        //    {
                        //        botonOpcionesElementosSalida.Visibility = Visibility.Visible;
                        //    }
                        //}

                        checkElementoSalida.Checked += CheckElementoSalida_Checked1;
                        checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked1;
                        checkElementoSalida.Click += CheckElementoSalidaOperacion_Click;

                        stackOpciones.Children.Add(checkElementoSalida);
                        //stackOpciones.Children.Add(botonOpcionesElementosSalida);

                        //opcionElementoSalida.Header = stackOpciones;                        
                    //}
                }
            }
        }

        private void CheckElementoSalida_Checked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];
                Button boton = (Button)((ContextMenu)objetos[2]).Tag;

                if (check.IsChecked == true)
                {
                    AsociacionCondicionTextosInformacion_Entradas_ElementoSalida asociacion = new AsociacionCondicionTextosInformacion_Entradas_ElementoSalida();
                    asociacion.ElementoSalida_Operacion = elementoSalida;
                    asociacion.Condiciones = condiciones.Condiciones;
                    AsociacionesCondicionesEntradas_ElementosSalida.Add(asociacion);

                    VistaEntrada.ListarOperandos();
                    VistaEntrada.listaCondiciones.ListarCondiciones();
                    boton.ContextMenu.IsOpen = false;

                    //if (!elementoSalida.DefinicionSimple_Operacion)
                    //{
                    //    botonElementosInternos.Visibility = Visibility.Visible;
                    //}
                }
            }
        }

        private void CheckElementoSalida_Unchecked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];
                Button boton = (Button)((ContextMenu)objetos[2]).Tag;

                if (check.IsChecked == false)
                {
                    AsociacionCondicionTextosInformacion_Entradas_ElementoSalida asociacion = (from A in AsociacionesCondicionesEntradas_ElementosSalida
                                                                                               where
                                      A.Condiciones == condiciones.Condiciones &
                                      A.ElementoSalida_Operacion == elementoSalida &
                                      !A.ModoManual
                                                                                               select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        AsociacionesCondicionesEntradas_ElementosSalida.Remove(asociacion);

                        VistaEntrada.ListarOperandos();
                        VistaEntrada.listaCondiciones.ListarCondiciones();
                        boton.ContextMenu.IsOpen = false;
                    }

                    //if (!elementoSalida.DefinicionSimple_Operacion)
                    //{
                    //    botonElementosInternos.Visibility = Visibility.Collapsed;
                    //}
                }
            }
        }

        private void CheckElementoSalidaOperacion_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;
                ListarOpcionesMenuBotonElementosSalida_Operacion((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[2]);
            }
        }

        private void QuitarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            var asociaciones = (from E in AsociacionesCondicionesEntradas_ElementosSalida where E.Condiciones == CondicionesEntradas[indice] select E).ToList();

            while (asociaciones.Any())
            {
                AsociacionesCondicionesEntradas_ElementosSalida.Remove(asociaciones.FirstOrDefault());
                asociaciones.Remove(asociaciones.FirstOrDefault());

                VistaEntrada.ListarOperandos();
                VistaEntrada.listaCondiciones.ListarCondiciones();
            }

            CondicionesEntradas.RemoveAt(indice);
            ListarCondiciones();
        }

        private void SubirTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice > 0)
            {
                CondicionTextosInformacion condiciones = CondicionesEntradas[indice];
                CondicionesEntradas.RemoveAt(indice);

                CondicionesEntradas.Insert(indice - 1, condiciones);
                ListarCondiciones();

                VistaEntrada.ListarOperandos();
                VistaEntrada.listaCondiciones.ListarCondiciones();
            }
        }

        private void BajarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice < CondicionesEntradas.Count - 1)
            {
                CondicionTextosInformacion condiciones = CondicionesEntradas[indice];
                CondicionesEntradas.RemoveAt(indice);

                CondicionesEntradas.Insert(indice + 1, condiciones);
                ListarCondiciones();

                VistaEntrada.ListarOperandos();
                VistaEntrada.listaCondiciones.ListarCondiciones();
            }
        }

        private void agregarCondiciones_Click(object sender, RoutedEventArgs e)
        {
            
            List<DiseñoOperacion> Operandos_Condiciones = new List<DiseñoOperacion>();
            List<DiseñoElementoOperacion> SubOperandos_Condiciones = new List<DiseñoElementoOperacion>();
            Operandos_Condiciones.AddRange(Operandos);

            //if (ModoOperacion)
            //{
            //    SubOperandos_Condiciones.AddRange(SubOperandos);
            //}

            //CondicionesEntradas.Add(new CondicionesAsignacionSalidas_TextosInformacion()
            //{
            //    Condiciones = condicionesEntradasSalida.Condiciones.ReplicarObjeto(),
            //    Operandos_AplicarCondiciones = Operandos_Condiciones,
            //    SubOperandos_AplicarCondiciones = SubOperandos_Condiciones
            //});
            CondicionesEntradas.Add(new CondicionTextosInformacion() { ContenedorCondiciones = true });
            ListarCondiciones();

            VistaEntrada.ListarOperandos();
            VistaEntrada.listaCondiciones.ListarCondiciones();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void agregarCondiciones_Entradas_SeleccionEntradas_Click(object sender, RoutedEventArgs e)
        {
            List<DiseñoOperacion> Operandos_Condiciones = new List<DiseñoOperacion>();
            List<DiseñoElementoOperacion> SubOperandos_Condiciones = new List<DiseñoElementoOperacion>();
            Operandos_Condiciones.AddRange(Operandos);

            //if (ModoOperacion)
            //{
            //    SubOperandos_Condiciones.AddRange(SubOperandos);
            //}

            //CondicionesEntradas.Add(new CondicionesAsignacionSalidas_TextosInformacion()
            //{
            //    Condiciones = condicionesEntradasSalida.Condiciones.ReplicarObjeto(),
            //    Operandos_AplicarCondiciones = Operandos_Condiciones,
            //    SubOperandos_AplicarCondiciones = SubOperandos_Condiciones
            //});
            CondicionesEntradas.Add(new CondicionTextosInformacion() { ContenedorCondiciones = true, ModoSeleccionEntradas = true });
            ListarCondiciones();

            VistaEntrada.ListarOperandos();
            VistaEntrada.listaCondiciones.ListarCondiciones();
        }
    }
}
