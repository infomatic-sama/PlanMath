using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using ProcessCalc.Controles;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using static PlanMath_para_Excel.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using System.Xml.Linq;
using System.Threading;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Input;
using ProcessCalc.Entidades.Operaciones;

namespace ProcessCalc
{
    /// <summary>
    /// Propiedades y lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int cantidadCalculos { get; set; }
        public List<Calculo> Calculos { get; set; }
        public List<VistaDiseñoOperaciones> vistaOperaciones = new List<VistaDiseñoOperaciones>();
        public List<VistaTextosInformacion> vistaTextosInformacion = new List<VistaTextosInformacion>();
        public List<VistaEjecucionCalculo> ejecuciones = new List<VistaEjecucionCalculo>();

        private List<VistaDiseñoCalculos> vistaCalculos = new List<VistaDiseñoCalculos>();
        public List<InfoCalculo> vistasInfoCalculo = new List<InfoCalculo>();
        public DiseñoOperacion Elemento_AgregarImplicacionTextos { get; set; }
        public DiseñoElementoOperacion ElementoDiseño_AgregarImplicacionTextos { get; set; }
        public VistaDiseñoOperacion_TextosInformacion vistaSeleccionada_ElementoDiseño_BotonImplicacion;
        public DiseñoCalculo Calculo_AgregarImplicacionTextos { get; set; }
        public void AgregarTab()
        {
            TabItem nuevaTab = new TabItem();
            cantidadCalculos++;
            InfoCalculo infoCalculo = new InfoCalculo();
            Calculo nuevoCalculo = new Calculo();
            nuevoCalculo.ID = App.GenerarID_Elemento();
            nuevoCalculo.VersionAplicacion = Application.ResourceAssembly.GetName().Version.ToString();
            nuevoCalculo.NombreArchivo = "NuevoCalculo" + cantidadCalculos + ".pmcalc";
            Calculos.Add(nuevoCalculo);
            infoCalculo.Calculo = nuevoCalculo;
            nuevaTab.Content = infoCalculo;
            nuevaTab.Header = nuevoCalculo.NombreArchivo;
            infoCalculo.nombreArchivo.Content = nuevaTab.Header;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("NuevoArchivoCalculo");
#endif

            btnCalculos_Click(this, null);
            btnOperaciones_Click(this, null);

            AgregarEjecucionToolTip(nuevoCalculo);
        }

