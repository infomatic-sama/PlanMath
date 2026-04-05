using Microsoft.Win32;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
using System.Xml;
using System.Xml.Serialization;
using Windows.UI.Composition;

namespace ProcessCalc
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Calculo CalculoActual { get; set; }
        public bool EjecutandoCalculos { get; set; }
        public bool Cerrando { get; set; }
        public Process ProcesoAsociado { get; set; }
        public ToolTipElementoVisual ToolTip { get; set; }
        public MainWindow()
        {
            Calculos = new List<Calculo>();
            EjecucionesToolTips = new List<Entidades.Ejecuciones.ToolTips.EjecucionTooltipsCalculo>();
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vistaInicio.Ventana = this;
            ListarArchivosRecientes();
            ListarEjecucionesRecientes();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirVentanaAplicacion");
#endif
        }

        public void ListarArchivosRecientes(string textoBusqueda = null)
        {
            vistaInicio.listaArchivosRecientes.Children.Clear();
            List<AperturaArchivo> listaRegistros = new List<AperturaArchivo>();

            if(string.IsNullOrEmpty(textoBusqueda))
            {
                listaRegistros = App.ArchivosRecientes.Historial.OrderByDescending((i) => i.FechaHora).ToList();
            }
            else
            {
                listaRegistros = App.ArchivosRecientes.Historial
                    .Where(i => ((!string.IsNullOrEmpty(i.DescripcionCalculo) && 
                    i.DescripcionCalculo.ToLower().Contains(textoBusqueda.ToLower()))) |

                    ((!string.IsNullOrEmpty(i.NombreArchivo) &&
                    i.NombreArchivo.ToLower().Contains(textoBusqueda.ToLower()))) |

                    ((!string.IsNullOrEmpty(i.RutaArchivo) &&
                    i.RutaArchivo.ToLower().Contains(textoBusqueda.ToLower())))).OrderByDescending((i) => i.FechaHora).ToList();
            }

            foreach (var itemApertura in listaRegistros)
            {
                AperturaArchivoEspecifica apertura = new AperturaArchivoEspecifica();
                apertura.Apertura = itemApertura;
                apertura.Ventana = this;
                vistaInicio.listaArchivosRecientes.Children.Add(apertura);
            }
        }

        public void ListarEjecucionesRecientes(string textoBusqueda = null)
        {
            vistaInicio.listaEjecucionesRecientes.Children.Clear();
            List<EjecucionArchivo> listaRegistros = new List<EjecucionArchivo>();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                listaRegistros = App.EjecucionesRecientes.Historial.OrderByDescending((i) => i.FechaHora).ToList();
            }
            else
            {
                listaRegistros = App.EjecucionesRecientes.Historial.
                    Where(i => ((!string.IsNullOrEmpty(i.DescripcionCalculo) &&
                    i.DescripcionCalculo.ToLower().Contains(textoBusqueda.ToLower()))) |

                    ((!string.IsNullOrEmpty(i.NombreArchivo) &&
                    i.NombreArchivo.ToLower().Contains(textoBusqueda.ToLower()))) |

                    ((!string.IsNullOrEmpty(i.RutaArchivo) &&
                    i.RutaArchivo.ToLower().Contains(textoBusqueda.ToLower())))).OrderByDescending((i) => i.FechaHora).ToList();
            }

            foreach (var itemEjecucion in listaRegistros)
            {
                EjecucionArchivoEspecifica ejecucion = new EjecucionArchivoEspecifica();
                ejecucion.Ejecucion = itemEjecucion;
                ejecucion.Ventana = this;
                vistaInicio.listaEjecucionesRecientes.Children.Add(ejecucion);
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AgregarTab();
        }

        private void btnNuevaVentana_Click(object sender, RoutedEventArgs e)
        {
           App.AgregarVentana(null);
        }

        private void contenido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculoActual = null;

            if (contenido.SelectedItem != null)
            {
                if (!(((TabItem)contenido.SelectedItem).Content != null && (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(InfoCalculo) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaResultados) |
                    ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos))))
                //    //MostrarOcultarBotonesCalculo(true);
                //else
                {
                    //MostrarOcultarBotonesCalculo(false);

                    if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoEntradaNumero) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaConjuntoNumerosEntrada) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacionEntrada) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperacion) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaEjecucionCalculo) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaInformeResultados) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaURLEntradaNumero) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaURLEntradaTextosInformacion) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoExcelEntradaNumero) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion) |
                        ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion))
                    {
                        //separadorOpciones2.Visibility = Visibility.Visible;
                        //btnCerrar.Visibility = Visibility.Visible;

                        if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoEntradaNumero))
                            CalculoActual = ((VistaArchivoEntradaNumero)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaConjuntoNumerosEntrada))
                            CalculoActual = ((VistaConjuntoNumerosEntrada)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacionEntrada))
                            CalculoActual = ((VistaTextosInformacionEntrada)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoEntradaConjuntoNumeros))
                            CalculoActual = ((VistaArchivoEntradaConjuntoNumeros)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoEntradaTextosInformacion))
                            CalculoActual = ((VistaArchivoEntradaTextosInformacion)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperacion))
                            CalculoActual = ((VistaDiseñoOperacion)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaURLEntradaNumero))
                            CalculoActual = ((VistaURLEntradaNumero)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaURLEntradaConjuntoNumeros))
                            CalculoActual = ((VistaURLEntradaConjuntoNumeros)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaURLEntradaTextosInformacion))
                            CalculoActual = ((VistaURLEntradaTextosInformacion)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoExcelEntradaNumero))
                            CalculoActual = ((VistaArchivoExcelEntradaNumero)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoExcelEntradaConjuntoNumeros))
                            CalculoActual = ((VistaArchivoExcelEntradaConjuntoNumeros)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaArchivoExcelEntradaTextosInformacion))
                            CalculoActual = ((VistaArchivoExcelEntradaTextosInformacion)((TabItem)contenido.SelectedItem).Content).Calculo;
                        else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperacion_TextosInformacion))
                            CalculoActual = ((VistaDiseñoOperacion_TextosInformacion)((TabItem)contenido.SelectedItem).Content).Calculo;

                        MarcarBotonCalculo(OpcionSeleccionada.Ninguna);
                    }
                }

                if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(InfoCalculo))
                {
                    CalculoActual = ((InfoCalculo)((TabItem)contenido.SelectedItem).Content).Calculo;
                    MarcarBotonCalculo(OpcionSeleccionada.EditarInfo);
                }
                else if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo))
                {
                    CalculoActual = ((VistaCalculo)((TabItem)contenido.SelectedItem).Content).Calculo;
                    MarcarBotonCalculo(OpcionSeleccionada.Informacion);
                }
                else if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas))
                {
                    CalculoActual = ((Entradas)((TabItem)contenido.SelectedItem).Content).Calculo;
                    MarcarBotonCalculo(OpcionSeleccionada.Entradas);
                }
                else if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones))
                {
                    CalculoActual = ((VistaDiseñoOperaciones)((TabItem)contenido.SelectedItem).Content).Calculo;
                    MarcarBotonCalculo(OpcionSeleccionada.Operaciones);
                }
                else if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaResultados))
                {
                    CalculoActual = ((VistaResultados)((TabItem)contenido.SelectedItem).Content).Calculo;
                    MarcarBotonCalculo(OpcionSeleccionada.Resultados);
                }
                else if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos))
                {
                    CalculoActual = ((VistaDiseñoCalculos)((TabItem)contenido.SelectedItem).Content).Calculo;
                    MarcarBotonCalculo(OpcionSeleccionada.Calculos);
                }
                else if (((TabItem)contenido.SelectedItem).Content != null && ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacion))
                {
                    CalculoActual = ((VistaTextosInformacion)((TabItem)contenido.SelectedItem).Content).CalculoSeleccionado;
                    MarcarBotonCalculo(OpcionSeleccionada.TextosInformacion);
                }
            }

            if (CalculoActual == null) MarcarBotonCalculo(OpcionSeleccionada.Ninguna);

            EstablecerTituloVentana();
            MostrarOcultarBotonOverFlow_BarraOpciones(opciones);
            MostrarOcultarBotonOverFlow_BarraOpciones(opcionesCalculo);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoActual != null && 
                !CalculoActual.ModoSubCalculo)
            {
                if (CalculoActual != null && string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                {
                    SaveFileDialog guardar = new SaveFileDialog();
                    guardar.Filter = "Cálculo de PlanMath | *.pmcalc";
                    guardar.Title = "Guardar cálculo " + CalculoActual.NombreArchivo;
                    bool? resp = guardar.ShowDialog();
                    if (resp == true)
                    {
                        bool setearCalculoGuardado = false;
                        if (string.IsNullOrEmpty(CalculoActual.RutaArchivo))
                            setearCalculoGuardado = true;

                        CalculoActual.RutaArchivo = guardar.FileName;
                        CalculoActual.NombreArchivo = guardar.FileName.Substring(guardar.FileName.LastIndexOf("\\") + 1);

                        foreach (var itemCalculo in CalculoActual.Calculos)
                        {
                            foreach (var itemEntrada in itemCalculo.ListaEntradas)
                            {
                                itemEntrada.CantidadesDigitadas.GuardarCantidadesDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, itemEntrada.ID);
                                itemEntrada.CantidadesDigitadas.VaciarCantidadesDigitadas();
                            }
                        }

                        XmlWriter guarda = XmlWriter.Create(CalculoActual.RutaArchivo);
                        DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                        objeto.WriteObject(guarda, CalculoActual);
                        guarda.Close();

                        if (setearCalculoGuardado)
                        {
                            ((TabItem)contenido.SelectedItem).Header = CalculoActual.NombreArchivo;

                            if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo))
                                ((VistaCalculo)((TabItem)contenido.SelectedItem).Content).Calculo = CalculoActual;
                            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(InfoCalculo))
                                ((InfoCalculo)((TabItem)contenido.SelectedItem).Content).Calculo = CalculoActual;
                            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas))
                                ((Entradas)((TabItem)contenido.SelectedItem).Content).Calculo = CalculoActual;
                            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones))
                                ((VistaDiseñoOperaciones)((TabItem)contenido.SelectedItem).Content).Calculo = CalculoActual;
                            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaResultados))
                                ((VistaResultados)((TabItem)contenido.SelectedItem).Content).Calculo = CalculoActual;
                            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos))
                                ((VistaDiseñoCalculos)((TabItem)contenido.SelectedItem).Content).Calculo = CalculoActual;
                            else if (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacion))
                                ((VistaTextosInformacion)((TabItem)contenido.SelectedItem).Content).CalculoSeleccionado = CalculoActual;

                            EstablecerNombreRutaArchivoCalculo_Pestañas(CalculoActual);
                        }

                        GuardarArchivoReciente(CalculoActual);
                        ListarArchivosRecientes();
                    }
                }
                else if (CalculoActual != null)
                {
                    if (VerificarCambiosArchivo(CalculoActual))
                    {
                        XmlWriter guarda = XmlWriter.Create(CalculoActual.RutaArchivo);
                        DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                        objeto.WriteObject(guarda, CalculoActual);
                        guarda.Close();
                    }
                }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("GuardarArchivoCalculo");
