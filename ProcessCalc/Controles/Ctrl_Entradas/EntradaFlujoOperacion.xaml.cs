using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UglyToad.PdfPig.Graphics.Operations.SpecialGraphicsState;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para EntradaFlujoOperacion.xaml
    /// </summary>
    public partial class EntradaFlujoOperacion : UserControl
    {
        private DiseñoOperacion disOper;
        public DiseñoOperacion DiseñoOperacion 
        {
            get
            {
                return disOper;
            }
            set
            {
                nombreEntradaFlujo.Text = value.Nombre;

                switch (value.Tipo)
                {
                    case TipoElementoOperacion.Suma:
                        iconoSuma.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Resta:
                        iconoResta.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Multiplicacion:
                        iconoMultiplicacion.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Division:
                        iconoDivision.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Porcentaje:
                        iconoPorcentaje.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Potencia:
                        iconoPotencia.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Raiz:
                        iconoRaiz.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Logaritmo:
                        iconoLogaritmo.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Factorial:
                        iconoFactorial.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Inverso:
                        iconoInverso.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.ContarCantidades:
                        iconoContarCantidades.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.SeleccionarOrdenar:
                        iconoSeleccionarOrdenar.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                        iconoSeleccionarOrdenar_ConjuntoAgrupado.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.CondicionesFlujo:
                        iconoCondicionesFlujo.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Espera:
                        iconoEspera.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.LimpiarDatos:
                        iconoLimpiarDatos.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.RedondearCantidades:
                        iconoRedondear.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.ArchivoExterno:
                        iconoArchivoExterno.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.SubCalculo:
                        iconoSubCalculo.Visibility = Visibility.Visible;
                        break;
                }

                disOper = value;
            }
        }
        public VistaDiseñoOperacion VistaOperacion { get; set; }
        private DiseñoElementoOperacion disOperElement;
        public DiseñoElementoOperacion DiseñoElementoOperacion {
            get
            {
                return disOperElement;
            }
            set
            {
                nombreEntradaFlujo.Text = value.NombreElemento;
                nombreOperacion.Text = value.Nombre;
                disOperElement = value;
            }
        }
        public bool EnDiagrama;
        public bool Bloqueado { get; set; }
        public Point PuntoMouseClic { get; set; }
        public EntradaFlujoOperacion()
        {
            InitializeComponent();
            botonFondo.Background = Brushes.Transparent;
            nombreOperacion.Text = string.Empty;

            botonFondo.MouseEnter += BotonFondo_MouseEnter;
        }

        private MainWindow ObtenerVentana()
        {
            if (VistaOperacion != null)
            {
                return VistaOperacion.Ventana;
            }

            return null;
        }

        private void BotonFondo_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VistaOperacion != null)
            {
                VistaOperacion.OcultarToolTips(this);
            }

            if ((DiseñoOperacion != null || DiseñoElementoOperacion != null) &&
                (VistaOperacion != null && VistaOperacion.rectanguloSeleccion == null))
            {
                ObtenerVentana().popup.PlacementTarget = this;

                if (DiseñoOperacion != null)
                {
                    ObtenerVentana().EstablecerElementoTooltip_Ejecucion(ObtenerVentana().CalculoActual,
                        DiseñoOperacion);
                }
                else if(DiseñoElementoOperacion != null)
                {
                    ObtenerVentana().EstablecerSubElementoTooltip_Ejecucion(ObtenerVentana().CalculoActual,
                        DiseñoElementoOperacion);
                }
                
                ObtenerVentana().popup.IsOpen = true;
            }
        }
        public void Clic()
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                //e.Handled = true;
                return;
            }

            if (VistaOperacion.e_SeleccionarElemento != null)
                botonFondo_PreviewMouseLeftButtonDown(this, VistaOperacion.e_SeleccionarElemento);
            else
                botonFondo_PreviewMouseLeftButtonDown(this, null);
        }
        private void botonFondo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                return;
            }

            if (Bloqueado) return;

            if (VistaOperacion != null)
            {
                VistaOperacion.OcultarToolTips(this);
            }

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            if (edicionNombre.Visibility == Visibility.Collapsed)
            {
                if ((e != null && e.ClickCount == 1 && e.LeftButton == MouseButtonState.Pressed) | (VistaOperacion != null && VistaOperacion.e_SeleccionarElemento != null))
                {
                    if (!VistaOperacion.ElementosDiagramaSeleccionados.Any() ||
                        DiseñoElementoOperacion == null)
                    {
                        if (DiseñoElementoOperacion == null)
                            VistaOperacion.diagrama_MouseLeftButtonDown(this, e);

                        VistaOperacion.ElementoSeleccionado_Bool = true;
                        //VistaOperacion.EstablecerPuntoUbicacionElemento(this, e);
                        VistaOperacion.FlujoOperacionSeleccionado = this;
                        VistaOperacion.TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.FlujoOperacion;
                        if (EnDiagrama && DiseñoElementoOperacion != null) VistaOperacion.EstablecerTextoBotonSalida(DiseñoElementoOperacion.ContieneSalida);
                        VistaOperacion.DestacarElementoSeleccionado();
                        VistaOperacion.ElementoSeleccionado = disOperElement;
                        VistaOperacion.MostrarOrdenOperando_Elemento(disOperElement);
                        VistaOperacion.MostrarInfo_Elemento(disOperElement);
                        VistaOperacion.MostrarAcumulacion();
                    }
                    else
                    {
                        VistaOperacion.ElementoDiagramaSeleccionadoMover = this;
                    }

                    DragDrop.DoDragDrop(this, VistaOperacion.TipoElementoDiseñoOperacionSeleccionado, DragDropEffects.Move);
                }
                else if (e != null && e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                {
                    MostrarOcultarEdicion(true);
                }
                else
                {
                    if (VistaOperacion.ClicDiagrama)
                    {
                        Background = SystemColors.HighlightBrush;
                        botonFondo.Background = SystemColors.HighlightBrush;
                    }
                }
            }
        }

        private void MostrarOcultarEdicion(bool mostrar)
        {
            //OcultarIconos();

            if (mostrar)
            {
                nombreEntradaFlujo.Visibility = Visibility.Collapsed;
                edicionNombre.Text = disOperElement.NombreCombo;
                edicionNombre.Visibility = Visibility.Visible;
                btnGuardarNombre.Visibility = Visibility.Visible;
                edicionNombre.Focus();
            }
            else
            {
                btnGuardarNombre.Visibility = Visibility.Collapsed;
                edicionNombre.Visibility = Visibility.Collapsed;
                nombreEntradaFlujo.Visibility = Visibility.Visible;
            }
        }
        private void btnGuardarNombre_Click(object sender, RoutedEventArgs e)
        {
            DiseñoElementoOperacion encontrado = (from E in VistaOperacion.Operacion.ElementosDiseñoOperacion
                                                  where E != disOperElement && ((E.NombreElemento != null && (E.NombreElemento.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())) |
(E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())))
                                                  select E).FirstOrDefault();
            if (encontrado == null)
            {
                disOperElement.NombreElemento = edicionNombre.Text;
                nombreEntradaFlujo.Text = disOperElement.NombreElemento;
                MostrarOcultarEdicion(false);
                VistaOperacion.Ventana.ActualizarNombresDescripcionesElementoOperacion(disOperElement);                
                //VistaOpciones.CambioTamañoOpcionOperacion(this, null);
                //VistaOpciones.DibujarElementosDiseñoOperacion();
            }
            else
            {
                MessageBox.Show("Ya existe una variable, vector u operación con este nombre.", "Variable, vector u operación existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DiseñoElementoOperacion != null)
                {
                    DiseñoElementoOperacion.Anchura = ActualWidth;
                    DiseñoElementoOperacion.Altura = ActualHeight;

                    if(VistaOperacion != null)
                        VistaOperacion.DibujarTodasLineasElementos();
                }
            }
        }

        private void botonFondo_Drop(object sender, DragEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                return;
            }

            botonFondo_Click(sender, e);
        }

        private void botonFondo_Click(object sender, RoutedEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null && ObtenerVentana().popup.Child.IsMouseOver) || ObtenerVentana().popup.Child == null))
            {
                return;
            }

            if (DiseñoElementoOperacion == null && VistaOperacion != null)
            {
                VistaOperacion.AgregarFlujoOperacion(sender, null);
            }
        }
    }
}
