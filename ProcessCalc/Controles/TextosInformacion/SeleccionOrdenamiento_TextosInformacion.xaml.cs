using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
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

namespace ProcessCalc.Controles.TextosInformacion
{
    /// <summary>
    /// Lógica de interacción para SeleccionOrdenamiento_TextosInformacion.xaml
    /// </summary>
    public partial class SeleccionOrdenamiento_TextosInformacion : UserControl
    {
        public List<string> TextosInformacion { get; set; }
        public List<ConjuntoTextosInformacion_Digitacion> ConjuntoTextosInformacionDigitacion { get; set; }
        ConjuntoTextosInformacion_Digitacion ConjuntoTextosInformacionSeleccionado;
        public bool FijarCantidadTextosDigitacion { get; set; }
        public int CantidadTextosDigitacion { get; set; }
        public VistaTextosInformacionEntrada VistaTextos { get; set; }
        public DigitarConjuntoTextos VistaEntradaDigitacion { get; set; }
        public bool ModoFilaTextosInformacion { get; set; }
        public bool MostrarBotonQuitar {  get; set; }
        public int PosicionNumero {  get; set; }
        public bool UtilizarSoloTextosPredefinidos { get; set; }

        public SeleccionOrdenamiento_TextosInformacion()
        {
            InitializeComponent();

        }

