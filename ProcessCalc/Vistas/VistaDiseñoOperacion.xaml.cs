using Petzold.Media2D;
using ProcessCalc.Controles;
using ProcessCalc.Controles.Calculos;
using ProcessCalc.Controles.Notas;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Archivos;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Operaciones;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
using ProcessCalc.Ventanas.Configuraciones;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace ProcessCalc.Vistas
{
    /// <summary>
    /// Lógica de interacción para VistaDiseñoOperacion.xaml
    /// </summary>
    public partial class VistaDiseñoOperacion : UserControl
    {
        
        public Calculo Calculo { get; set; }
        public DiseñoOperacion Operacion { get; set; }
        public bool ElementoSeleccionado_Bool;
        public TipoElementoDiseñoOperacion TipoElementoDiseñoOperacionSeleccionado { get; set; }
        public EntradaFlujoOperacion FlujoOperacionSeleccionado { get; set; }
        public OpcionOperacion OpcionOperacionSeleccionado { get; set; }
        public OpcionSalida OpcionSalidaSeleccionado { get; set; }
        private ArrowLine lineaSeleccionada;
        public EntradaDiseñoOperaciones EntradaSeleccionada;
        private DiseñoElementoOperacion ElementoAnteriorLineaSeleccionada;
        private DiseñoElementoOperacion ElementoPosteriorLineaSeleccionada;
        public DiseñoElementoOperacion ElementoSeleccionado { get; set; }
        public bool MostrandoOrdenOperandos;
        private bool BuscandoOpcionesOperacion;
        Point ubicacionInicialAreaSeleccionada;
        Point ubicacionFinalAreaSeleccionada;
        public Rectangle rectanguloSeleccion;
        public bool ClicDiagrama;
        //bool elementoSeleccionado;
        List<DiseñoElementoOperacion> ElementosSeleccionados = new List<DiseñoElementoOperacion>();
        public MainWindow Ventana { get; set; }
        bool MostrandoInformacionElemento;
        List<UIElement> ElementosEncontrados = new List<UIElement>();
        int indiceElementosEncontrados = -1;
        double escalaZoom = 1;
        bool ClicElemento = false;
        public MouseButtonEventArgs e_SeleccionarElemento { get; set; }
        public NotaDiagrama NotaSeleccionada { get; set; }
        public Point ubicacionActualElemento { get; set; }
        public UIElement ElementoDiagramaSeleccionadoMover;
        public List<UIElement> ElementosDiagramaSeleccionados = new List<UIElement>();
        bool lineaSeleccionada_ = false;
        public DiseñoCalculo CalculoSeleccionado { get; set; }
        public VistaDiseñoOperacion()
        {
            InitializeComponent();
        }

        private void EstablecerCoordenadasElementoMover(UIElement elemento, DiseñoElementoOperacion elementoOperacion, DragEventArgs e)
        {
            Point puntoElemento;

            if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                puntoElemento = ((EntradaDiseñoOperaciones)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(EntradaFlujoOperacion))
                puntoElemento = ((EntradaFlujoOperacion)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(OpcionOperacion))
                puntoElemento = ((OpcionOperacion)elemento).PuntoMouseClic;
            else if (elemento.GetType() == typeof(OpcionSalida))
                puntoElemento = ((OpcionSalida)elemento).PuntoMouseClic;
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


            ubicacionActualElemento = new Point(elementoOperacion.PosicionX + ubicacionActualElemento.X - puntoElemento.X,
           elementoOperacion.PosicionY + ubicacionActualElemento.Y - puntoElemento.Y);

        }

        public void ListarOpcionesOperacion()
        {
            string textoBusqueda = null;

            if (BuscandoOpcionesOperacion &&
                !string.IsNullOrEmpty(busquedaOperaciones.Text))
            {
                textoBusqueda = busquedaOperaciones.Text;
            }

            opciones.Children.Clear();

            string nombreOperacion = string.Empty;
            string nombreOperacion_SeleccionarOrdenar = "Lógica de selección de números";
            string nombreOperacion_CondicionFlujo = "Lógica de selección de variables o vectores";
            string nombreOperacion_NumerosObtenidos = "Números obtenidos";
            string nombreOperacion_Inverso = "Inverso de los números";
            string nombreOperacion_ContarCantidades = "Contar números";
            string nombreOperacion_Factorial = "Factorial de los números";
            string nombreOperacion_Espera = "Esperar datos";
            string nombreOperacion_LimpiarDatos = "Limpiar datos";
            string nombreOperacion_RedondearCantidades = "Redondear números";
            string nombreOperacion_ArchivoExterno = "Subcálculo desde archivo";
            string nombreOperacion_SubCalculo = "Cálculo";

            switch (Operacion.Tipo)
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
                case TipoElementoOperacion.Nota:
                    nombreOperacion = "Nota";
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
                    nombreOperacion = "Inverso de los números";
                    break;
                case TipoElementoOperacion.Factorial:
                    nombreOperacion = "Factorial de los números";
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
                case TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar:
                    nombreOperacion = "Números obtenidos";
                    break;
                case TipoElementoOperacion.Espera:
                    nombreOperacion = "Esperar datos";
                    break;
                case TipoElementoOperacion.LimpiarDatos:
                    nombreOperacion = "Limpiar datos";
                    break;
                case TipoElementoOperacion.RedondearCantidades:
                    nombreOperacion = "Redondear números";
                    break;
                case TipoElementoOperacion.ArchivoExterno:
                    nombreOperacion = "Cálculo desde archivo";
                    break;
                case TipoElementoOperacion.SubCalculo:
                    nombreOperacion = "Cálculo";
                    break;
            }

            if (Operacion.Tipo == TipoElementoOperacion.Potencia)
            {
                OpcionOperacion calcularPotencia_UnaVez = new OpcionOperacion();
                calcularPotencia_UnaVez.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularPotencia_UnaVez.VistaOpciones = this;
                calcularPotencia_UnaVez.NombreOperacion = nombreOperacion;
                calcularPotencia_UnaVez.Tipo = TipoOpcionOperacion.CalculandoPotencias_UnaSolaVez;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularPotencia_UnaVez);
                else
                {
                    if (calcularPotencia_UnaVez.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularPotencia_UnaVez.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularPotencia_UnaVez);
                }

                //OpcionOperacion recalcularPotencia_agrupando = new OpcionOperacion();
                //recalcularPotencia_agrupando.nombreOpcion.Visibility = Visibility.Collapsed;
                //recalcularPotencia_agrupando.VistaOpciones = this;
                //recalcularPotencia_agrupando.NombreOperacion = nombreOperacion.Replace("C", "Rec");
                //recalcularPotencia_agrupando.Tipo = TipoOpcionOperacion.RecalculandoPotencias_AgrupandoBasesExponentes;

                //if (textoBusqueda == null)
                //    opciones.Children.Add(recalcularPotencia_agrupando);
                //else
                //{
                //    if (recalcularPotencia_agrupando.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                //        recalcularPotencia_agrupando.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //        opciones.Children.Add(recalcularPotencia_agrupando);
                //}

                OpcionOperacion calcularPotencia_PorFila = new OpcionOperacion();
                calcularPotencia_PorFila.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularPotencia_PorFila.VistaOpciones = this;
                calcularPotencia_PorFila.NombreOperacion = nombreOperacion;
                calcularPotencia_PorFila.Tipo = TipoOpcionOperacion.CalculandoPotencias_PorFila;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularPotencia_PorFila);
                else
                {
                    if (calcularPotencia_PorFila.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularPotencia_PorFila.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularPotencia_PorFila);
                }
            }
            else if (Operacion.Tipo == TipoElementoOperacion.Raiz)
            {
                OpcionOperacion calcularRaiz_UnaVez = new OpcionOperacion();
                calcularRaiz_UnaVez.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularRaiz_UnaVez.VistaOpciones = this;
                calcularRaiz_UnaVez.NombreOperacion = nombreOperacion;
                calcularRaiz_UnaVez.Tipo = TipoOpcionOperacion.CalculandoRaices_UnaSolaVez;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularRaiz_UnaVez);
                else
                {
                    if (calcularRaiz_UnaVez.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularRaiz_UnaVez.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularRaiz_UnaVez);
                }

                //OpcionOperacion recalcularPotencia_agrupando = new OpcionOperacion();
                //recalcularPotencia_agrupando.nombreOpcion.Visibility = Visibility.Collapsed;
                //recalcularPotencia_agrupando.VistaOpciones = this;
                //recalcularPotencia_agrupando.NombreOperacion = nombreOperacion.Replace("C", "Rec");
                //recalcularPotencia_agrupando.Tipo = TipoOpcionOperacion.RecalculandoPotencias_AgrupandoBasesExponentes;

                //if (textoBusqueda == null)
                //    opciones.Children.Add(recalcularPotencia_agrupando);
                //else
                //{
                //    if (recalcularPotencia_agrupando.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                //        recalcularPotencia_agrupando.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //        opciones.Children.Add(recalcularPotencia_agrupando);
                //}

                OpcionOperacion calcularRaiz_PorFila = new OpcionOperacion();
                calcularRaiz_PorFila.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularRaiz_PorFila.VistaOpciones = this;
                calcularRaiz_PorFila.NombreOperacion = nombreOperacion;
                calcularRaiz_PorFila.Tipo = TipoOpcionOperacion.CalculandoRaices_PorFila;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularRaiz_PorFila);
                else
                {
                    if (calcularRaiz_PorFila.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularRaiz_PorFila.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularRaiz_PorFila);
                }
            }
            else if (Operacion.Tipo == TipoElementoOperacion.Porcentaje)
            {
                OpcionOperacion calcularPorcentaje_UnaVez = new OpcionOperacion();
                calcularPorcentaje_UnaVez.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularPorcentaje_UnaVez.VistaOpciones = this;
                calcularPorcentaje_UnaVez.NombreOperacion = nombreOperacion;
                calcularPorcentaje_UnaVez.Tipo = TipoOpcionOperacion.CalculandoPorcentaje_UnaSolaVez;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularPorcentaje_UnaVez);
                else
                {
                    if (calcularPorcentaje_UnaVez.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularPorcentaje_UnaVez.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularPorcentaje_UnaVez);
                }

                //OpcionOperacion recalcularPotencia_agrupando = new OpcionOperacion();
                //recalcularPotencia_agrupando.nombreOpcion.Visibility = Visibility.Collapsed;
                //recalcularPotencia_agrupando.VistaOpciones = this;
                //recalcularPotencia_agrupando.NombreOperacion = nombreOperacion.Replace("C", "Rec");
                //recalcularPotencia_agrupando.Tipo = TipoOpcionOperacion.RecalculandoPotencias_AgrupandoBasesExponentes;

                //if (textoBusqueda == null)
                //    opciones.Children.Add(recalcularPotencia_agrupando);
                //else
                //{
                //    if (recalcularPotencia_agrupando.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                //        recalcularPotencia_agrupando.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //        opciones.Children.Add(recalcularPotencia_agrupando);
                //}

                OpcionOperacion calcularPorcentaje_PorFila = new OpcionOperacion();
                calcularPorcentaje_PorFila.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularPorcentaje_PorFila.VistaOpciones = this;
                calcularPorcentaje_PorFila.NombreOperacion = nombreOperacion;
                calcularPorcentaje_PorFila.Tipo = TipoOpcionOperacion.CalculandoPorcentaje_PorFila;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularPorcentaje_PorFila);
                else
                {
                    if (calcularPorcentaje_PorFila.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularPorcentaje_PorFila.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularPorcentaje_PorFila);
                }
            }
            else if (Operacion.Tipo == TipoElementoOperacion.Logaritmo)
            {
                OpcionOperacion calcularLogaritmo_UnaVez = new OpcionOperacion();
                calcularLogaritmo_UnaVez.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularLogaritmo_UnaVez.VistaOpciones = this;
                calcularLogaritmo_UnaVez.NombreOperacion = nombreOperacion;
                calcularLogaritmo_UnaVez.Tipo = TipoOpcionOperacion.CalculandoLogaritmo_UnaSolaVez;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularLogaritmo_UnaVez);
                else
                {
                    if (calcularLogaritmo_UnaVez.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularLogaritmo_UnaVez.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularLogaritmo_UnaVez);
                }

                //OpcionOperacion recalcularPotencia_agrupando = new OpcionOperacion();
                //recalcularPotencia_agrupando.nombreOpcion.Visibility = Visibility.Collapsed;
                //recalcularPotencia_agrupando.VistaOpciones = this;
                //recalcularPotencia_agrupando.NombreOperacion = nombreOperacion.Replace("C", "Rec");
                //recalcularPotencia_agrupando.Tipo = TipoOpcionOperacion.RecalculandoPotencias_AgrupandoBasesExponentes;

                //if (textoBusqueda == null)
                //    opciones.Children.Add(recalcularPotencia_agrupando);
                //else
                //{
                //    if (recalcularPotencia_agrupando.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                //        recalcularPotencia_agrupando.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                //        opciones.Children.Add(recalcularPotencia_agrupando);
                //}

                OpcionOperacion calcularLogaritmo_PorFila = new OpcionOperacion();
                calcularLogaritmo_PorFila.nombreOpcion.Visibility = Visibility.Collapsed;
                calcularLogaritmo_PorFila.VistaOpciones = this;
                calcularLogaritmo_PorFila.NombreOperacion = nombreOperacion;
                calcularLogaritmo_PorFila.Tipo = TipoOpcionOperacion.CalculandoLogaritmo_PorFila;

                if (textoBusqueda == null)
                    opciones.Children.Add(calcularLogaritmo_PorFila);
                else
                {
                    if (calcularLogaritmo_PorFila.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                        calcularLogaritmo_PorFila.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(calcularLogaritmo_PorFila);
                }
            }
            else if (Operacion.Tipo != TipoElementoOperacion.SeleccionarOrdenar &
                Operacion.Tipo != TipoElementoOperacion.CondicionesFlujo &
                Operacion.Tipo != TipoElementoOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &
                Operacion.Tipo != TipoElementoOperacion.Inverso &
                Operacion.Tipo != TipoElementoOperacion.Factorial &
                Operacion.Tipo != TipoElementoOperacion.ContarCantidades &
                Operacion.Tipo != TipoElementoOperacion.Espera &
                Operacion.Tipo != TipoElementoOperacion.LimpiarDatos &
                Operacion.Tipo != TipoElementoOperacion.RedondearCantidades &
                Operacion.Tipo != TipoElementoOperacion.ArchivoExterno &
                Operacion.Tipo != TipoElementoOperacion.SubCalculo)
            {
                OpcionOperacion todosSeparados = new OpcionOperacion();
                todosSeparados.nombreOpcion.Visibility = Visibility.Collapsed;
                todosSeparados.VistaOpciones = this;
                todosSeparados.NombreOperacion = nombreOperacion;
                todosSeparados.Tipo = TipoOpcionOperacion.TodosSeparados;

                if (textoBusqueda == null)
                    opciones.Children.Add(todosSeparados);
                else
                {
                    if (todosSeparados.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(todosSeparados);
                }

                OpcionOperacion todosJuntos = new OpcionOperacion();
                todosJuntos.nombreOpcion.Visibility = Visibility.Collapsed;
                todosJuntos.VistaOpciones = this;
                todosJuntos.NombreOperacion = nombreOperacion;
                todosJuntos.Tipo = TipoOpcionOperacion.TodosJuntos;

                if (textoBusqueda == null)
                    opciones.Children.Add(todosJuntos);
                else
                {
                    if (todosJuntos.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(todosJuntos);
                }

                OpcionOperacion porFila = new OpcionOperacion();
                porFila.nombreOpcion.Visibility = Visibility.Collapsed;
                porFila.VistaOpciones = this;
                porFila.NombreOperacion = nombreOperacion;
                porFila.Tipo = TipoOpcionOperacion.PorFila;

                if (textoBusqueda == null)
                    opciones.Children.Add(porFila);
                else
                {
                    if (porFila.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(porFila);
                }

                OpcionOperacion porFilaPorSeparados = new OpcionOperacion();
                porFilaPorSeparados.nombreOpcion.Visibility = Visibility.Collapsed;
                porFilaPorSeparados.VistaOpciones = this;
                porFilaPorSeparados.NombreOperacion = nombreOperacion;
                porFilaPorSeparados.Tipo = TipoOpcionOperacion.PorFilaPorSeparados;

                if (textoBusqueda == null)
                    opciones.Children.Add(porFilaPorSeparados);
                else
                {
                    if (porFila.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                        opciones.Children.Add(porFilaPorSeparados);
                }
            }

            OpcionOperacion numerosObtenidos = new OpcionOperacion();
            numerosObtenidos.nombreOpcion.Visibility = Visibility.Collapsed;
            numerosObtenidos.VistaOpciones = this;
            numerosObtenidos.NombreOperacion = nombreOperacion_NumerosObtenidos;
            numerosObtenidos.Tipo = TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

            if (textoBusqueda == null)
                opciones.Children.Add(numerosObtenidos);
            else
            {
                if (numerosObtenidos.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(numerosObtenidos);
            }

            OpcionOperacion calcularFactorial = new OpcionOperacion();
            calcularFactorial.nombreOpcion.Visibility = Visibility.Collapsed;
            calcularFactorial.VistaOpciones = this;
            calcularFactorial.NombreOperacion = nombreOperacion_Factorial;
            calcularFactorial.Tipo = TipoOpcionOperacion.CalculandoFactorial;

            if (textoBusqueda == null)
                opciones.Children.Add(calcularFactorial);
            else
            {
                if (calcularFactorial.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                    calcularFactorial.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(calcularFactorial);
            }

            OpcionOperacion calcularInverso = new OpcionOperacion();
            calcularInverso.nombreOpcion.Visibility = Visibility.Collapsed;
            calcularInverso.VistaOpciones = this;
            calcularInverso.NombreOperacion = nombreOperacion_Inverso;
            calcularInverso.Tipo = TipoOpcionOperacion.CalculandoInverso;

            if (textoBusqueda == null)
                opciones.Children.Add(calcularInverso);
            else
            {
                if (calcularInverso.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                    calcularInverso.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(calcularInverso);
            }

            OpcionOperacion contarCantidades_TodosJuntos = new OpcionOperacion();
            contarCantidades_TodosJuntos.nombreOpcion.Visibility = Visibility.Collapsed;
            contarCantidades_TodosJuntos.VistaOpciones = this;
            contarCantidades_TodosJuntos.NombreOperacion = nombreOperacion_ContarCantidades;
            contarCantidades_TodosJuntos.Tipo = TipoOpcionOperacion.ContandoCantidades_TodosJuntos;

            if (textoBusqueda == null)
                opciones.Children.Add(contarCantidades_TodosJuntos);
            else
            {
                if (contarCantidades_TodosJuntos.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                    contarCantidades_TodosJuntos.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(contarCantidades_TodosJuntos);
            }

            OpcionOperacion contarCantidades_Separados = new OpcionOperacion();
            contarCantidades_Separados.nombreOpcion.Visibility = Visibility.Collapsed;
            contarCantidades_Separados.VistaOpciones = this;
            contarCantidades_Separados.NombreOperacion = nombreOperacion_ContarCantidades;
            contarCantidades_Separados.Tipo = TipoOpcionOperacion.ContandoCantidades_Separados;

            if (textoBusqueda == null)
                opciones.Children.Add(contarCantidades_Separados);
            else
            {
                if (contarCantidades_Separados.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                    contarCantidades_Separados.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(contarCantidades_Separados);
            }

            OpcionOperacion redondearCantidades = new OpcionOperacion();
            redondearCantidades.nombreOpcion.Visibility = Visibility.Collapsed;
            redondearCantidades.VistaOpciones = this;
            redondearCantidades.NombreOperacion = nombreOperacion_RedondearCantidades;
            redondearCantidades.Tipo = TipoOpcionOperacion.RedondearCantidades;

            if (textoBusqueda == null)
                opciones.Children.Add(redondearCantidades);
            else
            {
                if (redondearCantidades.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(redondearCantidades);
            }

            OpcionOperacion todosSeparados_SeleccionarOrdenar = new OpcionOperacion();
            todosSeparados_SeleccionarOrdenar.nombreOpcion.Visibility = Visibility.Collapsed;
            todosSeparados_SeleccionarOrdenar.VistaOpciones = this;
            todosSeparados_SeleccionarOrdenar.NombreOperacion = nombreOperacion_SeleccionarOrdenar;
            todosSeparados_SeleccionarOrdenar.Tipo = TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados;

            if (textoBusqueda == null)
                opciones.Children.Add(todosSeparados_SeleccionarOrdenar);
            else
            {
                if (todosSeparados_SeleccionarOrdenar.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(todosSeparados_SeleccionarOrdenar);
            }

            OpcionOperacion todosJuntos_SeleccionarOrdenar = new OpcionOperacion();
            todosJuntos_SeleccionarOrdenar.nombreOpcion.Visibility = Visibility.Collapsed;
            todosJuntos_SeleccionarOrdenar.VistaOpciones = this;
            todosJuntos_SeleccionarOrdenar.NombreOperacion = nombreOperacion_SeleccionarOrdenar;
            todosJuntos_SeleccionarOrdenar.Tipo = TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos;

            if (textoBusqueda == null)
                opciones.Children.Add(todosJuntos_SeleccionarOrdenar);
            else
            {
                if (todosJuntos_SeleccionarOrdenar.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(todosJuntos_SeleccionarOrdenar);
            }

            OpcionOperacion soloUnir = new OpcionOperacion();
            soloUnir.nombreOpcion.Visibility = Visibility.Collapsed;
            soloUnir.VistaOpciones = this;
            soloUnir.NombreOperacion = nombreOperacion_SeleccionarOrdenar;
            soloUnir.Tipo = TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir;

            if (textoBusqueda == null)
                opciones.Children.Add(soloUnir);
            else
            {
                if (soloUnir.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(soloUnir);
            }

            OpcionOperacion condiciones = new OpcionOperacion();
            condiciones.nombreOpcion.Visibility = Visibility.Collapsed;
            condiciones.VistaOpciones = this;
            condiciones.NombreOperacion = nombreOperacion_CondicionFlujo;
            condiciones.Tipo = TipoOpcionOperacion.CondicionesFlujo;

            if (textoBusqueda == null)
                opciones.Children.Add(condiciones);
            else
            {
                if (condiciones.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(condiciones);
            }

            OpcionOperacion condiciones_PorSeparado = new OpcionOperacion();
            condiciones_PorSeparado.nombreOpcion.Visibility = Visibility.Collapsed;
            condiciones_PorSeparado.VistaOpciones = this;
            condiciones_PorSeparado.NombreOperacion = nombreOperacion_CondicionFlujo + " por separado";
            condiciones_PorSeparado.Tipo = TipoOpcionOperacion.CondicionesFlujo_PorSeparado;

            if (textoBusqueda == null)
                opciones.Children.Add(condiciones_PorSeparado);
            else
            {
                if (condiciones_PorSeparado.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(condiciones_PorSeparado);
            }

            OpcionOperacion subCalculo = new OpcionOperacion();
            subCalculo.nombreOpcion.Visibility = Visibility.Collapsed;
            subCalculo.VistaOpciones = this;
            subCalculo.NombreOperacion = nombreOperacion_SubCalculo;
            subCalculo.Tipo = TipoOpcionOperacion.SubCalculo;

            if (textoBusqueda == null)
                opciones.Children.Add(subCalculo);
            else
            {
                if (subCalculo.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(subCalculo);
            }

            OpcionOperacion archivoExterno = new OpcionOperacion();
            archivoExterno.nombreOpcion.Visibility = Visibility.Collapsed;
            archivoExterno.VistaOpciones = this;
            archivoExterno.NombreOperacion = nombreOperacion_ArchivoExterno;
            archivoExterno.Tipo = TipoOpcionOperacion.ArchivoExterno;

            if (textoBusqueda == null)
                opciones.Children.Add(archivoExterno);
            else
            {
                if (archivoExterno.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(archivoExterno);
            }

            OpcionOperacion espera = new OpcionOperacion();
            espera.nombreOpcion.Visibility = Visibility.Collapsed;
            espera.VistaOpciones = this;
            espera.NombreOperacion = nombreOperacion_Espera;
            espera.Tipo = TipoOpcionOperacion.Espera;

            if (textoBusqueda == null)
                opciones.Children.Add(espera);
            else
            {
                if (espera.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(espera);
            }

            OpcionOperacion limpiarDatos = new OpcionOperacion();
            limpiarDatos.nombreOpcion.Visibility = Visibility.Collapsed;
            limpiarDatos.VistaOpciones = this;
            limpiarDatos.NombreOperacion = nombreOperacion_LimpiarDatos;
            limpiarDatos.Tipo = TipoOpcionOperacion.LimpiarDatos;

            if (textoBusqueda == null)
                opciones.Children.Add(limpiarDatos);
            else
            {
                if (limpiarDatos.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(limpiarDatos);
            }

            OpcionOperacion nota = new OpcionOperacion();
            nota.nombreOpcion.Visibility = Visibility.Collapsed;
            nota.VistaOpciones = this;
            nota.NombreOperacion = nombreOperacion;
            nota.Tipo = TipoOpcionOperacion.Nota;

            if (textoBusqueda == null)
                opciones.Children.Add(nota);
            else
            {
                if (nota.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
                    opciones.Children.Add(nota);
            }
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Calculo.ModoSubCalculo)
            {
                btnAbrirCarpeta.Visibility = Visibility.Collapsed;
            }

            ListarOpcionesOperacion();
            ListarEntradas();

            entradas.Width = fichas.ActualWidth;
            opciones.Width = fichas.ActualWidth;

            entradas.Height = fichas.ActualHeight;
            opciones.Height = fichas.ActualHeight;

            if (double.IsNaN(Operacion.AnchoDiagrama) &
                    double.IsNaN(Operacion.AltoDiagrama))
            {
                diagrama.Width = contenedor.ActualWidth;
                Operacion.AnchoDiagrama = diagrama.Width;

                diagrama.Height = contenedor.ActualHeight;
                Operacion.AltoDiagrama = diagrama.Height;

            }

            DibujarElementosDiseñoOperacion();

#if !DEBUG
            App.ClienteMetricasUso?.TrackEvent("AbrirConfiguracionOperacionInternaCalculo");
#endif
        }

        public void ListarEntradas()
        {
            string textoBusqueda = null;

            if (BuscandoOpcionesOperacion &&
                !string.IsNullOrEmpty(busquedaOperaciones.Text))
            {
                textoBusqueda = busquedaOperaciones.Text;
            }

            entradas.Children.Clear();
            foreach (var itemEntrada in (from DiseñoOperacion E in Operacion.ElementosAnteriores where E.Tipo != TipoElementoOperacion.Ninguna & E.Tipo != TipoElementoOperacion.Linea select E).ToList())
            {
                if (itemEntrada.Tipo == TipoElementoOperacion.Entrada)
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();

                    if (itemEntrada.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                        itemEntrada.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        nuevaEntrada.EsEntrada = ((from C in Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(itemEntrada.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                            ? true : false;

                    nuevaEntrada.DesdeDiagramaOperacion = true;
                    nuevaEntrada.Entrada = itemEntrada.EntradaRelacionada;
                    nuevaEntrada.DiseñoOperacion = itemEntrada;
                    nuevaEntrada.VistaOperacion = this;

                    if (textoBusqueda == null)
                        entradas.Children.Add(nuevaEntrada);
                    else
                    {
                        if (nuevaEntrada.nombreEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                            nuevaEntrada.tipoEntrada.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            entradas.Children.Add(nuevaEntrada);
                    }
                }
                else
                {
                    EntradaFlujoOperacion nuevaEntrada = new EntradaFlujoOperacion();
                    nuevaEntrada.DiseñoOperacion = itemEntrada;
                    nuevaEntrada.VistaOperacion = this;

                    if (textoBusqueda == null)
                        entradas.Children.Add(nuevaEntrada);
                    else
                    {
                        if (nuevaEntrada.nombreEntradaFlujo.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            entradas.Children.Add(nuevaEntrada);
                    }
                }
            }
        }

        public void DibujarElementosDiseñoOperacion()
        {
            diagrama.Children.Clear();
            //EntradaSeleccionada = null;
            //FlujoOperacionSeleccionado = null;
            //OpcionOperacionSeleccionado = null;
            //OpcionSalidaSeleccionado = null;
            //lineaSeleccionada = null;

            foreach (var itemElemento in Operacion.ElementosDiseñoOperacion)
            {
                if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Entrada)
                {
                    EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();

                    if (itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior != null &&
                        itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                        nuevaEntrada.EsEntrada = ((from C in Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(itemElemento.EntradaRelacionada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                            ? true : false;

                    nuevaEntrada.VistaOperacion = this;
                    nuevaEntrada.DesdeDiagramaOperacion = true;
                    nuevaEntrada.EnDiagramaOperacion = true;
                    nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
                    nuevaEntrada.DiseñoElementoOperacion = itemElemento;
                    nuevaEntrada.Entrada = itemElemento.EntradaRelacionada;
                    nuevaEntrada.SizeChanged += CambioTamañoEntrada;

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
                    nuevoFlujo.VistaOperacion = this;
                    nuevoFlujo.EnDiagrama = true;
                    nuevoFlujo.botonFondo.BorderBrush = Brushes.Black;
                    nuevoFlujo.DiseñoOperacion = itemElemento.ElementoDiseñoRelacionado;
                    nuevoFlujo.DiseñoElementoOperacion = itemElemento;
                    nuevoFlujo.SizeChanged += CambioTamañoFlujoOperacion;

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
                    nuevaOpcion.VistaOpciones = this;
                    nuevaOpcion.EnDiagrama = true;
                    nuevaOpcion.botonFondo.BorderBrush = Brushes.Black;
                    nuevaOpcion.SizeChanged += CambioTamañoOpcionOperacion;

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
                        ; nombreOperacion = "Lógica de selección de números";
                    }
                    else if (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo)
                    {
                        nombreOperacion = "Lógica de selección de variables o vectores";
                    }
                    else if (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
                    {
                        nombreOperacion = "Lógica de selección de variables o vectores por separado";
                    }
                    else
                    {
                        switch (Operacion.Tipo)
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
                                nombreOperacion = "Calcular inverso";
                                break;
                            case TipoElementoOperacion.Factorial:
                                nombreOperacion = "Calcular factorial";
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
                    nuevaOpcion.VistaOpciones = this;
                    nuevaOpcion.EnDiagrama = true;
                    nuevaOpcion.botonFondo.BorderBrush = Brushes.Black;
                    nuevaOpcion.SizeChanged += CambioTamañoOpcionSalida;

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
                    nuevaNota.VistaOpciones = this;
                    nuevaNota.EnDiagrama = true;

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
                    switch (Operacion.Tipo)
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

                //DibujarLineasElemento(itemElemento);
            }

            DibujarTodasLineasElementos();
            EstablecerIndicesProfundidadElementos();
        }

        public void DibujarLineasElemento(DiseñoElementoOperacion item)
        {
            foreach (var itemElemento in item.ElementosPosteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea(item, itemElemento);
                if (nuevaLinea != null)
                {
                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;
                    diagrama.Children.Add(nuevaLinea);

                    if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                        lineaSeleccionada = nuevaLinea;
                }
            }

            foreach (var itemElemento in item.ElementosAnteriores)
            {
                ArrowLine nuevaLinea = BuscarLinea(itemElemento, item);
                if (nuevaLinea != null)
                {
                    nuevaLinea.MouseLeftButtonDown += SeleccionarLinea;
                    diagrama.Children.Add(nuevaLinea);

                    if (item == ElementoAnteriorLineaSeleccionada & itemElemento == ElementoPosteriorLineaSeleccionada)
                        lineaSeleccionada = nuevaLinea;
                }
            }
        }

        public void DibujarTodasLineasElementos()
        {
            QuitarTodasLineas();

            foreach (var itemElemento in Operacion.ElementosDiseñoOperacion)
            {
                DibujarLineasElemento(itemElemento);
            }
        }
        private DiseñoElementoOperacion VerificarArrastreOtroElemento(Point ubicacion)
        {
            foreach (var itemElemento in Operacion.ElementosDiseñoOperacion)
            {
                var itemControl = (from UIElement C in diagrama.Children
                                   where
((C.GetType() == typeof(EntradaFlujoOperacion) && ((EntradaFlujoOperacion)C).DiseñoElementoOperacion == itemElemento)) ||
((C.GetType() == typeof(EntradaDiseñoOperaciones) && ((EntradaDiseñoOperaciones)C).DiseñoElementoOperacion == itemElemento)) ||
((C.GetType() == typeof(OpcionOperacion) && ((OpcionOperacion)C).DiseñoElementoOperacion == itemElemento)) ||
((C.GetType() == typeof(OpcionSalida) && ((OpcionSalida)C).DiseñoElementoOperacion == itemElemento))
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

        private ArrowLine BuscarLinea(DiseñoElementoOperacion elementoOrigen, DiseñoElementoOperacion elementoDestino)
        {
            foreach (var item in Operacion.ElementosDiseñoOperacion)
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

        private void CambioTamañoEntrada(object sender, SizeChangedEventArgs e)
        {
            EntradaDiseñoOperaciones entrada = (EntradaDiseñoOperaciones)sender;
            entrada.DiseñoElementoOperacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoElementoOperacion.Altura = entrada.ActualHeight;

            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);
        }

        private void CambioTamañoFlujoOperacion(object sender, SizeChangedEventArgs e)
        {
            EntradaFlujoOperacion entrada = (EntradaFlujoOperacion)sender;
            entrada.DiseñoElementoOperacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoElementoOperacion.Altura = entrada.ActualHeight;

            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);
        }

        public void CambioTamañoOpcionOperacion(object sender, SizeChangedEventArgs e)
        {
            OpcionOperacion entrada = (OpcionOperacion)sender;
            entrada.DiseñoElementoOperacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoElementoOperacion.Altura = entrada.ActualHeight;

            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);
        }

        private void CambioTamañoOpcionSalida(object sender, SizeChangedEventArgs e)
        {
            OpcionSalida entrada = (OpcionSalida)sender;
            entrada.DiseñoElementoOperacion.Anchura = entrada.ActualWidth;
            entrada.DiseñoElementoOperacion.Altura = entrada.ActualHeight;

            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);
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

        public void DestacarElementoSeleccionado()
        {
            ElementosSeleccionados.Clear();
            ElementoSeleccionado = null;

            foreach (var item in diagrama.Children)
            {
                if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(OpcionOperacion))
                {
                    ((OpcionOperacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((OpcionOperacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(OpcionSalida))
                {
                    ((OpcionSalida)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((OpcionSalida)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(EntradaFlujoOperacion))
                {
                    ((EntradaFlujoOperacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                    ((EntradaFlujoOperacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                }
                else if (item.GetType() == typeof(NotaDiagrama))
                {
                    ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                }
                else if (item.GetType() == typeof(Line))
                    ((Line)item).Stroke = Brushes.Black;
            }

            LimpiarInfoElemento();

            if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Entrada)
            {
                if (EntradaSeleccionada.EnDiagramaOperacion)
                {
                    EntradaSeleccionada.Background = SystemColors.HighlightBrush;
                    EntradaSeleccionada.botonFondo.Background = SystemColors.HighlightBrush;
                }
            }
            else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.FlujoOperacion)
            {
                if (FlujoOperacionSeleccionado.EnDiagrama)
                {
                    FlujoOperacionSeleccionado.Background = SystemColors.HighlightBrush;
                    FlujoOperacionSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
                }
            }
            else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.OpcionOperacion)
            {
                if (OpcionOperacionSeleccionado.EnDiagrama)
                {
                    OpcionOperacionSeleccionado.Background = SystemColors.HighlightBrush;
                    OpcionOperacionSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
                }
            }
            else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Salida)
            {
                if (OpcionSalidaSeleccionado.EnDiagrama)
                {
                    OpcionSalidaSeleccionado.Background = SystemColors.HighlightBrush;
                    OpcionSalidaSeleccionado.botonFondo.Background = SystemColors.HighlightBrush;
                }
            }
            else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Nota)
            {
                if (NotaSeleccionada.EnDiagrama)
                {
                    NotaSeleccionada.fondo.BorderThickness = new Thickness(1);
                }
            }
            else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Linea)
                lineaSeleccionada.Stroke = SystemColors.HighlightBrush;
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
            condicionesTextosInformacionOperandosResultados.SubOperandos = null;
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
            opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.IsChecked = false;
            opcionUtilizarDefinicionAsignacionTextosInformacion.IsChecked = false;
            opcionAgregarCantidadNumerosCantidad.IsChecked = false;
            opcionIncluirCantidadNumero.IsChecked = false;
            opcionAgregarCantidadNumerosTextoInformacion.IsChecked = false;
            opcionAgregarNumerosTextoInformacion.IsChecked = false;
            opcionAntesEjecucion.IsChecked = false;
            opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked = false;
            opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked = false;
            ordenarAntesEjecucion_PorNumero.IsChecked = false;
            ordenarAntesEjecucion_PorNombre.IsChecked = false;
            opcionTipoOrdenamientoAntesEjecucion.SelectedItem = null;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked = false;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = false;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked = false;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked = false;
            opcionDespuesEjecucion.IsChecked = false;
            opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked = false;
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
            botonOpcionLimpiarDatos.Visibility = Visibility.Collapsed;
            MostrarInfo_Elemento(null);

        }
        public void diagrama_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OcultarToolTips(null);
            //if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Linea &&
            //lineaSeleccionada != null) return;
            if (lineaSeleccionada_)
            {
                lineaSeleccionada_ = false;
                return;
            }

            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Ninguno;
            EntradaSeleccionada = null;
            FlujoOperacionSeleccionado = null;
            OpcionOperacionSeleccionado = null;
            NotaSeleccionada = null;
            OpcionSalidaSeleccionado = null;
            lineaSeleccionada = null;
            ElementoSeleccionado = null;
            ElementosDiagramaSeleccionados.Clear();
            ElementosSeleccionados.Clear();

            ElementoSeleccionado_Bool = false;
            ClicDiagrama = true;

            MostrarOrdenOperando_Elemento(null);
            MostrarInfo_Elemento(null);
            MostrarAcumulacion();
            DestacarElementoSeleccionado();
            DibujarTodasLineasElementos();
            //diagrama.UpdateLayout();

            if (e != null)
                ubicacionInicialAreaSeleccionada = e.GetPosition(diagrama);
        }

        private void SeleccionarLinea(object sender, RoutedEventArgs e)
        {
            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Linea;
            lineaSeleccionada = (ArrowLine)sender;
            ElementoSeleccionado_Bool = true;

            ElementoAnteriorLineaSeleccionada = null;
            ElementoPosteriorLineaSeleccionada = null;

            EncontrarElementosLinea(lineaSeleccionada, ref ElementoAnteriorLineaSeleccionada, ref ElementoPosteriorLineaSeleccionada);

            MostrarOrdenOperando_Elemento(null);
            contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            DestacarElementoSeleccionado();

            lineaSeleccionada_ = true;
        }

        private void EncontrarElementosLinea(ArrowLine lineaSeleccionada, ref DiseñoElementoOperacion ElementoAnteriorLineaSeleccionada,
           ref DiseñoElementoOperacion ElementoPosteriorLineaSeleccionada)
        {

            foreach (var itemControl in (from UIElement C in diagrama.Children where C.GetType() != typeof(ArrowLine) select C).ToList())
            {
                var lineasOrigen = BuscarLineasUnElemento(new Point(Canvas.GetLeft(itemControl), Canvas.GetTop(itemControl)),
                true, itemControl);

                var lineaOrigenEncontrada = (from ArrowLine L in lineasOrigen where L == lineaSeleccionada select L).FirstOrDefault();

                if (lineaOrigenEncontrada != null)
                {
                    if (itemControl.GetType() == typeof(EntradaDiseñoOperaciones))
                        ElementoAnteriorLineaSeleccionada = ((EntradaDiseñoOperaciones)itemControl).DiseñoElementoOperacion;
                    if (itemControl.GetType() == typeof(EntradaFlujoOperacion))
                        ElementoAnteriorLineaSeleccionada = ((EntradaFlujoOperacion)itemControl).DiseñoElementoOperacion;
                    if (itemControl.GetType() == typeof(OpcionOperacion))
                        ElementoAnteriorLineaSeleccionada = ((OpcionOperacion)itemControl).DiseñoElementoOperacion;
                    if (itemControl.GetType() == typeof(OpcionSalida))
                        ElementoAnteriorLineaSeleccionada = ((OpcionSalida)itemControl).DiseñoElementoOperacion;

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
                        ElementoPosteriorLineaSeleccionada = ((EntradaDiseñoOperaciones)itemControl).DiseñoElementoOperacion;
                    if (itemControl.GetType() == typeof(EntradaFlujoOperacion))
                        ElementoPosteriorLineaSeleccionada = ((EntradaFlujoOperacion)itemControl).DiseñoElementoOperacion;
                    if (itemControl.GetType() == typeof(OpcionOperacion))
                        ElementoPosteriorLineaSeleccionada = ((OpcionOperacion)itemControl).DiseñoElementoOperacion;
                    if (itemControl.GetType() == typeof(OpcionSalida))
                        ElementoPosteriorLineaSeleccionada = ((OpcionSalida)itemControl).DiseñoElementoOperacion;

                    break;
                }
            }
        }
        private void diagrama_Drop(object sender, DragEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                DiseñoElementoOperacion arrastreHastaOtroElemento;
                arrastreHastaOtroElemento = VerificarArrastreOtroElemento(e.GetPosition(diagrama));

                if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Entrada)
                {
                    if (EntradaSeleccionada.EnDiagramaOperacion)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(EntradaSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(EntradaSeleccionada) + EntradaSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(EntradaSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(EntradaSeleccionada) + EntradaSeleccionada.ActualHeight))
                            return;

                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != EntradaSeleccionada.DiseñoElementoOperacion && arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Salida)
                        {
                            if (!EntradaSeleccionada.DiseñoElementoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento) &&
                            !arrastreHastaOtroElemento.ElementosAnteriores.Contains(EntradaSeleccionada.DiseñoElementoOperacion))
                            {
                                //if (arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Entrada &
                                //    !(arrastreHastaOtroElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                //    arrastreHastaOtroElemento.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar))
                                //{
                                if (VerificarElementos_Entradas(EntradaSeleccionada.DiseñoElementoOperacion, arrastreHastaOtroElemento))
                                {
                                    if (!EntradaSeleccionada.DiseñoElementoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento))
                                    {
                                        bool contieneSalida = false;
                                        if (EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida)
                                        {
                                            EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida = false;
                                            QuitarElementoSalida(EntradaSeleccionada.DiseñoElementoOperacion);
                                            EstablecerTextoBotonSalida(false);
                                            contieneSalida = true;
                                        }
                                        EntradaSeleccionada.DiseñoElementoOperacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                        arrastreHastaOtroElemento.ElementosAnteriores.Add(EntradaSeleccionada.DiseñoElementoOperacion);
                                        arrastreHastaOtroElemento.EntradaRelacionada = EntradaSeleccionada.DiseñoElementoOperacion.EntradaRelacionada;
                                        EntradaSeleccionada.DiseñoElementoOperacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);
                                        EntradaSeleccionada.DiseñoElementoOperacion.AgregarOrdenOperando(arrastreHastaOtroElemento);

                                        if (contieneSalida)
                                            DibujarElementosDiseñoOperacion();

                                        DibujarTodasLineasElementos();
                                        EstablecerIndicesProfundidadElementos();
                                    }
                                }
                                //}
                            }
                        }
                        else
                        {
                            //Point ubicacionOriginal = new Point(EntradaSeleccionada.DiseñoElementoOperacion.PosicionX, EntradaSeleccionada.DiseñoElementoOperacion.PosicionY);

                            EstablecerCoordenadasElementoMover(EntradaSeleccionada, EntradaSeleccionada.DiseñoElementoOperacion, e);

                            EntradaSeleccionada.DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                            EntradaSeleccionada.DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(EntradaSeleccionada, EntradaSeleccionada.DiseñoElementoOperacion.PosicionY);
                            Canvas.SetLeft(EntradaSeleccionada, EntradaSeleccionada.DiseñoElementoOperacion.PosicionX);

                            //List<Line> lineas = BuscarLineasUnElemento(ubicacionOriginal, true, EntradaSeleccionada);
                            //ReubicarLineasUnElemento(lineas, EntradaSeleccionada.DiseñoElementoOperacion, true);

                            //lineas = BuscarLineasUnElemento(ubicacionOriginal, false, EntradaSeleccionada);
                            //ReubicarLineasUnElemento(lineas, EntradaSeleccionada.DiseñoElementoOperacion, false);
                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                    else
                    {
                        AgregarEntrada(sender, e);
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.FlujoOperacion)
                {
                    if (FlujoOperacionSeleccionado.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(FlujoOperacionSeleccionado) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(FlujoOperacionSeleccionado) + FlujoOperacionSeleccionado.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(FlujoOperacionSeleccionado) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(FlujoOperacionSeleccionado) + FlujoOperacionSeleccionado.ActualHeight))
                            return;

                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != FlujoOperacionSeleccionado.DiseñoElementoOperacion && arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Salida)
                        {
                            if (!FlujoOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento) &&
                            !arrastreHastaOtroElemento.ElementosAnteriores.Contains(FlujoOperacionSeleccionado.DiseñoElementoOperacion))
                            {
                                //if (arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Entrada &
                                //    !(arrastreHastaOtroElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                //    arrastreHastaOtroElemento.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar))
                                //{
                                if (VerificarElementos_Entradas(FlujoOperacionSeleccionado.DiseñoElementoOperacion, arrastreHastaOtroElemento))
                                {
                                    if (!FlujoOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento))
                                    {
                                        bool contieneSalida = false;
                                        if (FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida)
                                        {
                                            FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida = false;
                                            QuitarElementoSalida(FlujoOperacionSeleccionado.DiseñoElementoOperacion);
                                            EstablecerTextoBotonSalida(false);
                                            contieneSalida = true;
                                        }

                                        FlujoOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                        arrastreHastaOtroElemento.ElementosAnteriores.Add(FlujoOperacionSeleccionado.DiseñoElementoOperacion);
                                        arrastreHastaOtroElemento.EntradaRelacionada = FlujoOperacionSeleccionado.DiseñoElementoOperacion.EntradaRelacionada;
                                        FlujoOperacionSeleccionado.DiseñoElementoOperacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);
                                        FlujoOperacionSeleccionado.DiseñoElementoOperacion.AgregarOrdenOperando(arrastreHastaOtroElemento);

                                        //}
                                        if (contieneSalida)
                                            DibujarElementosDiseñoOperacion();

                                        DibujarTodasLineasElementos();
                                        EstablecerIndicesProfundidadElementos();
                                    }
                                }
                                //}
                            }
                        }
                        else
                        {
                            //Point ubicacionOriginal = new Point(FlujoOperacionSeleccionado.DiseñoElementoOperacion.PosicionX, FlujoOperacionSeleccionado.DiseñoElementoOperacion.PosicionY);

                            EstablecerCoordenadasElementoMover(FlujoOperacionSeleccionado, FlujoOperacionSeleccionado.DiseñoElementoOperacion, e);

                            FlujoOperacionSeleccionado.DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                            FlujoOperacionSeleccionado.DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(FlujoOperacionSeleccionado, FlujoOperacionSeleccionado.DiseñoElementoOperacion.PosicionY);
                            Canvas.SetLeft(FlujoOperacionSeleccionado, FlujoOperacionSeleccionado.DiseñoElementoOperacion.PosicionX);

                            //List<Line> lineas = BuscarLineasUnElemento(ubicacionOriginal, true, FlujoOperacionSeleccionado);
                            //ReubicarLineasUnElemento(lineas, FlujoOperacionSeleccionado.DiseñoElementoOperacion, true);

                            //lineas = BuscarLineasUnElemento(ubicacionOriginal, false, FlujoOperacionSeleccionado);
                            //ReubicarLineasUnElemento(lineas, FlujoOperacionSeleccionado.DiseñoElementoOperacion, false);
                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                    else
                    {
                        AgregarFlujoOperacion(sender, e);
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.OpcionOperacion)
                {
                    if (OpcionOperacionSeleccionado.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(OpcionOperacionSeleccionado) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(OpcionOperacionSeleccionado) + OpcionOperacionSeleccionado.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(OpcionOperacionSeleccionado) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(OpcionOperacionSeleccionado) + OpcionOperacionSeleccionado.ActualHeight))
                            return;

                        if (arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != OpcionOperacionSeleccionado.DiseñoElementoOperacion && arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Salida)
                        {
                            if (!OpcionOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento) &&
                            !arrastreHastaOtroElemento.ElementosAnteriores.Contains(OpcionOperacionSeleccionado.DiseñoElementoOperacion))
                            {
                                if (VerificarElementos_SeleccionarOrdenar(OpcionOperacionSeleccionado.DiseñoElementoOperacion, arrastreHastaOtroElemento))
                                {
                                    if (arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Entrada)
                                    {
                                        if (!OpcionOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores.Contains(arrastreHastaOtroElemento))
                                        {
                                            bool contieneSalida = false;
                                            if (OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida)
                                            {
                                                OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida = false;
                                                QuitarElementoSalida(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                                                EstablecerTextoBotonSalida(false);
                                                contieneSalida = true;
                                            }

                                            OpcionOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores.Add(arrastreHastaOtroElemento);
                                            arrastreHastaOtroElemento.ElementosAnteriores.Add(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                                            arrastreHastaOtroElemento.EntradaRelacionada = OpcionOperacionSeleccionado.DiseñoElementoOperacion.EntradaRelacionada;
                                            OpcionOperacionSeleccionado.DiseñoElementoOperacion.ElementosContenedoresOperacion.Add(arrastreHastaOtroElemento);
                                            OpcionOperacionSeleccionado.DiseñoElementoOperacion.AgregarOrdenOperando(arrastreHastaOtroElemento);

                                            if (OpcionOperacionSeleccionado.DiseñoElementoOperacion.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                                OpcionOperacionSeleccionado.DiseñoElementoOperacion.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
                                                OpcionOperacionSeleccionado.DiseñoElementoOperacion.SalidasAgrupamiento_CondicionFlujo.Add(arrastreHastaOtroElemento);

                                            if (OpcionOperacionSeleccionado.DiseñoElementoOperacion.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                                OpcionOperacionSeleccionado.DiseñoElementoOperacion.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados)
                                                OpcionOperacionSeleccionado.DiseñoElementoOperacion.SalidasAgrupamiento_SeleccionOrdenamiento.Add(arrastreHastaOtroElemento);


                                            //}
                                            if (contieneSalida)
                                                DibujarElementosDiseñoOperacion();

                                            DibujarTodasLineasElementos();
                                            EstablecerIndicesProfundidadElementos();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Point ubicacionOriginal = new Point(OpcionOperacionSeleccionado.DiseñoElementoOperacion.PosicionX, OpcionOperacionSeleccionado.DiseñoElementoOperacion.PosicionY);

                            EstablecerCoordenadasElementoMover(OpcionOperacionSeleccionado, OpcionOperacionSeleccionado.DiseñoElementoOperacion, e);

                            OpcionOperacionSeleccionado.DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                            OpcionOperacionSeleccionado.DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(OpcionOperacionSeleccionado, OpcionOperacionSeleccionado.DiseñoElementoOperacion.PosicionY);
                            Canvas.SetLeft(OpcionOperacionSeleccionado, OpcionOperacionSeleccionado.DiseñoElementoOperacion.PosicionX);

                            //List<Line> lineas = BuscarLineasUnElemento(ubicacionOriginal, true, OpcionOperacionSeleccionado);
                            //ReubicarLineasUnElemento(lineas, OpcionOperacionSeleccionado.DiseñoElementoOperacion, true);

                            //lineas = BuscarLineasUnElemento(ubicacionOriginal, false, OpcionOperacionSeleccionado);
                            //ReubicarLineasUnElemento(lineas, OpcionOperacionSeleccionado.DiseñoElementoOperacion, false);
                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                    else
                    {
                        AgregarOpcionOperacion(sender, e);
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Nota)
                {
                    if (NotaSeleccionada.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(NotaSeleccionada) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(NotaSeleccionada) + NotaSeleccionada.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(NotaSeleccionada) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(NotaSeleccionada) + NotaSeleccionada.ActualHeight))
                            return;

                        EstablecerCoordenadasElementoMover(NotaSeleccionada, NotaSeleccionada.DiseñoElementoOperacion, e);

                        NotaSeleccionada.DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                        NotaSeleccionada.DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                        Canvas.SetTop(NotaSeleccionada, NotaSeleccionada.DiseñoElementoOperacion.PosicionY);
                        Canvas.SetLeft(NotaSeleccionada, NotaSeleccionada.DiseñoElementoOperacion.PosicionX);

                    }
                    else
                    {
                        NotaDiagrama nuevaNota = new NotaDiagrama();
                        nuevaNota.VistaOpciones = this;
                        nuevaNota.EnDiagrama = true;

                        string nombreOperacion = string.Empty;

                        var ultimoNombre = (from DiseñoElementoOperacion E in Operacion.ElementosDiseñoOperacion where E.Tipo == TipoElementoDiseñoOperacion.Nota && E.Info.LastIndexOf(" ") > -1 select E.Info).LastOrDefault();
                        int cantidadElementosTipo = 0;
                        if (ultimoNombre != null) int.TryParse(ultimoNombre.Substring(ultimoNombre.LastIndexOf(" ")).Trim(), out cantidadElementosTipo);
                        cantidadElementosTipo++;

                        nombreOperacion = "Nota " + cantidadElementosTipo.ToString();
                        nuevaNota.Tipo = NotaSeleccionada.Tipo;
                        //nuevaOperacion.SizeChanged += CambioTamañoOpcionOperacion;

                        DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
                        nuevoElementoOperacion.ID = App.GenerarID_Elemento();
                        nuevoElementoOperacion.Info = nombreOperacion;
                        nuevoElementoOperacion.Tipo = TipoElementoDiseñoOperacionSeleccionado;
                        nuevoElementoOperacion.TipoOpcionOperacion = TipoOpcionOperacion.Nota;
                        nuevoElementoOperacion.PosicionX = e.GetPosition(diagrama).X;
                        nuevoElementoOperacion.PosicionY = e.GetPosition(diagrama).Y;

                        Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
                        nuevaNota.DiseñoElementoOperacion = nuevoElementoOperacion;

                        diagrama.Children.Add(nuevaNota);

                        Canvas.SetTop(nuevaNota, e.GetPosition(diagrama).Y);
                        Canvas.SetLeft(nuevaNota, e.GetPosition(diagrama).X);

                        nuevoElementoOperacion.Anchura = nuevaNota.ActualWidth;
                        nuevoElementoOperacion.Altura = nuevaNota.ActualHeight;

                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Salida)
                {
                    if (OpcionSalidaSeleccionado.EnDiagrama)
                    {
                        if ((e.GetPosition(diagrama).X >= Canvas.GetLeft(OpcionSalidaSeleccionado) &
                            e.GetPosition(diagrama).X <= Canvas.GetLeft(OpcionSalidaSeleccionado) + OpcionSalidaSeleccionado.ActualWidth) &
                            (e.GetPosition(diagrama).Y >= Canvas.GetTop(OpcionSalidaSeleccionado) &
                            e.GetPosition(diagrama).Y <= Canvas.GetTop(OpcionSalidaSeleccionado) + OpcionSalidaSeleccionado.ActualHeight))
                            return;

                        if (!(arrastreHastaOtroElemento != null && arrastreHastaOtroElemento != OpcionSalidaSeleccionado.DiseñoElementoOperacion && arrastreHastaOtroElemento.Tipo != TipoElementoDiseñoOperacion.Salida))
                        {
                            EstablecerCoordenadasElementoMover(OpcionSalidaSeleccionado, OpcionSalidaSeleccionado.DiseñoElementoOperacion, e);

                            OpcionSalidaSeleccionado.DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                            OpcionSalidaSeleccionado.DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                            Canvas.SetTop(OpcionSalidaSeleccionado, OpcionSalidaSeleccionado.DiseñoElementoOperacion.PosicionY);
                            Canvas.SetLeft(OpcionSalidaSeleccionado, OpcionSalidaSeleccionado.DiseñoElementoOperacion.PosicionX);

                            //List<Line> lineas = BuscarLineasUnElemento(ubicacionOriginal, true, OpcionOperacionSeleccionado);
                            //ReubicarLineasUnElemento(lineas, OpcionOperacionSeleccionado.DiseñoElementoOperacion, true);

                            //lineas = BuscarLineasUnElemento(ubicacionOriginal, false, OpcionOperacionSeleccionado);
                            //ReubicarLineasUnElemento(lineas, OpcionOperacionSeleccionado.DiseñoElementoOperacion, false);
                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                }

                if (ElementoSeleccionado == null) ElementoSeleccionado_Bool = false;
            }
            else if (ElementosSeleccionados.Any())
            {
                Point diferenciaDistanciaPunto = new Point(0, 0);

                if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX,
                        ubicacionActualElemento.Y - ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);

                    ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                    ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((EntradaDiseñoOperaciones)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX);

                }
                else if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(NotaDiagrama))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX,
                        ubicacionActualElemento.Y - ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);

                    ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                    ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((NotaDiagrama)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX);
                }
                else if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(EntradaFlujoOperacion))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX,
                        ubicacionActualElemento.Y - ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);

                    ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                    ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((EntradaFlujoOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX);
                }
                else if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(OpcionOperacion))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX,
                        ubicacionActualElemento.Y - ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);

                    ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                    ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((OpcionOperacion)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX);
                }
                else if (ElementoDiagramaSeleccionadoMover.GetType() == typeof(OpcionSalida))
                {
                    EstablecerCoordenadasElementoMover(ElementoDiagramaSeleccionadoMover,
                        ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion, e);

                    diferenciaDistanciaPunto = new Point(ubicacionActualElemento.X - ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX,
                        ubicacionActualElemento.Y - ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);

                    ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX = ubicacionActualElemento.X;
                    ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY = ubicacionActualElemento.Y;

                    Canvas.SetTop(ElementoDiagramaSeleccionadoMover, ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionY);
                    Canvas.SetLeft(ElementoDiagramaSeleccionadoMover, ((OpcionSalida)ElementoDiagramaSeleccionadoMover).DiseñoElementoOperacion.PosicionX);
                }

                foreach (UIElement elemento in ElementosDiagramaSeleccionados)
                {
                    if (elemento != null)
                    {
                        if (elemento != ElementoDiagramaSeleccionadoMover)
                        {
                            if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                            {
                                ((EntradaDiseñoOperaciones)elemento).DiseñoElementoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((EntradaDiseñoOperaciones)elemento).DiseñoElementoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((EntradaDiseñoOperaciones)elemento).DiseñoElementoOperacion.PosicionY);
                                Canvas.SetLeft(elemento, ((EntradaDiseñoOperaciones)elemento).DiseñoElementoOperacion.PosicionX);

                            }
                            else if (elemento.GetType() == typeof(NotaDiagrama))
                            {
                                ((NotaDiagrama)elemento).DiseñoElementoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((NotaDiagrama)elemento).DiseñoElementoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((NotaDiagrama)elemento).DiseñoElementoOperacion.PosicionY);
                                Canvas.SetLeft(elemento, ((NotaDiagrama)elemento).DiseñoElementoOperacion.PosicionX);
                            }
                            else if (elemento.GetType() == typeof(EntradaFlujoOperacion))
                            {
                                ((EntradaFlujoOperacion)elemento).DiseñoElementoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((EntradaFlujoOperacion)elemento).DiseñoElementoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((EntradaFlujoOperacion)elemento).DiseñoElementoOperacion.PosicionY);
                                Canvas.SetLeft(elemento, ((EntradaFlujoOperacion)elemento).DiseñoElementoOperacion.PosicionX);
                            }
                            else if (elemento.GetType() == typeof(OpcionOperacion))
                            {
                                ((OpcionOperacion)elemento).DiseñoElementoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((OpcionOperacion)elemento).DiseñoElementoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((OpcionOperacion)elemento).DiseñoElementoOperacion.PosicionY);
                                Canvas.SetLeft(elemento, ((OpcionOperacion)elemento).DiseñoElementoOperacion.PosicionX);
                            }
                            else if (elemento.GetType() == typeof(OpcionSalida))
                            {
                                ((OpcionSalida)elemento).DiseñoElementoOperacion.PosicionX += diferenciaDistanciaPunto.X;
                                ((OpcionSalida)elemento).DiseñoElementoOperacion.PosicionY += diferenciaDistanciaPunto.Y;

                                Canvas.SetTop(elemento, ((OpcionSalida)elemento).DiseñoElementoOperacion.PosicionY);
                                Canvas.SetLeft(elemento, ((OpcionSalida)elemento).DiseñoElementoOperacion.PosicionX);
                            }
                        }
                    }
                }

                DibujarTodasLineasElementos();
                EstablecerIndicesProfundidadElementos();
            }

            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);
        }

        public void AgregarEntrada(object sender, DragEventArgs e)
        {
            EntradaDiseñoOperaciones nuevaEntrada = new EntradaDiseñoOperaciones();

            double X_Punto = 10;
            double Y_Punto = 10;

            if (EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior != null &&
                EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior.EntradaRelacionada != null)
                nuevaEntrada.EsEntrada = ((from C in Calculo.Calculos where C.EsEntradasArchivo select C).FirstOrDefault().ListaEntradas.Contains(EntradaSeleccionada.Entrada.ElementoSalidaCalculoAnterior.EntradaRelacionada))
                    ? true : false;

            nuevaEntrada.VistaOperacion = this;
            nuevaEntrada.DesdeDiagramaOperacion = true;
            nuevaEntrada.EnDiagramaOperacion = true;
            nuevaEntrada.botonFondo.BorderBrush = Brushes.Black;
            nuevaEntrada.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaEntrada.Entrada = EntradaSeleccionada.Entrada;
            //nuevaEntrada.Entrada.Nombre += " " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            nuevaEntrada.SizeChanged += CambioTamañoEntrada;

            DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.EntradaRelacionada = nuevaEntrada.Entrada;
            nuevoElementoOperacion.ElementoDiseñoRelacionado = EntradaSeleccionada.DiseñoOperacion;
            nuevoElementoOperacion.Tipo = TipoElementoDiseñoOperacionSeleccionado;
            //nuevoElementoOperacion.Nivel = 1;

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

            nuevoElementoOperacion.NombreElemento = nuevaEntrada.Entrada.Nombre.ToString() + " " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            nuevoElementoOperacion.Nombre = nuevaEntrada.Entrada.Nombre.ToString();

            Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            nuevaEntrada.DiseñoElementoOperacion = nuevoElementoOperacion;
            nuevaEntrada.Entrada = EntradaSeleccionada.Entrada;
            nuevoElementoOperacion.Actualizar_ToolTips = true;

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

            //if (itemElemento.ToolTip != null)
            //    nuevoCalculo.botonFondo.ToolTip = itemElemento.ToolTip;
            //else
            //{
            //Ventana.ObtenerEjecucionToolTips(Calculo).AgregarToolTipSubElemento_Asociaciones((ToolTipElementoVisual)((ContentControl)nuevaEntrada.botonFondo.ToolTip).Content,
            //    nuevoElementoOperacion.ID);
            ////nuevoElementoOperacion.EntradaRelacionada.ConCambios_ToolTips = true;
            //Calculo.ConCambiosVisuales = true;
            
            //}
        }

        public void AgregarFlujoOperacion(object sender, DragEventArgs e)
        {
            EntradaFlujoOperacion nuevaOperacion = new EntradaFlujoOperacion();

            double X_Punto = 10;
            double Y_Punto = 10;

            nuevaOperacion.VistaOperacion = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            //nuevaOperacion.Tipo = FlujoOperacionSeleccionado.Tipo;
            nuevaOperacion.SizeChanged += CambioTamañoFlujoOperacion;
            nuevaOperacion.DiseñoOperacion = FlujoOperacionSeleccionado.DiseñoOperacion;

            DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            //nuevoElementoOperacion.Tipo = nuevaOperacion.Tipo;
            nuevoElementoOperacion.ElementoDiseñoRelacionado = FlujoOperacionSeleccionado.DiseñoOperacion;
            nuevoElementoOperacion.Tipo = TipoElementoDiseñoOperacionSeleccionado;
            //nuevoElementoOperacion.Nivel = 1;

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

            nuevoElementoOperacion.NombreElemento = FlujoOperacionSeleccionado.DiseñoOperacion.Nombre + " " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            nuevoElementoOperacion.Nombre = FlujoOperacionSeleccionado.DiseñoOperacion.Nombre;
            nuevoElementoOperacion.Actualizar_ToolTips = true;

            Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoElementoOperacion = nuevoElementoOperacion;

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

            //if (itemElemento.ToolTip != null)
            //    nuevoCalculo.botonFondo.ToolTip = itemElemento.ToolTip;
            //else
            //{
            //Ventana.ObtenerEjecucionToolTips(Calculo).AgregarToolTipSubElemento_Asociaciones((ToolTipElementoVisual)((ContentControl)nuevaOperacion.botonFondo.ToolTip).Content,
            //    nuevoElementoOperacion.ID);
            //    Calculo.ConCambiosVisuales = true;
            //}
        }

        public void AgregarOpcionOperacion(object sender, DragEventArgs e)
        {
            OpcionOperacion nuevaOperacion = new OpcionOperacion();

            double X_Punto = 10;
            double Y_Punto = 10;

            nuevaOperacion.VistaOpciones = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;

            string nombreOperacion = string.Empty;

            if (Operacion.Tipo != TipoElementoOperacion.SeleccionarOrdenar &&
                (OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir |
                OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
            {
                nombreOperacion = "Lógica de selección de números";
            }
            else if (Operacion.Tipo != TipoElementoOperacion.CondicionesFlujo &&
                (OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.CondicionesFlujo))
            {
                nombreOperacion = "Lógica de selección de variables o vectores";
            }
            else if (OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
            {
                nombreOperacion = "Lógica de selección de variables o vectores por separado";
            }
            else
            {
                switch (Operacion.Tipo)
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
                        nombreOperacion = "Calcular inverso";
                        break;
                    case TipoElementoOperacion.Factorial:
                        nombreOperacion = "Calcular factorial";
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

            nuevaOperacion.NombreOperacion = nombreOperacion;
            nuevaOperacion.Tipo = OpcionOperacionSeleccionado.Tipo;
            nuevaOperacion.SizeChanged += CambioTamañoOpcionOperacion;

            DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.NombreElemento = "Variable o vector " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();            
            nuevoElementoOperacion.Tipo = TipoElementoDiseñoOperacionSeleccionado;
            
            if (nuevaOperacion.Tipo == TipoOpcionOperacion.SubCalculo)
                nuevoElementoOperacion.ConfigSubCalculo.SubCalculo_Operacion.NombreArchivo = nuevoElementoOperacion.NombreElemento;

            nuevoElementoOperacion.TipoOpcionOperacion = nuevaOperacion.Tipo;

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

            switch (Operacion.Tipo)
            {
                case TipoElementoOperacion.Potencia:
                    switch (Operacion.OpcionElementosFijosPotencia)
                    {
                        case TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos:
                            nuevoElementoOperacion.OpcionElementosFijosPotencia = TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos;
                            break;

                        case TipoOpcionElementosFijosOperacionPotencia.BaseFijaExponenteOperando:
                            nuevoElementoOperacion.OpcionElementosFijosPotencia = TipoOpcionElementosFijosOperacionPotencia.BaseFijaExponenteOperando;
                            break;

                        case TipoOpcionElementosFijosOperacionPotencia.BaseOperandoExponenteFijo:
                            nuevoElementoOperacion.OpcionElementosFijosPotencia = TipoOpcionElementosFijosOperacionPotencia.BaseOperandoExponenteFijo;
                            break;
                    }

                    break;

                case TipoElementoOperacion.Raiz:
                    switch (Operacion.OpcionElementosFijosRaiz)
                    {
                        case TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos:
                            nuevoElementoOperacion.OpcionElementosFijosRaiz = TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos;
                            break;

                        case TipoOpcionElementosFijosOperacionRaiz.RaizFijaRadicalOperando:
                            nuevoElementoOperacion.OpcionElementosFijosRaiz = TipoOpcionElementosFijosOperacionRaiz.RaizFijaRadicalOperando;
                            break;

                        case TipoOpcionElementosFijosOperacionRaiz.RaizOperandoRadicalFijo:
                            nuevoElementoOperacion.OpcionElementosFijosRaiz = TipoOpcionElementosFijosOperacionRaiz.RaizOperandoRadicalFijo;
                            break;
                    }
                    break;

                case TipoElementoOperacion.Logaritmo:
                    switch (Operacion.OpcionElementosFijosLogaritmo)
                    {
                        case TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos:
                            nuevoElementoOperacion.OpcionElementosFijosLogaritmo = TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos;
                            break;

                        case TipoOpcionElementosFijosOperacionLogaritmo.BaseFijaArgumentoOperando:
                            nuevoElementoOperacion.OpcionElementosFijosLogaritmo = TipoOpcionElementosFijosOperacionLogaritmo.BaseFijaArgumentoOperando;
                            break;

                        case TipoOpcionElementosFijosOperacionLogaritmo.BaseOperandoArgumentoFijo:
                            nuevoElementoOperacion.OpcionElementosFijosLogaritmo = TipoOpcionElementosFijosOperacionLogaritmo.BaseOperandoArgumentoFijo;
                            break;
                    }
                    break;

                case TipoElementoOperacion.Inverso:
                    switch (Operacion.OpcionElementosFijosInverso)
                    {
                        case TipoOpcionElementosFijosOperacionInverso.InversoSumaResta:
                            nuevoElementoOperacion.OpcionElementosFijosInverso = TipoOpcionElementosFijosOperacionInverso.InversoSumaResta;
                            break;

                        case TipoOpcionElementosFijosOperacionInverso.InversoMultiplicacionDivision:
                            nuevoElementoOperacion.OpcionElementosFijosInverso = TipoOpcionElementosFijosOperacionInverso.InversoMultiplicacionDivision;
                            break;
                    }
                    break;
            }

            nuevoElementoOperacion.Nombre = nuevaOperacion.nombreOpcion.Text;
            nuevoElementoOperacion.Actualizar_ToolTips = true;

            Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoElementoOperacion = nuevoElementoOperacion;

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

            //if (itemElemento.ToolTip != null)
            //    nuevoCalculo.botonFondo.ToolTip = itemElemento.ToolTip;
            //else
            //{
            //Ventana.ObtenerEjecucionToolTips(Calculo).AgregarToolTipSubElemento_Asociaciones((ToolTipElementoVisual)((ContentControl)nuevaOperacion.botonFondo.ToolTip).Content,
            //    nuevoElementoOperacion.ID);
            //    Calculo.ConCambiosVisuales = true;
            //}
        }

        private void QuitarTodasLineas()
        {
            List<UIElement> lineas = (from UIElement L in diagrama.Children where L.GetType() == typeof(ArrowLine) select L).ToList();
            foreach (var itemLinea in lineas)
                diagrama.Children.Remove(itemLinea);
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
        private void btnQuitarOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Salida) return;

                if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Entrada)
                {
                    if (EntradaSeleccionada != null)
                    {
                        MessageBoxResult resp = MessageBox.Show("¿Quitar esta variable o vector de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resp == MessageBoxResult.Yes)
                        {
                            QuitarElementoDiagrama(EntradaSeleccionada.DiseñoElementoOperacion);
                            //Calculo.ListaSubEntradas_Visuales.Remove(EntradaSeleccionada);
                            //Calculo.ConCambiosVisuales = true;

                            bool contieneSalida = false;
                            if (EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida)
                            {
                                //QuitarElementoSalida(EntradaSeleccionada.DiseñoElementoOperacion);
                                contieneSalida = true;
                            }

                            //foreach (var item in Operacion.ElementosDiseñoOperacion)
                            //    QuitarDeElementosPosterioresAnteriores(item, EntradaSeleccionada.DiseñoElementoOperacion);

                            //Operacion.ElementosDiseñoOperacion.Remove(EntradaSeleccionada.DiseñoElementoOperacion);

                            //ActualizarContenedoresElementos(EntradaSeleccionada.DiseñoElementoOperacion);

                            //diagrama.Children.Remove(EntradaSeleccionada);
                            EntradaSeleccionada = null;

                            if (contieneSalida)
                                DibujarElementosDiseñoOperacion();

                            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Ninguno;

                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.FlujoOperacion)
                {
                    if (FlujoOperacionSeleccionado != null)
                    {
                        MessageBoxResult resp = MessageBox.Show("¿Quitar esta variable o vector de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resp == MessageBoxResult.Yes)
                        {
                            QuitarElementoDiagrama(FlujoOperacionSeleccionado.DiseñoElementoOperacion);
                            //Calculo.ListaSubFlujoOperaciones_Visuales.Remove(FlujoOperacionSeleccionado);
                            //Calculo.ConCambiosVisuales = true;

                            bool contieneSalida = false;
                            if (FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida)
                            {
                                //QuitarElementoSalida(FlujoOperacionSeleccionado.DiseñoElementoOperacion);
                                contieneSalida = true;
                            }

                            //foreach (var item in Operacion.ElementosDiseñoOperacion)
                            //    QuitarDeElementosPosterioresAnteriores(item, FlujoOperacionSeleccionado.DiseñoElementoOperacion);

                            //Operacion.ElementosDiseñoOperacion.Remove(FlujoOperacionSeleccionado.DiseñoElementoOperacion);

                            //ActualizarContenedoresElementos(FlujoOperacionSeleccionado.DiseñoElementoOperacion);

                            //diagrama.Children.Remove(FlujoOperacionSeleccionado);
                            FlujoOperacionSeleccionado = null;

                            if (contieneSalida)
                                DibujarElementosDiseñoOperacion();

                            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Ninguno;

                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();

                            ActualizarListasElementosSalida_PestañaRelacionada(Operacion);
                        }
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.OpcionOperacion)
                {
                    if (OpcionOperacionSeleccionado != null)
                    {
                        MessageBoxResult resp = MessageBox.Show("¿Quitar esta variable o vector de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resp == MessageBoxResult.Yes)
                        {
                            QuitarElementoDiagrama(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                            //Calculo.ListaSubOperaciones_Visuales.Remove(OpcionOperacionSeleccionado);
                            //Calculo.ConCambiosVisuales = true;

                            bool contieneSalida = false;
                            if (OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida)
                            {
                                //QuitarElementoSalida(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                                contieneSalida = true;
                            }

                            //foreach (var item in Operacion.ElementosDiseñoOperacion)
                            //    QuitarDeElementosPosterioresAnteriores(item, OpcionOperacionSeleccionado.DiseñoElementoOperacion);

                            //Operacion.ElementosDiseñoOperacion.Remove(OpcionOperacionSeleccionado.DiseñoElementoOperacion);

                            //ActualizarContenedoresElementos(OpcionOperacionSeleccionado.DiseñoElementoOperacion);

                            //diagrama.Children.Remove(OpcionOperacionSeleccionado);
                            OpcionOperacionSeleccionado = null;

                            if (contieneSalida)
                                DibujarElementosDiseñoOperacion();

                            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Ninguno;

                            DibujarTodasLineasElementos();
                            EstablecerIndicesProfundidadElementos();
                        }
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Nota)
                {
                    if (NotaSeleccionada != null)
                    {
                        MessageBoxResult resp = MessageBox.Show("¿Quitar esta nota de forma permanente?", "Quitar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resp == MessageBoxResult.Yes)
                        {
                            QuitarElementoDiagrama(NotaSeleccionada.DiseñoElementoOperacion);
                            //Operacion.ElementosDiseñoOperacion.Remove(NotaSeleccionada.DiseñoElementoOperacion);
                            //ActualizarContenedoresElementos(NotaSeleccionada.DiseñoElementoOperacion);

                            //diagrama.Children.Remove(NotaSeleccionada);
                            NotaSeleccionada = null;

                            TipoElementoDiseñoOperacionSeleccionado = TipoElementoDiseñoOperacion.Ninguno;
                        }
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Linea)
                {
                    if (lineaSeleccionada != null)
                    {
                        if (ElementoAnteriorLineaSeleccionada != null & ElementoPosteriorLineaSeleccionada != null)
                        {
                            QuitarReferenciasElementos_DefinicionNombresCantidades(ElementoAnteriorLineaSeleccionada, ElementoPosteriorLineaSeleccionada);

                            foreach (var itemCondicion in ElementoPosteriorLineaSeleccionada.ProcesamientoCantidades)
                                itemCondicion.QuitarSubElemento(ElementoAnteriorLineaSeleccionada);

                            ElementoAnteriorLineaSeleccionada.ElementosPosteriores.Remove(ElementoPosteriorLineaSeleccionada);
                            ElementoPosteriorLineaSeleccionada.ElementosAnteriores.Remove(ElementoAnteriorLineaSeleccionada);
                            ElementoAnteriorLineaSeleccionada.ElementosContenedoresOperacion.Remove(ElementoPosteriorLineaSeleccionada);

                            ElementoAnteriorLineaSeleccionada.QuitarOrdenOperando(ElementoPosteriorLineaSeleccionada);

                        }

                        DibujarTodasLineasElementos();
                        EstablecerIndicesProfundidadElementos();
                    }
                }

                diagrama_MouseLeftButtonDown(this, null);
            }
            else
            {
                foreach (var itemElemento in ElementosSeleccionados)
                {
                    QuitarElementoDiagrama(itemElemento);
                }

                DibujarElementosDiseñoOperacion();

                diagrama_MouseLeftButtonDown(this, null);
            }

            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);

        }

        private void QuitarDeAsociacionesProcesamientoCantidades(DiseñoElementoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                foreach (var itemCondicion in itemAnterior.ProcesamientoCantidades)
                    itemCondicion.QuitarSubElemento(elemento);
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                foreach (var itemCondicion in itemPosterior.ProcesamientoCantidades)
                    itemCondicion.QuitarSubElemento(elemento);
            }

            foreach (var itemCondicion in elemento.ProcesamientoCantidades)
                itemCondicion.QuitarSubElemento(elemento);

            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                foreach (var itemCondicion in itemAnterior.ProcesamientoTextosInformacion)
                    itemCondicion.QuitarSubElemento(elemento);
            }

            foreach (var itemPosterior in elemento.ElementosPosteriores)
            {
                foreach (var itemCondicion in itemPosterior.ProcesamientoTextosInformacion)
                    itemCondicion.QuitarSubElemento(elemento);
            }

            foreach (var itemCondicion in elemento.ProcesamientoTextosInformacion)
                itemCondicion.QuitarSubElemento(elemento);
        }

        public void ActualizarContenedoresElementos(DiseñoElementoOperacion elemento)
        {
            foreach (var itemElemento in Operacion.ElementosDiseñoOperacion)
            {
                if (itemElemento.ElementosContenedoresOperacion.Contains(elemento))
                    itemElemento.ElementosContenedoresOperacion.Remove(elemento);
            }
        }

        public void ActualizarListasElementosSalida_PestañaRelacionada(DiseñoOperacion elemento)
        {
            var tabs = (from TabItem T in Ventana.contenido.Items
                        where T.Content != null && T.Content.GetType() == typeof(VistaDiseñoOperacion) &&
((VistaDiseñoOperacion)T.Content).Operacion.ElementosPosteriores.Contains(elemento)
                        select T).ToList();

            foreach (var itemTab in tabs)
            {
                foreach (var itemElemento in ((VistaDiseñoOperacion)itemTab.Content).Operacion.ElementosDiseñoOperacion)
                {
                    if (itemElemento.ContieneSalida &&
                        ((VistaDiseñoOperacion)itemTab.Content).ElementoSeleccionado == itemElemento)
                    {
                        ((VistaDiseñoOperacion)itemTab.Content).MostrarInfo_Elemento(itemElemento);
                    }
                }
            }
        }

        public void QuitarDeElementosPosterioresAnteriores(DiseñoElementoOperacion elemento, DiseñoElementoOperacion elementoAQuitar)
        {
            var item = (from DiseñoElementoOperacion E in elemento.ElementosAnteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                elemento.ElementosAnteriores.Remove(item);
                item.QuitarOrdenOperando(elemento);
            }

            item = (from DiseñoElementoOperacion E in elemento.ElementosPosteriores where E == elementoAQuitar select E).FirstOrDefault();
            if (item != null)
            {
                elemento.ElementosPosteriores.Remove(item);
                elemento.QuitarOrdenOperando(item);
            }
        }

        public void QuitarReferenciasElementos_DefinicionNombresCantidades(DiseñoElementoOperacion elemento, DiseñoElementoOperacion elementoReferencias)
        {
            List<OpcionesNombreCantidad_TextosInformacion> elementosAquitar = new List<OpcionesNombreCantidad_TextosInformacion>();

            foreach (var itemDefinicion in elementoReferencias.DefinicionOpcionesNombresCantidades.OpcionesTextos)
            {
                if (itemDefinicion.OperandoSubElemento == elemento)
                {
                    elementosAquitar.Add(itemDefinicion);
                }
                else
                {
                    if (itemDefinicion.Condiciones.OperandoSubElemento_Condicion == elemento)
                        itemDefinicion.Condiciones = null;
                    else
                    {
                        itemDefinicion.Condiciones.QuitarCondicionSubElementoDiseñoCondicion_Condiciones(elemento);
                    }
                }
            }

            while (elementosAquitar.Any())
            {
                elementoReferencias.DefinicionOpcionesNombresCantidades.OpcionesTextos.Remove(elementosAquitar.FirstOrDefault());
                elementosAquitar.Remove(elementosAquitar.FirstOrDefault());
            }
        }

        public void QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(DiseñoElementoOperacion elemento, DiseñoElementoOperacion elementoReferencias)
        {
            foreach (var itemCondicion in elementoReferencias.CondicionesTextosInformacion_SeleccionOrdenamiento)
            {
                if (itemCondicion.SubOperandos_AplicarCondiciones.Contains(elemento))
                {
                    itemCondicion.SubOperandos_AplicarCondiciones.Remove(elemento);
                }

                if (itemCondicion.Condiciones.OperandoSubElemento_Condicion_TextosInformacion == elemento)
                    itemCondicion.Condiciones.OperandoSubElemento_Condicion_TextosInformacion = null;

                if (itemCondicion.Condiciones.OperandoSubElemento_Valores_TextosInformacion == elemento)
                    itemCondicion.Condiciones.OperandoSubElemento_Valores_TextosInformacion = null;

                if (itemCondicion.Condiciones.SubOperandos_AplicarCondiciones.Contains(elemento))
                    itemCondicion.Condiciones.SubOperandos_AplicarCondiciones.Remove(elemento);

                itemCondicion.Condiciones.QuitarCondicionSubElementoDiseñoCondicion_Condiciones(elemento);

            }

            if (elementoReferencias.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elemento))
                elementoReferencias.SalidasAgrupamiento_SeleccionOrdenamiento.Remove(elemento);

            foreach (var itemAsignacion in elementoReferencias.AsociacionesTextosInformacion_ElementosSalida)
            {
                if (itemAsignacion.ElementoSalida == elemento)
                    itemAsignacion.ElementoSalida = null;
            }
        }

        public void QuitarReferenciasElementos_CondicionesFlujo(DiseñoElementoOperacion elemento, DiseñoElementoOperacion elementoReferencias)
        {
            foreach (var itemCondicion in elementoReferencias.CondicionesFlujo_SeleccionOrdenamiento)
            {
                itemCondicion.QuitarCondicionSubElementoDiseñoCondicion_Condiciones(elemento);
            }

            if (elementoReferencias.SalidasAgrupamiento_CondicionFlujo.Contains(elemento))
                elementoReferencias.SalidasAgrupamiento_CondicionFlujo.Remove(elemento);

            foreach (var itemAsignacion in elementoReferencias.AsociacionesCondicionFlujo_ElementosSalida)
            {
                if (itemAsignacion.ElementoSalida == elemento)
                    itemAsignacion.ElementoSalida = null;
            }

            foreach (var itemAsignacion in elementoReferencias.AsociacionesCondicionFlujo_ElementosSalida2)
            {
                if (itemAsignacion.ElementoSalida == elemento)
                    itemAsignacion.ElementoSalida = null;
            }
        }

        private void btnEsSalida_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Salida |
                    TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Nota) return;

                if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.Entrada)
                {
                    if (EntradaSeleccionada != null && EntradaSeleccionada.DiseñoElementoOperacion != null)
                    {
                        if ((from E in EntradaSeleccionada.DiseñoElementoOperacion.ElementosPosteriores where E.Tipo != TipoElementoDiseñoOperacion.Salida select E).ToList().Count == 0)
                        {
                            EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida = !EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida;

                            if (EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida)
                            {
                                AgregarElementoSalida(EntradaSeleccionada.DiseñoElementoOperacion);
                            }
                            else
                            {
                                QuitarElementoSalida(EntradaSeleccionada.DiseñoElementoOperacion);
                            }

                            EstablecerTextoBotonSalida(EntradaSeleccionada.DiseñoElementoOperacion.ContieneSalida);
                            MostrarInfo_Elemento(EntradaSeleccionada.DiseñoElementoOperacion);
                        }
                        else
                            MessageBox.Show("Esta variable o vector de entrada tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado.", "Variable o vector retornado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.FlujoOperacion)
                {
                    if (FlujoOperacionSeleccionado != null && FlujoOperacionSeleccionado.DiseñoElementoOperacion != null)
                    {
                        if ((from E in FlujoOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores where E.Tipo != TipoElementoDiseñoOperacion.Salida select E).ToList().Count == 0)
                        {
                            FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida = !FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida;

                            if (FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida)
                            {
                                AgregarElementoSalida(FlujoOperacionSeleccionado.DiseñoElementoOperacion);
                            }
                            else
                            {
                                QuitarElementoSalida(FlujoOperacionSeleccionado.DiseñoElementoOperacion);
                            }

                            EstablecerTextoBotonSalida(FlujoOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida);
                            MostrarInfo_Elemento(FlujoOperacionSeleccionado.DiseñoElementoOperacion);

                            ActualizarListasElementosSalida_PestañaRelacionada(Operacion);
                        }
                        else
                            MessageBox.Show("Esta variable o vector tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado.", "Variable o vector retornado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else if (TipoElementoDiseñoOperacionSeleccionado == TipoElementoDiseñoOperacion.OpcionOperacion)
                {
                    if (OpcionOperacionSeleccionado != null && OpcionOperacionSeleccionado.DiseñoElementoOperacion != null)
                    {
                        if (OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
                            OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                            OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.CondicionesFlujo |
                            OpcionOperacionSeleccionado.Tipo == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
                        {
                            agregarNuevoElementoSalida_Click(this, e);
                        }
                        else
                        {
                            if ((from E in OpcionOperacionSeleccionado.DiseñoElementoOperacion.ElementosPosteriores where E.Tipo != TipoElementoDiseñoOperacion.Salida select E).ToList().Count == 0)
                            {
                                OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida = !OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida;

                                if (OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida)
                                {
                                    AgregarElementoSalida(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                                }
                                else
                                {
                                    QuitarElementoSalida(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                                }

                                EstablecerTextoBotonSalida(OpcionOperacionSeleccionado.DiseñoElementoOperacion.ContieneSalida);
                                MostrarInfo_Elemento(OpcionOperacionSeleccionado.DiseñoElementoOperacion);
                            }
                            else
                                MessageBox.Show("Esta variable o vector tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado.", "Variable o vector retornado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                }
            }
            else
            {
                foreach (var itemElemento in ElementosSeleccionados)
                {
                    if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Salida |
                    itemElemento.Tipo == TipoElementoDiseñoOperacion.Nota) return;

                    if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Entrada)
                    {
                        if (itemElemento != null)
                        {
                            if ((from E in itemElemento.ElementosPosteriores where E.Tipo != TipoElementoDiseñoOperacion.Salida select E).ToList().Count == 0)
                            {
                                itemElemento.ContieneSalida = !itemElemento.ContieneSalida;

                                if (itemElemento.ContieneSalida)
                                {
                                    AgregarElementoSalida(itemElemento);
                                }
                                else
                                {
                                    QuitarElementoSalida(itemElemento);
                                }

                                EstablecerTextoBotonSalida(itemElemento.ContieneSalida);
                                MostrarInfo_Elemento(itemElemento);
                            }
                            else
                                MessageBox.Show("La variable o vector '" + itemElemento.Nombre + "' tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado.", "Variable o vector retornado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion)
                    {
                        if (itemElemento != null)
                        {
                            if ((from E in itemElemento.ElementosPosteriores where E.Tipo != TipoElementoDiseñoOperacion.Salida select E).ToList().Count == 0)
                            {
                                itemElemento.ContieneSalida = !itemElemento.ContieneSalida;

                                if (itemElemento.ContieneSalida)
                                {
                                    AgregarElementoSalida(itemElemento);
                                }
                                else
                                {
                                    QuitarElementoSalida(itemElemento);
                                }

                                EstablecerTextoBotonSalida(itemElemento.ContieneSalida);
                                MostrarInfo_Elemento(itemElemento);
                            }
                            else
                                MessageBox.Show("La variable o vector '" + itemElemento.Nombre + "' tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado.", "Variable o vector retornado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion)
                    {
                        if (itemElemento != null)
                        {
                            if (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
                            itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                            itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                            itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
                            {
                                if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                    itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                                {
                                    AgregarElementoSalida_AgrupadoSeleccionOrdenamiento(itemElemento);
                                }
                                else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                        (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                    itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                                {
                                    AgregarElementoSalida_AgrupadoSeleccionCondiciones(itemElemento);
                                }
                            }
                            else
                            {
                                if ((from E in itemElemento.ElementosPosteriores where E.Tipo != TipoElementoDiseñoOperacion.Salida select E).ToList().Count == 0)
                                {
                                    itemElemento.ContieneSalida = !itemElemento.ContieneSalida;

                                    if (itemElemento.ContieneSalida)
                                    {
                                        AgregarElementoSalida(itemElemento);
                                    }
                                    else
                                    {
                                        QuitarElementoSalida(itemElemento);
                                    }

                                    EstablecerTextoBotonSalida(itemElemento.ContieneSalida);
                                    MostrarInfo_Elemento(itemElemento);
                                }
                                else
                                    MessageBox.Show("La variable o vector '" + itemElemento.Nombre + "' tiene variables o vectores posteriores por lo que no se puede establecer como variable o vector retornado.", "Variable o vector retornado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                    }
                }
            }

            DibujarElementosDiseñoOperacion();
            Ventana.ActualizarPestañasDefinicionOperacion_TextosInformacion(Operacion);
        }

        public void EstablecerTextoBotonSalida(bool esSalida)
        {
            if (esSalida)
                etiquetaBtnEsSalida.Content = "Quitar variable o vector retornado";
            else
                etiquetaBtnEsSalida.Content = "Agregar variable o vector retornado";
        }

        public void AgregarElementoSalida(DiseñoElementoOperacion elemento)
        {
            DiseñoElementoOperacion elementoSalida = new DiseñoElementoOperacion();
            elementoSalida.ID = App.GenerarID_Elemento();
            elementoSalida.ElementosAnteriores.Add(elemento);
            //elementoSalida.ElementosContenedoresOperacion.Add(elemento);
            elementoSalida.Nombre = "Variable o vector retornado";
            elementoSalida.PosicionX = elemento.PosicionX + elemento.Anchura + 30;
            elementoSalida.PosicionY = elemento.PosicionY + elemento.Altura;
            elementoSalida.Tipo = TipoElementoDiseñoOperacion.Salida;

            Operacion.ElementosDiseñoOperacion.Add(elementoSalida);
            elemento.ElementosPosteriores.Add(elementoSalida);
        }

        public void QuitarElementoSalida(DiseñoElementoOperacion elemento)
        {
            DiseñoElementoOperacion elementoSalida = (from DiseñoElementoOperacion S in elemento.ElementosPosteriores where S.Tipo == TipoElementoDiseñoOperacion.Salida select S).FirstOrDefault();

            Operacion.ElementosDiseñoOperacion.Remove(elementoSalida);
            elemento.ElementosPosteriores.Remove(elementoSalida);

            elemento.ElementoSalidaOperacion_Agrupamiento = null;
            elemento.ElementoInternoSalidaOperacion_Agrupamiento = null;
        }

        public void MostrarOrdenOperando_Elemento(DiseñoElementoOperacion elemento)
        {
            contenedorOrdenOperandos.Content = null;

            if (elemento != null && elemento.ElementosAnteriores.Any())
            {
                if (MostrandoOrdenOperandos)
                {
                    ListarOperandos_Vacio();
                    ListarOperandos();
                }

                DibujarTodasLineasElementos();
            }
            //else
            //{
            //    MostrandoOrdenOperandos = false;
            //    contenedorOrdenOperandos.Visibility = Visibility.Collapsed;
            //}


            //diagrama.UpdateLayout();
        }

        private void btnDefinirOrdenOperandos_Click(object sender, RoutedEventArgs e)
        {
            MostrandoOrdenOperandos = !MostrandoOrdenOperandos;

            if (MostrandoOrdenOperandos)
            {
                if (!(ElementoSeleccionado != null && ElementoSeleccionado.ElementosAnteriores.Count > 0))
                {
                    MostrandoOrdenOperandos = false;
                }
            }

            if (MostrandoOrdenOperandos)
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

                    if (i == 2 && Operacion != null && ElementoSeleccionado != null &&
                        ((Operacion.Tipo == TipoElementoOperacion.Potencia &&
                        ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (Operacion.Tipo == TipoElementoOperacion.Raiz &&
                        ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                        Operacion.Tipo == TipoElementoOperacion.Porcentaje |
                        (Operacion.Tipo == TipoElementoOperacion.Logaritmo &&
                        ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)))
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
            tituloOperandos.Text = "Orden de las variable o vectores";

            ordenOperandos.Children.Add(tituloOperandos);

            Grid.SetRow(tituloOperandos, 0);
            Grid.SetColumn(tituloOperandos, 0);
            Grid.SetColumnSpan(tituloOperandos, Operacion != null && ElementoSeleccionado != null &&
                ((Operacion.Tipo == TipoElementoOperacion.Potencia &&
                        ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (Operacion.Tipo == TipoElementoOperacion.Raiz &&
                        ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                Operacion.Tipo == TipoElementoOperacion.Porcentaje |
                (Operacion.Tipo == TipoElementoOperacion.Logaritmo &&
                ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) ? 4 : 3);

            contenedorOrdenOperandos.Content = ordenOperandos;
        }

        private void ListarOperandos()
        {
            if (ElementoSeleccionado == null) return;

            List<DiseñoElementoOperacion> operandos = (from E in ElementoSeleccionado.ElementosAnteriores
                                                       select E).OrderBy((j) => j.OrdenOperandos.Where((a) => a.ElementoPadre == ElementoSeleccionado).FirstOrDefault().Orden).ToList();

            //ordenOperandos.RowDefinitions.Clear();
            //ordenOperandos.Children.Clear();

            Grid ordenOperandos = new Grid();

            for (int i = 1; i <= 5; i++)
            {
                ColumnDefinition columna = new ColumnDefinition();
                if (i < 3)
                {
                    columna.Width = GridLength.Auto;

                    if (i == 2 && ElementoSeleccionado != null &&
                        ((Operacion.Tipo == TipoElementoOperacion.Potencia &&
                        ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (Operacion.Tipo == TipoElementoOperacion.Raiz &&
                        ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                        Operacion.Tipo == TipoElementoOperacion.Porcentaje |
                        (Operacion.Tipo == TipoElementoOperacion.Logaritmo &&
                        ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)))
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
            Grid.SetColumnSpan(tituloOperandos, (ElementoSeleccionado != null && (Operacion.Tipo == TipoElementoOperacion.Potencia &&
                        ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (Operacion.Tipo == TipoElementoOperacion.Raiz &&
                        ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                Operacion.Tipo == TipoElementoOperacion.Porcentaje |
                (Operacion.Tipo == TipoElementoOperacion.Logaritmo &&
                ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)) ? 4 : 3);

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

                if (ElementoSeleccionado != null && ((Operacion.Tipo == TipoElementoOperacion.Potencia &&
                        ElementoSeleccionado.OpcionElementosFijosPotencia == TipoOpcionElementosFijosOperacionPotencia.BaseExponenteOperandos) |
                        (Operacion.Tipo == TipoElementoOperacion.Raiz &&
                        ElementoSeleccionado.OpcionElementosFijosRaiz == TipoOpcionElementosFijosOperacionRaiz.RaizRadicalOperandos) |
                    Operacion.Tipo == TipoElementoOperacion.Porcentaje |
                    (Operacion.Tipo == TipoElementoOperacion.Logaritmo &&
                    ElementoSeleccionado.OpcionElementosFijosLogaritmo == TipoOpcionElementosFijosOperacionLogaritmo.BaseArgumentoOperandos)))
                {
                    if (basePotencia_raizRadical)
                    {
                        if (Operacion.Tipo == TipoElementoOperacion.Potencia)
                            strBasePotencia = "Base";
                        else if (Operacion.Tipo == TipoElementoOperacion.Raiz)
                            strBasePotencia = "Raíz";
                        else if (Operacion.Tipo == TipoElementoOperacion.Porcentaje)
                            strBasePotencia = "Porcentaje a extraer";
                        else if (Operacion.Tipo == TipoElementoOperacion.Logaritmo)
                            strBasePotencia = "Base";
                        basePotencia_raizRadical = false;
                    }
                    else
                    {
                        if (Operacion.Tipo == TipoElementoOperacion.Potencia)
                            strBasePotencia = "Exponente";
                        else if (Operacion.Tipo == TipoElementoOperacion.Raiz)
                            strBasePotencia = "Radical";
                        else if (Operacion.Tipo == TipoElementoOperacion.Porcentaje)
                            strBasePotencia = "Cantidad de donde se extraerá";
                        else if (Operacion.Tipo == TipoElementoOperacion.Logaritmo)
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
                if (item.Tipo == TipoElementoDiseñoOperacion.Entrada)
                    nombreOperando.Text = item.EntradaRelacionada.Nombre;
                else
                    nombreOperando.Text = item.NombreCombo;

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
                botonOrdenar.Tag = new object[] { item, ElementoSeleccionado };
                botonOrdenar.Margin = new Thickness(10);
                botonOrdenar.Content = "Ordenar números"; //ImagenBotonOrdenar;
                botonOrdenar.Click += ClickBotonOrdenar;

                ordenOperandos.Children.Add(botonOrdenar);

                Grid.SetRow(botonOrdenar, indiceFila);
                Grid.SetColumn(botonOrdenar, indiceColumna);
                indiceColumna++;

                if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.TodosSeparados)
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
            DiseñoElementoOperacion elemento = (DiseñoElementoOperacion)((CheckBox)sender).Tag;

            if (elemento != null)
            {
                elemento.NoConsiderarEjecucion = (bool)((CheckBox)sender).IsChecked;
            }
        }

        private void NoConsiderarEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            DiseñoElementoOperacion elemento = (DiseñoElementoOperacion)((CheckBox)sender).Tag;

            if (elemento != null)
            {
                elemento.NoConsiderarEjecucion = (bool)((CheckBox)sender).IsChecked;
            }
        }

        private void BotonAgrupar_Click(object sender, RoutedEventArgs e)
        {
            EstablecerAgrupacionesOperandos_PorSeparado_Operacion agrupaciones = new EstablecerAgrupacionesOperandos_PorSeparado_Operacion();
            agrupaciones.OperacionElementoRelacionado = ElementoSeleccionado;
            agrupaciones.Operando = (DiseñoElementoOperacion)((Button)sender).Tag;

            bool agrupar = (bool)agrupaciones.ShowDialog();

            if (agrupar)
            {
                var agrupacionRelacionada = agrupaciones.Operando.AgrupacionesOperandos_PorSeparado.Where(i => i.OperacionElementoRelacionado == agrupaciones.OperacionElementoRelacionado).FirstOrDefault();

                if (!string.IsNullOrEmpty(agrupaciones.NombreAgrupacion))
                {
                    if (agrupacionRelacionada == null)
                    {
                        agrupaciones.Operando.AgrupacionesOperandos_PorSeparado.Add(new AgrupacionOperando_PorSeparado()
                        {
                            NombreAgrupacion = agrupaciones.NombreAgrupacion,
                            OperacionElementoRelacionado = agrupaciones.OperacionElementoRelacionado
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
                            while(itemOperando.AgrupacionesAsignadasOperandos_PorSeparado.Any(i => i.Agrupacion == agrupacionRelacionada))
                                itemOperando.AgrupacionesAsignadasOperandos_PorSeparado.Remove(
                                    itemOperando.AgrupacionesAsignadasOperandos_PorSeparado.FirstOrDefault(i => i.Agrupacion == agrupacionRelacionada));
                        }
                    }
                }

                MostrarInfo_Elemento(ElementoSeleccionado);
            }
        }

        private void ClickBotonSubir(object sender, RoutedEventArgs e)
        {
            List<DiseñoElementoOperacion> operandos = (from E in ElementoSeleccionado.ElementosAnteriores
                                                       select E).OrderBy((j) => j.OrdenOperandos.Where((a) => a.ElementoPadre == ElementoSeleccionado).FirstOrDefault().Orden).ToList();

            DiseñoElementoOperacion operandoAnterior = null;
            DiseñoElementoOperacion operando = null;
            foreach (var item in operandos)
            {
                if (item == ((Button)sender).Tag)
                {
                    operando = item;
                    break;
                }
                operandoAnterior = item;
            }

            if (operandoAnterior != null)
            {
                OrdenOperando_Elemento ordenOperando = (from O in operando.OrdenOperandos where O.ElementoPadre == ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden -= 1;
                }

                ordenOperando = (from O in operandoAnterior.OrdenOperandos where O.ElementoPadre == ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden += 1;
                }
            }

            DibujarElementosDiseñoOperacion();
            ListarOperandos();
        }

        private void ClickBotonBajar(object sender, RoutedEventArgs e)
        {
            List<DiseñoElementoOperacion> operandos = (from E in ElementoSeleccionado.ElementosAnteriores
                                                       select E).OrderByDescending((j) => j.OrdenOperandos.Where((a) => a.ElementoPadre == ElementoSeleccionado).FirstOrDefault().Orden).ToList();

            DiseñoElementoOperacion operandoPosterior = null;
            DiseñoElementoOperacion operando = null;
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
                OrdenOperando_Elemento ordenOperando = (from O in operando.OrdenOperandos where O.ElementoPadre == ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden += 1;
                }

                ordenOperando = (from O in operandoPosterior.OrdenOperandos where O.ElementoPadre == ElementoSeleccionado select O).FirstOrDefault();
                if (ordenOperando != null)
                {
                    ordenOperando.Orden -= 1;
                }
            }

            DibujarElementosDiseñoOperacion();
            ListarOperandos();
        }

        private void ClickBotonOrdenar(object sender, RoutedEventArgs e)
        {
            object[] objeto = (object[])((Button)sender).Tag;
            if (objeto != null)
            {
                if (objeto[0] != null && objeto[1] != null)
                {
                    OrdenarNumerosOperando ordenar = new OrdenarNumerosOperando();
                    ordenar.SubOperando = (DiseñoElementoOperacion)objeto[0];
                    ordenar.SubOperacionSeleccionada = (DiseñoElementoOperacion)objeto[1];
                    ordenar.ShowDialog();
                }
            }
        }
        private void btnBuscarOperaciones_Click(object sender, RoutedEventArgs e)
        {
            if (!BuscandoOpcionesOperacion)
            {
                btnBuscarOperaciones.Content = "Cerrar";
                BuscandoOpcionesOperacion = true;
            }
            else
            {
                btnBuscarOperaciones.Content = "Buscar";
                BuscandoOpcionesOperacion = false;
            }

            ListarEntradas();
            ListarOpcionesOperacion();
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
                                if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                                {
                                    //((EntradaDiseñoOperaciones)item).Background = SystemColors.HighlightTextBrush;
                                    //((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.HighlightTextBrush;
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((EntradaDiseñoOperaciones)item).DiseñoElementoOperacion);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(EntradaFlujoOperacion))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((EntradaFlujoOperacion)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((EntradaFlujoOperacion)item).DiseñoElementoOperacion);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(OpcionOperacion))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((OpcionOperacion)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((OpcionOperacion)item).DiseñoElementoOperacion);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(NotaDiagrama))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((NotaDiagrama)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((NotaDiagrama)item).DiseñoElementoOperacion);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                                else if (item.GetType() == typeof(OpcionSalida))
                                {
                                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                                    {
                                        Ventana.Dispatcher.Invoke(((OpcionSalida)item).Clic, DispatcherPriority.Loaded);
                                    }
                                    ElementosSeleccionados.Add(((OpcionSalida)item).DiseñoElementoOperacion);
                                    ElementosDiagramaSeleccionados.Add(item);
                                }
                            }
                        }
                        else
                        {
                            if (item.GetType() == typeof(EntradaDiseñoOperaciones))
                            {
                                ((EntradaDiseñoOperaciones)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((EntradaDiseñoOperaciones)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                ElementosSeleccionados.Remove(((EntradaDiseñoOperaciones)item).DiseñoElementoOperacion);
                            }
                            else if (item.GetType() == typeof(EntradaFlujoOperacion))
                            {
                                ((EntradaFlujoOperacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((EntradaFlujoOperacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                ElementosSeleccionados.Remove(((EntradaFlujoOperacion)item).DiseñoElementoOperacion);
                            }
                            else if (item.GetType() == typeof(OpcionOperacion))
                            {
                                ((OpcionOperacion)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((OpcionOperacion)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                ElementosSeleccionados.Remove(((OpcionOperacion)item).DiseñoElementoOperacion);
                            }
                            else if (item.GetType() == typeof(OpcionSalida))
                            {
                                ((OpcionSalida)item).Background = SystemColors.GradientInactiveCaptionBrush;
                                ((OpcionSalida)item).botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
                                ElementosSeleccionados.Remove(((OpcionSalida)item).DiseñoElementoOperacion);
                            }
                            else if (item.GetType() == typeof(NotaDiagrama))
                            {
                                ((NotaDiagrama)item).fondo.BorderThickness = new Thickness(0);
                                ElementosSeleccionados.Remove(((NotaDiagrama)item).DiseñoElementoOperacion);
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

        private void acumulacion_CambioSeleccion(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                if (ElementoSeleccionado != null)
                {
                    if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila |
                        ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados)
                    {
                        ElementoSeleccionado.ConAcumulacion = (acumulacion.IsChecked != null) ? (bool)acumulacion.IsChecked : false;
                    }
                }
            }
        }

        public void MostrarAcumulacion()
        {
            acumulacion.Visibility = Visibility.Collapsed;

            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                if (ElementoSeleccionado != null)
                {
                    if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila |
                        ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados)
                    {
                        acumulacion.IsChecked = ElementoSeleccionado.ConAcumulacion;
                        acumulacion.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void infoElemento_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.Info = infoElemento.Text;
            }
        }

        public void MostrarInfo_Elemento(DiseñoElementoOperacion elemento)
        {
            contenedorInformacion.Visibility = Visibility.Collapsed;
            opcionOperarFilasRestantes.Visibility = Visibility.Collapsed;
            agregarNuevoElementoSalida.Visibility = Visibility.Collapsed;
            agruparEnElementoSalida.Visibility = Visibility.Collapsed;
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
            agruparEnElementoSalida.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
            ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
            ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
            divisionZeroContinuar.Visibility = Visibility.Collapsed;
            opcionSeleccionManualCantidades.Visibility = Visibility.Collapsed;
            botonOpcionLimpiarDatos.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            definirAgrupacionesOperandosResultados.Visibility = Visibility.Collapsed;

            if (elemento != null && elemento.Tipo != TipoElementoDiseñoOperacion.Salida)
            {
                if (MostrandoInformacionElemento)
                {
                    contenedorInformacion.Visibility = Visibility.Visible;
                    infoElemento.Text = elemento.Info;
                    opcionPorcentajeRelativo.IsChecked = (bool)elemento.PorcentajeRelativo;
                    valorOpcionElementoFijo.Text = elemento.ValorOpcionElementosFijos.ToString();

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPorcentaje_PorFila |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPorcentaje_UnaSolaVez))
                        opcionPorcentajeRelativo.Visibility = Visibility.Visible;
                    else
                        opcionPorcentajeRelativo.Visibility = Visibility.Collapsed;

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPotencias_PorFila |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPotencias_UnaSolaVez))
                    {
                        opcionesElementosFijosPotencia.Visibility = Visibility.Visible;
                        opcionesElementosFijosPotencia.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosPotencia.Items where I.Uid == ((int)elemento.OpcionElementosFijosPotencia).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosPotencia.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosPotencia.SelectedItem = null;
                    }

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoRaices_PorFila |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoRaices_UnaSolaVez))
                    {
                        opcionesElementosFijosRaiz.Visibility = Visibility.Visible;
                        opcionesElementosFijosRaiz.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosRaiz.Items where I.Uid == ((int)elemento.OpcionElementosFijosRaiz).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosRaiz.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosRaiz.SelectedItem = null;
                    }

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoLogaritmo_PorFila |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoLogaritmo_UnaSolaVez))
                    {
                        opcionesElementosFijosLogaritmo.Visibility = Visibility.Visible;
                        opcionesElementosFijosLogaritmo.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosLogaritmo.Items where I.Uid == ((int)elemento.OpcionElementosFijosLogaritmo).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosLogaritmo.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosLogaritmo.SelectedItem = null;
                    }

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoInverso)
                    {
                        opcionesElementosFijosInverso.Visibility = Visibility.Visible;
                        opcionesElementosFijosInverso.SelectedItem = (from ComboBoxItem I in opcionesElementosFijosInverso.Items where I.Uid == ((int)elemento.OpcionElementosFijosInverso).ToString() select I).FirstOrDefault();
                    }
                    else
                    {
                        opcionesElementosFijosInverso.Visibility = Visibility.Collapsed;
                        opcionesElementosFijosInverso.SelectedItem = null;
                    }

                    if (Operacion.Tipo == TipoElementoOperacion.Division)
                    {
                        divisionZeroContinuar.IsChecked = (bool)elemento.DivisionZero_Continuar;
                        divisionZeroContinuar.Visibility = Visibility.Visible;
                    }

                    condicionesTextosInformacionOperandosResultados.Operandos = new List<DiseñoOperacion> { Operacion };
                    condicionesTextosInformacionOperandosResultados.SubOperandos = new List<DiseñoElementoOperacion> { elemento };
                    condicionesTextosInformacionOperandosResultados.SubOperandos.AddRange(elemento.ElementosAnteriores);

                    opcionTextosInformacionOperandosResultados.IsChecked = elemento.AsignarTextosInformacion_OperandosResultados;
                    opcionTextosInformacionCondicionesOperandosResultados.IsChecked = elemento.AsignarTextosInformacionCondiciones_OperandosResultados;

                    condicionesTextosInformacionOperandosResultados.SubOperandoSeleccionado = elemento;
                    condicionesTextosInformacionOperandosResultados.Condiciones = elemento.CondicionesTextosInformacionOperandosResultados;
                    condicionesTextosInformacionOperandosResultados.ListarCondiciones();

                    if (!elemento.NingunOperandoTextosInformacionOperandosResultados &
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

                    ListarOperandosTextosInformacionOperandosResultados(elemento);

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.PorFila |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados))
                    {
                        opcionOperarFilasRestantes.Visibility = Visibility.Visible;

                        if (!elemento.SeguirOperandoFilas_ConElementoNeutro &
                            !elemento.SeguirOperandoFilas_ConUltimoNumero)
                            opcionOperarFilasRestantes_ConCeros.IsChecked = true;

                        opcionOperarFilasRestantes_ConCeros.IsChecked = elemento.SeguirOperandoFilas_ConElementoNeutro;
                        opcionOperarFilasRestantes_ConUltimoNumero.IsChecked = elemento.SeguirOperandoFilas_ConUltimoNumero;
                    }

                    if (((elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))) ||
                        ((elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (elemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))))
                    {
                        agregarNuevoElementoSalida.Visibility = Visibility.Visible;
                    }

                    if (elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion)
                    {
                        opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.IsChecked = elemento.Ejecutar_SiTieneOtrosOperandosValidos;
                        opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones.Visibility = Visibility.Collapsed;
                    }

                    var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(Operacion);

                    if (definicionEncontrada != null &&
                        definicionEncontrada.Any())
                    {
                        if (definicionEncontrada.Any(i => i.VerificarEnCondiciones_DefinicionesTextosInformacion(elemento)))
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
                    }
                    else
                    {
                        //Grid.SetRowSpan(definirNombresCantidades, 2);
                        opcionUtilizarDefinicionAsignacionTextosInformacion.Visibility = Visibility.Collapsed;
                    }

                    if (elemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir)
                    {
                        opcionSeleccionManualCantidades.Visibility = Visibility.Visible;
                        opcionSeleccionManualCantidades.IsChecked = elemento.ModoSeleccionManual_SeleccionarOrdenar;
                    }

                    if (elemento.ElementosAnteriores.Any(i => i.AgrupacionesOperandos_PorSeparado.Any(j => j.OperacionElementoRelacionado == elemento)))
                    {
                        definirAgrupacionesOperandosResultados.Visibility = Visibility.Visible;
                    }

                    descripcionDefiniciones.Text = ObtenerDescripcionDefiniciones();
                    
                    if (!((elemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        elemento.TipoOpcionOperacion == TipoOpcionOperacion.TodosJuntos) ||
                        elemento.Tipo == TipoElementoDiseñoOperacion.Nota))
                    {
                        opcionesAgregarCantidadNumeros.Visibility = Visibility.Visible;
                        opcionAgregarCantidadNumerosCantidad.IsChecked = elemento.AgregarCantidadComoNumero;
                        opcionIncluirCantidadNumero.IsChecked = elemento.IncluirCantidadNumero;
                        opcionAgregarCantidadNumerosTextoInformacion.IsChecked = elemento.AgregarCantidadComoTextoInformacion;
                        opcionAgregarNombreTextoInformacion.IsChecked = elemento.AgregarNombreComoTextoInformacion;
                        opcionAgregarNumerosTextoInformacion.IsChecked = elemento.AgregarNumeroComoTextoInformacion;

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
                            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;

                            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor;
                            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor;
                            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                            if (ordenarAntesEjecucion_PorNombre.IsChecked == true)
                            {
                                opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Visible;
                                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Visible;

                                if (opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked == true)
                                {
                                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                                }
                            }

                            if (opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked == true)
                            {
                                opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                            }

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
                            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;

                            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor;
                            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor;
                            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = elemento.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                            if (ordenarDespuesEjecucion_PorNombre.IsChecked == true)
                            {
                                opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Visible;
                                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Visible;

                                if (opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked == true)
                                {
                                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                                }
                            }

                            if (opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked == true)
                            {
                                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                            }

                            if (opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked == true)
                            {
                                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                            }
                        }
                        else
                            opcionDespuesEjecucion.IsChecked = false;
                    }
                    else
                    {
                        opcionesAgregarCantidadNumeros.Visibility = Visibility.Collapsed;
                        opcionesOrdenarNumeros.Visibility = Visibility.Collapsed;
                    }

                }

                if (elemento.ContieneSalida)
                {
                    ListarElementosSalida_Agrupamiento(elemento);
                    agruparEnElementoSalida.Visibility = Visibility.Visible;
                }
                else
                {
                    agruparEnElementoSalida.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ListarOperandosTextosInformacionOperandosResultados(DiseñoElementoOperacion elemento)
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
                DiseñoElementoOperacion itemOperando = (DiseñoElementoOperacion)objetos[0];
                List<DiseñoElementoOperacion> Operandos = (List<DiseñoElementoOperacion>)objetos[1];

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
                DiseñoElementoOperacion itemOperando = (DiseñoElementoOperacion)objetos[0];
                List<DiseñoElementoOperacion> Operandos = (List<DiseñoElementoOperacion>)objetos[1];

                if (!Operandos.Contains(itemOperando))
                {
                    Operandos.Add(itemOperando);

                    Calculo.HayCambios = true;
                }
            }
        }

        private void btnInformacionElemento_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null && ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.Nota) return;

            MostrandoInformacionElemento = !MostrandoInformacionElemento;

            if (MostrandoInformacionElemento)
            {
                if (ElementoSeleccionado != null && ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.Salida)
                {
                    MostrandoInformacionElemento = false;
                }
            }

            if (MostrandoInformacionElemento && ElementoSeleccionado != null)
            {
                contenedorInformacion.Visibility = Visibility.Visible;

                opcionOperarFilasRestantes.Visibility = Visibility.Collapsed;
                agregarNuevoElementoSalida.Visibility = Visibility.Collapsed;

                if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila |
                        ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados))
                {
                    opcionOperarFilasRestantes.Visibility = Visibility.Visible;

                    if (!ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro &
                            !ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero)
                        opcionOperarFilasRestantes_ConCeros.IsChecked = true;

                    opcionOperarFilasRestantes_ConCeros.IsChecked = ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro;
                    opcionOperarFilasRestantes_ConUltimoNumero.IsChecked = ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero;
                }

                MostrarInfo_Elemento(ElementoSeleccionado);

            }
            else
            {
                contenedorInformacion.Visibility = Visibility.Collapsed;
                opcionOperarFilasRestantes.Visibility = Visibility.Collapsed;
                agregarNuevoElementoSalida.Visibility = Visibility.Collapsed;
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
            Operacion.AltoDiagrama = diagrama.Height;
            Operacion.AnchoDiagrama = diagrama.Width;
        }

        private void disminuirArea_Click(object sender, RoutedEventArgs e)
        {
            if (diagrama.ActualHeight - 300 > 0 &
                diagrama.ActualWidth - 300 > 0)
            {
                diagrama.Height = diagrama.ActualHeight - 300;
                diagrama.Width = diagrama.ActualWidth - 300;
                Operacion.AltoDiagrama = diagrama.Height;
                Operacion.AnchoDiagrama = diagrama.Width;
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

                if (elemento.GetType() == typeof(EntradaDiseñoOperaciones))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((EntradaDiseñoOperaciones)elemento).Clic);
                    }
                }

                if (elemento.GetType() == typeof(EntradaFlujoOperacion))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((EntradaFlujoOperacion)elemento).Clic);
                    }
                }

                if (elemento.GetType() == typeof(OpcionOperacion))
                {
                    if (!Ventana.Dispatcher.HasShutdownStarted &&
                            !Ventana.Dispatcher.HasShutdownFinished)
                    {
                        Ventana.Dispatcher.Invoke(((OpcionOperacion)elemento).Clic);
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
                else if (item.GetType() == typeof(EntradaFlujoOperacion))
                {
                    EntradaFlujoOperacion operacion = (EntradaFlujoOperacion)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (operacion.nombreEntradaFlujo.Text.ToLower().Contains(textoBusqueda.ToLower()))
                            elementos.Add(operacion);
                    }
                }
                else if (item.GetType() == typeof(OpcionOperacion))
                {
                    OpcionOperacion operacion = (OpcionOperacion)item;

                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        if (operacion.nombreElemento.Text.ToLower().Contains(textoBusqueda.ToLower()) |
                            operacion.nombreOpcion.Text.ToLower().Contains(textoBusqueda.ToLower()))
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
            indiceElementosEncontrados = -1;
            ElementosEncontrados.Clear();
            textoBusquedaDiagrama.Text = string.Empty;
            resultadosBusquedas.Visibility = Visibility.Collapsed;
        }

        private void opcionOperarFilasRestantes_ConUltimoNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila
                        | ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados))
                {
                    ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero = (bool)opcionOperarFilasRestantes_ConUltimoNumero.IsChecked;
                }
            }
        }

        private void opcionOperarFilasRestantes_ConUltimoNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila
                        | ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados))
                {
                    ElementoSeleccionado.SeguirOperandoFilas_ConUltimoNumero = (bool)opcionOperarFilasRestantes_ConUltimoNumero.IsChecked;
                }
            }
        }

        private void btnDefinirSeleccionOrdenamiento_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                if (TipoElementoDiseñoOperacionSeleccionado != TipoElementoDiseñoOperacion.OpcionOperacion) return;

                if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados)
                {
                    SeleccionarOrdenarCantidades_Operador seleccionarOrdenar = new SeleccionarOrdenarCantidades_Operador();
                    seleccionarOrdenar.Ventana = Ventana;
                    seleccionarOrdenar.listaTextos.Operandos.Add(Operacion);
                    seleccionarOrdenar.listaTextos.SubOperandos.AddRange(ElementoSeleccionado.ElementosAnteriores);
                    seleccionarOrdenar.listaTextos.SubOperandos.Add(ElementoSeleccionado);
                    seleccionarOrdenar.listaTextos.ModoOperacion = true;

                    List<AsociacionTextoInformacion_ElementoSalida> listaAsociaciones = new List<AsociacionTextoInformacion_ElementoSalida>();
                    List<AsociacionTextoInformacion_Clasificador> listaAsociacionesClasificadores = new List<AsociacionTextoInformacion_Clasificador>();
                    List<Clasificador> listaClasificadores = new List<Clasificador>();
                    
                    seleccionarOrdenar.listaTextos.CondicionesTextosInformacion = CopiarCondiciones(ElementoSeleccionado.CondicionesTextosInformacion_SeleccionOrdenamiento,
                                                ElementoSeleccionado.AsociacionesTextosInformacion_ElementosSalida,                                                
                                                listaAsociaciones,
                                                ElementoSeleccionado.AsociacionesTextosInformacion_Clasificadores,
                                                listaAsociacionesClasificadores,
                                                ElementoSeleccionado.Clasificadores,
                                                listaClasificadores);

                    seleccionarOrdenar.listaTextos.ElementosSalida = ElementoSeleccionado.SalidasAgrupamiento_SeleccionOrdenamiento;
                    seleccionarOrdenar.listaTextos.Clasificadores_Operacion = listaClasificadores;
                    seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_ElementosSalida = listaAsociaciones;
                    seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_Clasificadores = listaAsociacionesClasificadores;

                    seleccionarOrdenar.CalculoDiseñoSeleccionado = CalculoSeleccionado;
                    seleccionarOrdenar.listaTextos.OperacionRelacionada = Operacion;
                    seleccionarOrdenar.listaTextos.ElementoDiseñoRelacionado = ElementoSeleccionado;

                    var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(Operacion);

                    if (definicionEncontrada != null && definicionEncontrada.Any())
                    {
                        if (definicionEncontrada.Any(i => i.VerificarEnCondiciones_DefinicionesTextosInformacion(ElementoSeleccionado)))
                        {
                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked = ElementoSeleccionado.IncluirAsignacionTextosInformacionDespues;
                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked = ElementoSeleccionado.IncluirAsignacionTextosInformacionAntes;
                            seleccionarOrdenar.opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
                        }
                    }

                    bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                    if (definicionEstablecida)
                    {
                        ElementoSeleccionado.CondicionesTextosInformacion_SeleccionOrdenamiento = seleccionarOrdenar.listaTextos.CondicionesTextosInformacion;
                        ElementoSeleccionado.AsociacionesTextosInformacion_ElementosSalida = seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_ElementosSalida;
                        ElementoSeleccionado.AsociacionesTextosInformacion_Clasificadores = seleccionarOrdenar.listaTextos.AsociacionesTextosInformacion_Clasificadores;
                        ElementoSeleccionado.Clasificadores = seleccionarOrdenar.listaTextos.Clasificadores_Operacion;

                        ElementoSeleccionado.IncluirAsignacionTextosInformacionDespues = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked;
                        ElementoSeleccionado.IncluirAsignacionTextosInformacionAntes = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked;
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado)
                {
                    SeleccionarOrdenarCondiciones seleccionarOrdenar = new SeleccionarOrdenarCondiciones();
                    seleccionarOrdenar.listaCondiciones.ModoOperacion = true;
                    seleccionarOrdenar.opcionManual.Visibility = Visibility.Visible;
                    seleccionarOrdenar.opcionModoManual.IsChecked = ElementoSeleccionado.ModoManual_CondicionFlujo;

                    List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones = new List<AsociacionCondicionFlujo_ElementoSalida>();
                    List<AsociacionCondicionFlujo_ElementoSalida> listaAsociaciones2 = new List<AsociacionCondicionFlujo_ElementoSalida>();

                    seleccionarOrdenar.listaCondiciones.CondicionesFlujo = CopiarCondicionesFlujo(ElementoSeleccionado.CondicionesFlujo_SeleccionOrdenamiento,
                        ElementoSeleccionado.AsociacionesCondicionFlujo_ElementosSalida,
                                                ElementoSeleccionado.AsociacionesCondicionFlujo_ElementosSalida2,
                                                listaAsociaciones, listaAsociaciones2);

                    seleccionarOrdenar.listaCondiciones.Operandos.Add(Operacion);
                    seleccionarOrdenar.listaCondiciones.SubOperandos = ElementoSeleccionado.ElementosAnteriores.ToList();

                    seleccionarOrdenar.listaCondiciones.ElementosSalida = ElementoSeleccionado.SalidasAgrupamiento_CondicionFlujo;
                    seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida = listaAsociaciones;
                    seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2 = listaAsociaciones2;

                    var definicionEncontrada = Calculo.TextosInformacion.VerificarEnCondiciones_DefinicionesTextosInformacion(Operacion);

                    if (definicionEncontrada != null && definicionEncontrada.Any())
                    {
                        if (definicionEncontrada.Any(i => i.VerificarEnCondiciones_DefinicionesTextosInformacion(ElementoSeleccionado)))
                        {
                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked = ElementoSeleccionado.IncluirAsignacionTextosInformacionDespues;
                            seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked = ElementoSeleccionado.IncluirAsignacionTextosInformacionAntes;
                            seleccionarOrdenar.opcionesCondicionesAsignacionTextosInformacion.Visibility = Visibility.Visible;
                        }
                    }

                    bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
                    if (definicionEstablecida)
                    {
                        ElementoSeleccionado.CondicionesFlujo_SeleccionOrdenamiento = seleccionarOrdenar.listaCondiciones.CondicionesFlujo;
                        if (seleccionarOrdenar.opcionModoManual.IsChecked == true)
                            ElementoSeleccionado.ModoManual_CondicionFlujo = true;
                        else
                            ElementoSeleccionado.ModoManual_CondicionFlujo = false;
                        ElementoSeleccionado.AsociacionesCondicionFlujo_ElementosSalida = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida;
                        ElementoSeleccionado.AsociacionesCondicionFlujo_ElementosSalida2 = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesFlujo_ElementosSalida2;

                        ElementoSeleccionado.IncluirAsignacionTextosInformacionDespues = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionDespues.IsChecked;
                        ElementoSeleccionado.IncluirAsignacionTextosInformacionAntes = (bool)seleccionarOrdenar.opcionIncluirAsignacionTextosInformacionAntes.IsChecked;
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.Espera)
                {
                    DefinirOperacion_Espera definir = new DefinirOperacion_Espera();
                    definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                    definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                    definir.SubOperandosSalidas = ElementoSeleccionado.ElementosPosteriores;
                    definir.EntradasSalidasOperacion_Espera = ElementoSeleccionado.EntradasSalidasOperacion_Espera.ToList();
                    definir.TiempoEspera = ElementoSeleccionado.TiempoEspera;
                    definir.TipoTiempoEspera = ElementoSeleccionado.TipoTiempoEspera;
                    definir.CantidadEsperas_Fijas = ElementoSeleccionado.CantidadEsperas_Fijas;
                    definir.CantidadVerificaciones = ElementoSeleccionado.CantidadVerificaciones;

                    if (ElementoSeleccionado.CondicionesTextosInformacion_Espera != null)
                        definir.CondicionesTextosInformacion_Espera = ElementoSeleccionado.CondicionesTextosInformacion_Espera.ReplicarObjeto();

                    if (ElementoSeleccionado.CondicionesCantidades_Espera != null)
                        definir.CondicionesCantidades_Espera = ElementoSeleccionado.CondicionesCantidades_Espera.ReplicarObjeto();

                    definir.VerificarCondiciones_Hasta = ElementoSeleccionado.VerificarCondiciones_Hasta;
                    definir.EjecutarImplicacionesAntes_Espera = ElementoSeleccionado.EjecutarImplicacionesAntes_Espera;
                    definir.EjecutarImplicacionesDespues_Espera = ElementoSeleccionado.EjecutarImplicacionesDespues_Espera;
                    definir.EjecutarImplicacionesDurante_Espera = ElementoSeleccionado.EjecutarImplicacionesDurante_Espera;

                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoSeleccionado &&
                                    i.OperacionRelacionada == Operacion && i.Definiciones_TextosInformacion.Any(j => j.ElementoRelacionado == ElementoSeleccionado)))
                        definir.MostrarOpcionesImplicaciones = true;

                    definir.ModoOperacion = true;

                    bool definicionEstablecida = (bool)definir.ShowDialog();
                    if (definicionEstablecida)
                    {
                        ElementoSeleccionado.EntradasSalidasOperacion_Espera = definir.EntradasSalidasOperacion_Espera.ToList();
                        ElementoSeleccionado.TiempoEspera = definir.TiempoEspera;
                        ElementoSeleccionado.TipoTiempoEspera = definir.TipoTiempoEspera;
                        ElementoSeleccionado.CantidadEsperas_Fijas = definir.CantidadEsperas_Fijas;
                        ElementoSeleccionado.CantidadVerificaciones = definir.CantidadVerificaciones;
                        ElementoSeleccionado.CondicionesTextosInformacion_Espera = definir.CondicionesTextosInformacion_Espera;
                        ElementoSeleccionado.CondicionesCantidades_Espera = definir.CondicionesCantidades_Espera;
                        ElementoSeleccionado.VerificarCondiciones_Hasta = definir.VerificarCondiciones_Hasta;
                        ElementoSeleccionado.EjecutarImplicacionesAntes_Espera = definir.EjecutarImplicacionesAntes_Espera;
                        ElementoSeleccionado.EjecutarImplicacionesDespues_Espera = definir.EjecutarImplicacionesDespues_Espera;
                        ElementoSeleccionado.EjecutarImplicacionesDurante_Espera = definir.EjecutarImplicacionesDurante_Espera;
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.RedondearCantidades)
                {
                    DefinirOperacion_RedondearCantidades definir = new DefinirOperacion_RedondearCantidades();
                    definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                    definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                    definir.SubOperandosSalidas = ElementoSeleccionado.ElementosPosteriores;
                    definir.config = ElementoSeleccionado.ConfigRedondeo.CopiarObjeto();
                    definir.EntradasSalidasOperacion_RedondearCantidades = ElementoSeleccionado.EntradasSalidasOperacion_RedondearCantidades.ToList();
                    definir.ModoOperacion = true;

                    definir.EjecutarImplicacionesAntes_Redondeo = ElementoSeleccionado.EjecutarImplicacionesAntes_Redondeo;
                    definir.EjecutarImplicacionesDespues_Redondeo = ElementoSeleccionado.EjecutarImplicacionesDespues_Redondeo;
                    definir.EjecutarImplicacionesDurante_Redondeo = ElementoSeleccionado.EjecutarImplicacionesDurante_Redondeo;

                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoSeleccionado &&
                                    i.OperacionRelacionada == Operacion && i.Definiciones_TextosInformacion.Any(j => j.ElementoRelacionado == ElementoSeleccionado)))
                        definir.MostrarOpcionesImplicaciones = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigRedondeo.RedondearPar_Cercano = definir.config.RedondearPar_Cercano;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_LejanoDeCero = definir.config.RedondearNumero_LejanoDeCero;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_CercanoDeCero = definir.config.RedondearNumero_CercanoDeCero;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_Mayor = definir.config.RedondearNumero_Mayor;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_Menor = definir.config.RedondearNumero_Menor;

                        ElementoSeleccionado.EjecutarImplicacionesAntes_Redondeo = definir.EjecutarImplicacionesAntes_Redondeo;
                        ElementoSeleccionado.EjecutarImplicacionesDespues_Redondeo = definir.EjecutarImplicacionesDespues_Redondeo;
                        ElementoSeleccionado.EjecutarImplicacionesDurante_Redondeo = definir.EjecutarImplicacionesDurante_Redondeo;
                        ElementoSeleccionado.EntradasSalidasOperacion_RedondearCantidades = definir.EntradasSalidasOperacion_RedondearCantidades.ToList();
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.LimpiarDatos)
                {
                    DefinirOperacion_LimpiarDatos definir = new DefinirOperacion_LimpiarDatos();
                    definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                    definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                    definir.SubOperandosSalidas = ElementoSeleccionado.ElementosPosteriores;
                    definir.config = ElementoSeleccionado.ConfigLimpiarDatos.CopiarObjeto();
                    definir.EntradasSalidasOperacion_LimpiarDatos = ElementoSeleccionado.EntradasSalidasOperacion_LimpiarDatos.ToList();
                    definir.ModoOperacion = true;

                    definir.EjecutarImplicacionesAntes_Limpieza = ElementoSeleccionado.EjecutarImplicacionesAntes_Limpieza;
                    definir.EjecutarImplicacionesDespues_Limpieza = ElementoSeleccionado.EjecutarImplicacionesDespues_Limpieza;
                    definir.EjecutarImplicacionesDurante_Limpieza = ElementoSeleccionado.EjecutarImplicacionesDurante_Limpieza;

                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoSeleccionado &&
                                    i.OperacionRelacionada == Operacion && i.Definiciones_TextosInformacion.Any(j => j.ElementoRelacionado == ElementoSeleccionado)))
                        definir.MostrarOpcionesImplicaciones = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarDuplicados = definir.config.QuitarDuplicados;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCantidadesDuplicadas = definir.config.QuitarCantidadesDuplicadas;
                        ElementoSeleccionado.ConfigLimpiarDatos.Conector1_Duplicados = definir.config.Conector1_Duplicados;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCantidadesTextosDuplicadas = definir.config.QuitarCantidadesTextosDuplicadas;
                        ElementoSeleccionado.ConfigLimpiarDatos.Conector2_Duplicados = definir.config.Conector2_Duplicados;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCantidadesTextosDentroDuplicados = definir.config.QuitarCantidadesTextosDentroDuplicados;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCeros = definir.config.QuitarCeros;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCerosConTextos = definir.config.QuitarCerosConTextos;
                        ElementoSeleccionado.ConfigLimpiarDatos.Conector1_Ceros = definir.config.Conector1_Ceros;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCerosSinTextos = definir.config.QuitarCerosSinTextos;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarCantidadesSinTextos = definir.config.QuitarCantidadesSinTextos;
                        ElementoSeleccionado.ConfigLimpiarDatos.QuitarNegativas = definir.config.QuitarNegativas;
                        ElementoSeleccionado.EjecutarImplicacionesAntes_Limpieza = definir.EjecutarImplicacionesAntes_Limpieza;
                        ElementoSeleccionado.EjecutarImplicacionesDespues_Limpieza = definir.EjecutarImplicacionesDespues_Limpieza;
                        ElementoSeleccionado.EjecutarImplicacionesDurante_Limpieza = definir.EjecutarImplicacionesDurante_Limpieza;
                        ElementoSeleccionado.EntradasSalidasOperacion_LimpiarDatos = definir.EntradasSalidasOperacion_LimpiarDatos.ToList();
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.ArchivoExterno)
                {
                    DefinirOperacion_ArchivoExterno definir = new DefinirOperacion_ArchivoExterno();
                    definir.Calculo = Calculo;
                    definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                    definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                    definir.SubOperandosPosteriores = ElementoSeleccionado.ElementosPosteriores.Where(i => i.Tipo != TipoElementoDiseñoOperacion.Salida).ToList();
                    definir.EntradasSubOperandos_ArchivoExterno = ElementoSeleccionado.EntradasSubOperandos_ArchivoExterno.ToList();
                    definir.ResultadosSubOperandos_ArchivoExterno = ElementoSeleccionado.ResultadosSubOperandos_ArchivoExterno.ToList();
                    definir.Config = ElementoSeleccionado.ConfigArchivoExterno.CopiarObjeto();
                    //definir.EntradasSalidasOperacion_LimpiarDatos = ElementoSeleccionado.EntradasSalidasOperacion_LimpiarDatos.ToList();
                    definir.ModoOperacion = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigArchivoExterno = definir.Config.CopiarObjeto();
                        ElementoSeleccionado.EntradasSubOperandos_ArchivoExterno = definir.EntradasSubOperandos_ArchivoExterno.ToList();
                        ElementoSeleccionado.ResultadosSubOperandos_ArchivoExterno = definir.ResultadosSubOperandos_ArchivoExterno.ToList();
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.RedondearCantidades)
                {
                    DefinirOperacion_RedondearCantidades definir = new DefinirOperacion_RedondearCantidades();
                    definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                    definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                    definir.SubOperandosSalidas = ElementoSeleccionado.ElementosPosteriores;
                    definir.config = ElementoSeleccionado.ConfigRedondeo.CopiarObjeto();
                    definir.EntradasSalidasOperacion_RedondearCantidades = ElementoSeleccionado.EntradasSalidasOperacion_RedondearCantidades.ToList();
                    definir.ModoOperacion = true;

                    definir.EjecutarImplicacionesAntes_Redondeo = ElementoSeleccionado.EjecutarImplicacionesAntes_Redondeo;
                    definir.EjecutarImplicacionesDespues_Redondeo = ElementoSeleccionado.EjecutarImplicacionesDespues_Redondeo;
                    definir.EjecutarImplicacionesDurante_Redondeo = ElementoSeleccionado.EjecutarImplicacionesDurante_Redondeo;

                    if (Calculo.TextosInformacion.ElementosTextosInformacion.Any(i => i.CalculoRelacionado == CalculoSeleccionado &&
                                    i.OperacionRelacionada == Operacion && i.Definiciones_TextosInformacion.Any(j => j.ElementoRelacionado == ElementoSeleccionado)))
                        definir.MostrarOpcionesImplicaciones = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigRedondeo.RedondearPar_Cercano = definir.config.RedondearPar_Cercano;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_LejanoDeCero = definir.config.RedondearNumero_LejanoDeCero;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_CercanoDeCero = definir.config.RedondearNumero_CercanoDeCero;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_Mayor = definir.config.RedondearNumero_Mayor;
                        ElementoSeleccionado.ConfigRedondeo.RedondearNumero_Menor = definir.config.RedondearNumero_Menor;

                        ElementoSeleccionado.EjecutarImplicacionesAntes_Redondeo = definir.EjecutarImplicacionesAntes_Redondeo;
                        ElementoSeleccionado.EjecutarImplicacionesDurante_Redondeo = definir.EjecutarImplicacionesDurante_Redondeo;
                        ElementoSeleccionado.EjecutarImplicacionesDespues_Redondeo = definir.EjecutarImplicacionesDespues_Redondeo;
                        ElementoSeleccionado.EntradasSalidasOperacion_RedondearCantidades = definir.EntradasSalidasOperacion_RedondearCantidades.ToList();
                    }
                }
                else if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SubCalculo)
                {
                    DefinirOperacion_SubCalculo definir = new DefinirOperacion_SubCalculo();
                    definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                    definir.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                    definir.SubOperandosPosteriores = ElementoSeleccionado.ElementosPosteriores.Where(i => i.Tipo != TipoElementoDiseñoOperacion.Salida).ToList();
                    definir.EntradasSubOperandos_SubCalculo = CopiarAsociaciones_SubCalculo(ElementoSeleccionado.EntradasSubOperandos_SubCalculo);
                    definir.ResultadosSubOperandos_SubCalculo = CopiarAsociaciones_SubCalculo(ElementoSeleccionado.ResultadosSubOperandos_SubCalculo);
                    definir.Config = ElementoSeleccionado.ConfigSubCalculo.CopiarObjeto();
                    //definir.EntradasSalidasOperacion_LimpiarDatos = ElementoSeleccionado.EntradasSalidasOperacion_LimpiarDatos.ToList();
                    definir.ModoOperacion = true;
                    definir.NombreOperacion = ElementoSeleccionado.NombreElemento;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigSubCalculo = definir.Config.CopiarObjeto();
                        ElementoSeleccionado.EntradasSubOperandos_SubCalculo = definir.EntradasSubOperandos_SubCalculo;
                        ElementoSeleccionado.ResultadosSubOperandos_SubCalculo = definir.ResultadosSubOperandos_SubCalculo;

                        if(definir.AbrirDefinicionSubCalculo)
                        {
                            Ventana.AgregarTabArchivo(ElementoSeleccionado.ConfigSubCalculo.SubCalculo_Operacion);
                            Ventana.btnOperaciones_Click(this, null);
                        }
                    }
                }

                ListarOperandos();
            }
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
        private void agregarNuevoElementoSalida_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                {
                    AgregarElementoSalida_AgrupadoSeleccionOrdenamiento(ElementoSeleccionado);
                }
                else if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                {
                    AgregarElementoSalida_AgrupadoSeleccionCondiciones(ElementoSeleccionado);
                }
            }
        }

        private void AgregarElementoSalida_AgrupadoSeleccionOrdenamiento(DiseñoElementoOperacion elemento)
        {
            OpcionOperacion nuevaOperacion = new OpcionOperacion();
            nuevaOperacion.VistaOpciones = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaOperacion.SizeChanged += CambioTamañoOpcionOperacion;

            string nombreOperacion = "Números obtenidos de " + elemento.NombreElemento;

            nuevaOperacion.NombreOperacion = nombreOperacion;
            nuevaOperacion.Tipo = TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

            DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.NombreElemento = "Números obtenidos " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            nuevoElementoOperacion.Tipo = elemento.Tipo;
            nuevoElementoOperacion.TipoOpcionOperacion = nuevaOperacion.Tipo;
            nuevoElementoOperacion.PosicionX = elemento.PosicionX + elemento.Anchura + 10;
            nuevoElementoOperacion.PosicionY = elemento.PosicionY;
            nuevoElementoOperacion.Actualizar_ToolTips = true;

            nuevoElementoOperacion.Nombre = nuevaOperacion.nombreOpcion.Text;

            Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoElementoOperacion = nuevoElementoOperacion;

            elemento.ElementosPosteriores.Add(nuevoElementoOperacion);
            elemento.SalidasAgrupamiento_SeleccionOrdenamiento.Add(nuevoElementoOperacion);
            nuevoElementoOperacion.ElementosAnteriores.Add(elemento);
            nuevoElementoOperacion.EntradaRelacionada = elemento.EntradaRelacionada;
            elemento.ElementosContenedoresOperacion.Add(nuevoElementoOperacion);
            elemento.AgregarOrdenOperando(nuevoElementoOperacion);

            diagrama.Children.Add(nuevaOperacion);

            Canvas.SetTop(nuevaOperacion, nuevoElementoOperacion.PosicionY);
            Canvas.SetLeft(nuevaOperacion, nuevoElementoOperacion.PosicionX);

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            EstablecerIndicesProfundidadElementos();
            DibujarTodasLineasElementos();
        }

        private void AgregarElementoSalida_AgrupadoSeleccionCondiciones(DiseñoElementoOperacion elemento)
        {
            OpcionOperacion nuevaOperacion = new OpcionOperacion();
            nuevaOperacion.VistaOpciones = this;
            nuevaOperacion.EnDiagrama = true;
            nuevaOperacion.botonFondo.BorderBrush = Brushes.Black;
            nuevaOperacion.botonFondo.Background = SystemColors.GradientInactiveCaptionBrush;
            nuevaOperacion.SizeChanged += CambioTamañoOpcionOperacion;

            string nombreOperacion = "Números obtenidos de " + elemento.NombreElemento;

            nuevaOperacion.NombreOperacion = nombreOperacion;
            nuevaOperacion.Tipo = TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar;

            DiseñoElementoOperacion nuevoElementoOperacion = new DiseñoElementoOperacion();
            nuevoElementoOperacion.ID = App.GenerarID_Elemento();
            nuevoElementoOperacion.NombreElemento = "Números obtenidos " + (Operacion.ElementosDiseñoOperacion.Count + 1).ToString();
            nuevoElementoOperacion.Tipo = elemento.Tipo;
            nuevoElementoOperacion.TipoOpcionOperacion = nuevaOperacion.Tipo;
            nuevoElementoOperacion.PosicionX = elemento.PosicionX + elemento.Anchura + 10;
            nuevoElementoOperacion.PosicionY = elemento.PosicionY;
            nuevoElementoOperacion.Actualizar_ToolTips = true;

            nuevoElementoOperacion.Nombre = nuevaOperacion.nombreOpcion.Text;

            Operacion.ElementosDiseñoOperacion.Add(nuevoElementoOperacion);
            nuevaOperacion.DiseñoElementoOperacion = nuevoElementoOperacion;

            elemento.ElementosPosteriores.Add(nuevoElementoOperacion);
            elemento.SalidasAgrupamiento_CondicionFlujo.Add(nuevoElementoOperacion);
            nuevoElementoOperacion.ElementosAnteriores.Add(elemento);
            nuevoElementoOperacion.EntradaRelacionada = elemento.EntradaRelacionada;
            elemento.ElementosContenedoresOperacion.Add(nuevoElementoOperacion);
            elemento.AgregarOrdenOperando(nuevoElementoOperacion);

            diagrama.Children.Add(nuevaOperacion);

            Canvas.SetTop(nuevaOperacion, nuevoElementoOperacion.PosicionY);
            Canvas.SetLeft(nuevaOperacion, nuevoElementoOperacion.PosicionX);

            //nuevoElementoOperacion.Anchura = nuevaOperacion.ActualWidth;
            //nuevoElementoOperacion.Altura = nuevaOperacion.ActualHeight;

            DibujarTodasLineasElementos();
            EstablecerIndicesProfundidadElementos();
        }
        private void QuitarElementoDiagrama(DiseñoElementoOperacion itemElemento)
        {
            if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Salida) return;

            QuitarDeAsociacionesProcesamientoCantidades(itemElemento);

            if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Entrada)
            {
                if (itemElemento.ContieneSalida)
                {
                    QuitarElementoSalida(itemElemento);
                }

                foreach (var item in Operacion.ElementosDiseñoOperacion)
                    QuitarDeElementosPosterioresAnteriores(item, itemElemento);

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                    QuitarReferenciasElementos_DefinicionNombresCantidades(itemElemento, itemPosterior);

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                {
                    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                    {
                        QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemPosterior);
                    }
                }

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                    {
                        QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemAnterior);
                    }
                }

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                {
                    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                    {
                        QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemPosterior);
                    }
                }

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                    {
                        QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemAnterior);
                    }
                }

                QuitarReferenciasElementosImplicacion_AsignacionTextosInformacion(itemElemento, Calculo);

                foreach (var item in Operacion.ElementosDiseñoOperacion)
                {
                    foreach (var itemLogica in item.ProcesamientoCantidades)
                    {
                        if (itemLogica.CondicionesCantidades != null)
                        {
                            itemLogica.CondicionesCantidades.QuitarReferenciasCondicionesElemento_Interno(itemElemento);
                        }
                    }
                }

                Operacion.ElementosDiseñoOperacion.Remove(itemElemento);

                ActualizarContenedoresElementos(itemElemento);
                foreach (var definicion in Calculo.TextosInformacion.ElementosTextosInformacion)
                {
                    if (definicion.OperacionRelacionada == Operacion)
                        Ventana.ActualizarDefinicionesTextos_ElementoDiseñoOperacionEliminado(itemElemento, definicion);
                }

                EntradaDiseñoOperaciones entradaSeleccionada = (EntradaDiseñoOperaciones)(from UIElement E in diagrama.Children
                                                                                          where E.GetType() == typeof(EntradaDiseñoOperaciones) &&
                                                    ((EntradaDiseñoOperaciones)E).DiseñoElementoOperacion == itemElemento
                                                                                          select E).FirstOrDefault();

                diagrama.Children.Remove(entradaSeleccionada);
            }
            else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion)
            {
                if (itemElemento.ContieneSalida)
                {
                    QuitarElementoSalida(itemElemento);
                }

                foreach (var item in Operacion.ElementosDiseñoOperacion)
                    QuitarDeElementosPosterioresAnteriores(item, itemElemento);

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                    QuitarReferenciasElementos_DefinicionNombresCantidades(itemElemento, itemPosterior);

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                {
                    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                    {
                        QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemPosterior);
                    }
                }

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                    {
                        QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemAnterior);
                    }
                }

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                {
                    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                    {
                        QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemPosterior);
                    }
                }

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                    {
                        QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemAnterior);
                    }
                }

                QuitarReferenciasElementosImplicacion_AsignacionTextosInformacion(itemElemento, Calculo);

                Operacion.ElementosDiseñoOperacion.Remove(itemElemento);

                ActualizarContenedoresElementos(itemElemento);
                foreach (var definicion in Calculo.TextosInformacion.ElementosTextosInformacion)
                {
                    if (definicion.OperacionRelacionada == Operacion)
                        Ventana.ActualizarDefinicionesTextos_ElementoDiseñoOperacionEliminado(itemElemento, definicion);
                }

                EntradaFlujoOperacion flujoOperacionSeleccionado = (EntradaFlujoOperacion)(from UIElement E in diagrama.Children
                                                                                           where E.GetType() == typeof(EntradaFlujoOperacion) &&
                                                     ((EntradaFlujoOperacion)E).DiseñoElementoOperacion == itemElemento
                                                                                           select E).FirstOrDefault();

                diagrama.Children.Remove(flujoOperacionSeleccionado);
            }
            else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion)
            {
                //if (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
                //{
                QuitarDeAsociacionesTextosInformacion_SeleccionarOrdenar(itemElemento);
                QuitarDeAsociaciones_CondicionesFlujo(itemElemento);
                //}

                if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                    (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                    itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                {
                    List<DiseñoElementoOperacion> elementosAQuitar = new List<DiseñoElementoOperacion>();
                    elementosAQuitar.AddRange(itemElemento.SalidasAgrupamiento_SeleccionOrdenamiento.Where(i => i.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar));

                    while (elementosAQuitar.Any())
                    {
                        QuitarElementoDiagrama(elementosAQuitar.FirstOrDefault());
                        elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
                    }
                }

                if (itemElemento.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                    (itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                    itemElemento.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                {
                    List<DiseñoElementoOperacion> elementosAQuitar = new List<DiseñoElementoOperacion>();
                    elementosAQuitar.AddRange(itemElemento.SalidasAgrupamiento_CondicionFlujo.Where(i => i.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &
                    i.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar));

                    while (elementosAQuitar.Any())
                    {
                        QuitarElementoDiagrama(elementosAQuitar.FirstOrDefault());
                        elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
                    }
                }

                if (itemElemento.ContieneSalida)
                {
                    QuitarElementoSalida(itemElemento);
                }

                foreach (var item in Operacion.ElementosDiseñoOperacion)
                    QuitarDeElementosPosterioresAnteriores(item, itemElemento);

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                    QuitarReferenciasElementos_DefinicionNombresCantidades(itemElemento, itemPosterior);

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                {
                    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                    {
                        QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemPosterior);
                    }
                }

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                                itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                    {
                        QuitarReferenciasElementos_CondicionesSeleccionarOrdenar(itemElemento, itemAnterior);
                    }
                }

                foreach (var itemPosterior in itemElemento.ElementosPosteriores)
                {
                    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                    {
                        QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemPosterior);
                    }
                }

                foreach (var itemAnterior in itemElemento.ElementosAnteriores)
                {
                    if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                                (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                                itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                    {
                        QuitarReferenciasElementos_CondicionesFlujo(itemElemento, itemAnterior);
                    }
                }

                QuitarReferenciasElementosImplicacion_AsignacionTextosInformacion(itemElemento, Calculo);

                Operacion.ElementosDiseñoOperacion.Remove(itemElemento);

                ActualizarContenedoresElementos(itemElemento);
                foreach (var definicion in Calculo.TextosInformacion.ElementosTextosInformacion)
                {
                    if (definicion.OperacionRelacionada == Operacion)
                        Ventana.ActualizarDefinicionesTextos_ElementoDiseñoOperacionEliminado(itemElemento, definicion);
                }

                OpcionOperacion opcionOperacionSeleccionado = (OpcionOperacion)(from UIElement E in diagrama.Children
                                                                                where E.GetType() == typeof(OpcionOperacion) &&
                                          ((OpcionOperacion)E).DiseñoElementoOperacion == itemElemento
                                                                                select E).FirstOrDefault();

                diagrama.Children.Remove(opcionOperacionSeleccionado);
            }
            else if (itemElemento.Tipo == TipoElementoDiseñoOperacion.Nota)
            {

                Operacion.ElementosDiseñoOperacion.Remove(itemElemento);
                ActualizarContenedoresElementos(itemElemento);

                NotaDiagrama notaDiagramaSeleccionada = (NotaDiagrama)(from UIElement E in diagrama.Children
                                                                       where E.GetType() == typeof(NotaDiagrama) &&
                                 ((NotaDiagrama)E).DiseñoElementoOperacion == itemElemento
                                                                       select E).FirstOrDefault();

                diagrama.Children.Remove(notaDiagramaSeleccionada);
            }
        }

        public void QuitarReferenciasElementosImplicacion_AsignacionTextosInformacion(DiseñoElementoOperacion elemento, Calculo calculo)
        {
            foreach (var item in calculo.TextosInformacion.ElementosTextosInformacion)
            {
                foreach (var itemDefinicion in item.Relaciones_TextosInformacion)
                {
                    if (itemDefinicion.Condiciones_TextoCondicion != null)
                        itemDefinicion.Condiciones_TextoCondicion.QuitarReferenciasCondicionesElemento_Interno(elemento);

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
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_AsignarTextosInformacionCuando, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextosDefiniciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_DefinicionTextos_Definiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesComoTextos, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesDeCantidadesComoTextos, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_OperandosCondiciones(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_SusTextosCondiciones_Condiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_OperandosCondiciones(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_CantidadesCondiciones_Condiciones, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionTodosNumeros, elemento);
                        QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(itemInstancia.SubOperandos_DesdeAsignarTextosInformacion_TodosSusTextos_OpcionPosicionActual, elemento);
                    }
                }
            }
        }

        private void QuitarEntradasReferencias_Elemento_InstanciaImplicacion(List<DiseñoTextosInformacion> Entradas, DiseñoElementoOperacion elemento)
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

        private void QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> Operandos, DiseñoElementoOperacion elemento)
        {
            List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.ElementoRelacionado == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.SubOperando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarEntradasReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Operandos, DiseñoElementoOperacion elemento)
        {
            List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.ElementoRelacionado == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.SubOperando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion(List<AsignacionTextosOperando_Implicacion> Operandos, DiseñoElementoOperacion elemento)
        {
            List<AsignacionTextosOperando_Implicacion> elementosAQuitar = new List<AsignacionTextosOperando_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.SubOperando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion_Operandos(List<DiseñoElementoOperacion> Operandos, DiseñoElementoOperacion elemento)
        {
            List<DiseñoElementoOperacion> elementosAQuitar = new List<DiseñoElementoOperacion>();

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

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion_AsociacionOperandos(List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> Operandos, DiseñoElementoOperacion elemento)
        {
            List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosDefiniciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.ElementoRelacionado == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.SubOperando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private void QuitarOperandosReferencias_Elemento_InstanciaImplicacion_OperandosCondiciones(List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> Operandos, DiseñoElementoOperacion elemento)
        {
            List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion> elementosAQuitar = new List<AsociacionOperandosCondiciones_TextosAsignacion_Implicacion>();

            foreach (var itemElementoInstancia in Operandos)
            {
                if (itemElementoInstancia.Entrada != null &&
                    (itemElementoInstancia.Entrada.EntradaRelacionada == elemento.EntradaRelacionada |
                    itemElementoInstancia.Entrada.ElementoRelacionado == elemento))
                    elementosAQuitar.Add(itemElementoInstancia);

                if (itemElementoInstancia.SubOperando == elemento)
                    elementosAQuitar.Add(itemElementoInstancia);
            }

            while (elementosAQuitar.Any())
            {
                Operandos.Remove(elementosAQuitar.FirstOrDefault());
                elementosAQuitar.Remove(elementosAQuitar.FirstOrDefault());
            }
        }

        private bool VerificarElementos_SeleccionarOrdenar(DiseñoElementoOperacion elementoOrigen,
            DiseñoElementoOperacion elementoDestino)
        {
            //if (elementoOrigen.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
            //    (elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
            //    elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados) &&
            //    elementoOrigen.AgruparSeleccionesEnElementosSalida)
            //{
            //    return false;
            //}

            //if (elementoOrigen.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
            //    (elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
            //    elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos))
            //    return false;

            if (elementoOrigen.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elementoDestino)) return false;

            //var elementoContenedorSalida = (from E in Operacion.ElementosDiseñoOperacion where E.SalidasAgrupamiento_SeleccionOrdenamiento.Contains(elementoDestino) select E).FirstOrDefault();

            //if (elementoContenedorSalida != null) return false;
            if (elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar &&
                (elementoDestino.Tipo == TipoElementoDiseñoOperacion.Entrada | elementoDestino.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion)) return false;

            //if (!(elementoOrigen.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
            //    (elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
            //    elementoOrigen.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos)) && elementoDestino.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar) return false;

            if (elementoDestino.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion |
                elementoDestino.Tipo == TipoElementoDiseñoOperacion.Entrada) return false;

            return true;
        }

        private bool VerificarElementos_Entradas(DiseñoElementoOperacion elementoOrigen,
            DiseñoElementoOperacion elementoDestino)
        {
            if (elementoOrigen.Tipo == TipoElementoDiseñoOperacion.Entrada &
                elementoDestino.Tipo == TipoElementoDiseñoOperacion.Entrada) return false;

            if (elementoOrigen.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion &
                elementoDestino.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion) return false;

            if (elementoOrigen.Tipo == TipoElementoDiseñoOperacion.Entrada &
                elementoDestino.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion) return false;

            if (elementoOrigen.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion &
                elementoDestino.Tipo == TipoElementoDiseñoOperacion.Entrada) return false;

            if (elementoDestino.Tipo == TipoElementoDiseñoOperacion.FlujoOperacion |
                elementoDestino.Tipo == TipoElementoDiseñoOperacion.Entrada) return false;

            return true;
        }

        private void QuitarDeAsociacionesTextosInformacion_SeleccionarOrdenar(DiseñoElementoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                    (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                    itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados))
                {
                    var asociacion = (from A in itemAnterior.AsociacionesTextosInformacion_ElementosSalida where A.ElementoSalida == elemento select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        itemAnterior.AsociacionesTextosInformacion_ElementosSalida.Remove(asociacion);
                    }

                    itemAnterior.SalidasAgrupamiento_SeleccionOrdenamiento.Remove(elemento);
                }
            }

            //foreach (var itemPosterior in elemento.ElementosPosteriores)
            //{
            //    if (itemPosterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
            //        itemPosterior.TipoOpcionOperacion == TipoOpcionOperacion.ConjuntoNumerosAgrupado_SeleccionarOrdenar)
            //    {
            //        QuitarElementoDiagrama(itemPosterior);
            //    }
            //}
        }
        private void QuitarDeAsociaciones_CondicionesFlujo(DiseñoElementoOperacion elemento)
        {
            foreach (var itemAnterior in elemento.ElementosAnteriores)
            {
                if (itemAnterior.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                    (itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                    itemAnterior.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado))
                {
                    var asociacion = (from A in itemAnterior.AsociacionesCondicionFlujo_ElementosSalida where A.ElementoSalida == elemento select A).FirstOrDefault();

                    if (asociacion != null)
                    {
                        itemAnterior.AsociacionesCondicionFlujo_ElementosSalida.Remove(asociacion);
                    }

                    var asociacion2 = (from A in itemAnterior.AsociacionesCondicionFlujo_ElementosSalida2 where A.ElementoSalida == elemento select A).FirstOrDefault();

                    if (asociacion2 != null)
                    {
                        itemAnterior.AsociacionesCondicionFlujo_ElementosSalida2.Remove(asociacion2);
                    }

                    itemAnterior.SalidasAgrupamiento_CondicionFlujo.Remove(elemento);
                }
            }
        }

        public void ListarElementosSalida_Agrupamiento(DiseñoElementoOperacion elemento)
        {
            elementosSalidaAgrupamiento.DisplayMemberPath = "NombreCombo";
            elementosSalidaAgrupamiento.ItemsSource = (from O in Operacion.ElementosPosteriores where O.Tipo != TipoElementoOperacion.Salida select O).ToList();

            elementosSalidaAgrupamiento.SelectedItem = elemento.ElementoSalidaOperacion_Agrupamiento;
            ElementosSalidaAgrupamiento_SelectionChanged(elementosSalidaAgrupamiento, null);
        }

        private void ElementosSalidaAgrupamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ComboBox combo = (ComboBox)sender;
                ElementoSeleccionado.ElementoSalidaOperacion_Agrupamiento = (DiseñoOperacion)combo.SelectedItem;

                ListarElementosInternosSalidaAgrupamiento(ElementoSeleccionado.ElementoSalidaOperacion_Agrupamiento);

                elementosInternosSalidaAgrupamiento.Visibility = Visibility.Collapsed;
                agruparElementosSalidasOperacion.Visibility = Visibility.Collapsed;

                if (ElementoSeleccionado.ElementoSalidaOperacion_Agrupamiento != null &&
                !ElementoSeleccionado.ElementoSalidaOperacion_Agrupamiento.DefinicionSimple_Operacion)
                {
                    elementosInternosSalidaAgrupamiento.Visibility = Visibility.Visible;
                    agruparElementosSalidasOperacion.Visibility = Visibility.Visible;
                }
            }
        }

        public void ListarElementosInternosSalidaAgrupamiento(DiseñoOperacion elemento)
        {
            elementosInternosSalidaAgrupamiento.DisplayMemberPath = "NombreCombo";
            List<DiseñoElementoOperacion> listaElementos = new List<DiseñoElementoOperacion>();

            if (elemento != null &&
                !elemento.DefinicionSimple_Operacion)
                listaElementos = elemento.ObtenerElementosIniciales_FlujoOperacion();

            elementosInternosSalidaAgrupamiento.ItemsSource = listaElementos;

            if (ElementoSeleccionado.ElementoInternoSalidaOperacion_Agrupamiento != null &&
                listaElementos.Contains(ElementoSeleccionado.ElementoInternoSalidaOperacion_Agrupamiento))
                elementosInternosSalidaAgrupamiento.SelectedItem = ElementoSeleccionado.ElementoInternoSalidaOperacion_Agrupamiento;
            else
                elementosInternosSalidaAgrupamiento.SelectedItem = null;
        }

        private void ElementosInternosSalidaAgrupamiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool &&
                elementosInternosSalidaAgrupamiento.SelectedItem != null)
            {
                ComboBox combo = (ComboBox)sender;
                ElementoSeleccionado.ElementoInternoSalidaOperacion_Agrupamiento = (DiseñoElementoOperacion)combo.SelectedItem;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool)
            {
                elementosSalidaAgrupamiento.SelectedItem = null;
            }
        }

        private void opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.Ejecutar_SiTieneOtrosOperandosValidos = true;
            }
        }

        private void opcionPermitirEjecucion_SiElementoNoSeleccionado_PorCondiciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.Ejecutar_SiTieneOtrosOperandosValidos = false;
            }
        }

        private void opcionUtilizarDefinicionAsignacionTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.UtilizarDefinicionNombres_AsignacionTextosInformacion = true;
            }
        }

        private void opcionUtilizarDefinicionAsignacionTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.UtilizarDefinicionNombres_AsignacionTextosInformacion = false;
            }
        }

        private void definirNombresCantidades_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.Operandos = new List<DiseñoOperacion>() { Operacion };
                establecer.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
                establecer.TextosNombre = ElementoSeleccionado.DefinicionOpcionesNombresCantidades.ReplicarObjeto();
                if (establecer.ShowDialog() == true)
                {
                    ElementoSeleccionado.DefinicionOpcionesNombresCantidades = establecer.TextosNombre;
                    descripcionDefiniciones.Text = ObtenerDescripcionDefiniciones();
                }
            }
        }

        public string ObtenerDescripcionDefiniciones()
        {
            ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
            establecer.Operandos = new List<DiseñoOperacion>() { Operacion };
            establecer.SubOperandos = ElementoSeleccionado.ElementosAnteriores;
            establecer.TextosNombre = ElementoSeleccionado.DefinicionOpcionesNombresCantidades;
            return establecer.ObtenerDescripcion();
        }

        private void opcionAgregarCantidadNumerosCantidad_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarCantidadComoNumero = (bool)opcionAgregarCantidadNumerosCantidad.IsChecked;

                opcionIncluirCantidadNumero.Visibility = Visibility.Visible;
            }
        }

        private void opcionAgregarCantidadNumerosCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarCantidadComoNumero = (bool)opcionAgregarCantidadNumerosCantidad.IsChecked;

                opcionIncluirCantidadNumero.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionAgregarCantidadNumerosTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarCantidadComoTextoInformacion = (bool)opcionAgregarCantidadNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarCantidadNumerosTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarCantidadComoTextoInformacion = (bool)opcionAgregarCantidadNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarNumerosTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarNumeroComoTextoInformacion = (bool)opcionAgregarNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarNumerosTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarNumeroComoTextoInformacion = (bool)opcionAgregarNumerosTextoInformacion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosAntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.OrdenarNumerosAntesEjecucion == null)
                {
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion = new Entidades.Operaciones.OrdenarNumerosElemento();
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;
                }

                opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Visible;
                ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Visible;

                ordenarAntesEjecucion_PorNombre.IsChecked = ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre;
                ordenarAntesEjecucion_PorNombre_Checked(this, e);
            }
        }

        private void opcionOrdenarNumerosAntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion = null;

                opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
                ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
                opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarNumerosDespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.OrdenarNumerosDespuesEjecucion == null)
                {
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion = new Entidades.Operaciones.OrdenarNumerosElemento();
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;
                }

                opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Visible;
                ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Visible;

                ordenarDespuesEjecucion_PorNombre.IsChecked = ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre;
                ordenarDespuesEjecucion_PorNombre_Checked(this, e);
            }
        }

        private void opcionOrdenarNumerosDespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion = null;
                opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
                ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
                opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionTextosInformacionOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.AsignarTextosInformacion_OperandosResultados = (bool)opcionTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void opcionTextosInformacionOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool
                && ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AsignarTextosInformacion_OperandosResultados = (bool)opcionTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void ordenarAntesEjecucion_PorNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
            }
        }

        private void ordenarAntesEjecucion_PorNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
            }
        }

        private void ordenarAntesEjecucion_PorNombre_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;

                if (ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                {
                    opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Visible;
                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Checked(this, e);
            }
        }

        private void ordenarAntesEjecucion_PorNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;
                opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }

        private void ordenarDespuesEjecucion_PorNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;
            }
        }

        private void ordenarDespuesEjecucion_PorNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;
            }
        }

        private void ordenarDespuesEjecucion_PorNombre_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;

                if (ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre)
                {
                    opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Visible;
                    opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
                }

                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked = ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Checked(this, e);
            }
        }

        private void ordenarDespuesEjecucion_PorNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;
                opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionTipoOrdenamientoAntesEjecucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null &&
                    ((ComboBox)sender).SelectedItem != null)
                {
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)((ComboBox)sender).SelectedItem).Uid);
                }
            }
        }
        private void opcionTipoOrdenamientoDespuesEjecucion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null &&
                    ((ComboBox)sender).SelectedItem != null)
                {
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)((ComboBox)sender).SelectedItem).Uid);
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked;

                if (ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
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
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }
        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked;

                if (ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion)
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
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionTextosInformacionCondicionesOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.AsignarTextosInformacionCondiciones_OperandosResultados = (bool)opcionTextosInformacionCondicionesOperandosResultados.IsChecked;
            }
            opcionesCondicionesTextosInformacionOperandosResultados.Visibility = Visibility.Visible;
        }

        private void opcionTextosInformacionCondicionesOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool &&
                ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AsignarTextosInformacionCondiciones_OperandosResultados = (bool)opcionTextosInformacionCondicionesOperandosResultados.IsChecked;
            }

            opcionesCondicionesTextosInformacionOperandosResultados.Visibility = Visibility.Collapsed;
        }

        private void opcionAlgunosOperandosTextosInformacionOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.AlgunosOperandosTextosInformacionOperandosResultados = (bool)opcionAlgunosOperandosTextosInformacionOperandosResultados.IsChecked;
            }

            listaOperandosTextosInformacionOperandosResultados.Visibility = Visibility.Visible;
        }

        private void opcionAlgunosOperandosTextosInformacionOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.AlgunosOperandosTextosInformacionOperandosResultados = (bool)opcionAlgunosOperandosTextosInformacionOperandosResultados.IsChecked;
            }

            listaOperandosTextosInformacionOperandosResultados.Visibility = Visibility.Collapsed;
        }

        private void opcionNingunOperandoTextosInformacionOperandosResultados_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.NingunOperandoTextosInformacionOperandosResultados = (bool)opcionNingunOperandoTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void opcionNingunOperandoTextosInformacionOperandosResultados_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.NingunOperandoTextosInformacionOperandosResultados = (bool)opcionNingunOperandoTextosInformacionOperandosResultados.IsChecked;
            }
        }

        private void btnDefinirProcesamientoCantidades_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void ProcesamientoCantidades(object sender, RoutedEventArgs e)
        {
            menuProcesamiento.IsOpen = false;

            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                if (TipoElementoDiseñoOperacionSeleccionado != TipoElementoDiseñoOperacion.OpcionOperacion) return;

                OpcionesProcesamientoCantidades definir = new OpcionesProcesamientoCantidades();
                definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                definir.SubOperandos = new List<DiseñoElementoOperacion>() { ElementoSeleccionado };
                definir.SubOperandos.AddRange(ElementoSeleccionado.ElementosAnteriores);
                definir.SubOperandosElemento.AddRange(ElementoSeleccionado.ElementosAnteriores);
                definir.ProcesamientoCantidades = ElementoSeleccionado.CopiarProcesamientoCantidades();
                definir.NoConservarCambiosOperandos_ProcesamientoCantidades = ElementoSeleccionado.NoConservarCambiosOperandos_ProcesamientoCantidades;
                definir.EjecutarLogicasCantidades_AntesEjecucion = ElementoSeleccionado.EjecutarLogicasCantidades_AntesEjecucion;
                definir.EjecutarLogicasCantidades_DespuesEjecucion = ElementoSeleccionado.EjecutarLogicasCantidades_DespuesEjecucion;

                if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.TodosSeparados)
                    definir.OpcionesOperandoNumeros = true;

                if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.ContandoCantidades_Separados |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.ContandoCantidades_TodosJuntos)
                    definir.OpcionesInsertar = false;

                //if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoLogaritmo_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPorcentaje_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPotencias_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoRaices_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados)
                    definir.OpcionesInsertar_OperandosCantidades = true;

                definir.ModoDiseñoOperacion = true;

                if ((ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoLogaritmo_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPorcentaje_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPotencias_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoRaices_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila) && ElementoSeleccionado.ConAcumulacion)
                    definir.MostrarReiniciarAcumulacion = true;

                bool opcionElegida = (bool)definir.ShowDialog();
                if (opcionElegida)
                {
                    ElementoSeleccionado.ProcesamientoCantidades = CopiarProcesamientoCantidades(definir.ProcesamientoCantidades);
                    ElementoSeleccionado.NoConservarCambiosOperandos_ProcesamientoCantidades = definir.NoConservarCambiosOperandos_ProcesamientoCantidades;
                    ElementoSeleccionado.EjecutarLogicasCantidades_AntesEjecucion = definir.EjecutarLogicasCantidades_AntesEjecucion;
                    ElementoSeleccionado.EjecutarLogicasCantidades_DespuesEjecucion = definir.EjecutarLogicasCantidades_DespuesEjecucion;
                    //ElementoSeleccionado.SubOperandosInsertar_CantidadesProcesamientoCantidades.Clear();
                    //foreach (var operandos in ElementoSeleccionado.ProcesamientoCantidades.Select(i => i.SubOperandosInsertar_CantidadesProcesamientoCantidades))
                    //    ElementoSeleccionado.SubOperandosInsertar_CantidadesProcesamientoCantidades.AddRange(operandos);
                }
            }
        }

        private void ProcesamientoTextosInformacion(object sender, RoutedEventArgs e)
        {
            menuProcesamiento.IsOpen = false;

            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                if (TipoElementoDiseñoOperacionSeleccionado != TipoElementoDiseñoOperacion.OpcionOperacion) return;

                OpcionesProcesamientoTextosInformacion definir = new OpcionesProcesamientoTextosInformacion();
                definir.Operandos = new List<DiseñoOperacion>() { Operacion };
                definir.SubOperandos = new List<DiseñoElementoOperacion>() { ElementoSeleccionado };
                definir.SubOperandos.AddRange(ElementoSeleccionado.ElementosAnteriores);
                definir.SubOperandosElemento.AddRange(ElementoSeleccionado.ElementosAnteriores);
                definir.ProcesamientoTextosInformacion = ElementoSeleccionado.CopiarProcesamientoTextosInformacion();
                definir.NoConservarCambiosOperandos_ProcesamientoTextosInformacion = ElementoSeleccionado.NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
                definir.AplicarProcesamientoAntesImplicacionesTextosInformacion = ElementoSeleccionado.AplicarProcesamientoAntesImplicacionesTextosInformacion;
                definir.AplicarProcesamientoDespuesImplicacionesTextosInformacion = ElementoSeleccionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion;

                //if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.TodosSeparados)
                //    definir.OpcionesOperandoNumeros = true;

                //if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CondicionesFlujo_PorSeparado |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_SoloUnir |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosJuntos |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.SeleccionarOrdenar_TodosSeparados |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.ContandoCantidades_Separados |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.ContandoCantidades_TodosJuntos)
                //    definir.OpcionesInsertar = false;

                //if (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoLogaritmo_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPorcentaje_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPotencias_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoRaices_PorFila |
                //    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila)
                //    definir.OpcionesInsertar_OperandosCantidades = true;
                if ((ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoLogaritmo_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPorcentaje_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoPotencias_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.CalculandoRaices_PorFila |
                    ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila) && ElementoSeleccionado.ConAcumulacion)
                    definir.MostrarReiniciarAcumulacion = true;

                definir.ModoDiseñoOperacion = true;

                bool opcionElegida = (bool)definir.ShowDialog();
                if (opcionElegida)
                {
                    ElementoSeleccionado.ProcesamientoTextosInformacion = CopiarProcesamientoTextosInformacion(definir.ProcesamientoTextosInformacion);
                    ElementoSeleccionado.NoConservarCambiosOperandos_ProcesamientoTextosInformacion = definir.NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
                    ElementoSeleccionado.AplicarProcesamientoAntesImplicacionesTextosInformacion = definir.AplicarProcesamientoAntesImplicacionesTextosInformacion;
                    ElementoSeleccionado.AplicarProcesamientoDespuesImplicacionesTextosInformacion = definir.AplicarProcesamientoDespuesImplicacionesTextosInformacion;
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

        private void ColumnDefinition_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void opcionPorcentajeRelativo_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.PorcentajeRelativo = (bool)opcionPorcentajeRelativo.IsChecked;
            }
        }

        private void opcionPorcentajeRelativo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                ElementoSeleccionado.PorcentajeRelativo = (bool)opcionPorcentajeRelativo.IsChecked;
            }
        }

        private void opcionesElementosFijosPotencia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    if (opcionesElementosFijosPotencia.SelectedItem != null)
                        ElementoSeleccionado.OpcionElementosFijosPotencia = (TipoOpcionElementosFijosOperacionPotencia)int.Parse(((ComboBoxItem)opcionesElementosFijosPotencia.SelectedItem).Uid);
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
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    if (opcionesElementosFijosRaiz.SelectedItem != null)
                        ElementoSeleccionado.OpcionElementosFijosRaiz = (TipoOpcionElementosFijosOperacionRaiz)int.Parse(((ComboBoxItem)opcionesElementosFijosRaiz.SelectedItem).Uid);
                }

                if (opcionesElementosFijosRaiz.SelectedIndex == 1)
                {
                    textoOpcionElementoFijo.Text = "Base:";
                    textoOpcionElementoFijo.Visibility = Visibility.Visible;
                    valorOpcionElementoFijo.Visibility = Visibility.Visible;
                }
                else if (opcionesElementosFijosRaiz.SelectedIndex == 2)
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

        private void opcionesElementosFijosLogaritmo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    if (opcionesElementosFijosLogaritmo.SelectedItem != null)
                        ElementoSeleccionado.OpcionElementosFijosLogaritmo = (TipoOpcionElementosFijosOperacionLogaritmo)int.Parse(((ComboBoxItem)opcionesElementosFijosLogaritmo.SelectedItem).Uid);
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
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    if (opcionesElementosFijosInverso.SelectedItem != null)
                        ElementoSeleccionado.OpcionElementosFijosInverso = (TipoOpcionElementosFijosOperacionInverso)int.Parse(((ComboBoxItem)opcionesElementosFijosInverso.SelectedItem).Uid);
                }

                textoOpcionElementoFijo.Visibility = Visibility.Collapsed;
                valorOpcionElementoFijo.Visibility = Visibility.Collapsed;
            }
        }

        private void valorOpcionElementoFijo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ElementoSeleccionado_Bool &&
                ElementoSeleccionado != null)
            {
                double numero = 0;
                double.TryParse(valorOpcionElementoFijo.Text, out numero);
                ElementoSeleccionado.ValorOpcionElementosFijos = numero;
            }
        }

        private void agruparElementosSalidasOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                elementosInternosSalidaAgrupamiento.SelectedItem = null;
                ElementoSeleccionado.ElementoInternoSalidaOperacion_Agrupamiento = null;
            }
        }

        private void opcionesDividirOrdenacionTextosInformacion_AntesEjecucion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool &&
                ElementoSeleccionado != null)
            {
                DefinirOrdenacionesTextosInformacion definir = new DefinirOrdenacionesTextosInformacion();
                definir.Ordenaciones = CopiarOrdenaciones(ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenaciones);
                definir.RevertirListaTextos = ElementoSeleccionado.OrdenarNumerosAntesEjecucion.RevertirListaTextos;

                bool definicion = (bool)definir.ShowDialog();
                if ((bool)definicion == true)
                {
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenaciones = definir.Ordenaciones.ToList();
                    ElementoSeleccionado.OrdenarNumerosAntesEjecucion.RevertirListaTextos = definir.RevertirListaTextos;
                }
            }
        }

        private void opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool &&
                ElementoSeleccionado != null)
            {
                DefinirOrdenacionesTextosInformacion definir = new DefinirOrdenacionesTextosInformacion();
                definir.Ordenaciones = CopiarOrdenaciones(ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenaciones);
                definir.RevertirListaTextos = ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.RevertirListaTextos;

                bool definicion = (bool)definir.ShowDialog();
                if ((bool)definicion == true)
                {
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenaciones = definir.Ordenaciones.ToList();
                    ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.RevertirListaTextos = definir.RevertirListaTextos;
                }
            }
        }

        private List<OrdenacionNumeros> CopiarOrdenaciones(List<OrdenacionNumeros> lista)
        {
            List<OrdenacionNumeros> resultado = new List<OrdenacionNumeros>();
            foreach (var item in lista)
            {
                resultado.Add(item.CopiarObjeto());
            }

            return resultado;
        }

        private void opcionOperarFilasRestantes_ConCeros_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila
                        | ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados))
                {
                    ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro = (bool)opcionOperarFilasRestantes_ConCeros.IsChecked;
                }
            }
        }

        private void opcionOperarFilasRestantes_ConCeros_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                if (ElementoSeleccionado.Tipo == TipoElementoDiseñoOperacion.OpcionOperacion &&
                        (ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFila
                        | ElementoSeleccionado.TipoOpcionOperacion == TipoOpcionOperacion.PorFilaPorSeparados))
                {
                    ElementoSeleccionado.SeguirOperandoFilas_ConElementoNeutro = (bool)opcionOperarFilasRestantes_ConCeros.IsChecked;
                }
            }
        }

        private void opcionIncluirCantidadNumero_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.IncluirCantidadNumero = (bool)opcionIncluirCantidadNumero.IsChecked;
            }
        }

        private void opcionIncluirCantidadNumero_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.IncluirCantidadNumero = (bool)opcionIncluirCantidadNumero.IsChecked;
            }
        }

        private void opciones_Loaded(object sender, RoutedEventArgs e)
        {
            Ventana.MostrarOcultarBotonOverFlow_BarraOpciones((ToolBar)sender);
        }

        private void filaOperaciones_GotFocus(object sender, RoutedEventArgs e)
        {
            UserControl_Loaded(sender, e);
            ////entradas.Width = contenedorEntradas.ActualWidth;
            //opciones.Width = contenedorOpciones.ActualWidth;

            //contenedorOpciones.Height = opciones.ActualHeight;
            ////contenedorEntradas.MaxHeight = contenedorEntradas.ActualHeight;
        }

        private void filaEntradas_GotFocus(object sender, RoutedEventArgs e)
        {


            //entradas.Width = contenedorEntradas.ActualWidth;
            ////opciones.Width = contenedorOpciones.ActualWidth;

            ////contenedorOpciones.MaxHeight = contenedorOpciones.ActualHeight;
            //contenedorEntradas.Height = entradas.ActualHeight;
        }

        private void divisionZeroContinuar_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.DivisionZero_Continuar = (bool)divisionZeroContinuar.IsChecked;
            }
        }

        private void divisionZeroContinuar_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.DivisionZero_Continuar = (bool)divisionZeroContinuar.IsChecked;
            }
        }

        private void btnAbrirCarpeta_Click(object sender, RoutedEventArgs e)
        {
            string strRuta = rutaArchivo.Content.ToString().Substring(0, rutaArchivo.Content.ToString().LastIndexOf("\\"));
            Process.Start("explorer.exe", strRuta);
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
                Operacion.AltoDiagrama = diagrama.Height;
                Operacion.AnchoDiagrama = diagrama.Width;
            }
        }

        private void opcionAsignarTextosInformacionAntes_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionAntes_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_AntesEjecucion = (bool)opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked;
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
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_DespuesEjecucion = (bool)opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked;
                }

                if (opcionAsignarTextosInformacionAntes_EjecucionOperacion.IsChecked == false &&
                        !ClicElemento)
                    opcionAsignarTextosInformacionDespues_EjecucionOperacion.IsChecked = true;
            }
        }

        private void opcionAsignarTextosInformacionAntes_Implicaciones_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_AntesImplicaciones = (bool)opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionAntes_Implicaciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_AntesImplicaciones = (bool)opcionAsignarTextosInformacionAntes_Implicaciones.IsChecked;

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
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_DespuesImplicaciones = (bool)opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked;
                }
            }
        }

        private void opcionAsignarTextosInformacionDespues_Implicaciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.AsignarTextosInformacion_DespuesImplicaciones = (bool)opcionAsignarTextosInformacionDespues_Implicaciones.IsChecked;

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
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.QuitarTextosInformacion_Repetidos = (bool)opcionQuitarTextosInformacion_Repetidos.IsChecked;
                }
            }
        }

        private void opcionQuitarTextosInformacion_Repetidos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.QuitarTextosInformacion_Repetidos = (bool)opcionQuitarTextosInformacion_Repetidos.IsChecked;
                }
            }
        }

        private void opcionSeleccionManualCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.ModoSeleccionManual_SeleccionarOrdenar = (bool)opcionSeleccionManualCantidades.IsChecked;
                }
            }
        }

        private void opcionSeleccionManualCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.ModoSeleccionManual_SeleccionarOrdenar = (bool)opcionSeleccionManualCantidades.IsChecked;
                }
            }
        }

        private void btnDefinirAsignacionImplicacionTextos_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
            {
                if (ElementoSeleccionado.Tipo != TipoElementoDiseñoOperacion.Entrada &
                    ElementoSeleccionado.Tipo != TipoElementoDiseñoOperacion.FlujoOperacion &
                        ElementoSeleccionado.Tipo != TipoElementoDiseñoOperacion.Linea &
                        ElementoSeleccionado.Tipo != TipoElementoDiseñoOperacion.Salida &
                    ElementoSeleccionado.Tipo != TipoElementoDiseñoOperacion.Nota)
                {
                    Ventana.Elemento_AgregarImplicacionTextos = Operacion;
                    Ventana.Calculo_AgregarImplicacionTextos = CalculoSeleccionado;
                    Ventana.ElementoDiseño_AgregarImplicacionTextos = ElementoSeleccionado;

                    Ventana.btnTextosInformacion_Click(this, null);

                    Thread.Sleep(300);
                    Ventana.Elemento_AgregarImplicacionTextos = null;
                    Ventana.Calculo_AgregarImplicacionTextos = null;
                    Ventana.ElementoDiseño_AgregarImplicacionTextos = null;
                }
            }
        }

        private void opcionLimpiarDatos_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.LimpiarDatosResultados = (bool)opcionLimpiarDatos.IsChecked;

                    botonOpcionLimpiarDatos.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionLimpiarDatos_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    ElementoSeleccionado.LimpiarDatosResultados = (bool)opcionLimpiarDatos.IsChecked;

                    botonOpcionLimpiarDatos.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void botonOpcionLimpiarDatos_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null &&
                ElementoSeleccionado_Bool)
                {
                    DefinirOperacion_LimpiarDatos definir = new DefinirOperacion_LimpiarDatos();
                    definir.config = ElementoSeleccionado.ConfigLimpiezaDatosResultados.CopiarObjeto();
                    definir.ModoOperacion = true;
                    definir.ModoComportamiento = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarDuplicados = definir.config.QuitarDuplicados;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesDuplicadas = definir.config.QuitarCantidadesDuplicadas;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.Conector1_Duplicados = definir.config.Conector1_Duplicados;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesTextosDuplicadas = definir.config.QuitarCantidadesTextosDuplicadas;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.Conector2_Duplicados = definir.config.Conector2_Duplicados;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesTextosDentroDuplicados = definir.config.QuitarCantidadesTextosDentroDuplicados;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCeros = definir.config.QuitarCeros;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCerosConTextos = definir.config.QuitarCerosConTextos;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.Conector1_Ceros = definir.config.Conector1_Ceros;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCerosSinTextos = definir.config.QuitarCerosSinTextos;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarCantidadesSinTextos = definir.config.QuitarCantidadesSinTextos;
                        ElementoSeleccionado.ConfigLimpiezaDatosResultados.QuitarNegativas = definir.config.QuitarNegativas;
                       
                    }
                }
            }
        }

        private void opcionRedondearCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null)
                {
                    ElementoSeleccionado.RedondearCantidadesResultados = (bool)opcionRedondearCantidades.IsChecked;

                    botonOpcionRedondearCantidades.Visibility = Visibility.Visible;
                }
            }
        }

        private void opcionRedondearCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null)
                {
                    ElementoSeleccionado.RedondearCantidadesResultados = (bool)opcionRedondearCantidades.IsChecked;

                    botonOpcionRedondearCantidades.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void botonOpcionRedondearCantidades_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (ElementoSeleccionado != null)
                {
                    DefinirOperacion_RedondearCantidades definir = new DefinirOperacion_RedondearCantidades();
                    definir.config = ElementoSeleccionado.ConfigRedondeoResultados.CopiarObjeto();
                    definir.ModoComportamiento = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        ElementoSeleccionado.ConfigRedondeoResultados.RedondearPar_Cercano = definir.config.RedondearPar_Cercano;
                        ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_LejanoDeCero = definir.config.RedondearNumero_LejanoDeCero;
                        ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_CercanoDeCero = definir.config.RedondearNumero_CercanoDeCero;
                        ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_Mayor = definir.config.RedondearNumero_Mayor;
                        ElementoSeleccionado.ConfigRedondeoResultados.RedondearNumero_Menor = definir.config.RedondearNumero_Menor;

                    }
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked;

                if (ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
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
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosAntesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked;

                if (ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades)
                {
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
                opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
                opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        private void opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null &&
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion != null)
            {
                ElementoSeleccionado.OrdenarNumerosDespuesEjecucion.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
            }
        }

        public void OcultarToolTips(UIElement elementoActual)
        {
            Ventana.popup.IsOpen = false;
        }
        private void diagrama_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Ventana.popup != null && Ventana.popup.Child != null && !Ventana.popup.Child.IsMouseOver)
                OcultarToolTips(null);
        }

        private void definirAgrupacionesOperandosResultados_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado_Bool &&
                ElementoSeleccionado != null)
            {
                DefinirAgrupacionesResultados definirAgrupaciones = new DefinirAgrupacionesResultados();
                definirAgrupaciones.SubElementoAsociado = ElementoSeleccionado;
                definirAgrupaciones.SubOperandosResultados = ElementoSeleccionado.ElementosPosteriores;
                definirAgrupaciones.Agrupaciones = ElementoSeleccionado.ObtenerAgrupaciones();
                definirAgrupaciones.ModoOperacion = true;
                definirAgrupaciones.ShowDialog();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void definirOperacionesCadenasTexto_Click(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ConfigurarOperaciones_Elemento config = new ConfigurarOperaciones_Elemento();
                config.OperacionesCadenasTexto = ElementoSeleccionado.ConfigOperaciones_CadenasTexto.Operaciones.Select(i => i.ReplicarObjeto()).ToList();
                bool resp = (bool)config.ShowDialog();

                if (resp)
                {
                    ElementoSeleccionado.ConfigOperaciones_CadenasTexto.Operaciones = config.OperacionesCadenasTexto;
                }
            }
        }

        private void opcionAgregarNombreTextoInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarNombreComoTextoInformacion = (bool)opcionAgregarNombreTextoInformacion.IsChecked;
            }
        }

        private void opcionAgregarNombreTextoInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.AgregarNombreComoTextoInformacion = (bool)opcionAgregarNombreTextoInformacion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.QuitarClasificadores_AntesEjecucion = (bool)opcionQuitarClasificadores_AntesEjecucion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.QuitarClasificadores_AntesEjecucion = (bool)opcionQuitarClasificadores_AntesEjecucion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.QuitarClasificadores_DespuesEjecucion = (bool)opcionQuitarClasificadores_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionQuitarClasificadores_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.QuitarClasificadores_DespuesEjecucion = (bool)opcionQuitarClasificadores_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionAntesEjecucion_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;

                ElementoSeleccionado.OrdenarClasificadores_AntesEjecucion = (bool)opcionAntesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionAntesEjecucion_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;

                ElementoSeleccionado.OrdenarClasificadores_AntesEjecucion = (bool)opcionAntesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_AntesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_AntesEjecucion.IsChecked;
            }
        }

        private void opcionDespuesEjecucion_Clasificadores_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;

                ElementoSeleccionado.OrdenarClasificadores_DespuesEjecucion = (bool)opcionDespuesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionDespuesEjecucion_Clasificadores_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;

                ElementoSeleccionado.OrdenarClasificadores_DespuesEjecucion = (bool)opcionDespuesEjecucion_Clasificadores.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMenorAMayor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMenorAMayor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }

        private void opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ElementoSeleccionado != null)
            {
                ElementoSeleccionado.OrdenarClasificadoresDeMayorAMenor_DespuesEjecucion = (bool)opcionOrdenarClasificadoresDeMayorAMenor_DespuesEjecucion.IsChecked;
            }
        }
    }
}