        public void AgregarTabArchivo(Calculo calculo)
        {
            TabItem nuevaTab = new TabItem();
            Calculos.Add(calculo);
            VistaCalculo vistaCalculo = new VistaCalculo
            {
                Calculo = calculo
            };
            vistaCalculo.Ventana = this;

            if (calculo.SubCalculoSeleccionado_VistaCalculo == null)
                calculo.SubCalculoSeleccionado_VistaCalculo = calculo.Calculos.FirstOrDefault();

            nuevaTab.Content = vistaCalculo;
            nuevaTab.Header = calculo.NombreArchivo + " - Información";
            vistaCalculo.nombreArchivo.Content = calculo.NombreArchivo;
            vistaCalculo.rutaArchivo.Content = calculo.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;
        }
        public void QuitarTabActual()
        {
            Cerrando = true;

            if (contenido.SelectedItem != null)
            {
                if (((TabItem)contenido.SelectedItem).Content != null && (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(InfoCalculo) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaResultados) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos)) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacion))
                {
                    QuitarEjecucionToolTip(CalculoActual);
                    Calculos.Remove(CalculoActual);
                    App.QuitarArchivoAbierto(CalculoActual);

                    List<TabItem> itemsVistasCalculo;
                    itemsVistasCalculo = (from TabItem T in contenido.Items where T.Content != null &&
                                          ((T.Content.GetType() == typeof(VistaArchivoEntradaNumero) && ((VistaArchivoEntradaNumero)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaConjuntoNumerosEntrada) && ((VistaConjuntoNumerosEntrada)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaTextosInformacionEntrada) && ((VistaTextosInformacionEntrada)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) && ((VistaArchivoEntradaConjuntoNumeros)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) && ((VistaArchivoEntradaTextosInformacion)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaDiseñoOperacion) && ((VistaDiseñoOperacion)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaURLEntradaNumero) && ((VistaURLEntradaNumero)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) && ((VistaURLEntradaConjuntoNumeros)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaURLEntradaTextosInformacion) && ((VistaURLEntradaTextosInformacion)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) && ((VistaArchivoExcelEntradaNumero)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) && ((VistaArchivoExcelEntradaConjuntoNumeros)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) && ((VistaArchivoExcelEntradaTextosInformacion)T.Content).Calculo == CalculoActual) |
                                          (T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) && ((VistaDiseñoOperacion_TextosInformacion)T.Content).Calculo == CalculoActual)) select T).ToList();
                    while (itemsVistasCalculo.Count > 0)
                    {
                        contenido.Items.Remove(itemsVistasCalculo.First());
                        itemsVistasCalculo.Remove(itemsVistasCalculo.First());
                    }

                    if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones)) vistaOperaciones.Remove(((VistaDiseñoOperaciones)((TabItem)contenido.SelectedItem).Content));
                    if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos)) vistaCalculos.Remove(((VistaDiseñoCalculos)((TabItem)contenido.SelectedItem).Content));
                    if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacion)) vistaTextosInformacion.Remove(((VistaTextosInformacion)((TabItem)contenido.SelectedItem).Content));
                }
            }

            if((((ContentControl)((TabItem)contenido.SelectedItem).Content)).Content.GetType() != typeof(VistaInicio))
                contenido.Items.Remove(contenido.SelectedItem);

            Cerrando = false;
        }

        public void QuitarTab(ref TabItem tab)
        {
            contenido.Items.Remove(tab);
        }

        public void MarcarBotonCalculo(OpcionSeleccionada opcion)
        {
            btnEditarInfo.Background = System.Windows.Media.Brushes.Transparent;
            btnEditarInfo.BorderBrush = System.Windows.Media.Brushes.Transparent;
            btnInfo.Background = System.Windows.Media.Brushes.Transparent;
            btnInfo.BorderBrush = System.Windows.Media.Brushes.Transparent;
            btnEntradas.Background = System.Windows.Media.Brushes.Transparent;
            btnEntradas.BorderBrush = System.Windows.Media.Brushes.Transparent;
            btnOperaciones.Background = System.Windows.Media.Brushes.Transparent;
            btnOperaciones.BorderBrush = System.Windows.Media.Brushes.Transparent;
            btnResultados.Background = System.Windows.Media.Brushes.Transparent;
            btnResultados.BorderBrush = System.Windows.Media.Brushes.Transparent;
            btnCalculos.Background = System.Windows.Media.Brushes.Transparent;
            btnCalculos.BorderBrush = System.Windows.Media.Brushes.Transparent;
            btnTextosInformacion.Background = System.Windows.Media.Brushes.Transparent;
            btnTextosInformacion.BorderBrush = System.Windows.Media.Brushes.Transparent;

            if (CalculoActual == null) return;

            if (((TabItem)contenido.SelectedItem).Content != null && (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(InfoCalculo) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaResultados) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacion)))
            {
                switch (opcion)
                {
                    case OpcionSeleccionada.EditarInfo:
                        btnEditarInfo.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnEditarInfo.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                    case OpcionSeleccionada.Informacion:
                        btnInfo.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnInfo.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                    case OpcionSeleccionada.Entradas:
                        btnEntradas.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnEntradas.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                    case OpcionSeleccionada.Operaciones:
                        btnOperaciones.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnOperaciones.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                    case OpcionSeleccionada.Resultados:
                        btnResultados.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnResultados.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                    case OpcionSeleccionada.Calculos:
                        btnCalculos.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnCalculos.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                    case OpcionSeleccionada.TextosInformacion:
                        btnTextosInformacion.Background = System.Windows.SystemColors.InactiveBorderBrush;
                        btnTextosInformacion.BorderBrush = System.Windows.Media.Brushes.Black;
                        break;
                }
            }
        }

        public bool VerificaArchivoAbierto_ConId(string idCalculo)
        {
            if (!Dispatcher.HasShutdownStarted &&
                            !Dispatcher.HasShutdownFinished)
            {
                return Dispatcher.Invoke(() =>
                {
                    foreach (var ventana in App.Current.Windows)
                    {
                        if (ventana.GetType() == typeof(MainWindow))
                        {
                            if (((MainWindow)ventana).Calculos.Any(i => i.ID == idCalculo))
                                return true;                            
                        }
                    }

                    return false;
                });
            }
            else
                return false;
        }

        public void MostrarVista(OpcionSeleccionada opcion)
        {
            if (CalculoActual == null) return;

            TabItem tabActual;
            switch (opcion)
            {
                case OpcionSeleccionada.EditarInfo:
                    //tabActual = (TabItem)contenido.SelectedItem;
                    tabActual = (from TabItem T in contenido.Items
                                 where T.Content != null &&
((T.Content.GetType() == typeof(InfoCalculo) && ((InfoCalculo)T.Content).Calculo == CalculoActual)) select T).FirstOrDefault();

                    if (tabActual == null)
                    {
                        tabActual = (TabItem)contenido.SelectedItem;
                    }
                    else
                    {
                        contenido.SelectedItem = tabActual;
                    }


                    var infoCalculo = (from InfoCalculo V in vistasInfoCalculo where V.Calculo == CalculoActual select V).FirstOrDefault();

                    if (infoCalculo == null)
                    {
                        infoCalculo = new InfoCalculo();
                        infoCalculo.Calculo = CalculoActual;
                        vistasInfoCalculo.Add(infoCalculo);
                    }

                    tabActual.Content = infoCalculo;

                    if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                    {
                        tabActual.Header = CalculoActual.NombreArchivo + " - Editar información";
                        infoCalculo.nombreArchivo.Content = CalculoActual.NombreArchivo;
                        infoCalculo.rutaArchivo.Content = CalculoActual.RutaArchivo;
                    }

                    if (CalculoActual.TabSeleccionada_TextosInformacion > -1)
                        infoCalculo.contenido.SelectedIndex = CalculoActual.TabSeleccionada_TextosInformacion;

                    break;
                case OpcionSeleccionada.Informacion:
                    //tabActual = (TabItem)contenido.SelectedItem;

                    tabActual = (from TabItem T in contenido.Items
                                 where T.Content != null &&
((T.Content.GetType() == typeof(VistaCalculo) && ((VistaCalculo)T.Content).Calculo == CalculoActual))
                                 select T).FirstOrDefault();

                    if (tabActual == null)
                    {
                        tabActual = (TabItem)contenido.SelectedItem;
                    }
                    else
                    {
                        contenido.SelectedItem = tabActual;
                    }

                    VistaCalculo vistaCalculo = new VistaCalculo();
                    vistaCalculo.Ventana = this;
                    vistaCalculo.Calculo = CalculoActual;

                    if (CalculoActual.SubCalculoSeleccionado_VistaCalculo == null)
                        CalculoActual.SubCalculoSeleccionado_VistaCalculo = CalculoActual.Calculos.FirstOrDefault();

                    tabActual.Content = vistaCalculo;
                    if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                    {
                        tabActual.Header = CalculoActual.NombreArchivo + " - Información";
                        vistaCalculo.nombreArchivo.Content = CalculoActual.NombreArchivo;
                        vistaCalculo.rutaArchivo.Content = CalculoActual.RutaArchivo;
                    }
                    break;
                case OpcionSeleccionada.Entradas:
                    //tabActual = (TabItem)contenido.SelectedItem;

                    tabActual = (from TabItem T in contenido.Items
                                 where T.Content != null &&
((T.Content.GetType() == typeof(Entradas) && ((Entradas)T.Content).Calculo == CalculoActual))
                                 select T).FirstOrDefault();

                    if (tabActual == null)
                    {
                        tabActual = (TabItem)contenido.SelectedItem;
                    }
                    else
                    {
                        contenido.SelectedItem = tabActual;
                    }

                    Entradas vistaEntradas = new Entradas();
                    vistaEntradas.Ventana = this;

                    if (CalculoActual.SubCalculoSeleccionado_Entradas == null)
                        CalculoActual.SubCalculoSeleccionado_Entradas = CalculoActual.Calculos.FirstOrDefault(i => i.EsEntradasArchivo);

                    vistaEntradas.Calculo = CalculoActual;

                    tabActual.Content = vistaEntradas;
                    if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                    {
                        tabActual.Header = CalculoActual.NombreArchivo + " - Entradas";
                        vistaEntradas.nombreArchivo.Content = CalculoActual.NombreArchivo;
                        vistaEntradas.rutaArchivo.Content = CalculoActual.RutaArchivo;
                    }
                    break;
                case OpcionSeleccionada.Operaciones:
                    //tabActual = (TabItem)contenido.SelectedItem;

                    tabActual = (from TabItem T in contenido.Items
                                 where T.Content != null &&
((T.Content.GetType() == typeof(VistaDiseñoOperaciones) && ((VistaDiseñoOperaciones)T.Content).Calculo == CalculoActual))
                                 select T).FirstOrDefault();

                    if (tabActual == null)
                    {
                        tabActual = (TabItem)contenido.SelectedItem;
                    }
                    else
                    {
                        contenido.SelectedItem = tabActual;
                    }

                    bool primerClicCalculo = false;

                    var vista = (from VistaDiseñoOperaciones V in vistaOperaciones where V.Calculo == CalculoActual select V).FirstOrDefault();

                    if (vista == null)
                    {
                        primerClicCalculo = true;
                        vista = new VistaDiseñoOperaciones();
                        vista.Ventana = this;
                        vista.Calculo = CalculoActual;
                        vistaOperaciones.Add(vista);
                    }

                    tabActual.Content = vista;
                    if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                    {
                        tabActual.Header = CalculoActual.NombreArchivo + " - Operaciones";
                        vista.nombreArchivo.Content = CalculoActual.NombreArchivo;
                        vista.rutaArchivo.Content = CalculoActual.RutaArchivo;
                    }
                    if (primerClicCalculo ||
                        vista.CalculoClikeado)
                    {
                        if (CalculoActual.SubCalculoSeleccionado_Operaciones == null)
                            CalculoActual.SubCalculoSeleccionado_Operaciones = (from C in CalculoActual.Calculos where !C.EsEntradasArchivo select C).FirstOrDefault();

                        vista.CalculoDiseñoSeleccionado = CalculoActual.SubCalculoSeleccionado_Operaciones;
                        vista.ListarEntradas();
                        vista.DibujarElementosOperaciones();

                        vista.diagrama.Width = vista.CalculoDiseñoSeleccionado.Ancho;
                        vista.diagrama.Height = vista.CalculoDiseñoSeleccionado.Alto;

                        //vista.diagrama.Width = vista.diagrama.ActualWidth + 300;
                        //vista.Calculo.Diseño.Ancho = vista.diagrama.Width;

                        //vista.diagrama.Height = vista.diagrama.ActualHeight + 300;
                        //vista.Calculo.Diseño.Alto = vista.diagrama.Height;
                        if (vista.CalculoClikeado) vista.CalculoClikeado = false;
                    }
                    break;
                case OpcionSeleccionada.TextosInformacion:
                    //tabActual = (TabItem)contenido.SelectedItem;

                    tabActual = (from TabItem T in contenido.Items
                                 where T.Content != null &&
((T.Content.GetType() == typeof(VistaTextosInformacion) && ((VistaTextosInformacion)T.Content).CalculoSeleccionado == CalculoActual))
                                 select T).FirstOrDefault();

                    if (tabActual == null)
                    {
                        tabActual = (TabItem)contenido.SelectedItem;
                    }
                    else
                    {
                        contenido.SelectedItem = tabActual;
                    }

                    primerClicCalculo = false;
                    var vistaTextos = (from VistaTextosInformacion V in vistaTextosInformacion where V.CalculoSeleccionado == CalculoActual select V).FirstOrDefault();

                    if (vistaTextos == null)
                    {
                        primerClicCalculo = true;
                        vistaTextos = new VistaTextosInformacion();
                        vistaTextos.Ventana = this;
                        vistaTextos.CalculoSeleccionado = CalculoActual;
                        //vistaTextos.CalculoDiseñoSeleccionado = vistaTextos.CalculoSeleccionado.TextosInformacion;
                        vistaTextosInformacion.Add(vistaTextos);
                    }

                    tabActual.Content = vistaTextos;

                    if (primerClicCalculo ||
                        vistaTextos.CalculoClikeado)
                    {
                        if (CalculoActual.SubCalculoSeleccionado_TextosInformacion == null)
                            CalculoActual.SubCalculoSeleccionado_TextosInformacion = (from C in CalculoActual.Calculos where !C.EsEntradasArchivo select C).FirstOrDefault();

                        vistaTextos.CalculoDiseñoSeleccionado_Cantidades = CalculoActual.SubCalculoSeleccionado_TextosInformacion;
                        vistaTextos.ListarEntradas();
                        vistaTextos.DibujarElementosTextosInformacion(vistaTextos.CalculoDiseñoSeleccionado_Cantidades);

                        vistaTextos.diagrama.Width = vistaTextos.CalculoDiseñoSeleccionado_Cantidades.Ancho;
                        vistaTextos.diagrama.Height = vistaTextos.CalculoDiseñoSeleccionado_Cantidades.Alto;

                        if (vistaTextos.CalculoClikeado) vistaTextos.CalculoClikeado = false;
                    }

                    if (Elemento_AgregarImplicacionTextos != null &
                        ElementoDiseño_AgregarImplicacionTextos == null &
                        Calculo_AgregarImplicacionTextos != null)
                    {
                        var CalculoSeleccionadoOriginal = vistaTextos.CalculoDiseñoSeleccionado_Cantidades;
                        vistaTextos.CalculoDiseñoSeleccionado_Cantidades = Calculo_AgregarImplicacionTextos;

                        vistaTextos.UserControl_Loaded(this, null);
                        Thread.Sleep(300);

                        var definiciones = vistaTextos.AgregarImplicacionElemento_DesdeOperaciones(Elemento_AgregarImplicacionTextos, Calculo_AgregarImplicacionTextos);
                        
                        foreach (var definicion in definiciones)
                        {
                            definicion.Clic(true);
                            Thread.Sleep(300);

                            if (Elemento_AgregarImplicacionTextos.DefinicionSimple_Operacion)
                                vistaTextos.btnDefinicionSimple_RelacionesTextosInformacion_Click(this, null);
                            else
                                vistaTextos.btnDefinicionNormal_RelacionesTextosInformacion_Click(this, null);

                            Thread.Sleep(300);
                        }

                        if (Elemento_AgregarImplicacionTextos.DefinicionSimple_Operacion)
                            btnOperaciones_Click(this, null);

                        vistaTextos.CalculoDiseñoSeleccionado_Cantidades = CalculoSeleccionadoOriginal;
                    }

                    if (Elemento_AgregarImplicacionTextos != null &
                        ElementoDiseño_AgregarImplicacionTextos != null &
                        Calculo_AgregarImplicacionTextos != null)
                    {
                        var CalculoSeleccionadoOriginal = vistaTextos.CalculoDiseñoSeleccionado_Cantidades;
                        vistaTextos.CalculoDiseñoSeleccionado_Cantidades = Calculo_AgregarImplicacionTextos;

                        vistaTextos.UserControl_Loaded(this, null);
                        Thread.Sleep(300);

                        var definiciones = vistaTextos.AgregarImplicacionElemento_DesdeOperaciones(Elemento_AgregarImplicacionTextos, Calculo_AgregarImplicacionTextos);

                        foreach (var definicion in definiciones)
                        {
                            definicion.Clic(true);
                            Thread.Sleep(300);

                            vistaTextos.btnDefinicionNormal_RelacionesTextosInformacion_Click(this, null);

                            Thread.Sleep(300);

                            var def = definicion.DiseñoTextosInformacion;
                            AgregarTabDiseñoOperacion_TextosInformacion(ref def);

                            if (vistaSeleccionada_ElementoDiseño_BotonImplicacion != null)
                            {
                                var definicionesInternas = vistaSeleccionada_ElementoDiseño_BotonImplicacion.AgregarImplicacionElementoDiseño_DesdeOperaciones(ElementoDiseño_AgregarImplicacionTextos);

                                foreach (var definicionInterna in definicionesInternas)
                                {
                                    if(definicionInterna.GetType() == typeof(EntradaDiseñoOperaciones))
                                    {
                                        vistaSeleccionada_ElementoDiseño_BotonImplicacion.ElementoSeleccionado = ((EntradaDiseñoOperaciones)definicionInterna).DiseñoElementoOperacion;
                                    }

                                    else if (definicionInterna.GetType() == typeof(EntradaFlujoOperacion))
                                    {
                                        vistaSeleccionada_ElementoDiseño_BotonImplicacion.ElementoSeleccionado = ((EntradaFlujoOperacion)definicionInterna).DiseñoElementoOperacion;
                                    }

                                    else if (definicionInterna.GetType() == typeof(OpcionOperacion))
                                    {
                                        vistaSeleccionada_ElementoDiseño_BotonImplicacion.ElementoSeleccionado = ((OpcionOperacion)definicionInterna).DiseñoElementoOperacion;
                                    }

                                    Thread.Sleep(300);

                                    vistaSeleccionada_ElementoDiseño_BotonImplicacion.btnDefinirRelaciones_TextosInformacion_Click(this, null);

                                    Thread.Sleep(300);
                                    btnCerrar_Click(this, null);
                                }

                                Thread.Sleep(300);
                                btnCerrar_Click(this, null);
                            }
                        }

                        Thread.Sleep(300);
                        btnCerrar_Click(this, null);

                        var tab = (from TabItem T in contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperacion) && ((VistaDiseñoOperacion)T.Content).Operacion == Elemento_AgregarImplicacionTextos) select T).FirstOrDefault();
                        if (tab != null)
                        {
                            contenido.SelectedItem = tab;
                        }
                        else
                        {
                            var elemento = Elemento_AgregarImplicacionTextos;
                            AgregarTabDiseñoOperacionConjuntoNumeros(ref elemento, Calculo_AgregarImplicacionTextos);                            
                        }

                        //btnOperaciones_Click(this, null);

                        vistaTextos.CalculoDiseñoSeleccionado_Cantidades = CalculoSeleccionadoOriginal;
                    }
                    
                    break;
                case OpcionSeleccionada.Resultados:
                    //tabActual = (TabItem)contenido.SelectedItem;

                    tabActual = (from TabItem T in contenido.Items
                                 where T.Content != null &&
((T.Content.GetType() == typeof(VistaResultados) && ((VistaResultados)T.Content).Calculo == CalculoActual))
                                 select T).FirstOrDefault();

                    if (tabActual == null)
                    {
                        tabActual = (TabItem)contenido.SelectedItem;
                    }
                    else
                    {
                        contenido.SelectedItem = tabActual;
                    }

                    VistaResultados resultados = new VistaResultados();
                    resultados.Calculo = CalculoActual;
                    tabActual.Content = resultados;
                    if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                    {
                        tabActual.Header = CalculoActual.NombreArchivo + " - Resultados";
                        resultados.nombreArchivo.Content = CalculoActual.NombreArchivo;
                        resultados.rutaArchivo.Content = CalculoActual.RutaArchivo;
                    }
                    break;
                case OpcionSeleccionada.Ejecutar:
                    if (CalculoActual != null)
                    {
                        TabItem nuevaTab = new TabItem();

                        VistaEjecucionCalculo ejecucion = new VistaEjecucionCalculo();
                        ejecucion.Ventana = this;
                        ejecucion.Ejecucion = new EjecucionCalculo();
                        ejecucion.Ejecucion.Calculo = CalculoActual.ReplicarObjeto();
                        ejecucion.Ejecucion.Ventana = this;

                        ejecuciones.Add(ejecucion);
                        nuevaTab.Content = ejecucion;

                        nuevaTab.Header = "Ejecución de " + ejecucion.Ejecucion.Calculo.NombreArchivo;
                        ejecucion.nombreArchivo.Content = ejecucion.Ejecucion.Calculo.NombreArchivo;
                        if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                        {
                            ejecucion.rutaArchivo.Content = ejecucion.Ejecucion.Calculo.RutaArchivo;
                        }

                        contenido.Items.Add(nuevaTab);
                        contenido.SelectedItem = nuevaTab;
                    }
                    break;

                case OpcionSeleccionada.Calculos:
                    //tabActual = (TabItem)contenido.SelectedItem;

                    if (!CalculoActual.ModoSubCalculo || (CalculoActual.ModoSubCalculo &&
                        !CalculoActual.ModoSubCalculo_Simple))
                    {
                        tabActual = (from TabItem T in contenido.Items
                                     where T.Content != null &&
    ((T.Content.GetType() == typeof(VistaDiseñoCalculos) && ((VistaDiseñoCalculos)T.Content).Calculo == CalculoActual))
                                     select T).FirstOrDefault();

                        if (tabActual == null)
                        {
                            tabActual = (TabItem)contenido.SelectedItem;
                        }
                        else
                        {
                            contenido.SelectedItem = tabActual;
                        }

                        bool primerClicCalculo_Calculo = false;

                        var vista_Calculo = (from VistaDiseñoCalculos V in vistaCalculos where V.Calculo == CalculoActual select V).FirstOrDefault();

                        if (vista_Calculo == null)
                        {
                            primerClicCalculo_Calculo = true;
                            vista_Calculo = new VistaDiseñoCalculos();
                            vista_Calculo.Ventana = this;
                            vista_Calculo.Calculo = CalculoActual;
                            vistaCalculos.Add(vista_Calculo);
                        }

                        vista_Calculo.primerClicCalculo_Calculo = primerClicCalculo_Calculo;
                        tabActual.Content = vista_Calculo;
                        if (!string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                        {
                            tabActual.Header = CalculoActual.NombreArchivo + " - Cálculos";
                            vista_Calculo.nombreArchivo.Content = CalculoActual.NombreArchivo;
                            vista_Calculo.rutaArchivo.Content = CalculoActual.RutaArchivo;
                        }
                        if (primerClicCalculo_Calculo)
                        {
                            if (CalculoActual.SubCalculoSeleccionado == null)
                                CalculoActual.SubCalculoSeleccionado = CalculoActual.Calculos.FirstOrDefault();

                            //vista_Calculo.CalculoDiseñoSeleccionado = CalculoActual.SubCalculoSeleccionado;
                            vista_Calculo.DibujarElementosCalculos();

                            vista_Calculo.diagrama.Width = vista_Calculo.Calculo.Ancho;
                            vista_Calculo.diagrama.Height = vista_Calculo.Calculo.Alto;
                        }

                        if (!CalculoActual.Calculos.Any(i => !i.EsEntradasArchivo))
                            vista_Calculo.btnAgregarCalculo_Click(this, null);
                    }

                    break;
            }
        }

        public void EstablecerTituloVentana()
        {
            if (CalculoActual == null)
            {
                Title = "PlanMath";
            }
            else
            {
                Title = "PlanMath - " + CalculoActual.NombreArchivo;
            }
        }

        public void AgregarTabEntradaArchivo(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            TabItem nuevaTab = new TabItem();

            if(!entrada.ListaArchivos.Any())
                entrada.ListaArchivos.Add(new Entidades.Entradas.ConfiguracionArchivoEntrada()
                {
                    EntradaManual = entrada.EntradaArchivoManual
                });

            VistaArchivoEntradaNumero vistaArchivo = new VistaArchivoEntradaNumero
            {
                Entrada = entrada
            };
            vistaArchivo.Calculo = CalculoActual;
            vistaArchivo.VistaEntrada = vistaEntrada;
            vistaArchivo.busquedaNumero.Busqueda = entrada.BusquedaNumero;
            nuevaTab.Content = vistaArchivo;
            nuevaTab.Header = "Obtener número para " + CalculoActual.NombreArchivo;
            vistaArchivo.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaArchivo.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;
        }

        public void AgregarTabEntradaURL(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            Cursor = System.Windows.Input.Cursors.Wait;
            TabItem nuevaTab = new TabItem();
            VistaURLEntradaNumero vistaURL = new VistaURLEntradaNumero
            {
                Entrada = entrada
            };
            vistaURL.Calculo = CalculoActual;
            vistaURL.VistaEntrada = vistaEntrada;
            vistaURL.busquedaNumero.Busqueda = entrada.BusquedaNumero;
            nuevaTab.Content = vistaURL;
            nuevaTab.Header = "Obtener número para " + CalculoActual.NombreArchivo;
            vistaURL.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaURL.rutaArchivo.Content = CalculoActual.RutaArchivo;
            
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            if (!string.IsNullOrEmpty(entrada.ListaURLs.FirstOrDefault().URLEntrada))
            {
                vistaURL.txtURL.Text = entrada.ListaURLs.FirstOrDefault().URLEntrada;
                vistaURL.btnObtenerDeURL_Click(this, null);
            }
            Cursor = System.Windows.Input.Cursors.Arrow;
        }

        public void AgregarTabEntradaArchivo_Excel(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            TabItem nuevaTab = new TabItem();

            if (!entrada.ListaArchivos.Any())
                entrada.ListaArchivos.Add(new Entidades.Entradas.ConfiguracionArchivoEntrada()
                {
                    EntradaManual = entrada.EntradaArchivoManual
                });

            VistaArchivoExcelEntradaNumero vistaArchivo = new VistaArchivoExcelEntradaNumero
            {
                Entrada = entrada
            };
            vistaArchivo.Calculo = CalculoActual;
            vistaArchivo.VistaEntrada = vistaEntrada;
            nuevaTab.Content = vistaArchivo;
            nuevaTab.Header = "Obtener número para " + CalculoActual.NombreArchivo;
            vistaArchivo.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaArchivo.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;
        }

        public void AgregarTabConjuntoNumerosEntrada(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            if (entrada.ConjuntoNumerosFijo.Count == 0)
            {
                entrada.ConjuntoNumerosFijo.Add(new EntidadNumero("Número 1", 0));
            }

            TabItem nuevaTab = new TabItem();
            VistaConjuntoNumerosEntrada vistaConjuntoNums = new VistaConjuntoNumerosEntrada
            {
                Entrada = entrada,
                VistaEntrada = vistaEntrada
            };
            vistaConjuntoNums.Calculo = CalculoActual;
            //vistaConjuntoNums.VistaEntrada = vistaEntrada;
            //vistaConjuntoNums.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaConjuntoNums;
            nuevaTab.Header = "Obtener conjunto de números para " + CalculoActual.NombreArchivo;
            vistaConjuntoNums.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaConjuntoNums.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            vistaConjuntoNums.ListarNumeros();
        }

        public void AgregarTabTextosInformacionEntrada(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            //if (entrada.TextosInformacionFijos.Count == 0)
            //{
            //    entrada.TextosInformacionFijos.Add("Texto 1");
            //}

            TabItem nuevaTab = new TabItem();
            VistaTextosInformacionEntrada vistaTextosInformacion = new VistaTextosInformacionEntrada
            {
                Entrada = entrada,
                VistaEntrada = vistaEntrada
            };
            vistaTextosInformacion.Calculo = CalculoActual;
            //vistaConjuntoNums.VistaEntrada = vistaEntrada;
            //vistaConjuntoNums.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaTextosInformacion;
            nuevaTab.Header = "Obtener textos de información para " + CalculoActual.NombreArchivo;
            vistaTextosInformacion.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaTextosInformacion.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            //vistaTextosInformacion.ListarNumeros();
        }

        public void AgregarTabEntradaArchivoConjuntoNumeros(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            if (entrada.BusquedasConjuntoNumeros.Count == 0)
            {
                entrada.BusquedasConjuntoNumeros.Add(new BusquedaTextoArchivo("Búsqueda 1", string.Empty));
            }

            TabItem nuevaTab = new TabItem();
            VistaArchivoEntradaConjuntoNumeros vistaArchivo = new VistaArchivoEntradaConjuntoNumeros
            {
                Entrada = entrada
            };
            vistaArchivo.Calculo = CalculoActual;
            vistaArchivo.VistaEntrada = vistaEntrada;
            //vistaArchivo.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaArchivo;
            nuevaTab.Header = "Obtener conjunto de números para " + CalculoActual.NombreArchivo;
            vistaArchivo.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaArchivo.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            vistaArchivo.ListarBusquedas();
        }

        public void AgregarTabEntradaArchivoTextosInformacion(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            if (entrada.BusquedasTextosInformacion.Count == 0)
            {
                entrada.BusquedasTextosInformacion.Add(new BusquedaTextoArchivo("Búsqueda 1", string.Empty));
            }

            TabItem nuevaTab = new TabItem();
            VistaArchivoEntradaTextosInformacion vistaArchivo = new VistaArchivoEntradaTextosInformacion
            {
                Entrada = entrada
            };
            vistaArchivo.Calculo = CalculoActual;
            vistaArchivo.VistaEntrada = vistaEntrada;
            //vistaArchivo.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaArchivo;
            nuevaTab.Header = "Obtener textos de información para " + CalculoActual.NombreArchivo;
            vistaArchivo.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaArchivo.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            vistaArchivo.ListarBusquedas();
        }

        public void AgregarTabEntradaURLConjuntoNumeros(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            Cursor = System.Windows.Input.Cursors.Wait;

            if (entrada.BusquedasConjuntoNumeros.Count == 0)
            {
                entrada.BusquedasConjuntoNumeros.Add(new BusquedaTextoArchivo("Búsqueda 1", string.Empty));
            }

            TabItem nuevaTab = new TabItem();
            VistaURLEntradaConjuntoNumeros vistaURL = new VistaURLEntradaConjuntoNumeros
            {
                Entrada = entrada
            };
            vistaURL.Calculo = CalculoActual;
            vistaURL.VistaEntrada = vistaEntrada;
            //vistaArchivo.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaURL;
            nuevaTab.Header = "Obtener conjunto de números para " + CalculoActual.NombreArchivo;
            vistaURL.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaURL.rutaArchivo.Content = CalculoActual.RutaArchivo;
            
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            //if (!string.IsNullOrEmpty(entrada.URLEntrada))
            //{
            //    vistaURL.txtURL.Text = entrada.URLEntrada;
            //    vistaURL.btnObtenerDeURL_Click(this, null);
            //}

            vistaURL.ListarBusquedas();

            Cursor = System.Windows.Input.Cursors.Arrow;
        }

        public void AgregarTabEntradaURLTextosInformacion(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            Cursor = System.Windows.Input.Cursors.Wait;

            if (entrada.BusquedasTextosInformacion.Count == 0)
            {
                entrada.BusquedasTextosInformacion.Add(new BusquedaTextoArchivo("Búsqueda 1", string.Empty));
            }

            TabItem nuevaTab = new TabItem();
            VistaURLEntradaTextosInformacion vistaURL = new VistaURLEntradaTextosInformacion
            {
                Entrada = entrada
            };
            vistaURL.Calculo = CalculoActual;
            vistaURL.VistaEntrada = vistaEntrada;
            //vistaArchivo.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaURL;
            nuevaTab.Header = "Obtener textos de información para " + CalculoActual.NombreArchivo;
            vistaURL.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaURL.rutaArchivo.Content = CalculoActual.RutaArchivo;

            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            //if (!string.IsNullOrEmpty(entrada.URLEntrada))
            //{
            //    vistaURL.txtURL.Text = entrada.URLEntrada;
            //    vistaURL.btnObtenerDeURL_Click(this, null);
            //}

            vistaURL.ListarBusquedas();

            Cursor = System.Windows.Input.Cursors.Arrow;
        }

        public void AgregarTabEntradaArchivo_Excel_ConjuntoNumeros(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            TabItem nuevaTab = new TabItem();

            if (!entrada.ListaArchivos.Any())
                entrada.ListaArchivos.Add(new Entidades.Entradas.ConfiguracionArchivoEntrada()
                {
                    EntradaManual = entrada.EntradaArchivoManual
                });

            VistaArchivoExcelEntradaConjuntoNumeros vistaArchivo = new VistaArchivoExcelEntradaConjuntoNumeros
            {
                Entrada = entrada
            };
            vistaArchivo.Calculo = CalculoActual;
            vistaArchivo.VistaEntrada = vistaEntrada;
            //vistaArchivo.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaArchivo;
            nuevaTab.Header = "Obtener conjunto de números para " + CalculoActual.NombreArchivo;
            vistaArchivo.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaArchivo.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;
        }

        public void AgregarTabEntradaArchivo_Excel_TextosInformacion(ref Entrada entrada, EntradaEspecifica vistaEntrada)
        {
            TabItem nuevaTab = new TabItem();
            VistaArchivoExcelEntradaTextosInformacion vistaArchivo = new VistaArchivoExcelEntradaTextosInformacion
            {
                Entrada = entrada
            };
            vistaArchivo.Calculo = CalculoActual;
            vistaArchivo.VistaEntrada = vistaEntrada;
            //vistaArchivo.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaArchivo;
            nuevaTab.Header = "Obtener textos de información para " + CalculoActual.NombreArchivo;
            vistaArchivo.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaArchivo.rutaArchivo.Content = CalculoActual.RutaArchivo;
            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;
        }

        public void AgregarTabDiseñoOperacionConjuntoNumeros(ref DiseñoOperacion operacion, DiseñoCalculo Calculo)
        {
            if (CalculoActual != null)
            {
                TabItem nuevaTab = new TabItem();
                VistaDiseñoOperacion vistaConjuntoNums = new VistaDiseñoOperacion
                {
                    Operacion = operacion,
                    CalculoSeleccionado = Calculo
                };
                vistaConjuntoNums.Calculo = CalculoActual;
                vistaConjuntoNums.Ventana = this;
                //vistaConjuntoNums.VistaEntrada = vistaEntrada;
                //vistaConjuntoNums.busquedaNumero.Entrada = entrada;
                nuevaTab.Content = vistaConjuntoNums;

                nuevaTab.Header = "Definición " + operacion.Nombre + " para " + CalculoActual.NombreArchivo;
                vistaConjuntoNums.nombreArchivo.Content = CalculoActual.NombreArchivo;
                vistaConjuntoNums.rutaArchivo.Content = CalculoActual.RutaArchivo;

                vistaConjuntoNums.nombreCalculo.Content = "Cálculo: " + CalculoActual.SubCalculoSeleccionado_Operaciones.Nombre;
                vistaConjuntoNums.nombreOperacion.Content = "Operación: " + operacion.Nombre;

                contenido.Items.Add(nuevaTab);
                contenido.SelectedItem = nuevaTab;

                vistaConjuntoNums.diagrama.Width = vistaConjuntoNums.Operacion.AnchoDiagrama;
                vistaConjuntoNums.diagrama.Height = vistaConjuntoNums.Operacion.AltoDiagrama;

                vistaConjuntoNums.ListarEntradas();
                vistaConjuntoNums.DibujarElementosDiseñoOperacion();
            }
        }

        public void AgregarTabDiseñoOperacionesAgrupador(ref DiseñoOperacion operacion, DiseñoCalculo CalculoSeleccionado, VistaDiseñoOperaciones vistaOperaciones)
        {
            TabItem nuevaTab = new TabItem();
            VistaDiseñoOperaciones vistaAgrupador = new VistaDiseñoOperaciones
            {
                Agrupador = operacion,
                ModoAgrupador = true,
                CalculoDiseñoSeleccionado = CalculoSeleccionado,
                VistaOperaciones = vistaOperaciones
            };
            vistaAgrupador.Calculo = CalculoActual;
            vistaAgrupador.Ventana = this;
            //vistaConjuntoNums.VistaEntrada = vistaEntrada;
            //vistaConjuntoNums.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaAgrupador;

            nuevaTab.Header = "Definición " + operacion.Nombre + " para " + CalculoActual.NombreArchivo;
            vistaAgrupador.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaAgrupador.rutaArchivo.Content = CalculoActual.RutaArchivo;

            vistaAgrupador.NombreCalculoSeleccionado.Text = "Cálculo: " + CalculoSeleccionado.Nombre + " - " +
                "Agrupador: " + operacion.Nombre;

            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            vistaAgrupador.diagrama.Width = vistaAgrupador.Agrupador.AnchoDiagrama;
            vistaAgrupador.diagrama.Height = vistaAgrupador.Agrupador.AltoDiagrama;
        }

        public void AgregarTabDiseñoOperacion_TextosInformacion(ref DiseñoTextosInformacion definicionTextosInformacion)
        {
            TabItem nuevaTab = new TabItem();
            VistaDiseñoOperacion_TextosInformacion vistaTextosInformacion = new VistaDiseñoOperacion_TextosInformacion
            {
                Definicion = definicionTextosInformacion
            };
            vistaTextosInformacion.Calculo = CalculoActual;
            vistaTextosInformacion.Ventana = this;
            //vistaConjuntoNums.VistaEntrada = vistaEntrada;
            //vistaConjuntoNums.busquedaNumero.Entrada = entrada;
            nuevaTab.Content = vistaTextosInformacion;

            nuevaTab.Header = "Definición de relaciones de textos de información para " + definicionTextosInformacion.OperacionRelacionada.Nombre + " para " + CalculoActual.NombreArchivo;
            vistaTextosInformacion.nombreArchivo.Content = CalculoActual.NombreArchivo;
            vistaTextosInformacion.rutaArchivo.Content = CalculoActual.RutaArchivo;

            vistaTextosInformacion.nombreCalculo.Content = "Cálculo " + definicionTextosInformacion.CalculoRelacionado.Nombre;
            vistaTextosInformacion.nombreOperacion.Content = "Operación " + definicionTextosInformacion.OperacionRelacionada.Nombre;

            vistaTextosInformacion.Definicion_TextosInformacion = definicionTextosInformacion;
            vistaTextosInformacion.nombreDefinicionTextosInformacion.Content = "Definición " + definicionTextosInformacion.Nombre;

            contenido.Items.Add(nuevaTab);
            contenido.SelectedItem = nuevaTab;

            vistaTextosInformacion.diagrama.Width = vistaTextosInformacion.Definicion.OperacionRelacionada.AnchoDiagrama;
            vistaTextosInformacion.diagrama.Height = vistaTextosInformacion.Definicion.OperacionRelacionada.AltoDiagrama;

            //vistaConjuntoNums.ListarEntradas();
            vistaTextosInformacion.DibujarElementosDiseñoOperacion();
            vistaSeleccionada_ElementoDiseño_BotonImplicacion = vistaTextosInformacion;
        }

        //Métodos para actualizar elementos en las pestañas y ventanas
        public void CerrarPestañasEntradaModificada(Entrada entradaRelacionada)
        {
            var tabsAQuitar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaNumero) &&