        public void ListarTextos()
        {
            if (TextosInformacion != null)
            {
                textosInformacionSeleccionados.Children.Clear();

                int indice = 0;
                bool conEtiquetas = ConjuntoTextosInformacionDigitacion != null &&
                    ConjuntoTextosInformacionDigitacion.Any() ? true : false;
                int indiceEtiqueta = 0;

                if (FijarCantidadTextosDigitacion &&
                    CantidadTextosDigitacion > 0)
                {
                    int diferencia = CantidadTextosDigitacion - TextosInformacion.Count;
                    if (diferencia > 0)
                    {
                        for (int i = 1; i <= diferencia; i++)
                            TextosInformacion.Add(string.Empty);
                    }
                }

                foreach (var item in TextosInformacion)
                {
                    if ((conEtiquetas &&
                        ConjuntoTextosInformacionSeleccionado != null &&
                        (((!ConjuntoTextosInformacionSeleccionado.UtilizarCiclicamente &&
                        indiceEtiqueta <= ConjuntoTextosInformacionSeleccionado.TextosInformacion.Count - 1) ||
                        (ConjuntoTextosInformacionSeleccionado.UtilizarCiclicamente)) &&
                        ConjuntoTextosInformacionSeleccionado.TextosInformacion[indiceEtiqueta].SeleccionarFiltrarPosicion(PosicionNumero))) ||
                        !conEtiquetas)
                    {
                        textosInformacionSeleccionados.RowDefinitions.Add(new RowDefinition());
                        textosInformacionSeleccionados.RowDefinitions.Last().Height = GridLength.Auto;

                        TextBlock numero = new TextBlock();
                        numero.Text = (indice + 1).ToString();
                        numero.Margin = new Thickness(10);

                        textosInformacionSeleccionados.Children.Add(numero);

                        Grid.SetRow(numero, indice);
                        Grid.SetColumn(numero, 0);

                        string cadenaEtiqueta = string.Empty;


                        TextBlock textoEtiqueta = new TextBlock();
                        textoEtiqueta.Text = (indice + 1).ToString();
                        textoEtiqueta.Margin = new Thickness(10);
                        textoEtiqueta.Text = (conEtiquetas && ConjuntoTextosInformacionSeleccionado.TextosInformacion.Any() ? ConjuntoTextosInformacionSeleccionado.TextosInformacion[indiceEtiqueta].Nombre : string.Empty) + ": ";
                        textoEtiqueta.Tag = indice;

                        textosInformacionSeleccionados.Children.Add(textoEtiqueta);

                        Grid.SetRow(textoEtiqueta, indice);
                        Grid.SetColumn(textoEtiqueta, 1);

                        cadenaEtiqueta = conEtiquetas && ConjuntoTextosInformacionSeleccionado.TextosInformacion.Any() ? ConjuntoTextosInformacionSeleccionado.TextosInformacion[indiceEtiqueta].Texto : string.Empty;

                        TextBox texto = new TextBox();
                        texto.Text = (indice + 1).ToString();
                        texto.Margin = new Thickness(10);
                        texto.Text = !string.IsNullOrEmpty(cadenaEtiqueta) ? cadenaEtiqueta : item;
                        texto.Tag = indice;
                        texto.TextChanged += EditarTexto;

                        textosInformacionSeleccionados.Children.Add(texto);

                        Grid.SetRow(texto, indice);
                        Grid.SetColumn(texto, 2);

                    if (!FijarCantidadTextosDigitacion &&
                            !UtilizarSoloTextosPredefinidos)
                    {
                        Image ImagenBotonQuitar = new Image();
                        ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\06.png", UriKind.Relative));
                        ImagenBotonQuitar.Width = 24;
                        ImagenBotonQuitar.Height = 24;

                            Button botonQuitar = new Button();
                            botonQuitar.Content = ImagenBotonQuitar;
                            botonQuitar.Margin = new Thickness(10);
                            botonQuitar.MaxHeight = 70;
                            botonQuitar.Tag = indice;
                            botonQuitar.Click += QuitarTexto;

                            textosInformacionSeleccionados.Children.Add(botonQuitar);

                            Grid.SetRow(botonQuitar, indice);
                            Grid.SetColumn(botonQuitar, 3);
                        }

                        Image ImagenBotonSubir = new Image();
                        ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\07.png", UriKind.Relative));
                        ImagenBotonSubir.Width = 24;
                        ImagenBotonSubir.Height = 24;

                        Button botonSubir = new Button();
                        botonSubir.Content = ImagenBotonSubir;
                        botonSubir.Margin = new Thickness(10);
                        botonSubir.MaxHeight = 70;
                        botonSubir.Tag = indice;
                        botonSubir.Click += SubirTexto;

                        textosInformacionSeleccionados.Children.Add(botonSubir);

                        Grid.SetRow(botonSubir, indice);
                        Grid.SetColumn(botonSubir, 4);

                        Image ImagenBotonBajar = new Image();
                        ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\08.png", UriKind.Relative));
                        ImagenBotonBajar.Width = 24;
                        ImagenBotonBajar.Height = 24;

                        Button botonBajar = new Button();
                        botonBajar.Content = ImagenBotonBajar;
                        botonBajar.Margin = new Thickness(10);
                        botonBajar.MaxHeight = 70;
                        botonBajar.Tag = indice;
                        botonBajar.Click += BajarTexto;

                        textosInformacionSeleccionados.Children.Add(botonBajar);

                        Grid.SetRow(botonBajar, indice);
                        Grid.SetColumn(botonBajar, 5);

                    }

                    indiceEtiqueta++;

                    if (ConjuntoTextosInformacionSeleccionado != null && 
                        ConjuntoTextosInformacionSeleccionado.UtilizarCiclicamente &&
                        indiceEtiqueta == ConjuntoTextosInformacionSeleccionado.TextosInformacion.Count)
                        indiceEtiqueta = 0;

                    indice++;

                    if (FijarCantidadTextosDigitacion &&
                        CantidadTextosDigitacion > 0 &&
                        CantidadTextosDigitacion == indice)
                        break;
                }
            }
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }
                        
        private void EditarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((TextBox)sender).Tag;
            TextosInformacion[indice] = ((TextBox)sender).Text;
        }

