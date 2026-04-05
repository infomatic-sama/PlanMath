using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using PlanMath_para_Excel;
using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
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

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para Entradas.xaml
    /// </summary>
    public partial class Entradas : UserControl
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
        FiltroEntradas Filtros { get; set; }
        public Entradas()
        {
            calc = new Calculo();
            Filtros = new FiltroEntradas();
            InitializeComponent();
        }

        public void ListarEntradas(string textoBusqueda = null)
        {
            contenedorEntradas.Children.Clear();
            
            TextBlock nombre = new TextBlock();
            nombre.Text = Calculo.SubCalculoSeleccionado_Entradas.Nombre;

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                nombre.Text += " (Filtrando variables o vectores de entradas con la búsqueda: '" + textoBusqueda + "')";
            }

            nombre.FontWeight = FontWeights.Bold;
            nombre.VerticalAlignment = VerticalAlignment.Center;
            nombre.Margin = new Thickness(10);
            contenedorEntradas.Children.Add(nombre);

            if (Calculo.SubCalculoSeleccionado_Entradas.EsEntradasArchivo)
            {
                TextBlock etiqueta = new TextBlock();
                etiqueta.Text = "El orden de las entradas define su orden de ejecución. Esto indica la disponibilidad de la entrada como opción para cadenas de texto sugeridas para entradas posteriores.";

                //nombre.FontWeight = FontWeights.Bold;
                etiqueta.VerticalAlignment = VerticalAlignment.Center;
                etiqueta.Margin = new Thickness(10);
                contenedorEntradas.Children.Add(etiqueta);


                foreach (var itemEntrada in Calculo.SubCalculoSeleccionado_Entradas.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.SeleccionarEntradas))
                {
                    bool agregar = true;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        agregar = false;

                        if (itemEntrada.Nombre.ToLower().Contains(textoBusqueda.ToLower()))
                            agregar = true;
                    }

                    if (agregar)
                    {
                        SeleccionEntradasEspecifica nuevaEntrada = new SeleccionEntradasEspecifica();
                        //if (itemEntrada == Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.First())
                        //    nuevaEntrada.EsPrimeraEntrada = true;

                        nuevaEntrada.Calculo = Calculo.SubCalculoSeleccionado_Entradas;
                        //nuevaEntrada.CalculoEntradas = Calculo.ObtenerCalculoEntradas();
                        nuevaEntrada.Selector = itemEntrada;
                        nuevaEntrada.nombreEntrada.Text = itemEntrada.Nombre;
                        nuevaEntrada.VistaEntradas = this;
                        //nuevaEntrada.Ventana = Ventana;

                        contenedorEntradas.Children.Add(nuevaEntrada);
                    }
                }

                if ((from UIElement U in contenedorEntradas.Children
                     where
                     U.GetType() != typeof(TextBlock) &&
                     U.GetType() == typeof(SeleccionEntradasEspecifica)
                     select U).Count() == 0)
                {
                    AgregarBoton_AgregarSelector();
                }
            }

            foreach (var itemEntrada in Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas)
            {
                bool agregar = true;

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    agregar = false;

                    if (itemEntrada.Nombre.ToLower().Contains(textoBusqueda.ToLower()))
                        agregar = true;
                }

                if (agregar)
                {
                    agregar = false;

                    if (itemEntrada.Tipo == TipoEntrada.Ninguno)
                        agregar = true;

                    if (Filtros.TextosInformacion && itemEntrada.Tipo == TipoEntrada.TextosInformacion)
                        agregar = true;

                    if (Filtros.Numeros && itemEntrada.Tipo == TipoEntrada.Numero)
                        agregar = true;

                    if (Filtros.ConjuntoNumeros && itemEntrada.Tipo == TipoEntrada.ConjuntoNumeros)
                        agregar = true;

                    if (agregar)
                    {
                        agregar = false;

                        if (itemEntrada.TipoOpcionTextosInformacion == TipoOpcionTextosInformacionEntrada.Ninguno &
                            itemEntrada.TipoOpcionNumero == TipoOpcionNumeroEntrada.Ninguno &
                            itemEntrada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.Ninguno)
                            agregar = true;

                        if (Filtros.CantidadesFijas && itemEntrada.Tipo == TipoEntrada.TextosInformacion && itemEntrada.TipoOpcionTextosInformacion == TipoOpcionTextosInformacionEntrada.TextosInformacionFijos)
                            agregar = true;

                        if (Filtros.CantidadesFijas && itemEntrada.Tipo == TipoEntrada.Numero && itemEntrada.TipoOpcionNumero == TipoOpcionNumeroEntrada.NumeroFijo)
                            agregar = true;

                        if (Filtros.CantidadesFijas && itemEntrada.Tipo == TipoEntrada.ConjuntoNumeros && itemEntrada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.ConjuntoNumerosFijo)
                            agregar = true;

                        if (!agregar)
                        {
                            if (Filtros.CantidadesDigitadas && itemEntrada.Tipo == TipoEntrada.TextosInformacion && itemEntrada.TipoOpcionTextosInformacion == TipoOpcionTextosInformacionEntrada.SeDigita)
                                agregar = true;

                            if (Filtros.CantidadesDigitadas && itemEntrada.Tipo == TipoEntrada.Numero && itemEntrada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeDigita)
                                agregar = true;

                            if (Filtros.CantidadesDigitadas && itemEntrada.Tipo == TipoEntrada.ConjuntoNumeros && itemEntrada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeDigita)
                                agregar = true;
                        }

                        if (!agregar)
                        {
                            if (Filtros.CantidadesObtenidas && itemEntrada.Tipo == TipoEntrada.TextosInformacion && itemEntrada.TipoOpcionTextosInformacion == TipoOpcionTextosInformacionEntrada.SeObtiene)
                                agregar = true;

                            if (Filtros.CantidadesObtenidas && itemEntrada.Tipo == TipoEntrada.Numero && itemEntrada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeObtiene)
                                agregar = true;

                            if (Filtros.CantidadesObtenidas && itemEntrada.Tipo == TipoEntrada.ConjuntoNumeros && itemEntrada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene)
                                agregar = true;
                        }
                    }
                }

                if (agregar)
                {
                    EntradaEspecifica nuevaEntrada = new EntradaEspecifica();
                    if (itemEntrada == Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.First())
                        nuevaEntrada.EsPrimeraEntrada = true;

                    nuevaEntrada.Calculo = Calculo.SubCalculoSeleccionado_Entradas;
                    nuevaEntrada.CalculoEntradas = Calculo.ObtenerCalculoEntradas();
                    nuevaEntrada.Entrada = itemEntrada;
                    nuevaEntrada.VistaEntradas = this;
                    nuevaEntrada.Ventana = Ventana;

                    contenedorEntradas.Children.Add(nuevaEntrada);

                    nuevaEntrada.ActualizarTipo(itemEntrada.Tipo);
                }
            }

            if ((from UIElement U in contenedorEntradas.Children where 
                 U.GetType() != typeof(TextBlock) &&
                 U.GetType() == typeof(EntradaEspecifica)
                 select U).Count() == 0)
            {
                AgregarBoton_AgregarEntrada();
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void AgregarBoton_AgregarEntrada()
        {
            Button boton = new Button();
            boton.Click += Boton_Click;
            boton.Content = "Agregar variable o vector de entrada";
            boton.Margin = new Thickness(10);
            boton.Padding = new Thickness(5);
            boton.HorizontalAlignment = HorizontalAlignment.Left;
            boton.MaxWidth = 300;
            contenedorEntradas.Children.Add(boton);
        }

        private void AgregarBoton_AgregarSelector()
        {
            Button boton = new Button();
            boton.Click += BotonSelector_Click;
            boton.Content = "Agregar selector de variables o vectores de entradas";
            boton.Margin = new Thickness(10);
            boton.Padding = new Thickness(5);
            boton.HorizontalAlignment = HorizontalAlignment.Left;
            boton.MaxWidth = 300;
            contenedorEntradas.Children.Add(boton);
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            contenedorEntradas.Children.Remove((UIElement)sender);
            AgregarNuevaEntrada();
        }

        private void BotonSelector_Click(object sender, RoutedEventArgs e)
        {
            contenedorEntradas.Children.Remove((UIElement)sender);
            AgregarNuevoSelector();
        }

        public void ListarCalculos()
        {
            calculos.Children.Clear();
            calculos.RowDefinitions.Clear();
            calculos.ColumnDefinitions.Clear();

            ColumnDefinition definicionColumna = new ColumnDefinition();
            definicionColumna.Width = new GridLength(1, GridUnitType.Star);

            calculos.ColumnDefinitions.Add(definicionColumna);

            for (int cantidadFilas = 1; cantidadFilas <= calc.Calculos.Count; cantidadFilas++)
            {
                RowDefinition definicionFila = new RowDefinition();
                definicionFila.Height = GridLength.Auto;

                calculos.RowDefinitions.Add(definicionFila);
            }

            int indiceFila = 0;

            foreach (var itemCalculo in calc.Calculos)
            {
                CalculoEspecifico nuevoCalculo = new CalculoEspecifico();
                nuevoCalculo.CalculoDiseño = itemCalculo;
                //nuevoCalculo.VistaEntradas = this;
                nuevoCalculo.botonFondo.Click += SeleccionarCalculo;
                calculos.Children.Add(nuevoCalculo);

                Grid.SetRow(nuevoCalculo, indiceFila);
                indiceFila++;
            }
        }

        public void AgregarNuevaEntrada()
        {
            Entrada nueva = new Entrada();
            nueva.ID = App.GenerarID_Elemento();
            nueva.CalculoDiseñoAsociado = Calculo.SubCalculoSeleccionado_Entradas;
            Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.Add(nueva);

            if (Calculo.SubCalculoSeleccionado_Entradas.EsEntradasArchivo)
            {
                Calculo.SubCalculoSeleccionado_Entradas.AgregarEntrada_CalculoEntradas(nueva);
                nueva.EjecutarDeFormaGeneral = true;
            }

            Calculo.AgregarConfiguracionEntrada_Ejecucion(nueva);

            nueva.Nombre = "Variable o vector de entrada " + Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.Count;
            EntradaEspecifica nuevaEntrada = new EntradaEspecifica();
            nuevaEntrada.Calculo = Calculo.SubCalculoSeleccionado_Entradas;
            nuevaEntrada.CalculoEntradas = Calculo.ObtenerCalculoEntradas();
            nuevaEntrada.Entrada = nueva;            
            nuevaEntrada.VistaEntradas = this;
            nuevaEntrada.Ventana = Ventana;

            contenedorEntradas.Children.Add(nuevaEntrada);
            Ventana.ActualizarElementosEntradas(nueva, true, Calculo.SubCalculoSeleccionado_Entradas);
        }

        public void AgregarNuevoSelector()
        {
            DiseñoOperacion nueva = new DiseñoOperacion();
            nueva.ID = App.GenerarID_Elemento();
            nueva.Tipo = TipoElementoOperacion.SeleccionarEntradas;
            nueva.PosicionX = 0;
            nueva.PosicionY = 0;

            Calculo.SubCalculoSeleccionado_Entradas.ElementosOperaciones.Add(nueva);
            nueva.ContieneSalida = true;

            nueva.Nombre = "Selector " + Calculo.SubCalculoSeleccionado_Entradas.ElementosOperaciones.Count(i => i.Tipo == TipoElementoOperacion.SeleccionarEntradas);
            SeleccionEntradasEspecifica nuevaEntrada = new SeleccionEntradasEspecifica();
            nuevaEntrada.Calculo = Calculo.SubCalculoSeleccionado_Entradas;
            //nuevaEntrada.CalculoEntradas = Calculo.ObtenerCalculoEntradas();
            nuevaEntrada.Selector = nueva;
            nuevaEntrada.VistaEntradas = this;
            //nuevaEntrada.Ventana = Ventana;

            contenedorEntradas.Children.Insert(0, nuevaEntrada);
        }

        public void QuitarEntrada(EntradaEspecifica entrada)
        {
            Ventana.CerrarPestañasEntradaModificada(entrada.Entrada);

            if (entrada.Entrada.Tipo == TipoEntrada.TextosInformacion)
            {
                var itemCalculo = Ventana.CalculoActual.ObtenerCalculoEntradas();

                var elementosEntradas = itemCalculo.ElementosOperaciones.Where(i => i.EntradaRelacionada == entrada.Entrada).ToList();

                foreach (var elementoEntrada in elementosEntradas)
                {
                    if (elementoEntrada != null)
                    {
                        var elementosSeleccionEntradas = itemCalculo.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.SeleccionarEntradas).ToList();
                        foreach (var itemSeleccionEntrada in elementosSeleccionEntradas)
                        {
                            var entradasAsociadas = itemSeleccionEntrada.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Where(i => i.ElementoSalida_Operacion == elementoEntrada).ToList();
                            while (entradasAsociadas.Any())
                            {
                                itemSeleccionEntrada.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Remove(entradasAsociadas.FirstOrDefault());
                                entradasAsociadas.Remove(entradasAsociadas.FirstOrDefault());
                            }

                            itemSeleccionEntrada.ElementosPosteriores.Remove(elementoEntrada);
                            elementoEntrada.ElementosAnteriores.Remove(itemSeleccionEntrada);
                            itemSeleccionEntrada.ElementosContenedoresOperacion.Remove(elementoEntrada);
                        }
                    }
                }

                itemCalculo.QuitarEntrada_CalculoEntradas(entrada.Entrada);
                
            }

            if (Calculo.SubCalculoSeleccionado_Entradas.EsEntradasArchivo)
            {
                Calculo.SubCalculoSeleccionado_Entradas.QuitarEntrada_CalculoEntradas(entrada.Entrada);
            }

            QuitarReferenciasEntradasImplicacion_AsignacionTextosInformacion(entrada.Entrada, Calculo);

            QuitarReferenciasCondicionesEntrada_Calculo(entrada.Entrada);
            Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.Remove(entrada.Entrada);
            Calculo.AgregarConfiguracionEntrada_Ejecucion(entrada.Entrada);
            Calculo.QuitarConfiguracionEntrada_Ejecucion(entrada.Entrada);

            contenedorEntradas.Children.Remove(entrada);
            Ventana.ActualizarElementosEntradas(entrada.Entrada, false, Calculo.SubCalculoSeleccionado_Entradas);

            if (Calculo.SubCalculoSeleccionado_Entradas.EsEntradasArchivo)
            {
                foreach(var itemCalculo in Calculo.Calculos)
                {
                    Ventana.ActualizarElementosEntradas(entrada.Entrada, false, itemCalculo);
                }
            }

            Ventana.ActualizarDefinicionesTextos_EntradaEliminada(entrada.Entrada, Calculo);

            if (Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.Count == 0)
                AgregarBoton_AgregarEntrada();
        }

        public void QuitarSelector(SeleccionEntradasEspecifica selector)
        {
            QuitarConexionesSelector(selector);

            var itemCalculo = Ventana.CalculoActual.ObtenerCalculoEntradas();
            itemCalculo.QuitarElemento_CalculoEntradas(selector.Selector);
            ListarEntradas(textoBusquedaEntradas.Text);
        }

        public void QuitarConexionesSelector(SeleccionEntradasEspecifica selector)
        {
            var itemCalculo = Ventana.CalculoActual.ObtenerCalculoEntradas();

            var elementosEntradas = itemCalculo.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.Entrada).ToList();

            foreach (var elementoEntrada in elementosEntradas)
            {
                if (elementoEntrada != null)
                {
                    var entradasAsociadas = selector.Selector.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Where(i => i.ElementoSalida_Operacion == elementoEntrada).ToList();
                    while (entradasAsociadas.Any())
                    {
                        selector.Selector.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Remove(entradasAsociadas.FirstOrDefault());
                        entradasAsociadas.Remove(entradasAsociadas.FirstOrDefault());
                    }

                    elementoEntrada.ElementosAnteriores.Remove(selector.Selector);
                    selector.Selector.ElementosPosteriores.Remove(elementoEntrada);
                    selector.Selector.ElementosContenedoresOperacion.Remove(elementoEntrada);
                }
            }
        }

        public void AgregarConexionesSelector(SeleccionEntradasEspecifica selector, List<DiseñoOperacion> Entradas)
        {
            var itemCalculo = Ventana.CalculoActual.ObtenerCalculoEntradas();

            foreach (var elementoEntrada in Entradas)
            {
                if (elementoEntrada != null)
                {
                    selector.Selector.ElementosPosteriores.Add(elementoEntrada);
                    elementoEntrada.ElementosAnteriores.Add(selector.Selector);
                    selector.Selector.ElementosContenedoresOperacion.Add(elementoEntrada);
                }
            }
        }

        public void QuitarReferenciasEntradasImplicacion_AsignacionTextosInformacion(Entrada entrada, Calculo calculo)
        {
            foreach (var item in calculo.TextosInformacion.ElementosTextosInformacion)
            {
                foreach (var itemDefinicion in item.Relaciones_TextosInformacion)
                {
                    if (itemDefinicion.Condiciones_TextoCondicion != null)
                        itemDefinicion.Condiciones_TextoCondicion.QuitarReferenciasCondicionesEntrada(entrada);
                }
            }
        }

        private void SeleccionarCalculo(object sender, RoutedEventArgs e)
        {
            Calculo.SubCalculoSeleccionado_Entradas = ((CalculoEspecifico)((Control)sender).Parent).CalculoDiseño;

            MarcarCalculoSeleccionado();
            ListarEntradas();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            if (Calculo.ModoSubCalculo &&
                Calculo.ModoSubCalculo_Simple)
            {
                calculos.Visibility = Visibility.Collapsed;
                listaCalculos.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (Calculo.SubCalculoSeleccionado_Entradas != null)
                {
                    ListarCalculos();
                    MarcarCalculoSeleccionado();
                }
                else
                {
                    SeleccionarCalculo(calculos.Children[0], null);
                }
            }

            ListarEntradas();            
            CargarFiltros();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("VerAdministrarEntradasCalculo");
#endif
        }

        private void CargarFiltros()
        {
            opcionFiltroTextos.IsChecked = Filtros.TextosInformacion;
            opcionFiltroNumeros.IsChecked = Filtros.Numeros;
            opcionFiltroConjuntoNumeros.IsChecked = Filtros.ConjuntoNumeros;
            opcionFiltroFijas.IsChecked = Filtros.CantidadesFijas;
            opcionFiltroDigitadas.IsChecked = Filtros.CantidadesDigitadas;
            opcionFiltroObtenidas.IsChecked = Filtros.CantidadesObtenidas;
        }
        public void MarcarCalculoSeleccionado()
        {
            foreach (var itemCalculo in calculos.Children)
            {
                ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            }

            if (Calculo.SubCalculoSeleccionado_Entradas != null)
            {
                foreach (var itemCalculo in calculos.Children)
                {
                    if (((CalculoEspecifico)itemCalculo).CalculoDiseño == Calculo.SubCalculoSeleccionado_Entradas)
                    {
                        ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.InactiveBorderBrush;
                    }
                }
            }
        }

        private void buscarEntradas_Click(object sender, RoutedEventArgs e)
        {
            ListarEntradas(textoBusquedaEntradas.Text);
        }

        private void limpiarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            ListarEntradas();
        }

        private void QuitarReferenciasCondicionesEntrada_Calculo(Entrada entrada)
        {
            foreach(var itemCalculo in Calculo.Calculos)
            {
                foreach(var itemElemento in itemCalculo.ElementosOperaciones)
                {
                    if (itemElemento.Tipo == TipoElementoOperacion.Entrada && 
                        itemElemento.EntradaRelacionada == entrada ||
                        (itemElemento.EntradaRelacionada != null && 
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada == entrada))
                    {
                        foreach(var itemPosterior in itemElemento.ElementosPosteriores)
                        {
                            if(itemPosterior.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                            {
                                QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemPosterior);
                            }
                            else if(itemPosterior.Tipo == TipoElementoOperacion.CondicionesFlujo)
                            {
                                QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemPosterior);
                            }
                            else if(itemPosterior.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                            {                                
                                QuitarReferenciasElementos_CondicionesSeleccionarEntradas(itemElemento, itemPosterior);                                
                            }
                        }
                    }
                }
            }
        }

        public void QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(DiseñoOperacion elemento, DiseñoOperacion elementoReferencias)
        {
            foreach (var itemCondicion in elementoReferencias.CondicionesTextosInformacion_SeleccionOrdenamiento)
            {
                if (itemCondicion.Operandos_AplicarCondiciones.Contains(elemento))
                {
                    itemCondicion.Operandos_AplicarCondiciones.Remove(elemento);
                }

                if (itemCondicion.Condiciones != null)
                {
                    if (itemCondicion.Condiciones.ElementoOperacion_Valores == elemento)
                        itemCondicion.Condiciones.ElementoOperacion_Valores = null;

                    if (itemCondicion.Condiciones.OperandoCondicion == elemento)
                        itemCondicion.Condiciones.OperandoCondicion = null;

                    if (itemCondicion.Condiciones.Operandos_AplicarCondiciones.Contains(elemento))
                        itemCondicion.Condiciones.Operandos_AplicarCondiciones.Remove(elemento);

                    itemCondicion.Condiciones.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
                }

            }

            if (elementoReferencias.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elemento))
                elementoReferencias.SalidasAgrupamiento_SeleccionOrdenamiento.Remove(elemento);

            foreach (var itemAsignacion in elementoReferencias.AsociacionesTextosInformacion_ElementosSalida)
            {
                if (itemAsignacion.ElementoSalida_Operacion == elemento)
                    itemAsignacion.ElementoSalida_Operacion = null;
            }
        }

        public void QuitarReferenciasElementos_CondicionesFlujo(DiseñoOperacion elemento, DiseñoOperacion elementoReferencias)
        {
            foreach (var itemCondicion in elementoReferencias.CondicionesFlujo_SeleccionOrdenamiento)
            {
                itemCondicion.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
            }

            if (elementoReferencias.SalidasAgrupamiento_SeleccionCondicionesFlujo.Contains(elemento))
                elementoReferencias.SalidasAgrupamiento_SeleccionCondicionesFlujo.Remove(elemento);

            foreach (var itemAsignacion in elementoReferencias.AsociacionesCondicionesFlujo_ElementosSalida)
            {
                if (itemAsignacion.ElementoSalida_Operacion == elemento)
                    itemAsignacion.ElementoSalida_Operacion = null;
            }

            foreach (var itemAsignacion in elementoReferencias.AsociacionesCondicionesFlujo_ElementosSalida2)
            {
                if (itemAsignacion.ElementoSalida_Operacion == elemento)
                    itemAsignacion.ElementoSalida_Operacion = null;
            }
        }

        public void QuitarReferenciasElementos_CondicionesSeleccionarEntradas(DiseñoOperacion elemento, DiseñoOperacion elementoReferencias)
        {
            foreach (var itemCondicion in elementoReferencias.CondicionesTextosInformacion_SeleccionEntradas)
            {
                if (itemCondicion.Operandos_AplicarCondiciones.Contains(elemento))
                {
                    itemCondicion.Operandos_AplicarCondiciones.Remove(elemento);
                }

                if (itemCondicion.Condiciones != null)
                {
                    if (itemCondicion.ElementoOperacion_Valores == elemento)
                        itemCondicion.ElementoOperacion_Valores = null;

                    if (itemCondicion.OperandoCondicion == elemento)
                        itemCondicion.OperandoCondicion = null;

                    if (itemCondicion.Operandos_AplicarCondiciones.Contains(elemento))
                        itemCondicion.Operandos_AplicarCondiciones.Remove(elemento);

                    itemCondicion.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
                }

            }

            //if (elementoReferencias.SalidasAgrupamiento_SeleccionEntradas.Contains(elemento))
            //    elementoReferencias.SalidasAgrupamiento_SeleccionEntradas.Remove(elemento);

            foreach (var itemAsignacion in elementoReferencias.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida)
            {
                if (itemAsignacion.ElementoSalida_Operacion == elemento)
                    itemAsignacion.ElementoSalida_Operacion = null;
            }
        }

        private void opcionFiltroTextos_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                Filtros.TextosInformacion = (bool)opcionFiltroTextos.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroTextos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.TextosInformacion = (bool)opcionFiltroTextos.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroNumeros_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.Numeros = (bool)opcionFiltroNumeros.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroNumeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.Numeros = (bool)opcionFiltroNumeros.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroConjuntoNumeros_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.ConjuntoNumeros = (bool)opcionFiltroConjuntoNumeros.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroConjuntoNumeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.ConjuntoNumeros = (bool)opcionFiltroConjuntoNumeros.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroFijas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.CantidadesFijas = (bool)opcionFiltroFijas.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroFijas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.CantidadesFijas = (bool)opcionFiltroFijas.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroDigitadas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.CantidadesDigitadas = (bool)opcionFiltroDigitadas.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroDigitadas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.CantidadesDigitadas = (bool)opcionFiltroDigitadas.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroObtenidas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.CantidadesObtenidas = (bool)opcionFiltroObtenidas.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }

        private void opcionFiltroObtenidas_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Filtros.CantidadesObtenidas = (bool)opcionFiltroObtenidas.IsChecked;
                ListarEntradas(textoBusquedaEntradas.Text);
            }
        }
    }

    public class FiltroEntradas
    {
        public bool TextosInformacion { get; set; }
        public bool ConjuntoNumeros { get; set; }
        public bool Numeros { get; set; }

        public bool CantidadesFijas { get; set; }
        public bool CantidadesDigitadas { get; set; }
        public bool CantidadesObtenidas { get; set; }

        public FiltroEntradas()
        {
            TextosInformacion = true;
            ConjuntoNumeros = true;
            Numeros = true;
            CantidadesFijas = true;
            CantidadesDigitadas = true;
            CantidadesObtenidas = true;
        }
    }
}
