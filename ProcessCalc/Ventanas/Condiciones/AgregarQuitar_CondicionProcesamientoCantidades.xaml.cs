using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.Entradas;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas.Definiciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProcessCalc.Ventanas
{
    /// <summary>
    /// Lógica de interacción para AgregarQuitar_CondicionProcesamientoCantidades.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionProcesamientoCantidades : Window
    {
        public CondicionProcesamientoCantidades Condicion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoOperacion> OperandosElemento { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosElemento { get; set; }
        public bool OpcionesOperandoNumeros { get; set; }
        public bool OpcionesInsertar { get; set; }
        public bool OpcionesInsertar_OperandosCantidades { get; set; }
        public bool ModoDiseñoOperacion { get; set; }     
        public bool MostrarReiniciarAcumulacion {  get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public AgregarQuitar_CondicionProcesamientoCantidades()
        {
            OpcionesInsertar = true;
            OperandosElemento = new List<DiseñoOperacion>();
            SubOperandosElemento = new List<DiseñoElementoOperacion>();
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (opcionTipoCondicion.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de lógica.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (opcionTipoElemento.Visibility == Visibility.Visible && opcionTipoElemento.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de elemento.", "Seleccionar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (opcionTipoCondicion.SelectedItem != null)
                    Condicion.Tipo = (TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid);

                if(opcionTipoElemento.SelectedItem != null)
                    Condicion.TipoElemento = (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid);

                if (opcionTipoElementoAccion.SelectedItem != null)
                    Condicion.TipoElementoAccion = (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion.SelectedItem).Uid);

                if (opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem != null)
                    Condicion.TipoElementoAOperar_OperacionAlInsertar = (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem).Uid);

                if (opcionTipoElementoAccion_Insertar.SelectedItem != null)
                    Condicion.TipoElementoAccion_Insertar = (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion_Insertar.SelectedItem).Uid);

                if (opcionTipoElemento_OperacionAlInsertar.SelectedItem != null)
                    Condicion.TipoElemento_OperacionAlInsertar = (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento_OperacionAlInsertar.SelectedItem).Uid);

                if (opcionUbicacionAccion_Insertar.SelectedItem != null)
                    Condicion.TipoUbicacionAccion_Insertar = (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid);

                if (opcionNoInsertarPosicionAnterior.Visibility == Visibility.Visible)
                    Condicion.NoInsertarCantidad_EnPosicion = (bool)opcionNoInsertarPosicionAnterior.IsChecked;

                else if (opcionNoInsertarPosicionPosterior.Visibility == Visibility.Visible)
                    Condicion.NoInsertarCantidad_EnPosicion = (bool)opcionNoInsertarPosicionPosterior.IsChecked;

                Condicion.CondicionesCantidades = condicionesFiltroCantidades.Condiciones;
                Condicion.ValorFijo_Insercion = double.Parse(valorFijo.Text);

                Condicion.ValorPosicion_TipoElementoAccion_Insertar = double.Parse(opcionValoresPosiciones_TipoElementoAccion_Insertar.Text);
                Condicion.ValorPosicion_UbicacionAccion_Insertar = double.Parse(opcionValoresPosiciones_UbicacionAccion_Insertar.Text);

                Condicion.ValorPosicion_TipoElemento_OperacionAlInsertar = double.Parse(opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Text);

                Condicion.ValorFijo_OperacionAlInsertar = valorFijo_OperacionAlInsertar.Text;

                Condicion.InsertarValorFijo = (bool)opcionInsertarValorFijo.IsChecked;

                Condicion.FiltrarPorNumeros = (bool)opcionNumerosFiltroCantidades.IsChecked;
                Condicion.FiltrarPorElementos = (bool)opcionOperandosFiltroCantidades.IsChecked;
                Condicion.AplicarProcesamiento_SinCondiciones = (bool)opcionAplicarProcesamientoSinCondiciones.IsChecked;
                Condicion.AplicarProcesamiento_CantidadesInsertadas_Operandos = (bool)opcionAplicarProcesamientoCantidadesInsertadas.IsChecked;
                Condicion.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos = (bool)opcionAplicarProcesamientoSoloCantidadesInsertadas.IsChecked;
                Condicion.NoIncluirTextosInformacion_CantidadAInsertar = (bool)opcionNoIncluirTextosInformacion.IsChecked;
                Condicion.ReiniciarAcumulacion_OperacionPorFilas = (bool)opcionReiniciarAcumulacion.IsChecked;
                Condicion.AlInsertar_Operar = (bool)opcionOperarAlInsertar.IsChecked;
                Condicion.EsInsercionEdicion = (bool)opcionEsInsercionEdicion.IsChecked;
                Condicion.DesplazarsePosicionAnterior = (bool)opcionDesplazarsePosicionAnterior.IsChecked;
                Condicion.DesplazarsePosicionPosterior = (bool)opcionDesplazarsePosicionPosterior.IsChecked;

                if (opcionOperacionAlInsertar.SelectedItem != null)
                    Condicion.Operacion_AlInsertar = (TipoOperacion_AlInsertar_ProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionOperacionAlInsertar.SelectedItem).Uid);

                DialogResult = true;
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            condicionesFiltroCantidades.Operandos = Operandos;
            condicionesFiltroCantidades.SubOperandos = SubOperandos;
            condicionesFiltroCantidades.Entradas = new List<Entidades.TextosInformacion.DiseñoTextosInformacion>();
            condicionesFiltroCantidades.Elementos = ListaElementos;
            condicionesFiltroCantidades.Definiciones = new List<Entidades.TextosInformacion.DiseñoTextosInformacion>();
            condicionesFiltroCantidades.DefinicionesListas = new List<DiseñoListaCadenasTexto>();
            condicionesFiltroCantidades.ModoProcesamientoCantidades = true;

            if(OpcionesInsertar_OperandosCantidades)
            {
                textoOpcionOperandosInsertar.Visibility = Visibility.Visible;
                opcionOperandosInsertar.Visibility = Visibility.Visible;

                if (ModoDiseñoOperacion)
                {
                    foreach (var item in SubOperandosElemento)
                    {
                        System.Windows.Controls.CheckBox subOperando = new System.Windows.Controls.CheckBox();
                        subOperando.Content = item.NombreCombo;
                        subOperando.Margin = new Thickness(10);
                        subOperando.Padding = new Thickness(10);
                        subOperando.Tag = item;
                        subOperando.Checked += SubOperando_Checked;
                        subOperando.Unchecked += SubOperando_Unchecked;

                        if (Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades.Contains(item))
                            subOperando.IsChecked = true;

                        opcionOperandosInsertar.Children.Add(subOperando);

                        System.Windows.Controls.CheckBox subOperando2 = new System.Windows.Controls.CheckBox();
                        subOperando2.Content = item.NombreCombo;
                        subOperando2.Margin = new Thickness(10);
                        subOperando2.Padding = new Thickness(10);
                        subOperando2.Tag = item;
                        subOperando2.Checked += SubOperando2_Checked;
                        subOperando2.Unchecked += SubOperando2_Unchecked;

                        if (Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Contains(item))
                            subOperando2.IsChecked = true;
                        opcionOperandosInsertar_OperacionAlInsertar.Children.Add(subOperando2);
                    }
                }
                else
                {
                    foreach (var item in OperandosElemento)
                    {
                        System.Windows.Controls.CheckBox operando = new System.Windows.Controls.CheckBox();
                        operando.Content = item.NombreCombo;
                        operando.Margin = new Thickness(10);
                        operando.Padding = new Thickness(10);
                        operando.Tag = item;
                        operando.Checked += Operando_Checked;
                        operando.Unchecked += Operando_Unchecked;
                        
                        if (Condicion.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(item))
                            operando.IsChecked = true;

                        opcionOperandosInsertar.Children.Add(operando);

                        System.Windows.Controls.CheckBox operando2 = new System.Windows.Controls.CheckBox();
                        operando2.Content = item.NombreCombo;
                        operando2.Margin = new Thickness(10);
                        operando2.Padding = new Thickness(10);
                        operando2.Tag = item;
                        operando2.Checked += Operando2_Checked;
                        operando2.Unchecked += Operando2_Unchecked;

                        if (Condicion.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Contains(item))
                            operando2.IsChecked = true;
                        opcionOperandosInsertar_OperacionAlInsertar.Children.Add(operando2);
                    }
                }
            }

            opcionTipoCondicion.SelectedItem = (from ComboBoxItem I in opcionTipoCondicion.Items where I.Uid == ((int)Condicion.Tipo).ToString() select I).FirstOrDefault();
            opcionTipoCondicion_SelectionChanged(this, null);

            opcionTipoElemento.SelectedItem = (from ComboBoxItem I in opcionTipoElemento.Items where I.Uid == ((int)Condicion.TipoElemento).ToString() select I).FirstOrDefault();
            opcionTipoElemento_SelectionChanged(this, null);

            opcionTipoElementoAccion.SelectedItem = (from ComboBoxItem I in opcionTipoElementoAccion.Items where I.Uid == ((int)Condicion.TipoElementoAccion).ToString() select I).FirstOrDefault();
            opcionTipoElementoAccion_SelectionChanged(this, null);

            opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem = (from ComboBoxItem I in opcionTipoElementoAOperar_OperacionAlInsertar.Items where I.Uid == ((int)Condicion.TipoElementoAOperar_OperacionAlInsertar).ToString() select I).FirstOrDefault();
            opcionTipoElementoAOperar_OperacionAlInsertar_SelectionChanged(this, null);

            opcionTipoElementoAccion_Insertar.SelectedItem = (from ComboBoxItem I in opcionTipoElementoAccion_Insertar.Items where I.Uid == ((int)Condicion.TipoElementoAccion_Insertar).ToString() select I).FirstOrDefault();
            opcionTipoElementoAccion_Insertar_SelectionChanged(this, null);

            opcionUbicacionAccion_Insertar.SelectedItem = (from ComboBoxItem I in opcionUbicacionAccion_Insertar.Items where I.Uid == ((int)Condicion.TipoUbicacionAccion_Insertar).ToString() select I).FirstOrDefault();
            opcionUbicacionAccion_Insertar_SelectionChanged(this, null);

            if (opcionNoInsertarPosicionAnterior.Visibility == Visibility.Visible)
                opcionNoInsertarPosicionAnterior.IsChecked = Condicion.NoInsertarCantidad_EnPosicion;

            else if (opcionNoInsertarPosicionPosterior.Visibility == Visibility.Visible)
                opcionNoInsertarPosicionPosterior.IsChecked = Condicion.NoInsertarCantidad_EnPosicion;

            opcionOperacionAlInsertar.SelectedItem = (from ComboBoxItem I in opcionOperacionAlInsertar.Items where I.Uid == ((int)Condicion.Operacion_AlInsertar).ToString() select I).FirstOrDefault();
            opcionOperacionAlInsertar_SelectionChanged(this, null);

            opcionTipoElemento_OperacionAlInsertar.SelectedItem = (from ComboBoxItem I in opcionTipoElemento_OperacionAlInsertar.Items where I.Uid == ((int)Condicion.TipoElemento_OperacionAlInsertar).ToString() select I).FirstOrDefault();
            opcionTipoElemento_OperacionAlInsertar_SelectionChanged(this, null);

            condicionesFiltroCantidades.Condiciones = Condicion.CondicionesCantidades;
            condicionesFiltroCantidades.ListarCondiciones();
            opcionNumerosFiltroCantidades.IsChecked = Condicion.FiltrarPorNumeros;
            opcionOperandosFiltroCantidades.IsChecked = Condicion.FiltrarPorElementos;
            opcionAplicarProcesamientoSinCondiciones.IsChecked = Condicion.AplicarProcesamiento_SinCondiciones;
            opcionAplicarProcesamientoCantidadesInsertadas.IsChecked = Condicion.AplicarProcesamiento_CantidadesInsertadas_Operandos;
            opcionAplicarProcesamientoSoloCantidadesInsertadas.IsChecked = Condicion.AplicarProcesamiento_SoloCantidadesInsertadas_Operandos;
            opcionNoIncluirTextosInformacion.IsChecked = Condicion.NoIncluirTextosInformacion_CantidadAInsertar;
            opcionReiniciarAcumulacion.IsChecked = Condicion.ReiniciarAcumulacion_OperacionPorFilas;
            opcionOperarAlInsertar.IsChecked = Condicion.AlInsertar_Operar;

            if (opcionOperarAlInsertar.IsChecked == true)
                opcionOperarAlInsertar_Checked(this, null);
            else if (opcionOperarAlInsertar.IsChecked == false)
                opcionOperarAlInsertar_Unchecked(this, null);

            opcionEsInsercionEdicion.IsChecked = Condicion.EsInsercionEdicion;
            opcionDesplazarsePosicionAnterior.IsChecked = Condicion.DesplazarsePosicionAnterior;
            opcionDesplazarsePosicionPosterior.IsChecked = Condicion.DesplazarsePosicionPosterior;

            opcionInsertarValorFijo.IsChecked = Condicion.InsertarValorFijo;

            if(opcionInsertarValorFijo.IsChecked == true)
                opcionInsertarValorFijo_Checked(this, null);
            else
                opcionInsertarValorFijo_Unchecked(this, null);

            valorFijo.Text = Condicion.ValorFijo_Insercion.ToString();
            valorFijo_TextChanged(this, null);

            opcionValoresPosiciones_TipoElementoAccion_Insertar.Text = Condicion.ValorPosicion_TipoElementoAccion_Insertar.ToString();
            opcionValoresPosiciones_TipoElementoAccion_Insertar_TextChanged(this, null);

            opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Text = Condicion.ValorPosicion_TipoElemento_OperacionAlInsertar.ToString();
            opcionValoresPosiciones_TipoElemento_OperacionAlInsertar_TextChanged(this, null);

            opcionValoresPosiciones_UbicacionAccion_Insertar.Text = Condicion.ValorPosicion_UbicacionAccion_Insertar.ToString();
            opcionValoresPosiciones_UbicacionAccion_Insertar_TextChanged(this, null);

            valorFijo_OperacionAlInsertar.Text = Condicion.ValorFijo_OperacionAlInsertar;

            if (OpcionesOperandoNumeros)
                opcionesOperandoNumeros.Visibility = Visibility.Visible;

            //if (Condicion.Tipo == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes &&
            //        (Condicion.TipoElemento == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
            //        Condicion.TipoElemento == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados))
            //{
            //    opcionAplicarProcesamientoCantidadesInsertadas.Visibility = Visibility.Visible;
            //    opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
            //    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;


            //        textoOpcionTipoElementoAccion_Insertar.Visibility = Visibility.Visible;

            //        if (Condicion.TipoElementoAccion !=
            //    TipoOpcionElementoAccionProcesamientoCantidades.ValorFijo)
            //        {
            //            opcionTipoElementoAccion_Insertar.Visibility = Visibility.Visible;
            //            valorFijo.Visibility = Visibility.Collapsed;
            //        }
            //        else
            //        {
            //            opcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
            //            valorFijo.Visibility = Visibility.Visible;
            //        }

            //}
            //else
            //{
            //    opcionAplicarProcesamientoCantidadesInsertadas.Visibility = Visibility.Collapsed;
            //    opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
            //    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;


            //        textoOpcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
            //        opcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
            //        valorFijo.Visibility = Visibility.Collapsed;

            //}
            MostrarOcultarOpcionAplicarCantidadesInsertadas();

            if(MostrarReiniciarAcumulacion)
            {
                opcionReiniciarAcumulacion.Visibility = Visibility.Visible;
            }
            else
            {
                opcionReiniciarAcumulacion.Visibility = Visibility.Collapsed;
            }
        }

        private void SubOperando2_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Remove(subOperando);
            }
        }

        private void SubOperando2_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Add(subOperando);
            }
        }

        private void Operando2_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Remove(operando);
            }
        }

        private void Operando2_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoCantidades_OperacionAlInsertar.Add(operando);
            }
        }

        private void SubOperando_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades.Remove(subOperando);
            }
        }

        private void SubOperando_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoCantidades.Add(subOperando);
            }
        }

        private void Operando_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoCantidades.Remove(operando);
            }
        }

        private void Operando_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.OperandosInsertar_CantidadesProcesamientoCantidades.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoCantidades.Add(operando);
            }
        }

        private void opcionTipoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsLoaded)
            {
                if(OpcionesInsertar && opcionTipoCondicion.SelectedItem != null &&
                    ((TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes))
                {
                    textoOpcionTipoElemento.Text = "Elemento donde insertar:";

                    textoOpcionTipoElementoAccion.Text = "Elemento a insertar:";
                    textoOpcionTipoElementoAccion.Visibility = Visibility.Visible;
                    opcionTipoElementoAccion.Visibility = Visibility.Visible;

                    opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                    opcionUbicacionAccion_Insertar_SelectionChanged(sender, e);

                }
                //else if ((TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.QuitarCantidadActual)
                //{
                //    textoOpcionTipoElementoAccion.Text = "Elemento a quitar:";
                //    textoOpcionTipoElementoAccion.Visibility = Visibility.Visible;
                //    opcionTipoElementoAccion.Visibility = Visibility.Visible;
                //}
                else
                {
                    textoOpcionTipoElemento.Text = "Elemento donde realizar esta acción:";

                    textoOpcionTipoElementoAccion.Visibility = Visibility.Collapsed;
                    opcionTipoElementoAccion.Visibility = Visibility.Collapsed;

                    textoOpcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;

                    textoOpcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;

                    opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                    textoOpcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                }

                MostrarOcultarOpcionAplicarCantidadesInsertadas();

                if(OpcionesInsertar && opcionTipoCondicion.SelectedItem != null &&
                    (TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes)
                {
                    opcionOperarAlInsertar.Visibility = Visibility.Visible;
                    opcionOperarAlInsertar_Checked(this, e);
                    textoOpcionEsInsercionEdicion.Visibility = Visibility.Visible;
                    opcionEsInsercionEdicion.Visibility = Visibility.Visible;
                    opcionesInsertar_Posiciones.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionOperarAlInsertar.Visibility = Visibility.Collapsed;
                    opcionesOperarAlInsertar.Visibility = Visibility.Collapsed;
                    textoOpcionEsInsercionEdicion.Visibility = Visibility.Collapsed;
                    opcionEsInsercionEdicion.Visibility = Visibility.Collapsed;
                    opcionesInsertar_Posiciones.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoElemento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarOcultarOpcionAplicarCantidadesInsertadas();
        }

        private void MostrarOcultarOpcionAplicarCantidadesInsertadas()
        {
            if (IsLoaded &&
                opcionTipoCondicion.SelectedItem != null &&
                opcionTipoElemento.SelectedItem != null)
            {
                if(OpcionesInsertar && 
                    ((TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes &&
                    ((TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados)))
                {
                    opcionAplicarProcesamientoCantidadesInsertadas.Visibility = Visibility.Visible;
                    opcionAplicarProcesamientoSoloCantidadesInsertadas.Visibility = Visibility.Visible;
                    opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                    opcionUbicacionAccion_Insertar_SelectionChanged(this, null);

                    textoOpcionTipoElementoAccion_Insertar.Visibility = Visibility.Visible;

                }
                else if (OpcionesInsertar &&
                    ((TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes &&
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Resultados))
                {
                    opcionAplicarProcesamientoCantidadesInsertadas.Visibility = Visibility.Visible;
                    opcionAplicarProcesamientoSoloCantidadesInsertadas.Visibility = Visibility.Visible;
                    opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                    opcionUbicacionAccion_Insertar_SelectionChanged(this, null);

                    textoOpcionTipoElementoAccion_Insertar.Visibility = Visibility.Visible;

                }
                else
                {
                    opcionAplicarProcesamientoCantidadesInsertadas.Visibility = Visibility.Collapsed;
                    opcionAplicarProcesamientoSoloCantidadesInsertadas.Visibility = Visibility.Collapsed;
                    opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                    textoOpcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                    textoOpcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                    valorFijo.Visibility = Visibility.Collapsed;

                    textoOpcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;

                }

                if (opcionTipoCondicion.SelectedItem != null &&
                ((TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes))
                {
                    opcionNoIncluirTextosInformacion.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionNoIncluirTextosInformacion.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionOperarAlInsertar_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if ((TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes &&
                    opcionOperarAlInsertar.IsChecked == true)
                {
                    opcionesOperarAlInsertar.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionesOperarAlInsertar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionOperarAlInsertar_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (opcionTipoCondicion.SelectedItem != null &&
                    (TipoOpcionCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == 
                    TipoOpcionCondicionProcesamientoCantidades.InsertarCantidadesSiguientes &&
                    opcionOperarAlInsertar.IsChecked == true)
                {
                    opcionesOperarAlInsertar.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionesOperarAlInsertar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoElementoAccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                MostrarOcultarOpcionAplicarCantidadesInsertadas();
            }
        }

        private void valorFijo_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;
            if(!double.TryParse(valorFijo.Text, out numero))
            {
                valorFijo.Text = "0";
            }
        }
                
        private void botonOpcionRedondearCantidadesOperacionInterna_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Condicion != null)
                {
                    DefinirOperacion_RedondearCantidades definir = new DefinirOperacion_RedondearCantidades();
                    definir.config = Condicion.ConfigRedondeo_OperacionInterna.CopiarObjeto();
                    definir.ModoComportamiento = true;

                    bool opcionElegida = (bool)definir.ShowDialog();
                    if (opcionElegida)
                    {
                        Condicion.ConfigRedondeo_OperacionInterna.RedondearPar_Cercano = definir.config.RedondearPar_Cercano;
                        Condicion.ConfigRedondeo_OperacionInterna.RedondearNumero_LejanoDeCero = definir.config.RedondearNumero_LejanoDeCero;
                        Condicion.ConfigRedondeo_OperacionInterna.RedondearNumero_CercanoDeCero = definir.config.RedondearNumero_CercanoDeCero;
                        Condicion.ConfigRedondeo_OperacionInterna.RedondearNumero_Mayor = definir.config.RedondearNumero_Mayor;
                        Condicion.ConfigRedondeo_OperacionInterna.RedondearNumero_Menor = definir.config.RedondearNumero_Menor;

                    }
                }
            }
        }

        private void opcionOperacionAlInsertar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            botonOpcionRedondearCantidadesOperacionInterna.Visibility = Visibility.Collapsed;

            if ((TipoOperacion_AlInsertar_ProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionOperacionAlInsertar.SelectedItem).Uid) == TipoOperacion_AlInsertar_ProcesamientoCantidades.RedondearCantidades)
            {
                botonOpcionRedondearCantidadesOperacionInterna.Visibility = Visibility.Visible;
            }

            if(int.Parse(((ComboBoxItem)opcionOperacionAlInsertar.SelectedItem).Uid) > 5)
            {
                opcionesOperacionAlInsertar.Visibility = Visibility.Visible;

                textoOpcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Visible;
                opcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Visible;

                opcionTipoElementoAOperar_OperacionAlInsertar_SelectionChanged(this, null);
                opcionTipoElemento_OperacionAlInsertar_SelectionChanged(this, null);
            }
            else
            {
                opcionesOperacionAlInsertar.Visibility = Visibility.Collapsed;

                textoOpcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                opcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                textoOpcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                opcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                valorFijo_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                textoOpcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionAplicarProcesamientoCantidadesInsertadas_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                opcionAplicarProcesamientoSoloCantidadesInsertadas.IsChecked = false;
            }
        }

        private void opcionAplicarProcesamientoSoloCantidadesInsertadas_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionAplicarProcesamientoCantidadesInsertadas.IsChecked = false;
            }
        }

        private void opcionDesplazarsePosicionAnterior_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionDesplazarsePosicionPosterior.IsChecked = false;
            }
        }

        private void opcionDesplazarsePosicionPosterior_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionDesplazarsePosicionAnterior.IsChecked = false;
            }
        }

        private void opcionUbicacionAccion_Insertar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded &&
                opcionUbicacionAccion_Insertar.SelectedItem != null)
            {
                if (int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == 2)
                {
                    opcionNoInsertarPosicionAnterior.Visibility = Visibility.Visible;
                    opcionNoInsertarPosicionPosterior.Visibility = Visibility.Collapsed;
                }
                else if (int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == 3)
                {
                    opcionNoInsertarPosicionAnterior.Visibility = Visibility.Collapsed;
                    opcionNoInsertarPosicionPosterior.Visibility = Visibility.Visible;
                }
                else
                {
                    opcionNoInsertarPosicionAnterior.Visibility = Visibility.Collapsed;
                    opcionNoInsertarPosicionPosterior.Visibility = Visibility.Collapsed;
                }

                if (IsLoaded)
                {
                    if ((TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecifica |
                        (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaAnteriores |
                        (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaSiguientes |
                        (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploAnteriores |
                        (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) == TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.UbicacionEspecificaDesplazadaMultiploSiguientes)
                    {
                        textoOpcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Visible;
                        opcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoOpcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                        opcionValoresPosiciones_UbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void ProcesarOpcionValorFijo()
        {
            if (opcionInsertarValorFijo.IsChecked == false)
            {
                opcionTipoElementoAccion_Insertar.Visibility = Visibility.Visible;
                valorFijo.Visibility = Visibility.Collapsed;

                opcionTipoElementoAccion_Insertar_SelectionChanged(this, null);
            }
            else if(opcionInsertarValorFijo.IsChecked == true)
            {
                opcionTipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                valorFijo.Visibility = Visibility.Visible;

                textoOpcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                opcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionTipoElementoAccion_Insertar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(IsLoaded &&
                opcionTipoElementoAccion_Insertar.SelectedItem != null)
            {
                if((TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion_Insertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion_Insertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion_Insertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion_Insertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAccion_Insertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes)
                {
                    textoOpcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Visible;
                    opcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_TipoElementoAccion_Insertar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionValoresPosiciones_TipoElementoAccion_Insertar_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;
            if (!double.TryParse(opcionValoresPosiciones_TipoElementoAccion_Insertar.Text, out numero))
            {
                opcionValoresPosiciones_TipoElementoAccion_Insertar.Text = "0";
            }
        }

        private void opcionValoresPosiciones_UbicacionAccion_Insertar_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;
            if (!double.TryParse(opcionValoresPosiciones_UbicacionAccion_Insertar.Text, out numero))
            {
                opcionValoresPosiciones_UbicacionAccion_Insertar.Text = "0";
            }
        }

        private void opcionValoresPosiciones_TipoElemento_OperacionAlInsertar_TextChanged(object sender, TextChangedEventArgs e)
        {
            double numero = 0;
            if (!double.TryParse(opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Text, out numero))
            {
                opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Text = "0";
            }
        }

        private void opcionTipoElemento_OperacionAlInsertar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded &&
                opcionTipoElemento_OperacionAlInsertar.SelectedItem != null)
            {
                if ((TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecifica |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaAnteriores |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaSiguientes |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploAnteriores |
                    (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccion_InsertarProcesamientoCantidades.CantidadPosicionEspecificaDesplazadaMultiploSiguientes)
                {
                    textoOpcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Visible;
                    opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Visible;
                }
                else
                {
                    textoOpcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void opcionTipoElementoAOperar_OperacionAlInsertar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded &&
                opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem != null)
            {
                if (((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados))
                {
                    textoOpcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Visible;
                    opcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Visible;
                    valorFijo_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                    textoOpcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Visible;
                    opcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Visible;

                    opcionTipoElemento_OperacionAlInsertar_SelectionChanged(this, null);

                }
                else if ((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.Resultados)
                {
                    textoOpcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    opcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    valorFijo_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                    textoOpcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    opcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                    textoOpcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                }
                else if ((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoAOperar_OperacionAlInsertar.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.ValorFijo)
                {
                    textoOpcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Visible;
                    opcionTipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    valorFijo_OperacionAlInsertar.Visibility = Visibility.Visible;

                    textoOpcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    opcionOperandosInsertar_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                    textoOpcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;
                    opcionValoresPosiciones_TipoElemento_OperacionAlInsertar.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void valorFijo_OperacionAlInsertar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //double numero = 0;
            //if (!double.TryParse(valorFijo_OperacionAlInsertar.Text, out numero))
            //{
            //    valorFijo_OperacionAlInsertar.Text = "0";
            //}
        }

        private void opcionInsertarValorFijo_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                ProcesarOpcionValorFijo();
            }
        }

        private void opcionInsertarValorFijo_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                ProcesarOpcionValorFijo();
            }
        }
    }
}
