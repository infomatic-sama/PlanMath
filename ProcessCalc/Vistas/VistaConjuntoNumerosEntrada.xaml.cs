using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Operaciones;
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
    /// Lógica de interacción para VistaConjuntoNumerosEntrada.xaml
    /// </summary>
    public partial class VistaConjuntoNumerosEntrada : UserControl
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

                opcionCantidadAgregarNumeros.IsChecked = entr.UtilizarCantidadNumeros;
            }
        }
        public Calculo Calculo { get; set; }
        public EntradaEspecifica VistaEntrada { get; set; }
        public VistaConjuntoNumerosEntrada()
        {
            InitializeComponent();
        }

        public void ListarNumeros()
        {
            contenedorNumeros.Children.Clear();

            int indiceNumero = 0;
            foreach (var itemNumero in entr.ConjuntoNumerosFijo)
            {
                Numero nuevoNumero = new Numero();
                if (itemNumero == entr.ConjuntoNumerosFijo.First())
                    nuevoNumero.EsPrimerNumero = true;
                nuevoNumero.EntNumero = itemNumero;
                nuevoNumero.textos.Numero = nuevoNumero.EntNumero;
                nuevoNumero.VistaNumeros = this;
                nuevoNumero.OpcionesListaNumeros = entr.OpcionesListaNumeros;
                nuevoNumero.Entrada = entr;
                nuevoNumero.textos.IndiceNumero = indiceNumero;
                contenedorNumeros.Children.Add(nuevoNumero);
                indiceNumero++;
            }
        }

        public void AgregarNuevoNumero()
        {
            EntidadNumero nuevo = new EntidadNumero();
            entr.ConjuntoNumerosFijo.Add(nuevo);
            nuevo.Nombre = "Número " + entr.ConjuntoNumerosFijo.Count;
            Numero nuevoNumero = new Numero();
            nuevoNumero.EntNumero = nuevo;
            nuevoNumero.textos.Numero = nuevoNumero.EntNumero;
            nuevoNumero.VistaNumeros = this;
            nuevoNumero.OpcionesListaNumeros = entr.OpcionesListaNumeros;
            nuevoNumero.Entrada = entr;
            nuevoNumero.textos.IndiceNumero = entr.ConjuntoNumerosFijo.IndexOf(nuevo);
            contenedorNumeros.Children.Add(nuevoNumero);
            VistaEntrada.MostrarOcultarConjuntoNumerosFijo(true);
            //nuevoNumero.numeroDigitado.Focus();
        }

        public void QuitarNumero(Numero numero)
        {
            entr.ConjuntoNumerosFijo.Remove(numero.EntNumero);
            contenedorNumeros.Children.Remove(numero);

            //((Numero)contenedorNumeros.Children[contenedorNumeros.Children.Count - 1]).btnOtroNumero.IsEnabled = true;
            if (contenedorNumeros.Children.Count == 0) AgregarNuevoNumero();
            VistaEntrada.MostrarOcultarConjuntoNumerosFijo(true);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            if (IsLoaded)
            {
                contenedorNumeros.Width = ActualWidth;

                string strNumeros = string.Empty;
                foreach (var itemNumero in entr.ConjuntoNumerosFijo)
                {
                    string strLinea = string.Empty;
                    foreach (var itemTexto in itemNumero.Textos)
                    {
                        strLinea += itemTexto + ";";
                    }
                    strLinea += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + "\n";

                    strNumeros += strLinea;
                }

                listaTextoNumeros.Text = strNumeros;

                if(contenedorNumeros.Children.Count > 0)
                {
                    ((Numero)contenedorNumeros.Children[0]).opcionesNumeros.Focus();
                }
            }
        }

        private void opcionCantidadAgregarNumeros_Checked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UtilizarCantidadNumeros = (bool)opcionCantidadAgregarNumeros.IsChecked;
                entr.OpcionCantidadNumeros = OpcionCantidadNumerosEntrada.AgregarCantidadNumeros;
            }
        }

        private void opcionCantidadAgregarNumeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (entr != null)
            {
                entr.UtilizarCantidadNumeros = (bool)opcionCantidadAgregarNumeros.IsChecked;
                entr.OpcionCantidadNumeros = OpcionCantidadNumerosEntrada.AgregarCantidadNumeros;
            }
        }

        private void vistaOpcionLista_Click(object sender, RoutedEventArgs e)
        {
            textoNumeros.Visibility = Visibility.Collapsed;
            contenedorNumeros.Visibility = Visibility.Visible;

            contenedorNumeros.Width = ActualWidth;
            ListarNumeros();
        }

        private void vistaOpcionTexto_Click(object sender, RoutedEventArgs e)
        {
            contenedorNumeros.Visibility = Visibility.Collapsed;
            textoNumeros.Visibility = Visibility.Visible;

            string strNumeros = string.Empty;
            foreach (var itemNumero in entr.ConjuntoNumerosFijo)
            {
                string strLinea = string.Empty;
                foreach (var itemTexto in itemNumero.Textos)
                {
                    strLinea += itemTexto + ";";
                }
                strLinea += itemNumero.Numero.ToString("N" + Calculo.CantidadDecimalesCantidades.ToString()) + "\n";

                strNumeros += strLinea;
            }

            listaTextoNumeros.Text = strNumeros;
        }

        private void listaTextoNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textoNumeros.Visibility == Visibility.Visible)
            {
                entr.ConjuntoNumerosFijo.Clear();

                string[] lineasTexto = listaTextoNumeros.Text.Split('\n');

                foreach (var itemLineaTexto in lineasTexto)
                {
                    if (string.IsNullOrEmpty(itemLineaTexto)) continue;

                    List<string> textos = itemLineaTexto.Split(';').ToList();
                    List<string> TextosInformacion = new List<string>();
                    double numero = 0;

                    foreach (var itemTexto in textos)
                    {
                        if (itemTexto != textos.Last())
                            TextosInformacion.Add(itemTexto);
                        else
                        {
                            double.TryParse(itemTexto, out numero);
                        }
                    }

                    EntidadNumero nuevo = new EntidadNumero();

                    if (TextosInformacion.Count > 0)
                        nuevo.Nombre = TextosInformacion.First();

                    nuevo.Textos.AddRange(TextosInformacion);
                    nuevo.Numero = numero;

                    entr.ConjuntoNumerosFijo.Add(nuevo);
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
