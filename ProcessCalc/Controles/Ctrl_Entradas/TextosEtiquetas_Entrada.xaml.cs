using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
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
    /// Lógica de interacción para TextosEtiquetas_Entrada.xaml
    /// </summary>
    public partial class TextosEtiquetas_Entrada : UserControl
    {
        public Entrada Entrada { get; set; }
        public EntidadNumero Numero { get; set; }
        int indice = -1;
        public List<ConjuntoTextosInformacion_Digitacion> ConjuntosTextosInformacionDigitacion { get; set; }
        public bool Digitando { get; set; }
        public bool UtilizarPrimerConjunto_Automaticamente { get; set; }
        ConjuntoTextosInformacion_Digitacion ConjuntoTextosInformacion_Seleccionado;
        public bool UtilizarSoloTextosPredefinidos { get; set; }
        public int IndiceNumero { get; set; }
        public DigitarConjuntoNumeros DigitacionConjuntoNumeros { get; set; }
        public TextosEtiquetas_Entrada()
        {
            InitializeComponent();
        }

        public void ListarTextos(ConjuntoTextosInformacion_Digitacion conjunto)
        {
            VaciarListaTextos();

            int indiceTextos = -1;
            indice = -1;

            if (conjunto != null &&
                !conjunto.UtilizarCiclicamente)
            {
                if (Entrada != null)
                {
                    foreach (var itemTexto in conjunto.TextosInformacion)
                    {
                        indice++;

                        if (itemTexto.SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                        {

                            if (Entrada.Textos.Any() &&
                                indice <= Entrada.Textos.Count - 1)
                            {
                                Entrada.Textos[indice] = itemTexto.Texto;
                                AgregarTexto(Entrada.Textos[indice], Entrada.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == indice).FirstOrDefault(), 
                                    false, ref indiceTextos, itemTexto.Nombre);
                            }
                            else
                            {
                                AgregarTexto(itemTexto.Texto, Entrada.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == Entrada.Textos.Count).FirstOrDefault(), 
                                    false, ref indiceTextos, itemTexto.Nombre);
                            }
                        }
                    }

                    foreach (var texto in Entrada.Textos.Where(i => Entrada.Textos.IndexOf(i) > indiceTextos))
                    {
                        AgregarTexto(texto, Entrada.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                        i.IndiceAsociado == Entrada.Textos.IndexOf(texto)).FirstOrDefault(),
                            false, ref indice);
                    }
                }
                else if (Numero != null)
                {
                    foreach (var itemTexto in conjunto.TextosInformacion)
                    {
                        indice++;

                        if (itemTexto.SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                        {
                            if (Numero.Textos.Any() &&
                            indice <= Numero.Textos.Count - 1)
                            {
                                Numero.Textos[indice] = itemTexto.Texto;
                                AgregarTexto(Numero.Textos[indice], Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == indice).FirstOrDefault(),
                                    false, ref indiceTextos, itemTexto.Nombre);
                            }
                            else
                            {
                                AgregarTexto(itemTexto.Texto, Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == Numero.Textos.Count).FirstOrDefault(),
                                    false, ref indiceTextos, itemTexto.Nombre);
                            }
                        }
                    }

                    foreach (var texto in Numero.Textos.Where(i => Numero.Textos.IndexOf(i) > indiceTextos))
                    {
                        AgregarTexto(texto, Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                        i.IndiceAsociado == Numero.Textos.IndexOf(texto)).FirstOrDefault(),
                            false, ref indice);
                    }
                }
            }
            else if(conjunto != null &&
                conjunto.UtilizarCiclicamente &&
                conjunto.TextosInformacion.Any())
            {
                if (Entrada != null)
                {
                    while (true)
                    {
                        indice++;

                        if (conjunto.TextosInformacion[indiceTextos + 1].SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                        {

                            if (Entrada.Textos.Any() &&
                                indice <= Entrada.Textos.Count - 1)
                            {
                                Entrada.Textos[indice] = conjunto.TextosInformacion[indiceTextos + 1].Texto;
                                AgregarTexto(Entrada.Textos[indice], Entrada.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == indice).FirstOrDefault(), 
                                    false, ref indiceTextos, conjunto.TextosInformacion[indiceTextos + 1].Nombre);
                            }
                            else
                            {
                                AgregarTexto(conjunto.TextosInformacion[indiceTextos + 1].Texto,
                                    Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                    i.IndiceAsociado == indiceTextos + 1).FirstOrDefault(),
                                    false, ref indiceTextos, conjunto.TextosInformacion[indiceTextos + 1].Nombre);
                            }
                        }
                        else
                            indiceTextos++;

                        if (Entrada.Textos.Any() && 
                            indice == Entrada.Textos.Count - 1)
                            break;
                        else
                        {
                            if (indiceTextos == conjunto.TextosInformacion.Count - 1)
                            {
                                indiceTextos = -1;

                                if (!Entrada.Textos.Any())
                                    break;
                            }
                        }
                    }
                }
                else if (Numero != null)
                {
                    while (true)
                    {
                        indice++;

                        if (conjunto.TextosInformacion[indiceTextos + 1].SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                        {
                            if (Numero.Textos.Any() &&
                            indice <= Numero.Textos.Count - 1)
                            {
                                Numero.Textos[indice] = conjunto.TextosInformacion[indiceTextos + 1].Texto;
                                AgregarTexto(Numero.Textos[indice], Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == indice).FirstOrDefault(), 
                                    false, ref indiceTextos, conjunto.TextosInformacion[indiceTextos + 1].Nombre);
                            }
                            else
                            {
                                AgregarTexto(conjunto.TextosInformacion[indiceTextos + 1].Texto,
                                    Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                    i.IndiceAsociado == indiceTextos + 1).FirstOrDefault(),
                                    false, ref indiceTextos, conjunto.TextosInformacion[indiceTextos + 1].Nombre);
                            }
                        }
                        else
                            indiceTextos++;

                        if (Numero.Textos.Any() && 
                            indice == Numero.Textos.Count - 1)
                            break;
                        else
                        {
                            if (indiceTextos == conjunto.TextosInformacion.Count - 1)
                            {
                                indiceTextos = -1;

                                if (!Numero.Textos.Any())
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (Entrada != null)
                {
                    int indiceTexto = 0;
                    foreach (var texto in Entrada.Textos)
                    {
                        AgregarTexto(texto, Entrada.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                        i.IndiceAsociado == indiceTexto).FirstOrDefault(),
                            false, ref indice);

                        indiceTexto++;
                    }
                }
                else if (Numero != null)
                {
                    int indiceTexto = 0;
                    foreach (var texto in Numero.Textos)
                    {
                        AgregarTexto(texto, Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                        i.IndiceAsociado == indiceTexto).FirstOrDefault(),
                            false, ref indice);

                        indiceTexto++;
                    }
                }
            }

            ConjuntoTextosInformacion_Seleccionado = conjunto;
        }

        private void VaciarListaTextos()
        {
            var etiquetasLista = (from UIElement E in listaTextos.Children where E.GetType() == typeof(TextBlock) select E).ToList();

            while (etiquetasLista.Any())
            {
                listaTextos.Children.Remove(etiquetasLista.First());
                etiquetasLista.Remove(etiquetasLista.First());
            }

            var textosLista = (from UIElement E in listaTextos.Children where E.GetType() == typeof(TextBox) |
                               E.GetType() == typeof(ComboBox)
                               select E).ToList();

            while (textosLista.Any())
            {
                listaTextos.Children.Remove(textosLista.First());
                textosLista.Remove(textosLista.First());
            }

            var botonesLista = (from UIElement E in listaTextos.Children
                                where E.GetType() == typeof(Button) &&
                           ((Button)E).Tag != null
                                select E).ToList();

            while (botonesLista.Any())
            {
                listaTextos.Children.Remove(botonesLista.First());
                botonesLista.Remove(botonesLista.First());
            }
        }

        private void AgregarTexto(string cadenaTexto, ListaTextosDigitados listaTextosDigitados, 
            bool agregar, ref int indiceAgregar, string nombreTexto = null)
        {            
            indiceAgregar++;

            if (agregar)
            {
                if (Entrada != null)
                    Entrada.Textos.Add(cadenaTexto);
                else if (Numero != null)
                    Numero.Textos.Add(cadenaTexto);
            }

            if (listaTextosDigitados == null)
            {
                if (Entrada != null)
                {
                    Entrada.TextosDigitados.Add(new ListaTextosDigitados()
                    {
                        TextosDigitados = new List<string>(),
                        IndiceAsociado = Entrada.TextosDigitados.Count,
                        IndiceNumero = IndiceNumero
                    });
                    listaTextosDigitados = Entrada.TextosDigitados.LastOrDefault();
                }
                else if (Numero != null)
                {
                    Numero.TextosDigitados.Add(new ListaTextosDigitados()
                    {
                        TextosDigitados = new List<string>(),
                        IndiceAsociado = Numero.TextosDigitados.Count,
                        IndiceNumero = IndiceNumero
                    });
                    listaTextosDigitados = Numero.TextosDigitados.LastOrDefault();
                }
            }

            listaTextos.RowDefinitions.Add(new RowDefinition());
            listaTextos.RowDefinitions.Last().Height = GridLength.Auto;

            if (!string.IsNullOrEmpty(nombreTexto))
            {
                TextBlock nombre = new TextBlock();
                nombre.Tag = indiceAgregar;
                nombre.Margin = new Thickness(10);
                nombre.Text = nombreTexto;

                listaTextos.Children.Add(nombre);
                Grid.SetColumn(nombre, 0);
                Grid.SetRow(nombre, indiceAgregar);
            }

            ComboBox texto = new ComboBox();
            texto.IsEditable = true;
            texto.Tag = indiceAgregar;
            texto.Margin = new Thickness(10);

            LlenarComboBox_TextosDigitados(texto, listaTextosDigitados.TextosDigitados);            
            
            texto.Text = cadenaTexto;
            texto.SelectionChanged += EditarTextoSeleccion;
            texto.KeyUp += EditarTexto; 

            listaTextos.Children.Add(texto);
            Grid.SetColumn(texto, 1);
            Grid.SetRow(texto, indiceAgregar);

            if (!UtilizarSoloTextosPredefinidos)
            {
                Image ImagenBotonQuitar = new Image();
                ImagenBotonQuitar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\02.png", UriKind.Relative));
                ImagenBotonQuitar.Width = 24;
                ImagenBotonQuitar.Height = 24;

                Button botonQuitar = new Button();
                botonQuitar.Tag = indiceAgregar;
                botonQuitar.Margin = new Thickness(10);
                botonQuitar.Content = ImagenBotonQuitar;
                botonQuitar.Click += QuitarTexto;

                listaTextos.Children.Add(botonQuitar);
                Grid.SetColumn(botonQuitar, 2);
                Grid.SetRow(botonQuitar, indiceAgregar);

                Image ImagenBotonSubir = new Image();
                ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\07.png", UriKind.Relative));
                ImagenBotonSubir.Width = 24;
                ImagenBotonSubir.Height = 24;

                Button botonSubir = new Button();
                botonSubir.Content = ImagenBotonSubir;
                botonSubir.Margin = new Thickness(10);
                botonSubir.MaxHeight = 70;
                botonSubir.Tag = indice;
                botonSubir.Click += subir_Click;

                listaTextos.Children.Add(botonSubir);

                Grid.SetRow(botonSubir, indiceAgregar);
                Grid.SetColumn(botonSubir, 3);

                Image ImagenBotonBajar = new Image();
                ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos4\\08.png", UriKind.Relative));
                ImagenBotonBajar.Width = 24;
                ImagenBotonBajar.Height = 24;

                Button botonBajar = new Button();
                botonBajar.Content = ImagenBotonBajar;
                botonBajar.Margin = new Thickness(10);
                botonBajar.MaxHeight = 70;
                botonBajar.Tag = indice;
                botonBajar.Click += bajar_Click;

                listaTextos.Children.Add(botonBajar);

                Grid.SetRow(botonBajar, indiceAgregar);
                Grid.SetColumn(botonBajar, 4);
            }
        }

        private void LlenarComboBox_TextosDigitados(ComboBox combo, List<string> textosDigitados)
        {
            foreach(var itemTexto in textosDigitados)
            {
                combo.Items.Add(itemTexto);
            }
        }

        private void EditarTexto(object sender, RoutedEventArgs e)
        {
            int indiceTexto = (int)((ComboBox)sender).Tag;

            if (Entrada != null)
            {
                if (indiceTexto <= Entrada.Textos.Count - 1)
                    Entrada.Textos[indiceTexto] = ((ComboBox)sender).Text;
            }
            else if (Numero != null)
            {
                if (indiceTexto <= Numero.Textos.Count - 1)
                    Numero.Textos[indiceTexto] = ((ComboBox)sender).Text;
            }
        }

        private void EditarTextoSeleccion(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                int indiceTexto = (int)((ComboBox)sender).Tag;

                if (Entrada != null)
                {
                    if (indiceTexto <= Entrada.Textos.Count - 1)
                        Entrada.Textos[indiceTexto] = ((ComboBox)sender).SelectedItem.ToString();
                }
                else if (Numero != null)
                {
                    if (indiceTexto <= Numero.Textos.Count - 1)
                        Numero.Textos[indiceTexto] = ((ComboBox)sender).SelectedItem.ToString();
                }
            }
        }

        private void QuitarTexto(object sender, RoutedEventArgs e)
        {
            int indiceTexto = (int)((Button)sender).Tag;

            int indiceEstablecer;

            if (Entrada != null)
            {
                if (indiceTexto <= Entrada.Textos.Count - 1)
                {
                    Entrada.Textos.RemoveAt(indiceTexto);
                    Entrada.TextosDigitados.Remove(Entrada.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                    i.IndiceAsociado == indiceTexto));

                    var textosDigitados = Entrada.TextosDigitados.Where(i => i.IndiceAsociado >= indiceTexto).OrderBy(i => i.IndiceAsociado).ToList();

                    indiceEstablecer = indiceTexto;

                    foreach (var item in textosDigitados)
                    {
                        item.IndiceAsociado = indiceEstablecer;
                        indiceEstablecer++;
                    }
                }
            }
            else if (Numero != null)
            {
                if (indiceTexto <= Numero.Textos.Count - 1)
                {
                    Numero.Textos.RemoveAt(indiceTexto);
                    Numero.TextosDigitados.Remove(Numero.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                    i.IndiceAsociado == indiceTexto));

                    var textosDigitados = Numero.TextosDigitados.Where(i => i.IndiceAsociado >= indiceTexto).OrderBy(i => i.IndiceAsociado).ToList();

                    indiceEstablecer = indiceTexto;

                    foreach (var item in textosDigitados)
                    {
                        item.IndiceAsociado = indiceEstablecer;
                        indiceEstablecer++;
                    }
                }
            }

            indice--;

            UserControl_Loaded(this, e);

