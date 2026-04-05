using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para DefinirOperacion_Espera.xaml
    /// </summary>
    public partial class DefinirOperacion_Espera : Window
    {
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoOperacion> OperandosSalidas { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosSalidas { get; set; }
        public List<DiseñoOperacion[]> EntradasSalidas_Espera { get; set; }
        public List<DiseñoElementoOperacion[]> EntradasSalidasOperacion_Espera { get; set; }
        public double TiempoEspera { get; set; }
        public TipoTiempoEspera TipoTiempoEspera {  set; get; }
        public bool CantidadEsperas_Fijas { get; set; }
        public int CantidadVerificaciones {  get; set; }
        public CondicionTextosInformacion CondicionesTextosInformacion_Espera { get; set; }
        public CondicionFlujo CondicionesCantidades_Espera { get; set; }
        public bool VerificarCondiciones_Hasta { get; set; }
        public bool EjecutarImplicacionesAntes_Espera { get; set; }
        public bool EjecutarImplicacionesDespues_Espera { get; set; }
        public bool EjecutarImplicacionesDurante_Espera { get; set; }
        public bool MostrarOpcionesImplicaciones { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public DefinirOperacion_Espera()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(ModoOperacion)
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

                        var dupla = EntradasSalidasOperacion_Espera.Where(i => i[0] == item & i[1] == itemMenu).FirstOrDefault();

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
                foreach(var item in Operandos)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;

                    botonOperando.ContextMenu = new ContextMenu();
                    
                    foreach(var itemMenu in OperandosSalidas)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = itemMenu.NombreCombo;
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = EntradasSalidas_Espera.Where(i => i[0] == item & i[1] == itemMenu).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosSalidas.Children.Add(botonOperando);
                }
            }

            cantidadTiempo.Text = TiempoEspera.ToString();
            cantidadVerificaciones.Text = CantidadVerificaciones.ToString();
            opcionCantidadTiempo.SelectedItem = (from ComboBoxItem I in opcionCantidadTiempo.Items where I.Uid == ((int)TipoTiempoEspera).ToString() select I).FirstOrDefault();
            
            if(CantidadEsperas_Fijas)
            {
                opcionVerificacionesFijas.IsChecked = true;
            }
            else
            {
                opcionVerificacionesCondiciones.IsChecked = true;
            }

            cantidadVerificaciones.Text = CantidadVerificaciones.ToString();

            condicionesCantidades.Operandos = Operandos;
            condicionesCantidades.SubOperandos = SubOperandos;
            condicionesCantidades.ModoOperacion = ModoOperacion;
            condicionesCantidades.ListaElementos = ListaElementos;

            condicionesTextosInformacion.Operandos = Operandos;
            condicionesTextosInformacion.ListaElementos = ListaElementos;
            condicionesTextosInformacion.SubOperandos = SubOperandos;
            condicionesTextosInformacion.DefinicionesListas = DefinicionesListas;

            condicionesTextosInformacion.Condiciones = CondicionesTextosInformacion_Espera;
            condicionesCantidades.Condiciones = CondicionesCantidades_Espera;

            if(VerificarCondiciones_Hasta)
            {
                opcionVerificacionesCondicionesHasta.IsChecked = true;
            }
            else
            {
                opcionVerificacionesCondicionesMientras.IsChecked = true;
            }

            if (EjecutarImplicacionesAntes_Espera)
            {
                opcionImplicacionesAntes.IsChecked = true;
            }

            if(EjecutarImplicacionesDespues_Espera)
            {
                opcionImplicacionesDespues.IsChecked = true;
            }

            if(EjecutarImplicacionesDurante_Espera)
            {
                opcionImplicacionesDurante.IsChecked = true;
            }

            if(MostrarOpcionesImplicaciones)
            {
                opcionesImplicaciones.Visibility = Visibility.Visible;
            }
        }

        private void OpcionMenu_Click(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;
            bool seleccionado = (bool)((CheckBox)sender).IsChecked;

            if (ModoOperacion)
            {
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];
                DiseñoElementoOperacion salida = (DiseñoElementoOperacion)objetos[1];

                var dupla = EntradasSalidasOperacion_Espera.Where(i => i[0] == operando & i[1] == salida).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSalidasOperacion_Espera.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSalidasOperacion_Espera.Add(new DiseñoElementoOperacion[] { operando, salida });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                DiseñoOperacion salida = (DiseñoOperacion)objetos[1];

                var dupla = EntradasSalidas_Espera.Where(i => i[0] == operando & i[1] == salida).FirstOrDefault();

                if (dupla != null)
                {
                    if(!seleccionado)
                    {
                        EntradasSalidas_Espera.Remove(dupla);
                    }
                }
                else
                {
                    if(seleccionado)
                    {
                        EntradasSalidas_Espera.Add(new DiseñoOperacion[] { operando, salida });
                    }
                }
            }
        }

        private void BotonOperando_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void cantidadTiempo_TextChanged(object sender, TextChangedEventArgs e)
        {
            double cantidad = 0;
            if(double.TryParse(cantidadTiempo.Text, out cantidad))
            {
                TiempoEspera = cantidad;
            }
        }

        private void opcionCantidadTiempo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsLoaded)
                TipoTiempoEspera = (TipoTiempoEspera)int.Parse(((ComboBoxItem)opcionCantidadTiempo.SelectedItem).Uid);
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void opcionVerificacionesFijas_Checked(object sender, RoutedEventArgs e)
        {
            CantidadEsperas_Fijas = (bool)opcionVerificacionesFijas.IsChecked;
        }

        private void opcionVerificacionesCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            CantidadEsperas_Fijas = (bool)!opcionVerificacionesCondiciones.IsChecked;
        }

        private void cantidadVerificaciones_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cantidad = 0;
            if (int.TryParse(cantidadVerificaciones.Text, out cantidad))
            {
                CantidadVerificaciones = cantidad;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CondicionesTextosInformacion_Espera = condicionesTextosInformacion.Condiciones;
            CondicionesCantidades_Espera = condicionesCantidades.Condiciones;
        }

        private void opcionVerificacionesCondicionesHasta_Checked(object sender, RoutedEventArgs e)
        {
            VerificarCondiciones_Hasta = (bool)opcionVerificacionesCondicionesHasta.IsChecked;
        }

        private void opcionVerificacionesCondicionesMientras_Checked(object sender, RoutedEventArgs e)
        {
            VerificarCondiciones_Hasta = (bool)!opcionVerificacionesCondicionesMientras.IsChecked;
        }

        private void opcionImplicacionesAntes_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesAntes_Espera = (bool)opcionImplicacionesAntes.IsChecked;
        }

        private void opcionImplicacionesAntes_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesAntes_Espera = (bool)opcionImplicacionesAntes.IsChecked;
        }

        private void opcionImplicacionesDurante_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDurante_Espera = (bool)opcionImplicacionesDurante.IsChecked;
        }

        private void opcionImplicacionesDurante_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDurante_Espera = (bool)opcionImplicacionesDurante.IsChecked;
        }

        private void opcionImplicacionesDespues_Checked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDespues_Espera = (bool)opcionImplicacionesDespues.IsChecked;
        }

        private void opcionImplicacionesDespues_Unchecked(object sender, RoutedEventArgs e)
        {
            EjecutarImplicacionesDespues_Espera = (bool)opcionImplicacionesDespues.IsChecked;
        }
    }
}
