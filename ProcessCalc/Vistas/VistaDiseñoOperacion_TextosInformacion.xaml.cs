using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Notas;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Definiciones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaDiseñoOperacion_TextosInformacion.xaml
    /// </summary>
    public partial class VistaDiseñoOperacion_TextosInformacion : UserControl
    {
        public DiseñoTextosInformacion Definicion { get; set; }
        public DiseñoElementoOperacion ElementoSeleccionado { get; set; }
        public TipoElementoDiseñoOperacion TipoElementoDiseñoOperacionSeleccionado { get; set; }
        public OpcionOperacion OpcionOperacionSeleccionado { get; set; }
        double escalaZoom = 1;
        List<UIElement> ElementosEncontrados = new List<UIElement>();
        int indiceElementosEncontrados = -1;
        public MouseButtonEventArgs e_SeleccionarElemento { get; set; }
        public MainWindow Ventana { get; set; }
        public DiseñoTextosInformacion Definicion_TextosInformacion { get; set; }
        public Calculo Calculo { get; set; }
        public VistaDiseñoOperacion_TextosInformacion()
        {
            InitializeComponent();
        }

        public void DibujarElementosDiseñoOperacion()
        {
            diagrama.Children.Clear();
            
            foreach (var itemElemento in Definicion.OperacionRelacionada.ElementosDiseñoOperacion)
            {
                if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Entrada)
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                    //nuevaEntrada.VistaOperacion = this;
                    nuevaEntrada.Bloqueada = true;
                    //nuevaEntrada.Modo_TextosInformacion = true;
                    //nuevaEntrada.DesdeDiagramaOperacion = true;
                    //nuevaEntrada.EnDiagramaOperacion = true;
                    nuevaEntrada.EnDiagramaTextos = true;
                    nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
                    nuevaEntrada.DiseñoElementoOperacion = itemElemento;
                    nuevaEntrada.Entrada = itemElemento.EntradaRelacionada;                    

                    if (ElementoSeleccionado != null &&
                        ElementoSeleccionado == itemElemento)
                    {
                        nuevaEntrada.Background = SystemColors.HighlightBrush;
                        nuevaEntrada.botonFondo.Background = SystemColors.HighlightTextBrush;
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
                else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion)
                {
                    EntradaFlujoOperacion nuevoFlujo = new EntradaFlujoOperacion();
                    //nuevoFlujo.VistaOperacion = this;
                    //nuevoFlujo.Modo_TextosInformacion = true;
                    //nuevoFlujo.EnDiagrama = true;
                    nuevoFlujo.Bloqueado = true;
                    nuevoFlujo.botonFondo.BorderBrush = Brushes.Black;
                    nuevoFlujo.DiseñoOperacion = itemElemento.ElementoDiseñoRelacionado;
                    nuevoFlujo.DiseñoElementoOperacion = itemElemento;

                    if (ElementoSeleccionado != null &&
                        ElementoSeleccionado == itemElemento)
                    {
                        nuevoFlujo.Background = SystemColors.HighlightTextBrush;
                        nuevoFlujo.botonFondo.Background = SystemColors.HighlightTextBrush;
                    }
                    else
                    {
                        nuevoFlujo.Background = SystemColors.GradientInactiveCaptionBrush;
                        nuevoFlujo.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                    }

                    diagrama.Children.Add(nuevoFlujo);

                    Canvas.SetTop(nuevoFlujo, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevoFlujo, itemElemento.PosicionX);
                }
                else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion)
                {
                    OpcionOperacion nuevaOpcion = new OpcionOperacion();
                    nuevaOpcion.VistaOperacion_TextosInformacion = this;
                    nuevaOpcion.Modo_TextosInformacion = true;
                    nuevaOpcion.EnDiagrama = true;
                    nuevaOpcion.botonFondo.BorderBrush = Brushes.Black;

                    if (ElementoSeleccionado != null &&
                        ElementoSeleccionado == itemElemento)
                    {
                        nuevaOpcion.Background = SystemColors.HighlightTextBrush;
                        nuevaOpcion.botonFondo.Background = SystemColors.HighlightTextBrush;
                    }
                    else
                    {
                        nuevaOpcion.Background = SystemColors.GradientInactiveCaptionBrush;
                        nuevaOpcion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                    }

                    string nombreOperacion = string.Empty;
                    if (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir |
                        itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                        itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados)
                    {
                        ; nombreOperacion = "Lógica de selección de  números";
                    }
                    else if (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                        itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
                    {
                        nombreOperacion = "Lógica de selección de variables o vectores";
                    }
                    else
                    {
                        switch (Definicion.OperacionRelacionada.Tipo)
                        {
                            case TipoElementoOperacion.Suma:
                                nombreOperacion = "Sumar";
                                break;
                            case TipoElementoOperacion.Resta:
                                nombreOperacion = "Restar";
                                break;
                            case TipoElementoOperacion.Multiplicacion:
                                nombreOperacion = "Multiplicar";
                                break;
                            case TipoElementoOperacion.Division:
                                nombreOperacion = "Dividir";
                                break;
                            case TipoElementoOperacion.Porcentaje:
                                nombreOperacion = "Calcular porcentaje";
                                break;
                            case TipoElementoOperacion.Potencia:
                                nombreOperacion = "Calcular potencia";
                                break;
                            case TipoElementoOperacion.Raiz:
                                nombreOperacion = "Calcular raíz";
                                break;
                            case TipoElementoOperacion.Logaritmo:
                                nombreOperacion = "Calcular logaritmo";
                                break;
                            case TipoElementoOperacion.Inverso:
                                nombreOperacion = "Calcular el inverso";
                                break;
                            case TipoElementoOperacion.Factorial:
                                nombreOperacion = "Calcular el factorial";
                                break;
                            case TipoElementoOperacion.ContarCantidades:
                                nombreOperacion = "Contar números";
                                break;
                            case TipoElementoOperacion.SeleccionarOrdenar:
                                nombreOperacion = "Lógica de selección de números";
                                break;
                            case TipoElementoOperacion.CondicionesFlujo:
                                nombreOperacion = "Lógica de selección de variables o vectores";
                                break;
                            case TipoElementoOperacion.RedondearCantidades:
                                nombreOperacion = "Redondear números";
                                break;
                        }
                    }

                    nuevaOpcion.NombreOperacion = nombreOperacion;
                    nuevaOpcion.Tipo = itemElemento.TipoOpcionOperacion;
                    nuevaOpcion.DiseñoElementoOperacion = itemElemento;

                    diagrama.Children.Add(nuevaOpcion);

                    Canvas.SetTop(nuevaOpcion, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevaOpcion, itemElemento.PosicionX);
                }
                else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Salida)
                {
                    OpcionSalida nuevaOpcion = new OpcionSalida();
                    //nuevaOpcion.VistaOpciones = this;
                    //nuevaOpcion.Modo_TextosInformacion = true;
                    //nuevaOpcion.EnDiagrama = true;
                    nuevaOpcion.Bloqueada = true;
                    nuevaOpcion.botonFondo.BorderBrush = Brushes.Black;

                    if (ElementoSeleccionado != null &&
                        ElementoSeleccionado == itemElemento)
                    {
                        nuevaOpcion.Background = SystemColors.HighlightTextBrush;
                        nuevaOpcion.botonFondo.Background = SystemColors.HighlightTextBrush;
                    }
                    else
                    {
                        nuevaOpcion.Background = SystemColors.GradientInactiveCaptionBrush;
                        nuevaOpcion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                    }

                    nuevaOpcion.NombreOperacion = itemElemento.Nombre;
                    nuevaOpcion.nombreOpcion.Text = nuevaOpcion.NombreOperacion;
                    nuevaOpcion.DiseñoElementoOperacion = itemElemento;

                    diagrama.Children.Add(nuevaOpcion);

                    Canvas.SetTop(nuevaOpcion, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevaOpcion, itemElemento.PosicionX);
                }
                else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Nota)
                {
                    NotaDiagrama nuevaNota = new NotaDiagrama();
                    //nuevaNota.VistaOpciones = this;
                    //nuevaNota.Modo_TextosInformacion = true;
                    //nuevaNota.EnDiagrama = true;
                    nuevaNota.Bloqueada = true;

                    if (ElementoSeleccionado != null &&
                        ElementoSeleccionado == itemElemento)
                    {
                        nuevaNota.fondo.BorderThickness = new Thickness(1);
                    }
                    else
                    {
                        nuevaNota.fondo.BorderThickness = new Thickness(0);
                    }

                    string nombreOperacion = string.Empty;
                    switch (Definicion.OperacionRelacionada.Tipo)
                    {
                        case TipoElementoOperacion.Nota:
                            nombreOperacion = "Nota";
                            break;
                    }

                    nuevaNota.TipoOpcion = itemElemento.TipoOpcionOperacion;
                    nuevaNota.DiseñoElementoOperacion = itemElemento;

                    diagrama.Children.Add(nuevaNota);

                    Canvas.SetTop(nuevaNota, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevaNota, itemElemento.PosicionX);
                }

                DibujarLineasElemento(itemElemento);
            }

            EstablecerIndicesProfundidadElementos();
        }

        public void DibujarLineasElemento(DiseñoElementoOperacion item)
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

                    //nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                    if (lineaEncontrada == null)
                    {
                        diagrama.Children.Add(nuevaLinea);

                        //if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                        //    lineaSeleccionada = nuevaLinea;
                    }
                    //else
                    //{
                    //    if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                    //        lineaSeleccionada = (ArrowLine)lineaEncontrada;
                    //}
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

                    //nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                    if (lineaEncontrada == null)
                    {
                        diagrama.Children.Add(nuevaLinea);

                        //if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                        //    lineaSeleccionada = nuevaLinea;
                    }
                    else
                    {
                        //if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                        //    lineaSeleccionada = (ArrowLine)lineaEncontrada;
                    }
                }

            }
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

        public void DibujarTodasLineasElementos()
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var item in lineas)
                diagrama.Children.Remove(item);

            foreach (var itemElemento in Definicion.OperacionRelacionada.ElementosDiseñoOperacion)
            {
                DibujarLineasElemento(itemElemento);
            }
        }

        private ArrowLine BuscarLinea(DiseñoElementoOperacion elementoOrigen, DiseñoElementoOperacion elementoDestino)
        {
            foreach (var item in Definicion.OperacionRelacionada.ElementosDiseñoOperacion)
            {
                var elementoDestinoEncontrado = (from DiseñoElementoOperacion E in item.ElementosAnteriores where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY select E).FirstOrDefault();
                var elementoOrigenEncontrado = (from DiseñoElementoOperacion E in item.ElementosPosteriores where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY select E).FirstOrDefault();

                var elementoDestino2Encontrado = (from DiseñoElementoOperacion E in item.ElementosPosteriores where E == elementoOrigen && item == elementoDestino select E).FirstOrDefault();
                var elementoOrigen2Encontrado = (from DiseñoElementoOperacion E in item.ElementosAnteriores where E == elementoDestino && item == elementoOrigen select E).FirstOrDefault();

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
                ref double posicionXDestino, ref double posicionYDestino, DiseñoElementoOperacion elementoOrigen,
                DiseñoElementoOperacion elementoDestino)
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

        public void DestacarElementoSeleccionado()
        {
            //ElementosSeleccionados.Clear();

            foreach (var item in diagrama.Children)
            {
                //if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                //{
                //    ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                //    ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                //}
                if (item.GetType() == typeof(OpcionOperacion))
                {
                    ((OpcionOperacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((OpcionOperacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                //else if (item.GetType() == typeof(OpcionSalida))
                //{
                //    ((OpcionSalida)item).Background = SystemColors.GradientInactiveCaptionBrush;
                //    ((OpcionSalida)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                //}
                //else if (item.GetType() == typeof(EntradaFlujoOperacion))
                //{
                //    ((EntradaFlujoOperacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                //    ((EntradaFlujoOperacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                //}
                //else if (item.GetType() == typeof(NotaDiagrama))
                //{
                //    ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                //}
                //else if (item.GetType() == typeof(Line))
                //    ((Line)item).Stroke = Brushes.Black;
            }

            //if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Entrada)
            //{
            //    if (EntradaSeleccionada.EnDiagramaOperacion)
            //    {
            //        EntradaSeleccionada.Background = SystemColors.HighlightBrush;
            //        EntradaSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
            //    }
            //}
            //else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.FlujoOperacion)
            //{
            //    if (FlujoOperacionSeleccionado.EnDiagrama)
            //    {
            //        FlujoOperacionSeleccionado.Background = SystemColors.HighlightBrush;
            //        FlujoOperacionSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
            //    }
            //}
            if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.OpcionOperacion)
            {
                if (OpcionOperacionSeleccionado.EnDiagrama)
                {
                    OpcionOperacionSeleccionado.Background = SystemColors.HighlightBrush;
                    OpcionOperacionSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
                }
            }
            //else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Salida)
            //{
            //    if (OpcionSalidaSeleccionado.EnDiagrama)
            //    {
            //        OpcionSalidaSeleccionado.Background = SystemColors.HighlightBrush;
            //        OpcionSalidaSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
            //    }
            //}
            //else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Nota)
            //{
            //    if (NotaSeleccionada.EnDiagrama)
            //    {
            //        NotaSeleccionada.fondo.BorderThickness = new Thickness(1);
            //    }
            //}
            //else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Linea)
            //    lineaSeleccionada.Stroke = SystemColors.HighlightBrush;
        }

        private void diagrama_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Linea &&
            //lineaSeleccionada != null) return;

            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Ninguno;
            //EntradaSeleccionada = null;
            //FlujoOperacionSeleccionado = null;
            OpcionOperacionSeleccionado = null;
            //NotaSeleccionada = null;
            //OpcionSalidaSeleccionado = null;
            //lineaSeleccionada = null;
            ElementoSeleccionado = null;
            //ElementosDiagramaSeleccionados.Clear();
            //ElementosSeleccionados.Clear();

            //ElementoSeleccionado_Bool = false;
            //ClicDiagrama = true;

            //MostrarOrdenOperando_Elemento(null);
            //MostrarInfo_Elemento(null);
            //MostrarAcumulacion();
            //MostrarOcultarOpciones_RecalculoPotencias(null);
            DestacarElementoSeleccionado();
            DibujarTodasLineasElementos();
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
            //Operacion.AltoDiagrama = diagrama.Height;
            //Operacion.AnchoDiagrama = diagrama.Width;
        }

        private void disminuirArea_Click(object sender, RoutedEventArgs e)
        {
            if (diagrama.ActualHeight - 300 > 0 &
                diagrama.ActualWidth - 300 > 0)
            {
                diagrama.Height = diagrama.ActualHeight - 300;
                diagrama.Width = diagrama.ActualWidth - 300;
                //Operacion.AltoDiagrama = diagrama.Height;
                //Operacion.AnchoDiagrama = diagrama.Width;
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

                //if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                //    Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)elemento).Clic);

                //if (elemento.GetType() == typeof(EntradaFlujoOperacion))
                //    Ventana.Dispatcher.Invoke(((EntradaFlujoOperacion)elemento).Clic);

                if (elemento.GetType() == typeof(OpcionOperacion))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((OpcionOperacion)elemento).Clic);
                    }
                }

                //if (elemento.GetType() == typeof(NotaDiagrama))
                //    Ventana.Dispatcher.Invoke(((NotaDiagrama)elemento).Clic);

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
                //if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                //{
                //    EntradaDiseñoOperaciones entrada = (EntradaDiseñoOperaciones)item;

                //    if (!string.IsNullOrEmpty(textoBusqueda))
                //    {
                //        if (entrada.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                //            entrada.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //            elementos.Add(entrada);
                //    }
                //}
                //else if (item.GetType() == typeof(EntradaFlujoOperacion))
                //{
                //    EntradaFlujoOperacion operacion = (EntradaFlujoOperacion)item;

                //    if (!string.IsNullOrEmpty(textoBusqueda))
                //    {
                //        if (operacion.nombreEntradaFlujo.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //            elementos.Add(operacion);
                //    }
                //}
                if (item.GetType() == typeof(OpcionOperacion))
                {
                    OpcionOperacion operacion = (OpcionOperacion)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (operacion.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                            operacion.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(operacion);
                    }
                }
                //else if (item.GetType() == typeof(NotaDiagrama))
                //{
                //    NotaDiagrama operacion = (NotaDiagrama)item;

                //    if (!string.IsNullOrEmpty(textoBusqueda))
                //    {
                //        if (operacion.textoNota.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //            elementos.Add(operacion);
                //    }
                //}
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

        public void btnDefinirRelaciones_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                DiseñoTextosInformacion definicionSeleccionada = (from D in Definicion.Definiciones_TextosInformacion where D.ElementoRelacionado == ElementoSeleccionado select D).FirstOrDefault();

                if (definicionSeleccionada == null)
                {
                    definicionSeleccionada = new DiseñoTextosInformacion();
                    definicionSeleccionada.ElementoRelacionado = ElementoSeleccionado;
                    Definicion.Definiciones_TextosInformacion.Add(definicionSeleccionada);
                }

                DefinirRelaciones_TextosInformacion definir = new DefinirRelaciones_TextosInformacion();
                definir.listaTextos.ImplicacionesTextosInformacion = CopiarImplicaciones(definicionSeleccionada.Relaciones_TextosInformacion);
                definir.listaTextos.Entradas = Definicion.ObtenerEntradas();
                definir.listaTextos.Definiciones = Definicion.ObtenerDefiniciones();
                definir.listaTextos.DefinicionesListas = Definicion.ObtenerDefinicionesListas();
                definir.Elementos = Definicion.CalculoRelacionado.ElementosOperaciones;
                definir.Operandos = Definicion.OperacionRelacionada.ElementosAnteriores.ToList();
                definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                definir.OperacionRelacionada_Definicion = Definicion.OperacionRelacionada;
                definir.listaTextos.ModoOperacion = true;

                if (definir.ShowDialog() == true)
                {
                    definicionSeleccionada.Relaciones_TextosInformacion = definir.listaTextos.ImplicacionesTextosInformacion;
                    Definicion.OperacionRelacionada.IncluirAsignacionDentroDefinicionNormal = (bool)definir.listaTextos.opcionIncluirAsignacionDentroDefinicionNormal.IsChecked;
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

        private void btnQuitarRelaciones_TextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                MessageBoxResult resp = MessageBox.Show("¿Quitar la definición relacionada a la variable o vector seleccionado, de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resp == MessageBoxResult.Yes)
                {
                    DiseñoTextosInformacion definicionSeleccionada = (from D in Definicion.Definiciones_TextosInformacion where D.ElementoRelacionado == ElementoSeleccionado select D).FirstOrDefault();

                    if (definicionSeleccionada != null)
                    {
                        Definicion.Definiciones_TextosInformacion.Remove(definicionSeleccionada);
                    }
                }
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            if (double.IsNaN(Definicion.OperacionRelacionada.AnchoDiagrama) &
                    double.IsNaN(Definicion.OperacionRelacionada.AltoDiagrama))
            {
                diagrama.Width = contenedor.ActualWidth;
                //Operacion.AnchoDiagrama = diagrama.Width;

                diagrama.Height = contenedor.ActualHeight;
                //Operacion.AltoDiagrama = diagrama.Height;

            }
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
                //Operacion.AltoDiagrama = diagrama.Height;
                //Operacion.AnchoDiagrama = diagrama.Width;
            }
        }

        public List<UIElement> AgregarImplicacionElementoDiseño_DesdeOperaciones(DiseñoElementoOperacion elemento)
        {
            UIElement[] elementos = new UIElement[diagrama.Children.Count];
            diagrama.Children.CopyTo(elementos, 0);

            var definiciones = (from d in elementos where (d.GetType() == typeof(EntradaDiseñoOperaciones) && ((EntradaDiseñoOperaciones)d).DiseñoElementoOperacion == elemento) ||
                               (d.GetType() == typeof(EntradaFlujoOperacion) && ((EntradaFlujoOperacion)d).DiseñoElementoOperacion == elemento) ||
                                (d.GetType() == typeof(OpcionOperacion) && ((OpcionOperacion)d).DiseñoElementoOperacion == elemento)
                                select (UIElement)d).ToList();

            return definiciones;
        }
    }
}
