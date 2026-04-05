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

namespace ProcessCalc.Ventanas.Seleccionar
{
    /// <summary>
    /// Lógica de interacción para SeleccionarCantidadesOperando.xaml
    /// </summary>
    public partial class SeleccionarCantidadesOperando : Window
    {
        public bool Pausado { get; set; }
        public List<EntidadNumero> ListaNumerosOriginal {  get; set; }
        public List<EntidadNumero> ListaNumerosSeleccionados { get; set; }
        public Calculo CalculoRelacionado { get; set; }
        public SeleccionarCantidadesOperando()
        {
            InitializeComponent();
        }

        public void ListarNumerosOriginales()
        {
            listaNumeros.Children.Clear();

            foreach(var itemNumero in ListaNumerosOriginal)
            {
                AgregarNumeroCantidad(itemNumero);
            }
        }

        private void AgregarNumeroCantidad(EntidadNumero numero)
        {
            Grid item = new Grid();
            RowDefinition fila = new RowDefinition();
            fila.Height = GridLength.Auto;

            item.RowDefinitions.Add(fila);

            ColumnDefinition columna = new ColumnDefinition();
            columna.Width = GridLength.Auto;

            item.ColumnDefinitions.Add(columna);

            ColumnDefinition columna2 = new ColumnDefinition();
            columna2.Width = GridLength.Auto;

            item.ColumnDefinitions.Add(columna2);

            ColumnDefinition columna3 = new ColumnDefinition();
            columna3.Width = GridLength.Auto;

            item.ColumnDefinitions.Add(columna3);

            TextBlock texto = new TextBlock();
            texto.Margin = new Thickness(10);

            if (numero != null)
            {
                texto.Text = numero.Numero.ToString("N" + CalculoRelacionado.CantidadDecimalesCantidades);
            }

            item.Children.Add(texto);
            Grid.SetRow(texto, 0);
            Grid.SetColumn(texto, 0);

            TextBlock textosInformacion = new TextBlock();
            textosInformacion.Margin = new Thickness(10);

            if (numero != null)
            {
                textosInformacion.Text = GenerarCadenaTextosInformacion(numero.Textos);
            }
            
            item.Children.Add(textosInformacion);
            Grid.SetRow(textosInformacion, 0);
            Grid.SetColumn(textosInformacion, 1);

            Button botonSeleccionar = new Button();
            botonSeleccionar.Margin = new Thickness(10);
            botonSeleccionar.Content = "Seleccionar";

            if(numero != null)
            {
                botonSeleccionar.Tag = numero;
            }

            botonSeleccionar.Click += BotonSeleccionar_Click;

            item.Children.Add(botonSeleccionar);
            Grid.SetRow(botonSeleccionar, 0);
            Grid.SetColumn(botonSeleccionar, 2);

            listaNumeros.Children.Add(item);
        }

        private void BotonSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            var item = ((Button)sender).Tag;

            if (item != null)
            {
                if(item.GetType() == typeof(EntidadNumero))
                {
                    ListaNumerosSeleccionados.Add((EntidadNumero)item);
                    ListarNumerosSeleccionados();

                    ListaNumerosOriginal.Remove((EntidadNumero)item);
                    ListarNumerosOriginales();
                }
            }
        }

        private string GenerarCadenaTextosInformacion(List<string> Textos)
        {
            string cadena = string.Empty;
            foreach(var texto in Textos)
            {
                cadena += texto;
                if (texto != Textos.Last())
                    cadena += ", ";
            }

            return cadena;
        }

        public void ListarNumerosSeleccionados()
        {
            listaNumerosSeleccinados.Children.Clear();

            foreach (var itemNumero in ListaNumerosSeleccionados)
            {
                AgregarNumeroCantidad_Seleccionado(itemNumero);
            }
        }

        private void AgregarNumeroCantidad_Seleccionado(EntidadNumero numero)
        {
            Grid item = new Grid();
            RowDefinition fila = new RowDefinition();
            fila.Height = GridLength.Auto;

            item.RowDefinitions.Add(fila);

            ColumnDefinition columna = new ColumnDefinition();
            columna.Width = GridLength.Auto;

            item.ColumnDefinitions.Add(columna);

            ColumnDefinition columna2 = new ColumnDefinition();
            columna2.Width = GridLength.Auto;

            item.ColumnDefinitions.Add(columna2);

            ColumnDefinition columna3 = new ColumnDefinition();
            columna3.Width = GridLength.Auto;

            item.ColumnDefinitions.Add(columna3);

            TextBlock texto = new TextBlock();
            texto.Margin = new Thickness(10);

            if (numero != null)
            {
                texto.Text = numero.Numero.ToString("N" + CalculoRelacionado.CantidadDecimalesCantidades);
            }

            item.Children.Add(texto);
            Grid.SetRow(texto, 0);
            Grid.SetColumn(texto, 0);

            TextBlock textosInformacion = new TextBlock();
            textosInformacion.Margin = new Thickness(10);

            if (numero != null)
            {
                textosInformacion.Text = GenerarCadenaTextosInformacion(numero.Textos);
            }

            item.Children.Add(textosInformacion);
            Grid.SetRow(textosInformacion, 0);
            Grid.SetColumn(textosInformacion, 1);

            Button botonQuitar = new Button();
            botonQuitar.Margin = new Thickness(10);
            botonQuitar.Content = "Quitar";

            if (numero != null)
            {
                botonQuitar.Tag = numero;
            }

            botonQuitar.Click += BotonQuitar_Click; ;

            item.Children.Add(botonQuitar);
            Grid.SetRow(botonQuitar, 0);
            Grid.SetColumn(botonQuitar, 2);

            listaNumerosSeleccinados.Children.Add(item);
        }

        private void BotonQuitar_Click(object sender, RoutedEventArgs e)
        {
            var item = ((Button)sender).Tag;

            if (item != null)
            {
                if (item.GetType() == typeof(EntidadNumero))
                {
                    ListaNumerosSeleccionados.Remove((EntidadNumero)item);
                    ListarNumerosSeleccionados();

                    ListaNumerosOriginal.Add((EntidadNumero)item);
                    ListarNumerosOriginales();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListaNumerosSeleccionados = new List<EntidadNumero>();
            ListarNumerosOriginales();
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Pausado = true;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
