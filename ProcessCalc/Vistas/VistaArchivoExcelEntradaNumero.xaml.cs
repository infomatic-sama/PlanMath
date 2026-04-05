using Microsoft.Win32;
using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Ventanas;
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
    /// Lógica de interacción para VistaArchivoExcelEntradaNumero.xaml
    /// </summary>
    public partial class VistaArchivoExcelEntradaNumero : UserControl
    {
        private Entrada entr;
        public Entrada Entrada
        {
            get
            {
                return entr;
            }

            set
            {
                entr = value;
                MostrarTextoArchivoCarpetaSeleccionada();

                if (value.ParametrosExcel.Any())
                {
                    hoja.Text = value.ParametrosExcel.First().Hoja;
                    celda.Text = value.ParametrosExcel.First().Celdas;
                }

                lblNombreEntrada.Content = value.Nombre;

                opcionURLLibro.IsChecked = entr.UsarURL_Office;

                if (entr.ParametrosExcel.Any())
                {
                    opcionEntradaManual.IsChecked = entr.ParametrosExcel.FirstOrDefault().EntradaManual;
                }

                MostrarOcultarElementosVisuales_EntradaManual(entr.EntradaArchivoManual);

                entr = value;

                if (entr.EntradaArchivoManual)
                    opcionEntradaManual.IsEnabled = false;
            }
        }
        public EntradaEspecifica VistaEntrada { get; set; }
        public Calculo Calculo { get; set; }
        public VistaArchivoExcelEntradaNumero()
        {
            InitializeComponent();
        }

        private void btnCambiarArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                seleccionar.Entrada = entr;
                seleccionar.VistaEntrada = VistaEntrada;
                seleccionar.ArchivoEntrada = entr.ListaArchivos.FirstOrDefault();
                seleccionar.RutaCarpeta = Calculo.RutaArchivo == null ? string.Empty : Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));
                seleccionar.ShowDialog();

                MostrarTextoArchivoCarpetaSeleccionada();
            }
        }

        private void MostrarTextoArchivoCarpetaSeleccionada()
        {
            string nombreArchivoCarpeta = string.Empty;

            if (entr.Tipo == TipoEntrada.Numero)
                nombreArchivoCarpeta = entr.ListaArchivos.FirstOrDefault().RutaArchivoEntrada;
            else if (entr.Tipo == TipoEntrada.ConjuntoNumeros)
                nombreArchivoCarpeta = entr.ListaArchivos.FirstOrDefault().RutaArchivoConjuntoNumerosEntrada;

            switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionCarpeta)
            {
                case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                    switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se obtendrá la variable de número de la entrada, del archivo " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se obtendrá la variable de número de la entrada, del archivo seleccionado en la carpeta " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                            lblRutaArchivo.Content = "Para algunas variables o vectores de entrada, se obtendrá la variable de número de la entrada, del archivo " + nombreArchivoCarpeta;
                            break;
                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se obtendrá la variable de número de la entrada, del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se obtendrá la variable de número de la entrada, del archivo seleccionado en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                            lblRutaArchivo.Content = "Para algunas variables o vectores de entrada, se obtendrá la variable de número de la entrada, del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;
                    }

                    break;
            }
        }

        private void hoja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entr != null && entr.ParametrosExcel != null && entr.ParametrosExcel.Count > 0)
                entr.ParametrosExcel.First().Hoja = hoja.Text;
        }

        private void celda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entr != null && entr.ParametrosExcel != null && entr.ParametrosExcel.Count > 0)
                entr.ParametrosExcel.First().Celdas = celda.Text;
        }

        private void MostrarTextoConfiguracion_SeleccionArchivoCarpeta()
        {
            if (entr != null)
            {
                string strCarpeta = string.Empty;
                string strArchivo = string.Empty;

                switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                        strCarpeta = "Buscar en la carpeta indicada.";
                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                        strCarpeta = "Buscar en la carpeta en que está el archivo de cálculo en el momento de la ejecución.";
                        break;
                }

                switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                {
                    case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                        strArchivo = "Utilizar el archivo indicado.";
                        break;

                    case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                        strArchivo = "Seleccionar el archivo, en el momento de la ejecución del archivo de cálculo.";
                        break;

                    case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                        strArchivo = "Seleccionar el archivo, según lo indique la variable o vector de entrada en las variables o vectores, en el momento de la ejecución del archivo de cálculo.";
                        break;
                }

                etiquetaDefinicionSeleccionArchivoCarpeta.Text = "Definición actual: " + strArchivo + " " + strCarpeta;
            }
            else
                etiquetaDefinicionSeleccionArchivoCarpeta.Text = "";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
        }

        private void configurarSeleccionCarpetaArchivo_Click(object sender, RoutedEventArgs e)
        {
            ConfigCarpetaArchivo configurar = new ConfigCarpetaArchivo();
            configurar.Entrada = entr;
            configurar.ArchivoEntrada = entr.ListaArchivos.FirstOrDefault();
            configurar.ShowDialog();
            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
            MostrarTextoArchivoCarpetaSeleccionada();
        }

        private void opcionURLLibro_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UsarURL_Office = (bool)opcionURLLibro.IsChecked;
            }
        }

        private void opcionURLLibro_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UsarURL_Office = (bool)opcionURLLibro.IsChecked;
            }
        }

        private void btnDefinirTextosInformacionFijos_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
            seleccionarOrdenar.listaTextos.TextosInformacion = Entrada.TextosInformacionFijos.ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                Entrada.TextosInformacionFijos = seleccionarOrdenar.listaTextos.TextosInformacion;
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void opcionEntradaManual_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                if (entr.ParametrosExcel.Any())
                {
                    foreach (var item in entr.ParametrosExcel)
                    {
                        item.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                    }
                }

                MostrarOcultarElementosVisuales_EntradaManual((bool)opcionEntradaManual.IsChecked);

            }
        }

        private void opcionEntradaManual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                if (entr.ParametrosExcel.Any())
                {
                    foreach (var item in entr.ParametrosExcel)
                    {
                        item.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                    }
                }

                MostrarOcultarElementosVisuales_EntradaManual((bool)opcionEntradaManual.IsChecked);
            }
        }

        private void MostrarOcultarElementosVisuales_EntradaManual(bool entradaManual)
        {
            if (entradaManual)
            {
                areaCelda.Visibility = Visibility.Collapsed;
            }
            else
            {
                areaCelda.Visibility = Visibility.Visible;
            }
        }
    }
}
