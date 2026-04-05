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
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using ProcessCalc.Controles;
using System.Windows.Controls.Primitives;
using UglyToad.PdfPig.Graphics.Operations.SpecialGraphicsState;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para CalculoEspecificoDiseño.xaml
    /// </summary>
    public partial class CalculoEspecificoDiseño : UserControl
    {
        private DiseñoCalculo disCalc;
        public DiseñoCalculo DiseñoCalculo
        {
            get
            {
                return disCalc;
            }
            set
            {
                nombreCalculo.Text = value.Nombre;
                disCalc = value;
            }
        }
        public VistaDiseñoCalculos VistaCalculos { get; set; }
        public bool EnDiagrama { get; set; }
        public Point PuntoMouseClic { get; set; }
        public CalculoEspecificoDiseño()
        {
            InitializeComponent();
            botonFondo.Background = Brushes.Transparent;

            botonFondo.MouseEnter += BotonFondo_MouseEnter;
        }

        private void BotonFondo_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VistaCalculos != null)
            {
                VistaCalculos.OcultarToolTips(this);
            }

            if (DiseñoCalculo != null &&
                VistaCalculos.rectanguloSeleccion == null)
            {
                VistaCalculos.Ventana.popup.PlacementTarget = this;
                VistaCalculos.Ventana.EstablecerElementoCalculoTooltip_Ejecucion(VistaCalculos.Ventana.CalculoActual,
                    DiseñoCalculo);
                VistaCalculos.Ventana.popup.IsOpen = true;
            }
        }
        public void Clic()
        {
            if (VistaCalculos.Ventana.popup.IsOpen &&
                ((VistaCalculos.Ventana.popup.Child != null &&
                VistaCalculos.Ventana.popup.Child.IsMouseOver) ||
                VistaCalculos.Ventana.popup.Child == null))
            {
                //e.Handled = true;
                return;
            }

            Button_PreviewMouseLeftButtonDown(this, VistaCalculos.Elemento_e);
        }
        public void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VistaCalculos.Ventana.popup.IsOpen &&
                ((VistaCalculos.Ventana.popup.Child != null &&
                VistaCalculos.Ventana.popup.Child.IsMouseOver) ||
                VistaCalculos.Ventana.popup.Child == null))
            {
                return;
            }

            if (VistaCalculos != null)
            {
                VistaCalculos.OcultarToolTips(this);
            }

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            if ((e != null && e.ClickCount == 1 && e.LeftButton == MouseButtonState.Pressed) | VistaCalculos.SeleccionandoElemento |
                VistaCalculos.e_SeleccionarElemento != null)
            {
                if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                    VistaCalculos.ElementosDiagramaSeleccionados.Any() &&
                    !VistaCalculos.ElementosDiagramaSeleccionados.Contains(this))
                {
                    VistaCalculos.diagrama_MouseLeftButtonDown(sender, e);
                }

                if (!VistaCalculos.ElementosDiagramaSeleccionados.Any())
                {
                    VistaCalculos.Elemento_e = e;
                    VistaCalculos.ElementoSeleccionado_Bool = true;
                    VistaCalculos.CalculoSeleccionado = this;
                    VistaCalculos.DestacarElementoSeleccionado();
                    VistaCalculos.MostrarInfo_Elemento(disCalc);
                }
                else
                {
                    VistaCalculos.ElementoDiagramaSeleccionadoMover = this;
                }

                DragDrop.DoDragDrop(this, DiseñoCalculo, DragDropEffects.Move);
            }
            else if (e != null && e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                MostrarOcultarEdicion(true);
            }
            else
            {
                if (VistaCalculos.ClicDiagrama)
                {
                    Background = SystemColors.HighlightBrush;
                    botonFondo.Background = SystemColors.HighlightBrush;
                }
            }
        }

        private void MostrarOcultarEdicion(bool mostrar)
        {
            //OcultarIconos();

            if (mostrar)
            {
                nombreCalculo.Visibility = Visibility.Collapsed;
                edicionNombre.Text = disCalc.Nombre;
                edicionNombre.Visibility = Visibility.Visible;
                btnGuardarNombre.Visibility = Visibility.Visible;
                edicionNombre.Focus();
            }
            else
            {
                btnGuardarNombre.Visibility = Visibility.Collapsed;
                edicionNombre.Visibility = Visibility.Collapsed;
                nombreCalculo.Visibility = Visibility.Visible;
            }
        }

        private void btnGuardarNombre_Click(object sender, RoutedEventArgs e)
        {
            DiseñoCalculo encontrado = (from E in VistaCalculos.Calculo.Calculos
                                                  where !E.EsEntradasArchivo && E != disCalc && E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())
                                                  select E).FirstOrDefault();
            if (encontrado == null)
            {
                //Redibujar = true;
                disCalc.Nombre = edicionNombre.Text;
                nombreCalculo.Text = disCalc.Nombre;
                MostrarOcultarEdicion(false);
                VistaCalculos.Ventana.ActualizarNombresDescripcionesOperaciones_DesdeCalculo(disCalc);                
                //VistaOpciones.CambioTamañoOpcionOperacion(this, null);
                //VistaOpciones.DibujarElementosDiseñoOperacion();
            }
            else
            {
                MessageBox.Show("Ya existe un elemento con este nombre.", "Elemento existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void edicionNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter | e.Key == Key.Return)
                btnGuardarNombre_Click(this, null);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (edicionNombre.Visibility == Visibility.Collapsed)
            {
                if (disCalc != null)
                {
                    disCalc.AnchuraElemento = ActualWidth;
                    disCalc.AlturaElemento = ActualHeight;

                    VistaCalculos.DibujarTodasLineasElementos();
                }
            }
        }
    }
}
