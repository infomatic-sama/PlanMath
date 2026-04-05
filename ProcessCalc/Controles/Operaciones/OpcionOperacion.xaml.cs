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
    /// Lógica de interacción para OpcionOperacion.xaml
    /// </summary>
    public partial class OpcionOperacion : UserControl
    {
        private TipoOpcionOperacion tip;
        public TipoOpcionOperacion Tipo
        {
            get
            {
                return tip;
            }
            set
            {
                switch (value)
                {
                    case TipoOpcionOperacion.TodosSeparados:
                        nombreOpcion.Text = NombreOperacion + " todas las variables o vectores por separado";
                        nombreElemento.Text = NombreOperacion + " todas las variables o vectores por separado";
                        iconoTodosSeparados.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.TodosJuntos:
                        nombreOpcion.Text = NombreOperacion + " todas las variables o vectores juntos";
                        nombreElemento.Text = NombreOperacion + " todas las variables o vectores juntos";
                        iconoTodosJuntos.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.PorFila:
                        nombreOpcion.Text = NombreOperacion + " los números de las variables o vectores por fila";
                        nombreElemento.Text = NombreOperacion + " los números de las variables o vectores por fila";
                        iconoPorFila.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoPorcentaje_UnaSolaVez:
                        nombreOpcion.Text = NombreOperacion + " solo una vez";
                        nombreElemento.Text = NombreOperacion + " solo una vez";
                        iconoPorcentajes_SoloUnavez.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoPorcentaje_PorFila:
                        nombreOpcion.Text = NombreOperacion + " por fila de los vectores";
                        nombreElemento.Text = NombreOperacion + " por fila de los vectores";
                        iconoPorcentajes_PorFila.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoPotencias_UnaSolaVez:
                        nombreOpcion.Text = NombreOperacion + " solo una vez";
                        nombreElemento.Text = NombreOperacion + " solo una vez";
                        iconoPotencias_SoloUnavez.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoPotencias_PorFila:
                        nombreOpcion.Text = NombreOperacion + " por fila de los vectores";
                        nombreElemento.Text = NombreOperacion + " por fila de los vectores";
                        iconoPotencias_PorFila.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoRaices_UnaSolaVez:
                        nombreOpcion.Text = NombreOperacion + " solo una vez";
                        nombreElemento.Text = NombreOperacion + " solo una vez";
                        iconoRaices_SoloUnavez.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoRaices_PorFila:
                        nombreOpcion.Text = NombreOperacion + " por fila de los vectores";
                        nombreElemento.Text = NombreOperacion + " por fila de los vectores";
                        iconoRaices_PorFila.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoLogaritmo_UnaSolaVez:
                        nombreOpcion.Text = NombreOperacion + " solo una vez";
                        nombreElemento.Text = NombreOperacion + " solo una vez";
                        iconoLogaritmos_SoloUnavez.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoLogaritmo_PorFila:
                        nombreOpcion.Text = NombreOperacion + " por fila de los vectores";
                        nombreElemento.Text = NombreOperacion + " por fila de los vectores";
                        iconoLogaritmos_PorFila.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoFactorial:
                        nombreOpcion.Text = NombreOperacion;
                        nombreElemento.Text = NombreOperacion;
                        iconoFactorial.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.CalculandoInverso:
                        nombreOpcion.Text = NombreOperacion;
                        nombreElemento.Text = NombreOperacion;
                        iconoInverso.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.ContandoCantidades_TodosJuntos:
                        nombreOpcion.Text = NombreOperacion + " todas las variables o vectores juntos";
                        nombreElemento.Text = NombreOperacion + " todas las variables o vectores juntos";
                        iconoContarCantidades_TodosJuntos.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.ContandoCantidades_Separados:
                        nombreOpcion.Text = NombreOperacion + " todas las variables o vectores por separado";
                        nombreElemento.Text = NombreOperacion + " todas las variables o vectores por separado";
                        iconoContarCantidades_Separados.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.Nota:
                        nombreOpcion.Text = "Nota";
                        nombreElemento.Text = "Insertar una nota";
                        iconoNota.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados:
                        nombreOpcion.Text = NombreOperacion + " todas las variables o vectores por separado";
                        nombreElemento.Text = NombreOperacion + " todas las variables o vectores por separado";
                        iconoSeleccionarOrdenarTodosSeparados.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos:
                        nombreOpcion.Text = NombreOperacion + " todas las variables o vectores juntos";
                        nombreElemento.Text = NombreOperacion + " todas las variables o vectores juntos";
                        iconoSeleccionarOrdenarTodosJuntos.Visibility = Visibility.Visible;
                        break;
                    case TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir:
                        nombreOpcion.Text = "No " + NombreOperacion.ToLower() + ", solo unirlas";
                        nombreElemento.Text = "No " + NombreOperacion.ToLower() + ", solo unirlas";
                        iconoSeleccionarOrdenar_SoloUnir.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                        nombreOpcion.Text = "Números obtenidas de una variable  vector desde operador";
                        nombreElemento.Text = NombreOperacion;
                        iconoSeleccionarOrdenar_ConjuntoAgrupado.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.CondicionesFlujo:
                        nombreOpcion.Text = NombreOperacion;
                        nombreElemento.Text = NombreOperacion;
                        iconoCondicionesFlujo.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.CondicionesFlujo_PorSeparado:
                        nombreOpcion.Text = NombreOperacion;
                        nombreElemento.Text = NombreOperacion;
                        iconoCondicionesFlujo_PorSeparado.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.PorFilaPorSeparados:
                        nombreOpcion.Text = NombreOperacion + " por variable o vector separados y por filas";
                        nombreElemento.Text = NombreOperacion + " por variable o vector separados y por filas";
                        iconoPorFilasPorSeparados.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.Espera:
                        nombreOpcion.Text = "Esperar variables o vectores";
                        nombreElemento.Text = NombreOperacion;
                        iconoEspera.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.LimpiarDatos:
                        nombreOpcion.Text = "Limpiar variables o vectores";
                        nombreElemento.Text = NombreOperacion;
                        iconoLimpiarDatos.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.RedondearCantidades:
                        nombreOpcion.Text = "Redondear números de variables o vectores";
                        nombreElemento.Text = NombreOperacion;
                        iconoRedondear.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.ArchivoExterno:
                        nombreOpcion.Text = "Ejecutar cálculo desde archivo";
                        nombreElemento.Text = NombreOperacion;
                        iconoArchivoExterno.Visibility = Visibility.Visible;
                        break;

                    case TipoOpcionOperacion.SubCalculo:
                        nombreOpcion.Text = "Ejecutar cálculo";
                        nombreElemento.Text = NombreOperacion;
                        iconoSubCalculo.Visibility = Visibility.Visible;
                        break;
                }

                tip = value;
            }
        }
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
                nombreElemento.Text = value.NombreElemento;
                disElementOper = value;
            }
        }
        public bool EnDiagrama;
        public VistaDiseñoOperacion_TextosInformacion VistaOperacion_TextosInformacion { get; set; }
        public bool Modo_TextosInformacion { get; set; }
        public Point PuntoMouseClic { get; set; }
        public OpcionOperacion()
        {
            InitializeComponent();
            nombresElementosRelacionados.Text = string.Empty;
            nombresElementosRelacionados.Visibility = Visibility.Collapsed;
            botonFondo.Background = Brushes.Transparent;

            botonFondo.MouseEnter += BotonFondo_MouseEnter;
        }

        private MainWindow ObtenerVentana()
        {
            if (VistaOpciones != null)
            {
                return VistaOpciones.Ventana;
            }
            else if(VistaOperacion_TextosInformacion != null)
            {
                return VistaOperacion_TextosInformacion.Ventana;
            }

            return null;
        }

        private void BotonFondo_MouseEnter(object sender, MouseEventArgs e)
        {
            if (VistaOpciones != null)
            {
                VistaOpciones.OcultarToolTips(this);
            }

            if ((DiseñoElementoOperacion != null &&
                DiseñoElementoOperacion.Tipo != TipoElementoDiseñoOperacion.Salida) &&
                (VistaOpciones != null && VistaOpciones.rectanguloSeleccion == null))
            {
                ObtenerVentana().popup.PlacementTarget = this;
                ObtenerVentana().EstablecerSubElementoTooltip_Ejecucion(ObtenerVentana().CalculoActual,
                    DiseñoElementoOperacion);
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

            if (VistaOpciones.e_SeleccionarElemento != null)
                botonFondo_PreviewMouseLeftButtonDown(this, VistaOpciones.e_SeleccionarElemento);
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

            if (VistaOpciones != null)
            {
                VistaOpciones.OcultarToolTips(this);
            }

            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            if (edicionNombre.Visibility == Visibility.Collapsed)
            {
                if ((e != null && e.ClickCount == 1 && e.LeftButton == MouseButtonState.Pressed && !Modo_TextosInformacion) | (VistaOpciones != null && VistaOpciones.e_SeleccionarElemento != null))
                {
                    if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                    VistaOpciones.ElementosDiagramaSeleccionados.Any() &&
                    !VistaOpciones.ElementosDiagramaSeleccionados.Contains(this))
                    {
                        VistaOpciones.diagrama_MouseLeftButtonDown(sender, e);
                    }

                    if (!VistaOpciones.ElementosDiagramaSeleccionados.Any() ||
                        DiseñoElementoOperacion == null)
                    {
                        if (DiseñoElementoOperacion == null)
                            VistaOpciones.diagrama_MouseLeftButtonDown(this, e);

                        if (Tipo != TipoOpcionOperacion.Nota)
                        {
                            VistaOpciones.ElementoSeleccionado_Bool = true;
                            //VistaOpciones.EstablecerPuntoUbicacionElemento(this, e);
                            VistaOpciones.OpcionOperacionSeleccionado = this;
                            VistaOpciones.TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.OpcionOperacion;
                            if (EnDiagrama && DiseñoElementoOperacion != null) VistaOpciones.EstablecerTextoBotonSalida(DiseñoElementoOperacion.ContieneSalida);
                            VistaOpciones.DestacarElementoSeleccionado();
                            VistaOpciones.ElementoSeleccionado = disElementOper;
                            VistaOpciones.MostrarOrdenOperando_Elemento(disElementOper);
                            VistaOpciones.MostrarInfo_Elemento(disElementOper);
                            VistaOpciones.MostrarAcumulacion();
                        }
                        else
                        {
                            VistaOpciones.ElementoSeleccionado_Bool = true;
                            //VistaOpciones.EstablecerPuntoUbicacionElemento(this, e);
                            VistaOpciones.NotaSeleccionada = new Notas.NotaDiagrama();
                            VistaOpciones.TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Nota;
                            if (EnDiagrama && DiseñoElementoOperacion != null) VistaOpciones.EstablecerTextoBotonSalida(DiseñoElementoOperacion.ContieneSalida);
                            VistaOpciones.DestacarElementoSeleccionado();
                        }
                    }
                    else
                    {
                        VistaOpciones.ElementoDiagramaSeleccionadoMover = this;
                    }

                    DragDrop.DoDragDrop(this, VistaOpciones.TipoElementoDiseñoOperacionSeleccionado, DragDropEffects.Move);
                }
                else if ((e != null && e.ClickCount == 1 && e.LeftButton == MouseButtonState.Pressed) | ((Modo_TextosInformacion) && VistaOperacion_TextosInformacion != null))
                {
                    VistaOperacion_TextosInformacion.OpcionOperacionSeleccionado = this;
                    VistaOperacion_TextosInformacion.TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.OpcionOperacion;
                    VistaOperacion_TextosInformacion.DestacarElementoSeleccionado();
                    VistaOperacion_TextosInformacion.ElementoSeleccionado = disElementOper;                    
                }
                else if (e != null && e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                {
                    MostrarOcultarEdicion(true);
                }
                else
                {
                    if (VistaOpciones.ClicDiagrama)
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
                nombreElemento.Visibility = Visibility.Collapsed;
                edicionNombre.Text = disElementOper.NombreElemento;
                edicionNombre.Visibility = Visibility.Visible;
                btnGuardarNombre.Visibility = Visibility.Visible;
                edicionNombre.Focus();
            }
            else
            {
                btnGuardarNombre.Visibility = Visibility.Collapsed;
                edicionNombre.Visibility = Visibility.Collapsed;
                nombreElemento.Visibility = Visibility.Visible;
            }
        }
        private void btnGuardarNombre_Click(object sender, RoutedEventArgs e)
        {
            DiseñoElementoOperacion encontrado = (from E in VistaOpciones.Operacion.ElementosDiseñoOperacion
                                                  where E != disElementOper && ((E.NombreElemento != null && (E.NombreElemento.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())) |
(E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())))
                                                  select E).FirstOrDefault();
            if (encontrado == null)
            {
                disElementOper.NombreElemento = edicionNombre.Text;
                nombreElemento.Text = disElementOper.NombreElemento;
                MostrarOcultarEdicion(false);
                VistaOpciones.Ventana.ActualizarNombresDescripcionesElementoOperacion(disElementOper);                
                //VistaOpciones.CambioTamañoOpcionOperacion(this, null);
                //VistaOpciones.DibujarElementosDiseñoOperacion();
            }
            else
            {
                MessageBox.Show("Ya existe un variable, vector de entradas o retornado con este nombre.", "Variables, vector de entradas o retornado existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

                    if (VistaOperacion_TextosInformacion != null)
                        VistaOperacion_TextosInformacion.DibujarTodasLineasElementos();

                    if(VistaOpciones != null)
                        VistaOpciones.DibujarTodasLineasElementos();
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
                ((ObtenerVentana().popup.Child != null &&
                ObtenerVentana().popup.Child.IsMouseOver) ||
                ObtenerVentana().popup.Child == null))
            {
                return;
            }

            if (DiseñoElementoOperacion == null && VistaOpciones != null)
            {
                VistaOpciones.AgregarOpcionOperacion(sender, null);
            }
        }
    }
}
