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
    /// Lógica de interacción para DefinirOpcionesTextosInformacion_Entrada.xaml
    /// </summary>
    public partial class DefinirOpcionesTextosInformacion_Entrada : Window
    {
        public bool OpcionTextoInformacion_Asignacion_AsignacionImplicacion { get; set; }
        public bool OpcionTextoInformacion_Condicion_AsignacionImplicacion { get; set; }
        public Entrada Entrada { get; set; }
        public DefinirOpcionesTextosInformacion_Entrada()
        {
            InitializeComponent();
        }

        //private void ListarOpcionesEntradaBusquedas()
        //{
        //    if (Entrada != null)
        //    {
        //        int indice = 2;
        //        OpcionesSeleccionadasBusquedas = new List<OpcionesSeleccionadas_TextosCondicionAsignacion>();

        //        foreach (var itemBusqueda in Entrada.BusquedasTextosInformacion)
        //        {
        //            OpcionesSeleccionadas_TextosCondicionAsignacion opciones = new OpcionesSeleccionadas_TextosCondicionAsignacion();
        //            opciones.Busqueda = itemBusqueda;
        //            opciones.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = itemBusqueda.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
        //            opciones.OpcionTextoInformacion_Condicion_AsignacionImplicacion = itemBusqueda.OpcionTextoInformacion_Condicion_AsignacionImplicacion;

        //            opcionesEntradaBusquedas.RowDefinitions.Add(new RowDefinition());
        //            opcionesEntradaBusquedas.RowDefinitions.Last().Height = GridLength.Auto;

        //            TextBlock texto = new TextBlock();
        //            texto.Text = "Búsqueda de entrada: " + itemBusqueda.Nombre + ":";
        //            texto.Margin = new Thickness(10);

        //            opcionesEntradaBusquedas.Children.Add(texto);

        //            Grid.SetRow(texto, indice);
        //            Grid.SetColumn(texto, 0);

        //            opcionesEntradaBusquedas.RowDefinitions.Add(new RowDefinition());
        //            opcionesEntradaBusquedas.RowDefinitions.Last().Height = GridLength.Auto;

        //            indice++;

        //            CheckBox opcionTextosCondicion = new CheckBox();
        //            opcionTextosCondicion.Content = "Los textos obtenidos en la búsqueda se utilizarán como texto de condición en las definiciones.";
        //            opcionTextosCondicion.Margin = new Thickness(10);
        //            opcionTextosCondicion.Tag = opciones;
        //            opcionTextosCondicion.IsChecked = itemBusqueda.OpcionTextoInformacion_Condicion_AsignacionImplicacion;
        //            opcionTextosCondicion.Checked += OpcionTextoCondicion_Checked_Busqueda;
        //            opcionTextosCondicion.Unchecked += OpcionTextoCondicion_Unchecked_Busqueda;

        //            opcionesEntradaBusquedas.Children.Add(opcionTextosCondicion);

        //            Grid.SetRow(opcionTextosCondicion, indice);
        //            Grid.SetColumn(opcionTextosCondicion, 0);

        //            opcionesEntradaBusquedas.RowDefinitions.Add(new RowDefinition());
        //            opcionesEntradaBusquedas.RowDefinitions.Last().Height = GridLength.Auto;

        //            indice++;

        //            CheckBox opcionTextosAsignacion = new CheckBox();
        //            opcionTextosAsignacion.Content = "Los textos obtenidos en la entrada se utilizarán como texto de asignación en las definiciones.";
        //            opcionTextosAsignacion.Margin = new Thickness(10);
        //            opcionTextosAsignacion.Tag = opciones;
        //            opcionTextosAsignacion.IsChecked = itemBusqueda.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
        //            opcionTextosAsignacion.Checked += OpcionTextosAsignacion_Checked_Busqueda;
        //            opcionTextosAsignacion.Unchecked += OpcionTextosAsignacion_Unchecked_Busqueda;

        //            opcionesEntradaBusquedas.Children.Add(opcionTextosAsignacion);

        //            Grid.SetRow(opcionTextosAsignacion, indice);
        //            Grid.SetColumn(opcionTextosAsignacion, 0);

        //            indice++;

        //            OpcionesSeleccionadasBusquedas.Add(opciones);

        //            foreach (var itemBusquedaConjunto in itemBusqueda.ConjuntoBusquedas)
        //            {
        //                OpcionesSeleccionadas_TextosCondicionAsignacion opciones_ = new OpcionesSeleccionadas_TextosCondicionAsignacion();
        //                opciones_.Busqueda = itemBusquedaConjunto;
        //                opciones_.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = itemBusquedaConjunto.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
        //                opciones_.OpcionTextoInformacion_Condicion_AsignacionImplicacion = itemBusquedaConjunto.OpcionTextoInformacion_Condicion_AsignacionImplicacion;

        //                opcionesEntradaBusquedas.RowDefinitions.Add(new RowDefinition());
        //                opcionesEntradaBusquedas.RowDefinitions.Last().Height = GridLength.Auto;

        //                TextBlock texto_ = new TextBlock();
        //                texto_.Text = "Búsqueda de entrada: " + itemBusquedaConjunto.Nombre + ":";
        //                texto_.Margin = new Thickness(10);

        //                opcionesEntradaBusquedas.Children.Add(texto_);

        //                Grid.SetRow(texto_, indice);
        //                Grid.SetColumn(texto_, 0);

        //                opcionesEntradaBusquedas.RowDefinitions.Add(new RowDefinition());
        //                opcionesEntradaBusquedas.RowDefinitions.Last().Height = GridLength.Auto;

        //                indice++;

        //                CheckBox opcionTextosCondicion_ = new CheckBox();
        //                opcionTextosCondicion_.Content = "Los textos obtenidos en la búsqueda se utilizarán como texto de condición en las definiciones.";
        //                opcionTextosCondicion_.Margin = new Thickness(10);
        //                opcionTextosCondicion_.Tag = opciones_;
        //                opcionTextosCondicion_.IsChecked = itemBusquedaConjunto.OpcionTextoInformacion_Condicion_AsignacionImplicacion;
        //                opcionTextosCondicion_.Checked += OpcionTextoCondicion_Checked_Busqueda;
        //                opcionTextosCondicion_.Unchecked += OpcionTextoCondicion_Unchecked_Busqueda;

        //                opcionesEntradaBusquedas.Children.Add(opcionTextosCondicion_);

        //                Grid.SetRow(opcionTextosCondicion_, indice);
        //                Grid.SetColumn(opcionTextosCondicion_, 0);

        //                opcionesEntradaBusquedas.RowDefinitions.Add(new RowDefinition());
        //                opcionesEntradaBusquedas.RowDefinitions.Last().Height = GridLength.Auto;

        //                indice++;

        //                CheckBox opcionTextosAsignacion_ = new CheckBox();
        //                opcionTextosAsignacion_.Content = "Los textos obtenidos en la entrada se utilizarán como texto de asignación en las definiciones.";
        //                opcionTextosAsignacion_.Margin = new Thickness(10);
        //                opcionTextosAsignacion_.Tag = opciones_;
        //                opcionTextosAsignacion_.IsChecked = itemBusquedaConjunto.OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
        //                opcionTextosAsignacion_.Checked += OpcionTextosAsignacion_Checked_Busqueda;
        //                opcionTextosAsignacion_.Unchecked += OpcionTextosAsignacion_Unchecked_Busqueda;

        //                opcionesEntradaBusquedas.Children.Add(opcionTextosAsignacion_);

        //                Grid.SetRow(opcionTextosAsignacion_, indice);
        //                Grid.SetColumn(opcionTextosAsignacion_, 0);

        //                indice++;

        //                OpcionesSeleccionadasBusquedas.Add(opciones_);
        //            }
        //        }
        //    }
        //}

        //private void OpcionTextosAsignacion_Unchecked_Busqueda(object sender, RoutedEventArgs e)
        //{
        //    OpcionesSeleccionadas_TextosCondicionAsignacion opciones = (OpcionesSeleccionadas_TextosCondicionAsignacion)((CheckBox)sender).Tag;
        //    if (((CheckBox)sender).IsChecked == false)
        //        opciones.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = false;
        //}

        //private void OpcionTextosAsignacion_Checked_Busqueda(object sender, RoutedEventArgs e)
        //{
        //    OpcionesSeleccionadas_TextosCondicionAsignacion opciones = (OpcionesSeleccionadas_TextosCondicionAsignacion)((CheckBox)sender).Tag;
        //    if (((CheckBox)sender).IsChecked == true)
        //        opciones.OpcionTextoInformacion_Asignacion_AsignacionImplicacion = true;
        //}

        //private void OpcionTextoCondicion_Unchecked_Busqueda(object sender, RoutedEventArgs e)
        //{
        //    OpcionesSeleccionadas_TextosCondicionAsignacion opciones = (OpcionesSeleccionadas_TextosCondicionAsignacion)((CheckBox)sender).Tag;
        //    if (((CheckBox)sender).IsChecked == false)
        //        opciones.OpcionTextoInformacion_Condicion_AsignacionImplicacion = false;
        //}

        //private void OpcionTextoCondicion_Checked_Busqueda(object sender, RoutedEventArgs e)
        //{
        //    OpcionesSeleccionadas_TextosCondicionAsignacion opciones = (OpcionesSeleccionadas_TextosCondicionAsignacion)((CheckBox)sender).Tag;
        //    if (((CheckBox)sender).IsChecked == true)
        //        opciones.OpcionTextoInformacion_Condicion_AsignacionImplicacion = true;
        //}

        private void opcionTextoCondicion_Checked(object sender, RoutedEventArgs e)
        {
            if (opcionTextoCondicion.IsChecked == true)
                OpcionTextoInformacion_Condicion_AsignacionImplicacion = true;
        }

        private void opcionTextoCondicion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (opcionTextoCondicion.IsChecked == false)
                OpcionTextoInformacion_Condicion_AsignacionImplicacion = false;
        }

        private void opcionTextoAsignacion_Checked(object sender, RoutedEventArgs e)
        {
            if (opcionTextoAsignacion.IsChecked == true)
                OpcionTextoInformacion_Asignacion_AsignacionImplicacion = true;
        }

        private void opcionTextoAsignacion_Unchecked(object sender, RoutedEventArgs e)
        {
            if (opcionTextoAsignacion.IsChecked == false)
                OpcionTextoInformacion_Asignacion_AsignacionImplicacion = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            opcionTextoAsignacion.IsChecked = OpcionTextoInformacion_Asignacion_AsignacionImplicacion;
            opcionTextoCondicion.IsChecked = OpcionTextoInformacion_Condicion_AsignacionImplicacion;
            //ListarOpcionesEntradaBusquedas();
        }
    }

    //public class OpcionesSeleccionadas_TextosCondicionAsignacion
    //{
    //    public BusquedaTextoArchivo Busqueda { get; set; }
    //    public bool OpcionTextoInformacion_Asignacion_AsignacionImplicacion { get; set; }
    //    public bool OpcionTextoInformacion_Condicion_AsignacionImplicacion { get; set; }
    //}
}
