using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.OrigenesDatos;
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
    /// Lógica de interacción para EstablecerParametrosURL_Ejecucion.xaml
    /// </summary>
    public partial class EstablecerHeadersURL_Ejecucion : Window
    {
        public List<ParametroURL> Headers { get; set; }
        public EstablecerHeadersURL_Ejecucion()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Headers.Count > 0)
            {
                ListarParametros();
            }
            else
            {
                AgregarBoton_Agregar();
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

            Headers.Add(new ParametroURL("Parámetro 1", "Valor 1"));

            ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
            parametro.checkParametroNumerico.Visibility = Visibility.Collapsed;
            parametro.Parametro = Headers.First();

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, 0);

            AgregarBotones(0);
        }

        private void AgregarParametro(object sender, RoutedEventArgs e)
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            ParametroURL nuevoParametro = new ParametroURL("Parámetro " + (Headers.Count + 1).ToString()
                , "Valor " + (Headers.Count + 1).ToString());

            int indice = Headers.Count;

            Headers.Add(nuevoParametro);

            ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
            parametro.checkParametroNumerico.Visibility = Visibility.Collapsed;
            parametro.Parametro = nuevoParametro;

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, indice);

            AgregarBotones(indice);
        }

        private void QuitarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            ParametroSolicitudURL_Entrada parametro = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                      where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                             && ((ParametroSolicitudURL_Entrada)E).Parametro == Headers[indice]
                                                                                      select E).FirstOrDefault();


            parametros.Children.Remove(parametro);
            QuitarBotones(indice);
            Headers.RemoveAt(indice);
            SetearIndices_Siguientes(indice);

            if (Headers.Count == 0) AgregarBoton_Agregar();
        }

        private void SubirParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == 0) return;

            ParametroSolicitudURL_Entrada ctrlParametroASubir = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == Headers[indice]
                                                                                                select E).FirstOrDefault();

            ParametroSolicitudURL_Entrada ctrlParametroABajar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == Headers[indice - 1]
                                                                                                select E).FirstOrDefault();

            ParametroURL parametroASubir = Headers[indice];
            ParametroURL parametroABajar = Headers[indice - 1];

            int nivelParametroASubir = parametroASubir.Nivel;
            int nivelParametroABajar = parametroABajar.Nivel;

            Headers[indice] = parametroABajar;
            Headers[indice - 1] = parametroASubir;

            ctrlParametroASubir.Parametro = Headers[indice];
            ctrlParametroABajar.Parametro = Headers[indice - 1];

            ctrlParametroASubir.Parametro.Nivel = nivelParametroASubir;
            ctrlParametroABajar.Parametro.Nivel = nivelParametroABajar;

            ListarParametros();
            //SetearIndices_Siguientes(indice - 1);
            //ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == Entrada.ParametrosSolicitudWeb[indice - 1]
            //                                                                                  select E).FirstOrDefault();
            //if (ctrlParametroASubir.Parametro.Nivel > ctrlParametroABajar.Parametro.Nivel + 1)
            //{
            //    ctrlParametroASubir.Parametro.Nivel = ctrlParametroABajar.Parametro.Nivel + 1;
            //    ListarParametros();
            //}

            //parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == Entrada.ParametrosSolicitudWeb[indice]
            //                                                                                  select E).FirstOrDefault();
            //if (ctrlParametroABajar.Parametro.Nivel > parametroAnterior.Parametro.Nivel + 1)
            //    ctrlParametroABajar.Parametro.Nivel = parametroAnterior.Parametro.Nivel + 1;
        }

        private void BajarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == Headers.Count - 1) return;

            ParametroSolicitudURL_Entrada ctrlParametroABajar = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == Headers[indice]
                                                                                                select E).FirstOrDefault();

            ParametroSolicitudURL_Entrada ctrlParametroASubir = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
                                                       && ((ParametroSolicitudURL_Entrada)E).Parametro == Headers[indice + 1]
                                                                                                select E).FirstOrDefault();

            ParametroURL parametroABajar = Headers[indice];
            ParametroURL parametroASubir = Headers[indice + 1];

            int nivelParametroASubir = parametroASubir.Nivel;
            int nivelParametroABajar = parametroABajar.Nivel;

            Headers[indice + 1] = parametroABajar;
            Headers[indice] = parametroASubir;

            ctrlParametroABajar.Parametro = Headers[indice];
            ctrlParametroASubir.Parametro = Headers[indice + 1];

            ctrlParametroASubir.Parametro.Nivel = nivelParametroASubir;
            ctrlParametroABajar.Parametro.Nivel = nivelParametroABajar;

            ListarParametros();
            //SetearIndices_Siguientes(indice);
            //ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == Entrada.ParametrosSolicitudWeb[indice - 1]
            //                                                                                  select E).FirstOrDefault();
            //if (ctrlParametroASubir.Parametro.Nivel > ctrlParametroABajar.Parametro.Nivel + 1)
            //{
            //    ctrlParametroASubir.Parametro.Nivel = ctrlParametroABajar.Parametro.Nivel + 1;
            //    ListarParametros();
            //}

            //ParametroSolicitudURL_Entrada parametroAnterior = (ParametroSolicitudURL_Entrada)(from UIElement E in parametros.Children
            //                                                                                  where E.GetType() == typeof(ParametroSolicitudURL_Entrada)
            //                                         && ((ParametroSolicitudURL_Entrada)E).Parametro == Entrada.ParametrosSolicitudWeb[indice - 1]
            //                                                                                  select E).FirstOrDefault();

            //if (ctrlParametroASubir.Parametro.Nivel > parametroAnterior.Parametro.Nivel + 1)
            //    ctrlParametroASubir.Parametro.Nivel = parametroAnterior.Parametro.Nivel + 1;
        }

        private void SetearIndices_Siguientes(int indice)
        {
            for (int indiceSiguiente = indice; indiceSiguiente <= Headers.Count - 1; indiceSiguiente++)
            {
                Button agregar = (Button)(from UIElement E in parametros.Children
                                          where E.GetType() == typeof(Button)
    && (int)((Button)E).Tag == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Agregar"
                                          select E).FirstOrDefault();

                agregar.Tag = indiceSiguiente;

                Button quitar = (Button)(from UIElement E in parametros.Children
                                         where E.GetType() == typeof(Button)
    && (int)((Button)E).Tag == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Quitar"
                                         select E).FirstOrDefault();

                quitar.Tag = indiceSiguiente;

                Button subir = (Button)(from UIElement E in parametros.Children
                                        where E.GetType() == typeof(Button)
   && (int)((Button)E).Tag == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Subir"
                                        select E).FirstOrDefault();

                subir.Tag = indiceSiguiente;

                Button bajar = (Button)(from UIElement E in parametros.Children
                                        where E.GetType() == typeof(Button)
   && (int)((Button)E).Tag == indiceSiguiente + 1 && ((Image)((Button)E).Content).Tag.ToString() == "Bajar"
                                        select E).FirstOrDefault();

                bajar.Tag = indiceSiguiente;
            }
        }

        private void ListarParametros()
        {
            parametros.Children.Clear();

            int indice = 0;
            foreach (var itemParametro in Headers)
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                ParametroSolicitudURL_Entrada parametro = new ParametroSolicitudURL_Entrada();
                parametro.checkParametroNumerico.Visibility = Visibility.Collapsed;
                parametro.Parametro = itemParametro;

                parametros.Children.Add(parametro);

                Grid.SetColumn(parametro, 0);
                Grid.SetRow(parametro, indice);

                AgregarBotones(indice);

                indice++;
            }
        }

        private void AgregarBotones(int indice)
        {
            Image ImagenBotonAgregar = new Image();
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\17.png", UriKind.Relative));
            ImagenBotonAgregar.Width = 24;
            ImagenBotonAgregar.Height = 24;
            ImagenBotonAgregar.Tag = "Agregar";

            Button agregar = new Button();
            agregar.Content = ImagenBotonAgregar;
            agregar.Margin = new Thickness(10);
            agregar.Tag = indice;
            agregar.Click += AgregarParametro;

            parametros.Children.Add(agregar);

            Grid.SetColumn(agregar, 1);
            Grid.SetRow(agregar, indice);

            Image ImagenBotonQuitar = new Image();
            ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\18.png", UriKind.Relative));
            ImagenBotonQuitar.Width = 24;
            ImagenBotonQuitar.Height = 24;
            ImagenBotonQuitar.Tag = "Quitar";

            Button quitar = new Button();
            quitar.Content = ImagenBotonQuitar;
            quitar.Margin = new Thickness(10);
            quitar.Tag = indice;
            quitar.Click += QuitarParametro;

            parametros.Children.Add(quitar);

            Grid.SetColumn(quitar, 2);
            Grid.SetRow(quitar, indice);

            Image ImagenBotonSubir = new Image();
            ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\19.png", UriKind.Relative));
            ImagenBotonSubir.Width = 24;
            ImagenBotonSubir.Height = 24;
            ImagenBotonSubir.Tag = "Subir";

            Button subir = new Button();
            subir.Content = ImagenBotonSubir;
            subir.Margin = new Thickness(10);
            subir.Tag = indice;
            subir.Click += SubirParametro;

            parametros.Children.Add(subir);

            Grid.SetColumn(subir, 3);
            Grid.SetRow(subir, indice);

            Image ImagenBotonBajar = new Image();
            ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos3\\20.png", UriKind.Relative));
            ImagenBotonBajar.Width = 24;
            ImagenBotonBajar.Height = 24;
            ImagenBotonBajar.Tag = "Bajar";

            Button bajar = new Button();
            bajar.Content = ImagenBotonBajar;
            bajar.Margin = new Thickness(10);
            bajar.Tag = indice;
            bajar.Click += BajarParametro;

            parametros.Children.Add(bajar);

            Grid.SetColumn(bajar, 4);
            Grid.SetRow(bajar, indice);
        }

        private void QuitarBotones(int indice)
        {
            Button agregar = (Button)(from UIElement E in parametros.Children
                                      where E.GetType() == typeof(Button)
&& (int)((Button)E).Tag == indice && ((Image)((Button)E).Content).Tag.ToString() == "Agregar"
                                      select E).FirstOrDefault();

            parametros.Children.Remove(agregar);

            Button quitar = (Button)(from UIElement E in parametros.Children
                                     where E.GetType() == typeof(Button)
&& (int)((Button)E).Tag == indice && ((Image)((Button)E).Content).Tag.ToString() == "Quitar"
                                     select E).FirstOrDefault();

            parametros.Children.Remove(quitar);

            Button subir = (Button)(from UIElement E in parametros.Children
                                    where E.GetType() == typeof(Button)
&& (int)((Button)E).Tag == indice && ((Image)((Button)E).Content).Tag.ToString() == "Subir"
                                    select E).FirstOrDefault();

            parametros.Children.Remove(subir);

            Button bajar = (Button)(from UIElement E in parametros.Children
                                    where E.GetType() == typeof(Button)
&& (int)((Button)E).Tag == indice && ((Image)((Button)E).Content).Tag.ToString() == "Bajar"
                                    select E).FirstOrDefault();

            parametros.Children.Remove(bajar);
        }

        private void continuar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
