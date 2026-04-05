using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
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
    /// Lógica de interacción para OpcionesProcesamientoTextosInformacion.xaml
    /// </summary>
    public partial class OpcionesProcesamientoTextosInformacion : Window
    {
        public List<CondicionProcesamientoTextosInformacion> ProcesamientoTextosInformacion { get; set; }
        Brush FondoNormal;
        Brush FondoSeleccionado = System.Windows.Media.Brushes.LightBlue;
        TextBlock TextoDefinicionSeleccionado;
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoOperacion> OperandosElemento { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosElemento { get; set; }
        public bool NoConservarCambiosOperandos_ProcesamientoTextosInformacion { get; set; }
        public bool AplicarProcesamientoAntesImplicacionesTextosInformacion { get; set; }
        public bool AplicarProcesamientoDespuesImplicacionesTextosInformacion { get; set; }
        public bool ModoDiseñoOperacion { get; set; }
        public bool MostrarReiniciarAcumulacion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public bool EjecutarLogicasTextos_DespuesEjecucion { get; set; }
        public bool EjecutarLogicasTextos_AntesEjecucion { get; set; }
        public OpcionesProcesamientoTextosInformacion()
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
            DialogResult = true;
            Close();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionProcesamientoTextosInformacion condicion = new AgregarQuitar_CondicionProcesamientoTextosInformacion();
            condicion.Operandos = Operandos;
            condicion.ListaElementos = ListaElementos;
            condicion.SubOperandos = SubOperandos;
            condicion.Condicion = new CondicionProcesamientoTextosInformacion();
            //condicion.OpcionesOperandoNumeros = OpcionesOperandoNumeros;
            //condicion.OpcionesInsertar = OpcionesInsertar;
            //condicion.OpcionesInsertar_OperandosCantidades = OpcionesInsertar_OperandosCantidades;
            condicion.OperandosElemento = OperandosElemento;
            condicion.SubOperandosElemento = SubOperandosElemento;
            condicion.ModoDiseñoOperacion = ModoDiseñoOperacion;
            condicion.MostrarReiniciarAcumulacion = MostrarReiniciarAcumulacion;

            bool agregada = (bool)condicion.ShowDialog();
            if (agregada)
            {
                ProcesamientoTextosInformacion.Add(condicion.Condicion);
                TextBlock textoDefinicion = new TextBlock();
                FondoNormal = Background.Clone();
                textoDefinicion.Margin = new Thickness(10);
                textoDefinicion.Padding = new Thickness(5);
                textoDefinicion.Tag = condicion.Condicion;
                textoDefinicion.Text = GenerarTextoCondicion(condicion.Condicion);
                textoDefinicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                condicionesProcesamientoTextosInformacion.Children.Add(textoDefinicion);
            }
        }

        private void TextoDefinicion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (TextBlock item in condicionesProcesamientoTextosInformacion.Children)
            {
                item.Background = FondoNormal.Clone();
            }

            ((TextBlock)sender).Background = FondoSeleccionado.Clone();
            TextoDefinicionSeleccionado = (TextBlock)sender;
        }

        private string GenerarTextoCondicion(CondicionProcesamientoTextosInformacion definicion)
        {
            string Texto = string.Empty;
            Texto = (ProcesamientoTextosInformacion.IndexOf(definicion) + 1).ToString() +
                " - " + definicion.MostrarEtiquetaCondiciones();
            return Texto;
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null)
            {
                CondicionProcesamientoTextosInformacion definicion = ((CondicionProcesamientoTextosInformacion)TextoDefinicionSeleccionado.Tag);

                AgregarQuitar_CondicionProcesamientoTextosInformacion establecer = new AgregarQuitar_CondicionProcesamientoTextosInformacion();
                establecer.Operandos = Operandos;
                establecer.ListaElementos = ListaElementos;
                establecer.SubOperandos = SubOperandos;
                establecer.Condicion = definicion;
                //establecer.OpcionesOperandoNumeros = OpcionesOperandoNumeros;
                //establecer.OpcionesInsertar = OpcionesInsertar;
                //establecer.OpcionesInsertar_OperandosCantidades = OpcionesInsertar_OperandosCantidades;
                establecer.OperandosElemento = OperandosElemento;
                establecer.SubOperandosElemento = SubOperandosElemento;
                establecer.ModoDiseñoOperacion = ModoDiseñoOperacion;
                establecer.MostrarReiniciarAcumulacion = MostrarReiniciarAcumulacion;

                bool editada = (bool)establecer.ShowDialog();

                if (editada)
                {
                    TextoDefinicionSeleccionado.Tag = establecer.Condicion;
                    TextoDefinicionSeleccionado.Text = GenerarTextoCondicion(establecer.Condicion);
                }

            }
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null)
            {
                ProcesamientoTextosInformacion.Remove((CondicionProcesamientoTextosInformacion)TextoDefinicionSeleccionado.Tag);
                condicionesProcesamientoTextosInformacion.Children.Remove(TextoDefinicionSeleccionado);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCondiciones();

            opcionLogicasTextosAntes_EjecucionOperacion.IsChecked = EjecutarLogicasTextos_AntesEjecucion;
            opcionLogicasTextoDespues_EjecucionOperacion.IsChecked = EjecutarLogicasTextos_DespuesEjecucion;
            opcionNoConservarCambiosProcesamientoTextosInformacion.IsChecked = NoConservarCambiosOperandos_ProcesamientoTextosInformacion;
            opcionAplicarProcesamientoAntesImplicacionesTextosInformacion.IsChecked = AplicarProcesamientoAntesImplicacionesTextosInformacion;
            opcionAplicarProcesamientoDespuesImplicacionesTextosInformacion.IsChecked = AplicarProcesamientoDespuesImplicacionesTextosInformacion;
        }

        private void opcionNoConservarCambiosProcesamientoTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                NoConservarCambiosOperandos_ProcesamientoTextosInformacion = (bool)opcionNoConservarCambiosProcesamientoTextosInformacion.IsChecked;
            }
        }

        private void opcionNoConservarCambiosProcesamientoTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                NoConservarCambiosOperandos_ProcesamientoTextosInformacion = (bool)opcionNoConservarCambiosProcesamientoTextosInformacion.IsChecked;
            }
        }

        private void ListarCondiciones()
        {
            foreach (var item in ProcesamientoTextosInformacion)
            {
                TextBlock textoDefinicion = new TextBlock();
                FondoNormal = Background.Clone();
                textoDefinicion.Margin = new Thickness(10);
                textoDefinicion.Padding = new Thickness(5);
                textoDefinicion.Tag = item;
                textoDefinicion.Text = GenerarTextoCondicion(item);
                textoDefinicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                condicionesProcesamientoTextosInformacion.Children.Add(textoDefinicion);
            }
        }

        private void moverCondicionAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null &&
                ProcesamientoTextosInformacion.Any())
            {
                CondicionProcesamientoTextosInformacion definicion = ((CondicionProcesamientoTextosInformacion)TextoDefinicionSeleccionado.Tag);

                int indice = ProcesamientoTextosInformacion.IndexOf(definicion);

                if (indice - 1 > -1)
                {
                    ProcesamientoTextosInformacion.RemoveAt(indice);
                    ProcesamientoTextosInformacion.Insert(indice - 1, definicion);

                    condicionesProcesamientoTextosInformacion.Children.RemoveAt(indice);
                    condicionesProcesamientoTextosInformacion.Children.Insert(indice - 1, TextoDefinicionSeleccionado);
                }

                ActualizarNumerosItems();
            }
        }

        private void moverCondicionADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null &&
                ProcesamientoTextosInformacion.Any())
            {
                CondicionProcesamientoTextosInformacion definicion = ((CondicionProcesamientoTextosInformacion)TextoDefinicionSeleccionado.Tag);

                int indice = ProcesamientoTextosInformacion.IndexOf(definicion);

                if (indice < ProcesamientoTextosInformacion.Count - 1)
                {
                    ProcesamientoTextosInformacion.RemoveAt(indice);
                    ProcesamientoTextosInformacion.Insert(indice + 1, definicion);

                    condicionesProcesamientoTextosInformacion.Children.RemoveAt(indice);
                    condicionesProcesamientoTextosInformacion.Children.Insert(indice + 1, TextoDefinicionSeleccionado);
                }

                ActualizarNumerosItems();
            }
        }

        private void ActualizarNumerosItems()
        {
            foreach (TextBlock texto in condicionesProcesamientoTextosInformacion.Children)
                texto.Text = GenerarTextoCondicion((CondicionProcesamientoTextosInformacion)texto.Tag);
        }

        private void opcionAplicarProcesamientoAntesImplicacionesTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                AplicarProcesamientoAntesImplicacionesTextosInformacion = (bool)opcionAplicarProcesamientoAntesImplicacionesTextosInformacion.IsChecked;
            }
        }

        private void opcionAplicarProcesamientoAntesImplicacionesTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                AplicarProcesamientoAntesImplicacionesTextosInformacion = (bool)opcionAplicarProcesamientoAntesImplicacionesTextosInformacion.IsChecked;
            }
        }

        private void opcionAplicarProcesamientoDespuesImplicacionesTextosInformacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                AplicarProcesamientoDespuesImplicacionesTextosInformacion = (bool)opcionAplicarProcesamientoDespuesImplicacionesTextosInformacion.IsChecked;
            }
        }

        private void opcionAplicarProcesamientoDespuesImplicacionesTextosInformacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                AplicarProcesamientoDespuesImplicacionesTextosInformacion = (bool)opcionAplicarProcesamientoDespuesImplicacionesTextosInformacion.IsChecked;
            }
        }

        private void opcionLogicasTextosAntes_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasTextos_AntesEjecucion = (bool)opcionLogicasTextosAntes_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionLogicasTextosAntes_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasTextos_AntesEjecucion = (bool)opcionLogicasTextosAntes_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionLogicasTextoDespues_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasTextos_DespuesEjecucion = (bool)opcionLogicasTextoDespues_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionLogicasTextoDespues_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasTextos_DespuesEjecucion = (bool)opcionLogicasTextoDespues_EjecucionOperacion.IsChecked;                
            }
        }
    }
}
