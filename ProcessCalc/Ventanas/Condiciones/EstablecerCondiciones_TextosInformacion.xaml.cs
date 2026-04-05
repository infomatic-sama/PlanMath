using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para EstablecerCondiciones_TextosInformacion.xaml
    /// </summary>
    public partial class EstablecerCondiciones_TextosInformacion : Window
    {
        public CondicionTextosInformacion Condiciones { get; set; }
        public CondicionTextosInformacion CondicionSeleccionada { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool MostrarOpcionesTextosImplicacionAsignacion { get; set; }
        public bool ModoSeleccionEntradas { get; set; }
        public List<Entrada> Entradas { get; set; }
        public Entrada ElementoEntradaAsociado_Asignacion { get; set; }
        public DiseñoOperacion ElementoOperandoAsociado_Asignacion { get; set; }
        public DiseñoElementoOperacion ElementoSubOperandoAsociado_Asignacion { get; set; }
        public DiseñoOperacion ElementoAsociado { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public bool ModoAsignacionLogicaImplicaciones { get; set; }
        public EstablecerCondiciones_TextosInformacion()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListarCondiciones()
        {
            listaCondiciones.Children.Clear();

            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (Condiciones != null)
            {
                EtiquetaCondicionTextosInformacion etiquetaCondicion = new EtiquetaCondicionTextosInformacion();
                etiquetaCondicion.Condiciones_ElementoContenedor = Condiciones;
                etiquetaCondicion.CondicionSeleccionada_ElementoContenedor = CondicionSeleccionada;
                etiquetaCondicion.ElementoContenedor = this;
                etiquetaCondicion.Condicion = Condiciones;
                listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
            //}
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionTextosInformacion agregar = new AgregarQuitar_CondicionTextosInformacion();

            agregar.ListaEntradas = Entradas;
            if (ElementoAsociado != null)
                agregar.ListaOperandos = Operandos.Intersect(ElementoAsociado.ElementosAnteriores).ToList();
            else
                agregar.ListaOperandos = Operandos;
            agregar.ListaOperandos = Operandos;
            agregar.ListaElementos = ListaElementos;
            agregar.ListaSubOperandos = SubOperandos;
            agregar.MostrarOpcionesTextosImplicacionAsignacion = MostrarOpcionesTextosImplicacionAsignacion;
            agregar.ModoSeleccionEntradas = ModoSeleccionEntradas;
            agregar.ElementoEntradaAsociado_Asignacion = ElementoEntradaAsociado_Asignacion;
            agregar.ElementoOperandoAsociado_Asignacion = ElementoOperandoAsociado_Asignacion;
            agregar.ElementoSubOperandoAsociado_Asignacion = ElementoSubOperandoAsociado_Asignacion;
            agregar.ListaDefinicionesListas = DefinicionesListas;
            agregar.ModoAsignacionLogicaImplicaciones = ModoAsignacionLogicaImplicaciones;

            agregar.ShowDialog();

            if (agregar.Aceptar)
            {
                if (CondicionSeleccionada != null)
                {
                    agregar.Condicion.CondicionContenedora = CondicionSeleccionada;
                    CondicionSeleccionada.Condiciones.Add(agregar.Condicion);
                }
                else
                {
                    if (Condiciones == null)
                    {
                        Condiciones = new CondicionTextosInformacion();
                        Condiciones.ContenedorCondiciones = true;
                    }
                    
                    agregar.Condicion.CondicionContenedora = Condiciones;
                    Condiciones.Condiciones.Add(agregar.Condicion);                    
                }

                ListarCondiciones();
            }
        }

        private void editarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null &&
                !CondicionSeleccionada.ContenedorCondiciones)
            {
                AgregarQuitar_CondicionTextosInformacion editar = new AgregarQuitar_CondicionTextosInformacion();

                editar.ListaEntradas = Entradas;
                if (ElementoAsociado != null)
                    editar.ListaOperandos = Operandos.Intersect(ElementoAsociado.ElementosAnteriores).ToList();
                else
                    editar.ListaOperandos = Operandos;
                editar.ListaElementos = ListaElementos;
                editar.ListaSubOperandos = SubOperandos;
                editar.MostrarOpcionesTextosImplicacionAsignacion = MostrarOpcionesTextosImplicacionAsignacion;
                editar.ModoSeleccionEntradas = ModoSeleccionEntradas;
                editar.ElementoEntradaAsociado_Asignacion = ElementoEntradaAsociado_Asignacion;
                editar.ElementoOperandoAsociado_Asignacion = ElementoOperandoAsociado_Asignacion;
                editar.ElementoSubOperandoAsociado_Asignacion = ElementoSubOperandoAsociado_Asignacion;
                editar.ListaDefinicionesListas = DefinicionesListas;
                editar.ModoAsignacionLogicaImplicaciones = ModoAsignacionLogicaImplicaciones;

                editar.ModoEdicion = true;
                editar.Condicion = CondicionSeleccionada;
                editar.ShowDialog();

                if (editar.Aceptar)
                {
                    ListarCondiciones();
                }
            }
        }

        private void quitarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                    CondicionSeleccionada.CondicionContenedora.Condiciones.Remove(CondicionSeleccionada);
                else
                    Condiciones = null;

                CondicionSeleccionada.CondicionContenedora = null;
                CondicionSeleccionada = null;
                ListarCondiciones();
            }
        }

        private void moverCondicionAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null && !CondicionSeleccionada.ContenedorCondiciones)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionada.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionada);

                    if (indice - 1 == -1)
                    {
                        CondicionTextosInformacion condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);

                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionContenedoraDestino.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionContenedoraDestino;

                            ListarCondiciones();
                        }
                    }
                    else
                    {
                        CondicionTextosInformacion condicionAnterior = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice - 1);

                        //if (condicionAnterior.Condiciones.Any())
                        //{
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionAnterior.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionAnterior;

                            ListarCondiciones();
                        //}
                        //else
                        //{
                        //    CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                        //    CondicionSeleccionada.CondicionContenedora.Condiciones.Insert(indice - 1, CondicionSeleccionada);

                        //    ListarCondiciones();
                        //}
                    }
                }
            }
        }

        private void moverCondicionADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null && !CondicionSeleccionada.ContenedorCondiciones)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionada.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionada);

                    if (indice + 1 == CondicionSeleccionada.CondicionContenedora.Condiciones.Count)
                    {
                        CondicionTextosInformacion condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);

                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionContenedoraDestino.Condiciones.Insert(indiceCondicionContenedora + 1, CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionContenedoraDestino;

                            ListarCondiciones();
                        }
                    }
                    else
                    {
                        CondicionTextosInformacion condicionSiguiente = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice + 1);

                        //if (condicionSiguiente.Condiciones.Any())
                        //{
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                            ListarCondiciones();
                        //}
                        //else
                        //{
                        //    CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                        //    CondicionSeleccionada.CondicionContenedora.Condiciones.Insert(indice + 1, CondicionSeleccionada);

                        //    ListarCondiciones();
                        //}
                    }
                }
            }
        }

        public void DesmarcarCondicionesBusquedas()
        {
            foreach (EtiquetaCondicionTextosInformacion itemCondicion in listaCondiciones.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCondiciones();
        }
    }
}
