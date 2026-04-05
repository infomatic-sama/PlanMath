using Microsoft.Win32;
using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.OrigenesDatos;
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
using Windows.AI.MachineLearning;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaArchivoEntrada.xaml
    /// </summary>
    public partial class VistaArchivoEntradaNumero : UserControl
    {
        private Entrada entr;
        public Entrada Entrada 
        {
            get {
                return entr;
            }

            set
            {
                entr = value;
                MostrarTextoArchivoCarpetaSeleccionada();

                if (value.OpcionBusquedaNumero == OpcionBusquedaNumero.BusquedaTexto)
                {
                    opcionBuscarTexto.IsChecked = true;
                    opcionBuscarTexto_Click(this, new RoutedEventArgs());
                }
                else if (value.OpcionBusquedaNumero == OpcionBusquedaNumero.BusquedaTextoNVeces)
                {
                    opcionBuscarTextoNveces.IsChecked = true;
                    opcionBuscarTextoNveces_Click(this, new RoutedEventArgs());
                }
                if (value.BusquedaNumero.TextoBusquedaNumero != null)
                {
                    //busquedaNumero.textoArchivo.Text = value.TextoBusquedaNumero;
                    busquedaNumero.textoArchivo.Text = value.BusquedaNumero.TextoBusquedaNumero.Replace(busquedaNumero.ObtenerCadenaFormatoNumeroGuardar(), busquedaNumero.ObtenerCadenaFormatoNumero());
                    busquedaNumero.textoArchivo.Text = busquedaNumero.textoArchivo.Text.Replace(busquedaNumero.ObtenerCadenaFormatoDatosGuardar(), busquedaNumero.ObtenerCadenaFormatoNumero());
                    busquedaNumero.textoArchivo.Text = busquedaNumero.textoArchivo.Text.Replace(busquedaNumero.ObtenerCadenaFormatoTextosGuardar(), busquedaNumero.ObtenerCadenaFormatoNumero());
                }

                busquedaNumero.txtVeces.Text = value.BusquedaNumero.NumeroVecesBusquedaNumero.ToString();
                busquedaNumero.opcionesTextosInformacion.Visibility = Visibility.Collapsed;
                busquedaNumero.opcionUsarCeros.IsChecked = value.BusquedaNumero.UsarCantidad_SiNohayNumeros;
                busquedaNumero.txtCantidadUtilizar_NoEncontrados.Text = value.BusquedaNumero.NumeroUtilizar_NoEncontrados.ToString();

                tipoArchivo.SelectedItem = (from ComboBoxItem I in tipoArchivo.Items where I.Uid == ((int)value.ListaArchivos.FirstOrDefault().TipoArchivo).ToString() select I).FirstOrDefault();
                if (value.ListaArchivos.FirstOrDefault().TipoArchivo == TipoArchivo.ServidorFTP |
                    value.ListaArchivos.FirstOrDefault().TipoArchivo == TipoArchivo.Internet)
                    rutaFTP.Text = value.ListaArchivos.FirstOrDefault().RutaArchivoEntrada;

                opcionEsperarArchivo.IsChecked = value.ListaArchivos.First().EsperarArchivos;
                opcionCantidadTiempo.SelectedItem = (from ComboBoxItem I in opcionCantidadTiempo.Items where I.Uid == ((int)value.ListaArchivos.First().TipoTiempoEspera).ToString() select I).FirstOrDefault();
                cantidadTiempo.Text = value.ListaArchivos.First().TiempoEspera.ToString();

                //busquedaNumero.opcionAsignarTextosInformacionNumeros.SelectedItem = (from ComboBoxItem I in busquedaNumero.opcionAsignarTextosInformacionNumeros.Items where I.Uid == ((int)value.BusquedaNumero.OpcionAsignarTextosInformacion_Numeros).ToString() select I).FirstOrDefault();
                //busquedaNumero.cantidadNumerosAsignarTextosInformacion.Text = value.BusquedaNumero.CantidadNumeros_TextosInformacion_AsignarNumeros.ToString();
                //busquedaNumero.textosInformacion_SeleccionNumeros.TextosInformacion = value.BusquedaNumero.TextosInformacion_AsignarNumeros;

                lblNombreEntrada.Content = value.Nombre;

                if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                {
                    opcionEntradaManual.IsChecked = entr.ListaArchivos.FirstOrDefault().EntradaManual;
                }
                else if (entr.ParametrosExcel.Any())
                {
                    opcionEntradaManual.IsChecked = entr.ParametrosExcel.FirstOrDefault().EntradaManual;
                }

                MostrarOcultarElementosVisuales_EntradaManual(entr.EntradaArchivoManual);

                entr = value;

                if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla)
                {
                    configurarSeleccionCarpetaArchivo.Visibility = Visibility.Collapsed;
                    etiquetaDefinicionSeleccionArchivoCarpeta.Visibility = Visibility.Collapsed;
                    btnCambiarArchivo.Visibility = Visibility.Collapsed;
                    tipoArchivo.Visibility = Visibility.Collapsed;
                    lblRutaArchivo.Visibility = Visibility.Collapsed;
                }

                if (entr.EntradaArchivoManual)
                    opcionEntradaManual.IsEnabled = false;
            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        List<List<int>> NumerosEncontrados = new List<List<int>>();
        int indiceBusquedaNumeros = -1;
        public VistaArchivoEntradaNumero()
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
                            lblRutaArchivo.Content = "Se obtendrá el número de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se obtendrá el número de la variable o vector de entrada, del archivo seleccionado en la carpeta " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                            lblRutaArchivo.Content = "Para algunas variables o vectores de entrada, se obtendrá el número de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta;
                            break;
                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (entr.ListaArchivos.FirstOrDefault().ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se obtendrá el número de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se obtendrá el número de la variable o vector de entrada, del archivo seleccionado en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                            lblRutaArchivo.Content = "Para algunas variables o vectores de entradas, se obtendrá el número de la variable o vector de entrada, del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;
                    }

                    break;
            }
        }

        private void opcionBuscarTexto_Click(object sender, RoutedEventArgs e)
        {
            if (opcionBuscarTexto.IsChecked == true)
            {
                busquedaNumero.Visibility = Visibility.Visible;
                if(entr != null) entr.OpcionBusquedaNumero = OpcionBusquedaNumero.BusquedaTexto;
            }
            else
            {
                busquedaNumero.Visibility = Visibility.Collapsed;
            }

            busquedaNumero.opcionHastaFinalArchivo.Visibility = Visibility.Collapsed;
            busquedaNumero.opcionMientrasCondicionesCumplan.Visibility = Visibility.Collapsed;
            busquedaNumero.opcionHastaCondicionesCumplan.Visibility = Visibility.Collapsed;
            busquedaNumero.opcionNveces.Visibility = Visibility.Collapsed;
            busquedaNumero.lblVeces2.Visibility = Visibility.Collapsed;
            busquedaNumero.txtVeces.Visibility = Visibility.Collapsed;
        }

        private void opcionBuscarTextoNveces_Click(object sender, RoutedEventArgs e)
        {
            if (opcionBuscarTextoNveces.IsChecked == true)
            {
                busquedaNumero.Visibility = Visibility.Visible;
                if (entr != null) entr.OpcionBusquedaNumero = OpcionBusquedaNumero.BusquedaTextoNVeces;
            }
            else
            {
                busquedaNumero.Visibility = Visibility.Collapsed;
            }

            busquedaNumero.opcionHastaFinalArchivo.Visibility = Visibility.Collapsed;
            busquedaNumero.opcionMientrasCondicionesCumplan.Visibility = Visibility.Collapsed;
            busquedaNumero.opcionHastaCondicionesCumplan.Visibility = Visibility.Collapsed;
            busquedaNumero.opcionNveces.Visibility = Visibility.Visible;
            busquedaNumero.lblVeces2.Visibility = Visibility.Visible;
            busquedaNumero.txtVeces.Visibility = Visibility.Visible;
        }

        private void tipoArchivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoArchivo tipo = (TipoArchivo)int.Parse(((ComboBoxItem)tipoArchivo.SelectedItem).Uid);

            if (entr != null)
                entr.ListaArchivos.FirstOrDefault().TipoArchivo = tipo;

            switch (tipo)
            {
                case TipoArchivo.EquipoLocal:
                case TipoArchivo.RedLocal:
                    btnCambiarArchivo.Visibility = Visibility.Visible;
                    lblRutaArchivo.Visibility = Visibility.Visible;
                    rutaFTP.Visibility = Visibility.Collapsed;
                    configCredencialesFTP.Visibility = Visibility.Collapsed;

                    break;
                case TipoArchivo.ServidorFTP:
                    btnCambiarArchivo.Visibility = Visibility.Collapsed;
                    lblRutaArchivo.Visibility = Visibility.Collapsed;
                    rutaFTP.Visibility = Visibility.Visible;
                    configCredencialesFTP.Visibility = Visibility.Visible;
                    rutaFTP_TextChanged(this, null);

                    break;
                case TipoArchivo.Internet:
                    btnCambiarArchivo.Visibility = Visibility.Collapsed;
                    lblRutaArchivo.Visibility = Visibility.Collapsed;
                    rutaFTP.Visibility = Visibility.Visible;
                    configCredencialesFTP.Visibility = Visibility.Collapsed;
                    
                    break;
            }

            MostrarTextoArchivoCarpetaSeleccionada();
            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
        }

        private void rutaFTP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entr != null)
            {
                if (!string.IsNullOrEmpty(rutaFTP.Text))
                {
                    entr.ListaArchivos.FirstOrDefault().RutaArchivoEntrada = rutaFTP.Text;
                    entr.ListaArchivos.FirstOrDefault().NombreArchivoEntrada = rutaFTP.Text.Substring(rutaFTP.Text.LastIndexOf("\\") + 1);
                }
            }
        }

        private void configCredencialesFTP_Click(object sender, RoutedEventArgs e)
        {
            if (entr.CredencialesFTP == null)
                entr.CredencialesFTP = new Entidades.Entradas.CredencialesFTP();

            Config_CredencialesFTP configurar = new Config_CredencialesFTP();
            configurar.Configuracion = entr.CredencialesFTP;
            configurar.ShowDialog();
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

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirConfiguracionEntradaCalculo");
#endif
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

        private void btnObtenerTexto_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;

            try
            {
                contenidoTexto.MaxWidth = contenidoTexto.ActualWidth;

                contenidoTexto.Text = entr.ListaArchivos.FirstOrDefault().ObtenerArchivoTextoPlano_Temporal(entr, Calculo.NombreArchivo,
                    Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), Calculo.NombreArchivo);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Cursor = Cursors.Arrow;
        }

        private void BuscarNumeros_Click(object sender, RoutedEventArgs e)
        {
            BuscarNumeros_ContenidoURL();
            PosicionarNumero_Siguiente();
        }

        private void BuscarNumeros_ContenidoURL()
        {
            NumerosEncontrados.Clear();
            indiceBusquedaNumeros = -1;

            int cantidadCaracteres = 0;
            List<int> numeroEncontrado = new List<int>();
            int indiceCaracter = 0;

            Cursor = Cursors.Wait;

            foreach (var caracter in contenidoTexto.Text)
            {
                if (char.IsDigit(caracter))
                {
                    if (numeroEncontrado.Count == 0)
                    {
                        cantidadCaracteres = 1;
                        numeroEncontrado.Add(indiceCaracter);
                    }
                    else if (numeroEncontrado.Count == 1)
                    {
                        cantidadCaracteres++;
                    }
                }
                else
                {
                    if (numeroEncontrado.Count == 1)
                    {
                        if (caracter == '.' | caracter == ',')
                        {
                            cantidadCaracteres++;
                        }
                        else
                        {
                            numeroEncontrado.Add(cantidadCaracteres);
                            NumerosEncontrados.Add(new List<int>() { numeroEncontrado[0], numeroEncontrado[1] });
                            numeroEncontrado.Clear();
                        }
                    }
                }

                indiceCaracter++;
            }

            Cursor = Cursors.Arrow;

            if (NumerosEncontrados.Count > 0)
            {
                BusquedaNumeros.Visibility = Visibility.Visible;
            }
            else
            {
                BusquedaNumeros.Visibility = Visibility.Collapsed;
            }
        }

        private void SeleccionarNumero_Actual()
        {
            contenidoTexto.Select(NumerosEncontrados[indiceBusquedaNumeros][0], NumerosEncontrados[indiceBusquedaNumeros][1]);
            contenidoTexto.Focus();
        }

        private void PosicionarNumero_Siguiente()
        {
            if (indiceBusquedaNumeros == NumerosEncontrados.Count - 1)
            {
                contenidoTexto.Focus();
                return;
            }
            indiceBusquedaNumeros++;

            SeleccionarNumero_Actual();
        }

        private void PosicionarNumero_Anterior()
        {
            if (indiceBusquedaNumeros == 0)
            {
                contenidoTexto.Focus();
                return;
            }
            indiceBusquedaNumeros--;

            SeleccionarNumero_Actual();
        }

        private void NumeroSiguiente_Click(object sender, RoutedEventArgs e)
        {
            PosicionarNumero_Siguiente();
        }

        private void NumeroAnterior_Click(object sender, RoutedEventArgs e)
        {
            PosicionarNumero_Anterior();
        }

        private void opcionEsperarArchivo_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
                entr.ListaArchivos.FirstOrDefault().EsperarArchivos = (bool)opcionEsperarArchivo.IsChecked;
        }

        private void opcionEsperarArchivo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
                entr.ListaArchivos.FirstOrDefault().EsperarArchivos = (bool)opcionEsperarArchivo.IsChecked;
        }

        private void cantidadTiempo_TextChanged(object sender, TextChangedEventArgs e)
        {
            double cantidad = 0;
            if (double.TryParse(cantidadTiempo.Text, out cantidad))
            {
                entr.ListaArchivos.FirstOrDefault().TiempoEspera = cantidad;
            }
        }

        private void opcionCantidadTiempo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
                entr.ListaArchivos.FirstOrDefault().TipoTiempoEspera = (TipoTiempoEspera)int.Parse(((ComboBoxItem)opcionCantidadTiempo.SelectedItem).Uid);
        }

        private void opcionEntradaManual_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                {
                    foreach (var item in entr.ListaArchivos)
                    {
                        item.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                    }
                }
                else if (entr.ParametrosExcel.Any())
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
                if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                {
                    foreach (var item in entr.ListaArchivos)
                    {
                        item.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                    }
                }
                else if (entr.ParametrosExcel.Any())
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
                panelArchivo.Visibility = Visibility.Collapsed;
                //panelBusqueda.Visibility = Visibility.Collapsed;
            }
            else
            {
                panelArchivo.Visibility = Visibility.Visible;
                //panelBusqueda.Visibility = Visibility.Visible;
            }
        }
    }
}
