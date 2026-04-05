using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Threading;
using System.Windows.Threading;
using ProcessCalc.Controles.Notas;
using ProcessCalc.Ventanas;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Controles.TextosInformacion;
using System.Windows.Automation;
using System.Xml.Linq;
using System.Globalization;
using ProcessCalc.Entidades.Condiciones;
using PlanMath_para_Excel;
using ProcessCalc.Entidades.Operaciones;
using System.Diagnostics;
using ProcessCalc.Ventanas.Definiciones;
using ProcessCalc.Controles.Calculos;
using System.Configuration;
using System.Windows.Controls.Primitives;
using System.Diagnostics.CodeAnalysis;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Ventanas.Configuraciones;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaDiseñoOperaciones.xaml
    /// </summary>
    public partial class VistaDiseñoOperaciones : UserControl
    {
        private Calculo calc;
        public Calculo Calculo
        {
            set
            {
                nombreArchivo.Content = value.NombreArchivo;
                rutaArchivo.Content = value.RutaArchivo;

                calc = value;

                ListarCalculos();
            }
            get
            {
                return calc;
            }
        }
        public MainWindow Ventana { get; set; }
        public DiseñoCalculo CalculoDiseñoSeleccionado { get; set; }
        public bool ElementoSeleccionado;
        
        //public bool ElementoMovido;
        //bool MoverVerticalmente;
        //bool MoverHorizontalmente;        
        private DiseñoOperacion ElementoAnteriorLineaSeleccionada;
        private DiseñoOperacion ElementoPosteriorLineaSeleccionada;        
        public bool SeleccionandoElemento { get; set; }
        private bool BuscandoOperaciones;
        Point ubicacionInicialAreaSeleccionada;
        Point ubicacionFinalAreaSeleccionada;
        public Rectangle rectanguloSeleccion;
        public bool ClicDiagrama;
        bool ClicElemento = false;
        //public bool ClicNota;
        //bool elementoSeleccionado;
        //bool arrastrando;
        public Point ubicacionActualElemento { get; set; }
        public bool ModoAgrupador { get; set; }
        public DiseñoOperacion Agrupador { get; set; }
        public VistaDiseñoOperaciones VistaOperaciones { get; set; }
        bool lineaSeleccionada = false;
        public bool CalculoClikeado { get; set; }
        public bool ModoIconosResumidos { get; set; }
        public VistaDiseñoOperaciones()
        {
            calc = new Calculo();
            InitializeComponent();
        }

        public void ListarOperaciones()
        {
            string textoBusqueda = null;

            if (BuscandoOperaciones &&
                !string.IsNullOrEmpty(busquedaOperaciones.Text))
            {
                textoBusqueda = busquedaOperaciones.Text;
            }

            operaciones.Children.Clear();

            EntradaDiseñoOperaciones entradaNumero = new EntradaDiseñoOperaciones();
            entradaNumero.VistaOperaciones = this;
            entradaNumero.EsEntradaNueva = true;
            entradaNumero.Entrada = new Entrada()
            {
                Tipo = TipoEntrada.Numero,
                Nombre = "Variable de número"
            };

            if (textoBusqueda == null)
                operaciones.Children.Add(entradaNumero);
            else
            {
                if (entradaNumero.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                    entradaNumero.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(entradaNumero);
            }

            EntradaDiseñoOperaciones entradaConjuntoNumeros = new EntradaDiseñoOperaciones();
            entradaConjuntoNumeros.VistaOperaciones = this;
            entradaConjuntoNumeros.EsEntradaNueva = true;
            entradaConjuntoNumeros.Entrada = new Entrada()
            {
                Tipo = TipoEntrada.ConjuntoNumeros,
                Nombre = "Vector de números"
            };

            if (textoBusqueda == null)
                operaciones.Children.Add(entradaConjuntoNumeros);
            else
            {
                if (entradaConjuntoNumeros.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                    entradaConjuntoNumeros.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(entradaConjuntoNumeros);
            }

            OperacionEspecifica suma = new OperacionEspecifica();
            suma.VistaOperaciones = this;
            suma.Tipo = TipoElementoOperacion.Suma;

            if(textoBusqueda == null)
                operaciones.Children.Add(suma);
            else
            {
                if (suma.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(suma);
            }

            OperacionEspecifica resta = new OperacionEspecifica();
            resta.VistaOperaciones = this;
            resta.Tipo = TipoElementoOperacion.Resta;

            if(textoBusqueda == null)
                operaciones.Children.Add(resta);
            else
            {
                if (resta.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(resta);
            }

            OperacionEspecifica multiplicacion = new OperacionEspecifica();
            multiplicacion.VistaOperaciones = this;
            multiplicacion.Tipo = TipoElementoOperacion.Multiplicacion;

            if(textoBusqueda == null)
                operaciones.Children.Add(multiplicacion);
            else
            {
                if (multiplicacion.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(multiplicacion);
            }

            OperacionEspecifica division = new OperacionEspecifica();
            division.VistaOperaciones = this;
            division.Tipo = TipoElementoOperacion.Division;

            if(textoBusqueda == null)
                operaciones.Children.Add(division);
            else
            {
                if (division.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(division);
            }

            OperacionEspecifica porcentaje = new OperacionEspecifica();
            porcentaje.VistaOperaciones = this;
            porcentaje.Tipo = TipoElementoOperacion.Porcentaje;

            if (textoBusqueda == null)
                operaciones.Children.Add(porcentaje);
            else
            {
                if (porcentaje.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(porcentaje);
            }

            OperacionEspecifica potencia = new OperacionEspecifica();
            potencia.VistaOperaciones = this;
            potencia.Tipo = TipoElementoOperacion.Potencia;

            if (textoBusqueda == null)
                operaciones.Children.Add(potencia);
            else
            {
                if (potencia.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(potencia);
            }

            OperacionEspecifica raiz = new OperacionEspecifica();
            raiz.VistaOperaciones = this;
            raiz.Tipo = TipoElementoOperacion.Raiz;

            if (textoBusqueda == null)
                operaciones.Children.Add(raiz);
            else
            {
                if (potencia.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(raiz);
            }

            OperacionEspecifica logaritmo = new OperacionEspecifica();
            logaritmo.VistaOperaciones = this;
            logaritmo.Tipo = TipoElementoOperacion.Logaritmo;

            if (textoBusqueda == null)
                operaciones.Children.Add(logaritmo);
            else
            {
                if (logaritmo.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(logaritmo);
            }

            OperacionEspecifica factorial = new OperacionEspecifica();
            factorial.VistaOperaciones = this;
            factorial.Tipo = TipoElementoOperacion.Factorial;

            if (textoBusqueda == null)
                operaciones.Children.Add(factorial);
            else
            {
                if (factorial.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(factorial);
            }

            OperacionEspecifica inverso = new OperacionEspecifica();
            inverso.VistaOperaciones = this;
            inverso.Tipo = TipoElementoOperacion.Inverso;

            if (textoBusqueda == null)
                operaciones.Children.Add(inverso);
            else
            {
                if (inverso.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(inverso);
            }

            OperacionEspecifica unirCantidades = new OperacionEspecifica();
            unirCantidades.VistaOperaciones = this;
            unirCantidades.Tipo = TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

            if (textoBusqueda == null)
                operaciones.Children.Add(unirCantidades);
            else
            {
                if (unirCantidades.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(unirCantidades);
            }

            OperacionEspecifica seleccionarEntradas = new OperacionEspecifica();
            seleccionarEntradas.VistaOperaciones = this;
            seleccionarEntradas.Tipo = TipoElementoOperacion.SeleccionarEntradas;

            if (textoBusqueda == null)
                operaciones.Children.Add(seleccionarEntradas);
            else
            {
                if (seleccionarEntradas.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(seleccionarEntradas);
            }

            OperacionEspecifica seleccionarOrdenar = new OperacionEspecifica();
            seleccionarOrdenar.VistaOperaciones = this;
            seleccionarOrdenar.Tipo = TipoElementoOperacion.SeleccionarOrdenar;

            if (textoBusqueda == null)
                operaciones.Children.Add(seleccionarOrdenar);
            else
            {
                if (seleccionarOrdenar.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(seleccionarOrdenar);
            }

            OperacionEspecifica condicionesFlujo = new OperacionEspecifica();
            condicionesFlujo.VistaOperaciones = this;
            condicionesFlujo.Tipo = TipoElementoOperacion.CondicionesFlujo;

            if (textoBusqueda == null)
                operaciones.Children.Add(condicionesFlujo);
            else
            {
                if (condicionesFlujo.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(condicionesFlujo);
            }

            OperacionEspecifica subCalculo = new OperacionEspecifica();
            subCalculo.VistaOperaciones = this;
            subCalculo.Tipo = TipoElementoOperacion.SubCalculo;

            if (textoBusqueda == null)
                operaciones.Children.Add(subCalculo);
            else
            {
                if (subCalculo.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(subCalculo);
            }

            OperacionEspecifica archivoExterno = new OperacionEspecifica();
            archivoExterno.VistaOperaciones = this;
            archivoExterno.Tipo = TipoElementoOperacion.ArchivoExterno;

            if (textoBusqueda == null)
                operaciones.Children.Add(archivoExterno);
            else
            {
                if (archivoExterno.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(archivoExterno);
            }

            OperacionEspecifica contarCantidades = new OperacionEspecifica();
            contarCantidades.VistaOperaciones = this;
            contarCantidades.Tipo = TipoElementoOperacion.ContarCantidades;

            if (textoBusqueda == null)
                operaciones.Children.Add(contarCantidades);
            else
            {
                if (contarCantidades.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(contarCantidades);
            }

            OperacionEspecifica redondearCantidades = new OperacionEspecifica();
            redondearCantidades.VistaOperaciones = this;
            redondearCantidades.Tipo = TipoElementoOperacion.RedondearCantidades;

            if (textoBusqueda == null)
                operaciones.Children.Add(redondearCantidades);
            else
            {
                if (redondearCantidades.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(redondearCantidades);
            }

            OperacionEspecifica espera = new OperacionEspecifica();
            espera.VistaOperaciones = this;
            espera.Tipo = TipoElementoOperacion.Espera;

            if (textoBusqueda == null)
                operaciones.Children.Add(espera);
            else
            {
                if (espera.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(espera);
            }

            OperacionEspecifica limpiarDatos = new OperacionEspecifica();
            limpiarDatos.VistaOperaciones = this;
            limpiarDatos.Tipo = TipoElementoOperacion.LimpiarDatos;

            if (textoBusqueda == null)
                operaciones.Children.Add(limpiarDatos);
            else
            {
                if (limpiarDatos.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(limpiarDatos);
            }

            OperacionEspecifica agrupador = new OperacionEspecifica();
            agrupador.VistaOperaciones = this;
            agrupador.Tipo = TipoElementoOperacion.AgrupadorOperaciones;

            if (textoBusqueda == null)
                operaciones.Children.Add(agrupador);
            else
            {
                if (agrupador.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(agrupador);
            }

            OperacionEspecifica nota = new OperacionEspecifica();
            nota.VistaOperaciones = this;
            nota.Tipo = TipoElementoOperacion.Nota;

            if (textoBusqueda == null)
                operaciones.Children.Add(nota);
            else
            {
                if (nota.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    operaciones.Children.Add(nota);
            }
        }

        public void ListarCalculos()
        {
            calculos.Children.Clear();
            calculos.RowDefinitions.Clear();
            calculos.ColumnDefinitions.Clear();

            //CalculoEspecifico nuevoCalculo = new CalculoEspecifico();
            //nuevoCalculo.CalculoDiseño = Ventana.SubCalculoSeleccionado_Operaciones;
            //nuevoCalculo.VistaOperaciones = this;
            //calculos.Children.Add(nuevoCalculo);
            ColumnDefinition definicionColumna = new ColumnDefinition();
            definicionColumna.Width = new GridLength(1, GridUnitType.Star);

            calculos.ColumnDefinitions.Add(definicionColumna);

            for (int cantidadFilas = 1; cantidadFilas <= calc.Calculos.Count((i) => !i.EsEntradasArchivo); cantidadFilas++)
            {
                RowDefinition definicionFila = new RowDefinition();
                definicionFila.Height = GridLength.Auto;

                calculos.RowDefinitions.Add(definicionFila);
            }

            int indiceFila = 0;

            foreach (var itemCalculo in calc.Calculos)
            {
                if (itemCalculo.EsEntradasArchivo) continue;

                CalculoEspecifico nuevoCalculo = new CalculoEspecifico();
                nuevoCalculo.Margin = new Thickness(10);
                nuevoCalculo.CalculoDiseño = itemCalculo;
                nuevoCalculo.VistaOperaciones = this;
                nuevoCalculo.botonFondo.Click += SeleccionarCalculo;
                calculos.Children.Add(nuevoCalculo);

                Grid.SetRow(nuevoCalculo, indiceFila);
                indiceFila++;
            }
        }

        public void ListarEntradas()
        {
            string textoBusqueda = null;

            if (BuscandoOperaciones &&
                !string.IsNullOrEmpty(busquedaOperaciones.Text))
            {
                textoBusqueda = busquedaOperaciones.Text;
            }

            entradas.Children.Clear();
            List<DiseñoCalculo> calculos = new List<DiseñoCalculo>();
            calculos.Add(Calculo.Calculos.Where(i => i.EsEntradasArchivo).FirstOrDefault());
            calculos.Add(CalculoDiseñoSeleccionado);

            foreach (var itemCalculo in calculos)
            {
                foreach (var itemEntrada in itemCalculo?.ListaEntradas.Where(i => i.Tipo != TipoEntrada.TextosInformacion))
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();                    
                    nuevaEntrada.Entrada = itemEntrada;
                    nuevaEntrada.VistaOperaciones = this;

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

            foreach (var itemCalculo in CalculoDiseñoSeleccionado.ElementosAnteriores)
            {
                if (itemCalculo.EsEntradasArchivo) continue;

                foreach (var itemEntrada in (from O in itemCalculo.ElementosOperaciones where O.ContieneSalida select O).ToList())
                {
                    if (itemEntrada.EntradaRelacionada != null && itemEntrada.EntradaRelacionada.Tipo == TipoEntrada.TextosInformacion) continue;

                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();
                    nuevaEntrada.EsEntrada = itemCalculo.EsEntradasArchivo;

                    Entrada nuevaElementoEntrada = new Entrada();
                    if(itemEntrada.Tipo != TipoElementoOperacion.Entrada)
                        nuevaElementoEntrada.Nombre = itemEntrada.Nombre + " desde " + itemCalculo.Nombre;
                    else
                        nuevaElementoEntrada.Nombre = itemEntrada.EntradaRelacionada.Nombre + " desde " + itemCalculo.Nombre;

                    nuevaElementoEntrada.Tipo = TipoEntrada.Calculo;
                    
                    nuevaElementoEntrada.ElementoSalidaCalculoAnterior = itemEntrada;
                    nuevaEntrada.Entrada = nuevaElementoEntrada;
                    nuevaEntrada.VistaOperaciones = this;

                    if(textoBusqueda == null)
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

        public void DibujarElementosOperaciones()
        {
            diagrama.Children.Clear();
            
            foreach (var itemElemento in (ModoAgrupador ? 
                Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones))
            {
                if ((ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(itemElemento)) || 
                    (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(itemElemento)))
                {
                    if (itemElemento.Tipo == TipoElementoOperacion.Entrada)
                    {
                        EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();

                        if (itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                            itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                            nuevaEntrada.EsEntrada = ((from C in Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                                ? true : false;

                        nuevaEntrada.VistaOperaciones = this;
                        nuevaEntrada.EnDiagrama = true;
                        nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
                        nuevaEntrada.Entrada = itemElemento.EntradaRelacionada;
                        nuevaEntrada.DiseñoOperacion = itemElemento;
                        nuevaEntrada.ModoIconosResumidos = ModoIconosResumidos;

                        if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                            CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado == itemElemento)
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
                    else if (itemElemento.Tipo == TipoElementoOperacion.Nota)
                    {
                        NotaDiagrama nuevaNota = new NotaDiagrama();
                        nuevaNota.VistaOperaciones = this;
                        nuevaNota.EnDiagrama = true;
                        nuevaNota.Tipo = itemElemento.Tipo;
                        nuevaNota.DiseñoOperacion = itemElemento;

                        if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                            CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado == itemElemento)
                        {
                            nuevaNota.fondo.BorderThickness = new Thickness(1);
                        }
                        else
                        {
                            nuevaNota.fondo.BorderThickness = new Thickness(0);
                        }

                        diagrama.Children.Add(nuevaNota);

                        Canvas.SetTop(nuevaNota, itemElemento.PosicionY);
                        Canvas.SetLeft(nuevaNota, itemElemento.PosicionX);
                    }
                    else
                    {
                        OperacionEspecifica nuevaOperacion = new OperacionEspecifica();
                        nuevaOperacion.VistaOperaciones = this;
                        nuevaOperacion.EnDiagrama = true;
                        nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
                        nuevaOperacion.Tipo = itemElemento.Tipo;
                        nuevaOperacion.DiseñoOperacion = itemElemento;
                        nuevaOperacion.ModoIconosResumidos = ModoIconosResumidos;

                        if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                            CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado == itemElemento)
                        {
                            nuevaOperacion.Background = SystemColors.HighlightBrush;
                            nuevaOperacion.botonFondo.Background = SystemColors.HighlightBrush;
                        }
                        else
                        {
                            nuevaOperacion.Background = SystemColors.GradientInactiveCaptionBrush;
                            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

                        }

                        diagrama.Children.Add(nuevaOperacion);

                        Canvas.SetTop(nuevaOperacion, itemElemento.PosicionY);
                        Canvas.SetLeft(nuevaOperacion, itemElemento.PosicionX);
                    }

                    //DibujarLineasElemento(itemElemento);
                    //DibujarLineasExtremos(itemElemento);
                }
            }

            DibujarTodasLineasElementos();
            EstablecerIndicesProfundidadElementos();
        }

        public void DibujarLineasElemento(DiseñoOperacion item)
        {
            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? 
                item.ElementosPosterioresAgrupados.Where(i => ModoAgrupador || (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i))) : 
                item.ElementosPosteriores.Where(i => (ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)) || 
                (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)))))
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;
                                        
                    diagrama.Children.Add(nuevaLinea);

                    if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                        CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = nuevaLinea;
                    
                }

            }

            foreach (var itemElemento in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosAnterioresAgrupados.Where(i => ModoAgrupador || (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i))) : 
                item.ElementosAnteriores.Where(i => (ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)) ||
                (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)))))
            {
                ArrowLine nuevaLinea = BuscarLinea(itemElemento, item);
                if (nuevaLinea != null)
                {
                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                    diagrama.Children.Add(nuevaLinea);

                    if (itemElemento == ElementoAnteriorLineaSeleccionada & item == ElementoPosteriorLineaSeleccionada)
                        CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = nuevaLinea;
                    
                }

            }

            if (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
            {
                foreach (var itemElemento in (ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones))
                {
                    ArrowLine nuevaLinea = BuscarLineaAgrupador(item, itemElemento);
                    if (nuevaLinea != null)
                    {
                        nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                        diagrama.Children.Add(nuevaLinea);

                        if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                            CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = nuevaLinea;
                        
                    }

                }

                foreach (var itemElemento in (ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones))
                {
                    ArrowLine nuevaLinea = BuscarLineaAgrupador(itemElemento, item);
                    if (nuevaLinea != null)
                    {
                        nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                        diagrama.Children.Add(nuevaLinea);

                        if (itemElemento == ElementoAnteriorLineaSeleccionada & item == ElementoPosteriorLineaSeleccionada)
                            CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = nuevaLinea;
                        
                    }

                }
            }
        }

        public void DibujarTodasLineasElementos()
        {
            QuitarTodasLineas();

            foreach (var itemElemento in (ModoAgrupador ?
                Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones))
            {
                if ((ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(itemElemento)) ||
                    (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(itemElemento)))
                {
                    DibujarLineasElemento(itemElemento);
                    DibujarLineasExtremos(itemElemento);
                }
            }
        }

        public void DibujarLineasExtremos(DiseñoOperacion item)
        {
            if (ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(item) && item.AgrupadorContenedor == Agrupador)
            {                
                var elementosAnteriores = (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosAnterioresAgrupados.Where(i => ModoAgrupador || (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i))) :
                item.ElementosAnteriores.Where(i => (ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)) ||
                (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)))).ToList();

                //if (elementosAnteriores.Any(i => (CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i) && i.AgrupadorContenedor != Agrupador) ||
                //!CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)))
                if ((item.ElementosAnterioresAgrupados.Any() || item.ElementosAnteriores.Any()) && 
                    !elementosAnteriores.Any())
                {
                    //ArrowLine linea = new ArrowLine();
                    //linea.Stroke = Brushes.Black;

                    //double posicionXOrigen = 0;
                    //double posicionYOrigen = 0;

                    //double posicionXDestino = 0;
                    //double posicionYDestino = 0;

                    //CalcularCoordenadasLinea(ref posicionXOrigen, ref posicionYOrigen, ref posicionXDestino,
                    //    ref posicionYDestino, null, item);

                    //linea.X1 = posicionXOrigen;
                    //linea.Y1 = posicionYOrigen;

                    //linea.X2 = posicionXDestino;
                    //linea.Y2 = posicionYDestino;

                    //diagrama.Children.Add(linea);

                    //if (item == ElementoPosteriorLineaSeleccionada)
                    //    CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = linea;
                    ArrowLine nuevaLinea = BuscarLinea(null, item);
                    if (nuevaLinea != null)
                    {

                        //nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                        diagrama.Children.Add(nuevaLinea);

                            //if (item == ElementoAnteriorLineaSeleccionada)
                            //    CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = nuevaLinea;
                        
                        //else
                        //{
                        //    //if (item == ElementoAnteriorLineaSeleccionada)
                        //    //    CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = (ArrowLine)lineaEncontrada;
                        //}
                    }

                }

                var elementosPosteriores = (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ?
                item.ElementosPosterioresAgrupados.Where(i => ModoAgrupador || (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i))) :
                item.ElementosPosteriores.Where(i => (ModoAgrupador && CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)) ||
                (!ModoAgrupador && !CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)))).ToList();

                //if (elementosPosteriores.Any(i => (CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i) && i.AgrupadorContenedor != Agrupador) ||
                //!CalculoDiseñoSeleccionado.VerificarElementoSiEsAgrupado(i)))
                if ((item.ElementosPosterioresAgrupados.Any() || item.ElementosPosteriores.Any()) && 
                    !elementosPosteriores.Any())
                {
                    //ArrowLine linea = new ArrowLine();
                    //linea.Stroke = Brushes.Black;

                    //double posicionXOrigen = 0;
                    //double posicionYOrigen = 0;

                    //double posicionXDestino = 0;
                    //double posicionYDestino = 0;

                    //CalcularCoordenadasLinea(ref posicionXOrigen, ref posicionYOrigen, ref posicionXDestino,
                    //    ref posicionYDestino, item, null);

                    //linea.X1 = posicionXOrigen;
                    //linea.Y1 = posicionYOrigen;

                    //linea.X2 = posicionXDestino;
                    //linea.Y2 = posicionYDestino;

                    //diagrama.Children.Add(linea);

                    //if (item == ElementoAnteriorLineaSeleccionada)
                    //    CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = linea;
                    ArrowLine nuevaLinea = BuscarLinea(item, null);

                    if (nuevaLinea != null)
                    {

                        //nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;

                        
                        diagrama.Children.Add(nuevaLinea);

                            //if (item == ElementoAnteriorLineaSeleccionada)
                            //    CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = nuevaLinea;
                        
                        //else
                        //{
                        //    if (item == ElementoAnteriorLineaSeleccionada)
                        //        CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = (ArrowLine)lineaEncontrada;
                        //}
                    }
                }

                //if (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                //{
                //    var elementosAgrupados = (ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones).ToList();


                //}
            }
        }

        private void diagrama_Drop(object sender, DragEventArgs e)
        {
            DiseñoOperacion arrastreHastaOtroElemento;
            arrastreHastaOtroElemento = VerificarArrastreOtroElemento(e.GetPosition(diagrama));

            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada) + CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada) + CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.ActualHeight))
                            return;


                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion && arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.Salida)
                        {
                            if ((!CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento) &&
                                !arrastreHastaOtroElemento.ElementosAnteriores.Contains(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion)))                            
                            {
                                if (arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.Entrada)
                                    //arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                                {

                                    if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ContieneSalida)
                                    {
                                        CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ContieneSalida = false;
                                        QuitarElementoSalida(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion, CalculoDiseñoSeleccionado);
                                        EstablecerTextoBotonSalida(false);
                                        //DibujarElementosOperaciones();
                                    }

                                    if (arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                                    {
                                        if (!CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento))
                                        {
                                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                            arrastreHastaOtroElemento.ElementosAnteriores.Add(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);

                                            if (arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.Entrada)
                                                arrastreHastaOtroElemento.EntradaRelacionada = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionadaArrastre.DiseñoOperacion.EntradaRelacionada;

                                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);
                                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.AgregarOrdenOperando(arrastreHastaOtroElemento);
                                            //arrastreHastaOtroElemento.OrdenarOperandos(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);

                                            //ArrowLine linea = UbicarLinea(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion, arrastreHastaOtroElemento);
                                            //linea.MouseLeftButtonDown += SeleccionarLinea;
                                            //diagrama.Children.Add(linea);
                                            //EstablecerIndicesProfundidadElementos();

                                            QuitarActualizarResultados_ElementosConectados(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);

                                            MostrarOrdenOperando_Elemento(null);

                                            DibujarTodasLineasElementos();
                                            EstablecerIndicesProfundidadElementos();

                                            Ventana.ActualizarPestañaElementoOperacion(arrastreHastaOtroElemento);
                                        }
                                    }
                                    else
                                    {                                        
                                        if (ModoAgrupador)
                                        {
                                            Agrupador.ElementosAgrupados.Remove(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.AgrupadorContenedor = null;

                                            //foreach(var item in CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosAnteriores)
                                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                                            //foreach (var item in CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosPosteriores)
                                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);
                                        }

                                        arrastreHastaOtroElemento.ElementosAgrupados.Add(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                                        CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.AgrupadorContenedor = arrastreHastaOtroElemento;
                                        //arrastreHastaOtroElemento.ElementosAnterioresAgrupados.AddRange(arrastreHastaOtroElemento.ElementosAgrupados.Last().ElementosAnteriores);
                                        //arrastreHastaOtroElemento.ElementosPosterioresAgrupados.AddRange(arrastreHastaOtroElemento.ElementosAgrupados.Last().ElementosPosteriores);

                                        DibujarTodasLineasElementos();
                                        EstablecerIndicesProfundidadElementos();
                                    }
                                }
                            }
                        }
                        else
                        {
                            EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada,
                                CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion, e);//Point ubicacionOriginal = new Point(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.PosicionX, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.PosicionY);

                            //QuitarTodasLineas();
                            //ReubicarLineasUnElemento(lineas, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion, true);

                            //lineas = BuscarLineasUnElemento(ubicacionOriginal, false, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada);
                            //foreach (var itemLinea in lineas)
                            //    diagrama.Children.Remove(itemLinea);
                            //ReubicarLineasUnElemento(lineas, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion, false);

                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.PosicionY);
                            Canvas.SetLeft(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.PosicionX);

                            //ReubicarLineasUnElemento();
                            //DibujarLineasElemento(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();

                            MostrarOrdenOperando_Elemento(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                        }
                    }
                    else
                    {
                        AgregarEntrada(sender, e);
                    }
                }
                else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada) + CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada) + CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.ActualHeight))
                            return;

                        
                        EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada,
                            CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion, e);

                        CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                        CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                        Canvas.SetTop(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada, CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion.PosicionY);
                        Canvas.SetLeft(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada, CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion.PosicionX);

                        //ReubicarLineasUnElemento();
                        //DibujarLineasElemento(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                        DibujarTodasLineasElementos();

                        EstablecerIndicesProfundidadElementos();
                        //MostrarOrdenOperando_Elemento(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                        //}
                    }
                    else
                    {
                        NotaDiagrama nuevaOperacion = new NotaDiagrama();
                        nuevaOperacion.VistaOperaciones = this;
                        nuevaOperacion.EnDiagrama = true;
                        //nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
                        //nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                        nuevaOperacion.Tipo = CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado;
                        //nuevaOperacion.SizeChanged += CambioTamañoOperacion;

                        DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
                        nuevoElementoOperacion.ID = App.GenerarID_Elemento();
                        nuevoElementoOperacion.Tipo = nuevaOperacion.Tipo;
                        nuevoElementoOperacion.PosicionX = e.GetPosition(diagrama).X;
                        nuevoElementoOperacion.PosicionY = e.GetPosition(diagrama).Y;

                        var ultimoNombre = (from DiseñoOperacion E in CalculoDiseñoSeleccionado.ElementosOperaciones where E.Tipo == CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado &&
                                            !string.IsNullOrEmpty(E.Info) && E.Info.LastIndexOf(" ") > -1 select E.Info).LastOrDefault();
                        int cantidadElementosTipo = 0;
                        if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
                        cantidadElementosTipo++;

                        switch (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado)
                        {
                            case TipoElementoOperacion.Nota:
                                nuevoElementoOperacion.Info = "Nota " + cantidadElementosTipo.ToString();
                                break;
                        }

                        CalculoDiseñoSeleccionado.ElementosOperaciones.Add(nuevoElementoOperacion);
                        nuevaOperacion.DiseñoOperacion = nuevoElementoOperacion;

                        diagrama.Children.Add(nuevaOperacion);

                        Canvas.SetTop(nuevaOperacion, e.GetPosition(diagrama).Y);
                        Canvas.SetLeft(nuevaOperacion, e.GetPosition(diagrama).X);

                        //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
                        //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

                        //EstablecerIndicesProfundidadElementos();
                        //MostrarOrdenOperando_Elemento(null);

                        if (ModoAgrupador)
                        {
                            Agrupador.ElementosAgrupados.Add(nuevoElementoOperacion);
                            nuevoElementoOperacion.AgrupadorContenedor = Agrupador;
                            //Agrupador.ElementosPosterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosPosteriores);
                            //Agrupador.ElementosAnterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosAnteriores);
                        }
                    }
                }
                else
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada != null)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.EnDiagrama)
                        {
                            if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada) &
                                e.GetPosition(diagrama).X <= Canvas.GetLeft(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada) + CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.ActualWidth) &
                                (e.GetPosition(diagrama).Y >= Canvas.GetTop(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada) &
                                e.GetPosition(diagrama).Y <= Canvas.GetTop(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada) + CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.ActualHeight))
                                return;

                            if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion && arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.Salida)
                            {
                                if ((!CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento) &&
                                !arrastreHastaOtroElemento.ElementosAnteriores.Contains(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion)))
                                {
                                    bool SeleccionEntradas = VerificarElementos_SeleccionarEntradas(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion, arrastreHastaOtroElemento);

                                    if (VerificarElementos_SeleccionarOrdenar(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion, arrastreHastaOtroElemento) &
                                        SeleccionEntradas)
                                    {
                                        if (arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.Entrada ||
                                            (SeleccionEntradas && arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.Entrada))
                                        {                                             
                                            if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo != TipoElementoOperacion.AgrupadorOperaciones && 
                                                arrastreHastaOtroElemento.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                                            {
                                                if (!CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento))
                                                {
                                                    if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ContieneSalida)
                                                    {
                                                        CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ContieneSalida = false;
                                                        QuitarElementoSalida(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion, CalculoDiseñoSeleccionado);
                                                        EstablecerTextoBotonSalida(false);
                                                        //DibujarElementosOperaciones();
                                                    }

                                                    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                                    arrastreHastaOtroElemento.ElementosAnteriores.Add(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);

                                                    if (!SeleccionEntradas && arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.Entrada)
                                                        arrastreHastaOtroElemento.EntradaRelacionada = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionadaArrastre.DiseñoOperacion.EntradaRelacionada;
                                                    else if (SeleccionEntradas && arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.Entrada)
                                                        arrastreHastaOtroElemento.EntradaRelacionada = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionadaArrastre.DiseñoOperacion.EntradaRelacionada;

                                                    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);
                                                    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.AgregarOrdenOperando(arrastreHastaOtroElemento);
                                                    //arrastreHastaOtroElemento.OrdenarOperandos();
                                                    if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo == TipoElementoOperacion.CondicionesFlujo)
                                                        CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.SalidasAgrupamiento_SeleccionCondicionesFlujo.Add(arrastreHastaOtroElemento);

                                                    if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                                                        CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.SalidasAgrupamiento_SeleccionOrdenamiento.Add(arrastreHastaOtroElemento);

                                                    //if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                                                    //    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.SalidasAgrupamiento_SeleccionEntradas.Add(arrastreHastaOtroElemento);

                                                    //ArrowLine linea = UbicarLinea(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion, arrastreHastaOtroElemento);
                                                    //linea.MouseLeftButtonDown += SeleccionarLinea;
                                                    //diagrama.Children.Add(linea);
                                                    //EstablecerIndicesProfundidadElementos();

                                                    QuitarActualizarResultados_ElementosConectados(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);

                                                    DibujarTodasLineasElementos();
                                                    EstablecerIndicesProfundidadElementos();
                                                    MostrarOrdenOperando_Elemento(null);

                                                    Ventana.ActualizarPestañaElementoOperacion(arrastreHastaOtroElemento);
                                                }
                                            }
                                            else
                                            {
                                                if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo == TipoElementoOperacion.AgrupadorOperaciones &&
                                                    arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                                                {
                                                    OpcionesArrastreAgrupadores arrastre = new OpcionesArrastreAgrupadores();
                                                    arrastre.AgrupadorSeleccionado = arrastreHastaOtroElemento;
                                                    arrastre.AgrupadorArrastre = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;

                                                    arrastre.ShowDialog();
                                                    if (arrastre.Aceptar)
                                                    {

                                                        if (ModoAgrupador)
                                                        {
                                                            Agrupador.ElementosAgrupados.Remove(arrastre.AgrupadorContenido);
                                                            arrastre.AgrupadorContenido.AgrupadorContenedor = null;

                                                            //foreach (var item in CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAnteriores)
                                                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                                                            //foreach (var item in CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosteriores)
                                                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);
                                                        }

                                                        arrastre.AgrupadorContenedor.ElementosAgrupados.Add(arrastre.AgrupadorContenido);
                                                        arrastre.AgrupadorContenido.AgrupadorContenedor = arrastre.AgrupadorContenedor;

                                                    }
                                                }
                                                else
                                                {
                                                    if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                                                    {
                                                        if (ModoAgrupador)
                                                        {
                                                            Agrupador.ElementosAgrupados.Remove(arrastreHastaOtroElemento);
                                                            arrastreHastaOtroElemento.AgrupadorContenedor = null;

                                                            //foreach (var item in arrastreHastaOtroElemento.ElementosAnteriores)
                                                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                                                            //foreach (var item in arrastreHastaOtroElemento.ElementosPosteriores)
                                                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);
                                                        }

                                                        CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAgrupados.Add(arrastreHastaOtroElemento);
                                                        arrastreHastaOtroElemento.AgrupadorContenedor = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;
                                                        //CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAnterioresAgrupados.AddRange(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAgrupados.Last().ElementosAnteriores);
                                                        //CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosterioresAgrupados.AddRange(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAgrupados.Last().ElementosPosteriores);
                                                    }
                                                    else if (arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                                                    {
                                                        if (ModoAgrupador)
                                                        {
                                                            Agrupador.ElementosAgrupados.Remove(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                                            CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.AgrupadorContenedor = null;

                                                            //foreach (var item in CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAnteriores)
                                                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                                                            //foreach (var item in CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosteriores)
                                                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);
                                                        }

                                                        arrastreHastaOtroElemento.ElementosAgrupados.Add(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                                        CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.AgrupadorContenedor = arrastreHastaOtroElemento;
                                                        //arrastreHastaOtroElemento.ElementosAnterioresAgrupados.AddRange(arrastreHastaOtroElemento.ElementosAgrupados.Last().ElementosAnteriores);
                                                        //arrastreHastaOtroElemento.ElementosPosterioresAgrupados.AddRange(arrastreHastaOtroElemento.ElementosAgrupados.Last().ElementosPosteriores);
                                                    }
                                                }

                                                DibujarElementosOperaciones();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada,
                                CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion, e);

                                CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                                CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                                Canvas.SetTop(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada, CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.PosicionY);
                                Canvas.SetLeft(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada, CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.PosicionX);

                                //ReubicarLineasUnElemento();
                                //DibujarLineasElemento(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                //DibujarElementosOperaciones();
                                //DibujarTodasLineasElementos();

                                //EstablecerIndicesProfundidadElementos();
                                DibujarTodasLineasElementos();
                                EstablecerIndicesProfundidadElementos();

                                MostrarOrdenOperando_Elemento(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                            }
                        }
                        else
                        {
                            AgregarOperacion(sender, e);                        
                        }
                    }
                }

                if(CalculoDiseñoSeleccionado != null &&
                    CalculoDiseñoSeleccionado.Seleccion != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado == null) ElementoSeleccionado = false;

                //if (MoverVerticalmente)
                //{
                //    contenedor.ScrollToVerticalOffset(diagrama.ActualHeight);
                //    MoverVerticalmente = false;
                //}
                //if (MoverHorizontalmente)
                //{
                //    contenedor.ScrollToHorizontalOffset(diagrama.ActualWidth);
                //    MoverHorizontalmente = false;
                //}
            }
            else if (CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Any())
            {
                if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                {
                    foreach (UIElement elemento in CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados)
                    {
                        if (elemento != null)
                        {
                            DiseñoOperacion elementoOperacion = null;

                            if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                            {
                                elementoOperacion = ((EntradaDiseñoOperaciones)elemento).DiseñoOperacion;
                            }
                            else if (elemento.GetType() == typeof(NotaDiagrama))
                            {
                                elementoOperacion = ((NotaDiagrama)elemento).DiseñoOperacion;
                            }
                            else if (elemento.GetType() == typeof(OperacionEspecifica))
                            {
                                elementoOperacion = ((OperacionEspecifica)elemento).DiseñoOperacion;
                            }

                            if (elementoOperacion != null && arrastreHastaOtroElemento != null)
                            {
                                if (ModoAgrupador)
                                {
                                    Agrupador.ElementosAgrupados.Remove(elementoOperacion);
                                    elementoOperacion.AgrupadorContenedor = null;

                                    //foreach (var item in elementoOperacion.ElementosAnteriores)
                                    //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                                    //foreach (var item in elementoOperacion.ElementosPosteriores)
                                    //Agrupador.ElementosPosterioresAgrupados.Remove(item);
                                }

                                arrastreHastaOtroElemento.ElementosAgrupados.Add(elementoOperacion);
                                elementoOperacion.AgrupadorContenedor = arrastreHastaOtroElemento;
                                //arrastreHastaOtroElemento.ElementosAnterioresAgrupados.AddRange(elementoOperacion.ElementosAnteriores);
                                //arrastreHastaOtroElemento.ElementosPosterioresAgrupados.AddRange(elementoOperacion.ElementosPosteriores);
                            }
                        }
                    }

                    DibujarElementosOperaciones();
                }
                else
                {
                    Point diferenciaDistanciaPunto = new Point(0, 0);

                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover.GetType() == typeof(EntradaDiseñoOperaciones))
                    {
                        EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover,
                            ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion, e);

                        diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX,
                            ubicacionActualElemento.Y - ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);

                        ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                        ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                        Canvas.SetTop(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover, ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);
                        Canvas.SetLeft(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover, ((EntradaDiseñoOperaciones)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX);

                    }
                    else if (CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover.GetType() == typeof(NotaDiagrama))
                    {
                        EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover,
                            ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion, e);

                        diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX,
                            ubicacionActualElemento.Y - ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);

                        ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                        ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                        Canvas.SetTop(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover, ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);
                        Canvas.SetLeft(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover, ((NotaDiagrama)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX);
                    }
                    else if (CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover.GetType() == typeof(OperacionEspecifica))
                    {
                        EstablecerCoordenadasElementoMover(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover,
                            ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion, e);

                        diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX,
                            ubicacionActualElemento.Y - ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);

                        ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX = ubicacionActualElemento.X;
                        ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY = ubicacionActualElemento.Y;

                        Canvas.SetTop(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover, ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionY);
                        Canvas.SetLeft(CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover, ((OperacionEspecifica)CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover).DiseñoOperacion.PosicionX);
                    }

                    foreach (UIElement elemento in CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados)
                    {
                        if (elemento != null)
                        {
                            if (elemento != CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionadoMover)
                            {
                                if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                                {
                                    ((EntradaDiseñoOperaciones)elemento).DiseñoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                    ((EntradaDiseñoOperaciones)elemento).DiseñoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                    Canvas.SetTop(elemento, ((EntradaDiseñoOperaciones)elemento).DiseñoOperacion.PosicionY);
                                    Canvas.SetLeft(elemento, ((EntradaDiseñoOperaciones)elemento).DiseñoOperacion.PosicionX);

                                }
                                else if (elemento.GetType() == typeof(NotaDiagrama))
                                {
                                    ((NotaDiagrama)elemento).DiseñoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                    ((NotaDiagrama)elemento).DiseñoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                    Canvas.SetTop(elemento, ((NotaDiagrama)elemento).DiseñoOperacion.PosicionY);
                                    Canvas.SetLeft(elemento, ((NotaDiagrama)elemento).DiseñoOperacion.PosicionX);
                                }
                                else if (elemento.GetType() == typeof(OperacionEspecifica))
                                {
                                    ((OperacionEspecifica)elemento).DiseñoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                    ((OperacionEspecifica)elemento).DiseñoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                    Canvas.SetTop(elemento, ((OperacionEspecifica)elemento).DiseñoOperacion.PosicionY);
                                    Canvas.SetLeft(elemento, ((OperacionEspecifica)elemento).DiseñoOperacion.PosicionX);
                                }
                            }
                        }
                    }

                    //QuitarTodasLineas();
                    DibujarTodasLineasElementos();
                    EstablecerIndicesProfundidadElementos();
                }
            }
            //else if(ClicDiagrama)
            //{


            //    ClicDiagrama = false;
            //    //diagrama.InvalidateVisual();
            //    //}, DispatcherPriority.Render);
            //}
            
        }

        public void AgregarOperacion(object sender, DragEventArgs e)
        {
            OperacionEspecifica nuevaOperacion = new OperacionEspecifica();
            nuevaOperacion.VistaOperaciones = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaOperacion.Tipo = CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado;
            nuevaOperacion.SizeChanged += CambioTamañoOperacion;

            DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.Tipo = nuevaOperacion.Tipo;

            double X_Punto = 10;
            double Y_Punto = 10;

            if (e != null)
            {
                nuevoElementoOperacion.PosicionX = e.GetPosition(diagrama).X;
                nuevoElementoOperacion.PosicionY = e.GetPosition(diagrama).Y;
            }
            else
            {
                nuevoElementoOperacion.PosicionX = X_Punto;
                nuevoElementoOperacion.PosicionY = Y_Punto;
            }

            var ultimoNombre = (from DiseñoOperacion E in CalculoDiseñoSeleccionado.ElementosOperaciones where E.Tipo == CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            cantidadElementosTipo++;

            switch (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado)
            {
                case TipoElementoOperacion.Suma:
                    nuevoElementoOperacion.Nombre = "Suma " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Resta:
                    nuevoElementoOperacion.Nombre = "Resta " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Multiplicacion:
                    nuevoElementoOperacion.Nombre = "Multiplicación " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Division:
                    nuevoElementoOperacion.Nombre = "División " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Porcentaje:
                    nuevoElementoOperacion.Nombre = "Porcentaje " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Potencia:
                    nuevoElementoOperacion.Nombre = "Potencia " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Raiz:
                    nuevoElementoOperacion.Nombre = "Raíz " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Logaritmo:
                    nuevoElementoOperacion.Nombre = "Logaritmo " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Factorial:
                    nuevoElementoOperacion.Nombre = "Factorial " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Inverso:
                    nuevoElementoOperacion.Nombre = "Inverso " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.ContarCantidades:
                    nuevoElementoOperacion.Nombre = "Contar números " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.SeleccionarOrdenar:
                    nuevoElementoOperacion.Nombre = "Lógica de selección de números " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.CondicionesFlujo:
                    nuevoElementoOperacion.Nombre = "Lógica de selección de variables o vectores " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.SeleccionarEntradas:
                    nuevoElementoOperacion.Nombre = "Lógica de selección de variables o vectores de entrada " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.AgrupadorOperaciones:
                    nuevoElementoOperacion.Nombre = "Agrupador de variables o vectores y lógicas " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                    nuevoElementoOperacion.Nombre = "Números obtenidos " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.Espera:
                    nuevoElementoOperacion.Nombre = "Esperar datos " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.LimpiarDatos:
                    nuevoElementoOperacion.Nombre = "Limpiar datos " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.RedondearCantidades:
                    nuevoElementoOperacion.Nombre = "Redondear números " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.ArchivoExterno:
                    nuevoElementoOperacion.Nombre = "Ejecutar archivo de cálculo externo " + cantidadElementosTipo.ToString();
                    break;
                case TipoElementoOperacion.SubCalculo:
                    nuevoElementoOperacion.Nombre = "Ejecutar subcálculo " + cantidadElementosTipo.ToString();
                    nuevoElementoOperacion.ConfigSubCalculo.SubCalculo_Operacion.NombreArchivo = nuevoElementoOperacion.Nombre;
                    break;
            }

            nuevoElementoOperacion.Actualizar_ToolTips = true;

            CalculoDiseñoSeleccionado.ElementosOperaciones.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoOperacion = nuevoElementoOperacion;

            diagrama.Children.Add(nuevaOperacion);

            if (e != null)
            {
                Canvas.SetTop(nuevaOperacion, e.GetPosition(diagrama).Y);
                Canvas.SetLeft(nuevaOperacion, e.GetPosition(diagrama).X);
            }
            else
            {
                Canvas.SetTop(nuevaOperacion, Y_Punto);
                Canvas.SetLeft(nuevaOperacion, X_Punto);
            }

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            EstablecerIndicesProfundidadElementos();
            MostrarOrdenOperando_Elemento(null);

            if (ModoAgrupador)
            {
                Agrupador.ElementosAgrupados.Add(nuevoElementoOperacion);
                nuevoElementoOperacion.AgrupadorContenedor = Agrupador;
                //Agrupador.ElementosPosterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosPosteriores);
                //Agrupador.ElementosAnterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosAnteriores);
            }
            else
            {
                CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada = nuevaOperacion;
            }

            if (nuevoElementoOperacion.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
            {
                CalculoDiseñoSeleccionado.Agrupadores.Add(nuevoElementoOperacion);
                //nuevoElementoOperacion.AnchoDiagrama = diagrama.ActualWidth;
                //nuevoElementoOperacion.AltoDiagrama = diagrama.ActualHeight;
            }

            double X = 0;
            double Y = 0;

            if(e != null)
            {
                X = e.GetPosition(diagrama).X;
                Y = e.GetPosition(diagrama).Y;
            }
            else
            {
                X = X_Punto;
                Y = Y_Punto;
            }

            var AgrupadorEncontrado = EncontrarAgrupador_Arrastre(nuevoElementoOperacion, X, Y);

            if (AgrupadorEncontrado != null)
            {
                AgrupadorEncontrado.ElementosAgrupados.Add(nuevoElementoOperacion);
                nuevoElementoOperacion.AgrupadorContenedor = AgrupadorEncontrado;

                DibujarElementosOperaciones();
            }

            //if (nuevoElementoOperacion.ToolTip != null)
            //    nuevaOperacion.botonFondo.ToolTip = nuevoElementoOperacion.ToolTip;
            //else
            //{
            //Ventana.ObtenerEjecucionToolTips(Calculo).AgregarToolTipElemento_Asociaciones((ToolTipElementoVisual)((ContentControl)nuevaOperacion.botonFondo.ToolTip).Content,
            //        nuevoElementoOperacion.ID);
            //    Calculo.ConCambiosVisuales = true;
            //}
        }

        public void AgregarEntrada(object sender, DragEventArgs e)
        {
            Entrada entradaAAgregar;

            double X_Punto = 10;
            double Y_Punto = 10;

            if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.EsEntradaNueva)
            {
                var ultimoNombre = (from DiseñoOperacion E in CalculoDiseñoSeleccionado.ElementosOperaciones
                                    where E.Tipo == CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado &&
                                    !string.IsNullOrEmpty(E.NombreCombo) && E.NombreCombo.LastIndexOf(" ") > -1
                                    select E.NombreCombo).LastOrDefault();
                int cantidadElementosTipo = 0;
                if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
                cantidadElementosTipo++;

                entradaAAgregar = new Entrada()
                {
                    Tipo = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.Tipo,
                    Nombre = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.Nombre + " " + cantidadElementosTipo.ToString()
                };

                entradaAAgregar.ID = App.GenerarID_Elemento();
                entradaAAgregar.CalculoDiseñoAsociado = CalculoDiseñoSeleccionado;
                CalculoDiseñoSeleccionado.ListaEntradas.Add(entradaAAgregar);
                Calculo.AgregarConfiguracionEntrada_Ejecucion(entradaAAgregar);

                SeleccionarCalculo(sender, e);
            }
            else
            {
                entradaAAgregar = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada;
            }

            EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();

            if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior != null &&
                CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                nuevaEntrada.EsEntrada = ((from C in Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                    ? true : false;

            nuevaEntrada.VistaOperaciones = this;
            nuevaEntrada.EnDiagrama = true;
            nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
            nuevaEntrada.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaEntrada.Entrada = entradaAAgregar;
            nuevaEntrada.SizeChanged += CambioTamañoEntrada;

            DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.EntradaRelacionada = nuevaEntrada.Entrada;
            nuevoElementoOperacion.Tipo = CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado;

            if (e != null)
            {
                nuevoElementoOperacion.PosicionX = e.GetPosition(diagrama).X;
                nuevoElementoOperacion.PosicionY = e.GetPosition(diagrama).Y;
            }
            else
            {
                nuevoElementoOperacion.PosicionX = X_Punto;
                nuevoElementoOperacion.PosicionY = Y_Punto;
            }

            nuevoElementoOperacion.Actualizar_ToolTips = true;

            CalculoDiseñoSeleccionado.ElementosOperaciones.Add(nuevoElementoOperacion);
            nuevaEntrada.DiseñoOperacion = nuevoElementoOperacion;

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
            MostrarOrdenOperando_Elemento(null);

            if (ModoAgrupador)
            {
                Agrupador.ElementosAgrupados.Add(nuevoElementoOperacion);
                nuevoElementoOperacion.AgrupadorContenedor = Agrupador;
                //Agrupador.ElementosPosterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosPosteriores);
                //Agrupador.ElementosAnterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosAnteriores);
            }
            else
            {
                CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada = nuevaEntrada;
            }

            //if (itemElemento.ToolTip != null)
            //    nuevaOperacion.botonFondo.ToolTip = itemElemento.ToolTip;
            //else
            //{
            //Ventana.ObtenerEjecucionToolTips(Calculo).AgregarToolTipElemento_Asociaciones((ToolTipElementoVisual)((ContentControl)nuevaEntrada.botonFondo.ToolTip).Content,
            //    nuevoElementoOperacion.ID);
            //nuevoElementoOperacion.EntradaRelacionada.ConCambios_ToolTips = true;
            //Calculo.ConCambiosVisuales = true;
            //}
        }
        private DiseñoOperacion EncontrarAgrupador_Arrastre(DiseñoOperacion elementoActual, double x, double y)
        {
            DiseñoOperacion agrupador = null;

            foreach (UIElement controlAgrupador in diagrama.Children)
            {
                if(controlAgrupador.GetType() == typeof(OperacionEspecifica) && 
                    ((x >= Canvas.GetLeft(controlAgrupador) &
                                x <= Canvas.GetLeft(controlAgrupador) + ((OperacionEspecifica)controlAgrupador).ActualWidth) &
                                (y >= Canvas.GetTop(controlAgrupador) &
                                y <= Canvas.GetTop(controlAgrupador) + ((OperacionEspecifica)controlAgrupador).ActualHeight)
                                && ((OperacionEspecifica)controlAgrupador).DiseñoOperacion != elementoActual &&
                                ((OperacionEspecifica)controlAgrupador).DiseñoOperacion.Tipo == TipoElementoOperacion.AgrupadorOperaciones))
                {
                    agrupador = ((OperacionEspecifica)controlAgrupador).DiseñoOperacion;
                    break;
                }
            }

            return agrupador;
        }

        public void AgruparElementosSeleccionados(List<DiseñoOperacion> ElementosSeleccionados)
        {
            if (ElementosSeleccionados.Any())
            {
                var PosicionX = (from E in ElementosSeleccionados
                                            orderby E.PosicionX ascending
                                            select E.PosicionX).FirstOrDefault();

                var PosicionY = (from E in ElementosSeleccionados
                                 orderby E.PosicionY ascending
                                 select E.PosicionY).FirstOrDefault();

                OperacionEspecifica nuevaOperacion = new OperacionEspecifica();
                nuevaOperacion.VistaOperaciones = this;
                nuevaOperacion.EnDiagrama = true;
                nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
                nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                nuevaOperacion.Tipo = TipoElementoOperacion.AgrupadorOperaciones;
                nuevaOperacion.SizeChanged += CambioTamañoOperacion;

                DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
                nuevoElementoOperacion.ID = App.GenerarID_Elemento();
                nuevoElementoOperacion.Tipo = nuevaOperacion.Tipo;
                nuevoElementoOperacion.PosicionX = PosicionX;
                nuevoElementoOperacion.PosicionY = PosicionY;
                //nuevoElementoOperacion.AnchoDiagrama = diagrama.ActualWidth;
                //nuevoElementoOperacion.AltoDiagrama = diagrama.ActualHeight;

                var ultimoNombre = (from DiseñoOperacion E in CalculoDiseñoSeleccionado.ElementosOperaciones where E.Tipo == CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
                int cantidadElementosTipo = 0;
                if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
                cantidadElementosTipo++;

                nuevoElementoOperacion.Nombre = "Agrupador de variables o vectores y lógicas " + cantidadElementosTipo.ToString();
                
                CalculoDiseñoSeleccionado.ElementosOperaciones.Add(nuevoElementoOperacion);
                nuevaOperacion.DiseñoOperacion = nuevoElementoOperacion;

                diagrama.Children.Add(nuevaOperacion);

                Canvas.SetTop(nuevaOperacion, PosicionY);
                Canvas.SetLeft(nuevaOperacion, PosicionX);

                //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
                //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

                EstablecerIndicesProfundidadElementos();
                MostrarOrdenOperando_Elemento(null);

                if (ModoAgrupador)
                {
                    Agrupador.ElementosAgrupados.Add(nuevoElementoOperacion);
                    nuevoElementoOperacion.AgrupadorContenedor = Agrupador;
                    //Agrupador.ElementosPosterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosPosteriores);
                    //Agrupador.ElementosAnterioresAgrupados.AddRange(nuevoElementoOperacion.ElementosAnteriores);
                }
                
                if(nuevoElementoOperacion.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                    CalculoDiseñoSeleccionado.Agrupadores.Add(nuevoElementoOperacion);
                
                foreach (var elemento in ElementosSeleccionados)
                {
                    if (elemento != null)
                    {
                        if (ModoAgrupador)
                        {
                            Agrupador.ElementosAgrupados.Remove(elemento);
                            elemento.AgrupadorContenedor = null;

                            //foreach(var item in elemento.ElementosAnteriores)
                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                            //foreach (var item in elemento.ElementosPosteriores)
                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);
                        }

                        nuevoElementoOperacion.ElementosAgrupados.Add(elemento);
                        elemento.AgrupadorContenedor = nuevoElementoOperacion;
                        //nuevoElementoOperacion.ElementosAnterioresAgrupados.AddRange(elemento.ElementosAnteriores);
                        //nuevoElementoOperacion.ElementosPosterioresAgrupados.AddRange(elemento.ElementosPosteriores);                        
                    }
                }

                //DibujarElementosOperaciones();
                //DibujarTodasLineasElementos();
            }

            DibujarElementosOperaciones();
            diagrama_MouseLeftButtonDown(this, null);
        }

        private void EstablecerCoordenadasElementoMover(UIElement elemento, DiseñoOperacion elementoOperacion, DragEventArgs e)
        {
            Point puntoElemento;

            if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                puntoElemento = ((EntradaDiseñoOperaciones)elemento).PuntoMouseClic;
            else if(elemento.GetType() == typeof(OperacionEspecifica))
                puntoElemento = ((OperacionEspecifica)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(NotaDiagrama))
                puntoElemento = ((NotaDiagrama)elemento).PuntoMouseClic;

            //Point diferencia = new Point(puntoElemento.X - elementoOperacion.PosicionX,
            //    puntoElemento.Y - elementoOperacion.PosicionY);

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


                ubicacionActualElemento = new Point(elementoOperacion.PosicionX + ubicacionActualElemento.X - puntoElemento.X,
               elementoOperacion.PosicionY + ubicacionActualElemento.Y - puntoElemento.Y);

        }

        private void QuitarTodasLineas()
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var itemLinea in lineas)
                diagrama.Children.Remove(itemLinea);
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
            //else
            //{
            //    //if (posicionXDestino < elementoOrigen.PosicionX)
            //    //    posicionXOrigen -= elementoOrigen.Anchura / 2;
            //    //else if (posicionXDestino > elementoOrigen.PosicionX + elementoOrigen.Anchura)
            //        posicionXOrigen += elementoOrigen.Anchura / 2;

            //    if (posicionYDestino < elementoOrigen.PosicionY)
            //        posicionYOrigen -= elementoOrigen.Altura / 2;
            //    else if (posicionYDestino > elementoOrigen.PosicionY + elementoOrigen.Altura)
            //        posicionYOrigen += elementoOrigen.Altura / 2;
            //}

            //if (elementoOrigen.PosicionX < elementoDestino.PosicionX &
            //    (elementoDestino.PosicionX > elementoOrigen.PosicionX + elementoOrigen.Anchura))
            //    posicionXOrigen = elementoOrigen.PosicionX + elementoOrigen.Anchura;
            //else if (elementoOrigen.PosicionX > elementoDestino.PosicionX)
            //    posicionXOrigen = elementoOrigen.PosicionX;
            //else
            //    posicionXOrigen = elementoOrigen.PosicionX + elementoOrigen.Anchura / 2;

            //if (elementoOrigen.PosicionY < elementoDestino.PosicionY &
            //    (elementoDestino.PosicionY > elementoOrigen.PosicionY + elementoOrigen.Altura))
            //    posicionYOrigen = elementoOrigen.PosicionY + elementoOrigen.Altura;
            //else if (elementoOrigen.PosicionY > elementoDestino.PosicionY)
            //    posicionYOrigen = elementoOrigen.PosicionY;
            //else
            //    posicionYOrigen = elementoOrigen.PosicionY + elementoOrigen.Altura / 2;

            //if (elementoDestino.PosicionX > elementoOrigen.PosicionX &
            //    (elementoOrigen.PosicionX > elementoDestino.PosicionX + elementoDestino.Altura))
            //    posicionXDestino = elementoDestino.PosicionX;
            //else if (elementoDestino.PosicionX < elementoOrigen.PosicionX)
            //    posicionXDestino = elementoDestino.PosicionX + elementoDestino.Anchura;
            //else
            //    posicionXDestino = elementoDestino.PosicionX + elementoDestino.Anchura / 2;

            //if (elementoDestino.PosicionY > elementoOrigen.PosicionY &
            //    (elementoOrigen.PosicionY > elementoDestino.PosicionY + elementoDestino.Altura))
            //    posicionYDestino = elementoDestino.PosicionY;
            //else if (elementoDestino.PosicionY < elementoOrigen.PosicionY)
            //    posicionYDestino = elementoOrigen.PosicionY + elementoDestino.Altura;
            //else
            //    posicionYDestino = elementoDestino.PosicionY + elementoDestino.Altura / 2;
        }
                
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo &&
                Calculo.ModoSubCalculo_Simple)
            {
                grillaCalculos.Visibility = Visibility.Collapsed;
            }

            CargarDatos();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("VerAdministrarOperacionesCalculo");
#endif
        }

        private void CargarDatosCalculoSeleccionado()
        {
            if(ModoAgrupador)
            {
                NombreCalculoSeleccionado.Text = "Cálculo: " + CalculoDiseñoSeleccionado.Nombre + " - " +
                "Agrupador: " + Agrupador.Nombre;
            }
            else
                NombreCalculoSeleccionado.Text = CalculoDiseñoSeleccionado.Nombre;

            ListarEntradas();

            entradas.Width = contenedorEntradas.ActualWidth;
            operaciones.Width = contenedorOperaciones.ActualWidth;
            //contenedorOperaciones.MaxHeight = grillaHerramientas.ActualHeight;
            //contenedorEntradas.MaxHeight = grillaHerramientas.ActualHeight;

            if (ModoAgrupador)
            {
                if (double.IsNaN(Agrupador.AnchoDiagrama) &
                double.IsNaN(Agrupador.AltoDiagrama))
                {
                    //CalculoDiseñoSeleccionado.Ancho = diagrama.Width;
                    //CalculoDiseñoSeleccionado.Alto = diagrama.Height;

                    diagrama.Width = contenedor.ActualWidth;
                    Agrupador.AnchoDiagrama = diagrama.Width;

                    diagrama.Height = contenedor.ActualHeight;
                    Agrupador.AltoDiagrama = diagrama.Height;

                }
                else
                {
                    diagrama.Width = Agrupador.AnchoDiagrama;
                    diagrama.Height = Agrupador.AltoDiagrama;
                }
            }
            else
            {
                if (double.IsNaN(CalculoDiseñoSeleccionado.Ancho) &
                    double.IsNaN(CalculoDiseñoSeleccionado.Alto))
                {
                    //CalculoDiseñoSeleccionado.Ancho = diagrama.Width;
                    //CalculoDiseñoSeleccionado.Alto = diagrama.Height;

                    diagrama.Width = contenedor.ActualWidth;
                    CalculoDiseñoSeleccionado.Ancho = diagrama.Width;

                    diagrama.Height = contenedor.ActualHeight;
                    CalculoDiseñoSeleccionado.Alto = diagrama.Height;

                }
                else
                {
                    diagrama.Width = CalculoDiseñoSeleccionado.Ancho;
                    diagrama.Height = CalculoDiseñoSeleccionado.Alto;
                }
            }

            DibujarElementosOperaciones();
        }

        public void SeleccionarCalculo(object sender, RoutedEventArgs e)
        {
            if(sender != null && sender.GetType() == typeof(CalculoEspecifico))
                CalculoDiseñoSeleccionado = ((CalculoEspecifico)((Control)sender).Parent).CalculoDiseño;

            Calculo.SubCalculoSeleccionado_Operaciones = CalculoDiseñoSeleccionado;
            CargarDatosCalculoSeleccionado();

            //CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos = false;
            //contenedorOrdenOperandos.Visibility = Visibility.Collapsed;

            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos)
            {
                contenedorOrdenOperandos.Visibility = Visibility.Visible;
            }
            else
            {
                contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            }

            //Action accion = new Action(DestacarElementoSeleccionado);
            //Ventana.Dispatcher.Invoke(DestacarElementoSeleccionado);
            ////DestacarElementoSeleccionado();

            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                SeleccionandoElemento = true;
                try
                {
                    foreach (var itemElemento in diagrama.Children)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                        {
                            if (itemElemento.GetType() == typeof(EntradaDiseñoOperaciones) &&
                                ((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado)
                            //((EntradaDiseñoOperaciones)itemElemento).Button_PreviewMouseLeftButtonDown(this, CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento);
                            {
                                CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada = (EntradaDiseñoOperaciones)itemElemento;
                                //Action accion = new Action();
                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Clic, DispatcherPriority.Loaded);
                                    //if (((EntradaDiseñoOperaciones)itemElemento).EnDiagrama && ((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion != null) EstablecerTextoBotonSalida(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion.ContieneSalida);
                                    //MostrarOrdenOperando_Elemento(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion);
                                }
                            }
                        }
                        if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota)
                        {
                            if (itemElemento.GetType() == typeof(NotaDiagrama) &&
                                ((NotaDiagrama)itemElemento).DiseñoOperacion == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado)
                            //((EntradaDiseñoOperaciones)itemElemento).Button_PreviewMouseLeftButtonDown(this, CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento);
                            {
                                CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada = (NotaDiagrama)itemElemento;
                                //Action accion = new Action();
                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.Clic, DispatcherPriority.Loaded);
                                    //if (((EntradaDiseñoOperaciones)itemElemento).EnDiagrama && ((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion != null) EstablecerTextoBotonSalida(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion.ContieneSalida);
                                    //MostrarOrdenOperando_Elemento(((EntradaDiseñoOperaciones)itemElemento).DiseñoOperacion);
                                }
                            }
                        }
                        else
                        {
                            if (itemElemento.GetType() == typeof(OperacionEspecifica) &&
                                ((OperacionEspecifica)itemElemento).DiseñoOperacion == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado)
                            //((OperacionEspecifica)itemElemento).Button_PreviewMouseLeftButtonDown(this, CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento);
                            {
                                CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada = (OperacionEspecifica)itemElemento;

                                if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                {
                                    Ventana.Dispatcher.Invoke(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.Clic, DispatcherPriority.Loaded);
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

            //Ventana.Dispatcher.Invoke(btnDefinirOrdenOperandos_Click, DispatcherPriority.Background);
            //if (CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos)
            //{
            //    ListarCalculos();
            //    contenedorOrdenOperandos.Visibility = Visibility.Visible;
            //}
            //else
            //    contenedorOrdenOperandos.Visibility = Visibility.Collapsed;

            //diagrama.UpdateLayout();
            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento && 
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                MostrarInfo_Elemento(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado);
            }

            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoConfiguracionSeparadores &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                MostrarOcultarConfig_Separadores();
            }

            zoom.Text = CalculoDiseñoSeleccionado.Seleccion.CantidadZoom.ToString();

            textoBusquedaDiagrama.Text = CalculoDiseñoSeleccionado.Seleccion.TextoBusquedaDiagrama;

            if (!string.IsNullOrEmpty(textoBusquedaDiagrama.Text))
            {
                List<UIElement> elementos = BuscarElementosDiagramas(textoBusquedaDiagrama.Text);

                CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Clear();
                if (elementos != null && elementos.Any())
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.AddRange(elementos);                    
                }
            }

            if (CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Count > 1)
            {
                resultadosBusquedas.Visibility = Visibility.Visible;
            }
            else
                resultadosBusquedas.Visibility = Visibility.Collapsed;

            foreach (var itemElemento in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
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
        private DiseñoOperacion VerificarArrastreOtroElemento(Point ubicacion)
        {
            foreach (var itemElemento in (ModoAgrupador ? 
                Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones))
            {
                var itemControl = (from UIElement C in diagrama.Children where 
                                   ((C.GetType() == typeof(OperacionEspecifica) && ((OperacionEspecifica)C).DiseñoOperacion == itemElemento)) ||
                                   ((C.GetType() == typeof(EntradaDiseñoOperaciones) && ((EntradaDiseñoOperaciones)C).DiseñoOperacion == itemElemento)) select C).FirstOrDefault();

                if (itemControl != null)
                {
                    if (ubicacion.X >= itemElemento.PosicionX & ubicacion.X <= itemElemento.PosicionX + itemControl.RenderSize.Width &
                        ubicacion.Y >= itemElemento.PosicionY & ubicacion.Y <= itemElemento.PosicionY + itemControl.RenderSize.Height)
                    {
                        if (itemControl.GetType() == typeof(OperacionEspecifica))
                        {
                            CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionadaArrastre = (OperacionEspecifica)itemControl;
    }
                        else if (itemControl.GetType() == typeof(EntradaDiseñoOperaciones))
                        {
                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionadaArrastre = (EntradaDiseñoOperaciones)itemControl;
                        }

                        return itemElemento;
                    }
                }
            }
            return null;
        }

        private ArrowLine BuscarLinea(DiseñoOperacion elementoOrigen, DiseñoOperacion elementoDestino)
        {
            foreach (var item in (ModoAgrupador ? Agrupador.ElementosAgrupados: CalculoDiseñoSeleccionado.ElementosOperaciones))
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


                var elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                 //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E.PosicionX == elementoOrigen?.PosicionX & E.PosicionY == elementoOrigen?.PosicionY & item.PosicionX == elementoDestino?.PosicionX & item.PosicionY == elementoDestino?.PosicionY
                                                 select E).FirstOrDefault();

                if(elementoDestinoEncontrado == null)
                {
                    //if(elementoOrigen == null)
                    //{
                    //    elementoDestinoEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                    //                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                    //                                 where item.PosicionX == elementoDestino?.PosicionX & item.PosicionY == elementoDestino?.PosicionY
                    //                                 select E).FirstOrDefault();
                    //}
                    
                    if(elementoDestino == null)
                    {
                        elementoDestinoEncontrado = (from DiseñoOperacion E in Agrupador.ElementosAgrupados
                                                         //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                     where E.PosicionX == elementoOrigen?.PosicionX & E.PosicionY == elementoOrigen?.PosicionY
                                                     select E).FirstOrDefault();
                    }
                }

                var elementoOrigenEncontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                where E.PosicionX == elementoDestino?.PosicionX & E.PosicionY == elementoDestino?.PosicionY & item.PosicionX == elementoOrigen?.PosicionX & item.PosicionY == elementoOrigen?.PosicionY
                                                select E).FirstOrDefault();

                if (elementoOrigenEncontrado == null)
                {
                    if (elementoOrigen == null)
                    {
                        elementoOrigenEncontrado = (from DiseñoOperacion E in Agrupador.ElementosAgrupados
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

                var elementoDestino2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
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
                        elementoDestino2Encontrado = (from DiseñoOperacion E in Agrupador.ElementosAgrupados
                                                          //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                      where E == elementoOrigen
                                                      select E).FirstOrDefault();
                    }
                }

                var elementoOrigen2Encontrado = (from DiseñoOperacion E in (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Where(i => (!ModoAgrupador) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                 //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E == elementoDestino && item == elementoOrigen
                                                 select E).FirstOrDefault();

                if (elementoOrigen2Encontrado == null)
                {
                    if (elementoOrigen == null)
                    {
                        elementoOrigen2Encontrado = (from DiseñoOperacion E in Agrupador.ElementosAgrupados
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
                if(elementoOrigen2Encontrado != null | elementoDestino2Encontrado != null)
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

        private ArrowLine BuscarLineaAgrupador(DiseñoOperacion elementoOrigen, DiseñoOperacion elementoDestino)
        {
            foreach (var item in (ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(item => item.Tipo == TipoElementoOperacion.AgrupadorOperaciones))
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


                var elementoDestinoEncontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresAnteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                 .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY & item.PosicionX == elementoDestino.PosicionX & item.PosicionY == elementoDestino.PosicionY
                                                 select E).FirstOrDefault();

                if (elementoDestinoEncontrado == null)
                {
                    if (elementoDestino == null)
                    {
                        elementoDestinoEncontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresAnteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                 .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                         //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                     where E.PosicionX == elementoOrigen.PosicionX & E.PosicionY == elementoOrigen.PosicionY
                                                     select E).FirstOrDefault();
                    }
                }

                    var elementoOrigenEncontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresPosteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                    //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY & item.PosicionX == elementoOrigen.PosicionX & item.PosicionY == elementoOrigen.PosicionY
                                                select E).FirstOrDefault();

                if (elementoOrigenEncontrado == null)
                {
                    if (elementoOrigen == null)
                    {
                        elementoOrigenEncontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresPosteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                        //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                    where E.PosicionX == elementoDestino.PosicionX & E.PosicionY == elementoDestino.PosicionY
                                                    select E).FirstOrDefault();
                    }
                }

                    var elementoDestino2Encontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresPosteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                  .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                      //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                  where E == elementoOrigen && item == elementoDestino
                                                  select E).FirstOrDefault();

                if (elementoDestino2Encontrado == null)
                {
                    if (elementoDestino == null)
                    {
                        elementoDestino2Encontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresPosteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                  .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                          //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosPosterioresAgrupados : item.ElementosPosteriores).Contains(j))))

                                                      where E == elementoOrigen
                                                      select E).FirstOrDefault();
                    }
                }

                    var elementoOrigen2Encontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresAnteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                 .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                     //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                 where E == elementoDestino && item == elementoOrigen
                                                 select E).FirstOrDefault();

                if (elementoOrigen2Encontrado == null)
                {
                    if (elementoOrigen == null)
                    {
                        elementoOrigen2Encontrado = (from DiseñoOperacion E in item.ObtenerTodosElementosAgrupadoresAnteriores(item.AgrupadorContenedor != null ? item.AgrupadorContenedor.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones)
                                                 .Where(i => (!ModoAgrupador && i.AgrupadorContenedor == null) || (ModoAgrupador && Agrupador.ElementosAgrupados.Contains(i)))
                                                         //.Union((ModoAgrupador ? Agrupador.ElementosAgrupados : CalculoDiseñoSeleccionado.ElementosOperaciones).Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones && i.ElementosAgrupados.Any(j => (item.Tipo == TipoElementoOperacion.AgrupadorOperaciones ? item.ElementosAnterioresAgrupados : item.ElementosAnteriores).Contains(j))))

                                                     where E == elementoDestino
                                                     select E).FirstOrDefault();
                    }
                }

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
            //if (elementoOrigen)
            //{
            //    var lineas = (from UIElement L in diagrama.Children
            //                  where (L.GetType() == typeof(ArrowLine)) &&
            //                  elemento.Clip.FillContains(new Point(((ArrowLine)L).X1, ((ArrowLine)L).Y1))
            //                  select L).ToList();

            //    foreach (var item in lineas)
            //        lineasEncontradas.Add((ArrowLine)item);
            //}
            //else
            //{
            //    var lineas = (from UIElement L in diagrama.Children
            //                  where (L.GetType() == typeof(ArrowLine)) &&
            //                  elemento.Clip.FillContains(new Point(((ArrowLine)L).X2, ((ArrowLine)L).Y2))
            //                  select L).ToList();

            //    foreach (var item in lineas)
            //        lineasEncontradas.Add((ArrowLine)item);
            //}

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

        private void CambioTamañoEntrada(object sender, SizeChangedEventArgs e)
        {
            EntradaDiseñoOperaciones entrada = (EntradaDiseñoOperaciones)sender;
            entrada.DiseñoOperacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoOperacion.Altura = entrada.ActualHeight;
        }

        public void CambioTamañoOperacion(object sender, SizeChangedEventArgs e)
        {
            OperacionEspecifica entrada = (OperacionEspecifica)sender;
            entrada.DiseñoOperacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoOperacion.Altura = entrada.ActualHeight;
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

        private void btnEsSalida_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Salida |
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota |
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.SeleccionarEntradas |
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.AgrupadorOperaciones) return;

                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada != null)
                    {
                        if ((from E in CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ElementosPosteriores where E.Tipo != TipoElementoOperacion.Salida select E).ToList().Count == 0)
                        {
                            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ContieneSalida = !CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ContieneSalida;

                            if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ContieneSalida)
                            {
                                if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                {
                                    Resultado resultado = new Resultado();
                                    resultado.ID = App.GenerarID_Elemento();
                                    resultado.SalidaRelacionada = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion;
                                    calc.ListaResultados.Add(resultado);
                                }
                                AgregarElementoSalida(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                            }
                            else
                            {
                                //if (Ventana.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count > 0)
                                //{
                                QuitarElementoSalida_ElementoPosterior(Calculo.SubCalculoSeleccionado_Operaciones, CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                                //}
                                //else
                                if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                {
                                    var itemSalida = (from Resultado E in calc.ListaResultados where E.SalidaRelacionada == CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion select E).FirstOrDefault();
                                    if (itemSalida != null)
                                        calc.ListaResultados.Remove(itemSalida);
                                }

                                QuitarElementoSalida(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion, CalculoDiseñoSeleccionado);
                            }

                            var tabResultados = (from TabItem T in Ventana.contenido.Items where T.Content.GetType() == typeof(VistaResultados) select T).FirstOrDefault();
                            if (tabResultados != null)
                                ((VistaResultados)tabResultados.Content).ListarResultados();

                            EstablecerTextoBotonSalida(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion.ContieneSalida);

                            MostrarInfo_Elemento(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                        }
                        else
                            MessageBox.Show("Esta variable o vector de entrada tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado del cálculo.", "Salida", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna &
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada != null)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                            CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.Tipo == TipoElementoOperacion.CondicionesFlujo)
                        {
                            agregarNuevoElementoSalida_Click(this, e);
                        }
                        else
                        {
                            if ((from E in CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosPosteriores where E.Tipo != TipoElementoOperacion.Salida select E).ToList().Count == 0)
                            {
                                CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ContieneSalida = !CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ContieneSalida;

                                if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ContieneSalida)
                                {
                                    if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                    {
                                        Resultado resultado = new Resultado();
                                        resultado.ID = App.GenerarID_Elemento();
                                        resultado.SalidaRelacionada = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;
                                        calc.ListaResultados.Add(resultado);
                                    }

                                    AgregarElementoSalida(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                }
                                else
                                {
                                    //if (Ventana.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count > 0)
                                    //{
                                    QuitarElementoSalida_ElementoPosterior(Calculo.SubCalculoSeleccionado_Operaciones, CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                    //}
                                    //else
                                    if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                    {
                                        var itemSalida = (from Resultado E in calc.ListaResultados where E.SalidaRelacionada == CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion select E).FirstOrDefault();
                                        if (itemSalida != null)
                                            calc.ListaResultados.Remove(itemSalida);
                                    }

                                    QuitarElementoSalida(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion, CalculoDiseñoSeleccionado);
                                }

                                var tabResultados = (from TabItem T in Ventana.contenido.Items where T.Content != null && T.Content.GetType() == typeof(VistaResultados) select T).FirstOrDefault();
                                if (tabResultados != null)
                                    ((VistaResultados)tabResultados.Content).ListarResultados();

                                EstablecerTextoBotonSalida(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ContieneSalida);

                                MostrarInfo_Elemento(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                            }
                            else
                                MessageBox.Show("Esta variable o vector tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado del cálculo.", "Salida", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                }

                
            }
            else
            {
                foreach (var itemElemento in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
                {
                    if (itemElemento.Tipo == TipoElementoOperacion.Salida |
                    itemElemento.Tipo == TipoElementoOperacion.Nota |
                    itemElemento.Tipo == TipoElementoOperacion.SeleccionarEntradas) return;

                    if (itemElemento.Tipo == TipoElementoOperacion.Entrada)
                    {
                        if (itemElemento != null)
                        {
                            if ((from E in itemElemento.ElementosPosteriores where E.Tipo != TipoElementoOperacion.Salida select E).ToList().Count == 0)
                            {
                                itemElemento.ContieneSalida = !itemElemento.ContieneSalida;

                                if (itemElemento.ContieneSalida)
                                {
                                    if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                    {
                                        Resultado resultado = new Resultado();
                                        resultado.ID = App.GenerarID_Elemento();
                                        resultado.SalidaRelacionada = itemElemento;
                                        calc.ListaResultados.Add(resultado);
                                    }
                                    AgregarElementoSalida(itemElemento);
                                }
                                else
                                {
                                    //if (Ventana.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count > 0)
                                    //{
                                    QuitarElementoSalida_ElementoPosterior(Calculo.SubCalculoSeleccionado_Operaciones, itemElemento);
                                    //}
                                    //else
                                    if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                    {
                                        var itemSalida = (from Resultado E in calc.ListaResultados where E.SalidaRelacionada == itemElemento select E).FirstOrDefault();
                                        if (itemSalida != null)
                                            calc.ListaResultados.Remove(itemSalida);
                                    }

                                    QuitarElementoSalida(itemElemento, CalculoDiseñoSeleccionado);
                                }

                                var tabResultados = (from TabItem T in Ventana.contenido.Items where T.Content.GetType() == typeof(VistaResultados) select T).FirstOrDefault();
                                if (tabResultados != null)
                                    ((VistaResultados)tabResultados.Content).ListarResultados();

                                EstablecerTextoBotonSalida(itemElemento.ContieneSalida);
                            }
                            else
                                MessageBox.Show("La variable o vector '" + itemElemento.Nombre + "' tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado del cálculo.", "Salida", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else if (itemElemento.Tipo != TipoElementoOperacion.Entrada &
                        itemElemento.Tipo != TipoElementoOperacion.Ninguna &
                        itemElemento.Tipo != TipoElementoOperacion.Linea)
                    {
                        if (itemElemento != null)
                        {
                            if (itemElemento.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                                itemElemento.Tipo == TipoElementoOperacion.CondicionesFlujo)
                            {
                                if (itemElemento.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                                {
                                    AgregarElementoSalida_AgrupadoSeleccionOrdenamiento(itemElemento);
                                }
                                else if (itemElemento.Tipo == TipoElementoOperacion.CondicionesFlujo)
                                {
                                    AgregarElementoSalida_AgrupadoSeleccionCondiciones(itemElemento);
                                }
                            }
                            else
                            {
                                if ((from E in itemElemento.ElementosPosteriores where E.Tipo != TipoElementoOperacion.Salida select E).ToList().Count == 0)
                                {
                                    itemElemento.ContieneSalida = !itemElemento.ContieneSalida;

                                    if (itemElemento.ContieneSalida)
                                    {
                                        if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                        {
                                            Resultado resultado = new Resultado();
                                            resultado.ID = App.GenerarID_Elemento();
                                            resultado.SalidaRelacionada = itemElemento;
                                            calc.ListaResultados.Add(resultado);
                                        }

                                        AgregarElementoSalida(itemElemento);
                                    }
                                    else
                                    {
                                        //if (Ventana.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count > 0)
                                        //{
                                        QuitarElementoSalida_ElementoPosterior(Calculo.SubCalculoSeleccionado_Operaciones, itemElemento);
                                        //}
                                        //else
                                        if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
                                        {
                                            var itemSalida = (from Resultado E in calc.ListaResultados where E.SalidaRelacionada == itemElemento select E).FirstOrDefault();
                                            if (itemSalida != null)
                                                calc.ListaResultados.Remove(itemSalida);
                                        }

                                        QuitarElementoSalida(itemElemento, Calculo.SubCalculoSeleccionado_Operaciones);
                                    }

                                    var tabResultados = (from TabItem T in Ventana.contenido.Items where T.Content != null && T.Content.GetType() == typeof(VistaResultados) select T).FirstOrDefault();
                                    if (tabResultados != null)
                                        ((VistaResultados)tabResultados.Content).ListarResultados();

                                    EstablecerTextoBotonSalida(itemElemento.ContieneSalida);
                                }
                                else
                                    MessageBox.Show("La variable o vector '" + itemElemento.Nombre + "' tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado del cálculo.", "Salida", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                    }
                }
            }

            DibujarElementosOperaciones();
        }

        public void QuitarElementoSalida_ElementoPosterior(DiseñoCalculo elemento, DiseñoOperacion elementoSalida)
        {
            foreach (var itemCalculoPosterior in elemento.ElementosPosteriores)
            {
                QuitarElementoSalida_ElementoPosterior(itemCalculoPosterior, elementoSalida);

                List<DiseñoOperacion> itemsAEliminar = new List<DiseñoOperacion>();

                foreach (var itemElementoRelacionado in itemCalculoPosterior.ElementosOperaciones)
                {
                    if (itemElementoRelacionado.EntradaRelacionada != null && 
                        itemElementoRelacionado.EntradaRelacionada.Tipo == TipoEntrada.Calculo &&
                        itemElementoRelacionado.EntradaRelacionada.ElementoSalidaCalculoAnterior == elementoSalida)
                    {
                        foreach (var itemElemento in itemCalculoPosterior.ElementosOperaciones)
                        {
                            QuitarDeElementosPosterioresAnteriores(itemElemento, itemElementoRelacionado);                            
                        }

                        Ventana.CerrarPestañasElementoOperacionEliminado(itemElementoRelacionado);
                        itemsAEliminar.Add(itemElementoRelacionado);
                    }
                }

                while (itemsAEliminar.Count > 0)
                {
                    QuitarElementoSalida(itemsAEliminar.First(), itemCalculoPosterior);
                    itemCalculoPosterior.ElementosOperaciones.Remove(itemsAEliminar.First());
                    itemsAEliminar.Remove(itemsAEliminar.First());
                }

                //itemCalculoPosterior.ElementosOperaciones.Remove(elementoSalida);
                //ActualizarContenedoresElementos_CalculoPosterior(itemCalculoPosterior, elementoSalida);
            }
        }

        public void EstablecerTextoBotonSalida(bool esSalida)
        {
            if (esSalida)
                etiquetaBtnEsSalida.Content = "Quitar variable o vector retornado";
            else
                etiquetaBtnEsSalida.Content = "Agregar variable o vector retornado";
        }

        public void AgregarElementoSalida(DiseñoOperacion elemento)
        {
            DiseñoOperacion elementoSalida = new DiseñoOperacion();
            elementoSalida.ID = App.GenerarID_Elemento();
            elementoSalida.ElementosAnteriores.Add(elemento);
            //elementoSalida.ElementosContenedoresOperacion.Add(elemento);
            elementoSalida.Nombre = "Variable o vector retornado";
            elementoSalida.PosicionX = elemento.PosicionX + elemento.Anchura + 30;
            elementoSalida.PosicionY = elemento.PosicionY + elemento.Altura;
            elementoSalida.Tipo = TipoElementoOperacion.Salida;

            Calculo.SubCalculoSeleccionado_Operaciones.ElementosOperaciones.Add(elementoSalida);
            elemento.ElementosPosteriores.Add(elementoSalida);
        }

        public void QuitarElementoSalida(DiseñoOperacion elemento, DiseñoCalculo elementoCalculo)
        {
            DiseñoOperacion elementoSalida = (from DiseñoOperacion S in elemento.ElementosPosteriores where S.Tipo == TipoElementoOperacion.Salida select S).FirstOrDefault();

            elementoCalculo.ElementosOperaciones.Remove(elementoSalida);
            elemento.ElementosPosteriores.Remove(elementoSalida);

            foreach (var itemCalculo in elementoCalculo.ElementosPosteriores)
                QuitarElementos_EntradasDesdeCalculo(elemento, itemCalculo);
        }

        public void QuitarElementos_EntradasDesdeCalculo(DiseñoOperacion elemento, DiseñoCalculo elementoCalculo)
        {
            foreach(var itemCondiciones in elementoCalculo.ElementosOperaciones)
            {
                if (itemCondiciones.EntradaRelacionada != null &&
                    itemCondiciones.EntradaRelacionada.ElementoSalidaCalculoAnterior == elemento)
                    QuitarElementoDiagrama(itemCondiciones);
                //if(itemCondiciones.Tipo == TipoElementoOperacion.CondicionesFlujo)
                //{
                //    foreach(var itemCondicion in itemCondiciones.CondicionesFlujo_SeleccionOrdenamiento)
                //    {
                //        itemCondicion.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
                //    }
                //}
                //else if(itemCondiciones.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                //{
                //    foreach (var itemCondicion in itemCondiciones.CondicionesTextosInformacion_SeleccionOrdenamiento)
                //    {
                //        itemCondicion.Condiciones.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
                //    }
                //}
            }
        }

        private void btnQuitarOperacion_Click(object sender, RoutedEventArgs e)
        {
            bool quitarElemento = true;
            bool confirmacionQuitar = false;

            if (ModoAgrupador && CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
            {
                SeleccionarOpcionQuitarElemento quitar = new SeleccionarOpcionQuitarElemento();
                quitar.ShowDialog();

                if (quitar.Aceptar)
                {
                    confirmacionQuitar = true;

                    if (ElementoSeleccionado)
                    {
                        DiseñoOperacion diseñoOperacion = null;

                        if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Salida)
                        {
                            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                            {
                                diseñoOperacion = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion;
                            }
                            else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota)
                            {
                                diseñoOperacion = CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion;
                            }
                            else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
                            {
                                diseñoOperacion = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;
                            }
                        }

                        if (diseñoOperacion != null)
                        {
                            //foreach (var item in diseñoOperacion.ElementosAnteriores)
                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                            //foreach (var item in diseñoOperacion.ElementosPosteriores)
                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);

                            Agrupador.ElementosAgrupados.Remove(diseñoOperacion);
                            diseñoOperacion.AgrupadorContenedor = null;

                            if (Agrupador.AgrupadorContenedor != null && !quitar.QuitarElementosCalculo)
                            {
                                Agrupador.AgrupadorContenedor.ElementosAgrupados.Add(diseñoOperacion);
                                diseñoOperacion.AgrupadorContenedor = Agrupador.AgrupadorContenedor;
                                //Agrupador.AgrupadorContenedor.ElementosPosterioresAgrupados.AddRange(diseñoOperacion.ElementosPosteriores);
                                //Agrupador.AgrupadorContenedor.ElementosAnterioresAgrupados.AddRange(diseñoOperacion.ElementosAnteriores);
                            }
                        }
                    }
                    else
                    {
                        foreach (var itemElemento in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
                        {
                            //foreach (var item in itemElemento.ElementosAnteriores)
                            //    Agrupador.ElementosAnterioresAgrupados.Remove(item);

                            //foreach (var item in itemElemento.ElementosPosteriores)
                            //    Agrupador.ElementosPosterioresAgrupados.Remove(item);

                            Agrupador.ElementosAgrupados.Remove(itemElemento);
                            itemElemento.AgrupadorContenedor = null;

                            if (Agrupador.AgrupadorContenedor != null && !quitar.QuitarElementosCalculo)
                            {
                                Agrupador.AgrupadorContenedor.ElementosAgrupados.Add(itemElemento);
                                itemElemento.AgrupadorContenedor = Agrupador.AgrupadorContenedor;
                                //Agrupador.AgrupadorContenedor.ElementosPosterioresAgrupados.AddRange(itemElemento.ElementosPosteriores);
                                //Agrupador.AgrupadorContenedor.ElementosAnterioresAgrupados.AddRange(itemElemento.ElementosAnteriores);
                            }
                        }
                    }

                    if (!quitar.QuitarElementosCalculo)
                        quitarElemento = false;
                }
                else
                    return;
            }

            if (quitarElemento)
            {
                if (ElementoSeleccionado)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Salida) return;

                    if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada != null)
                        {
                            MessageBoxResult resp = MessageBoxResult.Yes;

                            if (!confirmacionQuitar)
                            {
                                resp = MessageBox.Show("¿Quitar esta variable o vector de entrada de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            }
                            
                            if (resp == MessageBoxResult.Yes)
                            {
                                QuitarElementoDiagrama(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion);
                                                                
                                diagrama.Children.Remove(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada);
                                //Calculo.ListaEntradas_Visuales.Remove(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada);
                                //Calculo.ConCambiosVisuales = true;
                                CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada = null;
                                //DibujarTodasLineasElementos();
                                DibujarElementosOperaciones();

                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
                            }
                        }
                    }
                    else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada != null)
                        {
                            MessageBoxResult resp = MessageBoxResult.Yes;

                            if (!confirmacionQuitar)
                            {
                                resp = MessageBox.Show("¿Quitar esta nota de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            }

                            if (resp == MessageBoxResult.Yes)
                            {
                                CalculoDiseñoSeleccionado.ElementosOperaciones.Remove(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion);

                                if (ModoAgrupador)
                                {
                                    Agrupador.ElementosAgrupados.Remove(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion);
                                    CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion.AgrupadorContenedor = null;
                                }

                                ActualizarContenedoresElementos(CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion);

                                diagrama.Children.Remove(CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada);
                                CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada = null;
                                //DibujarTodasLineasElementos();
                                DibujarElementosOperaciones();

                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
                            }
                        }
                    }
                    else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada != null)
                        {
                            if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                            {
                                MessageBoxResult resp = MessageBoxResult.Yes;

                                if (!confirmacionQuitar)
                                {
                                    resp = MessageBox.Show("¿Quitar esta variable o vector de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                }
                                
                                if (resp == MessageBoxResult.Yes)
                                {
                                    QuitarElementoDiagrama(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                    
                                    diagrama.Children.Remove(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada);
                                    //Calculo.ListaOperaciones_Visuales.Remove(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada);
                                    //Calculo.ConCambiosVisuales = true;
                                    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada = null;

                                    //DibujarTodasLineasElementos();
                                    DibujarElementosOperaciones();

                                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
                                }
                            }
                            else
                            {
                                SeleccionarOpcionesDeshacerAgrupador deshacer = new SeleccionarOpcionesDeshacerAgrupador();
                                deshacer.ShowDialog();

                                if (deshacer.Aceptar)
                                {
                                    CalculoDiseñoSeleccionado.Agrupadores.Remove(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);
                                    QuitarElementoDiagrama(CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion);

                                    if (deshacer.QuitarElementosCalculo)
                                    {
                                        foreach (var item in CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion.ElementosAgrupados)
                                            QuitarElementoDiagrama(item);
                                    }
                                }
                            }
                        }
                    }
                    else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Linea)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada != null)
                        {
                            if (ElementoAnteriorLineaSeleccionada != null & ElementoPosteriorLineaSeleccionada != null)
                            {
                                List<DiseñoElementoOperacion> listaQuitarElementoSalida = new List<DiseñoElementoOperacion>();
                                List<DiseñoElementoOperacion> listaQuitarElementos = new List<DiseñoElementoOperacion>();

                                if (ElementoAnteriorLineaSeleccionada.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                                {
                                    var entradasAsociadas = ElementoAnteriorLineaSeleccionada.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Where(i => i.ElementoSalida_Operacion == ElementoPosteriorLineaSeleccionada).ToList();
                                    while (entradasAsociadas.Any())
                                    {
                                        ElementoAnteriorLineaSeleccionada.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Remove(entradasAsociadas.FirstOrDefault());
                                        entradasAsociadas.Remove(entradasAsociadas.FirstOrDefault());
                                    }
                                }

                                foreach (var itemElementoDiseñoOperacion in ElementoPosteriorLineaSeleccionada.ElementosDiseñoOperacion)
                                {
                                    if (itemElementoDiseñoOperacion.ElementoDiseñoRelacionado == ElementoAnteriorLineaSeleccionada)
                                    {
                                        if (itemElementoDiseñoOperacion.ContieneSalida)
                                        {
                                            listaQuitarElementoSalida.Add(itemElementoDiseñoOperacion);
                                        }

                                        foreach (var item in ElementoPosteriorLineaSeleccionada.ElementosDiseñoOperacion)
                                        {
                                            //QuitarDeElementosPosterioresAnteriores()
                                            var itemEncontrado = (from DiseñoElementoOperacion E in item.ElementosAnteriores where E == itemElementoDiseñoOperacion select E).FirstOrDefault();
                                            if (itemEncontrado != null)
                                            {
                                                item.ElementosAnteriores.Remove(itemEncontrado);
                                                itemEncontrado.QuitarOrdenOperando(item);

                                            }

                                            itemEncontrado = (from DiseñoElementoOperacion E in item.ElementosPosteriores where E == itemElementoDiseñoOperacion select E).FirstOrDefault();
                                            if (itemEncontrado != null)
                                            {
                                                item.ElementosPosteriores.Remove(itemEncontrado);
                                                item.QuitarOrdenOperando(itemEncontrado);
                                            }
                                        }

                                        listaQuitarElementos.Add(itemElementoDiseñoOperacion);

                                        //ActualizarContenedoresElementos()
                                        foreach (var itemElemento in ElementoPosteriorLineaSeleccionada.ElementosDiseñoOperacion)
                                        {
                                            if (itemElemento.ElementosContenedoresOperacion.Contains(itemElementoDiseñoOperacion))
                                                itemElemento.ElementosContenedoresOperacion.Remove(itemElementoDiseñoOperacion);
                                        }
                                    }
                                }

                                QuitarReferenciasElementos_DefinicionNombresCantidades(ElementoAnteriorLineaSeleccionada, ElementoPosteriorLineaSeleccionada);

                                while (listaQuitarElementoSalida.Any())
                                {
                                    DiseñoElementoOperacion elementoSalida = (from DiseñoElementoOperacion S in listaQuitarElementoSalida.First().ElementosPosteriores where S.Tipo == TipoElementoDiseñoOperacion.Salida select S).FirstOrDefault();

                                    ElementoPosteriorLineaSeleccionada.ElementosDiseñoOperacion.Remove(elementoSalida);
                                    listaQuitarElementoSalida.First().ElementosPosteriores.Remove(elementoSalida);

                                    listaQuitarElementoSalida.Remove(listaQuitarElementoSalida.First());
                                }

                                while (listaQuitarElementos.Any())
                                {
                                    ElementoPosteriorLineaSeleccionada.ElementosDiseñoOperacion.Remove(listaQuitarElementos.First());
                                    listaQuitarElementos.Remove(listaQuitarElementos.First());
                                }

                                Ventana.ActualizarPestañaElementoOperacion(ElementoPosteriorLineaSeleccionada);

                                QuitarDeAsociacionesTextosInformacion_CondicionFlujo(ElementoPosteriorLineaSeleccionada);
                                QuitarDeAsociacionesTextosInformacion_SeleccionarOrdenar(ElementoPosteriorLineaSeleccionada);
                                QuitarDeAsociacionesTextosInformacion_SeleccionarEntradas(ElementoPosteriorLineaSeleccionada);

                                foreach(var itemCondicion in ElementoPosteriorLineaSeleccionada.ProcesamientoCantidades)
                                    itemCondicion.QuitarElemento(ElementoAnteriorLineaSeleccionada);

                                ElementoAnteriorLineaSeleccionada.ElementosPosteriores.Remove(ElementoPosteriorLineaSeleccionada);
                                ElementoPosteriorLineaSeleccionada.ElementosAnteriores.Remove(ElementoAnteriorLineaSeleccionada);
                                ElementoAnteriorLineaSeleccionada.ElementosContenedoresOperacion.Remove(ElementoPosteriorLineaSeleccionada);

                                ElementoAnteriorLineaSeleccionada.QuitarOrdenOperando(ElementoPosteriorLineaSeleccionada);
                                //ElementoPosteriorLineaSeleccionada.OrdenarOperandos(ElementoAnteriorLineaSeleccionada);

                                ActualizarPestañasDefinicionElementosPosteriores(ElementoAnteriorLineaSeleccionada);
                                DibujarElementosOperaciones();
                            }
                        }
                    }

                    diagrama_MouseLeftButtonDown(this, null);
                }
                else
                {
                    foreach (var itemElemento in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
                    {
                        if (itemElemento.Tipo == TipoElementoOperacion.Salida) continue;

                        if (itemElemento.Tipo == TipoElementoOperacion.Entrada)
                        {
                            QuitarElementoDiagrama(itemElemento);
                            
                            EntradaDiseñoOperaciones EntradaSeleccionada = (EntradaDiseñoOperaciones)(from UIElement E in diagrama.Children
                                                                                                      where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                                                                ((EntradaDiseñoOperaciones)E).DiseñoOperacion == itemElemento
                                                                                                      select E).FirstOrDefault();

                            diagrama.Children.Remove(EntradaSeleccionada);
                            //CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada = null;
                            //DibujarTodasLineasElementos();
                            //DibujarElementosOperaciones();

                            //CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
                            //}
                            //}
                        }
                        else if (itemElemento.Tipo == TipoElementoOperacion.Nota)
                        {
                            CalculoDiseñoSeleccionado.ElementosOperaciones.Remove(itemElemento);

                            if (ModoAgrupador)
                            {
                                Agrupador.ElementosAgrupados.Remove(itemElemento);
                                itemElemento.AgrupadorContenedor = null;
                            }

                            ActualizarContenedoresElementos(itemElemento);

                            NotaDiagrama notaDiagramaSeleccionada = (NotaDiagrama)(from UIElement E in diagrama.Children
                                                                                   where E.GetType() == typeof(NotaDiagrama) &&
                                             ((NotaDiagrama)E).DiseñoOperacion == itemElemento
                                                                                   select E).FirstOrDefault();

                            diagrama.Children.Remove(notaDiagramaSeleccionada);
                        }
                        else if (itemElemento.Tipo != TipoElementoOperacion.Entrada &
                            itemElemento.Tipo != TipoElementoOperacion.Ninguna &
                            itemElemento.Tipo != TipoElementoOperacion.Linea)
                        {
                            if (itemElemento.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                            {
                                QuitarElementoDiagrama(itemElemento);
                                
                                OperacionEspecifica OperacionSeleccionada = (OperacionEspecifica)(from UIElement E in diagrama.Children
                                                                                                  where E.GetType() == typeof(OperacionEspecifica) &&
                                                            ((OperacionEspecifica)E).DiseñoOperacion == itemElemento
                                                                                                  select E).FirstOrDefault();

                                diagrama.Children.Remove(OperacionSeleccionada);
                                //CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada = null;

                                //DibujarTodasLineasElementos();
                                //DibujarElementosOperaciones();

                                //CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
                                //}
                                //}
                            }
                            else
                            {
                                SeleccionarOpcionesDeshacerAgrupador deshacer = new SeleccionarOpcionesDeshacerAgrupador();
                                deshacer.ShowDialog();

                                if (deshacer.Aceptar)
                                {
                                    CalculoDiseñoSeleccionado.Agrupadores.Remove(itemElemento);
                                    QuitarElementoDiagrama(itemElemento);

                                    if (deshacer.QuitarElementosCalculo)
                                    {
                                        foreach (var item in itemElemento.ElementosAgrupados)
                                            QuitarElementoDiagrama(item);
                                    }
                                }
                            }
                        }
                    }

                    DibujarElementosOperaciones();
                    diagrama_MouseLeftButtonDown(this, null);
                }
            }

            DibujarElementosOperaciones();
            diagrama_MouseLeftButtonDown(this, null);
        }

        public void ActualizarContenedoresElementos(DiseñoOperacion elemento)
        {
            foreach (var itemElemento in CalculoDiseñoSeleccionado.ElementosOperaciones)
            {
                if (itemElemento.ElementosContenedoresOperacion.Contains(elemento))
                    itemElemento.ElementosContenedoresOperacion.Remove(elemento);
            }
        }

        public void QuitarDeElementosPosterioresAnteriores(DiseñoOperacion elemento, DiseñoOperacion elementoAQuitar)
        {
            var item = (from DiseñoOperacion E in elemento.ElementosAnteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                elemento.ElementosAnteriores.Remove(item);
                item.QuitarOrdenOperando(elemento);
                //item.OrdenarOperandos(elemento);
            }

            item = (from DiseñoOperacion E in elemento.ElementosPosteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                elemento.ElementosPosteriores.Remove(item);
                elemento.QuitarOrdenOperando(item);
                //elemento.OrdenarOperandos(item);
            }

            //elementoAQuitar.OrdenarOperandos(elemento);
            //elemento.OrdenarTodosOperandos();
        }

        public void QuitarReferenciasElementos_DefinicionNombresCantidades(DiseñoOperacion elemento, DiseñoOperacion elementoReferencias)
        {
            List<OpcionesNombreCantidad_TextosInformacion> elementosAquitar = new List<OpcionesNombreCantidad_TextosInformacion>();

            foreach (var itemDefinicion in elementoReferencias.DefinicionOpcionesNombresCantidades.OpcionesTextos)
            {
                if (itemDefinicion.Operando == elemento)
                {
                    elementosAquitar.Add(itemDefinicion);
                }
                else
                {
                    if (itemDefinicion.Condiciones.OperandoCondicion == elemento)
                        itemDefinicion.Condiciones.OperandoCondicion = null;
                    else
                    {
                        itemDefinicion.Condiciones.QuitarCondicionElementoDiseñoCondicion_Condiciones(elemento);
                    }
                }
            }

            while (elementosAquitar.Any())
            {
                elementoReferencias.DefinicionOpcionesNombresCantidades.OpcionesTextos.Remove(elementosAquitar.FirstOrDefault());
                elementosAquitar.Remove(elementosAquitar.FirstOrDefault());
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
        public void QuitarReferenciasElementosImplicacion_AsignacionTextosInformacion(DiseñoOperacion elemento, Calculo calculo)
        {
            foreach(var item in calculo.TextosInformacion.ElementosTextosInformacion)
            {
                foreach(var itemDefinicion in item.Relaciones_TextosInformacion)
                {
                    if(itemDefinicion.Condiciones_TextoCondicion != null)
                        itemDefinicion.Condiciones_TextoCondicion.QuitarReferenciasCondicionesElemento(elemento);
                    
                    foreach (var itemInstancia in itemDefinicion.InstanciasAsignacion)
                    {
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(itemInstancia.Entradas_DesdeAsignarTextosInformacion_DefinicionSusTextos_Definiciones, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(itemInstancia.Entradas_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(itemInstancia.Entradas_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros, elemento);
                        QuitarEntradasReferencias_Elemento_InstanciaImplicacion(itemInstancia.Entradas_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion(itemInstancia.Operandos_AsignarTextosInformacionA, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_AsignarTextosInformacionCuando, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesComoTextos, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_OperandosCondiciones(itemInstancia.Operandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_OperandosCondiciones(itemInstancia.Operandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.Operandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual, elemento);
                    }
                }
            }
        }

        private void QuitarEntradasReferencias_Elemento_InstanciaImplicacion(List<DiseñoTextosInformacion> Entradas, DiseñoOperacion elemento)
        {
            List<DiseñoTextosInformacion> elementosAQuitar = new List<DiseñoTextosInformacion>();

            foreach (var itemElementoInstancia in Entradas)
            {
                if (itemElementoInstancia.EntradaRelacionada == elemento.EntradaRelacionada)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Entradas.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion(List<AsignacionTextosOperando_Implicacion> Operandos, DiseñoOperacion elemento)
        {
            List<AsignacionTextosOperando_Implicacion> elementosAQuitar = new List<AsignacionTextosOperando_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Operando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(List<DiseñoOperacion> Operandos, DiseñoOperacion elemento)
        {
            List<DiseñoOperacion> elementosAQuitar = new List<DiseñoOperacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> Operandos, DiseñoOperacion elemento)
        {
            List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.OperacionRelacionada == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);

                if(itemElementoInstancia.Operando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Operandos, DiseñoOperacion elemento)
        {
            List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.OperacionRelacionada == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.Operando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> Operandos, DiseñoOperacion elemento)
        {
            List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada != null && 
                    (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.OperacionRelacionada == elemento))
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.Operando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion_OperandosCondiciones(List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Operandos, DiseñoOperacion elemento)
        {
            List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada != null && 
                    (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.OperacionRelacionada == elemento))
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.Operando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }
        private void btnDefinirConfigurarOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Salida |
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota) return;

                DiseñoOperacion ElementoDiseñoOperacionSeleccionado = null;
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
                    {
                        ElementoDiseñoOperacionSeleccionado = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;

                        if (ElementoDiseñoOperacionSeleccionado != null)
                        {
                            //if ((CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.CondicionesFlujo &&
                            //    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.AgrupadorOperaciones ||
                            //    (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.CondicionesFlujo ||
                            //    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.AgrupadorOperaciones)))
                            //{
                            bool mostrarDefinicion = false;

                            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SeleccionarOrdenar &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.CondicionesFlujo &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SeleccionarEntradas &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Espera &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.LimpiarDatos &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.RedondearCantidades &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.ArchivoExterno &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SubCalculo &
                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.AgrupadorOperaciones)
                            {
                                SeleccionarDefinicion definir = new SeleccionarDefinicion();
                                definir.DefinicionSimple = ElementoDiseñoOperacionSeleccionado.DefinicionSimple_Operacion;
                                bool opcionElegida = (bool)definir.ShowDialog();
                                if (opcionElegida)
                                {
                                    ElementoDiseñoOperacionSeleccionado.DefinicionSimple_Operacion = definir.DefinicionSimple;

                                    if (!definir.DefinicionSimple)
                                        mostrarDefinicion = true;
                                    else
                                    {
                                        if ((CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Potencia ||
                                            (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Potencia &&
                                            ElementoDiseñoOperacionSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos)) &&
                                            (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Raiz ||
                                            (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Raiz &&
                                            ElementoDiseñoOperacionSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos)) &&
                                            (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Logaritmo ||
                                            (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Logaritmo &&
                                            ElementoDiseñoOperacionSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Inverso &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Factorial &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Espera &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.LimpiarDatos &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SubCalculo &&
                                            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.ArchivoExterno)
                                        {
                                            SeleccionarTipoOperacionEjecucion definirTipo = new SeleccionarTipoOperacionEjecucion();

                                            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Potencia |
                                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Raiz |
                                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Porcentaje |
                                                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Logaritmo)
                                            {
                                                definirTipo.MostrarOpcionPorSeparado = false;
                                            }

                                            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.ContarCantidades)
                                            {
                                                definirTipo.MostrarOpcionPorFilas = false;
                                            }

                                            definirTipo.TipoOperacion = ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion;
                                            opcionElegida = (bool)definirTipo.ShowDialog();

                                            if (opcionElegida)
                                            {
                                                ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion = definirTipo.TipoOperacion;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                                {
                                    SeleccionarDefinicion definir = new SeleccionarDefinicion();
                                    definir.DefinicionSimple = ElementoDiseñoOperacionSeleccionado.DefinicionSimple_TextosInformacion;
                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.DefinicionSimple_TextosInformacion = definir.DefinicionSimple;

                                        if (definir.DefinicionSimple)
                                        {
                                            SeleccionarOrdenarCantidades_Operador seleccionarOrdenar = new SeleccionarOrdenarCantidades_Operador();
                                            seleccionarOrdenar.Ventana = Ventana;
                                            seleccionarOrdenar.CalculoDiseñoSeleccionado = CalculoDiseñoSeleccionado;
                                            seleccionarOrdenar.listaTextos.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                            seleccionarOrdenar.listaTextos.Elementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(
                                                seleccionarOrdenar.listaTextos.Operandos).ToList();
                                            seleccionarOrdenar.opcionUnir.Visibility = Visibility.Visible;
                                            seleccionarOrdenar.opcionModoUnir.IsChecked = ElementoDiseñoOperacionSeleccionado.ModoUnir_SeleccionarOrdenar;
                                            seleccionarOrdenar.opcionSeleccionManual.Visibility = Visibility.Visible;
                                            seleccionarOrdenar.opcionModoSeleccionManual.IsChecked = ElementoDiseñoOperacionSeleccionado.ModoSeleccionManual_SeleccionarOrdenar;
                                            seleccionarOrdenar.listaTextos.DefinicionesListas = Calculo.TextosInformacion.ElementosTextosInformacion.Where(
                        i => i.GetType() == typeof(DiseñoListaCadenasTexto)).Select(i => (DiseñoListaCadenasTexto)i).ToList();


                                            List<AsociacionTextoInformacion_ElementoSalida> listaAsociaciones = new List<AsociacionTextoInformacion_ElementoSalida>();
                                            List<AsociacionTextoInformacion_Clasificador> listaAsociacionesClasificadores = new List<AsociacionTextoInformacion_Clasificador>();
                                            List<Clasificador> listaClasificadores = new List<Clasificador>();

                                            seleccionarOrdenar.listaTextos.CondicionesTextosInformacion = CopiarCondiciones(ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionOrdenamiento,
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_ElementosSalida,
                                                listaAsociaciones, 
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_Clasificadores,
                                                listaAsociacionesClasificadores,
                                                ElementoDiseñoOperacionSeleccionado.Clasificadores,
                                                listaClasificadores);
                                            seleccionarOrdenar.listaTextos.OperacionRelacionada = ElementoDiseñoOperacionSeleccionado;

                                            seleccionarOrdenar.listaTextos.ElementosSalida_Operacion = ElementoDiseñoOperacionSeleccionado.SalidasAgrupamiento_SeleccionOrdenamiento;
                                            seleccionarOrdenar.listaTextos.Clasificadores_Operacion = listaClasificadores;
                                            seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_ElementosSalida = listaAsociaciones;
                                            seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_Clasificadores = listaAsociacionesClasificadores;

                                        var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(ElementoDiseñoOperacionSeleccionado);

                                        if (definicionEncontrada != null &&
                                                definicionEncontrada.Any())
                                        {
                                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues;
                                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes;
                                                seleccionarOrdenar.MostrarOpcionesAsignacion = true;

                                                if (seleccionarOrdenar.opcionModoUnir.IsChecked == false)
                                                    seleccionarOrdenar.opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
                                        }

                                        bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                            if (definicionEstablecida)
                                            {
                                                ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionOrdenamiento = seleccionarOrdenar.listaTextos.CondicionesTextosInformacion;
                                                if (seleccionarOrdenar.opcionModoUnir.IsChecked == true)
                                                    ElementoDiseñoOperacionSeleccionado.ModoUnir_SeleccionarOrdenar = true;
                                                else
                                                    ElementoDiseñoOperacionSeleccionado.ModoUnir_SeleccionarOrdenar = false;
                                                if (seleccionarOrdenar.opcionModoSeleccionManual.IsChecked == true)
                                                    ElementoDiseñoOperacionSeleccionado.ModoSeleccionManual_SeleccionarOrdenar = true;
                                                else
                                                    ElementoDiseñoOperacionSeleccionado.ModoSeleccionManual_SeleccionarOrdenar = false;
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_ElementosSalida = seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_ElementosSalida;
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_Clasificadores = seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_Clasificadores;
                                                ElementoDiseñoOperacionSeleccionado.Clasificadores = seleccionarOrdenar.listaTextos.Clasificadores_Operacion;

                                                ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked;
                                            ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked;
                                        }
                                        }
                                        else
                                        {
                                            mostrarDefinicion = true;
                                        }
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo)
                                {
                                    SeleccionarDefinicion definir = new SeleccionarDefinicion();
                                    definir.DefinicionSimple = ElementoDiseñoOperacionSeleccionado.DefinicionSimple_CondicionesFlujo;
                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.DefinicionSimple_CondicionesFlujo = definir.DefinicionSimple;

                                        if (definir.DefinicionSimple)
                                        {
                                            SeleccionarOrdenarCondiciones seleccionarOrdenar = new SeleccionarOrdenarCondiciones();
                                            seleccionarOrdenar.opcionManual.Visibility = Visibility.Visible;
                                            seleccionarOrdenar.opcionModoManual.IsChecked = ElementoDiseñoOperacionSeleccionado.ModoManual_CondicionFlujo;

                                            List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones = new List<AsociacionCondicionFlujo_ElementoSalida>();
                                            List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones2 = new List<AsociacionCondicionFlujo_ElementoSalida>();

                                            seleccionarOrdenar.listaCondiciones.CondicionesFlujo = CopiarCondicionesFlujo(ElementoDiseñoOperacionSeleccionado.CondicionesFlujo_SeleccionOrdenamiento,
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida,
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2,
                                                listaAsociaciones, listaAsociaciones2);

                                            seleccionarOrdenar.listaCondiciones.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                            seleccionarOrdenar.listaCondiciones.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(
                                                seleccionarOrdenar.listaCondiciones.Operandos).ToList();
                                            seleccionarOrdenar.listaCondiciones.OperacionRelacionada = ElementoDiseñoOperacionSeleccionado;

                                            seleccionarOrdenar.listaCondiciones.ElementosSalida_Operacion = ElementoDiseñoOperacionSeleccionado.SalidasAgrupamiento_SeleccionCondicionesFlujo;
                                            seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida = listaAsociaciones;
                                            seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2 = listaAsociaciones2;

                                        var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(ElementoDiseñoOperacionSeleccionado);

                                        if (definicionEncontrada != null && definicionEncontrada.Any())
                                        {
                                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues;
                                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes;
                                            seleccionarOrdenar.opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;                                                
                                        }

                                        bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                            if (definicionEstablecida)
                                            {
                                                ElementoDiseñoOperacionSeleccionado.CondicionesFlujo_SeleccionOrdenamiento = seleccionarOrdenar.listaCondiciones.CondicionesFlujo;
                                                if (seleccionarOrdenar.opcionModoManual.IsChecked == true)
                                                    ElementoDiseñoOperacionSeleccionado.ModoManual_CondicionFlujo = true;
                                                else
                                                    ElementoDiseñoOperacionSeleccionado.ModoManual_CondicionFlujo = false;
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida;
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2 = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2;
                                                
                                            ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked;
                                            ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked;
                                        }
                                        }
                                        else
                                        {
                                            mostrarDefinicion = true;
                                        }
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                                {                                    
                                    SeleccionarEntradasCondiciones seleccionarOrdenar = new SeleccionarEntradasCondiciones();
                                    List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida> listaAsociaciones = new List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida>();
                                    seleccionarOrdenar.listaCondiciones.CondicionesEntradas = CopiarCondicionesTextosInformacion(ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionEntradas,
                                        ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida, listaAsociaciones);
                                    seleccionarOrdenar.ElementoAsociado = ElementoDiseñoOperacionSeleccionado;
                                    seleccionarOrdenar.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores.ToList();
                                    seleccionarOrdenar.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(
                                        seleccionarOrdenar.Operandos).ToList();
                                    seleccionarOrdenar.Operandos.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosPosteriores);
                                    seleccionarOrdenar.listaCondiciones.Entradas = Calculo.ObtenerCalculoEntradas().ListaEntradas.Where(i => i.Tipo == TipoEntrada.TextosInformacion).ToList();
                                    seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida = listaAsociaciones;
                                    seleccionarOrdenar.SeleccionManualEntradas = ElementoDiseñoOperacionSeleccionado.SeleccionManualEntradas;
                                    seleccionarOrdenar.SeleccionCondicionesEntradas = ElementoDiseñoOperacionSeleccionado.SeleccionCondicionesEntradas;
                                    seleccionarOrdenar.DefinicionesListas = Calculo.TextosInformacion.ElementosTextosInformacion.Where(
                        i => i.GetType() == typeof(DiseñoListaCadenasTexto)).Select(i => (DiseñoListaCadenasTexto)i).ToList();

                                    bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                    if (definicionEstablecida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionEntradas = seleccionarOrdenar.listaCondiciones.CondicionesEntradas;
                                        ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida;
                                        //ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2_Entradas = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida2;
                                        ElementoDiseñoOperacionSeleccionado.SeleccionManualEntradas = seleccionarOrdenar.SeleccionManualEntradas;
                                        ElementoDiseñoOperacionSeleccionado.SeleccionCondicionesEntradas = seleccionarOrdenar.SeleccionCondicionesEntradas;
                                    }                                        
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Espera)
                                {
                                    DefinirOperacion_Espera definir = new DefinirOperacion_Espera();
                                    definir.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                    definir.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(definir.Operandos).ToList();
                                    definir.OperandosSalidas = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores;
                                    definir.EntradasSalidas_Espera = ElementoDiseñoOperacionSeleccionado.EntradasSalidas_Espera.ToList();
                                    definir.TiempoEspera = ElementoDiseñoOperacionSeleccionado.TiempoEspera;
                                    definir.TipoTiempoEspera = ElementoDiseñoOperacionSeleccionado.TipoTiempoEspera;
                                    definir.CantidadEsperas_Fijas = ElementoDiseñoOperacionSeleccionado.CantidadEsperas_Fijas;
                                    definir.CantidadVerificaciones = ElementoDiseñoOperacionSeleccionado.CantidadVerificaciones;
                                    definir.DefinicionesListas = Calculo.TextosInformacion.ElementosTextosInformacion.Where(
                        i => i.GetType() == typeof(DiseñoListaCadenasTexto)).Select(i => (DiseñoListaCadenasTexto)i).ToList();

                                    if (ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_Espera != null)
                                        definir.CondicionesTextosInformacion_Espera = ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_Espera.ReplicarObjeto();
                                    
                                    if(ElementoDiseñoOperacionSeleccionado.CondicionesCantidades_Espera != null)
                                        definir.CondicionesCantidades_Espera = ElementoDiseñoOperacionSeleccionado.CondicionesCantidades_Espera.ReplicarObjeto();

                                    definir.VerificarCondiciones_Hasta = ElementoDiseñoOperacionSeleccionado.VerificarCondiciones_Hasta;
                                    definir.EjecutarImplicacionesAntes_Espera = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesAntes_Espera;
                                    definir.EjecutarImplicacionesDespues_Espera = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDespues_Espera;
                                    definir.EjecutarImplicacionesDurante_Espera = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDurante_Espera;

                                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoDiseñoSeleccionado &&
                                    i.OperacionRelacionada == ElementoDiseñoOperacionSeleccionado))
                                        definir.MostrarOpcionesImplicaciones = true;

                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.EntradasSalidas_Espera = definir.EntradasSalidas_Espera.ToList();
                                        ElementoDiseñoOperacionSeleccionado.TiempoEspera = definir.TiempoEspera;
                                        ElementoDiseñoOperacionSeleccionado.TipoTiempoEspera = definir.TipoTiempoEspera;
                                        ElementoDiseñoOperacionSeleccionado.CantidadEsperas_Fijas = definir.CantidadEsperas_Fijas;
                                        ElementoDiseñoOperacionSeleccionado.CantidadVerificaciones = definir.CantidadVerificaciones;
                                        ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_Espera = definir.CondicionesTextosInformacion_Espera;
                                        ElementoDiseñoOperacionSeleccionado.CondicionesCantidades_Espera = definir.CondicionesCantidades_Espera;
                                        ElementoDiseñoOperacionSeleccionado.VerificarCondiciones_Hasta = definir.VerificarCondiciones_Hasta;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesAntes_Espera = definir.EjecutarImplicacionesAntes_Espera;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDespues_Espera = definir.EjecutarImplicacionesDespues_Espera;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDurante_Espera = definir.EjecutarImplicacionesDurante_Espera;
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.LimpiarDatos)
                                {
                                    DefinirOperacion_LimpiarDatos definir = new DefinirOperacion_LimpiarDatos();
                                    definir.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                    definir.OperandosSalidas = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores;
                                    definir.config = ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.CopiarObjeto();
                                    definir.EntradasSalidas_LimpiarDatos = ElementoDiseñoOperacionSeleccionado.EntradasSalidas_LimpiarDatos.ToList();

                                    definir.EjecutarImplicacionesAntes_Limpieza = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesAntes_Limpieza;
                                    definir.EjecutarImplicacionesDurante_Limpieza = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDurante_Limpieza;
                                    definir.EjecutarImplicacionesDespues_Limpieza = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDespues_Limpieza;

                                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoDiseñoSeleccionado &&
                                    i.OperacionRelacionada == ElementoDiseñoOperacionSeleccionado))
                                        definir.MostrarOpcionesImplicaciones = true;

                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarDuplicados = definir.config.QuitarDuplicados;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCantidadesDuplicadas = definir.config.QuitarCantidadesDuplicadas;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.Conector1_Duplicados = definir.config.Conector1_Duplicados;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCantidadesTextosDuplicadas = definir.config.QuitarCantidadesTextosDuplicadas;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.Conector2_Duplicados = definir.config.Conector2_Duplicados;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCantidadesTextosDentroDuplicados = definir.config.QuitarCantidadesTextosDentroDuplicados;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCeros = definir.config.QuitarCeros;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCerosConTextos = definir.config.QuitarCerosConTextos;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.Conector1_Ceros = definir.config.Conector1_Ceros;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCerosSinTextos = definir.config.QuitarCerosSinTextos;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarCantidadesSinTextos = definir.config.QuitarCantidadesSinTextos;
                                        ElementoDiseñoOperacionSeleccionado.ConfigLimpiarDatos.QuitarNegativas = definir.config.QuitarNegativas;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesAntes_Limpieza = definir.EjecutarImplicacionesAntes_Limpieza;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDurante_Limpieza = definir.EjecutarImplicacionesDurante_Limpieza;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDespues_Limpieza = definir.EjecutarImplicacionesDespues_Limpieza;
                                        ElementoDiseñoOperacionSeleccionado.EntradasSalidas_LimpiarDatos = definir.EntradasSalidas_LimpiarDatos.ToList();
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.ArchivoExterno)
                                {
                                    DefinirOperacion_ArchivoExterno definir = new DefinirOperacion_ArchivoExterno();
                                    definir.Calculo = Calculo;
                                    definir.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                    definir.OperandosPosteriores = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida).ToList();
                                    definir.Config = ElementoDiseñoOperacionSeleccionado.ConfigArchivoExterno.CopiarObjeto();
                                    definir.EntradasOperandos_ArchivoExterno = ElementoDiseñoOperacionSeleccionado.EntradasOperandos_ArchivoExterno.ToList();
                                    definir.ResultadosOperandos_ArchivoExterno = ElementoDiseñoOperacionSeleccionado.ResultadosOperandos_ArchivoExterno.ToList();

                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.ConfigArchivoExterno = definir.Config.CopiarObjeto();
                                        ElementoDiseñoOperacionSeleccionado.EntradasOperandos_ArchivoExterno = definir.EntradasOperandos_ArchivoExterno.ToList();
                                        ElementoDiseñoOperacionSeleccionado.ResultadosOperandos_ArchivoExterno = definir.ResultadosOperandos_ArchivoExterno.ToList();
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SubCalculo)
                                {
                                    DefinirOperacion_SubCalculo definir = new DefinirOperacion_SubCalculo();
                                    definir.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                    definir.OperandosPosteriores = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores.Where(i => i.Tipo != TipoElementoOperacion.Salida).ToList();
                                    definir.Config = ElementoDiseñoOperacionSeleccionado.ConfigSubCalculo.CopiarObjeto();
                                    definir.EntradasOperandos_SubCalculo = CopiarAsociaciones_SubCalculo(ElementoDiseñoOperacionSeleccionado.EntradasOperandos_SubCalculo);
                                    definir.ResultadosOperandos_SubCalculo = CopiarAsociaciones_SubCalculo(ElementoDiseñoOperacionSeleccionado.ResultadosOperandos_SubCalculo);
                                    definir.NombreOperacion = ElementoDiseñoOperacionSeleccionado.Nombre;

                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.ConfigSubCalculo = definir.Config.CopiarObjeto();
                                        ElementoDiseñoOperacionSeleccionado.EntradasOperandos_SubCalculo = definir.EntradasOperandos_SubCalculo;
                                        ElementoDiseñoOperacionSeleccionado.ResultadosOperandos_SubCalculo = definir.ResultadosOperandos_SubCalculo;

                                        if (definir.AbrirDefinicionSubCalculo)
                                        {
                                            Ventana.AgregarTabArchivo(ElementoDiseñoOperacionSeleccionado.ConfigSubCalculo.SubCalculo_Operacion);
                                            Ventana.AgregarEjecucionToolTip(ElementoDiseñoOperacionSeleccionado.ConfigSubCalculo.SubCalculo_Operacion);
                                            Ventana.btnOperaciones_Click(this, null);                                            
                                        }
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.AgrupadorOperaciones)
                                {
                                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperaciones) && ((VistaDiseñoOperaciones)T.Content).ModoAgrupador && ((VistaDiseñoOperaciones)T.Content).Agrupador == ElementoDiseñoOperacionSeleccionado) select T).FirstOrDefault();
                                    if (tab != null)
                                    {
                                        Ventana.contenido.SelectedItem = tab;
                                    }
                                    else
                                    {
                                        Ventana.AgregarTabDiseñoOperacionesAgrupador(ref ElementoDiseñoOperacionSeleccionado, CalculoDiseñoSeleccionado, this);
                                    }
                                }
                                else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.RedondearCantidades)
                                {
                                    DefinirOperacion_RedondearCantidades definir = new DefinirOperacion_RedondearCantidades();
                                    definir.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;
                                    definir.OperandosSalidas = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores;
                                    definir.config = ElementoDiseñoOperacionSeleccionado.ConfigRedondeo.CopiarObjeto();
                                    definir.EntradasSalidas_RedondearCantidades = ElementoDiseñoOperacionSeleccionado.EntradasSalidas_RedondearCantidades.ToList();

                                    definir.EjecutarImplicacionesAntes_Redondeo = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesAntes_Redondeo;
                                    definir.EjecutarImplicacionesDurante_Redondeo = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDurante_Redondeo;
                                    definir.EjecutarImplicacionesDespues_Redondeo = ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDespues_Redondeo;

                                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoDiseñoSeleccionado &&
                                    i.OperacionRelacionada == ElementoDiseñoOperacionSeleccionado))
                                        definir.MostrarOpcionesImplicaciones = true;

                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.ConfigRedondeo.RedondearPar_Cercano = definir.config.RedondearPar_Cercano;
                                        ElementoDiseñoOperacionSeleccionado.ConfigRedondeo.RedondearNumero_LejanoDeCero = definir.config.RedondearNumero_LejanoDeCero;
                                        ElementoDiseñoOperacionSeleccionado.ConfigRedondeo.RedondearNumero_CercanoDeCero = definir.config.RedondearNumero_CercanoDeCero;
                                        ElementoDiseñoOperacionSeleccionado.ConfigRedondeo.RedondearNumero_Mayor = definir.config.RedondearNumero_Mayor;
                                        ElementoDiseñoOperacionSeleccionado.ConfigRedondeo.RedondearNumero_Menor = definir.config.RedondearNumero_Menor;

                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesAntes_Redondeo = definir.EjecutarImplicacionesAntes_Redondeo;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDurante_Redondeo = definir.EjecutarImplicacionesDurante_Redondeo;
                                        ElementoDiseñoOperacionSeleccionado.EjecutarImplicacionesDespues_Redondeo = definir.EjecutarImplicacionesDespues_Redondeo;
                                        ElementoDiseñoOperacionSeleccionado.EntradasSalidas_RedondearCantidades = definir.EntradasSalidas_RedondearCantidades.ToList();
                                    }
                                }
                            }

                            if (mostrarDefinicion)
                            {
                                var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperacion) && ((VistaDiseñoOperacion)T.Content).Operacion == ElementoDiseñoOperacionSeleccionado) select T).FirstOrDefault();
                                if (tab != null)
                                {
                                    Ventana.contenido.SelectedItem = tab;
                                }
                                else
                                {
                                    Ventana.AgregarTabDiseñoOperacionConjuntoNumeros(ref ElementoDiseñoOperacionSeleccionado, CalculoDiseñoSeleccionado);
                                }
                            }
                            //}
                            //else
                            //    MessageBox.Show("La operación no tiene conjuntos de números relacionados.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            MostrarInfo_Elemento(ElementoDiseñoOperacionSeleccionado);
                            MostrarAcumulacion();
                        }
                    }
                    else if(CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada != null &&
                    CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior != null) return;

                        ConfigEntrada configEntrada = new ConfigEntrada();
                        configEntrada.Ventana = Ventana;
                        configEntrada.Calculo = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada.CalculoDiseñoAsociado;
                        configEntrada.Entrada = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Entrada;
                        configEntrada.ShowDialog();
                        DibujarElementosOperaciones();
                    }
                    else
                        MessageBox.Show("La variable o vector no es válida.", "Lógica de variable o vector", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }            
            }
            else
            {
                foreach (var item in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
                {
                    if (item.Tipo == TipoElementoOperacion.Salida |
                        item.Tipo == TipoElementoOperacion.Nota |
                        item.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar) continue;

                    DiseñoOperacion ElementoDiseñoOperacionSeleccionado = null;
                    if (item.Tipo != TipoElementoOperacion.Ninguna)
                    {
                        if (item.Tipo != TipoElementoOperacion.Entrada &
                            item.Tipo != TipoElementoOperacion.Linea)
                        {
                            ElementoDiseñoOperacionSeleccionado = item;

                            if (ElementoDiseñoOperacionSeleccionado != null)
                            {
                                //if (VerificarSiTieneConjuntoNumeros(ElementoDiseñoOperacionSeleccionado))
                                //{
                                bool mostrarDefinicion = false;

                                if (ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.SeleccionarOrdenar &
                                    ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.CondicionesFlujo &
                                    ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.SeleccionarEntradas &
                                    ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.AgrupadorOperaciones)
                                {
                                    SeleccionarDefinicion definir = new SeleccionarDefinicion();
                                    definir.DefinicionSimple = ElementoDiseñoOperacionSeleccionado.DefinicionSimple_Operacion;
                                    bool opcionElegida = (bool)definir.ShowDialog();
                                    if (opcionElegida)
                                    {
                                        ElementoDiseñoOperacionSeleccionado.DefinicionSimple_Operacion = definir.DefinicionSimple;

                                        if (!definir.DefinicionSimple)
                                            mostrarDefinicion = true;
                                        else
                                        {
                                            if ((ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.Potencia ||
                                            (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Potencia &&
                                            ElementoDiseñoOperacionSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos)) &&
                                            (ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.Raiz ||
                                            (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Raiz &&
                                            ElementoDiseñoOperacionSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos)) &&
                                            (ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.Logaritmo ||
                                            (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Logaritmo &&
                                            ElementoDiseñoOperacionSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) &&
                                            ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.Inverso &&
                                            ElementoDiseñoOperacionSeleccionado.Tipo != TipoElementoOperacion.Factorial)
                                            {
                                                SeleccionarTipoOperacionEjecucion definirTipo = new SeleccionarTipoOperacionEjecucion();
                                                definirTipo.TipoOperacion = ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion; ;

                                                if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Potencia |
                                                ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Raiz |
                                                ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Porcentaje |
                                                ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.Logaritmo)
                                                {
                                                    definirTipo.MostrarOpcionPorSeparado = false;
                                                }

                                                if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.ContarCantidades)
                                                {
                                                    definirTipo.MostrarOpcionPorFilas = false;
                                                }

                                                opcionElegida = (bool)definirTipo.ShowDialog();

                                                if (opcionElegida)
                                                {
                                                    ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion = definirTipo.TipoOperacion;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                                    {
                                        SeleccionarDefinicion definir = new SeleccionarDefinicion();
                                        definir.DefinicionSimple = ElementoDiseñoOperacionSeleccionado.DefinicionSimple_TextosInformacion;
                                        bool opcionElegida = (bool)definir.ShowDialog();
                                        if (opcionElegida)
                                        {
                                            ElementoDiseñoOperacionSeleccionado.DefinicionSimple_TextosInformacion = definir.DefinicionSimple;

                                            if (definir.DefinicionSimple)
                                            {
                                                SeleccionarOrdenarCantidades_Operador seleccionarOrdenar = new SeleccionarOrdenarCantidades_Operador();
                                                seleccionarOrdenar.opcionUnir.Visibility = Visibility.Visible;
                                                seleccionarOrdenar.opcionModoUnir.IsChecked = ElementoDiseñoOperacionSeleccionado.ModoUnir_SeleccionarOrdenar;
                                                seleccionarOrdenar.opcionSeleccionManual.Visibility = Visibility.Visible;
                                                seleccionarOrdenar.opcionModoSeleccionManual.IsChecked = ElementoDiseñoOperacionSeleccionado.ModoSeleccionManual_SeleccionarOrdenar;

                                                List<AsociacionTextoInformacion_ElementoSalida> listaAsociaciones = new List<AsociacionTextoInformacion_ElementoSalida>();
                                                List<AsociacionTextoInformacion_Clasificador> listaAsociacionesClasificadores = new List<AsociacionTextoInformacion_Clasificador>();
                                                List<Clasificador> listaClasificadores = new List<Clasificador>();

                                                seleccionarOrdenar.listaTextos.CondicionesTextosInformacion = CopiarCondiciones(ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionOrdenamiento,
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_ElementosSalida,
                                                listaAsociaciones,
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_Clasificadores,
                                                listaAsociacionesClasificadores,
                                                ElementoDiseñoOperacionSeleccionado.Clasificadores,
                                                listaClasificadores);

                                                seleccionarOrdenar.listaTextos.ElementosSalida_Operacion = ElementoDiseñoOperacionSeleccionado.SalidasAgrupamiento_SeleccionOrdenamiento;
                                                seleccionarOrdenar.listaTextos.Clasificadores_Operacion = listaClasificadores;
                                                seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_ElementosSalida = listaAsociaciones;
                                                seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_Clasificadores = listaAsociacionesClasificadores;

                                                var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(ElementoDiseñoOperacionSeleccionado);

                                                if (definicionEncontrada != null && definicionEncontrada.Any())
                                                {
                                                    seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues;
                                                    seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes;

                                                    if (seleccionarOrdenar.opcionModoUnir.IsChecked == false)
                                                        seleccionarOrdenar.opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
                                                }

                                                bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                                if (definicionEstablecida)
                                                {
                                                    ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionOrdenamiento = seleccionarOrdenar.listaTextos.CondicionesTextosInformacion;
                                                    if (seleccionarOrdenar.opcionModoUnir.IsChecked == true)
                                                        ElementoDiseñoOperacionSeleccionado.ModoUnir_SeleccionarOrdenar = true;
                                                    else
                                                        ElementoDiseñoOperacionSeleccionado.ModoUnir_SeleccionarOrdenar = false;
                                                    if (seleccionarOrdenar.opcionModoSeleccionManual.IsChecked == true)
                                                        ElementoDiseñoOperacionSeleccionado.ModoSeleccionManual_SeleccionarOrdenar = true;
                                                    else
                                                        ElementoDiseñoOperacionSeleccionado.ModoSeleccionManual_SeleccionarOrdenar = false;
                                                    ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_ElementosSalida = seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_ElementosSalida;
                                                    ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_Clasificadores = seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_Clasificadores;

                                                    ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked;
                                                    ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked;
                                                }
                                            }
                                            else
                                            {
                                                mostrarDefinicion = true;
                                            }
                                        }
                                    }
                                    else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo)
                                    {
                                        SeleccionarDefinicion definir = new SeleccionarDefinicion();
                                        definir.DefinicionSimple = ElementoDiseñoOperacionSeleccionado.DefinicionSimple_CondicionesFlujo;
                                        bool opcionElegida = (bool)definir.ShowDialog();
                                        if (opcionElegida)
                                        {
                                            ElementoDiseñoOperacionSeleccionado.DefinicionSimple_CondicionesFlujo = definir.DefinicionSimple;

                                            if (definir.DefinicionSimple)
                                            {
                                                SeleccionarOrdenarCondiciones seleccionarOrdenar = new SeleccionarOrdenarCondiciones();
                                                seleccionarOrdenar.opcionManual.Visibility = Visibility.Visible;
                                                seleccionarOrdenar.opcionModoManual.IsChecked = ElementoDiseñoOperacionSeleccionado.ModoManual_CondicionFlujo;
                                                
                                                List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones = new List<AsociacionCondicionFlujo_ElementoSalida>();
                                                List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones2 = new List<AsociacionCondicionFlujo_ElementoSalida>();
                                                
                                                seleccionarOrdenar.listaCondiciones.CondicionesFlujo = CopiarCondicionesFlujo(ElementoDiseñoOperacionSeleccionado.CondicionesFlujo_SeleccionOrdenamiento,
                                                    ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida,
                                                ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2,
                                                listaAsociaciones, listaAsociaciones2);

                                                seleccionarOrdenar.listaCondiciones.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosAnteriores;

                                                seleccionarOrdenar.listaCondiciones.ElementosSalida_Operacion = ElementoDiseñoOperacionSeleccionado.SalidasAgrupamiento_SeleccionCondicionesFlujo;
                                                seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida = listaAsociaciones;
                                                seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2 = listaAsociaciones2;

                                                var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(ElementoDiseñoOperacionSeleccionado);

                                                if (definicionEncontrada != null && definicionEncontrada.Any())
                                                {
                                                    seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues;
                                                    seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked = ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes;
                                                    seleccionarOrdenar.opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
                                                }

                                                bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                                if (definicionEstablecida)
                                                {
                                                    ElementoDiseñoOperacionSeleccionado.CondicionesFlujo_SeleccionOrdenamiento = seleccionarOrdenar.listaCondiciones.CondicionesFlujo;
                                                    if (seleccionarOrdenar.opcionModoManual.IsChecked == true)
                                                        ElementoDiseñoOperacionSeleccionado.ModoManual_CondicionFlujo = true;
                                                    else
                                                        ElementoDiseñoOperacionSeleccionado.ModoManual_CondicionFlujo = false;
                                                    ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida;
                                                    ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2 = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2;

                                                    ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionDespues = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked;
                                                    ElementoDiseñoOperacionSeleccionado.IncluirAsignacionTextosInformacionAntes = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked;
                                                }
                                            }
                                            else
                                            {
                                                mostrarDefinicion = true;
                                            }
                                        }
                                    }
                                    else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                                    {                                        
                                        SeleccionarEntradasCondiciones seleccionarOrdenar = new SeleccionarEntradasCondiciones();
                                        seleccionarOrdenar.listaCondiciones.CondicionesEntradas = ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionEntradas.ToList();
                                        seleccionarOrdenar.Operandos = ElementoDiseñoOperacionSeleccionado.ElementosPosteriores;
                                        seleccionarOrdenar.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(
                                        seleccionarOrdenar.Operandos).ToList();
                                        //seleccionarOrdenar.OperacionRelacionada = ElementoDiseñoOperacionSeleccionado;
                                        seleccionarOrdenar.listaCondiciones.Entradas = Calculo.ObtenerCalculoEntradas().ListaEntradas.Where(i => i.Tipo == TipoEntrada.TextosInformacion).ToList();
                                        seleccionarOrdenar.SeleccionManualEntradas = ElementoDiseñoOperacionSeleccionado.SeleccionManualEntradas;
                                        seleccionarOrdenar.SeleccionCondicionesEntradas = ElementoDiseñoOperacionSeleccionado.SeleccionCondicionesEntradas;
                                        seleccionarOrdenar.DefinicionesListas = Calculo.TextosInformacion.ElementosTextosInformacion.Where(
                        i => i.GetType() == typeof(DiseñoListaCadenasTexto)).Select(i => (DiseñoListaCadenasTexto)i).ToList();

                                        //seleccionarOrdenar.listaCondiciones.ElementosSalida_Operacion = ElementoDiseñoOperacionSeleccionado.SalidasAgrupamiento_SeleccionEntradas;
                                        seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida = ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.ToList();
                                        //seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida2 = ElementoDiseñoOperacionSeleccionado.AsociacionesTextosInformacion_ElementosSalida;
                                                                                
                                        bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                                        if (definicionEstablecida)
                                        {
                                            ElementoDiseñoOperacionSeleccionado.CondicionesTextosInformacion_SeleccionEntradas = seleccionarOrdenar.listaCondiciones.CondicionesEntradas;
                                            ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida;
                                            //ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2 = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2;
                                            ElementoDiseñoOperacionSeleccionado.SeleccionManualEntradas = seleccionarOrdenar.SeleccionManualEntradas;
                                            ElementoDiseñoOperacionSeleccionado.SeleccionCondicionesEntradas = seleccionarOrdenar.SeleccionCondicionesEntradas;
                                        }                                            
                                    }
                                }

                                if (mostrarDefinicion)
                                {
                                    var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperacion) && ((VistaDiseñoOperacion)T.Content).Operacion == ElementoDiseñoOperacionSeleccionado) select T).FirstOrDefault();
                                    if (tab != null)
                                    {
                                        Ventana.contenido.SelectedItem = tab;
                                    }
                                    else
                                    {
                                        Ventana.AgregarTabDiseñoOperacionConjuntoNumeros(ref ElementoDiseñoOperacionSeleccionado, CalculoDiseñoSeleccionado);
                                    }
                                }

                                //var tab = (from TabItem T in Ventana.contenido.Items where (T.Content != null) && (T.Content.GetType() == typeof(VistaDiseñoOperacion) && ((VistaDiseñoOperacion)T.Content).Operacion == ElementoDiseñoOperacionSeleccionado) select T).FirstOrDefault();
                                //if (tab != null)
                                //{
                                //    Ventana.contenido.SelectedItem = tab;
                                //}
                                //else
                                //{
                                //    Ventana.AgregarTabDiseñoOperacionConjuntoNumeros(ref ElementoDiseñoOperacionSeleccionado);
                                //}
                                //}
                                //else
                                //    MessageBox.Show("La operación no tiene elementos de conjunto de números relacionados para diseñar una operación compleja.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                MostrarInfo_Elemento(ElementoDiseñoOperacionSeleccionado);
                                MostrarAcumulacion();
                            }
                        }
                        else if (item.Tipo == TipoElementoOperacion.Entrada)
                        {
                            ConfigEntrada configEntrada = new ConfigEntrada();
                            configEntrada.Ventana = Ventana;
                            configEntrada.Calculo = CalculoDiseñoSeleccionado;
                            configEntrada.Entrada = item.EntradaRelacionada;
                            configEntrada.ShowDialog();
                        }
                        //else
                        //    MessageBox.Show("El elemento no es una operación válida para diseñar una operación compleja.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }

            ListarOperandos();
        }
                
        public List<CondicionTextosInformacion> CopiarCondicionesTextosInformacion(List<CondicionTextosInformacion> listaCondiciones, 
            List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida> asociaciones,
            List<AsociacionCondicionTextosInformacion_Entradas_ElementoSalida> listaAsociaciones)
        {
            List<CondicionTextosInformacion> condicionesCopiadas = new List<CondicionTextosInformacion>();

            foreach (var itemCondicion in listaCondiciones)
            {
                var condicionCopiada = itemCondicion.ReplicarObjeto();

                foreach (var itemAsociacion in asociaciones)
                {
                    if(itemAsociacion.Condiciones == itemCondicion)
                    {
                        AsociacionCondicionTextosInformacion_Entradas_ElementoSalida asociacionCopiada = new AsociacionCondicionTextosInformacion_Entradas_ElementoSalida();
                        asociacionCopiada.Condiciones = condicionCopiada;
                        asociacionCopiada.ModoManual = itemAsociacion.ModoManual;
                        asociacionCopiada.ElementoSalida_Operacion = itemAsociacion.ElementoSalida_Operacion;

                        listaAsociaciones.Add(asociacionCopiada);
                    }
                }

                condicionesCopiadas.Add(condicionCopiada);
            }

            return condicionesCopiadas;
        }

        public List<CondicionFlujo> CopiarCondicionesFlujo(List<CondicionFlujo> listaCondiciones,
            List<AsociacionCondicionFlujo_ElementoSalida> asociaciones,
            List<AsociacionCondicionFlujo_ElementoSalida> asociaciones2,
            List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones,
            List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones2)
        {
            List<CondicionFlujo> condicionesCopiadas = new List<CondicionFlujo>();

            foreach (var itemCondicion in listaCondiciones)
            {
                var condicionCopiada = itemCondicion.ReplicarObjeto();

                foreach (var itemAsociacion in asociaciones)
                {
                    if (itemAsociacion.Condiciones == itemCondicion)
                    {
                        var asociacionCopiada = itemAsociacion.ReplicarObjeto();
                        asociacionCopiada.Condiciones = condicionCopiada;

                        listaAsociaciones.Add(asociacionCopiada);
                    }
                }

                foreach (var itemAsociacion in asociaciones2)
                {
                    if (itemAsociacion.Condiciones == itemCondicion)
                    {
                        var asociacionCopiada = itemAsociacion.ReplicarObjeto();
                        asociacionCopiada.Condiciones = condicionCopiada;

                        listaAsociaciones.Add(asociacionCopiada);
                    }
                }

                condicionesCopiadas.Add(condicionCopiada);
            }

            return condicionesCopiadas;
        }        
        public List<CondicionesAsignacionSalidas_TextosInformacion> CopiarCondiciones(List<CondicionesAsignacionSalidas_TextosInformacion> listaCondiciones,
            List<AsociacionTextoInformacion_ElementoSalida> asociaciones,
            List<AsociacionTextoInformacion_ElementoSalida> listaAsociaciones,
            List<AsociacionTextoInformacion_Clasificador> asociacionesClasificadores,
            List<AsociacionTextoInformacion_Clasificador> listaAsociacionesClasificadores,
            List<Clasificador> clasificadores,
            List<Clasificador> listaClasificadores)
        {
            List<CondicionesAsignacionSalidas_TextosInformacion> condicionesCopiadas = new List<CondicionesAsignacionSalidas_TextosInformacion>();

            foreach (var itemCondicion in listaCondiciones)
            {
                var condicionCopiada = itemCondicion.ReplicarObjeto();

                foreach (var itemAsociacion in asociaciones)
                {
                    if (itemAsociacion.CondicionesAsociadas == itemCondicion.Condiciones)
                    {
                        var asociacionCopiada = itemAsociacion.ReplicarObjeto();
                        asociacionCopiada.CondicionesAsociadas = condicionCopiada.Condiciones;

                        listaAsociaciones.Add(asociacionCopiada);
                    }
                }

                foreach (var itemAsociacion in asociacionesClasificadores)
                {
                    if (itemAsociacion.CondicionesAsociadas == itemCondicion.Condiciones)
                    {
                        var asociacionCopiada = itemAsociacion.ReplicarObjeto();
                        asociacionCopiada.CondicionesAsociadas = condicionCopiada.Condiciones;

                        foreach (var itemClasificador in clasificadores)
                        {
                            if (itemAsociacion.ElementoClasificador == itemClasificador)
                            {
                                if (!listaClasificadores.Any(i => i.ID == itemClasificador.ID))
                                {
                                    var clasificadorCopiado = itemClasificador.CopiarObjeto();
                                    asociacionCopiada.ElementoClasificador = clasificadorCopiado;
                                    listaClasificadores.Add(clasificadorCopiado);
                                }
                                else
                                {
                                    var clasificadorAsociado = listaClasificadores.FirstOrDefault(i => i.ID == itemClasificador.ID);
                                    asociacionCopiada.ElementoClasificador = clasificadorAsociado;
                                }
                            }
                            else
                            {
                                if (!listaClasificadores.Any(i => i.ID == itemClasificador.ID))
                                {
                                    var clasificadorCopiado = itemClasificador.CopiarObjeto();
                                    listaClasificadores.Add(clasificadorCopiado);
                                }
                            }
                        }

                        listaAsociacionesClasificadores.Add(asociacionCopiada);
                    }
                }

                condicionesCopiadas.Add(condicionCopiada);
            }

            return condicionesCopiadas;
        }

        public List<AsociacionEntradaOperando_ArchivoExterno> CopiarAsociaciones_SubCalculo(List<AsociacionEntradaOperando_ArchivoExterno> lista)
        {
            List<AsociacionEntradaOperando_ArchivoExterno> listaCopiada = new List<AsociacionEntradaOperando_ArchivoExterno>();
            foreach (var item in lista)
                listaCopiada.Add(item.ReplicarObjeto());

            return listaCopiada;
        }
        public List<AsociacionResultadoOperando_ArchivoExterno> CopiarAsociaciones_SubCalculo(List<AsociacionResultadoOperando_ArchivoExterno> lista)
        {
            List<AsociacionResultadoOperando_ArchivoExterno> listaCopiada = new List<AsociacionResultadoOperando_ArchivoExterno>();
            foreach (var item in lista)
                listaCopiada.Add(item.ReplicarObjeto());

            return listaCopiada;
        }
        public void DestacarElementoSeleccionado()
        {
            CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Clear();
            CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado = null;

            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(OperacionEspecifica))
                {
                    ((OperacionEspecifica)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((OperacionEspecifica)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(NotaDiagrama))
                {
                    ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                }
                else if (item.GetType() == typeof(ArrowLine))
                    ((ArrowLine)item).Stroke = Brushes.Black;
            }

            LimpiarInfoElemento();

            if (CalculoDiseñoSeleccionado == null) return;

            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Entrada)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.EnDiagrama)
                {                    
                    CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.Background = SystemColors.HighlightBrush;
                    CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
                }

                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada.DiseñoOperacion;
                CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionado = CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada;
            }
            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota)
            {                
                //if (CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.EnDiagrama)
                //{
                    CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.fondo.BorderThickness = new Thickness(1);
                //}

                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado = CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada.DiseñoOperacion;
                CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionado = CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada;

            }
            else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna &
                CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.EnDiagrama)
                {
                    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.Background = SystemColors.HighlightBrush;
                    CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
                }
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;
                CalculoDiseñoSeleccionado.Seleccion.ElementoDiagramaSeleccionado = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada;
            }
            else if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Linea)
                CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada.Stroke = SystemColors.HighlightBrush;

            
        }

        private void LimpiarInfoElemento()
        {
            infoElemento.Text = string.Empty;
            opcionPorcentajeRelativo.IsChecked = false;
            valorOpcionElementoFijo.Text = string.Empty;
            opcionesElementosFijosPotencia.SelectedItem = null;
            opcionesElementosFijosRaiz.SelectedItem = null;
            opcionesElementosFijosLogaritmo.SelectedItem = null;
            opcionesElementosFijosInverso.SelectedItem = null;
            condicionesTextosInformacionOperandosResultados.Operandos = null;
            opcionTextosInformacionOperandosResultados.IsChecked = false;
            opcionTextosInformacionCondicionesOperandosResultados.IsChecked = false;
            condicionesTextosInformacionOperandosResultados.Condiciones = null;
            condicionesTextosInformacionOperandosResultados.ListarCondiciones();
            opcionTodosOperandosTextosInformacionOperandosResultados.IsChecked = false;
            opcionNingunOperandoTextosInformacionOperandosResultados.IsChecked = false;
            opcionAlgunosOperandosTextosInformacionOperandosResultados.IsChecked = false;
            listaOperandosTextosInformacionOperandosResultados.Children.Clear();
            opcionOperarFilasRestantes_ConCeros.IsChecked = false;
            opcionOperarFilasRestantes_ConUltimoNumero.IsChecked = false;
            opcionSeleccionarArchivoEntradaEjecucion.IsChecked = false;
            opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.IsChecked = false;
            opcionUtilizarDefinicionAsignacionTextosInformacion.IsChecked = false;
            opcionAgregarCantidadNumerosCantidad.IsChecked = false;
            opcionIncluirCantidadNumero.IsChecked = false;
            opcionAgregarCantidadNumerosTextoInformacion.IsChecked = false;
            opcionAgregarNumerosTextoInformacion.IsChecked = false;
            opcionAntesEjecucion.IsChecked = false;
            opcionAntesEjecucion_Clasificadores.IsChecked = false;
            opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked = false;
            opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.IsChecked = false;
            opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked = false;
            opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.IsChecked = false;
            ordenarAntesEjecucion_PorNumero.IsChecked = false;
            ordenarAntesEjecucion_PorNombre.IsChecked = false;
            opcionTipoOrdenamientoAntesEjecucion.SelectedItem = null;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = false;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked = false;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked = false;
            opcionDespuesEjecucion.IsChecked = true;
            opcionDespuesEjecucion_Clasificadores.IsChecked = true;
            opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked = false;
            opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.IsChecked = false;
            opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked = false;
            ordenarDespuesEjecucion_PorNumero.IsChecked = false;
            ordenarDespuesEjecucion_PorNombre.IsChecked = false;
            opcionTipoOrdenamientoDespuesEjecucion.SelectedItem = null;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked = false;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked = false;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked = false;
            opcionLimpiarDatos.IsChecked = false;
            opcionRedondearCantidades.IsChecked = false;
            botonOpcionLimpiarDatos.Visibility = Visibility.Collapsed;
            botonOpcionRedondearCantidades.Visibility = Visibility.Collapsed;
            MostrarInfo_Elemento(null);
        }

        public void diagrama_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OcultarToolTips(null);

            if (lineaSeleccionada)
            {
                lineaSeleccionada = false;
                return;
            }
            //if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Linea &&
            //CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada != null) return;
            //if (!ClicNota)
            //{
            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Ninguna;
            CalculoDiseñoSeleccionado.Seleccion.EntradaSeleccionada = null;
            CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada = null;
            CalculoDiseñoSeleccionado.Seleccion.NotaSeleccionada = null;
            CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = null;
            CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado = null;
            CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento = null;
            CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Clear();
            CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Clear();

            ElementoSeleccionado = false;
            ClicDiagrama = true;

            //contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            MostrarOrdenOperando_Elemento(null);
            MostrarInfo_Elemento(null);
            MostrarAcumulacion();
            MostrarConfiguracionSeparadores_Elemento(null);
            DestacarElementoSeleccionado();
            //if(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            //    MostrarOrdenOperando_Elemento(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado);
            DibujarTodasLineasElementos();
            //diagrama.UpdateLayout();
            if(e != null)
                ubicacionInicialAreaSeleccionada = e.GetPosition(diagrama);

                //DragDrop.DoDragDrop(this, ubicacionInicialAreaSeleccionada, DragDropEffects.Move);
            //}
            //else
            //    ClicNota = false;
        }

        private void SeleccionarLinea(object sender, RoutedEventArgs e)
        {
            CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Linea;
            CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada = (ArrowLine)sender;
            ElementoSeleccionado = true;

            ElementoAnteriorLineaSeleccionada = null;
            ElementoPosteriorLineaSeleccionada = null;

            EncontrarElementosLinea(CalculoDiseñoSeleccionado.Seleccion.lineaSeleccionada, ref ElementoAnteriorLineaSeleccionada, ref ElementoPosteriorLineaSeleccionada);

            MostrarOrdenOperando_Elemento(null);
            contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            DestacarElementoSeleccionado();

            lineaSeleccionada = true;
        }

        private void EncontrarElementosLinea(ArrowLine lineaSeleccionada, ref DiseñoOperacion ElementoAnteriorLineaSeleccionada,
            ref DiseñoOperacion ElementoPosteriorLineaSeleccionada)
        {
            foreach (var itemControl in (from UIElement C in diagrama.Children where C.GetType() != typeof(ArrowLine) select C).ToList())
            {
                var lineasOrigen = BuscarLineasUnElemento(new Point(Canvas.GetLeft(itemControl), Canvas.GetTop(itemControl)),
                true, itemControl);

                var lineaOrigenEncontrada = (from ArrowLine L in lineasOrigen where L == lineaSeleccionada select L).FirstOrDefault();
                
                if (lineaOrigenEncontrada != null)
                {
                    if (itemControl.GetType() == typeof(EntradaDiseñoOperaciones))
                        ElementoAnteriorLineaSeleccionada = ((EntradaDiseñoOperaciones)itemControl).DiseñoOperacion;
                    if (itemControl.GetType() == typeof(OperacionEspecifica))
                        ElementoAnteriorLineaSeleccionada = ((OperacionEspecifica)itemControl).DiseñoOperacion;

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
                    if (itemControl.GetType() == typeof(EntradaDiseñoOperaciones))
                        ElementoPosteriorLineaSeleccionada = ((EntradaDiseñoOperaciones)itemControl).DiseñoOperacion;
                    if (itemControl.GetType() == typeof(OperacionEspecifica))
                        ElementoPosteriorLineaSeleccionada = ((OperacionEspecifica)itemControl).DiseñoOperacion;

                    break;
                }
            }
        }

        private void QuitarActualizarResultados_ElementosConectados(DiseñoOperacion item)
        {
            if (Calculo.SubCalculoSeleccionado_Operaciones.ElementosPosteriores.Count == 0)
            {
                Resultado resultadoRelacionado = (from Resultado R in Calculo.ListaResultados where R.SalidaRelacionada == item select R).FirstOrDefault();
                if (resultadoRelacionado != null)
                {
                    Calculo.ListaResultados.Remove(resultadoRelacionado);
                    foreach (var itemVentana in Ventana.contenido.Items)
                    {
                        if (((TabItem)itemVentana).Content != null && ((TabItem)itemVentana).Content.GetType() == typeof(VistaResultados))
                        {
                            ((VistaResultados)((TabItem)itemVentana).Content).ListarResultados();
                            break;
                        }
                    }
                }
            }
        }

        //private bool VerificarSiTieneConjuntoNumeros(DiseñoOperacion elemento)
        //{
        //    foreach (var itemAnterior in elemento.ElementosAnteriores)
        //    {
        //        if (itemAnterior.Tipo == TipoElementoOperacion.Entrada)
        //        {
        //            if (itemAnterior.EntradaRelacionada.Tipo == TipoEntrada.ConjuntoNumeros)
        //            {
        //                return true;
        //            }
        //            else if (itemAnterior.EntradaRelacionada.Tipo == TipoEntrada.Calculo &&
        //                itemAnterior.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
        //                Calculo.VerificarSiElementoEs_EntradaGeneral(itemAnterior.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada) &&
        //                itemAnterior.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.Tipo == TipoEntrada.ConjuntoNumeros)
        //            {
        //                return true;
        //            }
        //            else if (itemAnterior.EntradaRelacionada.Tipo == TipoEntrada.Calculo &
        //                itemAnterior.EntradaRelacionada.ElementoSalidaCalculoAnterior != null)
        //            {
        //                if (VerificarSiTieneConjuntoNumeros(itemAnterior.EntradaRelacionada.ElementoSalidaCalculoAnterior))
        //                    return true;
        //            }
        //        }
        //        else
        //        {
        //            if (VerificarSiTieneConjuntoNumeros(itemAnterior))
        //                return true;
        //        }
        //    }

        //    return false;
        //}

        public void MarcarCalculoSeleccionado()
        {
            foreach (var itemCalculo in calculos.Children)
            {
                ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            }

            if (Calculo.SubCalculoSeleccionado_Operaciones != null)
            {
                foreach (var itemCalculo in calculos.Children)
                {
                    if (((CalculoEspecifico)itemCalculo).CalculoDiseño == Calculo.SubCalculoSeleccionado_Operaciones)
                    {
                        ((CalculoEspecifico)itemCalculo).botonFondo.Background = SystemColors.InactiveBorderBrush;
                    }
                }
            }
        }

        public void MostrarOrdenOperando_Elemento(DiseñoOperacion elemento)
        {
            contenedorOrdenOperandos.Content = null;

            if (elemento != null && elemento.ElementosAnteriores.Any())
            {

                if (CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos)
                {
                    ListarOperandos_Vacio();
                    //contenedorOrdenOperandos.Visibility = Visibility.Visible;
                    ListarOperandos();
                }

                DibujarTodasLineasElementos();
            }
            //else
            //{
            //    CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos = false;
            //    contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            //}

            //
            //diagrama.UpdateLayout();
        }

        private void btnDefinirOrdenOperandos_Click(object sender, RoutedEventArgs e)
        {
            CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos = !CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos;

            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos)
            {
                if (!(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null && CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores.Count > 0))
                {
                    CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos = false;
                }
            }

            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoOrdenOperandos)
            {
                contenedorOrdenOperandos.Visibility = Visibility.Visible;
                ListarOperandos();
            }
            else
            {
                contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            }
        }

        private void ListarOperandos_Vacio()
        {
            Grid ordenOperandos = new Grid();

            for (int i = 1; i <= 4; i++)
            {
                ColumnDefinition columna = new ColumnDefinition();
                if (i < 3)
                {
                    columna.Width = GridLength.Auto;

                    if (i == 2 && CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                        ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje |
                        (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)))
                    {
                        ColumnDefinition columnaBaseExponente = new ColumnDefinition();
                        columnaBaseExponente.Width = GridLength.Auto;
                        ordenOperandos.ColumnDefinitions.Add(columnaBaseExponente);
                    }
                }
                ordenOperandos.ColumnDefinitions.Add(columna);
            }

            RowDefinition filaTitulo = new RowDefinition();
            filaTitulo.Height = GridLength.Auto;
            ordenOperandos.RowDefinitions.Add(filaTitulo);

            TextBlock tituloOperandos = new TextBlock();
            tituloOperandos.Margin = new Thickness(10);
            tituloOperandos.Text = "Orden de las variables o vectores";

            ordenOperandos.Children.Add(tituloOperandos);

            Grid.SetRow(tituloOperandos, 0);
            Grid.SetColumn(tituloOperandos, 0);
            Grid.SetColumnSpan(tituloOperandos, CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null && 
                ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizFijaRadicalOperando) |
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje |
                (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) ? 4:3);

            contenedorOrdenOperandos.Content = ordenOperandos;
        }

        private void ListarOperandos()
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado == null) return;

            List<DiseñoOperacion> operandos = (from E in CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores
                                               select E).OrderBy((j) => j.OrdenOperandos.Where((a) => a.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado).FirstOrDefault().Orden).ToList();

            //ordenOperandos.RowDefinitions.Clear();
            //ordenOperandos.Children.Clear();

            Grid ordenOperandos = new Grid();
            
            for (int i = 1; i <= 5; i++)
            {
                ColumnDefinition columna = new ColumnDefinition();
                if (i < 3)
                {
                    columna.Width = GridLength.Auto;

                    if (i == 2 & 
                        ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje |
                (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)))
                    {
                        ColumnDefinition columnaBaseExponente = new ColumnDefinition();
                        columnaBaseExponente.Width = GridLength.Auto;                        
                        ordenOperandos.ColumnDefinitions.Add(columnaBaseExponente);
                    }
                }

                if (i == 5)
                    columna.Width = GridLength.Auto;

                ordenOperandos.ColumnDefinitions.Add(columna);
            }

            ColumnDefinition columnaNoConsiderarEjecucion = new ColumnDefinition();
            columnaNoConsiderarEjecucion.Width = GridLength.Auto;
            ordenOperandos.ColumnDefinitions.Add(columnaNoConsiderarEjecucion);

            ColumnDefinition columnaOrdenarNumeros = new ColumnDefinition();
            columnaOrdenarNumeros.Width = GridLength.Auto;
            ordenOperandos.ColumnDefinitions.Add(columnaOrdenarNumeros);

            RowDefinition filaTitulo = new RowDefinition();
            filaTitulo.Height = GridLength.Auto;
            ordenOperandos.RowDefinitions.Add(filaTitulo);

            TextBlock tituloOperandos = new TextBlock();
            tituloOperandos.Margin = new Thickness(10);
            tituloOperandos.Text = "Orden de las variables o vectores";

            ordenOperandos.Children.Add(tituloOperandos);

            Grid.SetRow(tituloOperandos, 0);
            Grid.SetColumn(tituloOperandos, 0);
            Grid.SetColumnSpan(tituloOperandos, ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje |
                (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) ? 4 : 3);

            int indiceFila = 1;
            int indiceColumna = 0;
            string strBasePotencia = string.Empty;
            bool basePotencia_raizRadical = true;

            foreach (var item in operandos)
            {
                RowDefinition fila = new RowDefinition();
                fila.Height = GridLength.Auto;
                ordenOperandos.RowDefinitions.Add(fila);

                TextBlock numeroOperando = new TextBlock();
                numeroOperando.Margin = new Thickness(10);
                numeroOperando.Text = (indiceFila).ToString();

                ordenOperandos.Children.Add(numeroOperando);

                Grid.SetRow(numeroOperando, indiceFila);
                Grid.SetColumn(numeroOperando, indiceColumna);
                indiceColumna++;

                if ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje |
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos))
                {
                    if (basePotencia_raizRadical)
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia)
                            strBasePotencia = "Base";
                        else if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz)
                            strBasePotencia = "Raíz";
                        else if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje)
                            strBasePotencia = "Porcentaje a extraer";
                        else if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo)
                            strBasePotencia = "Base";
                        basePotencia_raizRadical = false;
                    }
                    else
                    {
                        if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Potencia)
                            strBasePotencia = "Exponente";
                        else if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Raiz)
                            strBasePotencia = "Radical";
                        else if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Porcentaje)
                            strBasePotencia = "Cantidad de donde se extraerá";
                        else if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Logaritmo)
                            strBasePotencia = "Argumento";
                        basePotencia_raizRadical = true;
                    }

                    TextBlock baseExponente = new TextBlock();
                    baseExponente.Margin = new Thickness(10);
                    baseExponente.Text = strBasePotencia;

                    ordenOperandos.Children.Add(baseExponente);

                    Grid.SetRow(baseExponente, indiceFila);
                    Grid.SetColumn(baseExponente, indiceColumna);
                    indiceColumna++;
                }

                TextBlock nombreOperando = new TextBlock();
                nombreOperando.Margin = new Thickness(10);
                if (item.Tipo == TipoElementoOperacion.Entrada)
                    nombreOperando.Text = item.EntradaRelacionada.Nombre;
                else
                    nombreOperando.Text = item.Nombre;

                ordenOperandos.Children.Add(nombreOperando);

                Grid.SetRow(nombreOperando, indiceFila);
                Grid.SetColumn(nombreOperando, indiceColumna);
                indiceColumna++;

                Image ImagenBotonSubir = new Image();
                ImagenBotonSubir.Source = new BitmapImage(new Uri("\\Imagenes\\35.png", UriKind.Relative));
                ImagenBotonSubir.Width = 24;
                ImagenBotonSubir.Height = 24;

                Button botonSubir = new Button();
                botonSubir.Tag = item;
                botonSubir.Margin = new Thickness(10);
                botonSubir.Content = ImagenBotonSubir;
                botonSubir.Click += ClickBotonSubir;

                ordenOperandos.Children.Add(botonSubir);

                Grid.SetRow(botonSubir, indiceFila);
                Grid.SetColumn(botonSubir, indiceColumna);
                indiceColumna++;

                Image ImagenBotonBajar = new Image();
                ImagenBotonBajar.Source = new BitmapImage(new Uri("\\Imagenes\\34.png", UriKind.Relative));
                ImagenBotonBajar.Width = 24;
                ImagenBotonBajar.Height = 24;

                Button botonBajar = new Button();
                botonBajar.Tag = item;
                botonBajar.Margin = new Thickness(10);
                botonBajar.Content = ImagenBotonBajar;
                botonBajar.Click += ClickBotonBajar;

                ordenOperandos.Children.Add(botonBajar);

                Grid.SetRow(botonBajar, indiceFila);
                Grid.SetColumn(botonBajar, indiceColumna);

                indiceColumna++;

                //Image ImagenBotonOrdenar = new Image();
                ////ImagenBotonOrdenar.Source = new BitmapImage(new Uri("\\Imagenes\\34.png", UriKind.Relative));
                //ImagenBotonOrdenar.Width = 24;
                //ImagenBotonOrdenar.Height = 24;

                Button botonOrdenar = new Button();
                botonOrdenar.Tag = new object[] { item, CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado };
                botonOrdenar.Margin = new Thickness(10);
                botonOrdenar.Content = "Ordenar números"; //ImagenBotonOrdenar;
                botonOrdenar.Click += ClickBotonOrdenar;

                ordenOperandos.Children.Add(botonOrdenar);

                Grid.SetRow(botonOrdenar, indiceFila);
                Grid.SetColumn(botonOrdenar, indiceColumna);
                indiceColumna++;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                {
                    Grid botonAgruparGrilla = new Grid();

                    botonAgruparGrilla.ColumnDefinitions.Add(new ColumnDefinition());
                    botonAgruparGrilla.ColumnDefinitions.Last().Width = GridLength.Auto;
                    botonAgruparGrilla.ColumnDefinitions.Add(new ColumnDefinition());
                    botonAgruparGrilla.ColumnDefinitions.Last().Width = GridLength.Auto;

                    TextBlock etiquetaBotonAgrupar = new TextBlock();
                    etiquetaBotonAgrupar.Text = "Agrupar";

                    Image ImagenBotonAgrupar = new Image();
                    ImagenBotonAgrupar.Source = new BitmapImage(new Uri("\\Imagenes\\Iconos6\\icono_06.png", UriKind.Relative));
                    ImagenBotonAgrupar.Width = 24;
                    ImagenBotonAgrupar.Height = 24;

                    botonAgruparGrilla.Children.Add(ImagenBotonAgrupar);
                    Grid.SetColumn(ImagenBotonAgrupar, 0);

                    botonAgruparGrilla.Children.Add(etiquetaBotonAgrupar);
                    Grid.SetColumn(etiquetaBotonAgrupar, 1);

                    Button botonAgrupar = new Button();
                    botonAgrupar.Tag = item;
                    botonAgrupar.Margin = new Thickness(10);
                    botonAgrupar.Content = botonAgruparGrilla;
                    botonAgrupar.Click += BotonAgrupar_Click;

                    ordenOperandos.Children.Add(botonAgrupar);

                    Grid.SetRow(botonAgrupar, indiceFila);
                    Grid.SetColumn(botonAgrupar, indiceColumna);

                    indiceColumna++;
                }

                CheckBox noConsiderarEjecucion = new CheckBox();
                noConsiderarEjecucion.Margin = new Thickness(10);
                noConsiderarEjecucion.Tag = item;
                noConsiderarEjecucion.Content = "No considerar en la ejecución de la operación\n(sólo en condiciones y otros elementos de ajuste)";

                noConsiderarEjecucion.Checked -= NoConsiderarEjecucion_Checked;
                noConsiderarEjecucion.Unchecked -= NoConsiderarEjecucion_Unchecked;

                noConsiderarEjecucion.IsChecked = item.NoConsiderarEjecucion;

                noConsiderarEjecucion.Checked += NoConsiderarEjecucion_Checked;
                noConsiderarEjecucion.Unchecked += NoConsiderarEjecucion_Unchecked;

                ordenOperandos.Children.Add(noConsiderarEjecucion);

                Grid.SetRow(noConsiderarEjecucion, indiceFila);
                Grid.SetColumn(noConsiderarEjecucion, indiceColumna);
                indiceColumna++;

                indiceColumna = 0;

                indiceFila++;
            }

            contenedorOrdenOperandos.Content = ordenOperandos;
        }

        private void NoConsiderarEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            DiseñoOperacion elemento = (DiseñoOperacion)((CheckBox)sender).Tag;

            if (elemento != null)
            {
                elemento.NoConsiderarEjecucion = (bool)((CheckBox)sender).IsChecked;
            }
        }

        private void NoConsiderarEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            DiseñoOperacion elemento = (DiseñoOperacion)((CheckBox)sender).Tag;

            if(elemento != null)
            {
                elemento.NoConsiderarEjecucion = (bool)((CheckBox)sender).IsChecked;
            }
        }

        private void BotonAgrupar_Click(object sender, RoutedEventArgs e)
        {
            EstablecerAgrupacionesOperandos_PorSeparado agrupaciones = new EstablecerAgrupacionesOperandos_PorSeparado();
            agrupaciones.OperacionRelacionada = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado;
            agrupaciones.Operando = (DiseñoOperacion)((Button)sender).Tag;

            bool agrupar = (bool)agrupaciones.ShowDialog();

            if(agrupar)
            {
                var agrupacionRelacionada = agrupaciones.Operando.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionRelacionada == agrupaciones.OperacionRelacionada).FirstOrDefault();

                if (!string.IsNullOrEmpty(agrupaciones.NombreAgrupacion))
                {                    
                    if (agrupacionRelacionada == null)
                    {
                        agrupaciones.Operando.AgrupacionesOperandos_PorSeparado.Add(new AgrupacionOperando_PorSeparado()
                        {
                            NombreAgrupacion = agrupaciones.NombreAgrupacion,
                            OperacionRelacionada = agrupaciones.OperacionRelacionada
                        });
                    }
                    else
                    {
                        agrupacionRelacionada.NombreAgrupacion = agrupaciones.NombreAgrupacion;
                    }
                }
                else
                {                    
                    if (agrupacionRelacionada != null)
                    {
                        agrupaciones.Operando.AgrupacionesOperandos_PorSeparado.Remove(agrupacionRelacionada);

                        foreach (var itemOperando in agrupaciones.Operando.ElementosPosteriores)
                        {
                            while (itemOperando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == agrupacionRelacionada))
                                itemOperando.AgrupacionesAsignadasOperandos_PorSeparado.Remove(
                                    itemOperando.AgrupacionesAsignadasOperandos_PorSeparado.FirstOrDefault(i => i.Agrupacion == agrupacionRelacionada));
                        }
                    }
                }

                MostrarInfo_Elemento(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado);
            }
        }

        private void ClickBotonSubir(object sender, RoutedEventArgs e)
        {
            List<DiseñoOperacion> operandos = (from E in CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores
                                               select E).OrderBy((j) => j.OrdenOperandos.Where((a) => a.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado).FirstOrDefault().Orden).ToList();

            DiseñoOperacion operandoAnterior = null;
            DiseñoOperacion operando = null;
            foreach (var item in operandos)
            {
                if (item == ((Button)sender).Tag)
                {
                    operando = item;
                    break;
                }
                operandoAnterior = item;
            }

            if(operandoAnterior != null)
            {
                OrdenOperando ordenOperando = (from O in operando.OrdenOperandos where O.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden -= 1;
                }
                
                ordenOperando = (from O in operandoAnterior.OrdenOperandos where O.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden += 1;
                }
            }

            DibujarElementosOperaciones();
            ListarOperandos();
        }

        private void ClickBotonBajar(object sender, RoutedEventArgs e)
        {
            List<DiseñoOperacion> operandos = (from E in CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores
                                               select E).OrderByDescending((j) => j.OrdenOperandos.Where((a) => a.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado).FirstOrDefault().Orden).ToList();

            DiseñoOperacion operandoPosterior = null;
            DiseñoOperacion operando = null;
            foreach (var item in operandos)
            {
                if (item == ((Button)sender).Tag)
                {
                    operando = item;
                    break;
                }
                operandoPosterior = item;
            }

            if (operandoPosterior != null)
            {
                OrdenOperando ordenOperando = (from O in operando.OrdenOperandos where O.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden += 1;
                }

                ordenOperando = (from O in operandoPosterior.OrdenOperandos where O.ElementoPadre == CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden -= 1;
                }
            }

            DibujarElementosOperaciones();
            ListarOperandos();
        }

        private void ClickBotonOrdenar(object sender, RoutedEventArgs e)
        {
            object[] objeto = (object[])((Button)sender).Tag;
            if(objeto != null)
            {
                if (objeto[0] != null && objeto[1] != null)
                {
                    OrdenarNumerosOperando ordenar = new OrdenarNumerosOperando();
                    ordenar.Operando = (DiseñoOperacion)objeto[0];
                    ordenar.OperacionSeleccionada = (DiseñoOperacion)objeto[1];
                    ordenar.ShowDialog();
                }
            }
        }
        private void btnBuscarOperaciones_Click(object sender, RoutedEventArgs e)
        {
            if (!BuscandoOperaciones)
            {
                btnBuscarOperaciones.Content = "Cerrar";
                BuscandoOperaciones = true;
            }
            else
            {
                btnBuscarOperaciones.Content = "Buscar";
                BuscandoOperaciones = false;
            }

            ListarEntradas();
            ListarOperaciones();
        }

        public void ActualizarDefinicionElementosPosteriores(DiseñoOperacion elemento)
        {
            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                Ventana.EliminarSubElementos_DefinicionOperacion(itemPosterior, elemento);
            }
        }

        private void ActualizarPestañasDefinicionElementosPosteriores(DiseñoOperacion elemento)
        {
            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                int elementosEntradas = (from E in itemPosterior.ElementosAnteriores
                                                where E != elemento &&
                                                E.Tipo != TipoElementoOperacion.Linea &
                                                E.Tipo != TipoElementoOperacion.Salida &
                                                E.Tipo != TipoElementoOperacion.Nota
                                                select E).Count();

                if (elementosEntradas > 0)
                {
                    Ventana.ActualizarPestañaElementoOperacion(itemPosterior);
                }
            }
        }

        private void diagrama_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed & !ElementoSeleccionado & ClicDiagrama)
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
                            if (!CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Contains(item))
                            {
                                if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                                {
                                    //((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightTextBrush;
                                    //((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightTextBrush;
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)item).Clic, DispatcherPriority.Loaded);
                                    }

                                    CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Add(((EntradaDiseñoOperaciones)item).DiseñoOperacion);
                                    CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(OperacionEspecifica))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((OperacionEspecifica)item).Clic, DispatcherPriority.Loaded);
                                    }

                                    CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Add(((OperacionEspecifica)item).DiseñoOperacion);
                                    CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(NotaDiagrama))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((NotaDiagrama)item).Clic, DispatcherPriority.Loaded);
                                    }

                                    CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Add(((NotaDiagrama)item).DiseñoOperacion);
                                    CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                            {
                                ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Remove(((EntradaDiseñoOperaciones)item).DiseñoOperacion);
                            }
                            else if (item.GetType() == typeof(OperacionEspecifica))
                            {
                                ((OperacionEspecifica)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((OperacionEspecifica)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Remove(((OperacionEspecifica)item).DiseñoOperacion);
                            }
                            else if (item.GetType() == typeof(NotaDiagrama))
                            {
                                ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                                CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados.Remove(((NotaDiagrama)item).DiseñoOperacion);
                            }

                            CalculoDiseñoSeleccionado.Seleccion.ElementosDiagramaSeleccionados.Remove(item);
                        }
                    }
                }
                catch (Exception) { }
            }
            else if (e.LeftButton == MouseButtonState.Released & !ElementoSeleccionado & ClicDiagrama)
            {
                ClicDiagrama = false;

                if (rectanguloSeleccion != null)
                    diagrama.Children.Remove(rectanguloSeleccion);

                rectanguloSeleccion = null;
            }

            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    if (Math.Round(contenedor.HorizontalOffset, 0) - Math.Round(e.GetPosition(diagrama).X, 0) <= 80)
            //    {
            //        if (contenedor.HorizontalOffset < contenedor.ScrollableWidth)
            //        {
            //            contenedor.ScrollToHorizontalOffset(contenedor.HorizontalOffset + 80);
            //        }
            //    }

            //    //if (Math.Round(contenedor.ActualWidth, 0) - Math.Round(e.GetPosition(diagrama).X, 0) <= 80)
            //    //{
            //    //    diagrama.Width = diagrama.ActualWidth + 300;
            //    //    CalculoDiseñoSeleccionado.Ancho = diagrama.Width;
            //    //    MoverHorizontalmente = true;
            //    //}

            //    if (Math.Round(contenedor.VerticalOffset, 0) - Math.Round(e.GetPosition(diagrama).Y, 0) <= 80)
            //    {
            //        if (contenedor.VerticalOffset < contenedor.ScrollableHeight)
            //        {
            //            contenedor.ScrollToVerticalOffset(contenedor.VerticalOffset + 80);
            //        }
            //    }

            //    //if (Math.Round(contenedor.ActualHeight, 0) - Math.Round(e.GetPosition(diagrama).Y, 0) <= 80)
            //    //{
            //    //    diagrama.Height = diagrama.ActualHeight + 300;
            //    //    CalculoDiseñoSeleccionado.Alto = diagrama.Height;
            //    //    MoverVerticalmente = true;                    
            //    //}
            //}
        }

        private void infoElemento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Info = infoElemento.Text;
            }
        }

        public void MostrarInfo_Elemento(DiseñoOperacion elemento)
        {
            if (elemento != null &&
                    (elemento.Tipo == TipoElementoOperacion.Nota ||
                    elemento.Tipo == TipoElementoOperacion.SeleccionarEntradas)) return;

            contenedorInformacion.Visibility = Visibility.Collapsed;
            opcionSeleccionarArchivoEntradaEjecucion.Visibility = Visibility.Collapsed;
            opcionOperarFilasRestantes.Visibility = Visibility.Collapsed;
            agregarNuevoElementoSalida.Visibility = Visibility.Collapsed;
            opcionPorcentajeRelativo.Visibility = Visibility.Collapsed;
            opcionesElementosFijosPotencia.Visibility = Visibility.Collapsed;
            opcionesElementosFijosRaiz.Visibility = Visibility.Collapsed;
            opcionesElementosFijosLogaritmo.Visibility = Visibility.Collapsed;
            opcionesElementosFijosInverso.Visibility = Visibility.Collapsed;
            opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.Visibility = Visibility.Collapsed;
            opcionUtilizarDefinicionAsignacionTextosInformacion.Visibility = Visibility.Collapsed;
            opcionesAgregarCantidadNumeros.Visibility = Visibility.Collapsed;
            opcionesOrdenarNumeros.Visibility = Visibility.Collapsed;
            opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
            opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
            ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
            ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Collapsed;
            divisionZeroContinuar.Visibility = Visibility.Collapsed;
            botonOpcionLimpiarDatos.Visibility = Visibility.Collapsed;
            botonOpcionRedondearCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            definirAgrupacionesOperandosResultados.Visibility = Visibility.Collapsed;

            if (elemento != null && elemento.Tipo != TipoElementoOperacion.Salida)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento)
                {
                    contenedorInformacion.Visibility = Visibility.Visible;
                    infoElemento.Text = elemento.Info;
                    opcionPorcentajeRelativo.IsChecked = (bool)elemento.PorcentajeRelativo;
                    valorOpcionElementoFijo.Text = elemento.ValorOpcionElementosFijos.ToString();

                    if (elemento.Tipo == TipoElementoOperacion.Porcentaje)
                        opcionPorcentajeRelativo.Visibility = Visibility.Visible;
                    else
                        opcionPorcentajeRelativo.Visibility = Visibility.Collapsed;

                    if (elemento.Tipo == TipoElementoOperacion.Potencia)
                    {
                        opcionesElementosFijosPotencia.Visibility = Visibility.Visible;
                        opcionesElementosFijosPotencia.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosPotencia.Items where I.Uid == ((int)elemento.OpcionElementosFijosPotencia).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosPotencia.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosPotencia.SelectedItem = null;
                    }

                    if (elemento.Tipo == TipoElementoOperacion.Raiz)
                    {
                        opcionesElementosFijosRaiz.Visibility = Visibility.Visible;
                        opcionesElementosFijosRaiz.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosRaiz.Items where I.Uid == ((int)elemento.OpcionElementosFijosRaiz).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosRaiz.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosRaiz.SelectedItem = null;
                    }

                    if (elemento.Tipo == TipoElementoOperacion.Logaritmo)
                    {
                        opcionesElementosFijosLogaritmo.Visibility = Visibility.Visible;
                        opcionesElementosFijosLogaritmo.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosLogaritmo.Items where I.Uid == ((int)elemento.OpcionElementosFijosLogaritmo).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosLogaritmo.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosLogaritmo.SelectedItem = null;
                    }

                    if (elemento.Tipo == TipoElementoOperacion.Inverso)
                    {
                        opcionesElementosFijosInverso.Visibility = Visibility.Visible;
                        opcionesElementosFijosInverso.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosInverso.Items where I.Uid == ((int)elemento.OpcionElementosFijosInverso).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosInverso.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosInverso.SelectedItem = null;
                    }

                    if(elemento.Tipo == TipoElementoOperacion.Division)
                    {
                        divisionZeroContinuar.IsChecked = (bool)elemento.DivisionZero_Continuar;
                        divisionZeroContinuar.Visibility = Visibility.Visible;
                    }

                    //opcionesElementosFijosPotencia_SelectionChanged(this, null);

                    condicionesTextosInformacionOperandosResultados.Operandos = new List<DiseñoOperacion> { elemento };
                    condicionesTextosInformacionOperandosResultados.Operandos.AddRange(elemento.ElementosAnteriores);
                    condicionesTextosInformacionOperandosResultados.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(
                        condicionesTextosInformacionOperandosResultados.Operandos).ToList();

                    condicionesTextosInformacionOperandosResultados.DefinicionesListas = Calculo.TextosInformacion.ElementosTextosInformacion.Where(
                        i => i.GetType() == typeof(DiseñoListaCadenasTexto)).Select(i => (DiseñoListaCadenasTexto)i).ToList();

                    opcionTextosInformacionOperandosResultados.IsChecked = elemento.AsignarTextosInformacion_OperandosResultados;
                    opcionTextosInformacionCondicionesOperandosResultados.IsChecked = elemento.AsignarTextosInformacionCondiciones_OperandosResultados;

                    condicionesTextosInformacionOperandosResultados.OperandoSeleccionado = elemento;
                    condicionesTextosInformacionOperandosResultados.Condiciones = elemento.CondicionesTextosInformacionOperandosResultados;
                    condicionesTextosInformacionOperandosResultados.ListarCondiciones();

                    if(!elemento.NingunOperandoTextosInformacionOperandosResultados &
                        !elemento.AlgunosOperandosTextosInformacionOperandosResultados)
                        opcionTodosOperandosTextosInformacionOperandosResultados.IsChecked = true;

                    opcionNingunOperandoTextosInformacionOperandosResultados.IsChecked = elemento.NingunOperandoTextosInformacionOperandosResultados;
                    opcionAlgunosOperandosTextosInformacionOperandosResultados.IsChecked = elemento.AlgunosOperandosTextosInformacionOperandosResultados;

                    ClicElemento = true;
                    opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked = elemento.AsignarTextosInformacion_AntesImplicaciones;
                    opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked = elemento.AsignarTextosInformacion_DespuesImplicaciones;
                    opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked = elemento.AsignarTextosInformacion_AntesEjecucion;
                    opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked = elemento.AsignarTextosInformacion_DespuesEjecucion;
                    ClicElemento = false;

                    opcionQuitarTextosInformacion_Repetidos.IsChecked = elemento.QuitarTextosInformacion_Repetidos;
                    opcionQuitarClasificadores_AntesEjecucion.IsChecked = elemento.QuitarClasificadores_AntesEjecucion;
                    opcionQuitarClasificadores_DespuesEjecucion.IsChecked = elemento.QuitarClasificadores_DespuesEjecucion;

                    opcionAntesEjecucion_Clasificadores.IsChecked = elemento.OrdenarClasificadores_AntesEjecucion;
                    if (opcionAntesEjecucion_Clasificadores.IsChecked == true)
                    {
                        opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                        opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;

                        opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.IsChecked = elemento.OrdenarClasificadoresDeMenorAMayor_AntesEjecucion;
                        opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.IsChecked = elemento.OrdenarClasificadoresDeMayorAMenor_AntesEjecucion;

                    }

                    opcionDespuesEjecucion_Clasificadores.IsChecked = elemento.OrdenarClasificadores_DespuesEjecucion;
                    if (opcionDespuesEjecucion_Clasificadores.IsChecked == true)
                    {
                        opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                        opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;

                        opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.IsChecked = elemento.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion;
                        opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.IsChecked = elemento.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion;
                    }

                    ListarOperandosTextosInformacionOperandosResultados(elemento);

                    if (elemento.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                        elemento.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                    {
                        opcionOperarFilasRestantes.Visibility = Visibility.Visible;

                        if (!elemento.SeguirOperandoFilas_ConElementoNeutro &
                            !elemento.SeguirOperandoFilas_ConUltimoNumero)
                            opcionOperarFilasRestantes_ConCeros.IsChecked = true;

                        opcionOperarFilasRestantes_ConCeros.IsChecked = elemento.SeguirOperandoFilas_ConElementoNeutro;
                        opcionOperarFilasRestantes_ConUltimoNumero.IsChecked = elemento.SeguirOperandoFilas_ConUltimoNumero;
                    }

                    if (elemento.Tipo == TipoElementoOperacion.Entrada &&
                        (((elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null && elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null) &&
                    (elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                    elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ListaArchivos.Any(i => i.ConfiguracionSeleccionArchivo != OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado)) ||
                    (elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior == null &&
                    (elemento.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.Archivo &&
                    elemento.EntradaRelacionada.ListaArchivos.Any(i => i.ConfiguracionSeleccionArchivo != OpcionSeleccionarArchivoEntrada.UtilizarArchivoIndicado))))))
                    {
                        opcionSeleccionarArchivoEntradaEjecucion.Visibility = Visibility.Visible;
                        opcionSeleccionarArchivoEntradaEjecucion.IsChecked = elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior == null ?
                        elemento.EntradaRelacionada.ConfigSeleccionarArchivoURL :
                        elemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfigSeleccionarArchivoURL;
                    }

                    if (elemento.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                        elemento.Tipo == TipoElementoOperacion.CondicionesFlujo)
                    {
                        agregarNuevoElementoSalida.Visibility = Visibility.Visible;
                    }

                    if (elemento.Tipo != TipoElementoOperacion.AgrupadorOperaciones &
                    elemento.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &
                    elemento.Tipo != TipoElementoOperacion.Entrada &
                    elemento.Tipo != TipoElementoOperacion.Linea)
                    {
                        opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.IsChecked = elemento.Ejecutar_SiTieneOtrosOperandosValidos;
                        opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.Visibility = Visibility.Collapsed;
                    }

                    var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(elemento);

                    if (definicionEncontrada != null && definicionEncontrada.Any())
                    {
                        opcionUtilizarDefinicionAsignacionTextosInformacion.IsChecked = elemento.UtilizarDefinicionNombres_AsignacionTextosInformacion;
                        //Grid.SetRowSpan(definirNombresCantidades, 1);
                        opcionUtilizarDefinicionAsignacionTextosInformacion.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        //Grid.SetRowSpan(definirNombresCantidades, 2);
                        opcionUtilizarDefinicionAsignacionTextosInformacion.Visibility = Visibility.Collapsed;
                    }

                    opcionLimpiarDatos.IsChecked = elemento.LimpiarDatosResultados;
                    if (opcionLimpiarDatos.IsChecked == true)
                        opcionLimpiarDatos_Checked(this, null);
                    else
                        opcionLimpiarDatos_Unchecked(this, null);

                    opcionRedondearCantidades.IsChecked = elemento.RedondearCantidadesResultados;
                    if (opcionRedondearCantidades.IsChecked == true)
                        opcionRedondearCantidades_Checked(this, null);
                    else
                        opcionRedondearCantidades_Unchecked(this, null);

                    if(elemento.ElementosAnteriores.Any(i => i.AgrupacionesOperandos_PorSeparado.Any(j => j.OperacionRelacionada == elemento)))
                    {
                        definirAgrupacionesOperandosResultados.Visibility = Visibility.Visible;
                    }
                    //if (!elemento.ElementosDiseñoOperacion.Any())
                    //{
                    //    opcionesEstablecerNombres.Visibility = Visibility.Visible;
                    //}
                    //else
                    //{
                    //    opcionesEstablecerNombres.Visibility = Visibility.Collapsed;
                    //}
                    descripcionDefiniciones.Text = ObtenerDescripcionDefiniciones();

                    if(Calculo.ListaResultados.Any(i => i.SalidaRelacionada == elemento))
                    {
                        textoDescripcionesResultados.Visibility = Visibility.Visible;
                        definirNombresResultados.Visibility = Visibility.Visible;
                        descripcionDefiniciones_Resultados.Visibility = Visibility.Visible;

                        descripcionDefiniciones_Resultados.Text = ObtenerDescripcionDefiniciones_Resultados();
                    }
                    else
                    {
                        textoDescripcionesResultados.Visibility = Visibility.Collapsed;
                        definirNombresResultados.Visibility = Visibility.Collapsed;
                        descripcionDefiniciones_Resultados.Visibility = Visibility.Collapsed;
                    }

                    if (!(elemento.Tipo == TipoElementoOperacion.AgrupadorOperaciones |
                        elemento.Tipo == TipoElementoOperacion.Nota |
                        elemento.Tipo == TipoElementoOperacion.Salida))
                    {
                        if (elemento.Tipo != TipoElementoOperacion.Entrada)
                        {
                            opcionesAgregarCantidadNumeros.Visibility = Visibility.Visible;
                            opcionAgregarCantidadNumerosCantidad.IsChecked = elemento.AgregarCantidadComoNumero;
                            opcionIncluirCantidadNumero.IsChecked = elemento.IncluirCantidadNumero;
                            opcionAgregarCantidadNumerosTextoInformacion.IsChecked = elemento.AgregarCantidadComoTextoInformacion;
                            opcionAgregarNombreTextoInformacion.IsChecked = elemento.AgregarNombreComoTextoInformacion;
                            opcionAgregarNumerosTextoInformacion.IsChecked = elemento.AgregarNumeroComoTextoInformacion;
                        }
                        else
                            opcionesAgregarCantidadNumeros.Visibility = Visibility.Collapsed;

                        opcionesOrdenarNumeros.Visibility = Visibility.Visible;

                        opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                        opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;

                        if (elemento.OrdenarNumerosAntesEjecucion != null)
                        {
                            opcionAntesEjecucion.IsChecked = true;

                            opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                            opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                            ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Visible;
                            ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Visible;

                            opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor;
                            opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor;
                            ordenarAntesEjecucion_PorNumero.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad;
                            ordenarAntesEjecucion_PorNombre.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre;
                            opcionTipoOrdenamientoAntesEjecucion.SelectedItem = (from ComboBoxItem I in opcionTipoOrdenamientoAntesEjecucion.Items where I.Uid == ((int)elemento.OrdenarNumerosAntesEjecucion.Ordenacion.Tipo_OrdenamientoNumeros).ToString() select I).FirstOrDefault();

                            opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;

                            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor;
                            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor;

                            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;

                            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                            if (ordenarAntesEjecucion_PorNombre.IsChecked == true)
                            {
                                opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Visible;
                                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Visible;
                                opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Visible;

                                if (opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked == true)
                                {
                                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                                }


                            }


                            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;


                            if (opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked == true)
                            {
                                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                            }

                        }
                        else
                            opcionAntesEjecucion.IsChecked = false;

                        if (elemento.OrdenarNumerosDespuesEjecucion != null)
                        {
                            opcionDespuesEjecucion.IsChecked = true;

                            opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                            opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                            ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Visible;
                            ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Visible;

                            opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor;
                            opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor;
                            ordenarDespuesEjecucion_PorNumero.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad;
                            ordenarDespuesEjecucion_PorNombre.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre;
                            opcionTipoOrdenamientoDespuesEjecucion.SelectedItem = (from ComboBoxItem I in opcionTipoOrdenamientoDespuesEjecucion.Items where I.Uid == ((int)elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.Tipo_OrdenamientoNumeros).ToString() select I).FirstOrDefault();

                            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;

                            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor;
                            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor;

                            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;

                            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                            if (ordenarDespuesEjecucion_PorNombre.IsChecked == true)
                            {
                                opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Visible;
                                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Visible;
                                opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Visible;

                                if (opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked == true)
                                {
                                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                                }
                            }

                            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;


                            if (opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked == true)
                            {
                                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                            }
                        }
                        else
                            opcionDespuesEjecucion.IsChecked = false;

                        if (elemento.Tipo == TipoElementoOperacion.Entrada)
                        {
                            opcionesAntesEjecucion.Visibility = Visibility.Collapsed;
                            Grid.SetRow(opcionesDespuesEjecucion, 0);
                            Grid.SetRowSpan(opcionesDespuesEjecucion, 2);
                        }
                        else
                        {
                            Grid.SetRow(opcionesDespuesEjecucion, 1);
                            Grid.SetRowSpan(opcionesDespuesEjecucion, 1);
                            opcionesAntesEjecucion.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        opcionesAgregarCantidadNumeros.Visibility = Visibility.Collapsed;
                        opcionesOrdenarNumeros.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public void MostrarConfiguracionSeparadores_Elemento(DiseñoOperacion elemento)
        {
            MostrarOcultarConfig_Separadores();            
        }

        private void btnInformacionElemento_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Nota ||
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.SeleccionarEntradas)) return;

            CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento = !CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento;

            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Salida)
                {
                    CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento = false;
                }
            }

            if (CalculoDiseñoSeleccionado.Seleccion.MostrandoInformacionElemento && CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                contenedorInformacion.Visibility = Visibility.Visible;
                opcionOperarFilasRestantes.Visibility = Visibility.Collapsed;
                agregarNuevoElementoSalida.Visibility = Visibility.Collapsed;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                {
                    opcionOperarFilasRestantes.Visibility = Visibility.Visible;

                    if (!CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro &
                            !CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero)
                        opcionOperarFilasRestantes_ConCeros.IsChecked = true;

                    opcionOperarFilasRestantes_ConCeros.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro;
                    opcionOperarFilasRestantes_ConUltimoNumero.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero;
                }

                //if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                //    ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                //    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada |
                //    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionEscribirURL == OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada)) ||
                //    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior == null &&
                //    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeleccionArchivo == OpcionSeleccionarArchivoEntrada.ElegirSeleccionarArchivoEjecucionPorEntrada |
                //    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionEscribirURL == OpcionEscribirURLEntrada.ElegirEscribirURLEjecucionPorEntrada))))
                //{
                //    opcionSeleccionarArchivoEntradaEjecucion.Visibility = Visibility.Visible;
                //    opcionSeleccionarArchivoEntradaEjecucion.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior == null ?
                //        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfigSeleccionarArchivoURL :
                //        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfigSeleccionarArchivoURL;
                //}

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo)
                {                   
                    agregarNuevoElementoSalida.Visibility = Visibility.Visible;
                }

                MostrarInfo_Elemento(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado);

            }
            else
            {
                contenedorInformacion.Visibility = Visibility.Collapsed;
                opcionSeleccionarArchivoEntradaEjecucion.Visibility = Visibility.Collapsed;
                agregarNuevoElementoSalida.Visibility = Visibility.Collapsed;
                opcionOperarFilasRestantes.Visibility = Visibility.Collapsed;
            }
        }

        private void ListarOperandosTextosInformacionOperandosResultados(DiseñoOperacion elemento)
        {
            listaOperandosTextosInformacionOperandosResultados.Children.Clear();
            foreach (var itemOperando in elemento.ElementosAnteriores)
            {
                CheckBox checkOperando = new CheckBox();
                checkOperando.Margin = new Thickness(10);
                checkOperando.Tag = new object[] { itemOperando, elemento.OperandosTextosInformacionOperandosResultados };
                checkOperando.Content = itemOperando.NombreCombo;

                checkOperando.IsChecked = elemento.OperandosTextosInformacionOperandosResultados.Any(item => item == itemOperando);

                checkOperando.Checked += CheckOperando_Checked;
                checkOperando.Unchecked += CheckOperando_Unchecked;

                listaOperandosTextosInformacionOperandosResultados.Children.Add(checkOperando);
            }

            if (elemento.AlgunosOperandosTextosInformacionOperandosResultados)
            {
                listaOperandosTextosInformacionOperandosResultados.Visibility = Visibility.Visible;
            }
            else
                listaOperandosTextosInformacionOperandosResultados.Visibility = Visibility.Collapsed;
        }

        private void CheckOperando_Unchecked(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;

            if (objetos != null)
            {
                DiseñoOperacion itemOperando = (DiseñoOperacion)objetos[0];
                List<DiseñoOperacion> Operandos = (List<DiseñoOperacion>)objetos[1];

                if (Operandos.Contains(itemOperando))
                {
                    Operandos.Remove(itemOperando);

                    Calculo.HayCambios = true;
                }
            }
        }

        private void CheckOperando_Checked(object sender, RoutedEventArgs e)
        {
            object[] objetos = (object[])((CheckBox)sender).Tag;

            if (objetos != null)
            {
                DiseñoOperacion itemOperando = (DiseñoOperacion)objetos[0];
                List<DiseñoOperacion> Operandos = (List<DiseñoOperacion>)objetos[1];

                if (!Operandos.Contains(itemOperando))
                {
                    Operandos.Add(itemOperando);

                    Calculo.HayCambios = true;
                }
            }
        }
        public void AplicarZoom(int zoom)
        {
            if (CalculoDiseñoSeleccionado != null && CalculoDiseñoSeleccionado.Seleccion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.escalaZoom = (double)zoom / 100.0;
                diagrama.LayoutTransform = new ScaleTransform(CalculoDiseñoSeleccionado.Seleccion.escalaZoom,
                    CalculoDiseñoSeleccionado.Seleccion.escalaZoom);
                diagrama.UpdateLayout();

                if (ModoAgrupador)
                {
                    DibujarTodasLineasElementos();
                }
            }
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
                if (CalculoDiseñoSeleccionado != null && CalculoDiseñoSeleccionado.Seleccion != null)
                    CalculoDiseñoSeleccionado.Seleccion.CantidadZoom = cantidadZoom;
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

            if (ModoAgrupador)
            {
                Agrupador.AltoDiagrama = diagrama.Height;
                Agrupador.AnchoDiagrama = diagrama.Width;
            }
            else
            {
                CalculoDiseñoSeleccionado.Alto = diagrama.Height;
                CalculoDiseñoSeleccionado.Ancho = diagrama.Width;
            }

            if (ModoAgrupador)
            {
                DibujarTodasLineasElementos();
            }

            //foreach (var agrupador in CalculoDiseñoSeleccionado.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones))
            //{
            //    agrupador.AnchoDiagrama = diagrama.Width;
            //    agrupador.AltoDiagrama = diagrama.Height;
            //}
        }

        private void disminuirArea_Click(object sender, RoutedEventArgs e)
        {
            if (diagrama.ActualHeight - 300 > 0 &
                diagrama.ActualWidth - 300 > 0)
            {
                diagrama.Height = diagrama.ActualHeight - 300;
                diagrama.Width = diagrama.ActualWidth - 300;

                if (ModoAgrupador)
                {
                    Agrupador.AltoDiagrama = diagrama.Height;
                    Agrupador.AnchoDiagrama = diagrama.Width;
                }
                else
                {
                    CalculoDiseñoSeleccionado.Alto = diagrama.Height;
                    CalculoDiseñoSeleccionado.Ancho = diagrama.Width;
                }

                if (ModoAgrupador)
                {
                    DibujarTodasLineasElementos();
                }

                //foreach (var agrupador in CalculoDiseñoSeleccionado.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.AgrupadorOperaciones))
                //{
                //    agrupador.AnchoDiagrama = diagrama.Width;
                //    agrupador.AltoDiagrama = diagrama.Height;
                //}
            }
        }

        private void buscarDiagrama_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textoBusquedaDiagrama.Text))
            {
                List<UIElement> elementos = BuscarElementosDiagramas(textoBusquedaDiagrama.Text);

                CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Clear();
                CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados = -1;
                resultadosBusquedas.Visibility = Visibility.Collapsed;

                if (elementos != null && elementos.Any())
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.AddRange(elementos);                    
                    siguienteResultado_Click(this, e);
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Count > 1)
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
            if (CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados < CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Count - 1)
            {
                CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados++;
                MostrarElementoEncontrado();
            }
        }

        private void anteriorResultado_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados > 0)
            {
                CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados--;
                MostrarElementoEncontrado();
            }
        }

        private void MostrarElementoEncontrado()
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Any())
            {
                UIElement elemento = CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados[CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados];
                //         UIElement elementoDiagrama = (from UIElement E in diagrama.Children
                //                                       where
                //(E.GetType() == typeof(EntradaDiseñoOperaciones) && ((EntradaDiseñoOperaciones)E).DiseñoOperacion == elemento) |
                //(E.GetType() == typeof(OperacionEspecifica) && ((OperacionEspecifica)E).DiseñoOperacion == elemento)
                //                                       select E).FirstOrDefault();

                //if(elementoDiagrama.GetType() == typeof(EntradaDiseñoOperaciones))
                //    Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)elementoDiagrama).Clic);

                //if (elementoDiagrama.GetType() == typeof(OperacionEspecifica))
                //    Ventana.Dispatcher.Invoke(((OperacionEspecifica)elementoDiagrama).Clic);

                SeleccionandoElemento = true;
                CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento = new MouseButtonEventArgs(Mouse.PrimaryDevice, DateTime.Now.Hour, MouseButton.Left);

                if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)elemento).Clic);
                    }
                }

                if (elemento.GetType() == typeof(OperacionEspecifica))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((OperacionEspecifica)elemento).Clic);
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

                CalculoDiseñoSeleccionado.Seleccion.e_SeleccionarElemento = null;
                SeleccionandoElemento = false;

                contenedor.ScrollToHorizontalOffset(Canvas.GetLeft(elemento) * CalculoDiseñoSeleccionado.Seleccion.escalaZoom);
                contenedor.ScrollToVerticalOffset(Canvas.GetTop(elemento) * CalculoDiseñoSeleccionado.Seleccion.escalaZoom);
            }
        }

        private List<UIElement> BuscarElementosDiagramas(string textoBusqueda)
        {
            List<UIElement> elementos = new List<UIElement>();
            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    EntradaDiseñoOperaciones entrada = (EntradaDiseñoOperaciones)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (entrada.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                            entrada.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(entrada);
                    }
                }
                else if (item.GetType() == typeof(OperacionEspecifica))
                {
                    OperacionEspecifica operacion = (OperacionEspecifica)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (operacion.nombreOperacion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(operacion);
                    }
                }
                else if (item.GetType() == typeof(NotaDiagrama))
                {
                    NotaDiagrama operacion = (NotaDiagrama)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (operacion.textoNota.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(operacion);
                    }
                }
            }

            return elementos;
        }

        private void limpiarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            CalculoDiseñoSeleccionado.Seleccion.indiceElementosEncontrados = -1;
            CalculoDiseñoSeleccionado.Seleccion.ElementosEncontrados.Clear();
            textoBusquedaDiagrama.Text = string.Empty;
            resultadosBusquedas.Visibility = Visibility.Collapsed;
        }

        private void textoBusquedaDiagrama_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculoDiseñoSeleccionado.Seleccion.TextoBusquedaDiagrama = textoBusquedaDiagrama.Text;
        }

        private void UserControl_Loaded(object sender, SizeChangedEventArgs e)
        {
            if(Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            if (Calculo.ModoSubCalculo &&
                Calculo.ModoSubCalculo_Simple)
            {
                grillaCalculos.Visibility = Visibility.Collapsed;
            }

            CargarDatos();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirOperacionesCalculo");
#endif
        }

        private void CargarDatos()
        {
            if (!ModoAgrupador)
            {
                ListarOperaciones();

                if (!Calculo.ModoSubCalculo ||
                    !Calculo.ModoSubCalculo_Simple)
                    ListarCalculos();

                if (!Calculo.Calculos.Contains(CalculoDiseñoSeleccionado))
                    CalculoDiseñoSeleccionado = Calculo.Calculos.First();

                SeleccionarCalculo(null, null);

                if (!Calculo.ModoSubCalculo ||
                    !Calculo.ModoSubCalculo_Simple)
                    MarcarCalculoSeleccionado();
                //CargarDatosCalculoSeleccionado();
                if (double.IsNaN(CalculoDiseñoSeleccionado.Ancho) &
                double.IsNaN(CalculoDiseñoSeleccionado.Alto))
                { 
                    diagrama.Width = contenedor.ActualWidth;
                    CalculoDiseñoSeleccionado.Ancho = diagrama.Width;

                    diagrama.Height = contenedor.ActualHeight;
                    CalculoDiseñoSeleccionado.Alto = diagrama.Height;

                }
            }
            else
            {
                ListarEntradas();
                ListarOperaciones();
                grillaCalculos.Visibility = Visibility.Collapsed;
                contenedorCalculos.Visibility = Visibility.Collapsed;
                btnDeshacerAgrupador.Visibility = Visibility.Visible;

                if (double.IsNaN(Agrupador.AnchoDiagrama) &
                double.IsNaN(Agrupador.AltoDiagrama))
                {
                    diagrama.Width = contenedor.ActualWidth;
                    Agrupador.AnchoDiagrama = diagrama.Width;

                    diagrama.Height = contenedor.ActualHeight;
                    Agrupador.AltoDiagrama = diagrama.Height;

                }
                else
                {
                    diagrama.Width = Agrupador.AnchoDiagrama;
                    diagrama.Height = Agrupador.AltoDiagrama;
                }

                DibujarElementosOperaciones();
            }

            if (Calculo.ModoSubCalculo &&
                    Calculo.ModoSubCalculo_Simple)
            {
                calculos.Visibility = Visibility.Collapsed;
            }
        }

        private void btnConfiguracionSeparadores_Click(object sender, RoutedEventArgs e)
        {
            bool mostrar = false;

            if (!CalculoDiseñoSeleccionado.Seleccion.MostrandoConfiguracionSeparadores)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior == null && 
                        (((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeObtiene |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene) &&
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet))) ||
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null && 
                    ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeObtiene |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene) &&
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet))))
                    {
                        mostrar = true;
                    }

                }
                else
                {
                    mostrar = false;
                }
            }
            else
                mostrar = true;

            if (mostrar)
            {
                CalculoDiseñoSeleccionado.Seleccion.MostrandoConfiguracionSeparadores = !CalculoDiseñoSeleccionado.Seleccion.MostrandoConfiguracionSeparadores;
                MostrarOcultarConfig_Separadores();
            }            
        }

        private void MostrarOcultarConfig_Separadores()
        {
            bool mostrar = false;

            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
            {
                if (!(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior == null &&
                        (((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeObtiene |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene) &&
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet))) ||
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    ((CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null &&
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOpcionNumero == TipoOpcionNumeroEntrada.SeObtiene |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOpcionConjuntoNumeros == TipoOpcionConjuntoNumerosEntrada.SeObtiene) &&
                    (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.Archivo |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.TipoOrigenDatos == TipoOrigenDatos.DesdeInternet))))))
                {
                    mostrar = false;
                }
                else
                {
                    if(CalculoDiseñoSeleccionado.Seleccion.MostrandoConfiguracionSeparadores)
                        mostrar = true;
                }

            }
            else
            {
                mostrar = false;
            }

            if (mostrar)
            {
                Entrada entrada = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null)
                    entrada = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada;

                switch (entrada.ConfiguracionSeparadores)
                {
                    case TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesPunto:
                        opcionSeparadorMilesComa_SeparadorDecimalesPunto.IsChecked = true;
                        break;

                    case TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesComa:
                        opcionSeparadorMilesNinguno_SeparadorDecimalesComa.IsChecked = true;
                        break;

                    case TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesPunto:
                        opcionSeparadorMilesNinguno_SeparadorDecimalesPunto.IsChecked = true;
                        break;

                    case TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesComa:
                        opcionSeparadorMilesPunto_SeparadorDecimalesComa.IsChecked = true;
                        break;
                }
                contenedorConfiguracionSeparadores.Visibility = Visibility.Visible;
            }
            else
            {
                contenedorConfiguracionSeparadores.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionSeparadorMilesPunto_SeparadorDecimalesComa_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (opcionSeparadorMilesPunto_SeparadorDecimalesComa.IsChecked == true)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesComa;
                    else
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesComa;
                }
            }
        }

        private void opcionSeparadorMilesNinguno_SeparadorDecimalesComa_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (opcionSeparadorMilesNinguno_SeparadorDecimalesComa.IsChecked == true)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesComa;
                    else
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesComa;
                }
            }
        }

        private void opcionSeparadorMilesNinguno_SeparadorDecimalesPunto_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (opcionSeparadorMilesNinguno_SeparadorDecimalesPunto.IsChecked == true)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesPunto;
                    else
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesNinguno_SeparadorDecimalesPunto;
                }
            }
        }

        private void opcionSeparadorMilesComa_SeparadorDecimalesPunto_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (opcionSeparadorMilesComa_SeparadorDecimalesPunto.IsChecked == true)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesPunto;
                    else
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesPunto;
                }
            }
        }

        private void opcionSeleccionarArchivoEntradaEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfigSeleccionarArchivoURL = (bool)opcionSeleccionarArchivoEntradaEjecucion.IsChecked;
                else
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfigSeleccionarArchivoURL = (bool)opcionSeleccionarArchivoEntradaEjecucion.IsChecked;
            }
        }

        private void opcionSeleccionarArchivoEntradaEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfigSeleccionarArchivoURL = (bool)opcionSeleccionarArchivoEntradaEjecucion.IsChecked;
                else
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfigSeleccionarArchivoURL = (bool)opcionSeleccionarArchivoEntradaEjecucion.IsChecked;
            }
        }

        public void QuitarElementoDiagrama(DiseñoOperacion elemento, bool quitarVistasAgrupadores = true)
        {
            //if (elemento.Tipo == TipoElementoOperacion.Entrada)
            //{
            //if (elemento.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
            //{
            QuitarDeAsociacionesTextosInformacion_SeleccionarOrdenar(elemento);
            QuitarDeAsociacionesTextosInformacion_CondicionFlujo(elemento);
            QuitarDeAsociacionesTextosInformacion_SeleccionarEntradas(elemento);
            QuitarDeAsociacionesProcesamientoCantidades(elemento);
            //}

            foreach (var elementoAnterior in elemento.ElementosAnteriores)
            {
                foreach (var item in elementoAnterior.ElementosDiseñoOperacion.Where(i => i.ContieneSalida &&
                i.ElementoSalidaOperacion_Agrupamiento == elemento))
                {
                    item.ElementoSalidaOperacion_Agrupamiento = null;
                    item.ElementoInternoSalidaOperacion_Agrupamiento = null;
                }
            }

            Ventana.CerrarPestañasElementoOperacionEliminado(elemento, quitarVistasAgrupadores);
            Ventana.ActualizarDefinicionesTextos_ElementoOperacionEliminado(elemento, Calculo);

            ActualizarDefinicionElementosPosteriores(elemento);

            if (CalculoDiseñoSeleccionado.ElementosPosteriores.Count == 0)
            {
                Resultado resultadoRelacionado = (from Resultado R in Calculo.ListaResultados where R.SalidaRelacionada == elemento select R).FirstOrDefault();
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

            QuitarElementoSalida_ElementoPosterior(CalculoDiseñoSeleccionado, elemento);
                
            if (elemento.ContieneSalida)
            {
                QuitarElementoSalida(elemento, CalculoDiseñoSeleccionado);
            }

            //QuitarTodasLineas();

            foreach (var item in CalculoDiseñoSeleccionado.ElementosOperaciones)
                QuitarDeElementosPosterioresAnteriores(item, elemento);

            foreach (var itemPosterior in elemento.ElementosPosteriores)
                QuitarReferenciasElementos_DefinicionNombresCantidades(elemento, itemPosterior);

            var tabs = (from TabItem T in Ventana.contenido.Items
                        where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
elemento.ElementosAnteriores.Contains(((VistaDiseñoOperacion)T.Content).Operacion)
                        select T).ToList();

            foreach (var tab in tabs)
            {
                ((VistaDiseñoOperacion)tab.Content).ListarElementosSalida_Agrupamiento(((VistaDiseñoOperacion)tab.Content).ElementoSeleccionado);
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                if (itemPosterior.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                {
                    QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(elemento, itemPosterior);
                }
            }

            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                {
                    QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(elemento, itemAnterior);
                }
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                if (itemPosterior.Tipo == TipoElementoOperacion.CondicionesFlujo)
                {
                    QuitarReferenciasElementos_CondicionesFlujo(elemento, itemPosterior);
                }
            }

            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoOperacion.CondicionesFlujo)
                {
                    QuitarReferenciasElementos_CondicionesFlujo(elemento, itemAnterior);
                }
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                if (itemPosterior.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                {
                    QuitarReferenciasElementos_CondicionesSeleccionarEntradas(elemento, itemPosterior);
                }
            }

            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                {
                    QuitarReferenciasElementos_CondicionesSeleccionarEntradas(elemento, itemAnterior);
                }
            }

            foreach(var itemElemento in CalculoDiseñoSeleccionado.ElementosOperaciones)
            {
                foreach (var itemLogica in itemElemento.ProcesamientoCantidades)
                {
                    if (itemLogica.CondicionesCantidades != null)
                    {
                        itemLogica.CondicionesCantidades.QuitarReferenciasCondicionesElemento(elemento);

                        if (elemento.EntradaRelacionada != null)
                            itemLogica.CondicionesCantidades.QuitarReferenciasCondicionesEntrada(elemento.EntradaRelacionada);
                    }
                }
            }

            CalculoDiseñoSeleccionado.ElementosOperaciones.Remove(elemento);
            ActualizarContenedoresElementos(elemento);

            ActualizarPestañasDefinicionElementosPosteriores(elemento);

            QuitarReferenciasElementosImplicacion_AsignacionTextosInformacion(elemento, Calculo);

            if (ModoAgrupador)
            {
                Agrupador.ElementosAgrupados.Remove(elemento);
                elemento.AgrupadorContenedor = null;
            }
            //}
        }
        private void agregarNuevoElementoSalida_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea)
            {
                var ElementoDiseñoOperacionSeleccionado = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;

                if (ElementoDiseñoOperacionSeleccionado != null)
                {
                    if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                    {
                        AgregarElementoSalida_AgrupadoSeleccionOrdenamiento(ElementoDiseñoOperacionSeleccionado);
                    }
                    else if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo)
                    {
                        AgregarElementoSalida_AgrupadoSeleccionCondiciones(ElementoDiseñoOperacionSeleccionado);
                    }
                }
            }
        }

        private void AgregarElementoSalida_AgrupadoSeleccionOrdenamiento(DiseñoOperacion elemento)
        {
            //OpcionOperacion nuevaOperacion = new OpcionOperacion();
            //nuevaOperacion.VistaOpciones = this;
            //nuevaOperacion.EnDiagrama = true;
            //nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            //nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            OperacionEspecifica nuevaOperacion = new OperacionEspecifica();
            nuevaOperacion.VistaOperaciones = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaOperacion.Tipo = TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;
            nuevaOperacion.SizeChanged += CambioTamañoOperacion;
            //string nombreOperacion = "Números obtenidos de " + elemento.NombreElemento;

            //nuevaOperacion.NombreOperacion = nombreOperacion;
            //nuevaOperacion.Tipo = TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

            //DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            //nuevoElementoOperacion.NombreElemento = "Números obtenidos " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            //nuevoElementoOperacion.Tipo = elemento.Tipo;
            //nuevoElementoOperacion.TipoOpcionOperacion = nuevaOperacion.Tipo;
            //nuevoElementoOperacion.PosicionX = elemento.PosicionX + elemento.Anchura + 10;
            //nuevoElementoOperacion.PosicionY = elemento.PosicionY;

            //nuevoElementoOperacion.Nombre = nuevaOperacion.nombreOpcion.Text;

            DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.Tipo = nuevaOperacion.Tipo;
            nuevoElementoOperacion.PosicionX = elemento.PosicionX + elemento.Anchura + 10;
            nuevoElementoOperacion.PosicionY = elemento.PosicionY;

            var ultimoNombre = (from DiseñoOperacion E in CalculoDiseñoSeleccionado.ElementosOperaciones where E.Tipo ==  TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            cantidadElementosTipo++;

            nuevoElementoOperacion.Nombre = "Números obtenidos de lógica de selección de números " + cantidadElementosTipo.ToString();
            nuevoElementoOperacion.Actualizar_ToolTips = true;

            //Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            //nuevaOperacion.DiseñoElementoOperacion = nuevoElementoOperacion;

            CalculoDiseñoSeleccionado.ElementosOperaciones.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoOperacion = nuevoElementoOperacion;

            elemento.ElementosPosteriores.Add(nuevoElementoOperacion);
            nuevoElementoOperacion.ElementosAnteriores.Add(elemento);

            elemento.ElementosContenedoresOperacion.Add(nuevoElementoOperacion);
            elemento.AgregarOrdenOperando(nuevoElementoOperacion);

            elemento.SalidasAgrupamiento_SeleccionOrdenamiento.Add(nuevoElementoOperacion);

            if (ModoAgrupador)
            {
                Agrupador.ElementosAgrupados.Add(nuevoElementoOperacion);
                nuevoElementoOperacion.AgrupadorContenedor = Agrupador;
            }

            //elemento.ElementosPosteriores.Add(nuevoElementoOperacion);
            //elemento.SalidasAgrupamiento_SeleccionOrdenamiento.Add(nuevoElementoOperacion);
            //nuevoElementoOperacion.ElementosAnteriores.Add(elemento);
            //nuevoElementoOperacion.EntradaRelacionada = elemento.EntradaRelacionada;
            //elemento.ElementosContenedoresOperacion.Add(nuevoElementoOperacion);
            //elemento.AgregarOrdenOperando(nuevoElementoOperacion);

            diagrama.Children.Add(nuevaOperacion);

            Canvas.SetTop(nuevaOperacion, nuevaOperacion.DiseñoOperacion.PosicionY);
            Canvas.SetLeft(nuevaOperacion, nuevaOperacion.DiseñoOperacion.PosicionX);

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            QuitarActualizarResultados_ElementosConectados(elemento);

            DibujarElementosOperaciones();
            MostrarOrdenOperando_Elemento(null);

            Ventana.ActualizarPestañaElementoOperacion(nuevoElementoOperacion);

            //diagrama.Children.Add(nuevaOperacion);

            //Canvas.SetTop(nuevaOperacion, nuevoElementoOperacion.PosicionY);
            //Canvas.SetLeft(nuevaOperacion, nuevoElementoOperacion.PosicionX);

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            //EstablecerIndicesProfundidadElementos();
            //DibujarTodasLineasElementos();
        }

        private void AgregarElementoSalida_AgrupadoSeleccionCondiciones(DiseñoOperacion elemento)
        {
            //OpcionOperacion nuevaOperacion = new OpcionOperacion();
            //nuevaOperacion.VistaOpciones = this;
            //nuevaOperacion.EnDiagrama = true;
            //nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            //nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            OperacionEspecifica nuevaOperacion = new OperacionEspecifica();
            nuevaOperacion.VistaOperaciones = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaOperacion.Tipo = TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;
            nuevaOperacion.SizeChanged += CambioTamañoOperacion;
            //string nombreOperacion = "Números obtenidos de " + elemento.NombreElemento;

            //nuevaOperacion.NombreOperacion = nombreOperacion;
            //nuevaOperacion.Tipo = TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

            //DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            //nuevoElementoOperacion.NombreElemento = "Números obtenidos " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            //nuevoElementoOperacion.Tipo = elemento.Tipo;
            //nuevoElementoOperacion.TipoOpcionOperacion = nuevaOperacion.Tipo;
            //nuevoElementoOperacion.PosicionX = elemento.PosicionX + elemento.Anchura + 10;
            //nuevoElementoOperacion.PosicionY = elemento.PosicionY;

            //nuevoElementoOperacion.Nombre = nuevaOperacion.nombreOpcion.Text;

            DiseñoOperacion nuevoElementoOperacion = new DiseñoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.Tipo = nuevaOperacion.Tipo;
            nuevoElementoOperacion.PosicionX = elemento.PosicionX + elemento.Anchura + 10;
            nuevoElementoOperacion.PosicionY = elemento.PosicionY;

            var ultimoNombre = (from DiseñoOperacion E in CalculoDiseñoSeleccionado.ElementosOperaciones where E.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar && E.Nombre.LastIndexOf(" ") > -1 select E.Nombre).LastOrDefault();
            int cantidadElementosTipo = 0;
            if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
            cantidadElementosTipo++;

            nuevoElementoOperacion.Nombre = "Números obtenidos de lógica de selección " + cantidadElementosTipo.ToString();
            nuevoElementoOperacion.Actualizar_ToolTips = true;

            //Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            //nuevaOperacion.DiseñoElementoOperacion = nuevoElementoOperacion;

            CalculoDiseñoSeleccionado.ElementosOperaciones.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoOperacion = nuevoElementoOperacion;

            elemento.ElementosPosteriores.Add(nuevoElementoOperacion);
            nuevoElementoOperacion.ElementosAnteriores.Add(elemento);

            elemento.ElementosContenedoresOperacion.Add(nuevoElementoOperacion);
            elemento.AgregarOrdenOperando(nuevoElementoOperacion);

            elemento.SalidasAgrupamiento_SeleccionCondicionesFlujo.Add(nuevoElementoOperacion);

            if (ModoAgrupador)
            {
                Agrupador.ElementosAgrupados.Add(nuevoElementoOperacion);
                nuevoElementoOperacion.AgrupadorContenedor = Agrupador;
            }

            //elemento.ElementosPosteriores.Add(nuevoElementoOperacion);
            //elemento.SalidasAgrupamiento_SeleccionOrdenamiento.Add(nuevoElementoOperacion);
            //nuevoElementoOperacion.ElementosAnteriores.Add(elemento);
            //nuevoElementoOperacion.EntradaRelacionada = elemento.EntradaRelacionada;
            //elemento.ElementosContenedoresOperacion.Add(nuevoElementoOperacion);
            //elemento.AgregarOrdenOperando(nuevoElementoOperacion);

            diagrama.Children.Add(nuevaOperacion);

            Canvas.SetTop(nuevaOperacion, nuevaOperacion.DiseñoOperacion.PosicionY);
            Canvas.SetLeft(nuevaOperacion, nuevaOperacion.DiseñoOperacion.PosicionX);

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            QuitarActualizarResultados_ElementosConectados(elemento);

            DibujarElementosOperaciones();
            MostrarOrdenOperando_Elemento(null);

            Ventana.ActualizarPestañaElementoOperacion(nuevoElementoOperacion);

            //diagrama.Children.Add(nuevaOperacion);

            //Canvas.SetTop(nuevaOperacion, nuevoElementoOperacion.PosicionY);
            //Canvas.SetLeft(nuevaOperacion, nuevoElementoOperacion.PosicionX);

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            //EstablecerIndicesProfundidadElementos();
            //DibujarTodasLineasElementos();
        }

        private bool VerificarElementos_SeleccionarOrdenar(DiseñoOperacion elementoOrigen,
            DiseñoOperacion elementoDestino)
        {
            //if (elementoOrigen.Tipo == TipoElementoOperacion.SeleccionarOrdenar &&
            //    elementoOrigen.AgruparSeleccionesEnElementosSalida)
            //{
            //    return false;
            //}

            //if (elementoOrigen.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
            //    return false;

            if ((elementoOrigen.Tipo == TipoElementoOperacion.SeleccionarOrdenar && 
                elementoOrigen.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elementoDestino)) ||
                (elementoOrigen.Tipo == TipoElementoOperacion.CondicionesFlujo && 
                elementoOrigen.SalidasAgrupamiento_SeleccionCondicionesFlujo.Contains(elementoDestino))) return false;

            //var elementoContenedorSalida = (from E in Operacion.ElementosDiseñoOperacion where E.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elementoDestino) select E).FirstOrDefault();

            //if (elementoContenedorSalida != null) return false;
            if (elementoOrigen.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &&
                (elementoDestino.Tipo == TipoElementoOperacion.Entrada | elementoDestino.Tipo == TipoElementoOperacion.Linea)) return false;

            //if ((elementoOrigen.Tipo != TipoElementoOperacion.SeleccionarOrdenar && elementoDestino.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar) ||
            //    elementoOrigen.Tipo != TipoElementoOperacion.CondicionesFlujo && elementoDestino.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar) return false;

            return true;
        }

        private bool VerificarElementos_SeleccionarEntradas(DiseñoOperacion elementoOrigen,
            DiseñoOperacion elementoDestino)
        {

            //if (elementoOrigen.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elementoDestino) ||
            //    elementoOrigen.SalidasAgrupamiento_SeleccionCondicionesFlujo.Contains(elementoDestino)) return false;

            //var elementoContenedorSalida = (from E in Operacion.ElementosDiseñoOperacion where E.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elementoDestino) select E).FirstOrDefault();
            if(elementoOrigen.Tipo != TipoElementoOperacion.SeleccionarEntradas &&
                elementoDestino.Tipo == TipoElementoOperacion.SeleccionarEntradas) return false;

            //if (elementoContenedorSalida != null) return false;
            if (elementoOrigen.Tipo == TipoElementoOperacion.SeleccionarEntradas &&
                elementoDestino.Tipo != TipoElementoOperacion.Entrada &&
                elementoDestino.Tipo != TipoElementoOperacion.SeleccionarEntradas) return false;

            //if ((elementoOrigen.Tipo != TipoElementoOperacion.SeleccionarOrdenar && elementoDestino.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar) ||
            //    elementoOrigen.Tipo != TipoElementoOperacion.CondicionesFlujo && elementoDestino.Tipo == TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar) return false;

            return true;
        }

        private void QuitarDeAsociacionesTextosInformacion_SeleccionarOrdenar(DiseñoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoOperacion.SeleccionarOrdenar)
                {
                    var asociacion = (from A in itemAnterior.AsociacionesTextosInformacion_ElementosSalida where A.ElementoSalida_Operacion == elemento select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        itemAnterior.AsociacionesTextosInformacion_ElementosSalida.Remove(asociacion);
                    }

                    itemAnterior.SalidasAgrupamiento_SeleccionOrdenamiento.Remove(elemento);
                }
            }
        }
        private void QuitarDeAsociacionesTextosInformacion_CondicionFlujo(DiseñoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoOperacion.CondicionesFlujo)
                {
                    var asociacion = (from A in itemAnterior.AsociacionesCondicionesFlujo_ElementosSalida where A.ElementoSalida_Operacion == elemento select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        itemAnterior.AsociacionesCondicionesFlujo_ElementosSalida.Remove(asociacion);
                    }

                    var asociacion2 = (from A in itemAnterior.AsociacionesCondicionesFlujo_ElementosSalida2 where A.ElementoSalida_Operacion == elemento select A).FirstOrDefault();

                    if (asociacion2 != null)
                    {
                        itemAnterior.AsociacionesCondicionesFlujo_ElementosSalida2.Remove(asociacion2);
                    }

                    itemAnterior.SalidasAgrupamiento_SeleccionCondicionesFlujo.Remove(elemento);
                }
            }
        }

        private void QuitarDeAsociacionesTextosInformacion_SeleccionarEntradas(DiseñoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoOperacion.SeleccionarEntradas)
                {
                    var asociacion = (from A in itemAnterior.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida where A.ElementoSalida_Operacion == elemento select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        itemAnterior.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.Remove(asociacion);
                    }

                    //itemAnterior.SalidasAgrupamiento_SeleccionEntradas.Remove(elemento);
                }
            }
        }

        private void QuitarDeAsociacionesProcesamientoCantidades(DiseñoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                foreach (var itemCondicion in itemAnterior.ProcesamientoCantidades)
                    itemCondicion.QuitarElemento(elemento);
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                foreach (var itemCondicion in itemPosterior.ProcesamientoCantidades)
                    itemCondicion.QuitarElemento(elemento);
            }

            foreach (var itemCondicion in elemento.ProcesamientoCantidades)
                itemCondicion.QuitarElemento(elemento);

            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                foreach (var itemCondicion in itemAnterior.ProcesamientoTextosInformacion)
                    itemCondicion.QuitarElemento(elemento);
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                foreach (var itemCondicion in itemPosterior.ProcesamientoTextosInformacion)
                    itemCondicion.QuitarElemento(elemento);
            }

            foreach (var itemCondicion in elemento.ProcesamientoTextosInformacion)
                itemCondicion.QuitarElemento(elemento);
        }

        private void btnDeshacerAgrupador_Click(object sender, RoutedEventArgs e)
        {
            if (ModoAgrupador)
            {
                SeleccionarOpcionesDeshacerAgrupador deshacer = new SeleccionarOpcionesDeshacerAgrupador();
                deshacer.ShowDialog();

                if (deshacer.Aceptar)
                {
                    CalculoDiseñoSeleccionado.Agrupadores.Remove(Agrupador);
                    VistaOperaciones.QuitarElementoDiagrama(Agrupador, false);

                    if (deshacer.QuitarElementosCalculo)
                    {
                        foreach (var item in Agrupador.ElementosAgrupados)
                            VistaOperaciones.QuitarElementoDiagrama(item);
                    }
                    else
                    {                        
                        if (Agrupador.AgrupadorContenedor != null)
                        {
                            foreach (var item in Agrupador.ElementosAgrupados)
                            {
                                Agrupador.AgrupadorContenedor.ElementosAgrupados.Add(item);
                                item.AgrupadorContenedor = Agrupador.AgrupadorContenedor;
                                //Agrupador.AgrupadorContenedor.ElementosPosterioresAgrupados.AddRange(item.ElementosPosteriores);
                                //Agrupador.AgrupadorContenedor.ElementosAnterioresAgrupados.AddRange(item.ElementosAnteriores);
                            }
                        }
                    }

                    Ventana.btnCerrar_Click(this, e);
                }
            }
        }

        private void opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Ejecutar_SiTieneOtrosOperandosValidos = true;
            }
        }

        private void opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Ejecutar_SiTieneOtrosOperandosValidos = false;
            }
        }

        private void opcionUtilizarDefinicionAsignacionTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.UtilizarDefinicionNombres_AsignacionTextosInformacion = true;
            }
        }

        private void opcionUtilizarDefinicionAsignacionTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.UtilizarDefinicionNombres_AsignacionTextosInformacion = false;
            }
        }

        private void definirNombresCantidades_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.Operandos = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores;
                establecer.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(establecer.Operandos).ToList();
                establecer.TextosNombre = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DefinicionOpcionesNombresCantidades.ReplicarObjeto();

                if (establecer.ShowDialog() == true)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DefinicionOpcionesNombresCantidades = establecer.TextosNombre;
                    descripcionDefiniciones.Text = ObtenerDescripcionDefiniciones();

                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                    }
                }
            }
        }

        public string ObtenerDescripcionDefiniciones()
        {
            ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
            establecer.Operandos = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores;
            establecer.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(establecer.Operandos).ToList();
            establecer.TextosNombre = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DefinicionOpcionesNombresCantidades;
            return establecer.ObtenerDescripcion();
        }

        public string ObtenerDescripcionDefiniciones_Resultados()
        {
            ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
            establecer.Operandos = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores;
            establecer.TextosNombre = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DefinicionOpcionesNombresResultados;
            return establecer.ObtenerDescripcion();
        }

        private void opcionAgregarCantidadNumerosCantidad_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarCantidadComoNumero = (bool)opcionAgregarCantidadNumerosCantidad.IsChecked;

                opcionIncluirCantidadNumero.Visibility = Visibility.Visible;
            }
        }

        private void opcionAgregarCantidadNumerosCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarCantidadComoNumero = (bool)opcionAgregarCantidadNumerosCantidad.IsChecked;

                opcionIncluirCantidadNumero.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionAgregarCantidadNumerosTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarCantidadComoTextoInformacion = (bool)opcionAgregarCantidadNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarCantidadNumerosTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarCantidadComoTextoInformacion = (bool)opcionAgregarCantidadNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarNumerosTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarNumeroComoTextoInformacion = (bool)opcionAgregarNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarNumerosTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarNumeroComoTextoInformacion = (bool)opcionAgregarNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosAntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion == null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion = new Entidades.Operaciones.OrdenarNumerosElemento();
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;
                }

                opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Visible;
                ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Visible;

                ordenarAntesEjecucion_PorNombre.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre;
                ordenarAntesEjecucion_PorNombre_Checked(this, e);
            }
        }

        private void opcionOrdenarNumerosAntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion = null;
                opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
                ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
                opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarNumerosDespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion == null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion = new Entidades.Operaciones.OrdenarNumerosElemento();
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;
                }

                opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Visible;
                ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Visible;

                ordenarDespuesEjecucion_PorNombre.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre;
                ordenarDespuesEjecucion_PorNombre_Checked(this, e);

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarNumerosDespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion = null;
                opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
                ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
                opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionTextosInformacionOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_OperandosResultados = (bool)opcionTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void opcionTextosInformacionOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_OperandosResultados = (bool)opcionTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void acumulacion_CambioSeleccion(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConAcumulacion = (acumulacion.IsChecked != null) ? (bool)acumulacion.IsChecked : false;
                    }
                }
            }
        }

        public void MostrarAcumulacion()
        {
            acumulacion.Visibility = Visibility.Collapsed;

            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                    {
                        acumulacion.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConAcumulacion;
                        acumulacion.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void opcionOperarFilasRestantes_ConUltimoNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero = (bool)opcionOperarFilasRestantes_ConUltimoNumero.IsChecked;
                }
            }
        }

        private void opcionOperarFilasRestantes_ConUltimoNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero = (bool)opcionOperarFilasRestantes_ConUltimoNumero.IsChecked;
                }
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void ordenarAntesEjecucion_PorNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
            }
        }

        private void ordenarAntesEjecucion_PorNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
            }
        }

        private void ordenarAntesEjecucion_PorNombre_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                {
                    opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Visible;
                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Visible;
                    opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
                    opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Checked(this, e);
            }
        }

        private void ordenarAntesEjecucion_PorNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;
                opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void ordenarDespuesEjecucion_PorNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void ordenarDespuesEjecucion_PorNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void ordenarDespuesEjecucion_PorNombre_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                {
                    opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Visible;
                    opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Visible;
                    opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
                    opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Checked(this, e);

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void ordenarDespuesEjecucion_PorNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;
                opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }
        private void opcionTipoOrdenamientoAntesEjecucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)((ComboBox)sender).SelectedItem).Uid);
                }
            }
        }
        private void opcionTipoOrdenamientoDespuesEjecucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null &&
                ((ComboBox)sender).SelectedItem != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)((ComboBox)sender).SelectedItem).Uid);

                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                    }
                }
            }
        }
        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                {
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;                    
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }
        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
                {
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;                    
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionTextosInformacionCondicionesOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacionCondiciones_OperandosResultados = (bool)opcionTextosInformacionCondicionesOperandosResultados.IsChecked;
            }

            opcionesCondicionesTextosInformacionOperandosResultados.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionCondicionesOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacionCondiciones_OperandosResultados = (bool)opcionTextosInformacionCondicionesOperandosResultados.IsChecked;
            }

            opcionesCondicionesTextosInformacionOperandosResultados.Visibility = Visibility.Collapsed;
        }

        private void opcionAlgunosOperandosTextosInformacionOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AlgunosOperandosTextosInformacionOperandosResultados = (bool)opcionAlgunosOperandosTextosInformacionOperandosResultados.IsChecked;
            }

            listaOperandosTextosInformacionOperandosResultados.Visibility = Visibility.Visible;
        }

        private void opcionAlgunosOperandosTextosInformacionOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AlgunosOperandosTextosInformacionOperandosResultados = (bool)opcionAlgunosOperandosTextosInformacionOperandosResultados.IsChecked;
            }

            listaOperandosTextosInformacionOperandosResultados.Visibility = Visibility.Collapsed;
        }

        private void opcionNingunOperandoTextosInformacionOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.NingunOperandoTextosInformacionOperandosResultados = (bool)opcionNingunOperandoTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void opcionNingunOperandoTextosInformacionOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.NingunOperandoTextosInformacionOperandosResultados = (bool)opcionNingunOperandoTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void ProcesamientoCantidades(object sender, RoutedEventArgs e)
        {
            menuProcesamiento.IsOpen = false;

            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Salida |
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota) return;

                DiseñoOperacion ElementoDiseñoOperacionSeleccionado = null;
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.AgrupadorOperaciones &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SeleccionarEntradas &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.CondicionesFlujo &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SeleccionarOrdenar)
                    {
                        ElementoDiseñoOperacionSeleccionado = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;

                        if (ElementoDiseñoOperacionSeleccionado != null)
                        {
                            OpcionesProcesamientoCantidades definir = new OpcionesProcesamientoCantidades();
                            definir.Operandos = new List<DiseñoOperacion>() { ElementoDiseñoOperacionSeleccionado };
                            definir.Operandos.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                            definir.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(definir.Operandos).ToList();
                            definir.OperandosElemento.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                            definir.ProcesamientoCantidades = ElementoDiseñoOperacionSeleccionado.CopiarProcesamientoCantidades();
                            definir.NoConservarCambiosOperandos_ProcesamientoCantidades = ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoCantidades;
                            definir.ProcesarCantidades_AntesImplicaciones = ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_AntesImplicaciones;
                            definir.ProcesarCantidades_DespuesImplicaciones = ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_DespuesImplicaciones;
                            definir.EjecutarLogicasCantidades_AntesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_AntesEjecucion;
                            definir.EjecutarLogicasCantidades_DespuesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_DespuesEjecucion;
                            
                            if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado |
                    ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                definir.OpcionesOperandoNumeros = true;

                            if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo |                               
                               ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                               ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.ContarCantidades)
                                definir.OpcionesInsertar = false;

                            //if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                            //    ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                definir.OpcionesInsertar_OperandosCantidades = true;

                            if ((ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                                ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas) &&
                                ElementoDiseñoOperacionSeleccionado.ConAcumulacion)
                                definir.MostrarReiniciarAcumulacion = true;

                            bool opcionElegida = (bool)definir.ShowDialog();
                            if (opcionElegida)
                            {
                                ElementoDiseñoOperacionSeleccionado.ProcesamientoCantidades = CopiarProcesamientoCantidades(definir.ProcesamientoCantidades);
                                ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoCantidades = definir.NoConservarCambiosOperandos_ProcesamientoCantidades;
                                ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_AntesEjecucion = definir.EjecutarLogicasCantidades_AntesEjecucion;
                                ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_DespuesEjecucion = definir.EjecutarLogicasCantidades_DespuesEjecucion;
                                ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_AntesImplicaciones = definir.ProcesarCantidades_AntesImplicaciones;
                                ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_DespuesImplicaciones = definir.ProcesarCantidades_DespuesImplicaciones;
                            }
                        }
                    }
                    else
                        MessageBox.Show("La variable o vector no es válida.", "Lógica de variable o vector", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                foreach (var item in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
                {
                    if (item.Tipo == TipoElementoOperacion.Salida |
                        item.Tipo == TipoElementoOperacion.Nota) continue;

                    DiseñoOperacion ElementoDiseñoOperacionSeleccionado = null;
                    if (item.Tipo != TipoElementoOperacion.Ninguna)
                    {
                        if (item.Tipo != TipoElementoOperacion.Entrada &
                            item.Tipo != TipoElementoOperacion.Linea &
                            item.Tipo != TipoElementoOperacion.AgrupadorOperaciones &
                            item.Tipo != TipoElementoOperacion.SeleccionarEntradas &
                            item.Tipo != TipoElementoOperacion.CondicionesFlujo &
                            item.Tipo != TipoElementoOperacion.SeleccionarOrdenar)
                        {
                            ElementoDiseñoOperacionSeleccionado = item;

                            if (ElementoDiseñoOperacionSeleccionado != null)
                            {
                                OpcionesProcesamientoCantidades definir = new OpcionesProcesamientoCantidades();
                                definir.Operandos = new List<DiseñoOperacion>() { ElementoDiseñoOperacionSeleccionado };
                                definir.Operandos.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                                definir.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(definir.Operandos).ToList();
                                definir.OperandosElemento.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                                definir.ProcesamientoCantidades = ElementoDiseñoOperacionSeleccionado.CopiarProcesamientoCantidades();
                                definir.NoConservarCambiosOperandos_ProcesamientoCantidades = ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoCantidades;
                                definir.ProcesarCantidades_AntesImplicaciones = ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_AntesImplicaciones;
                                definir.ProcesarCantidades_DespuesImplicaciones = ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_DespuesImplicaciones;
                                definir.EjecutarLogicasCantidades_AntesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_AntesEjecucion;
                                definir.EjecutarLogicasCantidades_DespuesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_DespuesEjecucion;
                                
                                if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado |
                    ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                    definir.OpcionesOperandoNumeros = true;

                                if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo |                               
                               ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                               ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.ContarCantidades)
                                definir.OpcionesInsertar = false;

                                //if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                    definir.OpcionesInsertar_OperandosCantidades = true;

                                if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas &&
                                ElementoDiseñoOperacionSeleccionado.ConAcumulacion)
                                    definir.MostrarReiniciarAcumulacion = true;

                                bool opcionElegida = (bool)definir.ShowDialog();
                                if (opcionElegida)
                                {
                                    ElementoDiseñoOperacionSeleccionado.ProcesamientoCantidades = CopiarProcesamientoCantidades(definir.ProcesamientoCantidades);
                                    ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoCantidades = definir.NoConservarCambiosOperandos_ProcesamientoCantidades;
                                    ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_AntesEjecucion = definir.EjecutarLogicasCantidades_AntesEjecucion;
                                    ElementoDiseñoOperacionSeleccionado.EjecutarLogicasCantidades_DespuesEjecucion = definir.EjecutarLogicasCantidades_DespuesEjecucion;//ElementoDiseñoOperacionSeleccionado.OperandosInsertar_CantidadesProcesamientoCantidades.Clear();
                                    ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_AntesImplicaciones = definir.ProcesarCantidades_AntesImplicaciones;
                                    ElementoDiseñoOperacionSeleccionado.ProcesarCantidades_DespuesImplicaciones = definir.ProcesarCantidades_DespuesImplicaciones;
                                }

                            }
                        }
                        //else
                        //    MessageBox.Show("El elemento no es una operación válida para diseñar una operación compleja.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        private void ProcesamientoTextosInformacion(object sender, RoutedEventArgs e)
        {
            menuProcesamiento.IsOpen = false;

            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Salida |
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado == TipoElementoOperacion.Nota) return;

                DiseñoOperacion ElementoDiseñoOperacionSeleccionado = null;
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Ninguna)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.AgrupadorOperaciones &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SeleccionarEntradas &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.CondicionesFlujo &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.SeleccionarOrdenar)
                    {
                        ElementoDiseñoOperacionSeleccionado = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;

                        if (ElementoDiseñoOperacionSeleccionado != null)
                        {
                            OpcionesProcesamientoTextosInformacion definir = new OpcionesProcesamientoTextosInformacion();
                            definir.Operandos = new List<DiseñoOperacion>() { ElementoDiseñoOperacionSeleccionado };
                            definir.Operandos.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                            definir.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(definir.Operandos).ToList();
                            definir.OperandosElemento.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                            definir.ProcesamientoTextosInformacion = ElementoDiseñoOperacionSeleccionado.CopiarProcesamientoTextosInformacion();
                            definir.NoConservarCambiosOperandos_ProcesamientoTextosInformacion = ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
                            definir.AplicarProcesamientoAntesImplicacionesTextosInformacion = ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoAntesImplicacionesTextosInformacion;
                            definir.AplicarProcesamientoDespuesImplicacionesTextosInformacion = ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion;
                            definir.EjecutarLogicasTextos_AntesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_AntesEjecucion;
                            definir.EjecutarLogicasTextos_DespuesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_DespuesEjecucion;
                            //        if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado |
                            //ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                            //            definir.OpcionesOperandoNumeros = true;

                            //        if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo |
                            //           ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                            //           ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.ContarCantidades)
                            //            definir.OpcionesInsertar = false;

                            //        if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                            //            definir.OpcionesInsertar_OperandosCantidades = true;
                            if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas &&
                                ElementoDiseñoOperacionSeleccionado.ConAcumulacion)
                                definir.MostrarReiniciarAcumulacion = true;

                            bool opcionElegida = (bool)definir.ShowDialog();
                            if (opcionElegida)
                            {
                                ElementoDiseñoOperacionSeleccionado.ProcesamientoTextosInformacion = CopiarProcesamientoTextosInformacion(definir.ProcesamientoTextosInformacion);
                                ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoTextosInformacion = definir.NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
                                ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoAntesImplicacionesTextosInformacion = definir.AplicarProcesamientoAntesImplicacionesTextosInformacion;
                                ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion = definir.AplicarProcesamientoDespuesImplicacionesTextosInformacion;
                                ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_AntesEjecucion = definir.EjecutarLogicasTextos_AntesEjecucion;
                                ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_DespuesEjecucion = definir.EjecutarLogicasTextos_DespuesEjecucion;
                            }
                        }
                    }
                    else
                        MessageBox.Show("La variable o vector no es válida.", "Lógica de variable o vector", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                foreach (var item in CalculoDiseñoSeleccionado.Seleccion.ElementosSeleccionados)
                {
                    if (item.Tipo == TipoElementoOperacion.Salida |
                        item.Tipo == TipoElementoOperacion.Nota) continue;

                    DiseñoOperacion ElementoDiseñoOperacionSeleccionado = null;
                    if (item.Tipo != TipoElementoOperacion.Ninguna)
                    {
                        if (item.Tipo != TipoElementoOperacion.Entrada &
                            item.Tipo != TipoElementoOperacion.Linea &
                            item.Tipo != TipoElementoOperacion.AgrupadorOperaciones &
                            item.Tipo != TipoElementoOperacion.SeleccionarEntradas &
                            item.Tipo != TipoElementoOperacion.CondicionesFlujo &
                            item.Tipo != TipoElementoOperacion.SeleccionarOrdenar)
                        {
                            ElementoDiseñoOperacionSeleccionado = item;

                            if (ElementoDiseñoOperacionSeleccionado != null)
                            {
                                OpcionesProcesamientoTextosInformacion definir = new OpcionesProcesamientoTextosInformacion();
                                definir.Operandos = new List<DiseñoOperacion>() { ElementoDiseñoOperacionSeleccionado };
                                definir.Operandos.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                                definir.ListaElementos = CalculoDiseñoSeleccionado.ElementosOperaciones.Except(definir.Operandos).ToList();
                                definir.OperandosElemento.AddRange(ElementoDiseñoOperacionSeleccionado.ElementosAnteriores);
                                definir.ProcesamientoTextosInformacion = ElementoDiseñoOperacionSeleccionado.CopiarProcesamientoTextosInformacion();
                                definir.NoConservarCambiosOperandos_ProcesamientoTextosInformacion = ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
                                definir.AplicarProcesamientoAntesImplicacionesTextosInformacion = ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoAntesImplicacionesTextosInformacion;
                                definir.AplicarProcesamientoDespuesImplicacionesTextosInformacion = ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion;
                                definir.EjecutarLogicasTextos_AntesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_AntesEjecucion;
                                definir.EjecutarLogicasTextos_DespuesEjecucion = ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_DespuesEjecucion;

                                //            if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparado |
                                //ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                                //                definir.OpcionesOperandoNumeros = true;

                                //            if (ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.CondicionesFlujo |
                                //           ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.SeleccionarOrdenar |
                                //           ElementoDiseñoOperacionSeleccionado.Tipo == TipoElementoOperacion.ContarCantidades)
                                //                definir.OpcionesInsertar = false;

                                //            if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas)
                                //                definir.OpcionesInsertar_OperandosCantidades = true;
                                if (ElementoDiseñoOperacionSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas &&
                                ElementoDiseñoOperacionSeleccionado.ConAcumulacion)
                                    definir.MostrarReiniciarAcumulacion = true;

                                bool opcionElegida = (bool)definir.ShowDialog();
                                if (opcionElegida)
                                {
                                    ElementoDiseñoOperacionSeleccionado.ProcesamientoTextosInformacion = CopiarProcesamientoTextosInformacion(definir.ProcesamientoTextosInformacion);
                                    ElementoDiseñoOperacionSeleccionado.NoConservarCambiosOperandos_ProcesamientoTextosInformacion = definir.NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
                                    ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoAntesImplicacionesTextosInformacion = definir.AplicarProcesamientoAntesImplicacionesTextosInformacion;
                                    ElementoDiseñoOperacionSeleccionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion = definir.AplicarProcesamientoDespuesImplicacionesTextosInformacion;
                                    ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_AntesEjecucion = definir.EjecutarLogicasTextos_AntesEjecucion;
                                    ElementoDiseñoOperacionSeleccionado.EjecutarLogicasTextos_DespuesEjecucion = definir.EjecutarLogicasTextos_DespuesEjecucion;
                                }

                            }
                        }
                        //else
                        //    MessageBox.Show("El elemento no es una operación válida para diseñar una operación compleja.", "Diseño de operación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
        }

        public List<CondicionProcesamientoCantidades> CopiarProcesamientoCantidades(List<CondicionProcesamientoCantidades> lista)
        {
            List<CondicionProcesamientoCantidades> procesamiento = new List<CondicionProcesamientoCantidades>();

            foreach (var item in lista)
                procesamiento.Add(item.ReplicarObjeto());

            return procesamiento;
        }

        public List<CondicionProcesamientoTextosInformacion> CopiarProcesamientoTextosInformacion(List<CondicionProcesamientoTextosInformacion> lista)
        {
            List<CondicionProcesamientoTextosInformacion> procesamiento = new List<CondicionProcesamientoTextosInformacion>();

            foreach (var item in lista)
                procesamiento.Add(item.ReplicarObjeto());

            return procesamiento;
        }

        private void opcionPorcentajeRelativo_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.PorcentajeRelativo = (bool)opcionPorcentajeRelativo.IsChecked;
            }
        }

        private void opcionPorcentajeRelativo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.PorcentajeRelativo = (bool)opcionPorcentajeRelativo.IsChecked;
            }
        }

        private void opcionesElementosFijosPotencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    if (opcionesElementosFijosPotencia.SelectedItem != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosPotencia = (TipoOpcionElementosFijosOperacionPotencia)int.Parse(((ComboBoxItem)opcionesElementosFijosPotencia.SelectedItem).Uid);
                }

                if (opcionesElementosFijosPotencia.SelectedIndex == 1)
                {
                    textoOpcionElementoFijo.Text = "Base:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else if (opcionesElementosFijosPotencia.SelectedIndex == 2)
                {
                    textoOpcionElementoFijo.Text = "Exponente:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionElementoFijo.Visibility = Visibility.Collapsed;
                    valorOpcionElementoFijo.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionesElementosFijosRaiz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    if (opcionesElementosFijosRaiz.SelectedItem != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosRaiz = (TipoOpcionElementosFijosOperacionRaiz)int.Parse(((ComboBoxItem)opcionesElementosFijosRaiz.SelectedItem).Uid);
                }

                if (opcionesElementosFijosRaiz.SelectedIndex == 1)
                {
                    textoOpcionElementoFijo.Text = "Raíz:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else if (opcionesElementosFijosRaiz.SelectedIndex == 2)
                {
                    textoOpcionElementoFijo.Text = "Radical:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionElementoFijo.Visibility = Visibility.Collapsed;
                    valorOpcionElementoFijo.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void opcionesElementosFijosLogaritmo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    if (opcionesElementosFijosLogaritmo.SelectedItem != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosLogaritmo = (TipoOpcionElementosFijosOperacionLogaritmo)int.Parse(((ComboBoxItem)opcionesElementosFijosLogaritmo.SelectedItem).Uid);
                }

                if (opcionesElementosFijosLogaritmo.SelectedIndex == 1)
                {
                    textoOpcionElementoFijo.Text = "Base:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else if (opcionesElementosFijosLogaritmo.SelectedIndex == 2)
                {
                    textoOpcionElementoFijo.Text = "Exponente:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionElementoFijo.Visibility = Visibility.Collapsed;
                    valorOpcionElementoFijo.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionesElementosFijosInverso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    if (opcionesElementosFijosInverso.SelectedItem != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OpcionElementosFijosInverso = (TipoOpcionElementosFijosOperacionInverso)int.Parse(((ComboBoxItem)opcionesElementosFijosInverso.SelectedItem).Uid);
                }

                textoOpcionElementoFijo.Visibility = Visibility.Collapsed;
                valorOpcionElementoFijo.Visibility = Visibility.Collapsed;                
            }
        }

        private void valorOpcionElementoFijo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                double numero = 0;
                double.TryParse(valorOpcionElementoFijo.Text, out numero);
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ValorOpcionElementosFijos = numero;
            }
        }

        private void opcionesDividirOrdenacionTextosInformacion_AntesEjecucion_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                DefinirOrdenacionesTextosInformacion definir = new DefinirOrdenacionesTextosInformacion();
                definir.Ordenaciones = CopiarOrdenaciones(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenaciones);
                definir.RevertirListaTextos = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.RevertirListaTextos;

                bool definicion = (bool)definir.ShowDialog();
                if((bool)definicion == true)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenaciones = definir.Ordenaciones.ToList();
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.RevertirListaTextos = definir.RevertirListaTextos;
                }
            }
        }

        private List<OrdenacionNumeros> CopiarOrdenaciones(List<OrdenacionNumeros> lista)
        {
            List<OrdenacionNumeros> resultado = new List<OrdenacionNumeros>();
            foreach(var item in lista)
            {
                resultado.Add(item.CopiarObjeto());
            }

            return resultado;
        }

        private void opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                DefinirOrdenacionesTextosInformacion definir = new DefinirOrdenacionesTextosInformacion();
                definir.Ordenaciones = CopiarOrdenaciones(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenaciones);
                definir.RevertirListaTextos = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.RevertirListaTextos;

                bool definicion = (bool)definir.ShowDialog();
                if ((bool)definicion == true)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenaciones = definir.Ordenaciones.ToList();
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.RevertirListaTextos = definir.RevertirListaTextos;

                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                    }
                }
            }
        }

        private void opcionOperarFilasRestantes_ConCeros_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro = (bool)opcionOperarFilasRestantes_ConCeros.IsChecked;
                }
            }
        }

        private void opcionOperarFilasRestantes_ConCeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorFilas |
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.TipoOperacion_Ejecucion == TipoOperacionEjecucion.OperarPorSeparadoPorFilas)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro = (bool)opcionOperarFilasRestantes_ConCeros.IsChecked;
                }
            }
        }

        private void opcionIncluirCantidadNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.IncluirCantidadNumero = (bool)opcionIncluirCantidadNumero.IsChecked;
            }
        }

        private void opcionIncluirCantidadNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.IncluirCantidadNumero = (bool)opcionIncluirCantidadNumero.IsChecked;
            }
        }

        private void filaEntradas_GotFocus(object sender, RoutedEventArgs e)
        {
            SeleccionarCalculo(sender, e);
        }

        private void opciones_Loaded(object sender, RoutedEventArgs e)
        {
            Ventana.MostrarOcultarBotonOverFlow_BarraOpciones((ToolBar)sender);
        }

        private void divisionZeroContinuar_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DivisionZero_Continuar = (bool)divisionZeroContinuar.IsChecked;
            }
        }

        private void divisionZeroContinuar_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DivisionZero_Continuar = (bool)divisionZeroContinuar.IsChecked;
            }
        }

        private void btnDefinirAsignacionImplicacionTextos_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Entrada &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Linea &
                        CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Salida &
                    CalculoDiseñoSeleccionado.Seleccion.TipoElementoOperacionSeleccionado != TipoElementoOperacion.Nota)
                {
                    Ventana.Elemento_AgregarImplicacionTextos = CalculoDiseñoSeleccionado.Seleccion.OperacionSeleccionada.DiseñoOperacion;
                    Ventana.Calculo_AgregarImplicacionTextos = CalculoDiseñoSeleccionado;
                    
                    Ventana.btnTextosInformacion_Click(this, null);
                    
                    Ventana.Elemento_AgregarImplicacionTextos = null;
                    Ventana.Calculo_AgregarImplicacionTextos = null;
                }
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
        }

        private void diagrama_LayoutUpdated(object sender, EventArgs e)
        {
            //if(ModoAgrupador)
            //{
            //    DibujarTodasLineasElementos();
            //}
        }

        private void mostrarOcultarInfoElementos_Click(object sender, RoutedEventArgs e)
        {
            if(ModoIconosResumidos)
            {
                ModoIconosResumidos = false;                
            }
            else
            {
                ModoIconosResumidos = true;
            }

            DibujarElementosOperaciones();
        }

        private void btnDefinirProcesamientoCantidades_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
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

                if(ModoAgrupador)
                {
                    Agrupador.AltoDiagrama = diagrama.Height;
                    Agrupador.AnchoDiagrama = diagrama.Width;
                }
                else
                {
                    CalculoDiseñoSeleccionado.Alto = diagrama.Height;
                    CalculoDiseñoSeleccionado.Ancho = diagrama.Width;
                }
                
            }
        }

        private void opcionAsignarTextosInformacionAntes_Implicaciones_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_AntesImplicaciones = (bool)opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionAntes_Implicaciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_AntesImplicaciones = (bool)opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked;

                    if (opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked == false &&
                        !ClicElemento)
                        opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked = true;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_Implicaciones_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_DespuesImplicaciones = (bool)opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_Implicaciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_DespuesImplicaciones = (bool)opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked;
                    if (opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked == false &&
                        !ClicElemento)
                        opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked = true;
                }
            }
        }

        private void opcionQuitarTextosInformacion_Repetidos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.QuitarTextosInformacion_Repetidos = (bool)opcionQuitarTextosInformacion_Repetidos.IsChecked;                    
                }
            }
        }

        private void opcionQuitarTextosInformacion_Repetidos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.QuitarTextosInformacion_Repetidos = (bool)opcionQuitarTextosInformacion_Repetidos.IsChecked;
                }
            }
        }

        private void opcionLimpiarDatos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.LimpiarDatosResultados = (bool)opcionLimpiarDatos.IsChecked;

                    botonOpcionLimpiarDatos.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionLimpiarDatos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.LimpiarDatosResultados = (bool)opcionLimpiarDatos.IsChecked;

                    botonOpcionLimpiarDatos.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void botonOpcionLimpiarDatos_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    DefinirOperacion_LimpiarDatos definir = new DefinirOperacion_LimpiarDatos();
                    definir.config = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.CopiarObjeto();
                    definir.ModoComportamiento = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarDuplicados = definir.config.QuitarDuplicados;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesDuplicadas = definir.config.QuitarCantidadesDuplicadas;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.Conector1_Duplicados = definir.config.Conector1_Duplicados;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesTextosDuplicadas = definir.config.QuitarCantidadesTextosDuplicadas;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.Conector2_Duplicados = definir.config.Conector2_Duplicados;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesTextosDentroDuplicados = definir.config.QuitarCantidadesTextosDentroDuplicados;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCeros = definir.config.QuitarCeros;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCerosConTextos = definir.config.QuitarCerosConTextos;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.Conector1_Ceros = definir.config.Conector1_Ceros;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCerosSinTextos = definir.config.QuitarCerosSinTextos;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesSinTextos = definir.config.QuitarCantidadesSinTextos;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarNegativas = definir.config.QuitarNegativas;
                        
                    }
                }
            }
        }

        private void opcionRedondearCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.RedondearCantidadesResultados = (bool)opcionRedondearCantidades.IsChecked;

                    botonOpcionRedondearCantidades.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionRedondearCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.RedondearCantidadesResultados = (bool)opcionRedondearCantidades.IsChecked;

                    botonOpcionRedondearCantidades.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void botonOpcionRedondearCantidades_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    DefinirOperacion_RedondearCantidades definir = new DefinirOperacion_RedondearCantidades();
                    definir.config = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigRedondeoResultados.CopiarObjeto();
                    definir.ModoComportamiento = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigRedondeoResultados.RedondearPar_Cercano = definir.config.RedondearPar_Cercano;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_LejanoDeCero = definir.config.RedondearNumero_LejanoDeCero;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_CercanoDeCero = definir.config.RedondearNumero_CercanoDeCero;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_Mayor = definir.config.RedondearNumero_Mayor;
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_Menor = definir.config.RedondearNumero_Menor;

                    }
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                {
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                {
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                }

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;

                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                }
            }
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

        private void definirAgrupacionesOperandosResultados_Click(object sender, RoutedEventArgs e)
        {
            if(CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                DefinirAgrupacionesResultados definirAgrupaciones = new DefinirAgrupacionesResultados();
                definirAgrupaciones.ElementoAsociado = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado;
                definirAgrupaciones.OperandosResultados = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosPosteriores;
                definirAgrupaciones.Agrupaciones = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ObtenerAgrupaciones();
                definirAgrupaciones.ShowDialog();
            }
        }

        private void opcionSeparadorMilesPunto_SeparadorDecimalesNinguno_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (opcionSeparadorMilesPunto_SeparadorDecimalesNinguno.IsChecked == true)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesNinguno;
                    else
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesPunto_SeparadorDecimalesNinguno;
                }
            }
        }

        private void opcionSeparadorMilesComa_SeparadorDecimalesNinguno_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                if (opcionSeparadorMilesComa_SeparadorDecimalesNinguno.IsChecked == true)
                {
                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesNinguno;
                    else
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConfiguracionSeparadores = TipoDefinicionSeparadores.SeparadorMilesComa_SeparadorDecimalesNinguno;
                }
            }
        }

        private void definirNombresResultados_Click(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.opcionesDefiniciones.Visibility = Visibility.Collapsed;
                establecer.Operandos = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ElementosAnteriores;
                establecer.TextosNombre = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DefinicionOpcionesNombresResultados.ReplicarObjeto();

                if (establecer.ShowDialog() == true)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.DefinicionOpcionesNombresResultados = establecer.TextosNombre;
                    descripcionDefiniciones_Resultados.Text = ObtenerDescripcionDefiniciones_Resultados();

                    if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.Tipo == TipoElementoOperacion.Entrada &&
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada != null)
                    {
                        CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.EntradaRelacionada.ConCambios_ToolTips = true;
                    }
                }
            }
        }

        private void definirOperacionesCadenasTexto_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado &&
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                ConfigurarOperaciones_Elemento config = new ConfigurarOperaciones_Elemento();
                config.OperacionesCadenasTexto = CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigOperaciones_CadenasTexto.Operaciones.Select(i => i.ReplicarObjeto()).ToList();
                bool resp = (bool)config.ShowDialog();

                if (resp)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.ConfigOperaciones_CadenasTexto.Operaciones = config.OperacionesCadenasTexto;
                }
            }
        }

        private void opcionAsignarTextosInformacionAntes_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionAntes_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }

                if (opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked == false &&
                        !ClicElemento)
                    opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked = true;
            }
        }

        private void opcionAsignarTextosInformacionDespues_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_DespuesEjecucion= (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
                {
                    CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AsignarTextosInformacion_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }

                if (opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked == false &&
                        !ClicElemento)
                    opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked = true;
            }
        }

        private void opcionAgregarNombreTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarNombreComoTextoInformacion = (bool)opcionAgregarNombreTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarNombreTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.AgregarNombreComoTextoInformacion = (bool)opcionAgregarNombreTextoInformacion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.QuitarClasificadores_AntesEjecucion = (bool)opcionQuitarClasificadores_AntesEjecucion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.QuitarClasificadores_AntesEjecucion = (bool)opcionQuitarClasificadores_AntesEjecucion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.QuitarClasificadores_DespuesEjecucion = (bool)opcionQuitarClasificadores_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.QuitarClasificadores_DespuesEjecucion = (bool)opcionQuitarClasificadores_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionAntesEjecucion_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;

                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadores_AntesEjecucion = (bool)opcionAntesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionAntesEjecucion_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;

                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadores_AntesEjecucion = (bool)opcionAntesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionDespuesEjecucion_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;

                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadores_DespuesEjecucion = (bool)opcionDespuesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionDespuesEjecucion_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;

                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadores_DespuesEjecucion = (bool)opcionDespuesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado != null)
            {
                CalculoDiseñoSeleccionado.Seleccion.ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }
    }
    public class MaxHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentException("El valor no es válido", "values");

            return ((double)value / 2.5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MaxWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentException("El valor no es válido", "values");

            return ((double)value / 3);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MinHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentException("El valor no es válido", "values");

            return ((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
