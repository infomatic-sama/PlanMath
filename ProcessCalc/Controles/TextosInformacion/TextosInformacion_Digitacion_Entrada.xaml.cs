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
    /// Lógica de interacción para TextosInformacion_Digitacion_Entrada.xaml
    /// </summary>
    public partial class TextosInformacion_Digitacion_Entrada : UserControl
    {
        private TextosInformacion_Digitacion param;

        public TextosInformacion_Digitacion Texto
        {
            get
            {
                return param;
            }

            set
            {
                param = value;
                nombreParametro.Text = param.Nombre;
                textoParametro.Text = param.Texto;
            }
        }
        public TextosInformacion_Digitacion_Entrada()
        {
            InitializeComponent();
        }
        private void nombreParametro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (param != null)
                param.Nombre = nombreParametro.Text;
        }
        private void textoParametro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (param != null)
                param.Texto = textoParametro.Text;
        }

        private void opcionesPosicion_Click(object sender, RoutedEventArgs e)
        {
            if (param != null)
            {
                ConfigSeleccionPosicionesTextos_Entrada config = new ConfigSeleccionPosicionesTextos_Entrada();
                config.Definicion = param.SeleccionNumeros.CopiarObjeto();
                if (config.ShowDialog() == true)
                {
                    param.SeleccionNumeros = config.Definicion;
                }
            }
        }
    }
}
