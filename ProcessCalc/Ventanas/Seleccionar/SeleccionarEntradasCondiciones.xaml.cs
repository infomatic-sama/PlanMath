using ProcessCalc.Entidades;
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

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para SeleccionarEntradasCondiciones.xaml
    /// </summary>
    public partial class SeleccionarEntradasCondiciones : Window
    {
        public List<DiseñoOperacion> Operandos { get; set; }
        public bool SeleccionManualEntradas { get; set; }
        public bool SeleccionCondicionesEntradas { get; set; }
        public DiseñoOperacion ElementoAsociado { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public SeleccionarEntradasCondiciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaCondiciones.VistaEntrada = this;
            listaCondiciones.ModoSeleccionEntradas = true;

            opcionSeccionAutomatica.IsChecked = SeleccionCondicionesEntradas;
            opcionSeccionManual.IsChecked = SeleccionManualEntradas;

            ListarOperandos();
            listaCondiciones.ListarCondiciones();
        }

        public void ListarOperandos()
        {
            List<DiseñoOperacion> OperandosAConfigurar = new List<DiseñoOperacion>();
            OperandosAConfigurar.AddRange(Operandos.Except(listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.Select(i => i.ElementoSalida_Operacion)));

            listaOperaciones.Children.Clear();

            foreach (var itemOperando in OperandosAConfigurar)
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

                Button botonAgregarModoManual = new Button();
                botonAgregarModoManual.Content = "Agregar a la selección de lógica manual";
                botonAgregarModoManual.Padding = new Thickness(5);
                botonAgregarModoManual.Tag = itemOperando;
                botonAgregarModoManual.Click += BotonAgregarModoManual_Click;

                operando.Children.Add(botonAgregarModoManual);
                Grid.SetColumn(botonAgregarModoManual, 1);

                listaOperaciones.Children.Add(operando);
            }

            listaCondiciones.Operandos = Operandos.Except(listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.Where(i => i.ModoManual).Select(i => i.ElementoSalida_Operacion)).ToList();
            listaCondiciones.ListaElementos = ListaElementos;
            listaCondiciones.DefinicionesListas = DefinicionesListas;
            listaCondiciones.ElementoAsociado = ElementoAsociado;

            List<DiseñoOperacion> Operandos_ModoManual = new List<DiseñoOperacion>();
            Operandos_ModoManual.AddRange(listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.Where(i => i.ModoManual).Select(i => i.ElementoSalida_Operacion));

            listaOperandos_ModoManual.Children.Clear();

            foreach (var itemOperando in Operandos_ModoManual)
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

                Button botonAgregarModoManual = new Button();
                botonAgregarModoManual.Content = "Quitar de la selección de lógica manual";
                botonAgregarModoManual.Padding = new Thickness(5);
                botonAgregarModoManual.Tag = itemOperando;
                botonAgregarModoManual.Click += BotonAgregarModoManual_Click1;

                operando.Children.Add(botonAgregarModoManual);
                Grid.SetColumn(botonAgregarModoManual, 1);

                listaOperandos_ModoManual.Children.Add(operando);
            }
        }

        private void BotonAgregarModoManual_Click1(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag != null)
            {
                var elemento = (DiseñoOperacion)((Button)sender).Tag;
                var reg = listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.FirstOrDefault(i => i.ModoManual &&
                i.ElementoSalida_Operacion == elemento);

                if (reg != null)
                {
                    listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.Remove(reg);
                    ListarOperandos();
                    listaCondiciones.ListarCondiciones();
                }
            }
        }

        private void BotonAgregarModoManual_Click(object sender, RoutedEventArgs e)
        {
            if(((Button)sender).Tag != null)
            {
                var elemento = (DiseñoOperacion)((Button)sender).Tag;

                if(!listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.Any(i => i.ModoManual &&
                i.ElementoSalida_Operacion == elemento))
                {
                    listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida.Add(new Entidades.Condiciones.AsociacionCondicionTextosInformacion_Entradas_ElementoSalida()
                    {
                        ElementoSalida_Operacion = elemento,
                        ModoManual = true
                    });

                    ListarOperandos();
                    listaCondiciones.ListarCondiciones();
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

        private void opcionSeccionManual_Checked(object sender, RoutedEventArgs e)
        {
            SeleccionManualEntradas = (bool)opcionSeccionManual.IsChecked;
        }

        private void opcionSeccionManual_Unchecked(object sender, RoutedEventArgs e)
        {
            SeleccionManualEntradas = (bool)opcionSeccionManual.IsChecked;
        }

        private void opcionSeccionAutomatica_Checked(object sender, RoutedEventArgs e)
        {
            SeleccionCondicionesEntradas = (bool)opcionSeccionAutomatica.IsChecked;
        }

        private void opcionSeccionAutomatica_Unchecked(object sender, RoutedEventArgs e)
        {
            SeleccionCondicionesEntradas = (bool)opcionSeccionAutomatica.IsChecked;
        }
    }
}
