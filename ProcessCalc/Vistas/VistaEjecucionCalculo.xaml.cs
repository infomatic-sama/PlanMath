using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para EjecucionCalculo.xaml
    /// </summary>
    public partial class VistaEjecucionCalculo : UserControl
    {
        public MainWindow Ventana { get; set; }
        public EjecucionCalculo Ejecucion { get; set; }
        public BackgroundWorker HiloEjecucion { get; set; }
        bool boolDetener = false;
        bool boolPausar = false;
        bool boolReanudar = false;
        //ElementoDiagramaEjecucion rectanguloCalculo = new ElementoDiagramaEjecucion();
        List<DiseñoOperacion> AgrupadoresAgregadosDiagramas = new List<DiseñoOperacion>();
        bool CalculosDibujados = false;
        public VistaEjecucionCalculo()
        {
            InitializeComponent();
            HiloEjecucion = new BackgroundWorker();
            HiloEjecucion.WorkerSupportsCancellation = true;
            HiloEjecucion.DoWork += RealizarCalculo;
            HiloEjecucion.RunWorkerCompleted += FinalizarCalculo;
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

            if (!CalculosDibujados)
            {
                DibujarCalculos();
                CalculosDibujados = true;
            }
            if (Ejecucion != null && !Ejecucion.Finalizada && !Ejecucion.Iniciada)
            {
                Ventana.EjecutandoCalculos = true;
                Ejecucion.Iniciar();                
                HiloEjecucion.RunWorkerAsync();
                //rectanguloCalculo.Background = Brushes.LightBlue;
            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirEjecucionCalculo");
#endif
        }

        private void RealizarCalculo(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    if (!boolPausar)
                    {
                        bool terminar = false;

                        try
                        {
                            if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                            {
                                Dispatcher.Invoke(() =>
                            {
                                try
                                {
                                    progreso.Value = (int)Ejecucion.Progreso.PorcentajeAvance;

                                    double avance = 0;
                                    if (progreso.Maximum > 0)
                                        avance = (progreso.Value / progreso.Maximum) * 100.0;

                                    textoPorcentajeEjecucion.Text = ((int)avance).ToString() + " %";
                                    textoPorcentajeEjecucion1.Text = textoPorcentajeEjecucion.Text;
                                    textoPorcentajeEjecucion2.Text = textoPorcentajeEjecucion.Text;

                                    ActualizarColores();
                                    MostrarLog();

                                    if (Ejecucion.CambioSubcalculo)
                                    {
                                        Ejecucion.CambioSubcalculo = false;

                                        TabItem fichaCalculo = new TabItem();
                                        fichaCalculo.Header = Ejecucion.SubCalculoActual.Nombre;

                                        ScrollViewer contenedorCalculo = new ScrollViewer();
                                        contenedorCalculo.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                                        contenedorCalculo.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                                        if (Ejecucion.SubCalculoActual.EsEntradasArchivo)
                                        {
                                            StackPanel entradas = new StackPanel();
                                            entradas.Margin = new Thickness(10);
                                            entradas.Orientation = Orientation.Vertical;

                                            fichaCalculo.Content = contenedorCalculo;
                                            contenedorCalculo.Content = entradas;

                                            diagramasCalculos.Items.Add(fichaCalculo);
                                            diagramasCalculos.SelectedItem = fichaCalculo;

                                            DibujarElementosOperaciones_Entradas(entradas, Ejecucion.SubCalculoActual);
                                        }
                                        else
                                        {
                                            Canvas diagramaCalculo = new Canvas();

                                            fichaCalculo.Content = contenedorCalculo;
                                            contenedorCalculo.Content = diagramaCalculo;

                                            diagramasCalculos.Items.Add(fichaCalculo);
                                            diagramasCalculos.SelectedItem = fichaCalculo;

                                            IniciarSubCalculo(diagramaCalculo);
                                        }
                                    }

                                    if (Ejecucion.AgrupadorActual != null && !AgrupadoresAgregadosDiagramas.Contains(Ejecucion.AgrupadorActual))
                                    {
                                        if (columnaAgrupadores.Width == GridLength.Auto)
                                            columnaAgrupadores.Width = new GridLength(1, GridUnitType.Star);

                                        TabItem fichaAgrupador = new TabItem();
                                        fichaAgrupador.Header = Ejecucion.AgrupadorActual.NombreCombo;

                                        ScrollViewer contenedorAgrupador = new ScrollViewer();
                                        contenedorAgrupador.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                                        contenedorAgrupador.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                                        Canvas diagramaAgrupador = new Canvas();

                                        fichaAgrupador.Content = contenedorAgrupador;
                                        contenedorAgrupador.Content = diagramaAgrupador;

                                        diagramasAgrupadores.Items.Add(fichaAgrupador);
                                        diagramasAgrupadores.SelectedItem = fichaAgrupador;

                                        IniciarAgrupador(Ejecucion.AgrupadorActual, diagramaAgrupador);
                                        AgrupadoresAgregadosDiagramas.Add(Ejecucion.AgrupadorActual);

                                        Ejecucion.AgrupadorActual = null;
                                    }

                                    if (progreso.Value == progreso.Maximum & Ejecucion.Finalizada)
                                    {
                                        terminar = true;
                                    }
                                    else
                                    {
                                        if (Ejecucion.Detenida | Ejecucion.ConError) boolDetener = true;
                                        if (Ejecucion.Pausada) boolPausar = true;
                                    }
                                }
                                catch (Exception err)
                                {
                                }
                            });
                            }
                        }
                        catch (Exception err2)
                        {
                        }

                        if (terminar) break;
                        if (boolDetener)
                        {
                            DetenerCalculo();
                            break;
                        }
                        if (boolPausar)
                        {
                            PausarCalculo();
                        }
                    }
                    else
                    {
                        if (boolReanudar)
                        {
                            boolReanudar = false;
                            boolPausar = false;

                            try
                            {
                                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                                {
                                    Dispatcher.Invoke(() =>
                                {
                                    Ejecucion.Pausada = false;
                                });
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                }
                catch (Exception) { }
                try { Thread.Sleep(10); } catch (Exception) { };
            }
        }

        private void MostrarLog()
        {
            List<string> log = Ejecucion.TextoLog;
            foreach (var itemLog in log)
            {
                TextBlock texto = new TextBlock();
                texto.Margin = new Thickness(10);
                if (Ejecucion.ConError)
                    texto.Foreground = Brushes.Red;
                texto.Text = itemLog;
                logEjecucion.Children.Add(texto);

                contenedorLog.ScrollToVerticalOffset(logEjecucion.ActualHeight);
            }
        }

        private void ActualizarColores()
        {
            foreach (var itemEtapa in Ejecucion.etapasCalculosProgreso)
            {
                foreach (var itemCalculo in itemEtapa.Elementos)
                {
                    UIElement elementoCalculo = (from UIElement E in diagramaCalculos.Children
                                                 where E.GetType() == typeof(ElementoDiagramaEjecucion) &&
                                                 ((ElementoDiagramaEjecucion)E).ElementoCalculoRelacionado == itemCalculo.ElementoDiseñoCalculoRelacionado
                                                 select E).FirstOrDefault();

                    if (elementoCalculo != null)
                    {
                        switch (itemCalculo.Estado)
                        {
                            case EstadoEjecucion.Iniciado:
                                ((ElementoDiagramaEjecucion)elementoCalculo).Background = Brushes.LightYellow;
                                break;

                            case EstadoEjecucion.Procesado:
                                ((ElementoDiagramaEjecucion)elementoCalculo).Background = Brushes.LightBlue;
                                break;
                        }

                        if(itemCalculo.CantidadEtapas > 0)
                            ((ElementoDiagramaEjecucion)elementoCalculo).avanceConjuntoNumeros.Value = ((double)itemCalculo.CantidadEtapasProcesadas / (double)itemCalculo.CantidadEtapas) * (double)100.0;
                    }

                }
            }

            foreach (var itemEtapaCalculo in Ejecucion.etapasProgreso)
            {
                foreach (var itemElemento in itemEtapaCalculo.Elementos)
                {
                    foreach (var diagrama in (from TabItem D in diagramasCalculos.Items where ((ScrollViewer)D.Content).Content.GetType() == typeof(Canvas) select (Canvas)((ScrollViewer)D.Content).Content))
                    {
                        UIElement elemento = (from UIElement E in diagrama.Children
                                              where E.GetType() == typeof(ElementoDiagramaEjecucion) &&
                                              ((ElementoDiagramaEjecucion)E).ElementoRelacionado == itemElemento.ElementoDiseñoRelacionado
                                              select E).FirstOrDefault();

                        if (elemento != null)
                        {
                            switch (itemElemento.Estado)
                            {
                                case EstadoEjecucion.Iniciado:
                                    ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightYellow;
                                    break;

                                case EstadoEjecucion.Procesado:
                                    ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightBlue;
                                    break;
                            }

                            if (itemElemento.CantidadSubElementos > 0)
                                ((ElementoDiagramaEjecucion)elemento).avanceConjuntoNumeros.Value = ((double)itemElemento.CantidadSubElementosProcesados / (double)itemElemento.CantidadSubElementos) * (double)100.0;
                        }
                    }

                    foreach (var diagrama in (from TabItem D in diagramasCalculos.Items where ((ScrollViewer)D.Content).Content.GetType() == typeof(StackPanel) select (StackPanel)((ScrollViewer)D.Content).Content))
                    {
                        UIElement elemento = (from UIElement E in diagrama.Children
                                              where E.GetType() == typeof(ElementoDiagramaEjecucion) &&
                                              ((ElementoDiagramaEjecucion)E).ElementoRelacionado == itemElemento.ElementoDiseñoRelacionado
                                              select E).FirstOrDefault();

                        if (elemento != null)
                        {
                            switch (itemElemento.Estado)
                            {
                                case EstadoEjecucion.Iniciado:
                                    ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightYellow;
                                    break;

                                case EstadoEjecucion.Procesado:
                                    ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightBlue;
                                    break;
                            }

                            if (itemElemento.CantidadSubElementos > 0)
                                ((ElementoDiagramaEjecucion)elemento).avanceConjuntoNumeros.Value = ((double)itemElemento.CantidadSubElementosProcesados / (double)itemElemento.CantidadSubElementos) * (double)100.0;
                        }
                    }

                    foreach (var diagrama in (from TabItem D in diagramasAgrupadores.Items where ((ScrollViewer)D.Content).Content.GetType() == typeof(Canvas) select (Canvas)((ScrollViewer)D.Content).Content))
                    {
                        UIElement elemento = (from UIElement E in diagrama.Children
                                              where E.GetType() == typeof(ElementoDiagramaEjecucion) &&
                                              ((ElementoDiagramaEjecucion)E).ElementoRelacionado == itemElemento.ElementoDiseñoRelacionado
                                              select E).FirstOrDefault();

                        if (elemento != null)
                        {
                            switch (itemElemento.Estado)
                            {
                                case EstadoEjecucion.Iniciado:
                                    ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightYellow;
                                    break;

                                case EstadoEjecucion.Procesado:
                                    ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightBlue;
                                    break;
                            }

                            if (itemElemento.CantidadSubElementos > 0)
                                ((ElementoDiagramaEjecucion)elemento).avanceConjuntoNumeros.Value = ((double)itemElemento.CantidadSubElementosProcesados / (double)itemElemento.CantidadSubElementos) * (double)100.0;
                        }
                    }
                }
            }

            List<ElementoEjecucionCalculo> elementos = new List<ElementoEjecucionCalculo>();
            foreach (var itemE in Ejecucion.etapasProgreso)
            {
                elementos.AddRange(itemE.Elementos);
            }

            foreach (var itemElemento in Ejecucion.ElementosAgrupadores)
            {
                foreach (var diagrama in (from TabItem D in diagramasCalculos.Items where ((ScrollViewer)D.Content).Content.GetType() == typeof(Canvas) select (Canvas)((ScrollViewer)D.Content).Content))
                {
                    UIElement elemento = (from UIElement E in diagrama.Children
                                        where E.GetType() == typeof(ElementoDiagramaEjecucion) &&
                                        ((ElementoDiagramaEjecucion)E).ElementoRelacionado == itemElemento
                                        select E).FirstOrDefault();

                    if (elemento != null)
                    {
                        var elementosAgrupador = (from E in elementos where E.ElementoDiseñoRelacionado.AgrupadorContenedor == itemElemento select E).ToList();

                        if (elementosAgrupador.Any(i => i.Estado == EstadoEjecucion.Iniciado))
                        {
                            ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightYellow;
                        }

                        if (elementosAgrupador.Count(i => i.Estado == EstadoEjecucion.Procesado) == elementosAgrupador.Count)
                        {
                            ((ElementoDiagramaEjecucion)elemento).Background = Brushes.LightBlue;
                        }

                        if (elementosAgrupador.Count > 0)
                            ((ElementoDiagramaEjecucion)elemento).avanceConjuntoNumeros.Value = ((double)elementosAgrupador.Count(i => i.Estado == EstadoEjecucion.Procesado) / (double)elementosAgrupador.Count) * (double)100.0;

                    }
                }
                
            }

            //if (progreso.Value == Ejecucion.Progreso.PorcentajeAvance)
            //{
            //    rectanguloCalculo.Background = Brushes.LightBlue;
            //}
        }

        private void FinalizarCalculo(object sender, RunWorkerCompletedEventArgs e)
        {
            ActualizarColores();
            MostrarLog();
            
            if (boolDetener == false)
            {
                App.AgregarNuevaEjecucionReciente(Ejecucion);
                App.GuardarListaEjecucionesRecientes();
                Ventana.ListarEjecucionesRecientes();

                estado.Visibility = Visibility.Collapsed;
                finalizado.Visibility = Visibility.Visible;
                Ventana.EjecutandoCalculos = false;
            }
        }

        private void DetenerCalculo()
        {
            try
            {
                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                {
                    Dispatcher.Invoke(() =>
                {
                    Ventana.EjecutandoCalculos = false;
                    HiloEjecucion.CancelAsync();
                    estado.Visibility = Visibility.Collapsed;
                    detenido.Visibility = Visibility.Visible;
                });
                }
            }
            catch (Exception) { }
        }

        private void PausarCalculo()
        {
            try
            {
                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                {
                    Dispatcher.Invoke(() =>
                {
                    estado.Visibility = Visibility.Collapsed;
                    pausado.Visibility = Visibility.Visible;
                });
                }
            }
            catch (Exception) { }
        }

        private void ReanudarCalculo()
        {
            try
            {
                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                {
                    Dispatcher.Invoke(() =>
                {
                    estado.Visibility = Visibility.Visible;
                    pausado.Visibility = Visibility.Collapsed;
                    boolReanudar = true;
                });
                }
            }
            catch (Exception) { }
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            Ventana.QuitarTabActual();
        }

        public void detener_Click(object sender, RoutedEventArgs e)
        {
            ReanudarCalculo();

            try
            {
                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                {
                    Dispatcher.Invoke(() =>
                {
                    Ejecucion.Detener();
                });
                }
            }
            catch (Exception) { }
        }

        private void pausar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
                {
                    Dispatcher.Invoke(() =>
                {
                    Ejecucion.Pausada = true;
                });
                }
            }
            catch (Exception) { }
        }

        private void reanudar_Click(object sender, RoutedEventArgs e)
        {
            ReanudarCalculo();
        }

        private void IniciarSubCalculo(Canvas diagramaCalculo)
        {
            if (Ejecucion.SubCalculoActual.Ancho != double.NaN &
                Ejecucion.SubCalculoActual.Alto != double.NaN)
            {
                diagramaCalculo.Width = Ejecucion.SubCalculoActual.Ancho;
                diagramaCalculo.Height = Ejecucion.SubCalculoActual.Alto;
            }

            DibujarElementosOperaciones(diagramaCalculo);
        }

        private void IniciarAgrupador(DiseñoOperacion Agrupador, Canvas diagramaAgrupador)
        {
            if (Agrupador.AnchoDiagrama != double.NaN &
                Agrupador.AltoDiagrama != double.NaN)
            {
                diagramaAgrupador.Width = Agrupador.AnchoDiagrama;
                diagramaAgrupador.Height = Agrupador.AltoDiagrama;
            }

            DibujarElementosAgrupador(diagramaAgrupador, Agrupador);
        }

        public void DibujarCalculos()
        {
            diagramaCalculos.Children.Clear();

            //rectanguloCalculo.nombreElemento.Text = "Cálculo";
            //rectanguloCalculo.tipoElemento.Text = string.Empty;
            //rectanguloCalculo.avanceConjuntoNumeros.Visibility = Visibility.Collapsed;
            //rectanguloCalculo.tipoElemento.Visibility = Visibility.Collapsed;
            //rectanguloCalculo.iconoCalculo.Visibility = Visibility.Visible;

            //diagramaCalculos.Children.Add(rectanguloCalculo);

            //Canvas.SetTop(rectanguloCalculo, diagramaCalculos.ActualHeight / 2 );
            //Canvas.SetLeft(rectanguloCalculo, diagramaCalculos.ActualWidth / 2 );

            //EstablecerIndicesProfundidadElementosCalculos();

            foreach (var itemElemento in Ejecucion.Calculo.Calculos)
            {
                if (itemElemento.EsEntradasArchivo) continue;

                ElementoDiagramaEjecucion nuevoCalculo = new ElementoDiagramaEjecucion();
                nuevoCalculo.Bloqueado = true;
                nuevoCalculo.ElementoCalculoRelacionado = itemElemento;
                //nuevoCalculo.SizeChanged += CambioTamañoCalculo;

                diagramaCalculos.Children.Add(nuevoCalculo);

                Canvas.SetTop(nuevoCalculo, itemElemento.PosicionY);
                Canvas.SetLeft(nuevoCalculo, itemElemento.PosicionX);

                //itemElemento.AnchuraElemento = nuevoCalculo.ActualWidth;
                //itemElemento.AlturaElemento = nuevoCalculo.ActualHeight;

                DibujarLineasElemento_Calculo(itemElemento);
            }

            EstablecerIndicesProfundidadElementos_Calculos();
        }

        public void DibujarElementosOperaciones(Canvas diagramaCalculo)
        {
            diagramaCalculo.Children.Clear();

            foreach (var itemElemento in Ejecucion.SubCalculoActual.ElementosOperaciones)
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

                    nuevaEntrada.Bloqueado = true;
                    nuevaEntrada.ElementoRelacionado = itemElemento;

                    diagramaCalculo.Children.Add(nuevaEntrada);

                    Canvas.SetTop(nuevaEntrada, itemElemento.PosicionY);
                    Canvas.SetLeft(nuevaEntrada, itemElemento.PosicionX);

                    DibujarLineasElemento(itemElemento, diagramaCalculo);
                }
            }

            EstablecerIndicesProfundidadElementos(diagramaCalculo);
        }

        public void DibujarElementosAgrupador(Canvas diagramaAgrupador, DiseñoOperacion Agrupador)
        {
            diagramaAgrupador.Children.Clear();

            foreach (var itemElemento in Agrupador.ElementosAgrupados)
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

                nuevaEntrada.Bloqueado = true;
                nuevaEntrada.ElementoRelacionado = itemElemento;

                diagramaAgrupador.Children.Add(nuevaEntrada);

                Canvas.SetTop(nuevaEntrada, itemElemento.PosicionY);
                Canvas.SetLeft(nuevaEntrada, itemElemento.PosicionX);

                DibujarLineasElemento_Agrupador(Agrupador, itemElemento, diagramaAgrupador);                
            }

            EstablecerIndicesProfundidadElementos_Agrupador(diagramaAgrupador);
        }

        public void DibujarElementosOperaciones_Entradas(StackPanel diagramaCalculo, DiseñoCalculo Calculo)
        {            
            foreach (var itemElemento in Calculo.ElementosOperaciones)
            {                
                ElementoDiagramaEjecucion nuevaEntrada = new ElementoDiagramaEjecucion();
                nuevaEntrada.Margin = new Thickness(10);
                nuevaEntrada.EsEntrada = true;
                nuevaEntrada.Bloqueado = true;
                nuevaEntrada.ElementoRelacionado = itemElemento;

                diagramaCalculo.Children.Add(nuevaEntrada);                
            }
        }
        public void DibujarLineasElemento(DiseñoOperacion item, Canvas diagramaCalculo)
        {
            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosPosterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor == null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagramaCalculo.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    if (lineaEncontrada == null)
                    {
                        diagramaCalculo.Children.Add(nuevaLinea);

                    }
                }

            }

            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosAnterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor == null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagramaCalculo.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    if (lineaEncontrada == null)
                    {
                        diagramaCalculo.Children.Add(nuevaLinea);
                    }
                }

            }
        }

        public void DibujarLineasElemento_Agrupador(DiseñoOperacion Agrupador, DiseñoOperacion item, Canvas diagramaAgrupador)
        {
            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosPosterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor != null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagramaAgrupador.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    if (lineaEncontrada == null)
                    {
                        diagramaAgrupador.Children.Add(nuevaLinea);
                    }
                }

            }

            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosAnterioresAgrupados.Where(i => i.Tipo != TipoElementoOperacion.Salida) :
                item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida & i.AgrupadorContenedor != null)))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagramaAgrupador.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    if (lineaEncontrada == null)
                    {
                        diagramaAgrupador.Children.Add(nuevaLinea);
                    }
                }

            }
        }

        private ArrowLine BuscarLinea(DiseñoOperacion elementoOrigen, DiseñoOperacion elementoDestino)
        {
            foreach (var item in Ejecucion.SubCalculoActual.ElementosOperaciones)
            {
                //var elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                 ModoAgrupador ? item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                 CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones && 
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosAnteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosAnteriores.Contains(j))))

                //                                 where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY select E).FirstOrDefault();

                //var elementoOrigenEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                ModoAgrupador ? item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosPosteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosPosteriores.Contains(j))))

                //                                where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY select E).FirstOrDefault();

                //var elementoDestino2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                  ModoAgrupador ? item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                  CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosPosteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosPosteriores.Contains(j))))

                //                                  where E == elementoOrigen && item == elementoDestino select E).FirstOrDefault();

                //var elementoOrigen2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                 ModoAgrupador ? item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                 CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosAnteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosAnteriores.Contains(j))))

                //                                 where E == elementoDestino && item == elementoOrigen select E).FirstOrDefault();


                var elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores)
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY
                                                 select E).FirstOrDefault();

                var elementoOrigenEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores)
                                                    //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY
                                                select E).FirstOrDefault();

                var elementoDestino2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores)
                                                      //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                  where E == elementoOrigen && item == elementoDestino
                                                  select E).FirstOrDefault();

                var elementoOrigen2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores)
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E == elementoDestino && item == elementoOrigen
                                                 select E).FirstOrDefault();

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

        private ArrowLine BuscarLineaAgrupador(DiseñoOperacion Agrupador, DiseñoOperacion elementoOrigen, DiseñoOperacion elementoDestino)
        {
            foreach (var item in Agrupador.ElementosAgrupados)
            {
                //var elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                 ModoAgrupador ? item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                 CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones && 
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosAnteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosAnteriores.Contains(j))))

                //                                 where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY select E).FirstOrDefault();

                //var elementoOrigenEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                ModoAgrupador ? item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosPosteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosPosteriores.Contains(j))))

                //                                where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY select E).FirstOrDefault();

                //var elementoDestino2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                  ModoAgrupador ? item.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                  CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosPosteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosPosteriores.Contains(j))))

                //                                  where E == elementoOrigen && item == elementoDestino select E).FirstOrDefault();

                //var elementoOrigen2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados.Where(i => (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)) || (!ModoAgrupador)) :
                //                                 ModoAgrupador ? item.ElementosAnteriores.Where(i => i.Tipo != TipoElementoOperacion.AgrupadorOperaciones && Agrupador.ElementosAgrupados.Contains(i)) : 
                //                                 CalculoDiseñoSeleccionado.ElementosOperaciones.Where(j => (j.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                //                                 j.TodosElementosAgrupados.Any(h => item.ElementosAnteriores.Contains(h))) || (j.Tipo != TipoElementoOperacion.AgrupadorOperaciones && item.ElementosAnteriores.Contains(j))))

                //                                 where E == elementoDestino && item == elementoOrigen select E).FirstOrDefault();


                var elementoDestinoEncontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresAnteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : Ejecucion.SubCalculoActual.ElementosOperaciones)
                                                 .Where(i => ((Agrupador.ElementosAgrupados.Contains(i))))
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY
                                                 select E).FirstOrDefault();

                var elementoOrigenEncontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresPosteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : Ejecucion.SubCalculoActual.ElementosOperaciones)
                                                .Where(i => (Agrupador.ElementosAgrupados.Contains(i)))
                                                    //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY
                                                select E).FirstOrDefault();

                var elementoDestino2Encontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresPosteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : Ejecucion.SubCalculoActual.ElementosOperaciones)
                                                  .Where(i => (Agrupador.ElementosAgrupados.Contains(i)))
                                                      //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                  where E == elementoOrigen && item == elementoDestino
                                                  select E).FirstOrDefault();

                var elementoOrigen2Encontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresAnteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : Ejecucion.SubCalculoActual.ElementosOperaciones)
                                                 .Where(i => (Agrupador.ElementosAgrupados.Contains(i)))
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E == elementoDestino && item == elementoOrigen
                                                 select E).FirstOrDefault();

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
                ref double posicionXDestino, ref double posicionYDestino, DiseñoOperacion elementoOrigen,
                DiseñoOperacion elementoDestino)
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

        private void EstablecerIndicesProfundidadElementos(Canvas diagramaCalculo)
        {
            int indice = 0;
            
            var elementos = (from UIElement E in diagramaCalculo.Children where E.GetType() != typeof(ArrowLine) select E).ToList();
            foreach (var item in elementos)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }

            var lineas = (from UIElement L in diagramaCalculo.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var item in lineas)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }
        }

        private void EstablecerIndicesProfundidadElementos_Agrupador(Canvas diagramaAgrupador)
        {
            int indice = 0;

            var elementos = (from UIElement E in diagramaAgrupador.Children where E.GetType() != typeof(ArrowLine) select E).ToList();
            foreach (var item in elementos)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }

            var lineas = (from UIElement L in diagramaAgrupador.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var item in lineas)
            {
                Canvas.SetZIndex(item, indice);
                indice++;
            }
        }

        private void cerrarFinalizado_Click(object sender, RoutedEventArgs e)
        {
            Ventana.QuitarTabActual();
        }

        private void verResultados_Click(object sender, RoutedEventArgs e)
        {
            TabItem nuevaTab = new TabItem();

            VistaInformeResultados resultados = new VistaInformeResultados();
            resultados.Ventana = Ventana;
            resultados.Ejecucion = Ejecucion;

            nuevaTab.Content = resultados;
            Ventana.contenido.Items.Add(nuevaTab);

            nuevaTab.Header = "Variables o vectores retornados de la ejecución de " + Ejecucion.Calculo.NombreArchivo;
            resultados.nombreArchivo.Content = Ejecucion.Calculo.NombreArchivo;
            if (!string.IsNullOrEmpty(Ejecucion.Calculo.RutaArchivo))
            {
                resultados.rutaArchivo.Content = Ejecucion.Calculo.RutaArchivo;
            }

            cerrarFinalizado_Click(this, new RoutedEventArgs());
            Ventana.contenido.SelectedItem = nuevaTab;
        }

        public void DibujarLineasElemento_Calculo(DiseñoCalculo item)
        {
            foreach (var itemElemento in item.ElementosPosteriores)
            {
                if (itemElemento.EsEntradasArchivo) continue;

                ArrowLine nuevaLinea = BuscarLinea_Calculo(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagramaCalculos.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    if (lineaEncontrada == null)
                        diagramaCalculos.Children.Add(nuevaLinea);
                }

            }

            foreach (var itemElemento in item.ElementosAnteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea_Calculo(item, itemElemento);
                if (nuevaLinea != null)
                {
                    var lineaEncontrada = (from UIElement L in diagramaCalculos.Children
                                           where (L.GetType() == typeof(ArrowLine)) &&
          (((ArrowLine)L).X1 == nuevaLinea.X1 & ((ArrowLine)L).Y1 == nuevaLinea.Y1 &
          ((ArrowLine)L).X2 == nuevaLinea.X2 & ((ArrowLine)L).Y2 == nuevaLinea.Y2)
                                           select L).FirstOrDefault();

                    if (lineaEncontrada == null)
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

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }
    }
}
