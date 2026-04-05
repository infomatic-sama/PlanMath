using ProcessCalc.Controles.Condiciones;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
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
    /// Lógica de interacción para EstablecerCondiciones_Flujo.xaml
    /// </summary>
    public partial class EstablecerCondiciones_Flujo : Window
    {
        public CondicionFlujo Condiciones { get; set; }
        public CondicionFlujo CondicionSeleccionada { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public bool ModoOperacion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public EstablecerCondiciones_Flujo()
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

            if (Condiciones != null)
            {
                EtiquetaCondicionFlujo etiquetaCondicion = new EtiquetaCondicionFlujo();
                etiquetaCondicion.Condiciones_ElementoContenedor = Condiciones;
                etiquetaCondicion.CondicionSeleccionada_ElementoContenedor = CondicionSeleccionada;
                etiquetaCondicion.ElementoContenedor = this;
                etiquetaCondicion.Condicion = Condiciones;
                listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
        }
        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionFlujo agregar = new AgregarQuitar_CondicionFlujo();
            agregar.ModoOperacion = ModoOperacion;
            agregar.ListaOperandos = Operandos;
            agregar.SubOperandos = SubOperandos;
            agregar.ListaElementos = ListaElementos;

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
                        Condiciones = new CondicionFlujo();
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
                AgregarQuitar_CondicionFlujo editar = new AgregarQuitar_CondicionFlujo();
                editar.ModoOperacion = ModoOperacion;
                editar.ListaOperandos = Operandos;
                editar.SubOperandos = SubOperandos;
                editar.ListaElementos = ListaElementos;
                
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
                        CondicionFlujo condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

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
                        CondicionFlujo condicionAnterior = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice - 1);

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
                        CondicionFlujo condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

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
                        CondicionFlujo condicionSiguiente = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice + 1);

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
            foreach (EtiquetaCondicionFlujo itemCondicion in listaCondiciones.Children)
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
