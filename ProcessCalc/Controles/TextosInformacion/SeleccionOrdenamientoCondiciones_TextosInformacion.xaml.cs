using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas.Definiciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Lógica de interacción para SeleccionOrdenamientoCondiciones_TextosInformacion.xaml
    /// </summary>
    public partial class SeleccionOrdenamientoCondiciones_TextosInformacion : UserControl
    {
        public List<CondicionesAsignacionSalidas_TextosInformacion> CondicionesTextosInformacion { get; set; }
        public List<AsociacionTextoInformacion_ElementoSalida> AsociacionesTextosInformacion_ElementosSalida { get; set; }
        public List<DiseñoElementoOperacion> ElementosSalida { get; set; }
        public List<DiseñoOperacion> ElementosSalida_Operacion { get; set; }
        public bool ModoOperacion { get; set; }
        public CondicionTextosInformacion Condiciones_AgregarAsignacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool NoMostrarOpcion_ElementosSalida { get; set; }
        public DiseñoOperacion OperacionRelacionada { get; set; }
        public DiseñoElementoOperacion ElementoDiseñoRelacionado { get; set; }
        public List<DiseñoOperacion> Elementos { get; set; }
        public List<AsociacionTextoInformacion_Clasificador> AsociacionesTextosInformacion_Clasificadores { get; set; }
        public List<Clasificador> Clasificadores_Operacion { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        bool Cargando;
        public SeleccionOrdenamientoCondiciones_TextosInformacion()
        {
            InitializeComponent();
            AsociacionesTextosInformacion_ElementosSalida = new List<AsociacionTextoInformacion_ElementoSalida>();
            AsociacionesTextosInformacion_Clasificadores = new List<AsociacionTextoInformacion_Clasificador>();
        }

        private void ListarTextos()
        {
            if (CondicionesTextosInformacion != null)
            {
                textosInformacionSeleccionados.Children.Clear();

                int indice = 0;

                foreach (var item in CondicionesTextosInformacion)
                {
                    textosInformacionSeleccionados.RowDefinitions.Add(new RowDefinition());
                    textosInformacionSeleccionados.RowDefinitions.Last().Height = GridLength.Auto;

                    TextBlock numero = new TextBlock();
                    numero.Text = (indice + 1).ToString();
                    numero.Margin = new Thickness(10);

                    textosInformacionSeleccionados.Children.Add(numero);

                    Grid.SetRow(numero, indice);
                    Grid.SetColumn(numero, 0);
                                        
                    ScrollViewer contenedorCondiciones = new ScrollViewer();
                    contenedorCondiciones.MaxWidth = 400;
                    contenedorCondiciones.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    contenedorCondiciones.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    contenedorCondiciones.VerticalAlignment = VerticalAlignment.Center;

                    OpcionCondiciones_TextosInformacion condiciones = new OpcionCondiciones_TextosInformacion();
                    condiciones.Condiciones = item.Condiciones;
                    condiciones.Tag = indice;
                    condiciones.asignacion = item;
                    condiciones.Operandos = Operandos;
                    condiciones.ListaElementos = Elementos;
                    condiciones.DefinicionesListas = DefinicionesListas;
                    condiciones.SubOperandos = SubOperandos;
                    condiciones.Margin = new Thickness(10);
                    condiciones.VerticalAlignment = VerticalAlignment.Center;

                    contenedorCondiciones.Content = condiciones;

                    textosInformacionSeleccionados.Children.Add(contenedorCondiciones);

                    Grid.SetRow(contenedorCondiciones, indice);
                    Grid.SetColumn(contenedorCondiciones, 1);
                    
                    Button botonOpcionAsignacionOperandos = new Button();
                    botonOpcionAsignacionOperandos.Margin = new Thickness(10);
                    botonOpcionAsignacionOperandos.MaxHeight = 70;
                    botonOpcionAsignacionOperandos.VerticalAlignment = VerticalAlignment.Center;
                    //comboOpcionAsignacion.Tag = new object[] { indice, };
                    ListarOpcionesBotonTextoAsignacionOperandos(botonOpcionAsignacionOperandos, item);

                    textosInformacionSeleccionados.Children.Add(botonOpcionAsignacionOperandos);

                    Grid.SetRow(botonOpcionAsignacionOperandos, indice);
                    Grid.SetColumn(botonOpcionAsignacionOperandos, 2);

                    if (!NoMostrarOpcion_ElementosSalida)
                    {

                        TextBlock etiquetaElementoSalida = new TextBlock();
                        etiquetaElementoSalida.Text = "Agrupar en la variable o vector retornados: ";
                        etiquetaElementoSalida.Margin = new Thickness(10);

                        StackPanel asociacionTextoInformacionElementoSalida = new StackPanel();
                        asociacionTextoInformacionElementoSalida.Children.Add(etiquetaElementoSalida);

                        Button botonElementosSalida = null;

                        if (ModoOperacion)
                        {
                            Button botonOpcionesElementosSalida = new Button();
                            botonOpcionesElementosSalida.Margin = new Thickness(10);
                            botonOpcionesElementosSalida.MaxHeight = 70;
                            botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                            ListarOpcionesBotonElementosSalida(botonOpcionesElementosSalida, condiciones);
                            asociacionTextoInformacionElementoSalida.Children.Add(botonOpcionesElementosSalida);
                            botonElementosSalida = botonOpcionesElementosSalida;
                        }
                        else
                        {
                            Button botonOpcionesElementosSalida_Operacion = new Button();
                            botonOpcionesElementosSalida_Operacion.Margin = new Thickness(10);
                            botonOpcionesElementosSalida_Operacion.MaxHeight = 70;
                            botonOpcionesElementosSalida_Operacion.VerticalAlignment = VerticalAlignment.Center;
                            ListarOpcionesBotonElementosSalida_Operacion(botonOpcionesElementosSalida_Operacion, condiciones);
                            asociacionTextoInformacionElementoSalida.Children.Add(botonOpcionesElementosSalida_Operacion);
                            botonElementosSalida = botonOpcionesElementosSalida_Operacion;
                        }

                        Grid grillaOrdenamientoAlfabetico = new Grid();

                        grillaOrdenamientoAlfabetico.RowDefinitions.Add(new RowDefinition());
                        grillaOrdenamientoAlfabetico.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOrdenamientoAlfabetico.RowDefinitions.Add(new RowDefinition());
                        grillaOrdenamientoAlfabetico.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOrdenamientoAlfabetico.RowDefinitions.Add(new RowDefinition());
                        grillaOrdenamientoAlfabetico.RowDefinitions.Last().Height = GridLength.Auto;

                        grillaOrdenamientoAlfabetico.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamientoAlfabetico.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamientoAlfabetico.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamientoAlfabetico.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionOrdenamiento = new CheckBox();
                        opcionOrdenamiento.Content = "Ordenar automáticamente los números en las variables o vectores retornados";
                        opcionOrdenamiento.Margin = new Thickness(5);
                        opcionOrdenamiento.Checked += OpcionOrdenamiento_Checked;
                        opcionOrdenamiento.Unchecked += OpcionOrdenamiento_Unchecked;

                        if(ModoOperacion)
                            opcionOrdenamiento.Click += OpcionOrdenamiento_Click;
                        else
                            opcionOrdenamiento.Click += OpcionOrdenamientoOperacion_Click;

                        grillaOrdenamientoAlfabetico.Children.Add(opcionOrdenamiento);
                        Grid.SetRow(opcionOrdenamiento, 0);
                        Grid.SetColumn(opcionOrdenamiento, 0);
                        Grid.SetColumnSpan(opcionOrdenamiento, 2);

                        TextBlock textoOrdenamiento = new TextBlock();
                        textoOrdenamiento.Text = "Ordenar por:";
                        textoOrdenamiento.Margin = new Thickness(5);
                        grillaOrdenamientoAlfabetico.Children.Add(textoOrdenamiento);
                        Grid.SetRow(textoOrdenamiento, 1);
                        Grid.SetColumn(textoOrdenamiento, 0);

                        ComboBox opcionesOrdenamiento = new ComboBox();
                        opcionesOrdenamiento.ItemsSource = ListarOpcionesTextosInformacionOrdenamiento();
                        opcionesOrdenamiento.Margin = new Thickness(5);
                        opcionesOrdenamiento.Tag = item.Condiciones;
                        grillaOrdenamientoAlfabetico.Children.Add(opcionesOrdenamiento);
                        Grid.SetRow(opcionesOrdenamiento, 1);
                        Grid.SetColumn(opcionesOrdenamiento, 1);
                        opcionesOrdenamiento.SelectionChanged += OpcionesOrdenamiento_SelectionChanged;

                        RadioButton opcionAscendente = new RadioButton();
                        opcionAscendente.Margin = new Thickness(5);
                        opcionAscendente.Tag = item;
                        opcionAscendente.Content = "De forma ascendente";
                        opcionAscendente.Checked += OpcionAscendente_Checked;
                        opcionAscendente.Unchecked += OpcionAscendente_Unchecked;
                        grillaOrdenamientoAlfabetico.Children.Add(opcionAscendente);
                        Grid.SetRow(opcionAscendente, 2);
                        Grid.SetColumn(opcionAscendente, 0);
                        opcionAscendente.IsChecked = item.Condiciones?.OrdenamientoSalidasAscendente;

                        RadioButton opcionDescendente = new RadioButton();
                        opcionDescendente.Margin = new Thickness(5);
                        opcionDescendente.Tag = item;
                        opcionDescendente.Content = "De forma descendente";
                        opcionDescendente.Checked += OpcionDescendente_Checked;
                        opcionDescendente.Unchecked += OpcionDescendente_Unchecked;
                        grillaOrdenamientoAlfabetico.Children.Add(opcionDescendente);
                        Grid.SetRow(opcionDescendente, 2);
                        Grid.SetColumn(opcionDescendente, 1);
                        opcionDescendente.IsChecked = item.Condiciones?.OrdenamientoSalidasDescendente;

                        asociacionTextoInformacionElementoSalida.Children.Add(grillaOrdenamientoAlfabetico);
                        opcionOrdenamiento.Tag = new object[] {item, textoOrdenamiento, opcionesOrdenamiento, botonElementosSalida, condiciones, opcionAscendente, opcionDescendente };
                        opcionOrdenamiento.IsChecked = item.Condiciones?.OrdenarAlfabeticamenteNumerosSalidas;

                        if (opcionOrdenamiento.IsChecked == true)
                            OpcionOrdenamiento_Checked(opcionOrdenamiento, null);
                        else
                            OpcionOrdenamiento_Unchecked(opcionOrdenamiento, null);

                        if(item.Condiciones != null)
                            opcionesOrdenamiento.SelectedItem = (from ComboBoxItem I in opcionesOrdenamiento.Items where I.Uid == ((int)item.Condiciones.Tipo_OrdenamientoNumerosSalidas).ToString() select I).FirstOrDefault();

                        textosInformacionSeleccionados.Children.Add(asociacionTextoInformacionElementoSalida);

                        Grid.SetRow(asociacionTextoInformacionElementoSalida, indice);
                        Grid.SetColumn(asociacionTextoInformacionElementoSalida, 3);
                    }

                    TextBlock etiquetaClasificador = new TextBlock();
                    etiquetaClasificador.Text = "Agrupar en los clasificadores: ";
                    etiquetaClasificador.Margin = new Thickness(10);

                    StackPanel asociacionTextoInformacionClasificador = new StackPanel();
                    asociacionTextoInformacionClasificador.Children.Add(etiquetaClasificador);

                    Button botonOpcionesClasificadores = new Button();
                    botonOpcionesClasificadores.Margin = new Thickness(10);
                    botonOpcionesClasificadores.MaxHeight = 70;
                    botonOpcionesClasificadores.VerticalAlignment = VerticalAlignment.Center;
                    ListarOpcionesBotonClasificadores(botonOpcionesClasificadores, condiciones);
                    asociacionTextoInformacionClasificador.Children.Add(botonOpcionesClasificadores);

                    Grid grillaOrdenamientoAlfabetico_Clasificadores = new Grid();

                    grillaOrdenamientoAlfabetico_Clasificadores.RowDefinitions.Add(new RowDefinition());
                    grillaOrdenamientoAlfabetico_Clasificadores.RowDefinitions.Last().Height = GridLength.Auto;
                    grillaOrdenamientoAlfabetico_Clasificadores.RowDefinitions.Add(new RowDefinition());
                    grillaOrdenamientoAlfabetico_Clasificadores.RowDefinitions.Last().Height = GridLength.Auto;
                    grillaOrdenamientoAlfabetico_Clasificadores.RowDefinitions.Add(new RowDefinition());
                    grillaOrdenamientoAlfabetico_Clasificadores.RowDefinitions.Last().Height = GridLength.Auto;

                    grillaOrdenamientoAlfabetico_Clasificadores.ColumnDefinitions.Add(new ColumnDefinition());
                    grillaOrdenamientoAlfabetico_Clasificadores.ColumnDefinitions.Last().Width = GridLength.Auto;
                    grillaOrdenamientoAlfabetico_Clasificadores.ColumnDefinitions.Add(new ColumnDefinition());
                    grillaOrdenamientoAlfabetico_Clasificadores.ColumnDefinitions.Last().Width = GridLength.Auto;

                    CheckBox opcionOrdenamiento_Clasificadores = new CheckBox();
                    opcionOrdenamiento_Clasificadores.Content = "Ordenar automáticamente los números en los clasificadores";
                    opcionOrdenamiento_Clasificadores.Margin = new Thickness(5);
                    opcionOrdenamiento_Clasificadores.Checked += OpcionOrdenamiento_Clasificadores_Checked;
                    opcionOrdenamiento_Clasificadores.Unchecked += OpcionOrdenamiento_Clasificadores_Unchecked;

                    opcionOrdenamiento_Clasificadores.Click += OpcionOrdenamiento_Clasificadores_Click;

                    grillaOrdenamientoAlfabetico_Clasificadores.Children.Add(opcionOrdenamiento_Clasificadores);
                    Grid.SetRow(opcionOrdenamiento_Clasificadores, 0);
                    Grid.SetColumn(opcionOrdenamiento_Clasificadores, 0);
                    Grid.SetColumnSpan(opcionOrdenamiento_Clasificadores, 2);

                    TextBlock textoOrdenamiento_Clasificadores = new TextBlock();
                    textoOrdenamiento_Clasificadores.Text = "Ordenar por:";
                    textoOrdenamiento_Clasificadores.Margin = new Thickness(5);
                    grillaOrdenamientoAlfabetico_Clasificadores.Children.Add(textoOrdenamiento_Clasificadores);
                    Grid.SetRow(textoOrdenamiento_Clasificadores, 1);
                    Grid.SetColumn(textoOrdenamiento_Clasificadores, 0);

                    ComboBox opcionesOrdenamiento_Clasificadores = new ComboBox();
                    opcionesOrdenamiento_Clasificadores.ItemsSource = ListarOpcionesTextosInformacionOrdenamiento();
                    opcionesOrdenamiento_Clasificadores.Margin = new Thickness(5);
                    opcionesOrdenamiento_Clasificadores.Tag = item.Condiciones;
                    grillaOrdenamientoAlfabetico_Clasificadores.Children.Add(opcionesOrdenamiento_Clasificadores);
                    Grid.SetRow(opcionesOrdenamiento_Clasificadores, 1);
                    Grid.SetColumn(opcionesOrdenamiento_Clasificadores, 1);
                    opcionesOrdenamiento_Clasificadores.SelectionChanged += OpcionesOrdenamiento_Clasificadores_SelectionChanged;

                    RadioButton opcionAscendente_Clasificadores = new RadioButton();
                    opcionAscendente_Clasificadores.Margin = new Thickness(5);
                    opcionAscendente_Clasificadores.Tag = item;
                    opcionAscendente_Clasificadores.Content = "De forma ascendente";
                    opcionAscendente_Clasificadores.Checked += OpcionAscendente_Clasificadores_Checked;
                    opcionAscendente_Clasificadores.Unchecked += OpcionAscendente_Clasificadores_Unchecked;
                    grillaOrdenamientoAlfabetico_Clasificadores.Children.Add(opcionAscendente_Clasificadores);
                    Grid.SetRow(opcionAscendente_Clasificadores, 2);
                    Grid.SetColumn(opcionAscendente_Clasificadores, 0);
                    opcionAscendente_Clasificadores.IsChecked = item.Condiciones?.OrdenamientoClasificadoresAscendente;

                    RadioButton opcionDescendente_Clasificadores = new RadioButton();
                    opcionDescendente_Clasificadores.Margin = new Thickness(5);
                    opcionDescendente_Clasificadores.Tag = item;
                    opcionDescendente_Clasificadores.Content = "De forma descendente";
                    opcionDescendente_Clasificadores.Checked += OpcionDescendente_Clasificadores_Checked;
                    opcionDescendente_Clasificadores.Unchecked += OpcionDescendente_Clasificadores_Unchecked;
                    grillaOrdenamientoAlfabetico_Clasificadores.Children.Add(opcionDescendente_Clasificadores);
                    Grid.SetRow(opcionDescendente_Clasificadores, 2);
                    Grid.SetColumn(opcionDescendente_Clasificadores, 1);
                    opcionDescendente_Clasificadores.IsChecked = item.Condiciones?.OrdenamientoClasificadoresDescendente;

                    asociacionTextoInformacionClasificador.Children.Add(grillaOrdenamientoAlfabetico_Clasificadores);
                    opcionOrdenamiento_Clasificadores.Tag = new object[] { item, textoOrdenamiento_Clasificadores, opcionesOrdenamiento_Clasificadores, condiciones, botonOpcionesClasificadores, opcionAscendente_Clasificadores, opcionDescendente_Clasificadores };
                    opcionOrdenamiento_Clasificadores.IsChecked = item.Condiciones?.OrdenarAlfabeticamenteNumerosClasificadores;

                    if (opcionOrdenamiento_Clasificadores.IsChecked == true)
                        OpcionOrdenamiento_Clasificadores_Checked(opcionOrdenamiento_Clasificadores, null);
                    else
                        OpcionOrdenamiento_Clasificadores_Unchecked(opcionOrdenamiento_Clasificadores, null);

                    if (item.Condiciones != null)
                        opcionesOrdenamiento_Clasificadores.SelectedItem = (from ComboBoxItem I in opcionesOrdenamiento_Clasificadores.Items where I.Uid == ((int)item.Condiciones.Tipo_OrdenamientoNumerosClasificadores).ToString() select I).FirstOrDefault();


                    textosInformacionSeleccionados.Children.Add(asociacionTextoInformacionClasificador);

                    Grid.SetRow(asociacionTextoInformacionClasificador, indice);
                    Grid.SetColumn(asociacionTextoInformacionClasificador, 4);

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

                    textosInformacionSeleccionados.Children.Add(botonQuitar);

                    Grid.SetRow(botonQuitar, indice);
                    Grid.SetColumn(botonQuitar, 5);

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

                    textosInformacionSeleccionados.Children.Add(botonSubir);

                    Grid.SetRow(botonSubir, indice);
                    Grid.SetColumn(botonSubir, 6);

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

                    textosInformacionSeleccionados.Children.Add(botonBajar);

                    Grid.SetRow(botonBajar, indice);
                    Grid.SetColumn(botonBajar, 7);

                    indice++;
                }

                Cargando = true;

                if (CondicionesTextosInformacion.Any(i => i.Condiciones != null && i.Condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores))
                {
                    opcionActivarCondicionCantidadesNocumplen_Anteriores.IsChecked = true;
                }
                else
                {
                    opcionActivarCondicionCantidadesNocumplen_Anteriores.IsChecked = false;
                }

                Cargando = false;
            }
        }

        private void OpcionDescendente_Unchecked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoSalidasDescendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionAscendente_Unchecked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoSalidasAscendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionDescendente_Checked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoSalidasDescendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionAscendente_Checked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if(item != null)
            {
                item.Condiciones.OrdenamientoSalidasAscendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionDescendente_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoClasificadoresDescendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionAscendente_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoClasificadoresAscendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionDescendente_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoClasificadoresDescendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionAscendente_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            CondicionesAsignacionSalidas_TextosInformacion item = (CondicionesAsignacionSalidas_TextosInformacion)((RadioButton)sender).Tag;

            if (item != null)
            {
                item.Condiciones.OrdenamientoClasificadoresAscendente = (bool)((RadioButton)sender).IsChecked;
            }
        }

        private void OpcionOrdenamiento_Click(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;
            ListarOpcionesBotonElementosSalida((Button)elementos[3], (OpcionCondiciones_TextosInformacion)elementos[4]);
        }

        private void OpcionOrdenamiento_Clasificadores_Click(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;
            ListarOpcionesBotonMenuClasificadores((Button)elementos[4], (OpcionCondiciones_TextosInformacion)elementos[3]);
        }

        private void OpcionOrdenamientoOperacion_Click(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;
            ListarOpcionesBotonElementosSalida_Operacion((Button)elementos[3], (OpcionCondiciones_TextosInformacion)elementos[4]);
        }

        private void OpcionesOrdenamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboOrdenamiento = (ComboBox)sender;
            CondicionTextosInformacion condicion = (CondicionTextosInformacion)comboOrdenamiento.Tag;

            if (comboOrdenamiento.SelectedItem != null)
                condicion.Tipo_OrdenamientoNumerosSalidas = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)comboOrdenamiento.SelectedItem).Uid);
        }

        private void OpcionesOrdenamiento_Clasificadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboOrdenamiento = (ComboBox)sender;
            CondicionTextosInformacion condicion = (CondicionTextosInformacion)comboOrdenamiento.Tag;

            if (comboOrdenamiento.SelectedItem != null)
                condicion.Tipo_OrdenamientoNumerosClasificadores = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)comboOrdenamiento.SelectedItem).Uid);
        }

        private List<ComboBoxItem> ListarOpcionesTextosInformacionOrdenamiento()
        {
            List<ComboBoxItem> lista = new List<ComboBoxItem>();
            lista.Add(new ComboBoxItem() { Uid = "1", Content = "Según condiciones (si/entonces) relacionadas" });
            lista.Add(new ComboBoxItem() { Uid = "2", Content = "Nombre de variable o vector y cadenas de texto" });
            lista.Add(new ComboBoxItem() { Uid = "3", Content = "Sólo nombre de la variable o vector" });
            lista.Add(new ComboBoxItem() { Uid = "4", Content = "Sólo cadenas de texto" });

            return lista;
        }

        private void OpcionOrdenamiento_Unchecked(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;

            if (((CheckBox)sender).IsChecked != null)
            {
                MostrarOcultarElementos_OrdenamientoSalidas((bool)((CheckBox)sender).IsChecked, (TextBlock)elementos[1],
                    (ComboBox)elementos[2], (CondicionesAsignacionSalidas_TextosInformacion)elementos[0], (RadioButton)elementos[5],
                    (RadioButton)elementos[6]);
            }
        }

        private void OpcionOrdenamiento_Checked(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;

            MostrarOcultarElementos_OrdenamientoSalidas((bool)((CheckBox)sender).IsChecked, (TextBlock)elementos[1],
                (ComboBox)elementos[2], (CondicionesAsignacionSalidas_TextosInformacion)elementos[0], (RadioButton)elementos[5],
                (RadioButton)elementos[6]);
        }

        private void OpcionOrdenamiento_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;

            if (((CheckBox)sender).IsChecked != null)
            {
                MostrarOcultarElementos_OrdenamientoClasificadores((bool)((CheckBox)sender).IsChecked, (TextBlock)elementos[1],
                    (ComboBox)elementos[2],
                    (CondicionesAsignacionSalidas_TextosInformacion)elementos[0], (RadioButton)elementos[5],
                    (RadioButton)elementos[6]);
            }
        }

        private void OpcionOrdenamiento_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            object[] elementos = (object[])((CheckBox)sender).Tag;

            MostrarOcultarElementos_OrdenamientoClasificadores((bool)((CheckBox)sender).IsChecked, (TextBlock)elementos[1],
                (ComboBox)elementos[2],
                (CondicionesAsignacionSalidas_TextosInformacion)elementos[0], (RadioButton)elementos[5],
                (RadioButton)elementos[6]);
        }

        private void MostrarOcultarElementos_OrdenamientoSalidas(bool ordenar, TextBlock textoOrdenamiento,
            ComboBox opcionesOrdenamiento, CondicionesAsignacionSalidas_TextosInformacion item, RadioButton opcionAscendente,
            RadioButton opcionDescendente)
        {
            item.Condiciones.OrdenarAlfabeticamenteNumerosSalidas = ordenar;

            if(ordenar)
            {
                textoOrdenamiento.Visibility = Visibility.Visible;
                opcionesOrdenamiento.Visibility = Visibility.Visible;
                opcionAscendente.Visibility = Visibility.Visible;
                opcionDescendente.Visibility = Visibility.Visible;
            }
            else
            {
                textoOrdenamiento.Visibility = Visibility.Collapsed;
                opcionesOrdenamiento.Visibility = Visibility.Collapsed;
                opcionAscendente.Visibility = Visibility.Collapsed;
                opcionDescendente.Visibility = Visibility.Collapsed;
            }
        }

        private void MostrarOcultarElementos_OrdenamientoClasificadores(bool ordenar, TextBlock textoOrdenamiento,
            ComboBox opcionesOrdenamiento,
            CondicionesAsignacionSalidas_TextosInformacion item, RadioButton opcionAscendente,
            RadioButton opcionDescendente)
        {
            item.Condiciones.OrdenarAlfabeticamenteNumerosClasificadores = ordenar;

            if (ordenar)
            {
                textoOrdenamiento.Visibility = Visibility.Visible;
                opcionesOrdenamiento.Visibility = Visibility.Visible;
                opcionAscendente.Visibility = Visibility.Visible;
                opcionDescendente.Visibility = Visibility.Visible;
            }
            else
            {
                textoOrdenamiento.Visibility = Visibility.Collapsed;
                opcionesOrdenamiento.Visibility = Visibility.Collapsed;
                opcionAscendente.Visibility = Visibility.Collapsed;
                opcionDescendente.Visibility = Visibility.Collapsed;
            }
        }

        private void ListarOpcionesBotonTextoAsignacionOperandos(Button boton, CondicionesAsignacionSalidas_TextosInformacion itemInstancia)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Aplicar condiciones (si/entonces) a ";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            if (Operandos != null && !ModoOperacion)
            {
                foreach (var itemOperando in Operandos)
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    CheckBox checkOperandos = new CheckBox();
                    checkOperandos.Content = "Variable o vector retornados " + itemOperando.NombreCombo;
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
                foreach (var itemOperando in SubOperandos)
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    CheckBox checkOperandos = new CheckBox();
                    checkOperandos.Content = "Variable o vector retornados " + itemOperando.NombreCombo;
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

                CondicionesAsignacionSalidas_TextosInformacion asignacion = (CondicionesAsignacionSalidas_TextosInformacion)objetos[0];
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

                CondicionesAsignacionSalidas_TextosInformacion asignacion = (CondicionesAsignacionSalidas_TextosInformacion)objetos[0];
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

                CondicionesAsignacionSalidas_TextosInformacion asignacion = (CondicionesAsignacionSalidas_TextosInformacion)objetos[0];
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

                CondicionesAsignacionSalidas_TextosInformacion asignacion = (CondicionesAsignacionSalidas_TextosInformacion)objetos[0];
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    if (asignacion != null)
                        asignacion.SubOperandos_AplicarCondiciones.Remove(operando);
                }
            }
        }
        private void ListarOpcionesBotonElementosSalida(Button boton, OpcionCondiciones_TextosInformacion textoInformacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar variables o vectores retornados";
            boton.ContextMenu = menuBoton;
            boton.Tag = textoInformacion;
            boton.Click += Boton_Click;

            ListarOpcionesMenuBotonElementosSalida(textoInformacion, menuBoton);
        }

        private void ListarOpcionesBotonMenuClasificadores(Button boton, OpcionCondiciones_TextosInformacion textoInformacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar clasificadores";
            boton.ContextMenu = menuBoton;
            boton.Tag = textoInformacion;
            boton.Click += Boton_Click;

            ListarOpcionesMenuBotonClasificadores(textoInformacion, menuBoton);
        }

        private void ListarOpcionesBotonElementosSalida_DentroOperacion(ComboBox combo,
            DiseñoOperacion ElementoOperando, AsociacionTextoInformacion_ElementoSalida asociacion)
        {
            ComboBox menuBoton = new ComboBox();
            combo.Items.Add(new ComboBoxItem() { Content = "Seleccionar variables o vectores retornados", Visibility = Visibility.Collapsed });
            combo.SelectedIndex = 0;
            //boton.ContextMenu = menuBoton;
            combo.Tag = asociacion;
            //boton.Click += Boton_Click;
            
            ListarOpcionesMenuBotonElementosSalida_DentroOperacion(combo, ElementoOperando, asociacion);
        }

        private void ListarOpcionesMenuBotonElementosSalida(OpcionCondiciones_TextosInformacion textoInformacion,
            ContextMenu menuBoton)
        {
            menuBoton.Items.Clear();

            if (ElementosSalida != null)
            {
                int orden = 0;

                foreach (var itemSalida in ElementosSalida.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida == i &&
                        j.CondicionesAsociadas == textoInformacion.Condiciones).FirstOrDefault()) :
                        ElementosSalida.IndexOf(i)))
                {
                    if (textoInformacion.Condiciones.OrdenarAlfabeticamenteNumerosSalidas &&
                        AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                    {
                        orden++;

                        Grid grillaOrdenamiento = new Grid();
                        grillaOrdenamiento.RowDefinitions.Add(new RowDefinition());
                        grillaOrdenamiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        Grid grillaOpcionesCumplimiento = new Grid();
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOpcionesCumplimiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionCumple = new CheckBox();
                        opcionCumple.Margin = new Thickness(5);
                        opcionCumple.Content = "Si las condiciones (si/entonces) se cumplen";
                        opcionCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionCumple.Visibility = Visibility.Collapsed;

                        opcionCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesCumplen == true);

                        opcionCumple.Checked += OpcionCumple_Checked;
                        opcionCumple.Unchecked += OpcionCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionCumple);
                        Grid.SetRow(opcionCumple, 0);
                        Grid.SetColumn(opcionCumple, 0);

                        CheckBox opcionNoCumple = new CheckBox();
                        opcionNoCumple.Margin = new Thickness(5);
                        opcionNoCumple.Content = "Si las condiciones (si/entonces) no se cumplen";
                        opcionNoCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionNoCumple.Visibility = Visibility.Collapsed;

                        opcionNoCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionNoCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesNoCumplen == true);

                        opcionNoCumple.Checked += OpcionNoCumple_Checked;
                        opcionNoCumple.Unchecked += OpcionNoCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionNoCumple);
                        Grid.SetRow(opcionNoCumple, 1);
                        Grid.SetColumn(opcionNoCumple, 0);

                        grillaOrdenamiento.Children.Add(grillaOpcionesCumplimiento);
                        Grid.SetRow(grillaOpcionesCumplimiento, 0);
                        Grid.SetColumn(grillaOpcionesCumplimiento, 0);

                        TextBlock textoOrden = new TextBlock();
                        textoOrden.Margin = new Thickness(5);
                        textoOrden.Text = "Orden: " + orden.ToString();
                        textoOrden.VerticalAlignment = VerticalAlignment.Center;
                        grillaOrdenamiento.Children.Add(textoOrden);
                        Grid.SetRow(textoOrden, 0);
                        Grid.SetColumn(textoOrden, 1);

                        CheckBox checkElementoSalida = new CheckBox();
                        checkElementoSalida.Content = itemSalida.NombreElemento;
                        checkElementoSalida.Tag = new object[] { textoInformacion, itemSalida, menuBoton, opcionCumple, opcionNoCumple };

                        checkElementoSalida.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        if(checkElementoSalida.IsChecked == true)
                        {
                            opcionCumple.Visibility = Visibility.Visible;
                            opcionNoCumple.Visibility = Visibility.Visible;
                        }

                        checkElementoSalida.Checked += CheckElementoSalida_Checked;
                        checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked;
                        checkElementoSalida.Click += CheckElementoSalida_Click;

                        checkElementoSalida.VerticalAlignment = VerticalAlignment.Center;
                        grillaOrdenamiento.Children.Add(checkElementoSalida);
                        Grid.SetRow(checkElementoSalida, 0);
                        Grid.SetColumn(checkElementoSalida, 2);

                        StackPanel panelCantidadTextosInformacion = new StackPanel();
                        panelCantidadTextosInformacion.Margin = new Thickness(20, 0, 0, 0);
                        panelCantidadTextosInformacion.Orientation = Orientation.Horizontal;

                        TextBlock textoCantidadTextosInformacion = new TextBlock();
                        textoCantidadTextosInformacion.Inlines.Add("Vectores de cadenas de texto");
                        textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                        textoCantidadTextosInformacion.Inlines.Add("distintos antes de continuar");
                        textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                        textoCantidadTextosInformacion.Inlines.Add("con el siguiente variable o vector retornados:"); 
                        textoCantidadTextosInformacion.Margin = new Thickness(2);

                        TextBox cantidadTextosInformacion = new TextBox();
                        cantidadTextosInformacion.Margin = new Thickness(2);
                        cantidadTextosInformacion.Width = 50;
                        cantidadTextosInformacion.MaxWidth = 50;
                        cantidadTextosInformacion.TextAlignment = TextAlignment.Center;
                        cantidadTextosInformacion.HorizontalContentAlignment = HorizontalAlignment.Center;
                        cantidadTextosInformacion.VerticalContentAlignment = VerticalAlignment.Center;
                        cantidadTextosInformacion.Tag = new object[] { textoInformacion, itemSalida };

                        if (AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                        {
                            var asociacion = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                            if (asociacion != null)
                            {
                                cantidadTextosInformacion.Text = asociacion.CantidadConjuntosTextosInformacion.ToString();
                            }
                        }
                        else
                            cantidadTextosInformacion.Text = "1";

                        cantidadTextosInformacion.TextChanged += CantidadTextosInformacion_TextChanged;

                        panelCantidadTextosInformacion.Children.Add(textoCantidadTextosInformacion);
                        panelCantidadTextosInformacion.Children.Add(cantidadTextosInformacion);

                        grillaOrdenamiento.Children.Add(panelCantidadTextosInformacion);

                        Grid.SetRow(panelCantidadTextosInformacion, 0);
                        Grid.SetColumn(panelCantidadTextosInformacion, 3);

                        //Image ImagenBotonSubir = new Image();
                        ////La imagen es la que tiene que ser
                        //ImagenBotonSubir.Stretch= Stretch.Fill;
                        //ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_01.png", UriKind.Relative));
                        //ImagenBotonSubir.Width = 16;
                        //ImagenBotonSubir.Height = 16;

                        Button botonSubir = new Button();
                        //botonSubir.Content = ImagenBotonSubir;
                        botonSubir.Content = "Subir";
                        botonSubir.Margin = new Thickness(10);
                        botonSubir.Tag = new object[] { textoInformacion, menuBoton, itemSalida };
                        botonSubir.Click += BotonSubir_Click;
                        botonSubir.VerticalAlignment = VerticalAlignment.Center;

                        grillaOrdenamiento.Children.Add(botonSubir);

                        Grid.SetRow(botonSubir, 0);
                        Grid.SetColumn(botonSubir, 4);

                        //Image ImagenBotonBajar = new Image();
                        ////La imagen es la que tiene que ser
                        //ImagenBotonBajar.Stretch = Stretch.Fill;
                        //ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_02.png", UriKind.Relative));
                        //ImagenBotonBajar.Width = 16;
                        //ImagenBotonBajar.Height = 16;

                        Button botonBajar = new Button();
                        //botonBajar.Content = ImagenBotonBajar;
                        botonBajar.Content = "Bajar";
                        botonBajar.Margin = new Thickness(10);
                        botonBajar.Tag = new object[] { textoInformacion, menuBoton, itemSalida };
                        botonBajar.Click += BotonBajar_Click;
                        botonBajar.VerticalAlignment = VerticalAlignment.Center;

                        grillaOrdenamiento.Children.Add(botonBajar);

                        Grid.SetRow(botonBajar, 0);
                        Grid.SetColumn(botonBajar, 5);

                        MenuItem opcionElementoSalida = new MenuItem();
                        grillaOrdenamiento.VerticalAlignment = VerticalAlignment.Center;
                        opcionElementoSalida.Header = grillaOrdenamiento;
                        menuBoton.Items.Add(opcionElementoSalida);
                    }
                    else
                    {
                        MenuItem opcionElementoSalida = new MenuItem();

                        Grid grillaLista = new Grid();
                        grillaLista.RowDefinitions.Add(new RowDefinition());
                        grillaLista.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaLista.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaLista.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaLista.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaLista.ColumnDefinitions.Last().Width = GridLength.Auto;

                        Grid grillaOpcionesCumplimiento = new Grid();
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOpcionesCumplimiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionCumple = new CheckBox();
                        opcionCumple.Margin = new Thickness(5);
                        opcionCumple.Content = "Si las condiciones (si/entonces) se cumplen";
                        opcionCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionCumple.Visibility = Visibility.Collapsed;

                        opcionCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesCumplen == true);

                        opcionCumple.Checked += OpcionCumple_Checked;
                        opcionCumple.Unchecked += OpcionCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionCumple);
                        Grid.SetRow(opcionCumple, 0);
                        Grid.SetColumn(opcionCumple, 0);

                        CheckBox opcionNoCumple = new CheckBox();
                        opcionNoCumple.Margin = new Thickness(5);
                        opcionNoCumple.Content = "Si las condiciones (si/entonces) no se cumplen";
                        opcionNoCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionNoCumple.Visibility = Visibility.Collapsed;

                        opcionNoCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionNoCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesNoCumplen == true);

                        opcionNoCumple.Checked += OpcionNoCumple_Checked;
                        opcionNoCumple.Unchecked += OpcionNoCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionNoCumple);
                        Grid.SetRow(opcionNoCumple, 1);
                        Grid.SetColumn(opcionNoCumple, 0);

                        grillaLista.Children.Add(grillaOpcionesCumplimiento);
                        Grid.SetRow(grillaOpcionesCumplimiento, 0);
                        Grid.SetColumn(grillaOpcionesCumplimiento, 0);

                        CheckBox checkElementoSalida = new CheckBox();
                        checkElementoSalida.Content = itemSalida.NombreElemento;
                        checkElementoSalida.Tag = new object[] { textoInformacion, itemSalida, menuBoton, opcionCumple, opcionNoCumple };

                        checkElementoSalida.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        if (checkElementoSalida.IsChecked == true)
                        {
                            opcionCumple.Visibility = Visibility.Visible;
                            opcionNoCumple.Visibility = Visibility.Visible;
                        }

                        checkElementoSalida.Checked += CheckElementoSalida_Checked;
                        checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked;
                        checkElementoSalida.Click += CheckElementoSalida_Click;
                        
                        grillaLista.Children.Add(checkElementoSalida);
                        Grid.SetRow(checkElementoSalida, 0);
                        Grid.SetColumn(checkElementoSalida, 1);

                        opcionElementoSalida.Header = grillaLista;
                        menuBoton.Items.Add(opcionElementoSalida);
                    }
                }
            }
        }

        private void OpcionNoCumple_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_ElementoSalida asociacion = (AsociacionTextoInformacion_ElementoSalida)check.Tag;

                if (check.IsChecked == false)
                {
                    asociacion.SiCondicionesNoCumplen = false;
                }
            }
        }

        private void OpcionNoCumple_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_ElementoSalida asociacion = (AsociacionTextoInformacion_ElementoSalida)check.Tag;

                if (check.IsChecked == true)
                {
                    asociacion.SiCondicionesNoCumplen = true;
                }
            }
        }

        private void OpcionNoCumple_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_Clasificador asociacion = (AsociacionTextoInformacion_Clasificador)check.Tag;

                if (check.IsChecked == false)
                {
                    asociacion.SiCondicionesNoCumplen = false;
                }
            }
        }

        private void OpcionNoCumple_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_Clasificador asociacion = (AsociacionTextoInformacion_Clasificador)check.Tag;

                if (check.IsChecked == true)
                {
                    asociacion.SiCondicionesNoCumplen = true;
                }
            }
        }

        private void OpcionCumple_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_ElementoSalida asociacion = (AsociacionTextoInformacion_ElementoSalida)check.Tag;
                
                if (check.IsChecked == false)
                {
                    asociacion.SiCondicionesCumplen = false;
                }
            }
        }

        private void OpcionCumple_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_ElementoSalida asociacion = (AsociacionTextoInformacion_ElementoSalida)check.Tag;

                if (check.IsChecked == true)
                {
                    asociacion.SiCondicionesCumplen = true;
                }
            }
        }

        private void OpcionCumple_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_Clasificador asociacion = (AsociacionTextoInformacion_Clasificador)check.Tag;

                if (check.IsChecked == false)
                {
                    asociacion.SiCondicionesCumplen = false;
                }
            }
        }

        private void OpcionCumple_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                //object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_Clasificador asociacion = (AsociacionTextoInformacion_Clasificador)check.Tag;

                if (check.IsChecked == true)
                {
                    asociacion.SiCondicionesCumplen = true;
                }
            }
        }

        private void ListarOpcionesMenuBotonElementosSalida_DentroOperacion(ComboBox combo, DiseñoOperacion ElementoOperando,
            AsociacionTextoInformacion_ElementoSalida asociacion)
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

        private void CantidadTextosInformacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numeroVeces = 0;
            if (int.TryParse(((TextBox)sender).Text, out numeroVeces))
            {
                object[] objetos = (object[])((TextBox)sender).Tag;

                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];
                                
                AsociacionTextoInformacion_ElementoSalida asociacion = (from A in AsociacionesTextosInformacion_ElementosSalida
                                                                        where
                A.CondicionesAsociadas == condiciones.Condiciones &
                A.ElementoSalida == elementoSalida
                                                                        select A).FirstOrDefault();

                if (asociacion != null)
                    asociacion.CantidadConjuntosTextosInformacion = numeroVeces;
                
            }
            else
                ((TextBox)sender).Text = "1";
        }

        private void CantidadTextosInformacion_TextChanged2(object sender, TextChangedEventArgs e)
        {
            int numeroVeces = 0;
            if (int.TryParse(((TextBox)sender).Text, out numeroVeces))
            {
                object[] objetos = (object[])((TextBox)sender).Tag;

                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                DiseñoOperacion elementoSalida = (DiseñoOperacion)objetos[1];

                AsociacionTextoInformacion_ElementoSalida asociacion = (from A in AsociacionesTextosInformacion_ElementosSalida
                                                                        where
                A.CondicionesAsociadas == condiciones.Condiciones &
                A.ElementoSalida_Operacion == elementoSalida
                                                                        select A).FirstOrDefault();

                if (asociacion != null)
                    asociacion.CantidadConjuntosTextosInformacion = numeroVeces;

            }
            else
                ((TextBox)sender).Text = "1";
        }

        private void CantidadTextosInformacion_Clasificadores_TextChanged2(object sender, TextChangedEventArgs e)
        {
            int numeroVeces = 0;
            if (int.TryParse(((TextBox)sender).Text, out numeroVeces))
            {
                object[] objetos = (object[])((TextBox)sender).Tag;

                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                Clasificador elementoClasificador = (Clasificador)objetos[1];

                AsociacionTextoInformacion_Clasificador asociacion = (from A in AsociacionesTextosInformacion_Clasificadores
                                                                        where
                A.CondicionesAsociadas == condiciones.Condiciones &
                A.ElementoClasificador == elementoClasificador
                                                                      select A).FirstOrDefault();

                if (asociacion != null)
                    asociacion.CantidadConjuntosTextosInformacion = numeroVeces;

            }
            else
                ((TextBox)sender).Text = "1";
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

        private void CheckElementoSalida_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;
                ListarOpcionesMenuBotonElementosSalida((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[2]);
            } 
        }

        private void CheckElementoClasificador_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;
                ListarOpcionesMenuBotonClasificadores((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[2]);
            }
        }
        private void BotonBajar_Click(object sender, RoutedEventArgs e)
        {
            Button check = (Button)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;

                var elemento = (from E in AsociacionesTextosInformacion_ElementosSalida
                                where E.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones
                               & E.ElementoSalida == (DiseñoElementoOperacion)elementos[2]
                                select E).FirstOrDefault()
                                ;

                if (elemento != null)
                {
                    int indiceElemento = ElementosSalida.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        ElementosSalida.IndexOf(i)).ToList().IndexOf((DiseñoElementoOperacion)elementos[2]);

                    if (indiceElemento < ElementosSalida.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        ElementosSalida.IndexOf(i)).Where(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones)).ToList().Count - 1)
                    {
                        indiceElemento = AsociacionesTextosInformacion_ElementosSalida.IndexOf(elemento);
                        AsociacionesTextosInformacion_ElementosSalida.Remove(elemento);
                        indiceElemento++;
                        AsociacionesTextosInformacion_ElementosSalida.Insert(indiceElemento, elemento);
                    }
                }

                ListarOpcionesMenuBotonElementosSalida((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[1]);


            }
        }

        private void BotonSubir_Click(object sender, RoutedEventArgs e)
        {
            Button check = (Button)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;

                var elemento = (from E in AsociacionesTextosInformacion_ElementosSalida
                                where E.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones
                               & E.ElementoSalida == (DiseñoElementoOperacion)elementos[2]
                                select E).FirstOrDefault()
                                ;

                if (elemento != null)
                {
                    int indiceElemento = ElementosSalida.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        ElementosSalida.IndexOf(i)).ToList().IndexOf((DiseñoElementoOperacion)elementos[2]);

                    if (indiceElemento > 0)
                    {
                        indiceElemento = AsociacionesTextosInformacion_ElementosSalida.IndexOf(elemento);
                        AsociacionesTextosInformacion_ElementosSalida.Remove(elemento);
                        indiceElemento--;
                        AsociacionesTextosInformacion_ElementosSalida.Insert(indiceElemento, elemento);
                    }
                }

                ListarOpcionesMenuBotonElementosSalida((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[1]);
            }
        }

        private void CheckElementoSalida_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                CheckBox checkCumple = (CheckBox)objetos[3];
                CheckBox checkNoCumple = (CheckBox)objetos[4];

                if (check.IsChecked == false)
                {
                    AsociacionTextoInformacion_ElementoSalida asociacion = (from A in AsociacionesTextosInformacion_ElementosSalida
                                                                            where
                   A.CondicionesAsociadas == condiciones.Condiciones &
                   A.ElementoSalida == elementoSalida
                                                                            select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        AsociacionesTextosInformacion_ElementosSalida.Remove(asociacion);

                        checkCumple.Tag = null;
                        checkCumple.IsChecked = false;
                        checkCumple.Visibility = Visibility.Collapsed;

                        checkNoCumple.Tag = null;
                        checkNoCumple.IsChecked = false;
                        checkNoCumple.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void CheckElementoSalida_Unchecked_DentroOperacion(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                AsociacionTextoInformacion_ElementoSalida asociacion = (AsociacionTextoInformacion_ElementoSalida)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == false)
                {
                    if (asociacion.ElementosSalidas.Contains(elementoSalida))
                        asociacion.ElementosSalidas.Remove(elementoSalida);
                }
            }
        }

        private void CheckElementoSalida_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                CheckBox checkCumple = (CheckBox)objetos[3];
                CheckBox checkNoCumple = (CheckBox)objetos[4];

                if (check.IsChecked == true)
                {
                    AsociacionTextoInformacion_ElementoSalida asociacion = new AsociacionTextoInformacion_ElementoSalida();
                    asociacion.ElementoSalida = elementoSalida;
                    asociacion.CondicionesAsociadas = condiciones.Condiciones;
                    AsociacionesTextosInformacion_ElementosSalida.Add(asociacion);

                    checkCumple.IsChecked = true;
                    checkCumple.Tag = asociacion;
                    checkCumple.Visibility = Visibility.Visible;

                    checkNoCumple.IsChecked = false;
                    checkNoCumple.Tag = asociacion;
                    checkNoCumple.Visibility = Visibility.Visible;
                }
            }
        }

        private void CheckElementoSalida_Checked_DentroOperacion(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                AsociacionTextoInformacion_ElementoSalida asociacion = (AsociacionTextoInformacion_ElementoSalida)objetos[0];
                DiseñoElementoOperacion elementoSalida = (DiseñoElementoOperacion)objetos[1];

                if (check.IsChecked == true)
                {
                    if(!asociacion.ElementosSalidas.Contains(elementoSalida))
                        asociacion.ElementosSalidas.Add(elementoSalida);
                }
            }
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void ListarOpcionesBotonElementosSalida_Operacion(Button boton, OpcionCondiciones_TextosInformacion textoInformacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar variables o vectores retornados";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            ListarOpcionesMenuBotonElementosSalida_Operacion(textoInformacion, menuBoton);
        }

        private void ListarOpcionesBotonClasificadores(Button boton, OpcionCondiciones_TextosInformacion textoInformacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Seleccionar clasificadores";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            ListarOpcionesMenuBotonClasificadores(textoInformacion, menuBoton);
        }

        private void ListarOpcionesMenuBotonElementosSalida_Operacion(OpcionCondiciones_TextosInformacion textoInformacion,
            ContextMenu menuBoton)
        {
            menuBoton.Items.Clear();

            if (ElementosSalida_Operacion != null)
            {
                int orden = 0;

                foreach (var itemSalida in ElementosSalida_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida_Operacion == i &&
                        j.CondicionesAsociadas == textoInformacion.Condiciones).FirstOrDefault()) :
                        ElementosSalida_Operacion.IndexOf(i)))
                {
                    if (textoInformacion.Condiciones != null && (textoInformacion.Condiciones.OrdenarAlfabeticamenteNumerosSalidas &&
                        AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones)))
                    {
                        orden++;

                        Grid grillaOrdenamiento = new Grid();
                        grillaOrdenamiento.RowDefinitions.Add(new RowDefinition());
                        grillaOrdenamiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        Grid grillaOpcionesCumplimiento = new Grid();
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOpcionesCumplimiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionCumple = new CheckBox();
                        opcionCumple.Margin = new Thickness(5);
                        opcionCumple.Content = "Si las condiciones (si/entonces) se cumplen";
                        opcionCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionCumple.Visibility = Visibility.Collapsed;

                        opcionCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesCumplen == true);

                        opcionCumple.Checked += OpcionCumple_Checked;
                        opcionCumple.Unchecked += OpcionCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionCumple);
                        Grid.SetRow(opcionCumple, 0);
                        Grid.SetColumn(opcionCumple, 0);

                        CheckBox opcionNoCumple = new CheckBox();
                        opcionNoCumple.Margin = new Thickness(5);
                        opcionNoCumple.Content = "Si las condiciones (si/entonces) no se cumplen";
                        opcionNoCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionNoCumple.Visibility = Visibility.Collapsed;

                        opcionNoCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionNoCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesNoCumplen == true);

                        opcionNoCumple.Checked += OpcionNoCumple_Checked;
                        opcionNoCumple.Unchecked += OpcionNoCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionNoCumple);
                        Grid.SetRow(opcionNoCumple, 1);
                        Grid.SetColumn(opcionNoCumple, 0);

                        grillaOrdenamiento.Children.Add(grillaOpcionesCumplimiento);
                        Grid.SetRow(grillaOpcionesCumplimiento, 0);
                        Grid.SetColumn(grillaOpcionesCumplimiento, 0);

                        TextBlock textoOrden = new TextBlock();
                        textoOrden.Margin = new Thickness(5);
                        textoOrden.Text = "Orden: " + orden.ToString();
                        textoOrden.VerticalAlignment = VerticalAlignment.Center;
                        grillaOrdenamiento.Children.Add(textoOrden);
                        Grid.SetRow(textoOrden, 0);
                        Grid.SetColumn(textoOrden, 1);

                        ComboBox botonOpcionesElementosSalida = new ComboBox();
                        botonOpcionesElementosSalida.Margin = new Thickness(10);
                        botonOpcionesElementosSalida.MaxHeight = 70;
                        botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                        botonOpcionesElementosSalida.Visibility = Visibility.Collapsed;

                        botonOpcionesElementosSalida.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                            item.CondicionesAsociadas == textoInformacion.Condiciones);

                        ListarOpcionesBotonElementosSalida_DentroOperacion(botonOpcionesElementosSalida, itemSalida,
                            AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones));

                        grillaOrdenamiento.Children.Add(botonOpcionesElementosSalida);

                        Grid.SetRow(botonOpcionesElementosSalida, 0);
                        Grid.SetColumn(botonOpcionesElementosSalida, 2);

                        CheckBox checkElementoSalida = new CheckBox();
                        checkElementoSalida.Content = itemSalida.NombreCombo;
                        checkElementoSalida.Tag = new object[] { textoInformacion, itemSalida, menuBoton, botonOpcionesElementosSalida, opcionCumple, opcionNoCumple };

                        checkElementoSalida.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        if (checkElementoSalida.IsChecked == true)
                        {
                            opcionCumple.Visibility = Visibility.Visible;
                            opcionNoCumple.Visibility = Visibility.Visible;
                        }

                        if (checkElementoSalida.IsChecked == true)
                        {
                            if (!itemSalida.DefinicionSimple_Operacion)
                            {
                                botonOpcionesElementosSalida.Visibility = Visibility.Visible;
                            }
                        }

                        checkElementoSalida.Checked += CheckElementoSalida_Checked1;
                        checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked1;
                        checkElementoSalida.Click += CheckElementoSalidaOperacion_Click;

                        checkElementoSalida.VerticalAlignment = VerticalAlignment.Center;
                        grillaOrdenamiento.Children.Add(checkElementoSalida);
                        Grid.SetRow(checkElementoSalida, 0);
                        Grid.SetColumn(checkElementoSalida, 3);

                        StackPanel panelCantidadTextosInformacion = new StackPanel();
                        panelCantidadTextosInformacion.Margin = new Thickness(20, 0, 0, 0);
                        panelCantidadTextosInformacion.Orientation = Orientation.Horizontal;

                        TextBlock textoCantidadTextosInformacion = new TextBlock();
                        textoCantidadTextosInformacion.Inlines.Add("Vectores de cadenas de texto");
                        textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                        textoCantidadTextosInformacion.Inlines.Add("distintos antes de continuar");
                        textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                        textoCantidadTextosInformacion.Inlines.Add("con el siguiente variable o vector retornados:");
                        textoCantidadTextosInformacion.Margin = new Thickness(2);
                                               

                        TextBox cantidadTextosInformacion = new TextBox();
                        cantidadTextosInformacion.Margin = new Thickness(2);
                        cantidadTextosInformacion.Width = 50;
                        cantidadTextosInformacion.MaxWidth = 50;
                        cantidadTextosInformacion.TextAlignment = TextAlignment.Center;
                        cantidadTextosInformacion.HorizontalContentAlignment = HorizontalAlignment.Center;
                        cantidadTextosInformacion.VerticalContentAlignment = VerticalAlignment.Center;
                        cantidadTextosInformacion.Tag = new object[] { textoInformacion, itemSalida };

                        if (AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                        {
                            var asociacion = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                            if (asociacion != null)
                            {
                                cantidadTextosInformacion.Text = asociacion.CantidadConjuntosTextosInformacion.ToString();
                            }
                        }
                        else
                            cantidadTextosInformacion.Text = "1";

                        cantidadTextosInformacion.TextChanged += CantidadTextosInformacion_TextChanged2;

                        panelCantidadTextosInformacion.Children.Add(textoCantidadTextosInformacion);
                        panelCantidadTextosInformacion.Children.Add(cantidadTextosInformacion);

                        grillaOrdenamiento.Children.Add(panelCantidadTextosInformacion);

                        Grid.SetRow(panelCantidadTextosInformacion, 0);
                        Grid.SetColumn(panelCantidadTextosInformacion, 4);

                        //Image ImagenBotonSubir = new Image();
                        ////La imagen es la que tiene que ser
                        //ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_01.png", UriKind.Relative));
                        //ImagenBotonSubir.Width = 16;
                        //ImagenBotonSubir.Height = 16;

                        Button botonSubir = new Button();
                        //botonSubir.Content = ImagenBotonSubir;
                        botonSubir.Content = "Subir";
                        botonSubir.Margin = new Thickness(10);
                        botonSubir.Tag = new object[] { textoInformacion, menuBoton, itemSalida };
                        botonSubir.Click += BotonSubir_Click1;
                        botonSubir.VerticalAlignment = VerticalAlignment.Center;

                        grillaOrdenamiento.Children.Add(botonSubir);

                        Grid.SetRow(botonSubir, 0);
                        Grid.SetColumn(botonSubir, 5);

                        //Image ImagenBotonBajar = new Image();
                        ////La imagen es la que tiene que ser
                        //ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_02.png", UriKind.Relative));
                        //ImagenBotonBajar.Width = 16;
                        //ImagenBotonBajar.Height = 16;

                        Button botonBajar = new Button();
                        //botonBajar.Content = ImagenBotonBajar;
                        botonBajar.Content = "Bajar";
                        botonBajar.Margin = new Thickness(10);
                        botonBajar.Tag = new object[] { textoInformacion, menuBoton, itemSalida };
                        botonBajar.Click += BotonBajar_Click1;
                        botonBajar.VerticalAlignment = VerticalAlignment.Center;

                        grillaOrdenamiento.Children.Add(botonBajar);

                        Grid.SetRow(botonBajar, 0);
                        Grid.SetColumn(botonBajar, 6);

                        MenuItem opcionElementoSalida = new MenuItem();
                        grillaOrdenamiento.VerticalAlignment = VerticalAlignment.Center;
                        opcionElementoSalida.Header = grillaOrdenamiento;
                        menuBoton.Items.Add(opcionElementoSalida);
                    }
                    else
                    {
                        MenuItem opcionElementoSalida = new MenuItem();

                        Grid grillaLista = new Grid();
                        grillaLista.RowDefinitions.Add(new RowDefinition());
                        grillaLista.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaLista.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaLista.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaLista.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaLista.ColumnDefinitions.Last().Width = GridLength.Auto;

                        Grid grillaOpcionesCumplimiento = new Grid();
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOpcionesCumplimiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionCumple = new CheckBox();
                        opcionCumple.Margin = new Thickness(5);
                        opcionCumple.Content = "Si las condiciones (si/entonces) se cumplen";
                        opcionCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionCumple.Visibility = Visibility.Collapsed;

                        opcionCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesCumplen == true);

                        opcionCumple.Checked += OpcionCumple_Checked;
                        opcionCumple.Unchecked += OpcionCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionCumple);
                        Grid.SetRow(opcionCumple, 0);
                        Grid.SetColumn(opcionCumple, 0);

                        CheckBox opcionNoCumple = new CheckBox();
                        opcionNoCumple.Margin = new Thickness(5);
                        opcionNoCumple.Content = "Si las condiciones (si/entonces) no se cumplen";
                        opcionNoCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionNoCumple.Visibility = Visibility.Collapsed;

                        opcionNoCumple.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionNoCumple.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesNoCumplen == true);

                        opcionNoCumple.Checked += OpcionNoCumple_Checked;
                        opcionNoCumple.Unchecked += OpcionNoCumple_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionNoCumple);
                        Grid.SetRow(opcionNoCumple, 1);
                        Grid.SetColumn(opcionNoCumple, 0);

                        grillaLista.Children.Add(grillaOpcionesCumplimiento);
                        Grid.SetRow(grillaOpcionesCumplimiento, 0);
                        Grid.SetColumn(grillaOpcionesCumplimiento, 0);

                        StackPanel stackOpciones = new StackPanel();
                        stackOpciones.Orientation = Orientation.Horizontal;

                        menuBoton.Items.Add(stackOpciones);

                        ComboBox botonOpcionesElementosSalida = new ComboBox();
                        botonOpcionesElementosSalida.Margin = new Thickness(10);
                        botonOpcionesElementosSalida.MaxHeight = 70;
                        botonOpcionesElementosSalida.VerticalAlignment = VerticalAlignment.Center;
                        botonOpcionesElementosSalida.Visibility = Visibility.Collapsed;

                        botonOpcionesElementosSalida.Tag = AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                            item.CondicionesAsociadas == textoInformacion.Condiciones);

                        ListarOpcionesBotonElementosSalida_DentroOperacion(botonOpcionesElementosSalida, itemSalida,
                            AsociacionesTextosInformacion_ElementosSalida.FirstOrDefault(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones));

                        CheckBox checkElementoSalida = new CheckBox();
                        checkElementoSalida.Content = itemSalida.NombreCombo;
                        checkElementoSalida.Tag = new object[] { textoInformacion, itemSalida, menuBoton, botonOpcionesElementosSalida, opcionCumple, opcionNoCumple };

                        checkElementoSalida.IsChecked = AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == itemSalida &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        if (checkElementoSalida.IsChecked == true)
                        {
                            opcionCumple.Visibility = Visibility.Visible;
                            opcionNoCumple.Visibility = Visibility.Visible;
                        }

                        if (checkElementoSalida.IsChecked == true)
                        {
                            if(!itemSalida.DefinicionSimple_Operacion)
                            {
                                botonOpcionesElementosSalida.Visibility = Visibility.Visible;
                            }
                        }

                        checkElementoSalida.Checked += CheckElementoSalida_Checked1;
                        checkElementoSalida.Unchecked += CheckElementoSalida_Unchecked1;
                        checkElementoSalida.Click += CheckElementoSalidaOperacion_Click;

                        grillaLista.Children.Add(checkElementoSalida);
                        Grid.SetRow(checkElementoSalida, 0);
                        Grid.SetColumn(checkElementoSalida, 1);

                        stackOpciones.Children.Add(grillaLista);
                        stackOpciones.Children.Add(botonOpcionesElementosSalida);

                        //opcionElementoSalida.Header = stackOpciones;                        
                    }
                }
            }
        }

        private void ListarOpcionesMenuBotonClasificadores(OpcionCondiciones_TextosInformacion textoInformacion,
            ContextMenu menuBoton)
        {
            menuBoton.Items.Clear();

            if (Clasificadores_Operacion != null)
            {
                Grid grillaClasificadores = new Grid();
                grillaClasificadores.RowDefinitions.Add(new RowDefinition());
                grillaClasificadores.RowDefinitions.Last().Height = GridLength.Auto;

                grillaClasificadores.ColumnDefinitions.Add(new ColumnDefinition());
                grillaClasificadores.ColumnDefinitions.Last().Width = GridLength.Auto;
                grillaClasificadores.ColumnDefinitions.Add(new ColumnDefinition());
                grillaClasificadores.ColumnDefinitions.Last().Width = GridLength.Auto;
                grillaClasificadores.ColumnDefinitions.Add(new ColumnDefinition());
                grillaClasificadores.ColumnDefinitions.Last().Width = GridLength.Auto;

                Button agregarClasificador = new Button();
                agregarClasificador.Margin = new Thickness(5);
                agregarClasificador.Content = "Agregar clasificador";
                agregarClasificador.VerticalAlignment = VerticalAlignment.Center;
                agregarClasificador.Tag = new object[] { Clasificadores_Operacion, menuBoton };

                agregarClasificador.Click += AgregarClasificador_Click;

                grillaClasificadores.Children.Add(agregarClasificador);
                Grid.SetRow(agregarClasificador, 0);
                Grid.SetColumn(agregarClasificador, 0);

                Button administrarClasificadores = new Button();
                administrarClasificadores.Margin = new Thickness(5);
                administrarClasificadores.Content = "Gestionar clasificadores";
                administrarClasificadores.VerticalAlignment = VerticalAlignment.Center;
                administrarClasificadores.Tag = new object[] { Clasificadores_Operacion, menuBoton };

                administrarClasificadores.Click += AdministrarClasificadores_Click;

                grillaClasificadores.Children.Add(administrarClasificadores);
                Grid.SetRow(administrarClasificadores, 0);
                Grid.SetColumn(administrarClasificadores, 1);

                menuBoton.Items.Add(grillaClasificadores);

                int orden = 0;

                foreach (var itemClasificador in Clasificadores_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones) ? AsociacionesTextosInformacion_Clasificadores.IndexOf(AsociacionesTextosInformacion_Clasificadores.Where(j => j.ElementoClasificador == i &&
                        j.CondicionesAsociadas == textoInformacion.Condiciones).FirstOrDefault()) :
                        Clasificadores_Operacion.IndexOf(i)))
                {
                    if (textoInformacion.Condiciones != null && (textoInformacion.Condiciones.OrdenarAlfabeticamenteNumerosClasificadores &&
                        AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones)))
                    {
                        orden++;

                        Grid grillaOrdenamiento = new Grid();
                        grillaOrdenamiento.RowDefinitions.Add(new RowDefinition());
                        grillaOrdenamiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaOrdenamiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOrdenamiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        Grid grillaOpcionesCumplimiento = new Grid();
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOpcionesCumplimiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionCumple = new CheckBox();
                        opcionCumple.Margin = new Thickness(5);
                        opcionCumple.Content = "Si las condiciones (si/entonces) se cumplen";
                        opcionCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionCumple.Visibility = Visibility.Collapsed;

                        opcionCumple.Tag = AsociacionesTextosInformacion_Clasificadores.FirstOrDefault(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionCumple.IsChecked = AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesCumplen == true);

                        opcionCumple.Checked += OpcionCumple_Clasificadores_Checked;
                        opcionCumple.Unchecked += OpcionCumple_Clasificadores_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionCumple);
                        Grid.SetRow(opcionCumple, 0);
                        Grid.SetColumn(opcionCumple, 0);

                        CheckBox opcionNoCumple = new CheckBox();
                        opcionNoCumple.Margin = new Thickness(5);
                        opcionNoCumple.Content = "Si las condiciones (si/entonces) no se cumplen";
                        opcionNoCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionNoCumple.Visibility = Visibility.Collapsed;

                        opcionNoCumple.Tag = AsociacionesTextosInformacion_Clasificadores.FirstOrDefault(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionNoCumple.IsChecked = AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesNoCumplen == true);

                        opcionNoCumple.Checked += OpcionNoCumple_Clasificadores_Checked;
                        opcionNoCumple.Unchecked += OpcionNoCumple_Clasificadores_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionNoCumple);
                        Grid.SetRow(opcionNoCumple, 1);
                        Grid.SetColumn(opcionNoCumple, 0);

                        grillaOrdenamiento.Children.Add(grillaOpcionesCumplimiento);
                        Grid.SetRow(grillaOpcionesCumplimiento, 0);
                        Grid.SetColumn(grillaOpcionesCumplimiento, 0);

                        TextBlock textoOrden = new TextBlock();
                        textoOrden.Margin = new Thickness(5);
                        textoOrden.Text = "Orden: " + orden.ToString();
                        textoOrden.VerticalAlignment = VerticalAlignment.Center;
                        grillaOrdenamiento.Children.Add(textoOrden);
                        Grid.SetRow(textoOrden, 0);
                        Grid.SetColumn(textoOrden, 1);

                        CheckBox checkElementoClasificador = new CheckBox();
                        checkElementoClasificador.Content = itemClasificador.CadenaTexto + 
                            (itemClasificador.UtilizarCadenasTexto_DeCantidad ? "(se utilizarán las cadenas de texto que clasifican estas cantidades)" : string.Empty);
                        
                        checkElementoClasificador.Tag = new object[] { textoInformacion, itemClasificador, menuBoton, opcionCumple, opcionNoCumple };

                        checkElementoClasificador.IsChecked = AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        if (checkElementoClasificador.IsChecked == true)
                        {
                            opcionCumple.Visibility = Visibility.Visible;
                            opcionNoCumple.Visibility = Visibility.Visible;
                        }

                        checkElementoClasificador.Checked += CheckElementoClasificador_Checked1;
                        checkElementoClasificador.Unchecked += CheckElementoClasificador_Unchecked1;
                        checkElementoClasificador.Click += CheckElementoClasificador_Click;

                        checkElementoClasificador.VerticalAlignment = VerticalAlignment.Center;
                        grillaOrdenamiento.Children.Add(checkElementoClasificador);
                        Grid.SetRow(checkElementoClasificador, 0);
                        Grid.SetColumn(checkElementoClasificador, 2);

                        StackPanel panelCantidadTextosInformacion = new StackPanel();
                        panelCantidadTextosInformacion.Margin = new Thickness(20, 0, 0, 0);
                        panelCantidadTextosInformacion.Orientation = Orientation.Horizontal;

                        TextBlock textoCantidadTextosInformacion = new TextBlock();
                        textoCantidadTextosInformacion.Inlines.Add("Cadenas de texto");
                        textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                        textoCantidadTextosInformacion.Inlines.Add("distintas antes de continuar");
                        textoCantidadTextosInformacion.Inlines.Add(new LineBreak());
                        textoCantidadTextosInformacion.Inlines.Add("con el siguiente clasificador:");
                        textoCantidadTextosInformacion.Margin = new Thickness(2);


                        TextBox cantidadTextosInformacion = new TextBox();
                        cantidadTextosInformacion.Margin = new Thickness(2);
                        cantidadTextosInformacion.Width = 50;
                        cantidadTextosInformacion.MaxWidth = 50;
                        cantidadTextosInformacion.TextAlignment = TextAlignment.Center;
                        cantidadTextosInformacion.HorizontalContentAlignment = HorizontalAlignment.Center;
                        cantidadTextosInformacion.VerticalContentAlignment = VerticalAlignment.Center;
                        cantidadTextosInformacion.Tag = new object[] { textoInformacion, itemClasificador };

                        if (AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones))
                        {
                            var asociacion = AsociacionesTextosInformacion_Clasificadores.FirstOrDefault(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                            if (asociacion != null)
                            {
                                cantidadTextosInformacion.Text = asociacion.CantidadConjuntosTextosInformacion.ToString();
                            }
                        }
                        else
                            cantidadTextosInformacion.Text = "1";

                        cantidadTextosInformacion.TextChanged += CantidadTextosInformacion_Clasificadores_TextChanged2;

                        panelCantidadTextosInformacion.Children.Add(textoCantidadTextosInformacion);
                        panelCantidadTextosInformacion.Children.Add(cantidadTextosInformacion);

                        grillaOrdenamiento.Children.Add(panelCantidadTextosInformacion);

                        Grid.SetRow(panelCantidadTextosInformacion, 0);
                        Grid.SetColumn(panelCantidadTextosInformacion, 4);

                        //Image ImagenBotonSubir = new Image();
                        ////La imagen es la que tiene que ser
                        //ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_01.png", UriKind.Relative));
                        //ImagenBotonSubir.Width = 16;
                        //ImagenBotonSubir.Height = 16;

                        Button botonSubir = new Button();
                        //botonSubir.Content = ImagenBotonSubir;
                        botonSubir.Content = "Subir";
                        botonSubir.Margin = new Thickness(10);
                        botonSubir.Tag = new object[] { textoInformacion, menuBoton, itemClasificador };
                        botonSubir.Click += BotonSubir_Clasificadores_Click1;
                        botonSubir.VerticalAlignment = VerticalAlignment.Center;

                        grillaOrdenamiento.Children.Add(botonSubir);

                        Grid.SetRow(botonSubir, 0);
                        Grid.SetColumn(botonSubir, 5);

                        //Image ImagenBotonBajar = new Image();
                        ////La imagen es la que tiene que ser
                        //ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos8\\Icono_02.png", UriKind.Relative));
                        //ImagenBotonBajar.Width = 16;
                        //ImagenBotonBajar.Height = 16;

                        Button botonBajar = new Button();
                        //botonBajar.Content = ImagenBotonBajar;
                        botonBajar.Content = "Bajar";
                        botonBajar.Margin = new Thickness(10);
                        botonBajar.Tag = new object[] { textoInformacion, menuBoton, itemClasificador };
                        botonBajar.Click += BotonBajar_Clasificadores_Click1;
                        botonBajar.VerticalAlignment = VerticalAlignment.Center;

                        grillaOrdenamiento.Children.Add(botonBajar);

                        Grid.SetRow(botonBajar, 0);
                        Grid.SetColumn(botonBajar, 6);

                        MenuItem opcionElementoClasificador = new MenuItem();
                        grillaOrdenamiento.VerticalAlignment = VerticalAlignment.Center;
                        opcionElementoClasificador.Header = grillaOrdenamiento;
                        menuBoton.Items.Add(opcionElementoClasificador);
                    }
                    else
                    {
                        Grid grillaLista = new Grid();
                        grillaLista.RowDefinitions.Add(new RowDefinition());
                        grillaLista.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaLista.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaLista.ColumnDefinitions.Last().Width = GridLength.Auto;
                        grillaLista.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaLista.ColumnDefinitions.Last().Width = GridLength.Auto;

                        Grid grillaOpcionesCumplimiento = new Grid();
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.RowDefinitions.Add(new RowDefinition());
                        grillaOpcionesCumplimiento.RowDefinitions.Last().Height = GridLength.Auto;
                        grillaOpcionesCumplimiento.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaOpcionesCumplimiento.ColumnDefinitions.Last().Width = GridLength.Auto;

                        CheckBox opcionCumple = new CheckBox();
                        opcionCumple.Margin = new Thickness(5);
                        opcionCumple.Content = "Si las condiciones (si/entonces) se cumplen";
                        opcionCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionCumple.Visibility = Visibility.Collapsed;

                        opcionCumple.Tag = AsociacionesTextosInformacion_Clasificadores.FirstOrDefault(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionCumple.IsChecked = AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesCumplen == true);

                        opcionCumple.Checked += OpcionCumple_Clasificadores_Checked;
                        opcionCumple.Unchecked += OpcionCumple_Clasificadores_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionCumple);
                        Grid.SetRow(opcionCumple, 0);
                        Grid.SetColumn(opcionCumple, 0);

                        CheckBox opcionNoCumple = new CheckBox();
                        opcionNoCumple.Margin = new Thickness(5);
                        opcionNoCumple.Content = "Si las condiciones (si/entonces) no se cumplen";
                        opcionNoCumple.VerticalAlignment = VerticalAlignment.Center;
                        opcionNoCumple.Visibility = Visibility.Collapsed;

                        opcionNoCumple.Tag = AsociacionesTextosInformacion_Clasificadores.FirstOrDefault(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        opcionNoCumple.IsChecked = AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones && item.SiCondicionesNoCumplen == true);

                        opcionNoCumple.Checked += OpcionNoCumple_Clasificadores_Checked;
                        opcionNoCumple.Unchecked += OpcionNoCumple_Clasificadores_Unchecked;

                        grillaOpcionesCumplimiento.Children.Add(opcionNoCumple);
                        Grid.SetRow(opcionNoCumple, 1);
                        Grid.SetColumn(opcionNoCumple, 0);

                        grillaLista.Children.Add(grillaOpcionesCumplimiento);
                        Grid.SetRow(grillaOpcionesCumplimiento, 0);
                        Grid.SetColumn(grillaOpcionesCumplimiento, 0);

                        StackPanel stackOpciones = new StackPanel();
                        stackOpciones.Orientation = Orientation.Horizontal;

                        menuBoton.Items.Add(stackOpciones);
                                                
                        CheckBox checkElementoClasificador = new CheckBox();
                        checkElementoClasificador.Content = itemClasificador.CadenaTexto +
                            (itemClasificador.UtilizarCadenasTexto_DeCantidad ? "(se utilizarán las cadenas de texto que clasifican estas cantidades)" : string.Empty);

                        checkElementoClasificador.Tag = new object[] { textoInformacion, itemClasificador, menuBoton, opcionCumple, opcionNoCumple };

                        checkElementoClasificador.IsChecked = AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == itemClasificador &&
                        item.CondicionesAsociadas == textoInformacion.Condiciones);

                        if (checkElementoClasificador.IsChecked == true)
                        {
                            opcionCumple.Visibility = Visibility.Visible;
                            opcionNoCumple.Visibility = Visibility.Visible;
                        }

                        checkElementoClasificador.Checked += CheckElementoClasificador_Checked1;
                        checkElementoClasificador.Unchecked += CheckElementoClasificador_Unchecked1;
                        checkElementoClasificador.Click += CheckElementoClasificador_Click;

                        grillaLista.Children.Add(checkElementoClasificador);
                        Grid.SetRow(checkElementoClasificador, 0);
                        Grid.SetColumn(checkElementoClasificador, 1);

                        stackOpciones.Children.Add(grillaLista);

                        //opcionElementoSalida.Header = stackOpciones;                        
                    }
                }
            }
        }

        private void AdministrarClasificadores_Click(object sender, RoutedEventArgs e)
        {
            var objetos = (object[])((Button)sender).Tag;

            var listaClasificadores = (List<Clasificador>)objetos[0];

            if (listaClasificadores != null)
            {
                DefinirClasificadores definirListaClasificadores = new DefinirClasificadores();
                definirListaClasificadores.ListaClasificadores = listaClasificadores.ToList();

                if(definirListaClasificadores.ShowDialog() == true)
                {
                    listaClasificadores.Clear();
                    listaClasificadores.AddRange(definirListaClasificadores.ListaClasificadores);
                                        
                    var asociaciones = AsociacionesTextosInformacion_Clasificadores.Where(i => !listaClasificadores.Contains(i.ElementoClasificador)).ToList();
                    if (asociaciones != null)
                    {
                        while(asociaciones.Any())
                        {
                            AsociacionesTextosInformacion_Clasificadores.Remove(asociaciones.FirstOrDefault());
                            asociaciones.Remove(asociaciones.FirstOrDefault());
                        }
                    }

                    var menuBoton = (ContextMenu)objetos[1];
                    menuBoton.IsOpen = false;

                    ListarTextos();
                }
            }
        }

        private void AgregarClasificador_Click(object sender, RoutedEventArgs e)
        {
            var objetos = (object[])((Button)sender).Tag;

            var listaClasificadores = (List<Clasificador>)objetos[0];

            if (listaClasificadores != null)
            {
                IngresarClasificador ingresarClasificador = new IngresarClasificador();
                Clasificador Clasificador = new Clasificador();
                ingresarClasificador.SeleccionCadenasTexto = new List<OperacionCadenaTexto>();
                Clasificador.ID = App.GenerarID_Elemento();

                if (ingresarClasificador.ShowDialog() == true)
                {
                    Clasificador.CadenaTexto = ingresarClasificador.CadenaTexto;
                    Clasificador.UtilizarCadenasTexto_DeCantidad = ingresarClasificador.UtilizarCadenasTexto_DeCantidad;
                    Clasificador.SeleccionCadenasTexto = ingresarClasificador.SeleccionCadenasTexto;
                    listaClasificadores.Add(Clasificador);

                    var menuBoton = (ContextMenu)objetos[1];
                    menuBoton.IsOpen = false;

                    ListarTextos();
                }
            }
        }

        private void BotonBajar_Click1(object sender, RoutedEventArgs e)
        {
            Button check = (Button)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;

                var elemento = (from E in AsociacionesTextosInformacion_ElementosSalida
                                where E.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones
                               & E.ElementoSalida_Operacion == (DiseñoOperacion)elementos[2]
                                select E).FirstOrDefault();

                if (elemento != null)
                {
                    int indiceElemento = (ElementosSalida_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida_Operacion == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        ElementosSalida_Operacion.IndexOf(i))).ToList().IndexOf((DiseñoOperacion)elementos[2]);

                    if (indiceElemento < (ElementosSalida_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida_Operacion == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        ElementosSalida_Operacion.IndexOf(i))).Where(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones)).ToList().Count - 1)
                    {
                        indiceElemento = AsociacionesTextosInformacion_ElementosSalida.IndexOf(elemento);
                        AsociacionesTextosInformacion_ElementosSalida.Remove(elemento);
                        indiceElemento++;
                        AsociacionesTextosInformacion_ElementosSalida.Insert(indiceElemento, elemento);
                    }
                }

                ListarOpcionesMenuBotonElementosSalida_Operacion((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[1]);
            }
        }

        private void BotonBajar_Clasificadores_Click1(object sender, RoutedEventArgs e)
        {
            Button check = (Button)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;

                var elemento = (from E in AsociacionesTextosInformacion_Clasificadores
                                where E.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones
                               & E.ElementoClasificador == (Clasificador)elementos[2]
                                select E).FirstOrDefault();

                if (elemento != null)
                {
                    int indiceElemento = (Clasificadores_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_Clasificadores.IndexOf(AsociacionesTextosInformacion_Clasificadores.Where(j => j.ElementoClasificador == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        Clasificadores_Operacion.IndexOf(i))).ToList().IndexOf((Clasificador)elementos[2]);

                    if (indiceElemento < (Clasificadores_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_Clasificadores.IndexOf(AsociacionesTextosInformacion_Clasificadores.Where(j => j.ElementoClasificador == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        Clasificadores_Operacion.IndexOf(i))).Where(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones)).ToList().Count - 1)
                    {
                        indiceElemento = AsociacionesTextosInformacion_Clasificadores.IndexOf(elemento);
                        AsociacionesTextosInformacion_Clasificadores.Remove(elemento);
                        indiceElemento++;
                        AsociacionesTextosInformacion_Clasificadores.Insert(indiceElemento, elemento);
                    }
                }

                ListarOpcionesMenuBotonClasificadores((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[1]);
            }
        }

        private void BotonSubir_Click1(object sender, RoutedEventArgs e)
        {
            Button check = (Button)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;

                var elemento = (from E in AsociacionesTextosInformacion_ElementosSalida
                                where E.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones
                               & E.ElementoSalida_Operacion == (DiseñoOperacion)elementos[2]
                                select E).FirstOrDefault();

                if (elemento != null)
                {
                    int indiceElemento = (ElementosSalida_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_ElementosSalida.Any(item => item.ElementoSalida_Operacion == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_ElementosSalida.IndexOf(AsociacionesTextosInformacion_ElementosSalida.Where(j => j.ElementoSalida_Operacion == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        ElementosSalida_Operacion.IndexOf(i))).ToList().IndexOf((DiseñoOperacion)elementos[2]);
                    
                    if (indiceElemento > 0)
                    {
                        indiceElemento = AsociacionesTextosInformacion_ElementosSalida.IndexOf(elemento);
                        AsociacionesTextosInformacion_ElementosSalida.Remove(elemento);
                        indiceElemento--;
                        AsociacionesTextosInformacion_ElementosSalida.Insert(indiceElemento, elemento);
                    }
                }

                ListarOpcionesMenuBotonElementosSalida_Operacion((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[1]);
            }
        }

        private void BotonSubir_Clasificadores_Click1(object sender, RoutedEventArgs e)
        {
            Button check = (Button)sender;

            if (check.Tag != null)
            {
                object[] elementos = (object[])check.Tag;

                var elemento = (from E in AsociacionesTextosInformacion_Clasificadores
                                where E.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones
                               & E.ElementoClasificador == (Clasificador)elementos[2]
                                select E).FirstOrDefault();

                if (elemento != null)
                {
                    int indiceElemento = (Clasificadores_Operacion.OrderByDescending(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones))
                    .ThenBy(i => AsociacionesTextosInformacion_Clasificadores.Any(item => item.ElementoClasificador == i &&
                        item.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones) ? AsociacionesTextosInformacion_Clasificadores.IndexOf(AsociacionesTextosInformacion_Clasificadores.Where(j => j.ElementoClasificador == i &&
                        j.CondicionesAsociadas == ((OpcionCondiciones_TextosInformacion)elementos[0]).Condiciones).FirstOrDefault()) :
                        Clasificadores_Operacion.IndexOf(i))).ToList().IndexOf((Clasificador)elementos[2]);

                    if (indiceElemento > 0)
                    {
                        indiceElemento = AsociacionesTextosInformacion_Clasificadores.IndexOf(elemento);
                        AsociacionesTextosInformacion_Clasificadores.Remove(elemento);
                        indiceElemento--;
                        AsociacionesTextosInformacion_Clasificadores.Insert(indiceElemento, elemento);
                    }
                }

                ListarOpcionesMenuBotonClasificadores((OpcionCondiciones_TextosInformacion)elementos[0], (ContextMenu)elementos[1]);
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
                ComboBox botonElementosInternos = (ComboBox)objetos[3];

                CheckBox checkCumple = (CheckBox)objetos[4];
                CheckBox checkNoCumple = (CheckBox)objetos[5];

                if (check.IsChecked == false)
                {
                    AsociacionTextoInformacion_ElementoSalida asociacion = (from A in AsociacionesTextosInformacion_ElementosSalida
                                                                            where
                   A.CondicionesAsociadas == condiciones.Condiciones &
                   A.ElementoSalida_Operacion == elementoSalida
                                                                            select A).FirstOrDefault();

                    if (asociacion != null)
                        AsociacionesTextosInformacion_ElementosSalida.Remove(asociacion);

                    if (!elementoSalida.DefinicionSimple_Operacion)
                    {
                        botonElementosInternos.Visibility = Visibility.Collapsed;
                    }

                    checkCumple.Tag = null;
                    checkCumple.IsChecked = false;
                    checkCumple.Visibility = Visibility.Collapsed;

                    checkNoCumple.Tag = null;
                    checkNoCumple.IsChecked = false;
                    checkNoCumple.Visibility = Visibility.Collapsed;
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
                ComboBox botonElementosInternos = (ComboBox)objetos[3];

                CheckBox checkCumple = (CheckBox)objetos[4];
                CheckBox checkNoCumple = (CheckBox)objetos[5];

                if (check.IsChecked == true)
                {
                    AsociacionTextoInformacion_ElementoSalida asociacion = new AsociacionTextoInformacion_ElementoSalida();
                    asociacion.ElementoSalida_Operacion = elementoSalida;
                    asociacion.CondicionesAsociadas = condiciones.Condiciones;
                    AsociacionesTextosInformacion_ElementosSalida.Add(asociacion);

                    if (!elementoSalida.DefinicionSimple_Operacion)
                    {
                        botonElementosInternos.Visibility = Visibility.Visible;
                    }

                    checkCumple.IsChecked = true;
                    checkCumple.Tag = asociacion;
                    checkCumple.Visibility = Visibility.Visible;

                    checkNoCumple.IsChecked = false;
                    checkNoCumple.Tag = asociacion;
                    checkNoCumple.Visibility = Visibility.Visible;
                }
            }
        }

        private void CheckElementoClasificador_Unchecked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;

                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                Clasificador elementoClasificador = (Clasificador)objetos[1];
                
                CheckBox checkCumple = (CheckBox)objetos[3];
                CheckBox checkNoCumple = (CheckBox)objetos[4];

                if (check.IsChecked == false)
                {
                    AsociacionTextoInformacion_Clasificador asociacion = (from A in AsociacionesTextosInformacion_Clasificadores
                                                                            where
                   A.CondicionesAsociadas == condiciones.Condiciones &
                   A.ElementoClasificador == elementoClasificador
                                                                          select A).FirstOrDefault();

                    if (asociacion != null)
                        AsociacionesTextosInformacion_Clasificadores.Remove(asociacion);

                    checkCumple.Tag = null;
                    checkCumple.IsChecked = false;
                    checkCumple.Visibility = Visibility.Collapsed;

                    checkNoCumple.Tag = null;
                    checkNoCumple.IsChecked = false;
                    checkNoCumple.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void CheckElementoClasificador_Checked1(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;

            if (check.Tag != null)
            {
                object[] objetos = (object[])check.Tag;
                OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[0];
                Clasificador elementoClasificador = (Clasificador)objetos[1];
               
                CheckBox checkCumple = (CheckBox)objetos[3];
                CheckBox checkNoCumple = (CheckBox)objetos[4];

                if (check.IsChecked == true)
                {
                    AsociacionTextoInformacion_Clasificador asociacion = new AsociacionTextoInformacion_Clasificador();
                    asociacion.ElementoClasificador = elementoClasificador;
                    asociacion.CondicionesAsociadas = condiciones.Condiciones;
                    AsociacionesTextosInformacion_Clasificadores.Add(asociacion);

                    checkCumple.IsChecked = true;
                    checkCumple.Tag = asociacion;
                    checkCumple.Visibility = Visibility.Visible;

                    checkNoCumple.IsChecked = false;
                    checkNoCumple.Tag = asociacion;
                    checkNoCumple.Visibility = Visibility.Visible;
                }
            }
        }

        private void QuitarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;
                        
            var asociaciones = (from E in AsociacionesTextosInformacion_ElementosSalida where E.CondicionesAsociadas == CondicionesTextosInformacion[indice].Condiciones select E).ToList();

            while (asociaciones.Any())
            {
                AsociacionesTextosInformacion_ElementosSalida.Remove(asociaciones.FirstOrDefault());
                asociaciones.Remove(asociaciones.FirstOrDefault());
            }

            var asociacionesClasificadores = (from E in AsociacionesTextosInformacion_Clasificadores where E.CondicionesAsociadas == CondicionesTextosInformacion[indice].Condiciones select E).ToList();

            while (asociacionesClasificadores.Any())
            {
                AsociacionesTextosInformacion_Clasificadores.Remove(asociacionesClasificadores.FirstOrDefault());
                asociacionesClasificadores.Remove(asociacionesClasificadores.FirstOrDefault());
            }

            CondicionesTextosInformacion.RemoveAt(indice);
            ListarTextos();
        }

        private void SubirTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice > 0)
            {
                CondicionesAsignacionSalidas_TextosInformacion condiciones = CondicionesTextosInformacion[indice];
                CondicionesTextosInformacion.RemoveAt(indice);

                CondicionesTextosInformacion.Insert(indice - 1, condiciones);
                ListarTextos();
            }
        }

        private void BajarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice < CondicionesTextosInformacion.Count - 1)
            {
                CondicionesAsignacionSalidas_TextosInformacion condiciones = CondicionesTextosInformacion[indice];
                CondicionesTextosInformacion.RemoveAt(indice);

                CondicionesTextosInformacion.Insert(indice + 1, condiciones);
                ListarTextos();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListarTextos();
        }

        private void agregarTextoInformacion_Click(object sender, RoutedEventArgs e)
        {
            //if (condicionesAgregar.Condiciones != null)
            //{
                List<DiseñoOperacion> Operandos_Condiciones = new List<DiseñoOperacion>();
                List<DiseñoElementoOperacion> SubOperandos_Condiciones = new List<DiseñoElementoOperacion>();
                Operandos_Condiciones.AddRange(Operandos);

                if (ModoOperacion)
                {
                    SubOperandos_Condiciones.AddRange(SubOperandos);
                }

                CondicionesTextosInformacion.Add(new CondicionesAsignacionSalidas_TextosInformacion()
                {
                    Condiciones = new CondicionTextosInformacion() { ContenedorCondiciones = true },
                    Operandos_AplicarCondiciones = Operandos_Condiciones,
                    SubOperandos_AplicarCondiciones = SubOperandos_Condiciones
                });
                ListarTextos();
            //}
        }

        private void opcionActivarCondicionCantidadesNocumplen_Anteriores_Checked(object sender, RoutedEventArgs e)
        {
            if(!Cargando)
            {
                if(CondicionesTextosInformacion != null)
                {
                    if(!CondicionesTextosInformacion.Any(i => i.Condiciones != null && i.Condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores))
                    {
                        CondicionesTextosInformacion.Add(new CondicionesAsignacionSalidas_TextosInformacion()
                        {
                            Condiciones = new CondicionTextosInformacion() { ContenedorCondiciones = true, EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores = true },
                        });
                        ListarTextos();
                    }
                }
            }
        }

        private void opcionActivarCondicionCantidadesNocumplen_Anteriores_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (CondicionesTextosInformacion != null)
                {
                    var condicion = CondicionesTextosInformacion.FirstOrDefault(i => i.Condiciones != null && i.Condiciones.EsCondicionCuandoCantidadesNoCumplen_CondicionesAnteriores);
                    if (condicion != null)
                    {
                        CondicionesTextosInformacion.Remove(condicion);
                        ListarTextos();
                    }
                }
            }
        }
    }
}
