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
    /// Lógica de interacción para SeleccionarOrigenDatos.xaml
    /// </summary>
    public partial class SeleccionarOrigenDatos : Window
    {
        public TipoOrigenDatos TipoOrigenDatos { get; set; }
        public TipoFormatoArchivoEntrada TipoFormatoArchivo { get; set; }

        public SeleccionarOrigenDatos()
        {
            TipoOrigenDatos = TipoOrigenDatos.Ninguno;
            TipoFormatoArchivo = TipoFormatoArchivoEntrada.Ninguno;
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnArchivo_Click(object sender, RoutedEventArgs e)
        {
            TipoOrigenDatos = TipoOrigenDatos.Archivo;
            TipoFormatoArchivo = TipoFormatoArchivoEntrada.ArchivoTextoPlano;
            Close();
        }

        private void btnArchivoPantalla_Click(object sender, RoutedEventArgs e)
        {
            TipoOrigenDatos = TipoOrigenDatos.Archivo;
            TipoFormatoArchivo = TipoFormatoArchivoEntrada.TextoPantalla;
            Close();
        }

        private void btnURL_Click(object sender, RoutedEventArgs e)
        {
            TipoOrigenDatos = TipoOrigenDatos.DesdeInternet;
            Close();
        }

        private void btnArchivoExcel_Click(object sender, RoutedEventArgs e)
        {
            TipoOrigenDatos = TipoOrigenDatos.Excel;
            Close();
        }

        private void btnArchivoWord_Click(object sender, RoutedEventArgs e)
        {
            TipoOrigenDatos = TipoOrigenDatos.Archivo;
            TipoFormatoArchivo = TipoFormatoArchivoEntrada.Word;
            Close();
        }

        private void btnArchivoPDF_Click(object sender, RoutedEventArgs e)
        {
            TipoOrigenDatos = TipoOrigenDatos.Archivo;
            TipoFormatoArchivo = TipoFormatoArchivoEntrada.PDF;
            Close();
        }
    }
}
