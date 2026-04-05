using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaResultados.xaml
    /// </summary>
    public partial class VistaResultados : UserControl
    {
        private Calculo calc;
        public Calculo Calculo
        {
            set
            {
                nombreArchivo.Content = value.NombreArchivo;
                rutaArchivo.Content = value.RutaArchivo;

                calc = value;

                ListarResultados();
            }
            get
            {
                return calc;
            }
        }
        public VistaResultados()
        {
            InitializeComponent();
        }

        public void ListarResultados()
        {
            resultados.Children.Clear();

            foreach (var itemResultado in calc.ListaResultados)
            {
                ResultadoEspecifico nuevoResultado = new ResultadoEspecifico();
                nuevoResultado.Resultado = itemResultado;
                resultados.Children.Add(nuevoResultado);
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(Calculo != null &&
                Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("VerConfigurarResultadosCalculo");
#endif
        }
    }
}
