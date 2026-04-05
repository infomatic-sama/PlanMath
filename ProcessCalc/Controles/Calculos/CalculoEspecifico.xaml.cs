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
using ProcessCalc.Entidades;
using ProcessCalc.Vistas;
using ProcessCalc.Controles;

namespace ProcessCalc.Controles
{
    /// <summary>
    /// Lógica de interacción para CalculoEspecifico.xaml
    /// </summary>
    public partial class CalculoEspecifico : UserControl
    {
        private DiseñoCalculo dis;
        public DiseñoCalculo CalculoDiseño 
        {
            get
            {
                return dis;
            }
            set
            {
                nombreCalculo.Text = value.Nombre;
                descripcionCalculo.Text = value.Descricion;

                if (value.EsEntradasArchivo)
                    icono.Visibility = Visibility.Collapsed;
                else
                    iconoEntradas.Visibility = Visibility.Collapsed;

                dis = value;
            }
        }
        public VistaDiseñoOperaciones VistaOperaciones { get; set; }
        public VistaTextosInformacion VistaTextosInformacion { get; set; }
        public CalculoEspecifico()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VistaOperaciones != null)
            {
                VistaOperaciones.CalculoDiseñoSeleccionado = dis;
                VistaOperaciones.ListarEntradas();
                VistaOperaciones.DibujarElementosOperaciones();

                VistaOperaciones.diagrama.Width = VistaOperaciones.CalculoDiseñoSeleccionado.Ancho;
                VistaOperaciones.diagrama.Height = VistaOperaciones.CalculoDiseñoSeleccionado.Alto;

                VistaOperaciones.Ventana.CalculoActual.SubCalculoSeleccionado_Operaciones = VistaOperaciones.CalculoDiseñoSeleccionado;
                VistaOperaciones.MarcarCalculoSeleccionado();
            }
            else if (VistaTextosInformacion != null)
            {
                VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades = dis;
                VistaTextosInformacion.ListarEntradas();
                VistaTextosInformacion.DibujarElementosTextosInformacion(VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades);

                VistaTextosInformacion.diagrama.Width = VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.Ancho;
                VistaTextosInformacion.diagrama.Height = VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades.Alto;

                VistaTextosInformacion.Ventana.CalculoActual.SubCalculoSeleccionado_TextosInformacion = VistaTextosInformacion.CalculoDiseñoSeleccionado_Cantidades;
                VistaTextosInformacion.MarcarCalculoSeleccionado();
            }
        }
    }
}
