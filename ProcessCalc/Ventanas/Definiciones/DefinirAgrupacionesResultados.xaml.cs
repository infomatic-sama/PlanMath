using ProcessCalc.Entidades;
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
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para DefinirAgrupacionesResultados.xaml
    /// </summary>
    public partial class DefinirAgrupacionesResultados : Window
    {
        public List<DiseñoOperacion> OperandosResultados { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosResultados { get; set; }
        public List<AgrupacionOperando_PorSeparado> Agrupaciones {  get; set; }
        bool Cargando;
        public bool ModoOperacion {  get; set; }
        public DiseñoOperacion ElementoAsociado { get; set; }
        public DiseñoElementoOperacion SubElementoAsociado { get; set; }
        public DefinirAgrupacionesResultados()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cargando = true;
            listaOperandosResultados.Children.Clear();

            if (ModoOperacion)
            {
                foreach (var itemOperando in SubOperandosResultados)
                {
                    Grid operando = new Grid();
                    operando.Margin = new Thickness(10);

                    operando.ColumnDefinitions.Add(new ColumnDefinition());
                    operando.ColumnDefinitions.Last().Width = GridLength.Auto;

                    operando.ColumnDefinitions.Add(new ColumnDefinition());
                    operando.ColumnDefinitions.Last().Width = GridLength.Auto;

                    TextBlock botonOperando = new TextBlock();
                    botonOperando.Text = itemOperando.NombreCombo;
                    botonOperando.Padding = new Thickness(5);

                    operando.Children.Add(botonOperando);
                    Grid.SetColumn(botonOperando, 0);

                    Button botonAsignarAgrupaciones = new Button();
                    botonAsignarAgrupaciones.Content = "Asignar agrupaciones";
                    botonAsignarAgrupaciones.Padding = new Thickness(5);
                    botonAsignarAgrupaciones.Tag = itemOperando;

                    ListarAgrupaciones_Operacion(botonAsignarAgrupaciones, itemOperando);
                    botonAsignarAgrupaciones.Click += BotonAsignarAgrupaciones_Click;

                    operando.Children.Add(botonAsignarAgrupaciones);
                    Grid.SetColumn(botonAsignarAgrupaciones, 1);

                    listaOperandosResultados.Children.Add(operando);
                }
            }
            else
            {
                foreach (var itemOperando in OperandosResultados)
                {
                    Grid operando = new Grid();
                    operando.Margin = new Thickness(10);

                    operando.ColumnDefinitions.Add(new ColumnDefinition());
                    operando.ColumnDefinitions.Last().Width = GridLength.Auto;

                    operando.ColumnDefinitions.Add(new ColumnDefinition());
                    operando.ColumnDefinitions.Last().Width = GridLength.Auto;

                    TextBlock botonOperando = new TextBlock();
                    botonOperando.Text = itemOperando.NombreCombo;
                    botonOperando.Padding = new Thickness(5);

                    operando.Children.Add(botonOperando);
                    Grid.SetColumn(botonOperando, 0);

                    Button botonAsignarAgrupaciones = new Button();
                    botonAsignarAgrupaciones.Content = "Asignar agrupaciones";
                    botonAsignarAgrupaciones.Padding = new Thickness(5);
                    botonAsignarAgrupaciones.Tag = itemOperando;

                    ListarAgrupaciones(botonAsignarAgrupaciones, itemOperando);
                    botonAsignarAgrupaciones.Click += BotonAsignarAgrupaciones_Click;

                    operando.Children.Add(botonAsignarAgrupaciones);
                    Grid.SetColumn(botonAsignarAgrupaciones, 1);

                    listaOperandosResultados.Children.Add(operando);
                }
            }

            Cargando = false;
        }

        private void BotonAsignarAgrupaciones_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            boton.ContextMenu.IsOpen = true;            
        }

        private void ListarAgrupaciones(Button boton, DiseñoOperacion Operando)
        {
            ContextMenu menuBoton = new ContextMenu();

            boton.ContextMenu = menuBoton;

            foreach (var itemAgrupacion in Agrupaciones)
            {
                ComboBoxItem opcionOperandos = new ComboBoxItem();
                CheckBox checkOperandos = new CheckBox();
                checkOperandos.Content = "Asignar a " + itemAgrupacion.NombreAgrupacion;
                checkOperandos.Tag = new object[] { Operando, itemAgrupacion };

                checkOperandos.Checked -= CheckOperandos_Checked;
                checkOperandos.Unchecked -= CheckOperandos_Unchecked;

                if (Operando != null)
                    checkOperandos.IsChecked = Operando.AgrupacionesAsignadasOperandos_PorSeparado.Any( i => i.Agrupacion == itemAgrupacion &&
                    i.ElementoAsociado == Operando && i.OperandoAsociado == ElementoAsociado);

                checkOperandos.Checked += CheckOperandos_Checked;
                checkOperandos.Unchecked += CheckOperandos_Unchecked;
                opcionOperandos.Content = checkOperandos;
                menuBoton.Items.Add(opcionOperandos);
            }
        }

        private void ListarAgrupaciones_Operacion(Button boton, DiseñoElementoOperacion SubOperando)
        {
            ContextMenu menuBoton = new ContextMenu();

            boton.ContextMenu = menuBoton;

            foreach (var itemAgrupacion in Agrupaciones)
            {
                ComboBoxItem opcionOperandos = new ComboBoxItem();
                CheckBox checkOperandos = new CheckBox();
                checkOperandos.Content = "Asignar a " + itemAgrupacion.NombreAgrupacion;
                checkOperandos.Tag = new object[] { SubOperando, itemAgrupacion };

                checkOperandos.Checked -= CheckOperandos_Checked_Operacion;
                checkOperandos.Unchecked -= CheckOperandos_Unchecked_Operacion;

                if (SubOperando != null)
                    checkOperandos.IsChecked = SubOperando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == itemAgrupacion &&
                    i.SubElementoAsociado == SubOperando && i.SubOperandoAsociado == SubElementoAsociado);

                checkOperandos.Checked += CheckOperandos_Checked_Operacion;
                checkOperandos.Unchecked += CheckOperandos_Unchecked_Operacion;
                opcionOperandos.Content = checkOperandos;
                menuBoton.Items.Add(opcionOperandos);
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

                        AgrupacionOperando_PorSeparado asignacion = (AgrupacionOperando_PorSeparado)objetos[1];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[0];

                        if (check.IsChecked == false)
                        {
                            if (operando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == asignacion &&
                    i.ElementoAsociado == operando && i.OperandoAsociado == ElementoAsociado))
                                operando.AgrupacionesAsignadasOperandos_PorSeparado.Remove(
                                    operando.AgrupacionesAsignadasOperandos_PorSeparado.FirstOrDefault(
                                        i => i.Agrupacion == asignacion && i.ElementoAsociado == operando && i.OperandoAsociado == ElementoAsociado));

                            Window_Loaded(this, e);
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

                        AgrupacionOperando_PorSeparado asignacion = (AgrupacionOperando_PorSeparado)objetos[1];
                        DiseñoOperacion operando = (DiseñoOperacion)objetos[0];

                        if (check.IsChecked == true)
                        {
                            if (!operando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == asignacion &&
                    i.ElementoAsociado == operando && i.OperandoAsociado == ElementoAsociado))
                            {
                                operando.AgrupacionesAsignadasOperandos_PorSeparado.Add(new AsignacionAgrupacionOperando_PorSeparado()
                                {
                                    Agrupacion = asignacion,
                                    ElementoAsociado = operando,
                                    OperandoAsociado = ElementoAsociado
                                });
                            }

                            Window_Loaded(this, e);
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Unchecked_Operacion(object sender, RoutedEventArgs e)
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

                        AgrupacionOperando_PorSeparado asignacion = (AgrupacionOperando_PorSeparado)objetos[1];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];

                        if (check.IsChecked == false)
                        {
                            if (operando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == asignacion &&
                    i.SubElementoAsociado == operando && i.SubOperandoAsociado == SubElementoAsociado))
                                operando.AgrupacionesAsignadasOperandos_PorSeparado.Remove(
                                    operando.AgrupacionesAsignadasOperandos_PorSeparado.FirstOrDefault(
                                        i => i.Agrupacion == asignacion && i.SubElementoAsociado == operando && i.SubElementoAsociado == SubElementoAsociado));

                            Window_Loaded(this, e);
                        }
                    }
                }
            }
        }

        private void CheckOperandos_Checked_Operacion(object sender, RoutedEventArgs e)
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

                        AgrupacionOperando_PorSeparado asignacion = (AgrupacionOperando_PorSeparado)objetos[1];
                        DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];

                        if (check.IsChecked == true)
                        {
                            if (!operando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == asignacion &&
                    i.SubElementoAsociado == operando && i.SubOperandoAsociado == SubElementoAsociado))
                                operando.AgrupacionesAsignadasOperandos_PorSeparado.Add(new AsignacionAgrupacionOperando_PorSeparado()
                                {
                                    Agrupacion = asignacion,
                                    SubElementoAsociado = operando,
                                    SubOperandoAsociado = SubElementoAsociado
                                });

                            Window_Loaded(this, e);
                        }
                    }
                }
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
