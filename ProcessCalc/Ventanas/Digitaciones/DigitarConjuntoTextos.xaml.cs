using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para DigitarConjuntoTextos.xaml
    /// </summary>
    public partial class DigitarConjuntoTextos : Window
    {
        public bool Pausado { get; set; }
        public List<FilaTextosInformacion_Entrada> FilasTextos { get; set; }
        public List<ConjuntoTextosInformacion_Digitacion> ConjuntoTextosInformacionDigitacion { get; set; }
        public bool FijarCantidadTextosDigitacion { get; set; }
        public int CantidadTextosDigitacion { get; set; }
        public bool FijarCantidadFilasTextosDigitacion { get; set; }
        public int CantidadFilasTextosDigitacion { get; set; }
        bool MostrarBotonQuitar = false;
        public bool UtilizarSoloTextosPredefinidos { get; set; }
        public DigitarConjuntoTextos()
        {
            FilasTextos = new List<FilaTextosInformacion_Entrada>();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(FijarCantidadFilasTextosDigitacion)
            {
                for (int veces = 1; veces <= CantidadFilasTextosDigitacion; veces++)
                    agregarFila_Click(sender, e);

                agregarFila.Visibility = Visibility.Collapsed;
            }
            else
            {
                MostrarBotonQuitar = true;

                if(FilasTextos.Any())
                {
                    int indice = 1;
                    foreach(var fila in FilasTextos)
                    {
                        agregarFila_Lista(fila, indice);
                        indice++;
                    }
                }
                else
                    agregarFila_Click(sender, e);                
            }
        }

        private void btnDetener_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Pausado = true;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void agregarFila_Click(object sender, RoutedEventArgs e)
        {
            List<string> Textos = new List<string>();
            SeleccionOrdenamiento_TextosInformacion listaTextos = new SeleccionOrdenamiento_TextosInformacion();
            listaTextos.PosicionNumero = FilasTextos.Count + 1;
            listaTextos.TextosInformacion = Textos;
            listaTextos.ConjuntoTextosInformacionDigitacion = ConjuntoTextosInformacionDigitacion;
            listaTextos.FijarCantidadTextosDigitacion = FijarCantidadTextosDigitacion;
            listaTextos.CantidadTextosDigitacion = CantidadTextosDigitacion;
            listaTextos.VistaEntradaDigitacion = this;
            listaTextos.MostrarBotonQuitar = MostrarBotonQuitar;
            listaTextos.ModoFilaTextosInformacion = true;
            listaTextos.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;
            filasTextos.Children.Add(listaTextos);
            
            FilasTextos.Add(new FilaTextosInformacion_Entrada() { TextosInformacion = Textos , PosicionElemento_Lectura = FilasTextos.Count + 1 });
        }

        private void agregarFila_Lista(FilaTextosInformacion_Entrada fila, int indice)
        {
            SeleccionOrdenamiento_TextosInformacion listaTextos = new SeleccionOrdenamiento_TextosInformacion();
            listaTextos.PosicionNumero = FilasTextos.Count + 1;
            listaTextos.TextosInformacion = fila.TextosInformacion;
            listaTextos.ConjuntoTextosInformacionDigitacion = ConjuntoTextosInformacionDigitacion;
            listaTextos.FijarCantidadTextosDigitacion = FijarCantidadTextosDigitacion;
            listaTextos.CantidadTextosDigitacion = CantidadTextosDigitacion;
            listaTextos.VistaEntradaDigitacion = this;
            listaTextos.MostrarBotonQuitar = MostrarBotonQuitar;
            listaTextos.ModoFilaTextosInformacion = true;
            listaTextos.UtilizarSoloTextosPredefinidos = UtilizarSoloTextosPredefinidos;
            filasTextos.Children.Add(listaTextos);

            fila.PosicionElemento_Lectura = indice;
        }

        public void QuitarFila(SeleccionOrdenamiento_TextosInformacion listaTextos)
        {
            FilasTextos.Remove(FilasTextos.FirstOrDefault(i => i.TextosInformacion == listaTextos.TextosInformacion));
            filasTextos.Children.Remove(listaTextos);
        }
    }
}
