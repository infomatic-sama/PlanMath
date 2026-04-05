using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
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
    /// Lógica de interacción para EstablecerConjuntosTextosInformacionPredefinidos_Digitacion.xaml
    /// </summary>
    public partial class EstablecerConjuntosTextosInformacionPredefinidos_Digitacion : Window
    {
        public Entrada Entrada { get; set; }
        public DiseñoCalculo Calculo { get; set; }
        public EstablecerConjuntosTextosInformacionPredefinidos_Digitacion()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Entrada.ConjuntoTextosInformacion_Digitacion.Count > 0)
            {
                ListarParametros();
            }
            else
            {
                AgregarBoton_Agregar();
            }

            utilizarPrimerConjuntoAutomaticamente.IsChecked = Entrada.UtilizarPrimerConjunto_Automaticamente;
            utilizarSoloTextosDeInformacionPredefinidos.IsChecked = Entrada.UtilizarSoloTextosPredefinidos;
        }

        private void AgregarBoton_Agregar()
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            Image ImagenBotonAgregar = new Image();
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_03.png", UriKind.Relative));
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

            Entrada.ConjuntoTextosInformacion_Digitacion.Add(new ConjuntoTextosInformacion_Digitacion("Vector 1"));

            ConjuntoTextosInformacion_Digitacion_Entrada parametro = new ConjuntoTextosInformacion_Digitacion_Entrada();

            parametro.ListaEntradasAnteriores = Calculo.ListaEntradas.Where(i =>
                (!Calculo.ListaEntradas.Contains(Entrada) ||
                (Calculo.ListaEntradas.Contains(Entrada) && Calculo.ListaEntradas.IndexOf(i) < Calculo.ListaEntradas.IndexOf(Entrada)))
                && i.Tipo == TipoEntrada.TextosInformacion).ToList();

            if (parametro.ListaEntradasAnteriores != null && parametro.ListaEntradasAnteriores.Any())
                parametro.ConTextosInformacion_EntradasAnteriores = true;

            parametro.Conjunto = Entrada.ConjuntoTextosInformacion_Digitacion.First();

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, 0);

            AgregarBotones(0);
        }

        private void AgregarParametro(object sender, RoutedEventArgs e)
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            ConjuntoTextosInformacion_Digitacion nuevoParametro = new ConjuntoTextosInformacion_Digitacion("Vector " + (Entrada.ConjuntoTextosInformacion_Digitacion.Count + 1).ToString());

            int indice = Entrada.ConjuntoTextosInformacion_Digitacion.Count;

            Entrada.ConjuntoTextosInformacion_Digitacion.Add(nuevoParametro);

            ConjuntoTextosInformacion_Digitacion_Entrada parametro = new ConjuntoTextosInformacion_Digitacion_Entrada();

            parametro.ListaEntradasAnteriores = Calculo.ListaEntradas.Where(i =>
                (!Calculo.ListaEntradas.Contains(Entrada) ||
                (Calculo.ListaEntradas.Contains(Entrada) && Calculo.ListaEntradas.IndexOf(i) < Calculo.ListaEntradas.IndexOf(Entrada)))
                && i.Tipo == TipoEntrada.TextosInformacion).ToList();

            if (parametro.ListaEntradasAnteriores != null && parametro.ListaEntradasAnteriores.Any())
                parametro.ConTextosInformacion_EntradasAnteriores = true;

            parametro.Conjunto = nuevoParametro;

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, indice);

            AgregarBotones(indice);
        }

        private void QuitarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            ConjuntoTextosInformacion_Digitacion_Entrada parametro = (ConjuntoTextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                      where E.GetType() == typeof(ConjuntoTextosInformacion_Digitacion_Entrada)
                                             && ((ConjuntoTextosInformacion_Digitacion_Entrada)E).Conjunto == Entrada.ConjuntoTextosInformacion_Digitacion[indice]
                                                                                      select E).FirstOrDefault();


            parametros.Children.Remove(parametro);
            QuitarBotones(indice);
            Entrada.ConjuntoTextosInformacion_Digitacion.RemoveAt(indice);
            SetearIndices_Siguientes(indice);

            if (Entrada.ConjuntoTextosInformacion_Digitacion.Count == 0) AgregarBoton_Agregar();
        }

        private void SubirParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == 0) return;

            ConjuntoTextosInformacion_Digitacion_Entrada ctrlParametroASubir = (ConjuntoTextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ConjuntoTextosInformacion_Digitacion_Entrada)
                                                       && ((ConjuntoTextosInformacion_Digitacion_Entrada)E).Conjunto == Entrada.ConjuntoTextosInformacion_Digitacion[indice]
                                                                                                select E).FirstOrDefault();

            ConjuntoTextosInformacion_Digitacion_Entrada ctrlParametroABajar = (ConjuntoTextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ConjuntoTextosInformacion_Digitacion_Entrada)
                                                       && ((ConjuntoTextosInformacion_Digitacion_Entrada)E).Conjunto == Entrada.ConjuntoTextosInformacion_Digitacion[indice - 1]
                                                                                                select E).FirstOrDefault();

            ConjuntoTextosInformacion_Digitacion parametroASubir = Entrada.ConjuntoTextosInformacion_Digitacion[indice];
            ConjuntoTextosInformacion_Digitacion parametroABajar = Entrada.ConjuntoTextosInformacion_Digitacion[indice - 1];

            Entrada.ConjuntoTextosInformacion_Digitacion[indice] = parametroABajar;
            Entrada.ConjuntoTextosInformacion_Digitacion[indice - 1] = parametroASubir;

            ctrlParametroASubir.Conjunto = Entrada.ConjuntoTextosInformacion_Digitacion[indice];
            ctrlParametroABajar.Conjunto = Entrada.ConjuntoTextosInformacion_Digitacion[indice - 1];

            ListarParametros();
        }

        private void BajarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == Entrada.ConjuntoTextosInformacion_Digitacion.Count - 1) return;

            ConjuntoTextosInformacion_Digitacion_Entrada ctrlParametroABajar = (ConjuntoTextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ConjuntoTextosInformacion_Digitacion_Entrada)
                                                       && ((ConjuntoTextosInformacion_Digitacion_Entrada)E).Conjunto == Entrada.ConjuntoTextosInformacion_Digitacion[indice]
                                                                                                select E).FirstOrDefault();

            ConjuntoTextosInformacion_Digitacion_Entrada ctrlParametroASubir = (ConjuntoTextosInformacion_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                where E.GetType() == typeof(ConjuntoTextosInformacion_Digitacion_Entrada)
                                                       && ((ConjuntoTextosInformacion_Digitacion_Entrada)E).Conjunto == Entrada.ConjuntoTextosInformacion_Digitacion[indice + 1]
                                                                                                select E).FirstOrDefault();

            ConjuntoTextosInformacion_Digitacion parametroABajar = Entrada.ConjuntoTextosInformacion_Digitacion[indice];
            ConjuntoTextosInformacion_Digitacion parametroASubir = Entrada.ConjuntoTextosInformacion_Digitacion[indice + 1];

            Entrada.ConjuntoTextosInformacion_Digitacion[indice + 1] = parametroABajar;
            Entrada.ConjuntoTextosInformacion_Digitacion[indice] = parametroASubir;

            ctrlParametroABajar.Conjunto = Entrada.ConjuntoTextosInformacion_Digitacion[indice];
            ctrlParametroASubir.Conjunto = Entrada.ConjuntoTextosInformacion_Digitacion[indice + 1];

            ListarParametros();
        }

        private void SetearIndices_Siguientes(int indice)
        {
            for (int indiceSiguiente = indice; indiceSiguiente <= Entrada.ConjuntoTextosInformacion_Digitacion.Count - 1; indiceSiguiente++)
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
            foreach (var itemParametro in Entrada.ConjuntoTextosInformacion_Digitacion)
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                ConjuntoTextosInformacion_Digitacion_Entrada parametro = new ConjuntoTextosInformacion_Digitacion_Entrada();

                parametro.ListaEntradasAnteriores = Calculo.ListaEntradas.Where(i =>
                (!Calculo.ListaEntradas.Contains(Entrada) || 
                (Calculo.ListaEntradas.Contains(Entrada) && Calculo.ListaEntradas.IndexOf(i) < Calculo.ListaEntradas.IndexOf(Entrada))) 
                && i.Tipo == TipoEntrada.TextosInformacion).ToList();

                if (parametro.ListaEntradasAnteriores != null && parametro.ListaEntradasAnteriores.Any())
                    parametro.ConTextosInformacion_EntradasAnteriores = true;

                parametro.Conjunto = itemParametro;

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
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_03.png", UriKind.Relative));
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
            ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_04.png", UriKind.Relative));
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
            ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_05.png", UriKind.Relative));
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
            ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_06.png", UriKind.Relative));
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

        private void utilizarPrimerConjuntoAutomaticamente_Checked(object sender, RoutedEventArgs e)
        {
            if (Entrada != null)
                Entrada.UtilizarPrimerConjunto_Automaticamente = (bool)utilizarPrimerConjuntoAutomaticamente.IsChecked;
        }

        private void utilizarPrimerConjuntoAutomaticamente_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Entrada != null)
                Entrada.UtilizarPrimerConjunto_Automaticamente = (bool)utilizarPrimerConjuntoAutomaticamente.IsChecked;
        }

        private void utilizarSoloTextosDeInformacionPredefinidos_Checked(object sender, RoutedEventArgs e)
        {
            if (Entrada != null)
                Entrada.UtilizarSoloTextosPredefinidos = (bool)utilizarSoloTextosDeInformacionPredefinidos.IsChecked;
        }

        private void utilizarSoloTextosDeInformacionPredefinidos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Entrada != null)
                Entrada.UtilizarSoloTextosPredefinidos = (bool)utilizarSoloTextosDeInformacionPredefinidos.IsChecked;
        }
    }
}
