using PlanMath_para_Excel;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Lógica de interacción para Implicacion_TextosInformacion.xaml
    /// </summary>
    public partial class Implicacion_TextosInformacion : UserControl
    {
        public List<AsignacionImplicacion_TextosInformacion> ImplicacionesTextosInformacion { get; set; }
        public List<DiseñoTextosInformacion> Entradas { get; set; }
        public List<DiseñoOperacion> Elementos { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoTextosInformacion> Definiciones { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public DiseñoOperacion OperacionRelacionada_Definicion { get; set; }
        public DiseñoElementoOperacion SubOperacionRelacionada_Definicion { get; set; }
        public bool ModoOperacion { get; set; }
        public DiseñoCalculo CalculoAsociado { get; set; }
        public Implicacion_TextosInformacion()
        {
            InitializeComponent();
        }
        bool Cargando = false;
        private void ListarTextos()
        {
            if (ImplicacionesTextosInformacion != null)
            {
                //QuitarEventosCheckedUncheked();
                textosInformacionSeleccionados.Children.Clear();
                Cargando = true;

                int indice = 0;

                foreach (var item in ImplicacionesTextosInformacion)
                {
                    textosInformacionSeleccionados.RowDefinitions.Add(new RowDefinition());
                    textosInformacionSeleccionados.RowDefinitions.Last().Height = GridLength.Auto;

                    TextBlock numero = new TextBlock();
                    numero.Text = (indice + 1).ToString() + " Las condiciones (si/entonces):";
                    numero.Margin = new Thickness(10);
                    numero.VerticalAlignment = VerticalAlignment.Center;

                    textosInformacionSeleccionados.Children.Add(numero);

                    Grid.SetRow(numero, indice);
                    Grid.SetColumn(numero, 0);

                    ScrollViewer contenedorCondiciones = new ScrollViewer();
                    contenedorCondiciones.MaxWidth = 400;
                    contenedorCondiciones.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    contenedorCondiciones.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                    contenedorCondiciones.VerticalAlignment = VerticalAlignment.Center;

                    OpcionCondiciones_ImplicacionTextosInformacion condiciones = new OpcionCondiciones_ImplicacionTextosInformacion();
                    condiciones.Condiciones = item.Condiciones_TextoCondicion;
                    condiciones.asignacionImplicacion = item;
                    condiciones.Entradas = Entradas;

                    List<DiseñoOperacion> ListaOperandos = new List<DiseñoOperacion>();
                    ListaOperandos.AddRange(Operandos);
                    ListaOperandos.Add(OperacionRelacionada_Definicion); 
                    condiciones.Operandos = ListaOperandos;

                    condiciones.Elementos = Elementos;
                    condiciones.SubOperandos = SubOperandos;
                    condiciones.Definiciones = Definiciones;
                    condiciones.DefinicionesListas = DefinicionesListas;
                    condiciones.Margin = new Thickness(10);
                    condiciones.VerticalAlignment = VerticalAlignment.Center;

                    contenedorCondiciones.Content = condiciones;

                    textosInformacionSeleccionados.Children.Add(contenedorCondiciones);

                    Grid.SetRow(contenedorCondiciones, indice);
                    Grid.SetColumn(contenedorCondiciones, 1);

                    TextBlock implica = new TextBlock();
                    implica.Text = "implican asignar cadenas de texto cuando:";
                    implica.Margin = new Thickness(10);
                    implica.VerticalAlignment = VerticalAlignment.Center;

                    textosInformacionSeleccionados.Children.Add(implica);

                    Grid.SetRow(implica, indice);
                    Grid.SetColumn(implica, 2);

                    Grid grillaInstancias = new Grid();

                    for (int contarColumna = 1; contarColumna <= 6; contarColumna++)
                    {
                        grillaInstancias.ColumnDefinitions.Add(new ColumnDefinition());
                        grillaInstancias.ColumnDefinitions.Last().Width = GridLength.Auto;
                    }

                    grillaInstancias.RowDefinitions.Add(new RowDefinition());
                    grillaInstancias.RowDefinitions.Last().Height = GridLength.Auto;

                    Button agregarInstancia = new Button();
                    agregarInstancia.Content = "Agregar lógica de asignación";
                    agregarInstancia.Margin = new Thickness(10);
                    agregarInstancia.Tag = item;
                    agregarInstancia.Click += AgregarInstancia_Click;

                    int indiceInstancia = 0;
                    int indiceColumnaInstancia = 0;

                    grillaInstancias.Children.Add(agregarInstancia);
                    Grid.SetRow(agregarInstancia, indiceInstancia);
                    Grid.SetColumn(agregarInstancia, indiceColumnaInstancia);
                    
                    indiceInstancia++;

                    foreach (var itemInstancia in item.InstanciasAsignacion)
                    {
                        grillaInstancias.RowDefinitions.Add(new RowDefinition());
                        grillaInstancias.RowDefinitions.Last().Height = GridLength.Auto;

                        Button botonOpcionAsignacionOperandosCuando = new Button();
                        botonOpcionAsignacionOperandosCuando.Margin = new Thickness(10);
                        botonOpcionAsignacionOperandosCuando.VerticalAlignment = VerticalAlignment.Center;
                        //comboOpcionAsignacion.Tag = new object[] { indice, };
                        ListarOpcionesBotonTextoAsignacionOperandosCuando(botonOpcionAsignacionOperandosCuando, itemInstancia, item);

                        grillaInstancias.Children.Add(botonOpcionAsignacionOperandosCuando);

                        Grid.SetRow(botonOpcionAsignacionOperandosCuando, indiceInstancia);
                        Grid.SetColumn(botonOpcionAsignacionOperandosCuando, indiceColumnaInstancia);

                        indiceColumnaInstancia++;

                        Button botonOpcionAsignacion = new Button();
                        botonOpcionAsignacion.Margin = new Thickness(10);
                        botonOpcionAsignacion.VerticalAlignment = VerticalAlignment.Center;
                        //comboOpcionAsignacion.Tag = new object[] { indice, };
                        ListarOpcionesBotonTextoAsignacion(botonOpcionAsignacion, itemInstancia);

                        grillaInstancias.Children.Add(botonOpcionAsignacion);

                        Grid.SetRow(botonOpcionAsignacion, indiceInstancia);
                        Grid.SetColumn(botonOpcionAsignacion, indiceColumnaInstancia);

                        indiceColumnaInstancia++;

                        Button botonOpcionAsignacionOperandos = new Button();
                        botonOpcionAsignacionOperandos.Margin = new Thickness(10);
                        botonOpcionAsignacionOperandos.VerticalAlignment = VerticalAlignment.Center;
                        //comboOpcionAsignacion.Tag = new object[] { indice, };
                        ListarOpcionesBotonTextoAsignacionOperandos(botonOpcionAsignacionOperandos, itemInstancia);

                        grillaInstancias.Children.Add(botonOpcionAsignacionOperandos);

                        Grid.SetRow(botonOpcionAsignacionOperandos, indiceInstancia);
                        Grid.SetColumn(botonOpcionAsignacionOperandos, indiceColumnaInstancia);

                        indiceColumnaInstancia++;                        
                        
                        Button opcionConservarTextosInformacion = new Button();
                        opcionConservarTextosInformacion.Margin = new Thickness(10);
                        opcionConservarTextosInformacion.VerticalAlignment = VerticalAlignment.Center;
                        opcionConservarTextosInformacion.Tag = item;
                        ListarOpcionesComboConservarTextosInformacion(opcionConservarTextosInformacion, itemInstancia);

                        grillaInstancias.Children.Add(opcionConservarTextosInformacion);

                        Grid.SetRow(opcionConservarTextosInformacion, indiceInstancia);
                        Grid.SetColumn(opcionConservarTextosInformacion, indiceColumnaInstancia);

                        indiceColumnaInstancia++;

                        if (indiceInstancia > 1)
                        {
                            Image ImagenBotonQuitarInstancia = new Image();
                            //ImagenBotonQuitarInstancia.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\19.png", UriKind.Relative));
                            ImagenBotonQuitarInstancia.Width = 24;
                            ImagenBotonQuitarInstancia.Height = 24;

                            Button botonQuitarInstancia = new Button();
                            botonQuitarInstancia.Content = "Quitar lógica de asignación"; //ImagenBotonQuitarInstancia;
                            botonQuitarInstancia.VerticalAlignment = VerticalAlignment.Center;
                            botonQuitarInstancia.Margin = new Thickness(10);
                            botonQuitarInstancia.Tag = new object[] { item, indiceInstancia - 1 };
                            botonQuitarInstancia.Click += BotonQuitarInstancia_Click;
                        
                            grillaInstancias.Children.Add(botonQuitarInstancia);

                            Grid.SetRow(botonQuitarInstancia, indiceInstancia);
                            Grid.SetColumn(botonQuitarInstancia, indiceColumnaInstancia);
                        }

                        indiceInstancia++;
                        indiceColumnaInstancia = 0;
                    }

                    textosInformacionSeleccionados.Children.Add(grillaInstancias);

                    Grid.SetRow(grillaInstancias, indice);
                    Grid.SetColumn(grillaInstancias, 3);                    

                    Button botonEstablecerOpcionesNombresCantidades = new Button();
                    botonEstablecerOpcionesNombresCantidades.Content = "Opciones de nombres de variables, vectores o números";
                    botonEstablecerOpcionesNombresCantidades.VerticalAlignment = VerticalAlignment.Center;
                    botonEstablecerOpcionesNombresCantidades.Margin = new Thickness(10);
                    botonEstablecerOpcionesNombresCantidades.Tag = indice;
                    botonEstablecerOpcionesNombresCantidades.Click += EstablecerOpcionesNombresCantidades;

                    textosInformacionSeleccionados.Children.Add(botonEstablecerOpcionesNombresCantidades);

                    Grid.SetRow(botonEstablecerOpcionesNombresCantidades, indice);
                    Grid.SetColumn(botonEstablecerOpcionesNombresCantidades, 4);

                    Image ImagenBotonQuitar = new Image();
                    ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\19.png", UriKind.Relative));
                    ImagenBotonQuitar.Width = 24;
                    ImagenBotonQuitar.Height = 24;

                    Button botonQuitar = new Button();
                    botonQuitar.Content = ImagenBotonQuitar;
                    botonQuitar.VerticalAlignment = VerticalAlignment.Center;
                    botonQuitar.Margin = new Thickness(10);
                    botonQuitar.Tag = indice;
                    botonQuitar.Click += QuitarTexto;

                    textosInformacionSeleccionados.Children.Add(botonQuitar);

                    Grid.SetRow(botonQuitar, indice);
                    Grid.SetColumn(botonQuitar, 5);

                    Image ImagenBotonSubir = new Image();
                    ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\20.png", UriKind.Relative));
                    ImagenBotonSubir.Width = 24;
                    ImagenBotonSubir.Height = 24;

                    Button botonSubir = new Button();
                    botonSubir.Content = ImagenBotonSubir;
                    botonSubir.VerticalAlignment = VerticalAlignment.Center;
                    botonSubir.Margin = new Thickness(10);
                    botonSubir.Tag = indice;
                    botonSubir.Click += SubirTexto;

                    textosInformacionSeleccionados.Children.Add(botonSubir);

                    Grid.SetRow(botonSubir, indice);
                    Grid.SetColumn(botonSubir, 6);

                    Image ImagenBotonBajar = new Image();
                    ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\21.png", UriKind.Relative));
                    ImagenBotonBajar.Width = 24;
                    ImagenBotonBajar.Height = 24;

                    Button botonBajar = new Button();
                    botonBajar.Content = ImagenBotonBajar;
                    botonBajar.VerticalAlignment = VerticalAlignment.Center;
                    botonBajar.Margin = new Thickness(10);
                    botonBajar.Tag = indice;
                    botonBajar.Click += BajarTexto;

                    textosInformacionSeleccionados.Children.Add(botonBajar);

                    Grid.SetRow(botonBajar, indice);
                    Grid.SetColumn(botonBajar, 7);

                    indice++;
                }

                Cargando = false;
            }
        }
        private void BotonQuitarInstancia_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            if (boton.Tag != null)
            {
                object[] objetos = (object[])boton.Tag;

                AsignacionImplicacion_TextosInformacion asignacion = (AsignacionImplicacion_TextosInformacion)objetos[0];
                int indiceInstancia = (int)objetos[1];

                if (asignacion != null && indiceInstancia >= 0 && indiceInstancia <= asignacion.InstanciasAsignacion.Count - 1)
                {
                    asignacion.InstanciasAsignacion.RemoveAt(indiceInstancia);
                    ListarTextos();
                }
            }
        }

        private void AgregarInstancia_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            if (boton.Tag != null)
            {
                AsignacionImplicacion_TextosInformacion asignacion = (AsignacionImplicacion_TextosInformacion)boton.Tag;
                asignacion.InstanciasAsignacion.Add(new InstanciaAsignacionImplicacion_TextosInformacion());

                asignacion.InstanciasAsignacion.LastOrDefault().Entradas_DesdeAsignarTextosInformacion = Entradas.ToList();
                asignacion.InstanciasAsignacion.LastOrDefault().Operandos_DesdeAsignarTextosInformacion = Operandos.ToList();                    
                asignacion.InstanciasAsignacion.LastOrDefault().SubOperandos_DesdeAsignarTextosInformacion = SubOperandos.ToList();

                ListarTextos();
            }
        }

        private void ListarOpcionesBotonTextoAsignacion(Button boton, InstanciaAsignacionImplicacion_TextosInformacion itemInstancia)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Asignar los textos ";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            CheckBox opcionClasificadores = new CheckBox();
            opcionClasificadores.Padding = new Thickness(20, 10, 20, 10);
            opcionClasificadores.VerticalContentAlignment = VerticalAlignment.Center;
            opcionClasificadores.Tag = itemInstancia;

            opcionClasificadores.Checked -= OpcionClasificadores_Checked;
            opcionClasificadores.Unchecked -= OpcionClasificadores_Unchecked;

            opcionClasificadores.IsChecked = itemInstancia.AsignarCadenasTexto_Clasificadores;

            opcionClasificadores.Checked += OpcionClasificadores_Checked;
            opcionClasificadores.Unchecked += OpcionClasificadores_Unchecked;
            opcionClasificadores.Content = "Las cadenas de texto asignadas en esta lógica son clasificadores";

            menuBoton.Items.Add(opcionClasificadores);

            ComboBoxItem opcionTextoFijo = new ComboBoxItem();
            TextBox textoFijo = new TextBox();
            textoFijo.Padding = new Thickness(20, 10, 20, 10);
            textoFijo.Tag = itemInstancia;

            List<DiseñoOperacion> operandosSeleccionados = null;
            List<DiseñoElementoOperacion> subOperandosSeleccionados = null;

            if (itemInstancia != null)
            {
                textoFijo.Text = itemInstancia.TextoImplicaAsignacion;
                operandosSeleccionados = itemInstancia.Operandos_AsignarTextosInformacionCuando;
                subOperandosSeleccionados = itemInstancia.SubOperandos_AsignarTextosInformacionCuando;
            }

            if(!operandosSeleccionados.Any() &&
                itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones)
            {
                operandosSeleccionados = Operandos.ToList();
            }

            if (!subOperandosSeleccionados.Any() &&
                itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones)
            {
                subOperandosSeleccionados = SubOperandos.ToList();
            }

            textoFijo.TextChanged += EditarTexto;
            opcionTextoFijo.Content = textoFijo;
            menuBoton.Items.Add(opcionTextoFijo);

            CheckBox opcionDigitarTextos = new CheckBox();
            opcionDigitarTextos.Padding = new Thickness(20, 10, 20, 10);
            opcionDigitarTextos.VerticalContentAlignment = VerticalAlignment.Center;
            opcionDigitarTextos.Tag = itemInstancia;

            opcionDigitarTextos.Checked -= OpcionDigitarTextos_Checked;
            opcionDigitarTextos.Unchecked -= OpcionDigitarTextos_Unchecked;

            opcionDigitarTextos.IsChecked = itemInstancia.DigitarTextosInformacion_EnEjecucion;

            opcionDigitarTextos.Checked += OpcionDigitarTextos_Checked;
            opcionDigitarTextos.Unchecked += OpcionDigitarTextos_Unchecked;
            opcionDigitarTextos.Content = "Digitar las cadenas de texto en la ejecución de la lógica";

            menuBoton.Items.Add(opcionDigitarTextos);

            //menuBoton.Template = new ControlTemplate(typeof(Border));

            if (Entradas != null)
            {
                foreach (var itemEntrada in Entradas.Distinct())
                {
                    Grid opcionEntradas = new Grid();
                    
                    opcionEntradas.RowDefinitions.Add(new RowDefinition());
                    opcionEntradas.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionEntradas.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionEntradas.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionEntradas.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionEntradas.ColumnDefinitions.Last().Width = GridLength.Auto;

                    Grid opciones = new Grid();
                    opciones.ColumnDefinitions.Add(new ColumnDefinition());
                    opciones.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;

                    Grid opcionesTodosSusTextos = new Grid();
                    opcionesTodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesTodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesTodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesTodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesTodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesTodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionesTodosSusTextos.Margin = new Thickness(0, 10, 0, 0);

                    CheckBox checkEntradas = new CheckBox();
                    CheckBox checkCantidadesCondiciones = new CheckBox();
                    CheckBox checkTodosSusTextos = new CheckBox();
                    CheckBox checkSusTextosCondiciones = new CheckBox();
                    CheckBox checkSusTextosDefiniciones = new CheckBox();
                    CheckBox checkCantidadesComoTextos = new CheckBox();
                    CheckBox checkCantidadesDeCantidadesComoTextos = new CheckBox();

                    checkEntradas.VerticalAlignment = VerticalAlignment.Center;
                    checkEntradas.Content = "Desde la variable o vector de entrada " + itemEntrada.EntradaRelacionada.Nombre;
                    checkEntradas.Tag = new object[] { itemInstancia, itemEntrada, checkTodosSusTextos, checkSusTextosCondiciones, 
                        checkSusTextosDefiniciones , checkCantidadesCondiciones };

                    checkEntradas.Checked -= CheckEntradas_Checked;
                    checkEntradas.Unchecked -= CheckEntradas_Unchecked;

                    if (itemInstancia != null)
                        checkEntradas.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion.Contains(itemEntrada);

                    checkEntradas.Checked += CheckEntradas_Checked;
                    checkEntradas.Unchecked += CheckEntradas_Unchecked;

                    opcionEntradas.Children.Add(checkEntradas);
                    Grid.SetColumn(checkEntradas, 0);
                    Grid.SetRow(checkEntradas, 0);

                    OpcionCondiciones_TextosInformacion condicionesCantidades = new OpcionCondiciones_TextosInformacion();
                    condicionesCantidades.MostrarOpcionesTextosImplicacionAsignacion = true;
                    condicionesCantidades.ModoSeleccionEntradas = true;

                    checkCantidadesCondiciones.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesCondiciones.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesCondiciones.Content = "Cantidades que cumplen las condiciones (si/entonces): ";
                    checkCantidadesCondiciones.Tag = new object[] { itemInstancia, itemEntrada, condicionesCantidades };

                    checkCantidadesCondiciones.Checked -= CheckCantidadesCondiciones_Checked;
                    checkCantidadesCondiciones.Unchecked -= CheckCantidadesCondiciones_Unchecked;

                    if (checkEntradas.IsChecked == false)
                        checkCantidadesCondiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesCondiciones.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones.Contains(itemEntrada);

                    checkCantidadesCondiciones.Checked += CheckCantidadesCondiciones_Checked;
                    checkCantidadesCondiciones.Unchecked += CheckCantidadesCondiciones_Unchecked;

                    opciones.Children.Add(checkCantidadesCondiciones);
                    Grid.SetColumn(checkCantidadesCondiciones, 0);
                    Grid.SetRow(checkCantidadesCondiciones, 0);

                    condicionesCantidades.VerticalAlignment = VerticalAlignment.Center;
                    condicionesCantidades.Margin = new Thickness(5, 10, 0, 0);

                    condicionesCantidades.Operandos = Operandos;
                    condicionesCantidades.ElementoEntradaAsociado_Asignacion = itemEntrada.EntradaRelacionada;
                    condicionesCantidades.ModoAsignacionLogicaImplicaciones = true;

                    if (checkCantidadesCondiciones.IsChecked == false)
                        condicionesCantidades.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                    {
                        condicionesCantidades.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Entrada == itemEntrada);
                        condicionesCantidades.Condiciones = condicionesCantidades.AsociacionTextosInformacionOperando_Implicacion?.Condiciones;
                    }

                    opciones.Children.Add(condicionesCantidades);
                    Grid.SetColumn(condicionesCantidades, 0);
                    Grid.SetRow(condicionesCantidades, 1);

                    Grid opcionesCheks_TodosSusTextos = new Grid();
                    opcionesCheks_TodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesCheks_TodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;

                    checkTodosSusTextos.VerticalAlignment= VerticalAlignment.Center;
                    checkTodosSusTextos.Margin = new Thickness(5,0,0,0);
                    checkTodosSusTextos.Content = "Todas sus cadenas de texto";
                    checkTodosSusTextos.Tag = new object[] { itemInstancia, itemEntrada, opcionesCheks_TodosSusTextos };

                    checkTodosSusTextos.Checked -= CheckTodosSusTextos_Checked;
                    checkTodosSusTextos.Unchecked -= CheckTodosSusTextos_Unchecked;

                    if (checkEntradas.IsChecked == false)
                        checkTodosSusTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkTodosSusTextos.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos.Contains(itemEntrada);

                    checkTodosSusTextos.Checked += CheckTodosSusTextos_Checked;
                    checkTodosSusTextos.Unchecked += CheckTodosSusTextos_Unchecked;

                    opcionesTodosSusTextos.Children.Add(checkTodosSusTextos);
                    Grid.SetColumn(checkTodosSusTextos, 0);
                    Grid.SetRow(checkTodosSusTextos, 0);
                                        
                    if (checkTodosSusTextos.IsChecked == true)
                        opcionesCheks_TodosSusTextos.Visibility = Visibility.Visible;
                    else
                        opcionesCheks_TodosSusTextos.Visibility = Visibility.Collapsed;

                    RadioButton opcionTodosSusTextos_Elemento = new RadioButton();
                    opcionTodosSusTextos_Elemento.Content = "Las cadenas de texto de todos sus números dentro de los vectores";
                    opcionTodosSusTextos_Elemento.GroupName = "OpcionPosicionTodosNumeros-" + itemInstancia.GetHashCode().ToString() + "-" + itemEntrada.GetHashCode().ToString();
                    opcionTodosSusTextos_Elemento.Tag = new object[] { itemInstancia, itemEntrada };

                    opcionTodosSusTextos_Elemento.Checked -= OpcionTodosSusTextos_Elemento_Checked;
                    opcionTodosSusTextos_Elemento.Unchecked -= OpcionTodosSusTextos_Elemento_Unchecked;

                    if (itemInstancia != null)
                        opcionTodosSusTextos_Elemento.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Contains(itemEntrada);

                    opcionesCheks_TodosSusTextos.Children.Add(opcionTodosSusTextos_Elemento);
                    Grid.SetColumn(opcionTodosSusTextos_Elemento, 0);
                    Grid.SetRow(opcionTodosSusTextos_Elemento, 0);

                    RadioButton opcionTextosPosicionActual_Elemento = new RadioButton();
                    opcionTextosPosicionActual_Elemento.Content = "Las cadenas de texto del número de la posición actual dentro del vector";
                    opcionTextosPosicionActual_Elemento.GroupName = "OpcionPosicionTodosNumeros-" + itemInstancia.GetHashCode().ToString() + "-" + itemEntrada.GetHashCode().ToString();
                    opcionTextosPosicionActual_Elemento.Tag = new object[] { itemInstancia, itemEntrada };

                    opcionTextosPosicionActual_Elemento.Checked -= OpcionTextosPosicionActual_Elemento_Checked;
                    opcionTextosPosicionActual_Elemento.Unchecked -= OpcionTextosPosicionActual_Elemento_Unchecked;

                    if (itemInstancia != null)
                        opcionTextosPosicionActual_Elemento.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Contains(itemEntrada);

                    opcionTextosPosicionActual_Elemento.Checked += OpcionTextosPosicionActual_Elemento_Checked;
                    opcionTextosPosicionActual_Elemento.Unchecked += OpcionTextosPosicionActual_Elemento_Unchecked;

                    opcionTodosSusTextos_Elemento.Checked += OpcionTodosSusTextos_Elemento_Checked;
                    opcionTodosSusTextos_Elemento.Unchecked += OpcionTodosSusTextos_Elemento_Unchecked;

                    opcionesCheks_TodosSusTextos.Children.Add(opcionTextosPosicionActual_Elemento);
                    Grid.SetColumn(opcionTextosPosicionActual_Elemento, 1);
                    Grid.SetRow(opcionTextosPosicionActual_Elemento, 0);

                    opcionesTodosSusTextos.Children.Add(opcionesCheks_TodosSusTextos);
                    Grid.SetColumn(opcionesCheks_TodosSusTextos, 0);
                    Grid.SetRow(opcionesCheks_TodosSusTextos, 1);

                    opciones.Children.Add(opcionesTodosSusTextos);
                    Grid.SetColumn(opcionesTodosSusTextos, 0);
                    Grid.SetRow(opcionesTodosSusTextos, 2);

                    OpcionCondiciones_TextosInformacion condiciones = new OpcionCondiciones_TextosInformacion();
                    condiciones.MostrarOpcionesTextosImplicacionAsignacion = true;
                    condiciones.ModoSeleccionEntradas = true;

                    checkSusTextosCondiciones.VerticalAlignment = VerticalAlignment.Center;
                    checkSusTextosCondiciones.Margin = new Thickness(5, 10, 0, 0);
                    checkSusTextosCondiciones.Content = "Sus cadenas de texto que cumple las condiciones (si/entonces): ";
                    checkSusTextosCondiciones.Tag = new object[] { itemInstancia, itemEntrada, condiciones };

                    checkSusTextosCondiciones.Checked -= CheckSusTextosCondiciones_Checked;
                    checkSusTextosCondiciones.Unchecked -= CheckSusTextosCondiciones_Unchecked;

                    if (checkEntradas.IsChecked == false)
                        checkSusTextosCondiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkSusTextosCondiciones.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones.Contains(itemEntrada);

                    checkSusTextosCondiciones.Checked += CheckSusTextosCondiciones_Checked;
                    checkSusTextosCondiciones.Unchecked += CheckSusTextosCondiciones_Unchecked;

                    opciones.Children.Add(checkSusTextosCondiciones);
                    Grid.SetColumn(checkSusTextosCondiciones, 0);
                    Grid.SetRow(checkSusTextosCondiciones, 3);

                    condiciones.VerticalAlignment = VerticalAlignment.Center;
                    condiciones.Margin = new Thickness(5, 10, 0, 0);

                    //if(Elementos != null)
                    //    condiciones.Operandos = Elementos.Where(item => (item.EntradaRelacionada != null && ((item.EntradaRelacionada.Tipo != TipoEntrada.TextosInformacion 
                    //    && item.EntradaRelacionada == itemEntrada.EntradaRelacionada) ||
                    //    item.EntradaRelacionada.Tipo == TipoEntrada.TextosInformacion) ||
                    //    item.EntradaRelacionada == null)).ToList();
                    condiciones.Operandos = Operandos;
                    condiciones.ElementoEntradaAsociado_Asignacion = itemEntrada.EntradaRelacionada;
                    condiciones.ModoAsignacionLogicaImplicaciones = true;

                    if (checkSusTextosCondiciones.IsChecked == false)
                        condiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                    {
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Entrada == itemEntrada);
                        condiciones.Condiciones = condiciones.AsociacionTextosInformacionOperando_Implicacion?.Condiciones;
                    }

                    opciones.Children.Add(condiciones);
                    Grid.SetColumn(condiciones, 0);
                    Grid.SetRow(condiciones, 4);

                    OpcionDefiniciones_TextosInformacion definiciones = new OpcionDefiniciones_TextosInformacion();

                    checkSusTextosDefiniciones.VerticalAlignment = VerticalAlignment.Center;
                    checkSusTextosDefiniciones.Margin = new Thickness(5, 10, 0, 0);
                    checkSusTextosDefiniciones.Content = "Definiciones de lógicas de asignación de cadenas de texto: ";
                    checkSusTextosDefiniciones.Tag = new object[] { itemInstancia, itemEntrada, definiciones };

                    checkSusTextosDefiniciones.Checked -= CheckSusTextosDefiniciones_Checked;
                    checkSusTextosDefiniciones.Unchecked -= CheckSusTextosDefiniciones_Unchecked;

                    if (checkEntradas.IsChecked == false)
                        checkSusTextosDefiniciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkSusTextosDefiniciones.IsChecked = itemInstancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos.Contains(itemEntrada);

                    checkSusTextosDefiniciones.Checked += CheckSusTextosDefiniciones_Checked;
                    checkSusTextosDefiniciones.Unchecked += CheckSusTextosDefiniciones_Unchecked;

                    opciones.Children.Add(checkSusTextosDefiniciones);
                    Grid.SetColumn(checkSusTextosDefiniciones, 0);
                    Grid.SetRow(checkSusTextosDefiniciones, 5);

                    definiciones.VerticalAlignment = VerticalAlignment.Center;
                    definiciones.Margin = new Thickness(5, 0, 0, 0);
                    definiciones.MaxWidth = 500;

                    //if(Elementos != null)
                    //    definiciones.Operandos = Elementos.Where(item => (item.EntradaRelacionada != null && ((item.EntradaRelacionada.Tipo != TipoEntrada.TextosInformacion
                    //    && item.EntradaRelacionada == itemEntrada.EntradaRelacionada) ||
                    //    item.EntradaRelacionada.Tipo == TipoEntrada.TextosInformacion) ||
                    //    item.EntradaRelacionada == null)).ToList();
                    definiciones.Operandos = Operandos;
                    definiciones.CalculoAsociado = CalculoAsociado;

                    if (checkSusTextosDefiniciones.IsChecked == false)
                        definiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        definiciones.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.FirstOrDefault(item => item.Entrada == itemEntrada);

                    opciones.Children.Add(definiciones);
                    Grid.SetColumn(definiciones, 0);
                    Grid.SetRow(definiciones, 6);

                    opcionEntradas.Children.Add(opciones);
                    Grid.SetColumn(opciones, 1);
                    Grid.SetRow(opciones, 0);

                    Border borde = new Border();
                    borde.BorderBrush = agregarTextoInformacion.BorderBrush;
                    borde.BorderThickness = new Thickness(0, 0.5, 0, 0);
                    borde.Child = opcionEntradas;

                    menuBoton.Items.Add(borde);
                }
            }

            if (Operandos != null &&
                !ModoOperacion)
            {
                foreach (var itemOperando in Operandos.Where(i => operandosSeleccionados.Contains(i)).Distinct())
                {
                    Grid opcionOperandos = new Grid();
                    opcionOperandos.RowDefinitions.Add(new RowDefinition());
                    opcionOperandos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;

                    Grid opciones = new Grid();
                    opciones.ColumnDefinitions.Add(new ColumnDefinition());
                    opciones.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;

                    Grid opcionesTodosSusTextos = new Grid();
                    opcionesTodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesTodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesTodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesTodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesTodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesTodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionesTodosSusTextos.Margin = new Thickness(0, 10, 0, 0);

                    CheckBox checkOperandos = new CheckBox();
                    CheckBox checkCantidadesCondiciones = new CheckBox();
                    CheckBox checkTodosSusTextos = new CheckBox();
                    CheckBox checkSusTextosCondiciones = new CheckBox();
                    CheckBox checkSusTextosDefiniciones = new CheckBox();
                    CheckBox checkCantidadesComoTextos = new CheckBox();
                    CheckBox checkCantidadesDeCantidadesComoTextos = new CheckBox();

                    checkOperandos.VerticalAlignment = VerticalAlignment.Center;
                    checkOperandos.Content = "Desde la variable, vector o retornados " + itemOperando.NombreCombo;
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando, checkTodosSusTextos, checkSusTextosCondiciones, 
                        checkSusTextosDefiniciones, checkCantidadesCondiciones, checkCantidadesComoTextos , checkCantidadesDeCantidadesComoTextos };

                    checkOperandos.Checked -= CheckOperandos_Checked;
                    checkOperandos.Unchecked -= CheckOperandos_Unchecked;

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion.Contains(itemOperando);

                    checkOperandos.Checked += CheckOperandos_Checked;
                    checkOperandos.Unchecked += CheckOperandos_Unchecked;
                
                    opcionOperandos.Children.Add(checkOperandos);
                    Grid.SetColumn(checkOperandos, 0);
                    Grid.SetRow(checkOperandos, 0);

                    OpcionCondiciones_TextosInformacion condicionesCantidades = new OpcionCondiciones_TextosInformacion();
                    condicionesCantidades.MostrarOpcionesTextosImplicacionAsignacion = true;

                    checkCantidadesCondiciones.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesCondiciones.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesCondiciones.Content = "Cantidades que cumplen las condiciones (si/entonces): ";
                    checkCantidadesCondiciones.Tag = new object[] { itemInstancia, itemOperando, condicionesCantidades };

                    checkCantidadesCondiciones.Checked -= CheckCantidadesCondiciones_Checked1;
                    checkCantidadesCondiciones.Unchecked -= CheckCantidadesCondiciones_Unchecked1;

                    if (checkOperandos.IsChecked == false)
                        checkCantidadesCondiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesCondiciones.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Contains(itemOperando);

                    checkCantidadesCondiciones.Checked += CheckCantidadesCondiciones_Checked1;
                    checkCantidadesCondiciones.Unchecked += CheckCantidadesCondiciones_Unchecked1;

                    opciones.Children.Add(checkCantidadesCondiciones);
                    Grid.SetColumn(checkCantidadesCondiciones, 0);
                    Grid.SetRow(checkCantidadesCondiciones, 0);

                    condicionesCantidades.VerticalAlignment = VerticalAlignment.Center;
                    condicionesCantidades.Margin = new Thickness(5, 10, 0, 0);
                    condicionesCantidades.Operandos = Operandos;
                    condicionesCantidades.ElementoOperandoAsociado_Asignacion = itemOperando;
                    condicionesCantidades.ModoAsignacionLogicaImplicaciones = true;

                    if (checkCantidadesCondiciones.IsChecked == false)
                        condicionesCantidades.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                    {
                        condicionesCantidades.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Operando == itemOperando);
                        condicionesCantidades.Condiciones = condicionesCantidades.AsociacionTextosInformacionOperando_Implicacion?.Condiciones;
                    }

                    opciones.Children.Add(condicionesCantidades);
                    Grid.SetColumn(condicionesCantidades, 0);
                    Grid.SetRow(condicionesCantidades, 1);

                    Grid opcionesCheks_TodosSusTextos = new Grid();
                    opcionesCheks_TodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesCheks_TodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;

                    checkTodosSusTextos.VerticalAlignment = VerticalAlignment.Center;
                    checkTodosSusTextos.Margin = new Thickness(5, 10, 0, 0);
                    checkTodosSusTextos.Content = "Todas sus cadenas de texto";
                    checkTodosSusTextos.Tag = new object[] { itemInstancia, itemOperando, opcionesCheks_TodosSusTextos };

                    checkTodosSusTextos.Checked -= CheckTodosSusTextos_Checked1;
                    checkTodosSusTextos.Unchecked -= CheckTodosSusTextos_Unchecked1;

                    if (checkOperandos.IsChecked == false)
                        checkTodosSusTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkTodosSusTextos.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos.Contains(itemOperando);

                    checkTodosSusTextos.Checked += CheckTodosSusTextos_Checked1;
                    checkTodosSusTextos.Unchecked += CheckTodosSusTextos_Unchecked1;

                    opcionesTodosSusTextos.Children.Add(checkTodosSusTextos);
                    Grid.SetColumn(checkTodosSusTextos, 0);
                    Grid.SetRow(checkTodosSusTextos, 0);

                    if (checkTodosSusTextos.IsChecked == true)
                        opcionesCheks_TodosSusTextos.Visibility = Visibility.Visible;
                    else
                        opcionesCheks_TodosSusTextos.Visibility = Visibility.Collapsed;

                    RadioButton opcionTodosSusTextos_Elemento = new RadioButton();
                    opcionTodosSusTextos_Elemento.Content = "Las cadenas de texto de todos sus números dentro del vector";
                    opcionTodosSusTextos_Elemento.GroupName = "OpcionPosicionTodosNumeros-" + itemInstancia.GetHashCode().ToString() + "-" + itemOperando.GetHashCode().ToString();
                    opcionTodosSusTextos_Elemento.Tag = new object[] { itemInstancia, itemOperando };

                    opcionTodosSusTextos_Elemento.Checked -= OpcionTodosSusTextos_Elemento_Checked1;
                    opcionTodosSusTextos_Elemento.Unchecked -= OpcionTodosSusTextos_Elemento_Unchecked1;

                    if (itemInstancia != null)
                        opcionTodosSusTextos_Elemento.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Contains(itemOperando);

                    opcionesCheks_TodosSusTextos.Children.Add(opcionTodosSusTextos_Elemento);
                    Grid.SetColumn(opcionTodosSusTextos_Elemento, 0);
                    Grid.SetRow(opcionTodosSusTextos_Elemento, 0);

                    RadioButton opcionTextosPosicionActual_Elemento = new RadioButton();
                    opcionTextosPosicionActual_Elemento.Content = "Las cadenas de texto del número de la posición actual dentro del vector";
                    opcionTextosPosicionActual_Elemento.GroupName = "OpcionPosicionTodosNumeros-" + itemInstancia.GetHashCode().ToString() + "-" + itemOperando.GetHashCode().ToString();
                    opcionTextosPosicionActual_Elemento.Tag = new object[] { itemInstancia, itemOperando };

                    opcionTextosPosicionActual_Elemento.Checked -= OpcionTextosPosicionActual_Elemento_Checked1;
                    opcionTextosPosicionActual_Elemento.Unchecked -= OpcionTextosPosicionActual_Elemento_Unchecked1;

                    if (itemInstancia != null)
                        opcionTextosPosicionActual_Elemento.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Contains(itemOperando);

                    opcionTextosPosicionActual_Elemento.Checked += OpcionTextosPosicionActual_Elemento_Checked1;
                    opcionTextosPosicionActual_Elemento.Unchecked += OpcionTextosPosicionActual_Elemento_Unchecked1;

                    opcionTodosSusTextos_Elemento.Checked += OpcionTodosSusTextos_Elemento_Checked1;
                    opcionTodosSusTextos_Elemento.Unchecked += OpcionTodosSusTextos_Elemento_Unchecked1;

                    opcionesCheks_TodosSusTextos.Children.Add(opcionTextosPosicionActual_Elemento);
                    Grid.SetColumn(opcionTextosPosicionActual_Elemento, 1);
                    Grid.SetRow(opcionTextosPosicionActual_Elemento, 0);

                    opcionesTodosSusTextos.Children.Add(opcionesCheks_TodosSusTextos);
                    Grid.SetColumn(opcionesCheks_TodosSusTextos, 0);
                    Grid.SetRow(opcionesCheks_TodosSusTextos, 1);

                    opciones.Children.Add(opcionesTodosSusTextos);
                    Grid.SetColumn(opcionesTodosSusTextos, 0);
                    Grid.SetRow(opcionesTodosSusTextos, 2);

                    OpcionCondiciones_TextosInformacion condiciones = new OpcionCondiciones_TextosInformacion();
                    condiciones.MostrarOpcionesTextosImplicacionAsignacion = true;
                    
                    checkSusTextosCondiciones.VerticalAlignment = VerticalAlignment.Center;
                    checkSusTextosCondiciones.Margin = new Thickness(5, 10, 0, 0);
                    checkSusTextosCondiciones.Content = "Sus cadenas de texto que cumple las condiciones (si/entonces): ";
                    checkSusTextosCondiciones.Tag = new object[] { itemInstancia, itemOperando, condiciones };

                    checkSusTextosCondiciones.Checked -= CheckSusTextosCondiciones_Checked1;
                    checkSusTextosCondiciones.Unchecked -= CheckSusTextosCondiciones_Unchecked1;

                    if (checkOperandos.IsChecked == false)
                        checkSusTextosCondiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkSusTextosCondiciones.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Contains(itemOperando);

                    checkSusTextosCondiciones.Checked += CheckSusTextosCondiciones_Checked1;
                    checkSusTextosCondiciones.Unchecked += CheckSusTextosCondiciones_Unchecked1;

                    opciones.Children.Add(checkSusTextosCondiciones);
                    Grid.SetColumn(checkSusTextosCondiciones, 0);
                    Grid.SetRow(checkSusTextosCondiciones, 3);

                    condiciones.VerticalAlignment = VerticalAlignment.Center;
                    condiciones.Margin = new Thickness(5, 10, 0, 0);
                    condiciones.Operandos = Operandos;
                    condiciones.ElementoOperandoAsociado_Asignacion = itemOperando;
                    condiciones.ModoAsignacionLogicaImplicaciones = true;

                    if (checkSusTextosCondiciones.IsChecked == false)
                        condiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                    {
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Operando == itemOperando);
                        condiciones.Condiciones = condiciones.AsociacionTextosInformacionOperando_Implicacion?.Condiciones;
                    }

                    opciones.Children.Add(condiciones);
                    Grid.SetColumn(condiciones, 0);
                    Grid.SetRow(condiciones, 4);

                    OpcionDefiniciones_TextosInformacion definiciones = new OpcionDefiniciones_TextosInformacion();

                    checkSusTextosDefiniciones.VerticalAlignment = VerticalAlignment.Center;
                    checkSusTextosDefiniciones.Margin = new Thickness(5, 10, 0, 0);
                    checkSusTextosDefiniciones.Content = "Definiciones de lógicas de asignación de cadenas de texto: ";
                    checkSusTextosDefiniciones.Tag = new object[] { itemInstancia, itemOperando, definiciones };

                    checkSusTextosDefiniciones.Checked -= CheckSusTextosDefiniciones_Checked1;
                    checkSusTextosDefiniciones.Unchecked -= CheckSusTextosDefiniciones_Unchecked1;

                    if (checkOperandos.IsChecked == false)
                        checkSusTextosDefiniciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkSusTextosDefiniciones.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Contains(itemOperando);

                    checkSusTextosDefiniciones.Checked += CheckSusTextosDefiniciones_Checked1;
                    checkSusTextosDefiniciones.Unchecked += CheckSusTextosDefiniciones_Unchecked1;

                    opciones.Children.Add(checkSusTextosDefiniciones);
                    Grid.SetColumn(checkSusTextosDefiniciones, 0);
                    Grid.SetRow(checkSusTextosDefiniciones, 5);

                    definiciones.VerticalAlignment = VerticalAlignment.Center;
                    definiciones.Margin = new Thickness(5, 10, 0, 0);
                    definiciones.Operandos = Operandos;
                    definiciones.CalculoAsociado = CalculoAsociado;

                    if (checkSusTextosDefiniciones.IsChecked == false)
                        definiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        definiciones.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.Operando == itemOperando);

                    opciones.Children.Add(definiciones);
                    Grid.SetColumn(definiciones, 0);
                    Grid.SetRow(definiciones, 6);

                    checkCantidadesComoTextos.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesComoTextos.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesComoTextos.Content = "Cantidades como cadenas de texto";
                    checkCantidadesComoTextos.Tag = new object[] { itemInstancia, itemOperando };

                    checkCantidadesComoTextos.Checked -= CheckCantidadesComoTextos_Checked;
                    checkCantidadesComoTextos.Unchecked -= CheckCantidadesComoTextos_Unchecked;

                    if (checkOperandos.IsChecked == false)
                        checkCantidadesComoTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesComoTextos.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Contains(itemOperando);

                    checkCantidadesComoTextos.Checked += CheckCantidadesComoTextos_Checked;
                    checkCantidadesComoTextos.Unchecked += CheckCantidadesComoTextos_Unchecked;

                    opciones.Children.Add(checkCantidadesComoTextos);
                    Grid.SetColumn(checkCantidadesComoTextos, 0);
                    Grid.SetRow(checkCantidadesComoTextos, 7);

                    checkCantidadesDeCantidadesComoTextos.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesDeCantidadesComoTextos.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesDeCantidadesComoTextos.Content = "Cantidad de cantidades como cadena de texto";
                    checkCantidadesDeCantidadesComoTextos.Tag = new object[] { itemInstancia, itemOperando };

                    checkCantidadesDeCantidadesComoTextos.Checked -= CheckCantidadesDeCantidadesComoTextos_Checked;
                    checkCantidadesDeCantidadesComoTextos.Unchecked -= CheckCantidadesDeCantidadesComoTextos_Unchecked;

                    if (checkOperandos.IsChecked == false)
                        checkCantidadesDeCantidadesComoTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesDeCantidadesComoTextos.IsChecked = itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Contains(itemOperando);

                    checkCantidadesDeCantidadesComoTextos.Checked += CheckCantidadesDeCantidadesComoTextos_Checked;
                    checkCantidadesDeCantidadesComoTextos.Unchecked += CheckCantidadesDeCantidadesComoTextos_Unchecked;

                    opciones.Children.Add(checkCantidadesDeCantidadesComoTextos);
                    Grid.SetColumn(checkCantidadesDeCantidadesComoTextos, 0);
                    Grid.SetRow(checkCantidadesDeCantidadesComoTextos, 8);

                    opcionOperandos.Children.Add(opciones);
                    Grid.SetColumn(opciones, 1);
                    Grid.SetRow(opciones, 0);

                    Border borde = new Border();
                    borde.BorderBrush = agregarTextoInformacion.BorderBrush;
                    borde.BorderThickness = new Thickness(0, 0.5, 0, 0);
                    borde.Child = opcionOperandos;

                    menuBoton.Items.Add(borde);

                }
            }
            
            if (SubOperandos != null && ModoOperacion)
            {
                foreach (var itemOperando in SubOperandos.Where(i => subOperandosSeleccionados.Contains(i)).Distinct())
                {
                    Grid opcionOperandos = new Grid();
                    opcionOperandos.RowDefinitions.Add(new RowDefinition());
                    opcionOperandos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionOperandos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionOperandos.ColumnDefinitions.Last().Width = GridLength.Auto;

                    Grid opciones = new Grid();
                    opciones.ColumnDefinitions.Add(new ColumnDefinition());
                    opciones.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;
                    opciones.RowDefinitions.Add(new RowDefinition());
                    opciones.RowDefinitions.Last().Height = GridLength.Auto;

                    Grid opcionesTodosSusTextos = new Grid();
                    opcionesTodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesTodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesTodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesTodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesTodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesTodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionesTodosSusTextos.Margin = new Thickness(0, 10, 0, 0);

                    CheckBox checkOperandos = new CheckBox();
                    CheckBox checkCantidadesCondiciones = new CheckBox();
                    CheckBox checkTodosSusTextos = new CheckBox();
                    CheckBox checkSusTextosCondiciones = new CheckBox();
                    CheckBox checkSusTextosDefiniciones = new CheckBox();
                    CheckBox checkCantidadesComoTextos = new CheckBox();
                    CheckBox checkCantidadesDeCantidadesComoTextos = new CheckBox();

                    checkOperandos.VerticalAlignment = VerticalAlignment.Center;
                    checkOperandos.Content = "Desde la variable, vector o retornados " + itemOperando.NombreCombo;
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando, checkTodosSusTextos, checkSusTextosCondiciones, 
                        checkSusTextosDefiniciones, checkCantidadesCondiciones, checkCantidadesComoTextos , checkCantidadesDeCantidadesComoTextos };

                    checkOperandos.Checked -= CheckSubOperandos_Checked;
                    checkOperandos.Unchecked -= CheckSubOperandos_Unchecked;

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion.Contains(itemOperando);

                    checkOperandos.Checked += CheckSubOperandos_Checked;
                    checkOperandos.Unchecked += CheckSubOperandos_Unchecked;

                    opcionOperandos.Children.Add(checkOperandos);
                    Grid.SetColumn(checkOperandos, 0);
                    Grid.SetRow(checkOperandos, 0);

                    OpcionCondiciones_TextosInformacion condicionesCantidades = new OpcionCondiciones_TextosInformacion();
                    condicionesCantidades.MostrarOpcionesTextosImplicacionAsignacion = true;

                    checkCantidadesCondiciones.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesCondiciones.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesCondiciones.Content = "Cantidades que cumplen las condiciones (si/entonces): ";
                    checkCantidadesCondiciones.Tag = new object[] { itemInstancia, itemOperando, condicionesCantidades };

                    checkCantidadesCondiciones.Checked -= CheckCantidadesCondiciones_Checked2;
                    checkCantidadesCondiciones.Unchecked -= CheckCantidadesCondiciones_Unchecked2;

                    if (checkOperandos.IsChecked == false)
                        checkCantidadesCondiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesCondiciones.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Contains(itemOperando);

                    checkCantidadesCondiciones.Checked += CheckCantidadesCondiciones_Checked2;
                    checkCantidadesCondiciones.Unchecked += CheckCantidadesCondiciones_Unchecked2;

                    opciones.Children.Add(checkCantidadesCondiciones);
                    Grid.SetColumn(checkCantidadesCondiciones, 0);
                    Grid.SetRow(checkCantidadesCondiciones, 0);

                    condicionesCantidades.VerticalAlignment = VerticalAlignment.Center;
                    condicionesCantidades.Margin = new Thickness(5, 10, 0, 0);
                    condicionesCantidades.Operandos = new List<DiseñoOperacion>() { OperacionRelacionada_Definicion };
                    condicionesCantidades.SubOperandos = SubOperandos;
                    condicionesCantidades.ElementoSubOperandoAsociado_Asignacion = itemOperando;
                    condicionesCantidades.ModoAsignacionLogicaImplicaciones = true;

                    if (checkCantidadesCondiciones.IsChecked == false)
                        condicionesCantidades.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                    {
                        condicionesCantidades.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.SubOperando == itemOperando);
                        condicionesCantidades.Condiciones = condicionesCantidades.AsociacionTextosInformacionOperando_Implicacion?.Condiciones;
                    }

                    opciones.Children.Add(condicionesCantidades);
                    Grid.SetColumn(condicionesCantidades, 0);
                    Grid.SetRow(condicionesCantidades, 1);

                    Grid opcionesCheks_TodosSusTextos = new Grid();
                    opcionesCheks_TodosSusTextos.RowDefinitions.Add(new RowDefinition());
                    opcionesCheks_TodosSusTextos.RowDefinitions.Last().Height = GridLength.Auto;
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    opcionesCheks_TodosSusTextos.ColumnDefinitions.Last().Width = GridLength.Auto;

                    checkTodosSusTextos.VerticalAlignment = VerticalAlignment.Center;
                    checkTodosSusTextos.Margin = new Thickness(5, 10, 0, 0);
                    checkTodosSusTextos.Content = "Todas sus cadenas de texto";
                    checkTodosSusTextos.Tag = new object[] { itemInstancia, itemOperando, opcionesCheks_TodosSusTextos };

                    checkTodosSusTextos.Checked -= CheckTodosSusTextos_Checked2;
                    checkTodosSusTextos.Unchecked -= CheckTodosSusTextos_Unchecked2;

                    if (checkOperandos.IsChecked == false)
                        checkTodosSusTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkTodosSusTextos.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos.Contains(itemOperando);

                    checkTodosSusTextos.Checked += CheckTodosSusTextos_Checked2;
                    checkTodosSusTextos.Unchecked += CheckTodosSusTextos_Unchecked2;

                    opcionesTodosSusTextos.Children.Add(checkTodosSusTextos);
                    Grid.SetColumn(checkTodosSusTextos, 0);
                    Grid.SetRow(checkTodosSusTextos, 0);

                    if (checkTodosSusTextos.IsChecked == true)
                        opcionesCheks_TodosSusTextos.Visibility = Visibility.Visible;
                    else
                        opcionesCheks_TodosSusTextos.Visibility = Visibility.Collapsed;

                    RadioButton opcionTodosSusTextos_Elemento = new RadioButton();
                    opcionTodosSusTextos_Elemento.Content = "Las cadenas de texto de todos sus números dentro del vector";
                    opcionTodosSusTextos_Elemento.GroupName = "OpcionPosicionTodosNumeros-" + itemInstancia.GetHashCode().ToString() + "-" + itemOperando.GetHashCode().ToString();
                    opcionTodosSusTextos_Elemento.Tag = new object[] { itemInstancia, itemOperando };

                    opcionTodosSusTextos_Elemento.Checked -= OpcionTodosSusTextos_Elemento_Checked2;
                    opcionTodosSusTextos_Elemento.Unchecked -= OpcionTodosSusTextos_Elemento_Unchecked2;

                    if (itemInstancia != null)
                        opcionTodosSusTextos_Elemento.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Contains(itemOperando);

                    opcionesCheks_TodosSusTextos.Children.Add(opcionTodosSusTextos_Elemento);
                    Grid.SetColumn(opcionTodosSusTextos_Elemento, 0);
                    Grid.SetRow(opcionTodosSusTextos_Elemento, 0);

                    RadioButton opcionTextosPosicionActual_Elemento = new RadioButton();
                    opcionTextosPosicionActual_Elemento.Content = "Las cadenas de texto del número de la posición actual dentro del vector";
                    opcionTextosPosicionActual_Elemento.GroupName = "OpcionPosicionTodosNumeros-" + itemInstancia.GetHashCode().ToString() + "-" + itemOperando.GetHashCode().ToString();
                    opcionTextosPosicionActual_Elemento.Tag = new object[] { itemInstancia, itemOperando };

                    opcionTextosPosicionActual_Elemento.Checked -= OpcionTextosPosicionActual_Elemento_Checked2;
                    opcionTextosPosicionActual_Elemento.Unchecked -= OpcionTextosPosicionActual_Elemento_Unchecked2;

                    if (itemInstancia != null)
                        opcionTextosPosicionActual_Elemento.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Contains(itemOperando);

                    opcionTextosPosicionActual_Elemento.Checked += OpcionTextosPosicionActual_Elemento_Checked2;
                    opcionTextosPosicionActual_Elemento.Unchecked += OpcionTextosPosicionActual_Elemento_Unchecked2;

                    opcionTodosSusTextos_Elemento.Checked += OpcionTodosSusTextos_Elemento_Checked2;
                    opcionTodosSusTextos_Elemento.Unchecked += OpcionTodosSusTextos_Elemento_Unchecked2;

                    opcionesCheks_TodosSusTextos.Children.Add(opcionTextosPosicionActual_Elemento);
                    Grid.SetColumn(opcionTextosPosicionActual_Elemento, 1);
                    Grid.SetRow(opcionTextosPosicionActual_Elemento, 0);

                    opcionesTodosSusTextos.Children.Add(opcionesCheks_TodosSusTextos);
                    Grid.SetColumn(opcionesCheks_TodosSusTextos, 0);
                    Grid.SetRow(opcionesCheks_TodosSusTextos, 1);

                    opciones.Children.Add(opcionesTodosSusTextos);
                    Grid.SetColumn(opcionesTodosSusTextos, 0);
                    Grid.SetRow(opcionesTodosSusTextos, 2);

                    OpcionCondiciones_TextosInformacion condiciones = new OpcionCondiciones_TextosInformacion();
                    condiciones.MostrarOpcionesTextosImplicacionAsignacion = true;

                    checkSusTextosCondiciones.VerticalAlignment = VerticalAlignment.Center;
                    checkSusTextosCondiciones.Margin = new Thickness(5, 10, 0, 0);
                    checkSusTextosCondiciones.Content = "Sus cadenas de texto que cumple las condiciones (si/entonces): ";
                    checkSusTextosCondiciones.Tag = new object[] { itemInstancia, itemOperando, condiciones };

                    checkSusTextosCondiciones.Checked -= CheckSusTextosCondiciones_Checked2;
                    checkSusTextosCondiciones.Unchecked -= CheckSusTextosCondiciones_Unchecked2;

                    if (checkOperandos.IsChecked == false)
                        checkSusTextosCondiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkSusTextosCondiciones.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Contains(itemOperando);

                    checkSusTextosCondiciones.Checked += CheckSusTextosCondiciones_Checked2;
                    checkSusTextosCondiciones.Unchecked += CheckSusTextosCondiciones_Unchecked2;

                    opciones.Children.Add(checkSusTextosCondiciones);
                    Grid.SetColumn(checkSusTextosCondiciones, 0);
                    Grid.SetRow(checkSusTextosCondiciones, 3);

                    condiciones.VerticalAlignment = VerticalAlignment.Center;
                    condiciones.Margin = new Thickness(5, 10, 0, 0);
                    condiciones.Operandos = new List<DiseñoOperacion>() { OperacionRelacionada_Definicion };
                    condiciones.SubOperandos = SubOperandos;
                    condiciones.ElementoSubOperandoAsociado_Asignacion = itemOperando;
                    condiciones.ModoAsignacionLogicaImplicaciones = true;

                    if (checkSusTextosCondiciones.IsChecked == false)
                        condiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                    {
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.SubOperando == itemOperando);
                        condiciones.Condiciones = condiciones.AsociacionTextosInformacionOperando_Implicacion?.Condiciones;
                    }

                    opciones.Children.Add(condiciones);
                    Grid.SetColumn(condiciones, 0);
                    Grid.SetRow(condiciones, 4);

                    OpcionDefiniciones_TextosInformacion definiciones = new OpcionDefiniciones_TextosInformacion();

                    checkSusTextosDefiniciones.VerticalAlignment = VerticalAlignment.Center;
                    checkSusTextosDefiniciones.Margin = new Thickness(5, 10, 0, 0);
                    checkSusTextosDefiniciones.Content = "Definiciones de lógicas de asignación de cadenas de texto: ";
                    checkSusTextosDefiniciones.Tag = new object[] { itemInstancia, itemOperando, definiciones };

                    checkSusTextosDefiniciones.Checked -= CheckSusTextosDefiniciones_Checked2;
                    checkSusTextosDefiniciones.Unchecked -= CheckSusTextosDefiniciones_Unchecked2;

                    if (checkOperandos.IsChecked == false)
                        checkSusTextosDefiniciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkSusTextosDefiniciones.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Contains(itemOperando);

                    checkSusTextosDefiniciones.Checked += CheckSusTextosDefiniciones_Checked2;
                    checkSusTextosDefiniciones.Unchecked += CheckSusTextosDefiniciones_Unchecked2;

                    opciones.Children.Add(checkSusTextosDefiniciones);
                    Grid.SetColumn(checkSusTextosDefiniciones, 0);
                    Grid.SetRow(checkSusTextosDefiniciones, 5);

                    definiciones.VerticalAlignment = VerticalAlignment.Center;
                    definiciones.Margin = new Thickness(5, 10, 0, 0);
                    definiciones.Operandos = new List<DiseñoOperacion>() { OperacionRelacionada_Definicion };
                    definiciones.SubOperandos = SubOperandos;
                    definiciones.CalculoAsociado = CalculoAsociado;

                    if (checkSusTextosDefiniciones.IsChecked == false)
                        definiciones.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        definiciones.AsociacionTextosInformacionOperando_Implicacion = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.SubOperando == itemOperando);

                    opciones.Children.Add(definiciones);
                    Grid.SetColumn(definiciones, 0);
                    Grid.SetRow(definiciones, 6);

                    checkCantidadesComoTextos.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesComoTextos.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesComoTextos.Content = "Cantidades como cadenas de texto";
                    checkCantidadesComoTextos.Tag = new object[] { itemInstancia, itemOperando };

                    checkCantidadesComoTextos.Checked -= CheckCantidadesComoTextos_Checked1;
                    checkCantidadesComoTextos.Unchecked -= CheckCantidadesComoTextos_Unchecked1;

                    if (checkOperandos.IsChecked == false)
                        checkCantidadesComoTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesComoTextos.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Contains(itemOperando);

                    checkCantidadesComoTextos.Checked += CheckCantidadesComoTextos_Checked1;
                    checkCantidadesComoTextos.Unchecked += CheckCantidadesComoTextos_Unchecked1;

                    opciones.Children.Add(checkCantidadesComoTextos);
                    Grid.SetColumn(checkCantidadesComoTextos, 0);
                    Grid.SetRow(checkCantidadesComoTextos, 7);

                    checkCantidadesDeCantidadesComoTextos.VerticalAlignment = VerticalAlignment.Center;
                    checkCantidadesDeCantidadesComoTextos.Margin = new Thickness(5, 10, 0, 0);
                    checkCantidadesDeCantidadesComoTextos.Content = "Cantidad de cantidades como cadena de texto";
                    checkCantidadesDeCantidadesComoTextos.Tag = new object[] { itemInstancia, itemOperando };

                    checkCantidadesDeCantidadesComoTextos.Checked -= CheckCantidadesDeCantidadesComoTextos_Checked1;
                    checkCantidadesDeCantidadesComoTextos.Unchecked -= CheckCantidadesDeCantidadesComoTextos_Unchecked1;

                    if (checkOperandos.IsChecked == false)
                        checkCantidadesDeCantidadesComoTextos.Visibility = Visibility.Collapsed;

                    if (itemInstancia != null)
                        checkCantidadesDeCantidadesComoTextos.IsChecked = itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Contains(itemOperando);

                    checkCantidadesDeCantidadesComoTextos.Checked += CheckCantidadesDeCantidadesComoTextos_Checked1;
                    checkCantidadesDeCantidadesComoTextos.Unchecked += CheckCantidadesDeCantidadesComoTextos_Unchecked1;

                    opciones.Children.Add(checkCantidadesDeCantidadesComoTextos);
                    Grid.SetColumn(checkCantidadesDeCantidadesComoTextos, 0);
                    Grid.SetRow(checkCantidadesDeCantidadesComoTextos, 8);

                    opcionOperandos.Children.Add(opciones);
                    Grid.SetColumn(opciones, 1);
                    Grid.SetRow(opciones, 0);

                    Border borde = new Border();
                    borde.BorderBrush = agregarTextoInformacion.BorderBrush;
                    borde.BorderThickness = new Thickness(0, 0.5, 0, 0);
                    borde.Child = opcionOperandos;

                    menuBoton.Items.Add(borde);
                }
            }
        }

        private void OpcionClasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (((CheckBox)sender).Tag != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    asignacion.AsignarCadenasTexto_Clasificadores = (bool)((CheckBox)sender).IsChecked;
                }
            }
        }

        private void OpcionClasificadores_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (((CheckBox)sender).Tag != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    asignacion.AsignarCadenasTexto_Clasificadores = (bool)((CheckBox)sender).IsChecked;
                }
            }
        }

        private void CheckCantidadesDeCantidadesComoTextos_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Remove(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesDeCantidadesComoTextos_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Add(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesComoTextos_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Remove(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesComoTextos_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Add(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesDeCantidadesComoTextos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Remove(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesDeCantidadesComoTextos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Add(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesComoTextos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        
                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Remove(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesComoTextos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                                                
                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Add(operando);
                            }
                        }
                    }
                }
            }
        }

        private void CheckCantidadesCondiciones_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.SubOperando == operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Remove(operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Remove(asociacion);
                            }

                            condiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckCantidadesCondiciones_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        condiciones.Operandos = new List<DiseñoOperacion>() { OperacionRelacionada_Definicion };
                        condiciones.SubOperandos = SubOperandos;
                        condiciones.ElementoSubOperandoAsociado_Asignacion = operando;

                        AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
                        asociacion.SubOperando = operando;
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Add(operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Add(asociacion);
                            }

                            condiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckCantidadesCondiciones_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Operando == operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Remove(operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Remove(asociacion);
                            }

                            condiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckCantidadesCondiciones_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        condiciones.Operandos = Operandos;
                        condiciones.ElementoOperandoAsociado_Asignacion = operando;

                        AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
                        asociacion.Operando = operando;
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Add(operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Add(asociacion);
                            }

                            condiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckCantidadesCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = asignacion.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Entrada == entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones.Remove(entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Remove(asociacion);
                            }

                            condiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckCantidadesCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        condiciones.Operandos = Operandos;
                        condiciones.ElementoEntradaAsociado_Asignacion = entrada.EntradaRelacionada;

                        AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
                        asociacion.Entrada = entrada;
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones.Add(entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Add(asociacion);
                            }

                            condiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void OpcionDigitarTextos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (((CheckBox)sender).Tag != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    asignacion.DigitarTextosInformacion_EnEjecucion = (bool)((CheckBox)sender).IsChecked;
                }
            }
        }

        private void OpcionDigitarTextos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (((CheckBox)sender).Tag != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    asignacion.DigitarTextosInformacion_EnEjecucion = (bool)((CheckBox)sender).IsChecked;
                }
            }
        }

        private void OpcionTextosPosicionActual_Elemento_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Remove(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTextosPosicionActual_Elemento_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Add(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTodosSusTextos_Elemento_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Remove(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTodosSusTextos_Elemento_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Add(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTextosPosicionActual_Elemento_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Remove(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTextosPosicionActual_Elemento_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Add(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTodosSusTextos_Elemento_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Remove(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTodosSusTextos_Elemento_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Add(operando);
                        }
                    }
                }
            }
        }

        private void OpcionTextosPosicionActual_Elemento_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Remove(entrada);
                        }
                    }
                }
            }
        }

        private void OpcionTextosPosicionActual_Elemento_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Add(entrada);
                        }
                    }
                }
            }
        }

        private void OpcionTodosSusTextos_Elemento_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Remove(entrada);
                        }
                    }
                }
            }
        }

        private void OpcionTodosSusTextos_Elemento_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    RadioButton check = (RadioButton)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Add(entrada);
                        }
                    }
                }
            }
        }

        private void CheckSusTextosDefiniciones_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        OpcionDefiniciones_TextosInformacion definiciones = (OpcionDefiniciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                //AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = asignacion.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.SubOperando == operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Remove(operando);
                                //asignacion.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Remove(asociacion);
                            }

                            definiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosDefiniciones_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        OpcionDefiniciones_TextosInformacion definiciones = (OpcionDefiniciones_TextosInformacion)objetos[2];

                        definiciones.Operandos = new List<DiseñoOperacion>() { OperacionRelacionada_Definicion };
                        definiciones.SubOperandos = SubOperandos;

                        AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion();
                        asociacion.SubOperando = operando;
                        definiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Add(operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Add(asociacion);
                            }

                            definiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosDefiniciones_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        OpcionDefiniciones_TextosInformacion definiciones = (OpcionDefiniciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = asignacion.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.Operando == operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Remove(operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Remove(asociacion);
                            }

                            definiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosDefiniciones_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        OpcionDefiniciones_TextosInformacion definiciones = (OpcionDefiniciones_TextosInformacion)objetos[2];

                        definiciones.Operandos = Operandos;

                        AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion();
                        asociacion.Operando = operando;
                        definiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Add(operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Add(asociacion);
                            }

                            definiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosDefiniciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        OpcionDefiniciones_TextosInformacion definiciones = (OpcionDefiniciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = asignacion.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.FirstOrDefault(item => item.Entrada == entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos.Remove(entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.Remove(asociacion);
                            }

                            definiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosDefiniciones_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        OpcionDefiniciones_TextosInformacion definiciones = (OpcionDefiniciones_TextosInformacion)objetos[2];

                        //definiciones.Operandos = Elementos.Where(item => (item.EntradaRelacionada != null && ((item.EntradaRelacionada.Tipo != TipoEntrada.TextosInformacion
                        //        && item.EntradaRelacionada == entrada.EntradaRelacionada) ||
                        //        item.EntradaRelacionada.Tipo == TipoEntrada.TextosInformacion) ||
                        //        item.EntradaRelacionada == null)).ToList();
                        definiciones.Operandos = Operandos;

                        AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion();
                        asociacion.Entrada = entrada;
                        definiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos.Add(entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.Add(asociacion);
                            }

                            definiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosCondiciones_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = asignacion.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.SubOperando == operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Remove(operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Remove(asociacion);
                            }

                            condiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosCondiciones_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        condiciones.Operandos = new List<DiseñoOperacion>() { OperacionRelacionada_Definicion };
                        condiciones.SubOperandos = SubOperandos;
                        condiciones.ElementoSubOperandoAsociado_Asignacion = operando;

                        AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
                        asociacion.SubOperando = operando;
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Add(operando);
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Add(asociacion);
                            }

                            condiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosCondiciones_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = asignacion.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Operando == operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Remove(operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Remove(asociacion);
                            }

                            condiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosCondiciones_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        condiciones.Operandos = Operandos;
                        condiciones.ElementoOperandoAsociado_Asignacion = operando;

                        AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
                        asociacion.Operando = operando;
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Add(operando);
                                asignacion.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Add(asociacion);
                            }

                            condiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                            {
                                AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = asignacion.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Entrada == entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones.Remove(entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Remove(asociacion);
                            }

                            condiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSusTextosCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        OpcionCondiciones_TextosInformacion condiciones = (OpcionCondiciones_TextosInformacion)objetos[2];

                        //condiciones.Operandos = Elementos.Where(item => (item.EntradaRelacionada != null && ((item.EntradaRelacionada.Tipo != TipoEntrada.TextosInformacion
                        //        && item.EntradaRelacionada == entrada.EntradaRelacionada) ||
                        //        item.EntradaRelacionada.Tipo == TipoEntrada.TextosInformacion) ||
                        //        item.EntradaRelacionada == null)).ToList();
                        condiciones.Operandos = Operandos;
                        condiciones.ElementoEntradaAsociado_Asignacion = entrada.EntradaRelacionada;

                        AsociacionOperandosCondiciones_TextosAsignacion_Implicacion asociacion = new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion();
                        asociacion.Entrada = entrada;
                        condiciones.AsociacionTextosInformacionOperando_Implicacion = asociacion;

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                            {
                                asignacion.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones.Add(entrada);
                                asignacion.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Add(asociacion);
                            }

                            condiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckTodosSusTextos_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        Grid grillaOpciones = (Grid)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos.Remove(operando);

                            grillaOpciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckTodosSusTextos_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        Grid grillaOpciones = (Grid)objetos[2];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos.Add(operando);

                            grillaOpciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckTodosSusTextos_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        Grid grillaOpciones = (Grid)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos.Remove(operando);

                            grillaOpciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckTodosSusTextos_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        Grid grillaOpciones = (Grid)objetos[2];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos.Add(operando);

                            grillaOpciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckTodosSusTextos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        Grid grillaOpciones = (Grid)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos.Remove(entrada);

                            grillaOpciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckTodosSusTextos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        Grid grillaOpciones = (Grid)objetos[2];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos.Add(entrada);

                            grillaOpciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void ListarOpcionesBotonTextoAsignacionOperandos(Button boton, InstanciaAsignacionImplicacion_TextosInformacion itemInstancia)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Asignar a ";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            List<DiseñoOperacion> operandosSeleccionados = null;
            List<DiseñoElementoOperacion> subOperandosSeleccionados = null;

            if (itemInstancia != null)
            {
                operandosSeleccionados = itemInstancia.Operandos_AsignarTextosInformacionCuando;
                subOperandosSeleccionados = itemInstancia.SubOperandos_AsignarTextosInformacionCuando;
            }

            if (!operandosSeleccionados.Any() &&
                itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones)
            {
                operandosSeleccionados = Operandos.ToList();
            }

            if (!subOperandosSeleccionados.Any() &&
                itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones)
            {
                subOperandosSeleccionados = SubOperandos.ToList();
            }

            if (Operandos != null &&
                !ModoOperacion)
            {
                foreach (var itemOperando in Operandos.Where(i => operandosSeleccionados.Contains(i)))
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    StackPanel contenedorOpciones = new StackPanel();
                    contenedorOpciones.Orientation = Orientation.Horizontal;

                    CheckBox checkOperandos = new CheckBox();
                    
                    checkOperandos.Content = "Operando " + itemOperando.NombreCombo;
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando};

                    checkOperandos.Checked -= CheckOperandos_Checked1;
                    checkOperandos.Unchecked -= CheckOperandos_Unchecked1;

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.Operandos_AsignarTextosInformacionA.Any(i => i.Operando == itemOperando);

                    checkOperandos.Checked += CheckOperandos_Checked1;
                    checkOperandos.Unchecked += CheckOperandos_Unchecked1;

                    contenedorOpciones.Children.Add(checkOperandos);

                    opcionOperandos.Content = contenedorOpciones;
                    menuBoton.Items.Add(opcionOperandos);
                }
            }

            if (SubOperandos != null && ModoOperacion)
            {
                foreach (var itemOperando in SubOperandos.Where(i => subOperandosSeleccionados.Contains(i)))
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    StackPanel contenedorOpciones = new StackPanel();
                    contenedorOpciones.Orientation = Orientation.Horizontal;

                    CheckBox checkOperandos = new CheckBox();

                    checkOperandos.Content = "Variable, vector o retornados " + itemOperando.NombreCombo;
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando };

                    checkOperandos.Checked -= CheckSubOperandos_Checked1;
                    checkOperandos.Unchecked -= CheckSubOperandos_Unchecked1;

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.SubOperandos_AsignarTextosInformacionA.Any(i => i.SubOperando == itemOperando);

                    checkOperandos.Checked += CheckSubOperandos_Checked1;
                    checkOperandos.Unchecked += CheckSubOperandos_Unchecked1;

                    contenedorOpciones.Children.Add(checkOperandos);

                    opcionOperandos.Content = contenedorOpciones;
                    menuBoton.Items.Add(opcionOperandos);
                }
            }
        }

        private void ListarOpcionesBotonTextoAsignacionOperandosCuando(Button boton, 
            InstanciaAsignacionImplicacion_TextosInformacion itemInstancia,
            AsignacionImplicacion_TextosInformacion implicacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Se utilicen las variables, vectores o retornados ";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            CheckBox checkSoloOperandosCondiciones = new CheckBox();
            checkSoloOperandosCondiciones.Content = "Considerar solo las variables, vectores o retornados que cumplan las condiciones (si/entonces) ";
            checkSoloOperandosCondiciones.Tag = new object[] { itemInstancia, implicacion, menuBoton };

            checkSoloOperandosCondiciones.Checked -= CheckSoloOperandosCondiciones_Checked;
            checkSoloOperandosCondiciones.Unchecked -= CheckSoloOperandosCondiciones_Unchecked;

            if (itemInstancia != null)
                checkSoloOperandosCondiciones.IsChecked = itemInstancia.ConsiderarSoloOperandos_CumplanCondiciones;

            checkSoloOperandosCondiciones.Checked += CheckSoloOperandosCondiciones_Checked;
            checkSoloOperandosCondiciones.Unchecked += CheckSoloOperandosCondiciones_Unchecked;

            menuBoton.Items.Add(checkSoloOperandosCondiciones);
            
            if (Operandos != null && !ModoOperacion)
            {
                foreach (var itemOperando in Operandos)
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    CheckBox checkOperandos = new CheckBox();
                    checkOperandos.Content = itemOperando.NombreCombo + " en las variable o vectores retornados";
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando };

                    checkOperandos.Checked -= CheckOperandos_Checked2;
                    checkOperandos.Unchecked -= CheckOperandos_Unchecked2;

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.Operandos_AsignarTextosInformacionCuando.Contains(itemOperando);

                    checkOperandos.Checked += CheckOperandos_Checked2;
                    checkOperandos.Unchecked += CheckOperandos_Unchecked2;
                    opcionOperandos.Content = checkOperandos;
                    menuBoton.Items.Add(opcionOperandos);
                }
            }

            if (SubOperandos != null && ModoOperacion)
            {
                foreach (var itemOperando in SubOperandos)
                {
                    ComboBoxItem opcionOperandos = new ComboBoxItem();
                    CheckBox checkOperandos = new CheckBox();
                    checkOperandos.Content = itemOperando.NombreCombo + " en las variable o vectores retornados";
                    checkOperandos.Tag = new object[] { itemInstancia, itemOperando };

                    checkOperandos.Checked -= CheckSubOperandos_Checked2;
                    checkOperandos.Unchecked -= CheckSubOperandos_Unchecked2;

                    if (itemInstancia != null)
                        checkOperandos.IsChecked = itemInstancia.SubOperandos_AsignarTextosInformacionCuando.Contains(itemOperando);

                    checkOperandos.Checked += CheckSubOperandos_Checked2;
                    checkOperandos.Unchecked += CheckSubOperandos_Unchecked2;
                    opcionOperandos.Content = checkOperandos;
                    menuBoton.Items.Add(opcionOperandos);
                }
            }
        }

        private void CheckSoloOperandosCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
            {
                CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = null;

                        if (objetos[0] != null)
                            asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];

                        ContextMenu menu = (ContextMenu)objetos[2];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.ConsiderarSoloOperandos_CumplanCondiciones = (bool)check.IsChecked;

                            menu.IsOpen = false;

                            if (asignacion != null)
                                ListarTextos();
                            else
                                UserControl_Loaded(this, null);
                        }
                    }
                }
            }
        }

        private void CheckSoloOperandosCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = null;
                        AsignacionImplicacion_TextosInformacion implicacion = null;

                        if (objetos[0] != null)
                            asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];

                        if (objetos[1] != null)
                            implicacion = (AsignacionImplicacion_TextosInformacion)objetos[1];

                        ContextMenu menu = (ContextMenu)objetos[2];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.ConsiderarSoloOperandos_CumplanCondiciones = (bool)check.IsChecked;

                            menu.IsOpen = false;

                            if (asignacion != null &
                                implicacion != null)
                            {
                                var operandosSeleccionadosAquitar = asignacion.Operandos_AsignarTextosInformacionCuando.Where(i => !((implicacion.Condiciones_TextoCondicion != null &&
                                implicacion.Condiciones_TextoCondicion.VerificarOperando(i)) || implicacion.Condiciones_TextoCondicion == null)).ToList();

                                if (operandosSeleccionadosAquitar.Any())
                                {
                                    while (operandosSeleccionadosAquitar.Any())
                                    {
                                        asignacion.Operandos_AsignarTextosInformacionCuando.Remove(operandosSeleccionadosAquitar.FirstOrDefault());
                                        operandosSeleccionadosAquitar.Remove(operandosSeleccionadosAquitar.FirstOrDefault());
                                    }
                                }

                                var subOperandosSeleccionadosAquitar = asignacion.SubOperandos_AsignarTextosInformacionCuando.Where(i => !((implicacion.Condiciones_TextoCondicion != null &&
                                implicacion.Condiciones_TextoCondicion.VerificarSubOperando(i)) || implicacion.Condiciones_TextoCondicion == null)).ToList();

                                if (subOperandosSeleccionadosAquitar.Any())
                                {
                                    while (subOperandosSeleccionadosAquitar.Any())
                                    {
                                        asignacion.SubOperandos_AsignarTextosInformacionCuando.Remove(subOperandosSeleccionadosAquitar.FirstOrDefault());
                                        subOperandosSeleccionadosAquitar.Remove(subOperandosSeleccionadosAquitar.FirstOrDefault());
                                    }
                                }

                                ListarTextos();
                            }
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_AsignarTextosInformacionCuando.Remove(operando);

                            ListarTextos();
                        }
                    }
                }
            }
        }

        private void CheckSubOperandos_Unchecked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_AsignarTextosInformacionCuando.Remove(operando);

                            ListarTextos();
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_AsignarTextosInformacionCuando.Add(operando);

                            ListarTextos();
                        }
                    }
                }
            }
        }

        private void CheckSubOperandos_Checked2(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_AsignarTextosInformacionCuando.Add(operando);

                            ListarTextos();
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        AsignacionTextosOperando_Implicacion asig = null;

                        if (asignacion != null)
                            asig = asignacion.Operandos_AsignarTextosInformacionA.FirstOrDefault(i => i.Operando == operando);

                        if (asig != null && check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_AsignarTextosInformacionA.Remove(asig);
                        }
                    }
                }
            }
        }

        private void CheckSubOperandos_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        AsignacionTextosOperando_Implicacion asig = null;

                        if (asignacion != null)
                            asig = asignacion.SubOperandos_AsignarTextosInformacionA.FirstOrDefault(i => i.SubOperando == operando);

                        if (asig != null && check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_AsignarTextosInformacionA.Remove(asig);
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        AsignacionTextosOperando_Implicacion asig = new AsignacionTextosOperando_Implicacion();
                        asig.Operando = operando;

                        if (asig != null && check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_AsignarTextosInformacionA.Add(asig);
                        }
                    }
                }
            }
        }

        private void CheckSubOperandos_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        AsignacionTextosOperando_Implicacion asig = new AsignacionTextosOperando_Implicacion();
                        asig.SubOperando = operando;

                        if (asig != null && check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_AsignarTextosInformacionA.Add(asig);
                        }
                    }
                }
            }
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void CheckEntradas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        CheckBox opcionTodosSusTextos = (CheckBox)objetos[2];
                        CheckBox opcionSusTextosCondiciones = (CheckBox)objetos[3];
                        CheckBox opcionSusTextosDefiniciones = (CheckBox)objetos[4];
                        CheckBox checkCantidadesCondiciones = (CheckBox)objetos[5];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion.Remove(entrada);

                            opcionTodosSusTextos.Visibility = Visibility.Collapsed;
                            opcionSusTextosCondiciones.Visibility = Visibility.Collapsed;
                            opcionSusTextosDefiniciones.Visibility = Visibility.Collapsed;
                            checkCantidadesCondiciones.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckEntradas_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;
                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoTextosInformacion entrada = (DiseñoTextosInformacion)objetos[1];
                        CheckBox opcionTodosSusTextos = (CheckBox)objetos[2];
                        CheckBox opcionSusTextosCondiciones = (CheckBox)objetos[3];
                        CheckBox opcionSusTextosDefiniciones = (CheckBox)objetos[4];
                        CheckBox checkCantidadesCondiciones = (CheckBox)objetos[5];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Entradas_DesdeAsignarTextosInformacion.Add(entrada);

                            opcionTodosSusTextos.Visibility = Visibility.Visible;
                            opcionSusTextosCondiciones.Visibility = Visibility.Visible;
                            opcionSusTextosDefiniciones.Visibility = Visibility.Visible;
                            checkCantidadesCondiciones.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }
        private void CheckOperandos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        CheckBox opcionTodosSusTextos = (CheckBox)objetos[2];
                        CheckBox opcionSusTextosCondiciones = (CheckBox)objetos[3];
                        CheckBox opcionSusTextosDefiniciones = (CheckBox)objetos[4];
                        CheckBox checkCantidadesCondiciones = (CheckBox)objetos[5];
                        CheckBox checkCantidadesComoTextos = (CheckBox)objetos[6];
                        CheckBox checkCantidadesDeCantidadesComoTextos = (CheckBox)objetos[7];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion.Remove(operando);

                            opcionTodosSusTextos.Visibility = Visibility.Collapsed;
                            opcionSusTextosCondiciones.Visibility = Visibility.Collapsed;
                            opcionSusTextosDefiniciones.Visibility = Visibility.Collapsed;
                            checkCantidadesCondiciones.Visibility = Visibility.Collapsed;
                            checkCantidadesComoTextos.Visibility = Visibility.Collapsed;
                            checkCantidadesDeCantidadesComoTextos.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckSubOperandos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        CheckBox opcionTodosSusTextos = (CheckBox)objetos[2];
                        CheckBox opcionSusTextosCondiciones = (CheckBox)objetos[3];
                        CheckBox opcionSusTextosDefiniciones = (CheckBox)objetos[4];
                        CheckBox checkCantidadesCondiciones = (CheckBox)objetos[5];
                        CheckBox checkCantidadesComoTextos = (CheckBox)objetos[6];
                        CheckBox checkCantidadesDeCantidadesComoTextos = (CheckBox)objetos[7];

                        if (check.IsChecked == false)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion.Remove(operando);

                            opcionTodosSusTextos.Visibility = Visibility.Collapsed;
                            opcionSusTextosCondiciones.Visibility = Visibility.Collapsed;
                            opcionSusTextosDefiniciones.Visibility = Visibility.Collapsed;
                            checkCantidadesCondiciones.Visibility = Visibility.Collapsed;
                            checkCantidadesComoTextos.Visibility = Visibility.Collapsed;
                            checkCantidadesDeCantidadesComoTextos.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[1];
                        CheckBox opcionTodosSusTextos = (CheckBox)objetos[2];
                        CheckBox opcionSusTextosCondiciones = (CheckBox)objetos[3];
                        CheckBox opcionSusTextosDefiniciones = (CheckBox)objetos[4];
                        CheckBox checkCantidadesCondiciones = (CheckBox)objetos[5];
                        CheckBox checkCantidadesComoTextos = (CheckBox)objetos[6];
                        CheckBox checkCantidadesDeCantidadesComoTextos = (CheckBox)objetos[7];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.Operandos_DesdeAsignarTextosInformacion.Add(operando);

                            opcionTodosSusTextos.Visibility = Visibility.Visible;
                            opcionSusTextosCondiciones.Visibility = Visibility.Visible;
                            opcionSusTextosDefiniciones.Visibility = Visibility.Visible;
                            checkCantidadesCondiciones.Visibility = Visibility.Visible;
                            checkCantidadesComoTextos.Visibility = Visibility.Visible;
                            checkCantidadesDeCantidadesComoTextos.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void CheckSubOperandos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded)
                {
                    CheckBox check = (CheckBox)sender;

                    if (check.Tag != null &&
                        check.Parent != null)
                    {
                        object[] objetos = (object[])check.Tag;

                        InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];
                        CheckBox opcionTodosSusTextos = (CheckBox)objetos[2];
                        CheckBox opcionSusTextosCondiciones = (CheckBox)objetos[3];
                        CheckBox opcionSusTextosDefiniciones = (CheckBox)objetos[4];
                        CheckBox checkCantidadesCondiciones = (CheckBox)objetos[5];
                        CheckBox checkCantidadesComoTextos = (CheckBox)objetos[6];
                        CheckBox checkCantidadesDeCantidadesComoTextos = (CheckBox)objetos[7];

                        if (check.IsChecked == true)
                        {
                            if (asignacion != null)
                                asignacion.SubOperandos_DesdeAsignarTextosInformacion.Add(operando);

                            opcionTodosSusTextos.Visibility = Visibility.Visible;
                            opcionSusTextosCondiciones.Visibility = Visibility.Visible;
                            opcionSusTextosDefiniciones.Visibility = Visibility.Visible;
                            checkCantidadesCondiciones.Visibility = Visibility.Visible;
                            checkCantidadesComoTextos.Visibility = Visibility.Visible;
                            checkCantidadesDeCantidadesComoTextos.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void ListarOpcionesComboConservarTextosInformacion(Button boton, InstanciaAsignacionImplicacion_TextosInformacion asignacion)
        {
            ContextMenu menuBoton = new ContextMenu();
            boton.Content = "Al asignar cadenas de texto, procesar lógica con estas opciones ";
            boton.ContextMenu = menuBoton;
            boton.Click += Boton_Click;

            CheckBox opcionQuitarRepetidasCantidad = new CheckBox();
            opcionQuitarRepetidasCantidad.Tag = asignacion;

            opcionQuitarRepetidasCantidad.Checked -= OpcionQuitarRepetidasCantidad_Checked;
            opcionQuitarRepetidasCantidad.Unchecked -= OpcionQuitarRepetidasCantidad_Unchecked;

            opcionQuitarRepetidasCantidad.IsChecked = (bool)asignacion.QuitarTextosInformacion_RepetidosCantidad;
            opcionQuitarRepetidasCantidad.Checked += OpcionQuitarRepetidasCantidad_Checked;
            opcionQuitarRepetidasCantidad.Unchecked += OpcionQuitarRepetidasCantidad_Unchecked;

            opcionQuitarRepetidasCantidad.Content = "Quitar las cadenas de texto repetidas asignadas a cada cantidad";
            menuBoton.Items.Add(opcionQuitarRepetidasCantidad);

            CheckBox opcionConservarSoloOperandos = new CheckBox();
            opcionConservarSoloOperandos.Tag = asignacion;

            opcionConservarSoloOperandos.Checked -= OpcionConservarSoloOperandos_Checked;
            opcionConservarSoloOperandos.Unchecked -= OpcionConservarSoloOperandos_Unchecked;

            opcionConservarSoloOperandos.IsChecked = (bool)asignacion.AsignarTextosInformacion_CondicionOperandos;
            opcionConservarSoloOperandos.Checked += OpcionConservarSoloOperandos_Checked;
            opcionConservarSoloOperandos.Unchecked += OpcionConservarSoloOperandos_Unchecked;

            opcionConservarSoloOperandos.Content = "Asignar las cadenas de texto acumuladas asignadas en las variables, vectores o retornados seleccionados";
            menuBoton.Items.Add(opcionConservarSoloOperandos);

            CheckBox opcionQuitarRepetidasVariableVector = new CheckBox();
            opcionQuitarRepetidasVariableVector.Tag = asignacion;

            opcionQuitarRepetidasVariableVector.Checked -= OpcionQuitarRepetidasVariableVector_Checked;
            opcionQuitarRepetidasVariableVector.Unchecked -= OpcionQuitarRepetidasVariableVector_Unchecked;

            opcionQuitarRepetidasVariableVector.IsChecked = (bool)asignacion.QuitarTextosInformacion_RepetidosVariableVector;
            opcionQuitarRepetidasVariableVector.Checked += OpcionQuitarRepetidasVariableVector_Checked;
            opcionQuitarRepetidasVariableVector.Unchecked += OpcionQuitarRepetidasVariableVector_Unchecked;

            opcionQuitarRepetidasVariableVector.Content = "Quitar las cadenas de texto repetidas asignadas en las variables, vectores o retornados seleccionados";
            menuBoton.Items.Add(opcionQuitarRepetidasVariableVector);

            if (Operandos != null &&
                !ModoOperacion)
            {
                foreach (var itemOperando in Operandos)
                {
                    CheckBox opcionConservarTextosOperando = new CheckBox();
                    opcionConservarTextosOperando.Tag = new object[] { asignacion, itemOperando };

                    opcionConservarTextosOperando.Checked -= OpcionConservarTextosOperando_Checked;
                    opcionConservarTextosOperando.Unchecked -= OpcionConservarTextosOperando_Unchecked;

                    opcionConservarTextosOperando.IsChecked = asignacion.AsignarTextosInformacion_TextosOperandos.Contains(itemOperando);
                    opcionConservarTextosOperando.Checked += OpcionConservarTextosOperando_Checked;
                    opcionConservarTextosOperando.Unchecked += OpcionConservarTextosOperando_Unchecked;

                    opcionConservarTextosOperando.Content = "Asignar las cadenas de texto acumuladas asignadas en en la variable, vector o retornados " + itemOperando.NombreCombo;
                    menuBoton.Items.Add(opcionConservarTextosOperando);

                    CheckBox opcionQuitarRepetidasVariableVectorOperando = new CheckBox();
                    opcionQuitarRepetidasVariableVectorOperando.Tag = new object[] { asignacion, itemOperando };

                    opcionQuitarRepetidasVariableVectorOperando.Checked -= OpcionQuitarRepetidasVariableVectorOperando_Checked;
                    opcionQuitarRepetidasVariableVectorOperando.Unchecked -= OpcionQuitarRepetidasVariableVectorOperando_Unchecked;

                    opcionQuitarRepetidasVariableVectorOperando.IsChecked = asignacion.QuitarTextosInformacionRepetidos_TextosOperandos.Contains(itemOperando);
                    opcionQuitarRepetidasVariableVectorOperando.Checked += OpcionQuitarRepetidasVariableVectorOperando_Checked;
                    opcionQuitarRepetidasVariableVectorOperando.Unchecked += OpcionQuitarRepetidasVariableVectorOperando_Unchecked;

                    opcionQuitarRepetidasVariableVectorOperando.Content = "Quitar las cadenas de texto repetidas asignadas en en la variable, vector o retornados " + itemOperando.NombreCombo;
                    menuBoton.Items.Add(opcionQuitarRepetidasVariableVectorOperando);
                }
            }

            if (SubOperandos != null &&
                ModoOperacion)
            {
                foreach (var itemOperando in SubOperandos)
                {
                    CheckBox opcionConservarTextosSubOperando = new CheckBox();
                    opcionConservarTextosSubOperando.Tag = new object[] { asignacion, itemOperando };

                    opcionConservarTextosSubOperando.Checked -= OpcionConservarTextosSubOperando_Checked;
                    opcionConservarTextosSubOperando.Unchecked -= OpcionConservarTextosSubOperando_Unchecked;

                    opcionConservarTextosSubOperando.IsChecked = asignacion.AsignarTextosInformacion_TextosSubOperandos.Contains(itemOperando);
                    opcionConservarTextosSubOperando.Checked += OpcionConservarTextosSubOperando_Checked;
                    opcionConservarTextosSubOperando.Unchecked += OpcionConservarTextosSubOperando_Unchecked;

                    opcionConservarTextosSubOperando.Content = "Asignar las cadenas de texto acumuladas asignadas en la variable, vector o retornados " + itemOperando.NombreCombo;
                    menuBoton.Items.Add(opcionConservarTextosSubOperando);

                    CheckBox opcionQuitarRepetidasVariableVectorOperando = new CheckBox();
                    opcionQuitarRepetidasVariableVectorOperando.Tag = new object[] { asignacion, itemOperando };

                    opcionQuitarRepetidasVariableVectorOperando.Checked -= OpcionQuitarRepetidasVariableVectorOperando_Checked1;
                    opcionQuitarRepetidasVariableVectorOperando.Unchecked -= OpcionQuitarRepetidasVariableVectorOperando_Unchecked1;

                    opcionQuitarRepetidasVariableVectorOperando.IsChecked = asignacion.QuitarTextosInformacionRepetidos_TextosSubOperandos.Contains(itemOperando);
                    opcionQuitarRepetidasVariableVectorOperando.Checked += OpcionQuitarRepetidasVariableVectorOperando_Checked1;
                    opcionQuitarRepetidasVariableVectorOperando.Unchecked += OpcionQuitarRepetidasVariableVectorOperando_Unchecked1;

                    opcionQuitarRepetidasVariableVectorOperando.Content = "Quitar las cadenas de texto repetidas asignadas en la variable, vector o retornados " + itemOperando.NombreCombo;
                    menuBoton.Items.Add(opcionQuitarRepetidasVariableVectorOperando);
                }
            }

            CheckBox opcionConservarSoloOperacion = new CheckBox();
            opcionConservarSoloOperacion.Tag = asignacion;

            opcionConservarSoloOperacion.Checked -= OpcionConservarSoloOperacion_Checked;
            opcionConservarSoloOperacion.Unchecked -= OpcionConservarSoloOperacion_Unchecked;

            opcionConservarSoloOperacion.IsChecked = (bool)asignacion.AsignarTextosInformacion_Operacion;
            opcionConservarSoloOperacion.Checked += OpcionConservarSoloOperacion_Checked;
            opcionConservarSoloOperacion.Unchecked += OpcionConservarSoloOperacion_Unchecked;

            opcionConservarSoloOperacion.Content = "Asignar las cadenas de texto acumuladas asignadas en la variable o vector retornados actual";
            menuBoton.Items.Add(opcionConservarSoloOperacion);

            CheckBox opcionQuitarRepetidasVariableVector_Actual = new CheckBox();
            opcionQuitarRepetidasVariableVector_Actual.Tag = asignacion;

            opcionQuitarRepetidasVariableVector_Actual.Checked -= OpcionQuitarRepetidasVariableVector_Actual_Checked;
            opcionQuitarRepetidasVariableVector_Actual.Unchecked -= OpcionQuitarRepetidasVariableVector_Actual_Unchecked;

            opcionQuitarRepetidasVariableVector_Actual.IsChecked = (bool)asignacion.QuitarTextosInformacion_RepetidosVariableVector_Actual;
            opcionQuitarRepetidasVariableVector_Actual.Checked += OpcionQuitarRepetidasVariableVector_Actual_Checked;
            opcionQuitarRepetidasVariableVector_Actual.Unchecked += OpcionQuitarRepetidasVariableVector_Actual_Unchecked;

            opcionQuitarRepetidasVariableVector_Actual.Content = "Quitar las cadenas de texto repetidas asignadas en la variable o vector retornados actual";
            menuBoton.Items.Add(opcionQuitarRepetidasVariableVector_Actual);

            CheckBox opcionSoloCantidadActual = new CheckBox();
            opcionSoloCantidadActual.Tag = asignacion;
            opcionSoloCantidadActual.IsChecked = (bool)asignacion.AsignarTextosInformacion_CantidadActual;
            opcionSoloCantidadActual.Checked += OpcionConservarSoloCantidadActual_Checked;
            opcionSoloCantidadActual.Unchecked += OpcionConservarSoloCantidadActual_Unchecked;

            opcionSoloCantidadActual.Content = "Asignar las cadenas de texto acumuladas asignadas en la iteración de lógica actual";
            menuBoton.Items.Add(opcionSoloCantidadActual);

            CheckBox opcionQuitarRepetidasIteracion_Actual = new CheckBox();
            opcionQuitarRepetidasIteracion_Actual.Tag = asignacion;

            opcionQuitarRepetidasIteracion_Actual.Checked -= OpcionQuitarRepetidasIteracion_Actual_Checked;
            opcionQuitarRepetidasIteracion_Actual.Unchecked -= OpcionQuitarRepetidasIteracion_Actual_Unchecked;

            opcionQuitarRepetidasIteracion_Actual.IsChecked = (bool)asignacion.QuitarTextosInformacion_RepetidosIteracion_Actual;
            opcionQuitarRepetidasIteracion_Actual.Checked += OpcionQuitarRepetidasIteracion_Actual_Checked;
            opcionQuitarRepetidasIteracion_Actual.Unchecked += OpcionQuitarRepetidasIteracion_Actual_Unchecked;

            opcionQuitarRepetidasIteracion_Actual.Content = "Quitar las cadenas de texto repetidas asignadas en la iteración de lógica actual";
            menuBoton.Items.Add(opcionQuitarRepetidasIteracion_Actual);

            CheckBox opcionConservarSoloCondicion = new CheckBox();
            opcionConservarSoloCondicion.Tag = asignacion;
            
            opcionConservarSoloCondicion.Checked -= OpcionConservarSoloCondicion_Checked;
            opcionConservarSoloCondicion.Unchecked -= OpcionConservarSoloCondicion_Unchecked;

            opcionConservarSoloCondicion.IsChecked = (bool)asignacion.AsignarTextosInformacion_Condicion;
            opcionConservarSoloCondicion.Checked += OpcionConservarSoloCondicion_Checked;
            opcionConservarSoloCondicion.Unchecked += OpcionConservarSoloCondicion_Unchecked;

            opcionConservarSoloCondicion.Content = "Asignar las cadenas de texto acumuladas asignadas en la condición (si/entonces) actual";
            menuBoton.Items.Add(opcionConservarSoloCondicion);

            CheckBox opcionQuitarRepetidasCondicion_Actual = new CheckBox();
            opcionQuitarRepetidasCondicion_Actual.Tag = asignacion;

            opcionQuitarRepetidasCondicion_Actual.Checked -= OpcionQuitarRepetidasCondicion_Actual_Checked;
            opcionQuitarRepetidasCondicion_Actual.Unchecked -= OpcionQuitarRepetidasCondicion_Actual_Unchecked;

            opcionQuitarRepetidasCondicion_Actual.IsChecked = (bool)asignacion.QuitarTextosInformacion_RepetidosCondicion_Actual;
            opcionQuitarRepetidasCondicion_Actual.Checked += OpcionQuitarRepetidasCondicion_Actual_Checked;
            opcionQuitarRepetidasCondicion_Actual.Unchecked += OpcionQuitarRepetidasCondicion_Actual_Unchecked;

            opcionQuitarRepetidasCondicion_Actual.Content = "Quitar las cadenas de texto repetidas asignadas en la iteración de lógica actual";
            menuBoton.Items.Add(opcionQuitarRepetidasCondicion_Actual);

            CheckBox opcionConservarOriginales = new CheckBox();
            opcionConservarOriginales.Tag = asignacion;

            opcionConservarOriginales.Checked -= OpcionConservarOriginales_Checked;
            opcionConservarOriginales.Unchecked -= OpcionConservarOriginales_Unchecked;

            opcionConservarOriginales.IsChecked = (bool)asignacion.ReemplazarTextosInformacion;
            opcionConservarOriginales.Checked += OpcionConservarOriginales_Checked;
            opcionConservarOriginales.Unchecked += OpcionConservarOriginales_Unchecked;

            opcionConservarOriginales.Content = "Limpiar cadenas de texto asignadas al número";
            menuBoton.Items.Add(opcionConservarOriginales);

            CheckBox opcionConservarPrevios = new CheckBox();
            opcionConservarPrevios.Tag = asignacion;

            opcionConservarPrevios.Checked -= OpcionConservarPrevios_Checked;
            opcionConservarPrevios.Unchecked -= OpcionConservarPrevios_Unchecked;

            opcionConservarPrevios.IsChecked = (bool)asignacion.ConservarTextosInformacion;
            opcionConservarPrevios.Checked += OpcionConservarPrevios_Checked;
            opcionConservarPrevios.Unchecked += OpcionConservarPrevios_Unchecked;

            opcionConservarPrevios.Content = "Conservar cadenas de texto previas al limpiarlas del número";
            menuBoton.Items.Add(opcionConservarPrevios);

        }

        private void OpcionConservarPrevios_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.ConservarTextosInformacion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarPrevios_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.ConservarTextosInformacion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosCantidad = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasCantidad_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosCantidad = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVectorOperando_Unchecked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (asig.QuitarTextosInformacionRepetidos_TextosSubOperandos.Contains(operando))
                            asig.QuitarTextosInformacionRepetidos_TextosSubOperandos.Remove(operando);
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVectorOperando_Checked1(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (!asig.QuitarTextosInformacionRepetidos_TextosSubOperandos.Contains(operando))
                            asig.QuitarTextosInformacionRepetidos_TextosSubOperandos.Add(operando);
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVectorOperando_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (asig.QuitarTextosInformacionRepetidos_TextosOperandos.Contains(operando))
                            asig.QuitarTextosInformacionRepetidos_TextosOperandos.Remove(operando);
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVectorOperando_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (!asig.QuitarTextosInformacionRepetidos_TextosOperandos.Contains(operando))
                            asig.QuitarTextosInformacionRepetidos_TextosOperandos.Add(operando);
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasCondicion_Actual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosCondicion_Actual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasCondicion_Actual_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosCondicion_Actual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasIteracion_Actual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosIteracion_Actual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasIteracion_Actual_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosIteracion_Actual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVector_Actual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosVariableVector_Actual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVector_Actual_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosVariableVector_Actual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVector_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosVariableVector = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionQuitarRepetidasVariableVector_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.QuitarTextosInformacion_RepetidosVariableVector = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarTextosSubOperando_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (asig.AsignarTextosInformacion_TextosSubOperandos.Contains(operando))
                            asig.AsignarTextosInformacion_TextosSubOperandos.Remove(operando);
                    }
                }
            }
        }

        private void OpcionConservarTextosSubOperando_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (!asig.AsignarTextosInformacion_TextosSubOperandos.Contains(operando))
                            asig.AsignarTextosInformacion_TextosSubOperandos.Add(operando);
                    }
                }
            }
        }

        private void OpcionConservarTextosOperando_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if (asig.AsignarTextosInformacion_TextosOperandos.Contains(operando))
                            asig.AsignarTextosInformacion_TextosOperandos.Remove(operando);
                    }
                }
            }
        }

        private void OpcionConservarTextosOperando_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    object[] objetos = (object[])((CheckBox)sender).Tag;

                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)objetos[0];
                    DiseñoOperacion operando = (DiseñoOperacion)objetos[1];

                    if (asig != null)
                    {
                        if(!asig.AsignarTextosInformacion_TextosOperandos.Contains(operando))
                            asig.AsignarTextosInformacion_TextosOperandos.Add(operando);
                    }
                }
            }
        }

        private void OpcionConservarSoloOperandos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_CondicionOperandos = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloOperandos_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_CondicionOperandos = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloCantidadActual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_CantidadActual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloCantidadActual_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_CantidadActual = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloCondicion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_Condicion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloCondicion_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_Condicion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_Operacion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarSoloOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.AsignarTextosInformacion_Operacion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarOriginales_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.ReemplazarTextosInformacion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void OpcionConservarOriginales_Checked(object sender, RoutedEventArgs e)
        {
            if (!Cargando)
            {
                if (IsLoaded &&
                ((CheckBox)sender).Parent != null)
                {
                    InstanciaAsignacionImplicacion_TextosInformacion asig = (InstanciaAsignacionImplicacion_TextosInformacion)((CheckBox)sender).Tag;
                    if (asig != null)
                    {
                        asig.ReemplazarTextosInformacion = (bool)((CheckBox)sender).IsChecked;
                    }
                }
            }
        }

        private void EditarTexto(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Tag != null)
            {
                InstanciaAsignacionImplicacion_TextosInformacion asignacion = (InstanciaAsignacionImplicacion_TextosInformacion)((TextBox)sender).Tag;
                asignacion.TextoImplicaAsignacion = ((TextBox)sender).Text;
            }
        }

        private void QuitarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;
            ImplicacionesTextosInformacion.RemoveAt(indice);
            ListarTextos();
        }

        private void SubirTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice > 0)
            {
                AsignacionImplicacion_TextosInformacion textos = ImplicacionesTextosInformacion[indice];
                ImplicacionesTextosInformacion.RemoveAt(indice);

                ImplicacionesTextosInformacion.Insert(indice - 1, textos);
                ListarTextos();
            }
        }

        private void BajarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice < ImplicacionesTextosInformacion.Count - 1)
            {
                AsignacionImplicacion_TextosInformacion textos = ImplicacionesTextosInformacion[indice];
                ImplicacionesTextosInformacion.RemoveAt(indice);

                ImplicacionesTextosInformacion.Insert(indice + 1, textos);
                ListarTextos();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (OperacionRelacionada_Definicion != null)
            {
                opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked = (bool)OperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_AntesEjecucion;
                opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked = (bool)OperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_DespuesEjecucion;
            }
            else if (SubOperacionRelacionada_Definicion != null)
            {
                opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked = (bool)SubOperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_AntesEjecucion;
                opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked = (bool)SubOperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_DespuesEjecucion;
            }

            ListarTextos();
        }

        private void agregarTextoInformacion_Click(object sender, RoutedEventArgs e)
        {            

            ImplicacionesTextosInformacion.Add(new AsignacionImplicacion_TextosInformacion());
            ImplicacionesTextosInformacion.LastOrDefault().InstanciasAsignacion.Add(new InstanciaAsignacionImplicacion_TextosInformacion());

            ImplicacionesTextosInformacion.LastOrDefault().InstanciasAsignacion.LastOrDefault().Entradas_DesdeAsignarTextosInformacion = Entradas.ToList();
            ImplicacionesTextosInformacion.LastOrDefault().InstanciasAsignacion.LastOrDefault().Operandos_DesdeAsignarTextosInformacion = Operandos.ToList();
            ImplicacionesTextosInformacion.LastOrDefault().InstanciasAsignacion.LastOrDefault().SubOperandos_DesdeAsignarTextosInformacion = SubOperandos.ToList();

            ListarTextos();
        }

        private void EstablecerOpcionesNombresCantidades(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (ImplicacionesTextosInformacion[indice].DefinicionOpcionesNombresCantidades == null)
            ImplicacionesTextosInformacion[indice].DefinicionOpcionesNombresCantidades = new DefinicionTextoNombresCantidades();

            ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
            establecer.Operandos = Operandos;
            establecer.ListaElementos = Elementos.Except(establecer.Operandos).ToList();
            //establecer.SubOperandos = SubOperandos;
            establecer.TextosNombre = ImplicacionesTextosInformacion[indice].DefinicionOpcionesNombresCantidades;            
            establecer.ShowDialog();            
        }

        private List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> CopiarCondiciones(List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> lista)
        {
            List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> resultado = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();

            foreach(var item in lista)
            {
                resultado.Add(new AsociacionOperandosCondiciones_TextosAsignacion_Implicacion()
                {
                    Condiciones = item.Condiciones?.ReplicarObjeto(),
                    Entrada = item.Entrada,
                    Operando = item.Operando,
                    SubOperando = item.SubOperando
                });
            }

            return resultado;
        }

        private List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> CopiarDefiniciones(List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> lista)
        {
            List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> resultado = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();

            foreach (var item in lista)
            {
                resultado.Add(new AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion()
                {
                    Definiciones = item.ReplicarDefiniciones(),
                    Entrada = item.Entrada,
                    Operando = item.Operando,
                    SubOperando = item.SubOperando
                });
            }

            return resultado;
        }

        private void opcionAsignarTextosInformacionAntes_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (OperacionRelacionada_Definicion != null)
                {
                    OperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }
                else if (SubOperacionRelacionada_Definicion != null)
                {
                    SubOperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionAntes_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (OperacionRelacionada_Definicion != null)
                {
                    OperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }
                else if (SubOperacionRelacionada_Definicion != null)
                {
                    SubOperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (OperacionRelacionada_Definicion != null)
                {
                    OperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }
                else if (SubOperacionRelacionada_Definicion != null)
                {
                    SubOperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (OperacionRelacionada_Definicion != null)
                {
                    OperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }
                else if (SubOperacionRelacionada_Definicion != null)
                {
                    SubOperacionRelacionada_Definicion.AsignarTextosInformacionImplicaciones_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }
            }
        }
    }
}
