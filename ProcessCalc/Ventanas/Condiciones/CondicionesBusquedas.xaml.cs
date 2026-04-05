using ProcessCalc.Controles.Textos;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Vistas;
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
    /// Lógica de interacción para CondicionesBusquedas.xaml
    /// </summary>
    public partial class CondicionesBusquedas : Window
    {
        public VistaArchivoEntradaConjuntoNumeros VistaBusquedasArchivo { get; set; }
        public VistaArchivoEntradaTextosInformacion VistaBusquedasArchivo_TextosInformacion { get; set; }
        public VistaURLEntradaConjuntoNumeros VistaBusquedasURL { get; set; }
        public VistaURLEntradaTextosInformacion VistaBusquedasURL_TextosInformacion { get; set; }
        public CondicionConjuntoBusquedas CondicionSeleccionada { get; set; }
        public BusquedaTextoArchivo Busqueda { get; set; }
        public CondicionesBusquedas()
        {
            InitializeComponent();
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionConjuntoBusquedas agregar = new AgregarQuitar_CondicionConjuntoBusquedas();

            if (VistaBusquedasArchivo != null)
            {
                agregar.ListaBusquedas = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros;
            }
            else if (VistaBusquedasArchivo_TextosInformacion != null)
            {
                agregar.ListaBusquedas = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion;
            }
            else if (VistaBusquedasURL != null)
            {
                agregar.ListaBusquedas = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros;
            }
            else if (VistaBusquedasURL_TextosInformacion != null)
            {
                agregar.ListaBusquedas = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion;
            }

            agregar.ShowDialog();

            if (agregar.Aceptar)
            {
                agregar.Condicion.CondicionContenedora = CondicionSeleccionada;

                if (CondicionSeleccionada != null)
                {
                    CondicionSeleccionada.Condiciones.Add(agregar.Condicion);
                }
                else
                {
                    if (Busqueda.Condiciones_RealizacionBusqueda == null)
                    {
                        Busqueda.Condiciones_RealizacionBusqueda = agregar.Condicion;
                    }
                    else
                    {
                        agregar.Condicion.CondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda;
                        Busqueda.Condiciones_RealizacionBusqueda.Condiciones.Add(agregar.Condicion);
                    }
                }

                ListarCondicionesConjunto();
            }
        }

        private void ListarCondicionesConjunto()
        {
            listaCondiciones.Children.Clear();

            //foreach (var itemCondicion in Busqueda.Condiciones)
            //{
            if (Busqueda.Condiciones_RealizacionBusqueda != null)
            {
                EtiquetaCondicionConjuntoBusquedas etiquetaCondicion = new EtiquetaCondicionConjuntoBusquedas();
                etiquetaCondicion.VistaCondicionesBusqueda = this;
                etiquetaCondicion.Condicion = Busqueda.Condiciones_RealizacionBusqueda;
                listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
            //}
        }

        public void DesmarcarCondicionesBusquedas()
        {
            foreach (EtiquetaCondicionConjuntoBusquedas itemCondicion in listaCondiciones.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        private void editarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                AgregarQuitar_CondicionConjuntoBusquedas editar = new AgregarQuitar_CondicionConjuntoBusquedas();

                if (VistaBusquedasArchivo != null)
                {
                    editar.ListaBusquedas = VistaBusquedasArchivo.Entrada.BusquedasConjuntoNumeros;
                }
                else if (VistaBusquedasArchivo_TextosInformacion != null)
                {
                    editar.ListaBusquedas = VistaBusquedasArchivo_TextosInformacion.Entrada.BusquedasTextosInformacion;
                }
                else if (VistaBusquedasURL != null)
                {
                    editar.ListaBusquedas = VistaBusquedasURL.Entrada.BusquedasConjuntoNumeros;
                }
                else if (VistaBusquedasURL_TextosInformacion != null)
                {
                    editar.ListaBusquedas = VistaBusquedasURL_TextosInformacion.Entrada.BusquedasTextosInformacion;
                }

                editar.ModoEdicion = true;
                editar.Condicion = CondicionSeleccionada;
                editar.ShowDialog();

                if (editar.Aceptar)
                {
                    ListarCondicionesConjunto();
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
                    Busqueda.Condiciones_RealizacionBusqueda = null;

                CondicionSeleccionada.CondicionContenedora = null;
                CondicionSeleccionada = null;
                ListarCondicionesConjunto();
            }
        }

        private void moverCondicionAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionada.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionada);

                    if (indice - 1 == -1)
                    {
                        CondicionConjuntoBusquedas condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);

                            if (condicionContenedoraDestino != null)
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                condicionContenedoraDestino.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionContenedoraDestino;

                                ListarCondicionesConjunto();
                            }
                        }
                        else
                        {
                            int indiceCondicionContenedora = -1;
                            if (CondicionSeleccionada.CondicionContenedora != Busqueda.Condiciones_RealizacionBusqueda)
                                indiceCondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            if (indiceCondicionContenedora > -1)
                                Busqueda.Condiciones_RealizacionBusqueda.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);
                            else
                                Busqueda.Condiciones_RealizacionBusqueda.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda;

                            ListarCondicionesConjunto();
                        }
                    }
                    else
                    {
                        CondicionConjuntoBusquedas condicionAnterior = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice - 1);

                        if (condicionAnterior.Condiciones.Any())
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionAnterior.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionAnterior;

                            ListarCondicionesConjunto();
                        }
                        else
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            CondicionSeleccionada.CondicionContenedora.Condiciones.Insert(indice - 1, CondicionSeleccionada);

                            ListarCondicionesConjunto();
                        }
                    }
                }
            }
        }

        private void moverCondicionADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                if (CondicionSeleccionada.CondicionContenedora != null)
                {
                    int indice = CondicionSeleccionada.CondicionContenedora.Condiciones.IndexOf(CondicionSeleccionada);

                    if (indice + 1 == CondicionSeleccionada.CondicionContenedora.Condiciones.Count)
                    {
                        CondicionConjuntoBusquedas condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);

                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == condicionContenedoraDestino.Condiciones.Count)
                            {
                                condicionSiguiente = condicionContenedoraDestino.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionada.CondicionContenedora)
                                {
                                    condicionSiguiente = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = condicionContenedoraDestino.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if (agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionada);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto();
                            }
                            else
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                condicionContenedoraDestino.Condiciones.Add(CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionContenedoraDestino;
                            }
                        }
                        else
                        {
                            int indiceCondicionContenedora = -1;
                            if (CondicionSeleccionada.CondicionContenedora != Busqueda.Condiciones_RealizacionBusqueda)
                                indiceCondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionConjuntoBusquedas condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == Busqueda.Condiciones_RealizacionBusqueda.Condiciones.Count)
                            {
                                condicionSiguiente = Busqueda.Condiciones_RealizacionBusqueda.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionada.CondicionContenedora)
                                {
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = Busqueda.Condiciones_RealizacionBusqueda.Condiciones.ElementAt(indiceCondicionContenedora + 1);

                            if (condicionSiguiente.Condiciones.Any())
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                if (agregarFinal)
                                    condicionSiguiente.Condiciones.Add(CondicionSeleccionada);
                                else
                                    condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                                ListarCondicionesConjunto();
                            }
                            else
                            {
                                CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                                Busqueda.Condiciones_RealizacionBusqueda.Condiciones.Add(CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = Busqueda.Condiciones_RealizacionBusqueda;
                            }

                            ListarCondicionesConjunto();
                        }
                    }
                    else
                    {
                        CondicionConjuntoBusquedas condicionSiguiente = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice + 1);

                        if (condicionSiguiente.Condiciones.Any())
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            condicionSiguiente.Condiciones.Insert(0, CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = condicionSiguiente;

                            ListarCondicionesConjunto();
                        }
                        else
                        {
                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            CondicionSeleccionada.CondicionContenedora.Condiciones.Insert(indice + 1, CondicionSeleccionada);

                            ListarCondicionesConjunto();
                        }
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCondicionesConjunto();
        }
    }
}
