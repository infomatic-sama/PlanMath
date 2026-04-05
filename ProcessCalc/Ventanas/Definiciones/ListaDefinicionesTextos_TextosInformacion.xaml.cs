using ProcessCalc.Controles.TextosInformacion;
using ProcessCalc.Entidades;
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
    /// Lógica de interacción para ListaDefinicionesTextos_TextosInformacion.xaml
    /// </summary>
    public partial class ListaDefinicionesTextos_TextosInformacion : Window
    {
        public List<DefinicionTextoNombresCantidades> Definiciones { get; set; }
        public List<DiseñoOperacion> Operandos { get; set; }
        public List<DiseñoElementoOperacion> SubOperandos { get; set; }
        Brush FondoNormal;
        Brush FondoSeleccionado = System.Windows.Media.Brushes.LightBlue;
        TextBlock TextoDefinicionSeleccionado;
        public bool OcultarOpcionesDefiniciones { get; set; }
        public bool OcultarOpcionesOperando { get; set; }
        public bool ModoDefinicionesImplicaciones { get; set; }
        public DiseñoCalculo CalculoAsociado { get; set; }
        public ListaDefinicionesTextos_TextosInformacion()
        {
            InitializeComponent();
        }

        private void agregarDefinicionTexto_Click(object sender, RoutedEventArgs e)
        {
            if(Definiciones != null)
            {
                DefinicionTextoNombresCantidades definicion = new DefinicionTextoNombresCantidades();
                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.Operandos = Operandos;
                establecer.ListaElementos = CalculoAsociado.ElementosOperaciones.Except(establecer.Operandos).ToList();
                establecer.SubOperandos = SubOperandos;
                establecer.TextosNombre = definicion;
                establecer.OcultarOpcionesOperando = OcultarOpcionesOperando;
                establecer.OcultarOpcionesDefiniciones = OcultarOpcionesDefiniciones;
                establecer.ModoDefinicionesImplicaciones = ModoDefinicionesImplicaciones;

                if (definicion != null &&
                    establecer.ShowDialog() == true)
                {
                    Definiciones.Add(definicion);
                    //TextBlock textoDefinicion = new TextBlock();
                    //FondoNormal = Background.Clone();
                    //textoDefinicion.Margin = new Thickness(10);
                    //textoDefinicion.Padding = new Thickness(5);
                    //textoDefinicion.Tag = definicion;
                    //textoDefinicion.Text = GenerarTextoDefinicion(definicion);
                    //textoDefinicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                    //listaDefinicionesTextos.Children.Add(textoDefinicion);
                    Window_Loaded(this, e);
                }
            }
        }

        private void TextoDefinicion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach(TextBlock item in listaDefinicionesTextos.Children)
            {
                item.Background = FondoNormal.Clone();
            }

            ((TextBlock)sender).Background = FondoSeleccionado.Clone();
            TextoDefinicionSeleccionado = (TextBlock)sender;            
        }

        public string GenerarTextoDefinicion(DefinicionTextoNombresCantidades definicion)
        {
            string Texto = string.Empty;
            foreach (var itemOpcion in definicion.OpcionesTextos)
            {
                EtiquetaTextosInformacionNombreCantidades etiquetaCondicion = new EtiquetaTextosInformacionNombreCantidades();
                etiquetaCondicion.DefinicionTextoNombre_ElementoContenedor = definicion;
                etiquetaCondicion.ElementoContenedor = this;
                etiquetaCondicion.OpcionTextos = itemOpcion;
                etiquetaCondicion.MostrarEtiquetaCondiciones();
                Texto += etiquetaCondicion.Texto + "  ";
            }

            return Texto;
        }

        private void editarDefinicionTexto_Click(object sender, RoutedEventArgs e)
        {
            if (TextoDefinicionSeleccionado != null)
            {
                DefinicionTextoNombresCantidades definicion = (DefinicionTextoNombresCantidades)TextoDefinicionSeleccionado.Tag;
                ConjuntoOpciones_NombresCantidades establecer = new ConjuntoOpciones_NombresCantidades();
                establecer.Operandos = Operandos;
                establecer.SubOperandos = SubOperandos;
                establecer.TextosNombre = definicion.ReplicarObjeto();
                establecer.OcultarOpcionesOperando = OcultarOpcionesOperando;
                establecer.OcultarOpcionesDefiniciones = OcultarOpcionesDefiniciones;
                establecer.ModoDefinicionesImplicaciones = ModoDefinicionesImplicaciones;

                if (establecer.ShowDialog() == true)
                {
                    Definiciones.Remove((DefinicionTextoNombresCantidades)TextoDefinicionSeleccionado.Tag);
                    Definiciones.Add(establecer.TextosNombre);
                    Window_Loaded(this, e);
                }
            }
        }

        private void quitarDefinicionTexto_Click(object sender, RoutedEventArgs e)
        {
            if(TextoDefinicionSeleccionado != null)
            {
                if(Definiciones != null)
                {
                    Definiciones.Remove((DefinicionTextoNombresCantidades)TextoDefinicionSeleccionado.Tag);
                    //listaDefinicionesTextos.Children.Remove(TextoDefinicionSeleccionado);
                    Window_Loaded(this, e);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Definiciones != null)
            {
                listaDefinicionesTextos.Children.Clear();

                foreach (var item in Definiciones)
                {
                    TextBlock textoDefinicion = new TextBlock();
                    FondoNormal = Background.Clone();
                    textoDefinicion.Margin = new Thickness(10);
                    textoDefinicion.Padding = new Thickness(5);
                    textoDefinicion.Tag = item;
                    textoDefinicion.Text = GenerarTextoDefinicion(item);
                    textoDefinicion.MouseLeftButtonUp += TextoDefinicion_MouseLeftButtonUp;
                    listaDefinicionesTextos.Children.Add(textoDefinicion);

                    if(TextoDefinicionSeleccionado != null && 
                        item == TextoDefinicionSeleccionado.Tag)
                    {
                        TextoDefinicion_MouseLeftButtonUp(textoDefinicion, null);
                    }
                }
            }
        }
    }
}
