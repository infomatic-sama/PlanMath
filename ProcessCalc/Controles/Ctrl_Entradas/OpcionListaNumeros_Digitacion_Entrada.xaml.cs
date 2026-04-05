using ProcessCalc.Entidades.Entradas;
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

namespace ProcessCalc.Controles.Ctrl_Entradas
{
    /// <summary>
    /// Lógica de interacción para OpcionListaNumeros_Digitacion_Entrada.xaml
    /// </summary>
    public partial class OpcionListaNumeros_Digitacion_Entrada : UserControl
    {
        private OpcionListaNumeros_Digitacion param;

        public OpcionListaNumeros_Digitacion Numero
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
        public OpcionListaNumeros_Digitacion_Entrada()
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
    }
}
