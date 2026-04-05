using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaURLEntradaTextosInformacion.xaml
    /// </summary>
    public partial class VistaURLEntradaTextosInformacion : UserControl
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
                lblNombreEntrada.Content = value.Nombre;

                opcionMismaLectura.IsChecked = value.MismaLecturaBusquedasArchivo;
                if (!value.MismaLecturaBusquedasArchivo)
                    opcionLecturasDistintas.IsChecked = true;

                entr = value;

                CargarListaURLs(value.ListaURLs);

            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        ConfiguracionURLEntrada configuracionURL_Seleccionada;
        public VistaURLEntradaTextosInformacion()
        {
            InitializeComponent();
        }

        public void ListarBusquedas()
        {
            busquedas.Children.Clear();

            foreach (var itemBusqueda in entr.BusquedasTextosInformacion)
            {
                BusquedaEnArchivo busquedaNumero = new BusquedaEnArchivo();
                busquedaNumero.Entrada = Entrada;
                busquedaNumero.VistaBusquedasURL_TextosInformacion = this;
                busquedaNumero.Busqueda = itemBusqueda;
                busquedaNumero.MostrarOcultarOpcionesTextosInformacion();

                busquedas.Children.Add(busquedaNumero);
            }
        }

        public void MarcarBusquedaSeleccionada(BusquedaEnArchivo busquedaSeleccionada)
        {
            foreach (var busqueda in busquedas.Children)
            {
                if (busquedaSeleccionada != null &&
                    busqueda == busquedaSeleccionada)
                {
                    ((BusquedaEnArchivo)busqueda).Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else
                {
                    ((BusquedaEnArchivo)busqueda).Background = null;
                }
            }
        }

        private void opcionMismaLectura_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.MismaLecturaBusquedasArchivo = (bool)opcionMismaLectura.IsChecked;
            }
        }

        private void opcionLecturasDistintas_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.MismaLecturaBusquedasArchivo = !(bool)opcionLecturasDistintas.IsChecked;
            }
        }

        public void btnObtenerDeURL_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;

            try
            {
                contenidoURL.MaxWidth = contenidoURL.ActualWidth;

                if (configuracionURL_Seleccionada.URLEntrada.Length >= 8)
                {
                    if (!configuracionURL_Seleccionada.URLEntrada.Substring(0, 7).ToLower().Equals("http://") &
                    !configuracionURL_Seleccionada.URLEntrada.Substring(0, 8).ToLower().Equals("https://"))
                    {
                        configuracionURL_Seleccionada.URLEntrada = configuracionURL_Seleccionada.URLEntrada.Insert(0, "http://");
                    }

                    ObjetoURL solicitarContenido = new ObjetoURL(configuracionURL_Seleccionada.URLEntrada, 
                        configuracionURL_Seleccionada.ParametrosURL, configuracionURL_Seleccionada.HeadersURL);
                    contenidoURL.Text = solicitarContenido.ObtenerTexto();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Cursor = Cursors.Arrow;
        }

        
        public void AgregarNuevaBusqueda()
        {
            BusquedaTextoArchivo nueva = new BusquedaTextoArchivo();
            entr.BusquedasTextosInformacion.Add(nueva);
            nueva.Nombre = "Búsqueda " + entr.BusquedasTextosInformacion.Count;

            BusquedaEnArchivo nuevaBusqueda = new BusquedaEnArchivo();
            nuevaBusqueda.Entrada = Entrada;
            nuevaBusqueda.Busqueda = nueva;
            nuevaBusqueda.VistaBusquedasURL_TextosInformacion = this;
            nuevaBusqueda.MostrarOcultarOpcionesTextosInformacion();

            busquedas.Children.Add(nuevaBusqueda);
            nuevaBusqueda.Button_Click(this, new RoutedEventArgs());
        }

        public void ActualizarNombreBusqueda(BusquedaTextoArchivo busqueda)
        {
            if (contenedorBusqueda.Children.Count > 0)
            {
                if (((TextoEnArchivo)contenedorBusqueda.Children[0]).Busqueda == busqueda)
                    ((TextoEnArchivo)contenedorBusqueda.Children[0]).nombreBusqueda.Text = busqueda.Nombre;
            }
        }

        public void QuitarBusqueda(BusquedaEnArchivo busqueda)
        {
            entr.BusquedasTextosInformacion.Remove(busqueda.Busqueda);
            busquedas.Children.Remove(busqueda);
            contenedorBusqueda.Children.Clear();

            QuitarBusquedas_LecturasNavegaciones(busqueda.Busqueda);

            if (!entr.BusquedasTextosInformacion.Any())
            {
                busqueda.AgregarBusqueda_DesdeVista(this, null);
            }
        }

        public void QuitarBusquedas_LecturasNavegaciones(BusquedaTextoArchivo busqueda)
        {
            foreach (var itemArchivo in entr.ListaURLs)
            {
                foreach (var itemLecturaNavegacion in itemArchivo.LecturasNavegaciones)
                {
                    if (itemLecturaNavegacion.BusquedasARealizar.Contains(busqueda))
                        itemLecturaNavegacion.BusquedasARealizar.Remove(busqueda);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirConfiguracionEntradaURLTextosCalculo");
#endif
        }

        private void opcionUtilizarTextosInformacionUnicos_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UtilizarTextosInformacionUnicos = (bool)opcionUtilizarTextosInformacionUnicos.IsChecked;
            }
        }

        private void opcionUtilizarTextosInformacionUnicos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UtilizarTextosInformacionUnicos = (bool)opcionUtilizarTextosInformacionUnicos.IsChecked;
            }
        }

        private void agregarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            ((BusquedaEnArchivo)busquedas.Children[0]).AgregarBusqueda_DesdeVista(sender, e);
        }
        private void agregarArchivoLista_Click(object sender, RoutedEventArgs e)
        {
            entr.ListaURLs.Add(new ConfiguracionURLEntrada());

            listaArchivos.Children.Clear();
            CargarListaURLs(entr.ListaURLs);
        }

        public void CargarListaURLs(List<ConfiguracionURLEntrada> lista)
        {
            foreach (var item in lista)
                AgregarTabURL(item, lista);
        }

        public void AgregarTabURL(ConfiguracionURLEntrada archivoConfig,
            List<ConfiguracionURLEntrada> ListaArchivos)
        {
            Border configArchivo = new Border();
            configArchivo.BorderBrush = Brushes.Black;
            configArchivo.BorderThickness = new Thickness(0.3);
            configArchivo.VerticalAlignment = VerticalAlignment.Center;
            configArchivo.Margin = new Thickness(10);
            configArchivo.Padding = new Thickness(10);

            Grid gridConfigArchivo = new Grid();
            gridConfigArchivo.RowDefinitions.Add(new RowDefinition());
            gridConfigArchivo.RowDefinitions.Last().Height = GridLength.Auto;

            gridConfigArchivo.ColumnDefinitions.Add(new ColumnDefinition());
            gridConfigArchivo.ColumnDefinitions.Last().Width = GridLength.Auto;
            gridConfigArchivo.ColumnDefinitions.Add(new ColumnDefinition());
            gridConfigArchivo.ColumnDefinitions.Last().Width = GridLength.Auto;
            gridConfigArchivo.ColumnDefinitions.Add(new ColumnDefinition());
            gridConfigArchivo.ColumnDefinitions.Last().Width = GridLength.Auto;
            gridConfigArchivo.ColumnDefinitions.Add(new ColumnDefinition());
            gridConfigArchivo.ColumnDefinitions.Last().Width = GridLength.Auto;
            gridConfigArchivo.ColumnDefinitions.Add(new ColumnDefinition());
            gridConfigArchivo.ColumnDefinitions.Last().Width = GridLength.Auto;

            Grid gridBoton = new Grid();
            gridBoton.ColumnDefinitions.Add(new ColumnDefinition());
            gridBoton.ColumnDefinitions.Last().Width = GridLength.Auto;
            gridBoton.ColumnDefinitions.Add(new ColumnDefinition());
            gridBoton.ColumnDefinitions.Last().Width = GridLength.Auto;

            Image ImagenBoton = new Image();
            ImagenBoton.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos12\\icono_nuevo_03.png", UriKind.Relative));
            ImagenBoton.Width = 24;
            ImagenBoton.Height = 24;

            Label EtiquetaBoton = new Label();
            EtiquetaBoton.VerticalAlignment = VerticalAlignment.Center;
            EtiquetaBoton.Content = string.IsNullOrEmpty(archivoConfig.URLEntrada) ?
                "Seleccionar URL y configuración" : archivoConfig.URLEntrada;

            gridBoton.Children.Add(ImagenBoton);
            Grid.SetColumn(ImagenBoton, 0);

            gridBoton.Children.Add(EtiquetaBoton);
            Grid.SetColumn(EtiquetaBoton, 1);

            Button botonConfigArchivo = new Button();
            botonConfigArchivo.Margin = new Thickness(10);
            botonConfigArchivo.Padding = new Thickness(5);

            botonConfigArchivo.Content = gridBoton;
            botonConfigArchivo.Tag = archivoConfig;
            botonConfigArchivo.Click += BotonConfigArchivo_Click;

            gridConfigArchivo.Children.Add(botonConfigArchivo);
            Grid.SetColumn(botonConfigArchivo, 1);
            Grid.SetRow(botonConfigArchivo, 0);

            Button botonConfigNavegacion = new Button();
            botonConfigNavegacion.Margin = new Thickness(10);
            botonConfigNavegacion.Padding = new Thickness(5);

            botonConfigNavegacion.Content = "Navegación";
            botonConfigNavegacion.Tag = archivoConfig;
            botonConfigNavegacion.Click += BotonConfigNavegacion_Click;

            gridConfigArchivo.Children.Add(botonConfigNavegacion);
            Grid.SetColumn(botonConfigNavegacion, 2);
            Grid.SetRow(botonConfigNavegacion, 0);

            if (archivoConfig != ListaArchivos.FirstOrDefault())
            {
                Button botonIzquierdo = new Button();
                botonIzquierdo.Margin = new Thickness(10);
                botonIzquierdo.Padding = new Thickness(5);

                botonIzquierdo.Content = "<";
                botonIzquierdo.Tag = archivoConfig;
                botonIzquierdo.Click += BotonIzquierdo_Click;

                gridConfigArchivo.Children.Add(botonIzquierdo);
                Grid.SetColumn(botonIzquierdo, 0);
                Grid.SetRow(botonIzquierdo, 0);
            }

            Button botonQuitar = new Button();
            botonQuitar.Margin = new Thickness(10);
            botonQuitar.Padding = new Thickness(5);

            Image ImagenBotonQuitar = new Image();
            ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos12\\icono_nuevo_05.png", UriKind.Relative));
            ImagenBotonQuitar.Width = 24;
            ImagenBotonQuitar.Height = 24;

            botonQuitar.Content = ImagenBotonQuitar;
            botonQuitar.Tag = archivoConfig;
            botonQuitar.Click += quitarArchivoLista_Click;

            gridConfigArchivo.Children.Add(botonQuitar);
            Grid.SetColumn(botonQuitar, 3);
            Grid.SetRow(botonQuitar, 0);

            if (archivoConfig != ListaArchivos.LastOrDefault())
            {
                Button botonDerecho = new Button();
                botonDerecho.Margin = new Thickness(10);
                botonDerecho.Padding = new Thickness(5);

                botonDerecho.Content = ">";
                botonDerecho.Tag = archivoConfig;
                botonDerecho.Click += BotonDerecho_Click;

                gridConfigArchivo.Children.Add(botonDerecho);
                Grid.SetColumn(botonDerecho, 4);
                Grid.SetRow(botonDerecho, 0);
            }

            configArchivo.Child = gridConfigArchivo;
            configArchivo.Tag = archivoConfig;

            listaArchivos.Children.Add(configArchivo);

            //entr.ListaArchivos.Add(archivoConfig);
        }

        private void BotonConfigNavegacion_Click(object sender, RoutedEventArgs e)
        {
            var archivoConfig = ((ConfiguracionURLEntrada)((Button)sender).Tag);
            configuracionURL_Seleccionada = archivoConfig;
            urlText.Text = archivoConfig.URLEntrada;
            grillaNavegacion.Visibility = Visibility.Visible;
        }

        private void BotonDerecho_Click(object sender, RoutedEventArgs e)
        {
            var archivoConfig = ((ConfiguracionURLEntrada)((Button)sender).Tag);

            int index = entr.ListaURLs.IndexOf(archivoConfig);
            entr.ListaURLs.RemoveAt(index);
            entr.ListaURLs.Insert(index + 1, archivoConfig);

            listaArchivos.Children.Clear();
            CargarListaURLs(entr.ListaURLs);
        }

        private void BotonIzquierdo_Click(object sender, RoutedEventArgs e)
        {
            var archivoConfig = ((ConfiguracionURLEntrada)((Button)sender).Tag);

            int index = entr.ListaURLs.IndexOf(archivoConfig);
            entr.ListaURLs.RemoveAt(index);
            entr.ListaURLs.Insert(index - 1, archivoConfig);

            listaArchivos.Children.Clear();
            CargarListaURLs(entr.ListaURLs);
        }

        private void BotonConfigArchivo_Click(object sender, RoutedEventArgs e)
        {

            Border config = (from UIElement C in listaArchivos.Children
                             where C.GetType() == typeof(Border) &&
                             ((Border)C).Tag == ((Button)sender).Tag
                             select (Border)C).FirstOrDefault();

            MostrarConfiguracionURL(((ConfiguracionURLEntrada)((Button)sender).Tag));
        }

        public void MostrarConfiguracionURL(ConfiguracionURLEntrada archivoConfig)
        {
            ConfiguracionLecturaURLEntrada verConfig = new ConfiguracionLecturaURLEntrada();
            verConfig.config.Entrada = Entrada;
            //verConfig.config.VistaEntrada = VistaEntrada;
            //verConfig.config.Calculo = Calculo;
            //verConfig.config.VistaArchivoConjuntoNumeros = this;
            verConfig.config.ConfiguracionURL = archivoConfig;
            verConfig.ShowDialog();

            listaArchivos.Children.Clear();
            CargarListaURLs(entr.ListaURLs);
        }

        private void quitarArchivoLista_Click(object sender, RoutedEventArgs e)
        {
            Border config = (from UIElement C in listaArchivos.Children
                             where C.GetType() == typeof(Border) &&
                             ((Border)C).Tag == ((Button)sender).Tag
                             select (Border)C).FirstOrDefault();

            var configArchivoSeleccionado = (ConfiguracionURLEntrada)config.Tag;

            entr.ListaURLs.Remove(configArchivoSeleccionado);
            listaArchivos.Children.Remove(config);

            if (configArchivoSeleccionado == configuracionURL_Seleccionada)
            {
                grillaNavegacion.Visibility = Visibility.Collapsed;
                configuracionURL_Seleccionada = null;
            }

            if (!entr.ListaURLs.Any())
                agregarArchivoLista_Click(sender, e);

        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
