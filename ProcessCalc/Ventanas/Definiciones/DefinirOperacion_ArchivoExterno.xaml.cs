using Newtonsoft.Json.Linq;
using ProcessCalc.Controles.Archivos;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para DefinirOperacion_ArchivoExterno.xaml
    /// </summary>
    public partial class DefinirOperacion_ArchivoExterno : Window
    {
        public ConfiguracionArchivoExterno Config { get; set; }
        public Calculo Calculo { get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasOperandos_ArchivoExterno { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosOperandos_ArchivoExterno { get; set; }
        public List<AsociacionEntradaOperando_ArchivoExterno> EntradasSubOperandos_ArchivoExterno { get; set; }
        public List<AsociacionResultadoOperando_ArchivoExterno> ResultadosSubOperandos_ArchivoExterno { get; set; }
        public List<Entrada> EntradasArchivoExterno { get; set; }
        public List<Resultado> ResultadosArchivoExterno { get; set; }
        public List<DiseñoOperacion> OperandosPosteriores { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosPosteriores { get; set; }
        
        public DefinirOperacion_ArchivoExterno()
        {
            InitializeComponent();
        }

        private void configurarSeleccionCarpetaArchivo_Click(object sender, RoutedEventArgs e)
        {
            ConfigCarpetaArchivo configurar = new ConfigCarpetaArchivo();
            //configurar.Entrada = entr;
            configurar.ModoArchivoExterno = true;
            configurar.ArchivoEntrada = Config.Archivo;
            configurar.ShowDialog();
            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
            MostrarTextoArchivoCarpetaSeleccionada();
        }

        private void MostrarTextoConfiguracion_SeleccionArchivoCarpeta()
        {
            if (Config != null &&
                Config.Archivo != null)
            {
                string strCarpeta = string.Empty;
                string strArchivo = string.Empty;

                switch (Config.Archivo.ConfiguracionSeleccionCarpeta)
                {
                    case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:
                        strCarpeta = "Buscar en la carpeta indicada.";
                        break;

                    case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:
                        strCarpeta = "Buscar en la carpeta en que está el archivo de cálculo en el momento de la ejecución.";
                        break;
                }

                switch (Config.Archivo.ConfiguracionSeleccionArchivo)
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

        private void MostrarTextoArchivoCarpetaSeleccionada()
        {
            string nombreArchivoCarpeta = string.Empty;

            if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
            {
                nombreArchivoCarpeta = Config.Archivo.RutaArchivoPlantilla;
                rutaArchivoPlantilla.Text = nombreArchivoCarpeta;
                archivoPlantilla.Visibility = Visibility.Visible;
            }
            else
            {
                nombreArchivoCarpeta = Config.Archivo.RutaArchivoEntrada;
                archivoPlantilla.Visibility = Visibility.Collapsed;
            }

            switch (Config.Archivo.ConfiguracionSeleccionCarpeta)
            {
                case OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada:

                    switch (Config.Archivo.ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se ejecutará el archivo " + nombreArchivoCarpeta;
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            if(archivoPlantilla.Visibility == Visibility.Visible)
                                lblRutaArchivo.Content = "Se ejecutará el archivo seleccionado en ejecución en la carpeta " + Config.Archivo.RutaArchivoEntrada;
                            else
                                lblRutaArchivo.Content = "Se ejecutará el archivo seleccionado en ejecución en la carpeta " + nombreArchivoCarpeta;

                            break;

                    }

                    break;

                case OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion:

                    switch (Config.Archivo.ConfiguracionSeleccionArchivo)
                    {
                        case OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado:
                            lblRutaArchivo.Content = "Se ejecutará el archivo " + nombreArchivoCarpeta + " en la carpeta en el momento de la ejecución.";
                            break;

                        case OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion:
                            lblRutaArchivo.Content = "Se ejecutará el archivo seleccionado en ejecución en la carpeta en el momento de la ejecución.";
                            break;
                    }

                    break;
            }

        }

        private void btnCambiarArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (Config != null &&
                Config.Archivo != null)
            {
                SeleccionarArchivoEntrada seleccionar = new SeleccionarArchivoEntrada();
                //seleccionar.Entrada = entr;
                //seleccionar.VistaEntrada = VistaEntrada;
                seleccionar.ModoArchivoExterno = true;
                seleccionar.ArchivoEntrada = Config.Archivo;

                bool mostrar = false;

                if (string.IsNullOrEmpty(Config.Archivo.RutaArchivoEntrada))
                {
                    if (Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion &&
                    Calculo != null && !string.IsNullOrEmpty(Calculo.RutaArchivo))
                    {
                        if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
                            seleccionar.RutaCarpeta = Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));
                        else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)
                            seleccionar.RutaCarpeta = Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));
                        mostrar = true;
                    }
                    else if (Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada &&
                    Calculo != null && !string.IsNullOrEmpty(Calculo.RutaArchivo))
                    {
                        if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
                            seleccionar.RutaCarpeta = Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));
                        else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)
                            seleccionar.RutaCarpeta = Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\"));

                        mostrar = true;
                    }
                }
                else
                {
                    if (Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion)
                    {
                        if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)
                            seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada.Remove(Config.Archivo.RutaArchivoEntrada.LastIndexOf("\\"));
                        else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
                            seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada;

                        mostrar = true;
                    }
                    else if (Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                    {
                        if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)
                            seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada.Remove(Config.Archivo.RutaArchivoEntrada.LastIndexOf("\\"));
                        else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion)
                            seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada;
                        mostrar = true;
                    }

                    if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)
                        seleccionar.RutaArchivo = Config.Archivo.RutaArchivoEntrada;

                    mostrar = true;
                }

                //if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado &&
                //        Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion)
                //    seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada;
                //else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado &&
                //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                //    seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada;
                //else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion &&
                //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion)
                //    seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada.Remove(Config.Archivo.RutaArchivoEntrada.LastIndexOf("\\"));
                //else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion &&
                //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                //    seleccionar.RutaCarpeta = Config.Archivo.RutaArchivoEntrada.Remove(Config.Archivo.RutaArchivoEntrada.LastIndexOf("\\"));

                if (mostrar)
                {
                    if (seleccionar.ShowDialog() == true)
                    {
                        //if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado &&
                        //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion)
                        //    Config.Archivo.RutaArchivoEntrada = seleccionar.RutaCarpeta;
                        //else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado &&
                        //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                        //    Config.Archivo.RutaArchivoEntrada = seleccionar.RutaCarpeta;
                        //else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion &&
                        //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaArchivoCalculoEjecucion)
                        //    Config.Archivo.RutaArchivoEntrada = seleccionar.RutaCarpeta.Remove(Config.Archivo.RutaArchivoEntrada.LastIndexOf("\\"));
                        //else if (Config.Archivo.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.SeleccionarArchivoEjecucion &&
                        //    Config.Archivo.ConfiguracionSeleccionCarpeta == OpcionCarpetaEntrada.CarpetaEspecificaSeleccionada)
                        //    Config.Archivo.RutaArchivoEntrada = seleccionar.RutaCarpeta.Remove(Config.Archivo.RutaArchivoEntrada.LastIndexOf("\\"));

                        MostrarTextoArchivoCarpetaSeleccionada();
                        CargarElementosArchivo_Calculo();
                        ListarElementosArchivo_Calculo();
                    }
                }
            }
        }

        private void tipoArchivo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoArchivo tipo = (TipoArchivo)int.Parse(((ComboBoxItem)tipoArchivo.SelectedItem).Uid);

            if (Config != null &&
                Config.Archivo != null)
                Config.Archivo.TipoArchivo = tipo;

            switch (tipo)
            {
                case TipoArchivo.EquipoLocal:
                case TipoArchivo.RedLocal:
                    btnCambiarArchivo.Visibility = Visibility.Visible;
                    lblRutaArchivo.Visibility = Visibility.Visible;
                    //rutaFTP.Visibility = Visibility.Collapsed;
                    //configCredencialesFTP.Visibility = Visibility.Collapsed;

                    break;
                case TipoArchivo.ServidorFTP:
                    btnCambiarArchivo.Visibility = Visibility.Collapsed;
                    lblRutaArchivo.Visibility = Visibility.Collapsed;
                    //rutaFTP.Visibility = Visibility.Visible;
                    //configCredencialesFTP.Visibility = Visibility.Visible;
                    //rutaFTP_TextChanged(this, null);

                    break;
                case TipoArchivo.Internet:
                    btnCambiarArchivo.Visibility = Visibility.Collapsed;
                    lblRutaArchivo.Visibility = Visibility.Collapsed;
                    //rutaFTP.Visibility = Visibility.Visible;
                    //configCredencialesFTP.Visibility = Visibility.Collapsed;

                    break;
            }

            MostrarTextoArchivoCarpetaSeleccionada();
            MostrarTextoConfiguracion_SeleccionArchivoCarpeta();
        }

        private void opcionEsperarArchivo_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
                Config.Archivo.EsperarArchivos = (bool)opcionEsperarArchivo.IsChecked;
        }

        private void opcionEsperarArchivo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
                Config.Archivo.EsperarArchivos = (bool)opcionEsperarArchivo.IsChecked;
        }

        private void cantidadTiempo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Config != null &&
                Config.Archivo != null)
            {
                double cantidad = 0;
                if (double.TryParse(cantidadTiempo.Text, out cantidad))
                {
                    Config.Archivo.TiempoEspera = cantidad;
                }
            }
        }
        private void opcionCantidadTiempo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
                Config.Archivo.TipoTiempoEspera = (TipoTiempoEspera)int.Parse(((ComboBoxItem)opcionCantidadTiempo.SelectedItem).Uid);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config != null &&
                Config.Archivo != null)
            {
                MostrarTextoArchivoCarpetaSeleccionada();

                opcionEsperarArchivo.IsChecked = Config.Archivo.EsperarArchivos;
                opcionCantidadTiempo.SelectedItem = (from ComboBoxItem I in opcionCantidadTiempo.Items where I.Uid == ((int)Config.Archivo.TipoTiempoEspera).ToString() select I).FirstOrDefault();
                cantidadTiempo.Text = Config.Archivo.TiempoEspera.ToString();
                opcionEjecucionSin_InfoVisual.IsChecked = Config.EjecutarArchivoSin_InfoVisual;
                opcionEjecucion_MismaEjecucion.IsChecked = Config.EjecutarArchivo_MismaEjecucion;
                tipoArchivo.SelectedItem = (from ComboBoxItem I in tipoArchivo.Items where I.Uid == ((int)Config.Archivo.TipoArchivo).ToString() select I).FirstOrDefault();

                CargarElementosArchivo_Calculo();
                ListarElementosArchivo_Calculo();
            }
        }

        private void ListarElementosArchivo_Calculo()
        {
            operandosEntradas.Children.Clear();
            operandosSalidas.Children.Clear();
            configEntradas.Children.Clear();

            if (ModoOperacion)
            {
                foreach (var item in SubOperandos)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in EntradasArchivoExterno)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = itemMenu.NombreCombo;
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = EntradasSubOperandos_ArchivoExterno.Where(i => i.SubOperacion == item & i.IDEntrada_Externa == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosEntradas.Children.Add(botonOperando);
                }

                foreach (var item in SubOperandosPosteriores)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in ResultadosArchivoExterno)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = (!string.IsNullOrEmpty(itemMenu.Nombre) ? itemMenu.Nombre : "Variable o vector: " + itemMenu.SalidaRelacionada.NombreCombo);
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = ResultadosSubOperandos_ArchivoExterno.Where(i => i.SubOperacion == item & i.IDResultado == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click1;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosSalidas.Children.Add(botonOperando);
                }

                foreach (var itemConfig in EntradasSubOperandos_ArchivoExterno)
                {
                    ConfigEntradas_ArchivoExterno config = new ConfigEntradas_ArchivoExterno();
                    var entrada = EntradasArchivoExterno.FirstOrDefault(i => i.ID == itemConfig.IDEntrada_Externa);
                    config.NombreEntrada = entrada.NombreCombo.ToString();
                    config.IDEntrada = entrada.ID.ToString();
                    config.SubOperando = itemConfig.SubOperacion;
                    config.Config = itemConfig.Configuracion;

                    configEntradas.Children.Add(config);
                }

            }
            else
            {
                foreach (var item in Operandos)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in EntradasArchivoExterno)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = itemMenu.NombreCombo;
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = EntradasOperandos_ArchivoExterno.Where(i => i.Operacion == item & i.IDEntrada_Externa == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosEntradas.Children.Add(botonOperando);
                }

                foreach (var item in OperandosPosteriores)
                {
                    Button botonOperando = new Button();
                    //botonOperando.Tag = item;
                    botonOperando.Content = item.NombreCombo;
                    botonOperando.Margin = new Thickness(10);
                    botonOperando.Padding = new Thickness(5);
                    botonOperando.Click += BotonOperando_Click;
                    botonOperando.MaxHeight = 35;

                    botonOperando.ContextMenu = new ContextMenu();

                    foreach (var itemMenu in ResultadosArchivoExterno)
                    {
                        CheckBox opcionMenu = new CheckBox();
                        opcionMenu.Tag = new object[] { item, itemMenu };
                        opcionMenu.Content = (!string.IsNullOrEmpty(itemMenu.Nombre) ? itemMenu.Nombre : "Variable o vector: " + itemMenu.SalidaRelacionada.NombreCombo);
                        opcionMenu.Padding = new Thickness(5);

                        var dupla = ResultadosOperandos_ArchivoExterno.Where(i => i.Operacion == item & i.IDResultado == itemMenu.ID).FirstOrDefault();

                        if (dupla != null)
                            opcionMenu.IsChecked = true;

                        opcionMenu.Click += OpcionMenu_Click1;
                        botonOperando.ContextMenu.Items.Add(opcionMenu);
                    }

                    operandosSalidas.Children.Add(botonOperando);
                }

                foreach (var itemConfig in EntradasOperandos_ArchivoExterno)
                {
                    ConfigEntradas_ArchivoExterno config = new ConfigEntradas_ArchivoExterno();
                    var entrada = EntradasArchivoExterno.FirstOrDefault(i => i.ID == itemConfig.IDEntrada_Externa);
                    config.NombreEntrada = entrada.NombreCombo.ToString();
                    config.IDEntrada = entrada.ID.ToString();
                    config.Operando = itemConfig.Operacion;
                    config.Config = itemConfig.Configuracion;

                    configEntradas.Children.Add(config);
                }
            }

            if (operandosSalidas.Children.Count == 0)
                textoResultados.Visibility = Visibility.Collapsed;
            else
                textoResultados.Visibility = Visibility.Visible;
        }

        private void OpcionMenu_Click1(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;
            bool seleccionado = (bool)((CheckBox)sender).IsChecked;

            if (ModoOperacion)
            {
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];
                Resultado resultado = (Resultado)objetos[1];

                var dupla = ResultadosSubOperandos_ArchivoExterno.Where(i => i.SubOperacion == operando & i.IDResultado == resultado.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        ResultadosSubOperandos_ArchivoExterno.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        ResultadosSubOperandos_ArchivoExterno.Add(new AsociacionResultadoOperando_ArchivoExterno { SubOperacion = operando, IDResultado = resultado.ID });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                Resultado resultado = (Resultado)objetos[1];

                var dupla = ResultadosOperandos_ArchivoExterno.Where(i => i.Operacion == operando & i.IDResultado == resultado.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        ResultadosOperandos_ArchivoExterno.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        ResultadosOperandos_ArchivoExterno.Add(new AsociacionResultadoOperando_ArchivoExterno { Operacion = operando, IDResultado = resultado.ID });
                    }
                }
            }

            CargarElementosArchivo_Calculo();
            ListarElementosArchivo_Calculo();
        }

        private void BotonOperando_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void OpcionMenu_Click(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;
            bool seleccionado = (bool)((CheckBox)sender).IsChecked;

            if (ModoOperacion)
            {
                DiseñoElementoOperacion operando = (DiseñoElementoOperacion)objetos[0];
                Entrada entrada = (Entrada)objetos[1];

                var dupla = EntradasSubOperandos_ArchivoExterno.Where(i => i.SubOperacion == operando & i.IDEntrada_Externa == entrada.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasSubOperandos_ArchivoExterno.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasSubOperandos_ArchivoExterno.Add(new AsociacionEntradaOperando_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador) { SubOperacion = operando, IDEntrada_Externa = entrada.ID });
                    }
                }
            }
            else
            {
                DiseñoOperacion operando = (DiseñoOperacion)objetos[0];
                Entrada entrada = (Entrada)objetos[1];

                var dupla = EntradasOperandos_ArchivoExterno.Where(i => i.Operacion == operando & i.IDEntrada_Externa == entrada.ID).FirstOrDefault();

                if (dupla != null)
                {
                    if (!seleccionado)
                    {
                        EntradasOperandos_ArchivoExterno.Remove(dupla);
                    }
                }
                else
                {
                    if (seleccionado)
                    {
                        EntradasOperandos_ArchivoExterno.Add(new AsociacionEntradaOperando_ArchivoExterno(TipoConfiguracionTraspasoCantidades_ArchivoExterno.ConfiguracionOperador) { Operacion = operando, IDEntrada_Externa = entrada.ID });
                    }
                }
            }

            CargarElementosArchivo_Calculo();
            ListarElementosArchivo_Calculo();
        }

        private void opcionEjecucionSin_InfoVisual_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                if(Config != null)
                {
                    Config.EjecutarArchivoSin_InfoVisual = (bool)opcionEjecucionSin_InfoVisual.IsChecked;
                }
            }
        }

        private void opcionEjecucionSin_InfoVisual_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarArchivoSin_InfoVisual = (bool)opcionEjecucionSin_InfoVisual.IsChecked;
                }
            }
        }

        private void opcionEjecucion_MismaEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarArchivo_MismaEjecucion = (bool)opcionEjecucion_MismaEjecucion.IsChecked;
                }
            }
        }

        private void opcionEjecucion_MismaEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Config != null)
                {
                    Config.EjecutarArchivo_MismaEjecucion = (bool)opcionEjecucion_MismaEjecucion.IsChecked;
                }
            }
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public void CargarElementosArchivo_Calculo()
        {
            EntradasArchivoExterno = Config.ObtenerEntradas_ArchivoCalculo();
            ResultadosArchivoExterno = Config.ObtenerResultados_ArchivoCalculo();            
        }
    }

    public class MaxWidthConverter_DefinirArchivoExterno : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentException("El valor no es válido", "values");

            return ((double)value / 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
