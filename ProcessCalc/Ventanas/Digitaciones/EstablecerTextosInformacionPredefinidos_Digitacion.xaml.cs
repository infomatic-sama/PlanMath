using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.TextosInformacion;
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
    /// Lógica de interacción para EstablecerTextosInformacionPredefinidos_Digitacion.xaml
    /// </summary>
    public partial class EstablecerTextosInformacionPredefinidos_Digitacion : Window
    {
        public ConjuntoTextosInformacion_Digitacion Conjunto { get; set; }
        public EstablecerTextosInformacionPredefinidos_Digitacion()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Conjunto.TextosInformacion.Count > 0)
            {
                ListarParametros();
            }
            else
            {
                AgregarBoton_Agregar();
            }

            if(Conjunto != null)
                opcionCiclo.IsChecked = Conjunto.UtilizarCiclicamente;
        }

        private void AgregarBoton_Agregar()
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            Image ImagenBotonAgregar = new Image();
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_07.png", UriKind.Relative));
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

            Conjunto.TextosInformacion.Add(new TextosInformacion_Digitacion("Nombre Texto 1", "Texto 1"));

            TextosInformacion_Digitacion_Entrada parametro = new TextosInformacion_Digitacion_Entrada();
            parametro.Texto = Conjunto.TextosInformacion.First();

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, 0);

            AgregarBotones(0);
        }

        private void AgregarParametro(object sender, RoutedEventArgs e)
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            TextosInformacion_Digitacion nuevoParametro = new TextosInformacion_Digitacion("Nombre Texto " + (Conjunto.TextosInformacion.Count + 1).ToString(),
                "Texto " + (Conjunto.TextosInformacion.Count + 1).ToString());

            int indice = Conjunto.TextosInformacion.Count;

            Conjunto.TextosInformacion.Add(nuevoParametro);

            TextosInformacion_Digitacion_Entrada parametro = new TextosInformacion_Digitacion_Entrada();
            parametro.Texto = nuevoParametro;

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, indice);

            AgregarBotones(indice);
        }

        private void QuitarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            TextosInformacion_Digitacion_Entrada parametro = (TextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                    where E.GetType() == typeof(TextosInformacion_Digitacion_Entrada)
                                                                           && ((TextosInformacion_Digitacion_Entrada)E).Texto == Conjunto.TextosInformacion[indice]
                                                                                                                    select E).FirstOrDefault();


            parametros.Children.Remove(parametro);
            QuitarBotones(indice);
            Conjunto.TextosInformacion.RemoveAt(indice);
            SetearIndices_Siguientes(indice);

            if (Conjunto.TextosInformacion.Count == 0) AgregarBoton_Agregar();
        }

        private void SubirParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == 0) return;

            TextosInformacion_Digitacion_Entrada ctrlParametroASubir = (TextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(TextosInformacion_Digitacion_Entrada)
                                                                                     && ((TextosInformacion_Digitacion_Entrada)E).Texto == Conjunto.TextosInformacion[indice]
                                                                                                                              select E).FirstOrDefault();

            TextosInformacion_Digitacion_Entrada ctrlParametroABajar = (TextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(TextosInformacion_Digitacion_Entrada)
                                                                                     && ((TextosInformacion_Digitacion_Entrada)E).Texto == Conjunto.TextosInformacion[indice - 1]
                                                                                                                              select E).FirstOrDefault();

            TextosInformacion_Digitacion parametroASubir = Conjunto.TextosInformacion[indice];
            TextosInformacion_Digitacion parametroABajar = Conjunto.TextosInformacion[indice - 1];

            Conjunto.TextosInformacion[indice] = parametroABajar;
            Conjunto.TextosInformacion[indice - 1] = parametroASubir;

            ctrlParametroASubir.Texto = Conjunto.TextosInformacion[indice];
            ctrlParametroABajar.Texto = Conjunto.TextosInformacion[indice - 1];

            ListarParametros();
        }

        private void BajarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == Conjunto.TextosInformacion.Count - 1) return;

            TextosInformacion_Digitacion_Entrada ctrlParametroABajar = (TextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(TextosInformacion_Digitacion_Entrada)
                                                                                     && ((TextosInformacion_Digitacion_Entrada)E).Texto == Conjunto.TextosInformacion[indice]
                                                                                                                              select E).FirstOrDefault();

            TextosInformacion_Digitacion_Entrada ctrlParametroASubir = (TextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(TextosInformacion_Digitacion_Entrada)
                                                                                     && ((TextosInformacion_Digitacion_Entrada)E).Texto == Conjunto.TextosInformacion[indice + 1]
                                                                                                                              select E).FirstOrDefault();

            TextosInformacion_Digitacion parametroABajar = Conjunto.TextosInformacion[indice];
            TextosInformacion_Digitacion parametroASubir = Conjunto.TextosInformacion[indice + 1];

            Conjunto.TextosInformacion[indice + 1] = parametroABajar;
            Conjunto.TextosInformacion[indice] = parametroASubir;

            ctrlParametroABajar.Texto = Conjunto.TextosInformacion[indice];
            ctrlParametroASubir.Texto = Conjunto.TextosInformacion[indice + 1];

            ListarParametros();
        }

        private void SetearIndices_Siguientes(int indice)
        {
            for (int indiceSiguiente = indice; indiceSiguiente <= Conjunto.TextosInformacion.Count - 1; indiceSiguiente++)
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
            foreach (var itemParametro in Conjunto.TextosInformacion)
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                TextosInformacion_Digitacion_Entrada parametro = new TextosInformacion_Digitacion_Entrada();
                parametro.Texto = itemParametro;

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
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_07.png", UriKind.Relative));
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
            ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_08.png", UriKind.Relative));
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
            ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_09.png", UriKind.Relative));
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
            ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_10.png", UriKind.Relative));
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

        private void opcionCiclo_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                if (Conjunto != null) 
                    Conjunto.UtilizarCiclicamente = (bool)opcionCiclo.IsChecked;
            }
        }

        private void opcionCiclo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Conjunto != null)
                    Conjunto.UtilizarCiclicamente = (bool)opcionCiclo.IsChecked;
            }
        }
    }
}
