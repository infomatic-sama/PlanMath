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
    /// Lógica de interacción para DefinirRelaciones_TextosInformacion.xaml
    /// </summary>
    public partial class DefinirRelaciones_TextosInformacion : Window
    {
        public List<DiseñoOperacion> Elementos { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public DiseñoOperacion OperacionRelacionada_Definicion { get; set; }
        public DiseñoCalculo CalculoAsociado { get; set; }
        public DefinirRelaciones_TextosInformacion()
        {
            InitializeComponent();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ListaOperandos = new List<DiseñoOperacion>();
            ListaOperandos.AddRange(Operandos);
            ListaOperandos.Add(OperacionRelacionada_Definicion);

            listaTextos.Elementos = Elementos;
            listaTextos.Operandos = ListaOperandos;
            listaTextos.CalculoAsociado = CalculoAsociado;
            listaTextos.SubOperandos = SubOperandos;
            listaTextos.OperacionRelacionada_Definicion = OperacionRelacionada_Definicion;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; 
            Close();
        }
    }
}
