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
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using ProcessCalc.Controles;
using System.Diagnostics;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para AperturaArchivoEspecifica.xaml
    /// </summary>
    public partial class AperturaArchivoEspecifica : UserControl
    {
        private AperturaArchivo aper;
        public AperturaArchivo Apertura
        {
            set
            {
                nombreArchivo.Text = value.NombreArchivo;
                descripcionCalculo.Text = value.DescripcionCalculo;
                fechaHoraApertura.Text = value.FechaHora.ToString();
                rutaArchivo.Text = value.RutaArchivo;
                aper = value;
            }

            get
            {
                return aper;
            }
        }
        public MainWindow Ventana { get; set; }
        public AperturaArchivoEspecifica()
        {
            InitializeComponent();
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            Ventana.QuitarArchivoReciente(Apertura);
        }

        private void nombreArchivo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Ventana.AbrirArchivo(Apertura.RutaArchivo);
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = aper.RutaArchivo.Substring(0, aper.RutaArchivo.LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
