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
using static PlanMath_para_Word.Entradas;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VerTextosBusquedaEntrada_DocumentoWord.xaml
    /// </summary>
    public partial class VerTextosBusquedaEntrada_DocumentoWord : Window
    {
        public List<TextoBusqueda_DocumentoWord> TextosBusqueda {  get; set; }
        public VerTextosBusquedaEntrada_DocumentoWord()
        {
            InitializeComponent();
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (TextosBusqueda != null)
            {
                ListarTextos();
            }
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            var item = (TextoBusqueda_DocumentoWord)((Button)sender).Tag;

            if (item != null)
            {
                TextosBusqueda.Remove(item);
                ListarTextos();
            }
        }

        private void ListarTextos()
        {
            textos.Children.Clear();

            foreach (var itemTexto in TextosBusqueda)
            {
                Grid grilla = new Grid();
                grilla.ColumnDefinitions.Add(new ColumnDefinition());
                grilla.ColumnDefinitions.Last().Width = GridLength.Auto;

                grilla.ColumnDefinitions.Add(new ColumnDefinition());
                grilla.ColumnDefinitions.Last().Width = GridLength.Auto;

                grilla.RowDefinitions.Add(new RowDefinition());
                grilla.RowDefinitions.Last().Height = GridLength.Auto;

                TextBlock texto = new TextBlock();
                texto.Margin = new Thickness(10);
                texto.Padding = new Thickness(5);
                texto.Text = "Número de página: " + itemTexto.NumeroPagina.ToString() + " " +
                    "Número de Fila: " + itemTexto.NumeroFila.ToString() + " " +
                    "Posición letra inicial texto: " + itemTexto.CaracterInicial.ToString() + " " +
                    "Posición letra final texto: " + itemTexto.CaracterFinal.ToString();
                grilla.Children.Add(texto);

                Grid.SetColumn(texto, 0);
                Grid.SetRow(texto, 0);

                Button quitar = new Button();
                quitar.Margin = new Thickness(10);
                quitar.Content = "Quitar";
                quitar.Tag = itemTexto;

                quitar.Click += Quitar_Click;

                grilla.Children.Add(quitar);
                Grid.SetColumn(quitar, 1);
                Grid.SetRow(quitar, 0);

                textos.Children.Add(grilla);
            }
        }
    }
}
