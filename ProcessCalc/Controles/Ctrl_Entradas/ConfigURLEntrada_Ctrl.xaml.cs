using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.OrigenesDatos;
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
    /// Lógica de interacción para ConfigURLEntrada.xaml
    /// </summary>
    public partial class ConfigURLEntrada_Ctrl : UserControl
    {
        private ConfiguracionURLEntrada config;
        public ConfiguracionURLEntrada ConfiguracionURL
        {
            get
            {
                return config;
            }

            set
            {
                //opcionMismaLectura.IsChecked = value.MismaLecturaBusquedasArchivo;
                //if (!value.MismaLecturaBusquedasArchivo)
                //    opcionLecturasDistintas.IsChecked = true;

                config = value;

                txtURL.Text = config.URLEntrada;
                MostrarTextoConfiguracion_EscribirURL();
            }
        }
        private Entrada entr;
        public Entrada Entrada
        {
            get
            {
                return entr;
            }

            set
            {
                //opcionMismaLectura.IsChecked = value.MismaLecturaBusquedasArchivo;
                //if (!value.MismaLecturaBusquedasArchivo)
                //    opcionLecturasDistintas.IsChecked = true;

                entr = value;
            }
        }

        public VistaURLEntradaConjuntoNumeros VistaEntradaConjuntoNumeros { get; set; }
        public ConfigURLEntrada_Ctrl()
        {
            InitializeComponent();
        }

        private void configurarEscribirURL_Click(object sender, RoutedEventArgs e)
        {            
            ConfigURLEntrada configurar = new ConfigURLEntrada();
            configurar.Config = config;
            configurar.ShowDialog();
            MostrarTextoConfiguracion_EscribirURL();            
        }

        private void txtURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (config != null)
                config.URLEntrada = txtURL.Text;
        }

        private void btnMostrarOcultarParametros_Click(object sender, RoutedEventArgs e)
        {
            if (tituloParametros.Visibility == Visibility.Collapsed)
            {
                if (config.ParametrosURL.Count > 0)
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

        private void btnDefinirHeaders_Click(object sender, RoutedEventArgs e)
        {
            EstablecerHeadersURL_Ejecucion establecerHeaders = new EstablecerHeadersURL_Ejecucion();
            establecerHeaders.url.Text = txtURL.Text;
            establecerHeaders.Headers = config.HeadersURL;
            establecerHeaders.ShowDialog();
        }

        private void MostrarTextoConfiguracion_EscribirURL()
        {            
            if (config != null)
            {
                string strArchivo = string.Empty;

                switch (config.ConfiguracionEscribirURL)
                {
                    case OpcionEscribirURLEntrada.UtilizarURLIndicada:
                        strArchivo = "Utilizar la URL indicada.";
                        break;

                    case OpcionEscribirURLEntrada.EscribirURLEjecucion:
                        strArchivo = "Escribir la URL, en el momento de la ejecución del archivo de cálculo.";
                        break;

                    case OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada:
                        strArchivo = "Escribir URL, según lo indique la entrada en las operaciones, en el momento de la ejecución del archivo de cálculo.";
                        break;
                }

                etiquetaDefinicionEscribirURL.Text = "Definición actual: " + strArchivo;
            }
            else
                etiquetaDefinicionEscribirURL.Text = "";
        }

        private void enviarParametrosEnUrl_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (config != null)
                {
                    foreach (var param in config.ParametrosURL)
                        param.ParametrosEnUrl = (bool)enviarParametrosEnUrl.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void enviarParametrosEnUrl_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (config != null)
                {
                    foreach (var param in config.ParametrosURL)
                        param.ParametrosEnUrl = (bool)enviarParametrosEnUrl.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void enviarParametrosEnBody_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (config != null)
                {
                    foreach (var param in config.ParametrosURL)
                        param.ParametrosEnBody = (bool)enviarParametrosEnBody.IsChecked;

                    ListarParametros();
                }
            }
        }

        private void enviarParametrosEnBody_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (config != null)
                {
                    foreach (var param in config.ParametrosURL)
                        param.ParametrosEnBody = (bool)enviarParametrosEnBody.IsChecked;

                    ListarParametros();
                }
            }
        }

        public void ListarParametros()
        {
            parametros.Children.Clear();

            int indice = 0;
            foreach (var itemParametro in config.ParametrosURL)
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                parametro.VistaConfiguracion = this;
                parametro.Parametro = itemParametro;

                parametros.Children.Add(parametro);

                Grid.SetColumn(parametro, 0);
                Grid.SetRow(parametro, indice);

                AgregarBotones(indice, itemParametro);

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

            config.ParametrosURL.Add(new ParametroURL("Parámetro 1", "Valor 1"));

            ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
            parametro.Parametro = config.ParametrosURL.First();

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, 0);

            AgregarBotones(0, config.ParametrosURL.First());
        }

        private void AgregarParametro(object sender, RoutedEventArgs e)
        {
            int indiceParametro = (int)((object[])((Button)sender).Tag)[0];

            ParametroSolicitudURL_Entrada ctrlParametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                          where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                 && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indiceParametro]
                                                                                          select E).FirstOrDefault();

            if (ctrlParametro.Parametro.EsConjuntoParametros)
            {
                for (int indiceActual = indiceParametro; indiceActual < config.ParametrosURL.Count; indiceActual++)
                {
                    if (config.ParametrosURL[indiceActual].ConjuntoParametros != ctrlParametro.Parametro)
                    {
                        indiceActual++;
                        parametros.RowDefinitions.Insert(indiceActual, new RowDefinition());
                        parametros.RowDefinitions[indiceActual].Height = GridLength.Auto;

                        ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (config.ParametrosURL.Count + 1).ToString()
                            , "Valor " + (config.ParametrosURL.Count + 1).ToString(), ctrlParametro.Parametro);

                        config.ParametrosURL.Insert(indiceActual, nuevoParametro);

                        ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                        parametro.Parametro = nuevoParametro;

                        parametros.Children.Insert(indiceActual, parametro);

                        Grid.SetColumn(parametro, 0);
                        Grid.SetRow(parametro, indiceActual);

                        AgregarBotones(indiceActual, nuevoParametro);

                        for (int indiceDesplazar = config.ParametrosURL.Count - 1; indiceDesplazar > indiceActual; indiceDesplazar--)
                        {
                            ParametroSolicitudURL_Entrada ctrlParametroDesplazar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                                   where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                                          && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indiceDesplazar]
                                                                                                                   select E).FirstOrDefault();
                            Grid.SetRow(ctrlParametroDesplazar, indiceDesplazar);

                            List<Button> botones = (from UIElement E in parametros.Children
                                                    where E.GetType() == typeof(Button)
           && (ParametroURL)((object[])((Button)E).Tag)[1] == config.ParametrosURL[indiceDesplazar]
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

                ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (config.ParametrosURL.Count + 1).ToString()
                    , "Valor " + (config.ParametrosURL.Count + 1).ToString(), ctrlParametro.Parametro.ConjuntoParametros);

                config.ParametrosURL.Insert(indiceParametro, nuevoParametro);

                ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                parametro.Parametro = nuevoParametro;

                parametros.Children.Insert(indiceParametro, parametro);

                Grid.SetColumn(parametro, 0);
                Grid.SetRow(parametro, indiceParametro);

                AgregarBotones(indiceParametro, nuevoParametro);

                for (int indiceDesplazar = config.ParametrosURL.Count - 1; indiceDesplazar > indiceParametro; indiceDesplazar--)
                {
                    ParametroSolicitudURL_Entrada ctrlParametroDesplazar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                           where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                                  && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indiceDesplazar]
                                                                                                           select E).FirstOrDefault();
                    Grid.SetRow(ctrlParametroDesplazar, indiceDesplazar);

                    List<Button> botones = (from UIElement E in parametros.Children
                                            where E.GetType() == typeof(Button)
   && (ParametroURL)((object[])((Button)E).Tag)[1] == config.ParametrosURL[indiceDesplazar]
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

                ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (config.ParametrosURL.Count + 1).ToString()
                    , "Valor " + (config.ParametrosURL.Count + 1).ToString());

                int indice = config.ParametrosURL.Count;

                config.ParametrosURL.Add(nuevoParametro);

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
                                                 && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice]
                                                                                          select E).FirstOrDefault();

                ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice - 1]
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
                        for (int indiceActual = indice + 1; indiceActual < config.ParametrosURL.Count; indiceActual++)
                        {
                            if (config.ParametrosURL[indiceActual].ConjuntoParametros != config.ParametrosURL[indice])
                            {
                                if (config.ParametrosURL[indiceActual].Nivel < parametro.Parametro.Nivel)
                                    break;

                                config.ParametrosURL[indiceActual].Nivel--;
                            }
                            else
                                config.ParametrosURL[indiceActual].Nivel--;
                        }
                    }

                    ListarParametros();

                }
            }
        }

        private void MoverDerechaParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            if (indice > 0 & indice < config.ParametrosURL.Count)
            {

                ParametroSolicitudURL_Entrada parametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                          where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                 && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice]
                                                                                          select E).FirstOrDefault();

                ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice - 1]
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
                        for (int indiceActual = indice + 1; indiceActual < config.ParametrosURL.Count; indiceActual++)
                        {
                            if (config.ParametrosURL[indiceActual].ConjuntoParametros != config.ParametrosURL[indice])
                            {
                                if (config.ParametrosURL[indiceActual].Nivel < parametro.Parametro.Nivel)
                                    break;

                                config.ParametrosURL[indiceActual].Nivel++;
                            }
                            else
                                config.ParametrosURL[indiceActual].Nivel++;
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
                                             && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice]
                                                                                      select E).FirstOrDefault();


            parametros.Children.Remove(parametro);
            QuitarBotones(indice);
            config.ParametrosURL.RemoveAt(indice);
            SetearIndices_Siguientes(indice);

            if (config.ParametrosURL.Count == 0) AgregarBoton_Agregar();
        }

        private void SubirParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((object[])((Button)sender).Tag)[0];

            if (indice == 0) return;

            ParametroSolicitudURL_Entrada ctrlParametroASubir = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice]
                                                                                                select E).FirstOrDefault();

            ParametroSolicitudURL_Entrada ctrlParametroABajar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice - 1]
                                                                                                select E).FirstOrDefault();

            int indiceSubir = indice;

            int indiceBajar = indiceSubir - 1;

            ParametroURL parametroASubir = config.ParametrosURL[indiceSubir];
            ParametroURL parametroABajar = config.ParametrosURL[indiceBajar];

            bool esConjuntoParametros_Subir = parametroASubir.EsConjuntoParametros;
            bool esConjuntoParametros_Bajar = parametroABajar.EsConjuntoParametros;

            parametroABajar.EsConjuntoParametros = esConjuntoParametros_Subir;
            parametroASubir.EsConjuntoParametros = esConjuntoParametros_Bajar;

            int nivelParametroASubir = parametroASubir.Nivel;
            int nivelParametroABajar = parametroABajar.Nivel;

            config.ParametrosURL[indiceSubir] = parametroABajar;
            config.ParametrosURL[indiceBajar] = parametroASubir;

            ctrlParametroASubir.Parametro = config.ParametrosURL[indiceSubir];
            ctrlParametroABajar.Parametro = config.ParametrosURL[indiceBajar];

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

            if (indice == config.ParametrosURL.Count - 1) return;

            ParametroSolicitudURL_Entrada ctrlParametroABajar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice]
                                                                                                select E).FirstOrDefault();

            ParametroSolicitudURL_Entrada ctrlParametroASubir = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == config.ParametrosURL[indice + 1]
                                                                                                select E).FirstOrDefault();


            int indiceBajar = indice;

            int indiceSubir = indiceBajar + 1;

            ParametroURL parametroABajar = config.ParametrosURL[indiceBajar];
            ParametroURL parametroASubir = config.ParametrosURL[indiceSubir];

            bool esConjuntoParametros_Subir = parametroASubir.EsConjuntoParametros;
            bool esConjuntoParametros_Bajar = parametroABajar.EsConjuntoParametros;

            parametroABajar.EsConjuntoParametros = esConjuntoParametros_Subir;
            parametroASubir.EsConjuntoParametros = esConjuntoParametros_Bajar;

            int nivelParametroASubir = parametroASubir.Nivel;
            int nivelParametroABajar = parametroABajar.Nivel;

            config.ParametrosURL[indiceSubir] = parametroABajar;
            config.ParametrosURL[indiceBajar] = parametroASubir;

            ctrlParametroABajar.Parametro = config.ParametrosURL[indiceBajar];
            ctrlParametroASubir.Parametro = config.ParametrosURL[indiceSubir];

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
            for (int indiceSiguiente = indice; indiceSiguiente <= config.ParametrosURL.Count - 1; indiceSiguiente++)
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

        private void EstablecerParametros_Parametros()
        {
            int indice = 0;
            //ParametroURL parametrosConjuntos = null;
            //int nivelActual = -1;
            //bool cambioNivel = false;
            //ParametroURL parametroAnterior = null;


            foreach (var itemParametro in config.ParametrosURL)
            {

                //if (parametroAnterior != null &&
                //    //parametrosConjuntos != null &&
                //    //nivelActual > 1 &&
                //    (
                //    //(
                //    nivelActual > itemParametro.Nivel //||
                //                                          //itemParametro.ConjuntoParametros != null &&
                //                                          //itemParametro.ConjuntoParametros != parametrosConjuntos &&
                //                                          //parametrosConjuntos != parametroAnterior.ConjuntoParametros &&
                //                                          //itemParametro.ConjuntoParametros != parametroAnterior.ConjuntoParametros) ||
                //                                          //(
                //                                          //nivelActual == parametroAnterior.Nivel //&&
                //                                          //itemParametro.ConjuntoParametros == parametrosConjuntos &&
                //                                          ////parametrosConjuntos.Count > 1 && 
                //                                          //parametrosConjuntos != parametroAnterior)
                //                                          //&& !itemParametro.EsParametroEnConjunto
                //                                          //entr.ParametrosURL[indice - 1].ConjuntoParametros == null &&
                //    ))
                //{
                //    //EstablecerParametros_Parametro(itemParametro, parametroAnterior, parametrosConjuntos);

                //    //if (parametrosConjuntos.Any())
                //    if (itemParametro.EsParametroEnConjunto &&
                //        itemParametro.Nivel != parametroAnterior.Nivel)
                //        parametrosConjuntos = parametroAnterior.ConjuntoParametros;
                //    else if(itemParametro.EsParametroEnConjunto &&
                //        itemParametro.Nivel == parametroAnterior.Nivel)
                //        parametrosConjuntos = parametroAnterior.ConjuntoParametros.ConjuntoParametros;
                //    //else if(itemParametro.EsParametroEnConjunto &&
                //    //    nivelActual == parametroAnterior.Nivel)
                //    //parametrosConjuntos = parametroAnterior;
                //    else
                //        parametrosConjuntos = null;

                //    //parametroAnterior.ConjuntoParametros = parametrosConjuntos.Any() ? parametrosConjuntos.Last() : null;
                //    ////parametroAnterior.EsParametroEnConjunto = true;

                //    //if (parametroAnterior.ConjuntoParametros != null)
                //    //{
                //    //    parametroAnterior.EsParametroEnConjunto = true;
                //    //}
                //    //else
                //    //    parametroAnterior.EsParametroEnConjunto = false;

                //    cambioNivel = true;
                //}

                //if (indice > 0 && parametrosConjuntos.Any() && 
                //    ((nivelActual > entr.ParametrosURL[indice - 1].Nivel &&
                //    itemParametro.ConjuntoParametros != null &&
                //    itemParametro.ConjuntoParametros != parametrosConjuntos.Last() &&
                //    parametrosConjuntos.Last() == entr.ParametrosURL[indice - 1].ConjuntoParametros) ||
                //    (nivelActual == entr.ParametrosURL[indice - 1].Nivel &&
                //    itemParametro.ConjuntoParametros == parametrosConjuntos.Last() &&
                //    //parametrosConjuntos.Count > 1 && 
                //    parametrosConjuntos.Last() != entr.ParametrosURL[indice - 1])
                //    //&& !itemParametro.EsParametroEnConjunto
                //    //entr.ParametrosURL[indice - 1].ConjuntoParametros == null &&
                //    ))
                //{
                //    if (parametrosConjuntos.Any())
                //        parametrosConjuntos.Remove(parametrosConjuntos.Last());

                //    cambioNivel = true;
                //}

                EstablecerParametros_Parametro(itemParametro, (indice > 0) ? config.ParametrosURL[indice - 1] : null);//, parametrosConjuntos);

                indice++;

                //bool aumentoNivel = false;

                //if (nivelActual < itemParametro.Nivel &&
                //itemParametro.EsConjuntoParametros)
                //{
                //    cambioNivel = true;
                //    parametrosConjuntos = itemParametro;
                //    aumentoNivel = true;
                //}



                //if (indice > 0)
                //{
                //    parametroAnterior = itemParametro;
                //}

                //if (!cambioNivel && indice < entr.ParametrosURL.Count - 1 && parametrosConjuntos.Any() &&
                //    ((nivelActual > itemParametro.Nivel &&
                //    entr.ParametrosURL[indice + 1].ConjuntoParametros != null &&
                //    entr.ParametrosURL[indice + 1].ConjuntoParametros != parametrosConjuntos.Last() &&
                //    parametrosConjuntos.Last() == itemParametro.ConjuntoParametros) ||
                //    (nivelActual == itemParametro.Nivel &&
                //    entr.ParametrosURL[indice + 1].ConjuntoParametros == parametrosConjuntos.Last() &&
                //    //parametrosConjuntos.Count > 1 && 
                //    parametrosConjuntos.Last() != itemParametro)
                //    //&& !itemParametro.EsParametroEnConjunto
                //    //entr.ParametrosURL[indice - 1].ConjuntoParametros == null &&
                //    ))
                //{
                //    if (parametrosConjuntos.Any())
                //        parametrosConjuntos.Remove(parametrosConjuntos.Last());

                //    cambioNivel = true;
                //}

                //if (cambioNivel)
                //{
                //    cambioNivel = false;
                //    nivelActual = itemParametro.Nivel > 0 ? itemParametro.Nivel : aumentoNivel ? itemParametro.Nivel : -1;
                //}

                //if (nivelActual == itemParametro.Nivel &&
                //    itemParametro.EsConjuntoParametros)
                //{
                //    if (parametrosConjuntos.Any())
                //        parametrosConjuntos[parametrosConjuntos.Count - 1] = itemParametro;
                //}
            }
        }

        private void opcionEstablecerLecturasNavegaciones_Busquedas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (config != null)
                {
                    config.EstablecerLecturasNavegaciones_Busquedas = (bool)opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked;

                    conjuntoLecturas.ListarLecturasNavegaciones();
                    conjuntoLecturas.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionEstablecerLecturasNavegaciones_Busquedas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (config != null)
                {
                    config.EstablecerLecturasNavegaciones_Busquedas = (bool)opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked;

                    conjuntoLecturas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (config != null)
            {
                conjuntoLecturas.Etiqueta = "Navegación";
                conjuntoLecturas.EtiquetaPlural = "Navegaciones";

                if (Entrada.Tipo == TipoEntrada.Numero)
                    conjuntoLecturas.BusquedasEntrada = new List<BusquedaTextoArchivo>() { Entrada.BusquedaNumero };
                else if (Entrada.Tipo == TipoEntrada.ConjuntoNumeros)
                    conjuntoLecturas.BusquedasEntrada = Entrada.BusquedasConjuntoNumeros;
                else if (Entrada.Tipo == TipoEntrada.TextosInformacion)
                    conjuntoLecturas.BusquedasEntrada = Entrada.BusquedasTextosInformacion;

                conjuntoLecturas.LecturasNavegaciones = config.LecturasNavegaciones;
                conjuntoLecturas.ListarLecturasNavegaciones();

                opcionEstablecerLecturasNavegaciones_Busquedas.IsChecked = config.EstablecerLecturasNavegaciones_Busquedas;
            }
        }

        private void btnDefinirTextosInformacionFijos_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarOrdenarCantidades seleccionarOrdenar = new SeleccionarOrdenarCantidades();
            seleccionarOrdenar.listaTextos.TextosInformacion = config.TextosInformacionFijos.ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                config.TextosInformacionFijos = seleccionarOrdenar.listaTextos.TextosInformacion;
            }
        }
    }
}
