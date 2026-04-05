using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Entidades;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaInformeResultados.xaml
    /// </summary>
    public partial class VistaInformeResultados : UserControl
    {
        public EjecucionCalculo Ejecucion { get; set; }
        public MainWindow Ventana { get; set; }
        public DiseñoCalculo SubCalculoSeleccionado_VistaInformeResultados { get; set; }
        public DiseñoOperacion AgrupadorSeleccionado;
        public DiseñoOperacion AgrupadorAnterior;
        public VistaInformeResultados()
        {
            InitializeComponent();
        }

        public void DibujarCalculos()
        {
            diagramaCalculos.Children.Clear();

            //rectanguloCalculo.nombreElemento.Text = "Cálculo";
            //rectanguloCalculo.tipoElemento.Text = string.Empty;
            //rectanguloCalculo.avanceConjuntoNumeros.Visibility = Visibility.Collapsed;
            //rectanguloCalculo.tipoElemento.Visibility = Visibility.Collapsed;
            //rectanguloCalculo.Background = SystemColors.GradientInactiveCaptionBrush;
            //rectanguloCalculo.iconoCalculo.Visibility = Visibility.Visible;

            //diagramaCalculos.Children.Add(rectanguloCalculo);

            //Canvas.SetTop(rectanguloCalculo, diagramaCalculos.ActualHeight / 2);
            //Canvas.SetLeft(rectanguloCalculo, diagramaCalculos.ActualWidth / 2);

            //EstablecerIndicesProfundidadElementosCalculos();

            foreach (var itemElemento in Ejecucion.Calculo.Calculos)
            {
                if (itemElemento.EsEntradasArchivo) continue;

                ElementoDiagramaEjecucion nuevoCalculo = new ElementoDiagramaEjecucion();
                nuevoCalculo.Ventana = this;
                nuevoCalculo.ElementoCalculoRelacionado = itemElemento;
                nuevoCalculo.avanceConjuntoNumeros.Visibility = Visibility.Collapsed;
                nuevoCalculo.Background = SystemColors.GradientInactiveCaptionBrush;
                //nuevoCalculo.SizeChanged += CambioTamañoCalculo;

                diagramaCalculos.Children.Add(nuevoCalculo);

                Canvas.SetTop(nuevoCalculo, itemElemento.PosicionY);
                Canvas.SetLeft(nuevoCalculo, itemElemento.PosicionX);

                //itemElemento.AnchuraElemento = nuevoCalculo.ActualWidth;
                //itemElemento.AlturaElemento = nuevoCalculo.ActualHeight;

                //DibujarLineasElemento_Calculo(itemElemento);
            }

            QuitarTodasLineas_Calculo();

            foreach (var itemElemento in Ejecucion.Calculo.Calculos)
            {
                if (itemElemento.EsEntradasArchivo) continue;
                DibujarLineasElemento_Calculo(itemElemento);
            }

            EstablecerIndicesProfundidadElementos_Calculos();
        }

        public void DibujarElementosOperaciones()
        {
            diagrama.Children.Clear();

            foreach (var itemElemento in SubCalculoSeleccionado_VistaInformeResultados.ElementosOperaciones)
            {
                if (itemElemento.Tipo == TipoElementoOperacion.Salida) continue;

                if (itemElemento.AgrupadorContenedor == null)
                {
                    ElementoDiagramaEjecucion nuevaEntrada = new ElementoDiagramaEjecucion();

                    if (itemElemento.Tipo == TipoElementoOperacion.Entrada)
                    {
                        if (itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                                itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                            nuevaEntrada.EsEntrada = ((from C in Ejecucion.Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                                ? true : false;
                    }

                    nuevaEntrada.Ventana = this;
                    nuevaEntrada.ElementoRelacionado = itemElemento;
                    nuevaEntrada.avanceConjuntoNumeros.Visibility = Visibility.Collapsed;
                    nuevaEntrada.Background = SystemColors.GradientInactiveCaptionBrush;

                    diagrama.Children.Add(nuevaEntrada);

                    Canvas.SetTop(nuevaEntrada, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevaEntrada, itemElemento.PosicionX);

                    //DibujarLineasElemento(itemElemento);
                }
            }

            QuitarTodasLineas();
            foreach (var itemElemento in SubCalculoSeleccionado_VistaInformeResultados.ElementosOperaciones)
            {
                if (itemElemento.Tipo == TipoElementoOperacion.Salida) continue;

                if (itemElemento.AgrupadorContenedor == null)
                {

                    DibujarLineasElemento(itemElemento);
                }
            }

            EstablecerIndicesProfundidadElementos();
        }

        private void QuitarTodasLineas()
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var itemLinea in lineas)
                diagrama.Children.Remove(itemLinea);
        }

        private void QuitarTodasLineas_Calculo()
        {
            List<UIElement> lineas = (from UIElement L in diagramaCalculos.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var itemLinea in lineas)
                diagramaCalculos.Children.Remove(itemLinea);
        }

        public void DibujarElementosOperaciones_Agrupador()
        {
            diagrama.Children.Clear();

            foreach (var itemElemento in AgrupadorSeleccionado.ElementosAgrupados)
            {
                if (itemElemento.Tipo == TipoElementoOperacion.Salida) continue;
                                
                ElementoDiagramaEjecucion nuevaEntrada = new ElementoDiagramaEjecucion();

                if (itemElemento.Tipo == TipoElementoOperacion.Entrada)
                {
                    if (itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        nuevaEntrada.EsEntrada = ((from C in Ejecucion.Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                            ? true : false;
                }

                nuevaEntrada.Ventana = this;
                nuevaEntrada.ElementoRelacionado = itemElemento;
                nuevaEntrada.avanceConjuntoNumeros.Visibility = Visibility.Collapsed;
                nuevaEntrada.Background = SystemColors.GradientInactiveCaptionBrush;

                diagrama.Children.Add(nuevaEntrada);

                Canvas.SetTop(nuevaEntrada, itemElemento.PosicionY);
                Canvas.SetLeft(nuevaEntrada, itemElemento.PosicionX);

                //DibujarLineasElemento_Agrupador(AgrupadorSeleccionado, itemElemento);                
            }

            QuitarTodasLineas();

            foreach (var itemElemento in AgrupadorSeleccionado.ElementosAgrupados)
            {
                if (itemElemento.Tipo == TipoElementoOperacion.Salida) continue;
                DibujarLineasElemento_Agrupador(AgrupadorSeleccionado, itemElemento);
            }

            EstablecerIndicesProfundidadElementos();

            Button botonAtrasDiagramaAgrupador = new Button();
            botonAtrasDiagramaAgrupador.Content = "<- Atrás";
            botonAtrasDiagramaAgrupador.Click += BotonAtrasDiagramaAgrupador_Click;

            diagrama.Children.Add(botonAtrasDiagramaAgrupador);

            Canvas.SetTop(botonAtrasDiagramaAgrupador, 5);
            Canvas.SetLeft(botonAtrasDiagramaAgrupador, 5);
        }

        private void BotonAtrasDiagramaAgrupador_Click(object sender, RoutedEventArgs e)
        {
            if (AgrupadorAnterior != null)
            {
                AgrupadorSeleccionado = AgrupadorAnterior;
                AgrupadorAnterior = AgrupadorAnterior.AgrupadorContenedor;
                CargarDiagramaOperaciones_Agrupador();
            }
            else
            {
                CargarDiagramaOperaciones_Calculo();
            }
        }

        public void DibujarLineasElemento(DiseñoOperacion item)
        {
            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosPosterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor == null)))
            {
                if (itemElemento.Tipo == TipoElementoOperacion.Salida) continue;
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento, false);
                if (nuevaLinea != null)
                {

                    diagrama.Children.Add(nuevaLinea);

                }

            }

            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosAnterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor == null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento, false);
                if (nuevaLinea != null)
                {

                    diagrama.Children.Add(nuevaLinea);

                }

            }
        }

        public void DibujarLineasElemento_Agrupador(DiseñoOperacion Agrupador, DiseñoOperacion item)
        {
            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosPosterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor != null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento, true);
                if (nuevaLinea != null)
                {

                    diagrama.Children.Add(nuevaLinea);

                }

            }

            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosAnterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor != null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento, true);
                if (nuevaLinea != null)
                {

                    diagrama.Children.Add(nuevaLinea);

                }

            }
        }

        private ArrowLine BuscarLinea(DiseñoOperacion elementoOrigen, DiseñoOperacion elementoDestino, bool ModoAgrupador)
        {
            foreach (var item in SubCalculoSeleccionado_VistaInformeResultados.ElementosOperaciones)
            {
                //var elementoDestinoEncontrado = (from DiseñoOperacion E in item.ElementosAnteriores where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY select E).FirstOrDefault();
                //var elementoOrigenEncontrado = (from DiseñoOperacion E in item.ElementosPosteriores where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY select E).FirstOrDefault();

                //var elementoDestino2Encontrado = (from DiseñoOperacion E in item.ElementosPosteriores where E == elementoOrigen && item == elementoDestino select E).FirstOrDefault();
                //var elementoOrigen2Encontrado = (from DiseñoOperacion E in item.ElementosAnteriores where E == elementoDestino && item == elementoOrigen select E).FirstOrDefault();

                var elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && AgrupadorSeleccionado.ElementosAgrupados.Contains(i)))
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E.PosicionX == elementoOrigen?.PosicionX & E.PosicionY == elementoOrigen?.PosicionY & item.PosicionX == elementoDestino?.PosicionX & item.PosicionY == elementoDestino?.PosicionY
                                                 select E).FirstOrDefault();

                if (elementoDestinoEncontrado == null)
                {
                    //if(elementoOrigen == null)
                    //{
                    //    elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                    //                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                    //                                 where item.PosicionX == elementoDestino?.PosicionX & item.PosicionY == elementoDestino?.PosicionY
                    //                                 select E).FirstOrDefault();
                    //}

                    if (elementoDestino == null)
                    {
                        elementoDestinoEncontrado = (from DiseñoOperacion E in AgrupadorSeleccionado.ElementosAgrupados
                                                         //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                     where E.PosicionX == elementoOrigen?.PosicionX & E.PosicionY == elementoOrigen?.PosicionY
                                                     select E).FirstOrDefault();
                    }
                }

                var elementoOrigenEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && AgrupadorSeleccionado.ElementosAgrupados.Contains(i)))
                                                    //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                where E.PosicionX == elementoDestino?.PosicionX & E.PosicionY == elementoDestino?.PosicionY & item.PosicionX == elementoOrigen?.PosicionX & item.PosicionY == elementoOrigen?.PosicionY
                                                select E).FirstOrDefault();

                if (elementoOrigenEncontrado == null)
                {
                    if (elementoOrigen == null)
                    {
                        elementoOrigenEncontrado = (from DiseñoOperacion E in AgrupadorSeleccionado.ElementosAgrupados
                                                        //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                    where E.PosicionX == elementoDestino?.PosicionX & E.PosicionY == elementoDestino?.PosicionY
                                                    select E).FirstOrDefault();
                    }

                    //if (elementoDestino == null)
                    //{
                    //    elementoOrigenEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                    //                                    //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                    //                                where item.PosicionX == elementoOrigen?.PosicionX & item.PosicionY == elementoOrigen?.PosicionY
                    //                                select E).FirstOrDefault();
                    //}
                }

                var elementoDestino2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && AgrupadorSeleccionado.ElementosAgrupados.Contains(i)))
                                                      //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                  where E == elementoOrigen && item == elementoDestino
                                                  select E).FirstOrDefault();

                if (elementoDestino2Encontrado == null)
                {
                    //if (elementoOrigen == null)
                    //{
                    //    elementoDestino2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                    //                                      //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                    //                                  where item == elementoDestino
                    //                                  select E).FirstOrDefault();
                    //}

                    if (elementoDestino == null)
                    {
                        elementoDestino2Encontrado = (from DiseñoOperacion E in AgrupadorSeleccionado.ElementosAgrupados
                                                          //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                      where E == elementoOrigen
                                                      select E).FirstOrDefault();
                    }
                }

                var elementoOrigen2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && AgrupadorSeleccionado.ElementosAgrupados.Contains(i)))
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E == elementoDestino && item == elementoOrigen
                                                 select E).FirstOrDefault();

                if (elementoOrigen2Encontrado == null)
                {
                    if (elementoOrigen == null)
                    {
                        elementoOrigen2Encontrado = (from DiseñoOperacion E in AgrupadorSeleccionado.ElementosAgrupados
                                                         //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                     where E == elementoDestino
                                                     select E).FirstOrDefault();
                    }

                    //if (elementoDestino == null)
                    //{
                    //    elementoOrigen2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                    //                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                    //                                 where item == elementoOrigen
                    //                                 select E).FirstOrDefault();
                    //}
                }

                //if ((ModoAgrupador && (elementoOrigenEncontrado == null && elementoDestinoEncontrado == null)) ||
                //    ((elementoOrigenEncontrado != null | elementoDestinoEncontrado != null)))
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

                //if ((ModoAgrupador && (elementoOrigen2Encontrado == null && elementoDestino2Encontrado == null)) ||
                //    ((elementoOrigen2Encontrado != null | elementoDestino2Encontrado != null)))
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
                ref double posicionXDestino, ref double posicionYDestino, DiseñoOperacion elementoOrigen,
                DiseñoOperacion elementoDestino)
        {
            if (elementoOrigen != null)
            {
                posicionXOrigen = elementoOrigen.PosicionX + elementoOrigen.Anchura / 2;
                posicionYOrigen = elementoOrigen.PosicionY + elementoOrigen.Altura / 2;
            }
            else
            {
                posicionXOrigen = 0;
                posicionYOrigen = diagrama.Height / 2;
            }

            if (elementoDestino != null)
            {
                posicionXDestino = elementoDestino.PosicionX + elementoDestino.Anchura / 2;
                posicionYDestino = elementoDestino.PosicionY + elementoDestino.Altura / 2;
            }
            else
            {
                posicionXDestino = diagrama.Width;
                posicionYDestino = diagrama.Height / 2;
            }



            if (elementoOrigen != null)
            {
                if (posicionXDestino < elementoOrigen.PosicionX)
                    posicionXOrigen -= elementoOrigen.Anchura / 2;
                else if (posicionXDestino > elementoOrigen.PosicionX + elementoOrigen.Anchura)
                    posicionXOrigen += elementoOrigen.Anchura / 2;

                if (posicionYDestino < elementoOrigen.PosicionY)
                    posicionYOrigen -= elementoOrigen.Altura / 2;
                else if (posicionYDestino > elementoOrigen.PosicionY + elementoOrigen.Altura)
                    posicionYOrigen += elementoOrigen.Altura / 2;
            }
            //else
            //{
            //    //if (posicionXOrigen < elementoDestino.PosicionX)
            //        posicionXDestino -= elementoDestino.Anchura / 2;
            //    //else if (posicionXOrigen > elementoDestino.PosicionX + elementoDestino.Anchura)
            //    //    posicionXDestino += elementoDestino.Anchura / 2;

            //    if (posicionYOrigen < elementoDestino.PosicionY)
            //        posicionYDestino -= elementoDestino.Altura / 2;
            //    else if (posicionYOrigen > elementoDestino.PosicionY + elementoDestino.Altura)
            //        posicionYDestino += elementoDestino.Altura / 2;
            //}


            if (elementoDestino != null)
            {
                if (posicionXOrigen < elementoDestino.PosicionX)
                    posicionXDestino -= elementoDestino.Anchura / 2;
                else if (posicionXOrigen > elementoDestino.PosicionX + elementoDestino.Anchura)
                    posicionXDestino += elementoDestino.Anchura / 2;

                if (posicionYOrigen < elementoDestino.PosicionY)
                    posicionYDestino -= elementoDestino.Altura / 2;
                else if (posicionYOrigen > elementoDestino.PosicionY + elementoDestino.Altura)
                    posicionYDestino += elementoDestino.Altura / 2;
            }
        }

        private void EstablecerIndicesProfundidadElementos()
        {
            int indice = 0;

            var elementos = (from UIElement L in diagrama.Children where L.GetType() != typeof(ArrowLine) select L).ToList();
            foreach (var item in elementos)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }

            var lineas = (from UIElement E in diagrama.Children where E.GetType() == typeof(ArrowLine) select E).ToList();
            foreach (var item in lineas)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(Ejecucion.Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            if (!double.IsNaN(Ejecucion.Calculo.Ancho) &
                !double.IsNaN(Ejecucion.Calculo.Alto))
            {
                diagramaCalculos.Width = Ejecucion.Calculo.Ancho;
                diagramaCalculos.Height = Ejecucion.Calculo.Alto;
            }

            DibujarCalculos();

            ListarLog();
            MostrarResultados();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirResultadosEjecucionCalculo");
#endif
        }

        private void ListarLog()
        {
            logProceso.Children.Clear();
            foreach (var itemLog in Ejecucion.InformeResultados.TextoLog)
            {
                TextBlock texto = new TextBlock();
                texto.Margin = new Thickness(10);
                texto.Text = itemLog;
                logProceso.Children.Add(texto);
            }
        }

        private void MostrarResultados()
        {
            contenidoResultados.Children.Clear();

            ResultadoInforme resultadoCabecera = new ResultadoInforme();
            resultadoCabecera.ModoTodos = true;
            resultadoCabecera.HorizontalAlignment = HorizontalAlignment.Left;
            resultadoCabecera.HorizontalContentAlignment = HorizontalAlignment.Left;
            resultadoCabecera.ListaResultados = contenidoResultados;

            contenidoResultados.Children.Add(resultadoCabecera);

            foreach (var itemResultado in Ejecucion.InformeResultados.Resultados)
            {
                ResultadoInforme resultado = new ResultadoInforme();
                resultado.CantidadDecimalesCantidades = Ejecucion.Calculo.CantidadDecimalesCantidades;
                resultado.Resultado = itemResultado;
                resultado.HorizontalAlignment = HorizontalAlignment.Left;
                resultado.HorizontalContentAlignment = HorizontalAlignment.Left;

                if (!string.IsNullOrEmpty(itemResultado.Nombre))
                    resultado.nombre.Text = itemResultado.Nombre;
                else
                    resultado.nombre.Visibility = Visibility.Collapsed;

                if (!itemResultado.EsConjuntoNumeros)
                {
                    //resultado.valorNumerico.Text = itemResultado.ValorNumerico.ToString("N" + Ejecucion.Calculo.CantidadDecimalesCantidades.ToString());

                    Grid tablaNumero = new Grid();
                    tablaNumero.HorizontalAlignment = HorizontalAlignment.Left;
                    tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());
                    //tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());
                    //tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());

                    //foreach (var columna in tablaNumero.ColumnDefinitions)
                    //{
                    //    columna.Width = GridLength.Auto;
                    //}

                    //TextBlock nombre = new TextBlock();
                    //nombre.FontWeight = resultado.valorNumerico.FontWeight;
                    //nombre.FontStyle = resultado.valorNumerico.FontStyle;

                    //if (!string.IsNullOrEmpty(numero.Nombre))
                    //    nombre.Text = numero.Nombre + "  ";
                    //else
                    //    nombre.Visibility = Visibility.Collapsed;

                    //tablaNumero.Children.Add(nombre);
                    //Grid.SetColumn(nombre, 0);

                    TextBlock valor = new TextBlock();
                    valor.FontWeight = FontWeights.Bold;
                    valor.Text = itemResultado.ValorNumerico.ToString("N" + Ejecucion.Calculo.CantidadDecimalesCantidades.ToString()) + " " +
                        itemResultado.TextoAcompañante;
                    valor.Padding = new Thickness(10);
                    valor.VerticalAlignment = VerticalAlignment.Top;
                    valor.HorizontalAlignment = HorizontalAlignment.Left;

                    tablaNumero.Children.Add(valor);
                    Grid.SetColumn(valor, 0);

                    //Grid tablaTextos = new Grid();
                    //tablaTextos.ColumnDefinitions.Add(new ColumnDefinition());
                    //tablaTextos.ColumnDefinitions.Last().Width = GridLength.Auto;

                    //int indice = 0;

                    foreach (var itemTexto in itemResultado.TextosInformacion)
                    {
                        resultado.textos.Text += itemTexto + " ";
                        //tablaNumero.ShowGridLines = true;
                        //tablaTextos.RowDefinitions.Add(new RowDefinition());
                        //tablaTextos.RowDefinitions.Last().Height = GridLength.Auto;

                        //TextBlock texto = new TextBlock();
                        //texto.FontWeight = FontWeights.Bold;
                        //texto.Padding = new Thickness(10);

                        //if (!string.IsNullOrEmpty(itemTexto))
                        //    texto.Text = itemTexto;
                        //else
                        //    texto.Visibility = Visibility.Collapsed;

                        //tablaTextos.Children.Add(texto);

                        //Grid.SetColumn(texto, 0);
                        //Grid.SetRow(texto, indice);

                        //indice++;
                    }

                    //tablaNumero.Children.Add(tablaTextos);
                    //Grid.SetColumn(tablaTextos, 1);

                    resultado.contenidoNumerosResultado.Children.Add(tablaNumero);
                }
                else
                {
                    foreach (var itemClasificador in itemResultado.ResultadoAsociado.Clasificadores_Cantidades)
                    {
                        Grid tablaClasificador = new Grid();
                        tablaClasificador.HorizontalAlignment = HorizontalAlignment.Left;

                        tablaClasificador.ColumnDefinitions.Add(new ColumnDefinition());

                        //foreach (var columna in tablaClasificador.ColumnDefinitions)
                        //{
                        //    columna.Width = GridLength.Auto;
                        //}

                        TextBlock nombreClasificador = new TextBlock();
                        nombreClasificador.FontWeight = FontWeights.Bold;
                        nombreClasificador.Text = !string.IsNullOrEmpty(itemClasificador.CadenaTexto) ? itemClasificador.CadenaTexto : string.Empty;
                        nombreClasificador.Padding = new Thickness(10);
                        nombreClasificador.VerticalAlignment = VerticalAlignment.Top;
                        nombreClasificador.HorizontalAlignment = HorizontalAlignment.Left;

                        tablaClasificador.Children.Add(nombreClasificador);
                        Grid.SetColumn(nombreClasificador, 0);

                        resultado.contenidoNumerosResultado.Children.Add(tablaClasificador);

                        foreach (var numero in itemResultado.ValoresNumericos.Where(i => i.Clasificadores_SeleccionarOrdenar_Resultados.Any(i => i.CadenaTexto == itemClasificador.CadenaTexto)))
                        {
                            if (itemResultado.NoMostrar_SiEsCero && numero.Numero == 0)
                                continue;

                            Grid tablaNumero = new Grid();
                            tablaNumero.HorizontalAlignment = HorizontalAlignment.Left;
                            //tablaNumero.ShowGridLines = true;
                            tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());
                            tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());
                            tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());
                            //tablaNumero.ColumnDefinitions.Add(new ColumnDefinition());

                            //foreach (var columna in tablaNumero.ColumnDefinitions)
                            //{
                            //    columna.Width = GridLength.Auto;
                            //}

                            tablaNumero.RowDefinitions.Add(new RowDefinition());

                            //foreach (var fila in tablaNumero.RowDefinitions)
                            //{
                            //    fila.Height = GridLength.Auto;                                
                            //}

                            TextBlock nombre = new TextBlock();
                            nombre.FontWeight = FontWeights.Bold;
                            nombre.Padding = new Thickness(10);
                            nombre.VerticalAlignment = VerticalAlignment.Top;
                            nombre.HorizontalAlignment = HorizontalAlignment.Left;

                            if (!string.IsNullOrEmpty(numero.Nombre))
                                nombre.Text = numero.Nombre + "  ";
                            else
                                nombre.Visibility = Visibility.Collapsed;

                            tablaNumero.Children.Add(nombre);
                            Grid.SetColumn(nombre, 0);
                            Grid.SetRow(nombre, 0);

                            TextBlock valor = new TextBlock();
                            valor.FontWeight = FontWeights.Bold;
                            valor.Text = numero.Numero.ToString("N" + Ejecucion.Calculo.CantidadDecimalesCantidades.ToString()) + " " +
                                            numero.TextoAcompañante;
                            valor.Padding = new Thickness(10);
                            valor.VerticalAlignment = VerticalAlignment.Top;
                            valor.HorizontalAlignment = HorizontalAlignment.Left;

                            tablaNumero.Children.Add(valor);
                            Grid.SetColumn(valor, 1);
                            Grid.SetRow(valor, 0);
                            //Grid tablaTextos = new Grid();
                            //tablaTextos.ColumnDefinitions.Add(new ColumnDefinition());
                            //tablaTextos.ColumnDefinitions.Last().Width = GridLength.Auto;

                            //int indice = 0;
                            TextBlock textos = new TextBlock();
                            textos.Padding = new Thickness(10);
                            textos.VerticalAlignment = VerticalAlignment.Top;
                            textos.HorizontalAlignment = HorizontalAlignment.Left;

                            foreach (var itemTexto in numero.Textos)
                            {
                                textos.Text += itemTexto + " ";
                                //tablaNumero.ShowGridLines = true;
                                //tablaTextos.RowDefinitions.Add(new RowDefinition());
                                //tablaTextos.RowDefinitions.Last().Height = GridLength.Auto;

                                //TextBlock texto = new TextBlock();
                                //texto.FontWeight = FontWeights.Bold;
                                //texto.Padding = new Thickness(10);

                                //if (!string.IsNullOrEmpty(itemTexto))
                                //    texto.Text = itemTexto;
                                //else
                                //    texto.Visibility = Visibility.Collapsed;

                                //tablaTextos.Children.Add(texto);

                                //Grid.SetColumn(texto, 0);
                                //Grid.SetRow(texto, indice);

                                //indice++;
                            }

                            tablaNumero.Children.Add(textos);
                            Grid.SetColumn(textos, 2);
                            Grid.SetRow(textos, 0);

                            resultado.contenidoNumerosResultado.Children.Add(tablaNumero);
                        }
                    }
                }

                resultado.HorizontalAlignment = HorizontalAlignment.Left;
                resultado.HorizontalContentAlignment = HorizontalAlignment.Left;

                if (!string.IsNullOrEmpty(itemResultado.Descripcion))
                    resultado.descripcion.Text = itemResultado.Descripcion;
                else
                    resultado.descripcion.Visibility = Visibility.Collapsed;

                bool mostrar = true;

                if (itemResultado.NoMostrar_SiEsConjunto_SiNoTieneNumeros &&
                    itemResultado.EsConjuntoNumeros && !itemResultado.ValoresNumericos.Any())
                {
                    mostrar = false;
                }

                if (itemResultado.NoMostrar_SiEsCero &&
                    !itemResultado.EsConjuntoNumeros &&
                    itemResultado.ValorNumerico == 0)
                {
                    mostrar = false;
                }

                if (mostrar)
                    contenidoResultados.Children.Add(resultado);
            }
        }

        public void DibujarLineasElemento_Calculo(DiseñoCalculo item)
        {
            foreach (var itemElemento in item.ElementosPosteriores)
            {
                if (itemElemento.EsEntradasArchivo) continue;

                ArrowLine nuevaLinea = BuscarLinea_Calculo(item, itemElemento);
                if (nuevaLinea != null)
                {

                    diagramaCalculos.Children.Add(nuevaLinea);

                }

            }

            foreach (var itemElemento in item.ElementosAnteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea_Calculo(item, itemElemento);
                if (nuevaLinea != null)
                {

                    diagramaCalculos.Children.Add(nuevaLinea);

                }

            }
        }

        private ArrowLine BuscarLinea_Calculo(DiseñoCalculo elementoOrigen, DiseñoCalculo elementoDestino)
        {
            foreach (var item in Ejecucion.Calculo.Calculos)
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

                    CalcularCoordenadasLinea_Calculo(ref posicionXOrigen, ref posicionYOrigen, ref posicionXDestino,
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

                    CalcularCoordenadasLinea_Calculo(ref posicionXOrigen, ref posicionYOrigen, ref posicionXDestino,
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

        private void CalcularCoordenadasLinea_Calculo(ref double posicionXOrigen, ref double posicionYOrigen,
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

        private void EstablecerIndicesProfundidadElementos_Calculos()
        {
            int indice = 0;

            var elementos = (from UIElement E in diagramaCalculos.Children where E.GetType() != typeof(ArrowLine) select E).ToList();
            foreach (var item in elementos)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }

            var lineas = (from UIElement L in diagramaCalculos.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var item in lineas)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }
        }

        public void CargarDiagramaOperaciones_Calculo()
        {
            if (SubCalculoSeleccionado_VistaInformeResultados.Ancho != double.NaN &
                SubCalculoSeleccionado_VistaInformeResultados.Alto != double.NaN)
            {
                diagrama.Width = SubCalculoSeleccionado_VistaInformeResultados.Ancho;
                diagrama.Height = SubCalculoSeleccionado_VistaInformeResultados.Alto;
            }

            DibujarElementosOperaciones();
        }

        public void CargarDiagramaOperaciones_Agrupador()
        {
            if (AgrupadorSeleccionado.AnchoDiagrama != double.NaN &
                AgrupadorSeleccionado.AltoDiagrama != double.NaN)
            {
                diagrama.Width = AgrupadorSeleccionado.AnchoDiagrama;
                diagrama.Height = AgrupadorSeleccionado.AltoDiagrama;
            }

            DibujarElementosOperaciones_Agrupador();
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
