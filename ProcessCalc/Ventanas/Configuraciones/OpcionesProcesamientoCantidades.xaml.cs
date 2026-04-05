using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
using ProcessCalc.Entidades.Condiciones;
using ProcessCalc.Entidades.TextosInformacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Lógica de interacción para OpcionesProcesamientoCantidades.xaml
    /// </summary>
    public partial class OpcionesProcesamientoCantidades : Window
    {
        public List<CondicionProcesamientoCantidades> ProcesamientoCantidades { get; set; }
        Brush FondoNormal;
        Brush FondoSeleccionado = System.Windows.Media.Brushes.LightBlue;
        TextBlock TextoDefinicionSeleccionado;
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoOperacion> OperandosElemento { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandosElemento { get; set; }
        public bool OpcionesOperandoNumeros { get; set; }
        public bool OpcionesInsertar { get; set; }
        public bool NoConservarCambiosOperandos_ProcesamientoCantidades { get; set; }
        public bool OpcionesInsertar_OperandosCantidades { get; set; }
        public bool ModoDiseñoOperacion { get; set; }
        public bool MostrarReiniciarAcumulacion { get; set; }
        public List<DiseñoOperacion> ListaElementos { get; set; }
        public bool EjecutarLogicasCantidades_DespuesEjecucion { get; set; }
        public bool EjecutarLogicasCantidades_AntesEjecucion { get; set; }
        public bool ProcesarCantidades_AntesImplicaciones { get; set; }
        public bool ProcesarCantidades_DespuesImplicaciones { get; set; }
        public OpcionesProcesamientoCantidades()
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
            DialogResult = true;
            Close();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarQuitar_CondicionProcesamientoCantidades condicion = new AgregarQuitar_CondicionProcesamientoCantidades();
            condicion.Operandos = Operandos;
            condicion.ListaElementos = ListaElementos;
            condicion.SubOperandos = SubOperandos;
            condicion.Condicion = new CondicionProcesamientoCantidades();
            condicion.OpcionesOperandoNumeros = OpcionesOperandoNumeros;
            condicion.OpcionesInsertar = OpcionesInsertar;
            condicion.OpcionesInsertar_OperandosCantidades = OpcionesInsertar_OperandosCantidades;
            condicion.OperandosElemento = OperandosElemento;
            condicion.SubOperandosElemento = SubOperandosElemento;
            condicion.ModoDiseñoOperacion = ModoDiseñoOperacion;
            condicion.MostrarReiniciarAcumulacion = MostrarReiniciarAcumulacion;

            bool agregada = (bool)condicion.ShowDialog();
            if(agregada)
            {
                ProcesamientoCantidades.Add(condicion.Condicion);
                TextBlock textoDefinicion = new TextBlock();
                FondoNormal = Background.Clone();
                textoDefinicion.Margin = new Thickness(10);
                textoDefinicion.Padding = new Thickness(5);
                textoDefinicion.Tag = condicion.Condicion;
                textoDefinicion.Text = GenerarTextoCondicion(condicion.Condicion);
                textoDefinicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                condicionesProcesamientoCantidades.Children.Add(textoDefinicion);
            }
        }

        private void TextoDefinicion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (TextBlock item in condicionesProcesamientoCantidades.Children)
            {
                item.Background = FondoNormal.Clone();
            }

            ((TextBlock)sender).Background = FondoSeleccionado.Clone();
            TextoDefinicionSeleccionado = (TextBlock)sender;
        }

        private string GenerarTextoCondicion(CondicionProcesamientoCantidades definicion)
        {
            string Texto = string.Empty;            
            Texto = (ProcesamientoCantidades.IndexOf(definicion) + 1).ToString() + 
                " - " + definicion.MostrarEtiquetaCondiciones();
            return Texto;
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null)
            {
                CondicionProcesamientoCantidades definicion = ((CondicionProcesamientoCantidades)TextoDefinicionSeleccionado.Tag);
                
                AgregarQuitar_CondicionProcesamientoCantidades establecer = new AgregarQuitar_CondicionProcesamientoCantidades();
                establecer.Operandos = Operandos;
                establecer.ListaElementos = ListaElementos;
                establecer.SubOperandos = SubOperandos;
                establecer.Condicion = definicion;
                establecer.OpcionesOperandoNumeros = OpcionesOperandoNumeros;
                establecer.OpcionesInsertar = OpcionesInsertar;
                establecer.OpcionesInsertar_OperandosCantidades = OpcionesInsertar_OperandosCantidades;
                establecer.OperandosElemento = OperandosElemento;
                establecer.SubOperandosElemento = SubOperandosElemento;
                establecer.ModoDiseñoOperacion = ModoDiseñoOperacion;
                establecer.MostrarReiniciarAcumulacion = MostrarReiniciarAcumulacion;

                bool editada = (bool)establecer.ShowDialog();

                if(editada)
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
                ProcesamientoCantidades.Remove((CondicionProcesamientoCantidades)TextoDefinicionSeleccionado.Tag);
                condicionesProcesamientoCantidades.Children.Remove(TextoDefinicionSeleccionado);                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarCondiciones();

            opcionNoConservarCambiosProcesamientoCantidades.IsChecked = NoConservarCambiosOperandos_ProcesamientoCantidades;
            opcionLogicasCantidadesAntes_EjecucionOperacion.IsChecked = EjecutarLogicasCantidades_AntesEjecucion;
            opcionLogicasCantidadesDespues_EjecucionOperacion.IsChecked = EjecutarLogicasCantidades_DespuesEjecucion;
            opcionProcesarCantidades_AntesImplicaciones.IsChecked = ProcesarCantidades_AntesImplicaciones;
            opcionProcesarCantidades_DespuesImplicaciones.IsChecked = ProcesarCantidades_DespuesImplicaciones;
        }

        private void opcionNoConservarCambiosProcesamientoCantidades_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                NoConservarCambiosOperandos_ProcesamientoCantidades = (bool)opcionNoConservarCambiosProcesamientoCantidades.IsChecked;
            }
        }

        private void opcionNoConservarCambiosProcesamientoCantidades_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                NoConservarCambiosOperandos_ProcesamientoCantidades = (bool)opcionNoConservarCambiosProcesamientoCantidades.IsChecked;
            }
        }

        private void ListarCondiciones()
        {
            foreach (var item in ProcesamientoCantidades)
            {
                TextBlock textoDefinicion = new TextBlock();
                FondoNormal = Background.Clone();
                textoDefinicion.Margin = new Thickness(10);
                textoDefinicion.Padding = new Thickness(5);
                textoDefinicion.Tag = item;
                textoDefinicion.Text = GenerarTextoCondicion(item);
                textoDefinicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                condicionesProcesamientoCantidades.Children.Add(textoDefinicion);
            }
        }

        private void moverCondicionAIzquierda_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null &&
                ProcesamientoCantidades.Any())
            {
                CondicionProcesamientoCantidades definicion = ((CondicionProcesamientoCantidades)TextoDefinicionSeleccionado.Tag);

                int indice = ProcesamientoCantidades.IndexOf(definicion);

                if (indice - 1 > -1)
                {                    
                    ProcesamientoCantidades.RemoveAt(indice);
                    ProcesamientoCantidades.Insert(indice - 1, definicion);

                    condicionesProcesamientoCantidades.Children.RemoveAt(indice);
                    condicionesProcesamientoCantidades.Children.Insert(indice - 1, TextoDefinicionSeleccionado);
                }

                ActualizarNumerosItems();
            }
        }

        private void moverCondicionADerecha_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null &&
                ProcesamientoCantidades.Any())
            {
                CondicionProcesamientoCantidades definicion = ((CondicionProcesamientoCantidades)TextoDefinicionSeleccionado.Tag);

                int indice = ProcesamientoCantidades.IndexOf(definicion);

                if (indice < ProcesamientoCantidades.Count - 1)
                {
                    ProcesamientoCantidades.RemoveAt(indice);
                    ProcesamientoCantidades.Insert(indice + 1, definicion);

                    condicionesProcesamientoCantidades.Children.RemoveAt(indice);
                    condicionesProcesamientoCantidades.Children.Insert(indice + 1, TextoDefinicionSeleccionado);
                }

                ActualizarNumerosItems();
            }
        }

        private void ActualizarNumerosItems()
        {
            foreach(TextBlock texto in condicionesProcesamientoCantidades.Children)
                texto.Text = GenerarTextoCondicion((CondicionProcesamientoCantidades)texto.Tag);
        }

        private void opcionLogicasCantidadesAntes_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasCantidades_AntesEjecucion = (bool)opcionLogicasCantidadesAntes_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionLogicasCantidadesAntes_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasCantidades_AntesEjecucion = (bool)opcionLogicasCantidadesAntes_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionLogicasCantidadesDespues_EjecucionOperacion_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasCantidades_DespuesEjecucion = (bool)opcionLogicasCantidadesDespues_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionLogicasCantidadesDespues_EjecucionOperacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                EjecutarLogicasCantidades_DespuesEjecucion = (bool)opcionLogicasCantidadesDespues_EjecucionOperacion.IsChecked;                
            }
        }

        private void opcionProcesarCantidades_AntesImplicaciones_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                ProcesarCantidades_AntesImplicaciones = (bool)opcionProcesarCantidades_AntesImplicaciones.IsChecked;
            }
        }

        private void opcionProcesarCantidades_AntesImplicaciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                ProcesarCantidades_AntesImplicaciones = (bool)opcionProcesarCantidades_AntesImplicaciones.IsChecked;
            }
        }

        private void opcionProcesarCantidades_DespuesImplicaciones_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                ProcesarCantidades_DespuesImplicaciones = (bool)opcionProcesarCantidades_DespuesImplicaciones.IsChecked;
            }
        }

        private void opcionProcesarCantidades_DespuesImplicaciones_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                ProcesarCantidades_DespuesImplicaciones = (bool)opcionProcesarCantidades_DespuesImplicaciones.IsChecked;
            }
        }
    }
}
