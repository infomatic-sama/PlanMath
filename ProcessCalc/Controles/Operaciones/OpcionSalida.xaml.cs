using ProcessCalc.Entidades;
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

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para OpcionSalida.xaml
    /// </summary>
    public partial class OpcionSalida : UserControl
    {
        public VistaDiseñoOperacion VistaOpciones { get; set; }
        public string NombreOperacion { get; set; }
        private DiseñoElementoOperacion disElementOper;
        public DiseñoElementoOperacion DiseñoElementoOperacion
        {
            get
            {
                return disElementOper;
            }
            set
            {
                nombresElementosRelacionados.Text = value.Nombre;
                disElementOper = value;
            }
        }
        public bool EnDiagrama;
        public bool Bloqueada { get; set; }
        public Point PuntoMouseClic { get; set; }
        public OpcionSalida()
        {
            InitializeComponent();
            nombresElementosRelacionados.Text = string.Empty;
            nombresElementosRelacionados.Visibility = Visibility.Collapsed;
            botonFondo.Background = Brushes.Transparent;
        }
        public void Clic()
        {
            botonFondo_PreviewMouseLeftButtonDown(this, null);
        }
        private void botonFondo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bloqueada) return;

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            if (!VistaOpciones.ClicDiagrama)
            {
                if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                    VistaOpciones.ElementosDiagramaSeleccionados.Any() &&
                    !VistaOpciones.ElementosDiagramaSeleccionados.Contains(this))
                {
                    VistaOpciones.diagrama_MouseLeftButtonDown(sender, e);
                }

                if (!VistaOpciones.ElementosDiagramaSeleccionados.Any())
                {
                    VistaOpciones.ElementoSeleccionado_Bool = true;
                    //VistaOpciones.EstablecerPuntoUbicacionElemento(this, e);
                    VistaOpciones.OpcionSalidaSeleccionado = this;
                    VistaOpciones.TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Salida;
                    if (EnDiagrama && DiseñoElementoOperacion != null) VistaOpciones.EstablecerTextoBotonSalida(DiseñoElementoOperacion.ContieneSalida);
                    VistaOpciones.DestacarElementoSeleccionado();
                    VistaOpciones.ElementoSeleccionado = disElementOper;
                    VistaOpciones.MostrarOrdenOperando_Elemento(disElementOper);
                }
                else
                {
                    VistaOpciones.ElementoDiagramaSeleccionadoMover = this;
                }

                DragDrop.DoDragDrop(this, disElementOper, DragDropEffects.Move);
            }
            else
            {
                Background = SystemColors.HighlightBrush;
                botonFondo.Background = SystemColors.HighlightBrush;
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (DiseñoElementoOperacion != null)
                {
                    DiseñoElementoOperacion.Anchura = ActualWidth;
                    DiseñoElementoOperacion.Altura = ActualHeight;

                    //VistaOpciones.DibujarElementosDiseñoOperacion();
                }
            }
        }
    }
}