#endif

                EstablecerTituloVentana();
                MostrarOcultarBotonOverFlow_BarraOpciones(opciones);
                MostrarOcultarBotonOverFlow_BarraOpciones(opcionesCalculo);
            }
            else if (CalculoActual != null)
            {
                MessageBox.Show("Este cálculo es un subcálculo dentro de otro. Guarda el cálculo contenedor de éste.", "Subcálculo abierto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void GuardarArchivoReciente(Calculo calculo)
        {
            if (App.BuscarAperturaArchivoReciente(calculo) == null)
            {
                App.AgregarNuevoArchivoReciente(calculo, ProcesoAsociado);
            }
            App.ActualizarArchivoReciente(calculo);
            App.GuardarListaArchivosRecientes();
        }

        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Cálculo de PlanMath | *.pmcalc";
            abrir.Title = "Abrir cálculo";
            bool? resp = null;

            do
            {
                if (resp != null)
                {
                    MessageBox.Show("El archivo ya está abierto.", "Archivo abierto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                resp = abrir.ShowDialog();
            } while (resp == true && App.VerificarArchivoAbierto(abrir.FileName));

            if (resp == true)
            {
                AbrirArchivo(abrir.FileName);
            }
        }

        public void AbrirArchivo(string rutaArchivo)
        {
            if (!App.VerificarArchivoAbierto(rutaArchivo))
            {
                if (File.Exists(rutaArchivo))
                {
                    try
                    {
                        XmlReader guarda = XmlReader.Create(rutaArchivo);
                        DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                        Calculo nuevoCalculo = (Calculo)objeto.ReadObject(guarda);
                        guarda.Close();

                        foreach (var itemCalculo in nuevoCalculo.Calculos)
                        {
                            foreach (var itemEntrada in itemCalculo.ListaEntradas)
                            {
                                itemEntrada.CantidadesDigitadas.CargarCantidadesDigitadas(nuevoCalculo.RutaArchivo, nuevoCalculo.ID, itemEntrada.ID);                                
                            }
                        }

                        if (nuevoCalculo.Notas == null) nuevoCalculo.Notas = new List<DiseñoOperacion>();
                        if (!(from C in nuevoCalculo.Calculos where C.EsEntradasArchivo select C).Any())
                        {
                            nuevoCalculo.CrearCalculoEntradas();
                        }

                        nuevoCalculo.ProcesarIDs_Elementos(VerificaArchivoAbierto_ConId(nuevoCalculo.ID));

                        nuevoCalculo.RutaArchivo = rutaArchivo;
                        nuevoCalculo.NombreArchivo = rutaArchivo.Substring(rutaArchivo.LastIndexOf("\\") + 1);

                        foreach(var item in nuevoCalculo.Calculos)
                        {
                            foreach(var itemEntrada in item.ListaEntradas)
                            {
                                if(!nuevoCalculo.VerificarConfiguracionEntrada_Ejecucion(itemEntrada))
                                {
                                    nuevoCalculo.AgregarConfiguracionEntrada_Ejecucion(itemEntrada);
                                }
                            }
                        }

                        AgregarEjecucionToolTip(nuevoCalculo);

                        AgregarTabArchivo(nuevoCalculo);
                        GuardarArchivoReciente(CalculoActual);
                        App.AgregarNuevoArchivoAbierto(CalculoActual, ProcesoAsociado);
                        ListarArchivosRecientes();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirArchivoCalculo");
#endif

                        btnOperaciones_Click(this, null);
                    }
                    catch (Exception e) 
                    {
                        MessageBox.Show("El archivo tiene un problema. Puede que la versión de la aplicación no coincida con la versión del archivo.", "Archivo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("El archivo ya no se encuentra en la ubicación especificada.", "Archivo no encontrado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
                MessageBox.Show("El archivo ya está abierto.", "Archivo abierto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public EjecucionCalculo EjecutarArchivoExterno(Calculo calculo, bool modoToolTips)
        {
            if (calculo != null)
            {
                TabItem nuevaTab = new TabItem();

                VistaEjecucionCalculo ejecucion = new VistaEjecucionCalculo();
                ejecucion.Ventana = this;
                ejecucion.Ejecucion = new EjecucionCalculo();
                ejecucion.Ejecucion.Calculo = calculo.ReplicarObjeto();
                ejecucion.Ejecucion.ModoEjecucionExterna = true;
                ejecucion.Ejecucion.ModoToolTips = modoToolTips;
                ejecucion.Ejecucion.Ventana = this;

                foreach (var itemCalculoEjecucion in ejecucion.Ejecucion.Calculo.Calculos)
                {
                    foreach (var itemCalculoOriginal in calculo.Calculos)
                    {
                        foreach (var itemEntrada in itemCalculoEjecucion.ListaEntradas)
                        {
                            var itemEntradaOriginal = itemCalculoOriginal.ListaEntradas.FirstOrDefault(i => i.ID == itemEntrada.ID);
                            if (itemEntradaOriginal != null)
                            {
                                itemEntrada.EjecucionesExternas_SubElementos_Config = itemEntradaOriginal.EjecucionesExternas_SubElementos_Config;

                                foreach (var itemElemento in itemCalculoEjecucion.ElementosOperaciones)
                                {
                                    if (itemElemento.EntradaRelacionada != null &&
                                    itemElemento.EntradaRelacionada.ID == itemEntradaOriginal.ID)
                                    {
                                        itemElemento.EntradaRelacionada.EjecucionesExternas_SubElementos_Config = itemEntrada.EjecucionesExternas_SubElementos_Config;
                                    }
                                    //else
                                    //{
                                        //foreach (var subElemento in itemElemento.ElementosDiseñoOperacion)
                                        //{
                                        //    if (subElemento.EntradaRelacionada != null &&
                                        //subElemento.EntradaRelacionada.ID == itemEntradaOriginal.ID)
                                        //    {
                                        //        subElemento.EntradaRelacionada.EjecucionExterna_Config = itemEntrada.EjecucionExterna_Config;
                                        //    }
                                        //}
                                    //}
                                }
                            }
                        }
                    }
                }

                ejecuciones.Add(ejecucion);
                nuevaTab.Content = ejecucion;

                nuevaTab.Header = "Ejecución de " + ejecucion.Ejecucion.Calculo.NombreArchivo;
                ejecucion.nombreArchivo.Content = ejecucion.Ejecucion.Calculo.NombreArchivo;
                if (!string.IsNullOrEmpty(calculo.RutaArchivo))
                {
                    ejecucion.rutaArchivo.Content = ejecucion.Ejecucion.Calculo.RutaArchivo;
                }

                contenido.Items.Add(nuevaTab);
                contenido.SelectedItem = nuevaTab;

                return ejecucion.Ejecucion;
            }
            else
                return null;
        }
        public void btnCerrar_Click(object sender, RoutedEventArgs e)
        {            
            if (((TabItem)contenido.SelectedItem).Content != null &&
                ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaEjecucionCalculo))
            {
                if (((VistaEjecucionCalculo)((TabItem)contenido.SelectedItem).Content).Ejecucion.Iniciada &&
                    !((VistaEjecucionCalculo)((TabItem)contenido.SelectedItem).Content).Ejecucion.Finalizada)
                {
                    ((VistaEjecucionCalculo)((TabItem)contenido.SelectedItem).Content).detener_Click(this, null);
                }
            }

            if (((TabItem)contenido.SelectedItem).Content != null && (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(InfoCalculo) |
            ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaCalculo) |
            ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(Entradas) |
            (((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoOperaciones) && !((VistaDiseñoOperaciones)((TabItem)contenido.SelectedItem).Content).ModoAgrupador) |
            ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaResultados) |
            ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaDiseñoCalculos) |
            ((TabItem)contenido.SelectedItem).Content.GetType() == typeof(VistaTextosInformacion)))
            {
                if(!CalculoActual.ModoSubCalculo)
                    btnGuardar_Click(this, e);
            }

            QuitarTabActual();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MarcarBotonCalculo(OpcionSeleccionada.Informacion);
            MostrarVista(OpcionSeleccionada.Informacion);
        }

        private void btnEditarInfo_Click(object sender, RoutedEventArgs e)
        {
            MarcarBotonCalculo(OpcionSeleccionada.EditarInfo);
            MostrarVista(OpcionSeleccionada.EditarInfo);
        }

        public void btnEntradas_Click(object sender, RoutedEventArgs e)
        {
            MarcarBotonCalculo(OpcionSeleccionada.Entradas);
            MostrarVista(OpcionSeleccionada.Entradas);
        }

        public void btnOperaciones_Click(object sender, RoutedEventArgs e)
        {
            MarcarBotonCalculo(OpcionSeleccionada.Operaciones);
            MostrarVista(OpcionSeleccionada.Operaciones);
        }

        private void btnResultados_Click(object sender, RoutedEventArgs e)
        {
            MarcarBotonCalculo(OpcionSeleccionada.Resultados);
            MostrarVista(OpcionSeleccionada.Resultados);
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoActual != null && 
                !CalculoActual.ModoSubCalculo)
            {
                MarcarBotonCalculo(OpcionSeleccionada.Ejecutar);
                MostrarVista(OpcionSeleccionada.Ejecutar);
            }
            else if (CalculoActual != null)
            {
                MessageBox.Show("Este cálculo es un subcálculo dentro de otro. Ejecuta el cálculo contenedor de éste.", "Subcálculo abierto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void btnTextosInformacion_Click(object sender, RoutedEventArgs e)
        {
            MarcarBotonCalculo(OpcionSeleccionada.TextosInformacion);
            MostrarVista(OpcionSeleccionada.TextosInformacion);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (EjecutandoCalculos)
            {
                MessageBox.Show("Hay cálculos ejecutándose. Para cerrar la aplicación no debe haber ninguno ejecutándose.", "Calculando", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                e.Cancel = true;
            }
            else
            {
                Label_MouseLeftButtonDown_1(this, null);

                GuardarTodo();

                App.GuardarInformacionVentana();
                App.EliminarArchivosTemporales();
                
                Cerrando = true;

                if (App.VentanasAbiertas.Count == 1)
                {
                    TerminarProceso_SegundoPlano();

                    //CerrandoHilos_ToolTips();

                    if (App.HiloCierre.ThreadState == System.Threading.ThreadState.Unstarted)
                        App.HiloCierre.Start();
                }
            }
        }

        public void QuitarArchivoReciente(AperturaArchivo apertura)
        {
            App.QuitarAperturaArchivoReciente(apertura);
            App.GuardarListaArchivosRecientes();
            ListarArchivosRecientes();
        }

        public void QuitarEjecucionReciente(EjecucionArchivo ejecucion)
        {
            App.QuitarEjecucionArchivoReciente(ejecucion);
            App.GuardarListaArchivosRecientes();
            ListarEjecucionesRecientes();
        }

        private void opciones_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarOcultarBotonOverFlow_BarraOpciones((ToolBar)sender);
        }

        public void MostrarOcultarBotonOverFlow_BarraOpciones(ToolBar opcionesSeleccionada)
        {
            ToolBar toolBar = opcionesSeleccionada;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {                
                if (!toolBar.HasOverflowItems)
                    overflowGrid.Visibility = Visibility.Collapsed;
                else
                    overflowGrid.Visibility = Visibility.Visible;                
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                App.InformacionVentana.Height = Height;
                App.InformacionVentana.Width = Width;
            }

            MostrarOcultarBotonOverFlow_BarraOpciones(opciones);
            MostrarOcultarBotonOverFlow_BarraOpciones(opcionesCalculo);
        }

        private void btnCalculos_Click(object sender, RoutedEventArgs e)
        {
            bool mostrar = false;

            if (CalculoActual != null && 
                !CalculoActual.ModoSubCalculo)
            {
                mostrar = true;
            }
            else
            {
                if (CalculoActual != null && 
                    CalculoActual.ModoSubCalculo &&
                !CalculoActual.ModoSubCalculo_Simple)
                {
                    mostrar = true;
                }
                else if (CalculoActual != null)
                {
                    MessageBox.Show("Este cálculo es un subcálculo dentro de otro y además tiene definición simple. La definición simple no permite multiples cálculos.", "Subcálculo abierto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

            if(mostrar)
            {
                MarcarBotonCalculo(OpcionSeleccionada.Calculos);
                MostrarVista(OpcionSeleccionada.Calculos);
            }
            
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            App.InformacionVentana.Estado = WindowState;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                App.InformacionVentana.Top = Top;
                App.InformacionVentana.Left = Left;
            }
        }

        private void Label_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            btnCerrar_Click(this, e);
            menuCerrar.IsOpen = false;
        }

        private void Label_MouseLeftButtonDown_1(object sender, RoutedEventArgs e)
        {
            while (contenido.Items.Count > 1)
            {
                if (((TabItem)contenido.SelectedItem).Name != "inicio")
                    btnCerrar_Click(this, e);
                else
                {
                    TabItem vista = (from TabItem V in contenido.Items where V.Name != "inicio" select V).FirstOrDefault();
                    if (vista != null)
                        contenido.SelectedItem = vista;
                }
            }

           menuCerrar.IsOpen = false;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBox)sender).SelectedItem != null)
                ((ComboBox)sender).SelectedItem = null;
        }

        private void botonCerrar_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            OcultarToolTips_VistaActual();
        }

        public void OcultarToolTips_VistaActual()
        {
            var fichaActual = (TabItem)contenido.SelectedItem;

            if (fichaActual != null)
            {
                if (fichaActual.Content.GetType() == typeof(VistaDiseñoOperaciones))
                {
                    var vista = (VistaDiseñoOperaciones)(fichaActual.Content);

                    if (vista != null)
                    {
                        if (vista.Ventana.popup != null && vista.Ventana.popup.Child != null && !vista.Ventana.popup.Child.IsMouseOver && !vista.diagrama.IsMouseOver)
                            vista.OcultarToolTips(null);
                    }
                }
                else if (fichaActual.Content.GetType() == typeof(VistaDiseñoOperacion))
                {
                    var vista = (VistaDiseñoOperacion)(fichaActual.Content);

                    if (vista != null)
                    {
                        if (vista.Ventana.popup != null && vista.Ventana.popup.Child != null && !vista.Ventana.popup.Child.IsMouseOver && !vista.diagrama.IsMouseOver)
                            vista.OcultarToolTips(null);
                    }
                }
                else if (fichaActual.Content.GetType() == typeof(VistaDiseñoCalculos))
                {
                    var vista = (VistaDiseñoCalculos)(fichaActual.Content);

                    if (vista != null)
                    {
                        if (vista.Ventana.popup != null && vista.Ventana.popup.Child != null && !vista.Ventana.popup.Child.IsMouseOver && !vista.diagrama.IsMouseOver)
                            vista.OcultarToolTips(null);
                    }
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            OcultarToolTips_VistaActual();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            OcultarToolTips_VistaActual();
        }

        private void btnGuardarTodo_Click(object sender, RoutedEventArgs e)
        {
            GuardarTodo();
        }

        private void GuardarTodo()
        {
            foreach (var item in contenido.Items)
            {
                Calculo calculo = null;
                if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(InfoCalculo))
                {
                    calculo = ((InfoCalculo)((TabItem)item).Content).Calculo;
                }
                else if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaCalculo))
                {
                    calculo = ((VistaCalculo)((TabItem)item).Content).Calculo;
                }
                else if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(Entradas))
                {
                    calculo = ((Entradas)((TabItem)item).Content).Calculo;
                }
                else if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaDiseñoOperaciones))
                {
                    calculo = ((VistaDiseñoOperaciones)((TabItem)item).Content).Calculo;
                }
                else if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaResultados))
                {
                    calculo = ((VistaResultados)((TabItem)item).Content).Calculo;
                }
                else if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaDiseñoCalculos))
                {
                    calculo = ((VistaDiseñoCalculos)((TabItem)item).Content).Calculo;
                }
                else if (((TabItem)item).Content != null && ((TabItem)item).Content.GetType() == typeof(VistaTextosInformacion))
                {
                    calculo = ((VistaTextosInformacion)((TabItem)item).Content).CalculoSeleccionado;
                }

                if (calculo != null && !calculo.ModoSubCalculo && string.IsNullOrEmpty(calculo.RutaArchivo))
                {
                    SaveFileDialog guardar = new SaveFileDialog();
                    guardar.Filter = "Cálculo de PlanMath | *.pmcalc";
                    guardar.Title = "Guardar cálculo " + calculo.NombreArchivo;
                    bool? resp = guardar.ShowDialog();
                    if (resp == true)
                    {
                        calculo.RutaArchivo = guardar.FileName;
                        calculo.NombreArchivo = guardar.FileName.Substring(guardar.FileName.LastIndexOf("\\") + 1);

                        foreach (var itemCalculo in CalculoActual.Calculos)
                        {
                            foreach (var itemEntrada in itemCalculo.ListaEntradas)
                            {
                                itemEntrada.CantidadesDigitadas.GuardarCantidadesDigitadas(CalculoActual.RutaArchivo, CalculoActual.ID, itemEntrada.ID);
                                itemEntrada.CantidadesDigitadas.VaciarCantidadesDigitadas();
                            }
                        }

                        XmlWriter guarda = XmlWriter.Create(calculo.RutaArchivo);
                        DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                        objeto.WriteObject(guarda, calculo);
                        guarda.Close();

                        EstablecerNombreRutaArchivoCalculo_Pestañas(calculo);
                        GuardarArchivoReciente(calculo);
                    }
                }
                else if (calculo != null && !calculo.ModoSubCalculo)
                {
                    if (VerificarCambiosArchivo(calculo))
                    {
                        XmlWriter guarda = XmlWriter.Create(calculo.RutaArchivo);
                        DataContractSerializer objeto = new DataContractSerializer(typeof(Calculo), new DataContractSerializerSettings() { PreserveObjectReferences = true });
                        objeto.WriteObject(guarda, calculo);
                        guarda.Close();
                    }
                }

                if (calculo != null && !calculo.ModoSubCalculo)
                {
                    App.QuitarArchivoAbierto(calculo);
                }
            }

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("GuardarTodo");
#endif

            ListarArchivosRecientes();
        }

        private void Window_Deactivated_1(object sender, EventArgs e)
        {

        }
    }
}
