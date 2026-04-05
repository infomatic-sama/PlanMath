using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para EstablecerListaNumerosPredefinidos_Digitacion.xaml
    /// </summary>
    public partial class EstablecerListaNumerosPredefinidos_Digitacion : Window
    {
        public Entrada Entrada { get; set; }
        public bool ConTextosInformacion_EntradasAnteriores { get; set; }
        public List<Entrada> ListaEntradasAnteriores { get; set; }
        public EstablecerListaNumerosPredefinidos_Digitacion()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Entrada.OpcionesListaNumeros.Count > 0)
            {
                ListarParametros();
            }
            else
            {
                AgregarBoton_Agregar();
            }

            soloPermitirSeleccionarNumeroLista.IsChecked = Entrada.SeleccionarNumeroDeOpciones;

            if (ConTextosInformacion_EntradasAnteriores &&
                ListaEntradasAnteriores.Any())
            {
                opcionesEntradasAnteriores.DisplayMemberPath = "NombreCombo";
                opcionesEntradasAnteriores.ItemsSource = ListaEntradasAnteriores;
            }

            if (ConTextosInformacion_EntradasAnteriores)
            {
                opcionesEntradasAnteriores.SelectedItem = Entrada.ConfigListaNumeros.EntradaAnterior_TextosInformacion_Predefinidos;
            }

            if (ConTextosInformacion_EntradasAnteriores &&
                ListaEntradasAnteriores.Any())
            {
                opcionesTextosInformacion_EntradasAnteriores.Visibility = Visibility.Visible;
            }
        }

        private void AgregarBoton_Agregar()
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            Image ImagenBotonAgregar = new Image();
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_11.png", UriKind.Relative));
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

            Entrada.OpcionesListaNumeros.Add(new OpcionListaNumeros_Digitacion("Número 1", "1"));

            OpcionListaNumeros_Digitacion_Entrada parametro = new OpcionListaNumeros_Digitacion_Entrada();
            parametro.Numero = Entrada.OpcionesListaNumeros.First();

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, 0);

            AgregarBotones(0);
        }

        private void AgregarParametro(object sender, RoutedEventArgs e)
        {
            parametros.RowDefinitions.Add(new RowDefinition());
            parametros.RowDefinitions.Last().Height = GridLength.Auto;

            OpcionListaNumeros_Digitacion nuevoParametro = new OpcionListaNumeros_Digitacion("Número " + (Entrada.OpcionesListaNumeros.Count + 1).ToString(),
                (Entrada.OpcionesListaNumeros.Count + 1).ToString());

            int indice = Entrada.OpcionesListaNumeros.Count;

            Entrada.OpcionesListaNumeros.Add(nuevoParametro);

            OpcionListaNumeros_Digitacion_Entrada parametro = new OpcionListaNumeros_Digitacion_Entrada();
            parametro.Numero = nuevoParametro;

            parametros.Children.Add(parametro);

            Grid.SetColumn(parametro, 0);
            Grid.SetRow(parametro, indice);

            AgregarBotones(indice);
        }

        private void QuitarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            OpcionListaNumeros_Digitacion_Entrada parametro = (OpcionListaNumeros_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                    where E.GetType() == typeof(OpcionListaNumeros_Digitacion_Entrada)
                                                                           && ((OpcionListaNumeros_Digitacion_Entrada)E).Numero == Entrada.OpcionesListaNumeros[indice]
                                                                                                                    select E).FirstOrDefault();


            parametros.Children.Remove(parametro);
            QuitarBotones(indice);
            Entrada.OpcionesListaNumeros.RemoveAt(indice);
            SetearIndices_Siguientes(indice);

            if (Entrada.OpcionesListaNumeros.Count == 0) AgregarBoton_Agregar();
        }

        private void SubirParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == 0) return;

            OpcionListaNumeros_Digitacion_Entrada ctrlParametroASubir = (OpcionListaNumeros_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(OpcionListaNumeros_Digitacion_Entrada)
                                                                                     && ((OpcionListaNumeros_Digitacion_Entrada)E).Numero == Entrada.OpcionesListaNumeros[indice]
                                                                                                                              select E).FirstOrDefault();

            OpcionListaNumeros_Digitacion_Entrada ctrlParametroABajar = (OpcionListaNumeros_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(OpcionListaNumeros_Digitacion_Entrada)
                                                                                     && ((OpcionListaNumeros_Digitacion_Entrada)E).Numero == Entrada.OpcionesListaNumeros[indice - 1]
                                                                                                                              select E).FirstOrDefault();

            OpcionListaNumeros_Digitacion parametroASubir = Entrada.OpcionesListaNumeros[indice];
            OpcionListaNumeros_Digitacion parametroABajar = Entrada.OpcionesListaNumeros[indice - 1];

            Entrada.OpcionesListaNumeros[indice] = parametroABajar;
            Entrada.OpcionesListaNumeros[indice - 1] = parametroASubir;

            ctrlParametroASubir.Numero = Entrada.OpcionesListaNumeros[indice];
            ctrlParametroABajar.Numero = Entrada.OpcionesListaNumeros[indice - 1];

            ListarParametros();
        }

        private void BajarParametro(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice == Entrada.ConjuntoTextosInformacion_Digitacion.Count - 1) return;

            OpcionListaNumeros_Digitacion_Entrada ctrlParametroABajar = (OpcionListaNumeros_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(OpcionListaNumeros_Digitacion_Entrada)
                                                                                     && ((OpcionListaNumeros_Digitacion_Entrada)E).Numero == Entrada.OpcionesListaNumeros[indice]
                                                                                                                              select E).FirstOrDefault();

            OpcionListaNumeros_Digitacion_Entrada ctrlParametroASubir = (OpcionListaNumeros_Digitacion_Entrada)(from UIElement E in parametros.Children
                                                                                                                              where E.GetType() == typeof(OpcionListaNumeros_Digitacion_Entrada)
                                                                                     && ((OpcionListaNumeros_Digitacion_Entrada)E).Numero == Entrada.OpcionesListaNumeros[indice + 1]
                                                                                                                              select E).FirstOrDefault();

            OpcionListaNumeros_Digitacion parametroABajar = Entrada.OpcionesListaNumeros[indice];
            OpcionListaNumeros_Digitacion parametroASubir = Entrada.OpcionesListaNumeros[indice + 1];

            Entrada.OpcionesListaNumeros[indice + 1] = parametroABajar;
            Entrada.OpcionesListaNumeros[indice] = parametroASubir;

            ctrlParametroABajar.Numero = Entrada.OpcionesListaNumeros[indice];
            ctrlParametroASubir.Numero = Entrada.OpcionesListaNumeros[indice + 1];

            ListarParametros();
        }

        private void SetearIndices_Siguientes(int indice)
        {
            for (int indiceSiguiente = indice; indiceSiguiente <= Entrada.OpcionesListaNumeros.Count - 1; indiceSiguiente++)
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
            foreach (var itemParametro in Entrada.OpcionesListaNumeros)
            {
                parametros.RowDefinitions.Add(new RowDefinition());
                parametros.RowDefinitions.Last().Height = GridLength.Auto;

                OpcionListaNumeros_Digitacion_Entrada parametro = new OpcionListaNumeros_Digitacion_Entrada();
                parametro.Numero = itemParametro;

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
            ImagenBotonAgregar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_11.png", UriKind.Relative));
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
            ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_12.png", UriKind.Relative));
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
            ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_13.png", UriKind.Relative));
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
            ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos7\\Icono_14.png", UriKind.Relative));
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

        private void soloPermitirSeleccionarNumeroLista_Checked(object sender, RoutedEventArgs e)
        {
            if (Entrada != null)
                Entrada.SeleccionarNumeroDeOpciones = (bool)soloPermitirSeleccionarNumeroLista.IsChecked;
        }

        private void soloPermitirSeleccionarNumeroLista_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Entrada != null)
                Entrada.SeleccionarNumeroDeOpciones = (bool)soloPermitirSeleccionarNumeroLista.IsChecked;
        }

        private void opcionesEntradasAnteriores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (opcionesEntradasAnteriores.SelectedItem != null)
            {
                if (Entrada != null)
                {
                    Entrada.ConfigListaNumeros.ConTextosInformacion_EntradasAnteriores = true;
                    Entrada.ConfigListaNumeros.EntradaAnterior_TextosInformacion_Predefinidos = (Entrada)opcionesEntradasAnteriores.SelectedItem;
                }
            }
        }

        private void quitarSeleccionEntradaAnterior_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            opcionesEntradasAnteriores.SelectedItem = null;
            if (Entrada != null)
            {
                Entrada.ConfigListaNumeros.ConTextosInformacion_EntradasAnteriores = false;
                Entrada.ConfigListaNumeros.EntradaAnterior_TextosInformacion_Predefinidos = null;
            }
        }
    }
}
