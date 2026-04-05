using ProcessCalc.Entidades;
using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Ventanas;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProcessCalc.Controles.Ctrl_Entradas
{
    /// <summary>
    /// Lógica de interacción para SeleccionEntradasEspecifica.xaml
    /// </summary>
    public partial class SeleccionEntradasEspecifica : UserControl
    {
        public DiseñoOperacion Selector { get; set; }
        public DiseñoCalculo Calculo { get; set; }
        public Entradas VistaEntradas { get; set; }
        public SeleccionEntradasEspecifica()
        {
            InitializeComponent();
        }

        private void nombreEntrada_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(IsLoaded)
                Selector.Nombre = nombreEntrada.Text;
        }

        private void subir_Click(object sender, RoutedEventArgs e)
        {
            if (Calculo.EsEntradasArchivo &&
                    Calculo.ElementosOperaciones.Contains(Selector))
            {
                int indiceInicial = 0;
                int indiceFinal = Calculo.ElementosOperaciones.Count - 1;

                int indiceElemento = Calculo.ElementosOperaciones.IndexOf(Selector);

                if ((indiceElemento >= indiceInicial &&
                    indiceElemento <= indiceFinal) &&
                    (indiceElemento - 1 >= indiceInicial &&
                    indiceElemento - 1 <= indiceFinal))
                {
                    Calculo.ElementosOperaciones.RemoveAt(indiceElemento);
                    Calculo.ElementosOperaciones.Insert(indiceElemento - 1, Selector);
                    VistaEntradas.ListarEntradas();

                    Calculo.ReordenarElementos_EntradasGenerales();
                }
            }
        }

        private void bajar_Click(object sender, RoutedEventArgs e)
        {
            int indiceInicial = 0;
            int indiceFinal = Calculo.ElementosOperaciones.Count - 1;

            int indiceElemento = Calculo.ElementosOperaciones.IndexOf(Selector);

            if ((indiceElemento >= indiceInicial &&
                indiceElemento <= indiceFinal) &&
                (indiceElemento + 1 >= indiceInicial &&
                indiceElemento + 1 <= indiceFinal))
            {
                Calculo.ElementosOperaciones.RemoveAt(indiceElemento);
                Calculo.ElementosOperaciones.Insert(indiceElemento + 1, Selector);
                VistaEntradas.ListarEntradas();

                Calculo.ReordenarElementos_EntradasGenerales();
            }
        }

        private void btnOtraEntrada_Click(object sender, RoutedEventArgs e)
        {
            VistaEntradas.AgregarNuevoSelector();
        }

        private void btnQuitarEntrada_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Quitar el selector de forma permanente?", "Quitar entrada", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                VistaEntradas.QuitarSelector(this);
        }

        private void definir_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarEntradasCondiciones seleccionarOrdenar = new SeleccionarEntradasCondiciones();
            seleccionarOrdenar.listaCondiciones.CondicionesEntradas = Selector.CondicionesTextosInformacion_SeleccionEntradas.ToList();
            seleccionarOrdenar.Operandos = Selector.ElementosPosteriores;
            seleccionarOrdenar.ListaElementos = Calculo.ElementosOperaciones.Except(
                                        seleccionarOrdenar.Operandos).ToList();
            seleccionarOrdenar.listaCondiciones.Entradas = VistaEntradas.Calculo.SubCalculoSeleccionado_Entradas.ListaEntradas.Where(i => i.Tipo == TipoEntrada.TextosInformacion).ToList();
            seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida = Selector.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida.ToList();
            seleccionarOrdenar.SeleccionManualEntradas = Selector.SeleccionManualEntradas;
            seleccionarOrdenar.SeleccionCondicionesEntradas = Selector.SeleccionCondicionesEntradas;
            seleccionarOrdenar.DefinicionesListas = VistaEntradas.Calculo.TextosInformacion.ElementosTextosInformacion.Where(
                        i => i.GetType() == typeof(DiseñoListaCadenasTexto)).Select(i => (DiseñoListaCadenasTexto)i).ToList();

            bool definicionEstablecida = (bool)seleccionarOrdenar.ShowDialog();
            if (definicionEstablecida)
            {
                Selector.CondicionesTextosInformacion_SeleccionEntradas = seleccionarOrdenar.listaCondiciones.CondicionesEntradas;
                Selector.AsociacionesCondicionesTextosInformacion_Entradas_ElementosSalida = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida;
                //ElementoDiseñoOperacionSeleccionado.AsociacionesCondicionesFlujo_ElementosSalida2_Entradas = seleccionarOrdenar.listaCondiciones.AsociacionesCondicionesEntradas_ElementosSalida2;
                Selector.SeleccionManualEntradas = seleccionarOrdenar.SeleccionManualEntradas;
                Selector.SeleccionCondicionesEntradas = seleccionarOrdenar.SeleccionCondicionesEntradas;
            }
        }

        private void enlazarEntradas_Click(object sender, RoutedEventArgs e)
        {
            SeleccionManualOperaciones_CondicionEntradas seleccionar = new SeleccionManualOperaciones_CondicionEntradas();
            seleccionar.descripcionCondiciones.Text = "Selector de entradas: " + Selector.NombreCombo +
                ". Selecciona las entradas a enlazar:";
            seleccionar.titulo.Text = "Seleccionar entradas a enlazar al selector";

            seleccionar.Entradas.AddRange(Calculo.ElementosOperaciones.Where(i => i.Tipo == TipoElementoOperacion.Entrada && i.EntradaRelacionada != null).ToList());
            seleccionar.EntradasSeleccionadas.AddRange(Selector.ElementosPosteriores.ToList());

            List<ElementoEntradaEjecucion> entradasSeleccionadas = new List<ElementoEntradaEjecucion>();

            bool digita = (bool)seleccionar.ShowDialog();
            if (digita == true)
            {
                VistaEntradas.QuitarConexionesSelector(this);
                VistaEntradas.AgregarConexionesSelector(this, seleccionar.Entradas);
            }
        }
    }
}
