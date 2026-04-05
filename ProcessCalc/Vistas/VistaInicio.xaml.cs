using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para VistaInicio.xaml
    /// </summary>
    public partial class VistaInicio : UserControl
    {
        public MainWindow Ventana { get; set; }
        bool BusquedaArchivosAbiertos;
        bool BusquedaEjecucionesRecientes;
        public VistaInicio()
        {
            InitializeComponent();
        }

        private void btnVaciarArchivosRecientes_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(App.RutaArchivo_ArchivoRecientes))
            {
                File.Delete(App.RutaArchivo_ArchivoRecientes);
                App.ArchivosRecientes.Historial.Clear();
                Ventana.ListarArchivosRecientes(BusquedaArchivosAbiertos ? textoBusquedaArchivosAbiertos.Text : null);
                UserControl_Loaded(this, e);
            }
        }

        private void btnVaciarEjecucionesRecientes_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(App.RutaArchivo_EjecucionesRecientes))
            {
                File.Delete(App.RutaArchivo_EjecucionesRecientes);
                App.EjecucionesRecientes.Historial.Clear();
                Ventana.ListarEjecucionesRecientes(BusquedaEjecucionesRecientes ? textoBusquedaEjecucionesRecientes.Text : null);
                UserControl_Loaded(this, e);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(App.RutaArchivo_ArchivoRecientes))
            {
                FileInfo archivoArchivosRecientes = new FileInfo(App.RutaArchivo_ArchivoRecientes);
                textoArchivosAbiertos.Text = "Archivos abiertos recientemente (" + ObtenerTamaño(archivoArchivosRecientes.Length) + ")";
            }
            else
                textoArchivosAbiertos.Text = "Archivos abiertos recientemente";

            if (File.Exists(App.RutaArchivo_EjecucionesRecientes))
            {
                FileInfo archivoEjecucionesRecientes = new FileInfo(App.RutaArchivo_EjecucionesRecientes);
                textoEjecucionesRecientes.Text = "Ejecuciones de cálculos recientes (" + ObtenerTamaño(archivoEjecucionesRecientes.Length) + ")";
            }
            else
                textoEjecucionesRecientes.Text = "Ejecuciones de cálculos recientes";
        }

        private string ObtenerTamaño(long tamaño)
        {
            if (tamaño > 1024)
            {
                long tamañoKiloBytes = tamaño / 1024;
                if(tamañoKiloBytes > 1024)
                {
                    long tamañoMegaBytes = tamañoKiloBytes / 1024;
                    if (tamañoMegaBytes > 1024)
                    {
                        long tamañoGigaBytes = tamañoMegaBytes / 1024;
                        if (tamañoGigaBytes > 1024)
                        {
                            long tamañoTeraBytes = tamañoGigaBytes / 1024;
                            return tamañoTeraBytes.ToString() + " TB";
                        }
                        else
                            return tamañoGigaBytes.ToString() + " GB";
                    }
                    else
                        return tamañoMegaBytes.ToString() + " MB";
                }
                else
                    return tamañoKiloBytes.ToString() + " KB";
            }
            else
                return tamaño.ToString() + " bytes";
        }

        private void buscarArchivosAbiertos_Click(object sender, RoutedEventArgs e)
        {
            if(BusquedaArchivosAbiertos)
            {
                BusquedaArchivosAbiertos = false;
                Ventana.ListarArchivosRecientes(BusquedaArchivosAbiertos ? textoBusquedaArchivosAbiertos.Text : null);
                UserControl_Loaded(this, e);
                buscarArchivosAbiertos.Content = "Buscar";
            }
            else
            {
                BusquedaArchivosAbiertos = true;
                Ventana.ListarArchivosRecientes(BusquedaArchivosAbiertos ? textoBusquedaArchivosAbiertos.Text : null);
                UserControl_Loaded(this, e);
                buscarArchivosAbiertos.Content = "Cerrar búsqueda";
            }
        }

        private void buscarEjecucionesRecientes_Click(object sender, RoutedEventArgs e)
        {
            if (BusquedaEjecucionesRecientes)
            {
                BusquedaEjecucionesRecientes = false;
                Ventana.ListarEjecucionesRecientes(BusquedaEjecucionesRecientes ? textoBusquedaEjecucionesRecientes.Text : null);
                UserControl_Loaded(this, e);
                buscarEjecucionesRecientes.Content = "Buscar";
            }
            else
            {
                BusquedaEjecucionesRecientes = true;
                Ventana.ListarEjecucionesRecientes(BusquedaEjecucionesRecientes ? textoBusquedaEjecucionesRecientes.Text : null);
                UserControl_Loaded(this, e);
                buscarEjecucionesRecientes.Content = "Cerrar búsqueda";
            }
        }
    }
}