((VistaArchivoEntradaNumero)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaConjuntoNumerosEntrada) &&
((VistaConjuntoNumerosEntrada)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaTextosInformacionEntrada) &&
((VistaTextosInformacionEntrada)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) &&
((VistaArchivoEntradaConjuntoNumeros)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) &&
((VistaArchivoEntradaTextosInformacion)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaNumero) &&
((VistaURLEntradaNumero)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) &&
((VistaURLEntradaConjuntoNumeros)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaTextosInformacion) &&
((VistaURLEntradaTextosInformacion)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) &&
((VistaArchivoExcelEntradaNumero)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) &&
((VistaArchivoExcelEntradaConjuntoNumeros)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) &&
((VistaArchivoExcelEntradaTextosInformacion)T.Content).Entrada == entradaRelacionada
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }
        }

        public void ActualizarNombresDescripcionesEntrada(Entrada entradaRelacionada)
        {
            var tabsAActualizar = (from TabItem T in contenido.Items
                                   where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) &&
        ((VistaArchivoEntradaConjuntoNumeros)T.Content).Entrada == entradaRelacionada
                                   select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaArchivoEntradaConjuntoNumeros)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaConjuntoNumeros)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaConjuntoNumeros)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaNumero) &&
    ((VistaArchivoEntradaNumero)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaArchivoEntradaNumero)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaNumero)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaNumero)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaConjuntoNumerosEntrada) &&
    ((VistaConjuntoNumerosEntrada)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaConjuntoNumerosEntrada)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaConjuntoNumerosEntrada)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaConjuntoNumerosEntrada)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaTextosInformacionEntrada) &&
    ((VistaTextosInformacionEntrada)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaTextosInformacionEntrada)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) &&
    ((VistaURLEntradaConjuntoNumeros)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaURLEntradaConjuntoNumeros)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaConjuntoNumeros)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaConjuntoNumeros)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaTextosInformacion) &&
    ((VistaURLEntradaTextosInformacion)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaURLEntradaTextosInformacion)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaNumero) &&
    ((VistaURLEntradaNumero)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaURLEntradaNumero)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaNumero)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaNumero)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) &&
    ((VistaArchivoExcelEntradaNumero)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaArchivoExcelEntradaNumero)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaNumero)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaNumero)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                                   where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) &&
        ((VistaArchivoExcelEntradaConjuntoNumeros)T.Content).Entrada == entradaRelacionada
                                   select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaArchivoExcelEntradaConjuntoNumeros)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaConjuntoNumeros)item.Content).Entrada.Nombre = entradaRelacionada.Nombre;
                //((VistaArchivoEntradaConjuntoNumeros)item.Content).Entrada.Descripcion = entradaRelacionada.Descripcion;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) &&
    ((VistaArchivoExcelEntradaTextosInformacion)T.Content).Entrada == entradaRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaArchivoExcelEntradaTextosInformacion)item.Content).lblNombreEntrada.Content = entradaRelacionada.Nombre;
            }


            foreach (var tabVistaOperaciones in vistaOperaciones)
            {
                if (tabVistaOperaciones != null)
                {
                    var entradasAActualizar = (from UIElement E in tabVistaOperaciones.entradas.Children
                                               where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                    ((EntradaDiseñoOperaciones)E).Entrada == entradaRelacionada
                                               select E).ToList();

                    foreach (var item in entradasAActualizar)
                    {
                        ((EntradaDiseñoOperaciones)item).nombreEntrada.Text = entradaRelacionada.Nombre;
                        //((EntradaDiseñoOperaciones)item).Entrada.Nombre = entradaRelacionada.Nombre;
                        //((EntradaDiseñoOperaciones)item).Entrada.Descripcion = entradaRelacionada.Descripcion;
                    }

                    entradasAActualizar = (from UIElement E in tabVistaOperaciones.diagrama.Children
                                           where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                ((EntradaDiseñoOperaciones)E).Entrada == entradaRelacionada
                                           select E).ToList();

                    foreach (var item in entradasAActualizar)
                    {
                        ((EntradaDiseñoOperaciones)item).nombreEntrada.Text = entradaRelacionada.Nombre;
                        //((EntradaDiseñoOperaciones)item).Entrada.Nombre = entradaRelacionada.Nombre;
                    }
                }
            }

            var TabsVistaOperacion = (from TabItem T in contenido.Items
                                      where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion)
                                      select T).ToList();

            foreach (var itemTab in TabsVistaOperacion)
            {
                VistaDiseñoOperacion tabVistaOperacion = (VistaDiseñoOperacion)itemTab.Content;

                if (tabVistaOperacion != null)
                {
                    var entradasAActualizar = (from UIElement E in tabVistaOperacion.entradas.Children
                                               where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                    ((EntradaDiseñoOperaciones)E).Entrada == entradaRelacionada
                                               select E).ToList();

                    foreach (var item in entradasAActualizar)
                    {
                        ((EntradaDiseñoOperaciones)item).EstablecerNombre(entradaRelacionada);
                        //((EntradaDiseñoOperaciones)item).Entrada.Nombre = entradaRelacionada.Nombre;
                        //((EntradaDiseñoOperaciones)item).Entrada.Descripcion = entradaRelacionada.Descripcion;
                    }

                    entradasAActualizar = (from UIElement E in tabVistaOperacion.diagrama.Children
                                           where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                ((EntradaDiseñoOperaciones)E).Entrada == entradaRelacionada
                                           select E).ToList();

                    foreach (var item in entradasAActualizar)
                    {
                        ((EntradaDiseñoOperaciones)item).EstablecerNombre(entradaRelacionada);
                        //((EntradaDiseñoOperaciones)item).Entrada.Nombre = entradaRelacionada.Nombre;
                    }
                }
            }

            var TabsVistaOperacion_TextosInformacion = (from TabItem T in contenido.Items
                                      where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion)
                                      select T).ToList();

            foreach (var itemTab in TabsVistaOperacion_TextosInformacion)
            {
                VistaDiseñoOperacion_TextosInformacion tabVistaOperacion_TextosInformacion = (VistaDiseñoOperacion_TextosInformacion)itemTab.Content;

                if (tabVistaOperacion_TextosInformacion != null)
                {
                    var entradasAActualizar = (from UIElement E in tabVistaOperacion_TextosInformacion.diagrama.Children
                                           where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                ((EntradaDiseñoOperaciones)E).Entrada == entradaRelacionada
                                           select E).ToList();

                    foreach (var item in entradasAActualizar)
                    {
                        ((EntradaDiseñoOperaciones)item).nombreEntrada.Text = entradaRelacionada.Nombre;
                        //((EntradaDiseñoOperaciones)item).Entrada.Nombre = entradaRelacionada.Nombre;
                    }
                }
            }

            //DiseñoCalculo itemCalculoAnterior = null;
            //DiseñoOperacion itemElementoCalculoAnterior = null;

            if (CalculoActual != null)
            {
                foreach (var itemCalculo in CalculoActual.Calculos)
                {
                    foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                    {
                        if (itemElemento.EntradaRelacionada != null &&
                            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada == entradaRelacionada)
                        {
                            //itemElementoCalculoAnterior = itemElemento;
                            //itemCalculoAnterior = (from C in CalculoActual.Calculos
                            //                       where C.ElementosOperaciones.Contains(itemElemento)
                            //                       select C).FirstOrDefault();

                            string[] partesNombre = itemElemento.NombreCombo.Split(new string[] { " desde " }, System.StringSplitOptions.None);
                            //itemElemento.Nombre = entradaRelacionada.Nombre;
                            itemElemento.EntradaRelacionada.Nombre = entradaRelacionada.Nombre + " desde " + partesNombre[1];
                            //itemElemento.EntradaRelacionada.Nombre = itemElemento.EntradaRelacionada.Nombre + " desde " + itemCalculoAnterior.Nombre;


                            var tabAActualizar = (from TabItem T in contenido.Items
                                                  where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                      ((VistaDiseñoOperaciones)T.Content).CalculoDiseñoSeleccionado == itemCalculo
                                                  select T).FirstOrDefault();

                            if (tabAActualizar != null)
                            {
                                ((VistaDiseñoOperaciones)tabAActualizar.Content).ListarEntradas();
                                ((VistaDiseñoOperaciones)tabAActualizar.Content).DibujarElementosOperaciones();
                            }
                        }
                    }
                }
            }

            //if (itemElementoCalculoAnterior != null)
            //{
                
                //foreach (var itemCalculo in CalculoActual.Calculos)
                //{
                //    foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                //    {
                //        if (itemElemento.EntradaRelacionada != null &&
                //            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                //            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.Nombre == itemElementoCalculoAnterior.Nombre)
                //        {

                //            if (itemElementoCalculoAnterior.Tipo != TipoElementoOperacion.Entrada)
                //                itemElemento.EntradaRelacionada.Nombre = itemElementoCalculoAnterior.Nombre + " desde " + itemCalculoAnterior.Nombre;
                //            else
                //                itemElemento.EntradaRelacionada.Nombre = itemElementoCalculoAnterior.EntradaRelacionada.Nombre + " desde " + itemCalculoAnterior.Nombre;


                //            var tabAActualizar = (from TabItem T in contenido.Items
                //                                  where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                //      ((VistaDiseñoOperaciones)T.Content).CalculoDiseñoSeleccionado == itemCalculo
                //                                  select T).FirstOrDefault();

                //            if (tabAActualizar != null)
                //            {
                //                ((VistaDiseñoOperaciones)tabAActualizar.Content).ListarEntradas();
                //                ((VistaDiseñoOperaciones)tabAActualizar.Content).DibujarElementosOperaciones();
                //            }

                //            break;
                //        }

                //    }
                //}
            //}
        }

        public void CerrarPestañasElementoOperacionEliminado(DiseñoOperacion elemento, bool quitarAgrupadores = true)
        {
            var tabsAQuitar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
((VistaDiseñoOperacion)T.Content).Operacion == elemento
                               select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada == elemento
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            if (quitarAgrupadores)
            {
                tabsAQuitar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                               ((VistaDiseñoOperaciones)T.Content).ModoAgrupador &&
    ((VistaDiseñoOperaciones)T.Content).Agrupador == elemento
                               select T).ToList();

                while (tabsAQuitar.Count > 0)
                {
                    contenido.Items.Remove(tabsAQuitar.First());
                    tabsAQuitar.Remove(tabsAQuitar.First());
                }
            }
        }

        public void ActualizarElementosEntradas(Entrada entrada, bool agregada, DiseñoCalculo elementoCalculo)
        {
            if (!agregada)
            {
                List<TabItem> itemsVistasCalculo;
                itemsVistasCalculo = (from TabItem T in contenido.Items
                                      where T.Content != null &&
    ((T.Content.GetType() == typeof(VistaDiseñoOperacion) && ((VistaDiseñoOperacion)T.Content).Calculo == CalculoActual))
                                      select T).ToList();

                foreach (var vistaOperacion in itemsVistasCalculo)
                {
                    var operacion = ((VistaDiseñoOperacion)vistaOperacion.Content).Operacion;

                    var itemsEntradaOperacion = (from DiseñoElementoOperacion E in operacion.ElementosDiseñoOperacion where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoDiseñoOperacion.Entrada select E).ToList();

                    while (itemsEntradaOperacion.Count > 0)
                    {
                        if (itemsEntradaOperacion.First().ContieneSalida)
                        {
                            //var vistaOperacion = (from VistaDiseñoOperacion V in itemsVistasCalculo where V.Operacion == operacion select V).FirstOrDefault();
                            ((VistaDiseñoOperacion)vistaOperacion.Content).QuitarElementoSalida(itemsEntradaOperacion.First());
                        }

                        foreach (var item in operacion.ElementosDiseñoOperacion)
                            ((VistaDiseñoOperacion)vistaOperacion.Content).QuitarDeElementosPosterioresAnteriores(item, itemsEntradaOperacion.First());

                        operacion.ElementosDiseñoOperacion.Remove(itemsEntradaOperacion.First());
                        ((VistaDiseñoOperacion)vistaOperacion.Content).ActualizarContenedoresElementos(itemsEntradaOperacion.First());

                        itemsEntradaOperacion.Remove(itemsEntradaOperacion.First());
                    }
                }

                //                    var operaciones = (from DiseñoOperacion D in elementoCalculo.ElementosOperaciones
                //                                       where
                //!itemsVistasCalculo.Any((i) => ((VistaDiseñoOperacion)i.Content).Calculo == CalculoActual && 
                //((VistaDiseñoOperacion)i.Content).Operacion == D)
                //                                       select D).ToList();

                //                    foreach (var operacion in operaciones)
                //                    {
                //                        var itemsEntradaOperacion = (from DiseñoElementoOperacion E in operacion.ElementosDiseñoOperacion where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoDiseñoOperacion.Entrada select E).ToList();

                //                        while (itemsEntradaOperacion.Count > 0)
                //                        {
                //                            if (itemsEntradaOperacion.First().ContieneSalida)
                //                            {
                //                                var vistaOperacion = (from VistaDiseñoOperacion V in itemsVistasCalculo where V.Operacion == operacion select V).FirstOrDefault();
                //                                vistaOperacion.QuitarElementoSalida(itemsEntradaOperacion.First());
                //                            }

                //                            foreach (var item in operacion.ElementosDiseñoOperacion)
                //                                QuitarDeElementosPosterioresAnteriores(item, itemsEntradaOperacion.First());

                //                            ActualizarContenedoresElementos(operacion, itemsEntradaOperacion.First());
                //                            operacion.ElementosDiseñoOperacion.Remove(itemsEntradaOperacion.First());

                //                            itemsEntradaOperacion.Remove(itemsEntradaOperacion.First());
                //                        }
                //                    }

                var vista = (from VistaDiseñoOperaciones V in vistaOperaciones where V.Calculo == CalculoActual select V).FirstOrDefault();

                if (vista != null)
                {
                    List<DiseñoOperacion> itemsEntrada;

                    if (elementoCalculo.EsEntradasArchivo)
                    {
                        foreach (var itemCalculo in CalculoActual.Calculos.Where(i => !i.EsEntradasArchivo))
                        {
                            itemsEntrada = (from DiseñoOperacion E in itemCalculo.ElementosOperaciones
                                            where
E.EntradaRelacionada != null &&
E.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
E.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada == entrada && E.Tipo == TipoElementoOperacion.Entrada
                                            select E).ToList();

                            while (itemsEntrada.Count > 0)
                            {
                                vista.ActualizarDefinicionElementosPosteriores(itemsEntrada.First());

                                if (itemCalculo.ElementosPosteriores.Count == 0)
                                {
                                    Resultado resultadoRelacionado = (from Resultado R in CalculoActual.ListaResultados where R.SalidaRelacionada == itemsEntrada.First() select R).FirstOrDefault();
                                    if (resultadoRelacionado != null)
                                    {
                                        CalculoActual.ListaResultados.Remove(resultadoRelacionado);
                                        foreach (var item in vista.Ventana.contenido.Items)
                                        {
                                            if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                                            {
                                                ((VistaResultados)((TabItem)item).Content).ListarResultados();
                                                break;
                                            }
                                        }
                                    }
                                }

                                vista.QuitarElementoSalida_ElementoPosterior(itemCalculo, itemsEntrada.First());

                                if (itemsEntrada.First().ContieneSalida)
                                {
                                    vista.QuitarElementoSalida(itemsEntrada.First(), itemCalculo);
                                }

                                foreach (var item in itemCalculo.ElementosOperaciones)
                                    vista.QuitarDeElementosPosterioresAnteriores(item, itemsEntrada.First());

                                itemCalculo.ElementosOperaciones.Remove(itemsEntrada.First());
                                vista.ActualizarContenedoresElementos(itemsEntrada.First());

                                itemsEntrada.Remove(itemsEntrada.First());
                            }
                        }
                    }
                    else
                    {
                        itemsEntrada = (from DiseñoOperacion E in elementoCalculo.ElementosOperaciones where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoOperacion.Entrada select E).ToList();

                        while (itemsEntrada.Count > 0)
                        {
                            vista.ActualizarDefinicionElementosPosteriores(itemsEntrada.First());

                            if (elementoCalculo.ElementosPosteriores.Count == 0)
                            {
                                Resultado resultadoRelacionado = (from Resultado R in CalculoActual.ListaResultados where R.SalidaRelacionada == itemsEntrada.First() select R).FirstOrDefault();
                                if (resultadoRelacionado != null)
                                {
                                    CalculoActual.ListaResultados.Remove(resultadoRelacionado);
                                    foreach (var item in vista.Ventana.contenido.Items)
                                    {
                                        if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                                        {
                                            ((VistaResultados)((TabItem)item).Content).ListarResultados();
                                            break;
                                        }
                                    }
                                }
                            }

                            vista.QuitarElementoSalida_ElementoPosterior(elementoCalculo, itemsEntrada.First());

                            if (itemsEntrada.First().ContieneSalida)
                            {
                                vista.QuitarElementoSalida(itemsEntrada.First(), elementoCalculo);
                            }

                            foreach (var item in elementoCalculo.ElementosOperaciones)
                                vista.QuitarDeElementosPosterioresAnteriores(item, itemsEntrada.First());

                            elementoCalculo.ElementosOperaciones.Remove(itemsEntrada.First());
                            vista.ActualizarContenedoresElementos(itemsEntrada.First());

                            itemsEntrada.Remove(itemsEntrada.First());
                        }

                    }
                }


                //foreach (var itemElemento in CalculoActual.SubCalculoSeleccionado_Operaciones.ElementosOperaciones)
                //{
                //    foreach (var itemElementoOperacion in itemElemento.ElementosDiseñoOperacion)
                //    {
                //        var itemsElementosOperacionAnterioresEntrada = (from DiseñoElementoOperacion E in itemElementoOperacion.ElementosAnteriores where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoDiseñoOperacion.Entrada select E).ToList();
                //        var itemsElementosOperacionPosterioresEntrada = (from DiseñoElementoOperacion E in itemElementoOperacion.ElementosPosteriores where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoDiseñoOperacion.Entrada select E).ToList();

                //        while (itemsElementosOperacionAnterioresEntrada.Count > 0)
                //        {
                //            itemElementoOperacion.ElementosAnteriores.Remove(itemsElementosOperacionAnterioresEntrada.First());
                //            itemsElementosOperacionAnterioresEntrada.Remove(itemsElementosOperacionAnterioresEntrada.First());
                //        }

                //        while (itemsElementosOperacionPosterioresEntrada.Count > 0)
                //        {
                //            itemElementoOperacion.ElementosPosteriores.Remove(itemsElementosOperacionPosterioresEntrada.First());
                //            itemsElementosOperacionPosterioresEntrada.Remove(itemsElementosOperacionPosterioresEntrada.First());
                //        }
                //    }
                //}

                //foreach (var itemElemento in CalculoActual.SubCalculoSeleccionado_Operaciones.ElementosOperaciones)
                //{
                //    var itemsAnterioresEntrada = (from DiseñoOperacion E in itemElemento.ElementosAnteriores where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoOperacion.Entrada select E).ToList();
                //    var itemsPosterioresEntrada = (from DiseñoOperacion E in itemElemento.ElementosPosteriores where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoOperacion.Entrada select E).ToList();
                //    var itemsElementosOperacionesEntrada = (from DiseñoElementoOperacion E in itemElemento.ElementosDiseñoOperacion where E.EntradaRelacionada == entrada && E.Tipo == TipoElementoDiseñoOperacion.Entrada select E).ToList();

                //    while (itemsElementosOperacionesEntrada.Count > 0)
                //    {
                //        itemElemento.ElementosDiseñoOperacion.Remove(itemsElementosOperacionesEntrada.First());
                //        itemsElementosOperacionesEntrada.Remove(itemsElementosOperacionesEntrada.First());
                //    }

                //    while (itemsAnterioresEntrada.Count > 0)
                //    {
                //        itemElemento.ElementosAnteriores.Remove(itemsAnterioresEntrada.First());
                //        itemsAnterioresEntrada.Remove(itemsAnterioresEntrada.First());
                //    }

                //    while (itemsPosterioresEntrada.Count > 0)
                //    {
                //        itemElemento.ElementosPosteriores.Remove(itemsPosterioresEntrada.First());
                //        itemsPosterioresEntrada.Remove(itemsPosterioresEntrada.First());
                //    }
                //}
            }

            foreach (var tabVistaOperaciones in vistaOperaciones)
            {
                tabVistaOperaciones.ListarEntradas();
                tabVistaOperaciones.DibujarElementosOperaciones();
            }

            var TabsVistaOperacion = (from TabItem T in contenido.Items
                                      where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion)
                                      select T).ToList();

            foreach (var itemTab in TabsVistaOperacion)
            {
                ((VistaDiseñoOperacion)itemTab.Content).ListarEntradas();
                ((VistaDiseñoOperacion)itemTab.Content).DibujarElementosDiseñoOperacion();
            }

            TabsVistaOperacion = (from TabItem T in contenido.Items
                                  where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion)
                                  select T).ToList();

            foreach (var itemTab in TabsVistaOperacion)
            {
                ((VistaDiseñoOperacion_TextosInformacion)itemTab.Content).DibujarElementosDiseñoOperacion();
            }
        }

        public void VerResultadosEjecucionArchivo(EjecucionCalculo Ejecucion)
        {
            TabItem nuevaTab = new TabItem();

            VistaInformeResultados resultados = new VistaInformeResultados();
            resultados.Ventana = this;
            resultados.Ejecucion = Ejecucion;

            nuevaTab.Content = resultados;
            contenido.Items.Add(nuevaTab);
            if (!string.IsNullOrEmpty(Ejecucion.Calculo.RutaArchivo))
            {
                nuevaTab.Header = "Resultados de Ejecución de " + Ejecucion.Calculo.NombreArchivo;
                resultados.nombreArchivo.Content = Ejecucion.Calculo.NombreArchivo;
                resultados.rutaArchivo.Content = Ejecucion.Calculo.RutaArchivo;
            }

            contenido.SelectedItem = nuevaTab;
        }

        public void ActualizarNombresDescripcionesOperacion(DiseñoOperacion operacionRelacionada)
        {
            foreach (var itemCalculo in CalculoActual.Calculos)
            {
                foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                {
                    if (itemElemento.EntradaRelacionada != null &&
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior == operacionRelacionada)
                    {
                        string[] partesNombre = itemElemento.EntradaRelacionada.Nombre.Split(new string[] { " desde " }, System.StringSplitOptions.None);
                        itemElemento.EntradaRelacionada.Nombre = operacionRelacionada.Nombre + " desde " + partesNombre[1];
                    }

                    foreach (var itemElementoDiseñoOperacion in itemElemento.ElementosDiseñoOperacion)
                    {
                        if (itemElementoDiseñoOperacion.ElementoDiseñoRelacionado == operacionRelacionada)
                        {
                            itemElementoDiseñoOperacion.Nombre = operacionRelacionada.Nombre;
                        }
                    }
                }

                var tabAActualizar = (from TabItem T in contenido.Items
                                      where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
           ((VistaDiseñoOperaciones)T.Content).CalculoDiseñoSeleccionado == itemCalculo
                                      select T).FirstOrDefault();

                if (tabAActualizar != null)
                {
                    ((VistaDiseñoOperaciones)tabAActualizar.Content).ListarEntradas();
                    ((VistaDiseñoOperaciones)tabAActualizar.Content).DibujarElementosOperaciones();
                }
            }

            var tabsAActualizar = (from TabItem T in contenido.Items
                                   where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
        ((VistaDiseñoOperacion)T.Content).Operacion == operacionRelacionada
                                   select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaDiseñoOperacion)item.Content).nombreOperacion.Content = operacionRelacionada.Nombre;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
    (from UIElement O in ((VistaDiseñoOperacion)T.Content).diagrama.Children
     where O.GetType() == typeof(EntradaFlujoOperacion) &&
((EntradaFlujoOperacion)O).DiseñoOperacion == operacionRelacionada
     select O).Count() > 0
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaDiseñoOperacion)item.Content).ListarEntradas();

                //var elementos = (from E in ((VistaDiseñoOperacion)item.Content).Operacion.ElementosDiseñoOperacion where E.ElementoDiseñoRelacionado == operacionRelacionada select E).ToList();
                //foreach (var itemOperacionRelacionada in elementos)
                //{
                //    itemOperacionRelacionada.Nombre = operacionRelacionada.Nombre;
                //}

                ((VistaDiseñoOperacion)item.Content).DibujarElementosDiseñoOperacion();
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
    ((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada == operacionRelacionada
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaDiseñoOperacion_TextosInformacion)item.Content).nombreOperacion.Content = operacionRelacionada.Nombre;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
    (from UIElement O in ((VistaDiseñoOperacion_TextosInformacion)T.Content).diagrama.Children
     where O.GetType() == typeof(EntradaFlujoOperacion) &&
((EntradaFlujoOperacion)O).DiseñoOperacion == operacionRelacionada
     select O).Count() > 0
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                //var elementos = (from E in ((VistaDiseñoOperacion_TextosInformacion)item.Content).Definicion.OperacionRelacionada.ElementosDiseñoOperacion where E.ElementoDiseñoRelacionado == operacionRelacionada select E).ToList();
                //foreach (var itemOperacionRelacionada in elementos)
                //{
                //    itemOperacionRelacionada.Nombre = operacionRelacionada.Nombre;
                //}

                ((VistaDiseñoOperacion_TextosInformacion)item.Content).DibujarElementosDiseñoOperacion();
            }
        }

        public void ActualizarNombresDescripcionesOperaciones_DesdeCalculo(DiseñoCalculo calculoRelacionado)
        {
            foreach (var itemCalculo in CalculoActual.Calculos)
            {
                foreach (var itemElemento in itemCalculo.ElementosOperaciones)
                {
                    if (itemElemento.EntradaRelacionada != null &&
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                        calculoRelacionado.ElementosOperaciones.Contains(itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior))
                    {
                        string[] partesNombre = itemElemento.EntradaRelacionada.Nombre.Split(new string[] { " desde " }, System.StringSplitOptions.None);
                        itemElemento.EntradaRelacionada.Nombre = partesNombre[0] + " desde " + calculoRelacionado.Nombre;
                    }

                    foreach (var itemElementoDiseñoOperacion in itemElemento.ElementosDiseñoOperacion)
                    {
                        if (itemElementoDiseñoOperacion.ElementoDiseñoRelacionado != null && 
                            itemElementoDiseñoOperacion.ElementoDiseñoRelacionado.EntradaRelacionada != null &&
                        itemElementoDiseñoOperacion.ElementoDiseñoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                        calculoRelacionado.ElementosOperaciones.Contains(itemElementoDiseñoOperacion.ElementoDiseñoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior))
                        {
                            string[] partesNombre = itemElemento.EntradaRelacionada.Nombre.Split(new string[] { " desde " }, System.StringSplitOptions.None);
                            itemElementoDiseñoOperacion.Nombre = partesNombre[0] + " desde " + calculoRelacionado.Nombre;
                        }
                    }
                }

                var tabAActualizar = (from TabItem T in contenido.Items
                                      where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperaciones) &&
           ((VistaDiseñoOperaciones)T.Content).CalculoDiseñoSeleccionado == itemCalculo
                                      select T).FirstOrDefault();

                if (tabAActualizar != null)
                {
                    ((VistaDiseñoOperaciones)tabAActualizar.Content).ListarEntradas();
                    ((VistaDiseñoOperaciones)tabAActualizar.Content).DibujarElementosOperaciones();
                }
            }

            var tabsAActualizar = (from TabItem T in contenido.Items
                                   where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
        calculoRelacionado.ElementosOperaciones.Contains(((VistaDiseñoOperacion)T.Content).Operacion.EntradaRelacionada.ElementoSalidaCalculoAnterior)
                                   select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaDiseñoOperacion)item.Content).nombreOperacion.Content = ((VistaDiseñoOperacion)item.Content).Operacion.Nombre;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
    (from UIElement O in ((VistaDiseñoOperacion)T.Content).diagrama.Children
     where O.GetType() == typeof(EntradaFlujoOperacion) &&
     calculoRelacionado.ElementosOperaciones.Contains(((EntradaFlujoOperacion)O).DiseñoOperacion.EntradaRelacionada.ElementoSalidaCalculoAnterior)
     select O).Count() > 0
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaDiseñoOperacion)item.Content).ListarEntradas();

                //var elementos = (from E in ((VistaDiseñoOperacion)item.Content).Operacion.ElementosDiseñoOperacion where E.ElementoDiseñoRelacionado == operacionRelacionada select E).ToList();
                //foreach (var itemOperacionRelacionada in elementos)
                //{
                //    itemOperacionRelacionada.Nombre = operacionRelacionada.Nombre;
                //}

                ((VistaDiseñoOperacion)item.Content).DibujarElementosDiseñoOperacion();
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
                               calculoRelacionado.ElementosOperaciones.Contains(((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada.EntradaRelacionada.ElementoSalidaCalculoAnterior)
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                ((VistaDiseñoOperacion_TextosInformacion)item.Content).nombreOperacion.Content = ((VistaDiseñoOperacion_TextosInformacion)item.Content).Definicion.OperacionRelacionada.Nombre;
            }

            tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
    (from UIElement O in ((VistaDiseñoOperacion_TextosInformacion)T.Content).diagrama.Children
     where O.GetType() == typeof(EntradaFlujoOperacion) &&
     calculoRelacionado.ElementosOperaciones.Contains(((EntradaFlujoOperacion)O).DiseñoOperacion.EntradaRelacionada.ElementoSalidaCalculoAnterior)
     select O).Count() > 0
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                //var elementos = (from E in ((VistaDiseñoOperacion_TextosInformacion)item.Content).Definicion.OperacionRelacionada.ElementosDiseñoOperacion where E.ElementoDiseñoRelacionado == operacionRelacionada select E).ToList();
                //foreach (var itemOperacionRelacionada in elementos)
                //{
                //    itemOperacionRelacionada.Nombre = operacionRelacionada.Nombre;
                //}

                ((VistaDiseñoOperacion_TextosInformacion)item.Content).DibujarElementosDiseñoOperacion();
            }
        }

        public void ActualizarNombresDescripcionesElementoOperacion(DiseñoElementoOperacion elementoRelacionado)
        {
            var tabsAActualizar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
    (from UIElement O in ((VistaDiseñoOperacion_TextosInformacion)T.Content).diagrama.Children
     where 
//     (O.GetType() == typeof(EntradaDiseñoOperaciones) &&
//((EntradaDiseñoOperaciones)O).DiseñoElementoOperacion == elementoRelacionado) ||
//(O.GetType() == typeof(EntradaFlujoOperacion) &&
//((EntradaFlujoOperacion)O).DiseñoElementoOperacion == elementoRelacionado) ||
(O.GetType() == typeof(OpcionOperacion) &&
((OpcionOperacion)O).DiseñoElementoOperacion == elementoRelacionado)
     select O).Count() > 0
                               select T).ToList();

            foreach (var item in tabsAActualizar)
            {
                //((VistaDiseñoOperacion_TextosInformacion)item.Content).ListarEntradas();

                //var elementos = (from E in ((VistaDiseñoOperacion)item.Content).Operacion.ElementosDiseñoOperacion where E == elementoRelacionado select E).ToList();
                //foreach (var itemOperacionRelacionada in elementos)
                //{
                //    var elementoVisual = (from UIElement O in ((VistaDiseñoOperacion)item.Content).diagrama.Children
                //                          where 
                //                     //     (O.GetType() == typeof(EntradaDiseñoOperaciones) &&
                //                     //((EntradaDiseñoOperaciones)O).DiseñoElementoOperacion == elementoRelacionado) ||
                //                     //(O.GetType() == typeof(EntradaFlujoOperacion) &&
                //                     //((EntradaFlujoOperacion)O).DiseñoElementoOperacion == elementoRelacionado) ||
                //                     (O.GetType() == typeof(OpcionOperacion) &&
                //                     ((OpcionOperacion)O).DiseñoElementoOperacion == elementoRelacionado)
                //                          select O).FirstOrDefault();

                //    if (elementoVisual != null)
                //    {
                //        if (elementoVisual.GetType() == typeof(OpcionOperacion))
                //        {
                //            ((OpcionOperacion)elementoVisual).nombreElemento.Text = elementoRelacionado.NombreElemento;
                //        }
                //    }
                //}
                //Dispatcher.Invoke(() =>
                //    {
                //        ((VistaDiseñoOperacion)item.Content).DibujarElementosDiseñoOperacion();
                //    }, System.Windows.Threading.DispatcherPriority.Render
                //);
                ((VistaDiseñoOperacion_TextosInformacion)item.Content).DibujarElementosDiseñoOperacion();

            }
        }

        public void ActualizarPestañaElementoOperacion(DiseñoOperacion elemento)
        {
            var tabsAQuitar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
((VistaDiseñoOperacion)T.Content).Operacion == elemento
                               select T).ToList();

            foreach (var tab in tabsAQuitar)
            {
                //EliminarSubElementos_DefinicionOperacion((VistaDiseñoOperacion)tab.Content, elemento, elementoAQuitar);

                ((VistaDiseñoOperacion)tab.Content).ListarEntradas();
                ((VistaDiseñoOperacion)tab.Content).DibujarElementosDiseñoOperacion();
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada == elemento
                           select T).ToList();

            foreach (var tab in tabsAQuitar)
            {
                ((VistaDiseñoOperacion_TextosInformacion)tab.Content).DibujarElementosDiseñoOperacion();
            }

            var tabs = (from TabItem T in contenido.Items
                        where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
elemento.ElementosAnteriores.Contains(((VistaDiseñoOperacion)T.Content).Operacion)
                        select T).ToList();

            foreach (var tab in tabs)
            {
                foreach (var itemSalida in ((VistaDiseñoOperacion)tab.Content).Operacion.ElementosDiseñoOperacion.Where(i => i.ContieneSalida))
                {
                    ((VistaDiseñoOperacion)tab.Content).elementosSalidaAgrupamiento.ItemsSource = (from O in ((VistaDiseñoOperacion)tab.Content).Operacion.ElementosPosteriores where O.Tipo != TipoElementoOperacion.Salida select O).ToList();
                }
            }
        }

        public void EliminarSubElementos_DefinicionOperacion(DiseñoOperacion elementoPadre, DiseñoOperacion elementoAQuitar)
        {
            List<DiseñoElementoOperacion> subElementosAQuitar = (from E in elementoPadre.ElementosDiseñoOperacion where E.ElementoDiseñoRelacionado == elementoAQuitar select E).ToList();

            if (subElementosAQuitar != null)
            {
                foreach (var item in subElementosAQuitar)
                {
                    if (item.Tipo == TipoElementoDiseñoOperacion.Entrada)
                    {
                        if (item.ContieneSalida)
                        {
                            //vista.QuitarElementoSalida(item);
                            DiseñoElementoOperacion elementoSalida = (from DiseñoElementoOperacion S in item.ElementosPosteriores where S.Tipo == TipoElementoDiseñoOperacion.Salida select S).FirstOrDefault();

                            elementoPadre.ElementosDiseñoOperacion.Remove(elementoSalida);
                            item.ElementosPosteriores.Remove(elementoSalida);
                        }

                        foreach (var itemOperacion in elementoPadre.ElementosDiseñoOperacion)
                            QuitarDeElementosPosterioresAnteriores(itemOperacion, item);

                        elementoPadre.ElementosDiseñoOperacion.Remove(item);
                        ActualizarContenedoresElementos(elementoPadre, item);
                    }
                    else if (item.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion)
                    {
                        if (item.ContieneSalida)
                        {
                            DiseñoElementoOperacion elementoSalida = (from DiseñoElementoOperacion S in item.ElementosPosteriores where S.Tipo == TipoElementoDiseñoOperacion.Salida select S).FirstOrDefault();

                            elementoPadre.ElementosDiseñoOperacion.Remove(elementoSalida);
                            item.ElementosPosteriores.Remove(elementoSalida);
                        }

                        foreach (var itemOperacion in elementoPadre.ElementosDiseñoOperacion)
                            QuitarDeElementosPosterioresAnteriores(itemOperacion, item);

                        elementoPadre.ElementosDiseñoOperacion.Remove(item);
                        ActualizarContenedoresElementos(elementoPadre, item);
                    }
                }

                subElementosAQuitar.Clear();
            }
        }

        public void QuitarDeElementosPosterioresAnteriores(DiseñoElementoOperacion elemento, DiseñoElementoOperacion elementoAQuitar)
        {
            var item = (from DiseñoElementoOperacion E in elemento.ElementosAnteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                elemento.ElementosAnteriores.Remove(item);
                item.QuitarOrdenOperando(elemento);
                //item.OrdenarOperandos(elemento);

            }

            item = (from DiseñoElementoOperacion E in elemento.ElementosPosteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                elemento.ElementosPosteriores.Remove(item);
                elemento.QuitarOrdenOperando(item);
                //elemento.OrdenarOperandos(item);
            }

            //elemento.OrdenarTodosOperandos();
            //elementoAQuitar.OrdenarOperandos(elemento);
        }

        public void ActualizarContenedoresElementos(DiseñoOperacion elementoPadre, DiseñoElementoOperacion elemento)
        {
            foreach (var itemElemento in elementoPadre.ElementosDiseñoOperacion)
            {
                if (itemElemento.ElementosContenedoresOperacion.Contains(elemento))
                    itemElemento.ElementosContenedoresOperacion.Remove(elemento);
            }
        }

        public bool VerificarCambiosArchivo(Calculo calculo)
        {
            if (File.Exists(calculo.RutaArchivo))
            {
                DataContractSerializer objetoGuardado = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });

                XmlReader lee = XmlReader.Create(calculo.RutaArchivo);
                Calculo calculoGuardado = (Calculo)objetoGuardado.ReadObject(lee);
                lee.Close();

                MemoryStream flujoCalculoGuardado = new MemoryStream();
                objetoGuardado.WriteObject(flujoCalculoGuardado, calculoGuardado);

                MemoryStream flujoCalculo = new MemoryStream();
                objetoGuardado.WriteObject(flujoCalculo, calculo);

                bool resultado = false;
                if (!Calculo.VerificarIgualdad(flujoCalculo.ToArray(), flujoCalculoGuardado.ToArray()))
                    resultado = true;

                flujoCalculo.Close();
                flujoCalculoGuardado.Close();

                return resultado;
            }
            else
                return false;
        }

        public void ActualizarListasEntradas_PestañaActual()
        {
            if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo))
            {
                ((VistaCalculo)((TabItem)contenido.SelectedItem).Content).ListarEntradas();
            }
            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas))
            {
                ((Entradas)((TabItem)contenido.SelectedItem).Content).ListarEntradas();
            }
            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones))
            {
                ((VistaDiseñoOperaciones)((TabItem)contenido.SelectedItem).Content).ListarEntradas();
            }
            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperacion))
            {
                ((VistaDiseñoOperacion)((TabItem)contenido.SelectedItem).Content).ListarEntradas();
            }            
        }

        public void ActualizarListasCalculos_PestañaActual()
        {
            if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo))
            {
                ((VistaCalculo)((TabItem)contenido.SelectedItem).Content).ListarCalculos();
            }
            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas))
            {
                ((Entradas)((TabItem)contenido.SelectedItem).Content).ListarCalculos();
            }
            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones))
            {
                ((VistaDiseñoOperaciones)((TabItem)contenido.SelectedItem).Content).ListarCalculos();
            }
            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos))
            {
                ((VistaDiseñoCalculos)((TabItem)contenido.SelectedItem).Content).DibujarElementosCalculos();
            }
            
        }

        public void ActualizarVistasEntradas_Actuales_Excel(Entrada entrada)
        {
            foreach (var vista in contenido.Items)
            {
                if (((TabItem)vista).Content.GetType() == typeof(VistaArchivoExcelEntradaNumero))
                {
                    ((VistaArchivoExcelEntradaNumero)((TabItem)vista).Content).Entrada = entrada;
                }
                else if (((TabItem)vista).Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros))
                {
                    ((VistaArchivoExcelEntradaConjuntoNumeros)((TabItem)vista).Content).Entrada = entrada;
                }
                else if (((TabItem)vista).Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion))
                {
                    ((VistaArchivoExcelEntradaTextosInformacion)((TabItem)vista).Content).Entrada = entrada;
                }
            }
        }

        public void ActualizarVistasEntradas_Actuales_Word(Entrada entrada)
        {
            foreach (var vista in contenido.Items)
            {
                if (entrada.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                    entrada.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                {
                    if (((TabItem)vista).Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) &&
                        ((VistaArchivoEntradaTextosInformacion)((TabItem)vista).Content).Entrada.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                    ((VistaArchivoEntradaTextosInformacion)((TabItem)vista).Content).Entrada.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                    {
                        ((VistaArchivoEntradaTextosInformacion)((TabItem)vista).Content).Entrada = entrada;
                    }
                    else if (((TabItem)vista).Content.GetType() == typeof(VistaArchivoEntradaNumero) &&
                        ((VistaArchivoEntradaNumero)((TabItem)vista).Content).Entrada.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                    ((VistaArchivoEntradaNumero)((TabItem)vista).Content).Entrada.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                    {
                        ((VistaArchivoEntradaNumero)((TabItem)vista).Content).Entrada = entrada;
                    }
                    else if (((TabItem)vista).Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) &&
                        ((VistaArchivoEntradaConjuntoNumeros)((TabItem)vista).Content).Entrada.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                    ((VistaArchivoEntradaConjuntoNumeros)((TabItem)vista).Content).Entrada.TipoFormatoArchivo_Entrada == TipoFormatoArchivoEntrada.Word)
                    {
                        ((VistaArchivoEntradaConjuntoNumeros)((TabItem)vista).Content).Entrada = entrada;
                    }
                }
            }
        }

        public void ActualizarVistaEntradas(DiseñoCalculo calculoSeleccionado)
        {
            foreach (var item in contenido.Items)
            {
                if (((TabItem)item).Content.GetType() == typeof(Entradas))
                {
                    ((Entradas)((TabItem)item).Content).ListarCalculos();
                }
            }

            if(CalculoActual.SubCalculoSeleccionado_Entradas == calculoSeleccionado)
                CalculoActual.SubCalculoSeleccionado_Entradas = null;
        }

        public void CerrarPestañasCalculoEliminado(DiseñoCalculo elemento)
        {
            var tabsAQuitar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) &&
                                    elemento.ListaEntradas.Contains(((VistaArchivoEntradaConjuntoNumeros)T.Content).Entrada)
                               select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoEntradaNumero) &&
                                elemento.ListaEntradas.Contains(((VistaArchivoEntradaNumero)T.Content).Entrada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) &&
                                elemento.ListaEntradas.Contains(((VistaArchivoExcelEntradaConjuntoNumeros)T.Content).Entrada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) &&
                                elemento.ListaEntradas.Contains(((VistaArchivoExcelEntradaNumero)T.Content).Entrada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaConjuntoNumerosEntrada) &&
                                elemento.ListaEntradas.Contains(((VistaConjuntoNumerosEntrada)T.Content).Entrada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
                                elemento.ElementosOperaciones.Contains(((VistaDiseñoOperacion)T.Content).Operacion)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
                                elemento.ElementosOperaciones.Contains(((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) &&
                                elemento.ListaEntradas.Contains(((VistaURLEntradaConjuntoNumeros)T.Content).Entrada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }

            tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaURLEntradaNumero) &&
                                elemento.ListaEntradas.Contains(((VistaURLEntradaNumero)T.Content).Entrada)
                           select T).ToList();

            while (tabsAQuitar.Count > 0)
            {
                contenido.Items.Remove(tabsAQuitar.First());
                tabsAQuitar.Remove(tabsAQuitar.First());
            }
        }

        public void EstablecerNombreRutaArchivoCalculo_Pestañas(Calculo calculo)
        {
            foreach (var tab in contenido.Items)
            {
                if (((TabItem)tab).Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) &&
                    ((VistaArchivoEntradaConjuntoNumeros)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaArchivoEntradaConjuntoNumeros)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaArchivoEntradaConjuntoNumeros)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de números para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) &&
                    ((VistaArchivoEntradaTextosInformacion)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaArchivoEntradaTextosInformacion)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaArchivoEntradaTextosInformacion)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de textos de información para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaArchivoEntradaNumero) &&
                    ((VistaArchivoEntradaNumero)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaArchivoEntradaNumero)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaArchivoEntradaNumero)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener número para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) &&
                    ((VistaArchivoExcelEntradaConjuntoNumeros)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaArchivoExcelEntradaConjuntoNumeros)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaArchivoExcelEntradaConjuntoNumeros)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de números para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) &&
                    ((VistaArchivoExcelEntradaTextosInformacion)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaArchivoExcelEntradaTextosInformacion)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaArchivoExcelEntradaTextosInformacion)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de textos de información para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) &&
                    ((VistaArchivoExcelEntradaNumero)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaArchivoExcelEntradaNumero)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaArchivoExcelEntradaNumero)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener número para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaCalculo) &&
                    ((VistaCalculo)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaCalculo)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaCalculo)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaConjuntoNumerosEntrada) &&
                    ((VistaConjuntoNumerosEntrada)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaConjuntoNumerosEntrada)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaConjuntoNumerosEntrada)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de números para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaTextosInformacionEntrada) &&
                    ((VistaTextosInformacionEntrada)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaTextosInformacionEntrada)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaTextosInformacionEntrada)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de textos de información para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaDiseñoCalculos) &&
                    ((VistaDiseñoCalculos)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaDiseñoCalculos)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaDiseñoCalculos)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaDiseñoOperacion) &&
                    ((VistaDiseñoOperacion)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaDiseñoOperacion)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaDiseñoOperacion)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Definición " + ((VistaDiseñoOperacion)((TabItem)tab).Content).Operacion.Nombre + " para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
                    ((VistaDiseñoOperacion_TextosInformacion)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaDiseñoOperacion_TextosInformacion)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaDiseñoOperacion_TextosInformacion)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Definición de relaciones de textos de información para  " + ((VistaDiseñoOperacion_TextosInformacion)((TabItem)tab).Content).Definicion.OperacionRelacionada.Nombre + " para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                    ((VistaDiseñoOperaciones)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaDiseñoOperaciones)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaDiseñoOperaciones)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaEjecucionCalculo) &&
                    ((VistaEjecucionCalculo)((TabItem)tab).Content).Ejecucion.Calculo == calculo)
                {
                    ((VistaEjecucionCalculo)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaEjecucionCalculo)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaInformeResultados) &&
                    ((VistaInformeResultados)((TabItem)tab).Content).Ejecucion.Calculo == calculo)
                {
                    ((VistaInformeResultados)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaInformeResultados)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaResultados) &&
                    ((VistaResultados)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaResultados)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaResultados)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) &&
                    ((VistaURLEntradaConjuntoNumeros)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaURLEntradaConjuntoNumeros)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaURLEntradaConjuntoNumeros)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de números para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaURLEntradaTextosInformacion) &&
                    ((VistaURLEntradaTextosInformacion)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaURLEntradaTextosInformacion)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaURLEntradaTextosInformacion)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener conjunto de textos de información para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaURLEntradaNumero) &&
                    ((VistaURLEntradaNumero)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaURLEntradaNumero)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaURLEntradaNumero)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = "Obtener número para " + calculo.NombreArchivo;
                }
                else if (((TabItem)tab).Content.GetType() == typeof(VistaDiseñoOperaciones) &&
                    ((VistaDiseñoOperaciones)((TabItem)tab).Content).Calculo == calculo)
                {
                    ((VistaDiseñoOperaciones)((TabItem)tab).Content).nombreArchivo.Content = calculo.NombreArchivo;
                    ((VistaDiseñoOperaciones)((TabItem)tab).Content).rutaArchivo.Content = calculo.RutaArchivo;
                    ((TabItem)tab).Header = calculo.NombreArchivo;
                }
            }
        }

        public void ActualizarPestañasDefinicionOperacion_TextosInformacion(DiseñoOperacion elemento)
        {
            var tabsAQuitar = (from TabItem T in contenido.Items
                           where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion) &&
((VistaDiseñoOperacion_TextosInformacion)T.Content).Definicion.OperacionRelacionada == elemento
                           select T).ToList();

            foreach (var tab in tabsAQuitar)
            {
                ((VistaDiseñoOperacion_TextosInformacion)tab.Content).DibujarElementosDiseñoOperacion();
                ((VistaDiseñoOperacion_TextosInformacion)tab.Content).DestacarElementoSeleccionado();
            }
        }

        public void ActualizarDefinicionesTextos_ElementoOperacionEliminado(DiseñoOperacion elemento, Calculo calculo)
        {
            foreach (var itemDefinicion in calculo.TextosInformacion.ElementosTextosInformacion)
            {
                foreach (var itemRelacion in itemDefinicion.Relaciones_TextosInformacion)
                {
                    foreach (var itemInstancia in itemRelacion.InstanciasAsignacion)
                    {
                        if (itemInstancia.Operandos_AsignarTextosInformacionA.Any(item => item.Operando == elemento))
                            itemInstancia.Operandos_AsignarTextosInformacionA.Remove(itemInstancia.Operandos_AsignarTextosInformacionA.FirstOrDefault(item => item.Operando == elemento));

                        if (itemInstancia.Operandos_AsignarTextosInformacionCuando.Contains(elemento))
                            itemInstancia.Operandos_AsignarTextosInformacionCuando.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Any(item => item.Operando == elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Remove(
                                itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.Operando == elemento));

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Any(item => item.Operando == elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Remove(
                                itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.Operando == elemento));

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Contains(elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Remove(elemento);

                        if (itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Any(item => item.Operando == elemento))
                            itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Remove(
                                itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.Operando == elemento));
                    }

                    var lista = itemRelacion.ObtenerListaCondiciones_ElementoCondicion_Condiciones(elemento);
                    foreach (var item in lista)
                    {
                        item.ElementoCondicion = null;
                    }

                    var lista2 = itemRelacion.ObtenerListaCondiciones_OperandoCondicion_Condiciones(elemento);
                    foreach (var item in lista2)
                    {
                        item.OperandoCondicion = null;
                    }
                }
            }
        }

        public void ActualizarDefinicionesTextos_ElementoDiseñoOperacionEliminado(DiseñoElementoOperacion elemento, DiseñoTextosInformacion definicion)
        {            
            foreach (var itemRelacion in definicion.Relaciones_TextosInformacion)
            {
                foreach (var itemInstancia in itemRelacion.InstanciasAsignacion)
                {
                    if (itemInstancia.SubOperandos_AsignarTextosInformacionA.Any(item => item.SubOperando == elemento))
                        itemInstancia.SubOperandos_AsignarTextosInformacionA.Remove(itemInstancia.SubOperandos_AsignarTextosInformacionA.FirstOrDefault(item => item.SubOperando == elemento));

                    if (itemInstancia.SubOperandos_AsignarTextosInformacionCuando.Contains(elemento))
                        itemInstancia.SubOperandos_AsignarTextosInformacionCuando.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Contains(elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones.Remove(elemento);

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Any(item => item.SubOperando == elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.Remove(
                            itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones.FirstOrDefault(item => item.SubOperando == elemento));

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Any(item => item.SubOperando == elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.Remove(
                            itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones.FirstOrDefault(item => item.SubOperando == elemento));

                    if (itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Any(item => item.SubOperando == elemento))
                        itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.Remove(
                            itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones.FirstOrDefault(item => item.SubOperando == elemento));
                }

                var lista = itemRelacion.ObtenerListaCondiciones_ElementoDiseñoCondicion_Condiciones(elemento);
                foreach (var item in lista)
                {
                    item.OperandoSubElemento_Condicion = null;
                }
            }
        }

        public void ActualizarDefinicionesTextos_EntradaEliminada(Entrada entrada, Calculo calculo)
        {
            List<DiseñoTextosInformacion> entradasAQuitar = new List<DiseñoTextosInformacion>();

            foreach (var itemDefinicion in calculo.TextosInformacion.ElementosTextosInformacion)
            {
                foreach (var itemRelacion in itemDefinicion.Relaciones_TextosInformacion)
                {
                    var lista2 = itemRelacion.ObtenerListaCondiciones_Entrada_Condiciones(entrada);
                    foreach (var item in lista2)
                    {
                        item.ElementoEntrada_Valores = null;
                    }
                }

                if (itemDefinicion.EntradaRelacionada == entrada)
                {
                    entradasAQuitar.Add(itemDefinicion);
                }
            }

            var tabsAActualizar = (from VistaTextosInformacion I in vistaTextosInformacion where I.CalculoSeleccionado == calculo select I).ToList();
            

            while (entradasAQuitar.Any())
            {
                foreach (var vistaTextos in tabsAActualizar)
                {
                    var calculoSeleccionado = vistaTextos.CalculoDiseñoSeleccionado;

                    foreach (var item in calculoSeleccionado.ElementosTextosInformacion)
                        vistaTextos.QuitarDeElementosPosterioresAnteriores(item, entradasAQuitar.First());
                }

                calculo.TextosInformacion.ElementosTextosInformacion.Remove(entradasAQuitar.First());
                entradasAQuitar.Remove(entradasAQuitar.First());
            }

            foreach (var vistaTextos in tabsAActualizar)
            {
                //tab.vistaTextos.diagrama.Children.Clear();
                vistaTextos.DibujarElementosTextosInformacion(vistaTextos.CalculoDiseñoSeleccionado_Cantidades);
            }
        }

        public void ActualizarEntradas_TextosInformacion(Entrada entradaQuitar = null)
        {
            if (entradaQuitar != null)
            {
                var entradas = CalculoActual.TextosInformacion.ElementosTextosInformacion.Where(item => item.EntradaRelacionada == entradaQuitar).ToList();

                foreach (var itemEntrada in entradas)
                {
                    foreach (var item in CalculoActual.TextosInformacion.ElementosTextosInformacion)
                    {
                        QuitarDefinicionesTextosInformacion_Entrada(item, itemEntrada.EntradaRelacionada);
                        QuitarDeElementosPosterioresAnteriores_TextosInformacion(item, itemEntrada);
                    }

                    CalculoActual.TextosInformacion.ElementosTextosInformacion.Remove(itemEntrada);
                }
            }

            var tabsAQuitar = (from TabItem T in contenido.Items
                               where T.Content != null && T.Content.GetType() == typeof(VistaTextosInformacion)
                               select T).ToList();

            foreach (var tab in tabsAQuitar)
            {
                ((VistaTextosInformacion)tab.Content).ListarEntradas();
                ((VistaTextosInformacion)tab.Content).DibujarElementosTextosInformacion(((VistaTextosInformacion)tab.Content).CalculoDiseñoSeleccionado_Cantidades);
            }
        }

        public void QuitarDeElementosPosterioresAnteriores_TextosInformacion(DiseñoTextosInformacion elemento, DiseñoTextosInformacion elementoAQuitar)
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

            while (asignacionesImplicacionesTextosAquitar2.Any())
            {
                definicion.Relaciones_TextosInformacion.Remove(asignacionesImplicacionesTextosAquitar2.First());
                asignacionesImplicacionesTextosAquitar2.Remove(asignacionesImplicacionesTextosAquitar2.First());
            }

            var asignacionesImplicacionesTextosAquitar3 = (from A in definicion.Relaciones_TextosInformacion where A.InstanciasAsignacion.Any(I => I.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual.Any(item => item.EntradaRelacionada == entrada)) select A).ToList();

            while (asignacionesImplicacionesTextosAquitar3.Any())
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
    }
}