//            var texto = (from UIElement E in listaTextos.Children
//                         where (E.GetType() == typeof(TextBox) &&
//(int)((TextBox)E).Tag == indiceTexto) ||
//(E.GetType() == typeof(ComboBox) &&
//(int)((ComboBox)E).Tag == indiceTexto)
//                         select E).First();

//            listaTextos.Children.Remove(texto);
//            listaTextos.Children.Remove((Button)sender);


//            var botones = (from UIElement E in listaTextos.Children
//                         where (E.GetType() == typeof(Button) &&
//(int)((Button)E).Tag == indiceTexto)
//                         select E).ToList();

//            while (botones.Any())
//            {
//                listaTextos.Children.Remove(botones.FirstOrDefault());
//                botones.Remove(botones.FirstOrDefault());
//            }

//            var textos = (from UIElement E in listaTextos.Children
//                         where E.GetType() == typeof(Button) && ((Button)E).Tag != null &&
//(int)((Button)E).Tag >= indiceTexto orderby (int)((Button)E).Tag ascending
//                          select E).ToList();

//            int indiceEstablecer = indiceTexto;

//            foreach (var item in textos)
//            {
//                ((Button)item).Tag = indiceEstablecer;
//                indiceEstablecer++;
//            }

//            textos = (from UIElement E in listaTextos.Children
//                          where (E.GetType() == typeof(TextBox) && ((TextBox)E).Tag != null &&
// (int)((TextBox)E).Tag >= indiceTexto) ||
// E.GetType() == typeof(ComboBox) && ((ComboBox)E).Tag != null &&
// (int)((ComboBox)E).Tag >= indiceTexto
//                      orderby (int)((Control)E).Tag ascending
//                          select E).ToList();

