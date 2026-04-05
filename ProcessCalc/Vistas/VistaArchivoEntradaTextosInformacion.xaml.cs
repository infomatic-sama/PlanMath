using ProcessCalc.Controles;
using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para VistaArchivoEntradaTextosInformacion.xaml
    /// </summary>
    public partial class VistaArchivoEntradaTextosInformacion : UserControl
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

                lblNombreEntrada.Content = value.Nombre;

                opcionMismaLectura.IsChecked = value.MismaLecturaBusquedasArchivo;
                if (!value.MismaLecturaBusquedasArchivo)
                    opcionLecturasDistintas.IsChecked = true;

                //tipoArchivo.SelectedItem = (from ComboBoxItem I in tipoArchivo.Items where I.Uid == ((int)value.TipoArchivo).ToString() select I).FirstOrDefault();
                //if (value.TipoArchivo == TipoArchivo.ServidorFTP |
                //    value.TipoArchivo == TipoArchivo.Internet)
                //    rutaFTP.Text = value.RutaArchivoConjuntoTextosEntrada;
                listaArchivos.Children.Clear();
                CargarListaArchivos(value.ListaArchivos);

                //opcionCantidadAgregarNumeros.IsChecked = entr.UtilizarCantidadNumeros;
                //if (entr.UtilizarCantidadNumeros)
                //    opcionCantidadAgregarNumeros_Checked(this, null);
                //else
                //    opcionCantidadAgregarNumeros_Unchecked(this, null);

                //opcionUtilizacionCantidadAgregarNumeros.SelectedItem = (from ComboBoxItem I in opcionUtilizacionCantidadAgregarNumeros.Items where I.Uid == ((int)value.OpcionCantidadNumeros).ToString() select I).FirstOrDefault();

                //operacionInterna.SelectedItem = (from ComboBoxItem I in operacionInterna.Items where I.Uid == ((int)value.OperacionInterna).ToString() select I).FirstOrDefault();
            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        List<List<int>> NumerosEncontrados = new List<List<int>>();
        int indiceBusquedaNumeros = -1;
        ConfiguracionArchivoEntrada configuracionArchivo_Seleccionada;
        public VistaArchivoEntradaTextosInformacion()
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
                busquedaNumero.VistaBusquedasArchivo_TextosInformacion = this;
                busquedaNumero.Busqueda = itemBusqueda;
                busquedaNumero.MostrarOcultarOpcionesTextosInformacion();

                busquedas.Children.Add(busquedaNumero);
            }
        }

        public void CargarListaArchivos(List<ConfiguracionArchivoEntrada> lista)
        {
            foreach (var item in lista)
                AgregarTabArchivo(item, lista);
        }

        public void MarcarBusquedaSeleccionada(BusquedaEnArchivo busquedaSeleccionada)
        {
            foreach (var busqueda in busquedas.Children)
            {
                if (busquedaSeleccionada != null &&
                    busqueda == busquedaSeleccionada)
                {
                    ((BusquedaEnArchivo)busqueda).Background = SystemColors.InactiveBorderBrush;
                }
                else
                {
                    ((BusquedaEnArchivo)busqueda).Background = null;
                }
            }
        }

        public void AgregarNuevaBusqueda()
        {
            BusquedaTextoArchivo nueva = new BusquedaTextoArchivo();
            entr.BusquedasTextosInformacion.Add(nueva);
            nueva.Nombre = "Lógica de búsqueda " + entr.BusquedasTextosInformacion.Count;

            BusquedaEnArchivo nuevaBusqueda = new BusquedaEnArchivo();
            nuevaBusqueda.Entrada = Entrada;
            nuevaBusqueda.Busqueda = nueva;
            nuevaBusqueda.VistaBusquedasArchivo_TextosInformacion = this;
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
            foreach (var itemArchivo in entr.ListaArchivos)
            {
                foreach (var itemLecturaNavegacion in itemArchivo.LecturasNavegaciones)
                {
                    if (itemLecturaNavegacion.BusquedasARealizar.Contains(busqueda))
                        itemLecturaNavegacion.BusquedasARealizar.Remove(busqueda);
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
        private void rutaFTP_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (entr != null)
            //{
            //    if (!string.IsNullOrEmpty(rutaFTP.Text))
            //    {
            //        entr.RutaArchivoConjuntoTextosEntrada = rutaFTP.Text;
            //        entr.NombreArchivoEntrada = rutaFTP.Text.Substring(rutaFTP.Text.LastIndexOf("\\") + 1);
            //    }
            //}
        }

        private void configCredencialesFTP_Click(object sender, RoutedEventArgs e)
        {
            if (entr.CredencialesFTP == null)
                entr.CredencialesFTP = new Entidades.Entradas.CredencialesFTP();

            Config_CredencialesFTP configurar = new Config_CredencialesFTP();
            configurar.Configuracion = entr.CredencialesFTP;
            configurar.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirConfiguracionEntradaTextosCalculo");
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
            entr.ListaArchivos.Add(new ConfiguracionArchivoEntrada()
            {
                EntradaManual = entr.EntradaArchivoManual
            });

            listaArchivos.Children.Clear();
            CargarListaArchivos(entr.ListaArchivos);
        }

        private void quitarArchivoLista_Click(object sender, RoutedEventArgs e)
        {
            Border config = (from UIElement C in listaArchivos.Children
                             where C.GetType() == typeof(Border) &&
                             ((Border)C).Tag == ((Button)sender).Tag
                             select (Border)C).FirstOrDefault();

            var configArchivoSeleccionado = (ConfiguracionArchivoEntrada)config.Tag;

            if (configArchivoSeleccionado == configuracionArchivo_Seleccionada)
            {
                grillaLecturaArchivo.Visibility = Visibility.Collapsed;
                configuracionArchivo_Seleccionada = null;
            }

            entr.ListaArchivos.Remove(configArchivoSeleccionado);
            listaArchivos.Children.Remove(config);


        }

        public void AgregarTabArchivo(ConfiguracionArchivoEntrada archivoConfig, 
            List<ConfiguracionArchivoEntrada> ListaArchivos)
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

            if (entr.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.TextoPantalla)
            {
                Label etiquetaConfigArchivo = new Label();
                etiquetaConfigArchivo.Margin = new Thickness(10);
                etiquetaConfigArchivo.Padding = new Thickness(5);

                etiquetaConfigArchivo.Content = "Texto en pantalla";

                gridConfigArchivo.Children.Add(etiquetaConfigArchivo);
                Grid.SetColumn(etiquetaConfigArchivo, 1);
                Grid.SetRow(etiquetaConfigArchivo, 0);
            }
            else
            {
                Grid gridBoton = new Grid();
                gridBoton.ColumnDefinitions.Add(new ColumnDefinition());
                gridBoton.ColumnDefinitions.Last().Width = GridLength.Auto;
                gridBoton.ColumnDefinitions.Add(new ColumnDefinition());
                gridBoton.ColumnDefinitions.Last().Width = GridLength.Auto;

                Image ImagenBoton = new Image();
                ImagenBoton.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos12\\icono_nuevo_02.png", UriKind.Relative));
                ImagenBoton.Width = 24;
                ImagenBoton.Height = 24;

                Label EtiquetaBoton = new Label();
                EtiquetaBoton.VerticalAlignment = VerticalAlignment.Center;
                EtiquetaBoton.Content = string.IsNullOrEmpty(archivoConfig.NombreArchivoEntrada) ?
                    "Seleccionar archivo y configuración" : archivoConfig.NombreArchivoEntrada;

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

                if (!archivoConfig.EntradaManual)
                {
                    Button botonTextoArchivo = new Button();
                    botonTextoArchivo.Margin = new Thickness(10);
                    botonTextoArchivo.Padding = new Thickness(5);

                    botonTextoArchivo.Content = "Texto obtenido";
                    botonTextoArchivo.Tag = archivoConfig;
                    botonTextoArchivo.Click += BotonTextoArchivo_Click;

                    gridConfigArchivo.Children.Add(botonTextoArchivo);
                    Grid.SetColumn(botonTextoArchivo, 2);
                    Grid.SetRow(botonTextoArchivo, 0);
                }
            }

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

        private void BotonDerecho_Click(object sender, RoutedEventArgs e)
        {
            var archivoConfig = ((ConfiguracionArchivoEntrada)((Button)sender).Tag);

            int index = entr.ListaArchivos.IndexOf(archivoConfig);
            entr.ListaArchivos.Remove(archivoConfig);
            entr.ListaArchivos.Insert(index + 1, archivoConfig);

            listaArchivos.Children.Clear();
            CargarListaArchivos(entr.ListaArchivos);
        }

        private void BotonIzquierdo_Click(object sender, RoutedEventArgs e)
        {
            var archivoConfig = ((ConfiguracionArchivoEntrada)((Button)sender).Tag);

            int index = entr.ListaArchivos.IndexOf(archivoConfig);
            entr.ListaArchivos.Remove(archivoConfig);
            entr.ListaArchivos.Insert(index - 1, archivoConfig);

            listaArchivos.Children.Clear();
            CargarListaArchivos(entr.ListaArchivos);
        }

        private void BotonConfigArchivo_Click(object sender, RoutedEventArgs e)
        {
            configuracionArchivo_Seleccionada = (ConfiguracionArchivoEntrada)((Button)sender).Tag;
            MostrarConfiguracionArchivo(((ConfiguracionArchivoEntrada)((Button)sender).Tag));

            if (grillaLecturaArchivo.Visibility == Visibility.Visible &&
                grillaLecturaArchivo.Tag == configuracionArchivo_Seleccionada)
            {
                if (!string.IsNullOrEmpty(configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada))
                {
                    if (configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada.Contains("*") &&
                        Calculo.RutaArchivo != null)
                    {
                        configuracionArchivo_Seleccionada.LlenarComboRutas(rutasArchivosContenido,
                            configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada,
                            Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")));

                        rutasArchivosContenido.Visibility = Visibility.Visible;
                        rutaArchivoContenido.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        rutaArchivoContenido.Text = configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada;
                        rutaArchivoContenido.Visibility = Visibility.Visible;
                        rutasArchivosContenido.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void BotonTextoArchivo_Click(object sender, RoutedEventArgs e)
        {

            configuracionArchivo_Seleccionada = (ConfiguracionArchivoEntrada)((Button)sender).Tag;

            if (!string.IsNullOrEmpty(configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada))
            {
                grillaLecturaArchivo.Visibility = Visibility.Visible;
                grillaLecturaArchivo.Tag = configuracionArchivo_Seleccionada;

                if (configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada.Contains("*") &&
                    Calculo.RutaArchivo != null)
                {
                    configuracionArchivo_Seleccionada.LlenarComboRutas(rutasArchivosContenido,
                        configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada,
                        Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")));

                    rutasArchivosContenido.Visibility = Visibility.Visible;
                    rutaArchivoContenido.Visibility = Visibility.Collapsed;
                }
                else
                {
                    rutaArchivoContenido.Text = configuracionArchivo_Seleccionada.RutaArchivoConjuntoTextosEntrada;
                    rutaArchivoContenido.Visibility = Visibility.Visible;
                    rutasArchivosContenido.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                grillaLecturaArchivo.Visibility = Visibility.Collapsed;
                grillaLecturaArchivo.Tag = null;
            }
        }

        public void MostrarConfiguracionArchivo(ConfiguracionArchivoEntrada archivoConfig)
        {
            ConfiguracionLecturaArchivoEntrada verConfig = new ConfiguracionLecturaArchivoEntrada();
            verConfig.config.Entrada = Entrada;
            verConfig.config.VistaEntrada = VistaEntrada;
            verConfig.config.Calculo = Calculo;
            verConfig.config.VistaArchivoTextosInformacion = this;
            verConfig.config.EntradaArchivo = archivoConfig;            
            verConfig.ShowDialog();

            listaArchivos.Children.Clear();
            CargarListaArchivos(entr.ListaArchivos);
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void btnObtenerTexto_Click(object sender, RoutedEventArgs e)
        {
            if (configuracionArchivo_Seleccionada != null)
            {
                Cursor = Cursors.Wait;

                try
                {
                    contenidoTexto.MaxWidth = contenidoTexto.ActualWidth;

                    contenidoTexto.Text = configuracionArchivo_Seleccionada.ObtenerArchivoTextoPlano_Temporal(entr, Calculo.NombreArchivo,
                        Calculo.RutaArchivo.Remove(Calculo.RutaArchivo.LastIndexOf("\\")), Calculo.NombreArchivo, 
                        rutasArchivosContenido.Visibility == Visibility.Visible ? rutasArchivosContenido.Text : string.Empty);
                }
                catch (Exception error)
                {
                    MessageBox.Show("Hay un problema con la carpeta y el nombre del archivo. Error: " + error.Message + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Cursor = Cursors.Arrow;
            }
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
    }
}
