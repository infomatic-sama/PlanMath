using ProcessCalc.Controles;
using ProcessCalc.Entidades;
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

namespace ProcessCalc.Ventanas.Entradas
{
    /// <summary>
    /// Lógica de interacción para AdmCantidadesDigitadas.xaml
    /// </summary>
    public partial class AdmCantidadesDigitadas : Window
    {
        public Calculo Calculo {  get; set; }
        Entrada entr;
        public Entrada EntradaSeleccionada
        {
            get
            { return entr; }
            set
            {
                entr = value;
                ListarCantidadesDigitadas();
            }
        }
        public AdmCantidadesDigitadas()
        {
            InitializeComponent();
        }

        public void ListarEntradas()
        {
            listaEntradas.Children.Clear();

            foreach (var itemCalculo in Calculo.Calculos.Where(i => !i.EsEntradasArchivo))
            {
                foreach (var itemEntrada in itemCalculo.ListaEntradas)
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                    nuevaEntrada.Entrada = itemEntrada;
                    nuevaEntrada.EnVentanaAdm_CantidadesDigitadas = true;
                    nuevaEntrada.AdministrarCantidadesDigitadas = this;
                    //nuevaEntrada.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                    listaEntradas.Children.Add(nuevaEntrada);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarEntradas();
        }

        private void ListarCantidadesDigitadas()
        {
            listaCantidadesDigitadas.Children.Clear();

            int indice = 0;
            foreach(var item in EntradaSeleccionada.CantidadesDigitadas.NumerosDigitados)
            {
                listaCantidadesDigitadas.RowDefinitions.Add(new RowDefinition());
                listaCantidadesDigitadas.RowDefinitions.Last().Height = GridLength.Auto;

                TextBlock numero = new TextBlock();
                numero.Margin = new Thickness(10);
                numero.Text = item.ToString();

                listaCantidadesDigitadas.Children.Add(numero);
                Grid.SetRow(numero, indice);
                Grid.SetColumn(numero, 0);

                Button quitar = new Button();
                quitar.Tag = item;
                quitar.Margin = new Thickness(10);
                quitar.Content = "Quitar";

                quitar.Click += Quitar_Click;

                listaCantidadesDigitadas.Children.Add(quitar);
                Grid.SetRow(quitar, indice);
                Grid.SetColumn(quitar, 1);

                indice++;
            }
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            var item = ((Button)sender).Tag;

            if(item != null)
            {
                EntradaSeleccionada.CantidadesDigitadas.QuitarCantidadDigitada(item.ToString());
                EntradaSeleccionada.CantidadesDigitadas.GuardarCantidadesDigitadas(Calculo.RutaArchivo, Calculo.ID, EntradaSeleccionada.ID);
                ListarCantidadesDigitadas();
            }
        }

        private void vaciarCantidadesDigitadas_Click(object sender, RoutedEventArgs e)
        {
            EntradaSeleccionada.CantidadesDigitadas.VaciarCantidadesDigitadas();
            EntradaSeleccionada.CantidadesDigitadas.GuardarCantidadesDigitadas(Calculo.RutaArchivo, Calculo.ID, EntradaSeleccionada.ID);
            ListarCantidadesDigitadas();
        }
    }
}