//            indiceEstablecer = indiceTexto;

//            foreach (var item in textos)
//            {
//                ((Control)item).Tag = indiceEstablecer;
//                indiceEstablecer++;
//            }

            
        }

        private void subir_Click(object sender, RoutedEventArgs e)
        {
            List<string> Textos = new List<string>();
            if (Entrada != null)
                Textos = Entrada.Textos;
            else if (Numero != null)
                Textos = Numero.Textos;

            int indiceInicial = 0;
            int indiceFinal = Textos.Count - 1;

            int indiceElemento = (int)((Control)sender).Tag;
            var texto = Textos.ElementAt(indiceElemento);

            ListaTextosDigitados textosDigitados = null;
            if (Entrada != null)
                textosDigitados = Entrada.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento);
            else if (Numero != null)
                textosDigitados = Numero.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento);

            ListaTextosDigitados textosDigitadosAnterior = null;
            if (Entrada != null)
                textosDigitadosAnterior = Entrada.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento - 1);
            else if (Numero != null)
                textosDigitadosAnterior = Numero.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento - 1);

            if ((indiceElemento >= indiceInicial &&
                indiceElemento <= indiceFinal) &&
                (indiceElemento - 1 >= indiceInicial &&
                indiceElemento - 1 <= indiceFinal))
            {
                Textos.RemoveAt(indiceElemento);
                Textos.Insert(indiceElemento - 1, texto);

                textosDigitados.IndiceAsociado = indiceElemento - 1;
                textosDigitadosAnterior.IndiceAsociado = indiceElemento;

                UserControl_Loaded(this, e);
            }            
        }

        private void bajar_Click(object sender, RoutedEventArgs e)
        {
            List<string> Textos = new List<string>();
            if (Entrada != null)
                Textos = Entrada.Textos;
            else if (Numero != null)
                Textos = Numero.Textos;

            int indiceInicial = 0;
            int indiceFinal = Textos.Count - 1;

            int indiceElemento = (int)((Control)sender).Tag;
            var texto = Textos.ElementAt(indiceElemento);

            ListaTextosDigitados textosDigitados = null;
            if (Entrada != null)
                textosDigitados = Entrada.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero && 
                i.IndiceAsociado == indiceElemento);
            else if (Numero != null)
                textosDigitados = Numero.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento);

            ListaTextosDigitados textosDigitadosSiguiente = null;
            if (Entrada != null)
                textosDigitadosSiguiente = Entrada.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento + 1);
            else if (Numero != null)
                textosDigitadosSiguiente = Numero.TextosDigitados.FirstOrDefault(i => i.IndiceNumero == IndiceNumero &&
                i.IndiceAsociado == indiceElemento + 1);

            if ((indiceElemento >= indiceInicial &&
                indiceElemento <= indiceFinal) &&
                (indiceElemento + 1 >= indiceInicial &&
                indiceElemento + 1 <= indiceFinal))
            {
                Textos.RemoveAt(indiceElemento);
                Textos.Insert(indiceElemento + 1, texto);

                textosDigitados.IndiceAsociado = indiceElemento + 1;
                textosDigitadosSiguiente.IndiceAsociado = indiceElemento;

                UserControl_Loaded(this, e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListaTextosDigitados textosDigitados = null;
            if(Numero != null)
            {
                if(DigitacionConjuntoNumeros != null)
                    DigitacionConjuntoNumeros.CargarTextosDigitados(IndiceNumero);

                textosDigitados = Numero.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == indice + 1).FirstOrDefault();
            }
            else if(Entrada != null)
            {
                textosDigitados = Entrada.TextosDigitados.Where(i => i.IndiceNumero == IndiceNumero &&
                                i.IndiceAsociado == indice + 1).FirstOrDefault();
            }

            if (ConjuntoTextosInformacion_Seleccionado != null &&
                ConjuntoTextosInformacion_Seleccionado.UtilizarCiclicamente &&
                ConjuntoTextosInformacion_Seleccionado.TextosInformacion.Any())
            {
                int indiceTexto = 0;
                for (int indiceRecorrer = 0; indiceRecorrer <= indice; indiceRecorrer++)
                {
                    if (indiceTexto == ConjuntoTextosInformacion_Seleccionado.TextosInformacion.Count - 1)
                        indiceTexto = 0;
                    else
                        indiceTexto++;
                }

                if (indiceTexto > -1)
                {
                    if (ConjuntoTextosInformacion_Seleccionado.TextosInformacion[indiceTexto].SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                    {
                        AgregarTexto(ConjuntoTextosInformacion_Seleccionado.TextosInformacion[indiceTexto].Texto, textosDigitados,
                        true, ref indice, ConjuntoTextosInformacion_Seleccionado.TextosInformacion[indiceTexto].Nombre);
                    }
                }
            }
            else
            {
                AgregarTexto(string.Empty, textosDigitados, true, ref indice);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ListarTextos(null);

            if (!(Digitando && ConjuntosTextosInformacionDigitacion != null &&
                ConjuntosTextosInformacionDigitacion.Any()))
                utilizarTextosInformacionDigitacion.Visibility = Visibility.Collapsed;
            else
            {                
                if (UtilizarPrimerConjunto_Automaticamente)
                {
                    //foreach (var itemTexto in ConjuntosTextosInformacionDigitacion.FirstOrDefault().TextosInformacion)
                    //{
                    //    AgregarTexto(itemTexto.Texto, true, itemTexto.Nombre);
                    //}
                    if (Numero != null &&
                        !Numero.Textos.Any())
                    {
                        foreach (var item in ConjuntosTextosInformacionDigitacion.FirstOrDefault().TextosInformacion)
                        {
                            if (item.SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                                Numero.Textos.Add(item.Texto);
                            else
                                Numero.Textos.Add(string.Empty);
                        }
                    }

                    ListarTextos(ConjuntosTextosInformacionDigitacion.FirstOrDefault());
                }
            }

            if(UtilizarSoloTextosPredefinidos)
            {
                botonAgregar.Visibility = Visibility.Collapsed;
                vistaOpcionLista.Visibility = Visibility.Collapsed;
                vistaOpcionTexto.Visibility = Visibility.Collapsed;
            }
        }

        private void utilizarTextosInformacionDigitacion_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarConjuntoTextosInformacion_Digitacion seleccionar = new SeleccionarConjuntoTextosInformacion_Digitacion();
            seleccionar.ConjuntosTextosInformacionDigitacion = ConjuntosTextosInformacionDigitacion;
            seleccionar.ShowDialog();

            if (seleccionar.ConjuntoTextosInformacionDigitacion_Seleccionado != null)
            {
                //if (!seleccionar.ConjuntoTextosInformacionDigitacion_Seleccionado.UtilizarCiclicamente)
                //{
                //    foreach (var itemTexto in seleccionar.ConjuntoTextosInformacionDigitacion_Seleccionado.TextosInformacion)
                //    {
                //        AgregarTexto(itemTexto.Texto, true, itemTexto.Nombre);
                //    }
                //}
                if (Numero != null &&
                    !Numero.Textos.Any())
                {
                    Numero.Textos.Clear();
                    listaTextos.RowDefinitions.Clear();

                    foreach (var item in seleccionar.ConjuntoTextosInformacionDigitacion_Seleccionado.TextosInformacion)
                    {
                        if (item.SeleccionarFiltrarPosicion(Numero != null ? Numero.PosicionElemento_Lectura : 1))
                            Numero.Textos.Add(item.Texto);
                        else
                            Numero.Textos.Add(string.Empty);
                    }
                }

                ListarTextos(seleccionar.ConjuntoTextosInformacionDigitacion_Seleccionado);

                List<string> TextosInformacion = null;

                if (Entrada != null)
                    TextosInformacion = Entrada.Textos;
                else if (Numero != null)
                    TextosInformacion = Numero.Textos;

                string strNumeros = string.Empty;
                foreach (var itemNumero in TextosInformacion)
                {
                    if (itemNumero != TextosInformacion.Last())
                        strNumeros += itemNumero + ";";
                    else
                        strNumeros += itemNumero;
                }

                listaTextoNumeros.Text = strNumeros;
            }
        }

        private void vistaOpcionLista_Click(object sender, RoutedEventArgs e)
        {
            textoNumeros.Visibility = Visibility.Collapsed;
            botonAgregar.Visibility = Visibility.Visible;
            //textosInformacionSeleccionados.Visibility = Visibility.Visible;
            //ListarTextos();
            UserControl_Loaded(sender, e);
        }

        private void vistaOpcionTexto_Click(object sender, RoutedEventArgs e)
        {
            //textosInformacionSeleccionados.Visibility = Visibility.Collapsed;
            textoNumeros.Visibility = Visibility.Visible;
            botonAgregar.Visibility = Visibility.Collapsed;
            VaciarListaTextos();

            List<string> TextosInformacion = null;

            if (Entrada != null)
                TextosInformacion = Entrada.Textos;
            else if (Numero != null)
                TextosInformacion = Numero.Textos;

            string strNumeros = string.Empty;
            foreach (var itemNumero in TextosInformacion)
            {
                if (itemNumero != TextosInformacion.Last())
                    strNumeros += itemNumero + ";";
                else
                    strNumeros += itemNumero;
            }

            listaTextoNumeros.Text = strNumeros;
        }

        private void listaTextoNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textoNumeros.Visibility == Visibility.Visible)
            {
                List<string> TextosInformacion = null;

                if (Entrada != null)
                    TextosInformacion = Entrada.Textos;
                else if (Numero != null)
                    TextosInformacion = Numero.Textos;

                TextosInformacion.Clear();

                string[] lineasTexto = listaTextoNumeros.Text.Split('\n');

                foreach (var itemLineaTexto in lineasTexto)
                {
                    if (string.IsNullOrEmpty(itemLineaTexto)) continue;

                    List<string> textos = itemLineaTexto.Split(';').ToList();
                    List<string> TextosInformacion_Linea = new List<string>();

                    foreach (var itemTexto in textos)
                    {
                        TextosInformacion_Linea.Add(itemTexto);
                    }

                    TextosInformacion.AddRange(TextosInformacion_Linea);
                }
            }
        }
    }
}
