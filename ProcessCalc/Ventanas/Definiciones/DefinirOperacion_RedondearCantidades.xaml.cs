using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Operaciones;
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
    /// Lógica de interacción para DefinirOperacion_RedondearCantidades.xaml
    /// </summary>
    public partial class DefinirOperacion_RedondearCantidades : Window
    {
        public ConfiguracionRedondearCantidades config { get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoOperacion> OperandosSalidas { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosSalidas { get; set; }
        public List<DiseñoOperacion[]> EntradasSalidas_RedondearCantidades { get; set; }
        public List<DiseñoElementoOperacion[]> EntradasSalidasOperacion_RedondearCantidades { get; set; }
        public bool MostrarOpcionesImplicaciones { get; set; }
        public bool EjecutarImplicacionesAntes_Redondeo { get; set; }
        public bool EjecutarImplicacionesDurante_Redondeo { get; set; }
        public bool EjecutarImplicacionesDespues_Redondeo { get; set; }
        public bool ModoComportamiento { get; set; }
        public DefinirOperacion_RedondearCantidades()
        {
            InitializeComponent();
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            config.RedondearPar_Cercano = (bool)opcionRedondeoParCercano.IsChecked;
            config.RedondearNumero_LejanoDeCero = (bool)opcionRedondeoLejanoCero.IsChecked;
            config.RedondearNumero_CercanoDeCero = (bool)opcionRedondeoCercanoCero.IsChecked;
            config.RedondearNumero_Mayor = (bool)opcionRedondeoNumeroMayor.IsChecked;
            config.RedondearNumero_Menor = (bool)opcionRedondeoNumeroMenor.IsChecked;

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
            if (config != null)
            {
                opcionRedondeoParCercano.IsChecked = config.RedondearPar_Cercano;
                opcionRedondeoLejanoCero.IsChecked = config.RedondearNumero_LejanoDeCero;
                opcionRedondeoCercanoCero.IsChecked = config.RedondearNumero_CercanoDeCero;
                opcionRedondeoNumeroMayor.IsChecked = config.RedondearNumero_Mayor;
                opcionRedondeoNumeroMenor.IsChecked = config.RedondearNumero_Menor;

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

                                var dupla = EntradasSalidasOperacion_RedondearCantidades.Where(i => i[0] == item & i[1] == itemMenu).FirstOrDefault();

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

                                var dupla = EntradasSalidas_RedondearCantidades.Where(i => i[0] == item & i[1] == itemMenu).FirstOrDefault();

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
                if (EjecutarImplicacionesAntes_Redondeo)
                {
                    opcionImplicacionesAntes.IsChecked = true;
                }

                if (EjecutarImplicacionesDurante_Redondeo)
                {
                    opcionImplicacionesDurante.IsChecked = true;
                }

                if (EjecutarImplicacionesDespues_Redondeo)
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

                var dupla = EntradasSalidasOperacion_RedondearCantidades.Where(i => i[0] == operando & i[1] == salida).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSalidasOperacion_RedondearCantidades.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSalidasOperacion_RedondearCantidades.Add(new DiseñoElementoOperacion[] { operando, salida });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                DiseñoOperacion salida = (DiseñoOperacion)objetos[1];

                var dupla = EntradasSalidas_RedondearCantidades.Where(i => i[0] == operando & i[1] == salida).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSalidas_RedondearCantidades.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSalidas_RedondearCantidades.Add(new DiseñoOperacion[] { operando, salida });
                    }
                }
            }
        }

        private void opcionImplicacionesAntes_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesAntes_Redondeo = (bool)opcionImplicacionesAntes.IsChecked;
        }

        private void opcionImplicacionesAntes_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesAntes_Redondeo = (bool)opcionImplicacionesAntes.IsChecked;
        }

        private void opcionImplicacionesDurante_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDurante_Redondeo = (bool)opcionImplicacionesDurante.IsChecked;
        }

        private void opcionImplicacionesDurante_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDurante_Redondeo = (bool)opcionImplicacionesDurante.IsChecked;
        }

        private void opcionImplicacionesDespues_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDespues_Redondeo = (bool)opcionImplicacionesDespues.IsChecked;
        }

        private void opcionImplicacionesDespues_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDespues_Redondeo = (bool)opcionImplicacionesDespues.IsChecked;
        }
    }
}
