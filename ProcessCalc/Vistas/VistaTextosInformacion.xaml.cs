using Petzold.Media2D;
using PlanMath_para_Excel;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Notas;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Definiciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaTextosInformacion.xaml
    /// </summary>
    public partial class VistaTextosInformacion : UserControl
    {
        public Calculo CalculoSeleccionado { get; set; }
        public DiseñoCalculoTextosInformacion CalculoDiseñoSeleccionado
        {
            get
            {
                if (CalculoSeleccionado != null)
                    return CalculoSeleccionado.TextosInformacion;
                else
                    return null;
            }
        }
        private DiseñoTextosInformacion ElementoAnteriorLineaSeleccionada;
        private DiseñoTextosInformacion ElementoPosteriorLineaSeleccionada;
        public bool ElementoSeleccionado;
        public bool SeleccionandoElemento { get; set; }
        public bool ClicDiagrama;
        Point ubicacionInicialAreaSeleccionada;
        Point ubicacionFinalAreaSeleccionada;
        Rectangle rectanguloSeleccion;
        //List<object> ElementosSeleccionados = new List<object>();
        public MainWindow Ventana { get; set; }
        double escalaZoom = 1;
        List<UIElement> ElementosEncontrados = new List<UIElement>();
        int indiceElementosEncontrados = -1;
        public Point ubicacionActualElemento { get; set; }
        public DiseñoCalculo CalculoDiseñoSeleccionado_Cantidades { get; set; }
        public bool CalculoClikeado { get; set; }
        public VistaTextosInformacion()
        {
            InitializeComponent();
        }

        public void ListarEntradas(string textoBusqueda = null)
        {
            entradas.Children.Clear();

            if (CalculoSeleccionado != null &&
                CalculoSeleccionado.Calculos.FirstOrDefault(i => i.EsEntradasArchivo) != null)
            {
                foreach (var itemEntrada in CalculoSeleccionado.Calculos.FirstOrDefault(i => i.EsEntradasArchivo).ListaEntradas.Where(i => i.Tipo == TipoEntrada.TextosInformacion))
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                    nuevaEntrada.Entrada = itemEntrada;
                    nuevaEntrada.VistaTextosInformacion = this;

                    if (textoBusqueda == null)
                        entradas.Children.Add(nuevaEntrada);
                    else
                    {
                        if (nuevaEntrada.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                            nuevaEntrada.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            entradas.Children.Add(nuevaEntrada);
                    }
                }
            }
        }

        public void DibujarElementosTextosInformacion(DiseñoCalculo calculoSeleccionado)
        {
            diagrama.Children.Clear();
            if (CalculoDiseñoSeleccionado == null) return;
            
            foreach (var itemElemento in CalculoDiseñoSeleccionado.ElementosTextosInformacion.Where(i => i.CalculoRelacionado == calculoSeleccionado))
            {
                if (itemElemento.Tipo == TipoElementoOperacion.Definicion_TextosInformacion)
                {
                    if (itemElemento.EntradaRelacionada == null)
                    {
                        Definicion_TextosInformacion nuevaDefinicion = new Definicion_TextosInformacion();
                        nuevaDefinicion.SizeChanged += CambioTamañoDefinicion;
                        nuevaDefinicion.VistaTextos = this;
                        nuevaDefinicion.EnDiagrama = true;
                        nuevaDefinicion.botonFondo.BorderBrush = Brushes.Black;
                        nuevaDefinicion.DiseñoTextosInformacion = itemElemento;

                        if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado != null &&
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado == itemElemento)
                        {
                            nuevaDefinicion.Background = SystemColors.HighlightBrush;
                            nuevaDefinicion.botonFondo.Background = SystemColors.HighlightBrush;
                        }
                        else
                        {
                            nuevaDefinicion.Background = SystemColors.GradientInactiveCaptionBrush;
                            nuevaDefinicion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                        }

                        diagrama.Children.Add(nuevaDefinicion);

                        Canvas.SetTop(nuevaDefinicion, itemElemento.PosicionY);
                        Canvas.SetLeft(nuevaDefinicion, itemElemento.PosicionX);
                    }                    
                }
                else if(itemElemento.Tipo == TipoElementoOperacion.Entrada)
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                    nuevaEntrada.VistaTextosInformacion = this;
                    nuevaEntrada.EnDiagrama = true;
                    nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
                    nuevaEntrada.Entrada = itemElemento.EntradaRelacionada;
                    nuevaEntrada.DiseñoTextosInformacion = itemElemento;

                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado != null &&
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado == itemElemento)
                    {
                        nuevaEntrada.Background = SystemColors.HighlightBrush;
                        nuevaEntrada.botonFondo.Background = SystemColors.HighlightBrush;
                    }
                    else
                    {
                        nuevaEntrada.Background = SystemColors.GradientInactiveCaptionBrush;
                        nuevaEntrada.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                    }

                    diagrama.Children.Add(nuevaEntrada);

                    Canvas.SetTop(nuevaEntrada, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevaEntrada, itemElemento.PosicionX);
                }
                else if (itemElemento.Tipo == TipoElementoOperacion.Definicion_ListaCadenasTexto)
                {
                    if (itemElemento.EntradaRelacionada == null)
                    {
                        DefinicionListaCadenasTexto nuevaDefinicion = new DefinicionListaCadenasTexto();
                        nuevaDefinicion.SizeChanged += CambioTamañoDefinicion_ListaCadenas;
                        nuevaDefinicion.VistaTextos = this;
                        nuevaDefinicion.EnDiagrama = true;
                        nuevaDefinicion.botonFondo.BorderBrush = Brushes.Black;
                        nuevaDefinicion.DiseñoListaCadenasTexto = (DiseñoListaCadenasTexto)itemElemento;

                        if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado != null &&
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado == itemElemento)
                        {
                            nuevaDefinicion.Background = SystemColors.HighlightBrush;
                            nuevaDefinicion.botonFondo.Background = SystemColors.HighlightBrush;
                        }
                        else
                        {
                            nuevaDefinicion.Background = SystemColors.GradientInactiveCaptionBrush;
                            nuevaDefinicion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                        }

                        diagrama.Children.Add(nuevaDefinicion);

                        Canvas.SetTop(nuevaDefinicion, itemElemento.PosicionY);
                        Canvas.SetLeft(nuevaDefinicion, itemElemento.PosicionX);
                    }
                }

                //DibujarLineasElemento(itemElemento);
            }
            
            DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);
            EstablecerIndicesProfundidadElementos();
        }

        public void DibujarLineasElemento(DiseñoTextosInformacion item)
        {
            foreach (var itemElemento in item.ElementosPosteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagrama.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                    if (lineaEncontrada == null)
                    {
                        diagrama.Children.Add(nuevaLinea);

                        if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada = nuevaLinea;
                    }
                    else
                    {
                        if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada = (ArrowLine)lineaEncontrada;
                    }
                }

            }

            foreach (var itemElemento in item.ElementosAnteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagrama.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                    if (lineaEncontrada == null)
                    {
                        diagrama.Children.Add(nuevaLinea);

                        if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada = nuevaLinea;
                    }
                    else
                    {
                        if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada = (ArrowLine)lineaEncontrada;
                    }
                }

            }
        }

        public List<Definicion_TextosInformacion> AgregarImplicacionElemento_DesdeOperaciones(DiseñoOperacion elemento, DiseñoCalculo calculo)
        {
            UIElement[] elementos = new UIElement[diagrama.Children.Count];
            diagrama.Children.CopyTo(elementos, 0);

            var definiciones = (from d in elementos where
                               d.GetType() != typeof(DefinicionListaCadenasTexto) &&
                               d.GetType() == typeof(Definicion_TextosInformacion) && ((Definicion_TextosInformacion)d).DiseñoTextosInformacion.OperacionRelacionada == elemento select (Definicion_TextosInformacion)d).ToList();

            if (!definiciones.Any())
            {
                btnAgregarTextoInformacion_Click(this, null);

                Definicion_TextosInformacion definicion = (Definicion_TextosInformacion)diagrama.Children[diagrama.Children.Count - 1];
                definiciones.Add(definicion);

                definicion.DiseñoTextosInformacion.OperacionRelacionada = elemento;
                definicion.DiseñoTextosInformacion.CalculoRelacionado = calculo;

                Thread.Sleep(300);
            }

            return definiciones;
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

        private ArrowLine BuscarLinea(DiseñoTextosInformacion elementoOrigen, DiseñoTextosInformacion elementoDestino)
        {
            foreach (var item in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
            {
                var elementoDestinoEncontrado = (from DiseñoTextosInformacion E in item.ElementosAnteriores where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY select E).FirstOrDefault();
                var elementoOrigenEncontrado = (from DiseñoTextosInformacion E in item.ElementosPosteriores where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY select E).FirstOrDefault();

                var elementoDestino2Encontrado = (from DiseñoTextosInformacion E in item.ElementosPosteriores where E == elementoOrigen && item == elementoDestino select E).FirstOrDefault();
                var elementoOrigen2Encontrado = (from DiseñoTextosInformacion E in item.ElementosAnteriores where E == elementoDestino && item == elementoOrigen select E).FirstOrDefault();

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

        private void CalcularCoordenadasLinea(ref double posicionXOrigen, ref double posicionYOrigen,
                ref double posicionXDestino, ref double posicionYDestino, DiseñoTextosInformacion elementoOrigen,
                DiseñoTextosInformacion elementoDestino)
        {
            posicionXOrigen = elementoOrigen.PosicionX + elementoOrigen.Anchura / 2;
            posicionYOrigen = elementoOrigen.PosicionY + elementoOrigen.Altura / 2;

            posicionXDestino = elementoDestino.PosicionX + elementoDestino.Anchura / 2;
            posicionYDestino = elementoDestino.PosicionY + elementoDestino.Altura / 2;

            if (posicionXDestino < elementoOrigen.PosicionX)
                posicionXOrigen -= elementoOrigen.Anchura / 2;
            else if (posicionXDestino > elementoOrigen.PosicionX + elementoOrigen.Anchura)
                posicionXOrigen += elementoOrigen.Anchura / 2;

            if (posicionYDestino < elementoOrigen.PosicionY)
                posicionYOrigen -= elementoOrigen.Altura / 2;
            else if (posicionYDestino > elementoOrigen.PosicionY + elementoOrigen.Altura)
                posicionYOrigen += elementoOrigen.Altura / 2;



            if (posicionXOrigen < elementoDestino.PosicionX)
                posicionXDestino -= elementoDestino.Anchura / 2;
            else if (posicionXOrigen > elementoDestino.PosicionX + elementoDestino.Anchura)
                posicionXDestino += elementoDestino.Anchura / 2;

            if (posicionYOrigen < elementoDestino.PosicionY)
                posicionYDestino -= elementoDestino.Altura / 2;
            else if (posicionYOrigen > elementoDestino.PosicionY + elementoDestino.Altura)
                posicionYDestino += elementoDestino.Altura / 2;
        }

        private void SeleccionarLinea(object sender, RoutedEventArgs e)
        {
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Linea;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada = (ArrowLine)sender;
            ElementoSeleccionado = true;

            ElementoAnteriorLineaSeleccionada = null;
            ElementoPosteriorLineaSeleccionada = null;

            EncontrarElementosLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada, ref ElementoAnteriorLineaSeleccionada, ref ElementoPosteriorLineaSeleccionada);

            DestacarElementoSeleccionado();
        }

        private void EncontrarElementosLinea(ArrowLine lineaSeleccionada, ref DiseñoTextosInformacion ElementoAnteriorLineaSeleccionada,
            ref DiseñoTextosInformacion ElementoPosteriorLineaSeleccionada)
        {
            foreach (var itemControl in (from UIElement C in diagrama.Children where C.GetType() != typeof(ArrowLine) select C).ToList())
            {
                var lineasOrigen = BuscarLineasUnElemento(new Point(Canvas.GetLeft(itemControl), Canvas.GetTop(itemControl)),
                true, itemControl);

                var lineaOrigenEncontrada = (from ArrowLine L in lineasOrigen where L == lineaSeleccionada select L).FirstOrDefault();

                if (lineaOrigenEncontrada != null)
                {
                    if (itemControl.GetType() == typeof(DefinicionListaCadenasTexto))
                        ElementoAnteriorLineaSeleccionada = ((DefinicionListaCadenasTexto)itemControl).DiseñoListaCadenasTexto;
                    else
                    if (itemControl.GetType() == typeof(Definicion_TextosInformacion))
                        ElementoAnteriorLineaSeleccionada = ((Definicion_TextosInformacion)itemControl).DiseñoTextosInformacion;
                    //if (itemControl.GetType() == typeof(OperacionEspecifica))
                    //    ElementoAnteriorLineaSeleccionada = ((OperacionEspecifica)itemControl).DiseñoOperacion;

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
                    if (itemControl.GetType() == typeof(DefinicionListaCadenasTexto))
                        ElementoAnteriorLineaSeleccionada = ((DefinicionListaCadenasTexto)itemControl).DiseñoListaCadenasTexto;
                    else
                    if (itemControl.GetType() == typeof(Definicion_TextosInformacion))
                        ElementoPosteriorLineaSeleccionada = ((Definicion_TextosInformacion)itemControl).DiseñoTextosInformacion;
                    //if (itemControl.GetType() == typeof(OperacionEspecifica))
                    //    ElementoPosteriorLineaSeleccionada = ((OperacionEspecifica)itemControl).DiseñoOperacion;

                    break;
                }
            }
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

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListarEntradas();

                if (CalculoSeleccionado != null)
                {
                    if (CalculoSeleccionado.DefinicionListaCadenasTextoSeleccionada != null)
                    {
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado = CalculoSeleccionado.DefinicionListaCadenasTextoSeleccionada;
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada = (DefinicionListaCadenasTexto)(from UIElement D in diagrama.Children
                                                                                                                                    where D.GetType() == typeof(DefinicionListaCadenasTexto) &&
                                                                                                        ((DefinicionListaCadenasTexto)D).DiseñoListaCadenasTexto == CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado
                                                                                                                                    select D).FirstOrDefault();
                        ElementoSeleccionado = true;
                        DestacarElementoSeleccionado();
                    }
                    else
                    if (CalculoSeleccionado.DefinicionSeleccionada != null)
                    {
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado = CalculoSeleccionado.DefinicionSeleccionada;
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada = (Definicion_TextosInformacion)(from UIElement D in diagrama.Children
                                                                                                                    where D.GetType() != typeof(DefinicionListaCadenasTexto) &&
                                                                                                                    D.GetType() == typeof(Definicion_TextosInformacion) &&
                                                                                        ((Definicion_TextosInformacion)D).DiseñoTextosInformacion == CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado
                                                                                                                    select D).FirstOrDefault();
                        ElementoSeleccionado = true;
                        DestacarElementoSeleccionado();
                    }
                    else if (CalculoSeleccionado.ElementosSeleccionados_TextosInformacion != null &&
                        CalculoSeleccionado.ElementosSeleccionados_TextosInformacion.Any())
                    {
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados = CalculoSeleccionado.ElementosSeleccionados_TextosInformacion;
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados = new List<UIElement>();

                        foreach (var item in CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados)
                        {
                            var itemAgregar = (DefinicionListaCadenasTexto)(from UIElement D in diagrama.Children
                                                                        where D.GetType() == typeof(DefinicionListaCadenasTexto) &&
                                            ((DefinicionListaCadenasTexto)D).DiseñoListaCadenasTexto == item
                                                                        select D).FirstOrDefault();

                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Add(itemAgregar);

                            var itemAgregar2 = (Definicion_TextosInformacion)(from UIElement D in diagrama.Children
                                                                             where D.GetType() != typeof(DefinicionListaCadenasTexto) &&
                                                                             D.GetType() == typeof(Definicion_TextosInformacion) &&
                                                 ((Definicion_TextosInformacion)D).DiseñoTextosInformacion == item
                                                                             select D).FirstOrDefault();

                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Add(itemAgregar2);
                        }

                        try
                        {
                            foreach (UIElement item in diagrama.Children)
                            {
                                if (item.GetType() == typeof(ArrowLine)) continue;
                                //if (Canvas.GetTop((UIElement)item) >= Canvas.GetTop(rectanguloSeleccion) &
                                //    Canvas.GetTop((UIElement)item) <= Canvas.GetTop(rectanguloSeleccion) + rectanguloSeleccion.Height &
                                //    Canvas.GetLeft((UIElement)item) >= Canvas.GetLeft(rectanguloSeleccion) &
                                //    Canvas.GetLeft((UIElement)item) <= Canvas.GetLeft(rectanguloSeleccion) + rectanguloSeleccion.Width)
                                //{
                                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Contains(item))
                                {
                                    if (item.GetType() == typeof(DefinicionListaCadenasTexto))
                                    {
                                        ((DefinicionListaCadenasTexto)item).Background = SystemColors.HighlightBrush;
                                        ((DefinicionListaCadenasTexto)item).botonFondo.Background = SystemColors.HighlightBrush;
                                    }
                                    else
                                    if (item.GetType() == typeof(Definicion_TextosInformacion))
                                    {
                                        ((Definicion_TextosInformacion)item).Background = SystemColors.HighlightBrush;
                                        ((Definicion_TextosInformacion)item).botonFondo.Background = SystemColors.HighlightBrush;
                                    }
                                }
                            }

                        }
                        catch (Exception) { }
                    }

                    if (double.IsNaN(CalculoDiseñoSeleccionado_Cantidades.Ancho) &
                double.IsNaN(CalculoDiseñoSeleccionado_Cantidades.Alto))
                    {
                        //CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;
                        //CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;
                        if (contenedor.ActualWidth > 0)
                        {
                            diagrama.Width = contenedor.ActualWidth;
                            CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;
                        }

                        if (contenedor.ActualHeight > 0)
                        {
                            diagrama.Height = contenedor.ActualHeight;
                            CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;
                        }

                    }

                }
                else
                {
                    diagrama.Width = contenedor.ActualWidth;
                    CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;

                    diagrama.Height = contenedor.ActualHeight;
                    CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;
                }

                ListarCalculos();

                if (!CalculoSeleccionado.Calculos.Contains(CalculoDiseñoSeleccionado_Cantidades))
                    CalculoDiseñoSeleccionado_Cantidades = CalculoSeleccionado.Calculos.First();

                SeleccionarCalculo(null, null);

                MarcarCalculoSeleccionado();
            }
            catch(Exception)
            {

            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("VerConfigurarLogicasImplicacionesCadenasTextoCalculo");
#endif
        }

        public void DestacarElementoSeleccionado()
        {
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Clear();

            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(DefinicionListaCadenasTexto))
                {
                    ((DefinicionListaCadenasTexto)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((DefinicionListaCadenasTexto)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(Definicion_TextosInformacion))
                {
                    ((Definicion_TextosInformacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((Definicion_TextosInformacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }                
                else if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(ArrowLine))
                    ((ArrowLine)item).Stroke = Brushes.Black;
            }

            if (CalculoDiseñoSeleccionado == null) return;

            if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Definicion_TextosInformacion)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada != null && 
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.EnDiagrama)
                {
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.Background = SystemColors.HighlightBrush;
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
                }

                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada?.DiseñoTextosInformacion;
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada;

                CalculoSeleccionado.DefinicionSeleccionada = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado;
            }
            else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Definicion_ListaCadenasTexto)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada != null &&
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.EnDiagrama)
                {
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.Background = SystemColors.HighlightBrush;
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
                }

                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada?.DiseñoListaCadenasTexto;
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada;

                CalculoSeleccionado.DefinicionListaCadenasTextoSeleccionada = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado;
            }
            else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.EnDiagrama)
                {
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.Background = SystemColors.HighlightBrush;
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
                }

                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion;
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada;
            }
            else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Linea)
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada.Stroke = SystemColors.HighlightBrush;


        }

        public void diagrama_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Linea &&
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada != null) return;
            //if (!ClicNota)
            //{
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.e_SeleccionarElemento = null;
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Clear();
            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Clear();

            CalculoSeleccionado.DefinicionSeleccionada = null;
            CalculoSeleccionado.DefinicionListaCadenasTextoSeleccionada = null;

            ElementoSeleccionado = false;
            ClicDiagrama = true;

            DestacarElementoSeleccionado();
            DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);
            ubicacionInicialAreaSeleccionada = e.GetPosition(diagrama);
        }

        public void DibujarTodasLineasElementos(DiseñoCalculo calculoSeleccionado)
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var item in lineas)
                diagrama.Children.Remove(item);

            foreach (var itemElemento in CalculoDiseñoSeleccionado.ElementosTextosInformacion.Where(i => i.CalculoRelacionado == calculoSeleccionado))
            {
                DibujarLineasElemento(itemElemento);
            }
        }

        private void btnAgregarTextoInformacion_Click(object sender, RoutedEventArgs e)
        {
            Definicion_TextosInformacion nuevaDefinicion = new Definicion_TextosInformacion();
            nuevaDefinicion.VistaTextos = this;
            nuevaDefinicion.EnDiagrama = true;
            nuevaDefinicion.botonFondo.BorderBrush = Brushes.Black;
            nuevaDefinicion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaDefinicion.SizeChanged += CambioTamañoDefinicion;

            DiseñoTextosInformacion nuevoElementoTextoInformacion = new DiseñoTextosInformacion();
            nuevoElementoTextoInformacion.Tipo = TipoElementoOperacion.Definicion_TextosInformacion;
            nuevoElementoTextoInformacion.PosicionX = App.PosicionXPredeterminada;
            nuevoElementoTextoInformacion.PosicionY = App.PosicionYPredeterminada;
            nuevoElementoTextoInformacion.CalculoRelacionado = CalculoDiseñoSeleccionado_Cantidades;

            var ultimoNombre = (from DiseñoTextosInformacion E in CalculoDiseñoSeleccionado.ElementosTextosInformacion where E.Nombre != null && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null)
            {
                int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            }
            cantidadElementosTipo++;

            nuevoElementoTextoInformacion.Nombre = "Definición de lógica de asignación de cadenas de texto " + cantidadElementosTipo.ToString();

            CalculoDiseñoSeleccionado.ElementosTextosInformacion.Add(nuevoElementoTextoInformacion);
            nuevaDefinicion.DiseñoTextosInformacion = nuevoElementoTextoInformacion;

            diagrama.Children.Add(nuevaDefinicion);

            Canvas.SetTop(nuevaDefinicion, App.PosicionYPredeterminada);
            Canvas.SetLeft(nuevaDefinicion, App.PosicionXPredeterminada);

            //if(double.IsNaN(nuevoCalculo.DiseñoCalculo.AnchuraElemento) &
            //    double.IsNaN(nuevoCalculo.DiseñoCalculo.AlturaElemento))
            //{
            //    nuevoElementoCalculo.AnchuraElemento = nuevoCalculo.ActualWidth;
            //    nuevoElementoCalculo.AlturaElemento = nuevoCalculo.ActualHeight;
            //}


            EstablecerIndicesProfundidadElementos();
        }

        private void btnQuitarTextoInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada != null)
                {
                    MessageBoxResult resp = MessageBox.Show("¿Quitar esta definición de lista de cadenas de texto de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resp == MessageBoxResult.Yes)
                    {
                        foreach (var item in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
                            QuitarDeElementosPosterioresAnteriores(item, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado);

                        CalculoDiseñoSeleccionado.ElementosTextosInformacion.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado);

                        diagrama.Children.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada);

                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoListaSeleccionado = null;

                        DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);

                    }
                }
                else
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada != null)
                {
                    MessageBoxResult resp = MessageBox.Show("¿Quitar esta definición de lógica de asignación de cadenas de texto de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resp == MessageBoxResult.Yes)
                    {
                        foreach (var item in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
                            QuitarDeElementosPosterioresAnteriores(item, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado);

                        CalculoDiseñoSeleccionado.ElementosTextosInformacion.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado);

                        diagrama.Children.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada);

                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado = null;

                        DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);

                    }
                }
                else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada != null)
                {
                    MessageBoxResult resp = MessageBox.Show("¿Quitar esta entrada de vector de cadenas de texto de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resp == MessageBoxResult.Yes)
                    {
                        foreach (var item in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
                        {
                            QuitarDefinicionesTextosInformacion_Entrada(item, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.Entrada);
                            QuitarDeElementosPosterioresAnteriores(item, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado);
                        }

                        CalculoDiseñoSeleccionado.ElementosTextosInformacion.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado);

                        diagrama.Children.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada);

                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado = null;

                        DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);

                    }
                }
                else
                {
                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada != null)
                    {
                        if (ElementoAnteriorLineaSeleccionada != null & ElementoPosteriorLineaSeleccionada != null)
                        {
                            ElementoAnteriorLineaSeleccionada.ElementosPosteriores.Remove(ElementoPosteriorLineaSeleccionada);

                            if (ElementoAnteriorLineaSeleccionada.Tipo == TipoElementoOperacion.Entrada &
                                ElementoPosteriorLineaSeleccionada.Tipo == TipoElementoOperacion.Definicion_TextosInformacion)
                                QuitarDefinicionesTextosInformacion_Entrada(ElementoPosteriorLineaSeleccionada, ElementoAnteriorLineaSeleccionada.EntradaRelacionada);

                            ElementoPosteriorLineaSeleccionada.ElementosAnteriores.Remove(ElementoAnteriorLineaSeleccionada);
                            //ElementoAnteriorLineaSeleccionada.ElementosContenedoresOperacion.Remove(ElementoPosteriorLineaSeleccionada);
                            if (ElementoPosteriorLineaSeleccionada.Tipo == TipoElementoOperacion.Entrada &
                                ElementoAnteriorLineaSeleccionada.Tipo == TipoElementoOperacion.Definicion_TextosInformacion)
                                QuitarDefinicionesTextosInformacion_Entrada(ElementoAnteriorLineaSeleccionada, ElementoPosteriorLineaSeleccionada.EntradaRelacionada);
                        }

                        diagrama.Children.Remove(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.lineaSeleccionada);
                    }
                }

                DibujarElementosTextosInformacion(CalculoDiseñoSeleccionado_Cantidades);
            }
            else
            {
                foreach (var itemElemento in CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados)
                {
                    if (itemElemento.GetType() == typeof(DefinicionListaCadenasTexto))
                    {

                        //QuitarTodasLineas();

                        foreach (var item in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
                            QuitarDeElementosPosterioresAnteriores(item, itemElemento);

                        CalculoDiseñoSeleccionado.ElementosTextosInformacion.Remove(itemElemento);

                        DefinicionListaCadenasTexto definicionSeleccionada = (DefinicionListaCadenasTexto)(from UIElement E in diagrama.Children
                                                                                                             where E.GetType() == typeof(DefinicionListaCadenasTexto) &&
                                                                       ((DefinicionListaCadenasTexto)E).DiseñoListaCadenasTexto == itemElemento
                                                                                                             select E).FirstOrDefault();

                        if (definicionSeleccionada != null)
                            diagrama.Children.Remove(definicionSeleccionada);
                        else
                        {
                            EntradaDiseñoOperaciones entradaSeleccionada = (EntradaDiseñoOperaciones)(from UIElement E in diagrama.Children
                                                                                                      where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                                                                ((EntradaDiseñoOperaciones)E).DiseñoTextosInformacion == itemElemento
                                                                                                      select E).FirstOrDefault();

                            if (entradaSeleccionada != null)
                                diagrama.Children.Remove(entradaSeleccionada);
                        }
                    }
                    else
                    if (itemElemento.GetType() == typeof(DiseñoTextosInformacion))
                    {

                        //QuitarTodasLineas();

                        foreach (var item in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
                            QuitarDeElementosPosterioresAnteriores(item, itemElemento);

                        CalculoDiseñoSeleccionado.ElementosTextosInformacion.Remove(itemElemento);

                        Definicion_TextosInformacion definicionSeleccionada = (Definicion_TextosInformacion)(from UIElement E in diagrama.Children
                                                                                                             where E.GetType() != typeof(DefinicionListaCadenasTexto) &&
                                                                                                             E.GetType() == typeof(Definicion_TextosInformacion) &&
                                                                       ((Definicion_TextosInformacion)E).DiseñoTextosInformacion == itemElemento
                                                                                                             select E).FirstOrDefault();

                        if (definicionSeleccionada != null)
                            diagrama.Children.Remove(definicionSeleccionada);
                        else
                        {
                            EntradaDiseñoOperaciones entradaSeleccionada = (EntradaDiseñoOperaciones)(from UIElement E in diagrama.Children
                                                                                                      where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                                                                ((EntradaDiseñoOperaciones)E).DiseñoTextosInformacion == itemElemento
                                                                                                      select E).FirstOrDefault();

                            if (entradaSeleccionada != null)
                                diagrama.Children.Remove(entradaSeleccionada);
                        }
                    }                    
                }

                DibujarElementosTextosInformacion(CalculoDiseñoSeleccionado_Cantidades);

            }
        }
        public void QuitarDeElementosPosterioresAnteriores(DiseñoTextosInformacion elemento, DiseñoTextosInformacion elementoAQuitar)
        {
            var item = (from DiseñoTextosInformacion E in elemento.ElementosAnteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                if (item.Tipo == TipoElementoOperacion.Entrada &
                    elemento.Tipo == TipoElementoOperacion.Definicion_TextosInformacion)
                    QuitarDefinicionesTextosInformacion_Entrada(elemento, item.EntradaRelacionada);
                elemento.ElementosAnteriores.Remove(item);
            }

            item = (from DiseñoTextosInformacion E in elemento.ElementosPosteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                if (elemento.Tipo == TipoElementoOperacion.Entrada &
                    item.Tipo == TipoElementoOperacion.Definicion_TextosInformacion)
                    QuitarDefinicionesTextosInformacion_Entrada(item, elemento.EntradaRelacionada);
                elemento.ElementosPosteriores.Remove(item);
            }
        }

        private void diagrama_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed & !ElementoSeleccionado & ClicDiagrama)
            {
                ubicacionFinalAreaSeleccionada = e.GetPosition(diagrama);

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
                //try
                {
                    foreach (UIElement item in diagrama.Children)
                    {
                        if (item.GetType() == typeof(ArrowLine)) continue;
                        if (Canvas.GetTop((UIElement)item) >= Canvas.GetTop(rectanguloSeleccion) &
                            Canvas.GetTop((UIElement)item) <= Canvas.GetTop(rectanguloSeleccion) + rectanguloSeleccion.Height &
                            Canvas.GetLeft((UIElement)item) >= Canvas.GetLeft(rectanguloSeleccion) &
                            Canvas.GetLeft((UIElement)item) <= Canvas.GetLeft(rectanguloSeleccion) + rectanguloSeleccion.Width)
                        {
                            if (!CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Contains(item))
                            {
                                if (item.GetType() == typeof(DefinicionListaCadenasTexto))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((DefinicionListaCadenasTexto)item).Clic, DispatcherPriority.Loaded, false);
                                    }

                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Add(((DefinicionListaCadenasTexto)item).DiseñoListaCadenasTexto);
                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Add(item);
                                }
                                else
                                if (item.GetType() == typeof(Definicion_TextosInformacion))
                                {
                                    //((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightTextBrush;
                                    //((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightTextBrush;
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((Definicion_TextosInformacion)item).Clic, DispatcherPriority.Loaded, false);
                                    }

                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Add(((Definicion_TextosInformacion)item).DiseñoTextosInformacion);
                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                                {
                                    //((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightTextBrush;
                                    //((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightTextBrush;
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)item).Clic, DispatcherPriority.Loaded);
                                    }

                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Add(((EntradaDiseñoOperaciones)item).DiseñoTextosInformacion);
                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.GetType() == typeof(DefinicionListaCadenasTexto))
                            {
                                ((DefinicionListaCadenasTexto)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((DefinicionListaCadenasTexto)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Remove(((DefinicionListaCadenasTexto)item).DiseñoListaCadenasTexto);
                            }
                            else
                            if (item.GetType() == typeof(Definicion_TextosInformacion))
                            {
                                ((Definicion_TextosInformacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((Definicion_TextosInformacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Remove(((Definicion_TextosInformacion)item).DiseñoTextosInformacion);
                            }
                            else if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                            {
                                ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Remove(((EntradaDiseñoOperaciones)item).DiseñoTextosInformacion);
                            }

                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Remove(item);
                        }
                    }

                    CalculoSeleccionado.ElementosSeleccionados_TextosInformacion = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados;
                }
                //catch (Exception) { }
            }
            else if (e.LeftButton == MouseButtonState.Released & !ElementoSeleccionado & ClicDiagrama)
            {
                ClicDiagrama = false;
                if (rectanguloSeleccion != null)
                    diagrama.Children.Remove(rectanguloSeleccion);
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
            CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;
            CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;
        }

        private void disminuirArea_Click(object sender, RoutedEventArgs e)
        {
            if (diagrama.ActualHeight - 300 > 0 &
                diagrama.ActualWidth - 300 > 0)
            {
                diagrama.Height = diagrama.ActualHeight - 300;
                diagrama.Width = diagrama.ActualWidth - 300;
                CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;
                CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;
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

                SeleccionandoElemento = true;
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.e_SeleccionarElemento = new MouseButtonEventArgs(Mouse.PrimaryDevice, DateTime.Now.Hour, MouseButton.Left);

                if (elemento.GetType() == typeof(DefinicionListaCadenasTexto))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((DefinicionListaCadenasTexto)elemento).Clic);
                    }
                }
                else
                if (elemento.GetType() == typeof(Definicion_TextosInformacion))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((Definicion_TextosInformacion)elemento).Clic);
                    }
                }

                

                if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)elemento).Clic);
                    }
                }

                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.e_SeleccionarElemento = null;
                SeleccionandoElemento = false;

                contenedor.ScrollToHorizontalOffset(Canvas.GetLeft(elemento) * escalaZoom);
                contenedor.ScrollToVerticalOffset(Canvas.GetTop(elemento) * escalaZoom);
            }
        }

        private List<UIElement> BuscarElementosDiagramas(string textoBusqueda)
        {
            List<UIElement> elementos = new List<UIElement>();
            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(DefinicionListaCadenasTexto))
                {
                    DefinicionListaCadenasTexto definicion = (DefinicionListaCadenasTexto)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (definicion.nombreDefinicion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(definicion);
                    }
                }
                else
                if (item.GetType() == typeof(Definicion_TextosInformacion))
                {
                    Definicion_TextosInformacion definicion = (Definicion_TextosInformacion)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (definicion.nombreDefinicion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(definicion);
                    }
                }
                else if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    EntradaDiseñoOperaciones entrada = (EntradaDiseñoOperaciones)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (entrada.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                            entrada.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(entrada);
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

        private void diagrama_Drop(object sender, DragEventArgs e)
        {
            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada != null)
                {
                    DiseñoTextosInformacion arrastreHastaOtroElemento;
                    arrastreHastaOtroElemento = VerificarArrastreOtroElemento(e.GetPosition(diagrama));

                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada) + CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada) + CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.ActualHeight))
                            return;

                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto)
                        {
                            ArrowLine nuevaLinea = BuscarLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto, arrastreHastaOtroElemento);
                            if (nuevaLinea != null)
                            {
                                var lineaEncontrada = (from UIElement L in diagrama.Children
                                                       where (L.GetType() == typeof(ArrowLine)) &&
                       (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
                       ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                                       select L).FirstOrDefault();

                                if (lineaEncontrada == null)
                                    diagrama.Children.Add(nuevaLinea);
                            }
                            else
                            {
                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                arrastreHastaOtroElemento.ElementosAnteriores.Add(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto);
                                //CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);

                                ArrowLine linea = UbicarLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto, arrastreHastaOtroElemento);
                                linea.MouseLeftButtonDown += SeleccionarLinea;
                                diagrama.Children.Add(linea);

                                EstablecerIndicesProfundidadElementos();
                            }
                        }
                        else
                        {
                            QuitarTodasLineas();

                            EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto, e);

                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto.PosicionX = ubicacionActualElemento.X;
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto.PosicionY);
                            Canvas.SetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada.DiseñoListaCadenasTexto.PosicionX);

                            DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);

                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                }
                else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada != null)
                {
                    DiseñoTextosInformacion arrastreHastaOtroElemento;
                    arrastreHastaOtroElemento = VerificarArrastreOtroElemento(e.GetPosition(diagrama));

                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada) + CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada) + CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.ActualHeight))
                            return;

                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion)
                        {
                            ArrowLine nuevaLinea = BuscarLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion, arrastreHastaOtroElemento);
                            if (nuevaLinea != null)
                            {
                                var lineaEncontrada = (from UIElement L in diagrama.Children
                                                       where (L.GetType() == typeof(ArrowLine)) &&
                       (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
                       ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                                       select L).FirstOrDefault();

                                if (lineaEncontrada == null)
                                    diagrama.Children.Add(nuevaLinea);
                            }
                            else
                            {
                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                arrastreHastaOtroElemento.ElementosAnteriores.Add(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion);
                                //CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);

                                ArrowLine linea = UbicarLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion, arrastreHastaOtroElemento);
                                linea.MouseLeftButtonDown += SeleccionarLinea;
                                diagrama.Children.Add(linea);

                                EstablecerIndicesProfundidadElementos();
                            }
                        }
                        else
                        {
                            QuitarTodasLineas();

                            EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion, e);

                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.PosicionX = ubicacionActualElemento.X;
                            CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.PosicionY);
                            Canvas.SetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion.PosicionX);

                            DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);

                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                }
                else
                {
                    DiseñoTextosInformacion arrastreHastaOtroElemento;
                    arrastreHastaOtroElemento = VerificarArrastreOtroElemento(e.GetPosition(diagrama));

                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                    {
                        if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.EnDiagrama)
                        {
                            if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada) &
                                e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada) + CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.ActualWidth) &
                                (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada) &
                                e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada) + CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.ActualHeight))
                                return;


                            if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento.GetType() != CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.GetType()) //&& arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.Salida)
                            {
                                ArrowLine nuevaLinea = BuscarLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion, arrastreHastaOtroElemento);
                                if (nuevaLinea != null)
                                {
                                    var lineaEncontrada = (from UIElement L in diagrama.Children
                                                           where (L.GetType() == typeof(ArrowLine)) &&
                          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
                          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                                           select L).FirstOrDefault();

                                    if (lineaEncontrada == null)
                                        diagrama.Children.Add(nuevaLinea);
                                }
                                else
                                {
                                    if (arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.Entrada)
                                    {

                                        //if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion.ContieneSalida)
                                        //{
                                        //    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion.ContieneSalida = false;
                                        //    QuitarElementoSalida(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion, CalculoDiseñoSeleccionado);
                                        //    EstablecerTextoBotonSalida(false);
                                        //    //DibujarElementosOperaciones();
                                        //}

                                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                        arrastreHastaOtroElemento.ElementosAnteriores.Add(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion);
                                        //AgregarDefinicionesTextosInformacion_Entrada(arrastreHastaOtroElemento, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion);
                                        //if (arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.Entrada)
                                        //    arrastreHastaOtroElemento.EntradaRelacionada = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion.EntradaRelacionada;

                                        //CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);
                                        //CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.AgregarOrdenOperando(arrastreHastaOtroElemento);
                                        //arrastreHastaOtroElemento.OrdenarOperandos(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion);

                                        ArrowLine linea = UbicarLinea(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion, arrastreHastaOtroElemento);
                                        linea.MouseLeftButtonDown += SeleccionarLinea;
                                        diagrama.Children.Add(linea);
                                        EstablecerIndicesProfundidadElementos();

                                        //QuitarActualizarResultados_ElementosConectados(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion);

                                        DibujarElementosTextosInformacion(CalculoDiseñoSeleccionado_Cantidades);
                                        //MostrarOrdenOperando_Elemento(null);

                                        //Ventana.ActualizarPestañaElementoOperacion(arrastreHastaOtroElemento);
                                    }
                                }
                            }
                            else
                            {
                                EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada,
                                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion, e);//Point ubicacionOriginal = new Point(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion.PosicionX, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion.PosicionY);

                                QuitarTodasLineas();
                                //ReubicarLineasUnElemento(lineas, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion, true);

                                //lineas = BuscarLineasUnElemento(ubicacionOriginal, false, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada);
                                //foreach (var itemLinea in lineas)
                                //    diagrama.Children.Remove(itemLinea);
                                //ReubicarLineasUnElemento(lineas, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion, false);

                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.PosicionX = ubicacionActualElemento.X;
                                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.PosicionY = ubicacionActualElemento.Y;

                                Canvas.SetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.PosicionY);
                                Canvas.SetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoTextosInformacion.PosicionX);

                                //ReubicarLineasUnElemento();
                                //DibujarLineasElemento(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion);
                                DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);

                                EstablecerIndicesProfundidadElementos();
                                //MostrarOrdenOperando_Elemento(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.DiseñoOperacion);
                            }
                        }
                        else
                        {
                            AgregarEntrada(sender, e);
                        }
                    }
                }

                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado == null) ElementoSeleccionado = false;
            }
            else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados.Any())
            {
                Point diferenciaDistanciaPunto = new Point(0, 0);

                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover.GetType() == typeof(DefinicionListaCadenasTexto))
                {
                    EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover,
                        ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto.PosicionX,
                        ubicacionActualElemento.Y - ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto.PosicionY);

                    ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto.PosicionX = ubicacionActualElemento.X;
                    ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover, ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto.PosicionY);
                    Canvas.SetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover, ((DefinicionListaCadenasTexto)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoListaCadenasTexto.PosicionX);

                }
                else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover.GetType() == typeof(Definicion_TextosInformacion))
                {
                    EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover,
                        ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionX,
                        ubicacionActualElemento.Y - ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionY);

                    ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionX = ubicacionActualElemento.X;
                    ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover, ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionY);
                    Canvas.SetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover, ((Definicion_TextosInformacion)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionX);

                }
                else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover,
                        ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionX,
                        ubicacionActualElemento.Y - ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionY);

                    ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionX = ubicacionActualElemento.X;
                    ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover, ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionY);
                    Canvas.SetLeft(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover, ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover).DiseñoTextosInformacion.PosicionX);

                }
                

                foreach (UIElement elemento in CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados)
                {
                    if (elemento != null)
                    {
                        if (elemento != CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover)
                        {
                            if (elemento.GetType() == typeof(DefinicionListaCadenasTexto))
                            {
                                ((DefinicionListaCadenasTexto)elemento).DiseñoListaCadenasTexto.PosicionX += diferenciaDistanciaPunto.X;
                                ((DefinicionListaCadenasTexto)elemento).DiseñoListaCadenasTexto.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((DefinicionListaCadenasTexto)elemento).DiseñoListaCadenasTexto.PosicionY);
                                Canvas.SetLeft(elemento, ((DefinicionListaCadenasTexto)elemento).DiseñoListaCadenasTexto.PosicionX);

                            }
                            else if (elemento.GetType() == typeof(Definicion_TextosInformacion))
                            {
                                ((Definicion_TextosInformacion)elemento).DiseñoTextosInformacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((Definicion_TextosInformacion)elemento).DiseñoTextosInformacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((Definicion_TextosInformacion)elemento).DiseñoTextosInformacion.PosicionY);
                                Canvas.SetLeft(elemento, ((Definicion_TextosInformacion)elemento).DiseñoTextosInformacion.PosicionX);

                            }
                            else if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                            {
                                ((EntradaDiseñoOperaciones)elemento).DiseñoTextosInformacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((EntradaDiseñoOperaciones)elemento).DiseñoTextosInformacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((EntradaDiseñoOperaciones)elemento).DiseñoTextosInformacion.PosicionY);
                                Canvas.SetLeft(elemento, ((EntradaDiseñoOperaciones)elemento).DiseñoTextosInformacion.PosicionX);

                            }
                        }
                    }
                }

                QuitarTodasLineas();
                DibujarTodasLineasElementos(CalculoDiseñoSeleccionado_Cantidades);
                EstablecerIndicesProfundidadElementos();
            }
        }

        public void AgregarEntrada(object sender, DragEventArgs e)
        {
            EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();

            double X_Punto = 10;
            double Y_Punto = 10;

            nuevaEntrada.VistaTextosInformacion = this;
            nuevaEntrada.EnDiagrama = true;
            nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
            nuevaEntrada.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaEntrada.Entrada = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.EntradaSeleccionada.Entrada;
            nuevaEntrada.SizeChanged += CambioTamañoEntrada;

            DiseñoTextosInformacion nuevoElementoOperacion = new DiseñoTextosInformacion();
            nuevoElementoOperacion.EntradaRelacionada = nuevaEntrada.Entrada;
            nuevoElementoOperacion.Tipo = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado;
            nuevoElementoOperacion.CalculoRelacionado = CalculoDiseñoSeleccionado_Cantidades;

            if(e != null)
            {
                nuevoElementoOperacion.PosicionX = e.GetPosition(diagrama).X;
                nuevoElementoOperacion.PosicionY = e.GetPosition(diagrama).Y;
            }
            else
            {
                nuevoElementoOperacion.PosicionX = X_Punto;
                nuevoElementoOperacion.PosicionY = Y_Punto;
            }

            CalculoDiseñoSeleccionado.ElementosTextosInformacion.Add(nuevoElementoOperacion);
            nuevaEntrada.DiseñoTextosInformacion = nuevoElementoOperacion;

            diagrama.Children.Add(nuevaEntrada);

            if (e != null)
            {
                Canvas.SetTop(nuevaEntrada, e.GetPosition(diagrama).Y);
                Canvas.SetLeft(nuevaEntrada, e.GetPosition(diagrama).X);
            }
            else
            {
                Canvas.SetTop(nuevaEntrada, Y_Punto);
                Canvas.SetLeft(nuevaEntrada, X_Punto);
            }

            EstablecerIndicesProfundidadElementos();
            //MostrarOrdenOperando_Elemento(null);
        }

        private DiseñoTextosInformacion VerificarArrastreOtroElemento(Point ubicacion)
        {
            foreach (var itemElemento in CalculoDiseñoSeleccionado.ElementosTextosInformacion)
            {
                var itemControl = (from UIElement C in diagrama.Children
                                   where
(C.GetType() == typeof(Definicion_TextosInformacion) && (
itemElemento.GetType() != typeof(DiseñoListaCadenasTexto) &&
((Definicion_TextosInformacion)C).DiseñoTextosInformacion == itemElemento))
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

        private DiseñoOperacion VerificarArrastreOtroElemento_Entrada(Point ubicacion)
        {
            foreach (var itemElemento in CalculoSeleccionado.Calculos.FirstOrDefault(i => i.EsEntradasArchivo).ElementosOperaciones)
            {
                var itemControl = (from UIElement C in diagrama.Children
                                   where
((C.GetType() == typeof(OperacionEspecifica) && ((OperacionEspecifica)C).DiseñoOperacion == itemElemento)) ||
((C.GetType() == typeof(EntradaDiseñoOperaciones) && ((EntradaDiseñoOperaciones)C).DiseñoOperacion == itemElemento))
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

        private ArrowLine UbicarLinea(DiseñoTextosInformacion elementoOrigen, DiseñoTextosInformacion elementoDestino)
        {
            ArrowLine linea = new ArrowLine();
            linea.Stroke = Brushes.Black;

            double posicionXOrigen = 0;
            double posicionYOrigen = 0;

            double posicionXDestino = 0;
            double posicionYDestino = 0;

            CalcularCoordenadasLinea(ref posicionXOrigen, ref posicionYOrigen,
                ref posicionXDestino, ref posicionYDestino, elementoOrigen, elementoDestino);

            linea.X1 = posicionXOrigen;
            linea.Y1 = posicionYOrigen;

            linea.X2 = posicionXDestino;
            linea.Y2 = posicionYDestino;

            return linea;
        }

        private void QuitarTodasLineas()
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var itemLinea in lineas)
                diagrama.Children.Remove(itemLinea);
        }

        private void CambioTamañoEntrada(object sender, SizeChangedEventArgs e)
        {
            EntradaDiseñoOperaciones entrada = (EntradaDiseñoOperaciones)sender;
            entrada.DiseñoTextosInformacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoTextosInformacion.Altura = entrada.ActualHeight;
        }

        private void EstablecerCoordenadasElementoMover(UIElement elemento, DiseñoTextosInformacion elementoCalculo, DragEventArgs e)
        {
            Point puntoElemento;

            if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                puntoElemento = ((EntradaDiseñoOperaciones)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(DefinicionListaCadenasTexto))
                puntoElemento = ((DefinicionListaCadenasTexto)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(Definicion_TextosInformacion))
                puntoElemento = ((Definicion_TextosInformacion)elemento).PuntoMouseClic;

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

        private void CambioTamañoDefinicion(object sender, SizeChangedEventArgs e)
        {
            Definicion_TextosInformacion entrada = (Definicion_TextosInformacion)sender;
            entrada.DiseñoTextosInformacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoTextosInformacion.Altura = entrada.ActualHeight;
        }

        private void CambioTamañoDefinicion_ListaCadenas(object sender, SizeChangedEventArgs e)
        {
            DefinicionListaCadenasTexto entrada = (DefinicionListaCadenasTexto)sender;
            entrada.DiseñoListaCadenasTexto.Anchura = entrada.ActualWidth;
            entrada.DiseñoListaCadenasTexto.Altura = entrada.ActualHeight;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (contenedor.MaxWidth == double.PositiveInfinity &&
                contenedor.MaxHeight == double.PositiveInfinity)
            {
                //contenedor.Measure(Size.Empty);
                //contenedor.Arrange(;
                //Measure(Size.Empty);
                //Arrange();

                contenedor.MaxHeight = contenedor.ActualHeight;
                contenedor.MaxWidth = contenedor.ActualWidth;
            }

            if (CalculoDiseñoSeleccionado_Cantidades != null)
            {
                if (double.IsNaN(CalculoDiseñoSeleccionado_Cantidades.Ancho) &
                        double.IsNaN(CalculoDiseñoSeleccionado_Cantidades.Alto))
                {
                    diagrama.Width = contenedor.ActualWidth;
                    CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;

                    diagrama.Height = contenedor.ActualHeight;
                    CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;

                }
                else
                {
                    diagrama.Width = CalculoDiseñoSeleccionado_Cantidades.Ancho;
                    diagrama.Height = CalculoDiseñoSeleccionado_Cantidades.Alto;
                }
            }
        }

        public void btnDefinicionSimple_RelacionesTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.EntradaRelacionada == null)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada == null)
                {
                    btnOperacion_RelacionesTextosInformacion_Click(this, e);
                }

                DefinirRelaciones_TextosInformacion definir = new DefinirRelaciones_TextosInformacion();
                definir.listaTextos.ImplicacionesTextosInformacion = CopiarImplicaciones(CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.Relaciones_TextosInformacion);
                definir.listaTextos.Entradas = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.ObtenerEntradas();
                definir.listaTextos.Definiciones = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.ObtenerDefiniciones();
                definir.listaTextos.DefinicionesListas = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.ObtenerDefinicionesListas();

                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.CalculoRelacionado != null)
                    definir.Elementos = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.CalculoRelacionado.ElementosOperaciones;
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada != null)
                {
                    definir.Operandos = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada.ElementosAnteriores.ToList();
                    definir.SubOperandos = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada.ElementosDiseñoOperacion;
                    definir.OperacionRelacionada_Definicion = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada;

                    if (!CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada.DefinicionSimple_Operacion)
                    {
                        definir.listaTextos.opcionIncluirAsignacionDentroDefinicionNormal.Visibility = Visibility.Visible;
                        definir.listaTextos.opcionIncluirAsignacionDentroDefinicionNormal.IsChecked = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada.IncluirAsignacionDentroDefinicionNormal;
                    }
                }
                definir.CalculoAsociado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.CalculoRelacionado;

                if (definir.ShowDialog() == true)
                {
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.Relaciones_TextosInformacion = definir.listaTextos.ImplicacionesTextosInformacion;
                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada != null)
                        CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada.IncluirAsignacionDentroDefinicionNormal = (bool)definir.listaTextos.opcionIncluirAsignacionDentroDefinicionNormal.IsChecked;
                }
            }
            else if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.EntradaRelacionada != null)
            {
                DefinirOpcionesTextosInformacion_Entrada definir = new DefinirOpcionesTextosInformacion_Entrada();
                definir.Entrada = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.EntradaRelacionada;
                definir.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
                definir.OpcionTextoInformacion_Condicion_AsignacionImplicacion = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OpcionTextoInformacion_Condicion_AsignacionImplicacion;

                bool definicionEstablecida = (bool)definir.ShowDialog();
                if (definicionEstablecida)
                {
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = definir.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OpcionTextoInformacion_Condicion_AsignacionImplicacion = definir.OpcionTextoInformacion_Condicion_AsignacionImplicacion;

                    //foreach (var itemOpciones in definir.OpcionesSeleccionadasBusquedas)
                    //{
                    //    itemOpciones.Busqueda.OpcionTextoInformacion_Condicion_AsignacionImplicacion = itemOpciones.OpcionTextoInformacion_Condicion_AsignacionImplicacion;
                    //    itemOpciones.Busqueda.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = itemOpciones.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
                    //}
                }
            }
        }

        public List<AsignacionImplicacion_TextosInformacion> CopiarImplicaciones(List<AsignacionImplicacion_TextosInformacion> lista)
        {
            List<AsignacionImplicacion_TextosInformacion> listaCopiada = new List<AsignacionImplicacion_TextosInformacion>();
            foreach (var item in lista)
                listaCopiada.Add(item.ReplicarObjeto());

            return listaCopiada;
        }
        private void btnOperacion_RelacionesTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.EntradaRelacionada == null)
            {
                DefinirOperacion_TextosInformacion definir = new DefinirOperacion_TextosInformacion();
                definir.Calculo = CalculoSeleccionado;
                definir.OperacionRelacionada = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada;
                definir.CalculoRelacionadoSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.CalculoRelacionado;

                bool definicionEstablecida = (bool)definir.ShowDialog();
                if (definicionEstablecida)
                {
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada = definir.OperacionRelacionada;
                    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.CalculoRelacionado = definir.CalculoRelacionadoSeleccionado;
                }
            }
        }

        public void btnDefinicionNormal_RelacionesTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado &&
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.EntradaRelacionada == null)
            {
                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada == null)
                {
                    btnOperacion_RelacionesTextosInformacion_Click(this, e);
                }
                //if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Salida |
                //    CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota) return;

                if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoSeleccionado.OperacionRelacionada != null)
                {
                    DiseñoTextosInformacion ElementoDiseñoTextosInformacionSeleccionado = null;
                    if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna)
                    {
                        if (CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Definicion_TextosInformacion)
                        {
                            ElementoDiseñoTextosInformacionSeleccionado = CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionSeleccionada.DiseñoTextosInformacion;

                            if (ElementoDiseñoTextosInformacionSeleccionado != null)
                            {
                                var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) && ((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada == ElementoDiseñoTextosInformacionSeleccionado.OperacionRelacionada) select T).FirstOrDefault();
                                if (tab != null)
                                {
                                    Ventana.contenido.SelectedItem = tab;
                                }
                                else
                                {
                                    Ventana.AgregarTabDiseñoOperacion_TextosInformacion(ref ElementoDiseñoTextosInformacionSeleccionado);
                                }
                            }
                        }
                        else
                            MessageBox.Show("El elemento no es una definición válida.", "Diseño de lógica de asignación de cadenas de texto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            else
            {
                foreach (var item in CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosSeleccionados)
                {
                    //if (item.Tipo == TipoElementoOperacion.Salida) continue;

                    if (item.OperacionRelacionada != null & item.EntradaRelacionada == null)
                    {
                        DiseñoTextosInformacion ElementoDiseñoTextosInformacionSeleccionado = null;
                        if (item.Tipo != TipoElementoOperacion.Ninguna)
                        {
                            if (item.Tipo == TipoElementoOperacion.Definicion_TextosInformacion)
                            {
                                ElementoDiseñoTextosInformacionSeleccionado = item;

                                if (ElementoDiseñoTextosInformacionSeleccionado != null)
                                {
                                    //if (VerificarSiTieneConjuntoNumeros(ElementoDiseñoTextosInformacionSeleccionado))
                                    //{
                                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) && ((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada == ElementoDiseñoTextosInformacionSeleccionado.OperacionRelacionada) select T).FirstOrDefault();
                                    if (tab != null)
                                    {
                                        Ventana.contenido.SelectedItem = tab;
                                    }
                                    else
                                    {
                                        Ventana.AgregarTabDiseñoOperacion_TextosInformacion(ref ElementoDiseñoTextosInformacionSeleccionado);
                                    }
                                    //}
                                    //else
                                    //    MessageBox.Show("La operación no tiene elementos de conjunto de números relacionados para diseñar una operación compleja.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                }
                            }
                            //else
                            //    MessageBox.Show("El elemento no es una operación válida para diseñar una operación compleja.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                }
            }
        }

        //private void AgregarDefinicionesTextosInformacion_Entrada(DiseñoTextosInformacion definicion, DiseñoTextosInformacion entrada)
        //{
        //    AsignacionImplicacion_TextosInformacion asignacionImplicacion = new AsignacionImplicacion_TextosInformacion();
        //    asignacionImplicacion.CondicionAsignacion = TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual;
        //    asignacionImplicacion.EntradaRelacionada = entrada.EntradaRelacionada;

        //    definicion.Relaciones_TextosInformacion.Add(asignacionImplicacion);

        //    foreach (var itemBusqueda in entrada.EntradaRelacionada.BusquedasTextosInformacion)
        //    {
        //        AsignacionImplicacion_TextosInformacion asignacionImplicacion_ = new AsignacionImplicacion_TextosInformacion();
        //        asignacionImplicacion_.CondicionAsignacion = TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual;
        //        asignacionImplicacion_.BusquedaRelacionada = itemBusqueda;

        //        definicion.Relaciones_TextosInformacion.Add(asignacionImplicacion_);

        //        foreach (var itemBusquedaConjunto in itemBusqueda.ConjuntoBusquedas)
        //        {
        //            AsignacionImplicacion_TextosInformacion asignacionImplicacion__ = new AsignacionImplicacion_TextosInformacion();
        //            asignacionImplicacion__.CondicionAsignacion = TipoOpcionImplicacion_AsignacionTextoInformacion.TextoIgual;
        //            asignacionImplicacion__.BusquedaRelacionada = itemBusquedaConjunto;

        //            definicion.Relaciones_TextosInformacion.Add(asignacionImplicacion__);
        //        }
        //    }
        //}

        private void QuitarDefinicionesTextosInformacion_Entrada(DiseñoTextosInformacion definicion, Entrada entrada)
        {
            //var asignacionesImplicacionesAquitar = (from A in definicion.Relaciones_TextosInformacion where entrada.BusquedasTextosInformacion.Any(i => i == A.BusquedaRelacionada | i.ConjuntoBusquedas.Any(j => j == A.BusquedaRelacionada)) select A).ToList();
            var asignacionesImplicacionesAquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesAquitar.First());
                asignacionesImplicacionesAquitar.Remove(asignacionesImplicacionesAquitar.First());
            }

            var asignacionesImplicacionesTextosAquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosAquitar.First());
                asignacionesImplicacionesTextosAquitar.Remove(asignacionesImplicacionesTextosAquitar.First());
            }

            var asignacionesImplicacionesTextosAquitar2 = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosAquitar2.First());
                asignacionesImplicacionesTextosAquitar2.Remove(asignacionesImplicacionesTextosAquitar2.First());
            }

            var asignacionesImplicacionesTextosAquitar3 = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosAquitar3.First());
                asignacionesImplicacionesTextosAquitar3.Remove(asignacionesImplicacionesTextosAquitar3.First());
            }

            var asignacionesImplicacionesTextosCondicionesAquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosCondicionesAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosCondicionesAquitar.First());
                asignacionesImplicacionesTextosCondicionesAquitar.Remove(asignacionesImplicacionesTextosCondicionesAquitar.First());
            }

            asignacionesImplicacionesTextosCondicionesAquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosCondicionesAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosCondicionesAquitar.First());
                asignacionesImplicacionesTextosCondicionesAquitar.Remove(asignacionesImplicacionesTextosCondicionesAquitar.First());
            }

            var asignacionesImplicacionesTextosDefinicionesAquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosDefinicionesAquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosDefinicionesAquitar.First());
                asignacionesImplicacionesTextosDefinicionesAquitar.Remove(asignacionesImplicacionesTextosDefinicionesAquitar.First());
            }

            var asignacionesImplicacionesTextosCondiciones2Aquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Any(item => item.Entrada.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosCondiciones2Aquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosCondiciones2Aquitar.First());
                asignacionesImplicacionesTextosCondiciones2Aquitar.Remove(asignacionesImplicacionesTextosCondiciones2Aquitar.First());
            }

            asignacionesImplicacionesTextosCondiciones2Aquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Any(item => item.Entrada.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosCondiciones2Aquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosCondiciones2Aquitar.First());
                asignacionesImplicacionesTextosCondiciones2Aquitar.Remove(asignacionesImplicacionesTextosCondiciones2Aquitar.First());
            }

            var asignacionesImplicacionesTextosDefiniciones2Aquitar = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones.Any(item => item.Entrada.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosDefiniciones2Aquitar.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosDefiniciones2Aquitar.First());
                asignacionesImplicacionesTextosDefiniciones2Aquitar.Remove(asignacionesImplicacionesTextosDefiniciones2Aquitar.First());
            }

            var relacionesAsociadasEntradaCondicion = (from C in definicion.Relaciones_TextosInformacion where C.VerificarEntradaEn_Condiciones(entrada) select C).ToList();

            foreach (var relacionImplicacion in relacionesAsociadasEntradaCondicion)
            {
                relacionImplicacion.QuitarEntradaCondiciones(entrada);
            }
        }

        private void btnAgregarEntradaTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            Entrada nueva = new Entrada();
            nueva.ID = App.GenerarID_Elemento();
            nueva.CalculoDiseñoAsociado = CalculoSeleccionado.ObtenerCalculoEntradas();
            nueva.CalculoDiseñoAsociado.ListaEntradas.Add(nueva);

            nueva.CalculoDiseñoAsociado.AgregarEntrada_CalculoEntradas(nueva);
            nueva.EjecutarDeFormaGeneral = true;

            nueva.Nombre = "Variable o vector de entrada " + nueva.CalculoDiseñoAsociado.ListaEntradas.Count;
            nueva.Tipo = TipoEntrada.TextosInformacion;
            Ventana.ActualizarElementosEntradas(nueva, true, CalculoSeleccionado.ObtenerCalculoEntradas());

            ListarEntradas();
        }

        private void ajustarTamañoPizarra_Click(object sender, RoutedEventArgs e)
        {
            if (diagrama.Children.Count > 0)
            {
                UIElement[] controles = new UIElement[diagrama.Children.Count];
                diagrama.Children.CopyTo(controles, 0);

                double altura = controles.ToList().Select(e => Canvas.GetTop(e) + e.RenderSize.Height).Max();
                double anchura = controles.ToList().Select(e => Canvas.GetLeft(e) + e.RenderSize.Width).Max();

                diagrama.Height = altura + 50;
                diagrama.Width = anchura + 50;
                
                CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;
                CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;  
            }
        }

        private void btnAgregarListaCadenasTexto_Click(object sender, RoutedEventArgs e)
        {
            DefinicionListaCadenasTexto nuevaDefinicion = new DefinicionListaCadenasTexto();
            nuevaDefinicion.VistaTextos = this;
            nuevaDefinicion.EnDiagrama = true;
            nuevaDefinicion.botonFondo.BorderBrush = Brushes.Black;
            nuevaDefinicion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaDefinicion.SizeChanged += CambioTamañoDefinicion_ListaCadenas;

            DiseñoListaCadenasTexto nuevoElementoTextoInformacion = new DiseñoListaCadenasTexto();
            nuevoElementoTextoInformacion.Tipo = TipoElementoOperacion.Definicion_ListaCadenasTexto;
            nuevoElementoTextoInformacion.PosicionX = App.PosicionXPredeterminada;
            nuevoElementoTextoInformacion.PosicionY = App.PosicionYPredeterminada;
            nuevoElementoTextoInformacion.CalculoRelacionado = CalculoDiseñoSeleccionado_Cantidades;

            var ultimoNombre = (from DiseñoTextosInformacion E in CalculoDiseñoSeleccionado.ElementosTextosInformacion where 
                                E.GetType() == typeof(DiseñoListaCadenasTexto) &&
                                E.Nombre != null && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null)
            {
                int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            }
            cantidadElementosTipo++;

            nuevoElementoTextoInformacion.Nombre = "Definición de lista de cadenas de texto " + cantidadElementosTipo.ToString();

            CalculoDiseñoSeleccionado.ElementosTextosInformacion.Add(nuevoElementoTextoInformacion);
            nuevaDefinicion.DiseñoListaCadenasTexto = nuevoElementoTextoInformacion;

            diagrama.Children.Add(nuevaDefinicion);

            Canvas.SetTop(nuevaDefinicion, App.PosicionYPredeterminada);
            Canvas.SetLeft(nuevaDefinicion, App.PosicionXPredeterminada);

            EstablecerIndicesProfundidadElementos();
        }

        public void ListarCalculos()
        {
            calculos.Children.Clear();
            calculos.RowDefinitions.Clear();
            calculos.ColumnDefinitions.Clear();

            ColumnDefinition definicionColumna = new ColumnDefinition();
            definicionColumna.Width = new GridLength(1, GridUnitType.Star);

            calculos.ColumnDefinitions.Add(definicionColumna);

            for (int cantidadFilas = 1; cantidadFilas <= CalculoSeleccionado.Calculos.Count((i) => !i.EsEntradasArchivo); cantidadFilas++)
            {
                RowDefinition definicionFila = new RowDefinition();
                definicionFila.Height = GridLength.Auto;

                calculos.RowDefinitions.Add(definicionFila);
            }

            int indiceFila = 0;

            foreach (var itemCalculo in CalculoSeleccionado.Calculos)
            {
                if (itemCalculo.EsEntradasArchivo) continue;

                CalculoEspecifico nuevoCalculo = new CalculoEspecifico();
                nuevoCalculo.Margin = new Thickness(10);
                nuevoCalculo.CalculoDiseño = itemCalculo;
                nuevoCalculo.VistaTextosInformacion = this;
                nuevoCalculo.botonFondo.Click += SeleccionarCalculo;
                calculos.Children.Add(nuevoCalculo);

                Grid.SetRow(nuevoCalculo, indiceFila);
                indiceFila++;
            }
        }

        public void SeleccionarCalculo(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(CalculoEspecifico))
                CalculoDiseñoSeleccionado_Cantidades = ((CalculoEspecifico)((Control)sender).Parent).CalculoDiseño;

            CalculoSeleccionado.SubCalculoSeleccionado_TextosInformacion = CalculoDiseñoSeleccionado_Cantidades;
            CargarDatosCalculoSeleccionado();

            if (CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementoSeleccionado != null)
            {
                SeleccionandoElemento = true;
                try
                {
                    foreach (var itemElemento in diagrama.Children)
                    {
                        if (CalculoDiseñoSeleccionado_Cantidades.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                        {
                            if (itemElemento.GetType() == typeof(EntradaDiseñoOperaciones) &&
                                ((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion == CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementoSeleccionado)
                            //((EntradaDiseñoOperaciones)itemElemento).Button_PreviewMouseLeftButtonDown(this, CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.e_SeleccionarElemento);
                            {
                                CalculoDiseñoSeleccionado_Cantidades.Seleccion.EntradaSeleccionada = (EntradaDiseñoOperaciones)itemElemento;
                                //Action accion = new Action();
                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(CalculoDiseñoSeleccionado_Cantidades.Seleccion.EntradaSeleccionada.Clic, DispatcherPriority.Loaded);
                                    //if (((EntradaDiseñoOperaciones)itemElemento).EnDiagrama && ((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion != null) EstablecerTextoBotonSalida(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion.ContieneSalida);
                                    //MostrarOrdenOperando_Elemento(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion);
                                }
                            }
                        }
                        if (CalculoDiseñoSeleccionado_Cantidades.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota)
                        {
                            if (itemElemento.GetType() == typeof(NotaDiagrama) &&
                                ((NotaDiagrama)itemElemento).DiseñoOperacion == CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementoSeleccionado)
                            //((EntradaDiseñoOperaciones)itemElemento).Button_PreviewMouseLeftButtonDown(this, CalculoDiseñoSeleccionado_Cantidades.Seleccion.e_SeleccionarElemento);
                            {
                                CalculoDiseñoSeleccionado_Cantidades.Seleccion.NotaSeleccionada = (NotaDiagrama)itemElemento;
                                //Action accion = new Action();
                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(CalculoDiseñoSeleccionado_Cantidades.Seleccion.NotaSeleccionada.Clic, DispatcherPriority.Loaded);
                                    //if (((EntradaDiseñoOperaciones)itemElemento).EnDiagrama && ((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion != null) EstablecerTextoBotonSalida(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion.ContieneSalida);
                                    //MostrarOrdenOperando_Elemento(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion);
                                }
                            }
                        }
                        else
                        {
                            if (itemElemento.GetType() == typeof(OperacionEspecifica) &&
                                ((OperacionEspecifica)itemElemento).DiseñoOperacion == CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementoSeleccionado)
                            //((OperacionEspecifica)itemElemento).Button_PreviewMouseLeftButtonDown(this, CalculoDiseñoSeleccionado_Cantidades.Seleccion.e_SeleccionarElemento);
                            {
                                CalculoDiseñoSeleccionado_Cantidades.Seleccion.OperacionSeleccionada = (OperacionEspecifica)itemElemento;

                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(CalculoDiseñoSeleccionado_Cantidades.Seleccion.OperacionSeleccionada.Clic, DispatcherPriority.Loaded);
                                    //if (((OperacionEspecifica)itemElemento).EnDiagrama && ((OperacionEspecifica)itemElemento).DiseñoOperacion != null) EstablecerTextoBotonSalida(((OperacionEspecifica)itemElemento).DiseñoOperacion.ContieneSalida);
                                    //MostrarOrdenOperando_Elemento(((OperacionEspecifica)itemElemento).DiseñoOperacion);
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
                SeleccionandoElemento = false;
            }

            zoom.Text = CalculoDiseñoSeleccionado_Cantidades.Seleccion.CantidadZoom.ToString();

            textoBusquedaDiagrama.Text = CalculoDiseñoSeleccionado_Cantidades.Seleccion.TextoBusquedaDiagrama;

            if (!string.IsNullOrEmpty(textoBusquedaDiagrama.Text))
            {
                List<UIElement> elementos = BuscarElementosDiagramas(textoBusquedaDiagrama.Text);

                CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementosEncontrados.Clear();
                if (elementos != null && elementos.Any())
                {
                    CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementosEncontrados.AddRange(elementos);
                }
            }

            if (CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementosEncontrados.Count > 1)
            {
                resultadosBusquedas.Visibility = Visibility.Visible;
            }
            else
                resultadosBusquedas.Visibility = Visibility.Collapsed;

            foreach (var itemElemento in CalculoDiseñoSeleccionado_Cantidades.Seleccion.ElementosSeleccionados)
            {
                foreach (var item in diagrama.Children)
                {
                    if (item.GetType() == typeof(ArrowLine)) continue;

                    if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                    {
                        if (((EntradaDiseñoOperaciones)item).DiseñoOperacion == itemElemento)
                        {
                            //Ventana.Dispatcher.Invoke(.Clic, DispatcherPriority.Loaded);
                            if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                            {
                                Ventana.Dispatcher.Invoke(() =>
                                {
                                    ((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightBrush;
                                    ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightBrush;
                                }, DispatcherPriority.Loaded);
                            }
                        }
                    }
                    else if (item.GetType() == typeof(OperacionEspecifica))
                    {
                        if (((OperacionEspecifica)item).DiseñoOperacion == itemElemento)
                        {
                            if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                            {
                                Ventana.Dispatcher.Invoke(() =>
                                {
                                    ((OperacionEspecifica)item).Background = SystemColors.HighlightBrush;
                                    ((OperacionEspecifica)item).botonFondo.Background = SystemColors.HighlightBrush;
                                }, DispatcherPriority.Loaded);
                            }
                        }
                    }
                }
            }
        }

        private void CargarDatosCalculoSeleccionado()
        {
            ListarEntradas();

            entradas.Width = contenedorEntradas.ActualWidth;
            
            if (double.IsNaN(CalculoDiseñoSeleccionado_Cantidades.Ancho) &
                double.IsNaN(CalculoDiseñoSeleccionado_Cantidades.Alto))
            {
                diagrama.Width = contenedor.ActualWidth;
                CalculoDiseñoSeleccionado_Cantidades.Ancho = diagrama.Width;

                diagrama.Height = contenedor.ActualHeight;
                CalculoDiseñoSeleccionado_Cantidades.Alto = diagrama.Height;

            }
            else
            {
                diagrama.Width = CalculoDiseñoSeleccionado_Cantidades.Ancho;
                diagrama.Height = CalculoDiseñoSeleccionado_Cantidades.Alto;
            }
            
            DibujarElementosTextosInformacion(CalculoDiseñoSeleccionado_Cantidades);
        }

        public void MarcarCalculoSeleccionado()
        {
            foreach (var itemCalculo in calculos.Children)
            {
                ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            }

            if (CalculoSeleccionado.SubCalculoSeleccionado_TextosInformacion != null)
            {
                foreach (var itemCalculo in calculos.Children)
                {
                    if (((CalculoEspecifico)itemCalculo).CalculoDiseño == CalculoSeleccionado.SubCalculoSeleccionado_TextosInformacion)
                    {
                        ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.InactiveBorderBrush;
                    }
                }
            }
        }
    }
}
