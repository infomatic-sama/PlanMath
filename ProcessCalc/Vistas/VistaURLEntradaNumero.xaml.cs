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

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaURLEntradaNumero.xaml
    /// </summary>
    public partial class VistaURLEntradaNumero : UserControl
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

                lblNombreEntrada.Content = value.Nombre;
                entr = value;
            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        List<List<int>> NumerosEncontrados = new List<List<int>>();
        int indiceBusquedaNumeros = -1;
        public VistaURLEntradaNumero()
        {
            InitializeComponent();
        }

        private void opcionBuscarTexto_Click(object sender, RoutedEventArgs e)
        {
            if (opcionBuscarTexto.IsChecked == true)
            {
                busquedaNumero.Visibility = Visibility.Visible;
                if (entr != null) entr.OpcionBusquedaNumero = OpcionBusquedaNumero.BusquedaTexto;
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

        public void btnObtenerDeURL_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;

            try
            {
                contenidoURL.MaxWidth = contenidoURL.ActualWidth;

                if (txtURL.Text.Length >= 8)
                {
                    if (!txtURL.Text.Substring(0, 7).ToLower().Equals("http://") &
                    !txtURL.Text.Substring(0, 8).ToLower().Equals("https://"))
                    {
                        txtURL.Text = txtURL.Text.Insert(0, "http://");
                    }

                    ObjetoURL solicitarContenido = new ObjetoURL(txtURL.Text, 
                        entr.ListaURLs.FirstOrDefault().ParametrosURL, entr.ListaURLs.FirstOrDefault().HeadersURL);
                    contenidoURL.Text = solicitarContenido.ObtenerTexto();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Cursor = Cursors.Arrow;
        }

        private void txtURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entr != null)
                entr.ListaURLs.FirstOrDefault().URLEntrada = txtURL.Text;
        }

        private void btnMostrarOcultarParametros_Click(object sender, RoutedEventArgs e)
        {
            if (tituloParametros.Visibility == Visibility.Collapsed)
            {
                if (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count > 0)
                {
                    ListarParametros();
                }
                else
                {
                    AgregarBoton_Agregar();
                }
                tituloParametros.Visibility = Visibility.Visible;
                btnMostrarOcultarParametros.Content = "Ocultar parámetros";                
            }
            else if (tituloParametros.Visibility == Visibility.Visible)
            {
                tituloParametros.Visibility = Visibility.Collapsed;
                btnMostrarOcultarParametros.Content = "Mostrar parámetros";                
            }
        }

        private void AgregarBoton_Agregar()
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            Image ImagenBotonAgregar = new Image();
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\17.png", UriKind.Relative));
            ImagenBotonAgregar.Width = 24;
            ImagenBotonAgregar.Height = 24;
            ImagenBotonAgregar.Tag = "Agregar";

            Button agregar = new Button();
            agregar.Content = ImagenBotonAgregar;
            agregar.Margin = new Thickness(10);
            agregar.Click += AgregarPrimerParametro;
            
            parametros.Children.Add(agregar);

            Grid.SetColumn(agregar, 1);
            Grid.SetRow(agregar, 0);
        }

        private void AgregarPrimerParametro(object sender, RoutedEventArgs e)
        {
            parametros.Children.Clear();
            parametros.RowDefinitions.Clear();

            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            entr.ListaURLs.FirstOrDefault().ParametrosURL.Add(new ParametroURL("Parámetro 1", "Valor 1"));

            ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
            parametro.Parametro = entr.ListaURLs.FirstOrDefault().ParametrosURL.First();

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, 0);

            AgregarBotones(0, entr.ListaURLs.FirstOrDefault().ParametrosURL.First());
        }

        private void AgregarParametro(object sender, RoutedEventArgs e)
        {
            int indiceParametro = (int)((object[])((Button)sender).Tag)[0];

            ParametroSolicitudURL_Entrada ctrlParametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                          where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                 && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceParametro]
                                                                                          select E).FirstOrDefault();

            if (ctrlParametro.Parametro.EsConjuntoParametros)
            {
                for (int indiceActual = indiceParametro; indiceActual < entr.ListaURLs.FirstOrDefault().ParametrosURL.Count; indiceActual++)
                {
                    if (entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].ConjuntoParametros != ctrlParametro.Parametro)
                    {
                        indiceActual++;
                        parametros.RowDefinitions.Insert(indiceActual, new RowDefinition());
                        parametros.RowDefinitions[indiceActual].Height = GridLength.Auto;

                        ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count + 1).ToString()
                            , "Valor " + (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count + 1).ToString(), ctrlParametro.Parametro);

                        entr.ListaURLs.FirstOrDefault().ParametrosURL.Insert(indiceActual, nuevoParametro);

                        ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                        parametro.Parametro = nuevoParametro;

                        parametros.Children.Insert(indiceActual, parametro);

                        Grid.SetColumn(parametro, 0);
                        Grid.SetRow(parametro, indiceActual);

                        AgregarBotones(indiceActual, nuevoParametro);

                        for (int indiceDesplazar = entr.ListaURLs.FirstOrDefault().ParametrosURL.Count - 1; indiceDesplazar > indiceActual; indiceDesplazar--)
                        {
                            ParametroSolicitudURL_Entrada ctrlParametroDesplazar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                                   where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                                          && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceDesplazar]
                                                                                                                   select E).FirstOrDefault();
                            Grid.SetRow(ctrlParametroDesplazar, indiceDesplazar);

                            List<Button> botones = (from UIElement E in parametros.Children
                                                    where E.GetType() == typeof(Button)
           && (ParametroURL)((object[])((Button)E).Tag)[1] == entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceDesplazar]
                                                    select (Button)E).ToList();

                            foreach (var boton in botones)
                            {
                                Grid.SetRow(boton, indiceDesplazar);
                                ((object[])boton.Tag)[0] = indiceDesplazar;
                            }
                        }

                        break;
                    }
                }
            }
            else if (ctrlParametro.Parametro.EsParametroEnConjunto)
            {
                parametros.RowDefinitions.Insert(indiceParametro, new RowDefinition());
                parametros.RowDefinitions[indiceParametro].Height = GridLength.Auto;

                ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count + 1).ToString()
                    , "Valor " + (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count + 1).ToString(), ctrlParametro.Parametro.ConjuntoParametros);

                entr.ListaURLs.FirstOrDefault().ParametrosURL.Insert(indiceParametro, nuevoParametro);

                ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                parametro.Parametro = nuevoParametro;

                parametros.Children.Insert(indiceParametro, parametro);

                Grid.SetColumn(parametro, 0);
                Grid.SetRow(parametro, indiceParametro);

                AgregarBotones(indiceParametro, nuevoParametro);

                for (int indiceDesplazar = entr.ListaURLs.FirstOrDefault().ParametrosURL.Count - 1; indiceDesplazar > indiceParametro; indiceDesplazar--)
                {
                    ParametroSolicitudURL_Entrada ctrlParametroDesplazar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                           where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                                  && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceDesplazar]
                                                                                                           select E).FirstOrDefault();
                    Grid.SetRow(ctrlParametroDesplazar, indiceDesplazar);

                    List<Button> botones = (from UIElement E in parametros.Children
                                            where E.GetType() == typeof(Button)
   && (ParametroURL)((object[])((Button)E).Tag)[1] == entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceDesplazar]
                                            select (Button)E).ToList();

                    foreach (var boton in botones)
                    {
                        Grid.SetRow(boton, indiceDesplazar);
                        ((object[])boton.Tag)[0] = indiceDesplazar;
                    }
                }

            }
            else
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count + 1).ToString()
                    , "Valor " + (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count + 1).ToString());

                int indice = entr.ListaURLs.FirstOrDefault().ParametrosURL.Count;

                entr.ListaURLs.FirstOrDefault().ParametrosURL.Add(nuevoParametro);

                ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                parametro.Parametro = nuevoParametro;

                parametros.Children.Add(parametro);

                Grid.SetColumn(parametro, 0);
                Grid.SetRow(parametro, indice);

                AgregarBotones(indice, nuevoParametro);
            }
        }

        private void EstablecerParametros_Parametro(ParametroURL parametro,
            ParametroURL parametroAnterior)
        {
            parametro.ConjuntoParametros = null;
            parametro.EsParametroEnConjunto = false;

            if (parametroAnterior != null)
            {
                //if (parametroAnterior.Nivel <= parametro.Nivel)
                //{
                //    if (parametroAnterior.EsParametroEnConjunto ||
                //        parametroAnterior.EsConjuntoParametros)
                //    {
                //        //if (parametrosConjuntos.Any())
                //            parametro.ConjuntoParametros = parametrosConjuntos;
                //    }
                //}
                //else if(parametroAnterior.Nivel > parametro.Nivel)
                //{
                //    if(parametroAnterior.ConjuntoParametros != null &&
                //        parametroAnterior.ConjuntoParametros.EsConjuntoParametros)
                //    {
                //        //if (parametrosConjuntos.Any())
                //            parametro.ConjuntoParametros = parametrosConjuntos;
                //    }
                //}

                if (parametroAnterior.Nivel == parametro.Nivel)
                {
                    if (parametroAnterior.EsParametroEnConjunto &&
                        !parametroAnterior.EsConjuntoParametros)
                    {
                        //if (parametrosConjuntos.Any())
                        parametro.ConjuntoParametros = parametroAnterior.ConjuntoParametros;
                    }
                    else if (parametroAnterior.EsConjuntoParametros)
                    {
                        parametro.ConjuntoParametros = parametroAnterior;
                    }
                }
                else if (parametroAnterior.Nivel < parametro.Nivel)
                {
                    if (parametroAnterior.EsConjuntoParametros)
                    {
                        parametro.ConjuntoParametros = parametroAnterior;
                    }
                }
                else if (parametroAnterior.Nivel > parametro.Nivel)
                {
                    if (parametroAnterior.ConjuntoParametros != null &&
                        parametroAnterior.ConjuntoParametros.ConjuntoParametros != null &&
                        parametroAnterior.ConjuntoParametros.ConjuntoParametros.EsConjuntoParametros)
                    {
                        //if (parametrosConjuntos.Any())
                        parametro.ConjuntoParametros = parametroAnterior.ConjuntoParametros.ConjuntoParametros;
                    }
                }
            }

            if (parametro.ConjuntoParametros != null)
            {
                parametro.EsParametroEnConjunto = true;
            }
            else
                parametro.EsParametroEnConjunto = false;
        }

        private void MoverIzquierdaParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            if (indice > 0)
            {

                ParametroSolicitudURL_Entrada parametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                          where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                 && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice]
                                                                                          select E).FirstOrDefault();

                ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice - 1]
                                                                                                  select E).FirstOrDefault();

                if (parametro.Parametro.Nivel > 0)
                {

                    //for (int indiceLista = indice + 1; indiceLista <= entr.ParametrosURL.Count - 1; indiceLista++)
                    //{
                    //    if (entr.ParametrosURL[indiceLista].Nivel <= parametro.Parametro.Nivel)
                    //        break;

                    //    entr.ParametrosURL[indiceLista].Nivel--;
                    //}

                    parametro.Parametro.Nivel--;

                    parametro.Parametro.ConjuntoParametros = null;

                    if (parametroAnterior.Parametro.EsConjuntoParametros)
                        parametro.Parametro.ConjuntoParametros = parametroAnterior.Parametro;

                    if (parametroAnterior.Parametro.EsParametroEnConjunto &&
                        parametroAnterior.Parametro.Nivel == parametro.Parametro.Nivel)
                        parametro.Parametro.ConjuntoParametros = parametroAnterior.Parametro.ConjuntoParametros;

                    if (parametro.Parametro.ConjuntoParametros != null)
                        parametro.Parametro.EsParametroEnConjunto = true;
                    else
                        parametro.Parametro.EsParametroEnConjunto = false;


                    if (parametro.Parametro.EsConjuntoParametros)
                    {
                        for (int indiceActual = indice + 1; indiceActual < entr.ListaURLs.FirstOrDefault().ParametrosURL.Count; indiceActual++)
                        {
                            if (entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].ConjuntoParametros != entr.ListaURLs.FirstOrDefault().ParametrosURL[indice])
                            {
                                if (entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].Nivel < parametro.Parametro.Nivel)
                                    break;

                                entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].Nivel--;
                            }
                            else
                                entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].Nivel--;
                        }
                    }

                    ListarParametros();

                }
            }
        }

        private void MoverDerechaParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            if (indice > 0 & indice < entr.ListaURLs.FirstOrDefault().ParametrosURL.Count)
            {

                ParametroSolicitudURL_Entrada parametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                          where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                 && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice]
                                                                                          select E).FirstOrDefault();

                ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice - 1]
                                                                                                  select E).FirstOrDefault();
                if (parametro.Parametro.Nivel < parametroAnterior.Parametro.Nivel + 1)
                {
                    parametro.Parametro.Nivel++;

                    parametro.Parametro.ConjuntoParametros = null;

                    if (parametroAnterior.Parametro.EsConjuntoParametros)
                        parametro.Parametro.ConjuntoParametros = parametroAnterior.Parametro;

                    if (parametroAnterior.Parametro.EsParametroEnConjunto &&
                        parametroAnterior.Parametro.Nivel == parametro.Parametro.Nivel)
                        parametro.Parametro.ConjuntoParametros = parametroAnterior.Parametro.ConjuntoParametros;

                    if (parametro.Parametro.ConjuntoParametros != null)
                        parametro.Parametro.EsParametroEnConjunto = true;
                    else
                        parametro.Parametro.EsParametroEnConjunto = false;

                    if (parametro.Parametro.EsConjuntoParametros)
                    {
                        for (int indiceActual = indice + 1; indiceActual < entr.ListaURLs.FirstOrDefault().ParametrosURL.Count; indiceActual++)
                        {
                            if (entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].ConjuntoParametros != entr.ListaURLs.FirstOrDefault().ParametrosURL[indice])
                            {
                                if (entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].Nivel < parametro.Parametro.Nivel)
                                    break;

                                entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].Nivel++;
                            }
                            else
                                entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceActual].Nivel++;
                        }
                    }

                    ListarParametros();

                }
            }
        }

        private void QuitarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            ParametroSolicitudURL_Entrada parametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                       where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
              && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice]
                                                       select E).FirstOrDefault();


            parametros.Children.Remove(parametro);
            QuitarBotones(indice);
            entr.ListaURLs.FirstOrDefault().ParametrosURL.RemoveAt(indice);
            SetearIndices_Siguientes(indice);

            if (entr.ListaURLs.FirstOrDefault().ParametrosURL.Count == 0) AgregarBoton_Agregar();
        }

        private void SubirParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            if (indice == 0) return;

            ParametroSolicitudURL_Entrada ctrlParametroASubir = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice]
                                                                                                select E).FirstOrDefault();

            ParametroSolicitudURL_Entrada ctrlParametroABajar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice - 1]
                                                                                                select E).FirstOrDefault();

            int indiceSubir = indice;

            int indiceBajar = indiceSubir - 1;

            ParametroURL parametroASubir = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceSubir];
            ParametroURL parametroABajar = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceBajar];

            bool esConjuntoParametros_Subir = parametroASubir.EsConjuntoParametros;
            bool esConjuntoParametros_Bajar = parametroABajar.EsConjuntoParametros;

            parametroABajar.EsConjuntoParametros = esConjuntoParametros_Subir;
            parametroASubir.EsConjuntoParametros = esConjuntoParametros_Bajar;

            int nivelParametroASubir = parametroASubir.Nivel;
            int nivelParametroABajar = parametroABajar.Nivel;

            entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceSubir] = parametroABajar;
            entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceBajar] = parametroASubir;

            ctrlParametroASubir.Parametro = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceSubir];
            ctrlParametroABajar.Parametro = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceBajar];

            ctrlParametroASubir.Parametro.Nivel = nivelParametroASubir;
            ctrlParametroABajar.Parametro.Nivel = nivelParametroABajar;

            EstablecerParametros_Parametros();
            ListarParametros();

            //SetearIndices_Siguientes(indice - 1);
            //ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ParametrosURL[indice - 1]
            //                                                                                  select E).FirstOrDefault();
            //if (ctrlParametroASubir.Parametro.Nivel > ctrlParametroABajar.Parametro.Nivel + 1)
            //{
            //    ctrlParametroASubir.Parametro.Nivel = ctrlParametroABajar.Parametro.Nivel + 1;
            //    ListarParametros();
            //}

            //parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ParametrosURL[indice]
            //                                                                                  select E).FirstOrDefault();
            //if (ctrlParametroABajar.Parametro.Nivel > parametroAnterior.Parametro.Nivel + 1)
            //    ctrlParametroABajar.Parametro.Nivel = parametroAnterior.Parametro.Nivel + 1;
        }

        private void BajarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            if (indice == entr.ListaURLs.FirstOrDefault().ParametrosURL.Count - 1) return;

            ParametroSolicitudURL_Entrada ctrlParametroABajar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice]
                                                                                                select E).FirstOrDefault();

            ParametroSolicitudURL_Entrada ctrlParametroASubir = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ListaURLs.FirstOrDefault().ParametrosURL[indice + 1]
                                                                                                select E).FirstOrDefault();


            int indiceBajar = indice;

            int indiceSubir = indiceBajar + 1;

            ParametroURL parametroABajar = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceBajar];
            ParametroURL parametroASubir = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceSubir];

            bool esConjuntoParametros_Subir = parametroASubir.EsConjuntoParametros;
            bool esConjuntoParametros_Bajar = parametroABajar.EsConjuntoParametros;

            parametroABajar.EsConjuntoParametros = esConjuntoParametros_Subir;
            parametroASubir.EsConjuntoParametros = esConjuntoParametros_Bajar;

            int nivelParametroASubir = parametroASubir.Nivel;
            int nivelParametroABajar = parametroABajar.Nivel;

            entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceSubir] = parametroABajar;
            entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceBajar] = parametroASubir;

            ctrlParametroABajar.Parametro = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceBajar];
            ctrlParametroASubir.Parametro = entr.ListaURLs.FirstOrDefault().ParametrosURL[indiceSubir];

            ctrlParametroASubir.Parametro.Nivel = nivelParametroASubir;
            ctrlParametroABajar.Parametro.Nivel = nivelParametroABajar;

            EstablecerParametros_Parametros();
            ListarParametros();

            //SetearIndices_Siguientes(indice);
            //ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ParametrosURL[indice - 1]
            //                                                                                  select E).FirstOrDefault();
            //if (ctrlParametroASubir.Parametro.Nivel > ctrlParametroABajar.Parametro.Nivel + 1)
            //{
            //    ctrlParametroASubir.Parametro.Nivel = ctrlParametroABajar.Parametro.Nivel + 1;
            //    ListarParametros();
            //}

            //ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == entr.ParametrosURL[indice - 1]
            //                                                                                  select E).FirstOrDefault();

            //if (ctrlParametroASubir.Parametro.Nivel > parametroAnterior.Parametro.Nivel + 1)
            //    ctrlParametroASubir.Parametro.Nivel = parametroAnterior.Parametro.Nivel + 1;
        }

        private void SetearIndices_Siguientes(int indice)
        {
            for (int indiceSiguiente = indice; indiceSiguiente <= entr.ListaURLs.FirstOrDefault().ParametrosURL.Count - 1; indiceSiguiente++)
            {
                Button agregar = (Button)(from UIElement E in parametros.Children
                                          where E.GetType() == typeof(Button)
    && (int)((object[])((Button)E).Tag)[0] == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Agregar"
                                          select E).FirstOrDefault();

                ((object[])agregar.Tag)[0] = indiceSiguiente;

                Button moverIzquierda = (Button)(from UIElement E in parametros.Children
                                                 where E.GetType() == typeof(Button)
            && (int)((object[])((Button)E).Tag)[0] == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "MoverIzquierda"
                                                 select E).FirstOrDefault();

                ((object[])moverIzquierda.Tag)[0] = indiceSiguiente;

                Button moverDerecha = (Button)(from UIElement E in parametros.Children
                                               where E.GetType() == typeof(Button)
          && (int)((object[])((Button)E).Tag)[0] == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "MoverDerecha"
                                               select E).FirstOrDefault();

                ((object[])moverDerecha.Tag)[0] = indiceSiguiente;

                Button quitar = (Button)(from UIElement E in parametros.Children
                                         where E.GetType() == typeof(Button)
    && (int)((object[])((Button)E).Tag)[0] == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Quitar"
                                         select E).FirstOrDefault();

                ((object[])quitar.Tag)[0] = indiceSiguiente;

                Button subir = (Button)(from UIElement E in parametros.Children
                                        where E.GetType() == typeof(Button)
   && (int)((object[])((Button)E).Tag)[0] == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Subir"
                                        select E).FirstOrDefault();

                ((object[])subir.Tag)[0] = indiceSiguiente;

                Button bajar = (Button)(from UIElement E in parametros.Children
                                        where E.GetType() == typeof(Button)
   && (int)((object[])((Button)E).Tag)[0] == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Bajar"
                                        select E).FirstOrDefault();

                ((object[])bajar.Tag)[0] = indiceSiguiente;
            }
        }

        public void ListarParametros()
        {
            parametros.Children.Clear();

            int indice = 0;
            foreach (var itemParametro in entr.ListaURLs.FirstOrDefault().ParametrosURL)
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                parametro.VistaEntradaNumero = this;
                parametro.Parametro = itemParametro;

                parametros.Children.Add(parametro);

                Grid.SetColumn(parametro, 0);
                Grid.SetRow(parametro, indice);

                AgregarBotones(indice, itemParametro);

                indice++;
            }
        }

        private void EstablecerParametros_Parametros()
        {
            int indice = 0;

            foreach (var itemParametro in entr.ListaURLs.FirstOrDefault().ParametrosURL)
            {

                EstablecerParametros_Parametro(itemParametro, (indice > 0) ? entr.ListaURLs.FirstOrDefault().ParametrosURL[indice - 1] : null);//, parametrosConjuntos);
                indice++;
            }
        }

        private void AgregarBotones(int indice, ParametroURL parametro)
        {
            Image ImagenBotonAgregar = new Image();
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\17.png", UriKind.Relative));
            ImagenBotonAgregar.Width = 24;
            ImagenBotonAgregar.Height = 24;
            ImagenBotonAgregar.Tag = "Agregar";

            Button agregar = new Button();
            agregar.Content = ImagenBotonAgregar;
            agregar.Margin = new Thickness(10);
            agregar.Tag = new object[] { indice, parametro };
            agregar.Click += AgregarParametro;
            
            parametros.Children.Add(agregar);

            Grid.SetColumn(agregar, 1);
            Grid.SetRow(agregar, indice);

            Image ImagenBotonMoverIzquierda = new Image();
            ImagenBotonMoverIzquierda.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_02.png", UriKind.Relative));
            ImagenBotonMoverIzquierda.Width = 24;
            ImagenBotonMoverIzquierda.Height = 24;
            ImagenBotonMoverIzquierda.Tag = "MoverIzquierda";

            Button moverIzquierda = new Button();
            moverIzquierda.Content = ImagenBotonMoverIzquierda;
            moverIzquierda.Margin = new Thickness(10);
            moverIzquierda.Tag = new object[] { indice, parametro };
            moverIzquierda.Click += MoverIzquierdaParametro;

            parametros.Children.Add(moverIzquierda);

            Grid.SetColumn(moverIzquierda, 2);
            Grid.SetRow(moverIzquierda, indice);

            Image ImagenBotonMoverDerecha = new Image();
            ImagenBotonMoverDerecha.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_01.png", UriKind.Relative));
            ImagenBotonMoverDerecha.Width = 24;
            ImagenBotonMoverDerecha.Height = 24;
            ImagenBotonMoverDerecha.Tag = "MoverDerecha";

            Button moverDerecha = new Button();
            moverDerecha.Content = ImagenBotonMoverDerecha;
            moverDerecha.Margin = new Thickness(10);
            moverDerecha.Tag = new object[] { indice, parametro };
            moverDerecha.Click += MoverDerechaParametro;

            parametros.Children.Add(moverDerecha);

            Grid.SetColumn(moverDerecha, 3);
            Grid.SetRow(moverDerecha, indice);

            Image ImagenBotonQuitar = new Image();
            ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\18.png", UriKind.Relative));
            ImagenBotonQuitar.Width = 24;
            ImagenBotonQuitar.Height = 24;
            ImagenBotonQuitar.Tag = "Quitar";

            Button quitar = new Button();
            quitar.Content = ImagenBotonQuitar;
            quitar.Margin = new Thickness(10);
            quitar.Tag = new object[] { indice, parametro };
            quitar.Click += QuitarParametro;
            
            parametros.Children.Add(quitar);

            Grid.SetColumn(quitar, 4);
            Grid.SetRow(quitar, indice);

            Image ImagenBotonSubir = new Image();
            ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\19.png", UriKind.Relative));
            ImagenBotonSubir.Width = 24;
            ImagenBotonSubir.Height = 24;
            ImagenBotonSubir.Tag = "Subir";

            Button subir = new Button();
            subir.Content = ImagenBotonSubir;
            subir.Margin = new Thickness(10);
            subir.Tag = new object[] { indice, parametro };
            subir.Click += SubirParametro;
            
            parametros.Children.Add(subir);

            Grid.SetColumn(subir, 5);
            Grid.SetRow(subir, indice);

            Image ImagenBotonBajar = new Image();
            ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\20.png", UriKind.Relative));
            ImagenBotonBajar.Width = 24;
            ImagenBotonBajar.Height = 24;
            ImagenBotonBajar.Tag = "Bajar";

            Button bajar = new Button();
            bajar.Content = ImagenBotonBajar;
            bajar.Margin = new Thickness(10);
            bajar.Tag = new object[] { indice, parametro };
            bajar.Click += BajarParametro;
            
            parametros.Children.Add(bajar);

            Grid.SetColumn(bajar, 6);
            Grid.SetRow(bajar, indice);
        }

        private void QuitarBotones(int indice)
        {
            Button agregar = (Button)(from UIElement E in parametros.Children
                                      where E.GetType() == typeof(Button)
&& (int)((object[])((Button)E).Tag)[0] == indice && ((Image)((Button)E).Content).Tag.ToString() == "Agregar"
                                      select E).FirstOrDefault();

            parametros.Children.Remove(agregar);

            Button moverIzquierda = (Button)(from UIElement E in parametros.Children
                                             where E.GetType() == typeof(Button)
        && (int)((object[])((Button)E).Tag)[0] == indice && ((Image)((Button)E).Content).Tag.ToString() == "MoverIzquierda"
                                             select E).FirstOrDefault();

            parametros.Children.Remove(moverIzquierda);

            Button moverDerecha = (Button)(from UIElement E in parametros.Children
                                           where E.GetType() == typeof(Button)
      && (int)((object[])((Button)E).Tag)[0] == indice && ((Image)((Button)E).Content).Tag.ToString() == "MoverDerecha"
                                           select E).FirstOrDefault();

            parametros.Children.Remove(moverDerecha);

            Button quitar = (Button)(from UIElement E in parametros.Children
                                     where E.GetType() == typeof(Button)
&& (int)((object[])((Button)E).Tag)[0] == indice && ((Image)((Button)E).Content).Tag.ToString() == "Quitar"
                                     select E).FirstOrDefault();

            parametros.Children.Remove(quitar);

            Button subir = (Button)(from UIElement E in parametros.Children
                                    where E.GetType() == typeof(Button)
&& (int)((object[])((Button)E).Tag)[0] == indice && ((Image)((Button)E).Content).Tag.ToString() == "Subir"
                                    select E).FirstOrDefault();

            parametros.Children.Remove(subir);

            Button bajar = (Button)(from UIElement E in parametros.Children
                                    where E.GetType() == typeof(Button)
&& (int)((object[])((Button)E).Tag)[0] == indice && ((Image)((Button)E).Content).Tag.ToString() == "Bajar"
                                    select E).FirstOrDefault();

            parametros.Children.Remove(bajar);
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

            foreach (var caracter in contenidoURL.Text)
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
                            NumerosEncontrados.Add( new List<int>() { numeroEncontrado[0], numeroEncontrado[1] });
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
            contenidoURL.Select(NumerosEncontrados[indiceBusquedaNumeros][0], NumerosEncontrados[indiceBusquedaNumeros][1]);
            contenidoURL.Focus();
        }

        private void PosicionarNumero_Siguiente()
        {
            if (indiceBusquedaNumeros == NumerosEncontrados.Count - 1)
            {
                contenidoURL.Focus();
                return;
            }
            indiceBusquedaNumeros++;

            SeleccionarNumero_Actual();
        }

        private void PosicionarNumero_Anterior()
        {
            if (indiceBusquedaNumeros == 0)
            {
                contenidoURL.Focus();
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

        private void MostrarTextoConfiguracion_EscribirURL()
        {
            if (entr != null)
            {
                string strArchivo = string.Empty;

                switch (entr.ListaURLs.FirstOrDefault().ConfiguracionEscribirURL)
                {
                    case OpcionEscribirURLEntrada.UtilizarURLIndicada:
                        strArchivo = "Utilizar la URL indicada.";
                        break;

                    case OpcionEscribirURLEntrada.EscribirURLEjecucion:
                        strArchivo = "Escribir la URL, en el momento de la ejecución del archivo de cálculo.";
                        break;

                    case OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada:
                        strArchivo = "Escribir URL, según lo indique la variable o vector de entrada en las variables o vectores, en el momento de la ejecución del archivo de cálculo.";
                        break;
                }

                etiquetaDefinicionEscribirURL.Text = "Definición actual: " + strArchivo;
            }
            else
                etiquetaDefinicionEscribirURL.Text = "";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            MostrarTextoConfiguracion_EscribirURL();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirConfiguracionEntradaURLCalculo");
#endif
        }

        private void configurarEscribirURL_Click(object sender, RoutedEventArgs e)
        {
            ConfigURLEntrada configurar = new ConfigURLEntrada();
            configurar.Config = entr.ListaURLs.FirstOrDefault();
            configurar.ShowDialog();
            MostrarTextoConfiguracion_EscribirURL();
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

        private void btnDefinirHeaders_Click(object sender, RoutedEventArgs e)
        {
            EstablecerHeadersURL_Ejecucion establecerHeaders = new EstablecerHeadersURL_Ejecucion();
            establecerHeaders.url.Text = txtURL.Text;
            establecerHeaders.Headers = entr.ListaURLs.FirstOrDefault().HeadersURL;
            establecerHeaders.ShowDialog();
        }

        private void enviarParametrosEnUrl_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entr != null)
                {
                    foreach (var param in entr.ListaURLs.FirstOrDefault().ParametrosURL)
                        param.ParametrosEnUrl = (bool)enviarParametrosEnUrl.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void enviarParametrosEnUrl_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entr != null)
                {
                    foreach (var param in entr.ListaURLs.FirstOrDefault().ParametrosURL)
                        param.ParametrosEnUrl = (bool)enviarParametrosEnUrl.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void enviarParametrosEnBody_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entr != null)
                {
                    foreach (var param in entr.ListaURLs.FirstOrDefault().ParametrosURL)
                        param.ParametrosEnBody = (bool)enviarParametrosEnBody.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void enviarParametrosEnBody_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (entr != null)
                {
                    foreach (var param in entr.ListaURLs.FirstOrDefault().ParametrosURL)
                        param.ParametrosEnBody = (bool)enviarParametrosEnBody.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
