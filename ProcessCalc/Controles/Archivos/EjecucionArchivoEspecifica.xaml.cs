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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para EjecucionArchivoEspecifica.xaml
    /// </summary>
    public partial class EjecucionArchivoEspecifica : UserControl
    {
        private EjecucionArchivo ejec;
        public EjecucionArchivo Ejecucion
        {
            set
            {
                
                descripcionCalculo.Text = value.DescripcionCalculo;
                fechaHoraEjecucion.Text = value.FechaHora.ToString();
                nombreArchivo.Text = value.NombreArchivo;
                rutaArchivo.Text = value.RutaArchivo;
                ejec = value;
            }

            get
            {
                return ejec;
            }
        }
        public MainWindow Ventana { get; set; }
        public EjecucionArchivoEspecifica()
        {
            InitializeComponent();
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            Ventana.QuitarEjecucionReciente(Ejecucion);
        }

        private void verResultados_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Ventana.VerResultadosEjecucionArchivo(Ejecucion.Ejecucion);
        }
    }
}
