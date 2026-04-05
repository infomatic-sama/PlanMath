using ProcessCalc.Entidades.TextosInformacion;
using ProcessCalc.Vistas;
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

namespace ProcessCalc.Ventanas.Definiciones
{
    /// <summary>
    /// Lógica de interacción para DefinicionListaCadenasTexto.xaml
    /// </summary>
    public partial class DefinicionListaCadenasTexto : UserControl
    {
        public VistaTextosInformacion VistaTextos { get; set; }
        private DiseñoListaCadenasTexto disText;
        public DiseñoListaCadenasTexto DiseñoListaCadenasTexto
        {
            get
            {
                return disText;
            }
            set
            {
                nombreDefinicion.Text = value.Nombre;
                disText = value;
            }
        }
        public bool EnDiagrama { get; set; }
        bool haciendo_clic = false;
        public Point PuntoMouseClic { get; set; }
        public DefinicionListaCadenasTexto()
        {
            InitializeComponent();
            botonFondo.Background = Brushes.Transparent;
        }

        public void Clic(bool clickeando = false)
        {
            if (clickeando)
            {
                haciendo_clic = clickeando;
            }

            botonFondo_PreviewMouseLeftButtonDown(this, null);

            if (clickeando)
            {
                haciendo_clic = false;
            }
        }

        private void MostrarOcultarEdicion(bool mostrar)
        {
            //OcultarIconos();

            if (mostrar)
            {
                nombreDefinicion.Visibility = Visibility.Collapsed;
                edicionNombre.Text = disText.Nombre;
                edicionNombre.Visibility = Visibility.Visible;
                btnGuardarNombre.Visibility = Visibility.Visible;
                edicionNombre.Focus();
            }
            else
            {
                btnGuardarNombre.Visibility = Visibility.Collapsed;
                edicionNombre.Visibility = Visibility.Collapsed;
                nombreDefinicion.Visibility = Visibility.Visible;
            }
        }

        private void botonFondo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e != null)
                PuntoMouseClic = e.GetPosition(this);

            if (edicionNombre.Visibility == Visibility.Collapsed)
            {
                if ((e != null && e.ClickCount == 1 && (e.LeftButton == MouseButtonState.Pressed | (VistaTextos != null && VistaTextos.SeleccionandoElemento))) ||
                        haciendo_clic)
                {
                    if (e != null && e.LeftButton == MouseButtonState.Pressed &&
                    !VistaTextos.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Contains(this))
                    {
                        VistaTextos.diagrama_MouseLeftButtonDown(sender, e);
                    }

                    if (!VistaTextos.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementosDiagramaSeleccionados.Any() ||
                        DiseñoListaCadenasTexto == null)
                    {
                        if (DiseñoListaCadenasTexto == null)
                            VistaTextos.diagrama_MouseLeftButtonDown(this, e);

                        VistaTextos.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.e_SeleccionarElemento = e;
                        VistaTextos.ElementoSeleccionado = true;
                        //VistaOperaciones.EstablecerPuntoUbicacionElemento(this, e);
                        VistaTextos.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.DefinicionListaCadenasTextoSeleccionada = this;
                        VistaTextos.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.TipoElementoOperacionSeleccionado = TipoElementoOperacion.Definicion_ListaCadenasTexto;
                        //if (EnDiagrama && DiseñoOperacion != null) VistaOperaciones.EstablecerTextoBotonSalida(DiseñoOperacion.ContieneSalida);
                        VistaTextos.DestacarElementoSeleccionado();
                        //VistaTextos.MostrarOrdenOperando_Elemento(disOper);
                        //VistaTextos.MostrarConfiguracionSeparadores_Elemento(disOper);
                    }
                    else
                    {
                        VistaTextos.CalculoDiseñoSeleccionado_Cantidades.SeleccionTextos.ElementoDiagramaSeleccionadoMover = this;
                    }

                    DragDrop.DoDragDrop(this, (DiseñoTextosInformacion)disText, DragDropEffects.Move);
                }
                else if (e != null && e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                {
                    MostrarOcultarEdicion(true);
                }
                else
                {
                    if ((VistaTextos != null && VistaTextos.ClicDiagrama))
                    {
                        Background = SystemColors.HighlightBrush;
                        botonFondo.Background = SystemColors.HighlightBrush;
                    }
                }
            }
        }

        private void edicionNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter | e.Key == Key.Return)
                btnGuardarNombre_Click(this, null);
        }

        private void btnGuardarNombre_Click(object sender, RoutedEventArgs e)
        {
            DiseñoListaCadenasTexto encontrado = (DiseñoListaCadenasTexto)(from E in VistaTextos.CalculoDiseñoSeleccionado.ElementosTextosInformacion
                                                  where E.GetType() == typeof(DiseñoListaCadenasTexto) && 
                                                  E != disText && E.Nombre != null && (E.Nombre.ToLower().Trim() == edicionNombre.Text.ToLower().Trim())
                                                  select E).FirstOrDefault();
            if (encontrado == null)
            {
                disText.Nombre = edicionNombre.Text;
                nombreDefinicion.Text = disText.Nombre;
                MostrarOcultarEdicion(false);
            }
            else
            {
                MessageBox.Show("Ya existe una definición con este nombre.", "Definición existente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