        private void QuitarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            TextosInformacion.RemoveAt(indice);
            ListarTextos();
        }

        private void SubirTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice > 0)
            {
                string texto = TextosInformacion[indice];
                TextosInformacion.RemoveAt(indice);

                TextosInformacion.Insert(indice - 1, texto);

                if(ConjuntoTextosInformacionSeleccionado.TextosInformacion.Any() &&
                    indice < ConjuntoTextosInformacionSeleccionado.TextosInformacion.Count)
                {
                    TextosInformacion_Digitacion textoEtiqueta = ConjuntoTextosInformacionSeleccionado.TextosInformacion[indice];
                    ConjuntoTextosInformacionSeleccionado.TextosInformacion.RemoveAt(indice);

                    ConjuntoTextosInformacionSeleccionado.TextosInformacion.Insert(indice - 1, textoEtiqueta);
                }

                ListarTextos();
            }
        }

        private void BajarTexto(object sender, RoutedEventArgs e)
        {
            int indice = (int)((Button)sender).Tag;

            if (indice < TextosInformacion.Count - 1)
            {
                string texto = TextosInformacion[indice];
                TextosInformacion.RemoveAt(indice);

                TextosInformacion.Insert(indice + 1, texto);

                if (ConjuntoTextosInformacionSeleccionado.TextosInformacion.Any() &&
                    indice >= 0 && indice < ConjuntoTextosInformacionSeleccionado.TextosInformacion.Count)
                {
                    TextosInformacion_Digitacion textoEtiqueta = ConjuntoTextosInformacionSeleccionado.TextosInformacion[indice];
                    ConjuntoTextosInformacionSeleccionado.TextosInformacion.RemoveAt(indice);

                    ConjuntoTextosInformacionSeleccionado.TextosInformacion.Insert(indice + 1, textoEtiqueta);
                }

                ListarTextos();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(ConjuntoTextosInformacionDigitacion != null &&
                ConjuntoTextosInformacionDigitacion.Any())
            {
                opcionesConjuntosTextos.Visibility = Visibility.Visible;
                conjuntosTextos.DisplayMemberPath = "Nombre";
                conjuntosTextos.ItemsSource = ConjuntoTextosInformacionDigitacion;
                conjuntosTextos.SelectionChanged += ConjuntosTextos_SelectionChanged;
                conjuntosTextos.SelectedIndex = 0;

                if(FijarCantidadTextosDigitacion)
                {
                    textoEtiquetaInformacionAgregar.Visibility = Visibility.Collapsed;
                    textoInformacionAgregar.Visibility = Visibility.Collapsed;
                    agregarTextoInformacion.Visibility = Visibility.Collapsed;
                    opcionesDigitacion.Visibility = Visibility.Collapsed;
                }
            }

            ListarTextos();

            if (FijarCantidadTextosDigitacion &&
                CantidadTextosDigitacion > 0)
            {
                listaTextoNumeros.MaxLines = CantidadTextosDigitacion;
            }

            if(ModoFilaTextosInformacion)
            {
                BorderBrush = Brushes.Black;
                BorderThickness = new Thickness(0, 0, 0, 0.3);

                if(MostrarBotonQuitar)
                    quitarFilaTextos.Visibility = Visibility.Visible;
            }

            if(UtilizarSoloTextosPredefinidos)
            {
                agregarTextoInformacion.Visibility = Visibility.Collapsed;
                vistaOpcionLista.Visibility = Visibility.Collapsed;
                vistaOpcionTexto.Visibility = Visibility.Collapsed;
            }
        }

        private void ConjuntosTextos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextosInformacion.Clear();

            if (((ComboBox)sender).SelectedItem != null)
            {
                ConjuntoTextosInformacionSeleccionado = (ConjuntoTextosInformacion_Digitacion)((ComboBox)sender).SelectedItem;
                
                foreach(var item in ConjuntoTextosInformacionSeleccionado.TextosInformacion)
                {
                    TextosInformacion.Add(item.Texto);
                }
            }
            else
                ConjuntoTextosInformacionSeleccionado = null;

            ListarTextos();

            string strNumeros = string.Empty;
            foreach (var itemNumero in TextosInformacion)
            {
                if (itemNumero != TextosInformacion.Last())
                    strNumeros += itemNumero + "\n";
                else
                    strNumeros += itemNumero;
            }

            listaTextoNumeros.Text = strNumeros;
        }

        private void agregarTextoInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (!TextosInformacion.Any(item => item.Trim().ToLower() == textoInformacionAgregar.Text.Trim().ToLower()))
            {
                TextosInformacion.Add(textoInformacionAgregar.Text);
                ListarTextos();

                if (FijarCantidadTextosDigitacion &&
                        CantidadTextosDigitacion > 0 &&
                        CantidadTextosDigitacion == TextosInformacion.Count)
                {
                    textoEtiquetaInformacionAgregar.Visibility = Visibility.Collapsed;
                    textoInformacionAgregar.Visibility = Visibility.Collapsed;
                    agregarTextoInformacion.Visibility = Visibility.Collapsed;                    
                }
            }
        }

        private void vistaOpcionLista_Click(object sender, RoutedEventArgs e)
        {
            textoNumeros.Visibility = Visibility.Collapsed;
            textosInformacionSeleccionados.Visibility = Visibility.Visible;
            agregarTextoInformacion.Visibility = Visibility.Visible;
            textoEtiquetaInformacionAgregar.Visibility = Visibility.Visible;
            textoInformacionAgregar.Visibility = Visibility.Visible;

            ListarTextos();
        }

        private void vistaOpcionTexto_Click(object sender, RoutedEventArgs e)
        {
            textosInformacionSeleccionados.Visibility = Visibility.Collapsed;
            textoNumeros.Visibility = Visibility.Visible;
            agregarTextoInformacion.Visibility = Visibility.Collapsed;
            textoEtiquetaInformacionAgregar.Visibility = Visibility.Collapsed;
            textoInformacionAgregar.Visibility = Visibility.Collapsed;

            string strNumeros = string.Empty;
            foreach (var itemNumero in TextosInformacion)
            {
                if (itemNumero != TextosInformacion.Last())
                    strNumeros += itemNumero + "\n";
                else
                    strNumeros += itemNumero;
            }

            listaTextoNumeros.Text = strNumeros;
        }

        private void listaTextoNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuitarUltimaLinea();

            if (textoNumeros.Visibility == Visibility.Visible)
            {
                TextosInformacion.Clear();

                string[] lineasTexto = listaTextoNumeros.Text.Split('\n');

                foreach (var itemLineaTexto in lineasTexto)
                {
                    if (string.IsNullOrEmpty(itemLineaTexto)) continue;

                    //List<string> textos = itemLineaTexto.Split(';').ToList();
                    //List<string> TextosInformacion = new List<string>();

                    //foreach (var itemTexto in textos)
                    //{
                    //    TextosInformacion.Add(itemTexto);
                    //}

                    this.TextosInformacion.Add(itemLineaTexto);
                }
            }
        }

        private void quitarConjuntoTextos_Click(object sender, RoutedEventArgs e)
        {
            if(VistaTextos != null)
            {
                VistaTextos.QuitarConjuntoTextos(this);
            }
        }

        private void quitarFilaTextos_Click(object sender, RoutedEventArgs e)
        {
            if(VistaEntradaDigitacion != null)
            {
                VistaEntradaDigitacion.QuitarFila(this);
            }
        }

        private void QuitarUltimaLinea()
        {
            if (FijarCantidadTextosDigitacion &&
                CantidadTextosDigitacion > 0 &&
                listaTextoNumeros.Text.Any() &&
                (listaTextoNumeros.Text.Split("\r\n").Length > CantidadTextosDigitacion |
                listaTextoNumeros.Text.Split("\n").Length > CantidadTextosDigitacion))
            {
                if (listaTextoNumeros.Text.LastIndexOf("\r\n") > -1)
                    listaTextoNumeros.Text = listaTextoNumeros.Text.Remove(listaTextoNumeros.Text.LastIndexOf("\r\n"));

                else if (listaTextoNumeros.Text.LastIndexOf("\n") > -1)
                    listaTextoNumeros.Text = listaTextoNumeros.Text.Remove(listaTextoNumeros.Text.LastIndexOf("\n"));

                listaTextoNumeros.Select(listaTextoNumeros.Text.Length, 0);
            }
        }
    }
}
