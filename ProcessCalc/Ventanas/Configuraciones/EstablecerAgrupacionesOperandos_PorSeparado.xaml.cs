using Microsoft.VisualBasic;
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
    /// Lógica de interacción para EstablecerAgrupacionesOperandos_PorSeparado.xaml
    /// </summary>
    public partial class EstablecerAgrupacionesOperandos_PorSeparado : Window
    {
        public DiseñoOperacion OperacionRelacionada { get; set; }
        public DiseñoOperacion Operando {  get; set; }
        public string NombreAgrupacion {  get; set; }
        public EstablecerAgrupacionesOperandos_PorSeparado()
        {
            InitializeComponent();
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(agrupaciones.Text))
            {
                MessageBox.Show("Especifica una agrupación.", "Agrupación seleccionada", MessageBoxButton.OK);
            }
            else
            {
                NombreAgrupacion = agrupaciones.Text;
                DialogResult = true;
                Close();
            }
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void agregarAgrupacion_Click(object sender, RoutedEventArgs e)
        {
            string agrupacion = Interaction.InputBox("Nombre de la agrupación:", "Nueva agrupación");

            if (!string.IsNullOrEmpty(agrupacion))
            {
                agrupaciones.Items.Add(agrupacion);
                agrupaciones.SelectedIndex = agrupaciones.Items.Count - 1;
            }                   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var listaAgrupaciones = OperacionRelacionada.ElementosAnteriores.Select(i => i.AgrupacionesOperandos_PorSeparado.Where(l => l.OperacionRelacionada == OperacionRelacionada)
            .Select(j => j.NombreAgrupacion)).SelectMany(m => m).Distinct().ToList();

            foreach(var item in listaAgrupaciones)
            {
                agrupaciones.Items.Add(item);
            }

            var agrupacionRelacionada = Operando.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == OperacionRelacionada).Select(j => j.NombreAgrupacion).FirstOrDefault();

            if (!string.IsNullOrEmpty(agrupacionRelacionada))
            {
                agrupaciones.Text = agrupacionRelacionada;
            }
        }

        private void quitar_Click(object sender, RoutedEventArgs e)
        {
            NombreAgrupacion = null;
            DialogResult = true;
            Close();
        }
    }
}
