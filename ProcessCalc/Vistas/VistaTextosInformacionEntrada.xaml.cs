using ProcessCalc.Controles;
using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    /// Lógica de interacción para VistaTextosInformacionEntrada.xaml
    /// </summary>
    public partial class VistaTextosInformacionEntrada : UserControl
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
                entr = value;

            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        public VistaTextosInformacionEntrada()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            if (IsLoaded)
            {
                foreach (var textos in entr.ConjuntoTextosInformacionFijos)
                {
                    SeleccionOrdenamiento_TextosInformacion contenedorTextosInformacion = new SeleccionOrdenamiento_TextosInformacion();
                    contenedorTextosInformacion.Width = ActualWidth;
                    contenedorTextosInformacion.TextosInformacion = textos.TextosInformacion;
                    contenedorTextosInformacion.VistaTextos = this;

                    contenedorTextosInformacion.quitarConjuntoTextos.Visibility = Visibility.Visible;

                    string strNumeros = string.Empty;
                    foreach (var itemNumero in textos.TextosInformacion)
                    {
                        if (itemNumero != textos.TextosInformacion.Last())
                            strNumeros += itemNumero + ";";
                        else
                            strNumeros += itemNumero;
                    }

                    //listaTextoNumeros.Text = strNumeros;
                    contenedorTextosInformacion.Tag = textos;
                    contenedorTextos.Children.Add(contenedorTextosInformacion);
                }
            }
        }

        private void vistaOpcionLista_Click(object sender, RoutedEventArgs e)
        {
            textoNumeros.Visibility = Visibility.Collapsed;
            contenedorTextos.Visibility = Visibility.Visible;

            foreach (SeleccionOrdenamiento_TextosInformacion fila in contenedorTextos.Children)
            {
                fila.Width = ActualWidth;
                fila.TextosInformacion = ((FilaTextosInformacion_Entrada)fila.Tag).TextosInformacion;
                fila.ListarTextos();
            }
        }

        private void vistaOpcionTexto_Click(object sender, RoutedEventArgs e)
        {
            contenedorTextos.Visibility = Visibility.Collapsed;
            textoNumeros.Visibility = Visibility.Visible;
            listaTextoNumeros.Children.Clear();

            foreach (var textos in entr.ConjuntoTextosInformacionFijos)
            {
                TextBox listaTextoNumeros_Item = new TextBox();
                listaTextoNumeros_Item.Padding = new Thickness(5);
                listaTextoNumeros_Item.Margin = new Thickness(5);

                listaTextoNumeros_Item.TextWrapping = TextWrapping.Wrap;
                listaTextoNumeros_Item.AcceptsReturn = true;
                listaTextoNumeros_Item.TextChanged += listaTextoNumeros_TextChanged;

                string strNumeros = string.Empty;
                foreach (var itemNumero in textos.TextosInformacion)
                {
                    if (itemNumero != textos.TextosInformacion.Last())
                        strNumeros += itemNumero + ";";
                    else
                        strNumeros += itemNumero;
                }

                listaTextoNumeros_Item.Tag = textos;
                listaTextoNumeros_Item.Text = strNumeros;                
                listaTextoNumeros.Children.Add(listaTextoNumeros_Item);
            }
        }

        private void listaTextoNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textoNumeros.Visibility == Visibility.Visible)
            {
                ((FilaTextosInformacion_Entrada)((TextBox)sender).Tag).TextosInformacion.Clear();

                string[] lineasTexto = ((TextBox)sender).Text.Split('\n');

                foreach (var itemLineaTexto in lineasTexto)
                {
                    if (string.IsNullOrEmpty(itemLineaTexto)) continue;

                    List<string> textos = itemLineaTexto.Split(';').ToList();
                    List<string> TextosInformacion = new List<string>();
                    
                    foreach (var itemTexto in textos)
                    {
                        TextosInformacion.Add(itemTexto);
                    }

                    ((FilaTextosInformacion_Entrada)((TextBox)sender).Tag).TextosInformacion.AddRange(TextosInformacion);
                }
            }
        }

        private void agregarConjuntoTextos_Click(object sender, RoutedEventArgs e)
        {
            entr.ConjuntoTextosInformacionFijos.Add(new FilaTextosInformacion_Entrada());

            SeleccionOrdenamiento_TextosInformacion contenedorTextosInformacion = new SeleccionOrdenamiento_TextosInformacion();
            contenedorTextosInformacion.Width = ActualWidth;
            contenedorTextosInformacion.TextosInformacion = entr.ConjuntoTextosInformacionFijos.Last().TextosInformacion;
            
            string strNumeros = string.Empty;
            foreach (var itemNumero in entr.ConjuntoTextosInformacionFijos.Last().TextosInformacion)
            {
                if (itemNumero != entr.ConjuntoTextosInformacionFijos.Last().TextosInformacion.Last())
                    strNumeros += itemNumero + ";";
                else
                    strNumeros += itemNumero;
            }

            contenedorTextosInformacion.Tag = entr.ConjuntoTextosInformacionFijos.Last();
            contenedorTextos.Children.Add(contenedorTextosInformacion);

            if(textoNumeros.Visibility == Visibility.Visible)
                vistaOpcionTexto_Click(this, e);
            else if(contenedorTextos.Visibility == Visibility.Visible)
                vistaOpcionLista_Click(this, e);
        }

        public void QuitarConjuntoTextos(SeleccionOrdenamiento_TextosInformacion item)
        {
            entr.ConjuntoTextosInformacionFijos.Remove((FilaTextosInformacion_Entrada)item.Tag);
            contenedorTextos.Children.Remove(item);
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
