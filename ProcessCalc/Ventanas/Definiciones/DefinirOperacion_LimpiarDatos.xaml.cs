using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para DefinirOperacion_LimpiarDatos.xaml
    /// </summary>
    public partial class DefinirOperacion_LimpiarDatos : Window
    {
        public ConfiguracionLimpiarDatos config {  get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoOperacion> OperandosSalidas { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosSalidas { get; set; }
        public List<DiseñoOperacion[]> EntradasSalidas_LimpiarDatos { get; set; }
        public List<DiseñoElementoOperacion[]> EntradasSalidasOperacion_LimpiarDatos { get; set; }
        public bool MostrarOpcionesImplicaciones { get; set; }
        public bool EjecutarImplicacionesAntes_Limpieza { get; set; }
        public bool EjecutarImplicacionesDurante_Limpieza { get; set; }
        public bool EjecutarImplicacionesDespues_Limpieza { get; set; }
        public bool ModoComportamiento { get; set; }
        public DefinirOperacion_LimpiarDatos()
        {
            InitializeComponent();
        }

        private void opcionQuitarDuplicados_Checked(object sender, RoutedEventArgs e)
        {
            opcionesDuplicados.Visibility = Visibility.Visible;
        }

        private void opcionQuitarDuplicados_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionesDuplicados.Visibility = Visibility.Collapsed;
        }

        private void opcionQuitarCeros_Checked(object sender, RoutedEventArgs e)
        {
            opcionesCeros.Visibility = Visibility.Visible;
        }

        private void opcionQuitarCeros_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionesCeros.Visibility = Visibility.Collapsed;
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            config.QuitarDuplicados = (bool)opcionQuitarDuplicados.IsChecked;
            config.QuitarCantidadesDuplicadas = (bool)opcionQuitarCantidadesDuplicadas.IsChecked;
            if(opcionConector1_Duplicados.SelectedItem != null)
                config.Conector1_Duplicados = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)opcionConector1_Duplicados.SelectedItem).Uid);
            config.QuitarCantidadesTextosDuplicadas = (bool)opcionQuitarCantidadesTextosDuplicadas.IsChecked;
            if(opcionConector2_Duplicados.SelectedItem != null)
                config.Conector2_Duplicados = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)opcionConector2_Duplicados.SelectedItem).Uid);
            config.QuitarCantidadesTextosDentroDuplicados = (bool)opcionQuitarCantidadesTextosDentroDuplicados.IsChecked;
            config.QuitarCeros = (bool)opcionQuitarCeros.IsChecked;
            config.QuitarCerosConTextos = (bool)opcionQuitarCerosConTextos.IsChecked;
            if(opcionConector1_Ceros.SelectedItem != null)
                config.Conector1_Ceros = (TipoConectorCondiciones_ConjuntoBusquedas)int.Parse(((ComboBoxItem)opcionConector1_Ceros.SelectedItem).Uid);
            config.QuitarCerosSinTextos = (bool)opcionQuitarCerosSinTextos.IsChecked;
            config.QuitarCantidadesSinTextos = (bool)opcionQuitarCantidadesSinTextos.IsChecked;
            config.QuitarNegativas = (bool)opcionQuitarNegativas.IsChecked;
            
            DialogResult = true;
            Close();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(config != null)
            {
                opcionQuitarDuplicados.IsChecked = config.QuitarDuplicados;
                opcionQuitarCantidadesDuplicadas.IsChecked = config.QuitarCantidadesDuplicadas;
                opcionConector1_Duplicados.SelectedItem = (from ComboBoxItem I in opcionConector1_Duplicados.Items where I.Uid == ((int)config.Conector1_Duplicados).ToString() select I).FirstOrDefault();
                opcionQuitarCantidadesTextosDuplicadas.IsChecked = config.QuitarCantidadesTextosDuplicadas;
                opcionConector2_Duplicados.SelectedItem = (from ComboBoxItem I in opcionConector2_Duplicados.Items where I.Uid == ((int)config.Conector2_Duplicados).ToString() select I).FirstOrDefault();
                opcionQuitarCantidadesTextosDentroDuplicados.IsChecked = config.QuitarCantidadesTextosDentroDuplicados;
                opcionQuitarCeros.IsChecked = config.QuitarCeros;
                opcionQuitarCerosConTextos.IsChecked = config.QuitarCerosConTextos;
                opcionConector1_Ceros.SelectedItem = (from ComboBoxItem I in opcionConector1_Ceros.Items where I.Uid == ((int)config.Conector1_Ceros).ToString() select I).FirstOrDefault();
                opcionQuitarCerosSinTextos.IsChecked = config.QuitarCerosSinTextos;
                opcionQuitarCantidadesSinTextos.IsChecked = config.QuitarCantidadesSinTextos;
                opcionQuitarNegativas.IsChecked = config.QuitarNegativas;

                if (ModoComportamiento)
                {
                    operandosSalidas.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (ModoOperacion)
                    {
                        foreach (var item in SubOperandos)
                        {
                            Button botonOperando = new Button();
                            //botonOperando.Tag = item;
                            botonOperando.Content = item.NombreCombo;
                            botonOperando.Margin = new Thickness(10);
                            botonOperando.Padding = new Thickness(5);
                            botonOperando.Click += BotonOperando_Click;

                            botonOperando.ContextMenu = new ContextMenu();

                            foreach (var itemMenu in SubOperandosSalidas)
                            {
                                CheckBox opcionMenu = new CheckBox();
                                opcionMenu.Tag = new object[] { item, itemMenu };
                                opcionMenu.Content = itemMenu.NombreCombo;
                                opcionMenu.Padding = new Thickness(5);

                                var dupla = EntradasSalidasOperacion_LimpiarDatos.Where(i => i[0] == item & i[1] == itemMenu).FirstOrDefault();

                                if (dupla != null)
                                    opcionMenu.IsChecked = true;

                                opcionMenu.Click += OpcionMenu_Click;
                                botonOperando.ContextMenu.Items.Add(opcionMenu);
                            }

                            operandosSalidas.Children.Add(botonOperando);
                        }
                    }
                    else
                    {
                        foreach (var item in Operandos)
                        {
                            Button botonOperando = new Button();
                            //botonOperando.Tag = item;
                            botonOperando.Content = item.NombreCombo;
                            botonOperando.Margin = new Thickness(10);
                            botonOperando.Padding = new Thickness(5);
                            botonOperando.Click += BotonOperando_Click;

                            botonOperando.ContextMenu = new ContextMenu();

                            foreach (var itemMenu in OperandosSalidas)
                            {
                                CheckBox opcionMenu = new CheckBox();
                                opcionMenu.Tag = new object[] { item, itemMenu };
                                opcionMenu.Content = itemMenu.NombreCombo;
                                opcionMenu.Padding = new Thickness(5);

                                var dupla = EntradasSalidas_LimpiarDatos.Where(i => i[0] == item & i[1] == itemMenu).FirstOrDefault();

                                if (dupla != null)
                                    opcionMenu.IsChecked = true;

                                opcionMenu.Click += OpcionMenu_Click;
                                botonOperando.ContextMenu.Items.Add(opcionMenu);
                            }

                            operandosSalidas.Children.Add(botonOperando);
                        }
                    }
                }
            }

            if (ModoComportamiento)
            {
                opcionImplicacionesAntes.Visibility = Visibility.Collapsed;
                opcionImplicacionesDurante.Visibility = Visibility.Collapsed;
                opcionImplicacionesDespues.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (EjecutarImplicacionesAntes_Limpieza)
                {
                    opcionImplicacionesAntes.IsChecked = true;
                }

                if (EjecutarImplicacionesDurante_Limpieza)
                {
                    opcionImplicacionesDurante.IsChecked = true;
                }

                if (EjecutarImplicacionesDespues_Limpieza)
                {
                    opcionImplicacionesDespues.IsChecked = true;
                }
            }

            if (MostrarOpcionesImplicaciones)
            {
                opcionesImplicaciones.Visibility = Visibility.Visible;
            }
        }

        private void BotonOperando_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void OpcionMenu_Click(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;
            bool seleccionado = (bool)((CheckBox)sender).IsChecked;

            if (ModoOperacion)
            {
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];
                DiseñoElementoOperacion salida = (DiseñoElementoOperacion)objetos[1];

                var dupla = EntradasSalidasOperacion_LimpiarDatos.Where(i => i[0] == operando & i[1] == salida).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSalidasOperacion_LimpiarDatos.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSalidasOperacion_LimpiarDatos.Add(new DiseñoElementoOperacion[] { operando, salida });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                DiseñoOperacion salida = (DiseñoOperacion)objetos[1];

                var dupla = EntradasSalidas_LimpiarDatos.Where(i => i[0] == operando & i[1] == salida).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSalidas_LimpiarDatos.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSalidas_LimpiarDatos.Add(new DiseñoOperacion[] { operando, salida });
                    }
                }
            }
        }

        private void opcionImplicacionesAntes_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesAntes_Limpieza = (bool)opcionImplicacionesAntes.IsChecked;
        }

        private void opcionImplicacionesAntes_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesAntes_Limpieza = (bool)opcionImplicacionesAntes.IsChecked;
        }

        private void opcionImplicacionesDurante_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDurante_Limpieza = (bool)opcionImplicacionesDurante.IsChecked;
        }

        private void opcionImplicacionesDurante_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDurante_Limpieza = (bool)opcionImplicacionesDurante.IsChecked;
        }

        private void opcionImplicacionesDespues_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDespues_Limpieza = (bool)opcionImplicacionesDespues.IsChecked;
        }

        private void opcionImplicacionesDespues_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDespues_Limpieza = (bool)opcionImplicacionesDespues.IsChecked;
        }
    }
}
