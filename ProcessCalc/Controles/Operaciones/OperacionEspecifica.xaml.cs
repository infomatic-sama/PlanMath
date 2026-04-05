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
    /// Lógica de interacción para OperacionEspecifica.xaml
    /// </summary>
    public partial class OperacionEspecifica : UserControl
    {
        private TipoElementoOperacion tip;
        public TipoElementoOperacion Tipo
        {
            get
            {
                return tip;
            }
            set
            {
                switch (value)
                {
                    case TipoElementoOperacion.Suma:
                        nombreOperacion.Text = "Suma";
                        iconoSuma.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Resta:
                        nombreOperacion.Text = "Resta";
                        iconoResta.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Multiplicacion:
                        nombreOperacion.Text = "Multiplicación";
                        iconoMultiplicacion.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Division:
                        nombreOperacion.Text = "División";
                        iconoDivision.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Salida:
                        nombreOperacion.Text = "Salida";
                        iconoSalida.Visibility = Visibility.Visible;
                        //((ToolTip)botonFondo.ToolTip).Visibility = Visibility.Collapsed;
                        botonFondo.ToolTip = null;
                        break;
                    case TipoElementoOperacion.Nota:
                        nombreOperacion.Text = "Nota";
                        iconoNota.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Potencia:
                        nombreOperacion.Text = "Potencia";
                        iconoPotencia.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Porcentaje:
                        nombreOperacion.Text = "Porcentaje";
                        iconoPorcentaje.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Raiz:
                        nombreOperacion.Text = "Raíz";
                        iconoRaiz.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Logaritmo:
                        nombreOperacion.Text = "Logaritmo";
                        iconoLogaritmo.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Inverso:
                        nombreOperacion.Text = "Inverso";
                        iconoInverso.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Factorial:
                        nombreOperacion.Text = "Factorial";
                        iconoFactorial.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.ContarCantidades:
                        nombreOperacion.Text = "Contar números de variables o de vectores";
                        iconoContarCantidades.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.SeleccionarOrdenar:
                        nombreOperacion.Text = "Lógica de selección de números de variables o de vectores";
                        iconoSeleccionarOrdenar.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                        nombreOperacion.Text = "Números de variables o de vectores obtenidos";
                        iconoSeleccionarOrdenar_ConjuntoAgrupado.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.CondicionesFlujo:
                        nombreOperacion.Text = "Lógica de selección de variables o vectores";
                        iconoCondicionesFlujo.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.SeleccionarEntradas:
                        nombreOperacion.Text = "Lógica de selección de variables o vectores de entradas";
                        iconoSeleccionarEntradas.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.AgrupadorOperaciones:
                        nombreOperacion.Text = "Agrupar variables o vectores";
                        iconoAgrupador.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.Espera:
                        nombreOperacion.Text = "Esperar variables o vectores";
                        iconoEspera.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.LimpiarDatos:
                        nombreOperacion.Text = "Limpiar variables o vectores";
                        iconoLimpiarDatos.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.RedondearCantidades:
                        nombreOperacion.Text = "Redondear números de variables o de vectores";
                        iconoRedondear.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.ArchivoExterno:
                        nombreOperacion.Text = "Cálculo desde archivo";
                        iconoArchivoExterno.Visibility = Visibility.Visible;
                        break;
                    case TipoElementoOperacion.SubCalculo:
                        nombreOperacion.Text = "Cálculo";
                        iconoSubCalculo.Visibility = Visibility.Visible;
                        break;
                }

                tip = value;
            }
        }
        private DiseñoOperacion disOper;
        public DiseñoOperacion DiseñoOperacion 
        {
            get
            {
                return disOper;
            }
            set
            {
                nombreOperacion.Text = value.Nombre;
                disOper = value;
            }
        }
        public VistaDiseñoOperaciones VistaOperaciones { get; set; }
        public bool EnDiagrama { get; set; }
        List<DiseñoOperacion> ElementosSeleccionados = new List<DiseñoOperacion>();
        bool clickeado = false;
        bool modoResumido;
        public bool ModoIconosResumidos
        {
            get
            {
                return modoResumido;
            }

            set
            {
                modoResumido = value;
                if (modoResumido)
                {

                }
                else
                {

                }
            }
        }
        public Point PuntoMouseClic { get; set; }
        public OperacionEspecifica()
        {
            InitializeComponent();
            botonFondo.Background = Brushes.Transparent;
            
            botonFondo.MouseEnter += BotonFondo_MouseEnter;
        }

        private MainWindow ObtenerVentana()
        {
            if (VistaOperaciones != null)
            {
                return VistaOperaciones.Ventana;
            }

            return null;
        }

        private void BotonFondo_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VistaOperaciones != null)
            {
                VistaOperaciones.OcultarToolTips(this);
            }

            if ((DiseñoOperacion != null &&
                DiseñoOperacion.Tipo != TipoElementoOperacion.Salida) &&
                VistaOperaciones.rectanguloSeleccion == null)
            {
                ObtenerVentana().popup.PlacementTarget = this;
                ObtenerVentana().EstablecerElementoTooltip_Ejecucion(ObtenerVentana().CalculoActual,
                    DiseñoOperacion);
                ObtenerVentana().popup.IsOpen = true;
            }
        }
        public void Clic()
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null &&
                ObtenerVentana().popup.Child.IsMouseOver) ||
                ObtenerVentana().popup.Child == null))
            {
                //e.Handled = true;
                return;
            }

            clickeado = true;
            Button_PreviewMouseLeftButtonDown(this, VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento);
            clickeado = false;
        }
        public void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null &&
                ObtenerVentana().popup.Child.IsMouseOver) ||
                ObtenerVentana().popup.Child == null))
            {
                return;
            }

            if (VistaOperaciones != null)
            {
                VistaOperaciones.OcultarToolTips(this);
            }

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento = e;
            if (edicionNombre.Visibility == Visibility.Collapsed)
            {
                if (e != null && e.ClickCount == 1 && (e.LeftButton == MouseButtonState.Pressed | (clickeado && e.LeftButton == MouseButtonState.Released) 
                    | VistaOperaciones.SeleccionandoElemento))
                {
                    if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                    VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Any() &&
                    !VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Contains(this))
                    {
                        VistaOperaciones.diagrama_MouseLeftButtonDown(sender, e);
                    }

                    if (!VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Any() ||
                        DiseñoOperacion == null)
                    {
                        if (Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                        {
                            ElementosSeleccionados.Clear();
                            ElementosSeleccionados.AddRange(VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados);
                        }

                        if (DiseñoOperacion == null)
                            VistaOperaciones.diagrama_MouseLeftButtonDown(this, e);

                        //VistaOperaciones.ElementoMovido = false;
                        if (Tipo != TipoElementoOperacion.Nota)
                        {                            
                            VistaOperaciones.ElementoSeleccionado = true;
                            VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada = this;
                            //VistaOperaciones.EstablecerPuntoUbicacionElemento(this, e);
                            VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = Tipo;
                            if (EnDiagrama && DiseñoOperacion != null) VistaOperaciones.EstablecerTextoBotonSalida(DiseñoOperacion.ContieneSalida);
                            VistaOperaciones.DestacarElementoSeleccionado();
                            VistaOperaciones.MostrarOrdenOperando_Elemento(disOper);
                            VistaOperaciones.MostrarInfo_Elemento(disOper);
                            VistaOperaciones.MostrarAcumulacion();
                            VistaOperaciones.MostrarConfiguracionSeparadores_Elemento(disOper);                            
                        }
                        else
                        {
                            VistaOperaciones.ElementoSeleccionado = true;
                            VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada = new Notas.NotaDiagrama();
                            VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = Tipo;
                            if (EnDiagrama && DiseñoOperacion != null) VistaOperaciones.EstablecerTextoBotonSalida(DiseñoOperacion.ContieneSalida);
                            VistaOperaciones.DestacarElementoSeleccionado();
                        }
                    }
                    else
                    {
                        VistaOperaciones.CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover = this;
                    }

                    if(!clickeado)
                        DragDrop.DoDragDrop(this, tip, DragDropEffects.Move);
                }
                else if (e != null && e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                {
                    MostrarOcultarEdicion(true);
                }
                else
                {
                    if (VistaOperaciones.ClicDiagrama && disOper != null)
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

            if (mostrar && disOper != null)
            {
                nombreOperacion.Visibility = Visibility.Collapsed;
                edicionNombre.Text = disOper.Nombre;
                edicionNombre.Visibility = Visibility.Visible;
                btnGuardarNombre.Visibility = Visibility.Visible;
                edicionNombre.Focus();
            }
            else
            {
                btnGuardarNombre.Visibility = Visibility.Collapsed;
                edicionNombre.Visibility = Visibility.Collapsed;
                nombreOperacion.Visibility = Visibility.Visible;
            }
        }
        private void btnGuardarNombre_Click(object sender, RoutedEventArgs e)
        {
            DiseñoOperacion encontrado = (from E in VistaOperaciones.CalculoDiseñoSeleccionado.ElementosOperaciones
                                                  where E != disOper && E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())
                                                  select E).FirstOrDefault();
            if (encontrado == null)
            {
                disOper.Nombre = edicionNombre.Text;

                if (disOper.Tipo == TipoElementoOperacion.SubCalculo)
                    disOper.ConfigSubCalculo.SubCalculo_Operacion.NombreArchivo = disOper.Nombre;

                nombreOperacion.Text = disOper.Nombre;
                MostrarOcultarEdicion(false);
                VistaOperaciones.Ventana.ActualizarNombresDescripcionesOperacion(disOper);                
                //VistaOperaciones.CambioTamañoOperacion(this, null);
                //DiseñoOperacion.Anchura = ActualWidth;
                //DiseñoOperacion.Altura = Height;
                //VistaOperaciones.DibujarElementosOperaciones();
                //Redibujar = true;
            }
            else
            {
                MessageBox.Show("Ya existe una variable, vector de entradas o retornado con este nombre.", "Variable, vector de entradas o retornado existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                if (DiseñoOperacion != null)
                {
                    DiseñoOperacion.Anchura = ActualWidth;
                    DiseñoOperacion.Altura = ActualHeight;

                    VistaOperaciones.DibujarTodasLineasElementos();
                }
            }
        }

        private void botonFondo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (disOper == null)
            {
                if (Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                {
                    VistaOperaciones.AgruparElementosSeleccionados(ElementosSeleccionados);
                }
            }
        }

        private void botonFondo_Click(object sender, RoutedEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null &&
                ObtenerVentana().popup.Child.IsMouseOver) ||
                ObtenerVentana().popup.Child == null))
            {
                return;
            }

            if (DiseñoOperacion == null)
            {
                if(VistaOperaciones != null)
                {
                    VistaOperaciones.AgregarOperacion(sender, null);
                }
            }
        }

        private void botonFondo_Drop(object sender, DragEventArgs e)
        {
            if (ObtenerVentana().popup.IsOpen &&
                ((ObtenerVentana().popup.Child != null &&
                ObtenerVentana().popup.Child.IsMouseOver) ||
                ObtenerVentana().popup.Child == null))
            {
                return;
            }

            botonFondo_Click(this, e);
        }
    }
}
