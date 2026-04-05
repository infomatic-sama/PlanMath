using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades;
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
    /// Lógica de interacción para AgregarQuitar_CondicionProcesamientoTextosInformacion.xaml
    /// </summary>
    public partial class AgregarQuitar_CondicionProcesamientoTextosInformacion : Window
    {
        public CondicionProcesamientoTextosInformacion Condicion { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoOperacion> OperandosElemento { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosElemento { get; set; }
        public bool ModoDiseñoOperacion { get; set; }
        public bool MostrarReiniciarAcumulacion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public AgregarQuitar_CondicionProcesamientoTextosInformacion()
        {
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
                    Condicion.Tipo = (TipoOpcionCondicionProcesamientoTextosInformacion)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid);

                if (opcionTipoElemento.SelectedItem != null)
                    Condicion.TipoElementoDesde = (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid);

                if (opcionTipoElementoDesde.SelectedItem != null)
                    Condicion.TipoElementoDonde = (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid);

                if (opcionUbicacionElementoAccion.SelectedItem != null)
                    Condicion.UbicacionElementoAccion = (TipoOpcionElementoAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionElementoAccion.SelectedItem).Uid);

                if (opcionUbicacionAccion_Insertar.SelectedItem != null)
                    Condicion.TipoUbicacionAccion_Insertar = (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid);

                Condicion.CondicionesTextosInformacion = condicionesFiltroCantidades.Condiciones;
                Condicion.ValorFijo_Insercion = valorFijo.Text;

                Condicion.FiltrarPorNumeros = (bool)opcionNumerosFiltroCantidades.IsChecked;
                Condicion.FiltrarPorElementos = (bool)opcionOperandosFiltroCantidades.IsChecked;
                Condicion.AplicarProcesamiento_SinCondiciones = (bool)opcionAplicarProcesamientoSinCondiciones.IsChecked;
                Condicion.ReiniciarAcumulacion_OperacionPorFilas = (bool)opcionReiniciarAcumulacion.IsChecked;
                Condicion.ProcesarCadenasTextos_SinCumplirCondiciones_Textos = (bool)opcionProcesarCadenasTexto_SinCumplirCondiciones_CadenasTextos.IsChecked;
                Condicion.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades = (bool)opcionAplicarProcesamientoTexto_SoloACantidadesInsertadasProcesamientoCantidades.IsChecked;
                Condicion.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades = (bool)opcionAplicarProcesamientoTexto_ACantidadesInsertadasProcesamientoCantidades.IsChecked;

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
            condicionesFiltroCantidades.ModoProcesamientoCantidades = true;
            //if (OpcionesInsertar_OperandosCantidades)
            //{
                //textoOpcionOperandosInsertar.Visibility = Visibility.Visible;
                //opcionOperandosInsertar.Visibility = Visibility.Visible;

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

                        if (Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(item))
                            subOperando.IsChecked = true;

                        opcionOperandos.Children.Add(subOperando);

                    System.Windows.Controls.CheckBox subOperandoDesde = new System.Windows.Controls.CheckBox();
                    subOperandoDesde.Content = item.NombreCombo;
                    subOperandoDesde.Margin = new Thickness(10);
                    subOperandoDesde.Padding = new Thickness(10);
                    subOperandoDesde.Tag = item;
                    subOperandoDesde.Checked += SubOperandoDesde_Checked;
                    subOperandoDesde.Unchecked += SubOperandoDesde_Unchecked;

                    if (Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(item))
                        subOperandoDesde.IsChecked = true;

                    opcionOperandosDesde.Children.Add(subOperandoDesde);
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

                        if (Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(item))
                            operando.IsChecked = true;

                        opcionOperandos.Children.Add(operando);

                    System.Windows.Controls.CheckBox operandoDesde = new System.Windows.Controls.CheckBox();
                    operandoDesde.Content = item.NombreCombo;
                    operandoDesde.Margin = new Thickness(10);
                    operandoDesde.Padding = new Thickness(10);
                    operandoDesde.Tag = item;
                    operandoDesde.Checked += OperandoDesde_Checked;
                    operandoDesde.Unchecked += OperandoDesde_Unchecked;

                    if (Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(item))
                        operandoDesde.IsChecked = true;

                    opcionOperandosDesde.Children.Add(operandoDesde);
                }
                }
            //}

            opcionTipoElemento.SelectedItem = (from ComboBoxItem I in opcionTipoElemento.Items where I.Uid == ((int)Condicion.TipoElementoDesde).ToString() select I).FirstOrDefault();
            opcionTipoCondicion.SelectedItem = (from ComboBoxItem I in opcionTipoCondicion.Items where I.Uid == ((int)Condicion.Tipo).ToString() select I).FirstOrDefault();
            opcionTipoElementoDesde.SelectedItem = (from ComboBoxItem I in opcionTipoElementoDesde.Items where I.Uid == ((int)Condicion.TipoElementoDonde).ToString() select I).FirstOrDefault();

            opcionUbicacionElementoAccion.SelectedItem = (from ComboBoxItem I in opcionUbicacionElementoAccion.Items where I.Uid == ((int)Condicion.UbicacionElementoAccion).ToString() select I).FirstOrDefault();
            opcionUbicacionAccion_Insertar.SelectedItem = (from ComboBoxItem I in opcionUbicacionAccion_Insertar.Items where I.Uid == ((int)Condicion.TipoUbicacionAccion_Insertar).ToString() select I).FirstOrDefault();
            condicionesFiltroCantidades.Condiciones = Condicion.CondicionesTextosInformacion;
            condicionesFiltroCantidades.ListarCondiciones();
            opcionNumerosFiltroCantidades.IsChecked = Condicion.FiltrarPorNumeros;
            opcionOperandosFiltroCantidades.IsChecked = Condicion.FiltrarPorElementos;
            opcionAplicarProcesamientoSinCondiciones.IsChecked = Condicion.AplicarProcesamiento_SinCondiciones;
            opcionProcesarCadenasTexto_SinCumplirCondiciones_CadenasTextos.IsChecked = Condicion.ProcesarCadenasTextos_SinCumplirCondiciones_Textos;
            opcionReiniciarAcumulacion.IsChecked = Condicion.ReiniciarAcumulacion_OperacionPorFilas;
            opcionAplicarProcesamientoTexto_SoloACantidadesInsertadasProcesamientoCantidades.IsChecked = Condicion.AplicarProcesamiento_SoloCantidadesInsertadas_ProcesamientoCantidades;
            opcionAplicarProcesamientoTexto_ACantidadesInsertadasProcesamientoCantidades.IsChecked = Condicion.AplicarProcesamiento_CantidadesInsertadas_ProcesamientoCantidades;

            valorFijo.Text = Condicion.ValorFijo_Insercion;

            MostrarOcultarOpcionAplicarCantidadesInsertadas();

            if (MostrarReiniciarAcumulacion)
            {
                opcionReiniciarAcumulacion.Visibility = Visibility.Visible;
            }
            else
            {
                opcionReiniciarAcumulacion.Visibility = Visibility.Collapsed;
            }
        }

        private void SubOperando_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Remove(subOperando);
            }
        }

        private void SubOperando_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Desde.Add(subOperando);
            }
        }

        private void Operando_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Desde.Remove(operando);
            }
        }

        private void Operando_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Desde.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Desde.Add(operando);
            }
        }

        private void SubOperandoDesde_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Remove(subOperando);
            }
        }

        private void SubOperandoDesde_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoElementoOperacion subOperando = (DiseñoElementoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(subOperando))
                    Condicion.SubOperandosInsertar_CantidadesProcesamientoTextos_Donde.Add(subOperando);
            }
        }

        private void OperandoDesde_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == false)
            {
                if (Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Donde.Remove(operando);
            }
        }

        private void OperandoDesde_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox check = (System.Windows.Controls.CheckBox)sender;
            DiseñoOperacion operando = (DiseñoOperacion)check.Tag;

            if (check.IsChecked == true)
            {
                if (!Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Donde.Contains(operando))
                    Condicion.OperandosInsertar_CantidadesProcesamientoTextos_Donde.Add(operando);
            }
        }

        private void opcionTipoCondicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
                MostrarOcultarOpcionAplicarCantidadesInsertadas();
        }

        private void opcionTipoElemento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarOcultarOpcionAplicarCantidadesInsertadas();
        }

        private void opcionTipoElementoAccion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                MostrarOcultarOpcionAplicarCantidadesInsertadas();
            }
        }
        private void MostrarOcultarOpcionAplicarCantidadesInsertadas()
        {
            if (IsLoaded &&
                opcionTipoCondicion.SelectedItem != null &&
                opcionTipoElemento.SelectedItem != null)
            {
                if (((TipoOpcionCondicionProcesamientoTextosInformacion)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoTextosInformacion.InsertarTextosExistentes &&
                    ((TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados)))
                {
                    textoOpcionOperandos.Visibility = Visibility.Visible;
                    opcionOperandos.Visibility = Visibility.Visible;

                    textoOpcionTipoElementoDesde.Visibility = Visibility.Visible;
                    opcionTipoElementoDesde.Visibility = Visibility.Visible;

                    if(opcionTipoElementoDesde.SelectedItem != null && ((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados))
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Visible;
                        opcionOperandosDesde.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Collapsed;
                        opcionOperandosDesde.Visibility = Visibility.Collapsed;
                    }

                    if (opcionTipoElementoDesde.SelectedItem != null)
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Visible;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Visible;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                        //valorFijo.Visibility = Visibility.Visible;
                        opcionUbicacionAccion_Insertar_SelectionChanged(this, null);
                    }
                    else
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Collapsed;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Collapsed;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                        valorFijo.Visibility = Visibility.Collapsed;
                    }

                    

                }                
                else if (((TipoOpcionCondicionProcesamientoTextosInformacion)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoTextosInformacion.InsertarTextosExistentes &&
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Resultados))
                {
                    textoOpcionOperandos.Visibility = Visibility.Collapsed;
                    opcionOperandos.Visibility = Visibility.Collapsed;

                    textoOpcionTipoElementoDesde.Visibility = Visibility.Visible;
                    opcionTipoElementoDesde.Visibility = Visibility.Visible;

                    if (opcionTipoElementoDesde.SelectedItem != null && ((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados))
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Visible;
                        opcionOperandosDesde.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Collapsed;
                        opcionOperandosDesde.Visibility = Visibility.Collapsed;
                    }

                    if (opcionTipoElementoDesde.SelectedItem != null)
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Visible;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Visible;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                        //valorFijo.Visibility = Visibility.Visible;
                        opcionUbicacionAccion_Insertar_SelectionChanged(this, null);
                    }
                    else
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Collapsed;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Collapsed;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                        valorFijo.Visibility = Visibility.Collapsed;
                    }

                }
                else if((TipoOpcionCondicionProcesamientoTextosInformacion)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoTextosInformacion.EditarTextos &&
                    ((TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados))
                {
                    textoOpcionOperandos.Visibility = Visibility.Visible;
                    opcionOperandos.Visibility = Visibility.Visible;

                    textoOpcionTipoElementoDesde.Visibility = Visibility.Visible;
                    opcionTipoElementoDesde.Visibility = Visibility.Visible;

                    if (opcionTipoElementoDesde.SelectedItem != null && ((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados))
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Visible;
                        opcionOperandosDesde.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Collapsed;
                        opcionOperandosDesde.Visibility = Visibility.Collapsed;
                    }

                    if (opcionTipoElementoDesde.SelectedItem != null)
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Visible;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Visible;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                        //valorFijo.Visibility = Visibility.Visible;
                        opcionUbicacionAccion_Insertar_SelectionChanged(this, null);
                    }
                    else
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Collapsed;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Collapsed;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                        valorFijo.Visibility = Visibility.Collapsed;
                    }
                }
                else if (((TipoOpcionCondicionProcesamientoTextosInformacion)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoTextosInformacion.EditarTextos &&
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Resultados))
                {
                    textoOpcionOperandos.Visibility = Visibility.Collapsed;
                    opcionOperandos.Visibility = Visibility.Collapsed;

                    textoOpcionTipoElementoDesde.Visibility = Visibility.Visible;
                    opcionTipoElementoDesde.Visibility = Visibility.Visible;

                    if (opcionTipoElementoDesde.SelectedItem != null && ((TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoAccionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElementoDesde.SelectedItem).Uid) == TipoOpcionElementoAccionProcesamientoCantidades.OperandosYResultados))
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Visible;
                        opcionOperandosDesde.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoOpcionOperandosDesde.Visibility = Visibility.Collapsed;
                        opcionOperandosDesde.Visibility = Visibility.Collapsed;
                    }

                    if (opcionTipoElementoDesde.SelectedItem != null)
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Visible;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Visible;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Visible;

                        //valorFijo.Visibility = Visibility.Visible;
                        opcionUbicacionAccion_Insertar_SelectionChanged(this, null);
                    }
                    else
                    {
                        textoUbicacionElementoAccion.Visibility = Visibility.Collapsed;
                        opcionUbicacionElementoAccion.Visibility = Visibility.Collapsed;

                        opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                        textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                        valorFijo.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {   
                    textoOpcionTipoElementoDesde.Visibility = Visibility.Collapsed;
                    opcionTipoElementoDesde.Visibility = Visibility.Collapsed;

                    if ((TipoOpcionCondicionProcesamientoTextosInformacion)int.Parse(((ComboBoxItem)opcionTipoCondicion.SelectedItem).Uid) == TipoOpcionCondicionProcesamientoTextosInformacion.QuitarTextos &&
                        ((TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.Operando |
                    (TipoOpcionElementoCondicionProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionTipoElemento.SelectedItem).Uid) == TipoOpcionElementoCondicionProcesamientoCantidades.OperandosYResultados))
                    {
                        textoOpcionOperandos.Visibility = Visibility.Visible;
                        opcionOperandos.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        textoOpcionOperandos.Visibility = Visibility.Collapsed;
                        opcionOperandos.Visibility = Visibility.Collapsed;
                    }
                    
                    opcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;
                    textoOpcionUbicacionAccion_Insertar.Visibility = Visibility.Collapsed;

                    valorFijo.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void opcionUbicacionAccion_Insertar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (opcionUbicacionAccion_Insertar.SelectedItem != null &&
                        (TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades)int.Parse(((ComboBoxItem)opcionUbicacionAccion_Insertar.SelectedItem).Uid) ==
                    TipoOpcionUbicacionAccion_InsertarProcesamientoCantidades.ValorFijo)
            {
                valorFijo.Visibility = Visibility.Visible;
            }
            else
            {
                valorFijo.Visibility = Visibility.Collapsed;
            }
        }

        private void opcionAplicarProcesamientoTexto_ACantidadesInsertadasProcesamientoCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                opcionAplicarProcesamientoTexto_SoloACantidadesInsertadasProcesamientoCantidades.IsChecked = false;
            }
        }

        private void opcionAplicarProcesamientoTexto_SoloACantidadesInsertadasProcesamientoCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                opcionAplicarProcesamientoTexto_ACantidadesInsertadasProcesamientoCantidades.IsChecked = false;
            }
        }
    }
}
