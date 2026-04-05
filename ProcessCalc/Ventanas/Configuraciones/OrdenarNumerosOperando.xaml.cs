using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Operaciones;
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
using System.Xml.Linq;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para OrdenarNumerosOperando.xaml
    /// </summary>
    public partial class OrdenarNumerosOperando : Window
    {
        public DiseñoOperacion Operando { get; set; }
        public DiseñoElementoOperacion SubOperando { get; set; }
        public DiseñoOperacion OperacionSeleccionada { get; set; }
        public DiseñoElementoOperacion SubOperacionSeleccionada { get; set; }
        List<OrdenacionNumeros> OrdenacionesAntesEjecucion = new List<OrdenacionNumeros>();
        List<OrdenacionNumeros> OrdenacionesDespuesEjecucion = new List<OrdenacionNumeros>();
        bool RevertirListaTextos_OrdenacionesAntes;
        bool RevertirListaTextos_OrdenacionesDespues;
        public OrdenarNumerosOperando()
        {
            InitializeComponent();
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void guardarOpciones_Click(object sender, RoutedEventArgs e)
        {
            if(opcionAntesEjecucion.IsChecked == false)
            {
                if (Operando != null)
                {
                    OrdenarNumerosElemento operandoEncontrado = (from E in OperacionSeleccionada.OrdenarNumeros_AntesEjecucion
                                                                 where
                                                                E.Operando == Operando
                                                                 select E).FirstOrDefault();

                    if (operandoEncontrado != null)
                        OperacionSeleccionada.OrdenarNumeros_AntesEjecucion.Remove(operandoEncontrado);
                }
                else if (SubOperando != null)
                {
                    OrdenarNumerosElemento operandoEncontrado = (from E in SubOperacionSeleccionada.OrdenarNumeros_AntesEjecucion
                                                                 where
                                                                E.SubOperando == SubOperando
                                                                 select E).FirstOrDefault();

                    if (operandoEncontrado != null)
                        SubOperacionSeleccionada.OrdenarNumeros_AntesEjecucion.Remove(operandoEncontrado);
                }
            }
            else if(opcionAntesEjecucion.IsChecked == true)
            {
                if (Operando != null)
                {
                    OrdenarNumerosElemento operandoEncontrado = (from E in OperacionSeleccionada.OrdenarNumeros_AntesEjecucion
                                                                 where
                                                                E.Operando == Operando
                                                                 select E).FirstOrDefault();

                    if (operandoEncontrado == null)
                    {
                        operandoEncontrado = new OrdenarNumerosElemento()
                        {
                            Operando = this.Operando,
                            Ordenacion = new OrdenacionNumeros()
                            {
                                OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked,
                                OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked,
                                OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked,
                                OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked,
                                Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)(opcionTipoOrdenamientoAntesEjecucion).SelectedItem).Uid),
                                OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked,
                                OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked,
                                OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked,
                                OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked ,
                                OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked,
                                OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked
                            },
                            Ordenaciones = OrdenacionesAntesEjecucion.ToList(),
                            RevertirListaTextos = RevertirListaTextos_OrdenacionesAntes
                        };

                        OperacionSeleccionada.OrdenarNumeros_AntesEjecucion.Add(operandoEncontrado);
                    }
                    else
                    {
                        operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;
                        operandoEncontrado.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)(opcionTipoOrdenamientoAntesEjecucion).SelectedItem).Uid);
                        operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenaciones = OrdenacionesAntesEjecucion.ToList();
                        operandoEncontrado.RevertirListaTextos = RevertirListaTextos_OrdenacionesAntes;
                    }
                }
                else if (SubOperando != null)
                {
                    OrdenarNumerosElemento operandoEncontrado = (from E in SubOperacionSeleccionada.OrdenarNumeros_AntesEjecucion
                                                                 where
                                                                E.SubOperando == SubOperando
                                                                 select E).FirstOrDefault();

                    if (operandoEncontrado == null)
                    {
                        operandoEncontrado = new OrdenarNumerosElemento()
                        {
                            SubOperando = this.SubOperando,
                            Ordenacion = new OrdenacionNumeros()
                            {
                                OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked,
                                OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked,
                                OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked,
                                OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked,
                                Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)(opcionTipoOrdenamientoAntesEjecucion).SelectedItem).Uid),
                                OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked,
                                OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked,
                                OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked,
                                OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked,
                                 OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked,
                                OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked
                            },
                            Ordenaciones = OrdenacionesAntesEjecucion.ToList(),
                            RevertirListaTextos = RevertirListaTextos_OrdenacionesAntes
                        };

                        SubOperacionSeleccionada.OrdenarNumeros_AntesEjecucion.Add(operandoEncontrado);
                    }
                    else
                    {
                        operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarAntesEjecucion_PorNumero.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarAntesEjecucion_PorNombre.IsChecked;
                        operandoEncontrado.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)(opcionTipoOrdenamientoAntesEjecucion).SelectedItem).Uid);
                        operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenaciones = OrdenacionesAntesEjecucion.ToList();
                        operandoEncontrado.RevertirListaTextos = RevertirListaTextos_OrdenacionesAntes;
                    }
                }
            }

            if (opcionDespuesEjecucion.IsChecked == false)
            {
                //if (Operando != null)
                //{
                //    OrdenarNumerosElemento operandoEncontrado = (from E in OperacionSeleccionada.OrdenarNumeros_DespuesEjecucion
                //                                                 where
                //                                                E.Operando == Operando
                //                                                 select E).FirstOrDefault();

                //    if (operandoEncontrado != null)
                //        OperacionSeleccionada.OrdenarNumeros_DespuesEjecucion.Remove(operandoEncontrado);
                //}
                if (SubOperando != null)
                {
                    OrdenarNumerosElemento operandoEncontrado = (from E in SubOperacionSeleccionada.OrdenarNumeros_DespuesEjecucion
                                                                 where
                                                                E.SubOperando == SubOperando
                                                                 select E).FirstOrDefault();

                    if (operandoEncontrado != null)
                        SubOperacionSeleccionada.OrdenarNumeros_DespuesEjecucion.Remove(operandoEncontrado);
                }
            }
            else if (opcionDespuesEjecucion.IsChecked == true)
            {
                //if (Operando != null)
                //{
                //    OrdenarNumerosElemento operandoEncontrado = (from E in OperacionSeleccionada.OrdenarNumeros_DespuesEjecucion
                //                                                 where
                //                                                E.Operando == Operando
                //                                                 select E).FirstOrDefault();

                //    if (operandoEncontrado == null)
                //    {
                //        operandoEncontrado = new OrdenarNumerosElemento()
                //        {
                //            Operando = this.Operando,
                //            Ordenacion = new OrdenacionNumeros()
                //            {
                //                OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked,
                //                OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked,
                //                OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked,
                //                OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked
                //            }
                //        };

                //        OperacionSeleccionada.OrdenarNumeros_DespuesEjecucion.Add(operandoEncontrado);
                //    }
                //    else
                //    {
                //        operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;
                //        operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;
                //        operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;
                //        operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;
                //    }
                //}
                if (SubOperando != null)
                {
                    OrdenarNumerosElemento operandoEncontrado = (from E in SubOperacionSeleccionada.OrdenarNumeros_DespuesEjecucion
                                                                 where
                                                                E.SubOperando == SubOperando
                                                                 select E).FirstOrDefault();

                    if (operandoEncontrado == null)
                    {
                        operandoEncontrado = new OrdenarNumerosElemento()
                        {
                            SubOperando = this.SubOperando,
                            Ordenacion = new OrdenacionNumeros()
                            {
                                OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked,
                                OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked,
                                OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked,
                                OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked,
                                Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)(opcionTipoOrdenamientoDespuesEjecucion).SelectedItem).Uid),
                                OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked,
                                OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked,
                                OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked,
                                OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked,
                                OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked,
                                OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked
                            },
                            Ordenaciones = OrdenacionesDespuesEjecucion.ToList(),
                            RevertirListaTextos = RevertirListaTextos_OrdenacionesDespues
                        };

                        SubOperacionSeleccionada.OrdenarNumeros_DespuesEjecucion.Add(operandoEncontrado);
                    }
                    else
                    {
                        operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor = (bool)opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor = (bool)opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad = (bool)ordenarDespuesEjecucion_PorNumero.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre = (bool)ordenarDespuesEjecucion_PorNombre.IsChecked;
                        operandoEncontrado.Ordenacion.Tipo_OrdenamientoNumeros = (TipoOpcion_OrdenamientoNumerosSalidas)int.Parse(((ComboBoxItem)(opcionTipoOrdenamientoDespuesEjecucion).SelectedItem).Uid);
                        operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades = (bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades = (bool)opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked;
                        operandoEncontrado.Ordenaciones = OrdenacionesDespuesEjecucion.ToList();
                        operandoEncontrado.RevertirListaTextos = RevertirListaTextos_OrdenacionesDespues;
                    }
                }
            }

            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Operando != null)
            {
                OrdenarNumerosElemento operandoEncontrado = (from E in OperacionSeleccionada.OrdenarNumeros_AntesEjecucion
                                                             where
                                                            E.Operando == Operando
                                                             select E).FirstOrDefault();

                if (operandoEncontrado == null)
                    opcionAntesEjecucion.IsChecked = false;
                else
                {
                    opcionAntesEjecucion.IsChecked = true;
                    opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor;
                    opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor;
                    ordenarAntesEjecucion_PorNombre.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre;
                    ordenarAntesEjecucion_PorNumero.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad;

                    opcionTipoOrdenamientoAntesEjecucion.SelectedItem = (from ComboBoxItem I in opcionTipoOrdenamientoAntesEjecucion.Items where I.Uid == ((int)operandoEncontrado.Ordenacion.Tipo_OrdenamientoNumeros).ToString() select I).FirstOrDefault();
                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor;

                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                    //opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                    //opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                    //ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Visible;
                    //ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Visible;
                    OrdenacionesAntesEjecucion = operandoEncontrado.Ordenaciones;
                    RevertirListaTextos_OrdenacionesAntes = operandoEncontrado.RevertirListaTextos;
                }

                opcionDespuesEjecucion.Visibility = Visibility.Collapsed;

                //operandoEncontrado = (from E in OperacionSeleccionada.OrdenarNumeros_DespuesEjecucion
                //                                             where
                //                                            E.Operando == Operando
                //                                             select E).FirstOrDefault();

                //if (operandoEncontrado == null)
                //    opcionDespuesEjecucion.IsChecked = false;
                //else
                //{
                //    opcionDespuesEjecucion.IsChecked = true;
                //    opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor;
                //    opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor;
                //    ordenarDespuesEjecucion_PorNombre.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre;
                //    ordenarDespuesEjecucion_PorNumero.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad;

                //    opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                //    opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                //    ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Visible;
                //    ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Visible;
                //}
            }
            else if (SubOperando != null)
            {
                OrdenarNumerosElemento operandoEncontrado = (from E in SubOperacionSeleccionada.OrdenarNumeros_AntesEjecucion
                                                             where
                                                            E.SubOperando == SubOperando
                                                             select E).FirstOrDefault();

                if (operandoEncontrado == null)
                    opcionAntesEjecucion.IsChecked = false;
                else
                {
                    opcionAntesEjecucion.IsChecked = true;
                    opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor;
                    opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor;
                    ordenarAntesEjecucion_PorNombre.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre;
                    ordenarAntesEjecucion_PorNumero.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad;

                    opcionTipoOrdenamientoAntesEjecucion.SelectedItem = (from ComboBoxItem I in opcionTipoOrdenamientoAntesEjecucion.Items where I.Uid == ((int)operandoEncontrado.Ordenacion.Tipo_OrdenamientoNumeros).ToString() select I).FirstOrDefault();
                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor;

                    opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;
                    opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                    opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                    //opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                    //opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                    //ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Visible;
                    //ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Visible;
                    OrdenacionesAntesEjecucion = operandoEncontrado.Ordenaciones;
                    RevertirListaTextos_OrdenacionesAntes = operandoEncontrado.RevertirListaTextos;
                }

                operandoEncontrado = (from E in SubOperacionSeleccionada.OrdenarNumeros_DespuesEjecucion
                                                             where
                                                            E.SubOperando == SubOperando
                                                             select E).FirstOrDefault();

                if (operandoEncontrado == null)
                    opcionDespuesEjecucion.IsChecked = false;
                else
                {
                    opcionDespuesEjecucion.IsChecked = true;
                    opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMenorAMayor;
                    opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosDeMayorAMenor;
                    ordenarDespuesEjecucion_PorNombre.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorNombre;
                    ordenarDespuesEjecucion_PorNumero.IsChecked = operandoEncontrado.Ordenacion.OrdenarNumerosPorCantidad;

                    opcionTipoOrdenamientoDespuesEjecucion.SelectedItem = (from ComboBoxItem I in opcionTipoOrdenamientoDespuesEjecucion.Items where I.Uid == ((int)operandoEncontrado.Ordenacion.Tipo_OrdenamientoNumeros).ToString() select I).FirstOrDefault();
                    opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion;
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor;

                    opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosInformacionCantidades_Ejecucion_SinOrdenarCantidades;
                    opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMenorAMayor_SinOrdenarCantidades;
                    opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.IsChecked = operandoEncontrado.Ordenacion.OrdenarTextosDeMayorAMenor_SinOrdenarCantidades;

                    //opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                    //opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                    //ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Visible;
                    //ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Visible;
                    OrdenacionesDespuesEjecucion = operandoEncontrado.Ordenaciones;
                    RevertirListaTextos_OrdenacionesDespues = operandoEncontrado.RevertirListaTextos;
                }
            }
        }

        private void opcionAntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Visible;
                ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Visible;
                ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Visible;
            }
        }

        private void opcionAntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionOrdenarNumerosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarNumerosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
                ordenarAntesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
                ordenarAntesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionDespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Visible;
                opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Visible;
                ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Visible;
                ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Visible;
            }
        }

        private void opcionDespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionOrdenarNumerosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                opcionOrdenarNumerosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
                ordenarDespuesEjecucion_PorNombre.Visibility = Visibility.Collapsed;
                ordenarDespuesEjecucion_PorNumero.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)opcionOrdenarTextosInformacionCantidades_AntesEjecucion.IsChecked == true)
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

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.IsChecked == true)
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

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
        }

        private void ordenarAntesEjecucion_PorNombre_Checked(object sender, RoutedEventArgs e)
        {
            if (ordenarAntesEjecucion_PorNombre.IsChecked == true)
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

            opcionOrdenarTextosInformacionCantidades_AntesEjecucion_Checked(this, e);
        }

        private void ordenarAntesEjecucion_PorNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionTipoOrdenamientoAntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionesDividirOrdenacionTextosInformacion_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion.Visibility = Visibility.Collapsed;
        }

        private void ordenarDespuesEjecucion_PorNombre_Checked(object sender, RoutedEventArgs e)
        {
            if (ordenarDespuesEjecucion_PorNombre.IsChecked == true)
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

            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_Checked(this, e);
        }

        private void ordenarDespuesEjecucion_PorNombre_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionTipoOrdenamientoDespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosInformacionCantidades_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion.Visibility = Visibility.Collapsed;
        }

        private void opcionesDividirOrdenacionTextosInformacion_AntesEjecucion_Click(object sender, RoutedEventArgs e)
        {            
            DefinirOrdenacionesTextosInformacion definir = new DefinirOrdenacionesTextosInformacion();
            definir.Ordenaciones = CopiarOrdenaciones(OrdenacionesAntesEjecucion);
            definir.RevertirListaTextos = RevertirListaTextos_OrdenacionesAntes;

            bool definicion = (bool)definir.ShowDialog();
            if ((bool)definicion == true)
            {
                OrdenacionesAntesEjecucion = definir.Ordenaciones.ToList();
                RevertirListaTextos_OrdenacionesAntes = definir.RevertirListaTextos;
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

        private void opcionesDividirOrdenacionTextosInformacion_DespuesEjecucion_Click(object sender, RoutedEventArgs e)
        {
            DefinirOrdenacionesTextosInformacion definir = new DefinirOrdenacionesTextosInformacion();
            definir.Ordenaciones = CopiarOrdenaciones(OrdenacionesDespuesEjecucion);
            definir.RevertirListaTextos = RevertirListaTextos_OrdenacionesDespues;

            bool definicion = (bool)definir.ShowDialog();
            if ((bool)definicion == true)
            {
                OrdenacionesDespuesEjecucion = definir.Ordenaciones.ToList();
                RevertirListaTextos_OrdenacionesDespues = definir.RevertirListaTextos;
            }
        }

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {            
            if (opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades.IsChecked == true)
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

        private void opcionOrdenarTextosInformacionCantidades_AntesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {            
            opcionOrdenarTextosDeMenorAMayor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_AntesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;            
        }

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades_Checked(object sender, RoutedEventArgs e)
        {            
            if (opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades.IsChecked == true)
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

        private void opcionOrdenarTextosInformacionCantidades_DespuesEjecucion_SinOrdenarCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            opcionOrdenarTextosDeMenorAMayor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;
            opcionOrdenarTextosDeMayorAMenor_DespuesEjecucion_SinOrdenarCantidades.Visibility = Visibility.Collapsed;            
        }
    }
}
