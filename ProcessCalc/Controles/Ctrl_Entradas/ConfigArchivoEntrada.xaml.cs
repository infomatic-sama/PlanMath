using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Ventanas;
using ProcessCalc.Vistas;
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
    /// Lógica de interacción para ConfigArchivoEntrada.xaml
    /// </summary>
    public partial class ConfigArchivoEntrada : UserControl
    {
        private ConfiguracionArchivoEntrada entrArch;
        public ConfiguracionArchivoEntrada EntradaArchivo
        {
            get
            {
                return entrArch;
            }

            set
            {
                entrArch = value;
                MostrarTextoArchivoCarpetaSeleccionada();

                tipoArchivo.SelectedItem = (from ComboBoxItem I in tipoArchivo.Items where I.Uid == ((int)value.TipoArchivo).ToString() select I).FirstOrDefault();

                MostrarTextoConfiguracion_SeleccionArchivoCarpeta();                

                if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                    verTextosBusquedasWord.Visibility = Visibility.Visible;

                if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word ||
                    entr.TipoOrigenDatos == TipoOrigenDatos.Excel)
                    opcionURLDocumento.Visibility = Visibility.Visible;

                opcionURLDocumento.IsChecked = entrArch.UsarURLOffice_original;
                opcionEntradaManual.IsChecked = entrArch.EntradaManual;
                MostrarOcultarElementosVisuales_EntradaManual(entrArch.EntradaManual);

                opcionEsperarArchivo.IsChecked = entrArch.EsperarArchivos;
                opcionCantidadTiempo.SelectedItem = (from ComboBoxItem I in opcionCantidadTiempo.Items where I.Uid == ((int)entrArch.TipoTiempoEspera).ToString() select I).FirstOrDefault();
                cantidadTiempo.Text = entrArch.TiempoEspera.ToString();

                if(entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                    opcionEntradaManual.Visibility = Visibility.Visible;
                else
                    opcionEntradaManual.Visibility = Visibility.Collapsed;
            }
        }
        public Entrada entr;
        public Entrada Entrada
        {
            get { return entr; }
            set 
            { 
                entr = value;

                //opcionURLDocumento.IsChecked = entr.UsarURL_Office;
            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        public VistaArchivoEntradaConjuntoNumeros VistaArchivoConjuntoNumeros { get; set; }
        public VistaArchivoEntradaTextosInformacion VistaArchivoTextosInformacion { get; set; }
        public VistaArchivoExcelEntradaConjuntoNumeros VistaArchivoExcelConjuntoNumeros { get; set; }
        public ConfigArchivoEntrada()
        {
            InitializeComponent();
        }

        private void MostrarTextoArchivoCarpetaSeleccionada()
        {
            if (VistaArchivoConjuntoNumeros != null)
            {
                string nombreArchivoCarpeta = string.Empty;

                if (entr.Tipo == TipoEntrada.Numero)
                    nombreArchivoCarpeta = entrArch.RutaArchivoEntrada;
                else if (entr.Tipo == TipoEntrada.ConjuntoNumeros)
                    nombreArchivoCarpeta = entrArch.RutaArchivoConjuntoNumerosEntrada;

                switch (entrArch.ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                        switch (entrArch.ConfiguracionSeleccionArchivo)
                        {
                            case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                lblRutaArchivo.Content = "Se obtendrá el conjunto de números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta;
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                lblRutaArchivo.Content = "Se obtendrá el conjunto de números de la entrada (variable o vector), del archivo seleccionado en la carpeta " + nombreArchivoCarpeta;
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                lblRutaArchivo.Content = "Para algunas entradas, se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta;
                                break;
                        }

                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                        switch (entrArch.ConfiguracionSeleccionArchivo)
                        {
                            case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                lblRutaArchivo.Content = "Se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                lblRutaArchivo.Content = "Se obtendrá los números de la entrada (variable o vector), del archivo seleccionado en la carpeta en el momento de la ejecución.";
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                lblRutaArchivo.Content = "Para algunas entradas, se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                                break;
                        }

                        break;
                }
            }
            else if (VistaArchivoTextosInformacion != null)
            {
                string nombreArchivoCarpeta = string.Empty;

                if (entr.Tipo == TipoEntrada.Numero)
                    nombreArchivoCarpeta = entrArch.RutaArchivoEntrada;
                else if (entr.Tipo == TipoEntrada.ConjuntoNumeros)
                    nombreArchivoCarpeta = entrArch.RutaArchivoConjuntoNumerosEntrada;
                else if (entr.Tipo == TipoEntrada.TextosInformacion)
                    nombreArchivoCarpeta = entrArch.RutaArchivoConjuntoTextosEntrada;

                switch (entrArch.ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                        switch (entrArch.ConfiguracionSeleccionArchivo)
                        {
                            case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                lblRutaArchivo.Content = "Se obtendrá las cadenas de texto de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta;
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                lblRutaArchivo.Content = "Se obtendrá las cadenas de texto de la entrada (variable o vector), del archivo seleccionado en la carpeta " + nombreArchivoCarpeta;
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                lblRutaArchivo.Content = "Para algunas entradas, se obtendrá las cadenas de texto de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta;
                                break;
                        }

                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                        switch (entrArch.ConfiguracionSeleccionArchivo)
                        {
                            case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                lblRutaArchivo.Content = "Se obtendrá las cadenas de texto de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                lblRutaArchivo.Content = "Se obtendrá las cadenas de texto de la entrada (variable o vector), del archivo seleccionado en la carpeta en el momento de la ejecución.";
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                lblRutaArchivo.Content = "Para algunas entradas, se obtendrá las cadenas de texto de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                                break;
                        }

                        break;
                }
            }
            else if(VistaArchivoExcelConjuntoNumeros != null)
            {
                string nombreArchivoCarpeta = string.Empty;

                if (entr.Tipo == TipoEntrada.Numero)
                    nombreArchivoCarpeta = entrArch.RutaArchivoEntrada;
                else if (entr.Tipo == TipoEntrada.ConjuntoNumeros)
                    nombreArchivoCarpeta = entrArch.RutaArchivoConjuntoNumerosEntrada;

                switch (entrArch.ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                        switch (entrArch.ConfiguracionSeleccionArchivo)
                        {
                            case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                lblRutaArchivo.Content = "Se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta;
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                lblRutaArchivo.Content = "Se obtendrá los números de la entrada (variable o vector), del archivo seleccionado en la carpeta " + nombreArchivoCarpeta;
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                lblRutaArchivo.Content = "Para algunas entradas, se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta;
                                break;
                        }

                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                        switch (entrArch.ConfiguracionSeleccionArchivo)
                        {
                            case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                                lblRutaArchivo.Content = "Se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                                break;

                            case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                                lblRutaArchivo.Content = "Se obtendrá los números de la entrada (variable o vector), del archivo seleccionado en la carpeta en el momento de la ejecución.";
                                break;

                            case OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada:
                                lblRutaArchivo.Content = "Para algunas entradas, se obtendrá los números de la entrada (variable o vector), del archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                                break;
                        }

                        break;
                }
            }
        }

        private void MostrarTextoConfiguracion_SeleccionArchivoCarpeta()
        {
            if (VistaArchivoConjuntoNumeros != null)
            {
                if (entr != null)
                {
                    string strCarpeta = string.Empty;
                    string strArchivo = string.Empty;

                    switch (entrArch.ConfiguracionSeleccionCarpeta)
                    {
                        case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                            strCarpeta = "Buscar en la carpeta indicada.";
                            break;

                        case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                            strCarpeta = "Buscar en la carpeta en que está el archivo de cálculo en el momento de la ejecución.";
                            break;
                    }

                    switch (entrArch.ConfiguracionSeleccionArchivo)
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
            else if(VistaArchivoTextosInformacion != null)
            {
                if (entrArch != null)
                {
                    string strCarpeta = string.Empty;
                    string strArchivo = string.Empty;

                    switch (entrArch.ConfiguracionSeleccionCarpeta)
                    {
                        case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                            strCarpeta = "Buscar en la carpeta indicada.";
                            break;

                        case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                            strCarpeta = "Buscar en la carpeta en que está el archivo de cálculo en el momento de la ejecución.";
                            break;
                    }

                    switch (entrArch.ConfiguracionSeleccionArchivo)
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
            else if(VistaArchivoExcelConjuntoNumeros != null)
            {
                if (entr != null)
                {
                    string strCarpeta = string.Empty;
                    string strArchivo = string.Empty;

                    switch (entrArch.ConfiguracionSeleccionCarpeta)
                    {
                        case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                            strCarpeta = "Buscar en la carpeta indicada.";
                            break;

                        case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                            strCarpeta = "Buscar en la carpeta en que está el archivo de cálculo en el momento de la ejecución.";
                            break;
                    }

                    switch (entrArch.ConfiguracionSeleccionArchivo)
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
        }

        private void configurarSeleccionCarpetaArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (VistaArchivoConjuntoNumeros != null |
                VistaArchivoExcelConjuntoNumeros != null |
                VistaArchivoTextosInformacion != null)
            {
                ConfigCarpetaArchivo configurar = new ConfigCarpetaArchivo();
                configurar.Entrada = entr;
                configurar.ArchivoEntrada = entrArch;
                configurar.ShowDialog();
                MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
                MostrarTextoArchivoCarpetaSeleccionada();
            }
        }

        private void btnCambiarArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (VistaArchivoConjuntoNumeros != null |
                VistaArchivoTextosInformacion != null |
                VistaArchivoExcelConjuntoNumeros != null)
            {
                if (entr != null)
                {
                    SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                    seleccionar.Entrada = entr;
                    seleccionar.ArchivoEntrada = entrArch;
                    seleccionar.VistaEntrada = VistaEntrada;
                    seleccionar.RutaCarpeta = Calculo.RutaArchivo == null ? string.Empty : Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));
                    seleccionar.ShowDialog();

                    MostrarTextoArchivoCarpetaSeleccionada();
                }
            }
        }

        private void tipoArchivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VistaArchivoConjuntoNumeros != null |
                VistaArchivoTextosInformacion != null)
            {
                TipoArchivo tipo = (TipoArchivo)int.Parse(((ComboBoxItem)tipoArchivo.SelectedItem).Uid);

                if (entrArch != null)
                    entrArch.TipoArchivo = tipo;

                switch (tipo)
                {
                    case TipoArchivo.EquipoLocal:
                    case TipoArchivo.RedLocal:
                        btnCambiarArchivo.Visibility = Visibility.Visible;
                        lblRutaArchivo.Visibility = Visibility.Visible;
                        //rutaFTP.Visibility = Visibility.Collapsed;
                        //configCredencialesFTP.Visibility = Visibility.Collapsed;

                        if(IsLoaded)
                            entrArch.ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion;

                        break;
                    case TipoArchivo.ServidorFTP:
                        btnCambiarArchivo.Visibility = Visibility.Collapsed;
                        lblRutaArchivo.Visibility = Visibility.Collapsed;
                        //rutaFTP.Visibility = Visibility.Visible;
                        //configCredencialesFTP.Visibility = Visibility.Visible;
                        //rutaFTP_TextChanged(this, null);

                        if (IsLoaded)
                            entrArch.ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada;

                        break;
                    case TipoArchivo.Internet:
                        btnCambiarArchivo.Visibility = Visibility.Collapsed;
                        lblRutaArchivo.Visibility = Visibility.Collapsed;
                        //rutaFTP.Visibility = Visibility.Visible;
                        //configCredencialesFTP.Visibility = Visibility.Collapsed;

                        if (IsLoaded)
                            entrArch.ConfiguracionSeleccionCarpeta = OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada;

                        break;
                }

                MostrarTextoArchivoCarpetaSeleccionada();
                MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
            }
        }

        private void opcionEstablecerLecturasNavegaciones_Busquedas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entrArch != null)
                {
                    entrArch.EstablecerLecturasNavegaciones_Busquedas = (bool)opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked;

                    conjuntoLecturas.ListarLecturasNavegaciones();
                    conjuntoLecturas.Visibility = Visibility.Visible;
                }                
            }
        }

        private void opcionEstablecerLecturasNavegaciones_Busquedas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entrArch != null)
                {
                    entrArch.EstablecerLecturasNavegaciones_Busquedas = (bool)opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked;

                    conjuntoLecturas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (entrArch != null)
            {
                conjuntoLecturas.Etiqueta = "Lectura";
                conjuntoLecturas.EtiquetaPlural = "Lecturas";

                if (Entrada.Tipo == TipoEntrada.Numero)
                    conjuntoLecturas.BusquedasEntrada = new List<BusquedaTextoArchivo>() { Entrada.BusquedaNumero };
                else if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    conjuntoLecturas.BusquedasEntrada = Entrada.BusquedasConjuntoNumeros;
                else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                    conjuntoLecturas.BusquedasEntrada = Entrada.BusquedasTextosInformacion;

                conjuntoLecturas.LecturasNavegaciones = entrArch.LecturasNavegaciones;
                conjuntoLecturas.ListarLecturasNavegaciones();

                opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked = entrArch.EstablecerLecturasNavegaciones_Busquedas;
                opcionAgregarTextos_DeNombresArchivos.IsChecked = entrArch.IncluirTextosInformacion_DeNombresRutasArchivos;
            }
        }

        private void verTextosBusquedasWord_Click(object sender, RoutedEventArgs e)
        {
            VerTextosBusquedaEntrada_DocumentoWord verTextos = new VerTextosBusquedaEntrada_DocumentoWord();
            verTextos.TextosBusqueda = entrArch.ParametrosWord;
            verTextos.ShowDialog();
        }

        private void opcionURLDocumento_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded && 
                entrArch != null)
            {
                EntradaArchivo.UsarURLOffice_original = (bool)opcionURLDocumento.IsChecked;
            }
        }

        private void opcionURLDocumento_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded &&
                entrArch != null)
            {
                EntradaArchivo.UsarURLOffice_original = (bool)opcionURLDocumento.IsChecked;
            }
        }

        private void btnDefinirTextosInformacionFijos_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
            seleccionarOrdenar.listaTextos.TextosInformacion = entrArch.TextosInformacionFijos.ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                entrArch.TextosInformacionFijos = seleccionarOrdenar.listaTextos.TextosInformacion;
            }
        }

        private void cantidadTiempo_TextChanged(object sender, TextChangedEventArgs e)
        {
            double cantidad = 0;
            if (double.TryParse(cantidadTiempo.Text, out cantidad))
            {
                entrArch.TiempoEspera = cantidad;
            }
        }

        private void opcionCantidadTiempo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
                entrArch.TipoTiempoEspera = (TipoTiempoEspera)int.Parse(((ComboBoxItem)opcionCantidadTiempo.SelectedItem).Uid);
        }

        private void opcionEsperarArchivo_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
                entrArch.EsperarArchivos = (bool)opcionEsperarArchivo.IsChecked;
        }

        private void opcionEsperarArchivo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
                entrArch.EsperarArchivos = (bool)opcionEsperarArchivo.IsChecked;
        }

        private void opcionAgregarTextos_DeNombresArchivos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entrArch != null)
                {
                    entrArch.IncluirTextosInformacion_DeNombresRutasArchivos = (bool)opcionAgregarTextos_DeNombresArchivos.IsChecked;

                }
            }
        }

        private void opcionAgregarTextos_DeNombresArchivos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entrArch != null)
                {
                    entrArch.IncluirTextosInformacion_DeNombresRutasArchivos = (bool)opcionAgregarTextos_DeNombresArchivos.IsChecked;

                }
            }
        }

        private void opcionEntradaManual_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entrArch.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                MostrarOcultarElementosVisuales_EntradaManual(entrArch.EntradaManual);

                if(entr.ParametrosExcel.Any())
                {
                    foreach(var item in entr.ParametrosExcel)
                    { 
                        item.EntradaManual = entrArch.EntradaManual;
                    }
                }                
            }
        }

        private void opcionEntradaManual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entrArch.EntradaManual = (bool)opcionEntradaManual.IsChecked;
                MostrarOcultarElementosVisuales_EntradaManual(entrArch.EntradaManual);

                if (entr.ParametrosExcel.Any())
                {
                    foreach (var item in entr.ParametrosExcel)
                    {
                        item.EntradaManual = entrArch.EntradaManual;
                    }
                }
            }
        }

        private void MostrarOcultarElementosVisuales_EntradaManual(bool entradaManual)
        {
            if (entradaManual)
            {
                verTextosBusquedasWord.Visibility = Visibility.Collapsed;
                //opcionEstablecerLecturasNavegaciones_Busquedas.Visibility = Visibility.Collapsed;
                //conjuntoLecturas.Visibility = Visibility.Collapsed;
            }
            else
            {
                verTextosBusquedasWord.Visibility = Visibility.Visible;
                //opcionEstablecerLecturasNavegaciones_Busquedas.Visibility = Visibility.Visible;

                //if (opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked == true)
                //    opcionEstablecerLecturasNavegaciones_Busquedas_Checked(this, null);
                //else
                //    opcionEstablecerLecturasNavegaciones_Busquedas_Unchecked(this, null);
            }
        }
    }
}
