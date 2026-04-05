using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para EstablecerCondiciones_ImplicacionTextosInformacion.xaml
    /// </summary>
    public partial class EstablecerCondiciones_ImplicacionTextosInformacion : Window
    {
        public CondicionImplicacionTextosInformacion Condiciones { get; set; }
        public CondicionImplicacionTextosInformacion CondicionSeleccionada { get; set; }
        public List<DiseñoOperacion> Elementos { get; set; }
        public List<DiseñoTextosInformacion> Entradas { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoTextosInformacion> Definiciones { get; set; }
        public List<DiseñoListaCadenasTexto> DefinicionesListas { get; set; }
        public bool ModoProcesamientoCantidades { get; set; }
        public EstablecerCondiciones_ImplicacionTextosInformacion()
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
                EtiquetaCondicionImplicacionTextosInformacion etiquetaCondicion = new EtiquetaCondicionImplicacionTextosInformacion();
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
            AgregarQuitar_CondicionImplicacionTextosInformacion agregar = new AgregarQuitar_CondicionImplicacionTextosInformacion();

            agregar.ListaEntradas = ObtenerObjetosEntradas(Entradas);
            agregar.ListaElementos = Elementos;
            agregar.ListaOperandos = Operandos;
            agregar.ListaSubOperandos = SubOperandos;
            agregar.ListaDefiniciones = Definiciones;
            agregar.ListaDefinicionesListas = DefinicionesListas;
            agregar.ModoProcesamientoCantidades = ModoProcesamientoCantidades;

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
                        Condiciones = new CondicionImplicacionTextosInformacion();
                        Condiciones.ContenedorCondiciones = true;
                    }
                    
                    agregar.Condicion.CondicionContenedora = Condiciones;
                    Condiciones.Condiciones.Add(agregar.Condicion);                    
                }

                ListarCondiciones();
            }
        }

        public List<Entrada> ObtenerObjetosEntradas(List<DiseñoTextosInformacion> ElementosEntradas)
        {
            List<Entrada> entradas = new List<Entrada>();

            foreach (var item in ElementosEntradas)
            {
                if (item.EntradaRelacionada != null)
                    entradas.Add(item.EntradaRelacionada);
            }

            return entradas;
        }

        //public List<DiseñoTextosInformacion> ObtenerObjetosDefiniciones(List<DiseñoTextosInformacion> ElementosDefiniciones)
        //{
        //    List<DiseñoTextosInformacion> definiciones = new List<DiseñoTextosInformacion>();

        //    foreach (var item in ElementosDefiniciones)
        //    {
        //        if (item.EntradaRelacionada == null)
        //            definiciones.Add(item);
        //    }

        //    return definiciones;
        //}

        private void editarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null &&
                !CondicionSeleccionada.ContenedorCondiciones)
            {
                AgregarQuitar_CondicionImplicacionTextosInformacion editar = new AgregarQuitar_CondicionImplicacionTextosInformacion();

                editar.ListaEntradas = ObtenerObjetosEntradas(Entradas);
                editar.ListaElementos = Elementos;
                editar.ListaOperandos = Operandos;
                editar.ListaSubOperandos = SubOperandos;
                editar.ListaDefiniciones = Definiciones;
                editar.ListaDefinicionesListas = DefinicionesListas;
                editar.ModoProcesamientoCantidades = ModoProcesamientoCantidades;

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
                        CondicionImplicacionTextosInformacion condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

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
                        CondicionImplicacionTextosInformacion condicionAnterior = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice - 1);

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
                        CondicionImplicacionTextosInformacion condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

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
                        CondicionImplicacionTextosInformacion condicionSiguiente = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice + 1);

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
            foreach (EtiquetaCondicionImplicacionTextosInformacion itemCondicion in listaCondiciones.Children)
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
