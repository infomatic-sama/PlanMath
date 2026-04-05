using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Controles.Notas;
using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaDiseñoCalculos.xaml
    /// </summary>
    public partial class VistaDiseñoCalculos : UserControl
    {
        private Calculo calc;
        public Calculo Calculo
        {
            set
            {
                nombreArchivo.Content = value.NombreArchivo;
                rutaArchivo.Content = value.RutaArchivo;

                calc = value;

            }
            get
            {
                return calc;
            }
        }
        public MainWindow Ventana { get; set; }
        public CalculoEspecificoDiseño CalculoSeleccionado { get; set; }
        private ArrowLine lineaSeleccionada;
        public bool ElementoSeleccionado_Bool;
        private DiseñoCalculo ElementoSeleccionado;
        public bool SeleccionandoElemento { get; set; }
        private DiseñoCalculo ElementoAnteriorLineaSeleccionada;
        private DiseñoCalculo ElementoPosteriorLineaSeleccionada;
        public bool primerClicCalculo_Calculo;
        public MouseButtonEventArgs Elemento_e { get; set; }
        Point ubicacionInicialAreaSeleccionada;
        Point ubicacionFinalAreaSeleccionada;
        public Rectangle rectanguloSeleccion;
        public bool ClicDiagrama;
        //bool elementoSeleccionado;
        List<object> ElementosSeleccionados = new List<object>();
        bool MostrandoInformacionElemento;
        List<UIElement> ElementosEncontrados = new List<UIElement>();
        int indiceElementosEncontrados = -1;
        double escalaZoom = 1;
        public MouseButtonEventArgs e_SeleccionarElemento { get; set; }
        public NotaDiagrama NotaSeleccionada { get; set; }
        public Point ubicacionActualElemento { get; set; }
        public UIElement ElementoDiagramaSeleccionadoMover;
        public List<UIElement> ElementosDiagramaSeleccionados = new List<UIElement>();
        public VistaDiseñoCalculos()
        {
            calc = new Calculo();
            InitializeComponent();
        }

        private void EstablecerCoordenadasElementoMover(UIElement elemento, DiseñoCalculo elementoCalculo, DragEventArgs e)
        {
            Point puntoElemento;

            if (elemento.GetType() == typeof(CalculoEspecificoDiseño))
                puntoElemento = ((CalculoEspecificoDiseño)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(NotaDiagrama))
                puntoElemento = ((NotaDiagrama)elemento).PuntoMouseClic;

            if (e.GetPosition(diagrama).X > (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y >= Canvas.GetTop(elemento) &
                e.GetPosition(diagrama).Y <= (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(e.GetPosition(diagrama).X - Canvas.GetLeft(elemento), 0);
                puntoElemento.Y = 0;
            }
            else if (e.GetPosition(diagrama).X < (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y >= Canvas.GetTop(elemento) &
                e.GetPosition(diagrama).Y <= (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(-(Canvas.GetLeft(elemento) - e.GetPosition(diagrama).X), 0);
                puntoElemento.Y = 0;
            }
            else if (e.GetPosition(diagrama).X >= Canvas.GetLeft(elemento) &
                e.GetPosition(diagrama).X <= (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y > (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(0, e.GetPosition(diagrama).Y - Canvas.GetTop(elemento));
                puntoElemento.X = 0;
            }
            else if (e.GetPosition(diagrama).X >= Canvas.GetLeft(elemento) &
                e.GetPosition(diagrama).X <= (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y < (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(0, -(Canvas.GetTop(elemento) - e.GetPosition(diagrama).Y));
                puntoElemento.X = 0;
            }
            else if (e.GetPosition(diagrama).X > (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y > (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(e.GetPosition(diagrama).X - Canvas.GetLeft(elemento),
                                    e.GetPosition(diagrama).Y - Canvas.GetTop(elemento));
            }
            else if (e.GetPosition(diagrama).X < (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y < (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(-(Canvas.GetLeft(elemento) - e.GetPosition(diagrama).X),
                                    -(Canvas.GetTop(elemento) - e.GetPosition(diagrama).Y));
            }
            else if (e.GetPosition(diagrama).X > (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y < (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(e.GetPosition(diagrama).X - Canvas.GetLeft(elemento),
                    -(Canvas.GetTop(elemento) - e.GetPosition(diagrama).Y));
            }
            else if (e.GetPosition(diagrama).X < (Canvas.GetLeft(elemento) + elemento.RenderSize.Width) &
                e.GetPosition(diagrama).Y > (Canvas.GetTop(elemento) + elemento.RenderSize.Height))
            {
                ubicacionActualElemento = new Point(-(Canvas.GetLeft(elemento) - e.GetPosition(diagrama).X),
                    e.GetPosition(diagrama).Y - Canvas.GetTop(elemento));
            }

            ubicacionActualElemento = new Point(elementoCalculo.PosicionX + ubicacionActualElemento.X - puntoElemento.X,
           elementoCalculo.PosicionY + ubicacionActualElemento.Y - puntoElemento.Y);

        }

        public void DibujarElementosCalculos()
        {
            //foreach (var item in diagrama.Children)
            //{
            //    if (item.GetType() == typeof(CalculoEspecificoDiseño))
            //        Calculo.ListaCalculos_Visuales.Remove((CalculoEspecificoDiseño)item);
            //}

            diagrama.Children.Clear();

            CalculoSeleccionado = null;
            lineaSeleccionada = null;

            foreach (var itemElemento in calc.Calculos)
            {
                if (itemElemento.EsEntradasArchivo) continue;

                CalculoEspecificoDiseño nuevoCalculo = new CalculoEspecificoDiseño();
                nuevoCalculo.SizeChanged += CambioTamañoCalculo;
                diagrama.Children.Add(nuevoCalculo);

                Canvas.SetTop(nuevoCalculo, itemElemento.PosicionY);
                Canvas.SetLeft(nuevoCalculo, itemElemento.PosicionX);

                nuevoCalculo.VistaCalculos = this;
                nuevoCalculo.EnDiagrama = true;
                nuevoCalculo.botonFondo.BorderBrush = Brushes.Black;
                nuevoCalculo.DiseñoCalculo = itemElemento;

                if (ElementoSeleccionado != null &&
                        ElementoSeleccionado == itemElemento)
                {
                    nuevoCalculo.Background = SystemColors.HighlightBrush;
                    nuevoCalculo.botonFondo.Background = SystemColors.HighlightTextBrush;
                }
                else
                {
                    nuevoCalculo.Background = SystemColors.GradientInactiveCaptionBrush;
                    nuevoCalculo.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
            }

            DibujarTodasLineasElementos();

            EstablecerIndicesProfundidadElementos();

            foreach (var itemElemento in calc.Notas)
            {
                NotaDiagrama nuevaNota = new NotaDiagrama();
                diagrama.Children.Add(nuevaNota);

                Canvas.SetTop(nuevaNota, itemElemento.PosicionY);
                Canvas.SetLeft(nuevaNota, itemElemento.PosicionX);

                nuevaNota.VistaCalculos = this;
                nuevaNota.EnDiagrama = true;
                nuevaNota.DiseñoOperacion = itemElemento;

                if (NotaSeleccionada != null &&
                        NotaSeleccionada.DiseñoOperacion == itemElemento)
                {
                    nuevaNota.fondo.BorderThickness = new Thickness(1);
                }
                else
                {
                    nuevaNota.fondo.BorderThickness = new Thickness(0);
                }
            }
        }

        public void DibujarLineasElemento(DiseñoCalculo item)
        {
            foreach (var itemElemento in item.ElementosPosteriores)
            {
                if (itemElemento.EsEntradasArchivo) continue;

                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;
                    diagrama.Children.Add(nuevaLinea);
                }

            }

            foreach (var itemElemento in item.ElementosAnteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;
                    diagrama.Children.Add(nuevaLinea);
                }

            }
        }
        public void DibujarTodasLineasElementos()
        {
            QuitarTodasLineas();

            foreach (var itemElemento in calc.Calculos)
            {
                DibujarLineasElemento(itemElemento);
            }
        }

        private void diagrama_Drop(object sender, DragEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                if (CalculoSeleccionado != null)
                {
                    DiseñoCalculo arrastreHastaOtroElemento;
                    arrastreHastaOtroElemento = VerificarArrastreOtroElemento(e.GetPosition(diagrama));

                    if (CalculoSeleccionado.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoSeleccionado) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoSeleccionado) + CalculoSeleccionado.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoSeleccionado) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoSeleccionado) + CalculoSeleccionado.ActualHeight))
                            return;

                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != CalculoSeleccionado.DiseñoCalculo)
                        {
                            if (!CalculoSeleccionado.DiseñoCalculo.ElementosPosteriores.Contains(arrastreHastaOtroElemento) &&
                            !arrastreHastaOtroElemento.ElementosAnteriores.Contains(CalculoSeleccionado.DiseñoCalculo))
                            {
                                CalculoSeleccionado.DiseñoCalculo.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                arrastreHastaOtroElemento.ElementosAnteriores.Add(CalculoSeleccionado.DiseñoCalculo);
                                CalculoSeleccionado.DiseñoCalculo.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);

                                ActualizarElementosResultados_CalculoConectado(CalculoSeleccionado.DiseñoCalculo);
                                DibujarTodasLineasElementos();
                                EstablecerIndicesProfundidadElementos();
                            }
                        }
                        else
                        {
                            EstablecerCoordenadasElementoMover(CalculoSeleccionado, CalculoSeleccionado.DiseñoCalculo, e);

                            CalculoSeleccionado.DiseñoCalculo.PosicionX = ubicacionActualElemento.X;
                            CalculoSeleccionado.DiseñoCalculo.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(CalculoSeleccionado, CalculoSeleccionado.DiseñoCalculo.PosicionY);
                            Canvas.SetLeft(CalculoSeleccionado, CalculoSeleccionado.DiseñoCalculo.PosicionX);

                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                }
                else if (NotaSeleccionada != null)
                {
                    if (NotaSeleccionada.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(NotaSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(NotaSeleccionada) + NotaSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(NotaSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(NotaSeleccionada) + NotaSeleccionada.ActualHeight))
                            return;

                        EstablecerCoordenadasElementoMover(NotaSeleccionada, 
                            new DiseñoCalculo(NotaSeleccionada.DiseñoOperacion.PosicionX, NotaSeleccionada.DiseñoOperacion.PosicionY), e);

                        NotaSeleccionada.DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                        NotaSeleccionada.DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                        Canvas.SetTop(NotaSeleccionada, NotaSeleccionada.DiseñoOperacion.PosicionY);
                        Canvas.SetLeft(NotaSeleccionada, NotaSeleccionada.DiseñoOperacion.PosicionX);

                    }
                }
                
                if(ElementoSeleccionado == null) ElementoSeleccionado_Bool = false;
            }
            else if (ElementosSeleccionados.Any())
            {
                Point diferenciaDistanciaPunto = new Point(0, 0);

                if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(CalculoEspecificoDiseño))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo.PosicionX,
                        ubicacionActualElemento.Y - ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo.PosicionY);

                    ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo.PosicionX = ubicacionActualElemento.X;
                    ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((CalculoEspecificoDiseño)ElementoDiagramaSeleccionadoMover).DiseñoCalculo.PosicionX);

                }
                else if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(NotaDiagrama))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        new DiseñoCalculo(((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX,
                        ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY), e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX,
                        ubicacionActualElemento.Y - ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);

                    ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                    ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX);
                }
                
                foreach (UIElement elemento in ElementosDiagramaSeleccionados)
                {
                    if (elemento != null)
                    {
                        if (elemento != ElementoDiagramaSeleccionadoMover)
                        {
                            if (elemento.GetType() == typeof(CalculoEspecificoDiseño))
                            {
                                ((CalculoEspecificoDiseño)elemento).DiseñoCalculo.PosicionX += diferenciaDistanciaPunto.X;
                                ((CalculoEspecificoDiseño)elemento).DiseñoCalculo.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((CalculoEspecificoDiseño)elemento).DiseñoCalculo.PosicionY);
                                Canvas.SetLeft(elemento, ((CalculoEspecificoDiseño)elemento).DiseñoCalculo.PosicionX);

                            }
                            else if (elemento.GetType() == typeof(NotaDiagrama))
                            {
                                ((NotaDiagrama)elemento).DiseñoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((NotaDiagrama)elemento).DiseñoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((NotaDiagrama)elemento).DiseñoOperacion.PosicionY);
                                Canvas.SetLeft(elemento, ((NotaDiagrama)elemento).DiseñoOperacion.PosicionX);
                            }                            
                        }
                    }
                }

                DibujarTodasLineasElementos();
                EstablecerIndicesProfundidadElementos();
            }
        }

        private void SeleccionarPrimerCalculo()
        {
            CalculoEspecificoDiseño calculo = (from UIElement calc in diagrama.Children where calc.GetType() == typeof(CalculoEspecificoDiseño) select (CalculoEspecificoDiseño)calc).FirstOrDefault();
            Elemento_e = null;
            ElementoSeleccionado_Bool = true;
            CalculoSeleccionado = calculo;
            DestacarElementoSeleccionado();
            MostrarInfo_Elemento(calculo.DiseñoCalculo);

            Ventana.CalculoActual.SubCalculoSeleccionado_Operaciones = (from C in Ventana.CalculoActual.Calculos where !C.EsEntradasArchivo select C).FirstOrDefault();
            var vista = Ventana.vistaOperaciones.Where(i => i.Calculo == Ventana.CalculoActual).FirstOrDefault();
            
            if(vista != null) vista.CalculoClikeado = true;

            Ventana.CalculoActual.SubCalculoSeleccionado_TextosInformacion = (from C in Ventana.CalculoActual.Calculos where !C.EsEntradasArchivo select C).FirstOrDefault();
            var vista2 = Ventana.vistaTextosInformacion.Where(i => i.CalculoSeleccionado == Ventana.CalculoActual).FirstOrDefault();

            if (vista2 != null) vista2.CalculoClikeado = true;
        }

        public void btnAgregarCalculo_Click(object sender, RoutedEventArgs e)
        {
            CalculoEspecificoDiseño nuevoCalculo = new CalculoEspecificoDiseño();            
            nuevoCalculo.VistaCalculos = this;
            nuevoCalculo.EnDiagrama = true;
            nuevoCalculo.botonFondo.BorderBrush = Brushes.Black;
            nuevoCalculo.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevoCalculo.SizeChanged += CambioTamañoCalculo;

            DiseñoCalculo nuevoElementoCalculo = new DiseñoCalculo();
            nuevoElementoCalculo.ID = App.GenerarID_Elemento();

            var ultimoNombre = (from DiseñoCalculo E in calc.Calculos where !E.EsEntradasArchivo && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null)
            {
                int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            }
            cantidadElementosTipo++;

            nuevoElementoCalculo.Nombre = "Cálculo " + cantidadElementosTipo.ToString();

            calc.Calculos.Add(nuevoElementoCalculo);

            DiseñoCalculo calculoEntradas = Calculo.ObtenerCalculoEntradas();
            if (calculoEntradas != null)
            {
                calculoEntradas.ElementosPosteriores.Add(nuevoElementoCalculo);
                nuevoElementoCalculo.ElementosAnteriores.Add(calculoEntradas);
                calculoEntradas.ElementosContenedoresOperacion.Add(nuevoElementoCalculo);
            }

            nuevoCalculo.DiseñoCalculo = nuevoElementoCalculo;

            diagrama.Children.Add(nuevoCalculo);

            ubicacionActualElemento = new Point(App.PosicionXPredeterminada, App.PosicionYPredeterminada);

            //EstablecerCoordenadasElementoMover(nuevoCalculo, nuevoCalculo.DiseñoCalculo, null);
            nuevoElementoCalculo.PosicionX = ubicacionActualElemento.X;
            nuevoElementoCalculo.PosicionY = ubicacionActualElemento.Y;

            Canvas.SetTop(nuevoCalculo, ubicacionActualElemento.Y);
            Canvas.SetLeft(nuevoCalculo, ubicacionActualElemento.X);

            //if(double.IsNaN(nuevoCalculo.DiseñoCalculo.AnchuraElemento) &
            //    double.IsNaN(nuevoCalculo.DiseñoCalculo.AlturaElemento))
            //{
            //    nuevoElementoCalculo.AnchuraElemento = nuevoCalculo.ActualWidth;
            //    nuevoElementoCalculo.AlturaElemento = nuevoCalculo.ActualHeight;
            //}
            DibujarTodasLineasElementos();
            EstablecerIndicesProfundidadElementos();
            //e_SeleccionarElemento = new MouseButtonEventArgs(Mouse.PrimaryDevice, DateTime.Now.Hour, MouseButton.Left);
            //Ventana.Dispatcher.Invoke(nuevoCalculo.Clic);
            //e_SeleccionarElemento = null;
            //if (itemElemento.ToolTip != null)
            //    nuevaOperacion.botonFondo.ToolTip = itemElemento.ToolTip;
            //else
            //{
            //Ventana.ObtenerEjecucionToolTips(Calculo).AgregarToolTipCalculo_Asociaciones((ToolTipElementoVisual)((ContentControl)nuevoCalculo.ToolTip).Content,
            //    nuevoElementoCalculo.ID);
            //    Calculo.ConCambiosVisuales = true;
            ////}
        }

        private void QuitarTodasLineas()
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var itemLinea in lineas)
                diagrama.Children.Remove(itemLinea);
        }

        private void CalcularCoordenadasLinea(ref double posicionXOrigen, ref double posicionYOrigen,
                ref double posicionXDestino, ref double posicionYDestino, DiseñoCalculo elementoOrigen,
                DiseñoCalculo elementoDestino)
        {
            posicionXOrigen = elementoOrigen.PosicionX + elementoOrigen.AnchuraElemento / 2;
            posicionYOrigen = elementoOrigen.PosicionY + elementoOrigen.AlturaElemento / 2;

            posicionXDestino = elementoDestino.PosicionX + elementoDestino.AnchuraElemento / 2;
            posicionYDestino = elementoDestino.PosicionY + elementoDestino.AlturaElemento / 2;

            if (posicionXDestino < elementoOrigen.PosicionX)
                posicionXOrigen -= elementoOrigen.AnchuraElemento / 2;
            else if (posicionXDestino > elementoOrigen.PosicionX + elementoOrigen.AnchuraElemento)
                posicionXOrigen += elementoOrigen.AnchuraElemento / 2;

            if (posicionYDestino < elementoOrigen.PosicionY)
                posicionYOrigen -= elementoOrigen.AlturaElemento / 2;
            else if (posicionYDestino > elementoOrigen.PosicionY + elementoOrigen.AlturaElemento)
                posicionYOrigen += elementoOrigen.AlturaElemento / 2;



            if (posicionXOrigen < elementoDestino.PosicionX)
                posicionXDestino -= elementoDestino.AnchuraElemento / 2;
            else if (posicionXOrigen > elementoDestino.PosicionX + elementoDestino.AnchuraElemento)
                posicionXDestino += elementoDestino.AnchuraElemento / 2;

            if (posicionYOrigen < elementoDestino.PosicionY)
                posicionYDestino -= elementoDestino.AlturaElemento / 2;
            else if (posicionYOrigen > elementoDestino.PosicionY + elementoDestino.AlturaElemento)
                posicionYDestino += elementoDestino.AlturaElemento / 2;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            DibujarElementosCalculos();
            DestacarElementoSeleccionado();

            if (double.IsNaN(Calculo.Ancho) &
                double.IsNaN(Calculo.Alto))
            {
                diagrama.Width = contenedor.ActualWidth;
                Calculo.Ancho = diagrama.Width;

                diagrama.Height = contenedor.ActualHeight;
                Calculo.Alto = diagrama.Height;

            }
            else
            {
                diagrama.Width = Calculo.Ancho;
                diagrama.Height = Calculo.Alto;
            }

            if (!primerClicCalculo_Calculo)
            {
                if (ElementoSeleccionado != null)
                {
                    SeleccionandoElemento = true;
                    foreach (var itemElemento in diagrama.Children)
                    {
                        if (itemElemento.GetType() == typeof(CalculoEspecificoDiseño) &&
                            ((CalculoEspecificoDiseño)itemElemento).DiseñoCalculo == ElementoSeleccionado)
                        //((CalculoEspecificoDiseño)itemElemento).Button_PreviewMouseLeftButtonDown(this, new MouseButtonEventArgs(Mouse.PrimaryDevice, (int)DateTime.Now.TimeOfDay.TotalSeconds, MouseButton.Left));
                        {
                            if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                            {
                                Ventana.Dispatcher.Invoke(((CalculoEspecificoDiseño)itemElemento).Clic, DispatcherPriority.Loaded);
                            }
                            //((CalculoEspecificoDiseño)itemElemento).RaiseEvent(Elemento_e);
                        }
                    }
                    SeleccionandoElemento = false;
                }
            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("VerConfigurarCalculosArchivoCalculo");
#endif
        }

        private DiseñoCalculo VerificarArrastreOtroElemento(Point ubicacion)
        {
            foreach (var itemElemento in calc.Calculos)
            {
                var itemControl = (from UIElement C in diagrama.Children
                                   where
(C.GetType() == typeof(CalculoEspecificoDiseño) && ((CalculoEspecificoDiseño)C).DiseñoCalculo == itemElemento)
                                   select C).FirstOrDefault();

                if (itemControl != null)
                {
                    if (ubicacion.X >= itemElemento.PosicionX & ubicacion.X <= itemElemento.PosicionX + itemControl.RenderSize.Width &
                        ubicacion.Y >= itemElemento.PosicionY & ubicacion.Y <= itemElemento.PosicionY + itemControl.RenderSize.Height)
                    {
                        return itemElemento;
                    }
                }
            }
            return null;
        }

        private ArrowLine BuscarLinea(DiseñoCalculo elementoOrigen, DiseñoCalculo elementoDestino)
        {
            foreach (var item in calc.Calculos)
            {
                var elementoDestinoEncontrado = (from DiseñoCalculo E in item.ElementosAnteriores where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY select E).FirstOrDefault();
                var elementoOrigenEncontrado = (from DiseñoCalculo E in item.ElementosPosteriores where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY select E).FirstOrDefault();

                var elementoDestino2Encontrado = (from DiseñoCalculo E in item.ElementosPosteriores where E == elementoOrigen && item == elementoDestino select E).FirstOrDefault();
                var elementoOrigen2Encontrado = (from DiseñoCalculo E in item.ElementosAnteriores where E == elementoDestino && item == elementoOrigen select E).FirstOrDefault();

                if (elementoOrigenEncontrado != null | elementoDestinoEncontrado != null)
                {
                    ArrowLine linea = new ArrowLine();
                    linea.Stroke = Brushes.Black;

                    double posicionXOrigen = 0;
                    double posicionYOrigen = 0;

                    double posicionXDestino = 0;
                    double posicionYDestino = 0;

                    CalcularCoordenadasLinea(ref posicionXOrigen, ref posicionYOrigen, ref posicionXDestino,
                        ref posicionYDestino, elementoOrigen, elementoDestino);

                    linea.X1 = posicionXOrigen;
                    linea.Y1 = posicionYOrigen;

                    linea.X2 = posicionXDestino;
                    linea.Y2 = posicionYDestino;


                    return linea;
                }

                if (elementoOrigen2Encontrado != null | elementoDestino2Encontrado != null)
                {
                    ArrowLine linea = new ArrowLine();
                    linea.Stroke = Brushes.Black;

                    double posicionXOrigen = 0;
                    double posicionYOrigen = 0;

                    double posicionXDestino = 0;
                    double posicionYDestino = 0;

                    CalcularCoordenadasLinea(ref posicionXOrigen, ref posicionYOrigen, ref posicionXDestino,
                        ref posicionYDestino, elementoDestino, elementoOrigen);

                    linea.X1 = posicionXOrigen;
                    linea.Y1 = posicionYOrigen;

                    linea.X2 = posicionXDestino;
                    linea.Y2 = posicionYDestino;

                    return linea;
                }
            }

            return null;
        }

        private List<ArrowLine> BuscarLineasUnElemento(Point ubicacion, bool elementoOrigen, UIElement elemento)
        {
            List<ArrowLine> lineasEncontradas = new List<ArrowLine>();
            if (elementoOrigen)
            {
                var lineas = (from UIElement L in diagrama.Children
                              where (L.GetType() == typeof(ArrowLine)) &&
(((ArrowLine)L).X1 >= ubicacion.X - 10 & ((ArrowLine)L).X1 <= ubicacion.X + elemento.RenderSize.Width + 10) &
(((ArrowLine)L).Y1 >= ubicacion.Y - 10 & ((ArrowLine)L).Y1 <= ubicacion.Y + elemento.RenderSize.Height + 10)
                              select L).ToList();

                foreach (var item in lineas)
                    lineasEncontradas.Add((ArrowLine)item);
            }
            else
            {
                var lineas = (from UIElement L in diagrama.Children
                              where (L.GetType() == typeof(ArrowLine)) &&
(((ArrowLine)L).X2 >= ubicacion.X - 10 & ((ArrowLine)L).X2 <= ubicacion.X + elemento.RenderSize.Width + 10) &
(((ArrowLine)L).Y2 >= ubicacion.Y - 10 & ((ArrowLine)L).Y2 <= ubicacion.Y + elemento.RenderSize.Height + 10)
                              select L).ToList();

                foreach (var item in lineas)
                    lineasEncontradas.Add((ArrowLine)item);
            }
            return lineasEncontradas;
        }

        private void CambioTamañoCalculo(object sender, SizeChangedEventArgs e)
        {
            CalculoEspecificoDiseño calculo = (CalculoEspecificoDiseño)sender;
            calculo.DiseñoCalculo.AnchuraElemento = calculo.ActualWidth;
            calculo.DiseñoCalculo.AlturaElemento = calculo.ActualHeight;
        }

        private void EstablecerIndicesProfundidadElementos()
        {
            int indice = 0;

            var elementos = (from UIElement E in diagrama.Children where E.GetType() != typeof(ArrowLine) select E).ToList();
            foreach (var item in elementos)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }

            var lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var item in lineas)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }
        }

        private void btnQuitarCalculo_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                if (CalculoSeleccionado != null)
                {
                    MessageBoxResult resp = MessageBox.Show("¿Quitar este cálculo de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resp == MessageBoxResult.Yes)
                    {
                        //TabItem tabItem = (from TabItem T in Ventana.contenido.Items where T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                        //                   ((VistaDiseñoOperaciones)T.Content).CalculoDiseñoSeleccionado.Nombre == CalculoSeleccionado.DiseñoCalculo.Nombre select T).FirstOrDefault();
                        //if (tabItem != null)
                        //{
                        //    ((VistaDiseñoOperaciones)((TabItem)tabItem).Content).CalculoDiseñoSeleccionado = Calculo.Calculos.First();
                        //    ((VistaDiseñoOperaciones)((TabItem)tabItem).Content).SeleccionarCalculo(null, null);
                        //}

                        foreach (var itemOperacion in CalculoSeleccionado.DiseñoCalculo.ElementosOperaciones)
                        {
                            Resultado resultadoRelacionado = (from Resultado R in Calculo.ListaResultados where R.SalidaRelacionada == itemOperacion select R).FirstOrDefault();
                            if (resultadoRelacionado != null)
                            {
                                Calculo.ListaResultados.Remove(resultadoRelacionado);
                                foreach (var item in Ventana.contenido.Items)
                                {
                                    if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                                    {
                                        ((VistaResultados)((TabItem)item).Content).ListarResultados();
                                        break;
                                    }
                                }
                            }
                        }

                        ActualizarElementosSalida_CalculoQuitadoDesconectado(CalculoSeleccionado.DiseñoCalculo, null);
                        
                        foreach (var item in calc.Calculos)
                            QuitarDeElementosPosterioresAnteriores(item, CalculoSeleccionado.DiseñoCalculo);

                        DiseñoCalculo calculoEntradas = Calculo.ObtenerCalculoEntradas();
                        if (calculoEntradas != null)
                        {
                            calculoEntradas.ElementosPosteriores.Remove(CalculoSeleccionado.DiseñoCalculo);
                            CalculoSeleccionado.DiseñoCalculo.ElementosAnteriores.Remove(calculoEntradas);
                            calculoEntradas.ElementosContenedoresOperacion.Remove(CalculoSeleccionado.DiseñoCalculo);
                        }

                        calc.Calculos.Remove(CalculoSeleccionado.DiseñoCalculo);
                        ActualizarContenedoresElementos(CalculoSeleccionado.DiseñoCalculo);

                        diagrama.Children.Remove(CalculoSeleccionado);
                        //Calculo.ListaCalculos_Visuales.Remove(CalculoSeleccionado);
                        //Calculo.ConCambiosVisuales = true;

                        Ventana.ActualizarVistaEntradas(CalculoSeleccionado.DiseñoCalculo);
                        Ventana.CerrarPestañasCalculoEliminado(CalculoSeleccionado.DiseñoCalculo);
                        CalculoSeleccionado = null;

                        EstablecerUltimosResultados((from C in calc.Calculos where C.ElementosPosteriores.Count == 0 select C).ToList());

                        if (calc.Calculos.Where(i => !i.EsEntradasArchivo).ToList().Count == 0)
                            btnAgregarCalculo_Click(this, e);

                        DibujarTodasLineasElementos();
                        EstablecerIndicesProfundidadElementos();

                        SeleccionarPrimerCalculo();
                    }
                }
                else if (NotaSeleccionada != null)
                {
                    MessageBoxResult resp = MessageBox.Show("¿Quitar esta nota de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resp == MessageBoxResult.Yes)
                    {
                       
                        calc.Notas.Remove(NotaSeleccionada.DiseñoOperacion);
                        
                        diagrama.Children.Remove(NotaSeleccionada);
                        NotaSeleccionada = null;
                    }
                }
                else
                {
                    if (lineaSeleccionada != null)
                    {
                        if (ElementoAnteriorLineaSeleccionada != null & ElementoPosteriorLineaSeleccionada != null)
                        {
                            ActualizarElementosSalida_CalculoQuitadoDesconectado(ElementoAnteriorLineaSeleccionada, ElementoPosteriorLineaSeleccionada);

                            ElementoAnteriorLineaSeleccionada.ElementosPosteriores.Remove(ElementoPosteriorLineaSeleccionada);
                            ElementoPosteriorLineaSeleccionada.ElementosAnteriores.Remove(ElementoAnteriorLineaSeleccionada);
                            ElementoAnteriorLineaSeleccionada.ElementosContenedoresOperacion.Remove(ElementoPosteriorLineaSeleccionada);
                        }

                        DibujarTodasLineasElementos();
                        EstablecerIndicesProfundidadElementos();

                        EstablecerUltimosResultados((from C in calc.Calculos where C.ElementosPosteriores.Count == 0 select C).ToList());
                    }
                }
            }
            else
            {
                foreach(var itemElemento in ElementosSeleccionados)
                {
                    //if (CalculoSeleccionado != null)
                    //{
                    //    MessageBoxResult resp = MessageBox.Show("¿Quitar este cálculo de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    //    if (resp == MessageBoxResult.Yes)
                    //    {
                    //TabItem tabItem = (from TabItem T in Ventana.contenido.Items where T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                    //                   ((VistaDiseñoOperaciones)T.Content).CalculoDiseñoSeleccionado.Nombre == CalculoSeleccionado.DiseñoCalculo.Nombre select T).FirstOrDefault();
                    //if (tabItem != null)
                    //{
                    //    ((VistaDiseñoOperaciones)((TabItem)tabItem).Content).CalculoDiseñoSeleccionado = Calculo.Calculos.First();
                    //    ((VistaDiseñoOperaciones)((TabItem)tabItem).Content).SeleccionarCalculo(null, null);
                    //}
                    if (itemElemento.GetType() == typeof(DiseñoCalculo))
                    {
                        foreach (var itemOperacion in ((DiseñoCalculo)itemElemento).ElementosOperaciones)
                        {
                            Resultado resultadoRelacionado = (from Resultado R in Calculo.ListaResultados where R.SalidaRelacionada == itemOperacion select R).FirstOrDefault();
                            if (resultadoRelacionado != null)
                            {
                                Calculo.ListaResultados.Remove(resultadoRelacionado);
                                foreach (var item in Ventana.contenido.Items)
                                {
                                    if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                                    {
                                        ((VistaResultados)((TabItem)item).Content).ListarResultados();
                                        break;
                                    }
                                }
                            }
                        }

                        ActualizarElementosSalida_CalculoQuitadoDesconectado(((DiseñoCalculo)itemElemento), null);
                        
                        foreach (var item in calc.Calculos)
                            QuitarDeElementosPosterioresAnteriores(item, ((DiseñoCalculo)itemElemento));

                        calc.Calculos.Remove(((DiseñoCalculo)itemElemento));
                        ActualizarContenedoresElementos(((DiseñoCalculo)itemElemento));

                        CalculoEspecificoDiseño calculoSeleccionado = (CalculoEspecificoDiseño)(from UIElement E in diagrama.Children
                                                                                                where E.GetType() == typeof(CalculoEspecificoDiseño) &&
                                                          ((CalculoEspecificoDiseño)E).DiseñoCalculo == itemElemento
                                                                                                select E).FirstOrDefault();

                        diagrama.Children.Remove(calculoSeleccionado);
                        //CalculoSeleccionado = null;

                        //DibujarTodasLineasElementos();
                        //    }
                        //}
                        Ventana.ActualizarVistaEntradas(calculoSeleccionado.DiseñoCalculo);
                        Ventana.CerrarPestañasCalculoEliminado(calculoSeleccionado.DiseñoCalculo);
                    }
                    else if (itemElemento.GetType() == typeof(DiseñoOperacion))
                    {
                        
                        calc.Notas.Remove((DiseñoOperacion)itemElemento);
                        
                        NotaDiagrama notaSeleccionada = (NotaDiagrama)(from UIElement E in diagrama.Children
                                                                                                where E.GetType() == typeof(NotaDiagrama) &&
                                                          ((NotaDiagrama)E).DiseñoOperacion == itemElemento
                                                                                                select E).FirstOrDefault();

                        diagrama.Children.Remove(notaSeleccionada);
                        //CalculoSeleccionado = null;

                        //DibujarTodasLineasElementos();
                        //    }
                        //}
                    }
                }

                DibujarElementosCalculos();

                EstablecerUltimosResultados((from C in calc.Calculos where C.ElementosPosteriores.Count == 0 select C).ToList());

                if (calc.Calculos.Count(i => !i.EsEntradasArchivo) == 0)
                    btnAgregarCalculo_Click(this, e);

                SeleccionarPrimerCalculo();
            }
        }

        private void ActualizarContenedoresElementos(DiseñoCalculo elemento)
        {
            foreach (var itemElemento in calc.Calculos)
            {
                if (itemElemento.ElementosContenedoresOperacion.Contains(elemento))
                    itemElemento.ElementosContenedoresOperacion.Remove(elemento);
            }
        }

        private void QuitarDeElementosPosterioresAnteriores(DiseñoCalculo elemento, DiseñoCalculo elementoAQuitar)
        {
            var item = (from DiseñoCalculo E in elemento.ElementosAnteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
                elemento.ElementosAnteriores.Remove(item);

            item = (from DiseñoCalculo E in elemento.ElementosPosteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
                elemento.ElementosPosteriores.Remove(item);
        }

        public void DestacarElementoSeleccionado()
        {
            ElementosSeleccionados.Clear();
            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(CalculoEspecificoDiseño))
                {
                    ((CalculoEspecificoDiseño)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((CalculoEspecificoDiseño)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(NotaDiagrama))
                {
                    ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                }
                else if (item.GetType() == typeof(ArrowLine))
                    ((ArrowLine)item).Stroke = Brushes.Black;
            }

            if (CalculoSeleccionado != null)
            {
                if (CalculoSeleccionado.EnDiagrama)
                {
                    CalculoSeleccionado.Background = SystemColors.HighlightBrush;
                    CalculoSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
                }
                ElementoSeleccionado = CalculoSeleccionado.DiseñoCalculo;
            }
            else if (NotaSeleccionada != null)
            {
                if (NotaSeleccionada.EnDiagrama)
                {
                    NotaSeleccionada.fondo.BorderThickness = new Thickness(1);
                }
                ElementoSeleccionado = null;
            }
            else if (lineaSeleccionada != null)
                lineaSeleccionada.Stroke = SystemColors.HighlightBrush;
        }

        public void diagrama_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OcultarToolTips(null);

            if (lineaSeleccionada != null) return;

            CalculoSeleccionado = null;
            lineaSeleccionada = null;
            Calculo.SubCalculoSeleccionado = null;
            ElementoSeleccionado = null;
            NotaSeleccionada = null;
            ElementosDiagramaSeleccionados.Clear();
            ElementosSeleccionados.Clear();

            Elemento_e = null;
            ElementoSeleccionado_Bool = false;
            ClicDiagrama = true;

            MostrarInfo_Elemento(null);
            DestacarElementoSeleccionado();

            ubicacionInicialAreaSeleccionada = e.GetPosition(diagrama);
        }

        private void SeleccionarLinea(object sender, RoutedEventArgs e)
        {
            lineaSeleccionada = (ArrowLine)sender;
            ElementoSeleccionado_Bool = true;

            ElementoAnteriorLineaSeleccionada = null;
            ElementoPosteriorLineaSeleccionada = null;

            EncontrarElementosLinea(lineaSeleccionada, ref ElementoAnteriorLineaSeleccionada, ref ElementoPosteriorLineaSeleccionada);

            DestacarElementoSeleccionado();
            ElementoSeleccionado_Bool = true;
        }

        private void EncontrarElementosLinea(ArrowLine lineaSeleccionada, ref DiseñoCalculo ElementoAnteriorLineaSeleccionada,
            ref DiseñoCalculo ElementoPosteriorLineaSeleccionada)
        {
            foreach (var itemControl in (from UIElement C in diagrama.Children where C.GetType() != typeof(ArrowLine) select C).ToList())
            {
                var lineasOrigen = BuscarLineasUnElemento(new Point(Canvas.GetLeft(itemControl), Canvas.GetTop(itemControl)),
                true, itemControl);

                var lineaOrigenEncontrada = (from ArrowLine L in lineasOrigen where L == lineaSeleccionada select L).FirstOrDefault();

                if (lineaOrigenEncontrada != null)
                {
                    if (itemControl.GetType() == typeof(CalculoEspecificoDiseño))
                        ElementoAnteriorLineaSeleccionada = ((CalculoEspecificoDiseño)itemControl).DiseñoCalculo;

                    break;
                }
            }

            foreach (var itemControl in (from UIElement C in diagrama.Children where C.GetType() != typeof(ArrowLine) select C).ToList())
            {
                var lineasDestino = BuscarLineasUnElemento(new Point(Canvas.GetLeft(itemControl), Canvas.GetTop(itemControl)),
                    false, itemControl);

                var lineaDestinoEncontrada = (from ArrowLine L in lineasDestino where L == lineaSeleccionada select L).FirstOrDefault();

                if (lineaDestinoEncontrada != null)
                {
                    if (itemControl.GetType() == typeof(CalculoEspecificoDiseño))
                        ElementoPosteriorLineaSeleccionada = ((CalculoEspecificoDiseño)itemControl).DiseñoCalculo;

                    break;
                }
            }
        }

        private void ActualizarElementosSalida_CalculoQuitadoDesconectado(DiseñoCalculo calculo, DiseñoCalculo calculoConectado)
        {
            foreach (var itemCalculoPosterior in calculo.ElementosPosteriores)
            {                
                ActualizarElementosSalida_CalculoQuitadoDesconectado(itemCalculoPosterior, calculoConectado);

                List<DiseñoOperacion> itemsAEliminar = new List<DiseñoOperacion>();

                foreach (var elementoSalida in itemCalculoPosterior.ElementosOperaciones)
                {
                    if (elementoSalida.EntradaRelacionada != null &&
                        elementoSalida.EntradaRelacionada.Tipo == TipoEntrada.Calculo &&
                        calculo.ElementosOperaciones.Contains(elementoSalida.EntradaRelacionada.ElementoSalidaCalculoAnterior) &&
                        (calculoConectado == null || calculoConectado == itemCalculoPosterior))
                    {
                        itemsAEliminar.Add(elementoSalida);
                    }
                }

                while (itemsAEliminar.Count > 0)
                {
                    VistaDiseñoOperaciones vistaTemp = new VistaDiseñoOperaciones();
                    vistaTemp.Ventana = Ventana;
                    vistaTemp.Calculo = Calculo;
                    vistaTemp.CalculoDiseñoSeleccionado = itemCalculoPosterior;

                    vistaTemp.QuitarElementoDiagrama(itemsAEliminar.First());
                    //itemCalculoPosterior.ElementosOperaciones.Remove(itemsAEliminar.First());
                    itemsAEliminar.Remove(itemsAEliminar.First());
                }
            }
        }
        private void QuitarDeElementosPosterioresAnteriores(DiseñoOperacion elemento, DiseñoOperacion elementoAQuitar)
        {
            var item = (from DiseñoOperacion E in elemento.ElementosAnteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
                elemento.ElementosAnteriores.Remove(item);

            item = (from DiseñoOperacion E in elemento.ElementosPosteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
                elemento.ElementosPosteriores.Remove(item);
        }

        private void ActualizarElementosResultados_CalculoConectado(DiseñoCalculo calculo)
        {
            foreach (var itemElemento in calculo.ElementosOperaciones)
            {
                Resultado resultadoRelacionado = (from Resultado R in calc.ListaResultados where R.SalidaRelacionada == itemElemento select R).FirstOrDefault();
                if (resultadoRelacionado != null)
                {
                    Calculo.ListaResultados.Remove(resultadoRelacionado);
                    foreach (var item in Ventana.contenido.Items)
                    {
                        if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                        {
                            ((VistaResultados)((TabItem)item).Content).ListarResultados();
                            break;
                        }
                    }
                }
            }
        }

        private void btnOperacionesCalculo_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                if (CalculoSeleccionado != null)
                {
                    Ventana.btnOperaciones_Click(null, null);
                    ((VistaDiseñoOperaciones)((TabItem)Ventana.contenido.SelectedItem).Content).CalculoDiseñoSeleccionado = CalculoSeleccionado.DiseñoCalculo;
                    ((VistaDiseñoOperaciones)((TabItem)Ventana.contenido.SelectedItem).Content).SeleccionarCalculo(null, null);
                }
            }
        }

        private void diagrama_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed & !ElementoSeleccionado_Bool & ClicDiagrama)
            {
                ubicacionFinalAreaSeleccionada = e.GetPosition(diagrama);

                //Ventana.Dispatcher.Invoke(() =>
                //{
                //Point ubicacionInicial;
                //Point ubicacionFinal;

                double xInicial;
                double xFinal;
                double yInicial;
                double yFinal;

                if (ubicacionFinalAreaSeleccionada.X >= ubicacionInicialAreaSeleccionada.X)
                {
                    xFinal = ubicacionFinalAreaSeleccionada.X;
                    xInicial = ubicacionInicialAreaSeleccionada.X;
                }
                else
                {
                    xFinal = ubicacionInicialAreaSeleccionada.X;
                    xInicial = ubicacionFinalAreaSeleccionada.X;
                }

                if (ubicacionFinalAreaSeleccionada.Y >= ubicacionInicialAreaSeleccionada.Y)
                {
                    yFinal = ubicacionFinalAreaSeleccionada.Y;
                    yInicial = ubicacionInicialAreaSeleccionada.Y;
                }
                else
                {
                    yFinal = ubicacionInicialAreaSeleccionada.Y;
                    yInicial = ubicacionFinalAreaSeleccionada.Y;
                }

                //ubicacionInicialAreaSeleccionada = ubicacionInicial;
                //ubicacionFinalAreaSeleccionada = ubicacionFinal;

                if (rectanguloSeleccion != null)
                {
                    diagrama.Children.Remove(rectanguloSeleccion);
                }

                rectanguloSeleccion = new Rectangle();
                rectanguloSeleccion.Stroke = Brushes.Black;
                rectanguloSeleccion.Fill = SystemColors.HighlightBrush;
                rectanguloSeleccion.Opacity = 0.2;

                rectanguloSeleccion.Width = xFinal - xInicial;
                rectanguloSeleccion.Height = yFinal - yInicial;

                diagrama.Children.Add(rectanguloSeleccion);
                Canvas.SetTop(rectanguloSeleccion, yInicial);
                Canvas.SetLeft(rectanguloSeleccion, xInicial);

                //List<UIElement> elementos = (from UIElement E in diagrama.Children select E).ToList();
                try
                {
                    foreach (UIElement item in diagrama.Children)
                    {
                        if (item.GetType() == typeof(ArrowLine)) continue;
                        if (Canvas.GetTop((UIElement)item) >= Canvas.GetTop(rectanguloSeleccion) &
                            Canvas.GetTop((UIElement)item) <= Canvas.GetTop(rectanguloSeleccion) + rectanguloSeleccion.Height &
                            Canvas.GetLeft((UIElement)item) >= Canvas.GetLeft(rectanguloSeleccion) &
                            Canvas.GetLeft((UIElement)item) <= Canvas.GetLeft(rectanguloSeleccion) + rectanguloSeleccion.Width)
                        {
                            if (!ElementosDiagramaSeleccionados.Contains(item))
                            {
                                if (item.GetType() == typeof(CalculoEspecificoDiseño))
                                {
                                    //((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightTextBrush;
                                    //((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightTextBrush;
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                              !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((CalculoEspecificoDiseño)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((CalculoEspecificoDiseño)item).DiseñoCalculo);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(NotaDiagrama))
                                {
                                    //((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightTextBrush;
                                    //((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightTextBrush;
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                             !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((NotaDiagrama)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((NotaDiagrama)item).DiseñoOperacion);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.GetType() == typeof(CalculoEspecificoDiseño))
                            {
                                ((CalculoEspecificoDiseño)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((CalculoEspecificoDiseño)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                ElementosSeleccionados.Remove(((CalculoEspecificoDiseño)item).DiseñoCalculo);
                            }
                            else if (item.GetType() == typeof(NotaDiagrama))
                            {
                                ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                                ElementosSeleccionados.Remove(((NotaDiagrama)item).DiseñoOperacion);
                            }

                            ElementosDiagramaSeleccionados.Remove(item);
                        }
                    }
                }
                catch (Exception) { }
            }
            else if (e.LeftButton == MouseButtonState.Released & !ElementoSeleccionado_Bool & ClicDiagrama)
            {
                ClicDiagrama = false;
                if (rectanguloSeleccion != null)
                    diagrama.Children.Remove(rectanguloSeleccion);

                rectanguloSeleccion = null;
            }
        }

        private void btnInformacionCalculo_Click(object sender, RoutedEventArgs e)
        {
            if (NotaSeleccionada != null) return;
            MostrandoInformacionElemento = !MostrandoInformacionElemento;

            if (MostrandoInformacionElemento)
            {
                if (ElementoSeleccionado == null)
                {
                    MostrandoInformacionElemento = false;
                }
            }

            if (MostrandoInformacionElemento)
            {
                infoElemento.Text = ElementoSeleccionado.Info;
                contenedorInformacion.Visibility = Visibility.Visible;
            }
            else
            {
                contenedorInformacion.Visibility = Visibility.Collapsed;
            }
        }

        private void infoElemento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.Info = infoElemento.Text;
            }
            else if (NotaSeleccionada != null)
            {
                NotaSeleccionada.DiseñoOperacion.Info = infoElemento.Text;
            }
        }

        public void MostrarInfo_Elemento(DiseñoCalculo elemento)
        {
            contenedorInformacion.Visibility = Visibility.Collapsed;

            if (elemento != null)
            {
                if (MostrandoInformacionElemento)
                {
                    contenedorInformacion.Visibility = Visibility.Visible;
                    infoElemento.Text = elemento.Info;
                }
            }
        }

        public void AplicarZoom(int zoom)
        {
            escalaZoom = (double)zoom / 100.0;
            diagrama.LayoutTransform = new ScaleTransform(escalaZoom, escalaZoom);
            diagrama.UpdateLayout();
        }

        private void zoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cantidadZoom = 0;
            if (!int.TryParse(zoom.Text, out cantidadZoom))
            {
                cantidadZoom = 100;
                zoom.Text = cantidadZoom.ToString();
            }
            else
            {
                AplicarZoom(cantidadZoom);
            }
        }

        private void aumentarZoom_Click(object sender, RoutedEventArgs e)
        {
            int cantidadZoom = 0;
            if (int.TryParse(zoom.Text, out cantidadZoom))
            {
                cantidadZoom += 1;
                zoom.Text = cantidadZoom.ToString();
            }
        }

        private void disminuirZoom_Click(object sender, RoutedEventArgs e)
        {
            int cantidadZoom = 0;
            if (int.TryParse(zoom.Text, out cantidadZoom))
            {
                cantidadZoom -= 1;
                zoom.Text = cantidadZoom.ToString();
            }
        }

        private void zoomNormal_Click(object sender, RoutedEventArgs e)
        {
            zoom.Text = "100";
        }

        private void aumentarArea_Click(object sender, RoutedEventArgs e)
        {
            diagrama.Height = diagrama.ActualHeight + 300;
            diagrama.Width = diagrama.ActualWidth + 300;
            Calculo.Alto = diagrama.Height;
            Calculo.Ancho = diagrama.Width;
        }

        private void disminuirArea_Click(object sender, RoutedEventArgs e)
        {
            if (diagrama.ActualHeight - 300 > 0 &
                diagrama.ActualWidth - 300 > 0)
            {
                diagrama.Height = diagrama.ActualHeight - 300;
                diagrama.Width = diagrama.ActualWidth - 300;
                Calculo.Alto = diagrama.Height;
                Calculo.Ancho = diagrama.Width;
            }
        }

        private void buscarDiagrama_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textoBusquedaDiagrama.Text))
            {
                List<UIElement> elementos = BuscarElementosDiagramas(textoBusquedaDiagrama.Text);

                ElementosEncontrados.Clear();
                indiceElementosEncontrados = -1;
                resultadosBusquedas.Visibility = Visibility.Collapsed;

                if (elementos != null && elementos.Any())
                {
                    ElementosEncontrados.AddRange(elementos);
                    siguienteResultado_Click(this, e);
                    if (ElementosEncontrados.Count > 1)
                    {
                        resultadosBusquedas.Visibility = Visibility.Visible;
                    }
                    else
                        resultadosBusquedas.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void siguienteResultado_Click(object sender, RoutedEventArgs e)
        {
            if (indiceElementosEncontrados < ElementosEncontrados.Count - 1)
            {
                indiceElementosEncontrados++;
                MostrarElementoEncontrado();
            }
        }

        private void anteriorResultado_Click(object sender, RoutedEventArgs e)
        {
            if (indiceElementosEncontrados > 0)
            {
                indiceElementosEncontrados--;
                MostrarElementoEncontrado();
            }
        }

        private void MostrarElementoEncontrado()
        {
            if (ElementosEncontrados.Any())
            {
                UIElement elemento = ElementosEncontrados[indiceElementosEncontrados];
                //         UIElement elementoDiagrama = (from UIElement E in diagrama.Children
                //                                       where
                //(E.GetType() == typeof(EntradaDiseñoOperaciones) && ((EntradaDiseñoOperaciones)E).DiseñoOperacion == elemento) |
                //(E.GetType() == typeof(OperacionEspecifica) && ((OperacionEspecifica)E).DiseñoOperacion == elemento)
                //                                       select E).FirstOrDefault();

                //if(elementoDiagrama.GetType() == typeof(EntradaDiseñoOperaciones))
                //    Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)elementoDiagrama).Clic);

                //if (elementoDiagrama.GetType() == typeof(OperacionEspecifica))
                //    Ventana.Dispatcher.Invoke(((OperacionEspecifica)elementoDiagrama).Clic);

                e_SeleccionarElemento = new MouseButtonEventArgs(Mouse.PrimaryDevice, DateTime.Now.Hour, MouseButton.Left);

                if (elemento.GetType() == typeof(CalculoEspecificoDiseño))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((CalculoEspecificoDiseño)elemento).Clic);
                    }
                }

                if (elemento.GetType() == typeof(NotaDiagrama))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((NotaDiagrama)elemento).Clic);
                    }
                }

                e_SeleccionarElemento = null;

                contenedor.ScrollToHorizontalOffset(Canvas.GetLeft(elemento) * escalaZoom);
                contenedor.ScrollToVerticalOffset(Canvas.GetTop(elemento) * escalaZoom);
            }
        }

        private List<UIElement> BuscarElementosDiagramas(string textoBusqueda)
        {
            List<UIElement> elementos = new List<UIElement>();
            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(CalculoEspecificoDiseño))
                {
                    CalculoEspecificoDiseño calculo = (CalculoEspecificoDiseño)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (calculo.nombreCalculo.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(calculo);
                    }
                }
                else if (item.GetType() == typeof(NotaDiagrama))
                {
                    NotaDiagrama nota = (NotaDiagrama)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (nota.textoNota.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(nota);
                    }
                }
            }

            return elementos;
        }

        private void limpiarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            indiceElementosEncontrados = -1;
            ElementosEncontrados.Clear();
            textoBusquedaDiagrama.Text = string.Empty;
            resultadosBusquedas.Visibility = Visibility.Collapsed;
        }

        private void btnAgregarNota_Click(object sender, RoutedEventArgs e)
        {
            NotaDiagrama nuevaNota = new NotaDiagrama();
            nuevaNota.VistaCalculos = this;
            nuevaNota.EnDiagrama = true;
            nuevaNota.fondo.BorderThickness = new Thickness(0);

            DiseñoOperacion nuevoElementoCalculo = new DiseñoOperacion();
            nuevoElementoCalculo.ID = App.GenerarID_Elemento();
            nuevoElementoCalculo.PosicionX = App.PosicionXPredeterminada;
            nuevoElementoCalculo.PosicionY = App.PosicionYPredeterminada;

            var ultimoNombre = (from DiseñoOperacion E in calc.Notas where E.Info.LastIndexOf(" ") > -1 select E.Info).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            cantidadElementosTipo++;

            nuevoElementoCalculo.Info = "Nota " + cantidadElementosTipo.ToString();

            calc.Notas.Add(nuevoElementoCalculo);
            nuevaNota.DiseñoOperacion = nuevoElementoCalculo;

            diagrama.Children.Add(nuevaNota);

            Canvas.SetTop(nuevaNota, App.PosicionYPredeterminada);
            Canvas.SetLeft(nuevaNota, App.PosicionXPredeterminada);

            //if(double.IsNaN(nuevoCalculo.DiseñoCalculo.AnchuraElemento) &
            //    double.IsNaN(nuevoCalculo.DiseñoCalculo.AlturaElemento))
            //{
            //    nuevoElementoCalculo.AnchuraElemento = nuevoCalculo.ActualWidth;
            //    nuevoElementoCalculo.AlturaElemento = nuevoCalculo.ActualHeight;
            //}


            EstablecerIndicesProfundidadElementos();
        }

        private void EstablecerUltimosResultados(List<DiseñoCalculo> ultimosCalculos)
        {
            Calculo.ListaResultados.Clear();

            foreach (var itemCalculo in ultimosCalculos)
            {
                foreach (var itemOperacion in itemCalculo.ElementosOperaciones)
                {
                    if (itemOperacion.ContieneSalida && 
                        !itemOperacion.ElementosPosteriores.Any((i) => i.Tipo != TipoElementoOperacion.Salida))
                    {
                        Resultado resultadoRelacionado = new Resultado();
                        resultadoRelacionado.ID = App.GenerarID_Elemento();
                        resultadoRelacionado.SalidaRelacionada = itemOperacion;
                        Calculo.ListaResultados.Add(resultadoRelacionado);
                    }
                                        
                }
            }

            foreach (var item in Ventana.contenido.Items)
            {
                if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                {
                    ((VistaResultados)((TabItem)item).Content).ListarResultados();
                    break;
                }
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void ajustarTamañoPizarra_Click(object sender, RoutedEventArgs e)
        {
            UIElement[] controles = new UIElement[diagrama.Children.Count];
            diagrama.Children.CopyTo(controles, 0);

            double altura = controles.ToList().Select(e => Canvas.GetTop(e) + e.RenderSize.Height).Max();
            double anchura = controles.ToList().Select(e => Canvas.GetLeft(e) + e.RenderSize.Width).Max();

            diagrama.Height = altura + 50;
            diagrama.Width = anchura + 50;
            Calculo.Alto = diagrama.Height;
            Calculo.Ancho = diagrama.Width;            
        }

        public void OcultarToolTips(UIElement elementoActual)
        {
            Ventana.popup.IsOpen = false;
        }
        private void diagrama_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Ventana.popup != null && !Ventana.popup.Child.IsMouseOver)
                OcultarToolTips(null);
        }
    }
}
