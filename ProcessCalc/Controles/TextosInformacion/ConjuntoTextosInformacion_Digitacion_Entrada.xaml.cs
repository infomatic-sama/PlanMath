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
    /// Lógica de interacción para ConjuntoTextosInformacion_Digitacion_Entrada.xaml
    /// </summary>
    public partial class ConjuntoTextosInformacion_Digitacion_Entrada : UserControl
    {
        private ConjuntoTextosInformacion_Digitacion param;

        public ConjuntoTextosInformacion_Digitacion Conjunto
        {
            get
            {
                return param;
            }

            set
            {
                param = value;
                nombreParametro.Text = param.Nombre;

                if (ConTextosInformacion_EntradasAnteriores &&
                ListaEntradasAnteriores.Any())
                {                   
                    opcionesEntradasAnteriores.DisplayMemberPath = "NombreCombo";
                    opcionesEntradasAnteriores.ItemsSource = ListaEntradasAnteriores;
                }

                if (ConTextosInformacion_EntradasAnteriores)
                {
                    opcionesEntradasAnteriores.SelectedItem = param.EntradaAnterior_TextosInformacion_Predefinidos;
                }
            }
        }
        public bool ConTextosInformacion_EntradasAnteriores { get; set; }
        public List<Entrada> ListaEntradasAnteriores { get; set; }
        public ConjuntoTextosInformacion_Digitacion_Entrada()
        {
            InitializeComponent();
        }

        private void nombreParametro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (param != null)
                param.Nombre = nombreParametro.Text;
        }

        private void textosInformacionPredefinidos_Click(object sender, RoutedEventArgs e)
        {
            if (param != null)
            {
                EstablecerTextosInformacionPredefinidos_Digitacion establecer = new EstablecerTextosInformacionPredefinidos_Digitacion();
                establecer.Conjunto = param;
                establecer.ShowDialog();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ConTextosInformacion_EntradasAnteriores &&
                ListaEntradasAnteriores.Any())
            {
                opcionesTextosInformacion_EntradasAnteriores.Visibility = Visibility.Visible;
            }
        }

        private void opcionesEntradasAnteriores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(opcionesEntradasAnteriores.SelectedItem != null)
            {
                if(param != null)
                {
                    param.ConTextosInformacion_EntradasAnteriores = true;
                    param.EntradaAnterior_TextosInformacion_Predefinidos = (Entrada)opcionesEntradasAnteriores.SelectedItem;
                }
            }
        }

        private void quitarSeleccionEntradaAnterior_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionesEntradasAnteriores.SelectedItem = null;
            if (param != null)
            {
                param.ConTextosInformacion_EntradasAnteriores = false;
                param.EntradaAnterior_TextosInformacion_Predefinidos = null;
            }
        }
    }
}
