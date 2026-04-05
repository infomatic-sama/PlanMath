using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para OpcionDefiniciones_TextosInformacion.xaml
    /// </summary>
    public partial class OpcionDefiniciones_TextosInformacion : UserControl
    {
        public AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion AsociacionTextosInformacionOperando_Implicacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public DiseñoCalculo CalculoAsociado { get; set; }
        public OpcionDefiniciones_TextosInformacion()
        {
            InitializeComponent();
        }

        private void mostrarListaDefiniciones_Click(object sender, RoutedEventArgs e)
        {
            if(AsociacionTextosInformacionOperando_Implicacion != null &&
                AsociacionTextosInformacionOperando_Implicacion.Definiciones != null)
            {
                ListaDefinicionesTextos_TextosInformacion lista = new ListaDefinicionesTextos_TextosInformacion();
                lista.Operandos = Operandos;
                lista.CalculoAsociado = CalculoAsociado;
                lista.SubOperandos = SubOperandos;
                lista.Definiciones = AsociacionTextosInformacionOperando_Implicacion.Definiciones;
                lista.ModoDefinicionesImplicaciones = true;
                lista.OcultarOpcionesDefiniciones = true;
                lista.ShowDialog();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (AsociacionTextosInformacionOperando_Implicacion != null &&
                AsociacionTextosInformacionOperando_Implicacion.Definiciones != null)
            {
                ListaDefinicionesTextos_TextosInformacion lista = new ListaDefinicionesTextos_TextosInformacion();
                lista.Operandos = Operandos;
                lista.SubOperandos = SubOperandos;
                lista.Definiciones = AsociacionTextosInformacionOperando_Implicacion.Definiciones;
                lista.CalculoAsociado = CalculoAsociado;
                lista.ModoDefinicionesImplicaciones = true;
                lista.OcultarOpcionesDefiniciones = true;

                descripcionDefiniciones.Text = string.Empty;

                foreach(var itemDefinicion in lista.Definiciones)
                    descripcionDefiniciones.Text += lista.GenerarTextoDefinicion(itemDefinicion) + " ";
            }
        }
    }
}
