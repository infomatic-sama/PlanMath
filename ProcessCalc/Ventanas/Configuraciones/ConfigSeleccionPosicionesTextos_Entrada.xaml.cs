using ProcessCalc.Controles.Ctrl_Entradas;
using ProcessCalc.Entidades.Entradas;
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
    /// Lógica de interacción para ConfigSeleccionPosicionesTextos_Entrada.xaml
    /// </summary>
    public partial class ConfigSeleccionPosicionesTextos_Entrada : Window
    {
        public ConfiguracionSeleccionPosicionesTextos_Entrada Definicion { get; set; }
        public CondicionSeleccionCantidadNumeros_Entrada CondicionSeleccionada { get; set; }
        public ConfigSeleccionPosicionesTextos_Entrada()
        {
            InitializeComponent();
        }

        private void cantidadNumeros_TextChanged(object sender, TextChangedEventArgs e)
        {
            Definicion.PosicionesDeterminadasNumeros = posicionesNumeros.Text;
        }

        private void opcionTodosNumeros_Checked(object sender, RoutedEventArgs e)
        {
            if (Definicion != null)
                Definicion.Opcion = TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros;
        }

        private void opcionCantidadNumerosDeterminada_Checked(object sender, RoutedEventArgs e)
        {
            if (Definicion != null)
                Definicion.Opcion = TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarCantidadDeterminadaNumeros;
        }

        private void opcionNumerosCondicion_Checked(object sender, RoutedEventArgs e)
        {
            if (Definicion != null)
                Definicion.ConCondiciones = true;
        }

        private void opcionNumerosCondicion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Definicion != null)
                Definicion.ConCondiciones = false;
        }

        private void agregarCondicion_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionSeleccionarCantidades_Entrada agregar = new AgregarQuitar_CondicionSeleccionarCantidades_Entrada();
            agregar.ModoPosicion = true;
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
                    if (Definicion.CondicionesSeleccionarNumeros == null)
                    {
                        Definicion.CondicionesSeleccionarNumeros = agregar.Condicion;
                    }
                    else
                    {
                        agregar.Condicion.CondicionContenedora = Definicion.CondicionesSeleccionarNumeros;
                        Definicion.CondicionesSeleccionarNumeros.Condiciones.Add(agregar.Condicion);
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
            if (Definicion.CondicionesSeleccionarNumeros != null)
            {
                EtiquetaCondicionSeleccionarCantidadNumeros_Entrada etiquetaCondicion = new EtiquetaCondicionSeleccionarCantidadNumeros_Entrada();
                etiquetaCondicion.VistaConfiguracionSeleccionarPosiciones = this;
                etiquetaCondicion.Condicion = Definicion.CondicionesSeleccionarNumeros;
                listaCondiciones.Children.Add(etiquetaCondicion);
                etiquetaCondicion.MostrarEtiquetaCondiciones();
            }
            //}
        }

        public void DesmarcarCondicionesBusquedas()
        {
            foreach (EtiquetaCondicionSeleccionarCantidadNumeros_Entrada itemCondicion in listaCondiciones.Children)
            {
                itemCondicion.DesmarcarSeleccion();
            }
        }

        private void editarCondicion_Click(object sender, RoutedEventArgs e)
        {
            if (CondicionSeleccionada != null)
            {
                AgregarQuitar_CondicionSeleccionarCantidades_Entrada editar = new AgregarQuitar_CondicionSeleccionarCantidades_Entrada();
                editar.ModoEdicion = true;
                editar.ModoPosicion = true;
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
                    Definicion.CondicionesSeleccionarNumeros = null;

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
                        CondicionSeleccionCantidadNumeros_Entrada condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

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
                            if (CondicionSeleccionada.CondicionContenedora != Definicion.CondicionesSeleccionarNumeros)
                                indiceCondicionContenedora = Definicion.CondicionesSeleccionarNumeros.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Definicion.CondicionesSeleccionarNumeros.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionSeleccionada.CondicionContenedora.Condiciones.RemoveAt(indice);
                            if (indiceCondicionContenedora > -1)
                                Definicion.CondicionesSeleccionarNumeros.Condiciones.Insert(indiceCondicionContenedora, CondicionSeleccionada);
                            else
                                Definicion.CondicionesSeleccionarNumeros.Condiciones.Add(CondicionSeleccionada);
                            CondicionSeleccionada.CondicionContenedora = Definicion.CondicionesSeleccionarNumeros;

                            ListarCondicionesConjunto();
                        }
                    }
                    else
                    {
                        CondicionSeleccionCantidadNumeros_Entrada condicionAnterior = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice - 1);

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
                        CondicionSeleccionCantidadNumeros_Entrada condicionContenedoraDestino = CondicionSeleccionada.CondicionContenedora.CondicionContenedora;

                        if (condicionContenedoraDestino != null)
                        {
                            int indiceCondicionContenedora = condicionContenedoraDestino.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);

                            CondicionSeleccionCantidadNumeros_Entrada condicionSiguiente = null;
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
                            if (CondicionSeleccionada.CondicionContenedora != Definicion.CondicionesSeleccionarNumeros)
                                indiceCondicionContenedora = Definicion.CondicionesSeleccionarNumeros.Condiciones.IndexOf(CondicionSeleccionada.CondicionContenedora);
                            else
                                indiceCondicionContenedora = Definicion.CondicionesSeleccionarNumeros.Condiciones.IndexOf(CondicionSeleccionada);

                            CondicionSeleccionCantidadNumeros_Entrada condicionSiguiente = null;
                            bool agregarFinal = false;
                            if (indiceCondicionContenedora + 1 == Definicion.CondicionesSeleccionarNumeros.Condiciones.Count)
                            {
                                condicionSiguiente = Definicion.CondicionesSeleccionarNumeros.Condiciones.Last();
                                if (condicionSiguiente == CondicionSeleccionada.CondicionContenedora)
                                {
                                    agregarFinal = true;
                                }
                            }
                            else
                                condicionSiguiente = Definicion.CondicionesSeleccionarNumeros.Condiciones.ElementAt(indiceCondicionContenedora + 1);

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
                                Definicion.CondicionesSeleccionarNumeros.Condiciones.Add(CondicionSeleccionada);
                                CondicionSeleccionada.CondicionContenedora = Definicion.CondicionesSeleccionarNumeros;
                            }

                            ListarCondicionesConjunto();
                        }
                    }
                    else
                    {
                        CondicionSeleccionCantidadNumeros_Entrada condicionSiguiente = CondicionSeleccionada.CondicionContenedora.Condiciones.ElementAt(indice + 1);

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

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Definicion != null)
            {
                switch (Definicion.Opcion)
                {
                    case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarTodosNumeros:
                        opcionTodosNumeros.IsChecked = true;
                        break;

                    case TipoOpcionConfiguracionSeleccionNumeros_Entrada.SeleccionarCantidadDeterminadaNumeros:
                        opcionPosicionesNumerosDeterminadas.IsChecked = true;
                        break;
                }

                opcionNumerosCondicion.IsChecked = Definicion.ConCondiciones;
                posicionesNumeros.Text = Definicion.PosicionesDeterminadasNumeros;

                ListarCondicionesConjunto();
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
