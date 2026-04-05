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

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para DefinirOperacion_TextosInformacion.xaml
    /// </summary>
    public partial class DefinirOperacion_TextosInformacion : Window
    {
        public Calculo Calculo { get; set; }
        public DiseñoOperacion OperacionRelacionada { get; set; }
        public DiseñoCalculo CalculoRelacionadoSeleccionado { get; set; }
        public DefinirOperacion_TextosInformacion()
        {
            InitializeComponent();
        }

        private void ListarCalculos()
        {
            calculo.ItemsSource = Calculo.Calculos.Where(i => !i.EsEntradasArchivo);
        }

        private void ListarOperaciones(DiseñoCalculo calculoSelecc)
        {
            operacion.ItemsSource = (from O in calculoSelecc.ElementosOperaciones where O.Tipo != TipoElementoOperacion.Nota &
                                    O.Tipo != TipoElementoOperacion.Entrada &
                                    O.Tipo != TipoElementoOperacion.Linea &
                                    O.Tipo != TipoElementoOperacion.Salida select O).ToList();
        }
        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void calculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                CalculoRelacionadoSeleccionado = (DiseñoCalculo)((ComboBox)sender).SelectedItem;
                ListarOperaciones(CalculoRelacionadoSeleccionado);
            }
            else
            {
                CalculoRelacionadoSeleccionado = null;
                operacion.ItemsSource = null;
            }
        }

        private void operacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
                OperacionRelacionada = (DiseñoOperacion)((ComboBox)sender).SelectedItem;
            else
                OperacionRelacionada = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCalculos();
            calculo.SelectedItem = CalculoRelacionadoSeleccionado;
            operacion.SelectedItem = OperacionRelacionada;
        }
    }
}
